using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x02000033 RID: 51
	[Guid("b4db1657-70d7-485e-8e3e-6fcb5a5c1802")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IModalWindow
	{
		// Token: 0x0600024A RID: 586
		[MethodImpl(4224, MethodCodeType = 3)]
		int Show([In] IntPtr parent);
	}
}
