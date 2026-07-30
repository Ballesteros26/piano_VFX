using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	/// <summary>Specifies how custom errors are handled.</summary>
	// Token: 0x02000749 RID: 1865
	[ComVisible(true)]
	public enum CustomErrorsModes
	{
		/// <summary>All callers receive filtered exception information.</summary>
		// Token: 0x0400298D RID: 10637
		On,
		/// <summary>All callers receive complete exception information.</summary>
		// Token: 0x0400298E RID: 10638
		Off,
		/// <summary>Local callers receive complete exception information; remote callers receive filtered exception information.</summary>
		// Token: 0x0400298F RID: 10639
		RemoteOnly
	}
}
