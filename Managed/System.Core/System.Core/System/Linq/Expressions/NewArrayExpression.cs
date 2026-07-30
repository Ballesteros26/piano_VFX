using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic.Utils;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents creating a new array and possibly initializing the elements of the new array.</summary>
	// Token: 0x020002A3 RID: 675
	[DebuggerTypeProxy(typeof(Expression.NewArrayExpressionProxy))]
	public class NewArrayExpression : Expression
	{
		// Token: 0x060013AC RID: 5036 RVA: 0x0003CA61 File Offset: 0x0003AC61
		internal NewArrayExpression(Type type, ReadOnlyCollection<Expression> expressions)
		{
			this.Expressions = expressions;
			this.Type = type;
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x0003CA77 File Offset: 0x0003AC77
		internal static NewArrayExpression Make(ExpressionType nodeType, Type type, ReadOnlyCollection<Expression> expressions)
		{
			if (nodeType == ExpressionType.NewArrayInit)
			{
				return new NewArrayInitExpression(type, expressions);
			}
			return new NewArrayBoundsExpression(type, expressions);
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.NewArrayExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060013AE RID: 5038 RVA: 0x0003CA8D File Offset: 0x0003AC8D
		public sealed override Type Type { get; }

		/// <summary>Gets the bounds of the array if the value of the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property is <see cref="F:System.Linq.Expressions.ExpressionType.NewArrayBounds" />, or the values to initialize the elements of the new array if the value of the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property is <see cref="F:System.Linq.Expressions.ExpressionType.NewArrayInit" />.</summary>
		/// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Linq.Expressions.Expression" /> objects which represent either the bounds of the array or the initialization values.</returns>
		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x0003CA95 File Offset: 0x0003AC95
		public ReadOnlyCollection<Expression> Expressions { get; }

		/// <summary>Dispatches to the specific visit method for this node type. For example, <see cref="T:System.Linq.Expressions.MethodCallExpression" /> calls the <see cref="M:System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(System.Linq.Expressions.MethodCallExpression)" />.</summary>
		/// <returns>The result of visiting this node.</returns>
		/// <param name="visitor">The visitor to visit this node with.</param>
		// Token: 0x060013B0 RID: 5040 RVA: 0x0003CA9D File Offset: 0x0003AC9D
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitNewArray(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="expressions">The <see cref="P:System.Linq.Expressions.NewArrayExpression.Expressions" /> property of the result.</param>
		// Token: 0x060013B1 RID: 5041 RVA: 0x0003CAA8 File Offset: 0x0003ACA8
		public NewArrayExpression Update(IEnumerable<Expression> expressions)
		{
			ContractUtils.RequiresNotNull(expressions, "expressions");
			if (ExpressionUtils.SameElements<Expression>(ref expressions, this.Expressions))
			{
				return this;
			}
			if (this.NodeType != ExpressionType.NewArrayInit)
			{
				return Expression.NewArrayBounds(this.Type.GetElementType(), expressions);
			}
			return Expression.NewArrayInit(this.Type.GetElementType(), expressions);
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x0000220F File Offset: 0x0000040F
		internal NewArrayExpression()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
