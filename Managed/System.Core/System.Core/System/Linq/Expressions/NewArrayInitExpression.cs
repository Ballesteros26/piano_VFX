using System;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
	// Token: 0x020002A4 RID: 676
	internal sealed class NewArrayInitExpression : NewArrayExpression
	{
		// Token: 0x060013B3 RID: 5043 RVA: 0x0003CAFE File Offset: 0x0003ACFE
		internal NewArrayInitExpression(Type type, ReadOnlyCollection<Expression> expressions)
			: base(type, expressions)
		{
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060013B4 RID: 5044 RVA: 0x0003CB08 File Offset: 0x0003AD08
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.NewArrayInit;
			}
		}
	}
}
