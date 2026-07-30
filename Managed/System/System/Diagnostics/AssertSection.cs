using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x020001A8 RID: 424
	internal class AssertSection : ConfigurationElement
	{
		// Token: 0x06000C67 RID: 3175 RVA: 0x0003D2D8 File Offset: 0x0003B4D8
		static AssertSection()
		{
			AssertSection._properties.Add(AssertSection._propAssertUIEnabled);
			AssertSection._properties.Add(AssertSection._propLogFile);
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x0003D34C File Offset: 0x0003B54C
		[ConfigurationProperty("assertuienabled", DefaultValue = true)]
		public bool AssertUIEnabled
		{
			get
			{
				return (bool)base[AssertSection._propAssertUIEnabled];
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x0003D35E File Offset: 0x0003B55E
		[ConfigurationProperty("logfilename", DefaultValue = "")]
		public string LogFileName
		{
			get
			{
				return (string)base[AssertSection._propLogFile];
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000C6A RID: 3178 RVA: 0x0003D370 File Offset: 0x0003B570
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AssertSection._properties;
			}
		}

		// Token: 0x04001009 RID: 4105
		private static readonly ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x0400100A RID: 4106
		private static readonly ConfigurationProperty _propAssertUIEnabled = new ConfigurationProperty("assertuienabled", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x0400100B RID: 4107
		private static readonly ConfigurationProperty _propLogFile = new ConfigurationProperty("logfilename", typeof(string), string.Empty, ConfigurationPropertyOptions.None);
	}
}
