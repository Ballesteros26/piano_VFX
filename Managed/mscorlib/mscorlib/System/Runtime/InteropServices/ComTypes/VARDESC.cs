using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Describes a variable, constant, or data member.</summary>
	// Token: 0x02000996 RID: 2454
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct VARDESC
	{
		/// <summary>Indicates the member ID of a variable.</summary>
		// Token: 0x04002EA2 RID: 11938
		public int memid;

		/// <summary>This field is reserved for future use.</summary>
		// Token: 0x04002EA3 RID: 11939
		public string lpstrSchema;

		/// <summary>Contains information about a variable.</summary>
		// Token: 0x04002EA4 RID: 11940
		public VARDESC.DESCUNION desc;

		/// <summary>Contains the variable type.</summary>
		// Token: 0x04002EA5 RID: 11941
		public ELEMDESC elemdescVar;

		/// <summary>Defines the properties of a variable.</summary>
		// Token: 0x04002EA6 RID: 11942
		public short wVarFlags;

		/// <summary>Defines how to marshal a variable.</summary>
		// Token: 0x04002EA7 RID: 11943
		public VARKIND varkind;

		/// <summary>Contains information about a variable.</summary>
		// Token: 0x02000997 RID: 2455
		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		public struct DESCUNION
		{
			/// <summary>Indicates the offset of this variable within the instance.</summary>
			// Token: 0x04002EA8 RID: 11944
			[FieldOffset(0)]
			public int oInst;

			/// <summary>Describes a symbolic constant.</summary>
			// Token: 0x04002EA9 RID: 11945
			[FieldOffset(0)]
			public IntPtr lpvarValue;
		}
	}
}
