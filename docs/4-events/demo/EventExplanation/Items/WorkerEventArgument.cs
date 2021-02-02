using System;
using System.Collections.Generic;
using System.Text;

namespace EventExplanation.Items
{
	public class WorkerEventArgument : EventArgs
	{
		public string Name { get; set; }
		public int WorkAmount { get; set; }
	}
}
