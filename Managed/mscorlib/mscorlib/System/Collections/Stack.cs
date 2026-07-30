using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections
{
	/// <summary>Represents a simple last-in-first-out (LIFO) non-generic collection of objects.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020009E7 RID: 2535
	[DebuggerTypeProxy(typeof(Stack.StackDebugView))]
	[ComVisible(true)]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class Stack : ICollection, IEnumerable, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Stack" /> class that is empty and has the default initial capacity.</summary>
		// Token: 0x06005DD5 RID: 24021 RVA: 0x00135B12 File Offset: 0x00133D12
		public Stack()
		{
			this._array = new object[10];
			this._size = 0;
			this._version = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Stack" /> class that is empty and has the specified initial capacity or the default initial capacity, whichever is greater.</summary>
		/// <param name="initialCapacity">The initial number of elements that the <see cref="T:System.Collections.Stack" /> can contain. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="initialCapacity" /> is less than zero. </exception>
		// Token: 0x06005DD6 RID: 24022 RVA: 0x00135B38 File Offset: 0x00133D38
		public Stack(int initialCapacity)
		{
			if (initialCapacity < 0)
			{
				throw new ArgumentOutOfRangeException("initialCapacity", Environment.GetResourceString("Non-negative number required."));
			}
			if (initialCapacity < 10)
			{
				initialCapacity = 10;
			}
			this._array = new object[initialCapacity];
			this._size = 0;
			this._version = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Stack" /> class that contains elements copied from the specified collection and has the same initial capacity as the number of elements copied.</summary>
		/// <param name="col">The <see cref="T:System.Collections.ICollection" /> to copy elements from. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="col" /> is null. </exception>
		// Token: 0x06005DD7 RID: 24023 RVA: 0x00135B88 File Offset: 0x00133D88
		public Stack(ICollection col)
			: this((col == null) ? 32 : col.Count)
		{
			if (col == null)
			{
				throw new ArgumentNullException("col");
			}
			foreach (object obj in col)
			{
				this.Push(obj);
			}
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Stack" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.Stack" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700109E RID: 4254
		// (get) Token: 0x06005DD8 RID: 24024 RVA: 0x00135BD3 File Offset: 0x00133DD3
		public virtual int Count
		{
			get
			{
				return this._size;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.Stack" /> is synchronized (thread safe).</summary>
		/// <returns>true, if access to the <see cref="T:System.Collections.Stack" /> is synchronized (thread safe); otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700109F RID: 4255
		// (get) Token: 0x06005DD9 RID: 24025 RVA: 0x00015ED5 File Offset: 0x000140D5
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Stack" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that can be used to synchronize access to the <see cref="T:System.Collections.Stack" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170010A0 RID: 4256
		// (get) Token: 0x06005DDA RID: 24026 RVA: 0x00135BDB File Offset: 0x00133DDB
		public virtual object SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		/// <summary>Removes all objects from the <see cref="T:System.Collections.Stack" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005DDB RID: 24027 RVA: 0x00135BFD File Offset: 0x00133DFD
		public virtual void Clear()
		{
			Array.Clear(this._array, 0, this._size);
			this._size = 0;
			this._version++;
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Collections.Stack" />.</summary>
		/// <returns>A shallow copy of the <see cref="T:System.Collections.Stack" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005DDC RID: 24028 RVA: 0x00135C28 File Offset: 0x00133E28
		public virtual object Clone()
		{
			Stack stack = new Stack(this._size);
			stack._size = this._size;
			Array.Copy(this._array, 0, stack._array, 0, this._size);
			stack._version = this._version;
			return stack;
		}

		/// <summary>Determines whether an element is in the <see cref="T:System.Collections.Stack" />.</summary>
		/// <returns>true, if <paramref name="obj" /> is found in the <see cref="T:System.Collections.Stack" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.Stack" />. The value can be null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005DDD RID: 24029 RVA: 0x00135C74 File Offset: 0x00133E74
		public virtual bool Contains(object obj)
		{
			int size = this._size;
			while (size-- > 0)
			{
				if (obj == null)
				{
					if (this._array[size] == null)
					{
						return true;
					}
				}
				else if (this._array[size] != null && this._array[size].Equals(obj))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Copies the <see cref="T:System.Collections.Stack" /> to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Stack" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.Stack" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.Stack" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005DDE RID: 24030 RVA: 0x00135CC0 File Offset: 0x00133EC0
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
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Non-negative number required."));
			}
			if (array.Length - index < this._size)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			int i = 0;
			if (array is object[])
			{
				object[] array2 = (object[])array;
				while (i < this._size)
				{
					array2[i + index] = this._array[this._size - i - 1];
					i++;
				}
				return;
			}
			while (i < this._size)
			{
				array.SetValue(this._array[this._size - i - 1], i + index);
				i++;
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Stack" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Stack" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005DDF RID: 24031 RVA: 0x00135D8B File Offset: 0x00133F8B
		public virtual IEnumerator GetEnumerator()
		{
			return new Stack.StackEnumerator(this);
		}

		/// <summary>Returns the object at the top of the <see cref="T:System.Collections.Stack" /> without removing it.</summary>
		/// <returns>The <see cref="T:System.Object" /> at the top of the <see cref="T:System.Collections.Stack" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Stack" /> is empty. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005DE0 RID: 24032 RVA: 0x00135D93 File Offset: 0x00133F93
		public virtual object Peek()
		{
			if (this._size == 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Stack empty."));
			}
			return this._array[this._size - 1];
		}

		/// <summary>Removes and returns the object at the top of the <see cref="T:System.Collections.Stack" />.</summary>
		/// <returns>The <see cref="T:System.Object" /> removed from the top of the <see cref="T:System.Collections.Stack" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Stack" /> is empty. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005DE1 RID: 24033 RVA: 0x00135DBC File Offset: 0x00133FBC
		public virtual object Pop()
		{
			if (this._size == 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Stack empty."));
			}
			this._version++;
			object[] array = this._array;
			int num = this._size - 1;
			this._size = num;
			object obj = array[num];
			this._array[this._size] = null;
			return obj;
		}

		/// <summary>Inserts an object at the top of the <see cref="T:System.Collections.Stack" />.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to push onto the <see cref="T:System.Collections.Stack" />. The value can be null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005DE2 RID: 24034 RVA: 0x00135E18 File Offset: 0x00134018
		public virtual void Push(object obj)
		{
			if (this._size == this._array.Length)
			{
				object[] array = new object[2 * this._array.Length];
				Array.Copy(this._array, 0, array, 0, this._size);
				this._array = array;
			}
			object[] array2 = this._array;
			int size = this._size;
			this._size = size + 1;
			array2[size] = obj;
			this._version++;
		}

		/// <summary>Returns a synchronized (thread safe) wrapper for the <see cref="T:System.Collections.Stack" />.</summary>
		/// <returns>A synchronized wrapper around the <see cref="T:System.Collections.Stack" />.</returns>
		/// <param name="stack">The <see cref="T:System.Collections.Stack" /> to synchronize. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stack" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005DE3 RID: 24035 RVA: 0x00135E87 File Offset: 0x00134087
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static Stack Synchronized(Stack stack)
		{
			if (stack == null)
			{
				throw new ArgumentNullException("stack");
			}
			return new Stack.SyncStack(stack);
		}

		/// <summary>Copies the <see cref="T:System.Collections.Stack" /> to a new array.</summary>
		/// <returns>A new array containing copies of the elements of the <see cref="T:System.Collections.Stack" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005DE4 RID: 24036 RVA: 0x00135EA0 File Offset: 0x001340A0
		public virtual object[] ToArray()
		{
			object[] array = new object[this._size];
			for (int i = 0; i < this._size; i++)
			{
				array[i] = this._array[this._size - i - 1];
			}
			return array;
		}

		// Token: 0x04002FA6 RID: 12198
		private object[] _array;

		// Token: 0x04002FA7 RID: 12199
		private int _size;

		// Token: 0x04002FA8 RID: 12200
		private int _version;

		// Token: 0x04002FA9 RID: 12201
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04002FAA RID: 12202
		private const int _defaultCapacity = 10;

		// Token: 0x020009E8 RID: 2536
		[Serializable]
		private class SyncStack : Stack
		{
			// Token: 0x06005DE5 RID: 24037 RVA: 0x00135EDF File Offset: 0x001340DF
			internal SyncStack(Stack stack)
			{
				this._s = stack;
				this._root = stack.SyncRoot;
			}

			// Token: 0x170010A1 RID: 4257
			// (get) Token: 0x06005DE6 RID: 24038 RVA: 0x00003B29 File Offset: 0x00001D29
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170010A2 RID: 4258
			// (get) Token: 0x06005DE7 RID: 24039 RVA: 0x00135EFA File Offset: 0x001340FA
			public override object SyncRoot
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x170010A3 RID: 4259
			// (get) Token: 0x06005DE8 RID: 24040 RVA: 0x00135F04 File Offset: 0x00134104
			public override int Count
			{
				get
				{
					object root = this._root;
					int count;
					lock (root)
					{
						count = this._s.Count;
					}
					return count;
				}
			}

			// Token: 0x06005DE9 RID: 24041 RVA: 0x00135F4C File Offset: 0x0013414C
			public override bool Contains(object obj)
			{
				object root = this._root;
				bool flag2;
				lock (root)
				{
					flag2 = this._s.Contains(obj);
				}
				return flag2;
			}

			// Token: 0x06005DEA RID: 24042 RVA: 0x00135F94 File Offset: 0x00134194
			public override object Clone()
			{
				object root = this._root;
				object obj;
				lock (root)
				{
					obj = new Stack.SyncStack((Stack)this._s.Clone());
				}
				return obj;
			}

			// Token: 0x06005DEB RID: 24043 RVA: 0x00135FE8 File Offset: 0x001341E8
			public override void Clear()
			{
				object root = this._root;
				lock (root)
				{
					this._s.Clear();
				}
			}

			// Token: 0x06005DEC RID: 24044 RVA: 0x00136030 File Offset: 0x00134230
			public override void CopyTo(Array array, int arrayIndex)
			{
				object root = this._root;
				lock (root)
				{
					this._s.CopyTo(array, arrayIndex);
				}
			}

			// Token: 0x06005DED RID: 24045 RVA: 0x00136078 File Offset: 0x00134278
			public override void Push(object value)
			{
				object root = this._root;
				lock (root)
				{
					this._s.Push(value);
				}
			}

			// Token: 0x06005DEE RID: 24046 RVA: 0x001360C0 File Offset: 0x001342C0
			public override object Pop()
			{
				object root = this._root;
				object obj;
				lock (root)
				{
					obj = this._s.Pop();
				}
				return obj;
			}

			// Token: 0x06005DEF RID: 24047 RVA: 0x00136108 File Offset: 0x00134308
			public override IEnumerator GetEnumerator()
			{
				object root = this._root;
				IEnumerator enumerator;
				lock (root)
				{
					enumerator = this._s.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x06005DF0 RID: 24048 RVA: 0x00136150 File Offset: 0x00134350
			public override object Peek()
			{
				object root = this._root;
				object obj;
				lock (root)
				{
					obj = this._s.Peek();
				}
				return obj;
			}

			// Token: 0x06005DF1 RID: 24049 RVA: 0x00136198 File Offset: 0x00134398
			public override object[] ToArray()
			{
				object root = this._root;
				object[] array;
				lock (root)
				{
					array = this._s.ToArray();
				}
				return array;
			}

			// Token: 0x04002FAB RID: 12203
			private Stack _s;

			// Token: 0x04002FAC RID: 12204
			private object _root;
		}

		// Token: 0x020009E9 RID: 2537
		[Serializable]
		private class StackEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x06005DF2 RID: 24050 RVA: 0x001361E0 File Offset: 0x001343E0
			internal StackEnumerator(Stack stack)
			{
				this._stack = stack;
				this._version = this._stack._version;
				this._index = -2;
				this.currentElement = null;
			}

			// Token: 0x06005DF3 RID: 24051 RVA: 0x0002C3A3 File Offset: 0x0002A5A3
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x06005DF4 RID: 24052 RVA: 0x00136210 File Offset: 0x00134410
			public virtual bool MoveNext()
			{
				if (this._version != this._stack._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Collection was modified; enumeration operation may not execute."));
				}
				if (this._index == -2)
				{
					this._index = this._stack._size - 1;
					bool flag = this._index >= 0;
					if (flag)
					{
						this.currentElement = this._stack._array[this._index];
					}
					return flag;
				}
				if (this._index == -1)
				{
					return false;
				}
				int num = this._index - 1;
				this._index = num;
				bool flag2 = num >= 0;
				if (flag2)
				{
					this.currentElement = this._stack._array[this._index];
					return flag2;
				}
				this.currentElement = null;
				return flag2;
			}

			// Token: 0x170010A4 RID: 4260
			// (get) Token: 0x06005DF5 RID: 24053 RVA: 0x001362CA File Offset: 0x001344CA
			public virtual object Current
			{
				get
				{
					if (this._index == -2)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration has not started. Call MoveNext."));
					}
					if (this._index == -1)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration already finished."));
					}
					return this.currentElement;
				}
			}

			// Token: 0x06005DF6 RID: 24054 RVA: 0x00136305 File Offset: 0x00134505
			public virtual void Reset()
			{
				if (this._version != this._stack._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Collection was modified; enumeration operation may not execute."));
				}
				this._index = -2;
				this.currentElement = null;
			}

			// Token: 0x04002FAD RID: 12205
			private Stack _stack;

			// Token: 0x04002FAE RID: 12206
			private int _index;

			// Token: 0x04002FAF RID: 12207
			private int _version;

			// Token: 0x04002FB0 RID: 12208
			private object currentElement;
		}

		// Token: 0x020009EA RID: 2538
		internal class StackDebugView
		{
			// Token: 0x06005DF7 RID: 24055 RVA: 0x00136339 File Offset: 0x00134539
			public StackDebugView(Stack stack)
			{
				if (stack == null)
				{
					throw new ArgumentNullException("stack");
				}
				this.stack = stack;
			}

			// Token: 0x170010A5 RID: 4261
			// (get) Token: 0x06005DF8 RID: 24056 RVA: 0x00136356 File Offset: 0x00134556
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public object[] Items
			{
				get
				{
					return this.stack.ToArray();
				}
			}

			// Token: 0x04002FB1 RID: 12209
			private Stack stack;
		}
	}
}
