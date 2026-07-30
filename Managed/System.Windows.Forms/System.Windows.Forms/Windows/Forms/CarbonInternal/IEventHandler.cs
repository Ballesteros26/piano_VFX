using System;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020004AF RID: 1199
	internal interface IEventHandler
	{
		// Token: 0x06004BED RID: 19437
		bool ProcessEvent(IntPtr callref, IntPtr eventref, IntPtr handle, uint kind, ref MSG msg);
	}
}
