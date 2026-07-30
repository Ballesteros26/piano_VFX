using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Specifies how to invoke a function by IDispatch::Invoke.</summary>
	// Token: 0x0200099B RID: 2459
	[Flags]
	[Serializable]
	public enum INVOKEKIND
	{
		/// <summary>The member is called using a normal function invocation syntax.</summary>
		// Token: 0x04002EBE RID: 11966
		INVOKE_FUNC = 1,
		/// <summary>The function is invoked using a normal property access syntax.</summary>
		// Token: 0x04002EBF RID: 11967
		INVOKE_PROPERTYGET = 2,
		/// <summary>The function is invoked using a property value assignment syntax.</summary>
		// Token: 0x04002EC0 RID: 11968
		INVOKE_PROPERTYPUT = 4,
		/// <summary>The function is invoked using a property reference assignment syntax.</summary>
		// Token: 0x04002EC1 RID: 11969
		INVOKE_PROPERTYPUTREF = 8
	}
}
