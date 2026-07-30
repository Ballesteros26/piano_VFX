using System;

namespace System.Windows.Forms
{
	// Token: 0x02000452 RID: 1106
	internal struct MSG
	{
		// Token: 0x060048BD RID: 18621 RVA: 0x00119CC0 File Offset: 0x00117EC0
		public override string ToString()
		{
			return string.Format("msg=0x{0:x} ({1}) hwnd=0x{2:x} wparam=0x{3:x} lparam=0x{4:x} pt={5}", new object[]
			{
				(int)this.message,
				this.message.ToString(),
				this.hwnd.ToInt32(),
				this.wParam.ToInt32(),
				this.lParam.ToInt32(),
				this.pt
			});
		}

		// Token: 0x04002416 RID: 9238
		internal IntPtr hwnd;

		// Token: 0x04002417 RID: 9239
		internal Msg message;

		// Token: 0x04002418 RID: 9240
		internal IntPtr wParam;

		// Token: 0x04002419 RID: 9241
		internal IntPtr lParam;

		// Token: 0x0400241A RID: 9242
		internal uint time;

		// Token: 0x0400241B RID: 9243
		internal POINT pt;

		// Token: 0x0400241C RID: 9244
		internal object refobject;
	}
}
