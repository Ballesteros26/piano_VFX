using System;

namespace System.Net
{
	// Token: 0x0200003C RID: 60
	[Flags]
	internal enum ContextFlagsPal
	{
		// Token: 0x04000450 RID: 1104
		None = 0,
		// Token: 0x04000451 RID: 1105
		Delegate = 1,
		// Token: 0x04000452 RID: 1106
		MutualAuth = 2,
		// Token: 0x04000453 RID: 1107
		ReplayDetect = 4,
		// Token: 0x04000454 RID: 1108
		SequenceDetect = 8,
		// Token: 0x04000455 RID: 1109
		Confidentiality = 16,
		// Token: 0x04000456 RID: 1110
		UseSessionKey = 32,
		// Token: 0x04000457 RID: 1111
		AllocateMemory = 256,
		// Token: 0x04000458 RID: 1112
		Connection = 2048,
		// Token: 0x04000459 RID: 1113
		InitExtendedError = 16384,
		// Token: 0x0400045A RID: 1114
		AcceptExtendedError = 32768,
		// Token: 0x0400045B RID: 1115
		InitStream = 32768,
		// Token: 0x0400045C RID: 1116
		AcceptStream = 65536,
		// Token: 0x0400045D RID: 1117
		InitIntegrity = 65536,
		// Token: 0x0400045E RID: 1118
		AcceptIntegrity = 131072,
		// Token: 0x0400045F RID: 1119
		InitManualCredValidation = 524288,
		// Token: 0x04000460 RID: 1120
		InitUseSuppliedCreds = 128,
		// Token: 0x04000461 RID: 1121
		InitIdentify = 131072,
		// Token: 0x04000462 RID: 1122
		AcceptIdentify = 524288,
		// Token: 0x04000463 RID: 1123
		ProxyBindings = 67108864,
		// Token: 0x04000464 RID: 1124
		AllowMissingBindings = 268435456,
		// Token: 0x04000465 RID: 1125
		UnverifiedTargetName = 536870912
	}
}
