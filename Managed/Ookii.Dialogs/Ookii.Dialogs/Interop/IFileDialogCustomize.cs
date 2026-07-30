using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x0200003C RID: 60
	[Guid("e6fdd21a-163f-4975-9c8c-a69f1ba37034")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IFileDialogCustomize
	{
		// Token: 0x060002C0 RID: 704
		[MethodImpl(4096, MethodCodeType = 3)]
		void EnableOpenDropDown([In] int dwIDCtl);

		// Token: 0x060002C1 RID: 705
		[MethodImpl(4096, MethodCodeType = 3)]
		void AddMenu([In] int dwIDCtl, [MarshalAs(21)] [In] string pszLabel);

		// Token: 0x060002C2 RID: 706
		[MethodImpl(4096, MethodCodeType = 3)]
		void AddPushButton([In] int dwIDCtl, [MarshalAs(21)] [In] string pszLabel);

		// Token: 0x060002C3 RID: 707
		[MethodImpl(4096, MethodCodeType = 3)]
		void AddComboBox([In] int dwIDCtl);

		// Token: 0x060002C4 RID: 708
		[MethodImpl(4096, MethodCodeType = 3)]
		void AddRadioButtonList([In] int dwIDCtl);

		// Token: 0x060002C5 RID: 709
		[MethodImpl(4096, MethodCodeType = 3)]
		void AddCheckButton([In] int dwIDCtl, [MarshalAs(21)] [In] string pszLabel, [In] bool bChecked);

		// Token: 0x060002C6 RID: 710
		[MethodImpl(4096, MethodCodeType = 3)]
		void AddEditBox([In] int dwIDCtl, [MarshalAs(21)] [In] string pszText);

		// Token: 0x060002C7 RID: 711
		[MethodImpl(4096, MethodCodeType = 3)]
		void AddSeparator([In] int dwIDCtl);

		// Token: 0x060002C8 RID: 712
		[MethodImpl(4096, MethodCodeType = 3)]
		void AddText([In] int dwIDCtl, [MarshalAs(21)] [In] string pszText);

		// Token: 0x060002C9 RID: 713
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetControlLabel([In] int dwIDCtl, [MarshalAs(21)] [In] string pszLabel);

		// Token: 0x060002CA RID: 714
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetControlState([In] int dwIDCtl, out NativeMethods.CDCONTROLSTATE pdwState);

		// Token: 0x060002CB RID: 715
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetControlState([In] int dwIDCtl, [In] NativeMethods.CDCONTROLSTATE dwState);

		// Token: 0x060002CC RID: 716
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetEditBoxText([In] int dwIDCtl, [Out] IntPtr ppszText);

		// Token: 0x060002CD RID: 717
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetEditBoxText([In] int dwIDCtl, [MarshalAs(21)] [In] string pszText);

		// Token: 0x060002CE RID: 718
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetCheckButtonState([In] int dwIDCtl, out bool pbChecked);

		// Token: 0x060002CF RID: 719
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetCheckButtonState([In] int dwIDCtl, [In] bool bChecked);

		// Token: 0x060002D0 RID: 720
		[MethodImpl(4096, MethodCodeType = 3)]
		void AddControlItem([In] int dwIDCtl, [In] int dwIDItem, [MarshalAs(21)] [In] string pszLabel);

		// Token: 0x060002D1 RID: 721
		[MethodImpl(4096, MethodCodeType = 3)]
		void RemoveControlItem([In] int dwIDCtl, [In] int dwIDItem);

		// Token: 0x060002D2 RID: 722
		[MethodImpl(4096, MethodCodeType = 3)]
		void RemoveAllControlItems([In] int dwIDCtl);

		// Token: 0x060002D3 RID: 723
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetControlItemState([In] int dwIDCtl, [In] int dwIDItem, out NativeMethods.CDCONTROLSTATE pdwState);

		// Token: 0x060002D4 RID: 724
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetControlItemState([In] int dwIDCtl, [In] int dwIDItem, [In] NativeMethods.CDCONTROLSTATE dwState);

		// Token: 0x060002D5 RID: 725
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetSelectedControlItem([In] int dwIDCtl, out int pdwIDItem);

		// Token: 0x060002D6 RID: 726
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetSelectedControlItem([In] int dwIDCtl, [In] int dwIDItem);

		// Token: 0x060002D7 RID: 727
		[MethodImpl(4096, MethodCodeType = 3)]
		void StartVisualGroup([In] int dwIDCtl, [MarshalAs(21)] [In] string pszLabel);

		// Token: 0x060002D8 RID: 728
		[MethodImpl(4096, MethodCodeType = 3)]
		void EndVisualGroup();

		// Token: 0x060002D9 RID: 729
		[MethodImpl(4096, MethodCodeType = 3)]
		void MakeProminent([In] int dwIDCtl);
	}
}
