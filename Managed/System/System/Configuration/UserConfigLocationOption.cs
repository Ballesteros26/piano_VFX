using System;

namespace System.Configuration
{
	// Token: 0x02000174 RID: 372
	internal enum UserConfigLocationOption : uint
	{
		// Token: 0x04000FA2 RID: 4002
		Product = 32U,
		// Token: 0x04000FA3 RID: 4003
		Product_VersionMajor,
		// Token: 0x04000FA4 RID: 4004
		Product_VersionMinor,
		// Token: 0x04000FA5 RID: 4005
		Product_VersionBuild = 36U,
		// Token: 0x04000FA6 RID: 4006
		Product_VersionRevision = 40U,
		// Token: 0x04000FA7 RID: 4007
		Company_Product = 48U,
		// Token: 0x04000FA8 RID: 4008
		Company_Product_VersionMajor,
		// Token: 0x04000FA9 RID: 4009
		Company_Product_VersionMinor,
		// Token: 0x04000FAA RID: 4010
		Company_Product_VersionBuild = 52U,
		// Token: 0x04000FAB RID: 4011
		Company_Product_VersionRevision = 56U,
		// Token: 0x04000FAC RID: 4012
		Evidence = 64U,
		// Token: 0x04000FAD RID: 4013
		Other = 32768U
	}
}
