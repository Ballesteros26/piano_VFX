using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the ASP.NET trace service. This class cannot be inherited.</summary>
	// Token: 0x020005E2 RID: 1506
	public sealed class TraceSection : ConfigurationSection
	{
		// Token: 0x06004149 RID: 16713 RVA: 0x000AB394 File Offset: 0x000A9594
		static TraceSection()
		{
			TraceSection.properties.Add(TraceSection.enabledProp);
			TraceSection.properties.Add(TraceSection.localOnlyProp);
			TraceSection.properties.Add(TraceSection.mostRecentProp);
			TraceSection.properties.Add(TraceSection.pageOutputProp);
			TraceSection.properties.Add(TraceSection.requestLimitProp);
			TraceSection.properties.Add(TraceSection.traceModeProp);
			TraceSection.properties.Add(TraceSection.writeToDiagnosticsTraceProp);
		}

		/// <summary>Gets or sets a value indicating whether the ASP.NET trace service is enabled.</summary>
		/// <returns>true if trace is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x170014C3 RID: 5315
		// (get) Token: 0x0600414A RID: 16714 RVA: 0x000AB514 File Offset: 0x000A9714
		// (set) Token: 0x0600414B RID: 16715 RVA: 0x000AB526 File Offset: 0x000A9726
		[ConfigurationProperty("enabled", DefaultValue = "False")]
		public bool Enabled
		{
			get
			{
				return (bool)base[TraceSection.enabledProp];
			}
			set
			{
				base[TraceSection.enabledProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the ASP.NET trace viewer (Trace.axd) is available only to requests from the host Web server.</summary>
		/// <returns>true if the ASP.NET trace viewer (Trace.axd) is available only to requests from the host Web server; otherwise, false. The default is true.</returns>
		// Token: 0x170014C4 RID: 5316
		// (get) Token: 0x0600414C RID: 16716 RVA: 0x000AB539 File Offset: 0x000A9739
		// (set) Token: 0x0600414D RID: 16717 RVA: 0x000AB54B File Offset: 0x000A974B
		[ConfigurationProperty("localOnly", DefaultValue = "True")]
		public bool LocalOnly
		{
			get
			{
				return (bool)base[TraceSection.localOnlyProp];
			}
			set
			{
				base[TraceSection.localOnlyProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the most recent requests are always stored on the server.</summary>
		/// <returns>true if the most recent requests are always stored in the trace log; otherwise, false. The default is false.</returns>
		// Token: 0x170014C5 RID: 5317
		// (get) Token: 0x0600414E RID: 16718 RVA: 0x000AB55E File Offset: 0x000A975E
		// (set) Token: 0x0600414F RID: 16719 RVA: 0x000AB570 File Offset: 0x000A9770
		[ConfigurationProperty("mostRecent", DefaultValue = "False")]
		public bool MostRecent
		{
			get
			{
				return (bool)base[TraceSection.mostRecentProp];
			}
			set
			{
				base[TraceSection.mostRecentProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the ASP.NET trace information is appended to the output of each page.</summary>
		/// <returns>true if the trace information is appended to each page; otherwise, false. The default is false.</returns>
		// Token: 0x170014C6 RID: 5318
		// (get) Token: 0x06004150 RID: 16720 RVA: 0x000AB583 File Offset: 0x000A9783
		// (set) Token: 0x06004151 RID: 16721 RVA: 0x000AB595 File Offset: 0x000A9795
		[ConfigurationProperty("pageOutput", DefaultValue = "False")]
		public bool PageOutput
		{
			get
			{
				return (bool)base[TraceSection.pageOutputProp];
			}
			set
			{
				base[TraceSection.pageOutputProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the maximum number of requests to the application for which ASP.NET stores trace information. </summary>
		/// <returns>The maximum number of requests to store on the server. The default is 10.</returns>
		// Token: 0x170014C7 RID: 5319
		// (get) Token: 0x06004152 RID: 16722 RVA: 0x000AB5A8 File Offset: 0x000A97A8
		// (set) Token: 0x06004153 RID: 16723 RVA: 0x000AB5BA File Offset: 0x000A97BA
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647)]
		[ConfigurationProperty("requestLimit", DefaultValue = "10")]
		public int RequestLimit
		{
			get
			{
				return (int)base[TraceSection.requestLimitProp];
			}
			set
			{
				base[TraceSection.requestLimitProp] = value;
			}
		}

		/// <summary>Gets or sets the order in which ASP.NET trace information is displayed.</summary>
		/// <returns>One of the <see cref="T:System.Web.Configuration.TraceDisplayMode" /> values, indicating the order in which trace information is displayed.</returns>
		// Token: 0x170014C8 RID: 5320
		// (get) Token: 0x06004154 RID: 16724 RVA: 0x000AB5CD File Offset: 0x000A97CD
		// (set) Token: 0x06004155 RID: 16725 RVA: 0x000AB5DF File Offset: 0x000A97DF
		[ConfigurationProperty("traceMode", DefaultValue = "SortByTime")]
		public TraceDisplayMode TraceMode
		{
			get
			{
				return (TraceDisplayMode)base[TraceSection.traceModeProp];
			}
			set
			{
				base[TraceSection.traceModeProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the messages emitted through the page trace are forwarded to an instance of the <see cref="T:System.Diagnostics.Trace" /> class.</summary>
		/// <returns>true if the trace messages are sent to the <see cref="T:System.Diagnostics.Trace" /> class; otherwise, false. The default is false.</returns>
		// Token: 0x170014C9 RID: 5321
		// (get) Token: 0x06004156 RID: 16726 RVA: 0x000AB5F2 File Offset: 0x000A97F2
		// (set) Token: 0x06004157 RID: 16727 RVA: 0x000AB604 File Offset: 0x000A9804
		[ConfigurationProperty("writeToDiagnosticsTrace", DefaultValue = "False")]
		public bool WriteToDiagnosticsTrace
		{
			get
			{
				return (bool)base[TraceSection.writeToDiagnosticsTraceProp];
			}
			set
			{
				base[TraceSection.writeToDiagnosticsTraceProp] = value;
			}
		}

		// Token: 0x170014CA RID: 5322
		// (get) Token: 0x06004158 RID: 16728 RVA: 0x000AB617 File Offset: 0x000A9817
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TraceSection.properties;
			}
		}

		// Token: 0x0400232A RID: 9002
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), false);

		// Token: 0x0400232B RID: 9003
		private static ConfigurationProperty localOnlyProp = new ConfigurationProperty("localOnly", typeof(bool), true);

		// Token: 0x0400232C RID: 9004
		private static ConfigurationProperty mostRecentProp = new ConfigurationProperty("mostRecent", typeof(bool), false);

		// Token: 0x0400232D RID: 9005
		private static ConfigurationProperty pageOutputProp = new ConfigurationProperty("pageOutput", typeof(bool), false);

		// Token: 0x0400232E RID: 9006
		private static ConfigurationProperty requestLimitProp = new ConfigurationProperty("requestLimit", typeof(int), 10, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400232F RID: 9007
		private static ConfigurationProperty traceModeProp = new ConfigurationProperty("traceMode", typeof(TraceDisplayMode), TraceDisplayMode.SortByTime, new GenericEnumConverter(typeof(TraceDisplayMode)), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002330 RID: 9008
		private static ConfigurationProperty writeToDiagnosticsTraceProp = new ConfigurationProperty("writeToDiagnosticsTrace", typeof(bool), false);

		// Token: 0x04002331 RID: 9009
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
