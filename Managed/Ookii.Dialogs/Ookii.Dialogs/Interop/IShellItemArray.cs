using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x02000039 RID: 57
	[Guid("B63EA76D-1F85-456F-A19C-48159EFA858B")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IShellItemArray
	{
		// Token: 0x060002A6 RID: 678
		[MethodImpl(4096, MethodCodeType = 3)]
		void BindToHandler([MarshalAs(28)] [In] IntPtr pbc, [In] ref Guid rbhid, [In] ref Guid riid, out IntPtr ppvOut);

		// Token: 0x060002A7 RID: 679
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetPropertyStore([In] int Flags, [In] ref Guid riid, out IntPtr ppv);

		// Token: 0x060002A8 RID: 680
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetPropertyDescriptionList([In] ref NativeMethods.PROPERTYKEY keyType, [In] ref Guid riid, out IntPtr ppv);

		// Token: 0x060002A9 RID: 681
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetAttributes([In] NativeMethods.SIATTRIBFLAGS dwAttribFlags, [In] uint sfgaoMask, out uint psfgaoAttribs);

		// Token: 0x060002AA RID: 682
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetCount(out uint pdwNumItems);

		// Token: 0x060002AB RID: 683
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetItemAt([In] uint dwIndex, [MarshalAs(28)] out IShellItem ppsi);

		// Token: 0x060002AC RID: 684
		[MethodImpl(4096, MethodCodeType = 3)]
		void EnumItems([MarshalAs(28)] out IntPtr ppenumShellItems);
	}
}
