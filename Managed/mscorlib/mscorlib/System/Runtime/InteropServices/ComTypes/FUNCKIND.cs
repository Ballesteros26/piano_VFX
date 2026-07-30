using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Defines how to access a function.</summary>
	// Token: 0x0200099A RID: 2458
	[Serializable]
	public enum FUNCKIND
	{
		/// <summary>The function is accessed in the same way as <see cref="F:System.Runtime.InteropServices.FUNCKIND.FUNC_PUREVIRTUAL" />, except the function has an implementation.</summary>
		// Token: 0x04002EB8 RID: 11960
		FUNC_VIRTUAL,
		/// <summary>The function is accessed through the virtual function table (VTBL), and takes an implicit this pointer.</summary>
		// Token: 0x04002EB9 RID: 11961
		FUNC_PUREVIRTUAL,
		/// <summary>The function is accessed by static address and takes an implicit this pointer.</summary>
		// Token: 0x04002EBA RID: 11962
		FUNC_NONVIRTUAL,
		/// <summary>The function is accessed by static address and does not take an implicit this pointer.</summary>
		// Token: 0x04002EBB RID: 11963
		FUNC_STATIC,
		/// <summary>The function can be accessed only through IDispatch.</summary>
		// Token: 0x04002EBC RID: 11964
		FUNC_DISPATCH
	}
}
