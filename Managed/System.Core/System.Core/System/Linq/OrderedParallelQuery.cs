using System;
using System.Collections.Generic;
using System.Linq.Parallel;
using Unity;

namespace System.Linq
{
	/// <summary>Represents a sorted, parallel sequence.</summary>
	/// <typeparam name="TSource">The type of elements in the source collection.</typeparam>
	// Token: 0x02000094 RID: 148
	public class OrderedParallelQuery<TSource> : ParallelQuery<TSource>
	{
		// Token: 0x06000354 RID: 852 RVA: 0x0000869D File Offset: 0x0000689D
		internal OrderedParallelQuery(QueryOperator<TSource> sortOp)
			: base(sortOp.SpecifiedQuerySettings)
		{
			this._sortOp = sortOp;
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000355 RID: 853 RVA: 0x000086B2 File Offset: 0x000068B2
		internal QueryOperator<TSource> SortOperator
		{
			get
			{
				return this._sortOp;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000356 RID: 854 RVA: 0x000086BA File Offset: 0x000068BA
		internal IOrderedEnumerable<TSource> OrderedEnumerable
		{
			get
			{
				return (IOrderedEnumerable<TSource>)this._sortOp;
			}
		}

		/// <summary>Returns an enumerator that iterates through the sequence.</summary>
		/// <returns>An enumerator that iterates through the sequence.</returns>
		// Token: 0x06000357 RID: 855 RVA: 0x000086C7 File Offset: 0x000068C7
		public override IEnumerator<TSource> GetEnumerator()
		{
			return this._sortOp.GetEnumerator();
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000220F File Offset: 0x0000040F
		internal OrderedParallelQuery()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400031F RID: 799
		private QueryOperator<TSource> _sortOp;
	}
}
