using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>Indicates the mode in which a <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> is operating.</summary>
	// Token: 0x02000059 RID: 89
	public enum ForestMode
	{
		/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> is operating in Windows 2000 mode.</summary>
		// Token: 0x040000F7 RID: 247
		Windows2000Forest,
		/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> is operating in Windows Server 2003 domain-function mode.</summary>
		// Token: 0x040000F8 RID: 248
		Windows2003InterimForest,
		/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> is operating in Windows Server 2003 mode.</summary>
		// Token: 0x040000F9 RID: 249
		Windows2003Forest,
		/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> is operating in  mode.</summary>
		// Token: 0x040000FA RID: 250
		Windows2008Forest,
		/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> is operating in Windows 2008R2 mode.</summary>
		// Token: 0x040000FB RID: 251
		Windows2008R2Forest,
		// Token: 0x040000FC RID: 252
		Unknown = -1,
		/// <summary />
		// Token: 0x040000FD RID: 253
		Windows2012R2Forest = 6,
		/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> is operating in Windows 8 mode.</summary>
		// Token: 0x040000FE RID: 254
		Windows8Forest = 5
	}
}
