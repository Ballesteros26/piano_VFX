using System;
using System.Data.Common;

namespace System.Data.OleDb
{
	// Token: 0x0200013A RID: 314
	[MonoTODO("OleDb is not implemented.")]
	internal sealed class OleDbConnectionString : DbConnectionOptions
	{
		// Token: 0x06001006 RID: 4102 RVA: 0x00050F9E File Offset: 0x0004F19E
		internal OleDbConnectionString(string connectionString, bool validate)
			: base(connectionString, null)
		{
		}
	}
}
