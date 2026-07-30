using System;

namespace System.Linq.Expressions
{
	// Token: 0x02000223 RID: 547
	internal sealed class CoalesceConversionBinaryExpression : BinaryExpression
	{
		// Token: 0x06000DA3 RID: 3491 RVA: 0x0002D7AA File Offset: 0x0002B9AA
		internal CoalesceConversionBinaryExpression(Expression left, Expression right, LambdaExpression conversion)
			: base(left, right)
		{
			this._conversion = conversion;
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x0002D7BB File Offset: 0x0002B9BB
		internal override LambdaExpression GetConversion()
		{
			return this._conversion;
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x0002D7C3 File Offset: 0x0002B9C3
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Coalesce;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x0002D7C6 File Offset: 0x0002B9C6
		public sealed override Type Type
		{
			get
			{
				return base.Right.Type;
			}
		}

		// Token: 0x04000884 RID: 2180
		private readonly LambdaExpression _conversion;
	}
}
