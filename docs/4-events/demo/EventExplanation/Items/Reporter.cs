using System;
using System.Collections.Generic;
using System.Text;

namespace EventExplanation.Items
{
	public class Reporter
	{
		private List<string> _workersData = new List<string>();

		public string Name { get; set; }

		public void CollectReport(WorkerEventArgument data)
		{
			if (data.WorkAmount > 5)
			{
				_workersData.Add($"Работник {data.Name} выполнил огромный объем работы!");
			}
			else
			{
				_workersData.Add($"Работник {data.Name} не смог выполнить план и будет уволен");
			}
		}

		public void DoTwit()
		{
			Console.WriteLine("---------------------------------------");
			Console.WriteLine($"{Name} обнаружил что в этом месяце");
			Console.WriteLine("---------------------------------------");

			foreach (var item in _workersData)
			{
				Console.WriteLine(item);
			}
		}
	}
}
