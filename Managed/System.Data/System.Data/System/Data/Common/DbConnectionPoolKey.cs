using System;

namespace System.Data.Common
{
	// Token: 0x02000321 RID: 801
	internal class DbConnectionPoolKey : ICloneable
	{
		// Token: 0x0600247E RID: 9342 RVA: 0x000A6E06 File Offset: 0x000A5006
		internal DbConnectionPoolKey(string connectionString)
		{
			this._connectionString = connectionString;
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x000A6E15 File Offset: 0x000A5015
		protected DbConnectionPoolKey(DbConnectionPoolKey key)
		{
			this._connectionString = key.ConnectionString;
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x000A6E29 File Offset: 0x000A5029
		public virtual object Clone()
		{
			return new DbConnectionPoolKey(this);
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06002481 RID: 9345 RVA: 0x000A6E31 File Offset: 0x000A5031
		// (set) Token: 0x06002482 RID: 9346 RVA: 0x000A6E39 File Offset: 0x000A5039
		internal virtual string ConnectionString
		{
			get
			{
				return this._connectionString;
			}
			set
			{
				this._connectionString = value;
			}
		}

		// Token: 0x06002483 RID: 9347 RVA: 0x000A6E44 File Offset: 0x000A5044
		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != typeof(DbConnectionPoolKey))
			{
				return false;
			}
			DbConnectionPoolKey dbConnectionPoolKey = obj as DbConnectionPoolKey;
			return dbConnectionPoolKey != null && this._connectionString == dbConnectionPoolKey._connectionString;
		}

		// Token: 0x06002484 RID: 9348 RVA: 0x000A6E8A File Offset: 0x000A508A
		public override int GetHashCode()
		{
			if (this._connectionString != null)
			{
				return this._connectionString.GetHashCode();
			}
			return 0;
		}

		// Token: 0x040017CB RID: 6091
		private string _connectionString;
	}
}
