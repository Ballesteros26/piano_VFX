using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	/// <summary>Specifies the position in a stream to use for seeking.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003E3 RID: 995
	[ComVisible(true)]
	[Serializable]
	public enum SeekOrigin
	{
		/// <summary>Specifies the beginning of a stream.</summary>
		// Token: 0x0400184D RID: 6221
		Begin,
		/// <summary>Specifies the current position within a stream.</summary>
		// Token: 0x0400184E RID: 6222
		Current,
		/// <summary>Specifies the end of a stream.</summary>
		// Token: 0x0400184F RID: 6223
		End
	}
}
