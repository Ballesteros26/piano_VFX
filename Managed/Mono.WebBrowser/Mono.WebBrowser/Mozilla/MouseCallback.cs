using System;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000049 RID: 73
	// (Invoke) Token: 0x06000225 RID: 549
	internal delegate bool MouseCallback(MouseInfo mouseInfo, ModifierKeys modifiers, [MarshalAs(UnmanagedType.Interface)] nsIDOMNode target);
}
