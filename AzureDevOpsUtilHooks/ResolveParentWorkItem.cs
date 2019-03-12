using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FunctionApp2.Events;
using System.Net.Http;
using System.Net.Http.Headers;
using FunctionApp2.Models;
using System.Linq;
using System.Text;

namespace FunctionApp2
{
	public static class ResolveParentWorkItem
	{
		private static string DevOpsApiRoot = $"https://dev.azure.com/{Environment.GetEnvironmentVariable("DevOpsOrgName")}/_apis/wit";
		private static string DevOpsBasicAuth = Convert.ToBase64String(Encoding.ASCII.GetBytes($":{Environment.GetEnvironmentVariable("DevOpsPAT")}"));

		private const string ChildBodyFormat = @"{{
			""ids"": [{0}],
			""fields"": [
				""System.Id"",
				""System.State"",
				""System.WorkItemType""
			]
		}}";

		private const string StoryUpdateBody = @"[
			{
				""op"": ""replace"",
				""path"": ""/fields/System.State"",
				""value"": ""Resolved""
			}
		]";

		[FunctionName("ResolveParentWorkItem")]
		public static async Task<IActionResult> Run(
			[HttpTrigger(AuthorizationLevel.Function, "post", Route = "webhook/task/state")] HttpRequest req,
			ILogger log)
		{
			log.LogInformation("Task work item state changed...resolving parent work item if necessary.");

			try
			{
				var json = await new StreamReader(req.Body).ReadToEndAsync();
				var @event = JsonConvert.DeserializeObject<EventRoot>(json);

				var client = HttpClientFactory.Create();
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", DevOpsBasicAuth);

				var taskResponse = await client.GetAsync($"{DevOpsApiRoot}/workitems/{@event.resource.workItemId}?$expand=relations&api-version=5.0");
				if (taskResponse.IsSuccessStatusCode)
				{
					var task = await taskResponse.Content.ReadAsAsync<WorkItemRoot>();
					var parentUrl = task.relations.SingleOrDefault(w => w.rel.Equals("System.LinkTypes.Hierarchy-Reverse", StringComparison.OrdinalIgnoreCase))?.url;

					if (!string.IsNullOrEmpty(parentUrl))
					{
						var parentId = parentUrl.Substring(parentUrl.LastIndexOf('/') + 1);
						var parentResponse = await client.GetAsync($"{DevOpsApiRoot}/workitems/{parentId}?$expand=relations&api-version=5.0");

						if (parentResponse.IsSuccessStatusCode)
						{
							var parent = await parentResponse.Content.ReadAsAsync<WorkItemRoot>();

							var childIds = parent.relations
								.Where(w => w.rel.Equals("System.LinkTypes.Hierarchy-Forward", StringComparison.OrdinalIgnoreCase))
								.Select(s => int.Parse(s.url.Substring(s.url.LastIndexOf('/') + 1))).ToArray();

							if (childIds.Length > 0)
							{
								var childContent = new StringContent(string.Format(ChildBodyFormat, string.Join(',', childIds)), Encoding.UTF8, "application/json");
								var childResponse = await client.PostAsync($"{DevOpsApiRoot}/workitemsbatch?api-version=5.0", childContent);
								if (childResponse.IsSuccessStatusCode)
								{
									var children = await childResponse.Content.ReadAsAsync<TaskRoot>();

									var tasks = children.value.Where(w => w.fields.SystemWorkItemType.Equals("Task", StringComparison.InvariantCultureIgnoreCase));

									if (tasks.Count() > 0 && tasks.All(a => a.fields.SystemState.Equals("Closed", StringComparison.InvariantCultureIgnoreCase)))
									{
										var updateParent = new StringContent(StoryUpdateBody, Encoding.UTF8, "application/json-patch+json");
										var updateParentResponse = await client.PatchAsync($"{DevOpsApiRoot}/workitems/{parentId}?api-version=5.0", updateParent);
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				log.LogError($"ResolveParentWorkItem failed with ex message: {ex.Message}");
			}

			log.LogInformation("Completed.");

			return new OkResult();
		}
	}
}
