using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Directory_Analizer.Entities;
using Directory_Analizer.Helpers;

namespace Directory_Analizer.Workers
{
    /// <summary>
    /// "поток сбора информации" (выполняет сканирование указанной директории и создается только на время сканирования);
    /// передает сведения об очередной поддиректории или файле в "поток занесения результатов в XML файл" и "поток занесения результатов в дерево".
    /// </summary>
    public class TreeViewWorker : AbstractWorker<TreeNode>
    {
        private readonly UiHelper _uiHelper;

        public TreeViewWorker(UiHelper uiHelper)
        {
            _uiHelper = uiHelper;
			ParentNodes = new Dictionary<string, TreeNode>();
        }

        public override void Work()
        {
            _uiHelper.ClearTree();
            base.Work();
        }

        /// <summary>
        /// метод для добавления нода. Вызывает соответсвующий метод у класса "_uiHelper", 
        /// с помощью которого обновляется данные на форме - ProgressBar, TreeView и Progress_Label
        /// </summary>
        protected override void InsertNode(NodeModel nodeModel)
        {
            var newNode = CreateTreeNode(nodeModel);
			var parentNode = GetParentNode(nodeModel);
            _uiHelper.InsertTreeNode(parentNode, newNode);
        }

		/// <summary>
		/// Сначала использовал стандартный метод поиска у TreeView - Find, а у XML находил через Linq, но потом понял что при очень большой
		/// вложенности очень долго ищет, так как ищет везде. Потом переделал метод и внутри цикла "собирал" путь к родителю объекта, что было
		/// несколько быстрее, однако все равно долго. Поэтому пришел к выводу просто записывать все папки, и потом доставать значения
		/// по ключу. Стало работать намного быстрее. Скажу честно этот вариант подсказал мой хороший коллега :)
		/// </summary>
		private TreeNode GetParentNode(NodeModel nodeModel)
		{
			if (nodeModel.ParentPath == null)
				return _uiHelper.GetRootNode();

		    return ParentNodes[nodeModel.ParentPath];
		}

		private TreeNode CreateTreeNode(NodeModel nodeModel)
        {
            var dirInfo = new DirectoryInfo(nodeModel.Path);
            string imageKey = _uiHelper.CreateNodeIcon(dirInfo, nodeModel.IsFile);
            
            var node = new TreeNode
            {
                Text = dirInfo.Name,
                Name = dirInfo.FullName,
                ImageKey = imageKey,
                SelectedImageKey = imageKey
            };

            // если объект не файл, значит он теоретически может содержать объекты. Записываю его в колекцию родителей.
            if (!nodeModel.IsFile)
				ParentNodes.Add(nodeModel.Path, node);

            return node;
        }
    }
}
