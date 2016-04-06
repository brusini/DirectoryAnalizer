using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Directory_Analizer.Entities;
using Directory_Analizer.Helpers;

namespace Directory_Analizer.Workers
{
    /// <summary>
    /// "поток занесения результатов в XML файл" (получает от "поток сбора информации" 
    /// сведения об очередной поддиректории или файле и заносит эти сведенияв XML файл);
    /// PS. Записывать явно каждый объект в файл нашел ОЧЕНЬ нецелесообразным. 
    /// Поэтому запись в файл происходит уже в конце, после того как поиск закончен
    /// и структура папок и файлов сформирована.
    /// </summary>
	public class XmlWorker : AbstractWorker<XElement>
    {
        private XDocument _xmlFile = new XDocument();
        private XElement _root;
        private readonly string _filePath;
        private readonly string _folderPath;

        public XmlWorker(string filePath, string folderPath)
        {
            _filePath = filePath;
            _folderPath = folderPath;
            ParentNodes = new Dictionary<string, XElement>();
        }

        public override void Work()
        {
            var rootElement = new NodeModel(null, _folderPath, false);
            _root = CreateXmlNode(rootElement);

            base.Work();

            _xmlFile = new XDocument(_root);
            _xmlFile.Save(_filePath);
        }

        // метод для добавления нода. Если есть родитель - то добавляем в родителя, если нет - то просто в рут.
        protected override void InsertNode(NodeModel nodeModel)
        {
            var newNode = CreateXmlNode(nodeModel);
            var parentNode = GetParentNode(nodeModel);

            if (parentNode != null)
                parentNode.Add(newNode);
            else
                _root.Add(newNode);
        }

        private XElement GetParentNode(NodeModel nodeModel)
        {
            if (nodeModel.ParentPath == null)
                return _root.Descendants("folder").SingleOrDefault();

            return ParentNodes[nodeModel.ParentPath];
        }

        public XElement CreateXmlNode(NodeModel nodeModel)
        {
            var dirInfo = new DirectoryInfo(nodeModel.Path);

            string size = IoHelper.GetSize(dirInfo.FullName, nodeModel.IsFile);
            string owner = IoHelper.GetOwnerName(dirInfo.FullName);
            string permissions = IoHelper.GetPermissions(dirInfo.FullName);

            var node = new XElement(nodeModel.IsFile ? "file" : "folder",
                new XAttribute("Path", dirInfo.FullName),
                new XElement("Name", dirInfo.Name),
                new XElement("Created", dirInfo.CreationTime.ToString(CultureInfo.InvariantCulture)),
                new XElement("Modified", dirInfo.LastWriteTime.ToString(CultureInfo.InvariantCulture)),
                new XElement("Accessed", dirInfo.LastAccessTime.ToString(CultureInfo.InvariantCulture)),
                new XElement("Attributes", dirInfo.Attributes),
                new XElement("Size", size),
                new XElement("Owner", owner),
                new XElement("Permissions", permissions));

            if (!nodeModel.IsFile)
				ParentNodes.Add(nodeModel.Path, node);

            return node;
        }
    }
}