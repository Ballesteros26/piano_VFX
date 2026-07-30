using System;
using System.Xml.Schema;

namespace System.Data
{
	// Token: 0x02000103 RID: 259
	internal sealed class ConstraintTable
	{
		// Token: 0x06000D42 RID: 3394 RVA: 0x0003E694 File Offset: 0x0003C894
		public ConstraintTable(DataTable t, XmlSchemaIdentityConstraint c)
		{
			this.table = t;
			this.constraint = c;
		}

		// Token: 0x040008BE RID: 2238
		public DataTable table;

		// Token: 0x040008BF RID: 2239
		public XmlSchemaIdentityConstraint constraint;
	}
}
