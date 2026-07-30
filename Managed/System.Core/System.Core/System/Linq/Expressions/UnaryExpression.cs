using System;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents an expression that has a unary operator.</summary>
	// Token: 0x020002B9 RID: 697
	[DebuggerTypeProxy(typeof(Expression.UnaryExpressionProxy))]
	public sealed class UnaryExpression : Expression
	{
		// Token: 0x060014C9 RID: 5321 RVA: 0x0003DC6D File Offset: 0x0003BE6D
		internal UnaryExpression(ExpressionType nodeType, Expression expression, Type type, MethodInfo method)
		{
			this.Operand = expression;
			this.Method = method;
			this.NodeType = nodeType;
			this.Type = type;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.UnaryExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x170003DF RID: 991
		// (get) Token: 0x060014CA RID: 5322 RVA: 0x0003DC92 File Offset: 0x0003BE92
		public sealed override Type Type { get; }

		/// <summary>Returns the node type of this <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> that represents this expression.</returns>
		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x060014CB RID: 5323 RVA: 0x0003DC9A File Offset: 0x0003BE9A
		public sealed override ExpressionType NodeType { get; }

		/// <summary>Gets the operand of the unary operation.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the operand of the unary operation.</returns>
		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x060014CC RID: 5324 RVA: 0x0003DCA2 File Offset: 0x0003BEA2
		public Expression Operand { get; }

		/// <summary>Gets the implementing method for the unary operation.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodInfo" /> that represents the implementing method.</returns>
		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x060014CD RID: 5325 RVA: 0x0003DCAA File Offset: 0x0003BEAA
		public MethodInfo Method { get; }

		/// <summary>Gets a value that indicates whether the expression tree node represents a lifted call to an operator.</summary>
		/// <returns>true if the node represents a lifted call; otherwise, false.</returns>
		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x060014CE RID: 5326 RVA: 0x0003DCB4 File Offset: 0x0003BEB4
		public bool IsLifted
		{
			get
			{
				if (this.NodeType == ExpressionType.TypeAs || this.NodeType == ExpressionType.Quote || this.NodeType == ExpressionType.Throw)
				{
					return false;
				}
				bool flag = this.Operand.Type.IsNullableType();
				bool flag2 = this.Type.IsNullableType();
				if (this.Method != null)
				{
					return (flag && !TypeUtils.AreEquivalent(this.Method.GetParametersCached()[0].ParameterType, this.Operand.Type)) || (flag2 && !TypeUtils.AreEquivalent(this.Method.ReturnType, this.Type));
				}
				return flag || flag2;
			}
		}

		/// <summary>Gets a value that indicates whether the expression tree node represents a lifted call to an operator whose return type is lifted to a nullable type.</summary>
		/// <returns>true if the operator's return type is lifted to a nullable type; otherwise, false.</returns>
		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x060014CF RID: 5327 RVA: 0x0003DD57 File Offset: 0x0003BF57
		public bool IsLiftedToNull
		{
			get
			{
				return this.IsLifted && this.Type.IsNullableType();
			}
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x0003DD6E File Offset: 0x0003BF6E
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitUnary(this);
		}

		/// <summary>Gets a value that indicates whether the expression tree node can be reduced.</summary>
		/// <returns>True if a node can be reduced, otherwise false.</returns>
		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x060014D1 RID: 5329 RVA: 0x0003DD78 File Offset: 0x0003BF78
		public override bool CanReduce
		{
			get
			{
				ExpressionType nodeType = this.NodeType;
				return nodeType - ExpressionType.PreIncrementAssign <= 3;
			}
		}

		/// <summary>Reduces the expression node to a simpler expression. </summary>
		/// <returns>The reduced expression.</returns>
		// Token: 0x060014D2 RID: 5330 RVA: 0x0003DD98 File Offset: 0x0003BF98
		public override Expression Reduce()
		{
			if (!this.CanReduce)
			{
				return this;
			}
			ExpressionType nodeType = this.Operand.NodeType;
			if (nodeType == ExpressionType.MemberAccess)
			{
				return this.ReduceMember();
			}
			if (nodeType == ExpressionType.Index)
			{
				return this.ReduceIndex();
			}
			return this.ReduceVariable();
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x0003DDD9 File Offset: 0x0003BFD9
		private bool IsPrefix
		{
			get
			{
				return this.NodeType == ExpressionType.PreIncrementAssign || this.NodeType == ExpressionType.PreDecrementAssign;
			}
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x0003DDF4 File Offset: 0x0003BFF4
		private UnaryExpression FunctionalOp(Expression operand)
		{
			ExpressionType expressionType;
			if (this.NodeType == ExpressionType.PreIncrementAssign || this.NodeType == ExpressionType.PostIncrementAssign)
			{
				expressionType = ExpressionType.Increment;
			}
			else
			{
				expressionType = ExpressionType.Decrement;
			}
			return new UnaryExpression(expressionType, operand, operand.Type, this.Method);
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x0003DE30 File Offset: 0x0003C030
		private Expression ReduceVariable()
		{
			if (this.IsPrefix)
			{
				return Expression.Assign(this.Operand, this.FunctionalOp(this.Operand));
			}
			ParameterExpression parameterExpression = Expression.Parameter(this.Operand.Type, null);
			return Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(new ParameterExpression[] { parameterExpression }), new TrueReadOnlyCollection<Expression>(new Expression[]
			{
				Expression.Assign(parameterExpression, this.Operand),
				Expression.Assign(this.Operand, this.FunctionalOp(parameterExpression)),
				parameterExpression
			}));
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x0003DEB8 File Offset: 0x0003C0B8
		private Expression ReduceMember()
		{
			MemberExpression memberExpression = (MemberExpression)this.Operand;
			if (memberExpression.Expression == null)
			{
				return this.ReduceVariable();
			}
			ParameterExpression parameterExpression = Expression.Parameter(memberExpression.Expression.Type, null);
			BinaryExpression binaryExpression = Expression.Assign(parameterExpression, memberExpression.Expression);
			memberExpression = Expression.MakeMemberAccess(parameterExpression, memberExpression.Member);
			if (this.IsPrefix)
			{
				return Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(new ParameterExpression[] { parameterExpression }), new TrueReadOnlyCollection<Expression>(new Expression[]
				{
					binaryExpression,
					Expression.Assign(memberExpression, this.FunctionalOp(memberExpression))
				}));
			}
			ParameterExpression parameterExpression2 = Expression.Parameter(memberExpression.Type, null);
			return Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(new ParameterExpression[] { parameterExpression, parameterExpression2 }), new TrueReadOnlyCollection<Expression>(new Expression[]
			{
				binaryExpression,
				Expression.Assign(parameterExpression2, memberExpression),
				Expression.Assign(memberExpression, this.FunctionalOp(parameterExpression2)),
				parameterExpression2
			}));
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x0003DF9C File Offset: 0x0003C19C
		private Expression ReduceIndex()
		{
			bool isPrefix = this.IsPrefix;
			IndexExpression indexExpression = (IndexExpression)this.Operand;
			int argumentCount = indexExpression.ArgumentCount;
			Expression[] array = new Expression[argumentCount + (isPrefix ? 2 : 4)];
			ParameterExpression[] array2 = new ParameterExpression[argumentCount + (isPrefix ? 1 : 2)];
			ParameterExpression[] array3 = new ParameterExpression[argumentCount];
			int i = 0;
			array2[i] = Expression.Parameter(indexExpression.Object.Type, null);
			array[i] = Expression.Assign(array2[i], indexExpression.Object);
			for (i++; i <= argumentCount; i++)
			{
				Expression argument = indexExpression.GetArgument(i - 1);
				array3[i - 1] = (array2[i] = Expression.Parameter(argument.Type, null));
				array[i] = Expression.Assign(array2[i], argument);
			}
			indexExpression = Expression.MakeIndex(array2[0], indexExpression.Indexer, new TrueReadOnlyCollection<Expression>(array3));
			if (!isPrefix)
			{
				ParameterExpression parameterExpression = (array2[i] = Expression.Parameter(indexExpression.Type, null));
				array[i] = Expression.Assign(array2[i], indexExpression);
				i++;
				array[i++] = Expression.Assign(indexExpression, this.FunctionalOp(parameterExpression));
				array[i++] = parameterExpression;
			}
			else
			{
				array[i++] = Expression.Assign(indexExpression, this.FunctionalOp(indexExpression));
			}
			return Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(array2), new TrueReadOnlyCollection<Expression>(array));
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="operand">The <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property of the result.</param>
		// Token: 0x060014D8 RID: 5336 RVA: 0x0003E0F7 File Offset: 0x0003C2F7
		public UnaryExpression Update(Expression operand)
		{
			if (operand == this.Operand)
			{
				return this;
			}
			return Expression.MakeUnary(this.NodeType, operand, this.Type, this.Method);
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x0000220F File Offset: 0x0000040F
		internal UnaryExpression()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
