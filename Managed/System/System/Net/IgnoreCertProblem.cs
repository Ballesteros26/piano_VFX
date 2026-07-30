using System;

namespace System.Net
{
	// Token: 0x02000445 RID: 1093
	internal enum IgnoreCertProblem
	{
		// Token: 0x04001D29 RID: 7465
		not_time_valid = 1,
		// Token: 0x04001D2A RID: 7466
		ctl_not_time_valid,
		// Token: 0x04001D2B RID: 7467
		not_time_nested = 4,
		// Token: 0x04001D2C RID: 7468
		invalid_basic_constraints = 8,
		// Token: 0x04001D2D RID: 7469
		all_not_time_valid = 7,
		// Token: 0x04001D2E RID: 7470
		allow_unknown_ca = 16,
		// Token: 0x04001D2F RID: 7471
		wrong_usage = 32,
		// Token: 0x04001D30 RID: 7472
		invalid_name = 64,
		// Token: 0x04001D31 RID: 7473
		invalid_policy = 128,
		// Token: 0x04001D32 RID: 7474
		end_rev_unknown = 256,
		// Token: 0x04001D33 RID: 7475
		ctl_signer_rev_unknown = 512,
		// Token: 0x04001D34 RID: 7476
		ca_rev_unknown = 1024,
		// Token: 0x04001D35 RID: 7477
		root_rev_unknown = 2048,
		// Token: 0x04001D36 RID: 7478
		all_rev_unknown = 3840,
		// Token: 0x04001D37 RID: 7479
		none = 4095
	}
}
