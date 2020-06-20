namespace nullDCNetplayLauncher
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
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboGameSelect
            // 
            this.cboGameSelect.FormattingEnabled = true;
            this.cboGameSelect.Location = new System.Drawing.Point(5, 119);
            this.cboGameSelect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboGameSelect.Name = "cboGameSelect";
            this.cboGameSelect.Size = new System.Drawing.Size(339, 24);
            this.cboGameSelect.TabIndex = 38;
            this.cboGameSelect.SelectedIndexChanged += new System.EventHandler(this.cboGameSelect_SelectedIndexChanged);
            // 
            // btnHost
            // 
            this.btnHost.Location = new System.Drawing.Point(4, 47);
            this.btnHost.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnHost.Name = "btnHost";
            this.btnHost.Size = new System.Drawing.Size(340, 32);
            this.btnHost.TabIndex = 36;
            this.btnHost.Text = "Host Game";
            this.btnHost.UseVisualStyleBackColor = true;
            this.btnHost.Click += new System.EventHandler(this.btnHost_Click);
            // 
            // btnOffline
            // 
            this.btnOffline.Location = new System.Drawing.Point(4, 11);
            this.btnOffline.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOffline.Name = "btnOffline";
            this.btnOffline.Size = new System.Drawing.Size(340, 32);
            this.btnOffline.TabIndex = 35;
            this.btnOffline.Text = "Play Offline";
            this.btnOffline.UseVisualStyleBackColor = true;
            this.btnOffline.Click += new System.EventHandler(this.btnOffline_Click);
            // 
            // btnJoin
            // 
            this.btnJoin.Location = new System.Drawing.Point(4, 83);
            this.btnJoin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnJoin.Name = "btnJoin";
            this.btnJoin.Size = new System.Drawing.Size(340, 32);
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
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(349, 153);
            this.panel1.TabIndex = 39;
            // 
            // btnSettings
            // 
            this.btnSettings.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSettings.BackgroundImage")));
            this.btnSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSettings.Location = new System.Drawing.Point(4, 153);
            this.btnSettings.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(33, 31);
            this.btnSettings.TabIndex = 42;
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnController
            // 
            this.btnController.BackgroundImage = global::nullDCNetplayLauncher.Properties.Resources.icons8_game_controller_26;
            this.btnController.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnController.Location = new System.Drawing.Point(43, 153);
            this.btnController.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnController.Name = "btnController";
            this.btnController.Size = new System.Drawing.Size(33, 31);
            this.btnController.TabIndex = 43;
            this.btnController.UseVisualStyleBackColor = true;
            this.btnController.Click += new System.EventHandler(this.btnController_Click);
            // 
            // btnDragLoad
            // 
            this.btnDragLoad.BackgroundImage = global::nullDCNetplayLauncher.Properties.Resources.round_publish_black_18dp;
            this.btnDragLoad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDragLoad.Location = new System.Drawing.Point(82, 153);
            this.btnDragLoad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDragLoad.Name = "btnDragLoad";
            this.btnDragLoad.Size = new System.Drawing.Size(33, 31);
            this.btnDragLoad.TabIndex = 44;
            this.btnDragLoad.UseVisualStyleBackColor = true;
            this.btnDragLoad.Click += new System.EventHandler(this.btnDragLoad_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "NullDC Netplay";
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // NetplayLaunchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(348, 185);
            this.Controls.Add(this.btnDragLoad);
            this.Controls.Add(this.btnController);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
    }
}