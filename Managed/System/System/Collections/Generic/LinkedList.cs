using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections.Generic
{
	/// <summary>Represents a doubly linked list.</summary>
	/// <typeparam name="T">Specifies the element type of the linked list.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200071D RID: 1821
	[DebuggerTypeProxy(typeof(ICollectionDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class LinkedList<T> : ICollection<T>, IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>, ISerializable, IDeserializationCallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.LinkedList`1" /> class that is empty.</summary>
		// Token: 0x0600394B RID: 14667 RVA: 0x000020EB File Offset: 0x000002EB
		public LinkedList()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.LinkedList`1" /> class that contains elements copied from the specified <see cref="T:System.Collections.IEnumerable" /> and has sufficient capacity to accommodate the number of elements copied. </summary>
		/// <param name="collection">The <see cref="T:System.Collections.IEnumerable" /> whose elements are copied to the new <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> is null.</exception>
		// Token: 0x0600394C RID: 14668 RVA: 0x000D16BC File Offset: 0x000CF8BC
		public LinkedList(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			foreach (T t in collection)
			{
				this.AddLast(t);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.LinkedList`1" /> class that is serializable with the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing the information required to serialize the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		// Token: 0x0600394D RID: 14669 RVA: 0x000D171C File Offset: 0x000CF91C
		protected LinkedList(SerializationInfo info, StreamingContext context)
		{
			this._siInfo = info;
		}

		/// <summary>Gets the number of nodes actually contained in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>The number of nodes actually contained in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</returns>
		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x0600394E RID: 14670 RVA: 0x000D172B File Offset: 0x000CF92B
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		/// <summary>Gets the first node of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>The first <see cref="T:System.Collections.Generic.LinkedListNode`1" /> of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</returns>
		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x0600394F RID: 14671 RVA: 0x000D1733 File Offset: 0x000CF933
		public LinkedListNode<T> First
		{
			get
			{
				return this.head;
			}
		}

		/// <summary>Gets the last node of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>The last <see cref="T:System.Collections.Generic.LinkedListNode`1" /> of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</returns>
		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x06003950 RID: 14672 RVA: 0x000D173B File Offset: 0x000CF93B
		public LinkedListNode<T> Last
		{
			get
			{
				if (this.head != null)
				{
					return this.head.prev;
				}
				return null;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only; otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.LinkedList`1" />, this property always returns false.</returns>
		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x06003951 RID: 14673 RVA: 0x00004240 File Offset: 0x00002440
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Adds an item at the end of the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
		/// <param name="value">The value to add at the end of the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
		// Token: 0x06003952 RID: 14674 RVA: 0x000D1752 File Offset: 0x000CF952
		void ICollection<T>.Add(T value)
		{
			this.AddLast(value);
		}

		/// <summary>Adds a new node containing the specified value after the specified existing node in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>The new <see cref="T:System.Collections.Generic.LinkedListNode`1" /> containing <paramref name="value" />.</returns>
		/// <param name="node">The <see cref="T:System.Collections.Generic.LinkedListNode`1" /> after which to insert a new <see cref="T:System.Collections.Generic.LinkedListNode`1" /> containing <paramref name="value" />.</param>
		/// <param name="value">The value to add to the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="node" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="node" /> is not in the current <see cref="T:System.Collections.Generic.LinkedList`1" />.</exception>
		// Token: 0x06003953 RID: 14675 RVA: 0x000D175C File Offset: 0x000CF95C
		public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value)
		{
			this.ValidateNode(node);
			LinkedListNode<T> linkedListNode = new LinkedListNode<T>(node.list, value);
			this.InternalInsertNodeBefore(node.next, linkedListNode);
			return linkedListNode;
		}

		/// <summary>Adds the specified new node after the specified existing node in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <param name="node">The <see cref="T:System.Collections.Generic.LinkedListNode`1" /> after which to insert <paramref name="newNode" />.</param>
		/// <param name="newNode">The new <see cref="T:System.Collections.Generic.LinkedListNode`1" /> to add to the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="node" /> is null.-or-<paramref name="newNode" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="node" /> is not in the current <see cref="T:System.Collections.Generic.LinkedList`1" />.-or-<paramref name="newNode" /> belongs to another <see cref="T:System.Collections.Generic.LinkedList`1" />.</exception>
		// Token: 0x06003954 RID: 14676 RVA: 0x000D178B File Offset: 0x000CF98B
		public void AddAfter(LinkedListNode<T> node, LinkedListNode<T> newNode)
		{
			this.ValidateNode(node);
			this.ValidateNewNode(newNode);
			this.InternalInsertNodeBefore(node.next, newNode);
			newNode.list = this;
		}

		/// <summary>Adds a new node containing the specified value before the specified existing node in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>The new <see cref="T:System.Collections.Generic.LinkedListNode`1" /> containing <paramref name="value" />.</returns>
		/// <param name="node">The <see cref="T:System.Collections.Generic.LinkedListNode`1" /> before which to insert a new <see cref="T:System.Collections.Generic.LinkedListNode`1" /> containing <paramref name="value" />.</param>
		/// <param name="value">The value to add to the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="node" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="node" /> is not in the current <see cref="T:System.Collections.Generic.LinkedList`1" />.</exception>
		// Token: 0x06003955 RID: 14677 RVA: 0x000D17B0 File Offset: 0x000CF9B0
		public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
		{
			this.ValidateNode(node);
			LinkedListNode<T> linkedListNode = new LinkedListNode<T>(node.list, value);
			this.InternalInsertNodeBefore(node, linkedListNode);
			if (node == this.head)
			{
				this.head = linkedListNode;
			}
			return linkedListNode;
		}

		/// <summary>Adds the specified new node before the specified existing node in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <param name="node">The <see cref="T:System.Collections.Generic.LinkedListNode`1" /> before which to insert <paramref name="newNode" />.</param>
		/// <param name="newNode">The new <see cref="T:System.Collections.Generic.LinkedListNode`1" /> to add to the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="node" /> is null.-or-<paramref name="newNode" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="node" /> is not in the current <see cref="T:System.Collections.Generic.LinkedList`1" />.-or-<paramref name="newNode" /> belongs to another <see cref="T:System.Collections.Generic.LinkedList`1" />.</exception>
		// Token: 0x06003956 RID: 14678 RVA: 0x000D17EA File Offset: 0x000CF9EA
		public void AddBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
		{
			this.ValidateNode(node);
			this.ValidateNewNode(newNode);
			this.InternalInsertNodeBefore(node, newNode);
			newNode.list = this;
			if (node == this.head)
			{
				this.head = newNode;
			}
		}

		/// <summary>Adds a new node containing the specified value at the start of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>The new <see cref="T:System.Collections.Generic.LinkedListNode`1" /> containing <paramref name="value" />.</returns>
		/// <param name="value">The value to add at the start of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		// Token: 0x06003957 RID: 14679 RVA: 0x000D181C File Offset: 0x000CFA1C
		public LinkedListNode<T> AddFirst(T value)
		{
			LinkedListNode<T> linkedListNode = new LinkedListNode<T>(this, value);
			if (this.head == null)
			{
				this.InternalInsertNodeToEmptyList(linkedListNode);
			}
			else
			{
				this.InternalInsertNodeBefore(this.head, linkedListNode);
				this.head = linkedListNode;
			}
			return linkedListNode;
		}

		/// <summary>Adds the specified new node at the start of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <param name="node">The new <see cref="T:System.Collections.Generic.LinkedListNode`1" /> to add at the start of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="node" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="node" /> belongs to another <see cref="T:System.Collections.Generic.LinkedList`1" />.</exception>
		// Token: 0x06003958 RID: 14680 RVA: 0x000D1857 File Offset: 0x000CFA57
		public void AddFirst(LinkedListNode<T> node)
		{
			this.ValidateNewNode(node);
			if (this.head == null)
			{
				this.InternalInsertNodeToEmptyList(node);
			}
			else
			{
				this.InternalInsertNodeBefore(this.head, node);
				this.head = node;
			}
			node.list = this;
		}

		/// <summary>Adds a new node containing the specified value at the end of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>The new <see cref="T:System.Collections.Generic.LinkedListNode`1" /> containing <paramref name="value" />.</returns>
		/// <param name="value">The value to add at the end of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		// Token: 0x06003959 RID: 14681 RVA: 0x000D188C File Offset: 0x000CFA8C
		public LinkedListNode<T> AddLast(T value)
		{
			LinkedListNode<T> linkedListNode = new LinkedListNode<T>(this, value);
			if (this.head == null)
			{
				this.InternalInsertNodeToEmptyList(linkedListNode);
			}
			else
			{
				this.InternalInsertNodeBefore(this.head, linkedListNode);
			}
			return linkedListNode;
		}

		/// <summary>Adds the specified new node at the end of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <param name="node">The new <see cref="T:System.Collections.Generic.LinkedListNode`1" /> to add at the end of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="node" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="node" /> belongs to another <see cref="T:System.Collections.Generic.LinkedList`1" />.</exception>
		// Token: 0x0600395A RID: 14682 RVA: 0x000D18C0 File Offset: 0x000CFAC0
		public void AddLast(LinkedListNode<T> node)
		{
			this.ValidateNewNode(node);
			if (this.head == null)
			{
				this.InternalInsertNodeToEmptyList(node);
			}
			else
			{
				this.InternalInsertNodeBefore(this.head, node);
			}
			node.list = this;
		}

		/// <summary>Removes all nodes from the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		// Token: 0x0600395B RID: 14683 RVA: 0x000D18F0 File Offset: 0x000CFAF0
		public void Clear()
		{
			LinkedListNode<T> next = this.head;
			while (next != null)
			{
				LinkedListNode<T> linkedListNode = next;
				next = next.Next;
				linkedListNode.Invalidate();
			}
			this.head = null;
			this.count = 0;
			this.version++;
		}

		/// <summary>Determines whether a value is in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>true if <paramref name="value" /> is found in the <see cref="T:System.Collections.Generic.LinkedList`1" />; otherwise, false.</returns>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Generic.LinkedList`1" />. The value can be null for reference types.</param>
		// Token: 0x0600395C RID: 14684 RVA: 0x000D1934 File Offset: 0x000CFB34
		public bool Contains(T value)
		{
			return this.Find(value) != null;
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.Generic.LinkedList`1" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.LinkedList`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.LinkedList`1" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x0600395D RID: 14685 RVA: 0x000D1940 File Offset: 0x000CFB40
		public void CopyTo(T[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", index, "Non-negative number required.");
			}
			if (index > array.Length)
			{
				throw new ArgumentOutOfRangeException("index", index, "Must be less than or equal to the size of the collection.");
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException("Insufficient space in the target location to copy the information.");
			}
			LinkedListNode<T> next = this.head;
			if (next != null)
			{
				do
				{
					array[index++] = next.item;
					next = next.next;
				}
				while (next != this.head);
			}
		}

		/// <summary>Finds the first node that contains the specified value.</summary>
		/// <returns>The first <see cref="T:System.Collections.Generic.LinkedListNode`1" /> that contains the specified value, if found; otherwise, null.</returns>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		// Token: 0x0600395E RID: 14686 RVA: 0x000D19D8 File Offset: 0x000CFBD8
		public LinkedListNode<T> Find(T value)
		{
			LinkedListNode<T> linkedListNode = this.head;
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			if (linkedListNode != null)
			{
				if (value != null)
				{
					while (!@default.Equals(linkedListNode.item, value))
					{
						linkedListNode = linkedListNode.next;
						if (linkedListNode == this.head)
						{
							goto IL_005A;
						}
					}
					return linkedListNode;
				}
				while (linkedListNode.item != null)
				{
					linkedListNode = linkedListNode.next;
					if (linkedListNode == this.head)
					{
						goto IL_005A;
					}
				}
				return linkedListNode;
			}
			IL_005A:
			return null;
		}

		/// <summary>Finds the last node that contains the specified value.</summary>
		/// <returns>The last <see cref="T:System.Collections.Generic.LinkedListNode`1" /> that contains the specified value, if found; otherwise, null.</returns>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		// Token: 0x0600395F RID: 14687 RVA: 0x000D1A40 File Offset: 0x000CFC40
		public LinkedListNode<T> FindLast(T value)
		{
			if (this.head == null)
			{
				return null;
			}
			LinkedListNode<T> prev = this.head.prev;
			LinkedListNode<T> linkedListNode = prev;
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			if (linkedListNode != null)
			{
				if (value != null)
				{
					while (!@default.Equals(linkedListNode.item, value))
					{
						linkedListNode = linkedListNode.prev;
						if (linkedListNode == prev)
						{
							goto IL_0061;
						}
					}
					return linkedListNode;
				}
				while (linkedListNode.item != null)
				{
					linkedListNode = linkedListNode.prev;
					if (linkedListNode == prev)
					{
						goto IL_0061;
					}
				}
				return linkedListNode;
			}
			IL_0061:
			return null;
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.LinkedList`1.Enumerator" /> for the <see cref="T:System.Collections.Generic.LinkedList`1" />.</returns>
		// Token: 0x06003960 RID: 14688 RVA: 0x000D1AAF File Offset: 0x000CFCAF
		public LinkedList<T>.Enumerator GetEnumerator()
		{
			return new LinkedList<T>.Enumerator(this);
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06003961 RID: 14689 RVA: 0x000D1AB7 File Offset: 0x000CFCB7
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Removes the first occurrence of the specified value from the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>true if the element containing <paramref name="value" /> is successfully removed; otherwise, false.  This method also returns false if <paramref name="value" /> was not found in the original <see cref="T:System.Collections.Generic.LinkedList`1" />.</returns>
		/// <param name="value">The value to remove from the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		// Token: 0x06003962 RID: 14690 RVA: 0x000D1AC4 File Offset: 0x000CFCC4
		public bool Remove(T value)
		{
			LinkedListNode<T> linkedListNode = this.Find(value);
			if (linkedListNode != null)
			{
				this.InternalRemoveNode(linkedListNode);
				return true;
			}
			return false;
		}

		/// <summary>Removes the specified node from the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <param name="node">The <see cref="T:System.Collections.Generic.LinkedListNode`1" /> to remove from the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="node" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="node" /> is not in the current <see cref="T:System.Collections.Generic.LinkedList`1" />.</exception>
		// Token: 0x06003963 RID: 14691 RVA: 0x000D1AE6 File Offset: 0x000CFCE6
		public void Remove(LinkedListNode<T> node)
		{
			this.ValidateNode(node);
			this.InternalRemoveNode(node);
		}

		/// <summary>Removes the node at the start of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Generic.LinkedList`1" /> is empty.</exception>
		// Token: 0x06003964 RID: 14692 RVA: 0x000D1AF6 File Offset: 0x000CFCF6
		public void RemoveFirst()
		{
			if (this.head == null)
			{
				throw new InvalidOperationException("The LinkedList is empty.");
			}
			this.InternalRemoveNode(this.head);
		}

		/// <summary>Removes the node at the end of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Generic.LinkedList`1" /> is empty.</exception>
		// Token: 0x06003965 RID: 14693 RVA: 0x000D1B17 File Offset: 0x000CFD17
		public void RemoveLast()
		{
			if (this.head == null)
			{
				throw new InvalidOperationException("The LinkedList is empty.");
			}
			this.InternalRemoveNode(this.head.prev);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize the <see cref="T:System.Collections.Generic.LinkedList`1" /> instance.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the <see cref="T:System.Collections.Generic.LinkedList`1" /> instance.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.LinkedList`1" /> instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x06003966 RID: 14694 RVA: 0x000D1B40 File Offset: 0x000CFD40
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("Version", this.version);
			info.AddValue("Count", this.count);
			if (this.count != 0)
			{
				T[] array = new T[this.count];
				this.CopyTo(array, 0);
				info.AddValue("Data", array, typeof(T[]));
			}
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and raises the deserialization event when the deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Generic.LinkedList`1" /> instance is invalid.</exception>
		// Token: 0x06003967 RID: 14695 RVA: 0x000D1BB0 File Offset: 0x000CFDB0
		public virtual void OnDeserialization(object sender)
		{
			if (this._siInfo == null)
			{
				return;
			}
			int @int = this._siInfo.GetInt32("Version");
			if (this._siInfo.GetInt32("Count") != 0)
			{
				T[] array = (T[])this._siInfo.GetValue("Data", typeof(T[]));
				if (array == null)
				{
					throw new SerializationException("The values for this dictionary are missing.");
				}
				for (int i = 0; i < array.Length; i++)
				{
					this.AddLast(array[i]);
				}
			}
			else
			{
				this.head = null;
			}
			this.version = @int;
			this._siInfo = null;
		}

		// Token: 0x06003968 RID: 14696 RVA: 0x000D1C4C File Offset: 0x000CFE4C
		private void InternalInsertNodeBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
		{
			newNode.next = node;
			newNode.prev = node.prev;
			node.prev.next = newNode;
			node.prev = newNode;
			this.version++;
			this.count++;
		}

		// Token: 0x06003969 RID: 14697 RVA: 0x000D1C9B File Offset: 0x000CFE9B
		private void InternalInsertNodeToEmptyList(LinkedListNode<T> newNode)
		{
			newNode.next = newNode;
			newNode.prev = newNode;
			this.head = newNode;
			this.version++;
			this.count++;
		}

		// Token: 0x0600396A RID: 14698 RVA: 0x000D1CD0 File Offset: 0x000CFED0
		internal void InternalRemoveNode(LinkedListNode<T> node)
		{
			if (node.next == node)
			{
				this.head = null;
			}
			else
			{
				node.next.prev = node.prev;
				node.prev.next = node.next;
				if (this.head == node)
				{
					this.head = node.next;
				}
			}
			node.Invalidate();
			this.count--;
			this.version++;
		}

		// Token: 0x0600396B RID: 14699 RVA: 0x000D1D48 File Offset: 0x000CFF48
		internal void ValidateNewNode(LinkedListNode<T> node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (node.list != null)
			{
				throw new InvalidOperationException("The LinkedList node already belongs to a LinkedList.");
			}
		}

		// Token: 0x0600396C RID: 14700 RVA: 0x000D1D6B File Offset: 0x000CFF6B
		internal void ValidateNode(LinkedListNode<T> node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (node.list != this)
			{
				throw new InvalidOperationException("The LinkedList node does not belong to current LinkedList.");
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.LinkedList`1" />, this property always returns false.</returns>
		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x0600396D RID: 14701 RVA: 0x00004240 File Offset: 0x00002440
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.Generic.LinkedList`1" />, this property always returns the current instance.</returns>
		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x0600396E RID: 14702 RVA: 0x000D1D8F File Offset: 0x000CFF8F
		object ICollection.SyncRoot
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

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-<paramref name="array" /> does not have zero-based indexing.-or-The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.-or-The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x0600396F RID: 14703 RVA: 0x000D1DB4 File Offset: 0x000CFFB4
		void ICollection.CopyTo(Array array, int index)
		{
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
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", index, "Non-negative number required.");
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException("Insufficient space in the target location to copy the information.");
			}
			T[] array2 = array as T[];
			if (array2 != null)
			{
				this.CopyTo(array2, index);
				return;
			}
			object[] array3 = array as object[];
			if (array3 == null)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
			}
			LinkedListNode<T> next = this.head;
			try
			{
				if (next != null)
				{
					do
					{
						array3[index++] = next.item;
						next = next.next;
					}
					while (next != this.head);
				}
			}
			catch (ArrayTypeMismatchException)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
			}
		}

		/// <summary>Returns an enumerator that iterates through the linked list as a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the linked list as a collection.</returns>
		// Token: 0x06003970 RID: 14704 RVA: 0x000D1AB7 File Offset: 0x000CFCB7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04002CA5 RID: 11429
		internal LinkedListNode<T> head;

		// Token: 0x04002CA6 RID: 11430
		internal int count;

		// Token: 0x04002CA7 RID: 11431
		internal int version;

		// Token: 0x04002CA8 RID: 11432
		private object _syncRoot;

		// Token: 0x04002CA9 RID: 11433
		private SerializationInfo _siInfo;

		// Token: 0x04002CAA RID: 11434
		private const string VersionName = "Version";

		// Token: 0x04002CAB RID: 11435
		private const string CountName = "Count";

		// Token: 0x04002CAC RID: 11436
		private const string ValuesName = "Data";

		/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		// Token: 0x0200071E RID: 1822
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator, ISerializable, IDeserializationCallback
		{
			// Token: 0x06003971 RID: 14705 RVA: 0x000D1EB4 File Offset: 0x000D00B4
			internal Enumerator(LinkedList<T> list)
			{
				this._list = list;
				this._version = list.version;
				this._node = list.head;
				this._current = default(T);
				this._index = 0;
			}

			// Token: 0x06003972 RID: 14706 RVA: 0x0000F3CE File Offset: 0x0000D5CE
			private Enumerator(SerializationInfo info, StreamingContext context)
			{
				throw new PlatformNotSupportedException();
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the <see cref="T:System.Collections.Generic.LinkedList`1" /> at the current position of the enumerator.</returns>
			// Token: 0x17000DE4 RID: 3556
			// (get) Token: 0x06003973 RID: 14707 RVA: 0x000D1EE8 File Offset: 0x000D00E8
			public T Current
			{
				get
				{
					return this._current;
				}
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x17000DE5 RID: 3557
			// (get) Token: 0x06003974 RID: 14708 RVA: 0x000D1EF0 File Offset: 0x000D00F0
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index == this._list.Count + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._current;
				}
			}

			/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
			/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x06003975 RID: 14709 RVA: 0x000D1F28 File Offset: 0x000D0128
			public bool MoveNext()
			{
				if (this._version != this._list.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._node == null)
				{
					this._index = this._list.Count + 1;
					return false;
				}
				this._index++;
				this._current = this._node.item;
				this._node = this._node.next;
				if (this._node == this._list.head)
				{
					this._node = null;
				}
				return true;
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection. This class cannot be inherited.</summary>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x06003976 RID: 14710 RVA: 0x000D1FBC File Offset: 0x000D01BC
			void IEnumerator.Reset()
			{
				if (this._version != this._list.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._current = default(T);
				this._node = this._list.head;
				this._index = 0;
			}

			/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.LinkedList`1.Enumerator" />.</summary>
			// Token: 0x06003977 RID: 14711 RVA: 0x000027E8 File Offset: 0x000009E8
			public void Dispose()
			{
			}

			/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize the <see cref="T:System.Collections.Generic.LinkedList`1" /> instance.</summary>
			/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the <see cref="T:System.Collections.Generic.LinkedList`1" /> instance.</param>
			/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.LinkedList`1" /> instance.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="info" /> is null.</exception>
			// Token: 0x06003978 RID: 14712 RVA: 0x0000F3CE File Offset: 0x0000D5CE
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new PlatformNotSupportedException();
			}

			/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and raises the deserialization event when the deserialization is complete.</summary>
			/// <param name="sender">The source of the deserialization event.</param>
			/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Generic.LinkedList`1" /> instance is invalid.</exception>
			// Token: 0x06003979 RID: 14713 RVA: 0x0000F3CE File Offset: 0x0000D5CE
			void IDeserializationCallback.OnDeserialization(object sender)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x04002CAD RID: 11437
			private LinkedList<T> _list;

			// Token: 0x04002CAE RID: 11438
			private LinkedListNode<T> _node;

			// Token: 0x04002CAF RID: 11439
			private int _version;

			// Token: 0x04002CB0 RID: 11440
			private T _current;

			// Token: 0x04002CB1 RID: 11441
			private int _index;

			// Token: 0x04002CB2 RID: 11442
			private const string LinkedListName = "LinkedList";

			// Token: 0x04002CB3 RID: 11443
			private const string CurrentValueName = "Current";

			// Token: 0x04002CB4 RID: 11444
			private const string VersionName = "Version";

			// Token: 0x04002CB5 RID: 11445
			private const string IndexName = "Index";
		}
	}
}
