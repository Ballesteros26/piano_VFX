using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Web.Services.Interop
{
	// Token: 0x02000097 RID: 151
	[SuppressUnmanagedCodeSecurity]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("C43CC2F3-90AF-4e93-9112-DFB8B36749B5")]
	[ComImport]
	internal interface INotifySink2
	{
		// Token: 0x060003E7 RID: 999
		void OnSyncCallOut([In] CallId callId, out IntPtr out_ppBuffer, [In] [Out] ref int inout_pBufferSize);

		// Token: 0x060003E8 RID: 1000
		void OnSyncCallEnter([In] CallId callId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] in_pBuffer, [In] int in_BufferSize);

		// Token: 0x060003E9 RID: 1001
		void OnSyncCallReturn([In] CallId callId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] in_pBuffer, [In] int in_BufferSize);

		// Token: 0x060003EA RID: 1002
		void OnSyncCallExit([In] CallId callId, out IntPtr out_ppBuffer, [In] [Out] ref int inout_pBufferSize);
	}
}
