using System;

namespace Mono.Mozilla
{
	// Token: 0x02000052 RID: 82
	// (Invoke) Token: 0x06000249 RID: 585
	internal delegate bool CallbackOnPromptUsernameAndPassword(IntPtr title, IntPtr text, IntPtr chkMsg, ref bool chkState, out IntPtr username, out IntPtr password);
}
