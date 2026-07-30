using System;

namespace System.Collections.Generic
{
	/// <summary>Represents a node in a <see cref="T:System.Collections.Generic.LinkedList`1" />. This class cannot be inherited.</summary>
	/// <typeparam name="T">Specifies the element type of the linked list.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200071F RID: 1823
	public sealed class LinkedListNode<T>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.LinkedListNode`1" /> class, containing the specified value.</summary>
		/// <param name="value">The value to contain in the <see cref="T:System.Collections.Generic.LinkedListNode`1" />.</param>
		// Token: 0x0600397A RID: 14714 RVA: 0x000D200B File Offset: 0x000D020B
		public LinkedListNode(T value)
		{
			this.item = value;
		}

		// Token: 0x0600397B RID: 14715 RVA: 0x000D201A File Offset: 0x000D021A
		internal LinkedListNode(LinkedList<T> list, T value)
		{
			this.list = list;
			this.item = value;
		}

		/// <summary>Gets the <see cref="T:System.Collections.Generic.LinkedList`1" /> that the <see cref="T:System.Collections.Generic.LinkedListNode`1" /> belongs to.</summary>
		/// <returns>A reference to the <see cref="T:System.Collections.Generic.LinkedList`1" /> that the <see cref="T:System.Collections.Generic.LinkedListNode`1" /> belongs to, or null if the <see cref="T:System.Collections.Generic.LinkedListNode`1" /> is not linked.</returns>
		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x0600397C RID: 14716 RVA: 0x000D2030 File Offset: 0x000D0230
		public LinkedList<T> List
		{
			get
			{
				return this.list;
			}
		}

		/// <summary>Gets the next node in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>A reference to the next node in the <see cref="T:System.Collections.Generic.LinkedList`1" />, or null if the current node is the last element (<see cref="P:System.Collections.Generic.LinkedList`1.Last" />) of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</returns>
		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x0600397D RID: 14717 RVA: 0x000D2038 File Offset: 0x000D0238
		public LinkedListNode<T> Next
		{
			get
			{
				if (this.next != null && this.next != this.list.head)
				{
					return this.next;
				}
				return null;
			}
		}

		/// <summary>Gets the previous node in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>A reference to the previous node in the <see cref="T:System.Collections.Generic.LinkedList`1" />, or null if the current node is the first element (<see cref="P:System.Collections.Generic.LinkedList`1.First" />) of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</returns>
		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x0600397E RID: 14718 RVA: 0x000D205D File Offset: 0x000D025D
		public LinkedListNode<T> Previous
		{
			get
			{
				if (this.prev != null && this != this.list.head)
				{
					return this.prev;
				}
				return null;
			}
		}

		/// <summary>Gets the value contained in the node.</summary>
		/// <returns>The value contained in the node.</returns>
		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x0600397F RID: 14719 RVA: 0x000D207D File Offset: 0x000D027D
		// (set) Token: 0x06003980 RID: 14720 RVA: 0x000D2085 File Offset: 0x000D0285
		public T Value
		{
			get
			{
				return this.item;
			}
			set
			{
				this.item = value;
			}
		}

		// Token: 0x06003981 RID: 14721 RVA: 0x000D208E File Offset: 0x000D028E
		internal void Invalidate()
		{
			this.list = null;
			this.next = null;
			this.prev = null;
		}

		// Token: 0x04002CB6 RID: 11446
		internal LinkedList<T> list;

		// Token: 0x04002CB7 RID: 11447
		internal LinkedListNode<T> next;

		// Token: 0x04002CB8 RID: 11448
		internal LinkedListNode<T> prev;

		// Token: 0x04002CB9 RID: 11449
		internal T item;
	}
}
