using System;
using System.Collections;
using System.ComponentModel;

namespace System.Web.UI
{
	/// <summary>Represents a collection of <see cref="T:System.Web.UI.ExpressionBinding" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020001CD RID: 461
	public sealed class ExpressionBindingCollection : ICollection, IEnumerable
	{
		/// <summary>Occurs when the collection of <see cref="T:System.Web.UI.ExpressionBinding" /> objects is changed.</summary>
		// Token: 0x1400002B RID: 43
		// (add) Token: 0x060012CA RID: 4810 RVA: 0x0003325D File Offset: 0x0003145D
		// (remove) Token: 0x060012CB RID: 4811 RVA: 0x00033270 File Offset: 0x00031470
		public event EventHandler Changed
		{
			add
			{
				this.events.AddHandler(ExpressionBindingCollection.changedEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(ExpressionBindingCollection.changedEvent, value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ExpressionBindingCollection" /> class.</summary>
		// Token: 0x060012CC RID: 4812 RVA: 0x00033283 File Offset: 0x00031483
		public ExpressionBindingCollection()
		{
			this.list = new Hashtable();
			this.removed = new ArrayList();
		}

		/// <summary>Gets the number of <see cref="T:System.Web.UI.ExpressionBinding" /> objects in the <see cref="T:System.Web.UI.ExpressionBindingCollection" /> collection.</summary>
		/// <returns>The number of <see cref="T:System.Web.UI.ExpressionBinding" /> objects in the <see cref="T:System.Web.UI.ExpressionBindingCollection" />.</returns>
		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x060012CD RID: 4813 RVA: 0x000332AC File Offset: 0x000314AC
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.ExpressionBinding" /> objects in the collection can be modified.</summary>
		/// <returns>false in all cases. </returns>
		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x000332B9 File Offset: 0x000314B9
		public bool IsReadOnly
		{
			get
			{
				return this.list.IsReadOnly;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>false in all cases. </returns>
		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x060012CF RID: 4815 RVA: 0x000332C6 File Offset: 0x000314C6
		public bool IsSynchronized
		{
			get
			{
				return this.list.IsSynchronized;
			}
		}

		/// <summary>Gets an <see cref="T:System.Web.UI.ExpressionBinding" /> object from the collection with the specified <see cref="P:System.Web.UI.ExpressionBinding.PropertyName" /> value.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ExpressionBinding" /> in the <see cref="T:System.Web.UI.ExpressionBindingCollection" /> with the specified <see cref="P:System.Web.UI.ExpressionBinding.PropertyName" />.</returns>
		/// <param name="propertyName">The <see cref="P:System.Web.UI.ExpressionBinding.PropertyName" /> of the <see cref="T:System.Web.UI.ExpressionBinding" /> to retrieve.</param>
		// Token: 0x1700060A RID: 1546
		public ExpressionBinding this[string propertyName]
		{
			get
			{
				return this.list[propertyName] as ExpressionBinding;
			}
		}

		/// <summary>Gets a collection of strings representing the names of bindings that have been removed.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the names of bindings that have been removed.</returns>
		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x000332E6 File Offset: 0x000314E6
		public ICollection RemovedBindings
		{
			get
			{
				return this.removed;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An <see cref="T:System.Object" /> that can be used to synchronize access to the <see cref="T:System.Web.UI.ExpressionBindingCollection" />.</returns>
		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x000332EE File Offset: 0x000314EE
		public object SyncRoot
		{
			get
			{
				return this.list.SyncRoot;
			}
		}

		/// <summary>Appends the specified <see cref="T:System.Web.UI.ExpressionBinding" /> object to the end of the collection.</summary>
		/// <param name="binding">The <see cref="T:System.Web.UI.ExpressionBinding" /> to append to the <see cref="T:System.Web.UI.ExpressionBindingCollection" />.</param>
		// Token: 0x060012D3 RID: 4819 RVA: 0x000332FB File Offset: 0x000314FB
		public void Add(ExpressionBinding binding)
		{
			this.list.Add(binding.PropertyName, binding);
			this.OnChanged(new EventArgs());
		}

		/// <summary>Removes all the <see cref="T:System.Web.UI.ExpressionBinding" /> objects from the collection.</summary>
		// Token: 0x060012D4 RID: 4820 RVA: 0x0003331A File Offset: 0x0003151A
		public void Clear()
		{
			this.list.Clear();
			this.removed.Clear();
			this.OnChanged(new EventArgs());
		}

		/// <summary>Determines whether the <see cref="T:System.Web.UI.ExpressionBindingCollection" /> collection contains a specific <see cref="T:System.Web.UI.ExpressionBinding" /> object.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.ExpressionBinding" /> is found in the <see cref="T:System.Web.UI.ExpressionBindingCollection" />; otherwise, false.</returns>
		/// <param name="propName">The <see cref="P:System.Web.UI.ExpressionBinding.PropertyName" /> of the <see cref="T:System.Web.UI.ExpressionBinding" /> to locate in the collection.</param>
		// Token: 0x060012D5 RID: 4821 RVA: 0x0003333D File Offset: 0x0003153D
		public bool Contains(string propName)
		{
			return this.list.Contains(propName);
		}

		/// <summary>Copies all the <see cref="T:System.Web.UI.ExpressionBinding" /> objects from the <see cref="T:System.Web.UI.ExpressionBindingCollection" /> collection to a one-dimensional array, starting at the specified index in the target array.</summary>
		/// <param name="array">The zero-based array that receives the <see cref="T:System.Web.UI.ExpressionBinding" /> objects copied from the collection.</param>
		/// <param name="index">The position in the target array at which the array starts receiving the copied items.</param>
		// Token: 0x060012D6 RID: 4822 RVA: 0x0003334B File Offset: 0x0003154B
		public void CopyTo(Array array, int index)
		{
			this.list.CopyTo(array, index);
		}

		/// <summary>Copies all the <see cref="T:System.Web.UI.ExpressionBinding" /> objects from the <see cref="T:System.Web.UI.ExpressionBindingCollection" /> collection to a one-dimensional array of <see cref="T:System.Web.UI.ExpressionBinding" /> objects, starting at the specified index in the target array.</summary>
		/// <param name="array">The zero-based array of <see cref="T:System.Web.UI.ExpressionBinding" /> objects that receives the <see cref="T:System.Web.UI.ExpressionBinding" /> objects copied from the collection.</param>
		/// <param name="index">The position in the target array at which the array starts receiving the copied items.</param>
		// Token: 0x060012D7 RID: 4823 RVA: 0x0003335C File Offset: 0x0003155C
		public void CopyTo(ExpressionBinding[] array, int index)
		{
			if (index < 0)
			{
				throw new ArgumentNullException("Index cannot be negative");
			}
			if (index >= array.Length)
			{
				throw new ArgumentException("Index cannot be greater than or equal to length of array passed");
			}
			if (this.list.Count > array.Length - index + 1)
			{
				throw new ArgumentException("Number of elements in source is greater than available space from index to end of destination");
			}
			foreach (object obj in this.list.Keys)
			{
				string text = (string)obj;
				array[index++] = (ExpressionBinding)this.list[text];
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" />-implemented object that can be used to iterate through the <see cref="T:System.Web.UI.ExpressionBinding" /> objects in the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" />-implemented object that contains all the <see cref="T:System.Web.UI.ExpressionBinding" /> objects in the <see cref="T:System.Web.UI.ExpressionBindingCollection" />.</returns>
		// Token: 0x060012D8 RID: 4824 RVA: 0x0003340C File Offset: 0x0003160C
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.ExpressionBinding" /> object from the collection.</summary>
		/// <param name="binding">The <see cref="T:System.Web.UI.ExpressionBinding" /> to remove from the collection.</param>
		// Token: 0x060012D9 RID: 4825 RVA: 0x00033419 File Offset: 0x00031619
		public void Remove(ExpressionBinding binding)
		{
			this.Remove(binding.PropertyName, true);
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.ExpressionBinding" /> object from the collection.</summary>
		/// <param name="propertyName">The <see cref="P:System.Web.UI.ExpressionBinding.PropertyName" /> of the <see cref="T:System.Web.UI.ExpressionBinding" /> to remove from the collection.</param>
		// Token: 0x060012DA RID: 4826 RVA: 0x00033428 File Offset: 0x00031628
		public void Remove(string propertyName)
		{
			this.Remove(propertyName, true);
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.ExpressionBinding" /> object from the collection.</summary>
		/// <param name="propertyName">The <see cref="P:System.Web.UI.ExpressionBinding.PropertyName" /> of the <see cref="T:System.Web.UI.ExpressionBinding" /> to remove from the collection.</param>
		/// <param name="addToRemovedList">true to add the <see cref="T:System.Web.UI.ExpressionBinding" /> to the <see cref="P:System.Web.UI.ExpressionBindingCollection.RemovedBindings" /> collection; otherwise, false.</param>
		// Token: 0x060012DB RID: 4827 RVA: 0x00033432 File Offset: 0x00031632
		public void Remove(string propertyName, bool addToRemovedList)
		{
			if (addToRemovedList)
			{
				this.removed.Add(string.Empty);
			}
			else
			{
				this.removed.Add(propertyName);
			}
			this.list.Remove(propertyName);
			this.OnChanged(new EventArgs());
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x00033470 File Offset: 0x00031670
		private void OnChanged(EventArgs e)
		{
			EventHandler eventHandler = this.events[ExpressionBindingCollection.changedEvent] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x04001436 RID: 5174
		private static readonly object changedEvent = new object();

		// Token: 0x04001437 RID: 5175
		private Hashtable list;

		// Token: 0x04001438 RID: 5176
		private ArrayList removed;

		// Token: 0x04001439 RID: 5177
		private EventHandlerList events = new EventHandlerList();
	}
}
