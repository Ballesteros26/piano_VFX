using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x02000037 RID: 55
	[Guid("973510DB-7D7F-452B-8975-74A85828D354")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IFileDialogEvents
	{
		// Token: 0x0600029A RID: 666
		[MethodImpl(4224, MethodCodeType = 3)]
		HRESULT OnFileOk([MarshalAs(28)] [In] IFileDialog pfd);

		// Token: 0x0600029B RID: 667
		[MethodImpl(4224, MethodCodeType = 3)]
		HRESULT OnFolderChanging([MarshalAs(28)] [In] IFileDialog pfd, [MarshalAs(28)] [In] IShellItem psiFolder);

		// Token: 0x0600029C RID: 668
		[MethodImpl(4096, MethodCodeType = 3)]
		void OnFolderChange([MarshalAs(28)] [In] IFileDialog pfd);

		// Token: 0x0600029D RID: 669
		[MethodImpl(4096, MethodCodeType = 3)]
		void OnSelectionChange([MarshalAs(28)] [In] IFileDialog pfd);

		// Token: 0x0600029E RID: 670
		[MethodImpl(4096, MethodCodeType = 3)]
		void OnShareViolation([MarshalAs(28)] [In] IFileDialog pfd, [MarshalAs(28)] [In] IShellItem psi);

		// Token: 0x0600029F RID: 671
		[MethodImpl(4096, MethodCodeType = 3)]
		void OnTypeChange([MarshalAs(28)] [In] IFileDialog pfd);

		// Token: 0x060002A0 RID: 672
		[MethodImpl(4096, MethodCodeType = 3)]
		void OnOverwrite([MarshalAs(28)] [In] IFileDialog pfd, [MarshalAs(28)] [In] IShellItem psi);
	}
}
