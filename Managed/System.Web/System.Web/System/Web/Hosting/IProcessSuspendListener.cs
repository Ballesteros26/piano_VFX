using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	// Token: 0x02000540 RID: 1344
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("406E6C4C-1C5D-4357-9DFE-EF4BE00D654B")]
	[ComImport]
	internal interface IProcessSuspendListener
	{
		// Token: 0x06003A7F RID: 14975
		[return: MarshalAs(UnmanagedType.Interface)]
		IProcessResumeCallback Suspend();
	}
}
