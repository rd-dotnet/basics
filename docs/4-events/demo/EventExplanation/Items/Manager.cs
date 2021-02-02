using System;
using System.Collections.Generic;

namespace EventExplanation.Items
{
	public class Manager
	{
		private List<string> _workersData = new List<string>();

		public string Name { get; set; }

		public void CollectData(WorkerEventArgument data)
		{
			_workersData.Add($"Работник {data.Name} выпонил {data.WorkAmount} работы");
		}

		public void DoReport()
		{
			Console.WriteLine("---------------------------------------");
			Console.WriteLine($"Отчет менеджера {Name}");
			Console.WriteLine("---------------------------------------");
			foreach (var item in _workersData)
			{
				Console.WriteLine(item);
			}
		}
	}
}
