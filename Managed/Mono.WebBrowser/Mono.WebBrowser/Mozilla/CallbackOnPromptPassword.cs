using System;

namespace Mono.Mozilla
{
	// Token: 0x02000053 RID: 83
	// (Invoke) Token: 0x0600024D RID: 589
	internal delegate bool CallbackOnPromptPassword(IntPtr title, IntPtr text, IntPtr chkMsg, ref bool chkState, out IntPtr password);
}
