using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text.RegularExpressions;

namespace System.Windows.Forms
{
	/// <summary>Encapsulates the data source for a form.</summary>
	// Token: 0x02000062 RID: 98
	[ComplexBindingProperties("DataSource", "DataMember")]
	[Designer("System.Windows.Forms.Design.BindingSourceDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultProperty("DataSource")]
	[DefaultEvent("CurrentChanged")]
	public class BindingSource : Component, IDisposable, ICollection, IEnumerable, IList, IComponent, IBindingList, IBindingListView, ISupportInitializeNotification, ISupportInitialize, ICancelAddNew, ITypedList, ICurrencyManagerProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingSource" /> class and adds the <see cref="T:System.Windows.Forms.BindingSource" /> to the specified container.</summary>
		/// <param name="container">The <see cref="T:System.ComponentModel.IContainer" /> to add the current <see cref="T:System.Windows.Forms.BindingSource" /> to.</param>
		// Token: 0x060003F9 RID: 1017 RVA: 0x00013E40 File Offset: 0x00012040
		public BindingSource(IContainer container)
			: this()
		{
			container.Add(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingSource" /> class with the specified data source and data member.</summary>
		/// <param name="dataSource">The data source for the <see cref="T:System.Windows.Forms.BindingSource" />.</param>
		/// <param name="dataMember">The specific column or list name within the data source to bind to.</param>
		// Token: 0x060003FA RID: 1018 RVA: 0x00013E50 File Offset: 0x00012050
		public BindingSource(object dataSource, string dataMember)
		{
			this.datasource = dataSource;
			this.datamember = dataMember;
			this.raise_list_changed_events = true;
			this.ResetList();
			this.ConnectCurrencyManager();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingSource" /> class to the default property values.</summary>
		// Token: 0x060003FB RID: 1019 RVA: 0x00013E8C File Offset: 0x0001208C
		public BindingSource()
			: this(null, string.Empty)
		{
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00013E9C File Offset: 0x0001209C
		// Note: this type is marked as 'beforefieldinit'.
		static BindingSource()
		{
			BindingSource.AddingNewEvent = new object();
			BindingSource.BindingCompleteEvent = new object();
			BindingSource.CurrentChangedEvent = new object();
			BindingSource.CurrentItemChangedEvent = new object();
			BindingSource.DataErrorEvent = new object();
			BindingSource.DataMemberChangedEvent = new object();
			BindingSource.DataSourceChangedEvent = new object();
			BindingSource.ListChangedEvent = new object();
			BindingSource.PositionChangedEvent = new object();
			BindingSource.InitializedEvent = new object();
		}

		/// <summary>Occurs before an item is added to the underlying list.</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.ComponentModel.AddingNewEventArgs.NewObject" /> is not the same type as the type contained in the list.</exception>
		// Token: 0x1400003D RID: 61
		// (add) Token: 0x060003FD RID: 1021 RVA: 0x00013F10 File Offset: 0x00012110
		// (remove) Token: 0x060003FE RID: 1022 RVA: 0x00013F24 File Offset: 0x00012124
		public event AddingNewEventHandler AddingNew
		{
			add
			{
				base.Events.AddHandler(BindingSource.AddingNewEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(BindingSource.AddingNewEvent, value);
			}
		}

		/// <summary>Occurs when all the clients have been bound to this <see cref="T:System.Windows.Forms.BindingSource" />.</summary>
		// Token: 0x1400003E RID: 62
		// (add) Token: 0x060003FF RID: 1023 RVA: 0x00013F38 File Offset: 0x00012138
		// (remove) Token: 0x06000400 RID: 1024 RVA: 0x00013F4C File Offset: 0x0001214C
		public event BindingCompleteEventHandler BindingComplete
		{
			add
			{
				base.Events.AddHandler(BindingSource.BindingCompleteEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(BindingSource.BindingCompleteEvent, value);
			}
		}

		/// <summary>Occurs when the currently bound item changes.</summary>
		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06000401 RID: 1025 RVA: 0x00013F60 File Offset: 0x00012160
		// (remove) Token: 0x06000402 RID: 1026 RVA: 0x00013F74 File Offset: 0x00012174
		public event EventHandler CurrentChanged
		{
			add
			{
				base.Events.AddHandler(BindingSource.CurrentChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(BindingSource.CurrentChangedEvent, value);
			}
		}

		/// <summary>Occurs when a property value of the <see cref="P:System.Windows.Forms.BindingSource.Current" /> property has changed.</summary>
		// Token: 0x14000040 RID: 64
		// (add) Token: 0x06000403 RID: 1027 RVA: 0x00013F88 File Offset: 0x00012188
		// (remove) Token: 0x06000404 RID: 1028 RVA: 0x00013F9C File Offset: 0x0001219C
		public event EventHandler CurrentItemChanged
		{
			add
			{
				base.Events.AddHandler(BindingSource.CurrentItemChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(BindingSource.CurrentItemChangedEvent, value);
			}
		}

		/// <summary>Occurs when a currency-related exception is silently handled by the <see cref="T:System.Windows.Forms.BindingSource" />.</summary>
		// Token: 0x14000041 RID: 65
		// (add) Token: 0x06000405 RID: 1029 RVA: 0x00013FB0 File Offset: 0x000121B0
		// (remove) Token: 0x06000406 RID: 1030 RVA: 0x00013FC4 File Offset: 0x000121C4
		public event BindingManagerDataErrorEventHandler DataError
		{
			add
			{
				base.Events.AddHandler(BindingSource.DataErrorEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(BindingSource.DataErrorEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.BindingSource.DataMember" /> property value has changed.</summary>
		// Token: 0x14000042 RID: 66
		// (add) Token: 0x06000407 RID: 1031 RVA: 0x00013FD8 File Offset: 0x000121D8
		// (remove) Token: 0x06000408 RID: 1032 RVA: 0x00013FEC File Offset: 0x000121EC
		public event EventHandler DataMemberChanged
		{
			add
			{
				base.Events.AddHandler(BindingSource.DataMemberChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(BindingSource.DataMemberChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.BindingSource.DataSource" /> property value has changed.</summary>
		// Token: 0x14000043 RID: 67
		// (add) Token: 0x06000409 RID: 1033 RVA: 0x00014000 File Offset: 0x00012200
		// (remove) Token: 0x0600040A RID: 1034 RVA: 0x00014014 File Offset: 0x00012214
		public event EventHandler DataSourceChanged
		{
			add
			{
				base.Events.AddHandler(BindingSource.DataSourceChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(BindingSource.DataSourceChangedEvent, value);
			}
		}

		/// <summary>Occurs when the underlying list changes or an item in the list changes.</summary>
		// Token: 0x14000044 RID: 68
		// (add) Token: 0x0600040B RID: 1035 RVA: 0x00014028 File Offset: 0x00012228
		// (remove) Token: 0x0600040C RID: 1036 RVA: 0x0001403C File Offset: 0x0001223C
		public event ListChangedEventHandler ListChanged
		{
			add
			{
				base.Events.AddHandler(BindingSource.ListChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(BindingSource.ListChangedEvent, value);
			}
		}

		/// <summary>Occurs after the value of the <see cref="P:System.Windows.Forms.BindingSource.Position" /> property has changed.</summary>
		// Token: 0x14000045 RID: 69
		// (add) Token: 0x0600040D RID: 1037 RVA: 0x00014050 File Offset: 0x00012250
		// (remove) Token: 0x0600040E RID: 1038 RVA: 0x00014064 File Offset: 0x00012264
		public event EventHandler PositionChanged
		{
			add
			{
				base.Events.AddHandler(BindingSource.PositionChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(BindingSource.PositionChangedEvent, value);
			}
		}

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x0600040F RID: 1039 RVA: 0x00014078 File Offset: 0x00012278
		// (remove) Token: 0x06000410 RID: 1040 RVA: 0x0001408C File Offset: 0x0001228C
		event EventHandler ISupportInitializeNotification.Initialized
		{
			add
			{
				base.Events.AddHandler(BindingSource.InitializedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(BindingSource.InitializedEvent, value);
			}
		}

		/// <summary>Discards a pending new item from the collection.</summary>
		/// <param name="position">The index of the item that was added to the collection. </param>
		// Token: 0x06000411 RID: 1041 RVA: 0x000140A0 File Offset: 0x000122A0
		void ICancelAddNew.CancelNew(int position)
		{
			if (!this.add_pending)
			{
				return;
			}
			if (position != this.pending_add_index)
			{
				return;
			}
			this.add_pending = false;
			this.list.RemoveAt(position);
			if (this.raise_list_changed_events && !this.list_is_ibinding)
			{
				this.OnListChanged(new ListChangedEventArgs(2, position));
			}
		}

		/// <summary>Commits a pending new item to the collection.</summary>
		/// <param name="position">The index of the item that was added to the collection. </param>
		// Token: 0x06000412 RID: 1042 RVA: 0x000140FC File Offset: 0x000122FC
		void ICancelAddNew.EndNew(int position)
		{
			if (!this.add_pending)
			{
				return;
			}
			if (position != this.pending_add_index)
			{
				return;
			}
			this.add_pending = false;
		}

		/// <summary>Signals the <see cref="T:System.Windows.Forms.BindingSource" /> that initialization is starting.</summary>
		// Token: 0x06000413 RID: 1043 RVA: 0x0001412C File Offset: 0x0001232C
		void ISupportInitialize.BeginInit()
		{
			this.is_initialized = false;
		}

		/// <summary>Signals the <see cref="T:System.Windows.Forms.BindingSource" /> that initialization is complete. </summary>
		// Token: 0x06000414 RID: 1044 RVA: 0x00014138 File Offset: 0x00012338
		void ISupportInitialize.EndInit()
		{
			if (this.datasource != null && this.datasource is ISupportInitializeNotification)
			{
				ISupportInitializeNotification supportInitializeNotification = (ISupportInitializeNotification)this.datasource;
				if (!supportInitializeNotification.IsInitialized)
				{
					supportInitializeNotification.Initialized += new EventHandler(this.DataSourceEndInitHandler);
					return;
				}
			}
			this.is_initialized = true;
			this.ResetList();
			EventHandler eventHandler = (EventHandler)base.Events[BindingSource.InitializedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>Adds the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to the indexes used for searching.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to add to the indexes used for searching. </param>
		/// <exception cref="T:System.NotSupportedException">The underlying list is not an <see cref="T:System.ComponentModel.IBindingList" />.</exception>
		// Token: 0x06000415 RID: 1045 RVA: 0x000141C0 File Offset: 0x000123C0
		void IBindingList.AddIndex(PropertyDescriptor property)
		{
			if (!(this.list is IBindingList))
			{
				throw new NotSupportedException();
			}
			((IBindingList)this.list).AddIndex(property);
		}

		/// <summary>Removes the <see cref="T:System.ComponentModel.PropertyDescriptor" /> from the indexes used for searching.</summary>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to remove from the indexes used for searching.  </param>
		// Token: 0x06000416 RID: 1046 RVA: 0x000141EC File Offset: 0x000123EC
		void IBindingList.RemoveIndex(PropertyDescriptor prop)
		{
			if (!(this.list is IBindingList))
			{
				throw new NotSupportedException();
			}
			((IBindingList)this.list).RemoveIndex(prop);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.BindingSource" /> is initialized.</summary>
		/// <returns>true to indicate the <see cref="T:System.Windows.Forms.BindingSource" /> is initialized; otherwise, false.</returns>
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x00014218 File Offset: 0x00012418
		bool ISupportInitializeNotification.IsInitialized
		{
			get
			{
				return this.is_initialized;
			}
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00014220 File Offset: 0x00012420
		private IList GetListFromEnumerable(IEnumerable enumerable)
		{
			IEnumerator enumerator = enumerable.GetEnumerator();
			IList list;
			if (enumerable is string)
			{
				list = new BindingList<char>();
			}
			else
			{
				object obj = null;
				if (enumerator.MoveNext())
				{
					obj = enumerator.Current;
				}
				if (obj == null)
				{
					return null;
				}
				Type type = typeof(BindingList).MakeGenericType(new Type[] { obj.GetType() });
				list = (IList)Activator.CreateInstance(type);
			}
			enumerator.Reset();
			while (enumerator.MoveNext())
			{
				object obj2 = enumerator.Current;
				list.Add(obj2);
			}
			return list;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x000142B8 File Offset: 0x000124B8
		private void ConnectCurrencyManager()
		{
			this.currency_manager = new CurrencyManager(this);
			this.currency_manager.PositionChanged += delegate(object o, EventArgs args)
			{
				this.OnPositionChanged(args);
			};
			this.currency_manager.CurrentChanged += delegate(object o, EventArgs args)
			{
				this.OnCurrentChanged(args);
			};
			this.currency_manager.BindingComplete += delegate(object o, BindingCompleteEventArgs args)
			{
				this.OnBindingComplete(args);
			};
			this.currency_manager.DataError += delegate(object o, BindingManagerDataErrorEventArgs args)
			{
				this.OnDataError(args);
			};
			this.currency_manager.CurrentChanged += delegate(object o, EventArgs args)
			{
				this.OnCurrentChanged(args);
			};
			this.currency_manager.CurrentItemChanged += delegate(object o, EventArgs args)
			{
				this.OnCurrentItemChanged(args);
			};
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0001435C File Offset: 0x0001255C
		private void ResetList()
		{
			if (!this.is_initialized)
			{
				return;
			}
			object obj = ListBindingHelper.GetList(this.datasource, this.datamember);
			IList list;
			if (this.datasource == null)
			{
				list = new BindingList<object>();
			}
			else if (obj == null)
			{
				Type propertyType = ListBindingHelper.GetListItemProperties(this.datasource)[this.datamember].PropertyType;
				Type type = typeof(BindingList).MakeGenericType(new Type[] { propertyType });
				list = (IList)Activator.CreateInstance(type);
			}
			else if (obj is IList)
			{
				list = (IList)obj;
			}
			else if (obj is IEnumerable)
			{
				IList listFromEnumerable = this.GetListFromEnumerable((IEnumerable)obj);
				IList list3;
				if (listFromEnumerable == null)
				{
					IList list2 = this.list;
					list3 = list2;
				}
				else
				{
					list3 = listFromEnumerable;
				}
				list = list3;
			}
			else if (obj is Type)
			{
				Type type2 = typeof(BindingList).MakeGenericType(new Type[] { (Type)obj });
				list = (IList)Activator.CreateInstance(type2);
			}
			else
			{
				Type type3 = typeof(BindingList).MakeGenericType(new Type[] { obj.GetType() });
				list = (IList)Activator.CreateInstance(type3);
				list.Add(obj);
			}
			this.SetList(list);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x000144B4 File Offset: 0x000126B4
		private void SetList(IList l)
		{
			if (this.list is IBindingList)
			{
				((IBindingList)this.list).ListChanged -= new ListChangedEventHandler(this.IBindingListChangedHandler);
			}
			this.list = l;
			this.item_type = ListBindingHelper.GetListItemType(this.list);
			this.item_has_default_ctor = this.item_type.GetConstructor(Type.EmptyTypes) != null;
			this.list_is_ibinding = this.list is IBindingList;
			if (this.list_is_ibinding)
			{
				((IBindingList)this.list).ListChanged += new ListChangedEventHandler(this.IBindingListChangedHandler);
				if (this.list is IBindingListView)
				{
					((IBindingListView)this.list).Filter = this.filter;
				}
			}
			this.ResetBindings(true);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0001458C File Offset: 0x0001278C
		private void ConnectDataSourceEvents(object dataSource)
		{
			if (dataSource == null)
			{
				return;
			}
			ICurrencyManagerProvider currencyManagerProvider = dataSource as ICurrencyManagerProvider;
			if (currencyManagerProvider != null && currencyManagerProvider.CurrencyManager != null)
			{
				currencyManagerProvider.CurrencyManager.CurrentItemChanged += new EventHandler(this.OnParentCurrencyManagerChanged);
				currencyManagerProvider.CurrencyManager.MetaDataChanged += new EventHandler(this.OnParentCurrencyManagerChanged);
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x000145E8 File Offset: 0x000127E8
		private void OnParentCurrencyManagerChanged(object sender, EventArgs args)
		{
			this.ResetList();
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x000145F0 File Offset: 0x000127F0
		private void DisconnectDataSourceEvents(object dataSource)
		{
			if (dataSource == null)
			{
				return;
			}
			ICurrencyManagerProvider currencyManagerProvider = dataSource as ICurrencyManagerProvider;
			if (currencyManagerProvider != null && currencyManagerProvider.CurrencyManager != null)
			{
				currencyManagerProvider.CurrencyManager.CurrentItemChanged -= new EventHandler(this.OnParentCurrencyManagerChanged);
				currencyManagerProvider.CurrencyManager.MetaDataChanged -= new EventHandler(this.OnParentCurrencyManagerChanged);
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0001464C File Offset: 0x0001284C
		private void IBindingListChangedHandler(object o, ListChangedEventArgs args)
		{
			if (this.raise_list_changed_events)
			{
				this.OnListChanged(args);
			}
		}

		/// <summary>Gets a value indicating whether items in the underlying list can be edited.</summary>
		/// <returns>true to indicate list items can be edited; otherwise, false.</returns>
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x00014660 File Offset: 0x00012860
		[Browsable(false)]
		public virtual bool AllowEdit
		{
			get
			{
				return this.list != null && !this.list.IsReadOnly && (!(this.list is IBindingList) || ((IBindingList)this.list).AllowEdit);
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="M:System.Windows.Forms.BindingSource.AddNew" /> method can be used to add items to the list.</summary>
		/// <returns>true if <see cref="M:System.Windows.Forms.BindingSource.AddNew" /> can be used to add items to the list; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">This property is set to true when the underlying list represented by the <see cref="P:System.Windows.Forms.BindingSource.List" /> property has a fixed size or is read-only.</exception>
		/// <exception cref="T:System.MissingMethodException">The property is set to true and the <see cref="E:System.Windows.Forms.BindingSource.AddingNew" /> event is not handled when the underlying list type does not have a default constructor.</exception>
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x000146B0 File Offset: 0x000128B0
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x00014720 File Offset: 0x00012920
		public virtual bool AllowNew
		{
			get
			{
				if (this.allow_new_set)
				{
					return this.allow_new;
				}
				if (this.list is IBindingList)
				{
					return ((IBindingList)this.list).AllowNew;
				}
				return !this.list.IsFixedSize && !this.list.IsReadOnly && this.item_has_default_ctor;
			}
			set
			{
				if (value == this.allow_new && this.allow_new_set)
				{
					return;
				}
				if (value && (this.list.IsReadOnly || this.list.IsFixedSize))
				{
					throw new InvalidOperationException();
				}
				this.allow_new_set = true;
				this.allow_new = value;
				if (this.raise_list_changed_events)
				{
					this.OnListChanged(new ListChangedEventArgs(0, -1));
				}
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x00014798 File Offset: 0x00012998
		private bool IsAddingNewHandled
		{
			get
			{
				return base.Events[BindingSource.AddingNewEvent] != null;
			}
		}

		/// <summary>Gets a value indicating whether items can be removed from the underlying list.</summary>
		/// <returns>true to indicate list items can be removed from the list; otherwise, false.</returns>
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x000147B0 File Offset: 0x000129B0
		[Browsable(false)]
		public virtual bool AllowRemove
		{
			get
			{
				return this.list != null && !this.list.IsFixedSize && !this.list.IsReadOnly && (!(this.list is IBindingList) || ((IBindingList)this.list).AllowRemove);
			}
		}

		/// <summary>Gets the total number of items in the underlying list, taking the current <see cref="P:System.Windows.Forms.BindingSource.Filter" /> value into consideration.</summary>
		/// <returns>The total number of filtered items in the underlying list.</returns>
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x00014810 File Offset: 0x00012A10
		[Browsable(false)]
		public virtual int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets the currency manager associated with this <see cref="T:System.Windows.Forms.BindingSource" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.CurrencyManager" /> associated with this <see cref="T:System.Windows.Forms.BindingSource" />.</returns>
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x00014820 File Offset: 0x00012A20
		[Browsable(false)]
		public virtual CurrencyManager CurrencyManager
		{
			get
			{
				return this.currency_manager;
			}
		}

		/// <summary>Gets the current item in the list.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the current item in the underlying list represented by the <see cref="P:System.Windows.Forms.BindingSource.List" /> property, or null if the list has no items.</returns>
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x00014828 File Offset: 0x00012A28
		[Browsable(false)]
		public object Current
		{
			get
			{
				if (this.currency_manager.Count > 0)
				{
					return this.currency_manager.Current;
				}
				return null;
			}
		}

		/// <summary>Gets or sets the specific list in the data source to which the connector currently binds to.</summary>
		/// <returns>The name of a list (or row) in the <see cref="P:System.Windows.Forms.BindingSource.DataSource" />. The default is an empty string ("").</returns>
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x00014848 File Offset: 0x00012A48
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x00014850 File Offset: 0x00012A50
		[Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[RefreshProperties(2)]
		[DefaultValue("")]
		public string DataMember
		{
			get
			{
				return this.datamember;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (this.datamember != value)
				{
					this.datamember = value;
					this.ResetList();
					this.OnDataMemberChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the data source that the connector binds to.</summary>
		/// <returns>An <see cref="T:System.Object" /> that acts as a data source. The default is null.</returns>
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x00014894 File Offset: 0x00012A94
		// (set) Token: 0x0600042B RID: 1067 RVA: 0x0001489C File Offset: 0x00012A9C
		[DefaultValue(null)]
		[AttributeProvider(typeof(IListSource))]
		[RefreshProperties(2)]
		public object DataSource
		{
			get
			{
				return this.datasource;
			}
			set
			{
				if (this.datasource != value)
				{
					if (this.datasource == null)
					{
						this.datamember = string.Empty;
					}
					this.DisconnectDataSourceEvents(this.datasource);
					this.datasource = value;
					this.ConnectDataSourceEvents(this.datasource);
					this.ResetList();
					this.OnDataSourceChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the expression used to filter which rows are viewed.</summary>
		/// <returns>A string that specifies how rows are to be filtered. The default is null.</returns>
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x000148FC File Offset: 0x00012AFC
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x00014904 File Offset: 0x00012B04
		[DefaultValue(null)]
		public virtual string Filter
		{
			get
			{
				return this.filter;
			}
			set
			{
				if (this.SupportsFiltering)
				{
					((IBindingListView)this.list).Filter = value;
				}
				this.filter = value;
			}
		}

		/// <summary>Gets a value indicating whether the list binding is suspended.</summary>
		/// <returns>true to indicate the binding is suspended; otherwise, false. </returns>
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x0001492C File Offset: 0x00012B2C
		[Browsable(false)]
		public bool IsBindingSuspended
		{
			get
			{
				return this.currency_manager.IsBindingSuspended;
			}
		}

		/// <summary>Gets a value indicating whether the underlying list has a fixed size.</summary>
		/// <returns>true if the underlying list has a fixed size; otherwise, false.</returns>
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0001493C File Offset: 0x00012B3C
		[Browsable(false)]
		public virtual bool IsFixedSize
		{
			get
			{
				return this.list.IsFixedSize;
			}
		}

		/// <summary>Gets a value indicating whether the underlying list is read-only.</summary>
		/// <returns>true if the list is read-only; otherwise, false.</returns>
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x0001494C File Offset: 0x00012B4C
		[Browsable(false)]
		public virtual bool IsReadOnly
		{
			get
			{
				return this.list.IsReadOnly;
			}
		}

		/// <summary>Gets a value indicating whether the items in the underlying list are sorted. </summary>
		/// <returns>true if the list is an <see cref="T:System.ComponentModel.IBindingList" /> and is sorted; otherwise, false. </returns>
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0001495C File Offset: 0x00012B5C
		[Browsable(false)]
		public virtual bool IsSorted
		{
			get
			{
				return this.list is IBindingList && ((IBindingList)this.list).IsSorted;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>true to indicate the list is synchronized; otherwise, false.</returns>
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x00014984 File Offset: 0x00012B84
		[Browsable(false)]
		public virtual bool IsSynchronized
		{
			get
			{
				return this.list.IsSynchronized;
			}
		}

		/// <summary>Gets or sets the list element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to retrieve.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or is equal to or greater than <see cref="P:System.Windows.Forms.BindingSource.Count" />.</exception>
		// Token: 0x170000F3 RID: 243
		[Browsable(false)]
		public virtual object this[int index]
		{
			get
			{
				return this.list[index];
			}
			set
			{
				this.list[index] = value;
			}
		}

		/// <summary>Gets the list that the connector is bound to.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> that represents the list, or null if there is no underlying list associated with this <see cref="T:System.Windows.Forms.BindingSource" />.</returns>
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x000149B4 File Offset: 0x00012BB4
		[Browsable(false)]
		public IList List
		{
			get
			{
				return this.list;
			}
		}

		/// <summary>Gets or sets the index of the current item in the underlying list.</summary>
		/// <returns>A zero-based index that specifies the position of the current item in the underlying list.</returns>
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x000149BC File Offset: 0x00012BBC
		// (set) Token: 0x06000437 RID: 1079 RVA: 0x000149CC File Offset: 0x00012BCC
		[Browsable(false)]
		[DefaultValue(-1)]
		public int Position
		{
			get
			{
				return this.currency_manager.Position;
			}
			set
			{
				if (value >= this.Count)
				{
					value = this.Count - 1;
				}
				if (value < 0)
				{
					value = 0;
				}
				this.currency_manager.Position = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether <see cref="E:System.Windows.Forms.BindingSource.ListChanged" /> events should be raised.</summary>
		/// <returns>true if <see cref="E:System.Windows.Forms.BindingSource.ListChanged" /> events should be raised; otherwise, false. The default is true.</returns>
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x00014A08 File Offset: 0x00012C08
		// (set) Token: 0x06000439 RID: 1081 RVA: 0x00014A10 File Offset: 0x00012C10
		[DefaultValue(true)]
		[Browsable(false)]
		public bool RaiseListChangedEvents
		{
			get
			{
				return this.raise_list_changed_events;
			}
			set
			{
				this.raise_list_changed_events = value;
			}
		}

		/// <summary>Gets or sets the column names used for sorting, and the sort order for viewing the rows in the data source.</summary>
		/// <returns>A case-sensitive string containing the column name followed by "ASC" (for ascending) or "DESC" (for descending). The default is null.</returns>
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x00014A1C File Offset: 0x00012C1C
		// (set) Token: 0x0600043B RID: 1083 RVA: 0x00014A24 File Offset: 0x00012C24
		[DefaultValue(null)]
		public string Sort
		{
			get
			{
				return this.sort;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					if (this.list_is_ibinding && this.SupportsSorting)
					{
						this.RemoveSort();
					}
					this.sort = value;
					return;
				}
				if (!this.list_is_ibinding || !this.SupportsSorting)
				{
					throw new ArgumentException("value");
				}
				this.ProcessSortString(value);
				this.sort = value;
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00014A98 File Offset: 0x00012C98
		private void ProcessSortString(string sort)
		{
			sort = Regex.Replace(sort, "( )+", " ");
			string[] array = sort.Split(new char[] { ',' });
			PropertyDescriptorCollection itemProperties = this.GetItemProperties(null);
			if (array.Length == 1)
			{
				ListSortDescription listSortDescription = this.GetListSortDescription(itemProperties, array[0]);
				this.ApplySort(listSortDescription.PropertyDescriptor, listSortDescription.SortDirection);
			}
			else
			{
				if (!this.SupportsAdvancedSorting)
				{
					throw new ArgumentException("value");
				}
				ListSortDescription[] array2 = new ListSortDescription[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array2[i] = this.GetListSortDescription(itemProperties, array[i]);
				}
				this.ApplySort(new ListSortDescriptionCollection(array2));
			}
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00014B50 File Offset: 0x00012D50
		private ListSortDescription GetListSortDescription(PropertyDescriptorCollection prop_descs, string property)
		{
			property = property.Trim();
			string[] array = property.Split(new char[] { ' ' }, 2);
			string text = array[0];
			PropertyDescriptor propertyDescriptor = prop_descs[text];
			if (propertyDescriptor == null)
			{
				throw new ArgumentException("value");
			}
			ListSortDirection listSortDirection = 0;
			if (array.Length > 1)
			{
				string text2 = array[1];
				if (string.Compare(text2, "ASC", true) == 0)
				{
					listSortDirection = 0;
				}
				else
				{
					if (string.Compare(text2, "DESC", true) != 0)
					{
						throw new ArgumentException("value");
					}
					listSortDirection = 1;
				}
			}
			return new ListSortDescription(propertyDescriptor, listSortDirection);
		}

		/// <summary>Gets the collection of sort descriptions applied to the data source.</summary>
		/// <returns>If the data source is an <see cref="T:System.ComponentModel.IBindingListView" />, a <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> that contains the sort descriptions applied to the list; otherwise, null.</returns>
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x00014BEC File Offset: 0x00012DEC
		[EditorBrowsable(1)]
		[Browsable(false)]
		public virtual ListSortDescriptionCollection SortDescriptions
		{
			get
			{
				if (this.list is IBindingListView)
				{
					return ((IBindingListView)this.list).SortDescriptions;
				}
				return null;
			}
		}

		/// <summary>Gets the direction the items in the list are sorted.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values indicating the direction the list is sorted.</returns>
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x00014C1C File Offset: 0x00012E1C
		[Browsable(false)]
		[EditorBrowsable(1)]
		public virtual ListSortDirection SortDirection
		{
			get
			{
				if (this.list is IBindingList)
				{
					return ((IBindingList)this.list).SortDirection;
				}
				return 0;
			}
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> that is being used for sorting the list.</summary>
		/// <returns>If the list is an <see cref="T:System.ComponentModel.IBindingList" />, the <see cref="T:System.ComponentModel.PropertyDescriptor" /> that is being used for sorting; otherwise, null.</returns>
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x00014C4C File Offset: 0x00012E4C
		[EditorBrowsable(1)]
		[Browsable(false)]
		public virtual PropertyDescriptor SortProperty
		{
			get
			{
				if (this.list is IBindingList)
				{
					return ((IBindingList)this.list).SortProperty;
				}
				return null;
			}
		}

		/// <summary>Gets a value indicating whether the data source supports multi-column sorting.</summary>
		/// <returns>true if the list is an <see cref="T:System.ComponentModel.IBindingListView" /> and supports multi-column sorting; otherwise, false. </returns>
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x00014C7C File Offset: 0x00012E7C
		[Browsable(false)]
		public virtual bool SupportsAdvancedSorting
		{
			get
			{
				return this.list is IBindingListView && ((IBindingListView)this.list).SupportsAdvancedSorting;
			}
		}

		/// <summary>Gets a value indicating whether the data source supports change notification.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x00014CA4 File Offset: 0x00012EA4
		[Browsable(false)]
		public virtual bool SupportsChangeNotification
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether the data source supports filtering.</summary>
		/// <returns>true if the list is an <see cref="T:System.ComponentModel.IBindingListView" /> and supports filtering; otherwise, false.</returns>
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x00014CA8 File Offset: 0x00012EA8
		[Browsable(false)]
		public virtual bool SupportsFiltering
		{
			get
			{
				return this.list is IBindingListView && ((IBindingListView)this.list).SupportsFiltering;
			}
		}

		/// <summary>Gets a value indicating whether the data source supports searching with the <see cref="M:System.Windows.Forms.BindingSource.Find(System.ComponentModel.PropertyDescriptor,System.Object)" /> method.</summary>
		/// <returns>true if the list is a <see cref="T:System.ComponentModel.IBindingList" /> and supports the searching with the <see cref="Overload:System.Windows.Forms.BindingSource.Find" /> method; otherwise, false.</returns>
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x00014CD0 File Offset: 0x00012ED0
		[Browsable(false)]
		public virtual bool SupportsSearching
		{
			get
			{
				return this.list is IBindingList && ((IBindingList)this.list).SupportsSearching;
			}
		}

		/// <summary>Gets a value indicating whether the data source supports sorting.</summary>
		/// <returns>true if the data source is an <see cref="T:System.ComponentModel.IBindingList" /> and supports sorting; otherwise, false.</returns>
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x00014CF8 File Offset: 0x00012EF8
		[Browsable(false)]
		public virtual bool SupportsSorting
		{
			get
			{
				return this.list is IBindingList && ((IBindingList)this.list).SupportsSorting;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the underlying list.</summary>
		/// <returns>An object that can be used to synchronize access to the underlying list.</returns>
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x00014D20 File Offset: 0x00012F20
		[Browsable(false)]
		public virtual object SyncRoot
		{
			get
			{
				return this.list.SyncRoot;
			}
		}

		/// <summary>Adds an existing item to the internal list.</summary>
		/// <returns>The zero-based index at which <paramref name="value" /> was added to the underlying list represented by the <see cref="P:System.Windows.Forms.BindingSource.List" /> property. </returns>
		/// <param name="value">An <see cref="T:System.Object" /> to be added to the internal list.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="value" /> differs in type from the existing items in the underlying list.</exception>
		// Token: 0x06000447 RID: 1095 RVA: 0x00014D30 File Offset: 0x00012F30
		public virtual int Add(object value)
		{
			if (this.datasource == null && this.list.Count == 0 && value != null)
			{
				Type type = typeof(BindingList).MakeGenericType(new Type[] { value.GetType() });
				IList list = (IList)Activator.CreateInstance(type);
				this.SetList(list);
			}
			if (value != null && !this.item_type.IsAssignableFrom(value.GetType()))
			{
				throw new InvalidOperationException("Objects added to the list must all be of the same type.");
			}
			if (this.list.IsReadOnly)
			{
				throw new NotSupportedException("Collection is read-only.");
			}
			if (this.list.IsFixedSize)
			{
				throw new NotSupportedException("Collection has a fixed size.");
			}
			int num = this.list.Add(value);
			if (this.raise_list_changed_events && !this.list_is_ibinding)
			{
				this.OnListChanged(new ListChangedEventArgs(1, num));
			}
			return num;
		}

		/// <summary>Adds a new item to the underlying list.</summary>
		/// <returns>The <see cref="T:System.Object" /> that was created and added to the list.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.BindingSource.AllowNew" /> property is set to false. -or-A public default constructor could not be found for the current item type.</exception>
		// Token: 0x06000448 RID: 1096 RVA: 0x00014E20 File Offset: 0x00013020
		public virtual object AddNew()
		{
			if (!this.AllowEdit)
			{
				throw new InvalidOperationException("Item cannot be added to a read-only or fixed-size list.");
			}
			if (!this.AllowNew)
			{
				throw new InvalidOperationException("AddNew is set to false.");
			}
			this.EndEdit();
			AddingNewEventArgs addingNewEventArgs = new AddingNewEventArgs();
			this.OnAddingNew(addingNewEventArgs);
			object obj = addingNewEventArgs.NewObject;
			if (obj != null)
			{
				if (!this.item_type.IsAssignableFrom(obj.GetType()))
				{
					throw new InvalidOperationException("Objects added to the list must all be of the same type.");
				}
			}
			else
			{
				if (this.list is IBindingList)
				{
					object obj2 = ((IBindingList)this.list).AddNew();
					this.add_pending = true;
					this.pending_add_index = this.list.IndexOf(obj2);
					return obj2;
				}
				if (!this.item_has_default_ctor)
				{
					throw new InvalidOperationException("AddNew cannot be called on '" + this.item_type.Name + ", since it does not have a public default ctor. Set AllowNew to true , handling AddingNew and creating the appropriate object.");
				}
				obj = Activator.CreateInstance(this.item_type);
			}
			int num = this.list.Add(obj);
			if (this.raise_list_changed_events && !this.list_is_ibinding)
			{
				this.OnListChanged(new ListChangedEventArgs(1, num));
			}
			this.add_pending = true;
			this.pending_add_index = num;
			return obj;
		}

		/// <summary>Sorts the data source using the specified property descriptor and sort direction.</summary>
		/// <param name="property">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that describes the property by which to sort the data source.</param>
		/// <param name="sort">A <see cref="T:System.ComponentModel.ListSortDirection" /> indicating how the list should be sorted.</param>
		/// <exception cref="T:System.NotSupportedException">The data source is not an <see cref="T:System.ComponentModel.IBindingList" />.</exception>
		// Token: 0x06000449 RID: 1097 RVA: 0x00014F54 File Offset: 0x00013154
		[EditorBrowsable(1)]
		public virtual void ApplySort(PropertyDescriptor property, ListSortDirection sort)
		{
			if (!this.list_is_ibinding)
			{
				throw new NotSupportedException("This operation requires an IBindingList.");
			}
			IBindingList bindingList = (IBindingList)this.list;
			bindingList.ApplySort(property, sort);
		}

		/// <summary>Sorts the data source with the specified sort descriptions.</summary>
		/// <param name="sorts">A <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> containing the sort descriptions to apply to the data source.</param>
		/// <exception cref="T:System.NotSupportedException">The data source is not an <see cref="T:System.ComponentModel.IBindingListView" />.</exception>
		// Token: 0x0600044A RID: 1098 RVA: 0x00014F8C File Offset: 0x0001318C
		[EditorBrowsable(1)]
		public virtual void ApplySort(ListSortDescriptionCollection sorts)
		{
			if (!(this.list is IBindingListView))
			{
				throw new NotSupportedException("This operation requires an IBindingListView.");
			}
			IBindingListView bindingListView = (IBindingListView)this.list;
			bindingListView.ApplySort(sorts);
		}

		/// <summary>Cancels the current edit operation.</summary>
		// Token: 0x0600044B RID: 1099 RVA: 0x00014FC8 File Offset: 0x000131C8
		public void CancelEdit()
		{
			this.currency_manager.CancelCurrentEdit();
		}

		/// <summary>Removes all elements from the list.</summary>
		// Token: 0x0600044C RID: 1100 RVA: 0x00014FD8 File Offset: 0x000131D8
		public virtual void Clear()
		{
			if (this.list.IsReadOnly)
			{
				throw new NotSupportedException("Collection is read-only.");
			}
			this.list.Clear();
			if (this.raise_list_changed_events && !this.list_is_ibinding)
			{
				this.OnListChanged(new ListChangedEventArgs(0, -1));
			}
		}

		/// <summary>Determines whether an object is an item in the list.</summary>
		/// <returns>true if the <paramref name="value" /> parameter is found in the <see cref="P:System.Windows.Forms.BindingSource.List" />; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the underlying list represented by the <see cref="P:System.Windows.Forms.BindingSource.List" /> property. The value can be null. </param>
		// Token: 0x0600044D RID: 1101 RVA: 0x00015030 File Offset: 0x00013230
		public virtual bool Contains(object value)
		{
			return this.list.Contains(value);
		}

		/// <summary>Copies the contents of the <see cref="P:System.Windows.Forms.BindingSource.List" /> to the specified array, starting at the specified index value.</summary>
		/// <param name="arr">The destination array.</param>
		/// <param name="index">The index in the destination array at which to start the copy operation.</param>
		// Token: 0x0600044E RID: 1102 RVA: 0x00015040 File Offset: 0x00013240
		public virtual void CopyTo(Array arr, int index)
		{
			this.list.CopyTo(arr, index);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.BindingSource" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x0600044F RID: 1103 RVA: 0x00015050 File Offset: 0x00013250
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		/// <summary>Applies pending changes to the underlying data source.</summary>
		// Token: 0x06000450 RID: 1104 RVA: 0x0001505C File Offset: 0x0001325C
		public void EndEdit()
		{
			this.currency_manager.EndCurrentEdit();
		}

		/// <summary>Returns the index of the item in the list with the specified property name and value.</summary>
		/// <returns>The zero-based index of the item with the specified property name and value. </returns>
		/// <param name="propertyName">The name of the property to search for.</param>
		/// <param name="key">The value of the item with the specified <paramref name="propertyName" /> to find.</param>
		/// <exception cref="T:System.InvalidOperationException">The underlying list is not a <see cref="T:System.ComponentModel.IBindingList" /> with searching functionality implemented.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="propertyName" /> does not match a property in the list.</exception>
		// Token: 0x06000451 RID: 1105 RVA: 0x0001506C File Offset: 0x0001326C
		public int Find(string propertyName, object key)
		{
			PropertyDescriptor propertyDescriptor = this.GetItemProperties(null).Find(propertyName, true);
			if (propertyDescriptor == null)
			{
				throw new ArgumentException("propertyName");
			}
			return this.Find(propertyDescriptor, key);
		}

		/// <summary>Searches for the index of the item that has the given property descriptor.</summary>
		/// <returns>The zero-based index of the item that has the given value for <see cref="T:System.ComponentModel.PropertyDescriptor" />.</returns>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to search for. </param>
		/// <param name="key">The value of <paramref name="prop" /> to match. </param>
		/// <exception cref="T:System.NotSupportedException">The underlying list is not of type <see cref="T:System.ComponentModel.IBindingList" />.</exception>
		// Token: 0x06000452 RID: 1106 RVA: 0x000150A4 File Offset: 0x000132A4
		public virtual int Find(PropertyDescriptor prop, object key)
		{
			if (!this.list_is_ibinding)
			{
				throw new NotSupportedException();
			}
			return ((IBindingList)this.list).Find(prop, key);
		}

		/// <summary>Retrieves an enumerator for the <see cref="P:System.Windows.Forms.BindingSource.List" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="P:System.Windows.Forms.BindingSource.List" />. </returns>
		// Token: 0x06000453 RID: 1107 RVA: 0x000150CC File Offset: 0x000132CC
		public virtual IEnumerator GetEnumerator()
		{
			return this.List.GetEnumerator();
		}

		/// <summary>Retrieves an array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects representing the bindable properties of the data source list type.</summary>
		/// <returns>An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects that represents the properties on this list type used to bind data.</returns>
		/// <param name="listAccessors">An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects to find in the list as bindable.</param>
		// Token: 0x06000454 RID: 1108 RVA: 0x000150DC File Offset: 0x000132DC
		public virtual PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			return ListBindingHelper.GetListItemProperties(this.list, listAccessors);
		}

		/// <summary>Gets the name of the list supplying data for the binding.</summary>
		/// <returns>The name of the list supplying the data for binding.</returns>
		/// <param name="listAccessors">An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects to find in the list as bindable.</param>
		// Token: 0x06000455 RID: 1109 RVA: 0x000150EC File Offset: 0x000132EC
		public virtual string GetListName(PropertyDescriptor[] listAccessors)
		{
			return ListBindingHelper.GetListName(this.list, listAccessors);
		}

		/// <summary>Gets the related currency manager for the specified data member.</summary>
		/// <returns>The related <see cref="T:System.Windows.Forms.CurrencyManager" /> for the specified data member.</returns>
		/// <param name="dataMember">The name of column or list, within the data source to retrieve the currency manager for.</param>
		// Token: 0x06000456 RID: 1110 RVA: 0x000150FC File Offset: 0x000132FC
		public virtual CurrencyManager GetRelatedCurrencyManager(string dataMember)
		{
			if (dataMember == null || dataMember.Length == 0)
			{
				return this.currency_manager;
			}
			if (this.related_currency_managers.ContainsKey(dataMember))
			{
				return this.related_currency_managers[dataMember];
			}
			if (dataMember.IndexOf('.') != -1)
			{
				return null;
			}
			BindingSource bindingSource = new BindingSource(this, dataMember);
			this.related_currency_managers[dataMember] = bindingSource.CurrencyManager;
			return bindingSource.CurrencyManager;
		}

		/// <summary>Searches for the specified object and returns the index of the first occurrence within the entire list.</summary>
		/// <returns>The zero-based index of the first occurrence of the <paramref name="value" /> parameter; otherwise, -1 if <paramref name="value" /> is not in the list.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the underlying list represented by the <see cref="P:System.Windows.Forms.BindingSource.List" /> property. The value can be null. </param>
		// Token: 0x06000457 RID: 1111 RVA: 0x00015170 File Offset: 0x00013370
		public virtual int IndexOf(object value)
		{
			return this.list.IndexOf(value);
		}

		/// <summary>Inserts an item into the list at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert. The value can be null. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than <see cref="P:System.Windows.Forms.BindingSource.Count" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The list is read-only or has a fixed size.</exception>
		// Token: 0x06000458 RID: 1112 RVA: 0x00015180 File Offset: 0x00013380
		public virtual void Insert(int index, object value)
		{
			if (index < 0 || index > this.list.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (this.list.IsReadOnly || this.list.IsFixedSize)
			{
				throw new NotSupportedException();
			}
			if (!this.item_type.IsAssignableFrom(value.GetType()))
			{
				throw new ArgumentException("value");
			}
			this.list.Insert(index, value);
			if (this.raise_list_changed_events && !this.list_is_ibinding)
			{
				this.OnListChanged(new ListChangedEventArgs(1, index));
			}
		}

		/// <summary>Moves to the first item in the list.</summary>
		// Token: 0x06000459 RID: 1113 RVA: 0x00015228 File Offset: 0x00013428
		public void MoveFirst()
		{
			this.Position = 0;
		}

		/// <summary>Moves to the last item in the list.</summary>
		// Token: 0x0600045A RID: 1114 RVA: 0x00015234 File Offset: 0x00013434
		public void MoveLast()
		{
			this.Position = this.Count - 1;
		}

		/// <summary>Moves to the next item in the list.</summary>
		// Token: 0x0600045B RID: 1115 RVA: 0x00015244 File Offset: 0x00013444
		public void MoveNext()
		{
			this.Position++;
		}

		/// <summary>Moves to the previous item in the list.</summary>
		// Token: 0x0600045C RID: 1116 RVA: 0x00015254 File Offset: 0x00013454
		public void MovePrevious()
		{
			this.Position--;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.AddingNew" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600045D RID: 1117 RVA: 0x00015264 File Offset: 0x00013464
		protected virtual void OnAddingNew(AddingNewEventArgs e)
		{
			AddingNewEventHandler addingNewEventHandler = (AddingNewEventHandler)base.Events[BindingSource.AddingNewEvent];
			if (addingNewEventHandler != null)
			{
				addingNewEventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.BindingComplete" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.BindingCompleteEventArgs" />  that contains the event data. </param>
		// Token: 0x0600045E RID: 1118 RVA: 0x00015298 File Offset: 0x00013498
		protected virtual void OnBindingComplete(BindingCompleteEventArgs e)
		{
			BindingCompleteEventHandler bindingCompleteEventHandler = (BindingCompleteEventHandler)base.Events[BindingSource.BindingCompleteEvent];
			if (bindingCompleteEventHandler != null)
			{
				bindingCompleteEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.CurrentChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600045F RID: 1119 RVA: 0x000152CC File Offset: 0x000134CC
		protected virtual void OnCurrentChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[BindingSource.CurrentChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.CurrentItemChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000460 RID: 1120 RVA: 0x00015300 File Offset: 0x00013500
		protected virtual void OnCurrentItemChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[BindingSource.CurrentItemChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.DataError" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.BindingManagerDataErrorEventArgs" /> that contains the event data. </param>
		// Token: 0x06000461 RID: 1121 RVA: 0x00015334 File Offset: 0x00013534
		protected virtual void OnDataError(BindingManagerDataErrorEventArgs e)
		{
			BindingManagerDataErrorEventHandler bindingManagerDataErrorEventHandler = (BindingManagerDataErrorEventHandler)base.Events[BindingSource.DataErrorEvent];
			if (bindingManagerDataErrorEventHandler != null)
			{
				bindingManagerDataErrorEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.DataMemberChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000462 RID: 1122 RVA: 0x00015368 File Offset: 0x00013568
		protected virtual void OnDataMemberChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[BindingSource.DataMemberChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.DataSourceChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000463 RID: 1123 RVA: 0x0001539C File Offset: 0x0001359C
		protected virtual void OnDataSourceChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[BindingSource.DataSourceChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.ListChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000464 RID: 1124 RVA: 0x000153D0 File Offset: 0x000135D0
		protected virtual void OnListChanged(ListChangedEventArgs e)
		{
			ListChangedEventHandler listChangedEventHandler = (ListChangedEventHandler)base.Events[BindingSource.ListChangedEvent];
			if (listChangedEventHandler != null)
			{
				listChangedEventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.PositionChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.ListChangedEventArgs" /> that contains the event data.</param>
		// Token: 0x06000465 RID: 1125 RVA: 0x00015404 File Offset: 0x00013604
		protected virtual void OnPositionChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[BindingSource.PositionChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Removes the specified item from the list.</summary>
		/// <param name="value">The item to remove from the underlying list represented by the <see cref="P:System.Windows.Forms.BindingSource.List" /> property.</param>
		/// <exception cref="T:System.NotSupportedException">The underlying list has a fixed size or is read-only. </exception>
		// Token: 0x06000466 RID: 1126 RVA: 0x00015438 File Offset: 0x00013638
		public virtual void Remove(object value)
		{
			if (this.list.IsReadOnly)
			{
				throw new NotSupportedException("Collection is read-only.");
			}
			if (this.list.IsFixedSize)
			{
				throw new NotSupportedException("Collection has a fixed size.");
			}
			int num = ((!this.list_is_ibinding) ? this.list.IndexOf(value) : (-1));
			this.list.Remove(value);
			if (num != -1 && this.raise_list_changed_events)
			{
				this.OnListChanged(new ListChangedEventArgs(2, num));
			}
		}

		/// <summary>Removes the item at the specified index in the list.</summary>
		/// <param name="index">The zero-based index of the item to remove. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the value of the <see cref="P:System.Windows.Forms.BindingSource.Count" /> property.</exception>
		/// <exception cref="T:System.NotSupportedException">The underlying list represented by the <see cref="P:System.Windows.Forms.BindingSource.List" /> property is read-only or has a fixed size.</exception>
		// Token: 0x06000467 RID: 1127 RVA: 0x000154C4 File Offset: 0x000136C4
		public virtual void RemoveAt(int index)
		{
			if (index < 0 || index > this.list.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (this.list.IsReadOnly || this.list.IsFixedSize)
			{
				throw new InvalidOperationException();
			}
			this.list.RemoveAt(index);
			if (this.raise_list_changed_events && !this.list_is_ibinding)
			{
				this.OnListChanged(new ListChangedEventArgs(2, index));
			}
		}

		/// <summary>Removes the current item from the list.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.BindingSource.AllowRemove" /> property is false.-or-<see cref="P:System.Windows.Forms.BindingSource.Position" /> is less than zero or greater than <see cref="P:System.Windows.Forms.BindingSource.Count" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The underlying list represented by the <see cref="P:System.Windows.Forms.BindingSource.List" /> property is read-only or has a fixed size.</exception>
		// Token: 0x06000468 RID: 1128 RVA: 0x0001554C File Offset: 0x0001374C
		public void RemoveCurrent()
		{
			if (this.Position < 0)
			{
				throw new InvalidOperationException("Cannot remove item because there is no current item.");
			}
			if (!this.AllowRemove)
			{
				throw new InvalidOperationException("Cannot remove item because list does not allow removal of items.");
			}
			this.RemoveAt(this.Position);
		}

		/// <summary>Removes the filter associated with the <see cref="T:System.Windows.Forms.BindingSource" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The underlying list does not support filtering.</exception>
		// Token: 0x06000469 RID: 1129 RVA: 0x00015594 File Offset: 0x00013794
		public virtual void RemoveFilter()
		{
			this.Filter = null;
		}

		/// <summary>Removes the sort associated with the <see cref="T:System.Windows.Forms.BindingSource" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The underlying list does not support sorting.</exception>
		// Token: 0x0600046A RID: 1130 RVA: 0x000155A0 File Offset: 0x000137A0
		public virtual void RemoveSort()
		{
			if (!this.list_is_ibinding)
			{
				return;
			}
			this.sort = null;
			((IBindingList)this.list).RemoveSort();
		}

		/// <summary>Reinitializes the <see cref="P:System.Windows.Forms.BindingSource.AllowNew" /> property.</summary>
		// Token: 0x0600046B RID: 1131 RVA: 0x000155C8 File Offset: 0x000137C8
		[EditorBrowsable(2)]
		public virtual void ResetAllowNew()
		{
			this.allow_new_set = false;
		}

		/// <summary>Causes a control bound to the <see cref="T:System.Windows.Forms.BindingSource" /> to reread all the items in the list and refresh their displayed values. </summary>
		/// <param name="metadataChanged">true if the data schema has changed; false if only values have changed.</param>
		// Token: 0x0600046C RID: 1132 RVA: 0x000155D4 File Offset: 0x000137D4
		public void ResetBindings(bool metadataChanged)
		{
			if (metadataChanged)
			{
				this.OnListChanged(new ListChangedEventArgs(7, null));
			}
			this.OnListChanged(new ListChangedEventArgs(0, -1, -1));
		}

		/// <summary>Causes a control bound to the <see cref="T:System.Windows.Forms.BindingSource" /> to reread the currently selected item and refresh its displayed value.</summary>
		// Token: 0x0600046D RID: 1133 RVA: 0x000155F8 File Offset: 0x000137F8
		public void ResetCurrentItem()
		{
			this.OnListChanged(new ListChangedEventArgs(4, this.Position, -1));
		}

		/// <summary>Causes a control bound to the <see cref="T:System.Windows.Forms.BindingSource" /> to reread the item at the specified index, and refresh its displayed value. </summary>
		/// <param name="itemIndex">The zero-based index of the item that has changed.</param>
		// Token: 0x0600046E RID: 1134 RVA: 0x00015610 File Offset: 0x00013810
		public void ResetItem(int itemIndex)
		{
			this.OnListChanged(new ListChangedEventArgs(4, itemIndex, -1));
		}

		/// <summary>Resumes data binding.</summary>
		// Token: 0x0600046F RID: 1135 RVA: 0x00015620 File Offset: 0x00013820
		public void ResumeBinding()
		{
			this.currency_manager.ResumeBinding();
		}

		/// <summary>Suspends data binding to prevent changes from updating the bound data source.</summary>
		// Token: 0x06000470 RID: 1136 RVA: 0x00015630 File Offset: 0x00013830
		public void SuspendBinding()
		{
			this.currency_manager.SuspendBinding();
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00015640 File Offset: 0x00013840
		private void DataSourceEndInitHandler(object o, EventArgs args)
		{
			((ISupportInitializeNotification)this.datasource).Initialized -= new EventHandler(this.DataSourceEndInitHandler);
			this.EndInit();
		}

		// Token: 0x0400064A RID: 1610
		private bool is_initialized = true;

		// Token: 0x0400064B RID: 1611
		private IList list;

		// Token: 0x0400064C RID: 1612
		private CurrencyManager currency_manager;

		// Token: 0x0400064D RID: 1613
		private Dictionary<string, CurrencyManager> related_currency_managers = new Dictionary<string, CurrencyManager>();

		// Token: 0x0400064E RID: 1614
		internal Type item_type;

		// Token: 0x0400064F RID: 1615
		private bool item_has_default_ctor;

		// Token: 0x04000650 RID: 1616
		private bool list_is_ibinding;

		// Token: 0x04000651 RID: 1617
		private object datasource;

		// Token: 0x04000652 RID: 1618
		private string datamember;

		// Token: 0x04000653 RID: 1619
		private bool raise_list_changed_events;

		// Token: 0x04000654 RID: 1620
		private bool allow_new_set;

		// Token: 0x04000655 RID: 1621
		private bool allow_new;

		// Token: 0x04000656 RID: 1622
		private bool add_pending;

		// Token: 0x04000657 RID: 1623
		private int pending_add_index;

		// Token: 0x04000658 RID: 1624
		private string filter;

		// Token: 0x04000659 RID: 1625
		private string sort;

		// Token: 0x04000663 RID: 1635
		private static object InitializedEvent;
	}
}
