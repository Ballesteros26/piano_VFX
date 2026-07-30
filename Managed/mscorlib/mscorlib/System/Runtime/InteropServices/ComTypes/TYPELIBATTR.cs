using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Identifies a particular type library and provides localization support for member names.</summary>
	// Token: 0x020009A3 RID: 2467
	[Serializable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPELIBATTR
	{
		/// <summary>Represents a globally unique library ID of a type library.</summary>
		// Token: 0x04002EF3 RID: 12019
		public Guid guid;

		/// <summary>Represents a locale ID of a type library.</summary>
		// Token: 0x04002EF4 RID: 12020
		public int lcid;

		/// <summary>Represents the target hardware platform of a type library.</summary>
		// Token: 0x04002EF5 RID: 12021
		public SYSKIND syskind;

		/// <summary>Represents the major version number of a type library.</summary>
		// Token: 0x04002EF6 RID: 12022
		public short wMajorVerNum;

		/// <summary>Represents the minor version number of a type library.</summary>
		// Token: 0x04002EF7 RID: 12023
		public short wMinorVerNum;

		/// <summary>Represents library flags.</summary>
		// Token: 0x04002EF8 RID: 12024
		public LIBFLAGS wLibFlags;
	}
}
