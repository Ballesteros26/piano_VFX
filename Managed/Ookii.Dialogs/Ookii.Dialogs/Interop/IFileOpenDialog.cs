using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x02000035 RID: 53
	[Guid("d57c7288-d4ad-4768-be02-9d969532d960")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IFileOpenDialog : IFileDialog, IModalWindow
	{
		// Token: 0x06000263 RID: 611
		[MethodImpl(4224, MethodCodeType = 3)]
		int Show([In] IntPtr parent);

		// Token: 0x06000264 RID: 612
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetFileTypes([In] uint cFileTypes, [In] ref NativeMethods.COMDLG_FILTERSPEC rgFilterSpec);

		// Token: 0x06000265 RID: 613
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetFileTypeIndex([In] uint iFileType);

		// Token: 0x06000266 RID: 614
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetFileTypeIndex(out uint piFileType);

		// Token: 0x06000267 RID: 615
		[MethodImpl(4096, MethodCodeType = 3)]
		void Advise([MarshalAs(28)] [In] IFileDialogEvents pfde, out uint pdwCookie);

		// Token: 0x06000268 RID: 616
		[MethodImpl(4096, MethodCodeType = 3)]
		void Unadvise([In] uint dwCookie);

		// Token: 0x06000269 RID: 617
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetOptions([In] NativeMethods.FOS fos);

		// Token: 0x0600026A RID: 618
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetOptions(out NativeMethods.FOS pfos);

		// Token: 0x0600026B RID: 619
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetDefaultFolder([MarshalAs(28)] [In] IShellItem psi);

		// Token: 0x0600026C RID: 620
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetFolder([MarshalAs(28)] [In] IShellItem psi);

		// Token: 0x0600026D RID: 621
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetFolder([MarshalAs(28)] out IShellItem ppsi);

		// Token: 0x0600026E RID: 622
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetCurrentSelection([MarshalAs(28)] out IShellItem ppsi);

		// Token: 0x0600026F RID: 623
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetFileName([MarshalAs(21)] [In] string pszName);

		// Token: 0x06000270 RID: 624
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetFileName([MarshalAs(21)] out string pszName);

		// Token: 0x06000271 RID: 625
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetTitle([MarshalAs(21)] [In] string pszTitle);

		// Token: 0x06000272 RID: 626
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetOkButtonLabel([MarshalAs(21)] [In] string pszText);

		// Token: 0x06000273 RID: 627
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetFileNameLabel([MarshalAs(21)] [In] string pszLabel);

		// Token: 0x06000274 RID: 628
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetResult([MarshalAs(28)] out IShellItem ppsi);

		// Token: 0x06000275 RID: 629
		[MethodImpl(4096, MethodCodeType = 3)]
		void AddPlace([MarshalAs(28)] [In] IShellItem psi, NativeMethods.FDAP fdap);

		// Token: 0x06000276 RID: 630
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetDefaultExtension([MarshalAs(21)] [In] string pszDefaultExtension);

		// Token: 0x06000277 RID: 631
		[MethodImpl(4096, MethodCodeType = 3)]
		void Close([MarshalAs(45)] int hr);

		// Token: 0x06000278 RID: 632
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetClientGuid([In] ref Guid guid);

		// Token: 0x06000279 RID: 633
		[MethodImpl(4096, MethodCodeType = 3)]
		void ClearClientData();

		// Token: 0x0600027A RID: 634
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetFilter([MarshalAs(28)] IntPtr pFilter);

		// Token: 0x0600027B RID: 635
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetResults([MarshalAs(28)] out IShellItemArray ppenum);

		// Token: 0x0600027C RID: 636
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetSelectedItems([MarshalAs(28)] out IShellItemArray ppsai);
	}
}
