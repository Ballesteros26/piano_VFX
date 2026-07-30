using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Represents the simple binding between the property value of an object and the property value of a control.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000058 RID: 88
	[TypeConverter(typeof(ListBindingConverter))]
	public class Binding
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Binding" /> class that simple-binds the indicated control property to the specified data member of the data source.</summary>
		/// <param name="propertyName">The name of the control property to bind. </param>
		/// <param name="dataSource">An <see cref="T:System.Object" /> that represents the data source. </param>
		/// <param name="dataMember">The property or list to bind to. </param>
		/// <exception cref="T:System.Exception">
		///   <paramref name="propertyName" /> is neither a valid property of a control nor an empty string (""). </exception>
		/// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control.</exception>
		// Token: 0x0600033F RID: 831 RVA: 0x00011E80 File Offset: 0x00010080
		public Binding(string propertyName, object dataSource, string dataMember)
			: this(propertyName, dataSource, dataMember, false, DataSourceUpdateMode.OnValidation, null, string.Empty, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Binding" /> class that binds the indicated control property to the specified data member of the data source, and optionally enables formatting to be applied.</summary>
		/// <param name="propertyName">The name of the control property to bind. </param>
		/// <param name="dataSource">An <see cref="T:System.Object" /> that represents the data source. </param>
		/// <param name="dataMember">The property or list to bind to. </param>
		/// <param name="formattingEnabled">true to format the displayed data; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control.-or-The property given is a read-only property.</exception>
		/// <exception cref="T:System.Exception">Formatting is disabled and <paramref name="propertyName" /> is neither a valid property of a control nor an empty string (""). </exception>
		// Token: 0x06000340 RID: 832 RVA: 0x00011EA0 File Offset: 0x000100A0
		public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled)
			: this(propertyName, dataSource, dataMember, formattingEnabled, DataSourceUpdateMode.OnValidation, null, string.Empty, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Binding" /> class that binds the specified control property to the specified data member of the specified data source. Optionally enables formatting and propagates values to the data source based on the specified update setting.</summary>
		/// <param name="propertyName">The name of the control property to bind. </param>
		/// <param name="dataSource">An <see cref="T:System.Object" /> representing the data source. </param>
		/// <param name="dataMember">The property or list to bind to.</param>
		/// <param name="formattingEnabled">true to format the displayed data; otherwise, false.</param>
		/// <param name="dataSourceUpdateMode">One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control.-or-The data source or data member or control property specified are associated with another binding in the collection.</exception>
		// Token: 0x06000341 RID: 833 RVA: 0x00011EC0 File Offset: 0x000100C0
		public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode)
			: this(propertyName, dataSource, dataMember, formattingEnabled, dataSourceUpdateMode, null, string.Empty, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Binding" /> class that binds the indicated control property to the specified data member of the specified data source. Optionally enables formatting, propagates values to the data source based on the specified update setting, and sets the property to the specified value when a <see cref="T:System.DBNull" /> is returned from the data source.</summary>
		/// <param name="propertyName">The name of the control property to bind. </param>
		/// <param name="dataSource">An <see cref="T:System.Object" /> representing the data source. </param>
		/// <param name="dataMember">The property or list to bind to.</param>
		/// <param name="formattingEnabled">true to format the displayed data; otherwise, false.</param>
		/// <param name="dataSourceUpdateMode">One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode" /> values.</param>
		/// <param name="nullValue">The <see cref="T:System.Object" /> to be applied to the bound control property if the data source value is <see cref="T:System.DBNull" />.</param>
		/// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control.-or-The data source or data member or control property specified are associated with another binding in the collection.</exception>
		// Token: 0x06000342 RID: 834 RVA: 0x00011EE4 File Offset: 0x000100E4
		public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode, object nullValue)
			: this(propertyName, dataSource, dataMember, formattingEnabled, dataSourceUpdateMode, nullValue, string.Empty, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Binding" /> class that binds the specified control property to the specified data member of the specified data source. Optionally enables formatting with the specified format string; propagates values to the data source based on the specified update setting; and sets the property to the specified value when a <see cref="T:System.DBNull" /> is returned from the data source.</summary>
		/// <param name="propertyName">The name of the control property to bind. </param>
		/// <param name="dataSource">An <see cref="T:System.Object" /> representing the data source. </param>
		/// <param name="dataMember">The property or list to bind to.</param>
		/// <param name="formattingEnabled">true to format the displayed data; otherwise, false.</param>
		/// <param name="dataSourceUpdateMode">One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode" /> values.</param>
		/// <param name="nullValue">The <see cref="T:System.Object" /> to be applied to the bound control property if the data source value is <see cref="T:System.DBNull" />.</param>
		/// <param name="formatString">One or more format specifier characters that indicate how a value is to be displayed.</param>
		/// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control.-or-The data source or data member or control property specified are associated with another binding in the collection.</exception>
		// Token: 0x06000343 RID: 835 RVA: 0x00011F08 File Offset: 0x00010108
		public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode, object nullValue, string formatString)
			: this(propertyName, dataSource, dataMember, formattingEnabled, dataSourceUpdateMode, nullValue, formatString, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Binding" /> class with the specified control property to the specified data member of the specified data source. Optionally enables formatting with the specified format string; propagates values to the data source based on the specified update setting; enables formatting with the specified format string; sets the property to the specified value when a <see cref="T:System.DBNull" /> is returned from the data source; and sets the specified format provider.</summary>
		/// <param name="propertyName">The name of the control property to bind. </param>
		/// <param name="dataSource">An <see cref="T:System.Object" /> representing the data source. </param>
		/// <param name="dataMember">The property or list to bind to.</param>
		/// <param name="formattingEnabled">true to format the displayed data; otherwise, false.</param>
		/// <param name="dataSourceUpdateMode">One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode" /> values.</param>
		/// <param name="nullValue">The <see cref="T:System.Object" /> to be applied to the bound control property if the data source value is <see cref="T:System.DBNull" />.</param>
		/// <param name="formatString">One or more format specifier characters that indicate how a value is to be displayed.</param>
		/// <param name="formatInfo">An implementation of <see cref="T:System.IFormatProvider" /> to override default formatting behavior.</param>
		/// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control.-or-The data source or data member or control property specified are associated with another binding in the collection.</exception>
		// Token: 0x06000344 RID: 836 RVA: 0x00011F28 File Offset: 0x00010128
		public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode, object nullValue, string formatString, IFormatProvider formatInfo)
		{
			this.property_name = propertyName;
			this.data_source = dataSource;
			this.data_member = dataMember;
			this.binding_member_info = new BindingMemberInfo(dataMember);
			this.datasource_update_mode = dataSourceUpdateMode;
			this.null_value = nullValue;
			this.format_string = formatString;
			this.format_info = formatInfo;
		}

		/// <summary>Occurs when the property of a control is bound to a data value.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000033 RID: 51
		// (add) Token: 0x06000345 RID: 837 RVA: 0x00011F88 File Offset: 0x00010188
		// (remove) Token: 0x06000346 RID: 838 RVA: 0x00011FA4 File Offset: 0x000101A4
		public event ConvertEventHandler Format;

		/// <summary>Occurs when the value of a data-bound control changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000034 RID: 52
		// (add) Token: 0x06000347 RID: 839 RVA: 0x00011FC0 File Offset: 0x000101C0
		// (remove) Token: 0x06000348 RID: 840 RVA: 0x00011FDC File Offset: 0x000101DC
		public event ConvertEventHandler Parse;

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Binding.FormattingEnabled" /> property is set to true and a binding operation is complete, such as when data is pushed from the control to the data source or vice versa</summary>
		// Token: 0x14000035 RID: 53
		// (add) Token: 0x06000349 RID: 841 RVA: 0x00011FF8 File Offset: 0x000101F8
		// (remove) Token: 0x0600034A RID: 842 RVA: 0x00012014 File Offset: 0x00010214
		public event BindingCompleteEventHandler BindingComplete;

		/// <summary>Gets the control the <see cref="T:System.Windows.Forms.Binding" /> is associated with.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.IBindableComponent" /> the <see cref="T:System.Windows.Forms.Binding" /> is associated with.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00012030 File Offset: 0x00010230
		[DefaultValue(null)]
		public IBindableComponent BindableComponent
		{
			get
			{
				return this.control;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.BindingManagerBase" /> for this <see cref="T:System.Windows.Forms.Binding" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.BindingManagerBase" /> that manages this <see cref="T:System.Windows.Forms.Binding" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600034C RID: 844 RVA: 0x00012038 File Offset: 0x00010238
		public BindingManagerBase BindingManagerBase
		{
			get
			{
				return this.manager;
			}
		}

		/// <summary>Gets an object that contains information about this binding based on the <paramref name="dataMember" /> parameter in the <see cref="Overload:System.Windows.Forms.Binding.#ctor" /> constructor.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.BindingMemberInfo" /> that contains information about this <see cref="T:System.Windows.Forms.Binding" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00012040 File Offset: 0x00010240
		public BindingMemberInfo BindingMemberInfo
		{
			get
			{
				return this.binding_member_info;
			}
		}

		/// <summary>Gets the control that the binding belongs to.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that the binding belongs to.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00012048 File Offset: 0x00010248
		[DefaultValue(null)]
		public Control Control
		{
			get
			{
				return this.control as Control;
			}
		}

		/// <summary>Gets or sets when changes to the data source are propagated to the bound control property.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ControlUpdateMode" /> values. The default is <see cref="F:System.Windows.Forms.ControlUpdateMode.OnPropertyChanged" />.</returns>
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00012058 File Offset: 0x00010258
		// (set) Token: 0x06000350 RID: 848 RVA: 0x00012060 File Offset: 0x00010260
		[DefaultValue(ControlUpdateMode.OnPropertyChanged)]
		public ControlUpdateMode ControlUpdateMode
		{
			get
			{
				return this.control_update_mode;
			}
			set
			{
				this.control_update_mode = value;
			}
		}

		/// <summary>Gets the data source for this binding.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the data source.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0001206C File Offset: 0x0001026C
		public object DataSource
		{
			get
			{
				return this.data_source;
			}
		}

		/// <summary>Gets or sets a value that indicates when changes to the bound control property are propagated to the data source.</summary>
		/// <returns>A value that indicates when changes are propagated. The default is <see cref="F:System.Windows.Forms.DataSourceUpdateMode.OnValidation" />.</returns>
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000352 RID: 850 RVA: 0x00012074 File Offset: 0x00010274
		// (set) Token: 0x06000353 RID: 851 RVA: 0x0001207C File Offset: 0x0001027C
		[DefaultValue(DataSourceUpdateMode.OnValidation)]
		public DataSourceUpdateMode DataSourceUpdateMode
		{
			get
			{
				return this.datasource_update_mode;
			}
			set
			{
				this.datasource_update_mode = value;
			}
		}

		/// <summary>Gets or sets the value to be stored in the data source if the control value is null or empty.</summary>
		/// <returns>The <see cref="T:System.Object" /> to be stored in the data source when the control property is empty or null. The default is <see cref="T:System.DBNull" /> for value types and null for non-value types.</returns>
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000354 RID: 852 RVA: 0x00012088 File Offset: 0x00010288
		// (set) Token: 0x06000355 RID: 853 RVA: 0x00012090 File Offset: 0x00010290
		public object DataSourceNullValue
		{
			get
			{
				return this.datasource_null_value;
			}
			set
			{
				this.datasource_null_value = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether type conversion and formatting is applied to the control property data.</summary>
		/// <returns>true if type conversion and formatting of control property data is enabled; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0001209C File Offset: 0x0001029C
		// (set) Token: 0x06000357 RID: 855 RVA: 0x000120A4 File Offset: 0x000102A4
		[DefaultValue(false)]
		public bool FormattingEnabled
		{
			get
			{
				return this.formatting_enabled;
			}
			set
			{
				if (this.formatting_enabled == value)
				{
					return;
				}
				this.formatting_enabled = value;
				this.PushData();
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.IFormatProvider" /> that provides custom formatting behavior.</summary>
		/// <returns>The <see cref="T:System.IFormatProvider" /> implementation that provides custom formatting behavior.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000358 RID: 856 RVA: 0x000120C0 File Offset: 0x000102C0
		// (set) Token: 0x06000359 RID: 857 RVA: 0x000120C8 File Offset: 0x000102C8
		[DefaultValue(null)]
		public IFormatProvider FormatInfo
		{
			get
			{
				return this.format_info;
			}
			set
			{
				if (value == this.format_info)
				{
					return;
				}
				this.format_info = value;
				if (this.formatting_enabled)
				{
					this.PushData();
				}
			}
		}

		/// <summary>Gets or sets the format specifier characters that indicate how a value is to be displayed.</summary>
		/// <returns>The string of format specifier characters that indicate how a value is to be displayed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600035A RID: 858 RVA: 0x000120F0 File Offset: 0x000102F0
		// (set) Token: 0x0600035B RID: 859 RVA: 0x000120F8 File Offset: 0x000102F8
		public string FormatString
		{
			get
			{
				return this.format_string;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (value == this.format_string)
				{
					return;
				}
				this.format_string = value;
				if (this.formatting_enabled)
				{
					this.PushData();
				}
			}
		}

		/// <summary>Gets a value indicating whether the binding is active.</summary>
		/// <returns>true if the binding is active; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600035C RID: 860 RVA: 0x00012134 File Offset: 0x00010334
		public bool IsBinding
		{
			get
			{
				return this.manager != null && !this.manager.IsSuspended && this.is_binding;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Object" /> to be set as the control property when the data source contains a <see cref="T:System.DBNull" /> value. </summary>
		/// <returns>The <see cref="T:System.Object" /> to be set as the control property when the data source contains a <see cref="T:System.DBNull" /> value. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0001215C File Offset: 0x0001035C
		// (set) Token: 0x0600035E RID: 862 RVA: 0x00012164 File Offset: 0x00010364
		public object NullValue
		{
			get
			{
				return this.null_value;
			}
			set
			{
				if (value == this.null_value)
				{
					return;
				}
				this.null_value = value;
				if (this.formatting_enabled)
				{
					this.PushData();
				}
			}
		}

		/// <summary>Gets or sets the name of the control's data-bound property.</summary>
		/// <returns>The name of a control property to bind to.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0001218C File Offset: 0x0001038C
		[DefaultValue("")]
		public string PropertyName
		{
			get
			{
				return this.property_name;
			}
		}

		/// <summary>Sets the control property to the value read from the data source.</summary>
		// Token: 0x06000360 RID: 864 RVA: 0x00012194 File Offset: 0x00010394
		public void ReadValue()
		{
			this.PushData(true);
		}

		/// <summary>Reads the current value from the control property and writes it to the data source.</summary>
		// Token: 0x06000361 RID: 865 RVA: 0x000121A0 File Offset: 0x000103A0
		public void WriteValue()
		{
			this.PullData(true);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Binding.BindingComplete" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.BindingCompleteEventArgs" />  that contains the event data. </param>
		// Token: 0x06000362 RID: 866 RVA: 0x000121AC File Offset: 0x000103AC
		protected virtual void OnBindingComplete(BindingCompleteEventArgs e)
		{
			if (this.BindingComplete != null)
			{
				this.BindingComplete(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Binding.Format" /> event.</summary>
		/// <param name="cevent">A <see cref="T:System.Windows.Forms.ConvertEventArgs" /> that contains the event data. </param>
		// Token: 0x06000363 RID: 867 RVA: 0x000121C8 File Offset: 0x000103C8
		protected virtual void OnFormat(ConvertEventArgs cevent)
		{
			if (this.Format != null)
			{
				this.Format(this, cevent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Binding.Parse" /> event.</summary>
		/// <param name="cevent">A <see cref="T:System.Windows.Forms.ConvertEventArgs" /> that contains the event data. </param>
		// Token: 0x06000364 RID: 868 RVA: 0x000121E4 File Offset: 0x000103E4
		protected virtual void OnParse(ConvertEventArgs cevent)
		{
			if (this.Parse != null)
			{
				this.Parse(this, cevent);
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000365 RID: 869 RVA: 0x00012200 File Offset: 0x00010400
		internal string DataMember
		{
			get
			{
				return this.data_member;
			}
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00012208 File Offset: 0x00010408
		internal void SetControl(IBindableComponent control)
		{
			if (control == this.control)
			{
				return;
			}
			this.control_property = TypeDescriptor.GetProperties(control).Find(this.property_name, true);
			if (this.control_property == null)
			{
				throw new ArgumentException("Cannot bind to property '" + this.property_name + "' on target control.");
			}
			if (this.control_property.IsReadOnly)
			{
				throw new ArgumentException("Cannot bind to property '" + this.property_name + "' because it is read only.");
			}
			this.data_type = this.control_property.PropertyType;
			Control control2 = control as Control;
			if (control2 != null)
			{
				control2.Validating += new CancelEventHandler(this.ControlValidatingHandler);
				if (!control2.IsHandleCreated)
				{
					control2.HandleCreated += new EventHandler(this.ControlCreatedHandler);
				}
			}
			EventDescriptor propertyChangedEvent = this.GetPropertyChangedEvent(control, this.property_name);
			if (propertyChangedEvent != null)
			{
				propertyChangedEvent.AddEventHandler(control, new EventHandler(this.ControlPropertyChangedHandler));
			}
			this.control = control;
			this.UpdateIsBinding();
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0001230C File Offset: 0x0001050C
		internal void Check()
		{
			if (this.control == null || this.control.BindingContext == null)
			{
				return;
			}
			if (this.manager == null)
			{
				this.manager = this.control.BindingContext[this.data_source, this.binding_member_info.BindingPath];
				if (this.manager.Position > -1 && this.binding_member_info.BindingField != string.Empty && TypeDescriptor.GetProperties(this.manager.Current).Find(this.binding_member_info.BindingField, true) == null)
				{
					throw new ArgumentException("Cannot bind to property '" + this.binding_member_info.BindingField + "' on DataSource.", "dataMember");
				}
				this.manager.AddBinding(this);
				this.manager.PositionChanged += new EventHandler(this.PositionChangedHandler);
				if (this.manager is PropertyManager)
				{
					EventDescriptor propertyChangedEvent = this.GetPropertyChangedEvent(this.manager.Current, this.binding_member_info.BindingField);
					if (propertyChangedEvent != null)
					{
						propertyChangedEvent.AddEventHandler(this.manager.Current, new EventHandler(this.SourcePropertyChangedHandler));
					}
				}
			}
			if (this.manager.Position == -1)
			{
				return;
			}
			if (!this.checked_isnull)
			{
				this.is_null_desc = TypeDescriptor.GetProperties(this.manager.Current).Find(this.property_name + "IsNull", false);
				this.checked_isnull = true;
			}
			this.PushData();
		}

		// Token: 0x06000368 RID: 872 RVA: 0x000124A8 File Offset: 0x000106A8
		internal bool PullData()
		{
			return this.PullData(false);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x000124B4 File Offset: 0x000106B4
		private bool PullData(bool force)
		{
			if (!this.IsBinding || this.manager.Current == null)
			{
				return true;
			}
			if (!force && this.datasource_update_mode == DataSourceUpdateMode.Never)
			{
				return true;
			}
			this.data = this.control_property.GetValue(this.control);
			if (this.data == null)
			{
				this.data = this.datasource_null_value;
			}
			try
			{
				this.SetPropertyValue(this.data);
			}
			catch (Exception ex)
			{
				if (this.formatting_enabled)
				{
					this.FireBindingComplete(BindingCompleteContext.DataSourceUpdate, ex, ex.Message);
					return false;
				}
				throw ex;
			}
			if (this.formatting_enabled)
			{
				this.FireBindingComplete(BindingCompleteContext.DataSourceUpdate, null, null);
			}
			return true;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0001258C File Offset: 0x0001078C
		internal void PushData()
		{
			this.PushData(false);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00012598 File Offset: 0x00010798
		private void PushData(bool force)
		{
			if (this.manager == null || this.manager.IsSuspended || this.manager.Count == 0 || this.manager.Position == -1)
			{
				return;
			}
			if (!force && this.control_update_mode == ControlUpdateMode.Never)
			{
				return;
			}
			if (this.is_null_desc != null)
			{
				bool flag = (bool)this.is_null_desc.GetValue(this.manager.Current);
				if (flag)
				{
					this.data = Convert.DBNull;
					return;
				}
			}
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this.manager.Current).Find(this.binding_member_info.BindingField, true);
			if (propertyDescriptor == null)
			{
				this.data = this.manager.Current;
			}
			else
			{
				this.data = propertyDescriptor.GetValue(this.manager.Current);
			}
			if ((this.data == null || this.data == DBNull.Value) && this.null_value != null)
			{
				this.data = this.null_value;
			}
			try
			{
				this.data = this.FormatData(this.data);
				this.SetControlValue(this.data);
			}
			catch (Exception ex)
			{
				if (this.formatting_enabled)
				{
					this.FireBindingComplete(BindingCompleteContext.ControlUpdate, ex, ex.Message);
					return;
				}
				throw ex;
			}
			if (this.formatting_enabled)
			{
				this.FireBindingComplete(BindingCompleteContext.ControlUpdate, null, null);
			}
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00012730 File Offset: 0x00010930
		internal void UpdateIsBinding()
		{
			this.is_binding = false;
			if (this.control == null || (this.control is Control && !((Control)this.control).IsHandleCreated))
			{
				return;
			}
			this.is_binding = true;
			this.PushData();
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00012784 File Offset: 0x00010984
		private void SetControlValue(object data)
		{
			this.control_property.SetValue(this.control, data);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00012798 File Offset: 0x00010998
		private void SetPropertyValue(object data)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this.manager.Current).Find(this.binding_member_info.BindingField, true);
			if (propertyDescriptor.IsReadOnly)
			{
				return;
			}
			data = this.ParseData(data, propertyDescriptor.PropertyType);
			propertyDescriptor.SetValue(this.manager.Current, data);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x000127F4 File Offset: 0x000109F4
		private void ControlValidatingHandler(object sender, CancelEventArgs e)
		{
			if (this.datasource_update_mode != DataSourceUpdateMode.OnValidation)
			{
				return;
			}
			bool flag = true;
			try
			{
				flag = this.PullData();
			}
			catch
			{
				flag = false;
			}
			e.Cancel = !flag;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0001284C File Offset: 0x00010A4C
		private void ControlCreatedHandler(object o, EventArgs args)
		{
			this.UpdateIsBinding();
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00012854 File Offset: 0x00010A54
		private void PositionChangedHandler(object sender, EventArgs e)
		{
			this.Check();
			this.PushData();
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00012864 File Offset: 0x00010A64
		private EventDescriptor GetPropertyChangedEvent(object o, string property_name)
		{
			if (o == null || property_name == null || property_name.Length == 0)
			{
				return null;
			}
			string text = property_name + "Changed";
			Type typeFromHandle = typeof(EventHandler);
			EventDescriptor eventDescriptor = null;
			foreach (object obj in TypeDescriptor.GetEvents(o))
			{
				EventDescriptor eventDescriptor2 = (EventDescriptor)obj;
				if (eventDescriptor2.Name == text && eventDescriptor2.EventType == typeFromHandle)
				{
					eventDescriptor = eventDescriptor2;
					break;
				}
			}
			return eventDescriptor;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0001292C File Offset: 0x00010B2C
		private void SourcePropertyChangedHandler(object o, EventArgs args)
		{
			this.PushData();
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00012934 File Offset: 0x00010B34
		private void ControlPropertyChangedHandler(object o, EventArgs args)
		{
			if (this.datasource_update_mode != DataSourceUpdateMode.OnPropertyChanged)
			{
				return;
			}
			this.PullData();
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0001294C File Offset: 0x00010B4C
		private object ParseData(object data, Type data_type)
		{
			ConvertEventArgs convertEventArgs = new ConvertEventArgs(data, data_type);
			this.OnParse(convertEventArgs);
			if (data_type.IsInstanceOfType(convertEventArgs.Value))
			{
				return convertEventArgs.Value;
			}
			if (convertEventArgs.Value == Convert.DBNull)
			{
				return convertEventArgs.Value;
			}
			if (convertEventArgs.Value == null)
			{
				bool flag = data_type.IsGenericType && !data_type.ContainsGenericParameters && data_type.GetGenericTypeDefinition() == typeof(Nullable);
				return (!data_type.IsValueType || flag) ? null : Convert.DBNull;
			}
			return this.ConvertData(convertEventArgs.Value, data_type);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x000129F8 File Offset: 0x00010BF8
		private object FormatData(object data)
		{
			ConvertEventArgs convertEventArgs = new ConvertEventArgs(data, this.data_type);
			this.OnFormat(convertEventArgs);
			if (this.data_type.IsInstanceOfType(convertEventArgs.Value))
			{
				return convertEventArgs.Value;
			}
			if (this.formatting_enabled)
			{
				if ((convertEventArgs.Value == null || convertEventArgs.Value == Convert.DBNull) && this.null_value != null)
				{
					return this.null_value;
				}
				if (convertEventArgs.Value is IFormattable && this.data_type == typeof(string))
				{
					IFormattable formattable = (IFormattable)convertEventArgs.Value;
					return formattable.ToString(this.format_string, this.format_info);
				}
			}
			if (convertEventArgs.Value == null && this.data_type == typeof(object))
			{
				return Convert.DBNull;
			}
			return this.ConvertData(data, this.data_type);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00012AE8 File Offset: 0x00010CE8
		private object ConvertData(object data, Type data_type)
		{
			if (data == null)
			{
				return null;
			}
			TypeConverter typeConverter = TypeDescriptor.GetConverter(data.GetType());
			if (typeConverter != null && typeConverter.CanConvertTo(data_type))
			{
				return typeConverter.ConvertTo(data, data_type);
			}
			typeConverter = TypeDescriptor.GetConverter(data_type);
			if (typeConverter != null && typeConverter.CanConvertFrom(data.GetType()))
			{
				return typeConverter.ConvertFrom(data);
			}
			if (data is IConvertible)
			{
				object obj = Convert.ChangeType(data, data_type);
				if (data_type.IsInstanceOfType(obj))
				{
					return obj;
				}
			}
			return null;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00012B6C File Offset: 0x00010D6C
		private void FireBindingComplete(BindingCompleteContext context, Exception exc, string error_message)
		{
			BindingCompleteEventArgs bindingCompleteEventArgs = new BindingCompleteEventArgs(this, (exc != null) ? BindingCompleteState.Exception : BindingCompleteState.Success, context);
			if (exc != null)
			{
				bindingCompleteEventArgs.SetException(exc);
				bindingCompleteEventArgs.SetErrorText(error_message);
			}
			this.OnBindingComplete(bindingCompleteEventArgs);
		}

		// Token: 0x0400060D RID: 1549
		private string property_name;

		// Token: 0x0400060E RID: 1550
		private object data_source;

		// Token: 0x0400060F RID: 1551
		private string data_member;

		// Token: 0x04000610 RID: 1552
		private bool is_binding;

		// Token: 0x04000611 RID: 1553
		private bool checked_isnull;

		// Token: 0x04000612 RID: 1554
		private BindingMemberInfo binding_member_info;

		// Token: 0x04000613 RID: 1555
		private IBindableComponent control;

		// Token: 0x04000614 RID: 1556
		private BindingManagerBase manager;

		// Token: 0x04000615 RID: 1557
		private PropertyDescriptor control_property;

		// Token: 0x04000616 RID: 1558
		private PropertyDescriptor is_null_desc;

		// Token: 0x04000617 RID: 1559
		private object data;

		// Token: 0x04000618 RID: 1560
		private Type data_type;

		// Token: 0x04000619 RID: 1561
		private DataSourceUpdateMode datasource_update_mode;

		// Token: 0x0400061A RID: 1562
		private ControlUpdateMode control_update_mode;

		// Token: 0x0400061B RID: 1563
		private object datasource_null_value = Convert.DBNull;

		// Token: 0x0400061C RID: 1564
		private object null_value;

		// Token: 0x0400061D RID: 1565
		private IFormatProvider format_info;

		// Token: 0x0400061E RID: 1566
		private string format_string;

		// Token: 0x0400061F RID: 1567
		private bool formatting_enabled;
	}
}
