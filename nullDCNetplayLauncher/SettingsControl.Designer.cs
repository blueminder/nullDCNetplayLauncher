namespace nullDCNetplayLauncher
{
    partial class SettingsControl
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
            this.chkEnableMapper = new System.Windows.Forms.CheckBox();
            this.lblPlayer1 = new System.Windows.Forms.Label();
            this.lblBackup = new System.Windows.Forms.Label();
            this.lblPlayer2 = new System.Windows.Forms.Label();
            this.cboPlayer1 = new System.Windows.Forms.ComboBox();
            this.cboBackup = new System.Windows.Forms.ComboBox();
            this.cboPlayer2 = new System.Windows.Forms.ComboBox();
            this.rdoDefault = new System.Windows.Forms.RadioButton();
            this.rdoStartMax = new System.Windows.Forms.RadioButton();
            this.rdoCustomSize = new System.Windows.Forms.RadioButton();
            this.txtWindowX = new System.Windows.Forms.TextBox();
            this.txtWindowY = new System.Windows.Forms.TextBox();
            this.btnGrabWindowSize = new System.Windows.Forms.Button();
            this.btnEditCFG = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabInput = new System.Windows.Forms.TabPage();
            this.btnSaveInput = new System.Windows.Forms.Button();
            this.tabWindow = new System.Windows.Forms.TabPage();
            this.btnSaveWindow = new System.Windows.Forms.Button();
            this.tabAdvanced = new System.Windows.Forms.TabPage();
            this.grpShortcuts = new System.Windows.Forms.GroupBox();
            this.btnLaunchAntiMicro = new System.Windows.Forms.Button();
            this.btnOpenQKO = new System.Windows.Forms.Button();
            this.btnJoyCpl = new System.Windows.Forms.Button();
            this.btnSaveFPS = new System.Windows.Forms.Button();
            this.numGuestFPS = new System.Windows.Forms.NumericUpDown();
            this.numHostFPS = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabInput.SuspendLayout();
            this.tabWindow.SuspendLayout();
            this.tabAdvanced.SuspendLayout();
            this.grpShortcuts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGuestFPS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHostFPS)).BeginInit();
            this.SuspendLayout();
            // 
            // chkEnableMapper
            // 
            this.chkEnableMapper.AutoSize = true;
            this.chkEnableMapper.Location = new System.Drawing.Point(9, 6);
            this.chkEnableMapper.Name = "chkEnableMapper";
            this.chkEnableMapper.Size = new System.Drawing.Size(192, 21);
            this.chkEnableMapper.TabIndex = 4;
            this.chkEnableMapper.Text = "Enable Gamepad Mapper";
            this.chkEnableMapper.UseVisualStyleBackColor = true;
            this.chkEnableMapper.CheckedChanged += new System.EventHandler(this.chkEnableMapper_CheckedChanged);
            // 
            // lblPlayer1
            // 
            this.lblPlayer1.AutoSize = true;
            this.lblPlayer1.Location = new System.Drawing.Point(6, 39);
            this.lblPlayer1.Name = "lblPlayer1";
            this.lblPlayer1.Size = new System.Drawing.Size(60, 17);
            this.lblPlayer1.TabIndex = 5;
            this.lblPlayer1.Text = "Player 1";
            // 
            // lblBackup
            // 
            this.lblBackup.AutoSize = true;
            this.lblBackup.Location = new System.Drawing.Point(6, 68);
            this.lblBackup.Name = "lblBackup";
            this.lblBackup.Size = new System.Drawing.Size(55, 17);
            this.lblBackup.TabIndex = 6;
            this.lblBackup.Text = "Backup";
            // 
            // lblPlayer2
            // 
            this.lblPlayer2.AutoSize = true;
            this.lblPlayer2.Location = new System.Drawing.Point(6, 99);
            this.lblPlayer2.Name = "lblPlayer2";
            this.lblPlayer2.Size = new System.Drawing.Size(60, 17);
            this.lblPlayer2.TabIndex = 7;
            this.lblPlayer2.Text = "Player 2";
            // 
            // cboPlayer1
            // 
            this.cboPlayer1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPlayer1.FormattingEnabled = true;
            this.cboPlayer1.Items.AddRange(new object[] {
            "Keyboard",
            "Joystick 1",
            "Joystick 2"});
            this.cboPlayer1.Location = new System.Drawing.Point(73, 36);
            this.cboPlayer1.Name = "cboPlayer1";
            this.cboPlayer1.Size = new System.Drawing.Size(121, 24);
            this.cboPlayer1.TabIndex = 8;
            // 
            // cboBackup
            // 
            this.cboBackup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBackup.FormattingEnabled = true;
            this.cboBackup.Items.AddRange(new object[] {
            "Keyboard",
            "Joystick 1",
            "Joystick 2"});
            this.cboBackup.Location = new System.Drawing.Point(73, 65);
            this.cboBackup.Name = "cboBackup";
            this.cboBackup.Size = new System.Drawing.Size(121, 24);
            this.cboBackup.TabIndex = 9;
            // 
            // cboPlayer2
            // 
            this.cboPlayer2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPlayer2.FormattingEnabled = true;
            this.cboPlayer2.Items.AddRange(new object[] {
            "Keyboard",
            "Joystick 1",
            "Joystick 2"});
            this.cboPlayer2.Location = new System.Drawing.Point(73, 96);
            this.cboPlayer2.Name = "cboPlayer2";
            this.cboPlayer2.Size = new System.Drawing.Size(121, 24);
            this.cboPlayer2.TabIndex = 10;
            // 
            // rdoDefault
            // 
            this.rdoDefault.AutoSize = true;
            this.rdoDefault.Checked = true;
            this.rdoDefault.Location = new System.Drawing.Point(6, 6);
            this.rdoDefault.Name = "rdoDefault";
            this.rdoDefault.Size = new System.Drawing.Size(74, 21);
            this.rdoDefault.TabIndex = 0;
            this.rdoDefault.TabStop = true;
            this.rdoDefault.Text = "Default";
            this.rdoDefault.UseVisualStyleBackColor = true;
            // 
            // rdoStartMax
            // 
            this.rdoStartMax.AutoSize = true;
            this.rdoStartMax.Location = new System.Drawing.Point(6, 36);
            this.rdoStartMax.Name = "rdoStartMax";
            this.rdoStartMax.Size = new System.Drawing.Size(128, 21);
            this.rdoStartMax.TabIndex = 1;
            this.rdoStartMax.Text = "Start Maximized";
            this.rdoStartMax.UseVisualStyleBackColor = true;
            // 
            // rdoCustomSize
            // 
            this.rdoCustomSize.AutoSize = true;
            this.rdoCustomSize.Location = new System.Drawing.Point(6, 66);
            this.rdoCustomSize.Name = "rdoCustomSize";
            this.rdoCustomSize.Size = new System.Drawing.Size(107, 21);
            this.rdoCustomSize.TabIndex = 2;
            this.rdoCustomSize.Text = "Custom Size";
            this.rdoCustomSize.UseVisualStyleBackColor = true;
            // 
            // txtWindowX
            // 
            this.txtWindowX.Location = new System.Drawing.Point(34, 93);
            this.txtWindowX.Name = "txtWindowX";
            this.txtWindowX.Size = new System.Drawing.Size(40, 22);
            this.txtWindowX.TabIndex = 3;
            this.txtWindowX.Text = "800";
            this.txtWindowX.GotFocus += new System.EventHandler(this.txtWindowX_GotFocus);
            // 
            // txtWindowY
            // 
            this.txtWindowY.Location = new System.Drawing.Point(80, 93);
            this.txtWindowY.Name = "txtWindowY";
            this.txtWindowY.Size = new System.Drawing.Size(40, 22);
            this.txtWindowY.TabIndex = 4;
            this.txtWindowY.Text = "600";
            this.txtWindowY.GotFocus += new System.EventHandler(this.txtWindowY_GotFocus);
            // 
            // btnGrabWindowSize
            // 
            this.btnGrabWindowSize.Location = new System.Drawing.Point(129, 92);
            this.btnGrabWindowSize.Name = "btnGrabWindowSize";
            this.btnGrabWindowSize.Size = new System.Drawing.Size(65, 25);
            this.btnGrabWindowSize.TabIndex = 5;
            this.btnGrabWindowSize.Text = "Grab";
            this.btnGrabWindowSize.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnGrabWindowSize.UseVisualStyleBackColor = true;
            this.btnGrabWindowSize.Click += new System.EventHandler(this.btnGrabWindowSize_Click);
            // 
            // btnEditCFG
            // 
            this.btnEditCFG.Location = new System.Drawing.Point(9, 48);
            this.btnEditCFG.Name = "btnEditCFG";
            this.btnEditCFG.Size = new System.Drawing.Size(190, 25);
            this.btnEditCFG.TabIndex = 4;
            this.btnEditCFG.Text = "Edit nullDC.cfg";
            this.btnEditCFG.UseVisualStyleBackColor = true;
            this.btnEditCFG.Click += new System.EventHandler(this.btnEditCFG_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabInput);
            this.tabControl1.Controls.Add(this.tabWindow);
            this.tabControl1.Controls.Add(this.tabAdvanced);
            this.tabControl1.Location = new System.Drawing.Point(4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(215, 272);
            this.tabControl1.TabIndex = 11;
            // 
            // tabInput
            // 
            this.tabInput.Controls.Add(this.btnSaveInput);
            this.tabInput.Controls.Add(this.cboPlayer2);
            this.tabInput.Controls.Add(this.chkEnableMapper);
            this.tabInput.Controls.Add(this.cboBackup);
            this.tabInput.Controls.Add(this.lblPlayer1);
            this.tabInput.Controls.Add(this.cboPlayer1);
            this.tabInput.Controls.Add(this.lblBackup);
            this.tabInput.Controls.Add(this.lblPlayer2);
            this.tabInput.Location = new System.Drawing.Point(4, 25);
            this.tabInput.Name = "tabInput";
            this.tabInput.Padding = new System.Windows.Forms.Padding(3);
            this.tabInput.Size = new System.Drawing.Size(207, 243);
            this.tabInput.TabIndex = 0;
            this.tabInput.Text = "Input";
            this.tabInput.UseVisualStyleBackColor = true;
            // 
            // btnSaveInput
            // 
            this.btnSaveInput.Location = new System.Drawing.Point(9, 214);
            this.btnSaveInput.Name = "btnSaveInput";
            this.btnSaveInput.Size = new System.Drawing.Size(188, 23);
            this.btnSaveInput.TabIndex = 11;
            this.btnSaveInput.Text = "Save";
            this.btnSaveInput.UseVisualStyleBackColor = true;
            this.btnSaveInput.Click += new System.EventHandler(this.btnSaveInput_Click);
            // 
            // tabWindow
            // 
            this.tabWindow.Controls.Add(this.btnSaveWindow);
            this.tabWindow.Controls.Add(this.btnGrabWindowSize);
            this.tabWindow.Controls.Add(this.rdoDefault);
            this.tabWindow.Controls.Add(this.txtWindowX);
            this.tabWindow.Controls.Add(this.txtWindowY);
            this.tabWindow.Controls.Add(this.rdoStartMax);
            this.tabWindow.Controls.Add(this.rdoCustomSize);
            this.tabWindow.Location = new System.Drawing.Point(4, 25);
            this.tabWindow.Name = "tabWindow";
            this.tabWindow.Padding = new System.Windows.Forms.Padding(3);
            this.tabWindow.Size = new System.Drawing.Size(207, 243);
            this.tabWindow.TabIndex = 1;
            this.tabWindow.Text = "Window";
            this.tabWindow.UseVisualStyleBackColor = true;
            // 
            // btnSaveWindow
            // 
            this.btnSaveWindow.Location = new System.Drawing.Point(9, 214);
            this.btnSaveWindow.Name = "btnSaveWindow";
            this.btnSaveWindow.Size = new System.Drawing.Size(188, 23);
            this.btnSaveWindow.TabIndex = 6;
            this.btnSaveWindow.Text = "Save";
            this.btnSaveWindow.UseVisualStyleBackColor = true;
            this.btnSaveWindow.Click += new System.EventHandler(this.btnSaveWindow_Click);
            // 
            // tabAdvanced
            // 
            this.tabAdvanced.Controls.Add(this.grpShortcuts);
            this.tabAdvanced.Controls.Add(this.btnSaveFPS);
            this.tabAdvanced.Controls.Add(this.numGuestFPS);
            this.tabAdvanced.Controls.Add(this.numHostFPS);
            this.tabAdvanced.Controls.Add(this.label2);
            this.tabAdvanced.Controls.Add(this.label1);
            this.tabAdvanced.Location = new System.Drawing.Point(4, 25);
            this.tabAdvanced.Name = "tabAdvanced";
            this.tabAdvanced.Padding = new System.Windows.Forms.Padding(3);
            this.tabAdvanced.Size = new System.Drawing.Size(207, 243);
            this.tabAdvanced.TabIndex = 2;
            this.tabAdvanced.Text = "Advanced";
            this.tabAdvanced.UseVisualStyleBackColor = true;
            // 
            // grpShortcuts
            // 
            this.grpShortcuts.Controls.Add(this.btnLaunchAntiMicro);
            this.grpShortcuts.Controls.Add(this.btnEditCFG);
            this.grpShortcuts.Controls.Add(this.btnOpenQKO);
            this.grpShortcuts.Controls.Add(this.btnJoyCpl);
            this.grpShortcuts.Location = new System.Drawing.Point(3, 105);
            this.grpShortcuts.Name = "grpShortcuts";
            this.grpShortcuts.Size = new System.Drawing.Size(200, 135);
            this.grpShortcuts.TabIndex = 12;
            this.grpShortcuts.TabStop = false;
            this.grpShortcuts.Text = "Shortcuts";
            // 
            // btnLaunchAntiMicro
            // 
            this.btnLaunchAntiMicro.Location = new System.Drawing.Point(9, 21);
            this.btnLaunchAntiMicro.Name = "btnLaunchAntiMicro";
            this.btnLaunchAntiMicro.Size = new System.Drawing.Size(190, 25);
            this.btnLaunchAntiMicro.TabIndex = 8;
            this.btnLaunchAntiMicro.Text = "Launch AntiMicro";
            this.btnLaunchAntiMicro.UseVisualStyleBackColor = true;
            // 
            // btnOpenQKO
            // 
            this.btnOpenQKO.Location = new System.Drawing.Point(9, 75);
            this.btnOpenQKO.Name = "btnOpenQKO";
            this.btnOpenQKO.Size = new System.Drawing.Size(190, 25);
            this.btnOpenQKO.TabIndex = 5;
            this.btnOpenQKO.Text = "Open qkoJAMMA Folder";
            this.btnOpenQKO.UseVisualStyleBackColor = true;
            this.btnOpenQKO.Click += new System.EventHandler(this.btnOpenQKO_Click);
            // 
            // btnJoyCpl
            // 
            this.btnJoyCpl.Location = new System.Drawing.Point(9, 102);
            this.btnJoyCpl.Name = "btnJoyCpl";
            this.btnJoyCpl.Size = new System.Drawing.Size(190, 25);
            this.btnJoyCpl.TabIndex = 6;
            this.btnJoyCpl.Text = "Windows Game Controllers";
            this.btnJoyCpl.UseVisualStyleBackColor = true;
            this.btnJoyCpl.Click += new System.EventHandler(this.btnJoyCpl_Click);
            // 
            // btnSaveFPS
            // 
            this.btnSaveFPS.Location = new System.Drawing.Point(18, 64);
            this.btnSaveFPS.Name = "btnSaveFPS";
            this.btnSaveFPS.Size = new System.Drawing.Size(173, 23);
            this.btnSaveFPS.TabIndex = 11;
            this.btnSaveFPS.Text = "Save FPS Limits";
            this.btnSaveFPS.UseVisualStyleBackColor = true;
            this.btnSaveFPS.Click += new System.EventHandler(this.btnSaveFPS_Click);
            // 
            // numGuestFPS
            // 
            this.numGuestFPS.Location = new System.Drawing.Point(144, 36);
            this.numGuestFPS.Name = "numGuestFPS";
            this.numGuestFPS.Size = new System.Drawing.Size(47, 22);
            this.numGuestFPS.TabIndex = 10;
            // 
            // numHostFPS
            // 
            this.numHostFPS.Location = new System.Drawing.Point(144, 8);
            this.numHostFPS.Name = "numHostFPS";
            this.numHostFPS.Size = new System.Drawing.Size(47, 22);
            this.numHostFPS.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Guest FPS Limit";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Host FPS Limit";
            // 
            // SettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "SettingsControl";
            this.Size = new System.Drawing.Size(222, 279);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabInput.ResumeLayout(false);
            this.tabInput.PerformLayout();
            this.tabWindow.ResumeLayout(false);
            this.tabWindow.PerformLayout();
            this.tabAdvanced.ResumeLayout(false);
            this.tabAdvanced.PerformLayout();
            this.grpShortcuts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numGuestFPS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHostFPS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox cboPlayer2;
        private System.Windows.Forms.ComboBox cboBackup;
        private System.Windows.Forms.ComboBox cboPlayer1;
        private System.Windows.Forms.Label lblPlayer2;
        private System.Windows.Forms.Label lblBackup;
        private System.Windows.Forms.Label lblPlayer1;
        private System.Windows.Forms.CheckBox chkEnableMapper;
        private System.Windows.Forms.Button btnGrabWindowSize;
        private System.Windows.Forms.TextBox txtWindowY;
        private System.Windows.Forms.TextBox txtWindowX;
        private System.Windows.Forms.RadioButton rdoCustomSize;
        private System.Windows.Forms.RadioButton rdoStartMax;
        private System.Windows.Forms.RadioButton rdoDefault;
        private System.Windows.Forms.Button btnEditCFG;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabInput;
        private System.Windows.Forms.Button btnSaveInput;
        private System.Windows.Forms.TabPage tabWindow;
        private System.Windows.Forms.Button btnSaveWindow;
        private System.Windows.Forms.TabPage tabAdvanced;
        private System.Windows.Forms.GroupBox grpShortcuts;
        private System.Windows.Forms.Button btnLaunchAntiMicro;
        private System.Windows.Forms.Button btnOpenQKO;
        private System.Windows.Forms.Button btnJoyCpl;
        private System.Windows.Forms.Button btnSaveFPS;
        private System.Windows.Forms.NumericUpDown numGuestFPS;
        private System.Windows.Forms.NumericUpDown numHostFPS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}