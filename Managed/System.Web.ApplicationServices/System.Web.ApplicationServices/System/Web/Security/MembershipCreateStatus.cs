using System;
using System.Runtime.CompilerServices;

namespace System.Web.Security
{
	/// <summary>Describes the result of a <see cref="M:System.Web.Security.Membership.CreateUser(System.String,System.String)" /> operation.</summary>
	// Token: 0x0200000D RID: 13
	[TypeForwardedFrom("System.Web, Version=2.0.0.0, Culture=Neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public enum MembershipCreateStatus
	{
		/// <summary>The user was successfully created.</summary>
		// Token: 0x04000045 RID: 69
		Success,
		/// <summary>The user name was not found in the database.</summary>
		// Token: 0x04000046 RID: 70
		InvalidUserName,
		/// <summary>The password is not formatted correctly.</summary>
		// Token: 0x04000047 RID: 71
		InvalidPassword,
		/// <summary>The password question is not formatted correctly.</summary>
		// Token: 0x04000048 RID: 72
		InvalidQuestion,
		/// <summary>The password answer is not formatted correctly.</summary>
		// Token: 0x04000049 RID: 73
		InvalidAnswer,
		/// <summary>The e-mail address is not formatted correctly.</summary>
		// Token: 0x0400004A RID: 74
		InvalidEmail,
		/// <summary>The user name already exists in the database for the application.</summary>
		// Token: 0x0400004B RID: 75
		DuplicateUserName,
		/// <summary>The e-mail address already exists in the database for the application.</summary>
		// Token: 0x0400004C RID: 76
		DuplicateEmail,
		/// <summary>The user was not created, for a reason defined by the provider.</summary>
		// Token: 0x0400004D RID: 77
		UserRejected,
		/// <summary>The provider user key is of an invalid type or format.</summary>
		// Token: 0x0400004E RID: 78
		InvalidProviderUserKey,
		/// <summary>The provider user key already exists in the database for the application.</summary>
		// Token: 0x0400004F RID: 79
		DuplicateProviderUserKey,
		/// <summary>The provider returned an error that is not described by other <see cref="T:System.Web.Security.MembershipCreateStatus" /> enumeration values.</summary>
		// Token: 0x04000050 RID: 80
		ProviderError
	}
}
