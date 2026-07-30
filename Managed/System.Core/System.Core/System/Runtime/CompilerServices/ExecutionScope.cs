using System;
using System.Linq.Expressions;

namespace System.Runtime.CompilerServices
{
	/// <summary>Represents the runtime state of a dynamically generated method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000304 RID: 772
	[Obsolete("do not use this type", true)]
	public class ExecutionScope
	{
		// Token: 0x06001773 RID: 6003 RVA: 0x0004CE1A File Offset: 0x0004B01A
		internal ExecutionScope()
		{
			this.Parent = null;
			this.Globals = null;
			this.Locals = null;
		}

		/// <summary>Creates an array to store the hoisted local variables.</summary>
		/// <returns>An array to store hoisted local variables.</returns>
		// Token: 0x06001774 RID: 6004 RVA: 0x00003CCF File Offset: 0x00001ECF
		public object[] CreateHoistedLocals()
		{
			throw new NotSupportedException();
		}

		/// <summary>Creates a delegate that can be used to execute a dynamically generated method.</summary>
		/// <returns>A <see cref="T:System.Delegate" /> that can execute a dynamically generated method.</returns>
		/// <param name="indexLambda">The index of the object that stores information about associated lambda expression of the dynamic method.</param>
		/// <param name="locals">An array that contains the hoisted local variables from the parent context.</param>
		// Token: 0x06001775 RID: 6005 RVA: 0x00003CCF File Offset: 0x00001ECF
		public Delegate CreateDelegate(int indexLambda, object[] locals)
		{
			throw new NotSupportedException();
		}

		/// <summary>Frees a specified expression tree of external parameter references by replacing the parameter with its current value.</summary>
		/// <returns>An expression tree that does not contain external parameter references.</returns>
		/// <param name="expression">An expression tree to free of external parameter references.</param>
		/// <param name="locals">An array that contains the hoisted local variables.</param>
		// Token: 0x06001776 RID: 6006 RVA: 0x00003CCF File Offset: 0x00001ECF
		public Expression IsolateExpression(Expression expression, object[] locals)
		{
			throw new NotSupportedException();
		}

		/// <summary>Represents the execution scope of the calling delegate.</summary>
		// Token: 0x04000AD1 RID: 2769
		public ExecutionScope Parent;

		/// <summary>Represents the non-trivial constants and locally executable expressions that are referenced by a dynamically generated method.</summary>
		// Token: 0x04000AD2 RID: 2770
		public object[] Globals;

		/// <summary>Represents the hoisted local variables from the parent context.</summary>
		// Token: 0x04000AD3 RID: 2771
		public object[] Locals;
	}
}
