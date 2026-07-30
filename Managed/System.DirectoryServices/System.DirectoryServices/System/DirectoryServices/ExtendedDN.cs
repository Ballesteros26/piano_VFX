using System;

namespace System.DirectoryServices
{
	/// <summary>The <see cref="T:System.DirectoryServices.ExtendedDN" /> enumeration specifies the format in which to return the extended distinguished name. This enumeration is used with the <see cref="P:System.DirectoryServices.DirectorySearcher.ExtendedDN" /> property.</summary>
	// Token: 0x02000020 RID: 32
	public enum ExtendedDN
	{
		/// <summary>Indicates that the distinguished name uses the distinguished name format.</summary>
		// Token: 0x0400008F RID: 143
		None = -1,
		/// <summary>Indicates that the distinguished name uses the hexadecimal format.</summary>
		// Token: 0x04000090 RID: 144
		HexString,
		/// <summary>Indicates that the distinguished name uses the standard string format.</summary>
		// Token: 0x04000091 RID: 145
		Standard
	}
}
