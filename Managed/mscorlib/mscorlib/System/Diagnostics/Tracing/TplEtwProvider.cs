using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AF5 RID: 2805
	[EventSource(Name = "Microsoft.Tasks.Nuget")]
	internal class TplEtwProvider : EventSource
	{
		// Token: 0x170011F7 RID: 4599
		// (get) Token: 0x06006506 RID: 25862 RVA: 0x0014B5CA File Offset: 0x001497CA
		public bool Debug
		{
			get
			{
				return base.IsEnabled(EventLevel.Verbose, (EventKeywords)1L);
			}
		}

		// Token: 0x06006507 RID: 25863 RVA: 0x0014B5D5 File Offset: 0x001497D5
		public void DebugFacilityMessage(string Facility, string Message)
		{
			base.WriteEvent(1, Facility, Message);
		}

		// Token: 0x06006508 RID: 25864 RVA: 0x0014B5E0 File Offset: 0x001497E0
		public void DebugFacilityMessage1(string Facility, string Message, string Arg)
		{
			base.WriteEvent(2, Facility, Message, Arg);
		}

		// Token: 0x06006509 RID: 25865 RVA: 0x0014B5EC File Offset: 0x001497EC
		public void SetActivityId(Guid Id)
		{
			base.WriteEvent(3, new object[] { Id });
		}

		// Token: 0x04003228 RID: 12840
		public static TplEtwProvider Log = new TplEtwProvider();

		// Token: 0x02000AF6 RID: 2806
		public class Keywords
		{
			// Token: 0x04003229 RID: 12841
			public const EventKeywords Debug = (EventKeywords)1L;
		}
	}
}
