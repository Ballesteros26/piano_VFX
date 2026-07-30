using System;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020004BA RID: 1210
	internal struct CarbonEvent
	{
		// Token: 0x06004C1D RID: 19485 RVA: 0x0012EEF8 File Offset: 0x0012D0F8
		public CarbonEvent(IntPtr hWnd, IntPtr evt)
		{
			this.hWnd = hWnd;
			this.evt = evt;
		}

		// Token: 0x04002972 RID: 10610
		public IntPtr hWnd;

		// Token: 0x04002973 RID: 10611
		public IntPtr evt;
	}
}
