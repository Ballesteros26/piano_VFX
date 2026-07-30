using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Lifetime
{
	/// <summary>Indicates the possible lease states of a lifetime lease.</summary>
	// Token: 0x02000778 RID: 1912
	[ComVisible(true)]
	[Serializable]
	public enum LeaseState
	{
		/// <summary>The lease is not initialized.</summary>
		// Token: 0x04002A0B RID: 10763
		Null,
		/// <summary>The lease has been created, but is not yet active.</summary>
		// Token: 0x04002A0C RID: 10764
		Initial,
		/// <summary>The lease is active and has not expired.</summary>
		// Token: 0x04002A0D RID: 10765
		Active,
		/// <summary>The lease has expired and is seeking sponsorship.</summary>
		// Token: 0x04002A0E RID: 10766
		Renewing,
		/// <summary>The lease has expired and cannot be renewed.</summary>
		// Token: 0x04002A0F RID: 10767
		Expired
	}
}
