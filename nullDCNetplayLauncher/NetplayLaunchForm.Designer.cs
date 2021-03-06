﻿namespace nullDCNetplayLauncher
{
    partial class NetplayLaunchForm
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
        //private void InitializeComponent()
        private void InitializeComponent(bool StartTray)
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NetplayLaunchForm));
            this.cboGameSelect = new System.Windows.Forms.ComboBox();
            this.btnHost = new System.Windows.Forms.Button();
            this.btnOffline = new System.Windows.Forms.Button();
            this.btnJoin = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnController = new System.Windows.Forms.Button();
            this.btnDragLoad = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.btnHelp = new System.Windows.Forms.Button();
            this.tipSettings = new System.Windows.Forms.ToolTip(this.components);
            this.tipController = new System.Windows.Forms.ToolTip(this.components);
            this.tipFileDrop = new System.Windows.Forms.ToolTip(this.components);
            this.tipHelp = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboGameSelect
            // 
            this.cboGameSelect.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboGameSelect.FormattingEnabled = true;
            this.cboGameSelect.Location = new System.Drawing.Point(4, 97);
            this.cboGameSelect.Margin = new System.Windows.Forms.Padding(2);
            this.cboGameSelect.Name = "cboGameSelect";
            this.cboGameSelect.Size = new System.Drawing.Size(255, 21);
            this.cboGameSelect.TabIndex = 38;
            this.cboGameSelect.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cboGameSelect_DrawItem);
            this.cboGameSelect.SelectedIndexChanged += new System.EventHandler(this.cboGameSelect_SelectedIndexChanged);
            // 
            // btnHost
            // 
            this.btnHost.Location = new System.Drawing.Point(3, 38);
            this.btnHost.Margin = new System.Windows.Forms.Padding(2);
            this.btnHost.Name = "btnHost";
            this.btnHost.Size = new System.Drawing.Size(255, 26);
            this.btnHost.TabIndex = 36;
            this.btnHost.Text = "Host Game";
            this.btnHost.UseVisualStyleBackColor = true;
            this.btnHost.Click += new System.EventHandler(this.btnHost_Click);
            // 
            // btnOffline
            // 
            this.btnOffline.Location = new System.Drawing.Point(3, 9);
            this.btnOffline.Margin = new System.Windows.Forms.Padding(2);
            this.btnOffline.Name = "btnOffline";
            this.btnOffline.Size = new System.Drawing.Size(255, 26);
            this.btnOffline.TabIndex = 35;
            this.btnOffline.Text = "Play Offline";
            this.btnOffline.UseVisualStyleBackColor = true;
            this.btnOffline.Click += new System.EventHandler(this.btnOffline_Click);
            // 
            // btnJoin
            // 
            this.btnJoin.Location = new System.Drawing.Point(3, 67);
            this.btnJoin.Margin = new System.Windows.Forms.Padding(2);
            this.btnJoin.Name = "btnJoin";
            this.btnJoin.Size = new System.Drawing.Size(255, 26);
            this.btnJoin.TabIndex = 37;
            this.btnJoin.Text = "Join Game";
            this.btnJoin.UseVisualStyleBackColor = true;
            this.btnJoin.Click += new System.EventHandler(this.btnJoin_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOffline);
            this.panel1.Controls.Add(this.cboGameSelect);
            this.panel1.Controls.Add(this.btnJoin);
            this.panel1.Controls.Add(this.btnHost);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(262, 124);
            this.panel1.TabIndex = 39;
            // 
            // btnSettings
            // 
            this.btnSettings.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSettings.BackgroundImage")));
            this.btnSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSettings.Location = new System.Drawing.Point(3, 124);
            this.btnSettings.Margin = new System.Windows.Forms.Padding(2);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(25, 25);
            this.btnSettings.TabIndex = 42;
            this.tipSettings.SetToolTip(this.btnSettings, "Settings");
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnController
            // 
            this.btnController.BackgroundImage = global::nullDCNetplayLauncher.Properties.Resources.icons8_game_controller_26;
            this.btnController.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnController.Location = new System.Drawing.Point(32, 124);
            this.btnController.Margin = new System.Windows.Forms.Padding(2);
            this.btnController.Name = "btnController";
            this.btnController.Size = new System.Drawing.Size(25, 25);
            this.btnController.TabIndex = 43;
            this.tipController.SetToolTip(this.btnController, "Controller Setup");
            this.btnController.UseVisualStyleBackColor = true;
            this.btnController.Click += new System.EventHandler(this.btnController_Click);
            // 
            // btnDragLoad
            // 
            this.btnDragLoad.BackgroundImage = global::nullDCNetplayLauncher.Properties.Resources.round_publish_black_18dp;
            this.btnDragLoad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDragLoad.Location = new System.Drawing.Point(62, 124);
            this.btnDragLoad.Margin = new System.Windows.Forms.Padding(2);
            this.btnDragLoad.Name = "btnDragLoad";
            this.btnDragLoad.Size = new System.Drawing.Size(25, 25);
            this.btnDragLoad.TabIndex = 44;
            this.tipFileDrop.SetToolTip(this.btnDragLoad, "File Drop");
            this.btnDragLoad.UseVisualStyleBackColor = true;
            this.btnDragLoad.Click += new System.EventHandler(this.btnDragLoad_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "NullDC Netplay";
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // btnHelp
            // 
            this.btnHelp.BackgroundImage = global::nullDCNetplayLauncher.Properties.Resources.round_not_listed_location_black_18dp;
            this.btnHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnHelp.Location = new System.Drawing.Point(233, 124);
            this.btnHelp.Margin = new System.Windows.Forms.Padding(2);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(25, 25);
            this.btnHelp.TabIndex = 45;
            this.tipHelp.SetToolTip(this.btnHelp, "Help");
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // NetplayLaunchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(261, 150);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnDragLoad);
            this.Controls.Add(this.btnController);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NetplayLaunchForm";
            if (StartTray)
                this.ShowInTaskbar = false;
            this.Text = "NullDC Netplay";
            this.Resize += new System.EventHandler(this.NetplayLaunchForm_Resize);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox cboGameSelect;
        private System.Windows.Forms.Button btnHost;
        private System.Windows.Forms.Button btnOffline;
        private System.Windows.Forms.Button btnJoin;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnController;
        private System.Windows.Forms.Button btnDragLoad;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.ToolTip tipSettings;
        private System.Windows.Forms.ToolTip tipController;
        private System.Windows.Forms.ToolTip tipFileDrop;
        private System.Windows.Forms.ToolTip tipHelp;
    }
}