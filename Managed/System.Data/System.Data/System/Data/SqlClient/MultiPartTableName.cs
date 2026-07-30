using System;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x02000224 RID: 548
	internal struct MultiPartTableName
	{
		// Token: 0x06001891 RID: 6289 RVA: 0x0007D60D File Offset: 0x0007B80D
		internal MultiPartTableName(string[] parts)
		{
			this._multipartName = null;
			this._serverName = parts[0];
			this._catalogName = parts[1];
			this._schemaName = parts[2];
			this._tableName = parts[3];
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x0007D63A File Offset: 0x0007B83A
		internal MultiPartTableName(string multipartName)
		{
			this._multipartName = multipartName;
			this._serverName = null;
			this._catalogName = null;
			this._schemaName = null;
			this._tableName = null;
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001893 RID: 6291 RVA: 0x0007D65F File Offset: 0x0007B85F
		// (set) Token: 0x06001894 RID: 6292 RVA: 0x0007D66D File Offset: 0x0007B86D
		internal string ServerName
		{
			get
			{
				this.ParseMultipartName();
				return this._serverName;
			}
			set
			{
				this._serverName = value;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001895 RID: 6293 RVA: 0x0007D676 File Offset: 0x0007B876
		// (set) Token: 0x06001896 RID: 6294 RVA: 0x0007D684 File Offset: 0x0007B884
		internal string CatalogName
		{
			get
			{
				this.ParseMultipartName();
				return this._catalogName;
			}
			set
			{
				this._catalogName = value;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001897 RID: 6295 RVA: 0x0007D68D File Offset: 0x0007B88D
		// (set) Token: 0x06001898 RID: 6296 RVA: 0x0007D69B File Offset: 0x0007B89B
		internal string SchemaName
		{
			get
			{
				this.ParseMultipartName();
				return this._schemaName;
			}
			set
			{
				this._schemaName = value;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001899 RID: 6297 RVA: 0x0007D6A4 File Offset: 0x0007B8A4
		// (set) Token: 0x0600189A RID: 6298 RVA: 0x0007D6B2 File Offset: 0x0007B8B2
		internal string TableName
		{
			get
			{
				this.ParseMultipartName();
				return this._tableName;
			}
			set
			{
				this._tableName = value;
			}
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x0007D6BC File Offset: 0x0007B8BC
		private void ParseMultipartName()
		{
			if (this._multipartName != null)
			{
				string[] array = MultipartIdentifier.ParseMultipartIdentifier(this._multipartName, "[\"", "]\"", "Processing of results from SQL Server failed because of an invalid multipart name", false);
				this._serverName = array[0];
				this._catalogName = array[1];
				this._schemaName = array[2];
				this._tableName = array[3];
				this._multipartName = null;
			}
		}

		// Token: 0x040011B4 RID: 4532
		private string _multipartName;

		// Token: 0x040011B5 RID: 4533
		private string _serverName;

		// Token: 0x040011B6 RID: 4534
		private string _catalogName;

		// Token: 0x040011B7 RID: 4535
		private string _schemaName;

		// Token: 0x040011B8 RID: 4536
		private string _tableName;

		// Token: 0x040011B9 RID: 4537
		internal static readonly MultiPartTableName Null = new MultiPartTableName(new string[4]);
	}
}
