using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CodeBackupUtility
{
    public partial class Form1 : Form
    {
        private string _folderSource = "";
        private string _folderDestination = @"C:\tmpCodeFileStore\";
        private DateTime _dateTimeCutOff = DateTime.Now.AddHours(-2);
        private bool _stopProcessing = false;
        private string _backupLabelPlaceholder = "{project name or keyword}";
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            setDefaultValues();
            prepareToolTips();

            //for test only
            _folderSource = @"C:\insight2\Insight2.Web\Controllers\";
            txtSourceFolder.Text = _folderSource;
        }

        #region Setup
        private void setDefaultValues()
        {
            dtpQualDate.Value = DateTime.Now;
            dtpQualTime.Value = DateTime.Now.AddHours(-2);
            txtDestinationFolder.Text = defaultFolderDialogPath(_folderDestination);
            lblFeedback.Text = "";
            lblFeedbackSelected.Text = "";
            lblFeedbackProcessed.Text = "";
            btnStop.Visible = false;
            lblBackupLabel.Text = "";
            txtBackupLabel.Text = _backupLabelPlaceholder;
            chkIgnoreObj.Checked = true;
            chkIgnoreBin.Checked = true;
            chkIgnoreGit.Checked = true;
        }

        private void prepareToolTips()
        {
            string sampleBackupLabel = createBackupLabel("MyProject");
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(txtBackupLabel, "Used to name a parent folder for the backup, example 'MyProject'; the current date/time will be appended, example '" + sampleBackupLabel + "'.");
            ToolTip1.SetToolTip(lblBackup, "Used to name a parent folder for the backup, example 'MyProject'; the current date/time will be appended, example '" + sampleBackupLabel + "'.");
            ToolTip1.SetToolTip(dtpQualDate, "Qualification Date - only files modified after this date will be returned.");
            ToolTip1.SetToolTip(dtpQualTime, "Qualification Date - only files modified after this date will be returned.");
            ToolTip1.SetToolTip(lblDateTime, "Qualification Date - only files modified after this date will be returned.");
            ToolTip1.SetToolTip(txtSourceFolder, "The folder where the files to backup exist.");
            ToolTip1.SetToolTip(lblSourceFolder, "The folder where the files to backup exist.");
            ToolTip1.SetToolTip(txtDestinationFolder, "The folder where backup files will be created.");
            ToolTip1.SetToolTip(lblDestinationFolder, "The folder where backup files will be created.");
            ToolTip1.SetToolTip(btnScan, "Scan the source folder for files.");
            ToolTip1.SetToolTip(btnStop, "Stop processing.");
            ToolTip1.SetToolTip(btnSetSourceFolder, "Select the source folder.");
            ToolTip1.SetToolTip(btnSetDestinationFolder, "Select the destination folder.");
            ToolTip1.SetToolTip(btnBackup, "Create a backup copy of the files listed in the results.");
            ToolTip1.SetToolTip(chkAutoBackup, "Automatically perform the backup after the scan is complete.");
        }
        #endregion Setup

        #region buttonEvents
        private void btnSetSourceFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowNewFolderButton = true;
            folderBrowserDialog1.SelectedPath = defaultFolderDialogPath(txtSourceFolder.Text);
            
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                _folderSource = folderBrowserDialog1.SelectedPath;
            }
            
            _folderSource = ensurePathEndsWithSlash(_folderSource);            
            txtSourceFolder.Text = _folderSource;
        }

        private void btnSetDestinationFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowNewFolderButton = true;
            folderBrowserDialog1.SelectedPath = defaultFolderDialogPath(txtDestinationFolder.Text);

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                _folderDestination = folderBrowserDialog1.SelectedPath;
            }
            _folderDestination = ensurePathEndsWithSlash(_folderDestination);
            txtDestinationFolder.Text = _folderDestination;
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            btnScan.Enabled = false;
            btnBackup.Enabled = false;
            txtDisplay.Text = "";
            _folderDestination = ensurePathEndsWithSlash(_folderDestination);
            if (_folderSource != txtSourceFolder.Text) _folderSource = ensurePathEndsWithSlash(txtSourceFolder.Text);
            btnStop.Visible = true;
            ListDesiredFiles();
            if (chkAutoBackup.Checked)
            {
                btnBackup.Enabled = false;
                PerformBackup();
                btnBackup.Enabled = true;
            }
            btnScan.Enabled = true;
            btnBackup.Enabled = true;
            btnStop.Visible = false;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _stopProcessing = true;
            lblFeedback.Text += " - STOPPED";
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            if (txtDisplay.Text != string.Empty)
            {
                btnBackup.Enabled = false;
                PerformBackup();
                btnBackup.Enabled = true;
            }
            else 
            {
                MessageBox.Show("Nothing to backup");
            }
        }
        #endregion buttonEvents

        #region otherEvents
        private void dtpQualTime_ValueChanged(object sender, EventArgs e)
        {
            setDateTimeCutOff();
        }

        private void dtpQualDate_ValueChanged(object sender, EventArgs e)
        {
            setDateTimeCutOff();
        }

        private void txtBackupLabel_Leave(object sender, EventArgs e)
        {
            lblBackupLabel.Text = createBackupLabel(txtBackupLabel.Text);
        }

        private void txtBackupLabel_KeyUp(object sender, KeyEventArgs e)
        {
            lblBackupLabel.Text = createBackupLabel(txtBackupLabel.Text);
        }

        private void txtBackupLabel_Click(object sender, EventArgs e)
        {
            if (txtBackupLabel.Text == _backupLabelPlaceholder)
            {
                txtBackupLabel.SelectionStart = 0;
                txtBackupLabel.SelectionLength = 100;
            }
        }

        private void chkAutoBackup_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoBackup.Checked)
            {
                btnScan.Text = "Scan && Backup";
            }
            else
            {
                btnScan.Text = "Scan";
            }
        }
        #endregion otherEvents

        #region simpleFunctions
        private void setDateTimeCutOff()
        {
            _dateTimeCutOff = new DateTime(dtpQualDate.Value.Year, dtpQualDate.Value.Month, dtpQualDate.Value.Day, dtpQualTime.Value.Hour, dtpQualTime.Value.Minute, dtpQualDate.Value.Second);
        }

        private string ensurePathEndsWithSlash(string path)
        {
            if (!path.EndsWith(@"\"))
            {
                path += @"\";
            }
            return path;
        }

        private string createBackupLabel(string value)
        {
            DateTime dt = DateTime.Now;
            if (value == string.Empty || value == "{project name or keyword}")
            {
                value = "Backup";
            }
            value += "_" + dt.ToString("yyyyMMdd") + "_" + dt.ToString("HHmm");
            return value;
        }

        private string defaultFolderDialogPath(string value)
        {
            string path = @"C:\";
            if (Directory.Exists(value))
            {
                path = value;
            }
            return path;
        }
        #endregion simpleFunctions

        #region PrimaryMethods
        private void ListDesiredFiles()
        {
            int filesChecked = 0;
            int filesSelected = 0;
            bool skipThisFile = false;

            string[] files = Directory.GetFiles(_folderSource, "*.*", SearchOption.AllDirectories);
            lblFeedback.Text = files.Length.ToString() + " file(s) found";
            
            foreach (string filename in files)
            {
                Application.DoEvents();

                if (_stopProcessing == true)
                {
                    break;
                }
                else
                {
                    skipThisFile = false;
                    filesChecked += 1;
                    DateTime fileDate = File.GetLastWriteTime(filename);

                    if (chkIgnoreGit.Checked && filename.Contains(@"\.git\")) { skipThisFile = true; }
                    if (chkIgnoreBin.Checked && filename.Contains(@"\bin\")) { skipThisFile = true; }
                    if (chkIgnoreObj.Checked && filename.Contains(@"\obj\")) { skipThisFile = true; }
                    if (fileDate < _dateTimeCutOff) { skipThisFile = true; }

                    if (skipThisFile == false)
                    {
                        filesSelected += 1;
                        txtDisplay.Text += filename + Environment.NewLine;
                    }
                    lblFeedbackProcessed.Text = filesChecked.ToString() + " processed";
                    lblFeedbackSelected.Text = filesSelected.ToString() + " selected";
                }
            }
            _stopProcessing = false;
        }

        private void PerformBackup()
        {
            string[] delimiters = { "\r\n", "\n" };
            string allFiles = txtDisplay.Text;
            string[] files = allFiles.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            
            string newFileName = "";

            string destinationPlusBackupLabel = ensurePathEndsWithSlash(_folderDestination + createBackupLabel(txtBackupLabel.Text));

            if (!Directory.Exists(destinationPlusBackupLabel))
            {
                Directory.CreateDirectory(destinationPlusBackupLabel);
            }

            txtDisplay.Text += "-------------------- Backup Results --------------------" + Environment.NewLine;

            foreach (string filename in files)
            {
                if (File.Exists(filename))
                {
                    newFileName = filename.Replace(_folderSource, destinationPlusBackupLabel);
                    txtDisplay.Text += newFileName + Environment.NewLine;
                    string newPath = Path.GetDirectoryName(newFileName);
                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }
                    File.Copy(filename, newFileName, true);
                }
            }
            MessageBox.Show("Backup complete");
        }
        #endregion PrimaryMethods

        

        

    }
}
