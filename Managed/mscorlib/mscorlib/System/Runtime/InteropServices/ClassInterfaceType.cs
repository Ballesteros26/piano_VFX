using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Identifies the type of class interface that is generated for a class.</summary>
	// Token: 0x020008AA RID: 2218
	[ComVisible(true)]
	[Serializable]
	public enum ClassInterfaceType
	{
		/// <summary>Indicates that no class interface is generated for the class. If no interfaces are implemented explicitly, the class can only provide late-bound access through the IDispatch interface. This is the recommended setting for <see cref="T:System.Runtime.InteropServices.ClassInterfaceAttribute" />. Using ClassInterfaceType.None is the only way to expose functionality through interfaces implemented explicitly by the class.</summary>
		// Token: 0x04002BFE RID: 11262
		None,
		/// <summary>Indicates that the class only supports late binding for COM clients. A dispinterface for the class is automatically exposed to COM clients on request. The type library produced by Tlbexp.exe (Type Library Exporter) does not contain type information for the dispinterface in order to prevent clients from caching the DISPIDs of the interface. The dispinterface does not exhibit the versioning problems described in <see cref="T:System.Runtime.InteropServices.ClassInterfaceAttribute" /> because clients can only late-bind to the interface.</summary>
		// Token: 0x04002BFF RID: 11263
		AutoDispatch,
		/// <summary>Indicates that a dual class interface is automatically generated for the class and exposed to COM. Type information is produced for the class interface and published in the type library. Using AutoDual is strongly discouraged because of the versioning limitations described in <see cref="T:System.Runtime.InteropServices.ClassInterfaceAttribute" />.</summary>
		// Token: 0x04002C00 RID: 11264
		AutoDual
	}
}
