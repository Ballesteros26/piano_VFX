using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x020001C0 RID: 448
	internal class SystemDiagnosticsSection : ConfigurationSection
	{
		// Token: 0x06000D3D RID: 3389 RVA: 0x0003F68C File Offset: 0x0003D88C
		static SystemDiagnosticsSection()
		{
			SystemDiagnosticsSection._properties.Add(SystemDiagnosticsSection._propAssert);
			SystemDiagnosticsSection._properties.Add(SystemDiagnosticsSection._propPerfCounters);
			SystemDiagnosticsSection._properties.Add(SystemDiagnosticsSection._propSources);
			SystemDiagnosticsSection._properties.Add(SystemDiagnosticsSection._propSharedListeners);
			SystemDiagnosticsSection._properties.Add(SystemDiagnosticsSection._propSwitches);
			SystemDiagnosticsSection._properties.Add(SystemDiagnosticsSection._propTrace);
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x0003F7B7 File Offset: 0x0003D9B7
		[ConfigurationProperty("assert")]
		public AssertSection Assert
		{
			get
			{
				return (AssertSection)base[SystemDiagnosticsSection._propAssert];
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000D3F RID: 3391 RVA: 0x0003F7C9 File Offset: 0x0003D9C9
		[ConfigurationProperty("performanceCounters")]
		public PerfCounterSection PerfCounters
		{
			get
			{
				return (PerfCounterSection)base[SystemDiagnosticsSection._propPerfCounters];
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x0003F7DB File Offset: 0x0003D9DB
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SystemDiagnosticsSection._properties;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x0003F7E2 File Offset: 0x0003D9E2
		[ConfigurationProperty("sources")]
		public SourceElementsCollection Sources
		{
			get
			{
				return (SourceElementsCollection)base[SystemDiagnosticsSection._propSources];
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x0003F7F4 File Offset: 0x0003D9F4
		[ConfigurationProperty("sharedListeners")]
		public ListenerElementsCollection SharedListeners
		{
			get
			{
				return (ListenerElementsCollection)base[SystemDiagnosticsSection._propSharedListeners];
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000D43 RID: 3395 RVA: 0x0003F806 File Offset: 0x0003DA06
		[ConfigurationProperty("switches")]
		public SwitchElementsCollection Switches
		{
			get
			{
				return (SwitchElementsCollection)base[SystemDiagnosticsSection._propSwitches];
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x0003F818 File Offset: 0x0003DA18
		[ConfigurationProperty("trace")]
		public TraceSection Trace
		{
			get
			{
				return (TraceSection)base[SystemDiagnosticsSection._propTrace];
			}
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x0003F82A File Offset: 0x0003DA2A
		protected override void InitializeDefault()
		{
			this.Trace.Listeners.InitializeDefaultInternal();
		}

		// Token: 0x04001046 RID: 4166
		private static readonly ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x04001047 RID: 4167
		private static readonly ConfigurationProperty _propAssert = new ConfigurationProperty("assert", typeof(AssertSection), new AssertSection(), ConfigurationPropertyOptions.None);

		// Token: 0x04001048 RID: 4168
		private static readonly ConfigurationProperty _propPerfCounters = new ConfigurationProperty("performanceCounters", typeof(PerfCounterSection), new PerfCounterSection(), ConfigurationPropertyOptions.None);

		// Token: 0x04001049 RID: 4169
		private static readonly ConfigurationProperty _propSources = new ConfigurationProperty("sources", typeof(SourceElementsCollection), new SourceElementsCollection(), ConfigurationPropertyOptions.None);

		// Token: 0x0400104A RID: 4170
		private static readonly ConfigurationProperty _propSharedListeners = new ConfigurationProperty("sharedListeners", typeof(SharedListenerElementsCollection), new SharedListenerElementsCollection(), ConfigurationPropertyOptions.None);

		// Token: 0x0400104B RID: 4171
		private static readonly ConfigurationProperty _propSwitches = new ConfigurationProperty("switches", typeof(SwitchElementsCollection), new SwitchElementsCollection(), ConfigurationPropertyOptions.None);

		// Token: 0x0400104C RID: 4172
		private static readonly ConfigurationProperty _propTrace = new ConfigurationProperty("trace", typeof(TraceSection), new TraceSection(), ConfigurationPropertyOptions.None);
	}
}
