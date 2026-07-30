using System;
using System.Windows.Forms;

namespace SFB
{
	// Token: 0x0200002C RID: 44
	public class WindowWrapper : IWin32Window
	{
		// Token: 0x0600018A RID: 394 RVA: 0x000129FF File Offset: 0x00010BFF
		public WindowWrapper(IntPtr handle)
		{
			this._hwnd = handle;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00012A0E File Offset: 0x00010C0E
		public IntPtr Handle
		{
			get
			{
				return this._hwnd;
			}
		}

		// Token: 0x040003AE RID: 942
		private IntPtr _hwnd;
	}
}
