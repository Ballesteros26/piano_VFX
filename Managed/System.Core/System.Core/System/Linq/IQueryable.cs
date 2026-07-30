using System;
using System.Collections;
using System.Linq.Expressions;

namespace System.Linq
{
	/// <summary>Provides functionality to evaluate queries against a specific data source wherein the type of the data is not specified.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200008B RID: 139
	public interface IQueryable : IEnumerable
	{
		/// <summary>Gets the expression tree that is associated with the instance of <see cref="T:System.Linq.IQueryable" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> that is associated with this instance of <see cref="T:System.Linq.IQueryable" />.</returns>
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000340 RID: 832
		Expression Expression { get; }

		/// <summary>Gets the type of the element(s) that are returned when the expression tree associated with this instance of <see cref="T:System.Linq.IQueryable" /> is executed.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of the element(s) that are returned when the expression tree associated with this object is executed.</returns>
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000341 RID: 833
		Type ElementType { get; }

		/// <summary>Gets the query provider that is associated with this data source.</summary>
		/// <returns>The <see cref="T:System.Linq.IQueryProvider" /> that is associated with this data source.</returns>
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000342 RID: 834
		IQueryProvider Provider { get; }
	}
}
