using System;

namespace System.Xml.Schema
{
	// Token: 0x0200040C RID: 1036
	internal class RedefineEntry
	{
		// Token: 0x06002810 RID: 10256 RVA: 0x000EDE6E File Offset: 0x000EC06E
		public RedefineEntry(XmlSchemaRedefine external, XmlSchema schema)
		{
			this.redefine = external;
			this.schemaToUpdate = schema;
		}

		// Token: 0x04001AAE RID: 6830
		internal XmlSchemaRedefine redefine;

		// Token: 0x04001AAF RID: 6831
		internal XmlSchema schemaToUpdate;
	}
}
