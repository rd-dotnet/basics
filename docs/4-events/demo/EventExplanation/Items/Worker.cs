using System;

namespace EventExplanation.Items
{
	public delegate void WorkerDoWorkEventHandler(WorkerEventArgument args);

	public class Worker
	{
		public string Name { private get; set; }

		//public event Action<int, string> OnWork;
		//public event EventHandler<WorkerEventArgument> OnWork;
		public event WorkerDoWorkEventHandler OnWork;

		public void Work(int amount)
		{
			Console.WriteLine($"{Name} выполнил {amount} работы");

			if (OnWork != null)
			{
				OnWork.Invoke(new WorkerEventArgument()
				{
					Name = this.Name,
					WorkAmount = amount
				});
			}
		}
	}
}
