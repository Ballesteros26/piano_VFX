using System;
using System.Reflection;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents assignment operation for a field or property of an object.</summary>
	// Token: 0x0200028C RID: 652
	public sealed class MemberAssignment : MemberBinding
	{
		// Token: 0x06001321 RID: 4897 RVA: 0x0003BB58 File Offset: 0x00039D58
		internal MemberAssignment(MemberInfo member, Expression expression)
			: base(MemberBindingType.Assignment, member)
		{
			this._expression = expression;
		}

		/// <summary>Gets the expression to assign to the field or property.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> that represents the value to assign to the field or property.</returns>
		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06001322 RID: 4898 RVA: 0x0003BB69 File Offset: 0x00039D69
		public Expression Expression
		{
			get
			{
				return this._expression;
			}
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="expression">The <see cref="P:System.Linq.Expressions.MemberAssignment.Expression" /> property of the result.</param>
		// Token: 0x06001323 RID: 4899 RVA: 0x0003BB71 File Offset: 0x00039D71
		public MemberAssignment Update(Expression expression)
		{
			if (expression == this.Expression)
			{
				return this;
			}
			return Expression.Bind(base.Member, expression);
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x00003C4C File Offset: 0x00001E4C
		internal override void ValidateAsDefinedHere(int index)
		{
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x0000220F File Offset: 0x0000040F
		internal MemberAssignment()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400098A RID: 2442
		private readonly Expression _expression;
	}
}
