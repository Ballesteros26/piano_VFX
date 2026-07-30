using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x0200003A RID: 58
	[Guid("38521333-6A87-46A7-AE10-0F16706816C3")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IKnownFolder
	{
		// Token: 0x060002AD RID: 685
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetId(out Guid pkfid);

		// Token: 0x060002AE RID: 686
		void spacer1();

		// Token: 0x060002AF RID: 687
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetShellItem([In] uint dwFlags, ref Guid riid, out IShellItem ppv);

		// Token: 0x060002B0 RID: 688
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetPath([In] uint dwFlags, [MarshalAs(21)] out string ppszPath);

		// Token: 0x060002B1 RID: 689
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetPath([In] uint dwFlags, [MarshalAs(21)] [In] string pszPath);

		// Token: 0x060002B2 RID: 690
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetLocation([In] uint dwFlags, [ComAliasName("Interop.wirePIDL")] [Out] IntPtr ppidl);

		// Token: 0x060002B3 RID: 691
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetFolderType(out Guid pftid);

		// Token: 0x060002B4 RID: 692
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetRedirectionCapabilities(out uint pCapabilities);

		// Token: 0x060002B5 RID: 693
		void spacer2();
	}
}
