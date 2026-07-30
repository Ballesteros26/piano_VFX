using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a generic collection that supports data binding.</summary>
	/// <typeparam name="T">The type of elements in the list.</typeparam>
	// Token: 0x02000237 RID: 567
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[Serializable]
	public class BindingList<T> : Collection<T>, IBindingList, IList, ICollection, IEnumerable, ICancelAddNew, IRaiseItemChangedEvents
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindingList`1" /> class using default values.</summary>
		// Token: 0x0600123F RID: 4671 RVA: 0x0004D67D File Offset: 0x0004B87D
		public BindingList()
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindingList`1" /> class with the specified list.</summary>
		/// <param name="list">An <see cref="T:System.Collections.Generic.IList`1" /> of items to be contained in the <see cref="T:System.ComponentModel.BindingList`1" />.</param>
		// Token: 0x06001240 RID: 4672 RVA: 0x0004D6B5 File Offset: 0x0004B8B5
		public BindingList(IList<T> list)
			: base(list)
		{
			this.Initialize();
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x0004D6F0 File Offset: 0x0004B8F0
		private void Initialize()
		{
			this.allowNew = this.ItemTypeHasDefaultConstructor;
			if (typeof(INotifyPropertyChanged).IsAssignableFrom(typeof(T)))
			{
				this.raiseItemChangedEvents = true;
				foreach (T t in base.Items)
				{
					this.HookPropertyChanged(t);
				}
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x0004D76C File Offset: 0x0004B96C
		private bool ItemTypeHasDefaultConstructor
		{
			get
			{
				Type typeFromHandle = typeof(T);
				return typeFromHandle.IsPrimitive || typeFromHandle.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, new Type[0], null) != null;
			}
		}

		/// <summary>Occurs before an item is added to the list.</summary>
		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06001243 RID: 4675 RVA: 0x0004D7AC File Offset: 0x0004B9AC
		// (remove) Token: 0x06001244 RID: 4676 RVA: 0x0004D7DB File Offset: 0x0004B9DB
		public event AddingNewEventHandler AddingNew
		{
			add
			{
				bool flag = this.AllowNew;
				this.onAddingNew = (AddingNewEventHandler)Delegate.Combine(this.onAddingNew, value);
				if (flag != this.AllowNew)
				{
					this.FireListChanged(ListChangedType.Reset, -1);
				}
			}
			remove
			{
				bool flag = this.AllowNew;
				this.onAddingNew = (AddingNewEventHandler)Delegate.Remove(this.onAddingNew, value);
				if (flag != this.AllowNew)
				{
					this.FireListChanged(ListChangedType.Reset, -1);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BindingList`1.AddingNew" /> event.</summary>
		/// <param name="e">An <see cref="T:System.ComponentModel.AddingNewEventArgs" /> that contains the event data. </param>
		// Token: 0x06001245 RID: 4677 RVA: 0x0004D80A File Offset: 0x0004BA0A
		protected virtual void OnAddingNew(AddingNewEventArgs e)
		{
			if (this.onAddingNew != null)
			{
				this.onAddingNew(this, e);
			}
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x0004D824 File Offset: 0x0004BA24
		private object FireAddingNew()
		{
			AddingNewEventArgs addingNewEventArgs = new AddingNewEventArgs(null);
			this.OnAddingNew(addingNewEventArgs);
			return addingNewEventArgs.NewObject;
		}

		/// <summary>Occurs when the list or an item in the list changes.</summary>
		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06001247 RID: 4679 RVA: 0x0004D845 File Offset: 0x0004BA45
		// (remove) Token: 0x06001248 RID: 4680 RVA: 0x0004D85E File Offset: 0x0004BA5E
		public event ListChangedEventHandler ListChanged
		{
			add
			{
				this.onListChanged = (ListChangedEventHandler)Delegate.Combine(this.onListChanged, value);
			}
			remove
			{
				this.onListChanged = (ListChangedEventHandler)Delegate.Remove(this.onListChanged, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.ListChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x06001249 RID: 4681 RVA: 0x0004D877 File Offset: 0x0004BA77
		protected virtual void OnListChanged(ListChangedEventArgs e)
		{
			if (this.onListChanged != null)
			{
				this.onListChanged(this, e);
			}
		}

		/// <summary>Gets or sets a value indicating whether adding or removing items within the list raises <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> events.</summary>
		/// <returns>true if adding or removing items raises <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> events; otherwise, false. The default is true.</returns>
		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x0600124A RID: 4682 RVA: 0x0004D88E File Offset: 0x0004BA8E
		// (set) Token: 0x0600124B RID: 4683 RVA: 0x0004D896 File Offset: 0x0004BA96
		public bool RaiseListChangedEvents
		{
			get
			{
				return this.raiseListChangedEvents;
			}
			set
			{
				if (this.raiseListChangedEvents != value)
				{
					this.raiseListChangedEvents = value;
				}
			}
		}

		/// <summary>Raises a <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> event of type <see cref="F:System.ComponentModel.ListChangedType.Reset" />.</summary>
		// Token: 0x0600124C RID: 4684 RVA: 0x0004D8A8 File Offset: 0x0004BAA8
		public void ResetBindings()
		{
			this.FireListChanged(ListChangedType.Reset, -1);
		}

		/// <summary>Raises a <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> event of type <see cref="F:System.ComponentModel.ListChangedType.ItemChanged" /> for the item at the specified position.</summary>
		/// <param name="position">A zero-based index of the item to be reset.</param>
		// Token: 0x0600124D RID: 4685 RVA: 0x0004D8B2 File Offset: 0x0004BAB2
		public void ResetItem(int position)
		{
			this.FireListChanged(ListChangedType.ItemChanged, position);
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x0004D8BC File Offset: 0x0004BABC
		private void FireListChanged(ListChangedType type, int index)
		{
			if (this.raiseListChangedEvents)
			{
				this.OnListChanged(new ListChangedEventArgs(type, index));
			}
		}

		/// <summary>Removes all elements from the collection.</summary>
		// Token: 0x0600124F RID: 4687 RVA: 0x0004D8D4 File Offset: 0x0004BAD4
		protected override void ClearItems()
		{
			this.EndNew(this.addNewPos);
			if (this.raiseItemChangedEvents)
			{
				foreach (T t in base.Items)
				{
					this.UnhookPropertyChanged(t);
				}
			}
			base.ClearItems();
			this.FireListChanged(ListChangedType.Reset, -1);
		}

		/// <summary>Inserts the specified item in the list at the specified index.</summary>
		/// <param name="index">The zero-based index where the item is to be inserted.</param>
		/// <param name="item">The item to insert in the list.</param>
		// Token: 0x06001250 RID: 4688 RVA: 0x0004D944 File Offset: 0x0004BB44
		protected override void InsertItem(int index, T item)
		{
			this.EndNew(this.addNewPos);
			base.InsertItem(index, item);
			if (this.raiseItemChangedEvents)
			{
				this.HookPropertyChanged(item);
			}
			this.FireListChanged(ListChangedType.ItemAdded, index);
		}

		/// <summary>Removes the item at the specified index.</summary>
		/// <param name="index">The zero-based index of the item to remove. </param>
		/// <exception cref="T:System.NotSupportedException">You are removing a newly added item and <see cref="P:System.ComponentModel.IBindingList.AllowRemove" /> is set to false. </exception>
		// Token: 0x06001251 RID: 4689 RVA: 0x0004D974 File Offset: 0x0004BB74
		protected override void RemoveItem(int index)
		{
			if (!this.allowRemove && (this.addNewPos < 0 || this.addNewPos != index))
			{
				throw new NotSupportedException();
			}
			this.EndNew(this.addNewPos);
			if (this.raiseItemChangedEvents)
			{
				this.UnhookPropertyChanged(base[index]);
			}
			base.RemoveItem(index);
			this.FireListChanged(ListChangedType.ItemDeleted, index);
		}

		/// <summary>Replaces the item at the specified index with the specified item.</summary>
		/// <param name="index">The zero-based index of the item to replace.</param>
		/// <param name="item">The new value for the item at the specified index. The value can be null for reference types.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="index" /> is greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.</exception>
		// Token: 0x06001252 RID: 4690 RVA: 0x0004D9D1 File Offset: 0x0004BBD1
		protected override void SetItem(int index, T item)
		{
			if (this.raiseItemChangedEvents)
			{
				this.UnhookPropertyChanged(base[index]);
			}
			base.SetItem(index, item);
			if (this.raiseItemChangedEvents)
			{
				this.HookPropertyChanged(item);
			}
			this.FireListChanged(ListChangedType.ItemChanged, index);
		}

		/// <summary>Discards a pending new item.</summary>
		/// <param name="itemIndex">The index of the of the new item to be added </param>
		// Token: 0x06001253 RID: 4691 RVA: 0x0004DA07 File Offset: 0x0004BC07
		public virtual void CancelNew(int itemIndex)
		{
			if (this.addNewPos >= 0 && this.addNewPos == itemIndex)
			{
				this.RemoveItem(this.addNewPos);
				this.addNewPos = -1;
			}
		}

		/// <summary>Commits a pending new item to the collection.</summary>
		/// <param name="itemIndex">The index of the new item to be added.</param>
		// Token: 0x06001254 RID: 4692 RVA: 0x0004DA2E File Offset: 0x0004BC2E
		public virtual void EndNew(int itemIndex)
		{
			if (this.addNewPos >= 0 && this.addNewPos == itemIndex)
			{
				this.addNewPos = -1;
			}
		}

		/// <summary>Adds a new item to the collection.</summary>
		/// <returns>The item added to the list.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.BindingSource.AllowNew" /> property is set to false. -or-A public default constructor could not be found for the current item type.</exception>
		// Token: 0x06001255 RID: 4693 RVA: 0x0004DA49 File Offset: 0x0004BC49
		public T AddNew()
		{
			return (T)((object)((IBindingList)this).AddNew());
		}

		/// <summary>Adds a new item to the list. For more information, see <see cref="M:System.ComponentModel.IBindingList.AddNew" />.</summary>
		/// <returns>The item added to the list.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x06001256 RID: 4694 RVA: 0x0004DA58 File Offset: 0x0004BC58
		object IBindingList.AddNew()
		{
			object obj = this.AddNewCore();
			this.addNewPos = ((obj != null) ? base.IndexOf((T)((object)obj)) : (-1));
			return obj;
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x0004DA85 File Offset: 0x0004BC85
		private bool AddingNewHandled
		{
			get
			{
				return this.onAddingNew != null && this.onAddingNew.GetInvocationList().Length != 0;
			}
		}

		/// <summary>Adds a new item to the end of the collection.</summary>
		/// <returns>The item that was added to the collection.</returns>
		/// <exception cref="T:System.InvalidCastException">The new item is not the same type as the objects contained in the <see cref="T:System.ComponentModel.BindingList`1" />.</exception>
		// Token: 0x06001258 RID: 4696 RVA: 0x0004DAA0 File Offset: 0x0004BCA0
		protected virtual object AddNewCore()
		{
			object obj = this.FireAddingNew();
			if (obj == null)
			{
				obj = SecurityUtils.SecureCreateInstance(typeof(T));
			}
			base.Add((T)((object)obj));
			return obj;
		}

		/// <summary>Gets or sets a value indicating whether you can add items to the list using the <see cref="M:System.ComponentModel.BindingList`1.AddNew" /> method.</summary>
		/// <returns>true if you can add items to the list with the <see cref="M:System.ComponentModel.BindingList`1.AddNew" /> method; otherwise, false. The default depends on the underlying type contained in the list.</returns>
		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x0004DAD4 File Offset: 0x0004BCD4
		// (set) Token: 0x0600125A RID: 4698 RVA: 0x0004DAF3 File Offset: 0x0004BCF3
		public bool AllowNew
		{
			get
			{
				if (this.userSetAllowNew || this.allowNew)
				{
					return this.allowNew;
				}
				return this.AddingNewHandled;
			}
			set
			{
				bool flag = this.AllowNew;
				this.userSetAllowNew = true;
				this.allowNew = value;
				if (flag != value)
				{
					this.FireListChanged(ListChangedType.Reset, -1);
				}
			}
		}

		/// <summary>Gets a value indicating whether new items can be added to the list using the <see cref="M:System.ComponentModel.BindingList`1.AddNew" /> method.</summary>
		/// <returns>true if you can add items to the list with the <see cref="M:System.ComponentModel.BindingList`1.AddNew" /> method; otherwise, false. The default depends on the underlying type contained in the list.</returns>
		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x0004DB14 File Offset: 0x0004BD14
		bool IBindingList.AllowNew
		{
			get
			{
				return this.AllowNew;
			}
		}

		/// <summary>Gets or sets a value indicating whether items in the list can be edited.</summary>
		/// <returns>true if list items can be edited; otherwise, false. The default is true.</returns>
		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x0600125C RID: 4700 RVA: 0x0004DB1C File Offset: 0x0004BD1C
		// (set) Token: 0x0600125D RID: 4701 RVA: 0x0004DB24 File Offset: 0x0004BD24
		public bool AllowEdit
		{
			get
			{
				return this.allowEdit;
			}
			set
			{
				if (this.allowEdit != value)
				{
					this.allowEdit = value;
					this.FireListChanged(ListChangedType.Reset, -1);
				}
			}
		}

		/// <summary>Gets a value indicating whether items in the list can be edited.</summary>
		/// <returns>true if list items can be edited; otherwise, false. The default is true.</returns>
		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x0600125E RID: 4702 RVA: 0x0004DB3E File Offset: 0x0004BD3E
		bool IBindingList.AllowEdit
		{
			get
			{
				return this.AllowEdit;
			}
		}

		/// <summary>Gets or sets a value indicating whether you can remove items from the collection. </summary>
		/// <returns>true if you can remove items from the list with the <see cref="M:System.ComponentModel.BindingList`1.RemoveItem(System.Int32)" /> method otherwise, false. The default is true.</returns>
		// Token: 0x170003BA RID: 954
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x0004DB46 File Offset: 0x0004BD46
		// (set) Token: 0x06001260 RID: 4704 RVA: 0x0004DB4E File Offset: 0x0004BD4E
		public bool AllowRemove
		{
			get
			{
				return this.allowRemove;
			}
			set
			{
				if (this.allowRemove != value)
				{
					this.allowRemove = value;
					this.FireListChanged(ListChangedType.Reset, -1);
				}
			}
		}

		/// <summary>Gets a value indicating whether items can be removed from the list.</summary>
		/// <returns>true if you can remove items from the list with the <see cref="M:System.ComponentModel.BindingList`1.RemoveItem(System.Int32)" /> method; otherwise, false. The default is true.</returns>
		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x0004DB68 File Offset: 0x0004BD68
		bool IBindingList.AllowRemove
		{
			get
			{
				return this.AllowRemove;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsChangeNotification" />.</summary>
		/// <returns>true if a <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> event is raised when the list changes or when an item changes; otherwise, false.</returns>
		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001262 RID: 4706 RVA: 0x0004DB70 File Offset: 0x0004BD70
		bool IBindingList.SupportsChangeNotification
		{
			get
			{
				return this.SupportsChangeNotificationCore;
			}
		}

		/// <summary>Gets a value indicating whether <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> events are enabled.</summary>
		/// <returns>true if <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> events are supported; otherwise, false. The default is true.</returns>
		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001263 RID: 4707 RVA: 0x000027E2 File Offset: 0x000009E2
		protected virtual bool SupportsChangeNotificationCore
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSearching" />.</summary>
		/// <returns>true if the list supports searching using the <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)" /> method; otherwise, false.</returns>
		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x0004DB78 File Offset: 0x0004BD78
		bool IBindingList.SupportsSearching
		{
			get
			{
				return this.SupportsSearchingCore;
			}
		}

		/// <summary>Gets a value indicating whether the list supports searching.</summary>
		/// <returns>true if the list supports searching; otherwise, false. The default is false.</returns>
		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001265 RID: 4709 RVA: 0x00004240 File Offset: 0x00002440
		protected virtual bool SupportsSearchingCore
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" />.</summary>
		/// <returns>true if the list supports sorting; otherwise, false.</returns>
		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001266 RID: 4710 RVA: 0x0004DB80 File Offset: 0x0004BD80
		bool IBindingList.SupportsSorting
		{
			get
			{
				return this.SupportsSortingCore;
			}
		}

		/// <summary>Gets a value indicating whether the list supports sorting.</summary>
		/// <returns>true if the list supports sorting; otherwise, false. The default is false.</returns>
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001267 RID: 4711 RVA: 0x00004240 File Offset: 0x00002440
		protected virtual bool SupportsSortingCore
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.IsSorted" />.</summary>
		/// <returns>true if <see cref="M:System.ComponentModel.IBindingListView.ApplySort(System.ComponentModel.ListSortDescriptionCollection)" /> has been called and <see cref="M:System.ComponentModel.IBindingList.RemoveSort" /> has not been called; otherwise, false.</returns>
		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001268 RID: 4712 RVA: 0x0004DB88 File Offset: 0x0004BD88
		bool IBindingList.IsSorted
		{
			get
			{
				return this.IsSortedCore;
			}
		}

		/// <summary>Gets a value indicating whether the list is sorted. </summary>
		/// <returns>true if the list is sorted; otherwise, false. The default is false.</returns>
		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001269 RID: 4713 RVA: 0x00004240 File Offset: 0x00002440
		protected virtual bool IsSortedCore
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortProperty" />.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> that is being used for sorting.</returns>
		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x0600126A RID: 4714 RVA: 0x0004DB90 File Offset: 0x0004BD90
		PropertyDescriptor IBindingList.SortProperty
		{
			get
			{
				return this.SortPropertyCore;
			}
		}

		/// <summary>Gets the property descriptor that is used for sorting the list if sorting is implemented in a derived class; otherwise, returns null. </summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> used for sorting the list.</returns>
		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x0600126B RID: 4715 RVA: 0x00009E57 File Offset: 0x00008057
		protected virtual PropertyDescriptor SortPropertyCore
		{
			get
			{
				return null;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortDirection" />.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values.</returns>
		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x0600126C RID: 4716 RVA: 0x0004DB98 File Offset: 0x0004BD98
		ListSortDirection IBindingList.SortDirection
		{
			get
			{
				return this.SortDirectionCore;
			}
		}

		/// <summary>Gets the direction the list is sorted.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values. The default is <see cref="F:System.ComponentModel.ListSortDirection.Ascending" />. </returns>
		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x0600126D RID: 4717 RVA: 0x00004240 File Offset: 0x00002440
		protected virtual ListSortDirection SortDirectionCore
		{
			get
			{
				return ListSortDirection.Ascending;
			}
		}

		/// <summary>Sorts the list based on a <see cref="T:System.ComponentModel.PropertyDescriptor" /> and a <see cref="T:System.ComponentModel.ListSortDirection" />. For a complete description of this member, see <see cref="M:System.ComponentModel.IBindingList.ApplySort(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)" />. </summary>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to sort by.</param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values.</param>
		// Token: 0x0600126E RID: 4718 RVA: 0x0004DBA0 File Offset: 0x0004BDA0
		void IBindingList.ApplySort(PropertyDescriptor prop, ListSortDirection direction)
		{
			this.ApplySortCore(prop, direction);
		}

		/// <summary>Sorts the items if overridden in a derived class; otherwise, throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="prop">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that specifies the property to sort on.</param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.ListSortDirection" />  values.</param>
		/// <exception cref="T:System.NotSupportedException">Method is not overridden in a derived class. </exception>
		// Token: 0x0600126F RID: 4719 RVA: 0x000074E4 File Offset: 0x000056E4
		protected virtual void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
		{
			throw new NotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.RemoveSort" /></summary>
		// Token: 0x06001270 RID: 4720 RVA: 0x0004DBAA File Offset: 0x0004BDAA
		void IBindingList.RemoveSort()
		{
			this.RemoveSortCore();
		}

		/// <summary>Removes any sort applied with <see cref="M:System.ComponentModel.BindingList`1.ApplySortCore(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)" /> if sorting is implemented in a derived class; otherwise, raises <see cref="T:System.NotSupportedException" />.</summary>
		/// <exception cref="T:System.NotSupportedException">Method is not overridden in a derived class. </exception>
		// Token: 0x06001271 RID: 4721 RVA: 0x000074E4 File Offset: 0x000056E4
		protected virtual void RemoveSortCore()
		{
			throw new NotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)" />.</summary>
		/// <returns>The index of the row that has the given <see cref="T:System.ComponentModel.PropertyDescriptor" /> .</returns>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to search on.</param>
		/// <param name="key">The value of the <paramref name="property" /> parameter to search for.</param>
		// Token: 0x06001272 RID: 4722 RVA: 0x0004DBB2 File Offset: 0x0004BDB2
		int IBindingList.Find(PropertyDescriptor prop, object key)
		{
			return this.FindCore(prop, key);
		}

		/// <summary>Searches for the index of the item that has the specified property descriptor with the specified value, if searching is implemented in a derived class; otherwise, a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>The zero-based index of the item that matches the property descriptor and contains the specified value.</returns>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to search for.</param>
		/// <param name="key">The value of <paramref name="property" /> to match.</param>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="M:System.ComponentModel.BindingList`1.FindCore(System.ComponentModel.PropertyDescriptor,System.Object)" /> is not overridden in a derived class.</exception>
		// Token: 0x06001273 RID: 4723 RVA: 0x000074E4 File Offset: 0x000056E4
		protected virtual int FindCore(PropertyDescriptor prop, object key)
		{
			throw new NotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.AddIndex(System.ComponentModel.PropertyDescriptor)" />.</summary>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to add as a search criteria. </param>
		// Token: 0x06001274 RID: 4724 RVA: 0x000027E8 File Offset: 0x000009E8
		void IBindingList.AddIndex(PropertyDescriptor prop)
		{
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.RemoveIndex(System.ComponentModel.PropertyDescriptor)" />.</summary>
		/// <param name="prop">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> to remove from the indexes used for searching.</param>
		// Token: 0x06001275 RID: 4725 RVA: 0x000027E8 File Offset: 0x000009E8
		void IBindingList.RemoveIndex(PropertyDescriptor prop)
		{
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x0004DBBC File Offset: 0x0004BDBC
		private void HookPropertyChanged(T item)
		{
			INotifyPropertyChanged notifyPropertyChanged = item as INotifyPropertyChanged;
			if (notifyPropertyChanged != null)
			{
				if (this.propertyChangedEventHandler == null)
				{
					this.propertyChangedEventHandler = new PropertyChangedEventHandler(this.Child_PropertyChanged);
				}
				notifyPropertyChanged.PropertyChanged += this.propertyChangedEventHandler;
			}
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x0004DC00 File Offset: 0x0004BE00
		private void UnhookPropertyChanged(T item)
		{
			INotifyPropertyChanged notifyPropertyChanged = item as INotifyPropertyChanged;
			if (notifyPropertyChanged != null && this.propertyChangedEventHandler != null)
			{
				notifyPropertyChanged.PropertyChanged -= this.propertyChangedEventHandler;
			}
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x0004DC30 File Offset: 0x0004BE30
		private void Child_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (this.RaiseListChangedEvents)
			{
				if (sender == null || e == null || string.IsNullOrEmpty(e.PropertyName))
				{
					this.ResetBindings();
					return;
				}
				T t;
				try
				{
					t = (T)((object)sender);
				}
				catch (InvalidCastException)
				{
					this.ResetBindings();
					return;
				}
				int num = this.lastChangeIndex;
				if (num >= 0 && num < base.Count)
				{
					T t2 = base[num];
					if (t2.Equals(t))
					{
						goto IL_007B;
					}
				}
				num = base.IndexOf(t);
				this.lastChangeIndex = num;
				IL_007B:
				if (num == -1)
				{
					this.UnhookPropertyChanged(t);
					this.ResetBindings();
					return;
				}
				if (this.itemTypeProperties == null)
				{
					this.itemTypeProperties = TypeDescriptor.GetProperties(typeof(T));
				}
				PropertyDescriptor propertyDescriptor = this.itemTypeProperties.Find(e.PropertyName, true);
				ListChangedEventArgs listChangedEventArgs = new ListChangedEventArgs(ListChangedType.ItemChanged, num, propertyDescriptor);
				this.OnListChanged(listChangedEventArgs);
			}
		}

		/// <summary>Gets a value indicating whether item property value changes raise <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> events of type <see cref="F:System.ComponentModel.ListChangedType.ItemChanged" />. This member cannot be overridden in a derived class.</summary>
		/// <returns>true if the list type implements <see cref="T:System.ComponentModel.INotifyPropertyChanged" />, otherwise, false. The default is false.</returns>
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001279 RID: 4729 RVA: 0x0004DD1C File Offset: 0x0004BF1C
		bool IRaiseItemChangedEvents.RaisesItemChangedEvents
		{
			get
			{
				return this.raiseItemChangedEvents;
			}
		}

		// Token: 0x04001256 RID: 4694
		private int addNewPos = -1;

		// Token: 0x04001257 RID: 4695
		private bool raiseListChangedEvents = true;

		// Token: 0x04001258 RID: 4696
		private bool raiseItemChangedEvents;

		// Token: 0x04001259 RID: 4697
		[NonSerialized]
		private PropertyDescriptorCollection itemTypeProperties;

		// Token: 0x0400125A RID: 4698
		[NonSerialized]
		private PropertyChangedEventHandler propertyChangedEventHandler;

		// Token: 0x0400125B RID: 4699
		[NonSerialized]
		private AddingNewEventHandler onAddingNew;

		// Token: 0x0400125C RID: 4700
		[NonSerialized]
		private ListChangedEventHandler onListChanged;

		// Token: 0x0400125D RID: 4701
		[NonSerialized]
		private int lastChangeIndex = -1;

		// Token: 0x0400125E RID: 4702
		private bool allowNew = true;

		// Token: 0x0400125F RID: 4703
		private bool allowEdit = true;

		// Token: 0x04001260 RID: 4704
		private bool allowRemove = true;

		// Token: 0x04001261 RID: 4705
		private bool userSetAllowNew;
	}
}
