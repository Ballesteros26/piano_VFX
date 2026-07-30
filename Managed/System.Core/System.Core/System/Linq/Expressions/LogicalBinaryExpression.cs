using System;

namespace System.Linq.Expressions
{
	// Token: 0x02000220 RID: 544
	internal sealed class LogicalBinaryExpression : BinaryExpression
	{
		// Token: 0x06000D99 RID: 3481 RVA: 0x0002D74C File Offset: 0x0002B94C
		internal LogicalBinaryExpression(ExpressionType nodeType, Expression left, Expression right)
			: base(left, right)
		{
			this.NodeType = nodeType;
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x0002D75D File Offset: 0x0002B95D
		public sealed override Type Type
		{
			get
			{
				return typeof(bool);
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x0002D769 File Offset: 0x0002B969
		public sealed override ExpressionType NodeType { get; }
	}
}
