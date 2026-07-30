using System;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020001EC RID: 492
	internal class AttributePSVIInfo
	{
		// Token: 0x060011A4 RID: 4516 RVA: 0x00067DFA File Offset: 0x00065FFA
		internal AttributePSVIInfo()
		{
			this.attributeSchemaInfo = new XmlSchemaInfo();
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x00067E0D File Offset: 0x0006600D
		internal void Reset()
		{
			this.typedAttributeValue = null;
			this.localName = string.Empty;
			this.namespaceUri = string.Empty;
			this.attributeSchemaInfo.Clear();
		}

		// Token: 0x04000C77 RID: 3191
		internal string localName;

		// Token: 0x04000C78 RID: 3192
		internal string namespaceUri;

		// Token: 0x04000C79 RID: 3193
		internal object typedAttributeValue;

		// Token: 0x04000C7A RID: 3194
		internal XmlSchemaInfo attributeSchemaInfo;
	}
}
