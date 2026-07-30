using System;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020002D1 RID: 721
	internal enum LabelScopeKind
	{
		// Token: 0x04000A40 RID: 2624
		Statement,
		// Token: 0x04000A41 RID: 2625
		Block,
		// Token: 0x04000A42 RID: 2626
		Switch,
		// Token: 0x04000A43 RID: 2627
		Lambda,
		// Token: 0x04000A44 RID: 2628
		Try,
		// Token: 0x04000A45 RID: 2629
		Catch,
		// Token: 0x04000A46 RID: 2630
		Finally,
		// Token: 0x04000A47 RID: 2631
		Filter,
		// Token: 0x04000A48 RID: 2632
		Expression
	}
}
