using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Collections.Concurrent
{
	/// <summary>Represents a thread-safe first in-first out (FIFO) collection.</summary>
	/// <typeparam name="T">The type of the elements contained in the queue.</typeparam>
	// Token: 0x020009FD RID: 2557
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(IProducerConsumerCollectionDebugView<>))]
	[Serializable]
	public class ConcurrentQueue<T> : IProducerConsumerCollection<T>, IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> class.</summary>
		// Token: 0x06005F00 RID: 24320 RVA: 0x00138FC0 File Offset: 0x001371C0
		public ConcurrentQueue()
		{
			this._crossSegmentLock = new object();
			this._tail = (this._head = new ConcurrentQueue<T>.Segment(32));
		}

		// Token: 0x06005F01 RID: 24321 RVA: 0x00138FF8 File Offset: 0x001371F8
		private void InitializeFromCollection(IEnumerable<T> collection)
		{
			this._crossSegmentLock = new object();
			int num = 32;
			ICollection<T> collection2 = collection as ICollection<T>;
			if (collection2 != null)
			{
				int count = collection2.Count;
				if (count > num)
				{
					num = Math.Min(ConcurrentQueue<T>.RoundUpToPowerOf2(count), 1048576);
				}
			}
			this._tail = (this._head = new ConcurrentQueue<T>.Segment(num));
			foreach (T t in collection)
			{
				this.Enqueue(t);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> class that contains elements copied from the specified collection</summary>
		/// <param name="collection">The collection whose elements are copied to the new <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="collection" /> argument is null.</exception>
		// Token: 0x06005F02 RID: 24322 RVA: 0x00139094 File Offset: 0x00137294
		public ConcurrentQueue(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.InitializeFromCollection(collection);
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
		// Token: 0x06005F03 RID: 24323 RVA: 0x001390B4 File Offset: 0x001372B4
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
				throw new ArgumentNullException("array");
			}
			this.ToArray().CopyTo(array, index);
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized with the SyncRoot.</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized with the SyncRoot; otherwise, false. For <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" />, this property always returns false.</returns>
		// Token: 0x170010F3 RID: 4339
		// (get) Token: 0x06005F04 RID: 24324 RVA: 0x00015ED5 File Offset: 0x000140D5
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
		// Token: 0x170010F4 RID: 4340
		// (get) Token: 0x06005F05 RID: 24325 RVA: 0x001388D0 File Offset: 0x00136AD0
		object ICollection.SyncRoot
		{
			get
			{
				throw new NotSupportedException("The SyncRoot property may not be used for the synchronization of concurrent collections.");
			}
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06005F06 RID: 24326 RVA: 0x001390EF File Offset: 0x001372EF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<T>)this).GetEnumerator();
		}

		/// <summary>Attempts to add an object to the <see cref="T:System.Collections.Concurrent.IProducerConsumerCollection`1" />.</summary>
		/// <returns>true if the object was added successfully; otherwise, false.</returns>
		/// <param name="item">The object to add to the <see cref="T:System.Collections.Concurrent.IProducerConsumerCollection`1" />. The value can be a null reference (Nothing in Visual Basic) for reference types.</param>
		// Token: 0x06005F07 RID: 24327 RVA: 0x001390F7 File Offset: 0x001372F7
		bool IProducerConsumerCollection<T>.TryAdd(T item)
		{
			this.Enqueue(item);
			return true;
		}

		/// <summary>Attempts to remove and return an object from the <see cref="T:System.Collections.Concurrent.IProducerConsumerCollection`1" />.</summary>
		/// <returns>true if an element was removed and returned succesfully; otherwise, false.</returns>
		/// <param name="item">When this method returns, if the operation was successful, <paramref name="item" /> contains the object removed. If no object was available to be removed, the value is unspecified.</param>
		// Token: 0x06005F08 RID: 24328 RVA: 0x000D1D9D File Offset: 0x000CFF9D
		bool IProducerConsumerCollection<T>.TryTake(out T item)
		{
			return this.TryDequeue(out item);
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> is empty.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> is empty; otherwise, false.</returns>
		// Token: 0x170010F5 RID: 4341
		// (get) Token: 0x06005F09 RID: 24329 RVA: 0x00139104 File Offset: 0x00137304
		public bool IsEmpty
		{
			get
			{
				T t;
				return !this.TryPeek(out t, false);
			}
		}

		/// <summary>Copies the elements stored in the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> to a new array.</summary>
		/// <returns>A new array containing a snapshot of elements copied from the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" />.</returns>
		// Token: 0x06005F0A RID: 24330 RVA: 0x00139120 File Offset: 0x00137320
		public T[] ToArray()
		{
			ConcurrentQueue<T>.Segment segment;
			int num;
			ConcurrentQueue<T>.Segment segment2;
			int num2;
			this.SnapForObservation(out segment, out num, out segment2, out num2);
			T[] array = new T[ConcurrentQueue<T>.GetCount(segment, num, segment2, num2)];
			using (IEnumerator<T> enumerator = this.Enumerate(segment, num, segment2, num2))
			{
				int num3 = 0;
				while (enumerator.MoveNext())
				{
					T t = enumerator.Current;
					array[num3++] = t;
				}
			}
			return array;
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" />.</returns>
		// Token: 0x170010F6 RID: 4342
		// (get) Token: 0x06005F0B RID: 24331 RVA: 0x0013919C File Offset: 0x0013739C
		public int Count
		{
			get
			{
				SpinWait spinWait = default(SpinWait);
				ConcurrentQueue<T>.Segment head;
				ConcurrentQueue<T>.Segment tail;
				int num;
				int num2;
				int num3;
				int num4;
				for (;;)
				{
					head = this._head;
					tail = this._tail;
					num = Volatile.Read(ref head._headAndTail.Head);
					num2 = Volatile.Read(ref head._headAndTail.Tail);
					if (head == tail)
					{
						if (head == this._head && head == this._tail && num == Volatile.Read(ref head._headAndTail.Head) && num2 == Volatile.Read(ref head._headAndTail.Tail))
						{
							break;
						}
					}
					else
					{
						if (head._nextSegment != tail)
						{
							goto IL_013C;
						}
						num3 = Volatile.Read(ref tail._headAndTail.Head);
						num4 = Volatile.Read(ref tail._headAndTail.Tail);
						if (head == this._head && tail == this._tail && num == Volatile.Read(ref head._headAndTail.Head) && num2 == Volatile.Read(ref head._headAndTail.Tail) && num3 == Volatile.Read(ref tail._headAndTail.Head) && num4 == Volatile.Read(ref tail._headAndTail.Tail))
						{
							goto Block_12;
						}
					}
					spinWait.SpinOnce();
				}
				return ConcurrentQueue<T>.GetCount(head, num, num2);
				Block_12:
				return ConcurrentQueue<T>.GetCount(head, num, num2) + ConcurrentQueue<T>.GetCount(tail, num3, num4);
				IL_013C:
				this.SnapForObservation(out head, out num, out tail, out num4);
				return (int)ConcurrentQueue<T>.GetCount(head, num, tail, num4);
			}
		}

		// Token: 0x06005F0C RID: 24332 RVA: 0x0013930A File Offset: 0x0013750A
		private static int GetCount(ConcurrentQueue<T>.Segment s, int head, int tail)
		{
			if (head == tail || head == tail - s.FreezeOffset)
			{
				return 0;
			}
			head &= s._slotsMask;
			tail &= s._slotsMask;
			if (head >= tail)
			{
				return s._slots.Length - head + tail;
			}
			return tail - head;
		}

		// Token: 0x06005F0D RID: 24333 RVA: 0x00139348 File Offset: 0x00137548
		private static long GetCount(ConcurrentQueue<T>.Segment head, int headHead, ConcurrentQueue<T>.Segment tail, int tailTail)
		{
			long num = 0L;
			int num2 = ((head == tail) ? tailTail : Volatile.Read(ref head._headAndTail.Tail)) - head.FreezeOffset;
			if (headHead < num2)
			{
				headHead &= head._slotsMask;
				num2 &= head._slotsMask;
				num += (long)((headHead < num2) ? (num2 - headHead) : (head._slots.Length - headHead + num2));
			}
			if (head != tail)
			{
				for (ConcurrentQueue<T>.Segment segment = head._nextSegment; segment != tail; segment = segment._nextSegment)
				{
					num += (long)(segment._headAndTail.Tail - segment.FreezeOffset);
				}
				num += (long)(tailTail - tail.FreezeOffset);
			}
			return num;
		}

		/// <summary>Copies the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> elements to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> is equal to or greater than the length of the <paramref name="array" /> -or- The number of elements in the source <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x06005F0E RID: 24334 RVA: 0x001393E4 File Offset: 0x001375E4
		public void CopyTo(T[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "The index argument must be greater than or equal zero.");
			}
			ConcurrentQueue<T>.Segment segment;
			int num;
			ConcurrentQueue<T>.Segment segment2;
			int num2;
			this.SnapForObservation(out segment, out num, out segment2, out num2);
			long count = ConcurrentQueue<T>.GetCount(segment, num, segment2, num2);
			if ((long)index > (long)array.Length - count)
			{
				throw new ArgumentException("The number of elements in the collection is greater than the available space from index to the end of the destination array.");
			}
			int num3 = index;
			using (IEnumerator<T> enumerator = this.Enumerate(segment, num, segment2, num2))
			{
				while (enumerator.MoveNext())
				{
					T t = enumerator.Current;
					array[num3++] = t;
				}
			}
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" />.</summary>
		/// <returns>An enumerator for the contents of the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" />.</returns>
		// Token: 0x06005F0F RID: 24335 RVA: 0x00139490 File Offset: 0x00137690
		public IEnumerator<T> GetEnumerator()
		{
			ConcurrentQueue<T>.Segment segment;
			int num;
			ConcurrentQueue<T>.Segment segment2;
			int num2;
			this.SnapForObservation(out segment, out num, out segment2, out num2);
			return this.Enumerate(segment, num, segment2, num2);
		}

		// Token: 0x06005F10 RID: 24336 RVA: 0x001394B8 File Offset: 0x001376B8
		private void SnapForObservation(out ConcurrentQueue<T>.Segment head, out int headHead, out ConcurrentQueue<T>.Segment tail, out int tailTail)
		{
			object crossSegmentLock = this._crossSegmentLock;
			lock (crossSegmentLock)
			{
				head = this._head;
				tail = this._tail;
				ConcurrentQueue<T>.Segment segment = head;
				for (;;)
				{
					segment._preservedForObservation = true;
					if (segment == tail)
					{
						break;
					}
					segment = segment._nextSegment;
				}
				tail.EnsureFrozenForEnqueues();
				headHead = Volatile.Read(ref head._headAndTail.Head);
				tailTail = Volatile.Read(ref tail._headAndTail.Tail);
			}
		}

		// Token: 0x06005F11 RID: 24337 RVA: 0x0013954C File Offset: 0x0013774C
		private T GetItemWhenAvailable(ConcurrentQueue<T>.Segment segment, int i)
		{
			int num = (i + 1) & segment._slotsMask;
			if ((segment._slots[i].SequenceNumber & segment._slotsMask) != num)
			{
				SpinWait spinWait = default(SpinWait);
				while ((Volatile.Read(ref segment._slots[i].SequenceNumber) & segment._slotsMask) != num)
				{
					spinWait.SpinOnce();
				}
			}
			return segment._slots[i].Item;
		}

		// Token: 0x06005F12 RID: 24338 RVA: 0x001395C1 File Offset: 0x001377C1
		private IEnumerator<T> Enumerate(ConcurrentQueue<T>.Segment head, int headHead, ConcurrentQueue<T>.Segment tail, int tailTail)
		{
			int headTail = ((head == tail) ? tailTail : Volatile.Read(ref head._headAndTail.Tail)) - head.FreezeOffset;
			if (headHead < headTail)
			{
				headHead &= head._slotsMask;
				headTail &= head._slotsMask;
				if (headHead < headTail)
				{
					int num;
					for (int i = headHead; i < headTail; i = num + 1)
					{
						yield return this.GetItemWhenAvailable(head, i);
						num = i;
					}
				}
				else
				{
					int num;
					for (int j = headHead; j < head._slots.Length; j = num + 1)
					{
						yield return this.GetItemWhenAvailable(head, j);
						num = j;
					}
					for (int k = 0; k < headTail; k = num + 1)
					{
						yield return this.GetItemWhenAvailable(head, k);
						num = k;
					}
				}
			}
			if (head != tail)
			{
				int num;
				ConcurrentQueue<T>.Segment s;
				for (s = head._nextSegment; s != tail; s = s._nextSegment)
				{
					int sTail = s._headAndTail.Tail - s.FreezeOffset;
					for (int l = 0; l < sTail; l = num + 1)
					{
						yield return this.GetItemWhenAvailable(s, l);
						num = l;
					}
				}
				s = null;
				tailTail -= tail.FreezeOffset;
				for (int m = 0; m < tailTail; m = num + 1)
				{
					yield return this.GetItemWhenAvailable(tail, m);
					num = m;
				}
			}
			yield break;
		}

		// Token: 0x06005F13 RID: 24339 RVA: 0x001395ED File Offset: 0x001377ED
		private static int RoundUpToPowerOf2(int i)
		{
			i--;
			i |= i >> 1;
			i |= i >> 2;
			i |= i >> 4;
			i |= i >> 8;
			i |= i >> 16;
			return i + 1;
		}

		/// <summary>Adds an object to the end of the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" />.</summary>
		/// <param name="item">The object to add to the end of the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" />. The value can be a null reference (Nothing in Visual Basic) for reference types.</param>
		// Token: 0x06005F14 RID: 24340 RVA: 0x0013961B File Offset: 0x0013781B
		public void Enqueue(T item)
		{
			if (!this._tail.TryEnqueue(item))
			{
				this.EnqueueSlow(item);
			}
		}

		// Token: 0x06005F15 RID: 24341 RVA: 0x00139634 File Offset: 0x00137834
		private void EnqueueSlow(T item)
		{
			for (;;)
			{
				ConcurrentQueue<T>.Segment tail = this._tail;
				if (tail.TryEnqueue(item))
				{
					break;
				}
				object crossSegmentLock = this._crossSegmentLock;
				lock (crossSegmentLock)
				{
					if (tail == this._tail)
					{
						tail.EnsureFrozenForEnqueues();
						ConcurrentQueue<T>.Segment segment = new ConcurrentQueue<T>.Segment(tail._preservedForObservation ? 32 : Math.Min(tail.Capacity * 2, 1048576));
						tail._nextSegment = segment;
						this._tail = segment;
					}
				}
			}
		}

		/// <summary>Tries to remove and return the object at the beginning of the concurrent queue.</summary>
		/// <returns>true if an element was removed and returned from the beginning of the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> successfully; otherwise, false.</returns>
		/// <param name="result">When this method returns, if the operation was successful, <paramref name="result" /> contains the object removed. If no object was available to be removed, the value is unspecified.</param>
		// Token: 0x06005F16 RID: 24342 RVA: 0x001396D4 File Offset: 0x001378D4
		public bool TryDequeue(out T result)
		{
			return this._head.TryDequeue(out result) || this.TryDequeueSlow(out result);
		}

		// Token: 0x06005F17 RID: 24343 RVA: 0x001396F0 File Offset: 0x001378F0
		private bool TryDequeueSlow(out T item)
		{
			for (;;)
			{
				ConcurrentQueue<T>.Segment head = this._head;
				if (head.TryDequeue(out item))
				{
					break;
				}
				if (head._nextSegment == null)
				{
					goto Block_1;
				}
				if (head.TryDequeue(out item))
				{
					return true;
				}
				object crossSegmentLock = this._crossSegmentLock;
				lock (crossSegmentLock)
				{
					if (head == this._head)
					{
						this._head = head._nextSegment;
					}
				}
			}
			return true;
			Block_1:
			item = default(T);
			return false;
		}

		/// <summary>Tries to return an object from the beginning of the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> without removing it.</summary>
		/// <returns>true if an object was returned successfully; otherwise, false.</returns>
		/// <param name="result">When this method returns, <paramref name="result" /> contains an object from the beginning of the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> or an unspecified value if the operation failed.</param>
		// Token: 0x06005F18 RID: 24344 RVA: 0x00139780 File Offset: 0x00137980
		public bool TryPeek(out T result)
		{
			return this.TryPeek(out result, true);
		}

		// Token: 0x06005F19 RID: 24345 RVA: 0x0013978C File Offset: 0x0013798C
		private bool TryPeek(out T result, bool resultUsed)
		{
			ConcurrentQueue<T>.Segment segment = this._head;
			for (;;)
			{
				ConcurrentQueue<T>.Segment segment2 = Volatile.Read<ConcurrentQueue<T>.Segment>(ref segment._nextSegment);
				if (segment.TryPeek(out result, resultUsed))
				{
					break;
				}
				if (segment2 != null)
				{
					segment = segment2;
				}
				else if (Volatile.Read<ConcurrentQueue<T>.Segment>(ref segment._nextSegment) == null)
				{
					goto Block_3;
				}
			}
			return true;
			Block_3:
			result = default(T);
			return false;
		}

		// Token: 0x06005F1A RID: 24346 RVA: 0x001397D8 File Offset: 0x001379D8
		public void Clear()
		{
			object crossSegmentLock = this._crossSegmentLock;
			lock (crossSegmentLock)
			{
				this._tail.EnsureFrozenForEnqueues();
				this._tail = (this._head = new ConcurrentQueue<T>.Segment(32));
			}
		}

		// Token: 0x04002FE4 RID: 12260
		private const int InitialSegmentLength = 32;

		// Token: 0x04002FE5 RID: 12261
		private const int MaxSegmentLength = 1048576;

		// Token: 0x04002FE6 RID: 12262
		private object _crossSegmentLock;

		// Token: 0x04002FE7 RID: 12263
		private volatile ConcurrentQueue<T>.Segment _tail;

		// Token: 0x04002FE8 RID: 12264
		private volatile ConcurrentQueue<T>.Segment _head;

		// Token: 0x020009FE RID: 2558
		[DebuggerDisplay("Capacity = {Capacity}")]
		private sealed class Segment
		{
			// Token: 0x06005F1B RID: 24347 RVA: 0x0013983C File Offset: 0x00137A3C
			public Segment(int boundedLength)
			{
				this._slots = new ConcurrentQueue<T>.Segment.Slot[boundedLength];
				this._slotsMask = boundedLength - 1;
				for (int i = 0; i < this._slots.Length; i++)
				{
					this._slots[i].SequenceNumber = i;
				}
			}

			// Token: 0x170010F7 RID: 4343
			// (get) Token: 0x06005F1C RID: 24348 RVA: 0x00139889 File Offset: 0x00137A89
			internal int Capacity
			{
				get
				{
					return this._slots.Length;
				}
			}

			// Token: 0x170010F8 RID: 4344
			// (get) Token: 0x06005F1D RID: 24349 RVA: 0x00139893 File Offset: 0x00137A93
			internal int FreezeOffset
			{
				get
				{
					return this._slots.Length * 2;
				}
			}

			// Token: 0x06005F1E RID: 24350 RVA: 0x001398A0 File Offset: 0x00137AA0
			internal void EnsureFrozenForEnqueues()
			{
				if (!this._frozenForEnqueues)
				{
					this._frozenForEnqueues = true;
					SpinWait spinWait = default(SpinWait);
					for (;;)
					{
						int num = Volatile.Read(ref this._headAndTail.Tail);
						if (Interlocked.CompareExchange(ref this._headAndTail.Tail, num + this.FreezeOffset, num) == num)
						{
							break;
						}
						spinWait.SpinOnce();
					}
				}
			}

			// Token: 0x06005F1F RID: 24351 RVA: 0x001398FC File Offset: 0x00137AFC
			public bool TryDequeue(out T item)
			{
				SpinWait spinWait = default(SpinWait);
				int num;
				int num2;
				for (;;)
				{
					num = Volatile.Read(ref this._headAndTail.Head);
					num2 = num & this._slotsMask;
					int num3 = Volatile.Read(ref this._slots[num2].SequenceNumber) - (num + 1);
					if (num3 == 0)
					{
						if (Interlocked.CompareExchange(ref this._headAndTail.Head, num + 1, num) == num)
						{
							break;
						}
					}
					else if (num3 < 0)
					{
						bool frozenForEnqueues = this._frozenForEnqueues;
						int num4 = Volatile.Read(ref this._headAndTail.Tail);
						if (num4 - num <= 0 || (frozenForEnqueues && num4 - this.FreezeOffset - num <= 0))
						{
							goto IL_00EE;
						}
					}
					spinWait.SpinOnce();
				}
				item = this._slots[num2].Item;
				if (!Volatile.Read(ref this._preservedForObservation))
				{
					this._slots[num2].Item = default(T);
					Volatile.Write(ref this._slots[num2].SequenceNumber, num + this._slots.Length);
				}
				return true;
				IL_00EE:
				item = default(T);
				return false;
			}

			// Token: 0x06005F20 RID: 24352 RVA: 0x00139A0C File Offset: 0x00137C0C
			public bool TryPeek(out T result, bool resultUsed)
			{
				if (resultUsed)
				{
					this._preservedForObservation = true;
					Interlocked.MemoryBarrier();
				}
				SpinWait spinWait = default(SpinWait);
				int num2;
				for (;;)
				{
					int num = Volatile.Read(ref this._headAndTail.Head);
					num2 = num & this._slotsMask;
					int num3 = Volatile.Read(ref this._slots[num2].SequenceNumber) - (num + 1);
					if (num3 == 0)
					{
						break;
					}
					if (num3 < 0)
					{
						bool frozenForEnqueues = this._frozenForEnqueues;
						int num4 = Volatile.Read(ref this._headAndTail.Tail);
						if (num4 - num <= 0 || (frozenForEnqueues && num4 - this.FreezeOffset - num <= 0))
						{
							goto IL_00AE;
						}
					}
					spinWait.SpinOnce();
				}
				result = (resultUsed ? this._slots[num2].Item : default(T));
				return true;
				IL_00AE:
				result = default(T);
				return false;
			}

			// Token: 0x06005F21 RID: 24353 RVA: 0x00139ADC File Offset: 0x00137CDC
			public bool TryEnqueue(T item)
			{
				SpinWait spinWait = default(SpinWait);
				int num;
				int num2;
				for (;;)
				{
					num = Volatile.Read(ref this._headAndTail.Tail);
					num2 = num & this._slotsMask;
					int num3 = Volatile.Read(ref this._slots[num2].SequenceNumber) - num;
					if (num3 == 0)
					{
						if (Interlocked.CompareExchange(ref this._headAndTail.Tail, num + 1, num) == num)
						{
							break;
						}
					}
					else if (num3 < 0)
					{
						return false;
					}
					spinWait.SpinOnce();
				}
				this._slots[num2].Item = item;
				Volatile.Write(ref this._slots[num2].SequenceNumber, num + 1);
				return true;
			}

			// Token: 0x04002FE9 RID: 12265
			internal readonly ConcurrentQueue<T>.Segment.Slot[] _slots;

			// Token: 0x04002FEA RID: 12266
			internal readonly int _slotsMask;

			// Token: 0x04002FEB RID: 12267
			internal PaddedHeadAndTail _headAndTail;

			// Token: 0x04002FEC RID: 12268
			internal bool _preservedForObservation;

			// Token: 0x04002FED RID: 12269
			internal bool _frozenForEnqueues;

			// Token: 0x04002FEE RID: 12270
			internal ConcurrentQueue<T>.Segment _nextSegment;

			// Token: 0x020009FF RID: 2559
			[DebuggerDisplay("Item = {Item}, SequenceNumber = {SequenceNumber}")]
			[StructLayout(LayoutKind.Auto)]
			internal struct Slot
			{
				// Token: 0x04002FEF RID: 12271
				public T Item;

				// Token: 0x04002FF0 RID: 12272
				public int SequenceNumber;
			}
		}
	}
}
