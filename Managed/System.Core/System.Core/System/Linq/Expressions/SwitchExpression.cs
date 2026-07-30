using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Reflection;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents a control expression that handles multiple selections by passing control to <see cref="T:System.Linq.Expressions.SwitchCase" />.</summary>
	// Token: 0x020002B4 RID: 692
	[DebuggerTypeProxy(typeof(Expression.SwitchExpressionProxy))]
	public sealed class SwitchExpression : Expression
	{
		// Token: 0x0600149C RID: 5276 RVA: 0x0003D74C File Offset: 0x0003B94C
		internal SwitchExpression(Type type, Expression switchValue, Expression defaultBody, MethodInfo comparison, ReadOnlyCollection<SwitchCase> cases)
		{
			this.Type = type;
			this.SwitchValue = switchValue;
			this.DefaultBody = defaultBody;
			this.Comparison = comparison;
			this.Cases = cases;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.SwitchExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x0600149D RID: 5277 RVA: 0x0003D779 File Offset: 0x0003B979
		public sealed override Type Type { get; }

		/// <summary>Returns the node type of this Expression. Extension nodes should return <see cref="F:System.Linq.Expressions.ExpressionType.Extension" /> when overriding this method.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> of the expression.</returns>
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x0003D781 File Offset: 0x0003B981
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Switch;
			}
		}

		/// <summary>Gets the test for the switch.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> object representing the test for the switch.</returns>
		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x0600149F RID: 5279 RVA: 0x0003D785 File Offset: 0x0003B985
		public Expression SwitchValue { get; }

		/// <summary>Gets the collection of <see cref="T:System.Linq.Expressions.SwitchCase" /> objects for the switch.</summary>
		/// <returns>The collection of <see cref="T:System.Linq.Expressions.SwitchCase" /> objects.</returns>
		// Token: 0x170003CA RID: 970
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x0003D78D File Offset: 0x0003B98D
		public ReadOnlyCollection<SwitchCase> Cases { get; }

		/// <summary>Gets the test for the switch.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> object representing the test for the switch.</returns>
		// Token: 0x170003CB RID: 971
		// (get) Token: 0x060014A1 RID: 5281 RVA: 0x0003D795 File Offset: 0x0003B995
		public Expression DefaultBody { get; }

		/// <summary>Gets the equality comparison method, if any.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodInfo" /> object representing the equality comparison method.</returns>
		// Token: 0x170003CC RID: 972
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x0003D79D File Offset: 0x0003B99D
		public MethodInfo Comparison { get; }

		// Token: 0x060014A3 RID: 5283 RVA: 0x0003D7A5 File Offset: 0x0003B9A5
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitSwitch(this);
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x0003D7B0 File Offset: 0x0003B9B0
		internal bool IsLifted
		{
			get
			{
				return this.SwitchValue.Type.IsNullableType() && (this.Comparison == null || !TypeUtils.AreEquivalent(this.SwitchValue.Type, this.Comparison.GetParametersCached()[0].ParameterType.GetNonRefType()));
			}
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="switchValue">The <see cref="P:System.Linq.Expressions.SwitchExpression.SwitchValue" /> property of the result.</param>
		/// <param name="cases">The <see cref="P:System.Linq.Expressions.SwitchExpression.Cases" /> property of the result.</param>
		/// <param name="defaultBody">The <see cref="P:System.Linq.Expressions.SwitchExpression.DefaultBody" /> property of the result.</param>
		// Token: 0x060014A5 RID: 5285 RVA: 0x0003D80C File Offset: 0x0003BA0C
		public SwitchExpression Update(Expression switchValue, IEnumerable<SwitchCase> cases, Expression defaultBody)
		{
			if (((switchValue == this.SwitchValue) & (defaultBody == this.DefaultBody) & (cases != null)) && ExpressionUtils.SameElements<SwitchCase>(ref cases, this.Cases))
			{
				return this;
			}
			return Expression.Switch(this.Type, switchValue, defaultBody, this.Comparison, cases);
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x0000220F File Offset: 0x0000040F
		internal SwitchExpression()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
