using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	// Token: 0x0200053A RID: 1338
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("692D0723-C338-4D09-9057-C71F0F47DA87")]
	[ComImport]
	internal interface ICustomRuntime
	{
		// Token: 0x06003A72 RID: 14962
		void Start([In] IntPtr reserved0, [In] int reserved1);

		// Token: 0x06003A73 RID: 14963
		void ResolveModules([In] IntPtr pResolveModuleData, [In] int resolveModuleDataSize);

		// Token: 0x06003A74 RID: 14964
		void Stop([In] IntPtr reserved0, [In] int reserved1);
	}
}
