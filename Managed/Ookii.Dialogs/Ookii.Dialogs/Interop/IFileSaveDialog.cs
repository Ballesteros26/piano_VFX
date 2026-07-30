using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x02000036 RID: 54
	[Guid("84bccd23-5fde-4cdb-aea4-af64b83d78ab")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IFileSaveDialog : IFileDialog, IModalWindow
	{
		// Token: 0x0600027D RID: 637
		[MethodImpl(4224, MethodCodeType = 3)]
		int Show([In] IntPtr parent);

		// Token: 0x0600027E RID: 638
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetFileTypes([In] uint cFileTypes, [In] ref NativeMethods.COMDLG_FILTERSPEC rgFilterSpec);

		// Token: 0x0600027F RID: 639
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetFileTypeIndex([In] uint iFileType);

		// Token: 0x06000280 RID: 640
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetFileTypeIndex(out uint piFileType);

		// Token: 0x06000281 RID: 641
		[MethodImpl(4096, MethodCodeType = 3)]
		void Advise([MarshalAs(28)] [In] IFileDialogEvents pfde, out uint pdwCookie);

		// Token: 0x06000282 RID: 642
		[MethodImpl(4096, MethodCodeType = 3)]
		void Unadvise([In] uint dwCookie);

		// Token: 0x06000283 RID: 643
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetOptions([In] NativeMethods.FOS fos);

		// Token: 0x06000284 RID: 644
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetOptions(out NativeMethods.FOS pfos);

		// Token: 0x06000285 RID: 645
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetDefaultFolder([MarshalAs(28)] [In] IShellItem psi);

		// Token: 0x06000286 RID: 646
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetFolder([MarshalAs(28)] [In] IShellItem psi);

		// Token: 0x06000287 RID: 647
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetFolder([MarshalAs(28)] out IShellItem ppsi);

		// Token: 0x06000288 RID: 648
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetCurrentSelection([MarshalAs(28)] out IShellItem ppsi);

		// Token: 0x06000289 RID: 649
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetFileName([MarshalAs(21)] [In] string pszName);

		// Token: 0x0600028A RID: 650
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetFileName([MarshalAs(21)] out string pszName);

		// Token: 0x0600028B RID: 651
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetTitle([MarshalAs(21)] [In] string pszTitle);

		// Token: 0x0600028C RID: 652
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetOkButtonLabel([MarshalAs(21)] [In] string pszText);

		// Token: 0x0600028D RID: 653
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetFileNameLabel([MarshalAs(21)] [In] string pszLabel);

		// Token: 0x0600028E RID: 654
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetResult([MarshalAs(28)] out IShellItem ppsi);

		// Token: 0x0600028F RID: 655
		[MethodImpl(4096, MethodCodeType = 3)]
		void AddPlace([MarshalAs(28)] [In] IShellItem psi, NativeMethods.FDAP fdap);

		// Token: 0x06000290 RID: 656
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetDefaultExtension([MarshalAs(21)] [In] string pszDefaultExtension);

		// Token: 0x06000291 RID: 657
		[MethodImpl(4096, MethodCodeType = 3)]
		void Close([MarshalAs(45)] int hr);

		// Token: 0x06000292 RID: 658
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetClientGuid([In] ref Guid guid);

		// Token: 0x06000293 RID: 659
		[MethodImpl(4096, MethodCodeType = 3)]
		void ClearClientData();

		// Token: 0x06000294 RID: 660
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetFilter([MarshalAs(28)] IntPtr pFilter);

		// Token: 0x06000295 RID: 661
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetSaveAsItem([MarshalAs(28)] [In] IShellItem psi);

		// Token: 0x06000296 RID: 662
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetProperties([MarshalAs(28)] [In] IntPtr pStore);

		// Token: 0x06000297 RID: 663
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetCollectedProperties([MarshalAs(28)] [In] IntPtr pList, [In] int fAppendDefault);

		// Token: 0x06000298 RID: 664
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetProperties([MarshalAs(28)] out IntPtr ppStore);

		// Token: 0x06000299 RID: 665
		[MethodImpl(4096, MethodCodeType = 3)]
		void ApplyProperties([MarshalAs(28)] [In] IShellItem psi, [MarshalAs(28)] [In] IntPtr pStore, [ComAliasName("Interop.wireHWND")] [In] ref IntPtr hwnd, [MarshalAs(28)] [In] IntPtr pSink);
	}
}
