using System;
using System.Diagnostics;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents an unconditional jump. This includes return statements, break and continue statements, and other jumps.</summary>
	// Token: 0x02000273 RID: 627
	[DebuggerTypeProxy(typeof(Expression.GotoExpressionProxy))]
	public sealed class GotoExpression : Expression
	{
		// Token: 0x0600126F RID: 4719 RVA: 0x0003AE81 File Offset: 0x00039081
		internal GotoExpression(GotoExpressionKind kind, LabelTarget target, Expression value, Type type)
		{
			this.Kind = kind;
			this.Value = value;
			this.Target = target;
			this.Type = type;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.GotoExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06001270 RID: 4720 RVA: 0x0003AEA6 File Offset: 0x000390A6
		public sealed override Type Type { get; }

		/// <summary>Returns the node type of this <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> that represents this expression.</returns>
		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06001271 RID: 4721 RVA: 0x0003AEAE File Offset: 0x000390AE
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Goto;
			}
		}

		/// <summary>The value passed to the target, or null if the target is of type System.Void.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> object representing the value passed to the target or null.</returns>
		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x0003AEB2 File Offset: 0x000390B2
		public Expression Value { get; }

		/// <summary>The target label where this node jumps to.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.LabelTarget" /> object representing the target label for this node.</returns>
		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06001273 RID: 4723 RVA: 0x0003AEBA File Offset: 0x000390BA
		public LabelTarget Target { get; }

		/// <summary>The kind of the "go to" expression. Serves information purposes only.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.GotoExpressionKind" /> object representing the kind of the "go to" expression.</returns>
		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x0003AEC2 File Offset: 0x000390C2
		public GotoExpressionKind Kind { get; }

		// Token: 0x06001275 RID: 4725 RVA: 0x0003AECA File Offset: 0x000390CA
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitGoto(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="target">The <see cref="P:System.Linq.Expressions.GotoExpression.Target" /> property of the result. </param>
		/// <param name="value">The <see cref="P:System.Linq.Expressions.GotoExpression.Value" /> property of the result. </param>
		// Token: 0x06001276 RID: 4726 RVA: 0x0003AED3 File Offset: 0x000390D3
		public GotoExpression Update(LabelTarget target, Expression value)
		{
			if (target == this.Target && value == this.Value)
			{
				return this;
			}
			return Expression.MakeGoto(this.Kind, target, value, this.Type);
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x0000220F File Offset: 0x0000040F
		internal GotoExpression()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
