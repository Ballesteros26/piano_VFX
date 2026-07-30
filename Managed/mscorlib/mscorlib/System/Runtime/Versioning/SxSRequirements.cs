using System;

namespace System.Runtime.Versioning
{
	// Token: 0x020006C3 RID: 1731
	[Flags]
	internal enum SxSRequirements
	{
		// Token: 0x04002693 RID: 9875
		None = 0,
		// Token: 0x04002694 RID: 9876
		AppDomainID = 1,
		// Token: 0x04002695 RID: 9877
		ProcessID = 2,
		// Token: 0x04002696 RID: 9878
		CLRInstanceID = 4,
		// Token: 0x04002697 RID: 9879
		AssemblyName = 8,
		// Token: 0x04002698 RID: 9880
		TypeName = 16
	}
}
