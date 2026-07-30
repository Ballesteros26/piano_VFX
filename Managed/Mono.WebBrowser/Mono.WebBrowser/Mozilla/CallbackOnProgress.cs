using System;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000059 RID: 89
	// (Invoke) Token: 0x06000265 RID: 613
	internal delegate void CallbackOnProgress([MarshalAs(UnmanagedType.Interface)] nsIWebProgress progress, [MarshalAs(UnmanagedType.Interface)] nsIRequest request, int arg2, int arg3);
}
