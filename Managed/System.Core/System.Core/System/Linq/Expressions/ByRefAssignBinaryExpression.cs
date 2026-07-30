using System;

namespace System.Linq.Expressions
{
	// Token: 0x02000222 RID: 546
	internal class ByRefAssignBinaryExpression : AssignBinaryExpression
	{
		// Token: 0x06000DA1 RID: 3489 RVA: 0x0002D7A0 File Offset: 0x0002B9A0
		internal ByRefAssignBinaryExpression(Expression left, Expression right)
			: base(left, right)
		{
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x0000AA13 File Offset: 0x00008C13
		internal override bool IsByRef
		{
			get
			{
				return true;
			}
		}
	}
}
