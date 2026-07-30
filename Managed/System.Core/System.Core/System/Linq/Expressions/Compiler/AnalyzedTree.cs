using System;
using System.Collections.Generic;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020002BB RID: 699
	internal sealed class AnalyzedTree
	{
		// Token: 0x040009F8 RID: 2552
		internal readonly Dictionary<object, CompilerScope> Scopes = new Dictionary<object, CompilerScope>();

		// Token: 0x040009F9 RID: 2553
		internal readonly Dictionary<LambdaExpression, BoundConstants> Constants = new Dictionary<LambdaExpression, BoundConstants>();
	}
}
