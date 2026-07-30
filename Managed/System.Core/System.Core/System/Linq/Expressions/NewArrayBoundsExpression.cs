using System;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
	// Token: 0x020002A5 RID: 677
	internal sealed class NewArrayBoundsExpression : NewArrayExpression
	{
		// Token: 0x060013B5 RID: 5045 RVA: 0x0003CAFE File Offset: 0x0003ACFE
		internal NewArrayBoundsExpression(Type type, ReadOnlyCollection<Expression> expressions)
			: base(type, expressions)
		{
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060013B6 RID: 5046 RVA: 0x0003CB0C File Offset: 0x0003AD0C
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.NewArrayBounds;
			}
		}
	}
}
