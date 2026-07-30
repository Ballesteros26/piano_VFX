using System;
using System.Diagnostics;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents an expression that has a constant value.</summary>
	// Token: 0x02000258 RID: 600
	[DebuggerTypeProxy(typeof(Expression.ConstantExpressionProxy))]
	public class ConstantExpression : Expression
	{
		// Token: 0x0600107C RID: 4220 RVA: 0x00035EB3 File Offset: 0x000340B3
		internal ConstantExpression(object value)
		{
			this.Value = value;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.ConstantExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x170002CE RID: 718
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x00035EC2 File Offset: 0x000340C2
		public override Type Type
		{
			get
			{
				if (this.Value == null)
				{
					return typeof(object);
				}
				return this.Value.GetType();
			}
		}

		/// <summary>Returns the node type of this Expression. Extension nodes should return <see cref="F:System.Linq.Expressions.ExpressionType.Extension" /> when overriding this method.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> of the expression.</returns>
		// Token: 0x170002CF RID: 719
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x00035EE2 File Offset: 0x000340E2
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Constant;
			}
		}

		/// <summary>Gets the value of the constant expression.</summary>
		/// <returns>An <see cref="T:System.Object" /> equal to the value of the represented expression.</returns>
		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x0600107F RID: 4223 RVA: 0x00035EE6 File Offset: 0x000340E6
		public object Value { get; }

		/// <summary>Dispatches to the specific visit method for this node type. For example, <see cref="T:System.Linq.Expressions.MethodCallExpression" /> calls the <see cref="M:System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(System.Linq.Expressions.MethodCallExpression)" />.</summary>
		/// <returns>The result of visiting this node.</returns>
		/// <param name="visitor">The visitor to visit this node with.</param>
		// Token: 0x06001080 RID: 4224 RVA: 0x00035EEE File Offset: 0x000340EE
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitConstant(this);
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x0000220F File Offset: 0x0000040F
		internal ConstantExpression()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
