using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Directory_Analizer.Entities;

namespace Directory_Analizer.Workers
{
	/// <summary>
	/// класс предоставляющий базовые методы для работы с коллекциями в своих потоках. В принципе все стандартно. 
	/// Очередь объектов. Если хочу завершить работу потока, передаю в очередь null и выходу из цикла. 
	/// Использую lock для блокировки очереди и EventWaitHandle для более рационального использования ресурсов.
	/// </summary>
	public abstract class AbstractWorker<T>
	{
		private Thread _thread;
		private readonly EventWaitHandle _eventHandle = new AutoResetEvent(false);
		private readonly Queue<NodeModel> _queue = new Queue<NodeModel>();
		protected Dictionary<string, T> ParentNodes;

		public void StartWork()
		{
			ParentNodes.Clear();
			_thread = new Thread(Work) { IsBackground = true };
			_thread.Start();
		}

		public void FinishWork()
		{
			lock (((ICollection)_queue).SyncRoot)
			{
				_queue.Enqueue(null);
			}
			_eventHandle.Set();
			_thread.Join();
		}

		public void AbortWork()
		{
			_thread.Abort();
		}

		public virtual void Work()
		{
			while (true)
			{
				if (_queue.Count > 0)
				{
					lock (((ICollection)_queue).SyncRoot)
					{
						var queueObject = _queue.Dequeue();

						if (queueObject != null)
							InsertNode(queueObject);
						else
							break;
					}
				}
				else
					_eventHandle.WaitOne();
			}
		}

		public void AddNode(NodeModel nodeModel)
		{
			lock (((ICollection)_queue).SyncRoot)
			{
				_queue.Enqueue(nodeModel);
			}
			_eventHandle.Set();
		}

		protected virtual void InsertNode(NodeModel nodeModel)
		{
		}
	}
}
