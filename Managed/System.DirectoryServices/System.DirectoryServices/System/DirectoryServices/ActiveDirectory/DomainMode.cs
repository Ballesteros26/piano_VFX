using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>Indicates the mode that a domain is operating in.</summary>
	// Token: 0x02000057 RID: 87
	public enum DomainMode
	{
		/// <summary>The domain is operating in Windows 2000 mixed mode.</summary>
		// Token: 0x040000ED RID: 237
		Windows2000MixedDomain,
		/// <summary>The domain is operating in Windows 2000 native mode.</summary>
		// Token: 0x040000EE RID: 238
		Windows2000NativeDomain,
		/// <summary>The domain is operating in Windows Server 2003 domain-function mode.</summary>
		// Token: 0x040000EF RID: 239
		Windows2003InterimDomain,
		/// <summary>The domain is operating in Windows Server 2003 mode.</summary>
		// Token: 0x040000F0 RID: 240
		Windows2003Domain,
		/// <summary>The domain is operating in  mode.</summary>
		// Token: 0x040000F1 RID: 241
		Windows2008Domain,
		/// <summary>The domain is operating in Windows 2008 R2 mode.</summary>
		// Token: 0x040000F2 RID: 242
		Windows2008R2Domain,
		// Token: 0x040000F3 RID: 243
		Unknown = -1,
		/// <summary />
		// Token: 0x040000F4 RID: 244
		Windows2012R2Domain = 7,
		/// <summary>The domain is operating in Windows 8 mode.</summary>
		// Token: 0x040000F5 RID: 245
		Windows8Domain = 6
	}
}
