using System;

namespace System.Linq.Expressions
{
	// Token: 0x02000225 RID: 549
	internal class SimpleBinaryExpression : BinaryExpression
	{
		// Token: 0x06000DA9 RID: 3497 RVA: 0x0002D7F2 File Offset: 0x0002B9F2
		internal SimpleBinaryExpression(ExpressionType nodeType, Expression left, Expression right, Type type)
			: base(left, right)
		{
			this.NodeType = nodeType;
			this.Type = type;
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000DAA RID: 3498 RVA: 0x0002D80B File Offset: 0x0002BA0B
		public sealed override ExpressionType NodeType { get; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x0002D813 File Offset: 0x0002BA13
		public sealed override Type Type { get; }
	}
}
