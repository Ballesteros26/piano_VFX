using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Contains attributes of a UCOMITypeInfo.</summary>
	// Token: 0x0200098C RID: 2444
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPEATTR
	{
		/// <summary>A constant used with the <see cref="F:System.Runtime.InteropServices.TYPEATTR.memidConstructor" /> and <see cref="F:System.Runtime.InteropServices.TYPEATTR.memidDestructor" /> fields.</summary>
		// Token: 0x04002E65 RID: 11877
		public const int MEMBER_ID_NIL = -1;

		/// <summary>The GUID of the type information.</summary>
		// Token: 0x04002E66 RID: 11878
		public Guid guid;

		/// <summary>Locale of member names and documentation strings.</summary>
		// Token: 0x04002E67 RID: 11879
		public int lcid;

		/// <summary>Reserved for future use.</summary>
		// Token: 0x04002E68 RID: 11880
		public int dwReserved;

		/// <summary>ID of constructor, or <see cref="F:System.Runtime.InteropServices.TYPEATTR.MEMBER_ID_NIL" /> if none.</summary>
		// Token: 0x04002E69 RID: 11881
		public int memidConstructor;

		/// <summary>ID of destructor, or <see cref="F:System.Runtime.InteropServices.TYPEATTR.MEMBER_ID_NIL" /> if none.</summary>
		// Token: 0x04002E6A RID: 11882
		public int memidDestructor;

		/// <summary>Reserved for future use.</summary>
		// Token: 0x04002E6B RID: 11883
		public IntPtr lpstrSchema;

		/// <summary>The size of an instance of this type.</summary>
		// Token: 0x04002E6C RID: 11884
		public int cbSizeInstance;

		/// <summary>A <see cref="T:System.Runtime.InteropServices.TYPEKIND" /> value describing the type this information describes.</summary>
		// Token: 0x04002E6D RID: 11885
		public TYPEKIND typekind;

		/// <summary>Indicates the number of functions on the interface this structure describes.</summary>
		// Token: 0x04002E6E RID: 11886
		public short cFuncs;

		/// <summary>Indicates the number of variables and data fields on the interface described by this structure.</summary>
		// Token: 0x04002E6F RID: 11887
		public short cVars;

		/// <summary>Indicates the number of implemented interfaces on the interface this structure describes.</summary>
		// Token: 0x04002E70 RID: 11888
		public short cImplTypes;

		/// <summary>The size of this type's virtual method table (VTBL).</summary>
		// Token: 0x04002E71 RID: 11889
		public short cbSizeVft;

		/// <summary>Specifies the byte alignment for an instance of this type.</summary>
		// Token: 0x04002E72 RID: 11890
		public short cbAlignment;

		/// <summary>A <see cref="T:System.Runtime.InteropServices.TYPEFLAGS" /> value describing this information.</summary>
		// Token: 0x04002E73 RID: 11891
		public TYPEFLAGS wTypeFlags;

		/// <summary>Major version number.</summary>
		// Token: 0x04002E74 RID: 11892
		public short wMajorVerNum;

		/// <summary>Minor version number.</summary>
		// Token: 0x04002E75 RID: 11893
		public short wMinorVerNum;

		/// <summary>If <see cref="F:System.Runtime.InteropServices.TYPEATTR.typekind" /> == <see cref="F:System.Runtime.InteropServices.TYPEKIND.TKIND_ALIAS" />, specifies the type for which this type is an alias.</summary>
		// Token: 0x04002E76 RID: 11894
		public TYPEDESC tdescAlias;

		/// <summary>IDL attributes of the described type.</summary>
		// Token: 0x04002E77 RID: 11895
		public IDLDESC idldescType;
	}
}
