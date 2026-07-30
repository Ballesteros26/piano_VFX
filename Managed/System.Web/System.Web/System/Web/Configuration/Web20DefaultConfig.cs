using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020005F2 RID: 1522
	internal class Web20DefaultConfig : IConfigurationSystem
	{
		// Token: 0x0600422C RID: 16940 RVA: 0x000AD296 File Offset: 0x000AB496
		public static Web20DefaultConfig GetInstance()
		{
			return Web20DefaultConfig.instance;
		}

		// Token: 0x0600422D RID: 16941 RVA: 0x000AD2A0 File Offset: 0x000AB4A0
		public object GetConfig(string sectionName)
		{
			object webApplicationSection = WebConfigurationManager.GetWebApplicationSection(sectionName);
			if (webApplicationSection == null || webApplicationSection is IgnoreSection)
			{
				object config = WebConfigurationManager.oldConfig.GetConfig(sectionName);
				if (config != null)
				{
					return config;
				}
			}
			return webApplicationSection;
		}

		// Token: 0x0600422E RID: 16942 RVA: 0x0000393A File Offset: 0x00001B3A
		public void Init()
		{
		}

		// Token: 0x04002367 RID: 9063
		private static Web20DefaultConfig instance = new Web20DefaultConfig();
	}
}
