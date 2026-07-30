using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.FUNCKIND" /> instead.</summary>
	// Token: 0x02000904 RID: 2308
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.FUNCKIND instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Serializable]
	public enum FUNCKIND
	{
		/// <summary>The function is accessed the same as <see cref="F:System.Runtime.InteropServices.FUNCKIND.FUNC_PUREVIRTUAL" />, except the function has an implementation.</summary>
		// Token: 0x04002D52 RID: 11602
		FUNC_VIRTUAL,
		/// <summary>The function is accessed through the virtual function table (VTBL), and takes an implicit this pointer.</summary>
		// Token: 0x04002D53 RID: 11603
		FUNC_PUREVIRTUAL,
		/// <summary>The function is accessed by static address and takes an implicit this pointer.</summary>
		// Token: 0x04002D54 RID: 11604
		FUNC_NONVIRTUAL,
		/// <summary>The function is accessed by static address and does not take an implicit this pointer.</summary>
		// Token: 0x04002D55 RID: 11605
		FUNC_STATIC,
		/// <summary>The function can be accessed only through IDispatch.</summary>
		// Token: 0x04002D56 RID: 11606
		FUNC_DISPATCH
	}
}
