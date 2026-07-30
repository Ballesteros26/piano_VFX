using System;
using System.Security.Permissions;

namespace System.Configuration
{
	// Token: 0x02000028 RID: 40
	[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
	internal static class PrivilegedConfigurationManager
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00005E3C File Offset: 0x0000403C
		internal static ConnectionStringSettingsCollection ConnectionStrings
		{
			get
			{
				return ConfigurationManager.ConnectionStrings;
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00005E43 File Offset: 0x00004043
		internal static object GetSection(string sectionName)
		{
			return ConfigurationManager.GetSection(sectionName);
		}
	}
}
