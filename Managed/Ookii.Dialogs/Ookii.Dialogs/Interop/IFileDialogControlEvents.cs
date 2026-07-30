using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x0200003D RID: 61
	[Guid("36116642-D713-4b97-9B83-7484A9D00433")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IFileDialogControlEvents
	{
		// Token: 0x060002DA RID: 730
		[MethodImpl(4096, MethodCodeType = 3)]
		void OnItemSelected([MarshalAs(28)] [In] IFileDialogCustomize pfdc, [In] int dwIDCtl, [In] int dwIDItem);

		// Token: 0x060002DB RID: 731
		[MethodImpl(4096, MethodCodeType = 3)]
		void OnButtonClicked([MarshalAs(28)] [In] IFileDialogCustomize pfdc, [In] int dwIDCtl);

		// Token: 0x060002DC RID: 732
		[MethodImpl(4096, MethodCodeType = 3)]
		void OnCheckButtonToggled([MarshalAs(28)] [In] IFileDialogCustomize pfdc, [In] int dwIDCtl, [In] bool bChecked);

		// Token: 0x060002DD RID: 733
		[MethodImpl(4096, MethodCodeType = 3)]
		void OnControlActivating([MarshalAs(28)] [In] IFileDialogCustomize pfdc, [In] int dwIDCtl);
	}
}
