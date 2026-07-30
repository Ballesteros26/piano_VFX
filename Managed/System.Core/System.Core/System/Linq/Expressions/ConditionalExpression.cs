using System;
using System.Diagnostics;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents an expression that has a conditional operator.</summary>
	// Token: 0x02000255 RID: 597
	[DebuggerTypeProxy(typeof(Expression.ConditionalExpressionProxy))]
	public class ConditionalExpression : Expression
	{
		// Token: 0x0600106D RID: 4205 RVA: 0x00035DA2 File Offset: 0x00033FA2
		internal ConditionalExpression(Expression test, Expression ifTrue)
		{
			this.Test = test;
			this.IfTrue = ifTrue;
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x00035DB8 File Offset: 0x00033FB8
		internal static ConditionalExpression Make(Expression test, Expression ifTrue, Expression ifFalse, Type type)
		{
			if (ifTrue.Type != type || ifFalse.Type != type)
			{
				return new FullConditionalExpressionWithType(test, ifTrue, ifFalse, type);
			}
			if (ifFalse is DefaultExpression && ifFalse.Type == typeof(void))
			{
				return new ConditionalExpression(test, ifTrue);
			}
			return new FullConditionalExpression(test, ifTrue, ifFalse);
		}

		/// <summary>Returns the node type of this expression. Extension nodes should return <see cref="F:System.Linq.Expressions.ExpressionType.Extension" /> when overriding this method.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> of the expression.</returns>
		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x0600106F RID: 4207 RVA: 0x00035E1A File Offset: 0x0003401A
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Conditional;
			}
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.ConditionalExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x00035E1D File Offset: 0x0003401D
		public override Type Type
		{
			get
			{
				return this.IfTrue.Type;
			}
		}

		/// <summary>Gets the test of the conditional operation.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the test of the conditional operation.</returns>
		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x00035E2A File Offset: 0x0003402A
		public Expression Test { get; }

		/// <summary>Gets the expression to execute if the test evaluates to true.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the expression to execute if the test is true.</returns>
		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x00035E32 File Offset: 0x00034032
		public Expression IfTrue { get; }

		/// <summary>Gets the expression to execute if the test evaluates to false.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the expression to execute if the test is false.</returns>
		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06001073 RID: 4211 RVA: 0x00035E3A File Offset: 0x0003403A
		public Expression IfFalse
		{
			get
			{
				return this.GetFalse();
			}
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x00035E42 File Offset: 0x00034042
		internal virtual Expression GetFalse()
		{
			return Utils.Empty;
		}

		/// <summary>Dispatches to the specific visit method for this node type. For example, <see cref="T:System.Linq.Expressions.MethodCallExpression" /> calls the <see cref="M:System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(System.Linq.Expressions.MethodCallExpression)" />.</summary>
		/// <returns>The result of visiting this node.</returns>
		/// <param name="visitor">The visitor to visit this node with.</param>
		// Token: 0x06001075 RID: 4213 RVA: 0x00035E49 File Offset: 0x00034049
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitConditional(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression</summary>
		/// <returns>This expression if no children changed, or an expression with the updated children.</returns>
		/// <param name="test">The <see cref="P:System.Linq.Expressions.ConditionalExpression.Test" /> property of the result.</param>
		/// <param name="ifTrue">The <see cref="P:System.Linq.Expressions.ConditionalExpression.IfTrue" /> property of the result.</param>
		/// <param name="ifFalse">The <see cref="P:System.Linq.Expressions.ConditionalExpression.IfFalse" /> property of the result.</param>
		// Token: 0x06001076 RID: 4214 RVA: 0x00035E52 File Offset: 0x00034052
		public ConditionalExpression Update(Expression test, Expression ifTrue, Expression ifFalse)
		{
			if (test == this.Test && ifTrue == this.IfTrue && ifFalse == this.IfFalse)
			{
				return this;
			}
			return Expression.Condition(test, ifTrue, ifFalse, this.Type);
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x0000220F File Offset: 0x0000040F
		internal ConditionalExpression()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
