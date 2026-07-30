using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x0200003B RID: 59
	[Guid("44BEAAEC-24F4-4E90-B3F0-23D258FBB146")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IKnownFolderManager
	{
		// Token: 0x060002B6 RID: 694
		[MethodImpl(4096, MethodCodeType = 3)]
		void FolderIdFromCsidl([In] int nCsidl, out Guid pfid);

		// Token: 0x060002B7 RID: 695
		[MethodImpl(4096, MethodCodeType = 3)]
		void FolderIdToCsidl([In] ref Guid rfid, out int pnCsidl);

		// Token: 0x060002B8 RID: 696
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetFolderIds([Out] IntPtr ppKFId, [In] [Out] ref uint pCount);

		// Token: 0x060002B9 RID: 697
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetFolder([In] ref Guid rfid, [MarshalAs(28)] out IKnownFolder ppkf);

		// Token: 0x060002BA RID: 698
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetFolderByName([MarshalAs(21)] [In] string pszCanonicalName, [MarshalAs(28)] out IKnownFolder ppkf);

		// Token: 0x060002BB RID: 699
		[MethodImpl(4096, MethodCodeType = 3)]
		void RegisterFolder([In] ref Guid rfid, [In] ref NativeMethods.KNOWNFOLDER_DEFINITION pKFD);

		// Token: 0x060002BC RID: 700
		[MethodImpl(4096, MethodCodeType = 3)]
		void UnregisterFolder([In] ref Guid rfid);

		// Token: 0x060002BD RID: 701
		[MethodImpl(4096, MethodCodeType = 3)]
		void FindFolderFromPath([MarshalAs(21)] [In] string pszPath, [In] NativeMethods.FFFP_MODE mode, [MarshalAs(28)] out IKnownFolder ppkf);

		// Token: 0x060002BE RID: 702
		[MethodImpl(4096, MethodCodeType = 3)]
		void FindFolderFromIDList([In] IntPtr pidl, [MarshalAs(28)] out IKnownFolder ppkf);

		// Token: 0x060002BF RID: 703
		[MethodImpl(4096, MethodCodeType = 3)]
		void Redirect([In] ref Guid rfid, [In] IntPtr hwnd, [In] uint Flags, [MarshalAs(21)] [In] string pszTargetPath, [In] uint cFolders, [In] ref Guid pExclusion, [MarshalAs(21)] out string ppszError);
	}
}
