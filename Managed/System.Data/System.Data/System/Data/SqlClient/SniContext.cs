using System;

namespace System.Data.SqlClient
{
	// Token: 0x020001FF RID: 511
	internal enum SniContext
	{
		// Token: 0x040010AA RID: 4266
		Undefined,
		// Token: 0x040010AB RID: 4267
		Snix_Connect,
		// Token: 0x040010AC RID: 4268
		Snix_PreLoginBeforeSuccessfulWrite,
		// Token: 0x040010AD RID: 4269
		Snix_PreLogin,
		// Token: 0x040010AE RID: 4270
		Snix_LoginSspi,
		// Token: 0x040010AF RID: 4271
		Snix_ProcessSspi,
		// Token: 0x040010B0 RID: 4272
		Snix_Login,
		// Token: 0x040010B1 RID: 4273
		Snix_EnableMars,
		// Token: 0x040010B2 RID: 4274
		Snix_AutoEnlist,
		// Token: 0x040010B3 RID: 4275
		Snix_GetMarsSession,
		// Token: 0x040010B4 RID: 4276
		Snix_Execute,
		// Token: 0x040010B5 RID: 4277
		Snix_Read,
		// Token: 0x040010B6 RID: 4278
		Snix_Close,
		// Token: 0x040010B7 RID: 4279
		Snix_SendRows
	}
}
