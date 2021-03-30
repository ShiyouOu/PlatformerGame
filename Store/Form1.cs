using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Mail;
using MyVideo;


// Window size: 816, 489
namespace Store
{
    public partial class Form1 : Form
    {
        // Player Speed
        int intSpeed = 2;
        int intJumpSpeed = 2;
        int intFallSpeed = 3;

        // Booleans to enter testing mode and the mode for creating a new level
        bool testing = false;
        bool creatingNewLevel = false;

        // Whether user has logged in
        bool loggedIn = false;

        // Whether each key is being pressed
        bool upKey = false;
        bool leftKey = false;
        bool rightKey = false;

        // Player is falling (Also prevents certain actions from being performed in midair)
        bool falling = false;

        // Integers for jumping
        int intJumpLimit = 30;
        int startingPosition = 0;
        int currentPicFrame = 0;
        int intJumpCount = 0;

        // Current level and the available levels currently in the game
        int intLevel = 1;
        int maxLevel;

        // the number of platforms and an array containing each platform
        int platformCount = 0;
        int coinCount = 0;
        int spikeCount = 0;
        int effectCount = 0;
        Control[] platforms;
        Control[] coins;
        Control[] spikes;
        Control[] effects;
        int[] effectsTimer;

        // player = username; user = email
        string playerName = "";
        string user = "";

        // The Goal of each level
        Control lblGoal = new PictureBox();

        // Player hitbox
        Rectangle playerBox = new Rectangle();

        /* Allows player to move in certain directions
         * 0 = Open
         * 1 = Blocked
         * [0,1,2,3] == [top,left,down,right]
         */
        int[] intPath = new int[] { 0, 0, 0, 0 };

        // Current Player Action
        string playerAction = "idle";
        string facingDirection = "right";

        // IMAGES
        Image imgPlatform = Properties.Resources.Platform1;
        Image imgGoal = Properties.Resources.Star1;
        Image imgCoin = Properties.Resources.coin;
        Image imgSpike = Properties.Resources.Spike;

        // ANIMATIONS
        int intFrames = 0;
        Image[] runningAnim = new Image[] { Properties.Resources.run0, Properties.Resources.run1, Properties.Resources.run2, Properties.Resources.run3, Properties.Resources.run4, Properties.Resources.run5 };
        Image[] lrunningAnim = new Image[] { Properties.Resources.lrun0, Properties.Resources.lrun1, Properties.Resources.lrun2, Properties.Resources.lrun3, Properties.Resources.lrun4, Properties.Resources.lrun5 };
        Image[] idleAnim = new Image[] { Properties.Resources.idle0, Properties.Resources.idle1, Properties.Resources.idle2};
        Image[] lidleAnim = new Image[] { Properties.Resources.lidle0, Properties.Resources.lidle1, Properties.Resources.lidle3 };
        Image[] fallingAnim = new Image[] { Properties.Resources.fall0, Properties.Resources.fall1 };
        Image[] lfallingAnim = new Image[] { Properties.Resources.lfall0, Properties.Resources.lfall1 };
        Image[] attack0 = new Image[] {Properties.Resources.attack0, Properties.Resources.attack1, Properties.Resources.attack2, Properties.Resources.attack3 };

        // Game Currency and Controls
        int intCoins = 0;
        int origCoins = 0;
        int intLives = 3;

        // MUSIC
        Media bgMusic = new Media();
        bool music = true;

        public Form1()
        {
            InitializeComponent();
        }

        // Load save data if found
        private void GameStart()
        {
            restartToolStripMenuItem1.Enabled = true;
            saveToolStripMenuItem1.Enabled = true;
            logoutToolStripMenuItem1.Enabled = true;
            if (user == "admin") {
                createNewLevelToolStripMenuItem.Enabled = true;
            }
            try
            {
                StreamReader sr = new StreamReader("Saves/"+user + "_save.csv");
                while (sr.Peek() != -1)
                {
                    string line = sr.ReadLine();
                    string[] values = line.Split(',');

                    switch (values[0])
                    {
                        case "Current Level":
                            intLevel = Convert.ToInt32(values[1]);
                            LoadLevel(intLevel);
                            break;
                        case "Gold":
                            intCoins = Convert.ToInt32(values[1]);
                            origCoins = intCoins;
                            break;
                        case "Lives":
                            intLives = Convert.ToInt32(values[1]);
                            break;
                    }
                }
                sr.Close();
            }
            catch
            {
                LoadLevel(1);
            }
            // Set hitbox size and height
            playerBox.Y = 224;
            playerBox.X = 36;
            playerBox.Width = 21;
            playerBox.Height = 28;
            bgMusic.Play();
        }

        // Reads the leaderboard.txt file and outputs it to the leaderboard listbox
        private void LoadLeaderboard()
        {
            StreamReader sr = new StreamReader("Leaderboard.txt");
            lstLeaderboard.Items.Clear();
            while (sr.Peek() != -1)
            {
                string line = sr.ReadLine();
                string[] values = line.Split(',');
                if (line.Trim() != "")
                {
                    lstLeaderboard.Items.Add(values[1] + ": " + values[0]);
                }
            }
            sr.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Sets maxLevel to the number of csv files in the levels directory
            maxLevel = Directory.GetFiles("Levels/", "*csv", SearchOption.TopDirectoryOnly).Length;
            // Open the music file
            bgMusic.Open("music.wma", bgMusicBox);
            bgMusic.Repeat = true;
            // Loads the leaderboard
            LoadLeaderboard();
            // Changes the mode of the application for easy testing
            if (creatingNewLevel)
            {
                createNewLevelToolStripMenuItem.Enabled = true;
                SetTabControl(new TabPage[] { tabPageGame});
                FindObjects();
            }
            else if (testing)
            {
                user = "admin";
                playerName = "Admin";
                loggedIn = true;
                GameStart();
                SetTabControl(new TabPage[] { tabPageGame});
            }
            else
            {
                SetTabControl(new TabPage[] { tabPageLogin});
            }
        }

        // Go to registration page
        private void lblToRegistration_Click(object sender, EventArgs e)
        {
            SetTabControl(new TabPage[] { tabPageRegistration });
        }

        // Go to login page
        private void lblToLogin_Click(object sender, EventArgs e)
        {
            SetTabControl(new TabPage[] { tabPageLogin });
        }

        // Login
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "user" && txtPassword.Text == "password")
            {
                loggedIn = true;
                user = "admin";
                playerName = "Admin";
                SetTabControl(new TabPage[] { tabPageGame });
            }
            else
            {
                StreamReader sr = new StreamReader("accounts.csv");

                while (sr.Peek() != -1)
                {
                    string line = sr.ReadLine();
                    string[] values = line.Split(',');

                    if (values[0].Trim().ToLower() == txtName.Text.Trim().ToLower() && values[2].Trim() == txtPassword.Text.Trim())
                    {
                        loggedIn = true;
                        playerName = values[0].Trim();
                        user = values[1].Trim();
                        SetTabControl(new TabPage[] { tabPageGame });
                    }
                }
                sr.Close();
            }
            if (user != "")
            {
                GameStart();
            }
        }

        // Takes 6 arguments and creates a new picturebox
        private void CreateObject(string name, int top, int left, int width, int height, Image image, bool stretch)
        {
            PictureBox myPicture = new PictureBox();
            myPicture.Name = name;
            myPicture.Top = top;
            myPicture.Left = left;
            myPicture.Width = width;
            myPicture.Height = height;
            myPicture.Image = image;
            if (stretch)
            {
                myPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            tabPageGame.Controls.Add(myPicture);
        }

        // Reset temporary level variables
        private void ResetVariables()
        {
            upKey = false;
            rightKey = false;
            leftKey = false;
            falling = false;
            intFrames = 0;
            intJumpCount = 0;
            effectCount = 0;
        }

        // Removes all objects on the game screen
        private void ResetScreen()
        {
            tabPageGame.Controls.Remove(lblGoal);

            foreach (PictureBox i in platforms)
            {
                tabPageGame.Controls.Remove(i);
            }
            foreach (PictureBox i in coins)
            {
                if (i == null)
                {
                    continue;
                }
                tabPageGame.Controls.Remove(i);
            }
            foreach (Label i in effects)
            {
                if (i == null)
                {
                    break;
                }
                tabPageGame.Controls.Remove(i);
            }
            foreach (PictureBox i in spikes)
            {
                if (i == null)
                {
                    break;
                }
                tabPageGame.Controls.Remove(i);
            }
        }

        // Has player collided with obj?
        private bool Collide(Control obj)
        {
            Rectangle tempRectangle = new Rectangle(
                obj.Location.X,
                obj.Location.Y,
                obj.Width,
                obj.Height
            );
            if (tempRectangle.IntersectsWith(playerBox))
            {
                return true;
            }
            return false;
        }

        // Sets each index in the given array to the control found
        private void SetArray(string name, Control[] array, int length)
        {
            for (int i = 0; i < length; i++)
            {
                Control[] attempt = this.Controls.Find(name + i, true);
                if (attempt != null && attempt.Length > 0)
                {
                    array[i] = attempt[0];
                }
            }
        }

        // Finds the goal and calls the SetArray() method for each object specific array
        private void FindObjects()
        {
            platforms = new PictureBox[platformCount];
            coins = new PictureBox[coinCount];
            spikes = new PictureBox[spikeCount];

            Control[] goal = this.Controls.Find("Goal", true);

            if (goal != null && goal.Length > 0)
            {
                lblGoal = goal[0];
            }
            SetArray("platform", platforms, platformCount);
            SetArray("coin", coins, coinCount);
            SetArray("spike", spikes, spikeCount);

        }

        // 4 functions that check whether each side of the player is currently colliding
        private bool TopCollide(PictureBox i)
        {
            if ((playerBox.Top <= i.Bottom && playerBox.Top > i.Top) && playerBox.Left - i.Left < i.Width && playerBox.Right - i.Left > 0)
            {
                return true;
            }
            return false;
        }
        private bool LeftCollide(PictureBox i)
        {
            if (playerBox.Left <= i.Right && playerBox.Left > i.Left && playerBox.Bottom > i.Top && playerBox.Top < i.Bottom)
            {
                return true;
            }
            return false;
        }
        private bool RightCollide(PictureBox i)
        {
            if (playerBox.Right >= i.Left && playerBox.Left < i.Left && playerBox.Bottom > i.Top && playerBox.Top < i.Bottom)
            {
                return true;
            }
            return false;
        }
        private bool BottomCollide(PictureBox i)
        {
            if (playerBox.Bottom >= i.Top && playerBox.Top < i.Bottom && playerBox.Left - i.Left < i.Width && playerBox.Right - i.Left > 0)
            {
                return true;
            }
            return false;
        }

        // Sets an array with 4 indexs equal to 1 or 0 to allow player to move in certain directions
        private void CheckPath()
        {
            int[] tempArray = new int[] { 0, 0, 0, 0 };
            foreach (PictureBox i in platforms)
            {
                if (TopCollide(i))
                {
                    tempArray[0] = 1;
                }
                if (LeftCollide(i))
                {
                    tempArray[1] = 1;
                }
                if (BottomCollide(i))
                {
                    tempArray[2] = 1;
                }
                if (RightCollide(i))
                {
                    tempArray[3] = 1;
                }
            }
            intPath = tempArray;
        }

        // The player is falling!
        private void Fall()
        {
            if (intPath[2] == 0)
            {
                playerBox.Y += intFallSpeed;
                foreach (PictureBox i in platforms)
                {
                    // Makes sure the player doesn't start clipping inside collidables
                    if (BottomCollide(i))
                    {
                        playerBox.Y = i.Top - playerBox.Height;
                        falling = false;
                    }
                }
            }
        }

        // The player is jumping!
        private void Jump()
        {
            if (Math.Abs(startingPosition) - playerBox.Top < intJumpLimit && intPath[0] == 0)
            {
                playerBox.Y -= intJumpSpeed;
                //ChangePlayerState("jumping");
            }
            else
            {
                upKey = false;
                foreach (PictureBox i in platforms)
                {
                    // Makes sure the player doesn't start clipping into the collidables
                    if (TopCollide(i))
                    {
                        playerBox.Y = i.Bottom;
                    }
                }
            }
        }

        // Coin Effects
        private void CreateEffect(Color col, int top, int left, string text) {
            Label eff = new Label();
            eff.ForeColor = col;
            eff.Top = top - 20;
            eff.Left = left - 10;
            eff.AutoSize = true;
            eff.Text = text;
            eff.Font = new Font("Microsoft Sans Serif", Convert.ToUInt32(10), FontStyle.Bold);

            effects[effectCount] = eff;
            effectsTimer[effectCount] = eff.Top;
            effectCount++;
            tabPageGame.Controls.Add(eff);
        }

        // Takes care of player animation
        private void AnimatePlayer()
        {
            if (intFrames % 8 == 0 || intFrames == 0)
            {
                switch (playerAction)
                {
                    case "running":
                        if (facingDirection == "right")
                        {
                            Player.Image = runningAnim[currentPicFrame];
                        }
                        else
                        {
                            Player.Image = lrunningAnim[currentPicFrame];
                        }
                        currentPicFrame++;
                        if (currentPicFrame > 5)
                        {
                            currentPicFrame = 0;
                        }
                        break;
                    case "idle":
                        if (facingDirection == "right")
                        {
                            Player.Image = idleAnim[currentPicFrame];
                        }
                        else
                        {
                            Player.Image = lidleAnim[currentPicFrame];
                        }
                        currentPicFrame++;
                        if (currentPicFrame > 2)
                        {
                            currentPicFrame = 0;
                        }
                        break;
                    case "falling":
                        if (facingDirection == "right")
                        {
                            Player.Image = fallingAnim[currentPicFrame];
                        }
                        else
                        {
                            Player.Image = lfallingAnim[currentPicFrame];
                        }
                        currentPicFrame++;
                        if (currentPicFrame > 1)
                        {
                            currentPicFrame = 0;
                        }
                        break;
                }
            }
        }

        // Player is doing something else now
        private void ChangePlayerState(string state) {
            if (playerAction != state) {
                playerAction = state;
                currentPicFrame = 0;
            }
        }

        // Player has lost died
        private void PlayerDied() {
            timer1.Enabled = false;
            playerBox.Y = 224;
            playerBox.X = 36;
            falling = false;
            intFrames = 0;
            intJumpCount = 0;
            intLives--;
            timer1.Enabled = true;
        }

        // How did the player do?
        private void CalculateResults(bool win) {
            lblCoinsCollected.Text = intCoins.ToString();
            lblLivesLeft.Text = intLives.ToString();
            if (win)
            {
                lblLevelsPassed.Text = (intLevel).ToString();
                lblScore.Text = ((intCoins * 10) + ((intLevel) * 50) + (intLives * 100)).ToString();
            }
            else
            {
                lblLevelsPassed.Text = (intLevel - 1).ToString();
                lblScore.Text = ((intCoins * 10) + ((intLevel - 1) * 50) + (intLives * 100)).ToString();
            }

            // Reads and write leaderboard file to an temporary array then sort it
            StreamReader sr = new StreamReader("Leaderboard.txt");
            string tempString = "";
            while (sr.Peek() != -1) {
                tempString += sr.ReadLine() + ";";
            }
            tempString += lblScore.Text + "," + playerName;
            string[] tempArray = tempString.Split(';');
            Array.Sort(tempArray);
            Array.Reverse(tempArray);

            // Max of 8 entries
            for (int i = 0; i < tempArray.Length; i++) {
                if (i > 7) {
                    tempArray[i] = null;
                }
            }
            sr.Close();

            // Overwrite the leaderboard file with the sorted array
            StreamWriter sw = new StreamWriter("Leaderboard.txt");
            foreach (string i in tempArray)
            {
                sw.WriteLine(i);
            }
            sw.Close();
            LoadLeaderboard();
        }

        // Game Over...Player either lost all lives or won
        private void GameOver(bool win) {
            if (win)
            {
                bgMusic.Stop();
                CalculateResults(win);
                SetTabControl(new TabPage[] { tabPageResult });
                StreamWriter sw = new StreamWriter("Saves/" + user + "_save.csv");
                sw.WriteLine("Current Level, " + 1);
                sw.WriteLine("Gold, " + 0);
                sw.WriteLine("Lives, " + 3);
                sw.Close();
                ResetScreen();
                ResetVariables();
            }
            else
            {
                bgMusic.Stop();
                CalculateResults(win);
                SetTabControl(new TabPage[] {tabPageResult});
                StreamWriter sw = new StreamWriter("Saves/" + user + "_save.csv");
                sw.WriteLine("Current Level, " + 1);
                sw.WriteLine("Gold, " + 0);
                sw.WriteLine("Lives, " + 3);
                sw.Close();
                ResetScreen();
                ResetVariables();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Player.SendToBack();
            CheckPath();
            intFrames++;
            // If path is clear then proceed (Left and Right movement)
            if (rightKey)
            {
                if (intPath[3] == 0)
                {
                    playerBox.X += intSpeed;
                    foreach (PictureBox i in platforms)
                    {
                        if (RightCollide(i))
                        {
                            playerBox.X = i.Left - playerBox.Width;
                        }
                    }
                }
            }
            if (leftKey)
            {
                if (playerBox.X > 0 && intPath[1] == 0)
                {
                    playerBox.X -= intSpeed;
                    foreach (PictureBox i in platforms)
                    {
                        if (LeftCollide(i))
                        {
                            playerBox.X = i.Right;
                        }
                    }
                }
            }
            // if the player is standing on a platform then allow jumping
            if (intPath[2] == 1)
            {
                startingPosition = 0;
                intJumpCount = 0;
            }
            if (intPath[2] == 1 && !leftKey && !rightKey)
            {
                ChangePlayerState("idle");
            }
            else if (intPath[2] == 1 && (leftKey || rightKey) && playerAction != "attacking")
            {
                ChangePlayerState("running");
            }
            //player is trying to jump
            if (upKey)
            {
                if (intJumpCount == 0)
                {
                    intJumpCount = 1;
                    startingPosition = playerBox.Top;
                }
                Jump();
            }
            if (intPath[2] == 0 && !upKey)
            {
                falling = true;
                ChangePlayerState("falling");
                Fall();
            }
            // Refresh the player's hitbox
            Player.Top = playerBox.Bottom - Player.Height;
            Player.Left = playerBox.X - ((Player.Width - playerBox.Width) / 2);
            // refresh player image to give animation effect
            AnimatePlayer();

            // Has the player touched a coin?
            for (int i = 0; i < coinCount; i++)
            {
                if (coins[i] == null)
                {
                    continue;
                }
                if (Collide(coins[i]))
                {
                    CreateEffect(Color.Yellow, coins[i].Top, coins[i].Left, "+ " + 1);
                    tabPageGame.Controls.Remove(coins[i]);
                    coins[i] = null;
                    intCoins ++;
                }
            }
            for (int i = 0; i < spikeCount; i++)
            {
                if (spikes[i] == null)
                {
                    continue;
                }
                if (Collide(spikes[i]))
                {
                    PlayerDied();
                }
            }
            // Has the player collided with the goal?
            if (Collide(lblGoal))
            {
                timer1.Enabled = false;
                NextLevel();
            }

            //  Did the player fall off the map?
            if (playerBox.Top > 300)
            {
                PlayerDied();
            }
            lblLives.Text = "Lives: " + intLives.ToString();
            lblCoins.Text = "X " + intCoins.ToString();

            // move the effects
            for (int i = 0; i < effectCount; i++) {
                if (effects[i] == null)
                {
                    break;
                }
                effects[i].Top -= 1;
                if (effectsTimer[i] - effects[i].Top >= 30) {
                    tabPageGame.Controls.Remove(effects[i]);
                }
            }

            // uh oh player has lost
            if (intLives <= 0) {
                timer1.Enabled = false;
                GameOver(false);
            }
        }

        // Player has pushed down a key
        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (loggedIn)
            {
                switch (e.KeyCode)
                {
                    case Keys.W:
                        if (!falling)
                        {
                            upKey = true;
                        }
                        break;
                    case Keys.A:
                        rightKey = false;
                        leftKey = true;
                        facingDirection = "left";
                        ChangePlayerState("running");
                        break;
                    case Keys.D:
                        leftKey = false;
                        rightKey = true;
                        facingDirection = "right";
                        if (playerAction != "attacking")
                        {
                            ChangePlayerState("running");
                        }
                        break;
                }
            }
        }

        // Player has released a key
        private void tabControl1_KeyUp(object sender, KeyEventArgs e)
        {
            if (loggedIn)
            {
                switch (e.KeyCode)
                {
                    case Keys.A:
                        leftKey = false;
                        if (!rightKey)
                        {
                            ChangePlayerState("idle");
                        }
                        break;
                    case Keys.D:
                        rightKey = false;
                        if (!leftKey)
                        {
                            ChangePlayerState("idle");
                        }
                        break;
                    case Keys.W:
                        upKey = false;
                        break;
                }
            }
        }

        // Restart the current level
        private void RestartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            playerBox.Y = 224;
            playerBox.X = 36;
            ResetVariables();
            timer1.Enabled = true;
        }

        // Create a new level file with the current screen
        private void CreateLevelFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
            timer1.Enabled = false;
            PicVolumeControl_Click(sender, e);
        }

        // Player has proceeded to the next level
        private void NextLevel()
        {
            ResetScreen();
            ResetVariables();
            playerBox.Y = 224;
            playerBox.X = 36;
            origCoins = intCoins;
            if (intLevel == maxLevel)
            {
                timer1.Enabled = false;
                GameOver(true);
            }
            else
            {
                intLevel++;
                LoadLevel(intLevel);
                timer1.Enabled = true;
            }
        }

        // Add the tabPages inside the array to the tabControl and remove all others
        private void SetTabControl(TabPage[] visibleTabs) {
            tabControl1.TabPages.Clear();
            foreach (TabPage i in visibleTabs) {
                tabControl1.TabPages.Add(i);
            }
        }

        // Load a new level
        private void LoadLevel(int num)
        {
            StreamReader sr = new StreamReader("Levels/level" + num + ".csv");
            platformCount = 0;
            coinCount = 0;
            spikeCount = 0;
            effects = new Label[5000];
            effectsTimer = new int[5000];

            while (sr.Peek() != -1)
            {
                string line = sr.ReadLine();
                string[] values = line.Split(',');
                switch (values[0].Trim())
                {
                    case "Goal":
                        CreateObject("Goal", Convert.ToInt32(values[1]), Convert.ToInt32(values[2]), 15, 15, imgGoal, false);
                        break;
                    case "Platform":
                        CreateObject("platform" + platformCount, Convert.ToInt32(values[1]), Convert.ToInt32(values[2]), Convert.ToInt32(values[3]), Convert.ToInt32(values[4]), imgPlatform, false);
                        platformCount++;
                        break;
                    case "Coin":
                        CreateObject("coin" + coinCount, Convert.ToInt32(values[1]), Convert.ToInt32(values[2]), 10, 10, imgCoin, true);
                        coinCount++;
                        break;
                    case "Spike":
                        CreateObject("spike" + spikeCount, Convert.ToInt32(values[1]), Convert.ToInt32(values[2]), 10, 10, imgSpike, true);
                        spikeCount++;
                        break;
                }
            }
            sr.Close();
            FindObjects();
            timer1.Enabled = true;
        }

        // is the email taken?
        private bool CheckSave(string email, string path) {
            StreamReader sr = new StreamReader(path);
            while (sr.Peek() != -1)
            {
                string line = sr.ReadLine();
                string[] values = line.Split(',');

                if (values[1] == email)
                {
                    sr.Close();
                    return true;
                }
            }
            sr.Close();
            return false;
        }

        // Save the current level
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter("Saves/" + user+"_save.csv");
            sw.WriteLine("Current Level, " + intLevel);
            sw.WriteLine("Gold, " + origCoins);
            sw.WriteLine("Lives, " + intLives);
            sw.Close();
        }

        //CREDITS
        private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("CREDITS\n\nPlayer Images\n   Link: https://rvros.itch.io/animated-pixel-hero \n  Author Name: rvros\n\nItem Images\n    Link: https://shikashiassets.itch.io/shikashis-fantasy-icons-pack \n   Author Name: shikashiassets\n\nBackground Image\n    Link:https://bayat.itch.io/platform-game-assets \n Author Name:  Bayat Games");
        }

        // Register a new account
        private void BtnRegister_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader("accounts.csv");
            bool exists = CheckSave(txtREmail.Text, "accounts.csv");

            if (exists){
                MessageBox.Show("Email is already bound with an account");
            }

            sr.Close();
            if (!exists)
            {
                bool success = false;

                MailMessage myMail = new MailMessage();

                myMail.From = new MailAddress("cist0265@gmail.com", "Shifuu");

                myMail.To.Add(txtREmail.Text);

                myMail.Subject = "Account Creation";
                myMail.Body = "Your account has been created\nPlease login to begin playing the game";
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;

                smtp.Credentials = new NetworkCredential("cist0265@gmail.com", "cist2019");

                // Send email
                try
                {
                    smtp.Send(myMail);
                    success = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                if (success)
                {
                    StreamWriter sw = new StreamWriter("accounts.csv", true);

                    sw.WriteLine("{0}, {1}, {2}", txtRName.Text, txtREmail.Text, txtRPassword.Text);

                    sw.Close();

                    MessageBox.Show("Your account has been created please proceed to login");
                }
                txtRName.Text = "";
                txtREmail.Text = "";
                txtRPassword.Text = "";
                lblToLogin_Click(sender, e);
            }
        }

        // Log out the user and reset all variables
        private void LogOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bgMusic.Stop();
            restartToolStripMenuItem1.Enabled = false;
            saveToolStripMenuItem1.Enabled = false;
            logoutToolStripMenuItem1.Enabled = false;
            createNewLevelToolStripMenuItem.Enabled = false;
            ResetScreen();
            ResetVariables();
            timer1.Enabled = false;
            txtName.Text = "";
            txtPassword.Text = "";
            user = "";
            playerName = "";
            loggedIn = false;
            playerBox.Y = 224;
            playerBox.X = 36;
            intLevel = 1;
            SetTabControl(new TabPage[] {tabPageLogin});
        }

        // Allows admin account to teleport on click to allow easy testing
        private void TabPageGame_MouseClick(object sender, MouseEventArgs e)
        {
            if (user == "admin")
            {
                playerBox.Location = e.Location;
            }
        }

        // Controls message box
        private void controlsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("CONTROLS\n\nW = Jump\nA = Left\nD = Right");
        }
        
        // Game Objectives message box
        private void obectivesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Collect Coins and progress through each level.\n\n Beat the game by completing the last level, currently the game has " + maxLevel + " levels.");
        }

        // Sites I got the images from
        private void PlayerImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Url = new Uri("http://rvros.itch.io/animated-pixel-hero");
            if (user != "")
            {
                SetTabControl(new TabPage[] { tabPageGame, tabPageBrowser });
            }
            else
            {
                SetTabControl(new TabPage[] { tabPageLogin, tabPageBrowser });
            }
            tabControl1.SelectTab(tabPageBrowser);
        }

        private void itemImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Url = new Uri("http://shikashiassets.itch.io/shikashis-fantasy-icons-pack");
            if (user != "")
            {
                SetTabControl(new TabPage[] { tabPageGame, tabPageBrowser });
            }
            else
            {
                SetTabControl(new TabPage[] { tabPageLogin, tabPageBrowser });
            }
            tabControl1.SelectTab(tabPageBrowser);
        }

        private void BackgroundImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Url = new Uri("http://bayat.itch.io/platform-game-assets");
            if (user != "")
            {
                SetTabControl(new TabPage[] { tabPageGame, tabPageBrowser });
            }
            else
            {
                SetTabControl(new TabPage[] { tabPageLogin, tabPageBrowser });
            }
            tabControl1.SelectTab(tabPageBrowser);
        }

        // Music Control
        private void PicVolumeControl_Click(object sender, EventArgs e)
        {
            if (music)
            {
                music = false;
                picVolumeControl.Image = Properties.Resources.SoundOff;
                bgMusic.Volume = 0;
            }
            else
            {
                music = true;
                picVolumeControl.Image = Properties.Resources.SoundOn;
                bgMusic.Volume = 1000;
            }
        }

        private void PicRestart_Click(object sender, EventArgs e)
        {
            RestartToolStripMenuItem_Click(sender, e);
        }

        private void PicHelp_Click(object sender, EventArgs e)
        {
            controlsToolStripMenuItem_Click(sender, e);
        }

        // Play again
        private void BtnPlay_Click(object sender, EventArgs e)
        {
            SetTabControl(new TabPage[] { tabPageGame });
            GameStart();
        }

        // Sends the score to the email address associated with the logged in account
        private void BtnSendScore_Click(object sender, EventArgs e)
        {
            MailMessage myMail = new MailMessage();

            myMail.From = new MailAddress("cist0265@gmail.com", "Shifuuu");

            if (user != "admin")
            {
                myMail.To.Add(user);
            }
            else
            {
                myMail.To.Add("SHO23@pitt.edu");
            }

            myMail.Subject = "Game Score";
            myMail.Body = "Good Job you managed to pass " + lblLevelsPassed.Text + " levels and finished with a score of " + lblScore.Text + ".";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;

            smtp.Credentials = new NetworkCredential("cist0265@gmail.com", "cist2019");

            // Send email
            try
            {
                smtp.Send(myMail);
                MessageBox.Show("Your score has been sent to the email associated with this account");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}