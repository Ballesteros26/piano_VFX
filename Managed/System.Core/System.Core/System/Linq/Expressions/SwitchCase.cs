using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic.Utils;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents one case of a <see cref="T:System.Linq.Expressions.SwitchExpression" />.</summary>
	// Token: 0x020002B3 RID: 691
	[DebuggerTypeProxy(typeof(Expression.SwitchCaseProxy))]
	public sealed class SwitchCase
	{
		// Token: 0x06001496 RID: 5270 RVA: 0x0003D6F4 File Offset: 0x0003B8F4
		internal SwitchCase(Expression body, ReadOnlyCollection<Expression> testValues)
		{
			this.Body = body;
			this.TestValues = testValues;
		}

		/// <summary>Gets the values of this case. This case is selected for execution when the <see cref="P:System.Linq.Expressions.SwitchExpression.SwitchValue" /> matches any of these values.</summary>
		/// <returns>The read-only collection of the values for this case block.</returns>
		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001497 RID: 5271 RVA: 0x0003D70A File Offset: 0x0003B90A
		public ReadOnlyCollection<Expression> TestValues { get; }

		/// <summary>Gets the body of this case.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> object that represents the body of the case block.</returns>
		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001498 RID: 5272 RVA: 0x0003D712 File Offset: 0x0003B912
		public Expression Body { get; }

		/// <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
		// Token: 0x06001499 RID: 5273 RVA: 0x0003D71A File Offset: 0x0003B91A
		public override string ToString()
		{
			return ExpressionStringBuilder.SwitchCaseToString(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="testValues">The <see cref="P:System.Linq.Expressions.SwitchCase.TestValues" /> property of the result.</param>
		/// <param name="body">The <see cref="P:System.Linq.Expressions.SwitchCase.Body" /> property of the result.</param>
		// Token: 0x0600149A RID: 5274 RVA: 0x0003D722 File Offset: 0x0003B922
		public SwitchCase Update(IEnumerable<Expression> testValues, Expression body)
		{
			if (((body == this.Body) & (testValues != null)) && ExpressionUtils.SameElements<Expression>(ref testValues, this.TestValues))
			{
				return this;
			}
			return Expression.SwitchCase(body, testValues);
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x0000220F File Offset: 0x0000040F
		internal SwitchCase()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
