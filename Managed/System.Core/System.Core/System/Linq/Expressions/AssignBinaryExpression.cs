using System;

namespace System.Linq.Expressions
{
	// Token: 0x02000221 RID: 545
	internal class AssignBinaryExpression : BinaryExpression
	{
		// Token: 0x06000D9C RID: 3484 RVA: 0x0002D771 File Offset: 0x0002B971
		internal AssignBinaryExpression(Expression left, Expression right)
			: base(left, right)
		{
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x0002D77B File Offset: 0x0002B97B
		public static AssignBinaryExpression Make(Expression left, Expression right, bool byRef)
		{
			if (byRef)
			{
				return new ByRefAssignBinaryExpression(left, right);
			}
			return new AssignBinaryExpression(left, right);
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000D9E RID: 3486 RVA: 0x00002285 File Offset: 0x00000485
		internal virtual bool IsByRef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x0002D78F File Offset: 0x0002B98F
		public sealed override Type Type
		{
			get
			{
				return base.Left.Type;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x0002D79C File Offset: 0x0002B99C
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Assign;
			}
		}
	}
}
