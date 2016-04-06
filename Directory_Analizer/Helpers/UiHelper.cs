using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Directory_Analizer.Properties;

namespace Directory_Analizer.Helpers
{
    /// <summary>
    /// класс для работы в формой из другого потока или класса.
    /// специально решил вынести функционал для работы с формой в другой класс. UI всетаки :)
    /// </summary>
	public class UiHelper
	{
		// ссылка на форму
		private MainForm Form { get; set; }
		private readonly string _folderPath;

		public UiHelper(MainForm form, string folderPath)
		{
			Form = form;
			_folderPath = folderPath;

			// задание максимумального значения для progressBar. Так как эта процедура довольна трудоемкая решил ее запускать в пуле потоков
			ThreadPool.QueueUserWorkItem(SetProgressBarMaximum);
		}

		// метод для завершения работы. Зависит от того завершилась работа сама, или пользователь нажал 'Cancel'
		public void FinishWork(bool isCanceled, string time)
		{
			string message = isCanceled
				? Resources.Scanning_canceled
				: string.Format(Resources.Scanning_is_completed, Form.TreeView.GetNodeCount(true), time);

			Form.Invoke((MethodInvoker)(() => Form.ProgressLabel.Text = message));
			Form.Invoke((MethodInvoker)(() => Form.ProgressBar.Value = 0));
			Form.Invoke((MethodInvoker)(() => Form.ScanButton.Enabled = true));
		}

        // метод вызывается 1 раз для получения рутовой ноды дерева
		public TreeNode GetRootNode()
		{
			return Form.TreeView.Nodes.Find(string.Empty, false).SingleOrDefault();
		}

		public void ClearTree()
		{
			Form.Invoke((MethodInvoker)(() => Form.TreeView.Nodes.Clear()));
		}

		// метод для добавления нода в дерево. Если есть родитель - то добавляем в родителя, если нет - то просто в дерево.
		public void InsertTreeNode(TreeNode parentNode, TreeNode newNode)
		{
			if (parentNode != null)
				Form.Invoke((MethodInvoker)(() => parentNode.Nodes.Add(newNode)));
			else
				Form.Invoke((MethodInvoker)(() => Form.TreeView.Nodes.Add(newNode)));

			UpdateProgressBar();
		}

        /// <summary>
        /// Создание иконки. Вначале создается 'ключ' иконки, который зависит от того файл это или нет.
        /// Если это файл, и содержит расширение ".exe" или ".lnk" то в ключ записывается полный путь к файлу,
        /// так как такие файлы имеют разные иконки. Если нет - то записывается просто расширение файла.
        /// Если же это папка, то мы проверяем, папка ли это или локальный диск, так как диски имею свои уникальные иконки.
        /// И опять же записываем в ключ соответственное значение - или полный путь или атрибут.
        /// После этого проверяем есть ли такой ключ уже в колекции, если нет, то создаем соответсвующую
        /// иконку и записываем ее в коллекцию иконок с соответствующим ключем.
        /// </summary>
        public string CreateNodeIcon(DirectoryInfo dirInfo, bool isFile)
        {
            string imageKey = isFile
                ? dirInfo.Extension.Equals(".exe") || dirInfo.Extension.Equals(".lnk")
                    ? dirInfo.FullName
                    : dirInfo.Extension
                : dirInfo.Parent == null        // если локальный диск
                    ? dirInfo.FullName
                    : dirInfo.Attributes.ToString();

            if (!Form.TreeView.ImageList.Images.ContainsKey(imageKey))
            {
                var iconForFile = IconHelper.GetSmallIcon(dirInfo.FullName);

                // Как показал dotTrace и VS Analize 85% работы всей программы приходится выполнение этой строки!!!
                // Лечения не нашел. Лечение - программа должна по другомму быть немного написана, но тогда это будет не по ТЗ.
                Form.Invoke((MethodInvoker)(() => Form.TreeView.ImageList.Images.Add(imageKey, iconForFile)));
            }

            return imageKey;
        }

        /// <summary>
        /// специально так по кучерявому задаю вначале явный максимум (100000), так как 
        /// если сканируемая директория будет иметь более 10 000 объектов, выполнение FindAccessableFilesAndFolders функции
        /// будет довольно долгим. Соответственно пользователь не увидит когда возвращенное значение из этой фукции
        /// заменит 'заглушку' (100000) на реальную цифру. Если же папка содержит не много объектов, то опять же
        /// пользователь не заметит смены 100000 на скажем 1000. Ничего более красивого по быстрому придумать не смог :)
        /// </summary>
        private void SetProgressBarMaximum(object obj)
        {
            Form.Invoke((MethodInvoker)(() => Form.ProgressBar.Maximum = 100000));
            int filesCount = IoHelper.FindAccessableFilesAndFolders(_folderPath, "*", true).Count();
            Form.Invoke((MethodInvoker)(() => Form.ProgressBar.Maximum = filesCount));
        }

		// метод для обновления прогресс бара и лейбы, которая показывает соответственный процент прогресса
		private void UpdateProgressBar()
		{
			Form.Invoke((MethodInvoker)(() =>
			{
				if (Form.ProgressBar.Value < Form.ProgressBar.Maximum)
				{
					Form.ProgressBar.Value++;
					double proccess = (Convert.ToDouble(Form.ProgressBar.Value) / Convert.ToDouble(Form.ProgressBar.Maximum)) * 100;
					Form.ProgressLabel.Text = string.Format(Resources.Scan_progress, Math.Round(proccess, 1));
				}
			}));
		}
	}
}