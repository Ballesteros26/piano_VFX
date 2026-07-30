using System;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x02000190 RID: 400
	internal class SqlConnectionPoolKey : DbConnectionPoolKey
	{
		// Token: 0x060012CA RID: 4810 RVA: 0x0005DA00 File Offset: 0x0005BC00
		internal SqlConnectionPoolKey(string connectionString)
			: base(connectionString)
		{
			this.CalculateHashCode();
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x0005DA0F File Offset: 0x0005BC0F
		private SqlConnectionPoolKey(SqlConnectionPoolKey key)
			: base(key)
		{
			this.CalculateHashCode();
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x0005DA1E File Offset: 0x0005BC1E
		public override object Clone()
		{
			return new SqlConnectionPoolKey(this);
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x060012CD RID: 4813 RVA: 0x0005DA26 File Offset: 0x0005BC26
		// (set) Token: 0x060012CE RID: 4814 RVA: 0x0005DA2E File Offset: 0x0005BC2E
		internal override string ConnectionString
		{
			get
			{
				return base.ConnectionString;
			}
			set
			{
				base.ConnectionString = value;
				this.CalculateHashCode();
			}
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x0005DA40 File Offset: 0x0005BC40
		public override bool Equals(object obj)
		{
			SqlConnectionPoolKey sqlConnectionPoolKey = obj as SqlConnectionPoolKey;
			return sqlConnectionPoolKey != null && this.ConnectionString == sqlConnectionPoolKey.ConnectionString;
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x0005DA6A File Offset: 0x0005BC6A
		public override int GetHashCode()
		{
			return this._hashValue;
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x0005DA72 File Offset: 0x0005BC72
		private void CalculateHashCode()
		{
			this._hashValue = base.GetHashCode();
		}

		// Token: 0x04000C3A RID: 3130
		private int _hashValue;
	}
}
