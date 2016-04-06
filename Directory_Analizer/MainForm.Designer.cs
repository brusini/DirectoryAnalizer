namespace Directory_Analizer
{
	partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.TB_FolderPath = new System.Windows.Forms.TextBox();
            this.B_Browse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TV_Result = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.L_Progress = new System.Windows.Forms.Label();
            this.PB_Progress = new System.Windows.Forms.ProgressBar();
            this.B_Cancel = new System.Windows.Forms.Button();
            this.B_Scan = new System.Windows.Forms.Button();
            this.B_Exit = new System.Windows.Forms.Button();
            this.B_ShowLog = new System.Windows.Forms.Button();
            this.B_OpenFile = new System.Windows.Forms.Button();
            this.directryErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.directryErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.Description = "Please select the folder that you want to scan.";
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.folderBrowserDialog.ShowNewFolderButton = false;
            // 
            // TB_FolderPath
            // 
            this.TB_FolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_FolderPath.Location = new System.Drawing.Point(98, 5);
            this.TB_FolderPath.Name = "TB_FolderPath";
            this.TB_FolderPath.Size = new System.Drawing.Size(313, 20);
            this.TB_FolderPath.TabIndex = 0;
            // 
            // B_Browse
            // 
            this.B_Browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.B_Browse.Location = new System.Drawing.Point(417, 3);
            this.B_Browse.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.B_Browse.Name = "B_Browse";
            this.B_Browse.Size = new System.Drawing.Size(77, 24);
            this.B_Browse.TabIndex = 1;
            this.B_Browse.Text = "Browse...";
            this.B_Browse.UseVisualStyleBackColor = true;
            this.B_Browse.Click += new System.EventHandler(this.B_Browse_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Scan directory:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TV_Result
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.TV_Result, 3);
            this.TV_Result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TV_Result.Location = new System.Drawing.Point(10, 35);
            this.TV_Result.Margin = new System.Windows.Forms.Padding(10, 5, 10, 0);
            this.TV_Result.Name = "TV_Result";
            this.TV_Result.Size = new System.Drawing.Size(484, 542);
            this.TV_Result.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.Controls.Add(this.TV_Result, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.TB_FolderPath, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.B_Browse, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 10);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(504, 647);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 3);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.B_Cancel, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.PB_Progress, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.B_Scan, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.L_Progress, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.B_Exit, 4, 1);
            this.tableLayoutPanel2.Controls.Add(this.B_ShowLog, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.B_OpenFile, 2, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(7, 577);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(490, 70);
            this.tableLayoutPanel2.TabIndex = 10;
            // 
            // L_Progress
            // 
            this.L_Progress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.L_Progress.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.L_Progress, 3);
            this.L_Progress.Location = new System.Drawing.Point(183, 8);
            this.L_Progress.Margin = new System.Windows.Forms.Padding(3);
            this.L_Progress.Name = "L_Progress";
            this.L_Progress.Size = new System.Drawing.Size(304, 13);
            this.L_Progress.TabIndex = 4;
            this.L_Progress.Text = "Scan progress.";
            this.L_Progress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PB_Progress
            // 
            this.PB_Progress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.SetColumnSpan(this.PB_Progress, 2);
            this.PB_Progress.Location = new System.Drawing.Point(3, 6);
            this.PB_Progress.Name = "PB_Progress";
            this.PB_Progress.Size = new System.Drawing.Size(174, 17);
            this.PB_Progress.TabIndex = 3;
            // 
            // B_Cancel
            // 
            this.B_Cancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.B_Cancel.Image = global::Directory_Analizer.Properties.Resources.Cancel;
            this.B_Cancel.Location = new System.Drawing.Point(93, 35);
            this.B_Cancel.Name = "B_Cancel";
            this.B_Cancel.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.B_Cancel.Size = new System.Drawing.Size(84, 30);
            this.B_Cancel.TabIndex = 8;
            this.B_Cancel.Text = " Cancel";
            this.B_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.B_Cancel.UseVisualStyleBackColor = true;
            this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
            // 
            // B_Scan
            // 
            this.B_Scan.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.B_Scan.Image = ((System.Drawing.Image)(resources.GetObject("B_Scan.Image")));
            this.B_Scan.Location = new System.Drawing.Point(3, 35);
            this.B_Scan.Name = "B_Scan";
            this.B_Scan.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.B_Scan.Size = new System.Drawing.Size(84, 30);
            this.B_Scan.TabIndex = 7;
            this.B_Scan.Text = "Scan";
            this.B_Scan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.B_Scan.UseVisualStyleBackColor = true;
            this.B_Scan.Click += new System.EventHandler(this.B_Scan_Click);
            // 
            // B_Exit
            // 
            this.B_Exit.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.B_Exit.Image = global::Directory_Analizer.Properties.Resources.Exit;
            this.B_Exit.Location = new System.Drawing.Point(407, 35);
            this.B_Exit.Name = "B_Exit";
            this.B_Exit.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.B_Exit.Size = new System.Drawing.Size(80, 30);
            this.B_Exit.TabIndex = 10;
            this.B_Exit.Text = "Exit";
            this.B_Exit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.B_Exit.UseVisualStyleBackColor = true;
            this.B_Exit.Click += new System.EventHandler(this.B_Exit_Click);
            // 
            // B_ShowLog
            // 
            this.B_ShowLog.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.B_ShowLog.Image = global::Directory_Analizer.Properties.Resources.Logs;
            this.B_ShowLog.Location = new System.Drawing.Point(283, 35);
            this.B_ShowLog.Name = "B_ShowLog";
            this.B_ShowLog.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.B_ShowLog.Size = new System.Drawing.Size(94, 30);
            this.B_ShowLog.TabIndex = 9;
            this.B_ShowLog.Text = "Show log";
            this.B_ShowLog.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.B_ShowLog.UseVisualStyleBackColor = true;
            this.B_ShowLog.Click += new System.EventHandler(this.B_ShowLog_Click);
            // 
            // B_OpenFile
            // 
            this.B_OpenFile.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.B_OpenFile.Image = global::Directory_Analizer.Properties.Resources.Xml;
            this.B_OpenFile.Location = new System.Drawing.Point(183, 35);
            this.B_OpenFile.Name = "B_OpenFile";
            this.B_OpenFile.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.B_OpenFile.Size = new System.Drawing.Size(94, 30);
            this.B_OpenFile.TabIndex = 11;
            this.B_OpenFile.Text = "Open XML";
            this.B_OpenFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.B_OpenFile.UseVisualStyleBackColor = true;
            this.B_OpenFile.Click += new System.EventHandler(this.B_OpenFile_Click);
            // 
            // directryErrorProvider
            // 
            this.directryErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;
            this.directryErrorProvider.ContainerControl = this;
            // 
            // MainForm
            // 
            this.AcceptButton = this.B_Scan;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.B_Cancel;
            this.ClientSize = new System.Drawing.Size(514, 662);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(430, 500);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Directory_Analizer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.directryErrorProvider)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.TextBox TB_FolderPath;
        private System.Windows.Forms.Button B_Browse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TreeView TV_Result;
        private System.Windows.Forms.ErrorProvider directryErrorProvider;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button B_Cancel;
        private System.Windows.Forms.ProgressBar PB_Progress;
        private System.Windows.Forms.Button B_Scan;
        private System.Windows.Forms.Label L_Progress;
        private System.Windows.Forms.Button B_Exit;
        private System.Windows.Forms.Button B_ShowLog;
        private System.Windows.Forms.Button B_OpenFile;
	}
}

