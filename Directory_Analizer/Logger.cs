using System;
using System.IO;
using Directory_Analizer.Properties;

namespace Directory_Analizer
{
    // класс для записи ошибок в файл
    public class Logger
    {
        // путь к файлу лога. Нужен для открытия файла из формы
        public static string LogFilePath { get { return FilePath; } }
        private static readonly string FilePath;
        private static readonly object Locker = new object();

        static Logger()
        {
            string filePath = string.Format(@"{0}\log.txt", Directory.GetCurrentDirectory());
            if (!File.Exists(filePath))
                File.Create(filePath).Close();

            FilePath = filePath;
        }

        // вызывается при начале работы поиска дерикторий
        public static void StartLog(string path)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(FilePath))
                {
                    writer.WriteLine();
                    writer.WriteLine(Resources.Log_scan_started, DateTime.Now.ToLocalTime(), path);
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }

        // вызывается для записи ошибки в лог
        public static void Log(string message)
        {
            try
            {
                lock (Locker)
                {
                    using (StreamWriter writer = File.AppendText(FilePath))
                    {
                        writer.WriteLine("{0} \t {1} ", DateTime.Now.ToLocalTime(), message);
                    }
                }
            }
            catch (Exception exception)
            {
                Log(exception.Message);
            }
        }
    }
}
