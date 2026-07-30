using System;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x02000097 RID: 151
	internal interface IDtdInfo
	{
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600051D RID: 1309
		XmlQualifiedName Name { get; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600051E RID: 1310
		string InternalDtdSubset { get; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600051F RID: 1311
		bool HasDefaultAttributes { get; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000520 RID: 1312
		bool HasNonCDataAttributes { get; }

		// Token: 0x06000521 RID: 1313
		IDtdAttributeListInfo LookupAttributeList(string prefix, string localName);

		// Token: 0x06000522 RID: 1314
		IEnumerable<IDtdAttributeListInfo> GetAttributeLists();

		// Token: 0x06000523 RID: 1315
		IDtdEntityInfo LookupEntity(string name);
	}
}
