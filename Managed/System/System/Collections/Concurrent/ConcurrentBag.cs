using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace System.Collections.Concurrent
{
	/// <summary>Represents a thread-safe, unordered collection of objects.</summary>
	/// <typeparam name="T">The type of the elements to be stored in the collection.</typeparam>
	// Token: 0x020006ED RID: 1773
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(IProducerConsumerCollectionDebugView<>))]
	[Serializable]
	public class ConcurrentBag<T> : IProducerConsumerCollection<T>, IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> class.</summary>
		// Token: 0x06003784 RID: 14212 RVA: 0x000CC66C File Offset: 0x000CA86C
		public ConcurrentBag()
		{
			this._locals = new ThreadLocal<ConcurrentBag<T>.WorkStealingQueue>();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> class that contains elements copied from the specified collection.</summary>
		/// <param name="collection">The collection whose elements are copied to the new <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> is a null reference (Nothing in Visual Basic).</exception>
		// Token: 0x06003785 RID: 14213 RVA: 0x000CC680 File Offset: 0x000CA880
		public ConcurrentBag(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection", "The collection argument is null.");
			}
			this._locals = new ThreadLocal<ConcurrentBag<T>.WorkStealingQueue>();
			ConcurrentBag<T>.WorkStealingQueue currentThreadWorkStealingQueue = this.GetCurrentThreadWorkStealingQueue(true);
			foreach (T t in collection)
			{
				currentThreadWorkStealingQueue.LocalPush(t);
			}
		}

		/// <summary>Adds an object to the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</summary>
		/// <param name="item">The object to be added to the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />. The value can be a null reference (Nothing in Visual Basic) for reference types.</param>
		// Token: 0x06003786 RID: 14214 RVA: 0x000CC6F4 File Offset: 0x000CA8F4
		public void Add(T item)
		{
			this.GetCurrentThreadWorkStealingQueue(true).LocalPush(item);
		}

		/// <summary>Attempts to add an object to the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</summary>
		/// <returns>Always returns true</returns>
		/// <param name="item">The object to be added to the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />. The value can be a null reference (Nothing in Visual Basic) for reference types.</param>
		// Token: 0x06003787 RID: 14215 RVA: 0x000CC703 File Offset: 0x000CA903
		bool IProducerConsumerCollection<T>.TryAdd(T item)
		{
			this.Add(item);
			return true;
		}

		/// <summary>Attempts to remove and return an object from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</summary>
		/// <returns>true if an object was removed successfully; otherwise, false.</returns>
		/// <param name="result">When this method returns, <paramref name="result" /> contains the object removed from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> or the default value of <paramref name="T" /> if the bag is empty.</param>
		// Token: 0x06003788 RID: 14216 RVA: 0x000CC710 File Offset: 0x000CA910
		public bool TryTake(out T result)
		{
			ConcurrentBag<T>.WorkStealingQueue currentThreadWorkStealingQueue = this.GetCurrentThreadWorkStealingQueue(false);
			return (currentThreadWorkStealingQueue != null && currentThreadWorkStealingQueue.TryLocalPop(out result)) || this.TrySteal(out result, true);
		}

		/// <summary>Attempts to return an object from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> without removing it.</summary>
		/// <returns>true if and object was returned successfully; otherwise, false.</returns>
		/// <param name="result">When this method returns, <paramref name="result" /> contains an object from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> or the default value of <paramref name="T" /> if the operation failed.</param>
		// Token: 0x06003789 RID: 14217 RVA: 0x000CC73C File Offset: 0x000CA93C
		public bool TryPeek(out T result)
		{
			ConcurrentBag<T>.WorkStealingQueue currentThreadWorkStealingQueue = this.GetCurrentThreadWorkStealingQueue(false);
			return (currentThreadWorkStealingQueue != null && currentThreadWorkStealingQueue.TryLocalPeek(out result)) || this.TrySteal(out result, false);
		}

		// Token: 0x0600378A RID: 14218 RVA: 0x000CC767 File Offset: 0x000CA967
		private ConcurrentBag<T>.WorkStealingQueue GetCurrentThreadWorkStealingQueue(bool forceCreate)
		{
			ConcurrentBag<T>.WorkStealingQueue workStealingQueue;
			if ((workStealingQueue = this._locals.Value) == null)
			{
				if (!forceCreate)
				{
					return null;
				}
				workStealingQueue = this.CreateWorkStealingQueueForCurrentThread();
			}
			return workStealingQueue;
		}

		// Token: 0x0600378B RID: 14219 RVA: 0x000CC784 File Offset: 0x000CA984
		private ConcurrentBag<T>.WorkStealingQueue CreateWorkStealingQueueForCurrentThread()
		{
			object globalQueuesLock = this.GlobalQueuesLock;
			ConcurrentBag<T>.WorkStealingQueue workStealingQueue2;
			lock (globalQueuesLock)
			{
				ConcurrentBag<T>.WorkStealingQueue workStealingQueues = this._workStealingQueues;
				ConcurrentBag<T>.WorkStealingQueue workStealingQueue = ((workStealingQueues != null) ? this.GetUnownedWorkStealingQueue() : null);
				if (workStealingQueue == null)
				{
					workStealingQueue = (this._workStealingQueues = new ConcurrentBag<T>.WorkStealingQueue(workStealingQueues));
				}
				this._locals.Value = workStealingQueue;
				workStealingQueue2 = workStealingQueue;
			}
			return workStealingQueue2;
		}

		// Token: 0x0600378C RID: 14220 RVA: 0x000CC7F8 File Offset: 0x000CA9F8
		private ConcurrentBag<T>.WorkStealingQueue GetUnownedWorkStealingQueue()
		{
			int currentManagedThreadId = Environment.CurrentManagedThreadId;
			for (ConcurrentBag<T>.WorkStealingQueue workStealingQueue = this._workStealingQueues; workStealingQueue != null; workStealingQueue = workStealingQueue._nextQueue)
			{
				if (workStealingQueue._ownerThreadId == currentManagedThreadId)
				{
					return workStealingQueue;
				}
			}
			return null;
		}

		// Token: 0x0600378D RID: 14221 RVA: 0x000CC82C File Offset: 0x000CAA2C
		private bool TrySteal(out T result, bool take)
		{
			if (take)
			{
				CDSCollectionETWBCLProvider.Log.ConcurrentBag_TryTakeSteals();
			}
			else
			{
				CDSCollectionETWBCLProvider.Log.ConcurrentBag_TryPeekSteals();
			}
			ConcurrentBag<T>.WorkStealingQueue currentThreadWorkStealingQueue = this.GetCurrentThreadWorkStealingQueue(false);
			if (currentThreadWorkStealingQueue == null)
			{
				return this.TryStealFromTo(this._workStealingQueues, null, out result, take);
			}
			return this.TryStealFromTo(currentThreadWorkStealingQueue._nextQueue, null, out result, take) || this.TryStealFromTo(this._workStealingQueues, currentThreadWorkStealingQueue, out result, take);
		}

		// Token: 0x0600378E RID: 14222 RVA: 0x000CC894 File Offset: 0x000CAA94
		private bool TryStealFromTo(ConcurrentBag<T>.WorkStealingQueue startInclusive, ConcurrentBag<T>.WorkStealingQueue endExclusive, out T result, bool take)
		{
			for (ConcurrentBag<T>.WorkStealingQueue workStealingQueue = startInclusive; workStealingQueue != endExclusive; workStealingQueue = workStealingQueue._nextQueue)
			{
				if (workStealingQueue.TrySteal(out result, take))
				{
					return true;
				}
			}
			result = default(T);
			return false;
		}

		/// <summary>Copies the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> elements to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> is equal to or greater than the length of the <paramref name="array" /> -or- the number of elements in the source <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x0600378F RID: 14223 RVA: 0x000CC8C8 File Offset: 0x000CAAC8
		public void CopyTo(T[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", "The array argument is null.");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "The index argument must be greater than or equal zero.");
			}
			if (this._workStealingQueues == null)
			{
				return;
			}
			bool flag = false;
			try
			{
				this.FreezeBag(ref flag);
				int dangerousCount = this.DangerousCount;
				if (index > array.Length - dangerousCount)
				{
					throw new ArgumentException("The number of elements in the collection is greater than the available space from index to the end of the destination array.", "index");
				}
				try
				{
					this.CopyFromEachQueueToArray(array, index);
				}
				catch (ArrayTypeMismatchException ex)
				{
					throw new InvalidCastException(ex.Message, ex);
				}
			}
			finally
			{
				this.UnfreezeBag(flag);
			}
		}

		// Token: 0x06003790 RID: 14224 RVA: 0x000CC970 File Offset: 0x000CAB70
		private int CopyFromEachQueueToArray(T[] array, int index)
		{
			int num = index;
			for (ConcurrentBag<T>.WorkStealingQueue workStealingQueue = this._workStealingQueues; workStealingQueue != null; workStealingQueue = workStealingQueue._nextQueue)
			{
				num += workStealingQueue.DangerousCopyTo(array, num);
			}
			return num - index;
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional. -or- <paramref name="array" /> does not have zero-based indexing. -or- <paramref name="index" /> is equal to or greater than the length of the <paramref name="array" /> -or- The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. -or- The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06003791 RID: 14225 RVA: 0x000CC9A4 File Offset: 0x000CABA4
		void ICollection.CopyTo(Array array, int index)
		{
			T[] array2 = array as T[];
			if (array2 != null)
			{
				this.CopyTo(array2, index);
				return;
			}
			if (array == null)
			{
				throw new ArgumentNullException("array", "The array argument is null.");
			}
			this.ToArray().CopyTo(array, index);
		}

		/// <summary>Copies the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> elements to a new array.</summary>
		/// <returns>A new array containing a snapshot of elements copied from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</returns>
		// Token: 0x06003792 RID: 14226 RVA: 0x000CC9E4 File Offset: 0x000CABE4
		public T[] ToArray()
		{
			if (this._workStealingQueues != null)
			{
				bool flag = false;
				try
				{
					this.FreezeBag(ref flag);
					int dangerousCount = this.DangerousCount;
					if (dangerousCount > 0)
					{
						T[] array = new T[dangerousCount];
						this.CopyFromEachQueueToArray(array, 0);
						return array;
					}
				}
				finally
				{
					this.UnfreezeBag(flag);
				}
			}
			return Array.Empty<T>();
		}

		// Token: 0x06003793 RID: 14227 RVA: 0x000CCA48 File Offset: 0x000CAC48
		public void Clear()
		{
			if (this._workStealingQueues == null)
			{
				return;
			}
			ConcurrentBag<T>.WorkStealingQueue currentThreadWorkStealingQueue = this.GetCurrentThreadWorkStealingQueue(false);
			if (currentThreadWorkStealingQueue != null)
			{
				currentThreadWorkStealingQueue.LocalClear();
				if (currentThreadWorkStealingQueue._nextQueue == null && currentThreadWorkStealingQueue == this._workStealingQueues)
				{
					return;
				}
			}
			bool flag = false;
			try
			{
				this.FreezeBag(ref flag);
				for (ConcurrentBag<T>.WorkStealingQueue workStealingQueue = this._workStealingQueues; workStealingQueue != null; workStealingQueue = workStealingQueue._nextQueue)
				{
					T t;
					while (workStealingQueue.TrySteal(out t, true))
					{
					}
				}
			}
			finally
			{
				this.UnfreezeBag(flag);
			}
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</summary>
		/// <returns>An enumerator for the contents of the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</returns>
		// Token: 0x06003794 RID: 14228 RVA: 0x000CCACC File Offset: 0x000CACCC
		public IEnumerator<T> GetEnumerator()
		{
			return new ConcurrentBag<T>.Enumerator(this.ToArray());
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</summary>
		/// <returns>An enumerator for the contents of the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</returns>
		// Token: 0x06003795 RID: 14229 RVA: 0x000CCAD9 File Offset: 0x000CACD9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</returns>
		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x06003796 RID: 14230 RVA: 0x000CCAE4 File Offset: 0x000CACE4
		public int Count
		{
			get
			{
				if (this._workStealingQueues == null)
				{
					return 0;
				}
				bool flag = false;
				int dangerousCount;
				try
				{
					this.FreezeBag(ref flag);
					dangerousCount = this.DangerousCount;
				}
				finally
				{
					this.UnfreezeBag(flag);
				}
				return dangerousCount;
			}
		}

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x06003797 RID: 14231 RVA: 0x000CCB2C File Offset: 0x000CAD2C
		private int DangerousCount
		{
			get
			{
				int num = 0;
				checked
				{
					for (ConcurrentBag<T>.WorkStealingQueue workStealingQueue = this._workStealingQueues; workStealingQueue != null; workStealingQueue = workStealingQueue._nextQueue)
					{
						num += workStealingQueue.DangerousCount;
					}
					return num;
				}
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> is empty.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> is empty; otherwise, false.</returns>
		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x06003798 RID: 14232 RVA: 0x000CCB5C File Offset: 0x000CAD5C
		public bool IsEmpty
		{
			get
			{
				ConcurrentBag<T>.WorkStealingQueue currentThreadWorkStealingQueue = this.GetCurrentThreadWorkStealingQueue(false);
				if (currentThreadWorkStealingQueue != null)
				{
					if (!currentThreadWorkStealingQueue.IsEmpty)
					{
						return false;
					}
					if (currentThreadWorkStealingQueue._nextQueue == null && currentThreadWorkStealingQueue == this._workStealingQueues)
					{
						return true;
					}
				}
				bool flag = false;
				try
				{
					this.FreezeBag(ref flag);
					for (ConcurrentBag<T>.WorkStealingQueue workStealingQueue = this._workStealingQueues; workStealingQueue != null; workStealingQueue = workStealingQueue._nextQueue)
					{
						if (!workStealingQueue.IsEmpty)
						{
							return false;
						}
					}
				}
				finally
				{
					this.UnfreezeBag(flag);
				}
				return true;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized with the SyncRoot.</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized with the SyncRoot; otherwise, false. For <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />, this property always returns false.</returns>
		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x06003799 RID: 14233 RVA: 0x00004240 File Offset: 0x00002440
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />. This property is not supported.</summary>
		/// <returns>Returns null  (Nothing in Visual Basic).</returns>
		/// <exception cref="T:System.NotSupportedException">The SyncRoot property is not supported.</exception>
		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x0600379A RID: 14234 RVA: 0x000CB6E3 File Offset: 0x000C98E3
		object ICollection.SyncRoot
		{
			get
			{
				throw new NotSupportedException("The SyncRoot property may not be used for the synchronization of concurrent collections.");
			}
		}

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x0600379B RID: 14235 RVA: 0x000CCBDC File Offset: 0x000CADDC
		private object GlobalQueuesLock
		{
			get
			{
				return this._locals;
			}
		}

		// Token: 0x0600379C RID: 14236 RVA: 0x000CCBE4 File Offset: 0x000CADE4
		private void FreezeBag(ref bool lockTaken)
		{
			Monitor.Enter(this.GlobalQueuesLock, ref lockTaken);
			ConcurrentBag<T>.WorkStealingQueue workStealingQueues = this._workStealingQueues;
			for (ConcurrentBag<T>.WorkStealingQueue workStealingQueue = workStealingQueues; workStealingQueue != null; workStealingQueue = workStealingQueue._nextQueue)
			{
				Monitor.Enter(workStealingQueue, ref workStealingQueue._frozen);
			}
			Interlocked.MemoryBarrier();
			for (ConcurrentBag<T>.WorkStealingQueue workStealingQueue2 = workStealingQueues; workStealingQueue2 != null; workStealingQueue2 = workStealingQueue2._nextQueue)
			{
				if (workStealingQueue2._currentOp != 0)
				{
					SpinWait spinWait = default(SpinWait);
					do
					{
						spinWait.SpinOnce();
					}
					while (workStealingQueue2._currentOp != 0);
				}
			}
		}

		// Token: 0x0600379D RID: 14237 RVA: 0x000CCC58 File Offset: 0x000CAE58
		private void UnfreezeBag(bool lockTaken)
		{
			if (lockTaken)
			{
				for (ConcurrentBag<T>.WorkStealingQueue workStealingQueue = this._workStealingQueues; workStealingQueue != null; workStealingQueue = workStealingQueue._nextQueue)
				{
					if (workStealingQueue._frozen)
					{
						workStealingQueue._frozen = false;
						Monitor.Exit(workStealingQueue);
					}
				}
				Monitor.Exit(this.GlobalQueuesLock);
			}
		}

		// Token: 0x04002C0A RID: 11274
		private ThreadLocal<ConcurrentBag<T>.WorkStealingQueue> _locals;

		// Token: 0x04002C0B RID: 11275
		private volatile ConcurrentBag<T>.WorkStealingQueue _workStealingQueues;

		// Token: 0x020006EE RID: 1774
		private sealed class WorkStealingQueue
		{
			// Token: 0x0600379E RID: 14238 RVA: 0x000CCC9D File Offset: 0x000CAE9D
			internal WorkStealingQueue(ConcurrentBag<T>.WorkStealingQueue nextQueue)
			{
				this._ownerThreadId = Environment.CurrentManagedThreadId;
				this._nextQueue = nextQueue;
			}

			// Token: 0x17000D6A RID: 3434
			// (get) Token: 0x0600379F RID: 14239 RVA: 0x000CCCD0 File Offset: 0x000CAED0
			internal bool IsEmpty
			{
				get
				{
					return this._headIndex >= this._tailIndex;
				}
			}

			// Token: 0x060037A0 RID: 14240 RVA: 0x000CCCE8 File Offset: 0x000CAEE8
			internal void LocalPush(T item)
			{
				bool flag = false;
				try
				{
					Interlocked.Exchange(ref this._currentOp, 1);
					int num = this._tailIndex;
					if (num == 2147483647)
					{
						this._currentOp = 0;
						lock (this)
						{
							this._headIndex &= this._mask;
							num = (this._tailIndex &= this._mask);
							this._currentOp = 1;
						}
					}
					if (!this._frozen && num < this._headIndex + this._mask)
					{
						this._array[num & this._mask] = item;
						this._tailIndex = num + 1;
					}
					else
					{
						this._currentOp = 0;
						Monitor.Enter(this, ref flag);
						int headIndex = this._headIndex;
						int num2 = this._tailIndex - this._headIndex;
						if (num2 >= this._mask)
						{
							T[] array = new T[this._array.Length << 1];
							int num3 = headIndex & this._mask;
							if (num3 == 0)
							{
								Array.Copy(this._array, 0, array, 0, this._array.Length);
							}
							else
							{
								Array.Copy(this._array, num3, array, 0, this._array.Length - num3);
								Array.Copy(this._array, 0, array, this._array.Length - num3, num3);
							}
							this._array = array;
							this._headIndex = 0;
							num = (this._tailIndex = num2);
							this._mask = (this._mask << 1) | 1;
						}
						this._array[num & this._mask] = item;
						this._tailIndex = num + 1;
						this._addTakeCount -= this._stealCount;
						this._stealCount = 0;
					}
					checked
					{
						this._addTakeCount++;
					}
				}
				finally
				{
					this._currentOp = 0;
					if (flag)
					{
						Monitor.Exit(this);
					}
				}
			}

			// Token: 0x060037A1 RID: 14241 RVA: 0x000CCF3C File Offset: 0x000CB13C
			internal void LocalClear()
			{
				lock (this)
				{
					if (this._headIndex < this._tailIndex)
					{
						this._headIndex = (this._tailIndex = 0);
						this._addTakeCount = (this._stealCount = 0);
						Array.Clear(this._array, 0, this._array.Length);
					}
				}
			}

			// Token: 0x060037A2 RID: 14242 RVA: 0x000CCFC0 File Offset: 0x000CB1C0
			internal bool TryLocalPop(out T result)
			{
				int num = this._tailIndex;
				if (this._headIndex >= num)
				{
					result = default(T);
					return false;
				}
				bool flag = false;
				bool flag2;
				try
				{
					this._currentOp = 2;
					Interlocked.Exchange(ref this._tailIndex, --num);
					if (!this._frozen && this._headIndex < num)
					{
						int num2 = num & this._mask;
						result = this._array[num2];
						this._array[num2] = default(T);
						this._addTakeCount--;
						flag2 = true;
					}
					else
					{
						this._currentOp = 0;
						Monitor.Enter(this, ref flag);
						if (this._headIndex <= num)
						{
							int num3 = num & this._mask;
							result = this._array[num3];
							this._array[num3] = default(T);
							this._addTakeCount--;
							flag2 = true;
						}
						else
						{
							this._tailIndex = num + 1;
							result = default(T);
							flag2 = false;
						}
					}
				}
				finally
				{
					this._currentOp = 0;
					if (flag)
					{
						Monitor.Exit(this);
					}
				}
				return flag2;
			}

			// Token: 0x060037A3 RID: 14243 RVA: 0x000CD10C File Offset: 0x000CB30C
			internal bool TryLocalPeek(out T result)
			{
				int tailIndex = this._tailIndex;
				if (this._headIndex < tailIndex)
				{
					lock (this)
					{
						if (this._headIndex < tailIndex)
						{
							result = this._array[(tailIndex - 1) & this._mask];
							return true;
						}
					}
				}
				result = default(T);
				return false;
			}

			// Token: 0x060037A4 RID: 14244 RVA: 0x000CD190 File Offset: 0x000CB390
			internal bool TrySteal(out T result, bool take)
			{
				if (this._headIndex < this._tailIndex)
				{
					lock (this)
					{
						int headIndex = this._headIndex;
						if (take)
						{
							Interlocked.Exchange(ref this._headIndex, headIndex + 1);
							if (headIndex < this._tailIndex)
							{
								int num = headIndex & this._mask;
								result = this._array[num];
								this._array[num] = default(T);
								this._stealCount++;
								return true;
							}
							this._headIndex = headIndex;
						}
						else if (headIndex < this._tailIndex)
						{
							result = this._array[headIndex & this._mask];
							return true;
						}
					}
				}
				result = default(T);
				return false;
			}

			// Token: 0x060037A5 RID: 14245 RVA: 0x000CD290 File Offset: 0x000CB490
			internal int DangerousCopyTo(T[] array, int arrayIndex)
			{
				int headIndex = this._headIndex;
				int dangerousCount = this.DangerousCount;
				for (int i = arrayIndex + dangerousCount - 1; i >= arrayIndex; i--)
				{
					array[i] = this._array[headIndex++ & this._mask];
				}
				return dangerousCount;
			}

			// Token: 0x17000D6B RID: 3435
			// (get) Token: 0x060037A6 RID: 14246 RVA: 0x000CD2E0 File Offset: 0x000CB4E0
			internal int DangerousCount
			{
				get
				{
					return this._addTakeCount - this._stealCount;
				}
			}

			// Token: 0x04002C0C RID: 11276
			private const int InitialSize = 32;

			// Token: 0x04002C0D RID: 11277
			private const int StartIndex = 0;

			// Token: 0x04002C0E RID: 11278
			private volatile int _headIndex;

			// Token: 0x04002C0F RID: 11279
			private volatile int _tailIndex;

			// Token: 0x04002C10 RID: 11280
			private volatile T[] _array = new T[32];

			// Token: 0x04002C11 RID: 11281
			private volatile int _mask = 31;

			// Token: 0x04002C12 RID: 11282
			private int _addTakeCount;

			// Token: 0x04002C13 RID: 11283
			private int _stealCount;

			// Token: 0x04002C14 RID: 11284
			internal volatile int _currentOp;

			// Token: 0x04002C15 RID: 11285
			internal bool _frozen;

			// Token: 0x04002C16 RID: 11286
			internal readonly ConcurrentBag<T>.WorkStealingQueue _nextQueue;

			// Token: 0x04002C17 RID: 11287
			internal readonly int _ownerThreadId;
		}

		// Token: 0x020006EF RID: 1775
		internal enum Operation
		{
			// Token: 0x04002C19 RID: 11289
			None,
			// Token: 0x04002C1A RID: 11290
			Add,
			// Token: 0x04002C1B RID: 11291
			Take
		}

		// Token: 0x020006F0 RID: 1776
		[Serializable]
		private sealed class Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x060037A7 RID: 14247 RVA: 0x000CD2EF File Offset: 0x000CB4EF
			public Enumerator(T[] array)
			{
				this._array = array;
			}

			// Token: 0x060037A8 RID: 14248 RVA: 0x000CD300 File Offset: 0x000CB500
			public bool MoveNext()
			{
				if (this._index < this._array.Length)
				{
					T[] array = this._array;
					int index = this._index;
					this._index = index + 1;
					this._current = array[index];
					return true;
				}
				this._index = this._array.Length + 1;
				return false;
			}

			// Token: 0x17000D6C RID: 3436
			// (get) Token: 0x060037A9 RID: 14249 RVA: 0x000CD352 File Offset: 0x000CB552
			public T Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x17000D6D RID: 3437
			// (get) Token: 0x060037AA RID: 14250 RVA: 0x000CD35A File Offset: 0x000CB55A
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index == this._array.Length + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this.Current;
				}
			}

			// Token: 0x060037AB RID: 14251 RVA: 0x000CD38C File Offset: 0x000CB58C
			public void Reset()
			{
				this._index = 0;
				this._current = default(T);
			}

			// Token: 0x060037AC RID: 14252 RVA: 0x000027E8 File Offset: 0x000009E8
			public void Dispose()
			{
			}

			// Token: 0x04002C1C RID: 11292
			private readonly T[] _array;

			// Token: 0x04002C1D RID: 11293
			private T _current;

			// Token: 0x04002C1E RID: 11294
			private int _index;
		}
	}
}
