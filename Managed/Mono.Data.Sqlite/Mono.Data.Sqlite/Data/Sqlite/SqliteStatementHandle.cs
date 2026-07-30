using System;
using System.Runtime.InteropServices;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000037 RID: 55
	internal class SqliteStatementHandle : CriticalHandle
	{
		// Token: 0x060002EA RID: 746 RVA: 0x0000E54A File Offset: 0x0000C74A
		public static implicit operator IntPtr(SqliteStatementHandle stmt)
		{
			return stmt.handle;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000E552 File Offset: 0x0000C752
		public static implicit operator SqliteStatementHandle(IntPtr stmt)
		{
			return new SqliteStatementHandle(stmt);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000E55A File Offset: 0x0000C75A
		private SqliteStatementHandle(IntPtr stmt)
			: this()
		{
			base.SetHandle(stmt);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000E569 File Offset: 0x0000C769
		internal SqliteStatementHandle()
			: base(IntPtr.Zero)
		{
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000E578 File Offset: 0x0000C778
		protected override bool ReleaseHandle()
		{
			try
			{
				SQLiteBase.FinalizeStatement(this);
			}
			catch (SqliteException)
			{
			}
			return true;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000E5A4 File Offset: 0x0000C7A4
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}
	}
}
