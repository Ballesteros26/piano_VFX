using System;
using System.Security;

namespace System.Globalization
{
	// Token: 0x0200043F RID: 1087
	internal struct InternalEncodingDataItem
	{
		// Token: 0x04001BC7 RID: 7111
		[SecurityCritical]
		internal string webName;

		// Token: 0x04001BC8 RID: 7112
		internal ushort codePage;
	}
}
