namespace Directory_Analizer.Entities
{
    // вспомогательный класс, использующийся для создании модели Noda
    public class NodeModel
    {
        public string ParentPath;
        public string Path;
        public bool IsFile;

        public NodeModel(string parentPath, string path, bool isFile = true)
        {
            ParentPath = parentPath;
            Path = path;
            IsFile = isFile;
        }
    }
}