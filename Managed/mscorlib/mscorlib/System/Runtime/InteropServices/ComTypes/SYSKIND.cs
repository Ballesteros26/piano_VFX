using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Identifies the target operating system platform.</summary>
	// Token: 0x020009A1 RID: 2465
	[Serializable]
	public enum SYSKIND
	{
		/// <summary>The target operating system for the type library is 16-bit Windows systems. By default, data fields are packed.</summary>
		// Token: 0x04002EEA RID: 12010
		SYS_WIN16,
		/// <summary>The target operating system for the type library is 32-bit Windows systems. By default, data fields are naturally aligned (for example, 2-byte integers are aligned on even-byte boundaries; 4-byte integers are aligned on quad-word boundaries, and so on).</summary>
		// Token: 0x04002EEB RID: 12011
		SYS_WIN32,
		/// <summary>The target operating system for the type library is Apple Macintosh. By default, all data fields are aligned on even-byte boundaries.</summary>
		// Token: 0x04002EEC RID: 12012
		SYS_MAC,
		/// <summary>The target operating system for the type library is 64-bit Windows systems.</summary>
		// Token: 0x04002EED RID: 12013
		SYS_WIN64
	}
}
