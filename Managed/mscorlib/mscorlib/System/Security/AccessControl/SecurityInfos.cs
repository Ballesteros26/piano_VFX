using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the section of a security descriptor to be queried or set.</summary>
	// Token: 0x02000611 RID: 1553
	[Flags]
	public enum SecurityInfos
	{
		/// <summary>Specifies the owner identifier.</summary>
		// Token: 0x04002213 RID: 8723
		Owner = 1,
		/// <summary>Specifies the primary group identifier.</summary>
		// Token: 0x04002214 RID: 8724
		Group = 2,
		/// <summary>Specifies the discretionary access control list (DACL).</summary>
		// Token: 0x04002215 RID: 8725
		DiscretionaryAcl = 4,
		/// <summary>Specifies the system access control list (SACL).</summary>
		// Token: 0x04002216 RID: 8726
		SystemAcl = 8
	}
}
