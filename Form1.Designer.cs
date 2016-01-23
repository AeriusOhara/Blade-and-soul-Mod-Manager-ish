namespace BnSModBackup
{
    partial class mainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.label4 = new System.Windows.Forms.Label();
            this.performInstall = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textLog = new System.Windows.Forms.TextBox();
            this.performDeInstall = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.bnsFolderTextBox = new System.Windows.Forms.TextBox();
            this.bnsFolderButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.openModsFolderBtn = new System.Windows.Forms.Button();
            this.launchGameBtn = new System.Windows.Forms.Button();
            this.gameVersionDropDownMenu = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.launchNCSoftLauncherBtn = new System.Windows.Forms.Button();
            this.ncsoftFolderButton = new System.Windows.Forms.Button();
            this.ncsoftFolderTextBox = new System.Windows.Forms.TextBox();
            this.refreshModsBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 484);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(274, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "by Aeriussoftware - 2016 - aeriussoftware.wordpress.com";
            // 
            // performInstall
            // 
            this.performInstall.Enabled = false;
            this.performInstall.Location = new System.Drawing.Point(93, 345);
            this.performInstall.Name = "performInstall";
            this.performInstall.Size = new System.Drawing.Size(75, 23);
            this.performInstall.TabIndex = 7;
            this.performInstall.Text = "Go";
            this.performInstall.UseVisualStyleBackColor = true;
            this.performInstall.Click += new System.EventHandler(this.doInstall);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(341, 96);
            this.label2.TabIndex = 9;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // textLog
            // 
            this.textLog.Location = new System.Drawing.Point(12, 265);
            this.textLog.Multiline = true;
            this.textLog.Name = "textLog";
            this.textLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textLog.Size = new System.Drawing.Size(344, 73);
            this.textLog.TabIndex = 6;
            // 
            // performDeInstall
            // 
            this.performDeInstall.Enabled = false;
            this.performDeInstall.Location = new System.Drawing.Point(12, 345);
            this.performDeInstall.Name = "performDeInstall";
            this.performDeInstall.Size = new System.Drawing.Size(75, 23);
            this.performDeInstall.TabIndex = 12;
            this.performDeInstall.Text = "Undo";
            this.performDeInstall.UseVisualStyleBackColor = true;
            this.performDeInstall.Click += new System.EventHandler(this.doDeInstall);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(0, 501);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(384, 10);
            this.progressBar1.TabIndex = 13;
            // 
            // bnsFolderTextBox
            // 
            this.bnsFolderTextBox.Location = new System.Drawing.Point(12, 100);
            this.bnsFolderTextBox.Name = "bnsFolderTextBox";
            this.bnsFolderTextBox.ReadOnly = true;
            this.bnsFolderTextBox.Size = new System.Drawing.Size(228, 20);
            this.bnsFolderTextBox.TabIndex = 14;
            this.bnsFolderTextBox.Text = "BnS Folder Not Set";
            // 
            // bnsFolderButton
            // 
            this.bnsFolderButton.Location = new System.Drawing.Point(246, 98);
            this.bnsFolderButton.Name = "bnsFolderButton";
            this.bnsFolderButton.Size = new System.Drawing.Size(110, 23);
            this.bnsFolderButton.TabIndex = 15;
            this.bnsFolderButton.Text = "Set BnS Folder";
            this.bnsFolderButton.UseVisualStyleBackColor = true;
            this.bnsFolderButton.Click += new System.EventHandler(this.setBnSInstallDir);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(361, 80);
            this.label5.TabIndex = 16;
            this.label5.Text = resources.GetString("label5.Text");
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Location = new System.Drawing.Point(-8, 157);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(400, 2);
            this.label6.TabIndex = 17;
            this.label6.Text = "label6";
            // 
            // openModsFolderBtn
            // 
            this.openModsFolderBtn.Location = new System.Drawing.Point(246, 345);
            this.openModsFolderBtn.Name = "openModsFolderBtn";
            this.openModsFolderBtn.Size = new System.Drawing.Size(110, 23);
            this.openModsFolderBtn.TabIndex = 19;
            this.openModsFolderBtn.Text = "Open Mods Folder";
            this.openModsFolderBtn.UseVisualStyleBackColor = true;
            this.openModsFolderBtn.Click += new System.EventHandler(this.openModsFolder);
            // 
            // launchGameBtn
            // 
            this.launchGameBtn.Enabled = false;
            this.launchGameBtn.Location = new System.Drawing.Point(93, 413);
            this.launchGameBtn.Name = "launchGameBtn";
            this.launchGameBtn.Size = new System.Drawing.Size(263, 66);
            this.launchGameBtn.TabIndex = 20;
            this.launchGameBtn.Text = "Launch Blade and Soul";
            this.launchGameBtn.UseVisualStyleBackColor = true;
            this.launchGameBtn.Click += new System.EventHandler(this.launchGame);
            // 
            // gameVersionDropDownMenu
            // 
            this.gameVersionDropDownMenu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gameVersionDropDownMenu.FormattingEnabled = true;
            this.gameVersionDropDownMenu.Items.AddRange(new object[] {
            "REGION: Europe",
            "REGION: North America"});
            this.gameVersionDropDownMenu.Location = new System.Drawing.Point(210, 386);
            this.gameVersionDropDownMenu.Name = "gameVersionDropDownMenu";
            this.gameVersionDropDownMenu.Size = new System.Drawing.Size(140, 21);
            this.gameVersionDropDownMenu.TabIndex = 21;
            this.gameVersionDropDownMenu.SelectedIndexChanged += new System.EventHandler(this.gameVersionChanged);
            // 
            // label7
            // 
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label7.Location = new System.Drawing.Point(-8, 376);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(400, 2);
            this.label7.TabIndex = 22;
            this.label7.Text = "label7";
            // 
            // launchNCSoftLauncherBtn
            // 
            this.launchNCSoftLauncherBtn.Enabled = false;
            this.launchNCSoftLauncherBtn.Location = new System.Drawing.Point(12, 413);
            this.launchNCSoftLauncherBtn.Name = "launchNCSoftLauncherBtn";
            this.launchNCSoftLauncherBtn.Size = new System.Drawing.Size(75, 66);
            this.launchNCSoftLauncherBtn.TabIndex = 23;
            this.launchNCSoftLauncherBtn.Text = "Launch\r\nNCSoft\r\nLauncher";
            this.launchNCSoftLauncherBtn.UseVisualStyleBackColor = true;
            this.launchNCSoftLauncherBtn.Click += new System.EventHandler(this.launchNCSoftLauncher);
            // 
            // ncsoftFolderButton
            // 
            this.ncsoftFolderButton.Location = new System.Drawing.Point(246, 127);
            this.ncsoftFolderButton.Name = "ncsoftFolderButton";
            this.ncsoftFolderButton.Size = new System.Drawing.Size(110, 23);
            this.ncsoftFolderButton.TabIndex = 25;
            this.ncsoftFolderButton.Text = "Set NCSoft Folder";
            this.ncsoftFolderButton.UseVisualStyleBackColor = true;
            this.ncsoftFolderButton.Click += new System.EventHandler(this.setNCSoftDir);
            // 
            // ncsoftFolderTextBox
            // 
            this.ncsoftFolderTextBox.Location = new System.Drawing.Point(12, 129);
            this.ncsoftFolderTextBox.Name = "ncsoftFolderTextBox";
            this.ncsoftFolderTextBox.ReadOnly = true;
            this.ncsoftFolderTextBox.Size = new System.Drawing.Size(228, 20);
            this.ncsoftFolderTextBox.TabIndex = 24;
            this.ncsoftFolderTextBox.Text = "NCSoft Folder Not Set";
            // 
            // refreshModsBtn
            // 
            this.refreshModsBtn.Image = ((System.Drawing.Image)(resources.GetObject("refreshModsBtn.Image")));
            this.refreshModsBtn.Location = new System.Drawing.Point(217, 345);
            this.refreshModsBtn.Name = "refreshModsBtn";
            this.refreshModsBtn.Size = new System.Drawing.Size(23, 23);
            this.refreshModsBtn.TabIndex = 26;
            this.refreshModsBtn.UseVisualStyleBackColor = true;
            this.refreshModsBtn.Click += new System.EventHandler(this.refreshMods);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 511);
            this.Controls.Add(this.refreshModsBtn);
            this.Controls.Add(this.ncsoftFolderButton);
            this.Controls.Add(this.ncsoftFolderTextBox);
            this.Controls.Add(this.launchNCSoftLauncherBtn);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.gameVersionDropDownMenu);
            this.Controls.Add(this.launchGameBtn);
            this.Controls.Add(this.openModsFolderBtn);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.bnsFolderButton);
            this.Controls.Add(this.bnsFolderTextBox);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.performDeInstall);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.performInstall);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textLog);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "mainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Blade and Soul Mod Manager-ish";
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button performInstall;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textLog;
        private System.Windows.Forms.Button performDeInstall;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox bnsFolderTextBox;
        private System.Windows.Forms.Button bnsFolderButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button openModsFolderBtn;
        private System.Windows.Forms.Button launchGameBtn;
        private System.Windows.Forms.ComboBox gameVersionDropDownMenu;
        private System.Windows.Forms.Button launchNCSoftLauncherBtn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button ncsoftFolderButton;
        private System.Windows.Forms.TextBox ncsoftFolderTextBox;
        private System.Windows.Forms.Button refreshModsBtn;
    }
}

