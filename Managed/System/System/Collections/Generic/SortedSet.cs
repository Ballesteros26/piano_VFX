using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Collections.Generic
{
	/// <summary>Represents a collection of objects that is maintained in sorted order.</summary>
	/// <typeparam name="T">The type of elements in the set.</typeparam>
	// Token: 0x02000739 RID: 1849
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ICollectionDebugView<>))]
	[Serializable]
	public class SortedSet<T> : ISet<T>, ICollection<T>, IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>, ISerializable, IDeserializationCallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedSet`1" /> class. </summary>
		// Token: 0x06003A84 RID: 14980 RVA: 0x000D4AC2 File Offset: 0x000D2CC2
		public SortedSet()
		{
			this.comparer = Comparer<T>.Default;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedSet`1" /> class that uses a specified comparer.</summary>
		/// <param name="comparer">The default comparer to use for comparing objects. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="comparer" /> is null.</exception>
		// Token: 0x06003A85 RID: 14981 RVA: 0x000D4AD5 File Offset: 0x000D2CD5
		public SortedSet(IComparer<T> comparer)
		{
			this.comparer = comparer ?? Comparer<T>.Default;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedSet`1" /> class that contains elements copied from a specified enumerable collection.</summary>
		/// <param name="collection">The enumerable collection to be copied. </param>
		// Token: 0x06003A86 RID: 14982 RVA: 0x000D4AED File Offset: 0x000D2CED
		public SortedSet(IEnumerable<T> collection)
			: this(collection, Comparer<T>.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedSet`1" /> class that contains elements copied from a specified enumerable collection and that uses a specified comparer.</summary>
		/// <param name="collection">The enumerable collection to be copied. </param>
		/// <param name="comparer">The default comparer to use for comparing objects. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> is null.</exception>
		// Token: 0x06003A87 RID: 14983 RVA: 0x000D4AFC File Offset: 0x000D2CFC
		public SortedSet(IEnumerable<T> collection, IComparer<T> comparer)
			: this(comparer)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			SortedSet<T> sortedSet = collection as SortedSet<T>;
			if (sortedSet != null && !(sortedSet is SortedSet<T>.TreeSubSet) && this.HasEqualComparer(sortedSet))
			{
				if (sortedSet.Count > 0)
				{
					this.count = sortedSet.count;
					this.root = sortedSet.root.DeepClone(this.count);
				}
				return;
			}
			int num;
			T[] array = EnumerableHelpers.ToArray<T>(collection, out num);
			if (num > 0)
			{
				comparer = this.comparer;
				Array.Sort<T>(array, 0, num, comparer);
				int num2 = 1;
				for (int i = 1; i < num; i++)
				{
					if (comparer.Compare(array[i], array[i - 1]) != 0)
					{
						array[num2++] = array[i];
					}
				}
				num = num2;
				this.root = SortedSet<T>.ConstructRootFromSortedArray(array, 0, num - 1, null);
				this.count = num;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedSet`1" /> class that contains serialized data.</summary>
		/// <param name="info">The object that contains the information that is required to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <param name="context">The structure that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		// Token: 0x06003A88 RID: 14984 RVA: 0x000D4BDD File Offset: 0x000D2DDD
		protected SortedSet(SerializationInfo info, StreamingContext context)
		{
			this.siInfo = info;
		}

		// Token: 0x06003A89 RID: 14985 RVA: 0x000D4BEC File Offset: 0x000D2DEC
		private void AddAllElements(IEnumerable<T> collection)
		{
			foreach (T t in collection)
			{
				if (!this.Contains(t))
				{
					this.Add(t);
				}
			}
		}

		// Token: 0x06003A8A RID: 14986 RVA: 0x000D4C40 File Offset: 0x000D2E40
		private void RemoveAllElements(IEnumerable<T> collection)
		{
			T min = this.Min;
			T max = this.Max;
			foreach (T t in collection)
			{
				if (this.comparer.Compare(t, min) >= 0 && this.comparer.Compare(t, max) <= 0 && this.Contains(t))
				{
					this.Remove(t);
				}
			}
		}

		// Token: 0x06003A8B RID: 14987 RVA: 0x000D4CC0 File Offset: 0x000D2EC0
		private bool ContainsAllElements(IEnumerable<T> collection)
		{
			foreach (T t in collection)
			{
				if (!this.Contains(t))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003A8C RID: 14988 RVA: 0x000D4D14 File Offset: 0x000D2F14
		internal virtual bool InOrderTreeWalk(TreeWalkPredicate<T> action)
		{
			if (this.root == null)
			{
				return true;
			}
			Stack<SortedSet<T>.Node> stack = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.Log2(this.Count + 1));
			for (SortedSet<T>.Node node = this.root; node != null; node = node.Left)
			{
				stack.Push(node);
			}
			while (stack.Count != 0)
			{
				SortedSet<T>.Node node = stack.Pop();
				if (!action(node))
				{
					return false;
				}
				for (SortedSet<T>.Node node2 = node.Right; node2 != null; node2 = node2.Left)
				{
					stack.Push(node2);
				}
			}
			return true;
		}

		// Token: 0x06003A8D RID: 14989 RVA: 0x000D4D94 File Offset: 0x000D2F94
		internal virtual bool BreadthFirstTreeWalk(TreeWalkPredicate<T> action)
		{
			if (this.root == null)
			{
				return true;
			}
			Queue<SortedSet<T>.Node> queue = new Queue<SortedSet<T>.Node>();
			queue.Enqueue(this.root);
			while (queue.Count != 0)
			{
				SortedSet<T>.Node node = queue.Dequeue();
				if (!action(node))
				{
					return false;
				}
				if (node.Left != null)
				{
					queue.Enqueue(node.Left);
				}
				if (node.Right != null)
				{
					queue.Enqueue(node.Right);
				}
			}
			return true;
		}

		/// <summary>Gets the number of elements in the <see cref="T:System.Collections.Generic.SortedSet`1" />.</summary>
		/// <returns>The number of elements in the <see cref="T:System.Collections.Generic.SortedSet`1" />.</returns>
		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x06003A8E RID: 14990 RVA: 0x000D4E02 File Offset: 0x000D3002
		public int Count
		{
			get
			{
				this.VersionCheck();
				return this.count;
			}
		}

		/// <summary>Gets the <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> object that is used to determine equality for the values in the <see cref="T:System.Collections.Generic.SortedSet`1" />.</summary>
		/// <returns>The comparer that is used to determine equality for the values in the <see cref="T:System.Collections.Generic.SortedSet`1" />.</returns>
		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x06003A8F RID: 14991 RVA: 0x000D4E10 File Offset: 0x000D3010
		public IComparer<T> Comparer
		{
			get
			{
				return this.comparer;
			}
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Collections.ICollection" /> is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise, false.</returns>
		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x06003A90 RID: 14992 RVA: 0x00004240 File Offset: 0x00002440
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized; otherwise, false.</returns>
		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x06003A91 RID: 14993 RVA: 0x00004240 File Offset: 0x00002440
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />. In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />, this property always returns the current instance.</returns>
		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x06003A92 RID: 14994 RVA: 0x000D4E18 File Offset: 0x000D3018
		object ICollection.SyncRoot
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

		// Token: 0x06003A93 RID: 14995 RVA: 0x000027E8 File Offset: 0x000009E8
		internal virtual void VersionCheck()
		{
		}

		// Token: 0x06003A94 RID: 14996 RVA: 0x000027E2 File Offset: 0x000009E2
		internal virtual bool IsWithinRange(T item)
		{
			return true;
		}

		/// <summary>Adds an element to the set and returns a value that indicates if it was successfully added.</summary>
		/// <returns>true if <paramref name="item" /> is added to the set; otherwise, false. </returns>
		/// <param name="item">The element to add to the set.</param>
		// Token: 0x06003A95 RID: 14997 RVA: 0x000D4E3A File Offset: 0x000D303A
		public bool Add(T item)
		{
			return this.AddIfNotPresent(item);
		}

		/// <summary>Adds an item to an <see cref="T:System.Collections.Generic.ICollection`1" /> object.</summary>
		/// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" /> object.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.</exception>
		// Token: 0x06003A96 RID: 14998 RVA: 0x000D4E43 File Offset: 0x000D3043
		void ICollection<T>.Add(T item)
		{
			this.Add(item);
		}

		// Token: 0x06003A97 RID: 14999 RVA: 0x000D4E50 File Offset: 0x000D3050
		internal virtual bool AddIfNotPresent(T item)
		{
			if (this.root == null)
			{
				this.root = new SortedSet<T>.Node(item, NodeColor.Black);
				this.count = 1;
				this.version++;
				return true;
			}
			SortedSet<T>.Node node = this.root;
			SortedSet<T>.Node node2 = null;
			SortedSet<T>.Node node3 = null;
			SortedSet<T>.Node node4 = null;
			this.version++;
			int num = 0;
			while (node != null)
			{
				num = this.comparer.Compare(item, node.Item);
				if (num == 0)
				{
					this.root.ColorBlack();
					return false;
				}
				if (node.Is4Node)
				{
					node.Split4Node();
					if (SortedSet<T>.Node.IsNonNullRed(node2))
					{
						this.InsertionBalance(node, ref node2, node3, node4);
					}
				}
				node4 = node3;
				node3 = node2;
				node2 = node;
				node = ((num < 0) ? node.Left : node.Right);
			}
			SortedSet<T>.Node node5 = new SortedSet<T>.Node(item, NodeColor.Red);
			if (num > 0)
			{
				node2.Right = node5;
			}
			else
			{
				node2.Left = node5;
			}
			if (node2.IsRed)
			{
				this.InsertionBalance(node5, ref node2, node3, node4);
			}
			this.root.ColorBlack();
			this.count++;
			return true;
		}

		/// <summary>Removes a specified item from the <see cref="T:System.Collections.Generic.SortedSet`1" />.</summary>
		/// <returns>true if the element is found and successfully removed; otherwise, false. </returns>
		/// <param name="item">The element to remove.</param>
		// Token: 0x06003A98 RID: 15000 RVA: 0x000D4F5A File Offset: 0x000D315A
		public bool Remove(T item)
		{
			return this.DoRemove(item);
		}

		// Token: 0x06003A99 RID: 15001 RVA: 0x000D4F64 File Offset: 0x000D3164
		internal virtual bool DoRemove(T item)
		{
			if (this.root == null)
			{
				return false;
			}
			this.version++;
			SortedSet<T>.Node node = this.root;
			SortedSet<T>.Node node2 = null;
			SortedSet<T>.Node node3 = null;
			SortedSet<T>.Node node4 = null;
			SortedSet<T>.Node node5 = null;
			bool flag = false;
			while (node != null)
			{
				if (node.Is2Node)
				{
					if (node2 == null)
					{
						node.ColorRed();
					}
					else
					{
						SortedSet<T>.Node node6 = node2.GetSibling(node);
						if (node6.IsRed)
						{
							if (node2.Right == node6)
							{
								node2.RotateLeft();
							}
							else
							{
								node2.RotateRight();
							}
							node2.ColorRed();
							node6.ColorBlack();
							this.ReplaceChildOrRoot(node3, node2, node6);
							node3 = node6;
							if (node2 == node4)
							{
								node5 = node6;
							}
							node6 = node2.GetSibling(node);
						}
						if (node6.Is2Node)
						{
							node2.Merge2Nodes();
						}
						else
						{
							SortedSet<T>.Node node7 = node2.Rotate(node2.GetRotation(node, node6));
							node7.Color = node2.Color;
							node2.ColorBlack();
							node.ColorRed();
							this.ReplaceChildOrRoot(node3, node2, node7);
							if (node2 == node4)
							{
								node5 = node7;
							}
						}
					}
				}
				int num = (flag ? (-1) : this.comparer.Compare(item, node.Item));
				if (num == 0)
				{
					flag = true;
					node4 = node;
					node5 = node2;
				}
				node3 = node2;
				node2 = node;
				node = ((num < 0) ? node.Left : node.Right);
			}
			if (node4 != null)
			{
				this.ReplaceNode(node4, node5, node2, node3);
				this.count--;
			}
			SortedSet<T>.Node node8 = this.root;
			if (node8 != null)
			{
				node8.ColorBlack();
			}
			return flag;
		}

		/// <summary>Removes all elements from the set.</summary>
		// Token: 0x06003A9A RID: 15002 RVA: 0x000D50D0 File Offset: 0x000D32D0
		public virtual void Clear()
		{
			this.root = null;
			this.count = 0;
			this.version++;
		}

		/// <summary>Determines whether the set contains a specific element.</summary>
		/// <returns>true if the set contains <paramref name="item" />; otherwise, false.</returns>
		/// <param name="item">The element to locate in the set.</param>
		// Token: 0x06003A9B RID: 15003 RVA: 0x000D50EE File Offset: 0x000D32EE
		public virtual bool Contains(T item)
		{
			return this.FindNode(item) != null;
		}

		/// <summary>Copies the complete <see cref="T:System.Collections.Generic.SortedSet`1" /> to a compatible one-dimensional array, starting at the beginning of the target array.</summary>
		/// <param name="array">A one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.SortedSet`1" />.</param>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.SortedSet`1" /> exceeds the number of elements that the destination array can contain. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		// Token: 0x06003A9C RID: 15004 RVA: 0x000D50FA File Offset: 0x000D32FA
		public void CopyTo(T[] array)
		{
			this.CopyTo(array, 0, this.Count);
		}

		/// <summary>Copies the complete <see cref="T:System.Collections.Generic.SortedSet`1" /> to a compatible one-dimensional array, starting at the specified array index.</summary>
		/// <param name="array">A one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.SortedSet`1" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source array is greater than the available space from <paramref name="index" /> to the end of the destination array.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		// Token: 0x06003A9D RID: 15005 RVA: 0x000D510A File Offset: 0x000D330A
		public void CopyTo(T[] array, int index)
		{
			this.CopyTo(array, index, this.Count);
		}

		/// <summary>Copies a specified number of elements from <see cref="T:System.Collections.Generic.SortedSet`1" /> to a compatible one-dimensional array, starting at the specified array index.</summary>
		/// <param name="array">A one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.SortedSet`1" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <param name="count">The number of elements to copy.</param>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source array is greater than the available space from <paramref name="index" /> to the end of the destination array.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="count" /> is less than zero.</exception>
		// Token: 0x06003A9E RID: 15006 RVA: 0x000D511C File Offset: 0x000D331C
		public void CopyTo(T[] array, int index, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", index, "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (count > array.Length - index)
			{
				throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
			}
			count += index;
			this.InOrderTreeWalk(delegate(SortedSet<T>.Node node)
			{
				if (index >= count)
				{
					return false;
				}
				T[] array2 = array;
				int index2 = index;
				index = index2 + 1;
				array2[index2] = node.Item;
				return true;
			});
		}

		/// <summary>Copies the complete <see cref="T:System.Collections.Generic.SortedSet`1" /> to a compatible one-dimensional array, starting at the specified array index.</summary>
		/// <param name="array">A one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.SortedSet`1" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source array is greater than the available space from <paramref name="index" /> to the end of the destination array. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		// Token: 0x06003A9F RID: 15007 RVA: 0x000D51DC File Offset: 0x000D33DC
		void ICollection.CopyTo(Array array, int index)
		{
			SortedSet<T>.<>c__DisplayClass53_0 CS$<>8__locals1 = new SortedSet<T>.<>c__DisplayClass53_0();
			CS$<>8__locals1.index = index;
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
			}
			if (array.GetLowerBound(0) != 0)
			{
				throw new ArgumentException("The lower bound of target array must be zero.", "array");
			}
			if (CS$<>8__locals1.index < 0)
			{
				throw new ArgumentOutOfRangeException("index", CS$<>8__locals1.index, "Non-negative number required.");
			}
			if (array.Length - CS$<>8__locals1.index < this.Count)
			{
				throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
			}
			T[] array2 = array as T[];
			if (array2 != null)
			{
				this.CopyTo(array2, CS$<>8__locals1.index);
				return;
			}
			object[] objects = array as object[];
			if (objects == null)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
			}
			try
			{
				this.InOrderTreeWalk(delegate(SortedSet<T>.Node node)
				{
					object[] objects2 = objects;
					int index2 = CS$<>8__locals1.index;
					CS$<>8__locals1.index = index2 + 1;
					objects2[index2] = node.Item;
					return true;
				});
			}
			catch (ArrayTypeMismatchException)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
			}
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.SortedSet`1" />.</summary>
		/// <returns>An enumerator that iterates through the <see cref="T:System.Collections.Generic.SortedSet`1" /> in sorted order.</returns>
		// Token: 0x06003AA0 RID: 15008 RVA: 0x000D52FC File Offset: 0x000D34FC
		public SortedSet<T>.Enumerator GetEnumerator()
		{
			return new SortedSet<T>.Enumerator(this);
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06003AA1 RID: 15009 RVA: 0x000D5304 File Offset: 0x000D3504
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06003AA2 RID: 15010 RVA: 0x000D5304 File Offset: 0x000D3504
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06003AA3 RID: 15011 RVA: 0x000D5314 File Offset: 0x000D3514
		private void InsertionBalance(SortedSet<T>.Node current, ref SortedSet<T>.Node parent, SortedSet<T>.Node grandParent, SortedSet<T>.Node greatGrandParent)
		{
			bool flag = grandParent.Right == parent;
			bool flag2 = parent.Right == current;
			SortedSet<T>.Node node;
			if (flag == flag2)
			{
				node = (flag2 ? grandParent.RotateLeft() : grandParent.RotateRight());
			}
			else
			{
				node = (flag2 ? grandParent.RotateLeftRight() : grandParent.RotateRightLeft());
				parent = greatGrandParent;
			}
			grandParent.ColorRed();
			node.ColorBlack();
			this.ReplaceChildOrRoot(greatGrandParent, grandParent, node);
		}

		// Token: 0x06003AA4 RID: 15012 RVA: 0x000D5379 File Offset: 0x000D3579
		private void ReplaceChildOrRoot(SortedSet<T>.Node parent, SortedSet<T>.Node child, SortedSet<T>.Node newChild)
		{
			if (parent != null)
			{
				parent.ReplaceChild(child, newChild);
				return;
			}
			this.root = newChild;
		}

		// Token: 0x06003AA5 RID: 15013 RVA: 0x000D5390 File Offset: 0x000D3590
		private void ReplaceNode(SortedSet<T>.Node match, SortedSet<T>.Node parentOfMatch, SortedSet<T>.Node successor, SortedSet<T>.Node parentOfSuccessor)
		{
			if (successor == match)
			{
				successor = match.Left;
			}
			else
			{
				SortedSet<T>.Node right = successor.Right;
				if (right != null)
				{
					right.ColorBlack();
				}
				if (parentOfSuccessor != match)
				{
					parentOfSuccessor.Left = successor.Right;
					successor.Right = match.Right;
				}
				successor.Left = match.Left;
			}
			if (successor != null)
			{
				successor.Color = match.Color;
			}
			this.ReplaceChildOrRoot(parentOfMatch, match, successor);
		}

		// Token: 0x06003AA6 RID: 15014 RVA: 0x000D5400 File Offset: 0x000D3600
		internal virtual SortedSet<T>.Node FindNode(T item)
		{
			int num;
			for (SortedSet<T>.Node node = this.root; node != null; node = ((num < 0) ? node.Left : node.Right))
			{
				num = this.comparer.Compare(item, node.Item);
				if (num == 0)
				{
					return node;
				}
			}
			return null;
		}

		// Token: 0x06003AA7 RID: 15015 RVA: 0x000D5448 File Offset: 0x000D3648
		internal virtual int InternalIndexOf(T item)
		{
			SortedSet<T>.Node node = this.root;
			int num = 0;
			while (node != null)
			{
				int num2 = this.comparer.Compare(item, node.Item);
				if (num2 == 0)
				{
					return num;
				}
				node = ((num2 < 0) ? node.Left : node.Right);
				num = ((num2 < 0) ? (2 * num + 1) : (2 * num + 2));
			}
			return -1;
		}

		// Token: 0x06003AA8 RID: 15016 RVA: 0x000D54A0 File Offset: 0x000D36A0
		internal SortedSet<T>.Node FindRange(T from, T to)
		{
			return this.FindRange(from, to, true, true);
		}

		// Token: 0x06003AA9 RID: 15017 RVA: 0x000D54AC File Offset: 0x000D36AC
		internal SortedSet<T>.Node FindRange(T from, T to, bool lowerBoundActive, bool upperBoundActive)
		{
			SortedSet<T>.Node node = this.root;
			while (node != null)
			{
				if (lowerBoundActive && this.comparer.Compare(from, node.Item) > 0)
				{
					node = node.Right;
				}
				else
				{
					if (!upperBoundActive || this.comparer.Compare(to, node.Item) >= 0)
					{
						return node;
					}
					node = node.Left;
				}
			}
			return null;
		}

		// Token: 0x06003AAA RID: 15018 RVA: 0x000D550B File Offset: 0x000D370B
		internal void UpdateVersion()
		{
			this.version++;
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEqualityComparer" /> object that can be used to create a collection that contains individual sets.</summary>
		/// <returns>A comparer for creating a collection of sets.</returns>
		// Token: 0x06003AAB RID: 15019 RVA: 0x000D551B File Offset: 0x000D371B
		public static IEqualityComparer<SortedSet<T>> CreateSetComparer()
		{
			return SortedSet<T>.CreateSetComparer(null);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEqualityComparer" /> object, according to a specified comparer, that can be used to create a collection that contains individual sets.</summary>
		/// <returns>A comparer for creating a collection of sets.</returns>
		/// <param name="memberEqualityComparer">The comparer to use for creating the returned comparer.</param>
		// Token: 0x06003AAC RID: 15020 RVA: 0x000D5523 File Offset: 0x000D3723
		public static IEqualityComparer<SortedSet<T>> CreateSetComparer(IEqualityComparer<T> memberEqualityComparer)
		{
			return new SortedSetEqualityComparer<T>(memberEqualityComparer);
		}

		// Token: 0x06003AAD RID: 15021 RVA: 0x000D552C File Offset: 0x000D372C
		internal static bool SortedSetEquals(SortedSet<T> set1, SortedSet<T> set2, IComparer<T> comparer)
		{
			if (set1 == null)
			{
				return set2 == null;
			}
			if (set2 == null)
			{
				return false;
			}
			if (set1.HasEqualComparer(set2))
			{
				return set1.Count == set2.Count && set1.SetEquals(set2);
			}
			bool flag = false;
			foreach (T t in set1)
			{
				flag = false;
				foreach (T t2 in set2)
				{
					if (comparer.Compare(t, t2) == 0)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003AAE RID: 15022 RVA: 0x000D55F8 File Offset: 0x000D37F8
		private bool HasEqualComparer(SortedSet<T> other)
		{
			return this.Comparer == other.Comparer || this.Comparer.Equals(other.Comparer);
		}

		/// <summary>Modifies the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object so that it contains all elements that are present in either the current object or the specified collection. </summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x06003AAF RID: 15023 RVA: 0x000D561C File Offset: 0x000D381C
		public void UnionWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			SortedSet<T>.TreeSubSet treeSubSet = this as SortedSet<T>.TreeSubSet;
			if (treeSubSet != null)
			{
				this.VersionCheck();
			}
			if (sortedSet != null && treeSubSet == null && this.count == 0)
			{
				SortedSet<T> sortedSet2 = new SortedSet<T>(sortedSet, this.comparer);
				this.root = sortedSet2.root;
				this.count = sortedSet2.count;
				this.version++;
				return;
			}
			if (sortedSet != null && treeSubSet == null && this.HasEqualComparer(sortedSet) && sortedSet.Count > this.Count / 2)
			{
				T[] array = new T[sortedSet.Count + this.Count];
				int num = 0;
				SortedSet<T>.Enumerator enumerator = this.GetEnumerator();
				SortedSet<T>.Enumerator enumerator2 = sortedSet.GetEnumerator();
				bool flag = !enumerator.MoveNext();
				bool flag2 = !enumerator2.MoveNext();
				while (!flag && !flag2)
				{
					int num2 = this.Comparer.Compare(enumerator.Current, enumerator2.Current);
					if (num2 < 0)
					{
						array[num++] = enumerator.Current;
						flag = !enumerator.MoveNext();
					}
					else if (num2 == 0)
					{
						array[num++] = enumerator2.Current;
						flag = !enumerator.MoveNext();
						flag2 = !enumerator2.MoveNext();
					}
					else
					{
						array[num++] = enumerator2.Current;
						flag2 = !enumerator2.MoveNext();
					}
				}
				if (!flag || !flag2)
				{
					SortedSet<T>.Enumerator enumerator3 = (flag ? enumerator2 : enumerator);
					do
					{
						array[num++] = enumerator3.Current;
					}
					while (enumerator3.MoveNext());
				}
				this.root = null;
				this.root = SortedSet<T>.ConstructRootFromSortedArray(array, 0, num - 1, null);
				this.count = num;
				this.version++;
				return;
			}
			this.AddAllElements(other);
		}

		// Token: 0x06003AB0 RID: 15024 RVA: 0x000D5808 File Offset: 0x000D3A08
		private static SortedSet<T>.Node ConstructRootFromSortedArray(T[] arr, int startIndex, int endIndex, SortedSet<T>.Node redNode)
		{
			int num = endIndex - startIndex + 1;
			SortedSet<T>.Node node;
			switch (num)
			{
			case 0:
				return null;
			case 1:
				node = new SortedSet<T>.Node(arr[startIndex], NodeColor.Black);
				if (redNode != null)
				{
					node.Left = redNode;
				}
				break;
			case 2:
				node = new SortedSet<T>.Node(arr[startIndex], NodeColor.Black);
				node.Right = new SortedSet<T>.Node(arr[endIndex], NodeColor.Black);
				node.Right.ColorRed();
				if (redNode != null)
				{
					node.Left = redNode;
				}
				break;
			case 3:
				node = new SortedSet<T>.Node(arr[startIndex + 1], NodeColor.Black);
				node.Left = new SortedSet<T>.Node(arr[startIndex], NodeColor.Black);
				node.Right = new SortedSet<T>.Node(arr[endIndex], NodeColor.Black);
				if (redNode != null)
				{
					node.Left.Left = redNode;
				}
				break;
			default:
			{
				int num2 = (startIndex + endIndex) / 2;
				node = new SortedSet<T>.Node(arr[num2], NodeColor.Black);
				node.Left = SortedSet<T>.ConstructRootFromSortedArray(arr, startIndex, num2 - 1, redNode);
				node.Right = ((num % 2 == 0) ? SortedSet<T>.ConstructRootFromSortedArray(arr, num2 + 2, endIndex, new SortedSet<T>.Node(arr[num2 + 1], NodeColor.Red)) : SortedSet<T>.ConstructRootFromSortedArray(arr, num2 + 1, endIndex, null));
				break;
			}
			}
			return node;
		}

		/// <summary>Modifies the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object so that it contains only elements that are also in a specified collection.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x06003AB1 RID: 15025 RVA: 0x000D5934 File Offset: 0x000D3B34
		public virtual void IntersectWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.Count == 0)
			{
				return;
			}
			if (other == this)
			{
				return;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			SortedSet<T>.TreeSubSet treeSubSet = this as SortedSet<T>.TreeSubSet;
			if (treeSubSet != null)
			{
				this.VersionCheck();
			}
			if (sortedSet != null && treeSubSet == null && this.HasEqualComparer(sortedSet))
			{
				T[] array = new T[this.Count];
				int num = 0;
				SortedSet<T>.Enumerator enumerator = this.GetEnumerator();
				SortedSet<T>.Enumerator enumerator2 = sortedSet.GetEnumerator();
				bool flag = !enumerator.MoveNext();
				bool flag2 = !enumerator2.MoveNext();
				T max = this.Max;
				T min = this.Min;
				while (!flag && !flag2 && this.Comparer.Compare(enumerator2.Current, max) <= 0)
				{
					int num2 = this.Comparer.Compare(enumerator.Current, enumerator2.Current);
					if (num2 < 0)
					{
						flag = !enumerator.MoveNext();
					}
					else if (num2 == 0)
					{
						array[num++] = enumerator2.Current;
						flag = !enumerator.MoveNext();
						flag2 = !enumerator2.MoveNext();
					}
					else
					{
						flag2 = !enumerator2.MoveNext();
					}
				}
				this.root = null;
				this.root = SortedSet<T>.ConstructRootFromSortedArray(array, 0, num - 1, null);
				this.count = num;
				this.version++;
				return;
			}
			this.IntersectWithEnumerable(other);
		}

		// Token: 0x06003AB2 RID: 15026 RVA: 0x000D5A94 File Offset: 0x000D3C94
		internal virtual void IntersectWithEnumerable(IEnumerable<T> other)
		{
			List<T> list = new List<T>(this.Count);
			foreach (T t in other)
			{
				if (this.Contains(t))
				{
					list.Add(t);
				}
			}
			this.Clear();
			foreach (T t2 in list)
			{
				this.Add(t2);
			}
		}

		/// <summary>Removes all elements that are in a specified collection from the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</summary>
		/// <param name="other">The collection of items to remove from the <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x06003AB3 RID: 15027 RVA: 0x000D5B38 File Offset: 0x000D3D38
		public void ExceptWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.count == 0)
			{
				return;
			}
			if (other == this)
			{
				this.Clear();
				return;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && this.HasEqualComparer(sortedSet))
			{
				if (this.comparer.Compare(sortedSet.Max, this.Min) < 0 || this.comparer.Compare(sortedSet.Min, this.Max) > 0)
				{
					return;
				}
				T min = this.Min;
				T max = this.Max;
				using (IEnumerator<T> enumerator = other.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						T t = enumerator.Current;
						if (this.comparer.Compare(t, min) >= 0)
						{
							if (this.comparer.Compare(t, max) > 0)
							{
								break;
							}
							this.Remove(t);
						}
					}
					return;
				}
			}
			this.RemoveAllElements(other);
		}

		/// <summary>Modifies the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object so that it contains only elements that are present either in the current object or in the specified collection, but not both.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x06003AB4 RID: 15028 RVA: 0x000D5C30 File Offset: 0x000D3E30
		public void SymmetricExceptWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.Count == 0)
			{
				this.UnionWith(other);
				return;
			}
			if (other == this)
			{
				this.Clear();
				return;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && this.HasEqualComparer(sortedSet))
			{
				this.SymmetricExceptWithSameComparer(sortedSet);
				return;
			}
			int num;
			T[] array = EnumerableHelpers.ToArray<T>(other, out num);
			Array.Sort<T>(array, 0, num, this.Comparer);
			this.SymmetricExceptWithSameComparer(array, num);
		}

		// Token: 0x06003AB5 RID: 15029 RVA: 0x000D5CA0 File Offset: 0x000D3EA0
		private void SymmetricExceptWithSameComparer(SortedSet<T> other)
		{
			foreach (T t in other)
			{
				if (!this.Contains(t))
				{
					this.Add(t);
				}
				else
				{
					this.Remove(t);
				}
			}
		}

		// Token: 0x06003AB6 RID: 15030 RVA: 0x000D5D04 File Offset: 0x000D3F04
		private void SymmetricExceptWithSameComparer(T[] other, int count)
		{
			if (count == 0)
			{
				return;
			}
			T t = other[0];
			for (int i = 0; i < count; i++)
			{
				while (i < count && i != 0 && this.comparer.Compare(other[i], t) == 0)
				{
					i++;
				}
				if (i >= count)
				{
					break;
				}
				T t2 = other[i];
				if (!this.Contains(t2))
				{
					this.Add(t2);
				}
				else
				{
					this.Remove(t2);
				}
				t = t2;
			}
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a subset of the specified collection.</summary>
		/// <returns>true if the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a subset of <paramref name="other" />; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x06003AB7 RID: 15031 RVA: 0x000D5D74 File Offset: 0x000D3F74
		[SecuritySafeCritical]
		public bool IsSubsetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.Count == 0)
			{
				return true;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && this.HasEqualComparer(sortedSet))
			{
				return this.Count <= sortedSet.Count && this.IsSubsetOfSortedSetWithSameComparer(sortedSet);
			}
			SortedSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, false);
			return elementCount.UniqueCount == this.Count && elementCount.UnfoundCount >= 0;
		}

		// Token: 0x06003AB8 RID: 15032 RVA: 0x000D5DEC File Offset: 0x000D3FEC
		private bool IsSubsetOfSortedSetWithSameComparer(SortedSet<T> asSorted)
		{
			SortedSet<T> viewBetween = asSorted.GetViewBetween(this.Min, this.Max);
			foreach (T t in this)
			{
				if (!viewBetween.Contains(t))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a proper subset of the specified collection.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a proper subset of <paramref name="other" />; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x06003AB9 RID: 15033 RVA: 0x000D5E58 File Offset: 0x000D4058
		[SecuritySafeCritical]
		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (other is ICollection && this.Count == 0)
			{
				return (other as ICollection).Count > 0;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && this.HasEqualComparer(sortedSet))
			{
				return this.Count < sortedSet.Count && this.IsSubsetOfSortedSetWithSameComparer(sortedSet);
			}
			SortedSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, false);
			return elementCount.UniqueCount == this.Count && elementCount.UnfoundCount > 0;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a superset of the specified collection.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a superset of <paramref name="other" />; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x06003ABA RID: 15034 RVA: 0x000D5EE0 File Offset: 0x000D40E0
		public bool IsSupersetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (other is ICollection && (other as ICollection).Count == 0)
			{
				return true;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet == null || !this.HasEqualComparer(sortedSet))
			{
				return this.ContainsAllElements(other);
			}
			if (this.Count < sortedSet.Count)
			{
				return false;
			}
			SortedSet<T> viewBetween = this.GetViewBetween(sortedSet.Min, sortedSet.Max);
			foreach (T t in sortedSet)
			{
				if (!viewBetween.Contains(t))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a proper superset of the specified collection.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a proper superset of <paramref name="other" />; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x06003ABB RID: 15035 RVA: 0x000D5F9C File Offset: 0x000D419C
		[SecuritySafeCritical]
		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.Count == 0)
			{
				return false;
			}
			if (other is ICollection && (other as ICollection).Count == 0)
			{
				return true;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet == null || !this.HasEqualComparer(sortedSet))
			{
				SortedSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, true);
				return elementCount.UniqueCount < this.Count && elementCount.UnfoundCount == 0;
			}
			if (sortedSet.Count >= this.Count)
			{
				return false;
			}
			SortedSet<T> viewBetween = this.GetViewBetween(sortedSet.Min, sortedSet.Max);
			foreach (T t in sortedSet)
			{
				if (!viewBetween.Contains(t))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object and the specified collection contain the same elements.</summary>
		/// <returns>true if the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object is equal to <paramref name="other" />; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x06003ABC RID: 15036 RVA: 0x000D6080 File Offset: 0x000D4280
		[SecuritySafeCritical]
		public bool SetEquals(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && this.HasEqualComparer(sortedSet))
			{
				SortedSet<T>.Enumerator enumerator = this.GetEnumerator();
				SortedSet<T>.Enumerator enumerator2 = sortedSet.GetEnumerator();
				bool flag = !enumerator.MoveNext();
				bool flag2 = !enumerator2.MoveNext();
				while (!flag && !flag2)
				{
					if (this.Comparer.Compare(enumerator.Current, enumerator2.Current) != 0)
					{
						return false;
					}
					flag = !enumerator.MoveNext();
					flag2 = !enumerator2.MoveNext();
				}
				return flag && flag2;
			}
			SortedSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, true);
			return elementCount.UniqueCount == this.Count && elementCount.UnfoundCount == 0;
		}

		/// <summary>Determines whether the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object and a specified collection share common elements.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.SortedSet`1" /> object and <paramref name="other" /> share at least one common element; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x06003ABD RID: 15037 RVA: 0x000D613C File Offset: 0x000D433C
		public bool Overlaps(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.Count == 0)
			{
				return false;
			}
			if (other is ICollection<T> && (other as ICollection<T>).Count == 0)
			{
				return false;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && this.HasEqualComparer(sortedSet) && (this.comparer.Compare(this.Min, sortedSet.Max) > 0 || this.comparer.Compare(this.Max, sortedSet.Min) < 0))
			{
				return false;
			}
			foreach (T t in other)
			{
				if (this.Contains(t))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003ABE RID: 15038 RVA: 0x000D6208 File Offset: 0x000D4408
		private unsafe SortedSet<T>.ElementCount CheckUniqueAndUnfoundElements(IEnumerable<T> other, bool returnIfUnfound)
		{
			SortedSet<T>.ElementCount elementCount;
			if (this.Count == 0)
			{
				int num = 0;
				using (IEnumerator<T> enumerator = other.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						T t = enumerator.Current;
						num++;
					}
				}
				elementCount.UniqueCount = 0;
				elementCount.UnfoundCount = num;
				return elementCount;
			}
			int num2 = BitHelper.ToIntArrayLength(this.Count);
			BitHelper bitHelper;
			int num3;
			int num4;
			checked
			{
				if (num2 <= 100)
				{
					bitHelper = new BitHelper(stackalloc int[unchecked((UIntPtr)num2) * 4], num2);
				}
				else
				{
					bitHelper = new BitHelper(new int[num2], num2);
				}
				num3 = 0;
				num4 = 0;
			}
			foreach (T t2 in other)
			{
				int num5 = this.InternalIndexOf(t2);
				if (num5 >= 0)
				{
					if (!bitHelper.IsMarked(num5))
					{
						bitHelper.MarkBit(num5);
						num4++;
					}
				}
				else
				{
					num3++;
					if (returnIfUnfound)
					{
						break;
					}
				}
			}
			elementCount.UniqueCount = num4;
			elementCount.UnfoundCount = num3;
			return elementCount;
		}

		/// <summary>Removes all elements that match the conditions defined by the specified predicate from a <see cref="T:System.Collections.Generic.SortedSet`1" />.</summary>
		/// <returns>The number of elements that were removed from the <see cref="T:System.Collections.Generic.SortedSet`1" /> collection.. </returns>
		/// <param name="match">The delegate that defines the conditions of the elements to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="match" /> is null.</exception>
		// Token: 0x06003ABF RID: 15039 RVA: 0x000D6320 File Offset: 0x000D4520
		public int RemoveWhere(Predicate<T> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			List<T> matches = new List<T>(this.Count);
			this.BreadthFirstTreeWalk(delegate(SortedSet<T>.Node n)
			{
				if (match(n.Item))
				{
					matches.Add(n.Item);
				}
				return true;
			});
			int num = 0;
			for (int i = matches.Count - 1; i >= 0; i--)
			{
				if (this.Remove(matches[i]))
				{
					num++;
				}
			}
			return num;
		}

		/// <summary>Gets the minimum value in the <see cref="T:System.Collections.Generic.SortedSet`1" />, as defined by the comparer.</summary>
		/// <returns>The minimum value in the set.</returns>
		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x06003AC0 RID: 15040 RVA: 0x000D63A4 File Offset: 0x000D45A4
		public T Min
		{
			get
			{
				return this.MinInternal;
			}
		}

		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x06003AC1 RID: 15041 RVA: 0x000D63AC File Offset: 0x000D45AC
		internal virtual T MinInternal
		{
			get
			{
				if (this.root == null)
				{
					return default(T);
				}
				SortedSet<T>.Node left = this.root;
				while (left.Left != null)
				{
					left = left.Left;
				}
				return left.Item;
			}
		}

		/// <summary>Gets the maximum value in the <see cref="T:System.Collections.Generic.SortedSet`1" />, as defined by the comparer.</summary>
		/// <returns>The maximum value in the set.</returns>
		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x06003AC2 RID: 15042 RVA: 0x000D63E9 File Offset: 0x000D45E9
		public T Max
		{
			get
			{
				return this.MaxInternal;
			}
		}

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x06003AC3 RID: 15043 RVA: 0x000D63F4 File Offset: 0x000D45F4
		internal virtual T MaxInternal
		{
			get
			{
				if (this.root == null)
				{
					return default(T);
				}
				SortedSet<T>.Node right = this.root;
				while (right.Right != null)
				{
					right = right.Right;
				}
				return right.Item;
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.Generic.IEnumerable`1" /> that iterates over the <see cref="T:System.Collections.Generic.SortedSet`1" /> in reverse order.</summary>
		/// <returns>An enumerator that iterates over the <see cref="T:System.Collections.Generic.SortedSet`1" /> in reverse order.</returns>
		// Token: 0x06003AC4 RID: 15044 RVA: 0x000D6431 File Offset: 0x000D4631
		public IEnumerable<T> Reverse()
		{
			SortedSet<T>.Enumerator e = new SortedSet<T>.Enumerator(this, true);
			while (e.MoveNext())
			{
				T t = e.Current;
				yield return t;
			}
			yield break;
		}

		/// <summary>Returns a view of a subset in a <see cref="T:System.Collections.Generic.SortedSet`1" />.</summary>
		/// <returns>A subset view that contains only the values in the specified range.</returns>
		/// <param name="lowerValue">The lowest desired value in the view.</param>
		/// <param name="upperValue">The highest desired value in the view. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="lowerValue" /> is more than <paramref name="upperValue" /> according to the comparer.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">A tried operation on the view was outside the range specified by <paramref name="lowerValue" /> and <paramref name="upperValue" />.</exception>
		// Token: 0x06003AC5 RID: 15045 RVA: 0x000D6441 File Offset: 0x000D4641
		public virtual SortedSet<T> GetViewBetween(T lowerValue, T upperValue)
		{
			if (this.Comparer.Compare(lowerValue, upperValue) > 0)
			{
				throw new ArgumentException("Must be less than or equal to upperValue.", "lowerValue");
			}
			return new SortedSet<T>.TreeSubSet(this, lowerValue, upperValue, true, true);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface, and returns the data that you need to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information that is required to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x06003AC6 RID: 15046 RVA: 0x000D646D File Offset: 0x000D466D
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this.GetObjectData(info, context);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data that you must have to serialize a <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information that is required to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x06003AC7 RID: 15047 RVA: 0x000D6478 File Offset: 0x000D4678
		protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("Count", this.count);
			info.AddValue("Comparer", this.comparer, typeof(IComparer<T>));
			info.AddValue("Version", this.version);
			if (this.root != null)
			{
				T[] array = new T[this.Count];
				this.CopyTo(array, 0);
				info.AddValue("Items", array, typeof(T[]));
			}
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.IDeserializationCallback" /> interface, and raises the deserialization event when the deserialization is completed.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Generic.SortedSet`1" /> instance is invalid.</exception>
		// Token: 0x06003AC8 RID: 15048 RVA: 0x000D6502 File Offset: 0x000D4702
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.OnDeserialization(sender);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface, and raises the deserialization event when the deserialization is completed.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object is invalid.</exception>
		// Token: 0x06003AC9 RID: 15049 RVA: 0x000D650C File Offset: 0x000D470C
		protected virtual void OnDeserialization(object sender)
		{
			if (this.comparer != null)
			{
				return;
			}
			if (this.siInfo == null)
			{
				throw new SerializationException("OnDeserialization method was called while the object was not being deserialized.");
			}
			this.comparer = (IComparer<T>)this.siInfo.GetValue("Comparer", typeof(IComparer<T>));
			int @int = this.siInfo.GetInt32("Count");
			if (@int != 0)
			{
				T[] array = (T[])this.siInfo.GetValue("Items", typeof(T[]));
				if (array == null)
				{
					throw new SerializationException("The values for this dictionary are missing.");
				}
				for (int i = 0; i < array.Length; i++)
				{
					this.Add(array[i]);
				}
			}
			this.version = this.siInfo.GetInt32("Version");
			if (this.count != @int)
			{
				throw new SerializationException("The serialized Count information doesn't match the number of items.");
			}
			this.siInfo = null;
		}

		// Token: 0x06003ACA RID: 15050 RVA: 0x000D65EC File Offset: 0x000D47EC
		public bool TryGetValue(T equalValue, out T actualValue)
		{
			SortedSet<T>.Node node = this.FindNode(equalValue);
			if (node != null)
			{
				actualValue = node.Item;
				return true;
			}
			actualValue = default(T);
			return false;
		}

		// Token: 0x06003ACB RID: 15051 RVA: 0x000D661C File Offset: 0x000D481C
		private static int Log2(int value)
		{
			int num = 0;
			while (value > 0)
			{
				num++;
				value >>= 1;
			}
			return num;
		}

		// Token: 0x04002CFD RID: 11517
		private SortedSet<T>.Node root;

		// Token: 0x04002CFE RID: 11518
		private IComparer<T> comparer;

		// Token: 0x04002CFF RID: 11519
		private int count;

		// Token: 0x04002D00 RID: 11520
		private int version;

		// Token: 0x04002D01 RID: 11521
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04002D02 RID: 11522
		private SerializationInfo siInfo;

		// Token: 0x04002D03 RID: 11523
		private const string ComparerName = "Comparer";

		// Token: 0x04002D04 RID: 11524
		private const string CountName = "Count";

		// Token: 0x04002D05 RID: 11525
		private const string ItemsName = "Items";

		// Token: 0x04002D06 RID: 11526
		private const string VersionName = "Version";

		// Token: 0x04002D07 RID: 11527
		private const string TreeName = "Tree";

		// Token: 0x04002D08 RID: 11528
		private const string NodeValueName = "Item";

		// Token: 0x04002D09 RID: 11529
		private const string EnumStartName = "EnumStarted";

		// Token: 0x04002D0A RID: 11530
		private const string ReverseName = "Reverse";

		// Token: 0x04002D0B RID: 11531
		private const string EnumVersionName = "EnumVersion";

		// Token: 0x04002D0C RID: 11532
		private const string MinName = "Min";

		// Token: 0x04002D0D RID: 11533
		private const string MaxName = "Max";

		// Token: 0x04002D0E RID: 11534
		private const string LowerBoundActiveName = "lBoundActive";

		// Token: 0x04002D0F RID: 11535
		private const string UpperBoundActiveName = "uBoundActive";

		// Token: 0x04002D10 RID: 11536
		internal const int StackAllocThreshold = 100;

		// Token: 0x0200073A RID: 1850
		[Serializable]
		internal sealed class TreeSubSet : SortedSet<T>, ISerializable, IDeserializationCallback
		{
			// Token: 0x06003ACC RID: 15052 RVA: 0x000D663C File Offset: 0x000D483C
			public TreeSubSet(SortedSet<T> Underlying, T Min, T Max, bool lowerBoundActive, bool upperBoundActive)
				: base(Underlying.Comparer)
			{
				this._underlying = Underlying;
				this._min = Min;
				this._max = Max;
				this._lBoundActive = lowerBoundActive;
				this._uBoundActive = upperBoundActive;
				this.root = this._underlying.FindRange(this._min, this._max, this._lBoundActive, this._uBoundActive);
				this.count = 0;
				this.version = -1;
				this.VersionCheckImpl();
			}

			// Token: 0x06003ACD RID: 15053 RVA: 0x000D66B7 File Offset: 0x000D48B7
			internal override bool AddIfNotPresent(T item)
			{
				if (!this.IsWithinRange(item))
				{
					throw new ArgumentOutOfRangeException("item");
				}
				bool flag = this._underlying.AddIfNotPresent(item);
				this.VersionCheck();
				return flag;
			}

			// Token: 0x06003ACE RID: 15054 RVA: 0x000D66DF File Offset: 0x000D48DF
			public override bool Contains(T item)
			{
				this.VersionCheck();
				return base.Contains(item);
			}

			// Token: 0x06003ACF RID: 15055 RVA: 0x000D66EE File Offset: 0x000D48EE
			internal override bool DoRemove(T item)
			{
				if (!this.IsWithinRange(item))
				{
					return false;
				}
				bool flag = this._underlying.Remove(item);
				this.VersionCheck();
				return flag;
			}

			// Token: 0x06003AD0 RID: 15056 RVA: 0x000D6710 File Offset: 0x000D4910
			public override void Clear()
			{
				if (this.count == 0)
				{
					return;
				}
				List<T> toRemove = new List<T>();
				this.BreadthFirstTreeWalk(delegate(SortedSet<T>.Node n)
				{
					toRemove.Add(n.Item);
					return true;
				});
				while (toRemove.Count != 0)
				{
					this._underlying.Remove(toRemove[toRemove.Count - 1]);
					toRemove.RemoveAt(toRemove.Count - 1);
				}
				this.root = null;
				this.count = 0;
				this.version = this._underlying.version;
			}

			// Token: 0x06003AD1 RID: 15057 RVA: 0x000D67B4 File Offset: 0x000D49B4
			internal override bool IsWithinRange(T item)
			{
				return (this._lBoundActive ? base.Comparer.Compare(this._min, item) : (-1)) <= 0 && (this._uBoundActive ? base.Comparer.Compare(this._max, item) : 1) >= 0;
			}

			// Token: 0x17000E41 RID: 3649
			// (get) Token: 0x06003AD2 RID: 15058 RVA: 0x000D6808 File Offset: 0x000D4A08
			internal override T MinInternal
			{
				get
				{
					SortedSet<T>.Node node = this.root;
					T t = default(T);
					while (node != null)
					{
						int num = (this._lBoundActive ? base.Comparer.Compare(this._min, node.Item) : (-1));
						if (num == 1)
						{
							node = node.Right;
						}
						else
						{
							t = node.Item;
							if (num == 0)
							{
								break;
							}
							node = node.Left;
						}
					}
					return t;
				}
			}

			// Token: 0x17000E42 RID: 3650
			// (get) Token: 0x06003AD3 RID: 15059 RVA: 0x000D686C File Offset: 0x000D4A6C
			internal override T MaxInternal
			{
				get
				{
					SortedSet<T>.Node node = this.root;
					T t = default(T);
					while (node != null)
					{
						int num = (this._uBoundActive ? base.Comparer.Compare(this._max, node.Item) : 1);
						if (num == -1)
						{
							node = node.Left;
						}
						else
						{
							t = node.Item;
							if (num == 0)
							{
								break;
							}
							node = node.Right;
						}
					}
					return t;
				}
			}

			// Token: 0x06003AD4 RID: 15060 RVA: 0x000D68D0 File Offset: 0x000D4AD0
			internal override bool InOrderTreeWalk(TreeWalkPredicate<T> action)
			{
				this.VersionCheck();
				if (this.root == null)
				{
					return true;
				}
				Stack<SortedSet<T>.Node> stack = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.Log2(this.count + 1));
				SortedSet<T>.Node node = this.root;
				while (node != null)
				{
					if (this.IsWithinRange(node.Item))
					{
						stack.Push(node);
						node = node.Left;
					}
					else if (this._lBoundActive && base.Comparer.Compare(this._min, node.Item) > 0)
					{
						node = node.Right;
					}
					else
					{
						node = node.Left;
					}
				}
				while (stack.Count != 0)
				{
					node = stack.Pop();
					if (!action(node))
					{
						return false;
					}
					SortedSet<T>.Node node2 = node.Right;
					while (node2 != null)
					{
						if (this.IsWithinRange(node2.Item))
						{
							stack.Push(node2);
							node2 = node2.Left;
						}
						else if (this._lBoundActive && base.Comparer.Compare(this._min, node2.Item) > 0)
						{
							node2 = node2.Right;
						}
						else
						{
							node2 = node2.Left;
						}
					}
				}
				return true;
			}

			// Token: 0x06003AD5 RID: 15061 RVA: 0x000D69D8 File Offset: 0x000D4BD8
			internal override bool BreadthFirstTreeWalk(TreeWalkPredicate<T> action)
			{
				this.VersionCheck();
				if (this.root == null)
				{
					return true;
				}
				Queue<SortedSet<T>.Node> queue = new Queue<SortedSet<T>.Node>();
				queue.Enqueue(this.root);
				while (queue.Count != 0)
				{
					SortedSet<T>.Node node = queue.Dequeue();
					if (this.IsWithinRange(node.Item) && !action(node))
					{
						return false;
					}
					if (node.Left != null && (!this._lBoundActive || base.Comparer.Compare(this._min, node.Item) < 0))
					{
						queue.Enqueue(node.Left);
					}
					if (node.Right != null && (!this._uBoundActive || base.Comparer.Compare(this._max, node.Item) > 0))
					{
						queue.Enqueue(node.Right);
					}
				}
				return true;
			}

			// Token: 0x06003AD6 RID: 15062 RVA: 0x000D6AA4 File Offset: 0x000D4CA4
			internal override SortedSet<T>.Node FindNode(T item)
			{
				if (!this.IsWithinRange(item))
				{
					return null;
				}
				this.VersionCheck();
				return base.FindNode(item);
			}

			// Token: 0x06003AD7 RID: 15063 RVA: 0x000D6AC0 File Offset: 0x000D4CC0
			internal override int InternalIndexOf(T item)
			{
				int num = -1;
				foreach (T t in this)
				{
					num++;
					if (base.Comparer.Compare(item, t) == 0)
					{
						return num;
					}
				}
				return -1;
			}

			// Token: 0x06003AD8 RID: 15064 RVA: 0x000D6B24 File Offset: 0x000D4D24
			internal override void VersionCheck()
			{
				this.VersionCheckImpl();
			}

			// Token: 0x06003AD9 RID: 15065 RVA: 0x000D6B2C File Offset: 0x000D4D2C
			private void VersionCheckImpl()
			{
				if (this.version != this._underlying.version)
				{
					this.root = this._underlying.FindRange(this._min, this._max, this._lBoundActive, this._uBoundActive);
					this.version = this._underlying.version;
					this.count = 0;
					this.InOrderTreeWalk(delegate(SortedSet<T>.Node n)
					{
						this.count++;
						return true;
					});
				}
			}

			// Token: 0x06003ADA RID: 15066 RVA: 0x000D6BA0 File Offset: 0x000D4DA0
			public override SortedSet<T> GetViewBetween(T lowerValue, T upperValue)
			{
				if (this._lBoundActive && base.Comparer.Compare(this._min, lowerValue) > 0)
				{
					throw new ArgumentOutOfRangeException("lowerValue");
				}
				if (this._uBoundActive && base.Comparer.Compare(this._max, upperValue) < 0)
				{
					throw new ArgumentOutOfRangeException("upperValue");
				}
				return (SortedSet<T>.TreeSubSet)this._underlying.GetViewBetween(lowerValue, upperValue);
			}

			// Token: 0x06003ADB RID: 15067 RVA: 0x000D646D File Offset: 0x000D466D
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				this.GetObjectData(info, context);
			}

			// Token: 0x06003ADC RID: 15068 RVA: 0x0000F3CE File Offset: 0x0000D5CE
			protected override void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003ADD RID: 15069 RVA: 0x0000F3CE File Offset: 0x0000D5CE
			void IDeserializationCallback.OnDeserialization(object sender)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003ADE RID: 15070 RVA: 0x0000F3CE File Offset: 0x0000D5CE
			protected override void OnDeserialization(object sender)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x04002D11 RID: 11537
			private SortedSet<T> _underlying;

			// Token: 0x04002D12 RID: 11538
			private T _min;

			// Token: 0x04002D13 RID: 11539
			private T _max;

			// Token: 0x04002D14 RID: 11540
			private bool _lBoundActive;

			// Token: 0x04002D15 RID: 11541
			private bool _uBoundActive;
		}

		// Token: 0x0200073C RID: 1852
		[Serializable]
		internal sealed class Node
		{
			// Token: 0x06003AE2 RID: 15074 RVA: 0x000D6C34 File Offset: 0x000D4E34
			public Node(T item, NodeColor color)
			{
				this.Item = item;
				this.Color = color;
			}

			// Token: 0x06003AE3 RID: 15075 RVA: 0x000D6C4A File Offset: 0x000D4E4A
			public static bool IsNonNullBlack(SortedSet<T>.Node node)
			{
				return node != null && node.IsBlack;
			}

			// Token: 0x06003AE4 RID: 15076 RVA: 0x000D6C57 File Offset: 0x000D4E57
			public static bool IsNonNullRed(SortedSet<T>.Node node)
			{
				return node != null && node.IsRed;
			}

			// Token: 0x06003AE5 RID: 15077 RVA: 0x000D6C64 File Offset: 0x000D4E64
			public static bool IsNullOrBlack(SortedSet<T>.Node node)
			{
				return node == null || node.IsBlack;
			}

			// Token: 0x17000E43 RID: 3651
			// (get) Token: 0x06003AE6 RID: 15078 RVA: 0x000D6C71 File Offset: 0x000D4E71
			// (set) Token: 0x06003AE7 RID: 15079 RVA: 0x000D6C79 File Offset: 0x000D4E79
			public T Item { get; set; }

			// Token: 0x17000E44 RID: 3652
			// (get) Token: 0x06003AE8 RID: 15080 RVA: 0x000D6C82 File Offset: 0x000D4E82
			// (set) Token: 0x06003AE9 RID: 15081 RVA: 0x000D6C8A File Offset: 0x000D4E8A
			public SortedSet<T>.Node Left { get; set; }

			// Token: 0x17000E45 RID: 3653
			// (get) Token: 0x06003AEA RID: 15082 RVA: 0x000D6C93 File Offset: 0x000D4E93
			// (set) Token: 0x06003AEB RID: 15083 RVA: 0x000D6C9B File Offset: 0x000D4E9B
			public SortedSet<T>.Node Right { get; set; }

			// Token: 0x17000E46 RID: 3654
			// (get) Token: 0x06003AEC RID: 15084 RVA: 0x000D6CA4 File Offset: 0x000D4EA4
			// (set) Token: 0x06003AED RID: 15085 RVA: 0x000D6CAC File Offset: 0x000D4EAC
			public NodeColor Color { get; set; }

			// Token: 0x17000E47 RID: 3655
			// (get) Token: 0x06003AEE RID: 15086 RVA: 0x000D6CB5 File Offset: 0x000D4EB5
			public bool IsBlack
			{
				get
				{
					return this.Color == NodeColor.Black;
				}
			}

			// Token: 0x17000E48 RID: 3656
			// (get) Token: 0x06003AEF RID: 15087 RVA: 0x000D6CC0 File Offset: 0x000D4EC0
			public bool IsRed
			{
				get
				{
					return this.Color == NodeColor.Red;
				}
			}

			// Token: 0x17000E49 RID: 3657
			// (get) Token: 0x06003AF0 RID: 15088 RVA: 0x000D6CCB File Offset: 0x000D4ECB
			public bool Is2Node
			{
				get
				{
					return this.IsBlack && SortedSet<T>.Node.IsNullOrBlack(this.Left) && SortedSet<T>.Node.IsNullOrBlack(this.Right);
				}
			}

			// Token: 0x17000E4A RID: 3658
			// (get) Token: 0x06003AF1 RID: 15089 RVA: 0x000D6CEF File Offset: 0x000D4EEF
			public bool Is4Node
			{
				get
				{
					return SortedSet<T>.Node.IsNonNullRed(this.Left) && SortedSet<T>.Node.IsNonNullRed(this.Right);
				}
			}

			// Token: 0x06003AF2 RID: 15090 RVA: 0x000D6D0B File Offset: 0x000D4F0B
			public void ColorBlack()
			{
				this.Color = NodeColor.Black;
			}

			// Token: 0x06003AF3 RID: 15091 RVA: 0x000D6D14 File Offset: 0x000D4F14
			public void ColorRed()
			{
				this.Color = NodeColor.Red;
			}

			// Token: 0x06003AF4 RID: 15092 RVA: 0x000D6D20 File Offset: 0x000D4F20
			public SortedSet<T>.Node DeepClone(int count)
			{
				Stack<SortedSet<T>.Node> stack = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.Log2(count) + 2);
				Stack<SortedSet<T>.Node> stack2 = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.Log2(count) + 2);
				SortedSet<T>.Node node = this.ShallowClone();
				SortedSet<T>.Node node2 = this;
				SortedSet<T>.Node node3 = node;
				while (node2 != null)
				{
					stack.Push(node2);
					stack2.Push(node3);
					SortedSet<T>.Node node4 = node3;
					SortedSet<T>.Node left = node2.Left;
					node4.Left = ((left != null) ? left.ShallowClone() : null);
					node2 = node2.Left;
					node3 = node3.Left;
				}
				while (stack.Count != 0)
				{
					node2 = stack.Pop();
					node3 = stack2.Pop();
					SortedSet<T>.Node node5 = node2.Right;
					SortedSet<T>.Node node6 = ((node5 != null) ? node5.ShallowClone() : null);
					node3.Right = node6;
					while (node5 != null)
					{
						stack.Push(node5);
						stack2.Push(node6);
						SortedSet<T>.Node node7 = node6;
						SortedSet<T>.Node left2 = node5.Left;
						node7.Left = ((left2 != null) ? left2.ShallowClone() : null);
						node5 = node5.Left;
						node6 = node6.Left;
					}
				}
				return node;
			}

			// Token: 0x06003AF5 RID: 15093 RVA: 0x000D6E14 File Offset: 0x000D5014
			public TreeRotation GetRotation(SortedSet<T>.Node current, SortedSet<T>.Node sibling)
			{
				bool flag = this.Left == current;
				if (!SortedSet<T>.Node.IsNonNullRed(sibling.Left))
				{
					if (!flag)
					{
						return TreeRotation.LeftRight;
					}
					return TreeRotation.Left;
				}
				else
				{
					if (!flag)
					{
						return TreeRotation.Right;
					}
					return TreeRotation.RightLeft;
				}
			}

			// Token: 0x06003AF6 RID: 15094 RVA: 0x000D6E45 File Offset: 0x000D5045
			public SortedSet<T>.Node GetSibling(SortedSet<T>.Node node)
			{
				if (node != this.Left)
				{
					return this.Left;
				}
				return this.Right;
			}

			// Token: 0x06003AF7 RID: 15095 RVA: 0x000D6E5D File Offset: 0x000D505D
			public SortedSet<T>.Node ShallowClone()
			{
				return new SortedSet<T>.Node(this.Item, this.Color);
			}

			// Token: 0x06003AF8 RID: 15096 RVA: 0x000D6E70 File Offset: 0x000D5070
			public void Split4Node()
			{
				this.ColorRed();
				this.Left.ColorBlack();
				this.Right.ColorBlack();
			}

			// Token: 0x06003AF9 RID: 15097 RVA: 0x000D6E90 File Offset: 0x000D5090
			public SortedSet<T>.Node Rotate(TreeRotation rotation)
			{
				switch (rotation)
				{
				case TreeRotation.Left:
					this.Right.Right.ColorBlack();
					return this.RotateLeft();
				case TreeRotation.LeftRight:
					return this.RotateLeftRight();
				case TreeRotation.Right:
					this.Left.Left.ColorBlack();
					return this.RotateRight();
				case TreeRotation.RightLeft:
					return this.RotateRightLeft();
				default:
					return null;
				}
			}

			// Token: 0x06003AFA RID: 15098 RVA: 0x000D6EF4 File Offset: 0x000D50F4
			public SortedSet<T>.Node RotateLeft()
			{
				SortedSet<T>.Node right = this.Right;
				this.Right = right.Left;
				right.Left = this;
				return right;
			}

			// Token: 0x06003AFB RID: 15099 RVA: 0x000D6F1C File Offset: 0x000D511C
			public SortedSet<T>.Node RotateLeftRight()
			{
				SortedSet<T>.Node left = this.Left;
				SortedSet<T>.Node right = left.Right;
				this.Left = right.Right;
				right.Right = this;
				left.Right = right.Left;
				right.Left = left;
				return right;
			}

			// Token: 0x06003AFC RID: 15100 RVA: 0x000D6F60 File Offset: 0x000D5160
			public SortedSet<T>.Node RotateRight()
			{
				SortedSet<T>.Node left = this.Left;
				this.Left = left.Right;
				left.Right = this;
				return left;
			}

			// Token: 0x06003AFD RID: 15101 RVA: 0x000D6F88 File Offset: 0x000D5188
			public SortedSet<T>.Node RotateRightLeft()
			{
				SortedSet<T>.Node right = this.Right;
				SortedSet<T>.Node left = right.Left;
				this.Right = left.Left;
				left.Left = this;
				right.Left = left.Right;
				left.Right = right;
				return left;
			}

			// Token: 0x06003AFE RID: 15102 RVA: 0x000D6FCA File Offset: 0x000D51CA
			public void Merge2Nodes()
			{
				this.ColorBlack();
				this.Left.ColorRed();
				this.Right.ColorRed();
			}

			// Token: 0x06003AFF RID: 15103 RVA: 0x000D6FE8 File Offset: 0x000D51E8
			public void ReplaceChild(SortedSet<T>.Node child, SortedSet<T>.Node newChild)
			{
				if (this.Left == child)
				{
					this.Left = newChild;
					return;
				}
				this.Right = newChild;
			}
		}

		/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</summary>
		// Token: 0x0200073D RID: 1853
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator, ISerializable, IDeserializationCallback
		{
			// Token: 0x06003B00 RID: 15104 RVA: 0x000D7002 File Offset: 0x000D5202
			internal Enumerator(SortedSet<T> set)
			{
				this = new SortedSet<T>.Enumerator(set, false);
			}

			// Token: 0x06003B01 RID: 15105 RVA: 0x000D700C File Offset: 0x000D520C
			internal Enumerator(SortedSet<T> set, bool reverse)
			{
				this._tree = set;
				set.VersionCheck();
				this._version = set.version;
				this._stack = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.Log2(set.Count + 1));
				this._current = null;
				this._reverse = reverse;
				this.Initialize();
			}

			/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</summary>
			/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</param>
			/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="info" /> is null.</exception>
			// Token: 0x06003B02 RID: 15106 RVA: 0x0000F3CE File Offset: 0x0000D5CE
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new PlatformNotSupportedException();
			}

			/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and raises the deserialization event when the deserialization is complete.</summary>
			/// <param name="sender">The source of the deserialization event.</param>
			/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Generic.SortedSet`1" /> instance is invalid.</exception>
			// Token: 0x06003B03 RID: 15107 RVA: 0x0000F3CE File Offset: 0x0000D5CE
			void IDeserializationCallback.OnDeserialization(object sender)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B04 RID: 15108 RVA: 0x000D7060 File Offset: 0x000D5260
			private void Initialize()
			{
				this._current = null;
				SortedSet<T>.Node node = this._tree.root;
				while (node != null)
				{
					SortedSet<T>.Node node2 = (this._reverse ? node.Right : node.Left);
					SortedSet<T>.Node node3 = (this._reverse ? node.Left : node.Right);
					if (this._tree.IsWithinRange(node.Item))
					{
						this._stack.Push(node);
						node = node2;
					}
					else if (node2 == null || !this._tree.IsWithinRange(node2.Item))
					{
						node = node3;
					}
					else
					{
						node = node2;
					}
				}
			}

			/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.SortedSet`1" /> collection.</summary>
			/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x06003B05 RID: 15109 RVA: 0x000D70F8 File Offset: 0x000D52F8
			public bool MoveNext()
			{
				this._tree.VersionCheck();
				if (this._version != this._tree.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._stack.Count == 0)
				{
					this._current = null;
					return false;
				}
				this._current = this._stack.Pop();
				SortedSet<T>.Node node = (this._reverse ? this._current.Left : this._current.Right);
				while (node != null)
				{
					SortedSet<T>.Node node2 = (this._reverse ? node.Right : node.Left);
					SortedSet<T>.Node node3 = (this._reverse ? node.Left : node.Right);
					if (this._tree.IsWithinRange(node.Item))
					{
						this._stack.Push(node);
						node = node2;
					}
					else if (node3 == null || !this._tree.IsWithinRange(node3.Item))
					{
						node = node2;
					}
					else
					{
						node = node3;
					}
				}
				return true;
			}

			/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.SortedSet`1.Enumerator" />. </summary>
			// Token: 0x06003B06 RID: 15110 RVA: 0x000027E8 File Offset: 0x000009E8
			public void Dispose()
			{
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x17000E4B RID: 3659
			// (get) Token: 0x06003B07 RID: 15111 RVA: 0x000D71F0 File Offset: 0x000D53F0
			public T Current
			{
				get
				{
					if (this._current != null)
					{
						return this._current.Item;
					}
					return default(T);
				}
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x17000E4C RID: 3660
			// (get) Token: 0x06003B08 RID: 15112 RVA: 0x000D721A File Offset: 0x000D541A
			object IEnumerator.Current
			{
				get
				{
					if (this._current == null)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._current.Item;
				}
			}

			// Token: 0x17000E4D RID: 3661
			// (get) Token: 0x06003B09 RID: 15113 RVA: 0x000D723F File Offset: 0x000D543F
			internal bool NotStartedOrEnded
			{
				get
				{
					return this._current == null;
				}
			}

			// Token: 0x06003B0A RID: 15114 RVA: 0x000D724A File Offset: 0x000D544A
			internal void Reset()
			{
				if (this._version != this._tree.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._stack.Clear();
				this.Initialize();
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x06003B0B RID: 15115 RVA: 0x000D727B File Offset: 0x000D547B
			void IEnumerator.Reset()
			{
				this.Reset();
			}

			// Token: 0x04002D1B RID: 11547
			private static readonly SortedSet<T>.Node s_dummyNode = new SortedSet<T>.Node(default(T), NodeColor.Red);

			// Token: 0x04002D1C RID: 11548
			private SortedSet<T> _tree;

			// Token: 0x04002D1D RID: 11549
			private int _version;

			// Token: 0x04002D1E RID: 11550
			private Stack<SortedSet<T>.Node> _stack;

			// Token: 0x04002D1F RID: 11551
			private SortedSet<T>.Node _current;

			// Token: 0x04002D20 RID: 11552
			private bool _reverse;
		}

		// Token: 0x0200073E RID: 1854
		internal struct ElementCount
		{
			// Token: 0x04002D21 RID: 11553
			internal int UniqueCount;

			// Token: 0x04002D22 RID: 11554
			internal int UnfoundCount;
		}
	}
}
