using System;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020004AA RID: 1194
	internal enum WindowClass : uint
	{
		// Token: 0x040028E6 RID: 10470
		kAlertWindowClass = 1U,
		// Token: 0x040028E7 RID: 10471
		kMovableAlertWindowClass,
		// Token: 0x040028E8 RID: 10472
		kModalWindowClass,
		// Token: 0x040028E9 RID: 10473
		kMovableModalWindowClass,
		// Token: 0x040028EA RID: 10474
		kFloatingWindowClass,
		// Token: 0x040028EB RID: 10475
		kDocumentWindowClass,
		// Token: 0x040028EC RID: 10476
		kUtilityWindowClass = 8U,
		// Token: 0x040028ED RID: 10477
		kHelpWindowClass = 10U,
		// Token: 0x040028EE RID: 10478
		kSheetWindowClass,
		// Token: 0x040028EF RID: 10479
		kToolbarWindowClass,
		// Token: 0x040028F0 RID: 10480
		kPlainWindowClass,
		// Token: 0x040028F1 RID: 10481
		kOverlayWindowClass,
		// Token: 0x040028F2 RID: 10482
		kSheetAlertWindowClass,
		// Token: 0x040028F3 RID: 10483
		kAltPlainWindowClass,
		// Token: 0x040028F4 RID: 10484
		kDrawerWindowClass = 20U,
		// Token: 0x040028F5 RID: 10485
		kAllWindowClasses = 4294967295U
	}
}
