using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.VARDESC" /> instead.</summary>
	// Token: 0x02000900 RID: 2304
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.VARDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct VARDESC
	{
		/// <summary>Indicates the member ID of a variable.</summary>
		// Token: 0x04002D3E RID: 11582
		public int memid;

		/// <summary>This field is reserved for future use.</summary>
		// Token: 0x04002D3F RID: 11583
		public string lpstrSchema;

		/// <summary>Contains the variable type.</summary>
		// Token: 0x04002D40 RID: 11584
		public ELEMDESC elemdescVar;

		/// <summary>Defines the properties of a variable.</summary>
		// Token: 0x04002D41 RID: 11585
		public short wVarFlags;

		/// <summary>Defines how a variable should be marshaled.</summary>
		// Token: 0x04002D42 RID: 11586
		public VarEnum varkind;

		/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.VARDESC.DESCUNION" /> instead.</summary>
		// Token: 0x02000901 RID: 2305
		[ComVisible(false)]
		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		public struct DESCUNION
		{
			/// <summary>Indicates the offset of this variable within the instance.</summary>
			// Token: 0x04002D43 RID: 11587
			[FieldOffset(0)]
			public int oInst;

			/// <summary>Describes a symbolic constant.</summary>
			// Token: 0x04002D44 RID: 11588
			[FieldOffset(0)]
			public IntPtr lpvarValue;
		}
	}
}
