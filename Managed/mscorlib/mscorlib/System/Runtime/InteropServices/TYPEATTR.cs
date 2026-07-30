using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.TYPEATTR" /> instead.</summary>
	// Token: 0x020008F7 RID: 2295
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.TYPEATTR instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPEATTR
	{
		/// <summary>A constant used with the <see cref="F:System.Runtime.InteropServices.TYPEATTR.memidConstructor" /> and <see cref="F:System.Runtime.InteropServices.TYPEATTR.memidDestructor" /> fields.</summary>
		// Token: 0x04002D06 RID: 11526
		public const int MEMBER_ID_NIL = -1;

		/// <summary>The GUID of the type information.</summary>
		// Token: 0x04002D07 RID: 11527
		public Guid guid;

		/// <summary>Locale of member names and documentation strings.</summary>
		// Token: 0x04002D08 RID: 11528
		public int lcid;

		/// <summary>Reserved for future use.</summary>
		// Token: 0x04002D09 RID: 11529
		public int dwReserved;

		/// <summary>ID of constructor, or <see cref="F:System.Runtime.InteropServices.TYPEATTR.MEMBER_ID_NIL" /> if none.</summary>
		// Token: 0x04002D0A RID: 11530
		public int memidConstructor;

		/// <summary>ID of destructor, or <see cref="F:System.Runtime.InteropServices.TYPEATTR.MEMBER_ID_NIL" /> if none.</summary>
		// Token: 0x04002D0B RID: 11531
		public int memidDestructor;

		/// <summary>Reserved for future use.</summary>
		// Token: 0x04002D0C RID: 11532
		public IntPtr lpstrSchema;

		/// <summary>The size of an instance of this type.</summary>
		// Token: 0x04002D0D RID: 11533
		public int cbSizeInstance;

		/// <summary>A <see cref="T:System.Runtime.InteropServices.TYPEKIND" /> value describing the type this information describes.</summary>
		// Token: 0x04002D0E RID: 11534
		public TYPEKIND typekind;

		/// <summary>Indicates the number of functions on the interface this structure describes.</summary>
		// Token: 0x04002D0F RID: 11535
		public short cFuncs;

		/// <summary>Indicates the number of variables and data fields on the interface described by this structure.</summary>
		// Token: 0x04002D10 RID: 11536
		public short cVars;

		/// <summary>Indicates the number of implemented interfaces on the interface this structure describes.</summary>
		// Token: 0x04002D11 RID: 11537
		public short cImplTypes;

		/// <summary>The size of this type's virtual method table (VTBL).</summary>
		// Token: 0x04002D12 RID: 11538
		public short cbSizeVft;

		/// <summary>Specifies the byte alignment for an instance of this type.</summary>
		// Token: 0x04002D13 RID: 11539
		public short cbAlignment;

		/// <summary>A <see cref="T:System.Runtime.InteropServices.TYPEFLAGS" /> value describing this information.</summary>
		// Token: 0x04002D14 RID: 11540
		public TYPEFLAGS wTypeFlags;

		/// <summary>Major version number.</summary>
		// Token: 0x04002D15 RID: 11541
		public short wMajorVerNum;

		/// <summary>Minor version number.</summary>
		// Token: 0x04002D16 RID: 11542
		public short wMinorVerNum;

		/// <summary>If <see cref="F:System.Runtime.InteropServices.TYPEATTR.typekind" /> == <see cref="F:System.Runtime.InteropServices.TYPEKIND.TKIND_ALIAS" />, specifies the type for which this type is an alias.</summary>
		// Token: 0x04002D17 RID: 11543
		public TYPEDESC tdescAlias;

		/// <summary>IDL attributes of the described type.</summary>
		// Token: 0x04002D18 RID: 11544
		public IDLDESC idldescType;
	}
}
