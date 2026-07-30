using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Linq
{
	/// <summary>Represents an <see cref="T:System.Collections.Generic.IEnumerable`1" /> collection as an <see cref="T:System.Linq.IQueryable`1" /> data source.</summary>
	/// <typeparam name="T">The type of the data in the collection.</typeparam>
	// Token: 0x020000A6 RID: 166
	public class EnumerableQuery<T> : EnumerableQuery, IOrderedQueryable<T>, IQueryable<T>, IEnumerable<T>, IEnumerable, IQueryable, IOrderedQueryable, IQueryProvider
	{
		/// <summary>Gets the query provider that is associated with this instance.</summary>
		/// <returns>The query provider that is associated with this instance.</returns>
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x000021A0 File Offset: 0x000003A0
		IQueryProvider IQueryable.Provider
		{
			get
			{
				return this;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Linq.EnumerableQuery`1" /> class and associates it with an <see cref="T:System.Collections.Generic.IEnumerable`1" /> collection.</summary>
		/// <param name="enumerable">A collection to associate with the new instance.</param>
		// Token: 0x060004D5 RID: 1237 RVA: 0x0000C4A4 File Offset: 0x0000A6A4
		public EnumerableQuery(IEnumerable<T> enumerable)
		{
			this._enumerable = enumerable;
			this._expression = Expression.Constant(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Linq.EnumerableQuery`1" /> class and associates the instance with an expression tree.</summary>
		/// <param name="expression">An expression tree to associate with the new instance.</param>
		// Token: 0x060004D6 RID: 1238 RVA: 0x0000C4BF File Offset: 0x0000A6BF
		public EnumerableQuery(Expression expression)
		{
			this._expression = expression;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x0000C4CE File Offset: 0x0000A6CE
		internal override Expression Expression
		{
			get
			{
				return this._expression;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x0000C4D6 File Offset: 0x0000A6D6
		internal override IEnumerable Enumerable
		{
			get
			{
				return this._enumerable;
			}
		}

		/// <summary>Gets the expression tree that is associated with or that represents this instance.</summary>
		/// <returns>The expression tree that is associated with or that represents this instance.</returns>
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x0000C4CE File Offset: 0x0000A6CE
		Expression IQueryable.Expression
		{
			get
			{
				return this._expression;
			}
		}

		/// <summary>Gets the type of the data in the collection that this instance represents.</summary>
		/// <returns>The type of the data in the collection that this instance represents.</returns>
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x0000C4DE File Offset: 0x0000A6DE
		Type IQueryable.ElementType
		{
			get
			{
				return typeof(T);
			}
		}

		/// <summary>Constructs a new <see cref="T:System.Linq.EnumerableQuery`1" /> object and associates it with a specified expression tree that represents an <see cref="T:System.Linq.IQueryable" /> collection of data.</summary>
		/// <returns>An <see cref="T:System.Linq.EnumerableQuery`1" /> object that is associated with <paramref name="expression" />.</returns>
		/// <param name="expression">An expression tree that represents an <see cref="T:System.Linq.IQueryable" /> collection of data.</param>
		// Token: 0x060004DB RID: 1243 RVA: 0x0000C4EC File Offset: 0x0000A6EC
		IQueryable IQueryProvider.CreateQuery(Expression expression)
		{
			if (expression == null)
			{
				throw Error.ArgumentNull("expression");
			}
			Type type = TypeHelper.FindGenericType(typeof(IQueryable<>), expression.Type);
			if (type == null)
			{
				throw Error.ArgumentNotValid("expression");
			}
			return EnumerableQuery.Create(type.GetGenericArguments()[0], expression);
		}

		/// <summary>Constructs a new <see cref="T:System.Linq.EnumerableQuery`1" /> object and associates it with a specified expression tree that represents an <see cref="T:System.Linq.IQueryable`1" /> collection of data.</summary>
		/// <returns>An EnumerableQuery object that is associated with <paramref name="expression" />.</returns>
		/// <param name="expression">An expression tree to execute.</param>
		/// <typeparam name="S">The type of the data in the collection that <paramref name="expression" /> represents.</typeparam>
		// Token: 0x060004DC RID: 1244 RVA: 0x0000C53D File Offset: 0x0000A73D
		IQueryable<TElement> IQueryProvider.CreateQuery<TElement>(Expression expression)
		{
			if (expression == null)
			{
				throw Error.ArgumentNull("expression");
			}
			if (!typeof(IQueryable<TElement>).IsAssignableFrom(expression.Type))
			{
				throw Error.ArgumentNotValid("expression");
			}
			return new EnumerableQuery<TElement>(expression);
		}

		/// <summary>Executes an expression after rewriting it to call <see cref="T:System.Linq.Enumerable" /> methods instead of <see cref="T:System.Linq.Queryable" /> methods on any enumerable data sources that cannot be queried by <see cref="T:System.Linq.Queryable" /> methods.</summary>
		/// <returns>The value that results from executing <paramref name="expression" />.</returns>
		/// <param name="expression">An expression tree to execute.</param>
		// Token: 0x060004DD RID: 1245 RVA: 0x0000C575 File Offset: 0x0000A775
		object IQueryProvider.Execute(Expression expression)
		{
			if (expression == null)
			{
				throw Error.ArgumentNull("expression");
			}
			return EnumerableExecutor.Create(expression).ExecuteBoxed();
		}

		/// <summary>Executes an expression after rewriting it to call <see cref="T:System.Linq.Enumerable" /> methods instead of <see cref="T:System.Linq.Queryable" /> methods on any enumerable data sources that cannot be queried by <see cref="T:System.Linq.Queryable" /> methods.</summary>
		/// <returns>The value that results from executing <paramref name="expression" />.</returns>
		/// <param name="expression">An expression tree to execute.</param>
		/// <typeparam name="S">The type of the data in the collection that <paramref name="expression" /> represents.</typeparam>
		// Token: 0x060004DE RID: 1246 RVA: 0x0000C590 File Offset: 0x0000A790
		TElement IQueryProvider.Execute<TElement>(Expression expression)
		{
			if (expression == null)
			{
				throw Error.ArgumentNull("expression");
			}
			if (!typeof(TElement).IsAssignableFrom(expression.Type))
			{
				throw Error.ArgumentNotValid("expression");
			}
			return new EnumerableExecutor<TElement>(expression).Execute();
		}

		/// <summary>Returns an enumerator that can iterate through the associated <see cref="T:System.Collections.Generic.IEnumerable`1" /> collection, or, if it is null, through the collection that results from rewriting the associated expression tree as a query on an <see cref="T:System.Collections.Generic.IEnumerable`1" /> data source and executing it.</summary>
		/// <returns>An enumerator that can be used to iterate through the associated data source.</returns>
		// Token: 0x060004DF RID: 1247 RVA: 0x0000C5CD File Offset: 0x0000A7CD
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that can iterate through the associated <see cref="T:System.Collections.Generic.IEnumerable`1" /> collection, or, if it is null, through the collection that results from rewriting the associated expression tree as a query on an <see cref="T:System.Collections.Generic.IEnumerable`1" /> data source and executing it.</summary>
		/// <returns>An enumerator that can be used to iterate through the associated data source.</returns>
		// Token: 0x060004E0 RID: 1248 RVA: 0x0000C5CD File Offset: 0x0000A7CD
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000C5D8 File Offset: 0x0000A7D8
		private IEnumerator<T> GetEnumerator()
		{
			if (this._enumerable == null)
			{
				IEnumerable<T> enumerable = Expression.Lambda<Func<IEnumerable<T>>>(new EnumerableRewriter().Visit(this._expression), null).Compile()();
				if (enumerable == this)
				{
					throw Error.EnumeratingNullEnumerableExpression();
				}
				this._enumerable = enumerable;
			}
			return this._enumerable.GetEnumerator();
		}

		/// <summary>Returns a textual representation of the enumerable collection or, if it is null, of the expression tree that is associated with this instance.</summary>
		/// <returns>A textual representation of the enumerable collection or, if it is null, of the expression tree that is associated with this instance.</returns>
		// Token: 0x060004E2 RID: 1250 RVA: 0x0000C62C File Offset: 0x0000A82C
		public override string ToString()
		{
			ConstantExpression constantExpression = this._expression as ConstantExpression;
			if (constantExpression == null || constantExpression.Value != this)
			{
				return this._expression.ToString();
			}
			if (this._enumerable != null)
			{
				return this._enumerable.ToString();
			}
			return "null";
		}

		// Token: 0x040003B5 RID: 949
		private readonly Expression _expression;

		// Token: 0x040003B6 RID: 950
		private IEnumerable<T> _enumerable;
	}
}
