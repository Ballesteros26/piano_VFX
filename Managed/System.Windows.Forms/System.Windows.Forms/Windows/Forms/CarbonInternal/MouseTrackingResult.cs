using System;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020004AD RID: 1197
	internal enum MouseTrackingResult : ushort
	{
		// Token: 0x04002924 RID: 10532
		kMouseTrackingMouseDown = 1,
		// Token: 0x04002925 RID: 10533
		kMouseTrackingMouseUp,
		// Token: 0x04002926 RID: 10534
		kMouseTrackingMouseExited,
		// Token: 0x04002927 RID: 10535
		kMouseTrackingMouseEntered,
		// Token: 0x04002928 RID: 10536
		kMouseTrackingMouseDragged,
		// Token: 0x04002929 RID: 10537
		kMouseTrackingKeyModifiersChanged,
		// Token: 0x0400292A RID: 10538
		kMouseTrackingUserCancelled,
		// Token: 0x0400292B RID: 10539
		kMouseTrackingTimedOut,
		// Token: 0x0400292C RID: 10540
		kMouseTrackingMouseMoved
	}
}
