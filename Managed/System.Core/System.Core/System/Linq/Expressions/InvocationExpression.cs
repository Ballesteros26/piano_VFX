using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic.Utils;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents an expression that applies a delegate or lambda expression to a list of argument expressions.</summary>
	// Token: 0x02000278 RID: 632
	[DebuggerTypeProxy(typeof(Expression.InvocationExpressionProxy))]
	public class InvocationExpression : Expression, IArgumentProvider
	{
		// Token: 0x0600128B RID: 4747 RVA: 0x0003AFEA File Offset: 0x000391EA
		internal InvocationExpression(Expression expression, Type returnType)
		{
			this.Expression = expression;
			this.Type = returnType;
		}

		/// <summary>Gets the static type of the expression that this <see cref="P:System.Linq.Expressions.InvocationExpression.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.InvocationExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x1700030B RID: 779
		// (get) Token: 0x0600128C RID: 4748 RVA: 0x0003B000 File Offset: 0x00039200
		public sealed override Type Type { get; }

		/// <summary>Returns the node type of this expression. Extension nodes should return <see cref="F:System.Linq.Expressions.ExpressionType.Extension" /> when overriding this method.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> of the expression.</returns>
		// Token: 0x1700030C RID: 780
		// (get) Token: 0x0600128D RID: 4749 RVA: 0x0003B008 File Offset: 0x00039208
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Invoke;
			}
		}

		/// <summary>Gets the delegate or lambda expression to be applied.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the delegate to be applied.</returns>
		// Token: 0x1700030D RID: 781
		// (get) Token: 0x0600128E RID: 4750 RVA: 0x0003B00C File Offset: 0x0003920C
		public Expression Expression { get; }

		/// <summary>Gets the arguments that the delegate or lambda expression is applied to.</summary>
		/// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Linq.Expressions.Expression" /> objects which represent the arguments that the delegate is applied to.</returns>
		// Token: 0x1700030E RID: 782
		// (get) Token: 0x0600128F RID: 4751 RVA: 0x0003B014 File Offset: 0x00039214
		public ReadOnlyCollection<Expression> Arguments
		{
			get
			{
				return this.GetOrMakeArguments();
			}
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="expression">The <see cref="P:System.Linq.Expressions.InvocationExpression.Expression" /> property of the result.</param>
		/// <param name="arguments">The <see cref="P:System.Linq.Expressions.InvocationExpression.Arguments" /> property of the result.</param>
		// Token: 0x06001290 RID: 4752 RVA: 0x0003B01C File Offset: 0x0003921C
		public InvocationExpression Update(Expression expression, IEnumerable<Expression> arguments)
		{
			if (((expression == this.Expression) & (arguments != null)) && ExpressionUtils.SameElements<Expression>(ref arguments, this.Arguments))
			{
				return this;
			}
			return Expression.Invoke(expression, arguments);
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		internal virtual ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		public virtual Expression GetArgument(int index)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06001293 RID: 4755 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		public virtual int ArgumentCount
		{
			get
			{
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x0003B046 File Offset: 0x00039246
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitInvocation(this);
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		internal virtual InvocationExpression Rewrite(Expression lambda, Expression[] arguments)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x0003B04F File Offset: 0x0003924F
		internal LambdaExpression LambdaOperand
		{
			get
			{
				if (this.Expression.NodeType != ExpressionType.Quote)
				{
					return this.Expression as LambdaExpression;
				}
				return (LambdaExpression)((UnaryExpression)this.Expression).Operand;
			}
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x0000220F File Offset: 0x0000040F
		internal InvocationExpression()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
