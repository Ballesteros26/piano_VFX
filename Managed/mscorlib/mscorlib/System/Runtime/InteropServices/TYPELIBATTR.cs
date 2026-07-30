using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.TYPELIBATTR" /> instead.</summary>
	// Token: 0x02000927 RID: 2343
	[Obsolete]
	[Serializable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPELIBATTR
	{
		/// <summary>Represents a globally unique library ID of a type library.</summary>
		// Token: 0x04002DE7 RID: 11751
		public Guid guid;

		/// <summary>Represents a locale ID of a type library.</summary>
		// Token: 0x04002DE8 RID: 11752
		public int lcid;

		/// <summary>Represents the target hardware platform of a type library.</summary>
		// Token: 0x04002DE9 RID: 11753
		public SYSKIND syskind;

		/// <summary>Represents the major version number of a type library.</summary>
		// Token: 0x04002DEA RID: 11754
		public short wMajorVerNum;

		/// <summary>Represents the minor version number of a type library.</summary>
		// Token: 0x04002DEB RID: 11755
		public short wMinorVerNum;

		/// <summary>Represents library flags.</summary>
		// Token: 0x04002DEC RID: 11756
		public LIBFLAGS wLibFlags;
	}
}
