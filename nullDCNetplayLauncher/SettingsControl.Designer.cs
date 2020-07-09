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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsControl));
            this.chkEnableMapper = new System.Windows.Forms.CheckBox();
            this.lblPlayer1 = new System.Windows.Forms.Label();
            this.cboPlayer1 = new System.Windows.Forms.ComboBox();
            this.rdoDefault = new System.Windows.Forms.RadioButton();
            this.rdoStartMax = new System.Windows.Forms.RadioButton();
            this.rdoCustomSize = new System.Windows.Forms.RadioButton();
            this.txtWindowX = new System.Windows.Forms.TextBox();
            this.txtWindowY = new System.Windows.Forms.TextBox();
            this.btnGrabWindowSize = new System.Windows.Forms.Button();
            this.btnEditCFG = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabInput = new System.Windows.Forms.TabPage();
            this.chkCustomCFG = new System.Windows.Forms.CheckBox();
            this.cboRegion = new System.Windows.Forms.ComboBox();
            this.lblRegion = new System.Windows.Forms.Label();
            this.grpInput = new System.Windows.Forms.GroupBox();
            this.btnDeleteMapping = new System.Windows.Forms.Button();
            this.cboGamePadMappings = new System.Windows.Forms.ComboBox();
            this.btnSaveInput = new System.Windows.Forms.Button();
            this.tabWindow = new System.Windows.Forms.TabPage();
            this.btnSaveWindow = new System.Windows.Forms.Button();
            this.tabAdvanced = new System.Windows.Forms.TabPage();
            this.lblVersion = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.lblCurrentVersion = new System.Windows.Forms.Label();
            this.grpShortcuts = new System.Windows.Forms.GroupBox();
            this.btnOpenQKO = new System.Windows.Forms.Button();
            this.btnJoyCpl = new System.Windows.Forms.Button();
            this.btnSaveFPS = new System.Windows.Forms.Button();
            this.numHostFPS = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.chkVKBMapper = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabInput.SuspendLayout();
            this.grpInput.SuspendLayout();
            this.tabWindow.SuspendLayout();
            this.tabAdvanced.SuspendLayout();
            this.grpShortcuts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHostFPS)).BeginInit();
            this.SuspendLayout();
            // 
            // chkEnableMapper
            // 
            this.chkEnableMapper.AutoSize = true;
            this.chkEnableMapper.Location = new System.Drawing.Point(8, 64);
            this.chkEnableMapper.Margin = new System.Windows.Forms.Padding(2);
            this.chkEnableMapper.Name = "chkEnableMapper";
            this.chkEnableMapper.Size = new System.Drawing.Size(146, 17);
            this.chkEnableMapper.TabIndex = 4;
            this.chkEnableMapper.Text = "Enable Keyboard Mapper";
            this.chkEnableMapper.UseVisualStyleBackColor = true;
            this.chkEnableMapper.CheckedChanged += new System.EventHandler(this.chkEnableMapper_CheckedChanged);
            // 
            // lblPlayer1
            // 
            this.lblPlayer1.AutoSize = true;
            this.lblPlayer1.Location = new System.Drawing.Point(5, 21);
            this.lblPlayer1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPlayer1.Name = "lblPlayer1";
            this.lblPlayer1.Size = new System.Drawing.Size(41, 13);
            this.lblPlayer1.TabIndex = 5;
            this.lblPlayer1.Text = "Device";
            // 
            // cboPlayer1
            // 
            this.cboPlayer1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPlayer1.FormattingEnabled = true;
            this.cboPlayer1.Items.AddRange(new object[] {
            "Keyboard",
            "Joystick 1",
            "Joystick 2"});
            this.cboPlayer1.Location = new System.Drawing.Point(55, 18);
            this.cboPlayer1.Margin = new System.Windows.Forms.Padding(2);
            this.cboPlayer1.Name = "cboPlayer1";
            this.cboPlayer1.Size = new System.Drawing.Size(92, 21);
            this.cboPlayer1.TabIndex = 8;
            // 
            // rdoDefault
            // 
            this.rdoDefault.AutoSize = true;
            this.rdoDefault.Checked = true;
            this.rdoDefault.Location = new System.Drawing.Point(11, 5);
            this.rdoDefault.Margin = new System.Windows.Forms.Padding(2);
            this.rdoDefault.Name = "rdoDefault";
            this.rdoDefault.Size = new System.Drawing.Size(59, 17);
            this.rdoDefault.TabIndex = 0;
            this.rdoDefault.TabStop = true;
            this.rdoDefault.Text = "Default";
            this.rdoDefault.UseVisualStyleBackColor = true;
            // 
            // rdoStartMax
            // 
            this.rdoStartMax.AutoSize = true;
            this.rdoStartMax.Location = new System.Drawing.Point(11, 29);
            this.rdoStartMax.Margin = new System.Windows.Forms.Padding(2);
            this.rdoStartMax.Name = "rdoStartMax";
            this.rdoStartMax.Size = new System.Drawing.Size(99, 17);
            this.rdoStartMax.TabIndex = 1;
            this.rdoStartMax.Text = "Start Maximized";
            this.rdoStartMax.UseVisualStyleBackColor = true;
            // 
            // rdoCustomSize
            // 
            this.rdoCustomSize.AutoSize = true;
            this.rdoCustomSize.Location = new System.Drawing.Point(11, 54);
            this.rdoCustomSize.Margin = new System.Windows.Forms.Padding(2);
            this.rdoCustomSize.Name = "rdoCustomSize";
            this.rdoCustomSize.Size = new System.Drawing.Size(83, 17);
            this.rdoCustomSize.TabIndex = 2;
            this.rdoCustomSize.Text = "Custom Size";
            this.rdoCustomSize.UseVisualStyleBackColor = true;
            // 
            // txtWindowX
            // 
            this.txtWindowX.Location = new System.Drawing.Point(33, 76);
            this.txtWindowX.Margin = new System.Windows.Forms.Padding(2);
            this.txtWindowX.Name = "txtWindowX";
            this.txtWindowX.Size = new System.Drawing.Size(31, 20);
            this.txtWindowX.TabIndex = 3;
            this.txtWindowX.Text = "800";
            this.txtWindowX.GotFocus += new System.EventHandler(this.txtWindowX_GotFocus);
            // 
            // txtWindowY
            // 
            this.txtWindowY.Location = new System.Drawing.Point(67, 76);
            this.txtWindowY.Margin = new System.Windows.Forms.Padding(2);
            this.txtWindowY.Name = "txtWindowY";
            this.txtWindowY.Size = new System.Drawing.Size(31, 20);
            this.txtWindowY.TabIndex = 4;
            this.txtWindowY.Text = "600";
            this.txtWindowY.GotFocus += new System.EventHandler(this.txtWindowY_GotFocus);
            // 
            // btnGrabWindowSize
            // 
            this.btnGrabWindowSize.Location = new System.Drawing.Point(104, 75);
            this.btnGrabWindowSize.Margin = new System.Windows.Forms.Padding(2);
            this.btnGrabWindowSize.Name = "btnGrabWindowSize";
            this.btnGrabWindowSize.Size = new System.Drawing.Size(49, 20);
            this.btnGrabWindowSize.TabIndex = 5;
            this.btnGrabWindowSize.Text = "Grab";
            this.btnGrabWindowSize.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnGrabWindowSize.UseVisualStyleBackColor = true;
            this.btnGrabWindowSize.Click += new System.EventHandler(this.btnGrabWindowSize_Click);
            // 
            // btnEditCFG
            // 
            this.btnEditCFG.Location = new System.Drawing.Point(4, 17);
            this.btnEditCFG.Margin = new System.Windows.Forms.Padding(2);
            this.btnEditCFG.Name = "btnEditCFG";
            this.btnEditCFG.Size = new System.Drawing.Size(142, 23);
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
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(173, 221);
            this.tabControl1.TabIndex = 11;
            // 
            // tabInput
            // 
            this.tabInput.Controls.Add(this.chkCustomCFG);
            this.tabInput.Controls.Add(this.cboRegion);
            this.tabInput.Controls.Add(this.lblRegion);
            this.tabInput.Controls.Add(this.grpInput);
            this.tabInput.Controls.Add(this.btnSaveInput);
            this.tabInput.Location = new System.Drawing.Point(4, 22);
            this.tabInput.Margin = new System.Windows.Forms.Padding(2);
            this.tabInput.Name = "tabInput";
            this.tabInput.Padding = new System.Windows.Forms.Padding(2);
            this.tabInput.Size = new System.Drawing.Size(165, 195);
            this.tabInput.TabIndex = 0;
            this.tabInput.Text = "Main";
            this.tabInput.UseVisualStyleBackColor = true;
            // 
            // chkCustomCFG
            // 
            this.chkCustomCFG.AutoSize = true;
            this.chkCustomCFG.Location = new System.Drawing.Point(10, 152);
            this.chkCustomCFG.Name = "chkCustomCFG";
            this.chkCustomCFG.Size = new System.Drawing.Size(113, 17);
            this.chkCustomCFG.TabIndex = 57;
            this.chkCustomCFG.Text = "Allow Custom CFG";
            this.chkCustomCFG.UseVisualStyleBackColor = true;
            // 
            // cboRegion
            // 
            this.cboRegion.FormattingEnabled = true;
            this.cboRegion.Location = new System.Drawing.Point(88, 123);
            this.cboRegion.Name = "cboRegion";
            this.cboRegion.Size = new System.Drawing.Size(66, 21);
            this.cboRegion.TabIndex = 56;
            // 
            // lblRegion
            // 
            this.lblRegion.AutoSize = true;
            this.lblRegion.Location = new System.Drawing.Point(9, 126);
            this.lblRegion.Name = "lblRegion";
            this.lblRegion.Size = new System.Drawing.Size(74, 13);
            this.lblRegion.TabIndex = 55;
            this.lblRegion.Text = "Offline Region";
            // 
            // grpInput
            // 
            this.grpInput.Controls.Add(this.chkVKBMapper);
            this.grpInput.Controls.Add(this.cboPlayer1);
            this.grpInput.Controls.Add(this.btnDeleteMapping);
            this.grpInput.Controls.Add(this.lblPlayer1);
            this.grpInput.Controls.Add(this.cboGamePadMappings);
            this.grpInput.Controls.Add(this.chkEnableMapper);
            this.grpInput.Location = new System.Drawing.Point(5, 5);
            this.grpInput.Name = "grpInput";
            this.grpInput.Size = new System.Drawing.Size(155, 112);
            this.grpInput.TabIndex = 54;
            this.grpInput.TabStop = false;
            this.grpInput.Text = "Input";
            // 
            // btnDeleteMapping
            // 
            this.btnDeleteMapping.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteMapping.Image")));
            this.btnDeleteMapping.Location = new System.Drawing.Point(125, 84);
            this.btnDeleteMapping.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeleteMapping.Name = "btnDeleteMapping";
            this.btnDeleteMapping.Size = new System.Drawing.Size(22, 22);
            this.btnDeleteMapping.TabIndex = 53;
            this.btnDeleteMapping.UseVisualStyleBackColor = true;
            this.btnDeleteMapping.Click += new System.EventHandler(this.btnDeleteMapping_Click);
            // 
            // cboGamePadMappings
            // 
            this.cboGamePadMappings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGamePadMappings.Enabled = false;
            this.cboGamePadMappings.FormattingEnabled = true;
            this.cboGamePadMappings.Location = new System.Drawing.Point(5, 84);
            this.cboGamePadMappings.Margin = new System.Windows.Forms.Padding(2);
            this.cboGamePadMappings.Name = "cboGamePadMappings";
            this.cboGamePadMappings.Size = new System.Drawing.Size(113, 21);
            this.cboGamePadMappings.TabIndex = 12;
            this.cboGamePadMappings.SelectedIndexChanged += new System.EventHandler(this.cboGamePadMappings_SelectedIndexChanged);
            // 
            // btnSaveInput
            // 
            this.btnSaveInput.Location = new System.Drawing.Point(12, 174);
            this.btnSaveInput.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveInput.Name = "btnSaveInput";
            this.btnSaveInput.Size = new System.Drawing.Size(141, 19);
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
            this.tabWindow.Location = new System.Drawing.Point(4, 22);
            this.tabWindow.Margin = new System.Windows.Forms.Padding(2);
            this.tabWindow.Name = "tabWindow";
            this.tabWindow.Padding = new System.Windows.Forms.Padding(2);
            this.tabWindow.Size = new System.Drawing.Size(165, 195);
            this.tabWindow.TabIndex = 1;
            this.tabWindow.Text = "Window";
            this.tabWindow.UseVisualStyleBackColor = true;
            // 
            // btnSaveWindow
            // 
            this.btnSaveWindow.Location = new System.Drawing.Point(12, 174);
            this.btnSaveWindow.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveWindow.Name = "btnSaveWindow";
            this.btnSaveWindow.Size = new System.Drawing.Size(141, 19);
            this.btnSaveWindow.TabIndex = 6;
            this.btnSaveWindow.Text = "Save";
            this.btnSaveWindow.UseVisualStyleBackColor = true;
            this.btnSaveWindow.Click += new System.EventHandler(this.btnSaveWindow_Click);
            // 
            // tabAdvanced
            // 
            this.tabAdvanced.Controls.Add(this.lblVersion);
            this.tabAdvanced.Controls.Add(this.btnUpdate);
            this.tabAdvanced.Controls.Add(this.lblCurrentVersion);
            this.tabAdvanced.Controls.Add(this.grpShortcuts);
            this.tabAdvanced.Controls.Add(this.btnSaveFPS);
            this.tabAdvanced.Controls.Add(this.numHostFPS);
            this.tabAdvanced.Controls.Add(this.label1);
            this.tabAdvanced.Location = new System.Drawing.Point(4, 22);
            this.tabAdvanced.Margin = new System.Windows.Forms.Padding(2);
            this.tabAdvanced.Name = "tabAdvanced";
            this.tabAdvanced.Padding = new System.Windows.Forms.Padding(2);
            this.tabAdvanced.Size = new System.Drawing.Size(165, 195);
            this.tabAdvanced.TabIndex = 2;
            this.tabAdvanced.Text = "Advanced";
            this.tabAdvanced.UseVisualStyleBackColor = true;
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(117, 51);
            this.lblVersion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(42, 13);
            this.lblVersion.TabIndex = 15;
            this.lblVersion.Text = "Version";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(18, 69);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(130, 21);
            this.btnUpdate.TabIndex = 14;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // lblCurrentVersion
            // 
            this.lblCurrentVersion.AutoSize = true;
            this.lblCurrentVersion.Location = new System.Drawing.Point(15, 51);
            this.lblCurrentVersion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCurrentVersion.Name = "lblCurrentVersion";
            this.lblCurrentVersion.Size = new System.Drawing.Size(82, 13);
            this.lblCurrentVersion.TabIndex = 13;
            this.lblCurrentVersion.Text = "Current Version:";
            // 
            // grpShortcuts
            // 
            this.grpShortcuts.Controls.Add(this.btnEditCFG);
            this.grpShortcuts.Controls.Add(this.btnOpenQKO);
            this.grpShortcuts.Controls.Add(this.btnJoyCpl);
            this.grpShortcuts.Location = new System.Drawing.Point(6, 94);
            this.grpShortcuts.Margin = new System.Windows.Forms.Padding(2);
            this.grpShortcuts.Name = "grpShortcuts";
            this.grpShortcuts.Padding = new System.Windows.Forms.Padding(2);
            this.grpShortcuts.Size = new System.Drawing.Size(150, 101);
            this.grpShortcuts.TabIndex = 12;
            this.grpShortcuts.TabStop = false;
            this.grpShortcuts.Text = "Shortcuts";
            // 
            // btnOpenQKO
            // 
            this.btnOpenQKO.Location = new System.Drawing.Point(4, 45);
            this.btnOpenQKO.Margin = new System.Windows.Forms.Padding(2);
            this.btnOpenQKO.Name = "btnOpenQKO";
            this.btnOpenQKO.Size = new System.Drawing.Size(142, 23);
            this.btnOpenQKO.TabIndex = 5;
            this.btnOpenQKO.Text = "Open qkoJAMMA Folder";
            this.btnOpenQKO.UseVisualStyleBackColor = true;
            this.btnOpenQKO.Click += new System.EventHandler(this.btnOpenQKO_Click);
            // 
            // btnJoyCpl
            // 
            this.btnJoyCpl.Location = new System.Drawing.Point(4, 72);
            this.btnJoyCpl.Margin = new System.Windows.Forms.Padding(2);
            this.btnJoyCpl.Name = "btnJoyCpl";
            this.btnJoyCpl.Size = new System.Drawing.Size(142, 23);
            this.btnJoyCpl.TabIndex = 6;
            this.btnJoyCpl.Text = "Windows Game Controllers";
            this.btnJoyCpl.UseVisualStyleBackColor = true;
            this.btnJoyCpl.Click += new System.EventHandler(this.btnJoyCpl_Click);
            // 
            // btnSaveFPS
            // 
            this.btnSaveFPS.Location = new System.Drawing.Point(18, 26);
            this.btnSaveFPS.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveFPS.Name = "btnSaveFPS";
            this.btnSaveFPS.Size = new System.Drawing.Size(130, 21);
            this.btnSaveFPS.TabIndex = 11;
            this.btnSaveFPS.Text = "Save FPS Limit";
            this.btnSaveFPS.UseVisualStyleBackColor = true;
            this.btnSaveFPS.Click += new System.EventHandler(this.btnSaveFPS_Click);
            // 
            // numHostFPS
            // 
            this.numHostFPS.Location = new System.Drawing.Point(112, 6);
            this.numHostFPS.Margin = new System.Windows.Forms.Padding(2);
            this.numHostFPS.Name = "numHostFPS";
            this.numHostFPS.Size = new System.Drawing.Size(35, 20);
            this.numHostFPS.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Host FPS Limit";
            // 
            // chkVKBMapper
            // 
            this.chkVKBMapper.AutoSize = true;
            this.chkVKBMapper.Location = new System.Drawing.Point(8, 44);
            this.chkVKBMapper.Name = "chkVKBMapper";
            this.chkVKBMapper.Size = new System.Drawing.Size(117, 17);
            this.chkVKBMapper.TabIndex = 54;
            this.chkVKBMapper.Text = "Enable Virtual Keys";
            this.chkVKBMapper.UseVisualStyleBackColor = true;
            // 
            // SettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SettingsControl";
            this.Size = new System.Drawing.Size(176, 227);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabInput.ResumeLayout(false);
            this.tabInput.PerformLayout();
            this.grpInput.ResumeLayout(false);
            this.grpInput.PerformLayout();
            this.tabWindow.ResumeLayout(false);
            this.tabWindow.PerformLayout();
            this.tabAdvanced.ResumeLayout(false);
            this.tabAdvanced.PerformLayout();
            this.grpShortcuts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numHostFPS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox cboPlayer1;
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
        private System.Windows.Forms.Button btnOpenQKO;
        private System.Windows.Forms.Button btnJoyCpl;
        private System.Windows.Forms.Button btnSaveFPS;
        private System.Windows.Forms.NumericUpDown numHostFPS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboGamePadMappings;
        private System.Windows.Forms.Button btnDeleteMapping;
        private System.Windows.Forms.Label lblCurrentVersion;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.ComboBox cboRegion;
        private System.Windows.Forms.Label lblRegion;
        private System.Windows.Forms.GroupBox grpInput;
        private System.Windows.Forms.CheckBox chkCustomCFG;
        private System.Windows.Forms.CheckBox chkVKBMapper;
    }
}