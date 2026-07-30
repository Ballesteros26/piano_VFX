using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Manages all <see cref="T:System.Windows.Forms.Binding" /> objects that are bound to the same data source and data member. This class is abstract.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200005E RID: 94
	public abstract class BindingManagerBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingManagerBase" /> class.</summary>
		// Token: 0x0600039E RID: 926 RVA: 0x00013050 File Offset: 0x00011250
		public BindingManagerBase()
		{
		}

		/// <summary>Occurs when the currently bound item changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000037 RID: 55
		// (add) Token: 0x0600039F RID: 927 RVA: 0x00013058 File Offset: 0x00011258
		// (remove) Token: 0x060003A0 RID: 928 RVA: 0x00013074 File Offset: 0x00011274
		public event EventHandler CurrentChanged
		{
			add
			{
				this.onCurrentChangedHandler = (EventHandler)Delegate.Combine(this.onCurrentChangedHandler, value);
			}
			remove
			{
				this.onCurrentChangedHandler = (EventHandler)Delegate.Remove(this.onCurrentChangedHandler, value);
			}
		}

		/// <summary>Occurs after the value of the <see cref="P:System.Windows.Forms.BindingManagerBase.Position" /> property has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000038 RID: 56
		// (add) Token: 0x060003A1 RID: 929 RVA: 0x00013090 File Offset: 0x00011290
		// (remove) Token: 0x060003A2 RID: 930 RVA: 0x000130AC File Offset: 0x000112AC
		public event EventHandler PositionChanged
		{
			add
			{
				this.onPositionChangedHandler = (EventHandler)Delegate.Combine(this.onPositionChangedHandler, value);
			}
			remove
			{
				this.onPositionChangedHandler = (EventHandler)Delegate.Remove(this.onPositionChangedHandler, value);
			}
		}

		/// <summary>Occurs when the state of the currently bound item changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000039 RID: 57
		// (add) Token: 0x060003A3 RID: 931 RVA: 0x000130C8 File Offset: 0x000112C8
		// (remove) Token: 0x060003A4 RID: 932 RVA: 0x000130E4 File Offset: 0x000112E4
		public event EventHandler CurrentItemChanged
		{
			add
			{
				this.onCurrentItemChangedHandler = (EventHandler)Delegate.Combine(this.onCurrentItemChangedHandler, value);
			}
			remove
			{
				this.onCurrentItemChangedHandler = (EventHandler)Delegate.Remove(this.onCurrentItemChangedHandler, value);
			}
		}

		/// <summary>Occurs at the completion of a data-binding operation.</summary>
		// Token: 0x1400003A RID: 58
		// (add) Token: 0x060003A5 RID: 933 RVA: 0x00013100 File Offset: 0x00011300
		// (remove) Token: 0x060003A6 RID: 934 RVA: 0x0001311C File Offset: 0x0001131C
		public event BindingCompleteEventHandler BindingComplete;

		/// <summary>Occurs when an <see cref="T:System.Exception" /> is silently handled by the <see cref="T:System.Windows.Forms.BindingManagerBase" />. </summary>
		// Token: 0x1400003B RID: 59
		// (add) Token: 0x060003A7 RID: 935 RVA: 0x00013138 File Offset: 0x00011338
		// (remove) Token: 0x060003A8 RID: 936 RVA: 0x00013154 File Offset: 0x00011354
		public event BindingManagerDataErrorEventHandler DataError;

		/// <summary>Gets the collection of bindings being managed.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.BindingsCollection" /> that contains the <see cref="T:System.Windows.Forms.Binding" /> objects managed by this <see cref="T:System.Windows.Forms.BindingManagerBase" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x00013170 File Offset: 0x00011370
		public BindingsCollection Bindings
		{
			get
			{
				if (this.bindings == null)
				{
					this.bindings = new BindingsCollection();
				}
				return this.bindings;
			}
		}

		/// <summary>When overridden in a derived class, gets the number of rows managed by the <see cref="T:System.Windows.Forms.BindingManagerBase" />.</summary>
		/// <returns>The number of rows managed by the <see cref="T:System.Windows.Forms.BindingManagerBase" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060003AA RID: 938
		public abstract int Count { get; }

		/// <summary>When overridden in a derived class, gets the current object.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the current object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003AB RID: 939
		public abstract object Current { get; }

		/// <summary>Gets a value indicating whether binding is suspended.</summary>
		/// <returns>true if binding is suspended; otherwise, false.</returns>
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003AC RID: 940 RVA: 0x00013190 File Offset: 0x00011390
		public bool IsBindingSuspended
		{
			get
			{
				return this.IsSuspended;
			}
		}

		/// <summary>When overridden in a derived class, gets or sets the position in the underlying list that controls bound to this data source point to.</summary>
		/// <returns>A zero-based index that specifies a position in the underlying list.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003AD RID: 941
		// (set) Token: 0x060003AE RID: 942
		public abstract int Position { get; set; }

		/// <summary>When overridden in a derived class, adds a new item to the underlying list.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003AF RID: 943
		public abstract void AddNew();

		/// <summary>When overridden in a derived class, cancels the current edit.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003B0 RID: 944
		public abstract void CancelCurrentEdit();

		/// <summary>When overridden in a derived class, ends the current edit.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003B1 RID: 945
		public abstract void EndCurrentEdit();

		/// <summary>When overridden in a derived class, gets the collection of property descriptors for the binding.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the property descriptors for the binding.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003B2 RID: 946 RVA: 0x00013198 File Offset: 0x00011398
		public virtual PropertyDescriptorCollection GetItemProperties()
		{
			return this.GetItemPropertiesInternal();
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x000131A0 File Offset: 0x000113A0
		internal virtual PropertyDescriptorCollection GetItemPropertiesInternal()
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, deletes the row at the specified index from the underlying list.</summary>
		/// <param name="index">The index of the row to delete. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">There is no row at the specified <paramref name="index" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003B4 RID: 948
		public abstract void RemoveAt(int index);

		/// <summary>When overridden in a derived class, resumes data binding.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003B5 RID: 949
		public abstract void ResumeBinding();

		/// <summary>When overridden in a derived class, suspends data binding.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003B6 RID: 950
		public abstract void SuspendBinding();

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x000131A8 File Offset: 0x000113A8
		internal virtual bool IsSuspended
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the collection of property descriptors for the binding using the specified <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the property descriptors for the binding.</returns>
		/// <param name="dataSources">An <see cref="T:System.Collections.ArrayList" /> containing the data sources. </param>
		/// <param name="listAccessors">An <see cref="T:System.Collections.ArrayList" /> containing the table's bound properties. </param>
		// Token: 0x060003B8 RID: 952 RVA: 0x000131AC File Offset: 0x000113AC
		[MonoTODO("Not implemented, will throw NotImplementedException")]
		protected internal virtual PropertyDescriptorCollection GetItemProperties(ArrayList dataSources, ArrayList listAccessors)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the list of properties of the items managed by this <see cref="T:System.Windows.Forms.BindingManagerBase" />.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the property descriptors for the binding.</returns>
		/// <param name="listType">The <see cref="T:System.Type" /> of the bound list. </param>
		/// <param name="offset">A counter used to recursively call the method. </param>
		/// <param name="dataSources">An <see cref="T:System.Collections.ArrayList" /> containing the data sources. </param>
		/// <param name="listAccessors">An <see cref="T:System.Collections.ArrayList" /> containing the table's bound properties. </param>
		// Token: 0x060003B9 RID: 953 RVA: 0x000131B4 File Offset: 0x000113B4
		[MonoTODO("Not implemented, will throw NotImplementedException")]
		protected virtual PropertyDescriptorCollection GetItemProperties(Type listType, int offset, ArrayList dataSources, ArrayList listAccessors)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, gets the name of the list supplying the data for the binding.</summary>
		/// <returns>The name of the list supplying the data for the binding.</returns>
		/// <param name="listAccessors">An <see cref="T:System.Collections.ArrayList" /> containing the table's bound properties. </param>
		// Token: 0x060003BA RID: 954
		protected internal abstract string GetListName(ArrayList listAccessors);

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingManagerBase.CurrentChanged" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060003BB RID: 955
		protected internal abstract void OnCurrentChanged(EventArgs e);

		/// <summary>Pulls data from the data-bound control into the data source, returning no information.</summary>
		// Token: 0x060003BC RID: 956 RVA: 0x000131BC File Offset: 0x000113BC
		protected void PullData()
		{
			try
			{
				if (!this.transfering_data)
				{
					this.transfering_data = true;
					this.UpdateIsBinding();
				}
				foreach (object obj in this.Bindings)
				{
					Binding binding = (Binding)obj;
					binding.PullData();
				}
			}
			finally
			{
				this.transfering_data = false;
			}
		}

		/// <summary>Pushes data from the data source into the data-bound control, returning no information.</summary>
		// Token: 0x060003BD RID: 957 RVA: 0x00013268 File Offset: 0x00011468
		protected void PushData()
		{
			try
			{
				if (!this.transfering_data)
				{
					this.transfering_data = true;
					this.UpdateIsBinding();
				}
				foreach (object obj in this.Bindings)
				{
					Binding binding = (Binding)obj;
					binding.PushData();
				}
			}
			finally
			{
				this.transfering_data = false;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingManagerBase.BindingComplete" /> event. </summary>
		/// <param name="args">A <see cref="T:System.Windows.Forms.BindingCompleteEventArgs" />  that contains the event data. </param>
		// Token: 0x060003BE RID: 958 RVA: 0x00013314 File Offset: 0x00011514
		protected void OnBindingComplete(BindingCompleteEventArgs args)
		{
			if (this.BindingComplete != null)
			{
				this.BindingComplete(this, args);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingManagerBase.CurrentItemChanged" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060003BF RID: 959
		protected abstract void OnCurrentItemChanged(EventArgs e);

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingManagerBase.DataError" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Exception" /> that caused the <see cref="E:System.Windows.Forms.BindingManagerBase.DataError" /> event to occur.</param>
		// Token: 0x060003C0 RID: 960 RVA: 0x00013330 File Offset: 0x00011530
		protected void OnDataError(Exception e)
		{
			if (this.DataError != null)
			{
				this.DataError(this, new BindingManagerDataErrorEventArgs(e));
			}
		}

		/// <summary>When overridden in a derived class, updates the binding.</summary>
		// Token: 0x060003C1 RID: 961
		protected abstract void UpdateIsBinding();

		// Token: 0x060003C2 RID: 962 RVA: 0x00013350 File Offset: 0x00011550
		internal void AddBinding(Binding binding)
		{
			if (this.Bindings.Contains(binding))
			{
				return;
			}
			this.Bindings.Add(binding);
		}

		// Token: 0x04000633 RID: 1587
		private BindingsCollection bindings;

		// Token: 0x04000634 RID: 1588
		internal bool transfering_data;

		/// <summary>Specifies the event handler for the <see cref="E:System.Windows.Forms.BindingManagerBase.CurrentChanged" /> event.</summary>
		// Token: 0x04000635 RID: 1589
		protected EventHandler onCurrentChangedHandler;

		/// <summary>Specifies the event handler for the <see cref="E:System.Windows.Forms.BindingManagerBase.PositionChanged" /> event.</summary>
		// Token: 0x04000636 RID: 1590
		protected EventHandler onPositionChangedHandler;

		// Token: 0x04000637 RID: 1591
		internal EventHandler onCurrentItemChangedHandler;
	}
}
