using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x0200003E RID: 62
	[Guid("886D8EEB-8CF2-4446-8D02-CDBA1DBDCF99")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IPropertyStore
	{
		// Token: 0x060002DE RID: 734
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetCount(out uint cProps);

		// Token: 0x060002DF RID: 735
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetAt([In] uint iProp, out NativeMethods.PROPERTYKEY pkey);

		// Token: 0x060002E0 RID: 736
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetValue([In] ref NativeMethods.PROPERTYKEY key, out object pv);

		// Token: 0x060002E1 RID: 737
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetValue([In] ref NativeMethods.PROPERTYKEY key, [In] ref object pv);

		// Token: 0x060002E2 RID: 738
		[MethodImpl(4096, MethodCodeType = 3)]
		void Commit();
	}
}
