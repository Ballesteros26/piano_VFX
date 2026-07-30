using System;
using System.Data.Common;

namespace System.Data.Odbc
{
	// Token: 0x02000293 RID: 659
	internal sealed class OdbcConnectionString : DbConnectionOptions
	{
		// Token: 0x06001BE3 RID: 7139 RVA: 0x0008A288 File Offset: 0x00088488
		internal OdbcConnectionString(string connectionString, bool validate)
			: base(connectionString, null, true)
		{
			if (!validate)
			{
				string text = null;
				int num = 0;
				this._expandedConnectionString = base.ExpandDataDirectories(ref text, ref num);
			}
			if ((validate || this._expandedConnectionString == null) && connectionString != null && 1024 < connectionString.Length)
			{
				throw ODBC.ConnectionStringTooLong();
			}
		}

		// Token: 0x040014FC RID: 5372
		private readonly string _expandedConnectionString;
	}
}
