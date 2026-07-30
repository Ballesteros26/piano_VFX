using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Web.Services.Interop
{
	// Token: 0x02000096 RID: 150
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("1AF04045-6659-4aaa-9F4B-2741AC56224B")]
	[SuppressUnmanagedCodeSecurity]
	[ComImport]
	internal interface INotifyConnection2
	{
		// Token: 0x060003E5 RID: 997
		[return: MarshalAs(UnmanagedType.Interface)]
		INotifySink2 RegisterNotifySource([MarshalAs(UnmanagedType.Interface)] [In] INotifySource2 in_pNotifySource);

		// Token: 0x060003E6 RID: 998
		void UnregisterNotifySource([MarshalAs(UnmanagedType.Interface)] [In] INotifySource2 in_pNotifySource);
	}
}
