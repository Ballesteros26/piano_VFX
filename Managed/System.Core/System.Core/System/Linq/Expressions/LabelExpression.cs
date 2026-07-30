using System;
using System.Diagnostics;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents a label, which can be put in any <see cref="T:System.Linq.Expressions.Expression" /> context. If it is jumped to, it will get the value provided by the corresponding <see cref="T:System.Linq.Expressions.GotoExpression" />. Otherwise, it receives the value in <see cref="P:System.Linq.Expressions.LabelExpression.DefaultValue" />. If the <see cref="T:System.Type" /> equals System.Void, no value should be provided.</summary>
	// Token: 0x02000280 RID: 640
	[DebuggerTypeProxy(typeof(Expression.LabelExpressionProxy))]
	public sealed class LabelExpression : Expression
	{
		// Token: 0x060012BB RID: 4795 RVA: 0x0003B41F File Offset: 0x0003961F
		internal LabelExpression(LabelTarget label, Expression defaultValue)
		{
			this.Target = label;
			this.DefaultValue = defaultValue;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.LabelExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x17000318 RID: 792
		// (get) Token: 0x060012BC RID: 4796 RVA: 0x0003B435 File Offset: 0x00039635
		public sealed override Type Type
		{
			get
			{
				return this.Target.Type;
			}
		}

		/// <summary>Returns the node type of this <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> that represents this expression.</returns>
		// Token: 0x17000319 RID: 793
		// (get) Token: 0x060012BD RID: 4797 RVA: 0x0003B442 File Offset: 0x00039642
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Label;
			}
		}

		/// <summary>The <see cref="T:System.Linq.Expressions.LabelTarget" /> which this label is associated with.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.LabelTarget" /> which this label is associated with.</returns>
		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060012BE RID: 4798 RVA: 0x0003B446 File Offset: 0x00039646
		public LabelTarget Target { get; }

		/// <summary>The value of the <see cref="T:System.Linq.Expressions.LabelExpression" /> when the label is reached through regular control flow (for example, is not jumped to).</summary>
		/// <returns>The Expression object representing the value of the <see cref="T:System.Linq.Expressions.LabelExpression" />.</returns>
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060012BF RID: 4799 RVA: 0x0003B44E File Offset: 0x0003964E
		public Expression DefaultValue { get; }

		// Token: 0x060012C0 RID: 4800 RVA: 0x0003B456 File Offset: 0x00039656
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitLabel(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="target">The <see cref="P:System.Linq.Expressions.LabelExpression.Target" /> property of the result.</param>
		/// <param name="defaultValue">The <see cref="P:System.Linq.Expressions.LabelExpression.DefaultValue" /> property of the result</param>
		// Token: 0x060012C1 RID: 4801 RVA: 0x0003B45F File Offset: 0x0003965F
		public LabelExpression Update(LabelTarget target, Expression defaultValue)
		{
			if (target == this.Target && defaultValue == this.DefaultValue)
			{
				return this;
			}
			return Expression.Label(target, defaultValue);
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x0000220F File Offset: 0x0000040F
		internal LabelExpression()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
