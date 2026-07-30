using System;
using System.Diagnostics;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000243 RID: 579
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal static class CompModSwitches
	{
		// Token: 0x170003DD RID: 989
		// (get) Token: 0x060012B8 RID: 4792 RVA: 0x0004E5FF File Offset: 0x0004C7FF
		public static BooleanSwitch CommonDesignerServices
		{
			get
			{
				if (CompModSwitches.commonDesignerServices == null)
				{
					CompModSwitches.commonDesignerServices = new BooleanSwitch("CommonDesignerServices", "Assert if any common designer service is not found.");
				}
				return CompModSwitches.commonDesignerServices;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x060012B9 RID: 4793 RVA: 0x0004E627 File Offset: 0x0004C827
		public static TraceSwitch EventLog
		{
			get
			{
				if (CompModSwitches.eventLog == null)
				{
					CompModSwitches.eventLog = new TraceSwitch("EventLog", "Enable tracing for the EventLog component.");
				}
				return CompModSwitches.eventLog;
			}
		}

		// Token: 0x0400127E RID: 4734
		private static volatile BooleanSwitch commonDesignerServices;

		// Token: 0x0400127F RID: 4735
		private static volatile TraceSwitch eventLog;
	}
}
