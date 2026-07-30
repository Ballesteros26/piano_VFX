using System;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000055 RID: 85
	// (Invoke) Token: 0x06000255 RID: 597
	internal delegate void CallbackOnLocationChanged([MarshalAs(UnmanagedType.Interface)] nsIWebProgress progress, [MarshalAs(UnmanagedType.Interface)] nsIRequest request, [MarshalAs(UnmanagedType.Interface)] nsIURI uri);
}
