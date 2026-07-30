using System;

namespace System.Web.Security
{
	// Token: 0x02000015 RID: 21
	internal interface IMembershipHelper
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000054 RID: 84
		int UserIsOnlineTimeWindow { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000055 RID: 85
		MembershipProviderCollection Providers { get; }

		// Token: 0x06000056 RID: 86
		byte[] DecryptPassword(byte[] encodedPassword);

		// Token: 0x06000057 RID: 87
		byte[] EncryptPassword(byte[] password);
	}
}
