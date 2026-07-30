using System;
using System.Runtime.InteropServices;

namespace System.Web.Services.Interop
{
	// Token: 0x02000098 RID: 152
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("26E7F0F1-B49C-48cb-B43E-78DCD577E1D9")]
	[ComImport]
	internal interface INotifySource2
	{
		// Token: 0x060003EB RID: 1003
		void SetNotifyFilter([In] NotifyFilter in_NotifyFilter, [In] UserThread in_pUserThreadFilter);
	}
}
