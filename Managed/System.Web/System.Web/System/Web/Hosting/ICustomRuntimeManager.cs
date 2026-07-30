using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	// Token: 0x0200053B RID: 1339
	[Guid("A0BBBDFF-5AF5-42E3-9753-34441F764A6B")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ICustomRuntimeManager
	{
		// Token: 0x06003A75 RID: 14965
		[return: MarshalAs(UnmanagedType.Interface)]
		ICustomRuntimeRegistrationToken Register([MarshalAs(UnmanagedType.Interface)] [In] ICustomRuntime customRuntime);
	}
}
