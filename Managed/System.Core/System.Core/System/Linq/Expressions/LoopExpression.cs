using System;
using System.Diagnostics;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents an infinite loop. It can be exited with "break".</summary>
	// Token: 0x0200028B RID: 651
	[DebuggerTypeProxy(typeof(Expression.LoopExpressionProxy))]
	public sealed class LoopExpression : Expression
	{
		// Token: 0x06001318 RID: 4888 RVA: 0x0003BACF File Offset: 0x00039CCF
		internal LoopExpression(Expression body, LabelTarget @break, LabelTarget @continue)
		{
			this.Body = body;
			this.BreakLabel = @break;
			this.ContinueLabel = @continue;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.LoopExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06001319 RID: 4889 RVA: 0x0003BAEC File Offset: 0x00039CEC
		public sealed override Type Type
		{
			get
			{
				if (this.BreakLabel != null)
				{
					return this.BreakLabel.Type;
				}
				return typeof(void);
			}
		}

		/// <summary>Returns the node type of this expression. Extension nodes should return <see cref="F:System.Linq.Expressions.ExpressionType.Extension" /> when overriding this method.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> of the expression.</returns>
		// Token: 0x1700033A RID: 826
		// (get) Token: 0x0600131A RID: 4890 RVA: 0x0003BB0C File Offset: 0x00039D0C
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Loop;
			}
		}

		/// <summary>Gets the <see cref="T:System.Linq.Expressions.Expression" /> that is the body of the loop.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> that is the body of the loop.</returns>
		// Token: 0x1700033B RID: 827
		// (get) Token: 0x0600131B RID: 4891 RVA: 0x0003BB10 File Offset: 0x00039D10
		public Expression Body { get; }

		/// <summary>Gets the <see cref="T:System.Linq.Expressions.LabelTarget" /> that is used by the loop body as a break statement target.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.LabelTarget" /> that is used by the loop body as a break statement target.</returns>
		// Token: 0x1700033C RID: 828
		// (get) Token: 0x0600131C RID: 4892 RVA: 0x0003BB18 File Offset: 0x00039D18
		public LabelTarget BreakLabel { get; }

		/// <summary>Gets the <see cref="T:System.Linq.Expressions.LabelTarget" /> that is used by the loop body as a continue statement target.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.LabelTarget" /> that is used by the loop body as a continue statement target.</returns>
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x0600131D RID: 4893 RVA: 0x0003BB20 File Offset: 0x00039D20
		public LabelTarget ContinueLabel { get; }

		// Token: 0x0600131E RID: 4894 RVA: 0x0003BB28 File Offset: 0x00039D28
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitLoop(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="breakLabel">The <see cref="P:System.Linq.Expressions.LoopExpression.BreakLabel" /> property of the result.</param>
		/// <param name="continueLabel">The <see cref="P:System.Linq.Expressions.LoopExpression.ContinueLabel" /> property of the result.</param>
		/// <param name="body">The <see cref="P:System.Linq.Expressions.LoopExpression.Body" /> property of the result.</param>
		// Token: 0x0600131F RID: 4895 RVA: 0x0003BB31 File Offset: 0x00039D31
		public LoopExpression Update(LabelTarget breakLabel, LabelTarget continueLabel, Expression body)
		{
			if (breakLabel == this.BreakLabel && continueLabel == this.ContinueLabel && body == this.Body)
			{
				return this;
			}
			return Expression.Loop(body, breakLabel, continueLabel);
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x0000220F File Offset: 0x0000040F
		internal LoopExpression()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
