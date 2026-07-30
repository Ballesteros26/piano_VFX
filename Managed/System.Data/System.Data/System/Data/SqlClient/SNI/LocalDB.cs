using System;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x0200023F RID: 575
	internal class LocalDB
	{
		// Token: 0x060019BD RID: 6589 RVA: 0x0004B979 File Offset: 0x00049B79
		internal static string GetLocalDBConnectionString(string localDbInstance)
		{
			throw new PlatformNotSupportedException("LocalDB is not supported on this platform.");
		}
	}
}
