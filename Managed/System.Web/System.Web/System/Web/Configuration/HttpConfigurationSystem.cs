using System;
using System.Configuration.Internal;

namespace System.Web.Configuration
{
	// Token: 0x020005AA RID: 1450
	internal class HttpConfigurationSystem : IInternalConfigSystem
	{
		// Token: 0x06003E0D RID: 15885 RVA: 0x0000F5EF File Offset: 0x0000D7EF
		object IInternalConfigSystem.GetSection(string configKey)
		{
			return WebConfigurationManager.GetSection(configKey);
		}

		// Token: 0x06003E0E RID: 15886 RVA: 0x0000393A File Offset: 0x00001B3A
		void IInternalConfigSystem.RefreshConfig(string sectionName)
		{
		}

		// Token: 0x17001372 RID: 4978
		// (get) Token: 0x06003E0F RID: 15887 RVA: 0x00008B66 File Offset: 0x00006D66
		bool IInternalConfigSystem.SupportsUserConfig
		{
			get
			{
				return true;
			}
		}
	}
}
