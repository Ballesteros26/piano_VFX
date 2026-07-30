using System;
using System.Linq.Expressions;

namespace System.Linq
{
	/// <summary>Represents an expression tree and provides functionality to execute the expression tree after rewriting it.</summary>
	/// <typeparam name="T">The data type of the value that results from executing the expression tree.</typeparam>
	// Token: 0x020000A4 RID: 164
	public class EnumerableExecutor<T> : EnumerableExecutor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Linq.EnumerableExecutor`1" /> class.</summary>
		/// <param name="expression">An expression tree to associate with the new instance.</param>
		// Token: 0x060004CC RID: 1228 RVA: 0x0000C437 File Offset: 0x0000A637
		public EnumerableExecutor(Expression expression)
		{
			this._expression = expression;
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0000C446 File Offset: 0x0000A646
		internal override object ExecuteBoxed()
		{
			return this.Execute();
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0000C453 File Offset: 0x0000A653
		internal T Execute()
		{
			return Expression.Lambda<Func<T>>(new EnumerableRewriter().Visit(this._expression), null).Compile()();
		}

		// Token: 0x040003B4 RID: 948
		private readonly Expression _expression;
	}
}
