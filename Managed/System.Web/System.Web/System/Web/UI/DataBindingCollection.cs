using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Provides a collection of <see cref="T:System.Web.UI.DataBinding" /> objects for an ASP.NET server control. This class cannot be inherited.</summary>
	// Token: 0x020001C1 RID: 449
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class DataBindingCollection : ICollection, IEnumerable
	{
		/// <summary>Occurs when the collection of <see cref="T:System.Web.UI.DataBinding" /> objects is changed. </summary>
		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06001230 RID: 4656 RVA: 0x0003263A File Offset: 0x0003083A
		// (remove) Token: 0x06001231 RID: 4657 RVA: 0x0003264D File Offset: 0x0003084D
		public event EventHandler Changed
		{
			add
			{
				this.events.AddHandler(DataBindingCollection.changedEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(DataBindingCollection.changedEvent, value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DataBindingCollection" /> class.</summary>
		// Token: 0x06001232 RID: 4658 RVA: 0x00032660 File Offset: 0x00030860
		public DataBindingCollection()
		{
			this.list = new Hashtable();
			this.removed = new ArrayList();
		}

		/// <summary>Gets the number of <see cref="T:System.Web.UI.DataBinding" /> objects in the <see cref="T:System.Web.UI.DataBindingCollection" /> object.</summary>
		/// <returns>The number of <see cref="T:System.Web.UI.DataBinding" /> objects in the <see cref="T:System.Web.UI.DataBindingCollection" />.</returns>
		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x00032689 File Offset: 0x00030889
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataBindingCollection" /> collection is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise, false. The default is false.</returns>
		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x00032696 File Offset: 0x00030896
		public bool IsReadOnly
		{
			get
			{
				return this.list.IsReadOnly;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataBindingCollection" /> collection is synchronized (thread safe).</summary>
		/// <returns>Always false.</returns>
		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001235 RID: 4661 RVA: 0x000326A3 File Offset: 0x000308A3
		public bool IsSynchronized
		{
			get
			{
				return this.list.IsSynchronized;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.DataBinding" /> object with the specified property name.</summary>
		/// <returns>The <see cref="T:System.Web.UI.DataBinding" /> with the specified property name. If no object with the specified name exists, this value is null.</returns>
		/// <param name="propertyName">The name of the property to be found. </param>
		// Token: 0x170005DF RID: 1503
		public DataBinding this[string propertyName]
		{
			get
			{
				return this.list[propertyName] as DataBinding;
			}
		}

		/// <summary>Gets an array of the names of the <see cref="T:System.Web.UI.DataBinding" /> objects removed from the collection.</summary>
		/// <returns>The array of names of the <see cref="T:System.Web.UI.DataBinding" /> objects removed from the collection. </returns>
		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001237 RID: 4663 RVA: 0x000326C3 File Offset: 0x000308C3
		public string[] RemovedBindings
		{
			get
			{
				return (string[])this.removed.ToArray(typeof(string));
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Web.UI.DataBindingCollection" /> collection.</summary>
		/// <returns>The <see cref="T:System.Object" /> to be used to synchronize access to the collection.</returns>
		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001238 RID: 4664 RVA: 0x000326DF File Offset: 0x000308DF
		public object SyncRoot
		{
			get
			{
				return this.list.SyncRoot;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Web.UI.DataBinding" /> object to the <see cref="T:System.Web.UI.DataBindingCollection" /> collection.</summary>
		/// <param name="binding">The data-binding object to add to the collection. </param>
		// Token: 0x06001239 RID: 4665 RVA: 0x000326EC File Offset: 0x000308EC
		public void Add(DataBinding binding)
		{
			this.list.Add(binding.PropertyName, binding);
			this.RaiseChanged();
		}

		/// <summary>Removes all <see cref="T:System.Web.UI.DataBinding" /> objects from the <see cref="T:System.Web.UI.DataBindingCollection" /> collection.</summary>
		// Token: 0x0600123A RID: 4666 RVA: 0x00032706 File Offset: 0x00030906
		public void Clear()
		{
			this.list.Clear();
		}

		/// <summary>Copies the <see cref="T:System.Web.UI.DataBindingCollection" /> values to a one-dimensional <see cref="T:System.Array" />, beginning at the <see cref="T:System.Array" /> object's specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from <see cref="T:System.Web.UI.DataBindingCollection" />. </param>
		/// <param name="index">The index in the array, specified by the <paramref name="array" /> parameter, where copying begins. </param>
		// Token: 0x0600123B RID: 4667 RVA: 0x00032713 File Offset: 0x00030913
		public void CopyTo(Array array, int index)
		{
			this.list.Values.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator to iterate through the <see cref="T:System.Web.UI.DataBindingCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that contains the collection's members.</returns>
		// Token: 0x0600123C RID: 4668 RVA: 0x00032727 File Offset: 0x00030927
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.DataBinding" /> object from the <see cref="T:System.Web.UI.DataBindingCollection" /> collection and adds it to the <see cref="P:System.Web.UI.DataBindingCollection.RemovedBindings" /> collection.</summary>
		/// <param name="binding">The <see cref="T:System.Web.UI.DataBinding" /> to be removed from the <see cref="T:System.Web.UI.DataBindingCollection" />. </param>
		// Token: 0x0600123D RID: 4669 RVA: 0x00032734 File Offset: 0x00030934
		public void Remove(DataBinding binding)
		{
			string propertyName = binding.PropertyName;
			this.Remove(propertyName);
		}

		/// <summary>Removes the <see cref="T:System.Web.UI.DataBinding" /> object associated with the specified property name from the <see cref="T:System.Web.UI.DataBindingCollection" /> collection and adds it to the <see cref="P:System.Web.UI.DataBindingCollection.RemovedBindings" /> collection.</summary>
		/// <param name="propertyName">The property name associated with the <see cref="T:System.Web.UI.DataBinding" /> to be removed. </param>
		// Token: 0x0600123E RID: 4670 RVA: 0x0003274F File Offset: 0x0003094F
		public void Remove(string propertyName)
		{
			this.removed.Add(propertyName);
			this.list.Remove(propertyName);
			this.RaiseChanged();
		}

		/// <summary>Removes the <see cref="T:System.Web.UI.DataBinding" /> object, associated with the specified property name, from the <see cref="T:System.Web.UI.DataBindingCollection" /> collection and controls whether to add the binding to the <see cref="P:System.Web.UI.DataBindingCollection.RemovedBindings" /> list.</summary>
		/// <param name="propertyName">The property associated with the <see cref="T:System.Web.UI.DataBinding" /> to be removed. </param>
		/// <param name="addToRemovedList">A Boolean value that indicates whether to add the property name to the <see cref="P:System.Web.UI.DataBindingCollection.RemovedBindings" /> list. true indicates that the <paramref name="propertyName" /> parameter will be added to the <see cref="P:System.Web.UI.DataBindingCollection.RemovedBindings" /> property, and false indicates that <paramref name="propertyName" /> will not be added to the <see cref="P:System.Web.UI.DataBindingCollection.RemovedBindings" /> property. </param>
		// Token: 0x0600123F RID: 4671 RVA: 0x00032770 File Offset: 0x00030970
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
		}

		/// <summary>Determines whether the data-binding collection contains a specific <see cref="T:System.Web.UI.DataBinding" /> object.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.DataBindingCollection" /> contains an element with the specified name; otherwise, false.</returns>
		/// <param name="propertyName">The name of the object to locate in the collection.</param>
		// Token: 0x06001240 RID: 4672 RVA: 0x000327A1 File Offset: 0x000309A1
		public bool Contains(string propertyName)
		{
			return this.list.Contains(propertyName);
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x000327B0 File Offset: 0x000309B0
		internal void RaiseChanged()
		{
			EventHandler eventHandler = this.events[DataBindingCollection.changedEvent] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x04001417 RID: 5143
		private static readonly object changedEvent = new object();

		// Token: 0x04001418 RID: 5144
		private Hashtable list;

		// Token: 0x04001419 RID: 5145
		private ArrayList removed;

		// Token: 0x0400141A RID: 5146
		private EventHandlerList events = new EventHandlerList();
	}
}
