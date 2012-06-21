namespace WinFormPBUCON
{
    partial class frmMainApp
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
        /// the contents of this method with the code editor.D:\dev\OpenPBUCON\WinFormPBUCON\MainApp.cs
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainApp));
            this.serverTabs = new System.Windows.Forms.TabControl();
            this.tabImageList = new System.Windows.Forms.ImageList(this.components);
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.serversToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.broadcastCommandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoScrollToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // serverTabs
            // 
            this.serverTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serverTabs.ImageList = this.tabImageList;
            this.serverTabs.Location = new System.Drawing.Point(12, 27);
            this.serverTabs.Name = "serverTabs";
            this.serverTabs.SelectedIndex = 0;
            this.serverTabs.Size = new System.Drawing.Size(860, 423);
            this.serverTabs.TabIndex = 0;
            // 
            // tabImageList
            // 
            this.tabImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tabImageList.ImageStream")));
            this.tabImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.tabImageList.Images.SetKeyName(0, "delete.png");
            this.tabImageList.Images.SetKeyName(1, "accept.png");
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serversToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(884, 24);
            this.mainMenuStrip.TabIndex = 1;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // serversToolStripMenuItem
            // 
            this.serversToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addServerToolStripMenuItem,
            this.broadcastCommandToolStripMenuItem});
            this.serversToolStripMenuItem.Name = "serversToolStripMenuItem";
            this.serversToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.serversToolStripMenuItem.Text = "Servers";
            // 
            // addServerToolStripMenuItem
            // 
            this.addServerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("addServerToolStripMenuItem.Image")));
            this.addServerToolStripMenuItem.Name = "addServerToolStripMenuItem";
            this.addServerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addServerToolStripMenuItem.Text = "Add Server";
            this.addServerToolStripMenuItem.Click += new System.EventHandler(this.addServerToolStripMenuItem_Click);
            // 
            // broadcastCommandToolStripMenuItem
            // 
            this.broadcastCommandToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("broadcastCommandToolStripMenuItem.Image")));
            this.broadcastCommandToolStripMenuItem.Name = "broadcastCommandToolStripMenuItem";
            this.broadcastCommandToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.broadcastCommandToolStripMenuItem.Text = "Broadcast Command";
            this.broadcastCommandToolStripMenuItem.Click += new System.EventHandler(this.broadcastCommandToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoScrollToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // autoScrollToolStripMenuItem
            // 
            this.autoScrollToolStripMenuItem.Checked = true;
            this.autoScrollToolStripMenuItem.CheckOnClick = true;
            this.autoScrollToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoScrollToolStripMenuItem.Name = "autoScrollToolStripMenuItem";
            this.autoScrollToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.autoScrollToolStripMenuItem.Text = "Auto Scroll";
            // 
            // frmMainApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 462);
            this.Controls.Add(this.serverTabs);
            this.Controls.Add(this.mainMenuStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.mainMenuStrip;
            this.MinimumSize = new System.Drawing.Size(400, 200);
            this.Name = "frmMainApp";
            this.Text = "PunkBuster UCON Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMainApp_FormClosing);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl serverTabs;
        private System.Windows.Forms.ImageList tabImageList;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem serversToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem broadcastCommandToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem autoScrollToolStripMenuItem;
    }
}

