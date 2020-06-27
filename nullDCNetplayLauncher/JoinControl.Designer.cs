namespace nullDCNetplayLauncher
{
    partial class JoinControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JoinControl));
            this.splitGuest = new System.Windows.Forms.SplitContainer();
            this.btnPaste = new System.Windows.Forms.Button();
            this.lblHostCode = new System.Windows.Forms.Label();
            this.btnExpandCollapse = new System.Windows.Forms.Button();
            this.txtOpponentIP = new System.Windows.Forms.TextBox();
            this.btnLaunchGame = new System.Windows.Forms.Button();
            this.txtHostCode = new System.Windows.Forms.TextBox();
            this.lblVerifyDelay = new System.Windows.Forms.Label();
            this.lblGuestnameIP = new System.Windows.Forms.Label();
            this.btnGuess = new System.Windows.Forms.Button();
            this.numDelay = new System.Windows.Forms.NumericUpDown();
            this.grpAdvanced = new System.Windows.Forms.GroupBox();
            this.txtHostIP = new System.Windows.Forms.TextBox();
            this.cboMethod = new System.Windows.Forms.ComboBox();
            this.lblMethod = new System.Windows.Forms.Label();
            this.btnSavePreset = new System.Windows.Forms.Button();
            this.btnDeletePreset = new System.Windows.Forms.Button();
            this.cboPresetName = new System.Windows.Forms.ComboBox();
            this.lblHostIP = new System.Windows.Forms.Label();
            this.txtHostPort = new System.Windows.Forms.TextBox();
            this.lblGuestPort = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitGuest)).BeginInit();
            this.splitGuest.Panel1.SuspendLayout();
            this.splitGuest.Panel2.SuspendLayout();
            this.splitGuest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).BeginInit();
            this.grpAdvanced.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitGuest
            // 
            this.splitGuest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitGuest.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitGuest.IsSplitterFixed = true;
            this.splitGuest.Location = new System.Drawing.Point(0, 0);
            this.splitGuest.Margin = new System.Windows.Forms.Padding(2);
            this.splitGuest.Name = "splitGuest";
            this.splitGuest.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitGuest.Panel1
            // 
            this.splitGuest.Panel1.Controls.Add(this.btnPaste);
            this.splitGuest.Panel1.Controls.Add(this.lblHostCode);
            this.splitGuest.Panel1.Controls.Add(this.btnExpandCollapse);
            this.splitGuest.Panel1.Controls.Add(this.txtOpponentIP);
            this.splitGuest.Panel1.Controls.Add(this.btnLaunchGame);
            this.splitGuest.Panel1.Controls.Add(this.txtHostCode);
            this.splitGuest.Panel1.Controls.Add(this.lblVerifyDelay);
            this.splitGuest.Panel1.Controls.Add(this.lblGuestnameIP);
            this.splitGuest.Panel1.Controls.Add(this.btnGuess);
            this.splitGuest.Panel1.Controls.Add(this.numDelay);
            // 
            // splitGuest.Panel2
            // 
            this.splitGuest.Panel2.Controls.Add(this.grpAdvanced);
            this.splitGuest.Panel2Collapsed = true;
            this.splitGuest.Size = new System.Drawing.Size(216, 300);
            this.splitGuest.SplitterDistance = 162;
            this.splitGuest.SplitterWidth = 1;
            this.splitGuest.TabIndex = 72;
            // 
            // btnPaste
            // 
            this.btnPaste.AutoSize = true;
            this.btnPaste.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPaste.Image = ((System.Drawing.Image)(resources.GetObject("btnPaste.Image")));
            this.btnPaste.Location = new System.Drawing.Point(186, 20);
            this.btnPaste.Margin = new System.Windows.Forms.Padding(2);
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(22, 22);
            this.btnPaste.TabIndex = 79;
            this.btnPaste.UseVisualStyleBackColor = true;
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // lblHostCode
            // 
            this.lblHostCode.AutoSize = true;
            this.lblHostCode.Location = new System.Drawing.Point(41, 5);
            this.lblHostCode.Name = "lblHostCode";
            this.lblHostCode.Size = new System.Drawing.Size(134, 13);
            this.lblHostCode.TabIndex = 78;
            this.lblHostCode.Text = "Enter Received Host Code";
            // 
            // btnExpandCollapse
            // 
            this.btnExpandCollapse.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.btnExpandCollapse.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnExpandCollapse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExpandCollapse.Location = new System.Drawing.Point(-5, 129);
            this.btnExpandCollapse.Margin = new System.Windows.Forms.Padding(2);
            this.btnExpandCollapse.Name = "btnExpandCollapse";
            this.btnExpandCollapse.Size = new System.Drawing.Size(227, 22);
            this.btnExpandCollapse.TabIndex = 77;
            this.btnExpandCollapse.Text = "↑   Advanced Options   ↑";
            this.btnExpandCollapse.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExpandCollapse.UseVisualStyleBackColor = false;
            this.btnExpandCollapse.Click += new System.EventHandler(this.btnExpandCollapse_Click);
            // 
            // txtOpponentIP
            // 
            this.txtOpponentIP.Location = new System.Drawing.Point(89, 46);
            this.txtOpponentIP.Margin = new System.Windows.Forms.Padding(2);
            this.txtOpponentIP.Name = "txtOpponentIP";
            this.txtOpponentIP.Size = new System.Drawing.Size(119, 20);
            this.txtOpponentIP.TabIndex = 72;
            // 
            // btnLaunchGame
            // 
            this.btnLaunchGame.Location = new System.Drawing.Point(8, 94);
            this.btnLaunchGame.Margin = new System.Windows.Forms.Padding(2);
            this.btnLaunchGame.Name = "btnLaunchGame";
            this.btnLaunchGame.Size = new System.Drawing.Size(200, 19);
            this.btnLaunchGame.TabIndex = 71;
            this.btnLaunchGame.Text = "Launch Game";
            this.btnLaunchGame.UseVisualStyleBackColor = true;
            this.btnLaunchGame.Click += new System.EventHandler(this.btnLaunchGame_Click);
            // 
            // txtHostCode
            // 
            this.txtHostCode.Location = new System.Drawing.Point(8, 22);
            this.txtHostCode.Margin = new System.Windows.Forms.Padding(2);
            this.txtHostCode.Name = "txtHostCode";
            this.txtHostCode.Size = new System.Drawing.Size(176, 20);
            this.txtHostCode.TabIndex = 69;
            this.txtHostCode.TextChanged += new System.EventHandler(this.txtHostCode_TextChanged);
            // 
            // lblVerifyDelay
            // 
            this.lblVerifyDelay.AutoSize = true;
            this.lblVerifyDelay.Location = new System.Drawing.Point(10, 72);
            this.lblVerifyDelay.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblVerifyDelay.Name = "lblVerifyDelay";
            this.lblVerifyDelay.Size = new System.Drawing.Size(63, 13);
            this.lblVerifyDelay.TabIndex = 67;
            this.lblVerifyDelay.Text = "Verify Delay";
            // 
            // lblGuestnameIP
            // 
            this.lblGuestnameIP.AutoSize = true;
            this.lblGuestnameIP.Location = new System.Drawing.Point(10, 49);
            this.lblGuestnameIP.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGuestnameIP.Name = "lblGuestnameIP";
            this.lblGuestnameIP.Size = new System.Drawing.Size(67, 13);
            this.lblGuestnameIP.TabIndex = 66;
            this.lblGuestnameIP.Text = "Opponent IP";
            // 
            // btnGuess
            // 
            this.btnGuess.Location = new System.Drawing.Point(128, 70);
            this.btnGuess.Name = "btnGuess";
            this.btnGuess.Size = new System.Drawing.Size(80, 20);
            this.btnGuess.TabIndex = 74;
            this.btnGuess.Text = "Guess";
            this.btnGuess.UseVisualStyleBackColor = true;
            this.btnGuess.Click += new System.EventHandler(this.btnGuess_Click);
            // 
            // numDelay
            // 
            this.numDelay.Location = new System.Drawing.Point(89, 70);
            this.numDelay.Margin = new System.Windows.Forms.Padding(2);
            this.numDelay.Name = "numDelay";
            this.numDelay.Size = new System.Drawing.Size(33, 20);
            this.numDelay.TabIndex = 73;
            this.numDelay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // grpAdvanced
            // 
            this.grpAdvanced.AutoSize = true;
            this.grpAdvanced.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grpAdvanced.Controls.Add(this.txtHostIP);
            this.grpAdvanced.Controls.Add(this.cboMethod);
            this.grpAdvanced.Controls.Add(this.lblMethod);
            this.grpAdvanced.Controls.Add(this.btnSavePreset);
            this.grpAdvanced.Controls.Add(this.btnDeletePreset);
            this.grpAdvanced.Controls.Add(this.cboPresetName);
            this.grpAdvanced.Controls.Add(this.lblHostIP);
            this.grpAdvanced.Controls.Add(this.txtHostPort);
            this.grpAdvanced.Controls.Add(this.lblGuestPort);
            this.grpAdvanced.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpAdvanced.Location = new System.Drawing.Point(0, 0);
            this.grpAdvanced.Margin = new System.Windows.Forms.Padding(2);
            this.grpAdvanced.Name = "grpAdvanced";
            this.grpAdvanced.Padding = new System.Windows.Forms.Padding(4);
            this.grpAdvanced.Size = new System.Drawing.Size(150, 46);
            this.grpAdvanced.TabIndex = 59;
            this.grpAdvanced.TabStop = false;
            this.grpAdvanced.Text = "Advanced Options";
            // 
            // txtHostIP
            // 
            this.txtHostIP.Location = new System.Drawing.Point(83, 55);
            this.txtHostIP.Name = "txtHostIP";
            this.txtHostIP.Size = new System.Drawing.Size(105, 20);
            this.txtHostIP.TabIndex = 72;
            // 
            // cboMethod
            // 
            this.cboMethod.FormattingEnabled = true;
            this.cboMethod.Location = new System.Drawing.Point(83, 95);
            this.cboMethod.Margin = new System.Windows.Forms.Padding(2);
            this.cboMethod.Name = "cboMethod";
            this.cboMethod.Size = new System.Drawing.Size(105, 21);
            this.cboMethod.TabIndex = 71;
            // 
            // lblMethod
            // 
            this.lblMethod.AutoSize = true;
            this.lblMethod.Location = new System.Drawing.Point(28, 98);
            this.lblMethod.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMethod.Name = "lblMethod";
            this.lblMethod.Size = new System.Drawing.Size(43, 13);
            this.lblMethod.TabIndex = 70;
            this.lblMethod.Text = "Method";
            // 
            // btnSavePreset
            // 
            this.btnSavePreset.Image = ((System.Drawing.Image)(resources.GetObject("btnSavePreset.Image")));
            this.btnSavePreset.Location = new System.Drawing.Point(166, 24);
            this.btnSavePreset.Margin = new System.Windows.Forms.Padding(2);
            this.btnSavePreset.Name = "btnSavePreset";
            this.btnSavePreset.Size = new System.Drawing.Size(22, 22);
            this.btnSavePreset.TabIndex = 62;
            this.btnSavePreset.UseVisualStyleBackColor = true;
            this.btnSavePreset.Click += new System.EventHandler(this.btnSavePreset_Click);
            // 
            // btnDeletePreset
            // 
            this.btnDeletePreset.Image = ((System.Drawing.Image)(resources.GetObject("btnDeletePreset.Image")));
            this.btnDeletePreset.Location = new System.Drawing.Point(144, 24);
            this.btnDeletePreset.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeletePreset.Name = "btnDeletePreset";
            this.btnDeletePreset.Size = new System.Drawing.Size(22, 22);
            this.btnDeletePreset.TabIndex = 60;
            this.btnDeletePreset.UseVisualStyleBackColor = true;
            this.btnDeletePreset.Click += new System.EventHandler(this.btnDeletePreset_Click);
            // 
            // cboPresetName
            // 
            this.cboPresetName.FormattingEnabled = true;
            this.cboPresetName.Location = new System.Drawing.Point(31, 26);
            this.cboPresetName.Margin = new System.Windows.Forms.Padding(2);
            this.cboPresetName.Name = "cboPresetName";
            this.cboPresetName.Size = new System.Drawing.Size(108, 21);
            this.cboPresetName.TabIndex = 58;
            this.cboPresetName.SelectedIndexChanged += new System.EventHandler(this.cboPresetName_SelectedIndexChanged);
            // 
            // lblHostIP
            // 
            this.lblHostIP.AutoSize = true;
            this.lblHostIP.Location = new System.Drawing.Point(28, 58);
            this.lblHostIP.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHostIP.Name = "lblHostIP";
            this.lblHostIP.Size = new System.Drawing.Size(42, 13);
            this.lblHostIP.TabIndex = 69;
            this.lblHostIP.Text = "Host IP";
            // 
            // txtHostPort
            // 
            this.txtHostPort.Location = new System.Drawing.Point(83, 75);
            this.txtHostPort.Margin = new System.Windows.Forms.Padding(2);
            this.txtHostPort.Name = "txtHostPort";
            this.txtHostPort.Size = new System.Drawing.Size(105, 20);
            this.txtHostPort.TabIndex = 61;
            this.txtHostPort.Text = "27886";
            // 
            // lblGuestPort
            // 
            this.lblGuestPort.AutoSize = true;
            this.lblGuestPort.Location = new System.Drawing.Point(28, 78);
            this.lblGuestPort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGuestPort.Name = "lblGuestPort";
            this.lblGuestPort.Size = new System.Drawing.Size(51, 13);
            this.lblGuestPort.TabIndex = 66;
            this.lblGuestPort.Text = "Host Port";
            // 
            // JoinControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.splitGuest);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(216, 500);
            this.MinimumSize = new System.Drawing.Size(216, 300);
            this.Name = "JoinControl";
            this.Size = new System.Drawing.Size(216, 300);
            this.Load += new System.EventHandler(this.JoinControl_Load);
            this.splitGuest.Panel1.ResumeLayout(false);
            this.splitGuest.Panel1.PerformLayout();
            this.splitGuest.Panel2.ResumeLayout(false);
            this.splitGuest.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitGuest)).EndInit();
            this.splitGuest.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).EndInit();
            this.grpAdvanced.ResumeLayout(false);
            this.grpAdvanced.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitGuest;
        private System.Windows.Forms.TextBox txtOpponentIP;
        private System.Windows.Forms.Button btnLaunchGame;
        private System.Windows.Forms.TextBox txtHostCode;
        private System.Windows.Forms.Label lblVerifyDelay;
        private System.Windows.Forms.Label lblGuestnameIP;
        private System.Windows.Forms.Button btnGuess;
        private System.Windows.Forms.NumericUpDown numDelay;
        private System.Windows.Forms.GroupBox grpAdvanced;
        private System.Windows.Forms.ComboBox cboMethod;
        private System.Windows.Forms.Label lblMethod;
        private System.Windows.Forms.Button btnSavePreset;
        private System.Windows.Forms.Button btnDeletePreset;
        private System.Windows.Forms.ComboBox cboPresetName;
        private System.Windows.Forms.Label lblHostIP;
        private System.Windows.Forms.TextBox txtHostPort;
        private System.Windows.Forms.Label lblGuestPort;
        private System.Windows.Forms.Button btnExpandCollapse;
        private System.Windows.Forms.Label lblHostCode;
        private System.Windows.Forms.Button btnPaste;
        private System.Windows.Forms.TextBox txtHostIP;
    }
}
