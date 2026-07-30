using System;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x02000098 RID: 152
	internal interface IDtdAttributeListInfo
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000524 RID: 1316
		string Prefix { get; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000525 RID: 1317
		string LocalName { get; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000526 RID: 1318
		bool HasNonCDataAttributes { get; }

		// Token: 0x06000527 RID: 1319
		IDtdAttributeInfo LookupAttribute(string prefix, string localName);

		// Token: 0x06000528 RID: 1320
		IEnumerable<IDtdDefaultAttributeInfo> LookupDefaultAttributes();

		// Token: 0x06000529 RID: 1321
		IDtdAttributeInfo LookupIdAttribute();
	}
}
