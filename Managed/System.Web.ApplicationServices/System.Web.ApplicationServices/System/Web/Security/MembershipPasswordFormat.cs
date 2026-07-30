using System;
using System.Runtime.CompilerServices;

namespace System.Web.Security
{
	/// <summary>Describes the encryption format for storing passwords for membership users.</summary>
	// Token: 0x02000010 RID: 16
	[TypeForwardedFrom("System.Web, Version=2.0.0.0, Culture=Neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public enum MembershipPasswordFormat
	{
		/// <summary>Passwords are not encrypted.</summary>
		// Token: 0x04000053 RID: 83
		Clear,
		/// <summary>Passwords are encrypted one-way using the SHA1 hashing algorithm.</summary>
		// Token: 0x04000054 RID: 84
		Hashed,
		/// <summary>Passwords are encrypted using the encryption settings determined by the machineKey Element (ASP.NET Settings Schema) element configuration.</summary>
		// Token: 0x04000055 RID: 85
		Encrypted
	}
}
