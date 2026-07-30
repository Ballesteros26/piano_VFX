using System;
using System.IO;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000297 RID: 663
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal static class IntSecurity
	{
		// Token: 0x060014AF RID: 5295 RVA: 0x00052F69 File Offset: 0x00051169
		public static string UnsafeGetFullPath(string fileName)
		{
			return Path.GetFullPath(fileName);
		}
	}
}
