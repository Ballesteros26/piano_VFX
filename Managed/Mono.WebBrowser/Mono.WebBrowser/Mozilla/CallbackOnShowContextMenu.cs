using System;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x0200005A RID: 90
	// (Invoke) Token: 0x06000269 RID: 617
	internal delegate void CallbackOnShowContextMenu(uint contextFlags, [MarshalAs(UnmanagedType.Interface)] nsIDOMEvent eve, [MarshalAs(UnmanagedType.Interface)] nsIDOMNode node);
}
