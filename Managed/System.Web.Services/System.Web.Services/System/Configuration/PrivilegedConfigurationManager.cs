using System;
using System.Security.Permissions;

namespace System.Configuration
{
	// Token: 0x02000004 RID: 4
	[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
	internal static class PrivilegedConfigurationManager
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020AF File Offset: 0x000002AF
		internal static ConnectionStringSettingsCollection ConnectionStrings
		{
			get
			{
				return ConfigurationManager.ConnectionStrings;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020B6 File Offset: 0x000002B6
		internal static object GetSection(string sectionName)
		{
			return ConfigurationManager.GetSection(sectionName);
		}
	}
}
