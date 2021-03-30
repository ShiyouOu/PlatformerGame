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

namespace Store
{
    public partial class Form2 : Form
    {
        // Number of existing levels
        int levels = 0;

        // Integers keeping track of the number of each object
        int platformCount = 0;
        int coinCount = 0;
        int spikeCount = 0;

        // Arrays for the objects
        Control[] platforms;
        Control[] coins;
        Control[] spikes;
        Control lblGoal;

        // Has a goal already been created?
        bool goalCreated = false;

        // IMAGES
        Image imgPlatform = Properties.Resources.Platform1;
        Image imgGoal = Properties.Resources.Star1;
        Image imgCoin = Properties.Resources.coin;
        Image imgSpike = Properties.Resources.Spike;

        // Clicked control
        Control objectSelected;
        Control lastSelected;
        public Form2()
        {
            InitializeComponent();
        }

        // Form2 has loaded
        private void Form2_Load(object sender, EventArgs e)
        {
            // Search the Levels directory for the number of .csv files (number of levels)
            levels = Directory.GetFiles("Levels/", "*csv", SearchOption.TopDirectoryOnly).Length;
            numLevel.Value = levels + 1;

            // Adds the levels to File => Load => to allow you to load and edit previous levels
            for (int i = 0; i < levels; i++) {
                loadToolStripMenuItem.DropDownItems.Add("Level " + (i + 1));

            }
        }

        // Sets each index in the given array to the control found
        private void SetArray(string name, Control[] array, int length) {
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

        // Takes 6 arguments and creates a new picturebox
        // Adds a new mouse down event associated with the created picture box
        private PictureBox CreateObject(string name, int top, int left, int width, int height, Image image, bool stretch)
        {
            loadToolStripMenuItem.Enabled = false;
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

            tabPageCreateLevel.Controls.Add(myPicture);
            myPicture.MouseDown += (tempSender, EventArgs) => { ObjectClicked(myPicture); };
            return (myPicture);
        }

        // A created picture box has been clicked
        private void ObjectClicked(Control obj)
        {
            if (objectSelected == obj)
            {
                objectSelected = null;
                tabPageCreateLevel.Capture = false;
            }
            else {
                objectSelected = obj;
                lastSelected = obj;
                tabPageCreateLevel.Capture = true;
                try
                {
                    // If the clicked control is a platform enable the tbars to allow size adjustments
                    if (obj.Name.Substring(0, 8) == "platform")
                    {
                        tbarHeight.Enabled = true;
                        tbarWidth.Enabled = true;
                        tbarHeight.Value = obj.Height;
                        tbarWidth.Value = obj.Width;
                    }
                    else
                    {
                        // Clicked object was not a platform
                        tbarHeight.Enabled = false;
                        tbarWidth.Enabled = false;
                        tbarHeight.Value = 10;
                        tbarWidth.Value = 10;
                    }
                }
                catch
                {
                    // if error disable tbars
                    tbarHeight.Enabled = false;
                    tbarWidth.Enabled = false;
                    tbarHeight.Value = 10;
                    tbarWidth.Value = 10;
                }
            }
        }

        // Create a goal if there isnt already one
        private void GoalImage_DoubleClick(object sender, EventArgs e)
        {
            if (!goalCreated)
            {
                goalCreated = true;
                PictureBox newGoal = CreateObject("Goal", 80, 250, 15, 15, imgGoal, false);
            }
        }

        // Create a new coin
        private void CoinImage_DoubleClick(object sender, EventArgs e)
        {
            PictureBox newCoin = CreateObject("coin" + coinCount, 80, 250, 10, 10, imgCoin, true);
            coinCount++;
        }

        // Create a new object
        private void PlatformImage_DoubleClick(object sender, EventArgs e)
        {
            PictureBox newPlatform = CreateObject("platform" + platformCount, 80, 250, 68, 35, imgPlatform, false);
            platformCount++;
        }

        // Create a new spike
        private void SpikeImage_DoubleClick(object sender, EventArgs e)
        {
            PictureBox newSpike = CreateObject("spike" + spikeCount, 80, 250, 10, 10, imgSpike, true);
            spikeCount++;
        }

        // Save the create level to a .csv file
        private void SaveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FindObjects();
            var sb = new StringBuilder();

            var goalLine = string.Format("Goal, {0}, {1}", lblGoal.Top, lblGoal.Left);
            sb.AppendLine(goalLine);
            MessageBox.Show(lblGoal.Name + " Was Saved");
            foreach (PictureBox i in platforms)
            {
                var newLine = string.Format("Platform , {0}, {1}, {2}, {3}", i.Top, i.Left, i.Width, i.Height);
                sb.AppendLine(newLine);
                MessageBox.Show(i.Name + " Was Saved");
            }

            foreach (PictureBox i in coins)
            {
                var newLine = string.Format("Coin , {0}, {1}, {2}, {3}", i.Top, i.Left, 10, 10);
                sb.AppendLine(newLine);
                MessageBox.Show(i.Name + " Was Saved");
            }

            foreach (PictureBox i in spikes)
            {
                var newLine = string.Format("Spike , {0}, {1}, {2}, {3}", i.Top, i.Left, 10, 10);
                sb.AppendLine(newLine);
                MessageBox.Show(i.Name + " Was Saved");
            }

            try
            {
                File.WriteAllText("Levels/level" + numLevel.Value + ".csv", sb.ToString());
            }
            catch
            {
                MessageBox.Show("ERROR, Level probably already exists please change try changing the level number");
            }
        }

        //  Platform Width adjustment
        private void TbarWidth_Scroll(object sender, EventArgs e)
        {
            if (lastSelected != null) {
                lastSelected.Width = tbarWidth.Value;
            }
        }

        //  Platform Height adjustment
        private void TbarHeight_Scroll(object sender, EventArgs e)
        {
            if (lastSelected != null)
            {
                lastSelected.Height = tbarHeight.Value;
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (objectSelected != null)
            {
                objectSelected.Top = tabPageCreateLevel.PointToClient(Cursor.Position).Y - objectSelected.Height/2;
                objectSelected.Left = tabPageCreateLevel.PointToClient(Cursor.Position).X - objectSelected.Width/2;
            }
        }

        // Loads the selected level
        private void LoadToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            int lvl = Convert.ToInt32(e.ClickedItem.Text.Substring(5).Trim());
            numLevel.Value = lvl;
            StreamReader sr = new StreamReader("Levels/level" + lvl + ".csv");

            while (sr.Peek() != -1)
            {
                string line = sr.ReadLine();
                string[] values = line.Split(',');
                switch (values[0].Trim())
                {
                    case "Goal":
                        CreateObject("Goal", Convert.ToInt32(values[1]), Convert.ToInt32(values[2]), 15, 15, imgGoal, false);
                        goalCreated = true;
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
        }

        private void ExitForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Because this.Close() will result in an infinite loop of trying to close the form
            Application.Exit();
        }
    }
}
