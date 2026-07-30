using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;

namespace System.Web.Hosting
{
	// Token: 0x02000539 RID: 1337
	[Guid("50A3CE65-2F9F-44E9-9094-32C6C928F966")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ICustomLoader
	{
		// Token: 0x06003A71 RID: 14961
		[return: MarshalAs(UnmanagedType.Interface)]
		IObjectHandle LoadApplication([MarshalAs(UnmanagedType.LPWStr)] [In] string appId, [MarshalAs(UnmanagedType.LPWStr)] [In] string appConfigPath, [MarshalAs(UnmanagedType.Interface)] [In] IProcessHostSupportFunctions supportFunctions, [In] IntPtr pLoadAppData, [In] int loadAppDataSize);
	}
}
