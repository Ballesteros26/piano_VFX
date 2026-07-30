using System;
using System.Runtime.InteropServices;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000036 RID: 54
	internal class SqliteConnectionHandle : CriticalHandle
	{
		// Token: 0x060002E4 RID: 740 RVA: 0x0000E4DE File Offset: 0x0000C6DE
		public static implicit operator IntPtr(SqliteConnectionHandle db)
		{
			return db.handle;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000E4E6 File Offset: 0x0000C6E6
		public static implicit operator SqliteConnectionHandle(IntPtr db)
		{
			return new SqliteConnectionHandle(db);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000E4EE File Offset: 0x0000C6EE
		private SqliteConnectionHandle(IntPtr db)
			: this()
		{
			base.SetHandle(db);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000E4FD File Offset: 0x0000C6FD
		internal SqliteConnectionHandle()
			: base(IntPtr.Zero)
		{
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000E50C File Offset: 0x0000C70C
		protected override bool ReleaseHandle()
		{
			try
			{
				SQLiteBase.CloseConnection(this);
			}
			catch (SqliteException)
			{
			}
			return true;
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000E538 File Offset: 0x0000C738
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}
	}
}
