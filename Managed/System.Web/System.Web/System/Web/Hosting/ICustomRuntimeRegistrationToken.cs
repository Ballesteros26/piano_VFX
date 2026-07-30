using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	// Token: 0x0200053C RID: 1340
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("3A8E9CED-D3C9-4C4B-8956-6F15E2F559D9")]
	[ComImport]
	internal interface ICustomRuntimeRegistrationToken
	{
		// Token: 0x06003A76 RID: 14966
		void Unregister();
	}
}
