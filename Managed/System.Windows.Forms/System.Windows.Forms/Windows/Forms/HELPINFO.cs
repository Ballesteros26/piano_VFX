using System;

namespace System.Windows.Forms
{
	// Token: 0x0200045B RID: 1115
	internal struct HELPINFO
	{
		// Token: 0x04002519 RID: 9497
		internal uint cbSize;

		// Token: 0x0400251A RID: 9498
		internal int iContextType;

		// Token: 0x0400251B RID: 9499
		internal int iCtrlId;

		// Token: 0x0400251C RID: 9500
		internal IntPtr hItemHandle;

		// Token: 0x0400251D RID: 9501
		internal uint dwContextId;

		// Token: 0x0400251E RID: 9502
		internal POINT MousePos;
	}
}
