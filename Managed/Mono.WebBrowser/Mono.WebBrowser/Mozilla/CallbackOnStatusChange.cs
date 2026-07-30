using System;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000056 RID: 86
	// (Invoke) Token: 0x06000259 RID: 601
	internal delegate void CallbackOnStatusChange([MarshalAs(UnmanagedType.Interface)] nsIWebProgress progress, [MarshalAs(UnmanagedType.Interface)] nsIRequest request, [MarshalAs(UnmanagedType.LPWStr)] string message, int status);
}
