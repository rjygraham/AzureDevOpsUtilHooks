using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionApp2.Events
{


	public class EventRoot
	{
		public string id { get; set; }
		public string eventType { get; set; }
		public string publisherId { get; set; }
		public Message message { get; set; }
		public Detailedmessage detailedMessage { get; set; }
		public Resource resource { get; set; }
		public string resourceVersion { get; set; }
		public Resourcecontainers resourceContainers { get; set; }
		public DateTime createdDate { get; set; }
	}

	public class Message
	{
		public string text { get; set; }
		public string html { get; set; }
		public string markdown { get; set; }
	}

	public class Detailedmessage
	{
		public string text { get; set; }
		public string html { get; set; }
		public string markdown { get; set; }
	}

	public class Resource
	{
		public int id { get; set; }
		public int workItemId { get; set; }
		public int rev { get; set; }
		public Revisedby revisedBy { get; set; }
		public DateTime revisedDate { get; set; }
		public Fields fields { get; set; }
		public _Links2 _links { get; set; }
		public string url { get; set; }
		public Revision revision { get; set; }
	}

	public class Revisedby
	{
		public string id { get; set; }
		public string displayName { get; set; }
		public string url { get; set; }
		public _Links _links { get; set; }
		public string imageUrl { get; set; }
		public string descriptor { get; set; }
	}

	public class _Links
	{
		public Avatar avatar { get; set; }
	}

	public class Avatar
	{
		public string href { get; set; }
	}

	public class Fields
	{
		public SystemRev SystemRev { get; set; }
		public SystemAuthorizeddate SystemAuthorizedDate { get; set; }
		public SystemReviseddate SystemRevisedDate { get; set; }
		public SystemState SystemState { get; set; }
		public SystemReason SystemReason { get; set; }
		public SystemAssignedto SystemAssignedTo { get; set; }
		public SystemChangeddate SystemChangedDate { get; set; }
		public SystemWatermark SystemWatermark { get; set; }
		public MicrosoftVSTSCommonSeverity MicrosoftVSTSCommonSeverity { get; set; }
	}

	public class SystemRev
	{
		public string oldValue { get; set; }
		public string newValue { get; set; }
	}

	public class SystemAuthorizeddate
	{
		public DateTime oldValue { get; set; }
		public DateTime newValue { get; set; }
	}

	public class SystemReviseddate
	{
		public DateTime oldValue { get; set; }
		public DateTime newValue { get; set; }
	}

	public class SystemState
	{
		public string oldValue { get; set; }
		public string newValue { get; set; }
	}

	public class SystemReason
	{
		public string oldValue { get; set; }
		public string newValue { get; set; }
	}

	public class SystemAssignedto
	{
		public Newvalue newValue { get; set; }
	}

	public class Newvalue
	{
		public string displayName { get; set; }
		public string url { get; set; }
		public _Links1 _links { get; set; }
		public string id { get; set; }
		public string imageUrl { get; set; }
		public string descriptor { get; set; }
	}

	public class _Links1
	{
		public Avatar1 avatar { get; set; }
	}

	public class Avatar1
	{
		public string href { get; set; }
	}

	public class SystemChangeddate
	{
		public DateTime oldValue { get; set; }
		public DateTime newValue { get; set; }
	}

	public class SystemWatermark
	{
		public string oldValue { get; set; }
		public string newValue { get; set; }
	}

	public class MicrosoftVSTSCommonSeverity
	{
		public string oldValue { get; set; }
		public string newValue { get; set; }
	}

	public class _Links2
	{
		public Self self { get; set; }
		public Parent parent { get; set; }
		public Workitemupdates workItemUpdates { get; set; }
	}

	public class Self
	{
		public string href { get; set; }
	}

	public class Parent
	{
		public string href { get; set; }
	}

	public class Workitemupdates
	{
		public string href { get; set; }
	}

	public class Revision
	{
		public int id { get; set; }
		public int rev { get; set; }
		public Fields1 fields { get; set; }
		public string url { get; set; }
	}

	public class Fields1
	{
		public string SystemAreaPath { get; set; }
		public string SystemTeamProject { get; set; }
		public string SystemIterationPath { get; set; }
		public string SystemWorkItemType { get; set; }
		public string SystemState { get; set; }
		public string SystemReason { get; set; }
		public DateTime SystemCreatedDate { get; set; }
		public SystemCreatedby SystemCreatedBy { get; set; }
		public DateTime SystemChangedDate { get; set; }
		public SystemChangedby SystemChangedBy { get; set; }
		public string SystemTitle { get; set; }
		public string MicrosoftVSTSCommonSeverity { get; set; }
		public string WEF_EB329F44FE5F4A94ACB1DA153FDF38BA_KanbanColumn { get; set; }
	}

	public class SystemCreatedby
	{
		public string displayName { get; set; }
		public string url { get; set; }
		public _Links3 _links { get; set; }
		public string id { get; set; }
		public string imageUrl { get; set; }
		public string descriptor { get; set; }
	}

	public class _Links3
	{
		public Avatar2 avatar { get; set; }
	}

	public class Avatar2
	{
		public string href { get; set; }
	}

	public class SystemChangedby
	{
		public string displayName { get; set; }
		public string url { get; set; }
		public _Links4 _links { get; set; }
		public string id { get; set; }
		public string imageUrl { get; set; }
		public string descriptor { get; set; }
	}

	public class _Links4
	{
		public Avatar3 avatar { get; set; }
	}

	public class Avatar3
	{
		public string href { get; set; }
	}

	public class Resourcecontainers
	{
		public Collection collection { get; set; }
		public Account account { get; set; }
		public Project project { get; set; }
	}

	public class Collection
	{
		public string id { get; set; }
	}

	public class Account
	{
		public string id { get; set; }
	}

	public class Project
	{
		public string id { get; set; }
	}

}
