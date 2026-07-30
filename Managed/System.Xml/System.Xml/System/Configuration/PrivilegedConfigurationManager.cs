using System;
using System.Security.Permissions;

namespace System.Configuration
{
	// Token: 0x0200006D RID: 109
	[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
	internal static class PrivilegedConfigurationManager
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0000D32F File Offset: 0x0000B52F
		internal static ConnectionStringSettingsCollection ConnectionStrings
		{
			get
			{
				return ConfigurationManager.ConnectionStrings;
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000D336 File Offset: 0x0000B536
		internal static object GetSection(string sectionName)
		{
			return ConfigurationManager.GetSection(sectionName);
		}
	}
}
