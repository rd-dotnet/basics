using EventExplanation.Items;
using System;
using System.Collections.Generic;

namespace EventExplanation
{
	class Program
	{
		static void Main(string[] args)
		{
			Random _work = new Random(DateTime.Now.Millisecond);
			List<Worker> workers = new List<Worker>()
			{
				new Worker() { Name = "Дмитрий" },
				new Worker() { Name = "Анна" },
				new Worker() { Name = "Василий" },
				new Worker() { Name = "Алевтина" },
				new Worker() { Name = "Игорь" },
				new Worker() { Name = "Руслана" }
			};

			List<Manager> managers = new List<Manager>()
			{
				new Manager() { Name = "Джоанна" },
				new Manager() { Name = "Питер" },
				new Manager() { Name = "Джессика" }
			};

			List<Reporter> reporters = new List<Reporter>()
			{
				new Reporter() { Name = "Хосе" },
				new Reporter() { Name = "Мария" },
				new Reporter() { Name = "Пабло" },
			};

			foreach (var worker in workers)
			{
				foreach (var manager in managers)
				{
					worker.OnWork += manager.CollectData;
				}

				foreach (var reporter in reporters)
				{
					worker.OnWork += reporter.CollectReport;
				}
			}

			foreach (Worker worker in workers)
			{
				int workAmmount = _work.Next(1, 15);

				worker.Work(workAmmount);
			}

			foreach (var worker in workers)
			{
				foreach (var manager in managers)
				{
					worker.OnWork -= manager.CollectData;
				}

				foreach (var reporter in reporters)
				{
					worker.OnWork -= reporter.CollectReport;
				}
			}

			Console.WriteLine("=======================================");

			foreach (var manager in managers)
			{
				manager.DoReport();
			}

			Console.WriteLine("=======================================");

			foreach (var reporter in reporters)
			{
				reporter.DoTwit();
			}
		}
	}
}
