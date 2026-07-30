using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x02000034 RID: 52
	[Guid("42f85136-db7e-439c-85f1-e4075d135fc8")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IFileDialog : IModalWindow
	{
		// Token: 0x0600024B RID: 587
		[MethodImpl(4224, MethodCodeType = 3)]
		int Show([In] IntPtr parent);

		// Token: 0x0600024C RID: 588
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetFileTypes([In] uint cFileTypes, [MarshalAs(42)] [In] NativeMethods.COMDLG_FILTERSPEC[] rgFilterSpec);

		// Token: 0x0600024D RID: 589
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetFileTypeIndex([In] uint iFileType);

		// Token: 0x0600024E RID: 590
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetFileTypeIndex(out uint piFileType);

		// Token: 0x0600024F RID: 591
		[MethodImpl(4096, MethodCodeType = 3)]
		void Advise([MarshalAs(28)] [In] IFileDialogEvents pfde, out uint pdwCookie);

		// Token: 0x06000250 RID: 592
		[MethodImpl(4096, MethodCodeType = 3)]
		void Unadvise([In] uint dwCookie);

		// Token: 0x06000251 RID: 593
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetOptions([In] NativeMethods.FOS fos);

		// Token: 0x06000252 RID: 594
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetOptions(out NativeMethods.FOS pfos);

		// Token: 0x06000253 RID: 595
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetDefaultFolder([MarshalAs(28)] [In] IShellItem psi);

		// Token: 0x06000254 RID: 596
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetFolder([MarshalAs(28)] [In] IShellItem psi);

		// Token: 0x06000255 RID: 597
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetFolder([MarshalAs(28)] out IShellItem ppsi);

		// Token: 0x06000256 RID: 598
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetCurrentSelection([MarshalAs(28)] out IShellItem ppsi);

		// Token: 0x06000257 RID: 599
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetFileName([MarshalAs(21)] [In] string pszName);

		// Token: 0x06000258 RID: 600
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetFileName([MarshalAs(21)] out string pszName);

		// Token: 0x06000259 RID: 601
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetTitle([MarshalAs(21)] [In] string pszTitle);

		// Token: 0x0600025A RID: 602
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetOkButtonLabel([MarshalAs(21)] [In] string pszText);

		// Token: 0x0600025B RID: 603
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetFileNameLabel([MarshalAs(21)] [In] string pszLabel);

		// Token: 0x0600025C RID: 604
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetResult([MarshalAs(28)] out IShellItem ppsi);

		// Token: 0x0600025D RID: 605
		[MethodImpl(4096, MethodCodeType = 3)]
		void AddPlace([MarshalAs(28)] [In] IShellItem psi, NativeMethods.FDAP fdap);

		// Token: 0x0600025E RID: 606
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetDefaultExtension([MarshalAs(21)] [In] string pszDefaultExtension);

		// Token: 0x0600025F RID: 607
		[MethodImpl(4096, MethodCodeType = 3)]
		void Close([MarshalAs(45)] int hr);

		// Token: 0x06000260 RID: 608
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetClientGuid([In] ref Guid guid);

		// Token: 0x06000261 RID: 609
		[MethodImpl(4096, MethodCodeType = 3)]
		void ClearClientData();

		// Token: 0x06000262 RID: 610
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetFilter([MarshalAs(28)] IntPtr pFilter);
	}
}
