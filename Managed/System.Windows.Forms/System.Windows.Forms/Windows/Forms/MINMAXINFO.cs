using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000462 RID: 1122
	[StructLayout(0, CharSet = 3)]
	internal struct MINMAXINFO
	{
		// Token: 0x0400257F RID: 9599
		internal POINT ptReserved;

		// Token: 0x04002580 RID: 9600
		internal POINT ptMaxSize;

		// Token: 0x04002581 RID: 9601
		internal POINT ptMaxPosition;

		// Token: 0x04002582 RID: 9602
		internal POINT ptMinTrackSize;

		// Token: 0x04002583 RID: 9603
		internal POINT ptMaxTrackSize;
	}
}
