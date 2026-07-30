using System;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000048 RID: 72
	// (Invoke) Token: 0x06000221 RID: 545
	internal delegate bool KeyCallback(KeyInfo keyInfo, ModifierKeys modifiers, [MarshalAs(UnmanagedType.Interface)] nsIDOMNode target);
}
