using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Specifies whether applicable <see cref="Overload:System.String.Split" /> method overloads include or omit empty substrings from the return value.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001C3 RID: 451
	[Flags]
	[ComVisible(false)]
	public enum StringSplitOptions
	{
		/// <summary>The return value includes array elements that contain an empty string</summary>
		// Token: 0x04000AE4 RID: 2788
		None = 0,
		/// <summary>The return value does not include array elements that contain an empty string</summary>
		// Token: 0x04000AE5 RID: 2789
		RemoveEmptyEntries = 1
	}
}
