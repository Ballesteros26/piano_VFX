using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.FUNCDESC" /> instead.</summary>
	// Token: 0x020008F8 RID: 2296
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.FUNCDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	public struct FUNCDESC
	{
		/// <summary>Identifies the function member ID.</summary>
		// Token: 0x04002D19 RID: 11545
		public int memid;

		/// <summary>Stores the count of errors a function can return on a 16-bit system.</summary>
		// Token: 0x04002D1A RID: 11546
		public IntPtr lprgscode;

		/// <summary>Indicates the size of <see cref="F:System.Runtime.InteropServices.FUNCDESC.cParams" />.</summary>
		// Token: 0x04002D1B RID: 11547
		public IntPtr lprgelemdescParam;

		/// <summary>Specifies whether the function is virtual, static, or dispatch-only.</summary>
		// Token: 0x04002D1C RID: 11548
		public FUNCKIND funckind;

		/// <summary>Specifies the type of a property function.</summary>
		// Token: 0x04002D1D RID: 11549
		public INVOKEKIND invkind;

		/// <summary>Specifies the calling convention of a function.</summary>
		// Token: 0x04002D1E RID: 11550
		public CALLCONV callconv;

		/// <summary>Counts the total number of parameters.</summary>
		// Token: 0x04002D1F RID: 11551
		public short cParams;

		/// <summary>Counts the optional parameters.</summary>
		// Token: 0x04002D20 RID: 11552
		public short cParamsOpt;

		/// <summary>Specifies the offset in the VTBL for <see cref="F:System.Runtime.InteropServices.FUNCKIND.FUNC_VIRTUAL" />.</summary>
		// Token: 0x04002D21 RID: 11553
		public short oVft;

		/// <summary>Counts the permitted return values.</summary>
		// Token: 0x04002D22 RID: 11554
		public short cScodes;

		/// <summary>Contains the return type of the function.</summary>
		// Token: 0x04002D23 RID: 11555
		public ELEMDESC elemdescFunc;

		/// <summary>Indicates the <see cref="T:System.Runtime.InteropServices.FUNCFLAGS" /> of a function.</summary>
		// Token: 0x04002D24 RID: 11556
		public short wFuncFlags;
	}
}
