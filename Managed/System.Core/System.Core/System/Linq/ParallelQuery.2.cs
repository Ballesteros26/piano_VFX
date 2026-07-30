using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Parallel;
using Unity;

namespace System.Linq
{
	/// <summary>Represents a parallel sequence.</summary>
	/// <typeparam name="TSource">The type of element in the source sequence.</typeparam>
	// Token: 0x02000096 RID: 150
	public class ParallelQuery<TSource> : ParallelQuery, IEnumerable<TSource>, IEnumerable
	{
		// Token: 0x06000360 RID: 864 RVA: 0x000086F3 File Offset: 0x000068F3
		internal ParallelQuery(QuerySettings settings)
			: base(settings)
		{
		}

		// Token: 0x06000361 RID: 865 RVA: 0x000086FC File Offset: 0x000068FC
		internal sealed override ParallelQuery<TCastTo> Cast<TCastTo>()
		{
			return this.Select((TSource elem) => (TCastTo)((object)elem));
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00008724 File Offset: 0x00006924
		internal sealed override ParallelQuery<TCastTo> OfType<TCastTo>()
		{
			return from elem in this
				where elem is TCastTo
				select (TCastTo)((object)elem);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000877A File Offset: 0x0000697A
		internal override IEnumerator GetEnumeratorUntyped()
		{
			return ((IEnumerable<TSource>)this).GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through the sequence.</summary>
		/// <returns>An enumerator that iterates through the sequence.</returns>
		// Token: 0x06000364 RID: 868 RVA: 0x00003CCF File Offset: 0x00001ECF
		public virtual IEnumerator<TSource> GetEnumerator()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000220F File Offset: 0x0000040F
		internal ParallelQuery()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
