using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Dictates which character set marshaled strings should use.</summary>
	// Token: 0x020008D7 RID: 2263
	[ComVisible(true)]
	[Serializable]
	public enum CharSet
	{
		/// <summary>This value is obsolete and has the same behavior as <see cref="F:System.Runtime.InteropServices.CharSet.Ansi" />.</summary>
		// Token: 0x04002CC2 RID: 11458
		None = 1,
		/// <summary>Marshal strings as multiple-byte character strings.</summary>
		// Token: 0x04002CC3 RID: 11459
		Ansi,
		/// <summary>Marshal strings as Unicode 2-byte characters.</summary>
		// Token: 0x04002CC4 RID: 11460
		Unicode,
		/// <summary>Automatically marshal strings appropriately for the target operating system. The default is <see cref="F:System.Runtime.InteropServices.CharSet.Unicode" /> on Windows NT, Windows 2000, Windows XP, and the Windows Server 2003 family; the default is <see cref="F:System.Runtime.InteropServices.CharSet.Ansi" /> on Windows 98 and Windows Me. Although the common language runtime default is <see cref="F:System.Runtime.InteropServices.CharSet.Auto" />, languages may override this default. For example, by default C# marks all methods and types as <see cref="F:System.Runtime.InteropServices.CharSet.Ansi" />.</summary>
		// Token: 0x04002CC5 RID: 11461
		Auto
	}
}
