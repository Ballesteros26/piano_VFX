using System;
using System.Security.Permissions;

namespace System.Configuration
{
	// Token: 0x0200015F RID: 351
	[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
	internal static class PrivilegedConfigurationManager
	{
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x00038846 File Offset: 0x00036A46
		internal static ConnectionStringSettingsCollection ConnectionStrings
		{
			get
			{
				return ConfigurationManager.ConnectionStrings;
			}
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0003884D File Offset: 0x00036A4D
		internal static object GetSection(string sectionName)
		{
			return ConfigurationManager.GetSection(sectionName);
		}
	}
}
