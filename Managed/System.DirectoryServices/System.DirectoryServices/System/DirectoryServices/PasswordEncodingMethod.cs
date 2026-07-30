using System;

namespace System.DirectoryServices
{
	/// <summary>Specifies whether SSL is used when you set or change a password. This enumeration is used with the <see cref="P:System.DirectoryServices.DirectoryEntryConfiguration.PasswordEncoding" /> property.          </summary>
	// Token: 0x02000023 RID: 35
	public enum PasswordEncodingMethod
	{
		/// <summary>Passwords are encoded using SSL.</summary>
		// Token: 0x04000093 RID: 147
		PasswordEncodingSsl,
		/// <summary>Passwords are not encoded and are transmitted in plain text.</summary>
		// Token: 0x04000094 RID: 148
		PasswordEncodingClear
	}
}
