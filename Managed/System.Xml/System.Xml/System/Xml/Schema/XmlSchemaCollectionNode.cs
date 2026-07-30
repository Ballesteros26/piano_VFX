using System;

namespace System.Xml.Schema
{
	// Token: 0x02000440 RID: 1088
	internal sealed class XmlSchemaCollectionNode
	{
		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06002B46 RID: 11078 RVA: 0x0010562F File Offset: 0x0010382F
		// (set) Token: 0x06002B47 RID: 11079 RVA: 0x00105637 File Offset: 0x00103837
		internal string NamespaceURI
		{
			get
			{
				return this.namespaceUri;
			}
			set
			{
				this.namespaceUri = value;
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06002B48 RID: 11080 RVA: 0x00105640 File Offset: 0x00103840
		// (set) Token: 0x06002B49 RID: 11081 RVA: 0x00105648 File Offset: 0x00103848
		internal SchemaInfo SchemaInfo
		{
			get
			{
				return this.schemaInfo;
			}
			set
			{
				this.schemaInfo = value;
			}
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06002B4A RID: 11082 RVA: 0x00105651 File Offset: 0x00103851
		// (set) Token: 0x06002B4B RID: 11083 RVA: 0x00105659 File Offset: 0x00103859
		internal XmlSchema Schema
		{
			get
			{
				return this.schema;
			}
			set
			{
				this.schema = value;
			}
		}

		// Token: 0x04001D4A RID: 7498
		private string namespaceUri;

		// Token: 0x04001D4B RID: 7499
		private SchemaInfo schemaInfo;

		// Token: 0x04001D4C RID: 7500
		private XmlSchema schema;
	}
}
