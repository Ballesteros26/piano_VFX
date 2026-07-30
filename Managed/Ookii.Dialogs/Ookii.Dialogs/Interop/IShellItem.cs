using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x02000038 RID: 56
	[Guid("43826D1E-E718-42EE-BC55-A1E261C37BFE")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IShellItem
	{
		// Token: 0x060002A1 RID: 673
		[MethodImpl(4096, MethodCodeType = 3)]
		void BindToHandler([MarshalAs(28)] [In] IntPtr pbc, [In] ref Guid bhid, [In] ref Guid riid, out IntPtr ppv);

		// Token: 0x060002A2 RID: 674
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetParent([MarshalAs(28)] out IShellItem ppsi);

		// Token: 0x060002A3 RID: 675
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetDisplayName([In] NativeMethods.SIGDN sigdnName, [MarshalAs(21)] out string ppszName);

		// Token: 0x060002A4 RID: 676
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetAttributes([In] uint sfgaoMask, out uint psfgaoAttribs);

		// Token: 0x060002A5 RID: 677
		[MethodImpl(4096, MethodCodeType = 3)]
		void Compare([MarshalAs(28)] [In] IShellItem psi, [In] uint hint, out int piOrder);
	}
}
