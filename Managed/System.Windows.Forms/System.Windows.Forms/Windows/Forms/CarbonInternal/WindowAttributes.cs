using System;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020004AB RID: 1195
	internal enum WindowAttributes : uint
	{
		// Token: 0x040028F7 RID: 10487
		kWindowNoAttributes,
		// Token: 0x040028F8 RID: 10488
		kWindowCloseBoxAttribute,
		// Token: 0x040028F9 RID: 10489
		kWindowHorizontalZoomAttribute,
		// Token: 0x040028FA RID: 10490
		kWindowVerticalZoomAttribute = 4U,
		// Token: 0x040028FB RID: 10491
		kWindowFullZoomAttribute = 6U,
		// Token: 0x040028FC RID: 10492
		kWindowCollapseBoxAttribute = 8U,
		// Token: 0x040028FD RID: 10493
		kWindowResizableAttribute = 16U,
		// Token: 0x040028FE RID: 10494
		kWindowSideTitlebarAttribute = 32U,
		// Token: 0x040028FF RID: 10495
		kWindowToolbarButtonAttribute = 64U,
		// Token: 0x04002900 RID: 10496
		kWindowMetalAttribute = 256U,
		// Token: 0x04002901 RID: 10497
		kWindowNoUpdatesAttribute = 65536U,
		// Token: 0x04002902 RID: 10498
		kWindowNoActivatesAttribute = 131072U,
		// Token: 0x04002903 RID: 10499
		kWindowOpaqueForEventsAttribute = 262144U,
		// Token: 0x04002904 RID: 10500
		kWindowCompositingAttribute = 524288U,
		// Token: 0x04002905 RID: 10501
		kWindowNoShadowAttribute = 2097152U,
		// Token: 0x04002906 RID: 10502
		kWindowHideOnSuspendAttribute = 16777216U,
		// Token: 0x04002907 RID: 10503
		kWindowStandardHandlerAttribute = 33554432U,
		// Token: 0x04002908 RID: 10504
		kWindowHideOnFullScreenAttribute = 67108864U,
		// Token: 0x04002909 RID: 10505
		kWindowInWindowMenuAttribute = 134217728U,
		// Token: 0x0400290A RID: 10506
		kWindowLiveResizeAttribute = 268435456U,
		// Token: 0x0400290B RID: 10507
		kWindowIgnoreClicksAttribute = 536870912U,
		// Token: 0x0400290C RID: 10508
		kWindowNoConstrainAttribute = 2147483648U,
		// Token: 0x0400290D RID: 10509
		kWindowStandardDocumentAttributes = 31U,
		// Token: 0x0400290E RID: 10510
		kWindowStandardFloatingAttributes = 9U
	}
}
