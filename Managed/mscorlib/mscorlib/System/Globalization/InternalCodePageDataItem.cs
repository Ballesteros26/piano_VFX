using System;
using System.Security;

namespace System.Globalization
{
	// Token: 0x02000440 RID: 1088
	internal struct InternalCodePageDataItem
	{
		// Token: 0x04001BC9 RID: 7113
		internal ushort codePage;

		// Token: 0x04001BCA RID: 7114
		internal ushort uiFamilyCodePage;

		// Token: 0x04001BCB RID: 7115
		internal uint flags;

		// Token: 0x04001BCC RID: 7116
		[SecurityCritical]
		internal string Names;
	}
}
