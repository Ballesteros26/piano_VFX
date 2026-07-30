using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Defines the kind of variable.</summary>
	// Token: 0x02000995 RID: 2453
	[Serializable]
	public enum VARKIND
	{
		/// <summary>The variable is a field or member of the type. It exists at a fixed offset within each instance of the type.</summary>
		// Token: 0x04002E9E RID: 11934
		VAR_PERINSTANCE,
		/// <summary>There is only one instance of the variable.</summary>
		// Token: 0x04002E9F RID: 11935
		VAR_STATIC,
		/// <summary>The VARDESC structure describes a symbolic constant. There is no memory associated with it.</summary>
		// Token: 0x04002EA0 RID: 11936
		VAR_CONST,
		/// <summary>The variable can be accessed only through IDispatch::Invoke.</summary>
		// Token: 0x04002EA1 RID: 11937
		VAR_DISPATCH
	}
}
