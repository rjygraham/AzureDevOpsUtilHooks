using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionApp2.Models
{

	public class TaskRoot
	{
		public int count { get; set; }
		public Value[] value { get; set; }
	}

	public class Value
	{
		public int id { get; set; }
		public int rev { get; set; }
		public TaskFields fields { get; set; }
		public string url { get; set; }
	}

	public class TaskFields
	{
		[JsonProperty("System.Id")]
		public int SystemId { get; set; }

		[JsonProperty("System.WorkItemType")]
		public string SystemWorkItemType { get; set; }

		[JsonProperty("System.State")]
		public string SystemState { get; set; }
	}

}
