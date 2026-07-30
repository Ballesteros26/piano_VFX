using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections
{
	/// <summary>Represents a first-in, first-out collection of objects.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020009DC RID: 2524
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(Queue.QueueDebugView))]
	[ComVisible(true)]
	[Serializable]
	public class Queue : ICollection, IEnumerable, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Queue" /> class that is empty, has the default initial capacity, and uses the default growth factor.</summary>
		// Token: 0x06005D33 RID: 23859 RVA: 0x00133C37 File Offset: 0x00131E37
		public Queue()
			: this(32, 2f)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Queue" /> class that is empty, has the specified initial capacity, and uses the default growth factor.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Queue" /> can contain. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero. </exception>
		// Token: 0x06005D34 RID: 23860 RVA: 0x00133C46 File Offset: 0x00131E46
		public Queue(int capacity)
			: this(capacity, 2f)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Queue" /> class that is empty, has the specified initial capacity, and uses the specified growth factor.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Queue" /> can contain. </param>
		/// <param name="growFactor">The factor by which the capacity of the <see cref="T:System.Collections.Queue" /> is expanded. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.-or- <paramref name="growFactor" /> is less than 1.0 or greater than 10.0. </exception>
		// Token: 0x06005D35 RID: 23861 RVA: 0x00133C54 File Offset: 0x00131E54
		public Queue(int capacity, float growFactor)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("Non-negative number required."));
			}
			if ((double)growFactor < 1.0 || (double)growFactor > 10.0)
			{
				throw new ArgumentOutOfRangeException("growFactor", Environment.GetResourceString("Queue grow factor must be between {0} and {1}.", new object[] { 1, 10 }));
			}
			this._array = new object[capacity];
			this._head = 0;
			this._tail = 0;
			this._size = 0;
			this._growFactor = (int)(growFactor * 100f);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Queue" /> class that contains elements copied from the specified collection, has the same initial capacity as the number of elements copied, and uses the default growth factor.</summary>
		/// <param name="col">The <see cref="T:System.Collections.ICollection" /> to copy elements from. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="col" /> is null. </exception>
		// Token: 0x06005D36 RID: 23862 RVA: 0x00133CF8 File Offset: 0x00131EF8
		public Queue(ICollection col)
			: this((col == null) ? 32 : col.Count)
		{
			if (col == null)
			{
				throw new ArgumentNullException("col");
			}
			foreach (object obj in col)
			{
				this.Enqueue(obj);
			}
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Queue" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.Queue" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17001071 RID: 4209
		// (get) Token: 0x06005D37 RID: 23863 RVA: 0x00133D43 File Offset: 0x00131F43
		public virtual int Count
		{
			get
			{
				return this._size;
			}
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Collections.Queue" />.</summary>
		/// <returns>A shallow copy of the <see cref="T:System.Collections.Queue" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005D38 RID: 23864 RVA: 0x00133D4C File Offset: 0x00131F4C
		public virtual object Clone()
		{
			Queue queue = new Queue(this._size);
			queue._size = this._size;
			int num = this._size;
			int num2 = ((this._array.Length - this._head < num) ? (this._array.Length - this._head) : num);
			Array.Copy(this._array, this._head, queue._array, 0, num2);
			num -= num2;
			if (num > 0)
			{
				Array.Copy(this._array, 0, queue._array, this._array.Length - this._head, num);
			}
			queue._version = this._version;
			return queue;
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.Queue" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.Queue" /> is synchronized (thread safe); otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17001072 RID: 4210
		// (get) Token: 0x06005D39 RID: 23865 RVA: 0x00015ED5 File Offset: 0x000140D5
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Queue" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.Queue" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17001073 RID: 4211
		// (get) Token: 0x06005D3A RID: 23866 RVA: 0x00133DED File Offset: 0x00131FED
		public virtual object SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		/// <summary>Removes all objects from the <see cref="T:System.Collections.Queue" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005D3B RID: 23867 RVA: 0x00133E10 File Offset: 0x00132010
		public virtual void Clear()
		{
			if (this._head < this._tail)
			{
				Array.Clear(this._array, this._head, this._size);
			}
			else
			{
				Array.Clear(this._array, this._head, this._array.Length - this._head);
				Array.Clear(this._array, 0, this._tail);
			}
			this._head = 0;
			this._tail = 0;
			this._size = 0;
			this._version++;
		}

		/// <summary>Copies the <see cref="T:System.Collections.Queue" /> elements to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Queue" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.Queue" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.ArrayTypeMismatchException">The type of the source <see cref="T:System.Collections.Queue" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005D3C RID: 23868 RVA: 0x00133E9C File Offset: 0x0013209C
		public virtual void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Only single dimensional arrays are supported for the requested action."));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			if (array.Length - index < this._size)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			int num = this._size;
			if (num == 0)
			{
				return;
			}
			int num2 = ((this._array.Length - this._head < num) ? (this._array.Length - this._head) : num);
			Array.Copy(this._array, this._head, array, index, num2);
			num -= num2;
			if (num > 0)
			{
				Array.Copy(this._array, 0, array, index + this._array.Length - this._head, num);
			}
		}

		/// <summary>Adds an object to the end of the <see cref="T:System.Collections.Queue" />.</summary>
		/// <param name="obj">The object to add to the <see cref="T:System.Collections.Queue" />. The value can be null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005D3D RID: 23869 RVA: 0x00133F74 File Offset: 0x00132174
		public virtual void Enqueue(object obj)
		{
			if (this._size == this._array.Length)
			{
				int num = (int)((long)this._array.Length * (long)this._growFactor / 100L);
				if (num < this._array.Length + 4)
				{
					num = this._array.Length + 4;
				}
				this.SetCapacity(num);
			}
			this._array[this._tail] = obj;
			this._tail = (this._tail + 1) % this._array.Length;
			this._size++;
			this._version++;
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Queue" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Queue" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005D3E RID: 23870 RVA: 0x00134008 File Offset: 0x00132208
		public virtual IEnumerator GetEnumerator()
		{
			return new Queue.QueueEnumerator(this);
		}

		/// <summary>Removes and returns the object at the beginning of the <see cref="T:System.Collections.Queue" />.</summary>
		/// <returns>The object that is removed from the beginning of the <see cref="T:System.Collections.Queue" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Queue" /> is empty. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005D3F RID: 23871 RVA: 0x00134010 File Offset: 0x00132210
		public virtual object Dequeue()
		{
			if (this.Count == 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Queue empty."));
			}
			object obj = this._array[this._head];
			this._array[this._head] = null;
			this._head = (this._head + 1) % this._array.Length;
			this._size--;
			this._version++;
			return obj;
		}

		/// <summary>Returns the object at the beginning of the <see cref="T:System.Collections.Queue" /> without removing it.</summary>
		/// <returns>The object at the beginning of the <see cref="T:System.Collections.Queue" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Queue" /> is empty. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005D40 RID: 23872 RVA: 0x00134083 File Offset: 0x00132283
		public virtual object Peek()
		{
			if (this.Count == 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Queue empty."));
			}
			return this._array[this._head];
		}

		/// <summary>Returns a <see cref="T:System.Collections.Queue" /> wrapper that is synchronized (thread safe).</summary>
		/// <returns>A <see cref="T:System.Collections.Queue" /> wrapper that is synchronized (thread safe).</returns>
		/// <param name="queue">The <see cref="T:System.Collections.Queue" /> to synchronize. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="queue" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005D41 RID: 23873 RVA: 0x001340AA File Offset: 0x001322AA
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static Queue Synchronized(Queue queue)
		{
			if (queue == null)
			{
				throw new ArgumentNullException("queue");
			}
			return new Queue.SynchronizedQueue(queue);
		}

		/// <summary>Determines whether an element is in the <see cref="T:System.Collections.Queue" />.</summary>
		/// <returns>true if <paramref name="obj" /> is found in the <see cref="T:System.Collections.Queue" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.Queue" />. The value can be null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005D42 RID: 23874 RVA: 0x001340C0 File Offset: 0x001322C0
		public virtual bool Contains(object obj)
		{
			int num = this._head;
			int size = this._size;
			while (size-- > 0)
			{
				if (obj == null)
				{
					if (this._array[num] == null)
					{
						return true;
					}
				}
				else if (this._array[num] != null && this._array[num].Equals(obj))
				{
					return true;
				}
				num = (num + 1) % this._array.Length;
			}
			return false;
		}

		// Token: 0x06005D43 RID: 23875 RVA: 0x0013411E File Offset: 0x0013231E
		internal object GetElement(int i)
		{
			return this._array[(this._head + i) % this._array.Length];
		}

		/// <summary>Copies the <see cref="T:System.Collections.Queue" /> elements to a new array.</summary>
		/// <returns>A new array containing elements copied from the <see cref="T:System.Collections.Queue" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005D44 RID: 23876 RVA: 0x00134138 File Offset: 0x00132338
		public virtual object[] ToArray()
		{
			object[] array = new object[this._size];
			if (this._size == 0)
			{
				return array;
			}
			if (this._head < this._tail)
			{
				Array.Copy(this._array, this._head, array, 0, this._size);
			}
			else
			{
				Array.Copy(this._array, this._head, array, 0, this._array.Length - this._head);
				Array.Copy(this._array, 0, array, this._array.Length - this._head, this._tail);
			}
			return array;
		}

		// Token: 0x06005D45 RID: 23877 RVA: 0x001341CC File Offset: 0x001323CC
		private void SetCapacity(int capacity)
		{
			object[] array = new object[capacity];
			if (this._size > 0)
			{
				if (this._head < this._tail)
				{
					Array.Copy(this._array, this._head, array, 0, this._size);
				}
				else
				{
					Array.Copy(this._array, this._head, array, 0, this._array.Length - this._head);
					Array.Copy(this._array, 0, array, this._array.Length - this._head, this._tail);
				}
			}
			this._array = array;
			this._head = 0;
			this._tail = ((this._size == capacity) ? 0 : this._size);
			this._version++;
		}

		/// <summary>Sets the capacity to the actual number of elements in the <see cref="T:System.Collections.Queue" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Queue" /> is read-only.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005D46 RID: 23878 RVA: 0x0013428A File Offset: 0x0013248A
		public virtual void TrimToSize()
		{
			this.SetCapacity(this._size);
		}

		// Token: 0x04002F7A RID: 12154
		private object[] _array;

		// Token: 0x04002F7B RID: 12155
		private int _head;

		// Token: 0x04002F7C RID: 12156
		private int _tail;

		// Token: 0x04002F7D RID: 12157
		private int _size;

		// Token: 0x04002F7E RID: 12158
		private int _growFactor;

		// Token: 0x04002F7F RID: 12159
		private int _version;

		// Token: 0x04002F80 RID: 12160
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04002F81 RID: 12161
		private const int _MinimumGrow = 4;

		// Token: 0x04002F82 RID: 12162
		private const int _ShrinkThreshold = 32;

		// Token: 0x020009DD RID: 2525
		[Serializable]
		private class SynchronizedQueue : Queue
		{
			// Token: 0x06005D47 RID: 23879 RVA: 0x00134298 File Offset: 0x00132498
			internal SynchronizedQueue(Queue q)
			{
				this._q = q;
				this.root = this._q.SyncRoot;
			}

			// Token: 0x17001074 RID: 4212
			// (get) Token: 0x06005D48 RID: 23880 RVA: 0x00003B29 File Offset: 0x00001D29
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001075 RID: 4213
			// (get) Token: 0x06005D49 RID: 23881 RVA: 0x001342B8 File Offset: 0x001324B8
			public override object SyncRoot
			{
				get
				{
					return this.root;
				}
			}

			// Token: 0x17001076 RID: 4214
			// (get) Token: 0x06005D4A RID: 23882 RVA: 0x001342C0 File Offset: 0x001324C0
			public override int Count
			{
				get
				{
					object obj = this.root;
					int count;
					lock (obj)
					{
						count = this._q.Count;
					}
					return count;
				}
			}

			// Token: 0x06005D4B RID: 23883 RVA: 0x00134308 File Offset: 0x00132508
			public override void Clear()
			{
				object obj = this.root;
				lock (obj)
				{
					this._q.Clear();
				}
			}

			// Token: 0x06005D4C RID: 23884 RVA: 0x00134350 File Offset: 0x00132550
			public override object Clone()
			{
				object obj = this.root;
				object obj2;
				lock (obj)
				{
					obj2 = new Queue.SynchronizedQueue((Queue)this._q.Clone());
				}
				return obj2;
			}

			// Token: 0x06005D4D RID: 23885 RVA: 0x001343A4 File Offset: 0x001325A4
			public override bool Contains(object obj)
			{
				object obj2 = this.root;
				bool flag2;
				lock (obj2)
				{
					flag2 = this._q.Contains(obj);
				}
				return flag2;
			}

			// Token: 0x06005D4E RID: 23886 RVA: 0x001343EC File Offset: 0x001325EC
			public override void CopyTo(Array array, int arrayIndex)
			{
				object obj = this.root;
				lock (obj)
				{
					this._q.CopyTo(array, arrayIndex);
				}
			}

			// Token: 0x06005D4F RID: 23887 RVA: 0x00134434 File Offset: 0x00132634
			public override void Enqueue(object value)
			{
				object obj = this.root;
				lock (obj)
				{
					this._q.Enqueue(value);
				}
			}

			// Token: 0x06005D50 RID: 23888 RVA: 0x0013447C File Offset: 0x0013267C
			public override object Dequeue()
			{
				object obj = this.root;
				object obj2;
				lock (obj)
				{
					obj2 = this._q.Dequeue();
				}
				return obj2;
			}

			// Token: 0x06005D51 RID: 23889 RVA: 0x001344C4 File Offset: 0x001326C4
			public override IEnumerator GetEnumerator()
			{
				object obj = this.root;
				IEnumerator enumerator;
				lock (obj)
				{
					enumerator = this._q.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x06005D52 RID: 23890 RVA: 0x0013450C File Offset: 0x0013270C
			public override object Peek()
			{
				object obj = this.root;
				object obj2;
				lock (obj)
				{
					obj2 = this._q.Peek();
				}
				return obj2;
			}

			// Token: 0x06005D53 RID: 23891 RVA: 0x00134554 File Offset: 0x00132754
			public override object[] ToArray()
			{
				object obj = this.root;
				object[] array;
				lock (obj)
				{
					array = this._q.ToArray();
				}
				return array;
			}

			// Token: 0x06005D54 RID: 23892 RVA: 0x0013459C File Offset: 0x0013279C
			public override void TrimToSize()
			{
				object obj = this.root;
				lock (obj)
				{
					this._q.TrimToSize();
				}
			}

			// Token: 0x04002F83 RID: 12163
			private Queue _q;

			// Token: 0x04002F84 RID: 12164
			private object root;
		}

		// Token: 0x020009DE RID: 2526
		[Serializable]
		private class QueueEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x06005D55 RID: 23893 RVA: 0x001345E4 File Offset: 0x001327E4
			internal QueueEnumerator(Queue q)
			{
				this._q = q;
				this._version = this._q._version;
				this._index = 0;
				this.currentElement = this._q._array;
				if (this._q._size == 0)
				{
					this._index = -1;
				}
			}

			// Token: 0x06005D56 RID: 23894 RVA: 0x0002C3A3 File Offset: 0x0002A5A3
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x06005D57 RID: 23895 RVA: 0x0013463C File Offset: 0x0013283C
			public virtual bool MoveNext()
			{
				if (this._version != this._q._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Collection was modified; enumeration operation may not execute."));
				}
				if (this._index < 0)
				{
					this.currentElement = this._q._array;
					return false;
				}
				this.currentElement = this._q.GetElement(this._index);
				this._index++;
				if (this._index == this._q._size)
				{
					this._index = -1;
				}
				return true;
			}

			// Token: 0x17001077 RID: 4215
			// (get) Token: 0x06005D58 RID: 23896 RVA: 0x001346C8 File Offset: 0x001328C8
			public virtual object Current
			{
				get
				{
					if (this.currentElement != this._q._array)
					{
						return this.currentElement;
					}
					if (this._index == 0)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration has not started. Call MoveNext."));
					}
					throw new InvalidOperationException(Environment.GetResourceString("Enumeration already finished."));
				}
			}

			// Token: 0x06005D59 RID: 23897 RVA: 0x00134718 File Offset: 0x00132918
			public virtual void Reset()
			{
				if (this._version != this._q._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Collection was modified; enumeration operation may not execute."));
				}
				if (this._q._size == 0)
				{
					this._index = -1;
				}
				else
				{
					this._index = 0;
				}
				this.currentElement = this._q._array;
			}

			// Token: 0x04002F85 RID: 12165
			private Queue _q;

			// Token: 0x04002F86 RID: 12166
			private int _index;

			// Token: 0x04002F87 RID: 12167
			private int _version;

			// Token: 0x04002F88 RID: 12168
			private object currentElement;
		}

		// Token: 0x020009DF RID: 2527
		internal class QueueDebugView
		{
			// Token: 0x06005D5A RID: 23898 RVA: 0x00134776 File Offset: 0x00132976
			public QueueDebugView(Queue queue)
			{
				if (queue == null)
				{
					throw new ArgumentNullException("queue");
				}
				this.queue = queue;
			}

			// Token: 0x17001078 RID: 4216
			// (get) Token: 0x06005D5B RID: 23899 RVA: 0x00134793 File Offset: 0x00132993
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public object[] Items
			{
				get
				{
					return this.queue.ToArray();
				}
			}

			// Token: 0x04002F89 RID: 12169
			private Queue queue;
		}
	}
}
