using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200072D RID: 1837
	[Serializable]
	internal enum SoapAttributeType
	{
		// Token: 0x04002879 RID: 10361
		None,
		// Token: 0x0400287A RID: 10362
		SchemaType,
		// Token: 0x0400287B RID: 10363
		Embedded,
		// Token: 0x0400287C RID: 10364
		XmlElement = 4,
		// Token: 0x0400287D RID: 10365
		XmlAttribute = 8
	}
}
