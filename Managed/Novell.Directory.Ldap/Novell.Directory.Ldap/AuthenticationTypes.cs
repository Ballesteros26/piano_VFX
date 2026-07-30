using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000006 RID: 6
	public enum AuthenticationTypes
	{
		// Token: 0x0400002C RID: 44
		Anonymous = 16,
		// Token: 0x0400002D RID: 45
		Delegation = 256,
		// Token: 0x0400002E RID: 46
		Encryption = 2,
		// Token: 0x0400002F RID: 47
		FastBind = 32,
		// Token: 0x04000030 RID: 48
		None = 0,
		// Token: 0x04000031 RID: 49
		ReadonlyServer = 4,
		// Token: 0x04000032 RID: 50
		Sealing = 128,
		// Token: 0x04000033 RID: 51
		Secure = 1,
		// Token: 0x04000034 RID: 52
		SecureSocketsLayer,
		// Token: 0x04000035 RID: 53
		ServerBind = 512,
		// Token: 0x04000036 RID: 54
		Signing = 64
	}
}
