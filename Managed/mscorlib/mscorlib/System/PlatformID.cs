using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Identifies the operating system, or platform, supported by an assembly.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000229 RID: 553
	[ComVisible(true)]
	[Serializable]
	public enum PlatformID
	{
		/// <summary>The operating system is Win32s. Win32s is a layer that runs on 16-bit versions of Windows to provide access to 32-bit applications.</summary>
		// Token: 0x04000D13 RID: 3347
		Win32S,
		/// <summary>The operating system is Windows 95 or Windows 98.</summary>
		// Token: 0x04000D14 RID: 3348
		Win32Windows,
		/// <summary>The operating system is Windows NT or later.</summary>
		// Token: 0x04000D15 RID: 3349
		Win32NT,
		/// <summary>The operating system is Windows CE.</summary>
		// Token: 0x04000D16 RID: 3350
		WinCE,
		/// <summary>The operating system is Unix.</summary>
		// Token: 0x04000D17 RID: 3351
		Unix,
		/// <summary>The development platform is Xbox 360.</summary>
		// Token: 0x04000D18 RID: 3352
		Xbox,
		/// <summary>The operating system is Macintosh.</summary>
		// Token: 0x04000D19 RID: 3353
		MacOSX
	}
}
