using System;
using System.Linq;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides an extension method for objects that implement the <see cref="T:System.Linq.IQueryable`1" /> interface.</summary>
	// Token: 0x0200079C RID: 1948
	public static class QueryExtensions
	{
		/// <summary>Specifies a sort order.</summary>
		/// <returns>The sorted version of the <paramref name="source" /> object.</returns>
		/// <param name="source">The object to sort.</param>
		/// <param name="sortExpression">The sort expression.</param>
		/// <typeparam name="T">The generic type of the object.</typeparam>
		// Token: 0x06004E9B RID: 20123 RVA: 0x0000FAB7 File Offset: 0x0000DCB7
		public static IQueryable<T> SortBy<T>(this IQueryable<T> source, string sortExpression)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}
	}
}
