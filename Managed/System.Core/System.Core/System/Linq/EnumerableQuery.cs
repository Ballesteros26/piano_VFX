using System;
using System.Collections;
using System.Linq.Expressions;

namespace System.Linq
{
	/// <summary>Represents an <see cref="T:System.Collections.IEnumerable" /> as an <see cref="T:System.Linq.EnumerableQuery" /> data source. </summary>
	// Token: 0x020000A5 RID: 165
	public abstract class EnumerableQuery
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060004CF RID: 1231
		internal abstract Expression Expression { get; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060004D0 RID: 1232
		internal abstract IEnumerable Enumerable { get; }

		// Token: 0x060004D1 RID: 1233 RVA: 0x0000C475 File Offset: 0x0000A675
		internal static IQueryable Create(Type elementType, IEnumerable sequence)
		{
			return (IQueryable)Activator.CreateInstance(typeof(EnumerableQuery<>).MakeGenericType(new Type[] { elementType }), new object[] { sequence });
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0000C475 File Offset: 0x0000A675
		internal static IQueryable Create(Type elementType, Expression expression)
		{
			return (IQueryable)Activator.CreateInstance(typeof(EnumerableQuery<>).MakeGenericType(new Type[] { elementType }), new object[] { expression });
		}
	}
}
