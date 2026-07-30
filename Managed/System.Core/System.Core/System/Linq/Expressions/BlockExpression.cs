using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic.Utils;
using System.Threading;

namespace System.Linq.Expressions
{
	/// <summary>Represents a block that contains a sequence of expressions where variables can be defined.</summary>
	// Token: 0x02000245 RID: 581
	[DebuggerTypeProxy(typeof(Expression.BlockExpressionProxy))]
	public class BlockExpression : Expression
	{
		/// <summary>Gets the expressions in this block.</summary>
		/// <returns>The read-only collection containing all the expressions in this block.</returns>
		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06001008 RID: 4104 RVA: 0x0003527B File Offset: 0x0003347B
		public ReadOnlyCollection<Expression> Expressions
		{
			get
			{
				return this.GetOrMakeExpressions();
			}
		}

		/// <summary>Gets the variables defined in this block.</summary>
		/// <returns>The read-only collection containing all the variables defined in this block.</returns>
		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06001009 RID: 4105 RVA: 0x00035283 File Offset: 0x00033483
		public ReadOnlyCollection<ParameterExpression> Variables
		{
			get
			{
				return this.GetOrMakeVariables();
			}
		}

		/// <summary>Gets the last expression in this block.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> object representing the last expression in this block.</returns>
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x0600100A RID: 4106 RVA: 0x0003528B File Offset: 0x0003348B
		public Expression Result
		{
			get
			{
				return this.GetExpression(this.ExpressionCount - 1);
			}
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x0003529B File Offset: 0x0003349B
		internal BlockExpression()
		{
		}

		/// <summary>Dispatches to the specific visit method for this node type. For example, <see cref="T:System.Linq.Expressions.MethodCallExpression" /> calls the <see cref="M:System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(System.Linq.Expressions.MethodCallExpression)" />.</summary>
		/// <returns>The result of visiting this node.</returns>
		/// <param name="visitor">The visitor to visit this node with.</param>
		// Token: 0x0600100C RID: 4108 RVA: 0x000352A3 File Offset: 0x000334A3
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitBlock(this);
		}

		/// <summary>Returns the node type of this expression. Extension nodes should return <see cref="F:System.Linq.Expressions.ExpressionType.Extension" /> when overriding this method.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> of the expression.</returns>
		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x000352AC File Offset: 0x000334AC
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Block;
			}
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.BlockExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x0600100E RID: 4110 RVA: 0x000352B0 File Offset: 0x000334B0
		public override Type Type
		{
			get
			{
				return this.GetExpression(this.ExpressionCount - 1).Type;
			}
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children changed, or an expression with the updated children.</returns>
		/// <param name="variables">The <see cref="P:System.Linq.Expressions.BlockExpression.Variables" /> property of the result. </param>
		/// <param name="expressions">The <see cref="P:System.Linq.Expressions.BlockExpression.Expressions" /> property of the result. </param>
		// Token: 0x0600100F RID: 4111 RVA: 0x000352C8 File Offset: 0x000334C8
		public BlockExpression Update(IEnumerable<ParameterExpression> variables, IEnumerable<Expression> expressions)
		{
			if (expressions != null)
			{
				ICollection<ParameterExpression> collection;
				if (variables == null)
				{
					collection = null;
				}
				else
				{
					collection = variables as ICollection<ParameterExpression>;
					if (collection == null)
					{
						collection = (variables = variables.ToReadOnly<ParameterExpression>());
					}
				}
				if (this.SameVariables(collection))
				{
					ICollection<Expression> collection2 = expressions as ICollection<Expression>;
					if (collection2 == null)
					{
						collection2 = (expressions = expressions.ToReadOnly<Expression>());
					}
					if (this.SameExpressions(collection2))
					{
						return this;
					}
				}
			}
			return Expression.Block(this.Type, variables, expressions);
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x00035328 File Offset: 0x00033528
		internal virtual bool SameVariables(ICollection<ParameterExpression> variables)
		{
			return variables == null || variables.Count == 0;
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		internal virtual bool SameExpressions(ICollection<Expression> expressions)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		internal virtual Expression GetExpression(int index)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06001013 RID: 4115 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		internal virtual int ExpressionCount
		{
			get
			{
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		internal virtual ReadOnlyCollection<Expression> GetOrMakeExpressions()
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x0003533F File Offset: 0x0003353F
		internal virtual ReadOnlyCollection<ParameterExpression> GetOrMakeVariables()
		{
			return EmptyReadOnlyCollection<ParameterExpression>.Instance;
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		internal virtual BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x00035348 File Offset: 0x00033548
		internal static ReadOnlyCollection<Expression> ReturnReadOnlyExpressions(BlockExpression provider, ref object collection)
		{
			Expression expression = collection as Expression;
			if (expression != null)
			{
				Interlocked.CompareExchange(ref collection, new ReadOnlyCollection<Expression>(new BlockExpressionList(provider, expression)), expression);
			}
			return (ReadOnlyCollection<Expression>)collection;
		}
	}
}
