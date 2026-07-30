using System;
using System.Collections.Generic;

namespace System.Collections.Concurrent
{
	/// <summary>Represents a particular manner of splitting an orderable data source into multiple partitions.</summary>
	/// <typeparam name="TSource">Type of the elements in the collection.</typeparam>
	// Token: 0x02000A07 RID: 2567
	public abstract class OrderablePartitioner<TSource> : Partitioner<TSource>
	{
		/// <summary>Called from constructors in derived classes to initialize the <see cref="T:System.Collections.Concurrent.OrderablePartitioner`1" /> class with the specified constraints on the index keys.</summary>
		/// <param name="keysOrderedInEachPartition">Indicates whether the elements in each partition are yielded in the order of increasing keys.</param>
		/// <param name="keysOrderedAcrossPartitions">Indicates whether elements in an earlier partition always come before elements in a later partition. If true, each element in partition 0 has a smaller order key than any element in partition 1, each element in partition 1 has a smaller order key than any element in partition 2, and so on.</param>
		/// <param name="keysNormalized">Indicates whether keys are normalized. If true, all order keys are distinct integers in the range [0 .. numberOfElements-1]. If false, order keys must still be distinct, but only their relative order is considered, not their absolute values.</param>
		// Token: 0x06005F53 RID: 24403 RVA: 0x0013A4BC File Offset: 0x001386BC
		protected OrderablePartitioner(bool keysOrderedInEachPartition, bool keysOrderedAcrossPartitions, bool keysNormalized)
		{
			this.KeysOrderedInEachPartition = keysOrderedInEachPartition;
			this.KeysOrderedAcrossPartitions = keysOrderedAcrossPartitions;
			this.KeysNormalized = keysNormalized;
		}

		/// <summary>Partitions the underlying collection into the specified number of orderable partitions.</summary>
		/// <returns>A list containing <paramref name="partitionCount" /> enumerators.</returns>
		/// <param name="partitionCount">The number of partitions to create.</param>
		// Token: 0x06005F54 RID: 24404
		public abstract IList<IEnumerator<KeyValuePair<long, TSource>>> GetOrderablePartitions(int partitionCount);

		/// <summary>Creates an object that can partition the underlying collection into a variable number of partitions.</summary>
		/// <returns>An object that can create partitions over the underlying data source.</returns>
		/// <exception cref="T:System.NotSupportedException">Dynamic partitioning is not supported by this partitioner.</exception>
		// Token: 0x06005F55 RID: 24405 RVA: 0x0013A4D9 File Offset: 0x001386D9
		public virtual IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions()
		{
			throw new NotSupportedException("Dynamic partitions are not supported by this partitioner.");
		}

		/// <summary>Gets whether elements in each partition are yielded in the order of increasing keys.</summary>
		/// <returns>true if the elements in each partition are yielded in the order of increasing keys; otherwise false.</returns>
		// Token: 0x17001102 RID: 4354
		// (get) Token: 0x06005F56 RID: 24406 RVA: 0x0013A4E5 File Offset: 0x001386E5
		// (set) Token: 0x06005F57 RID: 24407 RVA: 0x0013A4ED File Offset: 0x001386ED
		public bool KeysOrderedInEachPartition { get; private set; }

		/// <summary>Gets whether elements in an earlier partition always come before elements in a later partition.</summary>
		/// <returns>true if the elements in an earlier partition always come before elements in a later partition; otherwise false.</returns>
		// Token: 0x17001103 RID: 4355
		// (get) Token: 0x06005F58 RID: 24408 RVA: 0x0013A4F6 File Offset: 0x001386F6
		// (set) Token: 0x06005F59 RID: 24409 RVA: 0x0013A4FE File Offset: 0x001386FE
		public bool KeysOrderedAcrossPartitions { get; private set; }

		/// <summary>Gets whether order keys are normalized.</summary>
		/// <returns>true if the keys are normalized; otherwise false.</returns>
		// Token: 0x17001104 RID: 4356
		// (get) Token: 0x06005F5A RID: 24410 RVA: 0x0013A507 File Offset: 0x00138707
		// (set) Token: 0x06005F5B RID: 24411 RVA: 0x0013A50F File Offset: 0x0013870F
		public bool KeysNormalized { get; private set; }

		/// <summary>Partitions the underlying collection into the given number of ordered partitions.</summary>
		/// <returns>A list containing <paramref name="partitionCount" /> enumerators.</returns>
		/// <param name="partitionCount">The number of partitions to create.</param>
		// Token: 0x06005F5C RID: 24412 RVA: 0x0013A518 File Offset: 0x00138718
		public override IList<IEnumerator<TSource>> GetPartitions(int partitionCount)
		{
			IList<IEnumerator<KeyValuePair<long, TSource>>> orderablePartitions = this.GetOrderablePartitions(partitionCount);
			if (orderablePartitions.Count != partitionCount)
			{
				throw new InvalidOperationException("GetPartitions returned an incorrect number of partitions.");
			}
			IEnumerator<TSource>[] array = new IEnumerator<TSource>[partitionCount];
			for (int i = 0; i < partitionCount; i++)
			{
				array[i] = new OrderablePartitioner<TSource>.EnumeratorDropIndices(orderablePartitions[i]);
			}
			return array;
		}

		/// <summary>Creates an object that can partition the underlying collection into a variable number of partitions.</summary>
		/// <returns>An object that can create partitions over the underlying data source.</returns>
		/// <exception cref="T:System.NotSupportedException">Dynamic partitioning is not supported by the base class. It must be implemented in derived classes.</exception>
		// Token: 0x06005F5D RID: 24413 RVA: 0x0013A564 File Offset: 0x00138764
		public override IEnumerable<TSource> GetDynamicPartitions()
		{
			return new OrderablePartitioner<TSource>.EnumerableDropIndices(this.GetOrderableDynamicPartitions());
		}

		// Token: 0x02000A08 RID: 2568
		private class EnumerableDropIndices : IEnumerable<TSource>, IEnumerable, IDisposable
		{
			// Token: 0x06005F5E RID: 24414 RVA: 0x0013A571 File Offset: 0x00138771
			public EnumerableDropIndices(IEnumerable<KeyValuePair<long, TSource>> source)
			{
				this._source = source;
			}

			// Token: 0x06005F5F RID: 24415 RVA: 0x0013A580 File Offset: 0x00138780
			public IEnumerator<TSource> GetEnumerator()
			{
				return new OrderablePartitioner<TSource>.EnumeratorDropIndices(this._source.GetEnumerator());
			}

			// Token: 0x06005F60 RID: 24416 RVA: 0x0013A592 File Offset: 0x00138792
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06005F61 RID: 24417 RVA: 0x0013A59C File Offset: 0x0013879C
			public void Dispose()
			{
				IDisposable disposable = this._source as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}

			// Token: 0x0400300E RID: 12302
			private readonly IEnumerable<KeyValuePair<long, TSource>> _source;
		}

		// Token: 0x02000A09 RID: 2569
		private class EnumeratorDropIndices : IEnumerator<TSource>, IDisposable, IEnumerator
		{
			// Token: 0x06005F62 RID: 24418 RVA: 0x0013A5BE File Offset: 0x001387BE
			public EnumeratorDropIndices(IEnumerator<KeyValuePair<long, TSource>> source)
			{
				this._source = source;
			}

			// Token: 0x06005F63 RID: 24419 RVA: 0x0013A5CD File Offset: 0x001387CD
			public bool MoveNext()
			{
				return this._source.MoveNext();
			}

			// Token: 0x17001105 RID: 4357
			// (get) Token: 0x06005F64 RID: 24420 RVA: 0x0013A5DC File Offset: 0x001387DC
			public TSource Current
			{
				get
				{
					KeyValuePair<long, TSource> keyValuePair = this._source.Current;
					return keyValuePair.Value;
				}
			}

			// Token: 0x17001106 RID: 4358
			// (get) Token: 0x06005F65 RID: 24421 RVA: 0x0013A5FC File Offset: 0x001387FC
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06005F66 RID: 24422 RVA: 0x0013A609 File Offset: 0x00138809
			public void Dispose()
			{
				this._source.Dispose();
			}

			// Token: 0x06005F67 RID: 24423 RVA: 0x0013A616 File Offset: 0x00138816
			public void Reset()
			{
				this._source.Reset();
			}

			// Token: 0x0400300F RID: 12303
			private readonly IEnumerator<KeyValuePair<long, TSource>> _source;
		}
	}
}
