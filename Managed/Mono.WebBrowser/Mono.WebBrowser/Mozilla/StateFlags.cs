using System;

namespace Mono.Mozilla
{
	// Token: 0x02000060 RID: 96
	[Flags]
	internal enum StateFlags
	{
		// Token: 0x040000C1 RID: 193
		Start = 1,
		// Token: 0x040000C2 RID: 194
		Redirecting = 2,
		// Token: 0x040000C3 RID: 195
		Transferring = 4,
		// Token: 0x040000C4 RID: 196
		Negotiating = 8,
		// Token: 0x040000C5 RID: 197
		Stop = 16,
		// Token: 0x040000C6 RID: 198
		IsRequest = 65536,
		// Token: 0x040000C7 RID: 199
		IsDocument = 131072,
		// Token: 0x040000C8 RID: 200
		IsNetwork = 262144,
		// Token: 0x040000C9 RID: 201
		IsWindow = 524288,
		// Token: 0x040000CA RID: 202
		Restoring = 16777216,
		// Token: 0x040000CB RID: 203
		IsInsecure = 4,
		// Token: 0x040000CC RID: 204
		IsBroken = 1,
		// Token: 0x040000CD RID: 205
		IsSecure = 2,
		// Token: 0x040000CE RID: 206
		SecureHigh = 262144,
		// Token: 0x040000CF RID: 207
		SecureMed = 65536,
		// Token: 0x040000D0 RID: 208
		SecureLow = 131072
	}
}
