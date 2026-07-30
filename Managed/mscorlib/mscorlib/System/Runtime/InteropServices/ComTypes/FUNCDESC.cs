using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Defines a function description.</summary>
	// Token: 0x0200098D RID: 2445
	public struct FUNCDESC
	{
		/// <summary>Identifies the function member ID.</summary>
		// Token: 0x04002E78 RID: 11896
		public int memid;

		/// <summary>Stores the count of errors a function can return on a 16-bit system.</summary>
		// Token: 0x04002E79 RID: 11897
		public IntPtr lprgscode;

		/// <summary>Indicates the size of <see cref="F:System.Runtime.InteropServices.FUNCDESC.cParams" />.</summary>
		// Token: 0x04002E7A RID: 11898
		public IntPtr lprgelemdescParam;

		/// <summary>Specifies whether the function is virtual, static, or dispatch-only.</summary>
		// Token: 0x04002E7B RID: 11899
		public FUNCKIND funckind;

		/// <summary>Specifies the type of a property function.</summary>
		// Token: 0x04002E7C RID: 11900
		public INVOKEKIND invkind;

		/// <summary>Specifies the calling convention of a function.</summary>
		// Token: 0x04002E7D RID: 11901
		public CALLCONV callconv;

		/// <summary>Counts the total number of parameters.</summary>
		// Token: 0x04002E7E RID: 11902
		public short cParams;

		/// <summary>Counts the optional parameters.</summary>
		// Token: 0x04002E7F RID: 11903
		public short cParamsOpt;

		/// <summary>Specifies the offset in the VTBL for <see cref="F:System.Runtime.InteropServices.FUNCKIND.FUNC_VIRTUAL" />.</summary>
		// Token: 0x04002E80 RID: 11904
		public short oVft;

		/// <summary>Counts the permitted return values.</summary>
		// Token: 0x04002E81 RID: 11905
		public short cScodes;

		/// <summary>Contains the return type of the function.</summary>
		// Token: 0x04002E82 RID: 11906
		public ELEMDESC elemdescFunc;

		/// <summary>Indicates the <see cref="T:System.Runtime.InteropServices.FUNCFLAGS" /> of a function.</summary>
		// Token: 0x04002E83 RID: 11907
		public short wFuncFlags;
	}
}
