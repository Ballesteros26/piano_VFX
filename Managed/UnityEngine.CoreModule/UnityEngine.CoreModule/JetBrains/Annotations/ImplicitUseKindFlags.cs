using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000086 RID: 134
	[Flags]
	public enum ImplicitUseKindFlags
	{
		// Token: 0x04000136 RID: 310
		Default = 7,
		// Token: 0x04000137 RID: 311
		Access = 1,
		// Token: 0x04000138 RID: 312
		Assign = 2,
		// Token: 0x04000139 RID: 313
		InstantiatedWithFixedConstructorSignature = 4,
		// Token: 0x0400013A RID: 314
		InstantiatedNoFixedConstructorSignature = 8
	}
}
