using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Identifies how to expose an interface to COM.</summary>
	// Token: 0x020008A7 RID: 2215
	[ComVisible(true)]
	[Serializable]
	public enum ComInterfaceType
	{
		/// <summary>Indicates that the interface is exposed to COM as a dual interface, which enables both early and late binding. <see cref="F:System.Runtime.InteropServices.ComInterfaceType.InterfaceIsDual" /> is the default value.</summary>
		// Token: 0x04002BF7 RID: 11255
		InterfaceIsDual,
		/// <summary>Indicates that an interface is exposed to COM as an interface that is derived from IUnknown, which enables only early binding.</summary>
		// Token: 0x04002BF8 RID: 11256
		InterfaceIsIUnknown,
		/// <summary>Indicates that an interface is exposed to COM as a dispinterface, which enables late binding only.</summary>
		// Token: 0x04002BF9 RID: 11257
		InterfaceIsIDispatch,
		/// <summary>Indicates that an interface is exposed to COM as a Windows Runtime interface. </summary>
		// Token: 0x04002BFA RID: 11258
		[ComVisible(false)]
		InterfaceIsIInspectable
	}
}
