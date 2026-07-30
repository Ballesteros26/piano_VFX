using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000961 RID: 2401
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("82BA7092-4C88-427D-A7BC-16DD93FEB67E")]
	[ComImport]
	internal interface IRestrictedErrorInfo
	{
		// Token: 0x06005946 RID: 22854
		void GetErrorDetails([MarshalAs(UnmanagedType.BStr)] out string description, out int error, [MarshalAs(UnmanagedType.BStr)] out string restrictedDescription, [MarshalAs(UnmanagedType.BStr)] out string capabilitySid);

		// Token: 0x06005947 RID: 22855
		void GetReference([MarshalAs(UnmanagedType.BStr)] out string reference);
	}
}
