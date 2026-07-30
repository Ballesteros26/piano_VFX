using System;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000057 RID: 87
	// (Invoke) Token: 0x0600025D RID: 605
	internal delegate void CallbackOnSecurityChange([MarshalAs(UnmanagedType.Interface)] nsIWebProgress progress, [MarshalAs(UnmanagedType.Interface)] nsIRequest request, uint status);
}
