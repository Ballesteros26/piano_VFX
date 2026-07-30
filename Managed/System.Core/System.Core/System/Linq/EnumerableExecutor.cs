using System;
using System.Linq.Expressions;

namespace System.Linq
{
	/// <summary>Represents an expression tree and provides functionality to execute the expression tree after rewriting it.</summary>
	// Token: 0x020000A3 RID: 163
	public abstract class EnumerableExecutor
	{
		// Token: 0x060004C9 RID: 1225
		internal abstract object ExecuteBoxed();

		// Token: 0x060004CA RID: 1226 RVA: 0x0000C403 File Offset: 0x0000A603
		internal static EnumerableExecutor Create(Expression expression)
		{
			return (EnumerableExecutor)Activator.CreateInstance(typeof(EnumerableExecutor<>).MakeGenericType(new Type[] { expression.Type }), new object[] { expression });
		}
	}
}
