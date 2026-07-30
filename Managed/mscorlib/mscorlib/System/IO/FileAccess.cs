using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	/// <summary>Defines constants for read, write, or read/write access to a file.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003D2 RID: 978
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum FileAccess
	{
		/// <summary>Read access to the file. Data can be read from the file. Combine with Write for read/write access.</summary>
		// Token: 0x040017C9 RID: 6089
		Read = 1,
		/// <summary>Write access to the file. Data can be written to the file. Combine with Read for read/write access.</summary>
		// Token: 0x040017CA RID: 6090
		Write = 2,
		/// <summary>Read and write access to the file. Data can be written to and read from the file.</summary>
		// Token: 0x040017CB RID: 6091
		ReadWrite = 3
	}
}
