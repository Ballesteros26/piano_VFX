using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x020001CB RID: 459
	internal class TraceSection : ConfigurationElement
	{
		// Token: 0x06000E04 RID: 3588 RVA: 0x00041E5C File Offset: 0x0004005C
		static TraceSection()
		{
			TraceSection._properties.Add(TraceSection._propListeners);
			TraceSection._properties.Add(TraceSection._propAutoFlush);
			TraceSection._properties.Add(TraceSection._propIndentSize);
			TraceSection._properties.Add(TraceSection._propUseGlobalLock);
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000E05 RID: 3589 RVA: 0x00041F2E File Offset: 0x0004012E
		[ConfigurationProperty("autoflush", DefaultValue = false)]
		public bool AutoFlush
		{
			get
			{
				return (bool)base[TraceSection._propAutoFlush];
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000E06 RID: 3590 RVA: 0x00041F40 File Offset: 0x00040140
		[ConfigurationProperty("indentsize", DefaultValue = 4)]
		public int IndentSize
		{
			get
			{
				return (int)base[TraceSection._propIndentSize];
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000E07 RID: 3591 RVA: 0x00041F52 File Offset: 0x00040152
		[ConfigurationProperty("listeners")]
		public ListenerElementsCollection Listeners
		{
			get
			{
				return (ListenerElementsCollection)base[TraceSection._propListeners];
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000E08 RID: 3592 RVA: 0x00041F64 File Offset: 0x00040164
		[ConfigurationProperty("useGlobalLock", DefaultValue = true)]
		public bool UseGlobalLock
		{
			get
			{
				return (bool)base[TraceSection._propUseGlobalLock];
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000E09 RID: 3593 RVA: 0x00041F76 File Offset: 0x00040176
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TraceSection._properties;
			}
		}

		// Token: 0x04001081 RID: 4225
		private static readonly ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x04001082 RID: 4226
		private static readonly ConfigurationProperty _propListeners = new ConfigurationProperty("listeners", typeof(ListenerElementsCollection), new ListenerElementsCollection(), ConfigurationPropertyOptions.None);

		// Token: 0x04001083 RID: 4227
		private static readonly ConfigurationProperty _propAutoFlush = new ConfigurationProperty("autoflush", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04001084 RID: 4228
		private static readonly ConfigurationProperty _propIndentSize = new ConfigurationProperty("indentsize", typeof(int), 4, ConfigurationPropertyOptions.None);

		// Token: 0x04001085 RID: 4229
		private static readonly ConfigurationProperty _propUseGlobalLock = new ConfigurationProperty("useGlobalLock", typeof(bool), true, ConfigurationPropertyOptions.None);
	}
}
