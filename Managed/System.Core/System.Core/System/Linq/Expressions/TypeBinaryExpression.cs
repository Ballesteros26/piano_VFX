using System;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents an operation between an expression and a type.</summary>
	// Token: 0x020002B8 RID: 696
	[DebuggerTypeProxy(typeof(Expression.TypeBinaryExpressionProxy))]
	public sealed class TypeBinaryExpression : Expression
	{
		// Token: 0x060014BE RID: 5310 RVA: 0x0003DA06 File Offset: 0x0003BC06
		internal TypeBinaryExpression(Expression expression, Type typeOperand, ExpressionType nodeType)
		{
			this.Expression = expression;
			this.TypeOperand = typeOperand;
			this.NodeType = nodeType;
		}

		/// <summary>Gets the static type of the expression that this <see cref="P:System.Linq.Expressions.TypeBinaryExpression.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.TypeBinaryExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x170003DB RID: 987
		// (get) Token: 0x060014BF RID: 5311 RVA: 0x0002D75D File Offset: 0x0002B95D
		public sealed override Type Type
		{
			get
			{
				return typeof(bool);
			}
		}

		/// <summary>Returns the node type of this Expression. Extension nodes should return <see cref="F:System.Linq.Expressions.ExpressionType.Extension" /> when overriding this method.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> of the expression.</returns>
		// Token: 0x170003DC RID: 988
		// (get) Token: 0x060014C0 RID: 5312 RVA: 0x0003DA23 File Offset: 0x0003BC23
		public sealed override ExpressionType NodeType { get; }

		/// <summary>Gets the expression operand of a type test operation.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the expression operand of a type test operation.</returns>
		// Token: 0x170003DD RID: 989
		// (get) Token: 0x060014C1 RID: 5313 RVA: 0x0003DA2B File Offset: 0x0003BC2B
		public Expression Expression { get; }

		/// <summary>Gets the type operand of a type test operation.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type operand of a type test operation.</returns>
		// Token: 0x170003DE RID: 990
		// (get) Token: 0x060014C2 RID: 5314 RVA: 0x0003DA33 File Offset: 0x0003BC33
		public Type TypeOperand { get; }

		// Token: 0x060014C3 RID: 5315 RVA: 0x0003DA3C File Offset: 0x0003BC3C
		internal Expression ReduceTypeEqual()
		{
			Type type = this.Expression.Type;
			if (type.IsValueType || this.TypeOperand.IsPointer)
			{
				if (!type.IsNullableType())
				{
					return Expression.Block(this.Expression, Utils.Constant(type == this.TypeOperand.GetNonNullableType()));
				}
				if (type.GetNonNullableType() != this.TypeOperand.GetNonNullableType())
				{
					return Expression.Block(this.Expression, Utils.Constant(false));
				}
				return Expression.NotEqual(this.Expression, Expression.Constant(null, this.Expression.Type));
			}
			else
			{
				if (this.Expression.NodeType == ExpressionType.Constant)
				{
					return this.ReduceConstantTypeEqual();
				}
				ParameterExpression parameterExpression = this.Expression as ParameterExpression;
				if (parameterExpression != null && !parameterExpression.IsByRef)
				{
					return this.ByValParameterTypeEqual(parameterExpression);
				}
				parameterExpression = Expression.Parameter(typeof(object));
				return Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(new ParameterExpression[] { parameterExpression }), new TrueReadOnlyCollection<Expression>(new Expression[]
				{
					Expression.Assign(parameterExpression, this.Expression),
					this.ByValParameterTypeEqual(parameterExpression)
				}));
			}
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x0003DB58 File Offset: 0x0003BD58
		private Expression ByValParameterTypeEqual(ParameterExpression value)
		{
			Expression expression = Expression.Call(value, CachedReflectionInfo.Object_GetType);
			if (this.TypeOperand.IsInterface)
			{
				ParameterExpression parameterExpression = Expression.Parameter(typeof(Type));
				expression = Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(new ParameterExpression[] { parameterExpression }), new TrueReadOnlyCollection<Expression>(new Expression[]
				{
					Expression.Assign(parameterExpression, expression),
					parameterExpression
				}));
			}
			return Expression.AndAlso(Expression.ReferenceNotEqual(value, Utils.Null), Expression.ReferenceEqual(expression, Expression.Constant(this.TypeOperand.GetNonNullableType(), typeof(Type))));
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x0003DBEC File Offset: 0x0003BDEC
		private Expression ReduceConstantTypeEqual()
		{
			ConstantExpression constantExpression = this.Expression as ConstantExpression;
			if (constantExpression.Value == null)
			{
				return Utils.Constant(false);
			}
			return Utils.Constant(this.TypeOperand.GetNonNullableType() == constantExpression.Value.GetType());
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x0003DC34 File Offset: 0x0003BE34
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitTypeBinary(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="expression">The <see cref="P:System.Linq.Expressions.TypeBinaryExpression.Expression" /> property of the result.</param>
		// Token: 0x060014C7 RID: 5319 RVA: 0x0003DC3D File Offset: 0x0003BE3D
		public TypeBinaryExpression Update(Expression expression)
		{
			if (expression == this.Expression)
			{
				return this;
			}
			if (this.NodeType == ExpressionType.TypeIs)
			{
				return Expression.TypeIs(expression, this.TypeOperand);
			}
			return Expression.TypeEqual(expression, this.TypeOperand);
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x0000220F File Offset: 0x0000040F
		internal TypeBinaryExpression()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
