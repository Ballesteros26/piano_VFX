using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms
{
	/// <summary>Represents the collection of data bindings for a control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000AC RID: 172
	[DefaultEvent("CollectionChanged")]
	[Editor("System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[TypeConverter("System.Windows.Forms.Design.ControlBindingsConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class ControlBindingsCollection : BindingsCollection
	{
		// Token: 0x06000AA6 RID: 2726 RVA: 0x0002C1AC File Offset: 0x0002A3AC
		internal ControlBindingsCollection(Control control)
		{
			this.control = control;
			this.bindable_component = control;
			this.default_datasource_update_mode = DataSourceUpdateMode.OnValidation;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ControlBindingsCollection" /> class with the specified bindable control.</summary>
		/// <param name="control">The <see cref="T:System.Windows.Forms.IBindableComponent" /> the binding collection belongs to.</param>
		// Token: 0x06000AA7 RID: 2727 RVA: 0x0002C1CC File Offset: 0x0002A3CC
		public ControlBindingsCollection(IBindableComponent control)
		{
			this.bindable_component = control;
			control = control as Control;
			this.default_datasource_update_mode = DataSourceUpdateMode.OnValidation;
		}

		/// <summary>Gets the control that the collection belongs to.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that the collection belongs to.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x0002C1EC File Offset: 0x0002A3EC
		public Control Control
		{
			get
			{
				return this.control;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.Binding" /> specified by the control's property name.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Binding" /> that binds the specified control property to a data source.</returns>
		/// <param name="propertyName">The name of the property on the data-bound control. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000253 RID: 595
		public Binding this[string propertyName]
		{
			get
			{
				foreach (object obj in base.List)
				{
					Binding binding = (Binding)obj;
					if (binding.PropertyName == propertyName)
					{
						return binding;
					}
				}
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.IBindableComponent" /> the binding collection belongs to.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.IBindableComponent" /> the binding collection belongs to.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x0002C278 File Offset: 0x0002A478
		public IBindableComponent BindableComponent
		{
			get
			{
				return this.bindable_component;
			}
		}

		/// <summary>Gets or sets the default <see cref="P:System.Windows.Forms.Binding.DataSourceUpdateMode" /> for a <see cref="T:System.Windows.Forms.Binding" /> in the collection.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode" /> values.</returns>
		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x0002C280 File Offset: 0x0002A480
		// (set) Token: 0x06000AAC RID: 2732 RVA: 0x0002C288 File Offset: 0x0002A488
		public DataSourceUpdateMode DefaultDataSourceUpdateMode
		{
			get
			{
				return this.default_datasource_update_mode;
			}
			set
			{
				this.default_datasource_update_mode = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Windows.Forms.Binding" /> to the collection.</summary>
		/// <param name="binding">The <see cref="T:System.Windows.Forms.Binding" /> to add. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="binding" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The control property is already data-bound. </exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Windows.Forms.Binding" /> does not specify a valid column of the <see cref="P:System.Windows.Forms.Binding.DataSource" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AAD RID: 2733 RVA: 0x0002C294 File Offset: 0x0002A494
		public new void Add(Binding binding)
		{
			this.AddCore(binding);
			this.OnCollectionChanged(new CollectionChangeEventArgs(1, binding));
		}

		/// <summary>Creates a <see cref="T:System.Windows.Forms.Binding" /> using the specified control property name, data source, and data member, and adds it to the collection.</summary>
		/// <returns>The newly created <see cref="T:System.Windows.Forms.Binding" />.</returns>
		/// <param name="propertyName">The name of the control property to bind. </param>
		/// <param name="dataSource">An <see cref="T:System.Object" /> that represents the data source. </param>
		/// <param name="dataMember">The property or list to bind to. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="binding" /> is null. </exception>
		/// <exception cref="T:System.Exception">The <paramref name="propertyName" /> is already data-bound. </exception>
		/// <exception cref="T:System.Exception">The <paramref name="dataMember" /> doesn't specify a valid member of the <paramref name="dataSource" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000AAE RID: 2734 RVA: 0x0002C2AC File Offset: 0x0002A4AC
		public Binding Add(string propertyName, object dataSource, string dataMember)
		{
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			Binding binding = new Binding(propertyName, dataSource, dataMember);
			binding.DataSourceUpdateMode = this.default_datasource_update_mode;
			this.Add(binding);
			return binding;
		}

		/// <summary>Creates a binding with the specified control property name, data source, data member, and information about whether formatting is enabled, and adds the binding to the collection.</summary>
		/// <returns>The newly created <see cref="T:System.Windows.Forms.Binding" />.</returns>
		/// <param name="propertyName">The name of the control property to bind.</param>
		/// <param name="dataSource">An <see cref="T:System.Object" /> representing the data source. </param>
		/// <param name="dataMember">The property or list to bind to.</param>
		/// <param name="formattingEnabled">true to format the displayed data; otherwise, false</param>
		/// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control.-or-The property given is a read-only property.</exception>
		/// <exception cref="T:System.Exception">If formatting is disabled and the <paramref name="propertyName" /> is neither a valid property of a control nor an empty string (""). </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000AAF RID: 2735 RVA: 0x0002C2E8 File Offset: 0x0002A4E8
		public Binding Add(string propertyName, object dataSource, string dataMember, bool formattingEnabled)
		{
			return this.Add(propertyName, dataSource, dataMember, formattingEnabled, this.default_datasource_update_mode, null, string.Empty, null);
		}

		/// <summary>Creates a binding that binds the specified control property to the specified data member of the specified data source, optionally enabling formatting, propagating values to the data source based on the specified update setting, and adding the binding to the collection.</summary>
		/// <returns>The newly created <see cref="T:System.Windows.Forms.Binding" />.</returns>
		/// <param name="propertyName">The name of the control property to bind. </param>
		/// <param name="dataSource">An <see cref="T:System.Object" /> representing the data source. </param>
		/// <param name="dataMember">The property or list to bind to.</param>
		/// <param name="formattingEnabled">true to format the displayed data; otherwise, false.</param>
		/// <param name="updateMode">One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control or is read-only.-or-The specified data member does not exist on the data source.-or-The data source, data member, or control property specified are associated with another binding in the collection.</exception>
		// Token: 0x06000AB0 RID: 2736 RVA: 0x0002C310 File Offset: 0x0002A510
		public Binding Add(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode updateMode)
		{
			return this.Add(propertyName, dataSource, dataMember, formattingEnabled, updateMode, null, string.Empty, null);
		}

		/// <summary>Creates a binding that binds the specified control property to the specified data member of the specified data source, optionally enabling formatting, propagating values to the data source based on the specified update setting, setting the property to the specified value when <see cref="T:System.DBNull" /> is returned from the data source, and adding the binding to the collection.</summary>
		/// <returns>The newly created <see cref="T:System.Windows.Forms.Binding" /></returns>
		/// <param name="propertyName">The name of the control property to bind. </param>
		/// <param name="dataSource">An <see cref="T:System.Object" /> representing the data source. </param>
		/// <param name="dataMember">The property or list to bind to.</param>
		/// <param name="formattingEnabled">true to format the displayed data; otherwise, false.</param>
		/// <param name="updateMode">One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode" /> values.</param>
		/// <param name="nullValue">The <see cref="T:System.Object" /> to be applied to the bound control property if the data source value is <see cref="T:System.DBNull" />.</param>
		/// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control or is read-only.-or-The specified data member does not exist on the data source.-or-The data source, data member, or control property specified are associated with another binding in the collection.</exception>
		// Token: 0x06000AB1 RID: 2737 RVA: 0x0002C334 File Offset: 0x0002A534
		public Binding Add(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode updateMode, object nullValue)
		{
			return this.Add(propertyName, dataSource, dataMember, formattingEnabled, updateMode, nullValue, string.Empty, null);
		}

		/// <summary>Creates a binding that binds the specified control property to the specified data member of the specified data source, optionally enabling formatting with the specified format string, propagating values to the data source based on the specified update setting, setting the property to the specified value when <see cref="T:System.DBNull" /> is returned from the data source, and adding the binding to the collection.</summary>
		/// <returns>The newly created <see cref="T:System.Windows.Forms.Binding" /></returns>
		/// <param name="propertyName">The name of the control property to bind. </param>
		/// <param name="dataSource">An <see cref="T:System.Object" /> representing the data source. </param>
		/// <param name="dataMember">The property or list to bind to.</param>
		/// <param name="formattingEnabled">true to format the displayed data; otherwise, false.</param>
		/// <param name="updateMode">One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode" /> values.</param>
		/// <param name="nullValue">The <see cref="T:System.Object" /> to be applied to the bound control property if the data source value is <see cref="T:System.DBNull" />.</param>
		/// <param name="formatString">One or more format specifier characters that indicate how a value is to be displayed.</param>
		/// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control or is read-only.-or-The specified data member does not exist on the data source.-or-The data source, data member, or control property specified are associated with another binding in the collection.</exception>
		// Token: 0x06000AB2 RID: 2738 RVA: 0x0002C358 File Offset: 0x0002A558
		public Binding Add(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode updateMode, object nullValue, string formatString)
		{
			return this.Add(propertyName, dataSource, dataMember, formattingEnabled, updateMode, nullValue, formatString, null);
		}

		/// <summary>Creates a binding that binds the specified control property to the specified data member of the specified data source, optionally enabling formatting with the specified format string, propagating values to the data source based on the specified update setting, setting the property to the specified value when <see cref="T:System.DBNull" /> is returned from the data source, setting the specified format provider, and adding the binding to the collection.</summary>
		/// <returns>The newly created <see cref="T:System.Windows.Forms.Binding" />.</returns>
		/// <param name="propertyName">The name of the control property to bind. </param>
		/// <param name="dataSource">An <see cref="T:System.Object" /> representing the data source. </param>
		/// <param name="dataMember">The property or list to bind to.</param>
		/// <param name="formattingEnabled">true to format the displayed data; otherwise, false.</param>
		/// <param name="updateMode">One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode" /> values.</param>
		/// <param name="nullValue">The <see cref="T:System.Object" /> to be applied to the bound control property if the data source value is <see cref="T:System.DBNull" />.</param>
		/// <param name="formatString">One or more format specifier characters that indicate how a value is to be displayed</param>
		/// <param name="formatInfo">An implementation of <see cref="T:System.IFormatProvider" /> to override default formatting behavior.</param>
		/// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control or is read-only.-or-The specified data member does not exist on the data source.-or-The data source, data member, or control property specified are associated with another binding in the collection.</exception>
		// Token: 0x06000AB3 RID: 2739 RVA: 0x0002C378 File Offset: 0x0002A578
		public Binding Add(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode updateMode, object nullValue, string formatString, IFormatProvider formatInfo)
		{
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			Binding binding = new Binding(propertyName, dataSource, dataMember);
			binding.FormattingEnabled = formattingEnabled;
			binding.DataSourceUpdateMode = updateMode;
			binding.NullValue = nullValue;
			binding.FormatString = formatString;
			binding.FormatInfo = formatInfo;
			this.Add(binding);
			return binding;
		}

		/// <summary>Clears the collection of any bindings.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000AB4 RID: 2740 RVA: 0x0002C3D0 File Offset: 0x0002A5D0
		public new void Clear()
		{
			base.Clear();
		}

		/// <summary>Deletes the specified <see cref="T:System.Windows.Forms.Binding" /> from the collection.</summary>
		/// <param name="binding">The <see cref="T:System.Windows.Forms.Binding" /> to remove. </param>
		/// <exception cref="T:System.NullReferenceException">The <paramref name="binding" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000AB5 RID: 2741 RVA: 0x0002C3D8 File Offset: 0x0002A5D8
		public new void Remove(Binding binding)
		{
			if (binding == null)
			{
				throw new NullReferenceException("The binding is null");
			}
			base.Remove(binding);
		}

		/// <summary>Deletes the <see cref="T:System.Windows.Forms.Binding" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the item to remove. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> value is less than 0, or it is greater than the number of bindings in the collection. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000AB6 RID: 2742 RVA: 0x0002C3F4 File Offset: 0x0002A5F4
		public new void RemoveAt(int index)
		{
			if (index < 0 || index >= base.List.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			base.RemoveAt(index);
		}

		/// <summary>Adds a binding to the collection.</summary>
		/// <param name="dataBinding">The <see cref="T:System.Windows.Forms.Binding" /> to add. </param>
		// Token: 0x06000AB7 RID: 2743 RVA: 0x0002C42C File Offset: 0x0002A62C
		protected override void AddCore(Binding dataBinding)
		{
			if (dataBinding == null)
			{
				throw new ArgumentNullException("dataBinding");
			}
			if (dataBinding.Control != null && dataBinding.BindableComponent != this.bindable_component)
			{
				throw new ArgumentException("dataBinding belongs to another BindingsCollection");
			}
			for (int i = 0; i < this.Count; i++)
			{
				Binding binding = base[i];
				if (binding != null && binding.PropertyName.Length != 0 && dataBinding.PropertyName.Length != 0)
				{
					if (string.Compare(binding.PropertyName, dataBinding.PropertyName, true) == 0)
					{
						throw new ArgumentException("The binding is already in the collection");
					}
				}
			}
			dataBinding.SetControl(this.bindable_component);
			dataBinding.Check();
			base.AddCore(dataBinding);
		}

		/// <summary>Clears the bindings in the collection.</summary>
		// Token: 0x06000AB8 RID: 2744 RVA: 0x0002C4F8 File Offset: 0x0002A6F8
		protected override void ClearCore()
		{
			base.ClearCore();
		}

		/// <summary>Removes the specified binding from the collection.</summary>
		/// <param name="dataBinding">The <see cref="T:System.Windows.Forms.Binding" /> to remove from the collection.</param>
		/// <exception cref="T:System.ArgumentException">The binding belongs to another <see cref="T:System.Windows.Forms.ControlBindingsCollection" />.</exception>
		// Token: 0x06000AB9 RID: 2745 RVA: 0x0002C500 File Offset: 0x0002A700
		protected override void RemoveCore(Binding dataBinding)
		{
			if (dataBinding == null)
			{
				throw new ArgumentNullException("dataBinding");
			}
			base.RemoveCore(dataBinding);
		}

		// Token: 0x04000838 RID: 2104
		private Control control;

		// Token: 0x04000839 RID: 2105
		private IBindableComponent bindable_component;

		// Token: 0x0400083A RID: 2106
		private DataSourceUpdateMode default_datasource_update_mode;
	}
}
