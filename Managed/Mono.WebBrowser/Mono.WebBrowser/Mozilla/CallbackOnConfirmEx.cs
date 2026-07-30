using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x02000050 RID: 80
	// (Invoke) Token: 0x06000241 RID: 577
	internal delegate bool CallbackOnConfirmEx(IntPtr title, IntPtr text, DialogButtonFlags flags, IntPtr title0, IntPtr title1, IntPtr title2, IntPtr chkMsg, ref bool chkState, out int retVal);
}
