using System;

namespace System.Linq.Expressions
{
	// Token: 0x02000257 RID: 599
	internal sealed class FullConditionalExpressionWithType : FullConditionalExpression
	{
		// Token: 0x0600107A RID: 4218 RVA: 0x00035E98 File Offset: 0x00034098
		internal FullConditionalExpressionWithType(Expression test, Expression ifTrue, Expression ifFalse, Type type)
			: base(test, ifTrue, ifFalse)
		{
			this.Type = type;
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x0600107B RID: 4219 RVA: 0x00035EAB File Offset: 0x000340AB
		public sealed override Type Type { get; }
	}
}
