using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x020001B4 RID: 436
	internal class PerfCounterSection : ConfigurationElement
	{
		// Token: 0x06000CE5 RID: 3301 RVA: 0x0003E8FC File Offset: 0x0003CAFC
		static PerfCounterSection()
		{
			PerfCounterSection._properties.Add(PerfCounterSection._propFileMappingSize);
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x0003E93B File Offset: 0x0003CB3B
		[ConfigurationProperty("filemappingsize", DefaultValue = 524288)]
		public int FileMappingSize
		{
			get
			{
				return (int)base[PerfCounterSection._propFileMappingSize];
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x0003E94D File Offset: 0x0003CB4D
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return PerfCounterSection._properties;
			}
		}

		// Token: 0x0400101E RID: 4126
		private static readonly ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x0400101F RID: 4127
		private static readonly ConfigurationProperty _propFileMappingSize = new ConfigurationProperty("filemappingsize", typeof(int), 524288, ConfigurationPropertyOptions.None);
	}
}
