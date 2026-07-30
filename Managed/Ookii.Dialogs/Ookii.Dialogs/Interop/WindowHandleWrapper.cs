using System;
using System.Windows.Forms;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x02000047 RID: 71
	internal class WindowHandleWrapper : IWin32Window
	{
		// Token: 0x060002EC RID: 748 RVA: 0x0000A14B File Offset: 0x0000834B
		public WindowHandleWrapper(IntPtr handle)
		{
			this._handle = handle;
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000A15C File Offset: 0x0000835C
		public IntPtr Handle
		{
			get
			{
				return this._handle;
			}
		}

		// Token: 0x040000EC RID: 236
		private IntPtr _handle;
	}
}
