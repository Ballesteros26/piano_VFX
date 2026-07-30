using System;
using System.Text;

namespace System.Data.SqlClient
{
	// Token: 0x02000221 RID: 545
	internal class SqlMetaDataPriv
	{
		// Token: 0x0600188C RID: 6284 RVA: 0x0007D4DC File Offset: 0x0007B6DC
		internal SqlMetaDataPriv()
		{
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x0007D4FC File Offset: 0x0007B6FC
		internal virtual void CopyFrom(SqlMetaDataPriv original)
		{
			this.type = original.type;
			this.tdsType = original.tdsType;
			this.precision = original.precision;
			this.scale = original.scale;
			this.length = original.length;
			this.collation = original.collation;
			this.codePage = original.codePage;
			this.encoding = original.encoding;
			this.isNullable = original.isNullable;
			this.udtDatabaseName = original.udtDatabaseName;
			this.udtSchemaName = original.udtSchemaName;
			this.udtTypeName = original.udtTypeName;
			this.udtAssemblyQualifiedName = original.udtAssemblyQualifiedName;
			this.xmlSchemaCollectionDatabase = original.xmlSchemaCollectionDatabase;
			this.xmlSchemaCollectionOwningSchema = original.xmlSchemaCollectionOwningSchema;
			this.xmlSchemaCollectionName = original.xmlSchemaCollectionName;
			this.metaType = original.metaType;
		}

		// Token: 0x04001194 RID: 4500
		internal SqlDbType type;

		// Token: 0x04001195 RID: 4501
		internal byte tdsType;

		// Token: 0x04001196 RID: 4502
		internal byte precision = byte.MaxValue;

		// Token: 0x04001197 RID: 4503
		internal byte scale = byte.MaxValue;

		// Token: 0x04001198 RID: 4504
		internal int length;

		// Token: 0x04001199 RID: 4505
		internal SqlCollation collation;

		// Token: 0x0400119A RID: 4506
		internal int codePage;

		// Token: 0x0400119B RID: 4507
		internal Encoding encoding;

		// Token: 0x0400119C RID: 4508
		internal bool isNullable;

		// Token: 0x0400119D RID: 4509
		internal string udtDatabaseName;

		// Token: 0x0400119E RID: 4510
		internal string udtSchemaName;

		// Token: 0x0400119F RID: 4511
		internal string udtTypeName;

		// Token: 0x040011A0 RID: 4512
		internal string udtAssemblyQualifiedName;

		// Token: 0x040011A1 RID: 4513
		internal string xmlSchemaCollectionDatabase;

		// Token: 0x040011A2 RID: 4514
		internal string xmlSchemaCollectionOwningSchema;

		// Token: 0x040011A3 RID: 4515
		internal string xmlSchemaCollectionName;

		// Token: 0x040011A4 RID: 4516
		internal MetaType metaType;
	}
}
