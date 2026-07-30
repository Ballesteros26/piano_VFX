using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	/// <summary>Specifies the type of a managed code policy level.</summary>
	// Token: 0x02000547 RID: 1351
	[ComVisible(true)]
	[Serializable]
	public enum PolicyLevelType
	{
		/// <summary>Security policy for all managed code that is run by the user.</summary>
		// Token: 0x04001F58 RID: 8024
		User,
		/// <summary>Security policy for all managed code that is run on the computer.</summary>
		// Token: 0x04001F59 RID: 8025
		Machine,
		/// <summary>Security policy for all managed code in an enterprise.</summary>
		// Token: 0x04001F5A RID: 8026
		Enterprise,
		/// <summary>Security policy for all managed code in an application.</summary>
		// Token: 0x04001F5B RID: 8027
		AppDomain
	}
}
