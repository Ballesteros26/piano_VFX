using System;

namespace System.Xml
{
	// Token: 0x02000099 RID: 153
	internal interface IDtdAttributeInfo
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600052A RID: 1322
		string Prefix { get; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600052B RID: 1323
		string LocalName { get; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600052C RID: 1324
		int LineNumber { get; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600052D RID: 1325
		int LinePosition { get; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600052E RID: 1326
		bool IsNonCDataType { get; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600052F RID: 1327
		bool IsDeclaredInExternal { get; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000530 RID: 1328
		bool IsXmlAttribute { get; }
	}
}
