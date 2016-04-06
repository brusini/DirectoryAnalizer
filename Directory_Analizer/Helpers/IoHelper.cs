using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using Directory_Analizer.Entities;
using Directory_Analizer.Properties;

namespace Directory_Analizer.Helpers
{
    // класс для получения информации файла или директории, для последующей записи ее в XML
    public class IoHelper
    {
        private static readonly WindowsIdentity CurrentUser = WindowsIdentity.GetCurrent();
        private static readonly string[] SizeSuffix = { "B", "KB", "MB", "GB", "TB" };

        /// <summary>
        /// метод для получения доступных папок и файлов, чтобы потом использовать их количество как ProgressBar.Maximum
        /// выполняется 1 раз при клике на кнопку Scan, в отдельном потоке, так как довольна трудоемкая операция, если
        /// директория имеет больше 10 000 объектов.
        /// </summary>
        public static IEnumerable<String> FindAccessableFilesAndFolders(string path, string filePattern, bool recurse)
        {
            if (File.Exists(path))
            {
                yield return path;
                yield break;
            }

            if (!Directory.Exists(path))
            {
                yield break;
            }
            yield return path;

            // Enumerate the files just in the top directory.
            var topDirectory = new DirectoryInfo(path);
            IEnumerator<FileInfo> files;
            try
            {
                files = topDirectory.EnumerateFiles(filePattern).GetEnumerator();
            }
            catch (Exception)
            {
                files = null;
            }

            while (true)
            {
                FileInfo file;
                try
                {
                    if (files != null && files.MoveNext())
                        file = files.Current;
                    else
                        break;
                }
                catch (UnauthorizedAccessException)
                {
                    continue;
                }
                catch (PathTooLongException)
                {
                    continue;
                }

                yield return file.FullName;
            }

            if (!recurse)
                yield break;

            IEnumerator<DirectoryInfo> dirs;
            try
            {
                dirs = topDirectory.EnumerateDirectories("*").GetEnumerator();
            }
            catch (Exception)
            {
                dirs = null;
            }

            while (true)
            {
                DirectoryInfo dir;
                try
                {
                    if (dirs != null && dirs.MoveNext())
                        dir = dirs.Current;
                    else
                        break;
                }
                catch (UnauthorizedAccessException)
                {
                    continue;
                }
                catch (PathTooLongException)
                {
                    continue;
                }

                foreach (var subpath in FindAccessableFilesAndFolders(dir.FullName, filePattern, true))
                    yield return subpath;
            }
        }

        // метод для получения владельца папки или файла
        public static string GetOwnerName(string path)
        {
            try
            {
                var security = File.GetAccessControl(path);
                IdentityReference identityReference = security.GetOwner(typeof (NTAccount));
                return identityReference.Value;
            }
            catch (Exception ex)
            {
                string message = string.Format("{0} {1}", Resources.Unable_to_get_owner_name, ex.Message);
                Logger.Log(message);
                return message;
            }
        }

        // метод для получения прав на папку или файл у пользователя
        public static string GetPermissions(string path)
        {
            var permissionsList = new List<string>();
            try
            {
                var security = File.GetAccessControl(path);
                AuthorizationRuleCollection rules = security.GetAccessRules(true, true, typeof(NTAccount));
                if (CurrentUser != null)
                {
                    var principal = new WindowsPrincipal(CurrentUser);
                    foreach (FileSystemAccessRule rule in rules)
                    {
                        var ntAccount = rule.IdentityReference as NTAccount;

                        if (ntAccount == null)
                            continue;

                        if (principal.IsInRole(ntAccount.Value))
                        {
                            FileSystemRights fileSystemRights = MapGenericRightsToFileSystemRights(rule.FileSystemRights);
                            var permissions = fileSystemRights.ToString().Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (var permission in permissions)
                            {
                                if (!permissionsList.Contains(permission))
                                    permissionsList.Add(permission);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = string.Format("{0} {1}", Resources.Unable_to_get_permissions, ex.Message);
                Logger.Log(message);
                return message;
            }

            return string.Join(", ", permissionsList.ToArray());
        }

        // метод для получения размера папки или файла
        public static string GetSize(string path, bool isFile)
        {
            try
            {
                if (isFile)
                    return FormatBytes(new FileInfo(path).Length);

                long folderSize = CalculateFolderSize(path);
                return FormatBytes(folderSize);
            }
            catch (Exception ex)
            {
                string message = string.Format("{0} {1}", Resources.Unable_to_get_size, ex.Message);
                Logger.Log(message);
                return message;
            }
        }

        /// <summary>
        /// метод который преобразовывает 'Generic' права в удобочитаемые 
        /// например: (-1610612736 == Modify), (–536805376 == Delete), (268435456 == FullControl)
        /// реализация взята с просторов интернета, и немного переделана под мои интересы :)
        /// </summary>
        private static FileSystemRights MapGenericRightsToFileSystemRights(FileSystemRights originalRights)
        {
            var mappedRights = new FileSystemRights();
            Boolean blnWasNumber = false;
            if (Convert.ToBoolean(Convert.ToInt64(originalRights) & Convert.ToInt64(GenericRights.GenericExecute)))
            {
                mappedRights = mappedRights
                    | FileSystemRights.ExecuteFile
                    | FileSystemRights.ReadPermissions
                    | FileSystemRights.ReadAttributes
                    | FileSystemRights.Synchronize;
                blnWasNumber = true;
            }

            if (Convert.ToBoolean(Convert.ToInt64(originalRights) & Convert.ToInt64(GenericRights.GenericRead)))
            {
                mappedRights = mappedRights
                    | FileSystemRights.ReadAttributes
                    | FileSystemRights.ReadData
                    | FileSystemRights.ReadExtendedAttributes
                    | FileSystemRights.ReadPermissions
                    | FileSystemRights.Synchronize;
                blnWasNumber = true;
            }

            if (Convert.ToBoolean(Convert.ToInt64(originalRights) & Convert.ToInt64(GenericRights.GenericWrite)))
            {
                mappedRights = mappedRights
                    | FileSystemRights.AppendData
                    | FileSystemRights.WriteAttributes
                    | FileSystemRights.WriteData
                    | FileSystemRights.WriteExtendedAttributes
                    | FileSystemRights.ReadPermissions
                    | FileSystemRights.Synchronize;
                blnWasNumber = true;
            }

            if (Convert.ToBoolean(Convert.ToInt64(originalRights) & Convert.ToInt64(GenericRights.GenericAll)))
            {
                mappedRights = mappedRights | FileSystemRights.FullControl;
                blnWasNumber = true;
            }

            if (blnWasNumber == false)
                mappedRights = originalRights;

            return mappedRights;
        }

        // метод для подсчета размера папки
        private static long CalculateFolderSize(string path)
        {
            long folderSize = 0;

            if (!Directory.Exists(path))
                return folderSize;

            folderSize = (from file in Directory.GetFiles(path)
                          where File.Exists(file)
                          select new FileInfo(file))
                              .Aggregate(folderSize, (current, finfo) => current + finfo.Length) +
                              Directory.GetDirectories(path).Sum(dir => CalculateFolderSize(dir));

            return folderSize;
        }

        // метод для форматирования размера файла или директории изменяет размер из B в более удобочитаемые, например "KB", "MB", "GB", "TB"
        private static string FormatBytes(long bytes)
        {
            int i = 0;
            double dblSByte = bytes;
            if (bytes > 1024)
                for (i = 0; (bytes / 1024) > 0; i++, bytes /= 1024)
                    dblSByte = bytes / 1024.0;

            return String.Format("{0:0.##} {1}", dblSByte, SizeSuffix[i]);
        }
    }
}
