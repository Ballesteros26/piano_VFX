using System;
using System.Diagnostics;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents a catch statement in a try block.</summary>
	// Token: 0x02000251 RID: 593
	[DebuggerTypeProxy(typeof(Expression.CatchBlockProxy))]
	public sealed class CatchBlock
	{
		// Token: 0x06001061 RID: 4193 RVA: 0x00035C46 File Offset: 0x00033E46
		internal CatchBlock(Type test, ParameterExpression variable, Expression body, Expression filter)
		{
			this.Test = test;
			this.Variable = variable;
			this.Body = body;
			this.Filter = filter;
		}

		/// <summary>Gets a reference to the <see cref="T:System.Exception" /> object caught by this handler.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ParameterExpression" /> object representing a reference to the <see cref="T:System.Exception" /> object caught by this handler.</returns>
		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x00035C6B File Offset: 0x00033E6B
		public ParameterExpression Variable { get; }

		/// <summary>Gets the type of <see cref="T:System.Exception" /> this handler catches.</summary>
		/// <returns>The <see cref="T:System.Type" /> object representing the type of <see cref="T:System.Exception" /> this handler catches.</returns>
		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06001063 RID: 4195 RVA: 0x00035C73 File Offset: 0x00033E73
		public Type Test { get; }

		/// <summary>Gets the body of the catch block.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> object representing the catch body.</returns>
		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x00035C7B File Offset: 0x00033E7B
		public Expression Body { get; }

		/// <summary>Gets the body of the <see cref="T:System.Linq.Expressions.CatchBlock" /> filter.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> object representing the body of the <see cref="T:System.Linq.Expressions.CatchBlock" /> filter.</returns>
		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x00035C83 File Offset: 0x00033E83
		public Expression Filter { get; }

		/// <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
		// Token: 0x06001066 RID: 4198 RVA: 0x00035C8B File Offset: 0x00033E8B
		public override string ToString()
		{
			return ExpressionStringBuilder.CatchBlockToString(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="variable">The <see cref="P:System.Linq.Expressions.CatchBlock.Variable" /> property of the result.</param>
		/// <param name="filter">The <see cref="P:System.Linq.Expressions.CatchBlock.Filter" /> property of the result.</param>
		/// <param name="body">The <see cref="P:System.Linq.Expressions.CatchBlock.Body" /> property of the result.</param>
		// Token: 0x06001067 RID: 4199 RVA: 0x00035C93 File Offset: 0x00033E93
		public CatchBlock Update(ParameterExpression variable, Expression filter, Expression body)
		{
			if (variable == this.Variable && filter == this.Filter && body == this.Body)
			{
				return this;
			}
			return Expression.MakeCatchBlock(this.Test, variable, body, filter);
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x0000220F File Offset: 0x0000040F
		internal CatchBlock()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
