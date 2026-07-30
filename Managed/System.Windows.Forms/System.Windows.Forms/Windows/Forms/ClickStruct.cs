using System;

namespace System.Windows.Forms
{
	// Token: 0x02000432 RID: 1074
	internal struct ClickStruct
	{
		// Token: 0x040021DD RID: 8669
		internal IntPtr Hwnd;

		// Token: 0x040021DE RID: 8670
		internal Msg Message;

		// Token: 0x040021DF RID: 8671
		internal IntPtr wParam;

		// Token: 0x040021E0 RID: 8672
		internal IntPtr lParam;

		// Token: 0x040021E1 RID: 8673
		internal long Time;

		// Token: 0x040021E2 RID: 8674
		internal bool Pending;
	}
}
