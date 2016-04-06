using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Directory_Analizer.Entities;
using Directory_Analizer.Helpers;
using Directory_Analizer.Properties;

namespace Directory_Analizer.Workers
{
    /// <summary>
    /// "поток сбора информации" (выполняет сканирование указанной директории и создается только на время сканирования);
    /// передает сведения об очередной поддиректории или файле в "поток занесения результатов в XML файл" и "поток занесения результатов в дерево".
    /// </summary>
    public class SearchInfo
    {
        public bool WorkInProccess { get; private set; }

        private Thread _thread;
        private readonly string _folderPath;
        private readonly XmlWorker _xmlWorker;
        private readonly TreeViewWorker _treeViewWorker;
        private readonly UiHelper _uiHelper;
        private readonly Stopwatch _timer = new Stopwatch();

        public SearchInfo(UiHelper uiHelper, string filePath, string folderPath)
        {
            _uiHelper = uiHelper;
            _xmlWorker = new XmlWorker(filePath, folderPath);
            _treeViewWorker = new TreeViewWorker(uiHelper);
            _folderPath = folderPath;
        }

        public void StartWork(string path)
        {
            WorkInProccess = true;
            _timer.Start();
            Logger.StartLog(path);
            _thread = new Thread(SearchThreadWork) { IsBackground = true };
            _thread.Start();
        }

        public void AbortWork()
        {
            _xmlWorker.AbortWork();
            _treeViewWorker.AbortWork();
            _thread.Abort();
            _uiHelper.FinishWork(true, null);
            WorkInProccess = false;
        }

        /// <summary>
        /// Основной метод потока, в котором вызываются старт других потоков, и запускается метод поиска папок и файлов.
        /// После отработки этого метода потоки аккуратно завершаются, и вызывается ф-ция "_uiHelper.FinishWork" в которой
        /// пользователю показывают сколько было найдено объектов и за какой период времени.
        /// </summary>
        private void SearchThreadWork()
        {
            _treeViewWorker.AddNode(new NodeModel(null, _folderPath, false));

            _xmlWorker.StartWork();
            _treeViewWorker.StartWork();

            DirSearch(_folderPath, true);

            _xmlWorker.FinishWork();
            _treeViewWorker.FinishWork();

            _timer.Stop();
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}", _timer.Elapsed.Hours, _timer.Elapsed.Minutes, _timer.Elapsed.Seconds);
            _uiHelper.FinishWork(false, elapsedTime);
            WorkInProccess = false;
        }

        // Метод поиска папок и файлов. После нахождения нового обекта он передается в другие потоки.
        private void DirSearch(string path, bool rootPath)
        {
            try
            {
                string[] directories = Directory.GetDirectories(path);
                foreach (string directoryPath in directories)
                {
                    AddNewNode(path, directoryPath, false);
                    DirSearch(directoryPath, false);

                    try
                    {
                        string[] files = Directory.GetFiles(directoryPath);
                        foreach (string filePath in files)
                            AddNewNode(directoryPath, filePath);
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        string message = string.Format("{0} {1}", Resources.Unable_to_get_files, ex.Message);
                        Logger.Log(message);
                    }
                }
                if (rootPath)
                {
                    try
                    {
                        string[] files = Directory.GetFiles(path);
                        foreach (string filePath in files)
                            AddNewNode(path, filePath);
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        string message = string.Format("{0} {1}", Resources.Unable_to_get_files, ex.Message);
                        Logger.Log(message);
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                string message = string.Format("{0} {1}", Resources.Unable_to_get_subdirectories, ex.Message);
                Logger.Log(message);
            }
        }

        private void AddNewNode(string parentPath, string path, bool isFile = true)
        {
            var newNode = new NodeModel(parentPath, path, isFile);

            _treeViewWorker.AddNode(newNode);
            _xmlWorker.AddNode(newNode);
        }
    }
}
