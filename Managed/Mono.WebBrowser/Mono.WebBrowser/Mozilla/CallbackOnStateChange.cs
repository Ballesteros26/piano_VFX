using System;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000058 RID: 88
	// (Invoke) Token: 0x06000261 RID: 609
	internal delegate void CallbackOnStateChange([MarshalAs(UnmanagedType.Interface)] nsIWebProgress progress, [MarshalAs(UnmanagedType.Interface)] nsIRequest request, int arg2, uint arg3);
}
