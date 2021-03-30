namespace Store
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageCreateLevel = new System.Windows.Forms.TabPage();
            this.numLevel = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitForm = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlSelection = new System.Windows.Forms.Panel();
            this.spikeImage = new System.Windows.Forms.PictureBox();
            this.coinImage = new System.Windows.Forms.PictureBox();
            this.goalImage = new System.Windows.Forms.PictureBox();
            this.platformImage = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.tbarHeight = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.tbarWidth = new System.Windows.Forms.TrackBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPageCreateLevel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLevel)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.pnlSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spikeImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.coinImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.goalImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.platformImage)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbarHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbarWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageCreateLevel);
            this.tabControl1.Location = new System.Drawing.Point(92, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(837, 346);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPageCreateLevel
            // 
            this.tabPageCreateLevel.BackgroundImage = global::Store.Properties.Resources.Background1;
            this.tabPageCreateLevel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPageCreateLevel.Controls.Add(this.numLevel);
            this.tabPageCreateLevel.Controls.Add(this.label1);
            this.tabPageCreateLevel.Location = new System.Drawing.Point(4, 22);
            this.tabPageCreateLevel.Name = "tabPageCreateLevel";
            this.tabPageCreateLevel.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCreateLevel.Size = new System.Drawing.Size(829, 320);
            this.tabPageCreateLevel.TabIndex = 3;
            this.tabPageCreateLevel.Text = "Create New Level";
            this.tabPageCreateLevel.UseVisualStyleBackColor = true;
            // 
            // numLevel
            // 
            this.numLevel.Location = new System.Drawing.Point(136, 12);
            this.numLevel.Name = "numLevel";
            this.numLevel.Size = new System.Drawing.Size(50, 20);
            this.numLevel.TabIndex = 1;
            this.numLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "What Level is This?";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(929, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem1,
            this.loadToolStripMenuItem,
            this.ExitForm});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem1.Text = "File";
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.saveToolStripMenuItem1.Text = "Save";
            this.saveToolStripMenuItem1.Click += new System.EventHandler(this.SaveToolStripMenuItem1_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.LoadToolStripMenuItem_DropDownItemClicked);
            // 
            // ExitForm
            // 
            this.ExitForm.Name = "ExitForm";
            this.ExitForm.Size = new System.Drawing.Size(100, 22);
            this.ExitForm.Text = "Exit";
            this.ExitForm.Click += new System.EventHandler(this.ExitForm_Click);
            // 
            // pnlSelection
            // 
            this.pnlSelection.BackColor = System.Drawing.Color.Black;
            this.pnlSelection.Controls.Add(this.spikeImage);
            this.pnlSelection.Controls.Add(this.coinImage);
            this.pnlSelection.Controls.Add(this.goalImage);
            this.pnlSelection.Controls.Add(this.platformImage);
            this.pnlSelection.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSelection.Location = new System.Drawing.Point(0, 24);
            this.pnlSelection.Name = "pnlSelection";
            this.pnlSelection.Size = new System.Drawing.Size(94, 398);
            this.pnlSelection.TabIndex = 4;
            // 
            // spikeImage
            // 
            this.spikeImage.Image = global::Store.Properties.Resources.Spike;
            this.spikeImage.Location = new System.Drawing.Point(35, 226);
            this.spikeImage.Name = "spikeImage";
            this.spikeImage.Size = new System.Drawing.Size(25, 25);
            this.spikeImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.spikeImage.TabIndex = 101;
            this.spikeImage.TabStop = false;
            this.spikeImage.DoubleClick += new System.EventHandler(this.SpikeImage_DoubleClick);
            // 
            // coinImage
            // 
            this.coinImage.Image = ((System.Drawing.Image)(resources.GetObject("coinImage.Image")));
            this.coinImage.Location = new System.Drawing.Point(30, 123);
            this.coinImage.Name = "coinImage";
            this.coinImage.Size = new System.Drawing.Size(30, 30);
            this.coinImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.coinImage.TabIndex = 96;
            this.coinImage.TabStop = false;
            this.coinImage.DoubleClick += new System.EventHandler(this.CoinImage_DoubleClick);
            // 
            // goalImage
            // 
            this.goalImage.Image = ((System.Drawing.Image)(resources.GetObject("goalImage.Image")));
            this.goalImage.Location = new System.Drawing.Point(30, 20);
            this.goalImage.Name = "goalImage";
            this.goalImage.Size = new System.Drawing.Size(30, 30);
            this.goalImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.goalImage.TabIndex = 100;
            this.goalImage.TabStop = false;
            this.goalImage.DoubleClick += new System.EventHandler(this.GoalImage_DoubleClick);
            // 
            // platformImage
            // 
            this.platformImage.Image = ((System.Drawing.Image)(resources.GetObject("platformImage.Image")));
            this.platformImage.Location = new System.Drawing.Point(18, 324);
            this.platformImage.Name = "platformImage";
            this.platformImage.Size = new System.Drawing.Size(60, 30);
            this.platformImage.TabIndex = 84;
            this.platformImage.TabStop = false;
            this.platformImage.DoubleClick += new System.EventHandler(this.PlatformImage_DoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.tbarHeight);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.tbarWidth);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(94, 372);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(835, 50);
            this.panel1.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(314, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Height";
            // 
            // tbarHeight
            // 
            this.tbarHeight.AutoSize = false;
            this.tbarHeight.Enabled = false;
            this.tbarHeight.Location = new System.Drawing.Point(367, 6);
            this.tbarHeight.Maximum = 60;
            this.tbarHeight.Minimum = 10;
            this.tbarHeight.Name = "tbarHeight";
            this.tbarHeight.Size = new System.Drawing.Size(134, 32);
            this.tbarHeight.TabIndex = 5;
            this.tbarHeight.TickFrequency = 5;
            this.tbarHeight.Value = 10;
            this.tbarHeight.Scroll += new System.EventHandler(this.TbarHeight_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(68, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Width";
            // 
            // tbarWidth
            // 
            this.tbarWidth.AutoSize = false;
            this.tbarWidth.Enabled = false;
            this.tbarWidth.Location = new System.Drawing.Point(127, 6);
            this.tbarWidth.Maximum = 150;
            this.tbarWidth.Minimum = 10;
            this.tbarWidth.Name = "tbarWidth";
            this.tbarWidth.Size = new System.Drawing.Size(134, 32);
            this.tbarWidth.TabIndex = 0;
            this.tbarWidth.TickFrequency = 10;
            this.tbarWidth.Value = 10;
            this.tbarWidth.Scroll += new System.EventHandler(this.TbarWidth_Scroll);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 422);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlSelection);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form2";
            this.Text = "Create Level";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageCreateLevel.ResumeLayout(false);
            this.tabPageCreateLevel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLevel)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlSelection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spikeImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.coinImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.goalImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.platformImage)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbarHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbarWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageCreateLevel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ExitForm;
        private System.Windows.Forms.Panel pnlSelection;
        private System.Windows.Forms.PictureBox platformImage;
        private System.Windows.Forms.PictureBox goalImage;
        private System.Windows.Forms.PictureBox coinImage;
        private System.Windows.Forms.NumericUpDown numLevel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar tbarWidth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar tbarHeight;
        private System.Windows.Forms.PictureBox spikeImage;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
    }
}