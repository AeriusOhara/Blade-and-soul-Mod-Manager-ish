using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;

namespace BnSModBackup
{
    public partial class mainForm : Form
    {
        public BackgroundWorker bw;
        public static string workingPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
        public string modFolderPath = workingPath + "\\mods";
        public string backupFolderPath = workingPath + "\\backups";
        public string originalFolderPath = "";
        public string bnsExeFolderPath = "";
        public string ncsoftExeFolderPath = "";
        public bool bnsFolderIsSet = true;
        public bool ncsoftFolderIsSet = true;
        public bool installFlag = true;

        filesDatabase database = new filesDatabase();

        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            // Load JSON file FIRST
            // The app HAS to know from the get-go whether it's the first time the
            // new version of it is being ran or not. This way it can set itself
            // up properly. Previously the app checked whether the backup (and mod)
            // folder had any files or not to determine whether the Go or Undo buttons
            // were enabled. This was bad practice. So we will use that old method
            // of checking to once and for all set a consistent variable into the
            // json file. That way we know for sure which button to enable/disable.
            if (!File.Exists(workingPath + "\\database.json"))
            {
                // Determine the install flag from the legacy way of checking things
                checkLegacy();

                database.firstEverRun = false;
                saveJSON();
            }

            // Load the JSON settings
            using (StreamReader file = File.OpenText(workingPath + "\\database.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                database = (filesDatabase)serializer.Deserialize(file, typeof(filesDatabase));
            }

            textLog.AppendText("Beep Boop, I am a text log.");

            // Set up backgroundworker or it will crash the application if you quit
            // without it aleast being used (so, set) at least once
            bw = new BackgroundWorker();

            // Create the "backups" and "mods" folder
            System.IO.Directory.CreateDirectory(workingPath + "\\backups\\"); // Won't do anything if the folder already exists
            System.IO.Directory.CreateDirectory(workingPath + "\\mods\\"); // Won't do anything if the folder already exists

            // Check whether the BnS install path is set or not
            isBnsFolderSet(true);

            // Check whether the NCSoft install path is set or not
            isNCSoftFolderSet(true);

            // Check which buttons should be enabled
            checkButtons(true);

            // Check the DropDownMenu
            checkDropDownMenuSelection();
        }

        private void saveJSON()
        {
            string json = JsonConvert.SerializeObject(database, Formatting.Indented);
            File.WriteAllText(workingPath + "\\database.json", json);
        }

        private void checkLegacy()
        {
            // If we are indeed running it for the first time (firstEverRun was not set
            // to false by the imported JSON, or it didn't exist in the first place)
            if (database.firstEverRun)
            {
                // Check how many upk files there are in the mods folder.
                int i = 0;
                int j = 0;
                DirectoryInfo dirInfo = new DirectoryInfo(modFolderPath);
                foreach (var file in dirInfo.GetFiles("*.upk"))
                {
                    i++;
                }

                // Check how many upk files there are in the backup folder
                dirInfo = new DirectoryInfo(backupFolderPath);

                foreach (var file in dirInfo.GetFiles("*.upk"))
                {
                    j++;
                }

                // Are there files in the backups folder?
                if (j == 0)
                {
                    // How about the mods folder?
                    if (i > 0)
                    {
                        database.installed = false;
                    }
                }
                else
                {
                    // "Found files in the "backups" folder, this takes priority as they are original files
                    database.installed = true;

                    // (Re)Build the list of files we can deinstall
                    foreach (var file in dirInfo.GetFiles("*.upk"))
                    {
                        string tmpFileName = file.Name;
                        database.movedFileNames.Add(tmpFileName);

                        saveJSON();
                    }
                }
            }
        }

        private bool isBnsFolderSet(bool outputMessage = false)
        {
            if(Properties.Settings.Default.BnSInstallDir == string.Empty)
            {
                // Isn't set
                if (outputMessage)
                {
                    textLog.AppendText(Environment.NewLine);
                    textLog.AppendText("[Notice] Your Blade and Soul directory has not been set. Please use the \"Set Bns Folder\" button to fix this as this. This application is useless without it.");
                }

                bnsFolderIsSet = false;

                return false;
            }
            else
            {
                originalFolderPath = Properties.Settings.Default.BnSInstallDir;
                bnsFolderTextBox.Text = originalFolderPath;

                // Grab the client.exe location
                string tmp = originalFolderPath;
                tmp = tmp.Replace("\\contents\\Local\\NCWEST\\ENGLISH\\CookedPC", ""); // Remove the path to CookedPC
                bnsExeFolderPath = tmp + "\\bin";

                bnsFolderIsSet = true;
            }

            return true;
        }

        private bool isNCSoftFolderSet(bool outputMessage = false)
        {
            if (Properties.Settings.Default.NCSoftInstallDir == string.Empty)
            {
                ncsoftFolderIsSet = false;

                return false;
            }
            else
            {
                ncsoftFolderTextBox.Text = Properties.Settings.Default.NCSoftInstallDir;
                ncsoftExeFolderPath = Properties.Settings.Default.NCSoftInstallDir;

                ncsoftFolderIsSet = true;
            }

            return true;
        }

        private void disableButtons()
        {
            performInstall.Enabled = false;
            performDeInstall.Enabled = false;
            bnsFolderButton.Enabled = false;
            ncsoftFolderBtn.Enabled = false;
            refreshModsBtn.Enabled = false;
            openModsFolderBtn.Enabled = false;
            gameVersionDropDownMenu.Enabled = false;
            launchGameBtn.Enabled = false;
            launchNCSoftLauncherBtn.Enabled = false;
        }

        private void enableButtons()
        {
            //performInstall.Enabled = true;
            //performDeInstall.Enabled = true;
            bnsFolderButton.Enabled = true;
            ncsoftFolderBtn.Enabled = true;
            refreshModsBtn.Enabled = true;
            openModsFolderBtn.Enabled = true;
            gameVersionDropDownMenu.Enabled = true;
            launchGameBtn.Enabled = true;
            launchNCSoftLauncherBtn.Enabled = true;

            checkButtons();
        }

        private void checkButtons(bool output = false)
        {

            // Check how many upk files there are in the mods folder.
            int i = 0;
            int j = 0;
            DirectoryInfo dirInfo = new DirectoryInfo(modFolderPath);
            foreach (var file in dirInfo.GetFiles("*.upk"))
            {
                i++;
            }

            // Check how many upk files there are in the backup folder
            dirInfo = new DirectoryInfo(backupFolderPath);

            foreach (var file in dirInfo.GetFiles("*.upk"))
            {
                j++;
            }

            // Are there files in the backups folder?
            if (j == 0)
            {
                // How about the mods folder?
                if (i == 0)
                {
                    if (output)
                    {
                        // No .upk files anywhere, start panicking!
                        textLog.AppendText(Environment.NewLine);
                        textLog.AppendText("[Notice] " + "No .upk/mod files detected in the \"mods\" folder. You can add some by pressing the \"Open Mods Folder\" button below." +
                                           "Then drag the mod files into the folder that opens and press the Refresh button to the left of it.");
                    }
                }
                else
                {
                    if (output)
                    {
                        textLog.AppendText(Environment.NewLine);
                        textLog.AppendText("[Log] " + "Found " + i.ToString() + " .upk files in the \"mods\" folder.");
                    }
                }
            }
            else
            {
                // "Found files in the "backups" folder, this takes priority as they are original files
                if (output)
                {
                    textLog.AppendText(Environment.NewLine);
                    textLog.AppendText("[Log] " + "Found " + j.ToString() + " .upk files in the \"backups\" folder.");
                }
            }

            // Determine whether the install or deinstall button should be enabled
            if (database.installed)
            {
                performDeInstall.Enabled = true;
            }
            else
            {
                performInstall.Enabled = true;
            }

            // Check if the BnS directory is set
            if (!bnsFolderIsSet)
            {
                performInstall.Enabled = false;
                performDeInstall.Enabled = false;
            }

            // The launch Game button
            if (bnsFolderIsSet)
            {
                launchGameBtn.Enabled = true;
            }
            else
            {
                launchGameBtn.Enabled = false;
            }

            // The launch NCSoft Launcher button
            if (ncsoftFolderIsSet)
            {
                launchNCSoftLauncherBtn.Enabled = true;
            }
            else
            {
                launchNCSoftLauncherBtn.Enabled = false;
            }
        }

        private void checkDropDownMenuSelection()
        {
            if (Properties.Settings.Default.gameVersionEurope == true)
            {
                gameVersionDropDownMenu.SelectedIndex = 0;
            }
            else
            {
                gameVersionDropDownMenu.SelectedIndex = 1;
            }
        }

        private void doInstall(object sender, EventArgs e)
        {
            //performInstall.Enabled = false;

            installFlag = true;
            doFileSwap();

            //performDeInstall.Enabled = true;
        }

        private void doDeInstall(object sender, EventArgs e)
        {
            //performDeInstall.Enabled = false;

            installFlag = false;
            doFileSwap();

            //performInstall.Enabled = true;
        }

        private void doFileSwap()
        {
            bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;

            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

            // Disable all the buttons
            disableButtons();

            // If the background worker is not busy, which it shouldn't be
            if (!bw.IsBusy)
            {
                // If the background worker is not busy, lets get this thing started
                bw.RunWorkerAsync();
            }
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(modFolderPath);
            BackgroundWorker worker = sender as BackgroundWorker;

            string message = string.Empty;
            int fileCounter = 0;
            int i = 0;
            int max = database.movedFileNames.Count();
            int curPercent = 0;

            if (!database.installed) // If we're installing
            {
                // Folder we'll scan for upk files
                dirInfo = new DirectoryInfo(modFolderPath);
                message = " into the CookedPC/backups folder.";
            }
            else // If we're deinstalling
            {
                // Folder we'll scan for upk files
                dirInfo = new DirectoryInfo(backupFolderPath);
                message = " back into its original folder.";
            }
            
            // If we're installing, rebuild the list, otherwise we'll just
            // use the list we loaded earlier
            if (!database.installed)
            {
                // Make sure the database list is clear of any entries
                database.movedFileNames.Clear();

                // (Re)Build the list 
                foreach (var file in dirInfo.GetFiles("*.upk"))
                {
                    string tmpFileName = file.Name;
                    database.movedFileNames.Add(tmpFileName);
                }

                // Save the newly created list
                saveJSON();

                // Set the max to the amount of files we have stored now
                max = database.movedFileNames.Count();
            }

            foreach (var fileName in database.movedFileNames)
            {
                textLog.AppendText(Environment.NewLine);
                textLog.AppendText("[Moving] " + fileName + message);

                if (!database.installed) // If we're installing
                {

                    // Check if the file we're about to install, exists in CookedPC or not.
                    // If yes, move it away
                    if (File.Exists(originalFolderPath + "\\" + fileName))
                    {
                        File.Copy(originalFolderPath + "\\" + fileName, backupFolderPath + "\\" + fileName, true);
                        File.Delete(originalFolderPath + "\\" + fileName);
                    }

                    // Now move over the mod to the CookedPC folder
                    if (File.Exists(modFolderPath + "\\" + fileName))
                    {
                        File.Copy(modFolderPath + "\\" + fileName, originalFolderPath + "\\" + fileName, true);
                        File.Delete(modFolderPath + "\\" + fileName);
                    }
                }
                else
                {
                    // Move the mod file back into the mods folder
                    if (File.Exists(originalFolderPath + "\\" + fileName))
                    {
                        File.Copy(originalFolderPath + "\\" + fileName, modFolderPath + "\\" + fileName, true);
                        File.Delete(originalFolderPath + "\\" + fileName);
                    }

                    // Check if the mod file we just uninstalled has a backup in the backup folder.
                    // If yes, move that back as the original
                    if (File.Exists(backupFolderPath + "\\" + fileName))
                    {
                        File.Copy(backupFolderPath + "\\" + fileName, originalFolderPath + "\\" + fileName, true);
                        File.Delete(backupFolderPath + "\\" + fileName);
                    }
                }

                fileCounter++;

                // Increment & update stats as well as progress bar
                i++;
                // This operating will only get to 50%, and the file transfer will be the other 50%
                curPercent = ((i * 100) / max);

                // Update the UI
                if ((worker.CancellationPending == true))
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    System.Threading.Thread.Sleep(50);
                    worker.ReportProgress(curPercent);
                }
            }
            
            // Show how many files we moved
            if (fileCounter > 0)
            {
                textLog.AppendText(Environment.NewLine);
                textLog.AppendText("[Log] Done! " + fileCounter + " files were moved.");
            }
            else
            {
                textLog.AppendText(Environment.NewLine);
                textLog.AppendText("[Log] Done! No files were moved.");
            }
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar.Value = e.ProgressPercentage;
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((e.Cancelled == true))
            {
                // Cancelled
            }
            else if (!(e.Error == null))
            {
                // An error occured
            }
            else
            {
                // Done enable buttons and set the progressbar to 100%
                this.progressBar.Value = 100;

                if (database.installed)
                {
                    database.installed = false;
                }
                else
                {
                    database.installed = true;
                }

                // Save the changes to the JSON file
                saveJSON();

                // Enable the buttons again
                enableButtons();
            }
        }

        private void onCloseAttempt(object sender, FormClosingEventArgs e)
        {
            if (bw.IsBusy)
            {
                MessageBox.Show("Please do not close this application while it's installing or uninstalling mods!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                e.Cancel = true;
            }

            //this.Close();
        }

        private void setBnSInstallDir(object sender, EventArgs e)
        {
            string bnsFolderPath = "";
            bool done = false;
            DialogResult dr;
            string message = "No .upk files were found in this folder. Are you sure you pointed to\n" +
                             "the Blade and Soul's CookedPC folder? It is usually located in:\n" +
                             "(InstallDirectory)\\BnS\\contents\\Local\\NCWEST\\ENGLISH\\CookedPC\\\n\n" +
                             "Do you want to try looking for it again?";
            
            while (!done)
            {
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    bnsFolderPath = folderBrowserDialog.SelectedPath;
                }

                // Check if we find any .upk files
                DirectoryInfo dirInfo = new DirectoryInfo(bnsFolderPath);
                if (dirInfo.GetFiles("*.upk").Length == 0)
                {
                    dr = MessageBox.Show(message, "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (dr == DialogResult.No)
                    {
                        done = true;
                    }
                }
                else
                {
                    // Save the BnS install dir so we can use it from here on
                    Properties.Settings.Default.BnSInstallDir = bnsFolderPath;
                    Properties.Settings.Default.Save();

                    isBnsFolderSet();

                    checkButtons(true);

                    done = true;
                }
            }
        }

        private void setNCSoftDir(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "NCSoft Launcher (NCLauncher.exe) | NCLauncher.exe";
            dialog.InitialDirectory = @"C:\";
            dialog.Title = "Please point to NCLauncher.exe";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // Save the BnS install dir so we can use it from here on
                ncsoftExeFolderPath = dialog.FileName;
                Properties.Settings.Default.NCSoftInstallDir = ncsoftExeFolderPath;
                Properties.Settings.Default.Save();

                //ncsoftFolderTextBox.Text = ncsoftExeFolderPath;
                //ncsoftFolderIsSet = true;

                isNCSoftFolderSet();

                checkButtons();
            }
        }

        private void openModsFolder(object sender, EventArgs e)
        {
            if(performDeInstall.Enabled == true)
            {
                string message = "Please note that according to the app's knowledge you have mods\n" +
                                 "currently installed already! Please press the Undo button to\n" +
                                 "undo the process before adding new mods, or updates to installed\n" +
                                 "mods.";
                MessageBox.Show(message);
            }

            Process.Start("explorer.exe", (System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\mods\\"));
        }

        private void launchGame(object sender, EventArgs e)
        {
            Process bnsDirectLaunch = new Process();
            bnsDirectLaunch.StartInfo.FileName = bnsExeFolderPath + "\\client.exe";
            bnsDirectLaunch.StartInfo.Arguments = " /LaunchByLauncher /SessKey:\"\" /MacAddr:\"\" /UserNick:\"\" /CompanyID:\"0\" /ChannelGroupIndex:\" - 1\" /ServerAddr:\" \" /StartGameID:\"BNS\" /RepositorySub:\" \" /GamePath:\"\" /LoginMode 2";
            if(gameVersionDropDownMenu.SelectedIndex == 0)
            {
                // If we're booting the european version, add the needed arguments
                bnsDirectLaunch.StartInfo.Arguments += " -lang:English -region:1";
            }
            bnsDirectLaunch.Start();
        }

        private void gameVersionChanged(object sender, EventArgs e)
        {
            if (gameVersionDropDownMenu.SelectedIndex == 0)
            {
                Properties.Settings.Default.gameVersionEurope = true;
            }
            else
            {
                Properties.Settings.Default.gameVersionEurope = false;
            }
            Properties.Settings.Default.Save();
        }

        private void launchNCSoftLauncher(object sender, EventArgs e)
        {
            Process NCSoftLauncherLaunch = new Process();
            NCSoftLauncherLaunch.StartInfo.FileName = ncsoftExeFolderPath;
            NCSoftLauncherLaunch.StartInfo.Arguments = "  /LauncherID:\"NCWest\" /CompanyID:\"12\" /GameID:\"BnS\" /LUpdateAddr:\"updater.nclauncher.ncsoft.com\"";
            NCSoftLauncherLaunch.Start();
        }

        private void refreshMods(object sender, EventArgs e)
        {
            textLog.AppendText(Environment.NewLine);
            textLog.AppendText("[Log] Checking Mods Folder...");

            checkButtons(true);
        }
    }

    public class filesDatabase
    {
        public bool firstEverRun = true;
        public bool installed = false;
        public List<string> movedFileNames = new List<string>();
    }
}
