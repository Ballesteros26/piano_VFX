using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Represents a collection of <see cref="T:System.ComponentModel.EventDescriptor" /> objects.</summary>
	// Token: 0x0200026D RID: 621
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public class EventDescriptorCollection : ICollection, IEnumerable, IList
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EventDescriptorCollection" /> class with the given array of <see cref="T:System.ComponentModel.EventDescriptor" /> objects.</summary>
		/// <param name="events">An array of type <see cref="T:System.ComponentModel.EventDescriptor" /> that provides the events for this collection. </param>
		// Token: 0x060013D1 RID: 5073 RVA: 0x00052278 File Offset: 0x00050478
		public EventDescriptorCollection(EventDescriptor[] events)
		{
			this.events = events;
			if (events == null)
			{
				this.events = new EventDescriptor[0];
				this.eventCount = 0;
			}
			else
			{
				this.eventCount = this.events.Length;
			}
			this.eventsOwned = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EventDescriptorCollection" /> class with the given array of <see cref="T:System.ComponentModel.EventDescriptor" /> objects. The collection is optionally read-only.</summary>
		/// <param name="events">An array of type <see cref="T:System.ComponentModel.EventDescriptor" /> that provides the events for this collection. </param>
		/// <param name="readOnly">true to specify a read-only collection; otherwise, false.</param>
		// Token: 0x060013D2 RID: 5074 RVA: 0x000522C6 File Offset: 0x000504C6
		public EventDescriptorCollection(EventDescriptor[] events, bool readOnly)
			: this(events)
		{
			this.readOnly = readOnly;
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x000522D8 File Offset: 0x000504D8
		private EventDescriptorCollection(EventDescriptor[] events, int eventCount, string[] namedSort, IComparer comparer)
		{
			this.eventsOwned = false;
			if (namedSort != null)
			{
				this.namedSort = (string[])namedSort.Clone();
			}
			this.comparer = comparer;
			this.events = events;
			this.eventCount = eventCount;
			this.needSort = true;
		}

		/// <summary>Gets the number of event descriptors in the collection.</summary>
		/// <returns>The number of event descriptors in the collection.</returns>
		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x0005232A File Offset: 0x0005052A
		public int Count
		{
			get
			{
				return this.eventCount;
			}
		}

		/// <summary>Gets or sets the event with the specified index number.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.EventDescriptor" /> with the specified index number.</returns>
		/// <param name="index">The zero-based index number of the <see cref="T:System.ComponentModel.EventDescriptor" /> to get or set. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is not a valid index for <see cref="P:System.ComponentModel.EventDescriptorCollection.Item(System.Int32)" />. </exception>
		// Token: 0x1700041A RID: 1050
		public virtual EventDescriptor this[int index]
		{
			get
			{
				if (index >= this.eventCount)
				{
					throw new IndexOutOfRangeException();
				}
				this.EnsureEventsOwned();
				return this.events[index];
			}
		}

		/// <summary>Gets or sets the event with the specified name.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.EventDescriptor" /> with the specified name, or null if the event does not exist.</returns>
		/// <param name="name">The name of the <see cref="T:System.ComponentModel.EventDescriptor" /> to get or set. </param>
		// Token: 0x1700041B RID: 1051
		public virtual EventDescriptor this[string name]
		{
			get
			{
				return this.Find(name, false);
			}
		}

		/// <summary>Adds an <see cref="T:System.ComponentModel.EventDescriptor" /> to the end of the collection.</summary>
		/// <returns>The position of the <see cref="T:System.ComponentModel.EventDescriptor" /> within the collection.</returns>
		/// <param name="value">An <see cref="T:System.ComponentModel.EventDescriptor" /> to add to the collection. </param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x060013D7 RID: 5079 RVA: 0x0005235C File Offset: 0x0005055C
		public int Add(EventDescriptor value)
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			this.EnsureSize(this.eventCount + 1);
			EventDescriptor[] array = this.events;
			int num = this.eventCount;
			this.eventCount = num + 1;
			array[num] = value;
			return this.eventCount - 1;
		}

		/// <summary>Removes all objects from the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x060013D8 RID: 5080 RVA: 0x000523A6 File Offset: 0x000505A6
		public void Clear()
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			this.eventCount = 0;
		}

		/// <summary>Returns whether the collection contains the given <see cref="T:System.ComponentModel.EventDescriptor" />.</summary>
		/// <returns>true if the collection contains the <paramref name="value" /> parameter given; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.EventDescriptor" /> to find within the collection. </param>
		// Token: 0x060013D9 RID: 5081 RVA: 0x000523BD File Offset: 0x000505BD
		public bool Contains(EventDescriptor value)
		{
			return this.IndexOf(value) >= 0;
		}

		/// <summary>Copies the elements of the collection to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from collection. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x060013DA RID: 5082 RVA: 0x000523CC File Offset: 0x000505CC
		void ICollection.CopyTo(Array array, int index)
		{
			this.EnsureEventsOwned();
			Array.Copy(this.events, 0, array, index, this.Count);
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x000523E8 File Offset: 0x000505E8
		private void EnsureEventsOwned()
		{
			if (!this.eventsOwned)
			{
				this.eventsOwned = true;
				if (this.events != null)
				{
					EventDescriptor[] array = new EventDescriptor[this.Count];
					Array.Copy(this.events, 0, array, 0, this.Count);
					this.events = array;
				}
			}
			if (this.needSort)
			{
				this.needSort = false;
				this.InternalSort(this.namedSort);
			}
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x00052450 File Offset: 0x00050650
		private void EnsureSize(int sizeNeeded)
		{
			if (sizeNeeded <= this.events.Length)
			{
				return;
			}
			if (this.events == null || this.events.Length == 0)
			{
				this.eventCount = 0;
				this.events = new EventDescriptor[sizeNeeded];
				return;
			}
			this.EnsureEventsOwned();
			EventDescriptor[] array = new EventDescriptor[Math.Max(sizeNeeded, this.events.Length * 2)];
			Array.Copy(this.events, 0, array, 0, this.eventCount);
			this.events = array;
		}

		/// <summary>Gets the description of the event with the specified name in the collection.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.EventDescriptor" /> with the specified name, or null if the event does not exist.</returns>
		/// <param name="name">The name of the event to get from the collection. </param>
		/// <param name="ignoreCase">true if you want to ignore the case of the event; otherwise, false. </param>
		// Token: 0x060013DD RID: 5085 RVA: 0x000524C8 File Offset: 0x000506C8
		public virtual EventDescriptor Find(string name, bool ignoreCase)
		{
			EventDescriptor eventDescriptor = null;
			if (ignoreCase)
			{
				for (int i = 0; i < this.Count; i++)
				{
					if (string.Equals(this.events[i].Name, name, StringComparison.OrdinalIgnoreCase))
					{
						eventDescriptor = this.events[i];
						break;
					}
				}
			}
			else
			{
				for (int j = 0; j < this.Count; j++)
				{
					if (string.Equals(this.events[j].Name, name, StringComparison.Ordinal))
					{
						eventDescriptor = this.events[j];
						break;
					}
				}
			}
			return eventDescriptor;
		}

		/// <summary>Returns the index of the given <see cref="T:System.ComponentModel.EventDescriptor" />.</summary>
		/// <returns>The index of the given <see cref="T:System.ComponentModel.EventDescriptor" /> within the collection.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.EventDescriptor" /> to find within the collection. </param>
		// Token: 0x060013DE RID: 5086 RVA: 0x00052541 File Offset: 0x00050741
		public int IndexOf(EventDescriptor value)
		{
			return Array.IndexOf<EventDescriptor>(this.events, value, 0, this.eventCount);
		}

		/// <summary>Inserts an <see cref="T:System.ComponentModel.EventDescriptor" /> to the collection at a specified index.</summary>
		/// <param name="index">The index within the collection in which to insert the <paramref name="value" /> parameter. </param>
		/// <param name="value">An <see cref="T:System.ComponentModel.EventDescriptor" /> to insert into the collection. </param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x060013DF RID: 5087 RVA: 0x00052558 File Offset: 0x00050758
		public void Insert(int index, EventDescriptor value)
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			this.EnsureSize(this.eventCount + 1);
			if (index < this.eventCount)
			{
				Array.Copy(this.events, index, this.events, index + 1, this.eventCount - index);
			}
			this.events[index] = value;
			this.eventCount++;
		}

		/// <summary>Removes the specified <see cref="T:System.ComponentModel.EventDescriptor" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.ComponentModel.EventDescriptor" /> to remove from the collection. </param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x060013E0 RID: 5088 RVA: 0x000525C0 File Offset: 0x000507C0
		public void Remove(EventDescriptor value)
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			int num = this.IndexOf(value);
			if (num != -1)
			{
				this.RemoveAt(num);
			}
		}

		/// <summary>Removes the <see cref="T:System.ComponentModel.EventDescriptor" /> at the specified index from the collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.ComponentModel.EventDescriptor" /> to remove. </param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x060013E1 RID: 5089 RVA: 0x000525F0 File Offset: 0x000507F0
		public void RemoveAt(int index)
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			if (index < this.eventCount - 1)
			{
				Array.Copy(this.events, index + 1, this.events, index, this.eventCount - index - 1);
			}
			this.events[this.eventCount - 1] = null;
			this.eventCount--;
		}

		/// <summary>Gets an enumerator for this <see cref="T:System.ComponentModel.EventDescriptorCollection" />.</summary>
		/// <returns>An enumerator that implements <see cref="T:System.Collections.IEnumerator" />.</returns>
		// Token: 0x060013E2 RID: 5090 RVA: 0x00052653 File Offset: 0x00050853
		public IEnumerator GetEnumerator()
		{
			if (this.events.Length == this.eventCount)
			{
				return this.events.GetEnumerator();
			}
			return new ArraySubsetEnumerator(this.events, this.eventCount);
		}

		/// <summary>Sorts the members of this <see cref="T:System.ComponentModel.EventDescriptorCollection" />, using the default sort for this collection, which is usually alphabetical.</summary>
		/// <returns>The new <see cref="T:System.ComponentModel.EventDescriptorCollection" />.</returns>
		// Token: 0x060013E3 RID: 5091 RVA: 0x00052682 File Offset: 0x00050882
		public virtual EventDescriptorCollection Sort()
		{
			return new EventDescriptorCollection(this.events, this.eventCount, this.namedSort, this.comparer);
		}

		/// <summary>Sorts the members of this <see cref="T:System.ComponentModel.EventDescriptorCollection" />, given a specified sort order.</summary>
		/// <returns>The new <see cref="T:System.ComponentModel.EventDescriptorCollection" />.</returns>
		/// <param name="names">An array of strings describing the order in which to sort the <see cref="T:System.ComponentModel.EventDescriptor" /> objects in the collection. </param>
		// Token: 0x060013E4 RID: 5092 RVA: 0x000526A1 File Offset: 0x000508A1
		public virtual EventDescriptorCollection Sort(string[] names)
		{
			return new EventDescriptorCollection(this.events, this.eventCount, names, this.comparer);
		}

		/// <summary>Sorts the members of this <see cref="T:System.ComponentModel.EventDescriptorCollection" />, given a specified sort order and an <see cref="T:System.Collections.IComparer" />.</summary>
		/// <returns>The new <see cref="T:System.ComponentModel.EventDescriptorCollection" />.</returns>
		/// <param name="names">An array of strings describing the order in which to sort the <see cref="T:System.ComponentModel.EventDescriptor" /> objects in the collection. </param>
		/// <param name="comparer">An <see cref="T:System.Collections.IComparer" /> to use to sort the <see cref="T:System.ComponentModel.EventDescriptor" /> objects in this collection. </param>
		// Token: 0x060013E5 RID: 5093 RVA: 0x000526BB File Offset: 0x000508BB
		public virtual EventDescriptorCollection Sort(string[] names, IComparer comparer)
		{
			return new EventDescriptorCollection(this.events, this.eventCount, names, comparer);
		}

		/// <summary>Sorts the members of this <see cref="T:System.ComponentModel.EventDescriptorCollection" />, using the specified <see cref="T:System.Collections.IComparer" />.</summary>
		/// <returns>The new <see cref="T:System.ComponentModel.EventDescriptorCollection" />.</returns>
		/// <param name="comparer">An <see cref="T:System.Collections.IComparer" /> to use to sort the <see cref="T:System.ComponentModel.EventDescriptor" /> objects in this collection. </param>
		// Token: 0x060013E6 RID: 5094 RVA: 0x000526D0 File Offset: 0x000508D0
		public virtual EventDescriptorCollection Sort(IComparer comparer)
		{
			return new EventDescriptorCollection(this.events, this.eventCount, this.namedSort, comparer);
		}

		/// <summary>Sorts the members of this <see cref="T:System.ComponentModel.EventDescriptorCollection" />. The specified order is applied first, followed by the default sort for this collection, which is usually alphabetical.</summary>
		/// <param name="names">An array of strings describing the order in which to sort the <see cref="T:System.ComponentModel.EventDescriptor" /> objects in this collection. </param>
		// Token: 0x060013E7 RID: 5095 RVA: 0x000526EC File Offset: 0x000508EC
		protected void InternalSort(string[] names)
		{
			if (this.events == null || this.events.Length == 0)
			{
				return;
			}
			this.InternalSort(this.comparer);
			if (names != null && names.Length != 0)
			{
				ArrayList arrayList = new ArrayList(this.events);
				int num = 0;
				int num2 = this.events.Length;
				for (int i = 0; i < names.Length; i++)
				{
					for (int j = 0; j < num2; j++)
					{
						EventDescriptor eventDescriptor = (EventDescriptor)arrayList[j];
						if (eventDescriptor != null && eventDescriptor.Name.Equals(names[i]))
						{
							this.events[num++] = eventDescriptor;
							arrayList[j] = null;
							break;
						}
					}
				}
				for (int k = 0; k < num2; k++)
				{
					if (arrayList[k] != null)
					{
						this.events[num++] = (EventDescriptor)arrayList[k];
					}
				}
			}
		}

		/// <summary>Sorts the members of this <see cref="T:System.ComponentModel.EventDescriptorCollection" />, using the specified <see cref="T:System.Collections.IComparer" />.</summary>
		/// <param name="sorter">A comparer to use to sort the <see cref="T:System.ComponentModel.EventDescriptor" /> objects in this collection. </param>
		// Token: 0x060013E8 RID: 5096 RVA: 0x000527C9 File Offset: 0x000509C9
		protected void InternalSort(IComparer sorter)
		{
			if (sorter == null)
			{
				TypeDescriptor.SortDescriptorArray(this);
				return;
			}
			Array.Sort(this.events, sorter);
		}

		/// <summary>Gets the number of elements contained in the collection.</summary>
		/// <returns>The number of elements contained in the collection.</returns>
		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x060013E9 RID: 5097 RVA: 0x000527E1 File Offset: 0x000509E1
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized.</summary>
		/// <returns>true if access to the collection is synchronized; otherwise, false.</returns>
		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x060013EA RID: 5098 RVA: 0x00004240 File Offset: 0x00002440
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x060013EB RID: 5099 RVA: 0x00009E57 File Offset: 0x00008057
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x060013EC RID: 5100 RVA: 0x000527E9 File Offset: 0x000509E9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is less than 0. -or-<paramref name="index" /> is equal to or greater than <see cref="P:System.ComponentModel.EventDescriptorCollection.Count" />.</exception>
		// Token: 0x1700041F RID: 1055
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				if (this.readOnly)
				{
					throw new NotSupportedException();
				}
				if (index >= this.eventCount)
				{
					throw new IndexOutOfRangeException();
				}
				this.EnsureEventsOwned();
				this.events[index] = (EventDescriptor)value;
			}
		}

		/// <summary>Adds an item to the collection.</summary>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to add to the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x060013EF RID: 5103 RVA: 0x0005282D File Offset: 0x00050A2D
		int IList.Add(object value)
		{
			return this.Add((EventDescriptor)value);
		}

		/// <summary>Removes all the items from the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x060013F0 RID: 5104 RVA: 0x0005283B File Offset: 0x00050A3B
		void IList.Clear()
		{
			this.Clear();
		}

		/// <summary>Determines whether the collection contains a specific value.</summary>
		/// <returns>true if the <see cref="T:System.Object" /> is found in the collection; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection.</param>
		// Token: 0x060013F1 RID: 5105 RVA: 0x00052843 File Offset: 0x00050A43
		bool IList.Contains(object value)
		{
			return this.Contains((EventDescriptor)value);
		}

		/// <summary>Determines the index of a specific item in the collection.</summary>
		/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection.</param>
		// Token: 0x060013F2 RID: 5106 RVA: 0x00052851 File Offset: 0x00050A51
		int IList.IndexOf(object value)
		{
			return this.IndexOf((EventDescriptor)value);
		}

		/// <summary>Inserts an item to the collection at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert into the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x060013F3 RID: 5107 RVA: 0x0005285F File Offset: 0x00050A5F
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (EventDescriptor)value);
		}

		/// <summary>Removes the first occurrence of a specific object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to remove from the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x060013F4 RID: 5108 RVA: 0x0005286E File Offset: 0x00050A6E
		void IList.Remove(object value)
		{
			this.Remove((EventDescriptor)value);
		}

		/// <summary>Removes the item at the specified index.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x060013F5 RID: 5109 RVA: 0x0005287C File Offset: 0x00050A7C
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise, false.</returns>
		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x060013F6 RID: 5110 RVA: 0x00052885 File Offset: 0x00050A85
		bool IList.IsReadOnly
		{
			get
			{
				return this.readOnly;
			}
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>true if the collection has a fixed size; otherwise, false.</returns>
		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x060013F7 RID: 5111 RVA: 0x00052885 File Offset: 0x00050A85
		bool IList.IsFixedSize
		{
			get
			{
				return this.readOnly;
			}
		}

		// Token: 0x040012D8 RID: 4824
		private EventDescriptor[] events;

		// Token: 0x040012D9 RID: 4825
		private string[] namedSort;

		// Token: 0x040012DA RID: 4826
		private IComparer comparer;

		// Token: 0x040012DB RID: 4827
		private bool eventsOwned = true;

		// Token: 0x040012DC RID: 4828
		private bool needSort;

		// Token: 0x040012DD RID: 4829
		private int eventCount;

		// Token: 0x040012DE RID: 4830
		private bool readOnly;

		/// <summary>Specifies an empty collection to use, rather than creating a new one with no items. This static field is read-only.</summary>
		// Token: 0x040012DF RID: 4831
		public static readonly EventDescriptorCollection Empty = new EventDescriptorCollection(null, true);
	}
}
