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
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NetplayLaunchForm));
            this.cboPresetName = new System.Windows.Forms.ComboBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.numDelay = new System.Windows.Forms.NumericUpDown();
            this.btnSavePreset = new System.Windows.Forms.Button();
            this.lblDelay = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblIP = new System.Windows.Forms.Label();
            this.cboGameSelect = new System.Windows.Forms.ComboBox();
            this.btnHost = new System.Windows.Forms.Button();
            this.btnOffline = new System.Windows.Forms.Button();
            this.btnJoin = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.connectionBox = new System.Windows.Forms.GroupBox();
            this.btnDeletePreset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).BeginInit();
            this.panel1.SuspendLayout();
            this.connectionBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboPresetName
            // 
            this.cboPresetName.FormattingEnabled = true;
            this.cboPresetName.Location = new System.Drawing.Point(116, 37);
            this.cboPresetName.Margin = new System.Windows.Forms.Padding(6);
            this.cboPresetName.Name = "cboPresetName";
            this.cboPresetName.Size = new System.Drawing.Size(262, 33);
            this.cboPresetName.TabIndex = 34;
            this.cboPresetName.TextChanged += new System.EventHandler(this.cboPresetName_TextChanged);
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(194, 87);
            this.txtIP.Margin = new System.Windows.Forms.Padding(4);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(184, 31);
            this.txtIP.TabIndex = 27;
            this.txtIP.Text = "0.0.0.0";
            // 
            // numDelay
            // 
            this.numDelay.Location = new System.Drawing.Point(194, 179);
            this.numDelay.Margin = new System.Windows.Forms.Padding(4);
            this.numDelay.Name = "numDelay";
            this.numDelay.Size = new System.Drawing.Size(188, 31);
            this.numDelay.TabIndex = 33;
            this.numDelay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnSavePreset
            // 
            this.btnSavePreset.Location = new System.Drawing.Point(115, 225);
            this.btnSavePreset.Margin = new System.Windows.Forms.Padding(4);
            this.btnSavePreset.Name = "btnSavePreset";
            this.btnSavePreset.Size = new System.Drawing.Size(125, 38);
            this.btnSavePreset.TabIndex = 28;
            this.btnSavePreset.Text = "Save";
            this.btnSavePreset.UseVisualStyleBackColor = true;
            this.btnSavePreset.Click += new System.EventHandler(this.btnSavePreset_Click);
            // 
            // lblDelay
            // 
            this.lblDelay.AutoSize = true;
            this.lblDelay.Location = new System.Drawing.Point(110, 183);
            this.lblDelay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDelay.Name = "lblDelay";
            this.lblDelay.Size = new System.Drawing.Size(67, 25);
            this.lblDelay.TabIndex = 32;
            this.lblDelay.Text = "Delay";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(110, 138);
            this.lblPort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(51, 25);
            this.lblPort.TabIndex = 31;
            this.lblPort.Text = "Port";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(194, 133);
            this.txtPort.Margin = new System.Windows.Forms.Padding(4);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(184, 31);
            this.txtPort.TabIndex = 30;
            this.txtPort.Text = "27886";
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Location = new System.Drawing.Point(110, 92);
            this.lblIP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(31, 25);
            this.lblIP.TabIndex = 29;
            this.lblIP.Text = "IP";
            // 
            // cboGameSelect
            // 
            this.cboGameSelect.FormattingEnabled = true;
            this.cboGameSelect.Location = new System.Drawing.Point(6, 187);
            this.cboGameSelect.Margin = new System.Windows.Forms.Padding(4);
            this.cboGameSelect.Name = "cboGameSelect";
            this.cboGameSelect.Size = new System.Drawing.Size(506, 33);
            this.cboGameSelect.TabIndex = 38;
            // 
            // btnHost
            // 
            this.btnHost.Location = new System.Drawing.Point(6, 71);
            this.btnHost.Margin = new System.Windows.Forms.Padding(4);
            this.btnHost.Name = "btnHost";
            this.btnHost.Size = new System.Drawing.Size(510, 50);
            this.btnHost.TabIndex = 36;
            this.btnHost.Text = "Host Game";
            this.btnHost.UseVisualStyleBackColor = true;
            this.btnHost.Click += new System.EventHandler(this.btnHost_Click);
            // 
            // btnOffline
            // 
            this.btnOffline.Location = new System.Drawing.Point(6, 17);
            this.btnOffline.Margin = new System.Windows.Forms.Padding(4);
            this.btnOffline.Name = "btnOffline";
            this.btnOffline.Size = new System.Drawing.Size(510, 50);
            this.btnOffline.TabIndex = 35;
            this.btnOffline.Text = "Play Offline";
            this.btnOffline.UseVisualStyleBackColor = true;
            this.btnOffline.Click += new System.EventHandler(this.btnOffline_Click);
            // 
            // btnJoin
            // 
            this.btnJoin.Location = new System.Drawing.Point(6, 129);
            this.btnJoin.Margin = new System.Windows.Forms.Padding(4);
            this.btnJoin.Name = "btnJoin";
            this.btnJoin.Size = new System.Drawing.Size(510, 50);
            this.btnJoin.TabIndex = 37;
            this.btnJoin.Text = "Join Game";
            this.btnJoin.UseVisualStyleBackColor = true;
            this.btnJoin.Click += new System.EventHandler(this.btnJoin_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.connectionBox);
            this.panel1.Controls.Add(this.btnOffline);
            this.panel1.Controls.Add(this.cboGameSelect);
            this.panel1.Controls.Add(this.btnJoin);
            this.panel1.Controls.Add(this.btnHost);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(524, 535);
            this.panel1.TabIndex = 39;
            // 
            // connectionBox
            // 
            this.connectionBox.Controls.Add(this.btnDeletePreset);
            this.connectionBox.Controls.Add(this.cboPresetName);
            this.connectionBox.Controls.Add(this.btnSavePreset);
            this.connectionBox.Controls.Add(this.txtIP);
            this.connectionBox.Controls.Add(this.lblDelay);
            this.connectionBox.Controls.Add(this.txtPort);
            this.connectionBox.Controls.Add(this.lblIP);
            this.connectionBox.Controls.Add(this.lblPort);
            this.connectionBox.Controls.Add(this.numDelay);
            this.connectionBox.Location = new System.Drawing.Point(6, 237);
            this.connectionBox.Margin = new System.Windows.Forms.Padding(6);
            this.connectionBox.Name = "connectionBox";
            this.connectionBox.Padding = new System.Windows.Forms.Padding(6);
            this.connectionBox.Size = new System.Drawing.Size(508, 290);
            this.connectionBox.TabIndex = 40;
            this.connectionBox.TabStop = false;
            // 
            // btnDeletePreset
            // 
            this.btnDeletePreset.Location = new System.Drawing.Point(257, 227);
            this.btnDeletePreset.Name = "btnDeletePreset";
            this.btnDeletePreset.Size = new System.Drawing.Size(125, 38);
            this.btnDeletePreset.TabIndex = 35;
            this.btnDeletePreset.Text = "Delete";
            this.btnDeletePreset.UseVisualStyleBackColor = true;
            this.btnDeletePreset.Click += new System.EventHandler(this.btnDeletePreset_Click);
            // 
            // NetplayLaunchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(520, 537);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "NetplayLaunchForm";
            this.Text = "NullDC Netplay";
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).EndInit();
            this.panel1.ResumeLayout(false);
            this.connectionBox.ResumeLayout(false);
            this.connectionBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboPresetName;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.NumericUpDown numDelay;
        private System.Windows.Forms.Button btnSavePreset;
        private System.Windows.Forms.Label lblDelay;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.ComboBox cboGameSelect;
        private System.Windows.Forms.Button btnHost;
        private System.Windows.Forms.Button btnOffline;
        private System.Windows.Forms.Button btnJoin;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox connectionBox;
        private System.Windows.Forms.Button btnDeletePreset;
    }
}