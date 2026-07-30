using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Manages a list of <see cref="T:System.Windows.Forms.Binding" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000B3 RID: 179
	public class CurrencyManager : BindingManagerBase
	{
		// Token: 0x06000B1B RID: 2843 RVA: 0x0002D198 File Offset: 0x0002B398
		internal CurrencyManager()
		{
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0002D1A0 File Offset: 0x0002B3A0
		internal CurrencyManager(object data_source)
		{
			this.SetDataSource(data_source);
		}

		/// <summary>Occurs when the list changes or an item in the list changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000B2 RID: 178
		// (add) Token: 0x06000B1D RID: 2845 RVA: 0x0002D1B0 File Offset: 0x0002B3B0
		// (remove) Token: 0x06000B1E RID: 2846 RVA: 0x0002D1CC File Offset: 0x0002B3CC
		public event ListChangedEventHandler ListChanged;

		/// <summary>Occurs when the current item has been altered.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000B3 RID: 179
		// (add) Token: 0x06000B1F RID: 2847 RVA: 0x0002D1E8 File Offset: 0x0002B3E8
		// (remove) Token: 0x06000B20 RID: 2848 RVA: 0x0002D204 File Offset: 0x0002B404
		public event ItemChangedEventHandler ItemChanged;

		/// <summary>Occurs when the metadata of the <see cref="P:System.Windows.Forms.CurrencyManager.List" /> has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000B4 RID: 180
		// (add) Token: 0x06000B21 RID: 2849 RVA: 0x0002D220 File Offset: 0x0002B420
		// (remove) Token: 0x06000B22 RID: 2850 RVA: 0x0002D23C File Offset: 0x0002B43C
		public event EventHandler MetaDataChanged;

		/// <summary>Gets the list for this <see cref="T:System.Windows.Forms.CurrencyManager" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> that contains the list.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x0002D258 File Offset: 0x0002B458
		public IList List
		{
			get
			{
				return this.list;
			}
		}

		/// <summary>Gets the current item in the list.</summary>
		/// <returns>A list item of type <see cref="T:System.Object" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x0002D260 File Offset: 0x0002B460
		public override object Current
		{
			get
			{
				if (this.listposition == -1 || this.listposition >= this.list.Count)
				{
					throw new IndexOutOfRangeException("list position");
				}
				return this.list[this.listposition];
			}
		}

		/// <summary>Gets the number of items in the list.</summary>
		/// <returns>The number of items in the list.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x0002D2AC File Offset: 0x0002B4AC
		public override int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets or sets the position you are at within the list.</summary>
		/// <returns>A number between 0 and <see cref="P:System.Windows.Forms.CurrencyManager.Count" /> minus 1.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x0002D2BC File Offset: 0x0002B4BC
		// (set) Token: 0x06000B27 RID: 2855 RVA: 0x0002D2C4 File Offset: 0x0002B4C4
		public override int Position
		{
			get
			{
				return this.listposition;
			}
			set
			{
				if (value < 0)
				{
					value = 0;
				}
				if (value >= this.list.Count)
				{
					value = this.list.Count - 1;
				}
				if (this.listposition == value)
				{
					return;
				}
				if (this.listposition != -1)
				{
					this.EndCurrentEdit();
				}
				this.listposition = value;
				this.OnCurrentChanged(EventArgs.Empty);
				this.OnPositionChanged(EventArgs.Empty);
			}
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0002D338 File Offset: 0x0002B538
		internal void SetDataSource(object data_source)
		{
			if (this.data_source is IBindingList)
			{
				((IBindingList)this.data_source).ListChanged -= new ListChangedEventHandler(this.ListChangedHandler);
			}
			if (data_source is IListSource)
			{
				data_source = ((IListSource)data_source).GetList();
			}
			this.data_source = data_source;
			if (data_source != null)
			{
				this.finalType = data_source.GetType();
			}
			this.listposition = -1;
			if (this.data_source is IBindingList)
			{
				((IBindingList)this.data_source).ListChanged += new ListChangedEventHandler(this.ListChangedHandler);
			}
			this.list = (IList)data_source;
			this.ListChangedHandler(null, new ListChangedEventArgs(0, -1));
		}

		/// <summary>Gets the property descriptor collection for the underlying list.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> for the list.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B29 RID: 2857 RVA: 0x0002D3F0 File Offset: 0x0002B5F0
		public override PropertyDescriptorCollection GetItemProperties()
		{
			return ListBindingHelper.GetListItemProperties(this.list);
		}

		/// <summary>Removes the item at the specified index.</summary>
		/// <param name="index">The index of the item to remove from the list. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">There is no row at the specified <paramref name="index" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B2A RID: 2858 RVA: 0x0002D400 File Offset: 0x0002B600
		public override void RemoveAt(int index)
		{
			this.list.RemoveAt(index);
		}

		/// <summary>Suspends data binding to prevents changes from updating the bound data source.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B2B RID: 2859 RVA: 0x0002D410 File Offset: 0x0002B610
		public override void SuspendBinding()
		{
			this.binding_suspended = true;
		}

		/// <summary>Resumes data binding.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B2C RID: 2860 RVA: 0x0002D41C File Offset: 0x0002B61C
		public override void ResumeBinding()
		{
			this.binding_suspended = false;
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x0002D428 File Offset: 0x0002B628
		internal override bool IsSuspended
		{
			get
			{
				return this.Count == 0 || this.binding_suspended;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x0002D440 File Offset: 0x0002B640
		internal bool AllowNew
		{
			get
			{
				if (this.list is IBindingList)
				{
					return ((IBindingList)this.list).AllowNew;
				}
				return this.list.IsReadOnly && false;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x0002D484 File Offset: 0x0002B684
		internal bool AllowRemove
		{
			get
			{
				return !this.list.IsReadOnly && this.list is IBindingList && ((IBindingList)this.list).AllowRemove;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x0002D4C8 File Offset: 0x0002B6C8
		internal bool AllowEdit
		{
			get
			{
				return this.list is IBindingList && ((IBindingList)this.list).AllowEdit;
			}
		}

		/// <summary>Adds a new item to the underlying list.</summary>
		/// <exception cref="T:System.NotSupportedException">The underlying data source does not implement <see cref="T:System.ComponentModel.IBindingList" />, or the data source has thrown an exception because the user has attempted to add a row to a read-only or fixed-size <see cref="T:System.Data.DataView" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B31 RID: 2865 RVA: 0x0002D4F8 File Offset: 0x0002B6F8
		public override void AddNew()
		{
			IBindingList bindingList = this.list as IBindingList;
			if (bindingList == null)
			{
				throw new NotSupportedException();
			}
			bindingList.AddNew();
			bool flag = this.Position != this.list.Count - 1;
			this.ChangeRecordState(this.list.Count - 1, flag, flag, true, true);
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0002D554 File Offset: 0x0002B754
		private void BeginEdit()
		{
			IEditableObject editableObject = this.Current as IEditableObject;
			if (editableObject != null)
			{
				try
				{
					editableObject.BeginEdit();
					this.editing = true;
				}
				catch
				{
				}
			}
		}

		/// <summary>Cancels the current edit operation.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B33 RID: 2867 RVA: 0x0002D5A8 File Offset: 0x0002B7A8
		public override void CancelCurrentEdit()
		{
			if (this.listposition == -1)
			{
				return;
			}
			IEditableObject editableObject = this.Current as IEditableObject;
			if (editableObject != null)
			{
				this.editing = false;
				editableObject.CancelEdit();
				this.OnItemChanged(new ItemChangedEventArgs(this.Position));
			}
			if (this.list is ICancelAddNew)
			{
				((ICancelAddNew)this.list).CancelNew(this.listposition);
			}
		}

		/// <summary>Ends the current edit operation.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B34 RID: 2868 RVA: 0x0002D618 File Offset: 0x0002B818
		public override void EndCurrentEdit()
		{
			if (this.listposition == -1)
			{
				return;
			}
			IEditableObject editableObject = this.Current as IEditableObject;
			if (editableObject != null)
			{
				this.editing = false;
				editableObject.EndEdit();
			}
			if (this.list is ICancelAddNew)
			{
				((ICancelAddNew)this.list).EndNew(this.listposition);
			}
		}

		/// <summary>Forces a repopulation of the data-bound list.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B35 RID: 2869 RVA: 0x0002D678 File Offset: 0x0002B878
		public void Refresh()
		{
			this.ListChangedHandler(null, new ListChangedEventArgs(0, -1));
		}

		/// <summary>Throws an exception if there is no list, or the list is empty.</summary>
		/// <exception cref="T:System.Exception">There is no list, or the list is empty. </exception>
		// Token: 0x06000B36 RID: 2870 RVA: 0x0002D688 File Offset: 0x0002B888
		protected void CheckEmpty()
		{
			if (this.list == null || this.list.Count < 1)
			{
				throw new Exception("List is empty.");
			}
		}

		/// <param name="e"></param>
		// Token: 0x06000B37 RID: 2871 RVA: 0x0002D6B4 File Offset: 0x0002B8B4
		protected internal override void OnCurrentChanged(EventArgs e)
		{
			if (this.onCurrentChangedHandler != null)
			{
				this.onCurrentChangedHandler.Invoke(this, e);
			}
			if (this.onCurrentItemChangedHandler != null)
			{
				this.onCurrentItemChangedHandler.Invoke(this, e);
			}
		}

		/// <param name="e"></param>
		// Token: 0x06000B38 RID: 2872 RVA: 0x0002D6F4 File Offset: 0x0002B8F4
		protected override void OnCurrentItemChanged(EventArgs e)
		{
			if (this.onCurrentItemChangedHandler != null)
			{
				this.onCurrentItemChangedHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.CurrencyManager.ItemChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.ItemChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x06000B39 RID: 2873 RVA: 0x0002D710 File Offset: 0x0002B910
		protected virtual void OnItemChanged(ItemChangedEventArgs e)
		{
			if (this.ItemChanged != null)
			{
				this.ItemChanged(this, e);
			}
			this.transfering_data = true;
			base.PushData();
			this.transfering_data = false;
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0002D74C File Offset: 0x0002B94C
		private void OnListChanged(ListChangedEventArgs args)
		{
			if (this.ListChanged != null)
			{
				this.ListChanged.Invoke(this, args);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingManagerBase.PositionChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000B3B RID: 2875 RVA: 0x0002D768 File Offset: 0x0002B968
		protected virtual void OnPositionChanged(EventArgs e)
		{
			if (this.onPositionChangedHandler != null)
			{
				this.onPositionChangedHandler.Invoke(this, e);
			}
		}

		/// <summary>Gets the name of the list supplying the data for the binding using the specified set of bound properties.</summary>
		/// <returns>If successful, a <see cref="T:System.String" /> containing name of the list supplying the data for the binding; otherwise, an <see cref="F:System.String.Empty" /> string.</returns>
		/// <param name="listAccessors">An <see cref="T:System.Collections.ArrayList" /> of properties to be found in the data source.</param>
		// Token: 0x06000B3C RID: 2876 RVA: 0x0002D784 File Offset: 0x0002B984
		protected internal override string GetListName(ArrayList listAccessors)
		{
			if (this.list is ITypedList)
			{
				PropertyDescriptor[] array = null;
				if (listAccessors != null)
				{
					array = new PropertyDescriptor[listAccessors.Count];
					listAccessors.CopyTo(array, 0);
				}
				return ((ITypedList)this.list).GetListName(array);
			}
			if (this.finalType != null)
			{
				return this.finalType.Name;
			}
			return string.Empty;
		}

		/// <summary>Updates the status of the binding.</summary>
		// Token: 0x06000B3D RID: 2877 RVA: 0x0002D7EC File Offset: 0x0002B9EC
		protected override void UpdateIsBinding()
		{
			this.UpdateItem();
			foreach (object obj in base.Bindings)
			{
				Binding binding = (Binding)obj;
				binding.UpdateIsBinding();
			}
			this.ChangeRecordState(this.listposition, false, false, true, false);
			this.OnItemChanged(new ItemChangedEventArgs(-1));
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0002D87C File Offset: 0x0002BA7C
		private void ChangeRecordState(int newPosition, bool validating, bool endCurrentEdit, bool firePositionChanged, bool pullData)
		{
			if (endCurrentEdit)
			{
				this.EndCurrentEdit();
			}
			int num = this.listposition;
			this.listposition = newPosition;
			if (this.listposition >= this.list.Count)
			{
				this.listposition = this.list.Count - 1;
			}
			if (num != -1 && this.listposition != -1)
			{
				this.OnCurrentChanged(EventArgs.Empty);
			}
			if (firePositionChanged)
			{
				this.OnPositionChanged(EventArgs.Empty);
			}
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0002D8FC File Offset: 0x0002BAFC
		private void UpdateItem()
		{
			if (!this.transfering_data && this.listposition == -1 && this.list.Count > 0)
			{
				this.listposition = 0;
				this.BeginEdit();
			}
		}

		// Token: 0x17000270 RID: 624
		internal object this[int index]
		{
			get
			{
				return this.list[index];
			}
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0002D944 File Offset: 0x0002BB44
		private PropertyDescriptorCollection GetBrowsableProperties(Type t)
		{
			return TypeDescriptor.GetProperties(t, new Attribute[]
			{
				new BrowsableAttribute(true)
			});
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.CurrencyManager.MetaDataChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000B42 RID: 2882 RVA: 0x0002D968 File Offset: 0x0002BB68
		protected void OnMetaDataChanged(EventArgs e)
		{
			if (this.MetaDataChanged != null)
			{
				this.MetaDataChanged.Invoke(this, e);
			}
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0002D984 File Offset: 0x0002BB84
		private void ListChangedHandler(object sender, ListChangedEventArgs e)
		{
			switch (e.ListChangedType)
			{
			case 0:
				base.PushData();
				this.UpdateIsBinding();
				this.OnListChanged(e);
				return;
			case 1:
				if (this.list.Count == 1)
				{
					this.ChangeRecordState(e.NewIndex, false, false, true, false);
					this.OnItemChanged(new ItemChangedEventArgs(-1));
					this.OnListChanged(e);
				}
				else if (e.NewIndex <= this.listposition)
				{
					this.ChangeRecordState(this.listposition + 1, false, false, false, false);
					this.OnItemChanged(new ItemChangedEventArgs(-1));
					this.OnListChanged(e);
					this.OnPositionChanged(EventArgs.Empty);
				}
				else
				{
					this.OnItemChanged(new ItemChangedEventArgs(-1));
					this.OnListChanged(e);
				}
				return;
			case 2:
				if (this.list.Count == 0)
				{
					this.listposition = -1;
					this.UpdateIsBinding();
					this.OnPositionChanged(EventArgs.Empty);
					this.OnCurrentChanged(EventArgs.Empty);
				}
				else if (e.NewIndex <= this.listposition)
				{
					this.ChangeRecordState(e.NewIndex, false, false, e.NewIndex != this.listposition, false);
				}
				this.OnItemChanged(new ItemChangedEventArgs(-1));
				this.OnListChanged(e);
				return;
			case 4:
				if (this.editing)
				{
					if (e.NewIndex == this.listposition)
					{
						this.OnCurrentItemChanged(EventArgs.Empty);
					}
					this.OnItemChanged(new ItemChangedEventArgs(e.NewIndex));
				}
				this.OnListChanged(e);
				return;
			case 5:
			case 6:
			case 7:
				this.OnMetaDataChanged(EventArgs.Empty);
				this.OnListChanged(e);
				return;
			}
			this.OnListChanged(e);
		}

		/// <summary>Specifies the current position of the <see cref="T:System.Windows.Forms.CurrencyManager" /> in the list.</summary>
		// Token: 0x04000863 RID: 2147
		protected int listposition;

		/// <summary>Specifies the data type of the list.</summary>
		// Token: 0x04000864 RID: 2148
		protected Type finalType;

		// Token: 0x04000865 RID: 2149
		private IList list;

		// Token: 0x04000866 RID: 2150
		private bool binding_suspended;

		// Token: 0x04000867 RID: 2151
		private object data_source;

		// Token: 0x04000868 RID: 2152
		private bool editing;
	}
}
