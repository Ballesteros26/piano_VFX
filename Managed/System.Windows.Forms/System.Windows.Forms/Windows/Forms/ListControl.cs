using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides a common implementation of members for the <see cref="T:System.Windows.Forms.ListBox" /> and <see cref="T:System.Windows.Forms.ComboBox" /> classes.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200021A RID: 538
	[LookupBindingProperties("DataSource", "DisplayMember", "ValueMember", "SelectedValue")]
	[ClassInterface(1)]
	[ComVisible(true)]
	public abstract class ListControl : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListControl" /> class. </summary>
		// Token: 0x060021BF RID: 8639 RVA: 0x0007E504 File Offset: 0x0007C704
		protected ListControl()
		{
			this.value_member = new BindingMemberInfo(string.Empty);
			this.display_member = string.Empty;
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.StandardClick | ControlStyles.UseTextForAccessibility, false);
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x0007E54C File Offset: 0x0007C74C
		// Note: this type is marked as 'beforefieldinit'.
		static ListControl()
		{
			ListControl.DataSourceChangedEvent = new object();
			ListControl.DisplayMemberChangedEvent = new object();
			ListControl.FormatEvent = new object();
			ListControl.FormatInfoChangedEvent = new object();
			ListControl.FormatStringChangedEvent = new object();
			ListControl.FormattingEnabledChangedEvent = new object();
			ListControl.SelectedValueChangedEvent = new object();
			ListControl.ValueMemberChangedEvent = new object();
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ListControl.DataSource" /> changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000207 RID: 519
		// (add) Token: 0x060021C1 RID: 8641 RVA: 0x0007E5AC File Offset: 0x0007C7AC
		// (remove) Token: 0x060021C2 RID: 8642 RVA: 0x0007E5C0 File Offset: 0x0007C7C0
		public event EventHandler DataSourceChanged
		{
			add
			{
				base.Events.AddHandler(ListControl.DataSourceChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListControl.DataSourceChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ListControl.DisplayMember" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000208 RID: 520
		// (add) Token: 0x060021C3 RID: 8643 RVA: 0x0007E5D4 File Offset: 0x0007C7D4
		// (remove) Token: 0x060021C4 RID: 8644 RVA: 0x0007E5E8 File Offset: 0x0007C7E8
		public event EventHandler DisplayMemberChanged
		{
			add
			{
				base.Events.AddHandler(ListControl.DisplayMemberChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListControl.DisplayMemberChangedEvent, value);
			}
		}

		/// <summary>Occurs when the control is bound to a data value.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000209 RID: 521
		// (add) Token: 0x060021C5 RID: 8645 RVA: 0x0007E5FC File Offset: 0x0007C7FC
		// (remove) Token: 0x060021C6 RID: 8646 RVA: 0x0007E610 File Offset: 0x0007C810
		public event ListControlConvertEventHandler Format
		{
			add
			{
				base.Events.AddHandler(ListControl.FormatEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListControl.FormatEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ListControl.FormatInfo" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400020A RID: 522
		// (add) Token: 0x060021C7 RID: 8647 RVA: 0x0007E624 File Offset: 0x0007C824
		// (remove) Token: 0x060021C8 RID: 8648 RVA: 0x0007E638 File Offset: 0x0007C838
		[EditorBrowsable(2)]
		[Browsable(false)]
		public event EventHandler FormatInfoChanged
		{
			add
			{
				base.Events.AddHandler(ListControl.FormatInfoChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListControl.FormatInfoChangedEvent, value);
			}
		}

		/// <summary>Occurs when value of the <see cref="P:System.Windows.Forms.ListControl.FormatString" /> property changes</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400020B RID: 523
		// (add) Token: 0x060021C9 RID: 8649 RVA: 0x0007E64C File Offset: 0x0007C84C
		// (remove) Token: 0x060021CA RID: 8650 RVA: 0x0007E660 File Offset: 0x0007C860
		public event EventHandler FormatStringChanged
		{
			add
			{
				base.Events.AddHandler(ListControl.FormatStringChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListControl.FormatStringChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ListControl.FormattingEnabled" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400020C RID: 524
		// (add) Token: 0x060021CB RID: 8651 RVA: 0x0007E674 File Offset: 0x0007C874
		// (remove) Token: 0x060021CC RID: 8652 RVA: 0x0007E688 File Offset: 0x0007C888
		public event EventHandler FormattingEnabledChanged
		{
			add
			{
				base.Events.AddHandler(ListControl.FormattingEnabledChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListControl.FormattingEnabledChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ListControl.SelectedValue" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400020D RID: 525
		// (add) Token: 0x060021CD RID: 8653 RVA: 0x0007E69C File Offset: 0x0007C89C
		// (remove) Token: 0x060021CE RID: 8654 RVA: 0x0007E6B0 File Offset: 0x0007C8B0
		public event EventHandler SelectedValueChanged
		{
			add
			{
				base.Events.AddHandler(ListControl.SelectedValueChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListControl.SelectedValueChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ListControl.ValueMember" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400020E RID: 526
		// (add) Token: 0x060021CF RID: 8655 RVA: 0x0007E6C4 File Offset: 0x0007C8C4
		// (remove) Token: 0x060021D0 RID: 8656 RVA: 0x0007E6D8 File Offset: 0x0007C8D8
		public event EventHandler ValueMemberChanged
		{
			add
			{
				base.Events.AddHandler(ListControl.ValueMemberChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListControl.ValueMemberChangedEvent, value);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.IFormatProvider" /> that provides custom formatting behavior. </summary>
		/// <returns>The <see cref="T:System.IFormatProvider" /> implementation that provides custom formatting behavior.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x060021D1 RID: 8657 RVA: 0x0007E6EC File Offset: 0x0007C8EC
		// (set) Token: 0x060021D2 RID: 8658 RVA: 0x0007E6F4 File Offset: 0x0007C8F4
		[Browsable(false)]
		[DefaultValue(null)]
		[EditorBrowsable(2)]
		public IFormatProvider FormatInfo
		{
			get
			{
				return this.format_info;
			}
			set
			{
				if (this.format_info != value)
				{
					this.format_info = value;
					this.RefreshItems();
					this.OnFormatInfoChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the format-specifier characters that indicate how a value is to be displayed.</summary>
		/// <returns>The string of format-specifier characters that indicates how a value is to be displayed.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x060021D3 RID: 8659 RVA: 0x0007E728 File Offset: 0x0007C928
		// (set) Token: 0x060021D4 RID: 8660 RVA: 0x0007E730 File Offset: 0x0007C930
		[DefaultValue("")]
		[MergableProperty(false)]
		[Editor("System.Windows.Forms.Design.FormatStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string FormatString
		{
			get
			{
				return this.format_string;
			}
			set
			{
				if (this.format_string != value)
				{
					this.format_string = value;
					this.RefreshItems();
					this.OnFormatStringChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether formatting is applied to the <see cref="P:System.Windows.Forms.ListControl.DisplayMember" /> property of the <see cref="T:System.Windows.Forms.ListControl" />.</summary>
		/// <returns>true if formatting of the <see cref="P:System.Windows.Forms.ListControl.DisplayMember" /> property is enabled; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x060021D5 RID: 8661 RVA: 0x0007E75C File Offset: 0x0007C95C
		// (set) Token: 0x060021D6 RID: 8662 RVA: 0x0007E764 File Offset: 0x0007C964
		[DefaultValue(false)]
		public bool FormattingEnabled
		{
			get
			{
				return this.formatting_enabled;
			}
			set
			{
				if (this.formatting_enabled != value)
				{
					this.formatting_enabled = value;
					this.RefreshItems();
					this.OnFormattingEnabledChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the data source for this <see cref="T:System.Windows.Forms.ListControl" />.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IList" /> or <see cref="T:System.ComponentModel.IListSource" /> interfaces, such as a <see cref="T:System.Data.DataSet" /> or an <see cref="T:System.Array" />. The default is null.</returns>
		/// <exception cref="T:System.ArgumentException">The assigned value does not implement the <see cref="T:System.Collections.IList" /> or <see cref="T:System.ComponentModel.IListSource" /> interfaces.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x060021D7 RID: 8663 RVA: 0x0007E798 File Offset: 0x0007C998
		// (set) Token: 0x060021D8 RID: 8664 RVA: 0x0007E7A0 File Offset: 0x0007C9A0
		[DefaultValue(null)]
		[RefreshProperties(2)]
		[AttributeProvider(typeof(IListSource))]
		[MWFCategory("Data")]
		public object DataSource
		{
			get
			{
				return this.data_source;
			}
			set
			{
				if (this.data_source == value)
				{
					return;
				}
				if (value == null)
				{
					this.display_member = string.Empty;
				}
				else if (!(value is IList) && !(value is IListSource))
				{
					throw new Exception("Complex DataBinding accepts as a data source either an IList or an IListSource");
				}
				this.data_source = value;
				this.ConnectToDataSource();
				this.OnDataSourceChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the property to display for this <see cref="T:System.Windows.Forms.ListControl" />.</summary>
		/// <returns>A <see cref="T:System.String" /> specifying the name of an object property that is contained in the collection specified by the <see cref="P:System.Windows.Forms.ListControl.DataSource" /> property. The default is an empty string (""). </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x060021D9 RID: 8665 RVA: 0x0007E80C File Offset: 0x0007CA0C
		// (set) Token: 0x060021DA RID: 8666 RVA: 0x0007E814 File Offset: 0x0007CA14
		[MWFCategory("Data")]
		[DefaultValue("")]
		[Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string DisplayMember
		{
			get
			{
				return this.display_member;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (this.display_member == value)
				{
					return;
				}
				this.display_member = value;
				this.ConnectToDataSource();
				this.OnDisplayMemberChanged(EventArgs.Empty);
			}
		}

		/// <summary>When overridden in a derived class, gets or sets the zero-based index of the currently selected item.</summary>
		/// <returns>A zero-based index of the currently selected item. A value of negative one (-1) is returned if no item is selected.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x060021DB RID: 8667
		// (set) Token: 0x060021DC RID: 8668
		public abstract int SelectedIndex { get; set; }

		/// <summary>Gets or sets the value of the member property specified by the <see cref="P:System.Windows.Forms.ListControl.ValueMember" /> property.</summary>
		/// <returns>An object containing the value of the member of the data source specified by the <see cref="P:System.Windows.Forms.ListControl.ValueMember" /> property.</returns>
		/// <exception cref="T:System.InvalidOperationException">The assigned value is null or the empty string ("").</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x060021DD RID: 8669 RVA: 0x0007E850 File Offset: 0x0007CA50
		// (set) Token: 0x060021DE RID: 8670 RVA: 0x0007E898 File Offset: 0x0007CA98
		[DefaultValue(null)]
		[DesignerSerializationVisibility(0)]
		[Bindable(1)]
		[Browsable(false)]
		public object SelectedValue
		{
			get
			{
				if (this.data_manager == null || this.SelectedIndex == -1)
				{
					return null;
				}
				object obj = this.data_manager[this.SelectedIndex];
				return this.FilterItemOnProperty(obj, this.ValueMember);
			}
			set
			{
				if (this.data_manager == null)
				{
					return;
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				PropertyDescriptorCollection itemProperties = this.data_manager.GetItemProperties();
				PropertyDescriptor propertyDescriptor = itemProperties.Find(this.ValueMember, true);
				for (int i = 0; i < this.data_manager.Count; i++)
				{
					if (value.Equals(propertyDescriptor.GetValue(this.data_manager[i])))
					{
						this.SelectedIndex = i;
						return;
					}
				}
				this.SelectedIndex = -1;
			}
		}

		/// <summary>Gets or sets the property to use as the actual value for the items in the <see cref="T:System.Windows.Forms.ListControl" />.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the name of an object property that is contained in the collection specified by the <see cref="P:System.Windows.Forms.ListControl.DataSource" /> property. The default is an empty string ("").</returns>
		/// <exception cref="T:System.ArgumentException">The specified property cannot be found on the object specified by the <see cref="P:System.Windows.Forms.ListControl.DataSource" /> property. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x060021DF RID: 8671 RVA: 0x0007E924 File Offset: 0x0007CB24
		// (set) Token: 0x060021E0 RID: 8672 RVA: 0x0007E934 File Offset: 0x0007CB34
		[Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[MWFCategory("Data")]
		[DefaultValue("")]
		public string ValueMember
		{
			get
			{
				return this.value_member.BindingMember;
			}
			set
			{
				BindingMemberInfo bindingMemberInfo = new BindingMemberInfo(value);
				if (this.value_member.Equals(bindingMemberInfo))
				{
					return;
				}
				this.value_member = bindingMemberInfo;
				if (this.display_member == string.Empty)
				{
					this.DisplayMember = this.value_member.BindingMember;
				}
				this.ConnectToDataSource();
				this.OnValueMemberChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets a value indicating whether the list enables selection of list items.</summary>
		/// <returns>true if the list enables list item selection; otherwise, false. The default is true.</returns>
		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x060021E1 RID: 8673 RVA: 0x0007E9A0 File Offset: 0x0007CBA0
		protected virtual bool AllowSelection
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x060021E2 RID: 8674 RVA: 0x0007E9A4 File Offset: 0x0007CBA4
		internal override bool ScaleChildrenInternal
		{
			get
			{
				return false;
			}
		}

		/// <summary>Retrieves the current value of the <see cref="T:System.Windows.Forms.ListControl" /> item, if it is a property of an object, given the item.</summary>
		/// <returns>The filtered object.</returns>
		/// <param name="item">The object the <see cref="T:System.Windows.Forms.ListControl" /> item is bound to.</param>
		// Token: 0x060021E3 RID: 8675 RVA: 0x0007E9A8 File Offset: 0x0007CBA8
		protected object FilterItemOnProperty(object item)
		{
			return this.FilterItemOnProperty(item, string.Empty);
		}

		/// <summary>Returns the current value of the <see cref="T:System.Windows.Forms.ListControl" /> item, if it is a property of an object given the item and the property name.</summary>
		/// <returns>The filtered object.</returns>
		/// <param name="item">The object the <see cref="T:System.Windows.Forms.ListControl" /> item is bound to.</param>
		/// <param name="field">The property name of the item the <see cref="T:System.Windows.Forms.ListControl" /> is bound to.</param>
		// Token: 0x060021E4 RID: 8676 RVA: 0x0007E9B8 File Offset: 0x0007CBB8
		protected object FilterItemOnProperty(object item, string field)
		{
			if (item == null)
			{
				return null;
			}
			if (field == null || field == string.Empty)
			{
				return item;
			}
			PropertyDescriptor propertyDescriptor;
			if (this.data_manager != null)
			{
				PropertyDescriptorCollection itemProperties = this.data_manager.GetItemProperties();
				propertyDescriptor = itemProperties.Find(field, true);
			}
			else
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(item);
				propertyDescriptor = properties.Find(field, true);
			}
			if (propertyDescriptor == null)
			{
				return item;
			}
			return propertyDescriptor.GetValue(item);
		}

		/// <summary>Returns the text representation of the specified item.</summary>
		/// <returns>If the <see cref="P:System.Windows.Forms.ListControl.DisplayMember" /> property is not specified, the value returned by <see cref="M:System.Windows.Forms.ListControl.GetItemText(System.Object)" /> is the value of the item's ToString method. Otherwise, the method returns the string value of the member specified in the <see cref="P:System.Windows.Forms.ListControl.DisplayMember" /> property for the object specified in the <paramref name="item" /> parameter.</returns>
		/// <param name="item">The object from which to get the contents to display. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060021E5 RID: 8677 RVA: 0x0007EA2C File Offset: 0x0007CC2C
		public string GetItemText(object item)
		{
			object obj = this.FilterItemOnProperty(item, this.DisplayMember);
			if (obj == null)
			{
				obj = item;
			}
			string text = obj.ToString();
			if (this.FormattingEnabled)
			{
				ListControlConvertEventArgs listControlConvertEventArgs = new ListControlConvertEventArgs(text, typeof(string), item);
				this.OnFormat(listControlConvertEventArgs);
				if (listControlConvertEventArgs.Value.ToString() != text)
				{
					return listControlConvertEventArgs.Value.ToString();
				}
				if (obj is IFormattable)
				{
					return ((IFormattable)obj).ToString((!string.IsNullOrEmpty(this.FormatString)) ? this.FormatString : null, this.FormatInfo);
				}
			}
			return text;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.CurrencyManager" /> associated with this control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.CurrencyManager" /> associated with this control. The default is null.</returns>
		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x060021E6 RID: 8678 RVA: 0x0007EAD8 File Offset: 0x0007CCD8
		protected CurrencyManager DataManager
		{
			get
			{
				return this.data_manager;
			}
		}

		/// <summary>Handles special input keys, such as PAGE UP, PAGE DOWN, HOME, END, and so on.</summary>
		/// <returns>true if the <paramref name="keyData" /> parameter specifies the <see cref="F:System.Windows.Forms.Keys.End" />, <see cref="F:System.Windows.Forms.Keys.Home" />, <see cref="F:System.Windows.Forms.Keys.PageUp" />, or <see cref="F:System.Windows.Forms.Keys.PageDown" /> key; false if the <paramref name="keyData" /> parameter specifies <see cref="F:System.Windows.Forms.Keys.Alt" />.</returns>
		/// <param name="keyData">One of the values of <see cref="T:System.Windows.Forms.Keys" />.</param>
		// Token: 0x060021E7 RID: 8679 RVA: 0x0007EAE0 File Offset: 0x0007CCE0
		protected override bool IsInputKey(Keys keyData)
		{
			switch (keyData)
			{
			case Keys.Space:
			case Keys.PageUp:
			case Keys.PageDown:
			case Keys.End:
			case Keys.Home:
			case Keys.Left:
			case Keys.Up:
			case Keys.Right:
			case Keys.Down:
				break;
			default:
				if (keyData != Keys.ShiftKey && keyData != Keys.ControlKey)
				{
					return false;
				}
				break;
			}
			return true;
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060021E8 RID: 8680 RVA: 0x0007EB34 File Offset: 0x0007CD34
		protected override void OnBindingContextChanged(EventArgs e)
		{
			base.OnBindingContextChanged(e);
			if (this.last_binding_context == this.BindingContext)
			{
				return;
			}
			this.last_binding_context = this.BindingContext;
			this.ConnectToDataSource();
			if (this.DataManager != null)
			{
				this.SetItemsCore(this.DataManager.List);
				if (this.AllowSelection)
				{
					this.SelectedIndex = this.DataManager.Position;
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.DataSourceChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060021E9 RID: 8681 RVA: 0x0007EBA4 File Offset: 0x0007CDA4
		protected virtual void OnDataSourceChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListControl.DataSourceChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.DisplayMemberChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060021EA RID: 8682 RVA: 0x0007EBD8 File Offset: 0x0007CDD8
		protected virtual void OnDisplayMemberChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListControl.DisplayMemberChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.Format" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ListControlConvertEventArgs" /> that contains the event data. </param>
		// Token: 0x060021EB RID: 8683 RVA: 0x0007EC0C File Offset: 0x0007CE0C
		protected virtual void OnFormat(ListControlConvertEventArgs e)
		{
			ListControlConvertEventHandler listControlConvertEventHandler = (ListControlConvertEventHandler)base.Events[ListControl.FormatEvent];
			if (listControlConvertEventHandler != null)
			{
				listControlConvertEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.FormatInfoChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060021EC RID: 8684 RVA: 0x0007EC40 File Offset: 0x0007CE40
		protected virtual void OnFormatInfoChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListControl.FormatInfoChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.FormatStringChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060021ED RID: 8685 RVA: 0x0007EC74 File Offset: 0x0007CE74
		protected virtual void OnFormatStringChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListControl.FormatStringChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.FormattingEnabledChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060021EE RID: 8686 RVA: 0x0007ECA8 File Offset: 0x0007CEA8
		protected virtual void OnFormattingEnabledChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListControl.FormattingEnabledChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.SelectedValueChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060021EF RID: 8687 RVA: 0x0007ECDC File Offset: 0x0007CEDC
		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			if (this.data_manager == null)
			{
				return;
			}
			if (this.data_manager.Position == this.SelectedIndex)
			{
				return;
			}
			this.data_manager.Position = this.SelectedIndex;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.SelectedValueChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060021F0 RID: 8688 RVA: 0x0007ED20 File Offset: 0x0007CF20
		protected virtual void OnSelectedValueChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListControl.SelectedValueChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.ValueMemberChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060021F1 RID: 8689 RVA: 0x0007ED54 File Offset: 0x0007CF54
		protected virtual void OnValueMemberChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListControl.ValueMemberChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>When overridden in a derived class, resynchronizes the data of the object at the specified index with the contents of the data source.</summary>
		/// <param name="index">The zero-based index of the item whose data to refresh. </param>
		// Token: 0x060021F2 RID: 8690
		protected abstract void RefreshItem(int index);

		/// <summary>When overridden in a derived class, resynchronizes the item data with the contents of the data source.</summary>
		// Token: 0x060021F3 RID: 8691 RVA: 0x0007ED88 File Offset: 0x0007CF88
		protected virtual void RefreshItems()
		{
		}

		/// <summary>When overridden in a derived class, sets the object with the specified index in the derived class.</summary>
		/// <param name="index">The array index of the object.</param>
		/// <param name="value">The object.</param>
		// Token: 0x060021F4 RID: 8692 RVA: 0x0007ED8C File Offset: 0x0007CF8C
		protected virtual void SetItemCore(int index, object value)
		{
		}

		/// <summary>When overridden in a derived class, sets the specified array of objects in a collection in the derived class.</summary>
		/// <param name="items">An array of items.</param>
		// Token: 0x060021F5 RID: 8693
		protected abstract void SetItemsCore(IList items);

		// Token: 0x060021F6 RID: 8694 RVA: 0x0007ED90 File Offset: 0x0007CF90
		internal void BindDataItems()
		{
			IList list2;
			if (this.data_manager != null)
			{
				IList list = this.data_manager.List;
				list2 = list;
			}
			else
			{
				list2 = new object[0];
			}
			this.SetItemsCore(list2);
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x0007EDC8 File Offset: 0x0007CFC8
		private void ConnectToDataSource()
		{
			if (this.BindingContext == null)
			{
				return;
			}
			CurrencyManager currencyManager = null;
			if (this.data_source != null)
			{
				currencyManager = (CurrencyManager)this.BindingContext[this.data_source];
			}
			if (currencyManager != this.data_manager)
			{
				if (this.data_manager != null)
				{
					this.data_manager.PositionChanged -= new EventHandler(this.OnPositionChanged);
					this.data_manager.ItemChanged -= this.OnItemChanged;
				}
				if (currencyManager != null)
				{
					currencyManager.PositionChanged += new EventHandler(this.OnPositionChanged);
					currencyManager.ItemChanged += this.OnItemChanged;
				}
				this.data_manager = currencyManager;
			}
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x0007EE7C File Offset: 0x0007D07C
		private void OnItemChanged(object sender, ItemChangedEventArgs e)
		{
			if (e.Index == -1)
			{
				this.SetItemsCore(this.data_manager.List);
			}
			else
			{
				this.RefreshItem(e.Index);
			}
			if (this.AllowSelection && this.SelectedIndex == -1 && this.data_manager.Count == 1)
			{
				this.SelectedIndex = this.data_manager.Position;
			}
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x0007EEF0 File Offset: 0x0007D0F0
		private void OnPositionChanged(object sender, EventArgs e)
		{
			if (this.AllowSelection && this.data_manager.Count > 1)
			{
				this.SelectedIndex = this.data_manager.Position;
			}
		}

		// Token: 0x040011DD RID: 4573
		private object data_source;

		// Token: 0x040011DE RID: 4574
		private BindingMemberInfo value_member;

		// Token: 0x040011DF RID: 4575
		private string display_member;

		// Token: 0x040011E0 RID: 4576
		private CurrencyManager data_manager;

		// Token: 0x040011E1 RID: 4577
		private BindingContext last_binding_context;

		// Token: 0x040011E2 RID: 4578
		private IFormatProvider format_info;

		// Token: 0x040011E3 RID: 4579
		private string format_string = string.Empty;

		// Token: 0x040011E4 RID: 4580
		private bool formatting_enabled;
	}
}
