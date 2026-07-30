using System;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
	// Token: 0x02000278 RID: 632
	internal class Expression
	{
		// Token: 0x06001298 RID: 4760 RVA: 0x0005359D File Offset: 0x0005179D
		public Expression(ExpressionType type)
		{
			this.type = type;
			this.combinator = ExpressionCombinator.None;
			this.multiplier = new ExpressionMultiplier(ExpressionMultiplierType.None);
			this.subExpressions = null;
			this.keyword = null;
		}

		// Token: 0x04000946 RID: 2374
		public ExpressionType type;

		// Token: 0x04000947 RID: 2375
		public ExpressionMultiplier multiplier;

		// Token: 0x04000948 RID: 2376
		public DataType dataType;

		// Token: 0x04000949 RID: 2377
		public ExpressionCombinator combinator;

		// Token: 0x0400094A RID: 2378
		public Expression[] subExpressions;

		// Token: 0x0400094B RID: 2379
		public string keyword;
	}
}
