using System;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000024 RID: 36
	public class SqliteFunctionEx : SqliteFunction
	{
		// Token: 0x060001FF RID: 511 RVA: 0x0000C090 File Offset: 0x0000A290
		protected CollationSequence GetCollationSequence()
		{
			return this._base.GetCollationSequence(this, this._context);
		}
	}
}
