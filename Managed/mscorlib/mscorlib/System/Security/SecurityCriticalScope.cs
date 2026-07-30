using System;

namespace System.Security
{
	/// <summary>Specifies the scope of a <see cref="T:System.Security.SecurityCriticalAttribute" />.</summary>
	// Token: 0x02000530 RID: 1328
	[Obsolete("SecurityCriticalScope is only used for .NET 2.0 transparency compatibility.")]
	public enum SecurityCriticalScope
	{
		/// <summary>The attribute applies only to the immediate target.</summary>
		// Token: 0x04001F2A RID: 7978
		Explicit,
		/// <summary>The attribute applies to all code that follows it.</summary>
		// Token: 0x04001F2B RID: 7979
		Everything
	}
}
