using System;
using System.Diagnostics;

namespace System.ComponentModel
{
	// Token: 0x02000151 RID: 337
	internal sealed class CompModSwitches
	{
		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x00045740 File Offset: 0x00043940
		public static BooleanSwitch DisableRemoteDebugging
		{
			get
			{
				if (CompModSwitches.disableRemoteDebugging == null)
				{
					CompModSwitches.disableRemoteDebugging = new BooleanSwitch("Remote.Disable", "Disable remote debugging for web methods.");
				}
				return CompModSwitches.disableRemoteDebugging;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x00045762 File Offset: 0x00043962
		public static TraceSwitch DynamicDiscoverySearcher
		{
			get
			{
				if (CompModSwitches.dynamicDiscoSearcher == null)
				{
					CompModSwitches.dynamicDiscoSearcher = new TraceSwitch("DynamicDiscoverySearcher", "Enable tracing for the DynamicDiscoverySearcher class.");
				}
				return CompModSwitches.dynamicDiscoSearcher;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x00045784 File Offset: 0x00043984
		public static BooleanSwitch DynamicDiscoveryVirtualSearch
		{
			get
			{
				if (CompModSwitches.dynamicDiscoVirtualSearch == null)
				{
					CompModSwitches.dynamicDiscoVirtualSearch = new BooleanSwitch("DynamicDiscoveryVirtualSearch", "Force virtual search for DiscoveryRequestHandler class.");
				}
				return CompModSwitches.dynamicDiscoVirtualSearch;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x000457A6 File Offset: 0x000439A6
		public static TraceSwitch Remote
		{
			get
			{
				if (CompModSwitches.remote == null)
				{
					CompModSwitches.remote = new TraceSwitch("Microsoft.WFC.Remote", "Enable tracing for remote method calls.");
				}
				return CompModSwitches.remote;
			}
		}

		// Token: 0x040005DB RID: 1499
		private static BooleanSwitch dynamicDiscoVirtualSearch;

		// Token: 0x040005DC RID: 1500
		private static TraceSwitch dynamicDiscoSearcher;

		// Token: 0x040005DD RID: 1501
		private static BooleanSwitch disableRemoteDebugging;

		// Token: 0x040005DE RID: 1502
		private static TraceSwitch remote;
	}
}
