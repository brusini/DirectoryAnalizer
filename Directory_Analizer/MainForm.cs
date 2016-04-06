using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Directory_Analizer.Helpers;
using Directory_Analizer.Properties;
using Directory_Analizer.Workers;

namespace Directory_Analizer
{
    public partial class MainForm : Form
    {
        public ProgressBar ProgressBar { get; set; }
        public Label ProgressLabel { get; set; }
        public TreeView TreeView { get; set; }
        public Button ScanButton { get; set; }

        private SearchInfo _searchInfo;
        private UiHelper _uiHelper;
        private readonly string _xmlFilePath;

        public MainForm()
        {
            InitializeComponent();

            TB_FolderPath.Text = Options.Default.DirectoryPath;
            _xmlFilePath = string.Format(@"{0}\result.xml", Directory.GetCurrentDirectory());

            TV_Result.ImageList = new ImageList();
            ProgressBar = PB_Progress;
            TreeView = TV_Result;
            ScanButton = B_Scan;
            ProgressLabel = L_Progress;
        }

        private void B_Browse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                TB_FolderPath.Text = folderBrowserDialog.SelectedPath;
        }

        private void B_OpenFile_Click(object sender, EventArgs e)
        {
            if (File.Exists(_xmlFilePath))
                Process.Start(_xmlFilePath);
            else
                MessageBox.Show(Resources.Xml_File_was_not_created_yet);
        }

        private void B_Scan_Click(object sender, EventArgs e)
        {
            if (!ValidateTextBox())
                return;

            _uiHelper = new UiHelper(this, TB_FolderPath.Text);

            _searchInfo = new SearchInfo(_uiHelper, _xmlFilePath, TB_FolderPath.Text);
            _searchInfo.StartWork(TB_FolderPath.Text);
            B_Scan.Enabled = false;
        }

        private void B_Cancel_Click(object sender, EventArgs e)
        {
            if (_searchInfo != null && _searchInfo.WorkInProccess)
                _searchInfo.AbortWork();
        }

        private void B_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // открытие файла с логом
        private void B_ShowLog_Click(object sender, EventArgs e)
        {
            if (File.Exists(Logger.LogFilePath))
                Process.Start(Logger.LogFilePath);
            else
                MessageBox.Show(Resources.There_is_no_logs_to_show);
        }

        // сохранение пути XML файла и пути сканирумой директории перед выходом из программы
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Options.Default.DirectoryPath = TB_FolderPath.Text;
            Options.Default.Save();
        }

        // проверка существования XML файла и сканирумой директории
        private bool ValidateTextBox()
        {
            if (!Directory.Exists(TB_FolderPath.Text))
            {
                directryErrorProvider.SetError(TB_FolderPath, "Folder does not exist!");
                TB_FolderPath.BackColor = Color.LightPink;

                return false;
            }

            // если пользователь ввел C: или D:
            if (TB_FolderPath.Text.EndsWith(":") && new DirectoryInfo(TB_FolderPath.Text).Parent == null)
                TB_FolderPath.Text += Path.DirectorySeparatorChar;

            directryErrorProvider.Clear();
            TB_FolderPath.BackColor = Color.White;

            return true;
        }
    }
}