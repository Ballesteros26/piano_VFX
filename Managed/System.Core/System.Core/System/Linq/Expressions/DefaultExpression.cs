using System;
using System.Diagnostics;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents the default value of a type or an empty expression.</summary>
	// Token: 0x0200025F RID: 607
	[DebuggerTypeProxy(typeof(Expression.DefaultExpressionProxy))]
	public sealed class DefaultExpression : Expression
	{
		// Token: 0x060010E7 RID: 4327 RVA: 0x00037995 File Offset: 0x00035B95
		internal DefaultExpression(Type type)
		{
			this.Type = type;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.DefaultExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x060010E8 RID: 4328 RVA: 0x000379A4 File Offset: 0x00035BA4
		public sealed override Type Type { get; }

		/// <summary>Returns the node type of this expression. Extension nodes should return <see cref="F:System.Linq.Expressions.ExpressionType.Extension" /> when overriding this method.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> of the expression.</returns>
		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x060010E9 RID: 4329 RVA: 0x000379AC File Offset: 0x00035BAC
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Default;
			}
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x000379B0 File Offset: 0x00035BB0
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitDefault(this);
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x0000220F File Offset: 0x0000040F
		internal DefaultExpression()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
