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

            //TODO
            //create a folder to hold setting files in (app.path or similar) and set as default
            //maybe add an option to include ONLY results that include filesnames containing values from a csv list

            //for test only
            //_folderSource = @"C:\insight2\Insight2.Web\Controllers\";
            _folderSource = "";
            txtSourceFolder.Text = _folderSource;

            txtDestinationFolder.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtDestinationFolder.AutoCompleteSource = AutoCompleteSource.AllSystemSources;

            txtSourceFolder.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSourceFolder.AutoCompleteSource = AutoCompleteSource.AllSystemSources;
        }

        #region Setup
        private void setDefaultValues()
        {
            txtSourceFolder.Text = "";
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
            chkIgnoreOther.Checked = false;
            txtFsIgnore.Text = "";
            updateFormTitle("");
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
            ToolTip1.SetToolTip(chkIgnoreOther, "Ignore filenames with any of the comma separated values in the box below.");
            ToolTip1.SetToolTip(txtFsIgnore, "not case sensitive, spaces are not trimmed, so 'a,b' is not the same as 'a, b'.");
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
            if (readyToScan())
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

        private void btnClearResults_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "";
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

        private void txtSourceFolder_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(txtSourceFolder.Text))
            {
                _folderSource = ensurePathEndsWithSlash(txtSourceFolder.Text);
                //txtSourceFolder.Text = _folderSource;
            }
        }

        private void txtDestinationFolder_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(txtDestinationFolder.Text))
            {
                _folderDestination = ensurePathEndsWithSlash(txtDestinationFolder.Text);
                //txtDestinationFolder.Text = _folderDestination;
            }   
        }

        private void txtFsIgnore_TextChanged(object sender, EventArgs e)
        {
            chkIgnoreOther.Checked = txtFsIgnore.Text != "" ? true : false;
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
            if (value == string.Empty || value == _backupLabelPlaceholder)
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
        private void updateFormTitle(string message)
        {
            this.Text = "Snapshot Backup Utility";
            if (message != "")
            {
                this.Text += " - " + message;
            }
        }
        #endregion simpleFunctions

        #region PrimaryMethods

        private bool readyToScan()
        {
            bool result = true;

            if (!Directory.Exists(txtSourceFolder.Text))
            {
                result = false;
                txtDisplay.Text = "source folder is not valid." + Environment.NewLine;
                txtSourceFolder.Focus();
            }

            if (!Directory.Exists(txtDestinationFolder.Text))
            {
                result = false;
                txtDisplay.Text += "destination folder is not valid." + Environment.NewLine;
                txtDestinationFolder.Focus();
            }

            return result;
        }

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

                    if (chkIgnoreOther.Checked) 
                    {   
                        string[] ignore = getIgnoreCSValues();
                        foreach (string item in ignore)
                        {
                            if (filename.ToUpper().Contains(item.ToUpper())) { skipThisFile = true; }
                        }
                    }
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

        private string[] getIgnoreCSValues()
        {
            if (txtFsIgnore.Text.Length == 0)
            {
                return new string[0];
            }

            string[] delimiters = { ","};
            string[] results = txtFsIgnore.Text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            return results;
        }
        #endregion PrimaryMethods

        #region Menu
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Save settings";
            saveFileDialog1.Filter = "XML|*.xml";
            saveFileDialog1.InitialDirectory = @"C:\billtmp\";
            saveFileDialog1.ShowDialog();
            
            if (saveFileDialog1.FileName != "")
            {
                Settings setting = new Settings();
                setting.FolderDestination = _folderDestination;
                setting.FolderSource = _folderSource;
                setting.IgnoreBin = chkIgnoreBin.Checked;
                setting.IgnoreGit = chkIgnoreGit.Checked;
                setting.IgnoreObj = chkIgnoreObj.Checked;
                setting.IgnoreListValues = txtFsIgnore.Text;
                setting.Label = txtBackupLabel.Text;
                setting.QualificationDateTime = _dateTimeCutOff;
                setting.Save(saveFileDialog1.FileName);
            }

            updateFormTitle(saveFileDialog1.FileName);
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadFromSettingFile(true);
        }

        private void loadWithoutDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadFromSettingFile(false);
        }

        private void loadFromSettingFile(bool loadDate)
        {
            openFileDialog1.ShowDialog();
            openFileDialog1.Filter = "XML|*.xml";
            openFileDialog1.InitialDirectory = @"C:\billtmp\";

            if (openFileDialog1.FileName != "")
            {
                Settings setting = new Settings();
                setting.Load(openFileDialog1.FileName);

                txtSourceFolder.Text = setting.FolderSource;
                _folderSource = setting.FolderSource;

                txtDestinationFolder.Text = setting.FolderDestination;
                _folderDestination = setting.FolderDestination;

                if (loadDate)
                {
                    if (setting.QualificationDateTime > DateTime.Parse("1/1/1970"))
                    {
                        dtpQualDate.Value = setting.QualificationDateTime;
                        dtpQualTime.Value = setting.QualificationDateTime;
                        _dateTimeCutOff = setting.QualificationDateTime;
                    }
                }

                txtBackupLabel.Text = setting.Label;
                lblBackupLabel.Text = createBackupLabel(setting.Label);

                chkIgnoreObj.Checked = setting.IgnoreObj;
                chkIgnoreGit.Checked = setting.IgnoreGit;
                chkIgnoreBin.Checked = setting.IgnoreBin;
                txtFsIgnore.Text = setting.IgnoreListValues;

                if (setting.IgnoreListValues != "")
                {
                    chkIgnoreOther.Checked = true;
                }

                updateFormTitle(openFileDialog1.FileName);
            }
        }

        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setDefaultValues();
        }
        #endregion Menu

        

        




    }
}
