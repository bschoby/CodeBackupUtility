namespace CodeBackupUtility
{
    partial class Form1
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
            this.txtSourceFolder = new System.Windows.Forms.TextBox();
            this.lblSourceFolder = new System.Windows.Forms.Label();
            this.lblDestinationFolder = new System.Windows.Forms.Label();
            this.txtDestinationFolder = new System.Windows.Forms.TextBox();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.txtDisplay = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnSetSourceFolder = new System.Windows.Forms.Button();
            this.btnSetDestinationFolder = new System.Windows.Forms.Button();
            this.btnScan = new System.Windows.Forms.Button();
            this.lblFeedback = new System.Windows.Forms.Label();
            this.lblFeedbackProcessed = new System.Windows.Forms.Label();
            this.lblFeedbackSelected = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnBackup = new System.Windows.Forms.Button();
            this.lblBackup = new System.Windows.Forms.Label();
            this.txtBackupLabel = new System.Windows.Forms.TextBox();
            this.lblBackupLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkAutoBackup = new System.Windows.Forms.CheckBox();
            this.chkIgnoreGit = new System.Windows.Forms.CheckBox();
            this.chkIgnoreBin = new System.Windows.Forms.CheckBox();
            this.chkIgnoreObj = new System.Windows.Forms.CheckBox();
            this.dtpQualTime = new System.Windows.Forms.DateTimePicker();
            this.dtpQualDate = new System.Windows.Forms.DateTimePicker();
            this.btnClearResults = new System.Windows.Forms.Button();
            this.chkIgnoreOther = new System.Windows.Forms.CheckBox();
            this.txtFsIgnore = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtSourceFolder
            // 
            this.txtSourceFolder.Location = new System.Drawing.Point(114, 12);
            this.txtSourceFolder.Name = "txtSourceFolder";
            this.txtSourceFolder.Size = new System.Drawing.Size(420, 20);
            this.txtSourceFolder.TabIndex = 0;
            // 
            // lblSourceFolder
            // 
            this.lblSourceFolder.AutoSize = true;
            this.lblSourceFolder.Location = new System.Drawing.Point(16, 12);
            this.lblSourceFolder.Name = "lblSourceFolder";
            this.lblSourceFolder.Size = new System.Drawing.Size(73, 13);
            this.lblSourceFolder.TabIndex = 1;
            this.lblSourceFolder.Text = "Source Folder";
            // 
            // lblDestinationFolder
            // 
            this.lblDestinationFolder.AutoSize = true;
            this.lblDestinationFolder.Location = new System.Drawing.Point(16, 38);
            this.lblDestinationFolder.Name = "lblDestinationFolder";
            this.lblDestinationFolder.Size = new System.Drawing.Size(92, 13);
            this.lblDestinationFolder.TabIndex = 3;
            this.lblDestinationFolder.Text = "Destination Folder";
            // 
            // txtDestinationFolder
            // 
            this.txtDestinationFolder.Location = new System.Drawing.Point(114, 38);
            this.txtDestinationFolder.Name = "txtDestinationFolder";
            this.txtDestinationFolder.Size = new System.Drawing.Size(420, 20);
            this.txtDestinationFolder.TabIndex = 3;
            // 
            // lblDateTime
            // 
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.Location = new System.Drawing.Point(16, 64);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(83, 13);
            this.lblDateTime.TabIndex = 5;
            this.lblDateTime.Text = "Qual Date/Time";
            // 
            // txtDisplay
            // 
            this.txtDisplay.Location = new System.Drawing.Point(12, 138);
            this.txtDisplay.Multiline = true;
            this.txtDisplay.Name = "txtDisplay";
            this.txtDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDisplay.Size = new System.Drawing.Size(578, 364);
            this.txtDisplay.TabIndex = 25;
            // 
            // btnSetSourceFolder
            // 
            this.btnSetSourceFolder.Location = new System.Drawing.Point(540, 11);
            this.btnSetSourceFolder.Name = "btnSetSourceFolder";
            this.btnSetSourceFolder.Size = new System.Drawing.Size(25, 20);
            this.btnSetSourceFolder.TabIndex = 2;
            this.btnSetSourceFolder.Text = "...";
            this.btnSetSourceFolder.UseVisualStyleBackColor = true;
            this.btnSetSourceFolder.Click += new System.EventHandler(this.btnSetSourceFolder_Click);
            // 
            // btnSetDestinationFolder
            // 
            this.btnSetDestinationFolder.Location = new System.Drawing.Point(540, 38);
            this.btnSetDestinationFolder.Name = "btnSetDestinationFolder";
            this.btnSetDestinationFolder.Size = new System.Drawing.Size(25, 20);
            this.btnSetDestinationFolder.TabIndex = 4;
            this.btnSetDestinationFolder.Text = "...";
            this.btnSetDestinationFolder.UseVisualStyleBackColor = true;
            this.btnSetDestinationFolder.Click += new System.EventHandler(this.btnSetDestinationFolder_Click);
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(595, 12);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(96, 33);
            this.btnScan.TabIndex = 8;
            this.btnScan.Text = "Scan";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // lblFeedback
            // 
            this.lblFeedback.AutoSize = true;
            this.lblFeedback.Location = new System.Drawing.Point(592, 67);
            this.lblFeedback.Name = "lblFeedback";
            this.lblFeedback.Size = new System.Drawing.Size(65, 13);
            this.lblFeedback.TabIndex = 10;
            this.lblFeedback.Text = "lblFeedback";
            // 
            // lblFeedbackProcessed
            // 
            this.lblFeedbackProcessed.AutoSize = true;
            this.lblFeedbackProcessed.Location = new System.Drawing.Point(592, 82);
            this.lblFeedbackProcessed.Name = "lblFeedbackProcessed";
            this.lblFeedbackProcessed.Size = new System.Drawing.Size(115, 13);
            this.lblFeedbackProcessed.TabIndex = 12;
            this.lblFeedbackProcessed.Text = "lblFeedbackProcessed";
            // 
            // lblFeedbackSelected
            // 
            this.lblFeedbackSelected.AutoSize = true;
            this.lblFeedbackSelected.Location = new System.Drawing.Point(592, 97);
            this.lblFeedbackSelected.Name = "lblFeedbackSelected";
            this.lblFeedbackSelected.Size = new System.Drawing.Size(107, 13);
            this.lblFeedbackSelected.TabIndex = 13;
            this.lblFeedbackSelected.Text = "lblFeedbackSelected";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(698, 48);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(96, 33);
            this.btnStop.TabIndex = 14;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnBackup
            // 
            this.btnBackup.Location = new System.Drawing.Point(698, 12);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(96, 33);
            this.btnBackup.TabIndex = 9;
            this.btnBackup.Text = "Backup";
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // lblBackup
            // 
            this.lblBackup.AutoSize = true;
            this.lblBackup.Location = new System.Drawing.Point(16, 90);
            this.lblBackup.Name = "lblBackup";
            this.lblBackup.Size = new System.Drawing.Size(73, 13);
            this.lblBackup.TabIndex = 17;
            this.lblBackup.Text = "Backup Label";
            // 
            // txtBackupLabel
            // 
            this.txtBackupLabel.Location = new System.Drawing.Point(114, 90);
            this.txtBackupLabel.Name = "txtBackupLabel";
            this.txtBackupLabel.Size = new System.Drawing.Size(167, 20);
            this.txtBackupLabel.TabIndex = 7;
            this.txtBackupLabel.Click += new System.EventHandler(this.txtBackupLabel_Click);
            this.txtBackupLabel.Leave += new System.EventHandler(this.txtBackupLabel_Leave);
            this.txtBackupLabel.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBackupLabel_KeyUp);
            // 
            // lblBackupLabel
            // 
            this.lblBackupLabel.AutoSize = true;
            this.lblBackupLabel.Location = new System.Drawing.Point(287, 93);
            this.lblBackupLabel.Name = "lblBackupLabel";
            this.lblBackupLabel.Size = new System.Drawing.Size(80, 13);
            this.lblBackupLabel.TabIndex = 18;
            this.lblBackupLabel.Text = "lblBackupLabel";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Results:";
            // 
            // chkAutoBackup
            // 
            this.chkAutoBackup.AutoSize = true;
            this.chkAutoBackup.Location = new System.Drawing.Point(595, 47);
            this.chkAutoBackup.Name = "chkAutoBackup";
            this.chkAutoBackup.Size = new System.Drawing.Size(84, 17);
            this.chkAutoBackup.TabIndex = 10;
            this.chkAutoBackup.Text = "Feelin\' lucky";
            this.chkAutoBackup.UseVisualStyleBackColor = true;
            this.chkAutoBackup.CheckedChanged += new System.EventHandler(this.chkAutoBackup_CheckedChanged);
            // 
            // chkIgnoreGit
            // 
            this.chkIgnoreGit.AutoSize = true;
            this.chkIgnoreGit.Location = new System.Drawing.Point(607, 140);
            this.chkIgnoreGit.Name = "chkIgnoreGit";
            this.chkIgnoreGit.Size = new System.Drawing.Size(99, 17);
            this.chkIgnoreGit.TabIndex = 11;
            this.chkIgnoreGit.Text = "Ignore git folder";
            this.chkIgnoreGit.UseVisualStyleBackColor = true;
            // 
            // chkIgnoreBin
            // 
            this.chkIgnoreBin.AutoSize = true;
            this.chkIgnoreBin.Location = new System.Drawing.Point(607, 163);
            this.chkIgnoreBin.Name = "chkIgnoreBin";
            this.chkIgnoreBin.Size = new System.Drawing.Size(102, 17);
            this.chkIgnoreBin.TabIndex = 12;
            this.chkIgnoreBin.Text = "Ignore bin folder";
            this.chkIgnoreBin.UseVisualStyleBackColor = true;
            // 
            // chkIgnoreObj
            // 
            this.chkIgnoreObj.AutoSize = true;
            this.chkIgnoreObj.Location = new System.Drawing.Point(607, 186);
            this.chkIgnoreObj.Name = "chkIgnoreObj";
            this.chkIgnoreObj.Size = new System.Drawing.Size(102, 17);
            this.chkIgnoreObj.TabIndex = 13;
            this.chkIgnoreObj.Text = "Ignore obj folder";
            this.chkIgnoreObj.UseVisualStyleBackColor = true;
            // 
            // dtpQualTime
            // 
            this.dtpQualTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpQualTime.Location = new System.Drawing.Point(225, 64);
            this.dtpQualTime.Name = "dtpQualTime";
            this.dtpQualTime.ShowUpDown = true;
            this.dtpQualTime.Size = new System.Drawing.Size(95, 20);
            this.dtpQualTime.TabIndex = 6;
            this.dtpQualTime.ValueChanged += new System.EventHandler(this.dtpQualTime_ValueChanged);
            // 
            // dtpQualDate
            // 
            this.dtpQualDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpQualDate.Location = new System.Drawing.Point(114, 64);
            this.dtpQualDate.Name = "dtpQualDate";
            this.dtpQualDate.Size = new System.Drawing.Size(105, 20);
            this.dtpQualDate.TabIndex = 5;
            this.dtpQualDate.Value = new System.DateTime(2013, 4, 9, 9, 52, 0, 0);
            this.dtpQualDate.ValueChanged += new System.EventHandler(this.dtpQualDate_ValueChanged);
            // 
            // btnClearResults
            // 
            this.btnClearResults.Location = new System.Drawing.Point(479, 102);
            this.btnClearResults.Name = "btnClearResults";
            this.btnClearResults.Size = new System.Drawing.Size(96, 33);
            this.btnClearResults.TabIndex = 27;
            this.btnClearResults.Text = "Clear Text";
            this.btnClearResults.UseVisualStyleBackColor = true;
            this.btnClearResults.Click += new System.EventHandler(this.btnClearResults_Click);
            // 
            // chkIgnoreOther
            // 
            this.chkIgnoreOther.AutoSize = true;
            this.chkIgnoreOther.Location = new System.Drawing.Point(607, 209);
            this.chkIgnoreOther.Name = "chkIgnoreOther";
            this.chkIgnoreOther.Size = new System.Drawing.Size(98, 17);
            this.chkIgnoreOther.TabIndex = 28;
            this.chkIgnoreOther.Text = "Ignore CSV list:";
            this.chkIgnoreOther.UseVisualStyleBackColor = true;
            // 
            // txtFsIgnore
            // 
            this.txtFsIgnore.Location = new System.Drawing.Point(607, 232);
            this.txtFsIgnore.Multiline = true;
            this.txtFsIgnore.Name = "txtFsIgnore";
            this.txtFsIgnore.Size = new System.Drawing.Size(200, 58);
            this.txtFsIgnore.TabIndex = 29;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 514);
            this.Controls.Add(this.txtFsIgnore);
            this.Controls.Add(this.chkIgnoreOther);
            this.Controls.Add(this.btnClearResults);
            this.Controls.Add(this.dtpQualDate);
            this.Controls.Add(this.dtpQualTime);
            this.Controls.Add(this.chkIgnoreObj);
            this.Controls.Add(this.chkIgnoreBin);
            this.Controls.Add(this.chkIgnoreGit);
            this.Controls.Add(this.chkAutoBackup);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblBackupLabel);
            this.Controls.Add(this.lblBackup);
            this.Controls.Add(this.txtBackupLabel);
            this.Controls.Add(this.btnBackup);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.lblFeedbackSelected);
            this.Controls.Add(this.lblFeedbackProcessed);
            this.Controls.Add(this.lblFeedback);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.btnSetDestinationFolder);
            this.Controls.Add(this.btnSetSourceFolder);
            this.Controls.Add(this.txtDisplay);
            this.Controls.Add(this.lblDateTime);
            this.Controls.Add(this.lblDestinationFolder);
            this.Controls.Add(this.txtDestinationFolder);
            this.Controls.Add(this.lblSourceFolder);
            this.Controls.Add(this.txtSourceFolder);
            this.Name = "Form1";
            this.Text = "Snapshot Backup Utility";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSourceFolder;
        private System.Windows.Forms.Label lblSourceFolder;
        private System.Windows.Forms.Label lblDestinationFolder;
        private System.Windows.Forms.TextBox txtDestinationFolder;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.TextBox txtDisplay;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnSetSourceFolder;
        private System.Windows.Forms.Button btnSetDestinationFolder;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Label lblFeedback;
        private System.Windows.Forms.Label lblFeedbackProcessed;
        private System.Windows.Forms.Label lblFeedbackSelected;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Label lblBackup;
        private System.Windows.Forms.TextBox txtBackupLabel;
        private System.Windows.Forms.Label lblBackupLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkAutoBackup;
        private System.Windows.Forms.CheckBox chkIgnoreGit;
        private System.Windows.Forms.CheckBox chkIgnoreBin;
        private System.Windows.Forms.CheckBox chkIgnoreObj;
        private System.Windows.Forms.DateTimePicker dtpQualTime;
        private System.Windows.Forms.DateTimePicker dtpQualDate;
        private System.Windows.Forms.Button btnClearResults;
        private System.Windows.Forms.CheckBox chkIgnoreOther;
        private System.Windows.Forms.TextBox txtFsIgnore;
    }
}

