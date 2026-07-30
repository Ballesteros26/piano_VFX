using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies the appearance, text formatting, and behavior of a <see cref="T:System.Windows.Forms.DataGrid" /> control column. This class is abstract.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000C5 RID: 197
	[ToolboxItem(false)]
	[DefaultProperty("Header")]
	[DesignTimeVisible(false)]
	public abstract class DataGridColumnStyle : Component, IDataGridColumnStyleEditingNotificationService
	{
		/// <summary>In a derived class, initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> class.</summary>
		// Token: 0x06000D1B RID: 3355 RVA: 0x00035EF8 File Offset: 0x000340F8
		public DataGridColumnStyle()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> class with the specified <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
		/// <param name="prop">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that provides the attributes for the column. </param>
		// Token: 0x06000D1C RID: 3356 RVA: 0x00035F04 File Offset: 0x00034104
		public DataGridColumnStyle(PropertyDescriptor prop)
		{
			this.property_descriptor = prop;
			this.fontheight = -1;
			this.table_style = null;
			this.header_text = string.Empty;
			this.mapping_name = string.Empty;
			this.null_text = DataGridColumnStyle.def_null_text;
			this.accesible_object = new DataGridColumnStyle.DataGridColumnHeaderAccessibleObject(this);
			this._readonly = prop != null && prop.IsReadOnly;
			this.width = -1;
			this.grid = null;
			this.is_default = false;
			this.alignment = HorizontalAlignment.Left;
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x00035F90 File Offset: 0x00034190
		// Note: this type is marked as 'beforefieldinit'.
		static DataGridColumnStyle()
		{
			DataGridColumnStyle.AlignmentChangedEvent = new object();
			DataGridColumnStyle.FontChangedEvent = new object();
			DataGridColumnStyle.HeaderTextChangedEvent = new object();
			DataGridColumnStyle.MappingNameChangedEvent = new object();
			DataGridColumnStyle.NullTextChangedEvent = new object();
			DataGridColumnStyle.PropertyDescriptorChangedEvent = new object();
			DataGridColumnStyle.ReadOnlyChangedEvent = new object();
			DataGridColumnStyle.WidthChangedEvent = new object();
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridColumnStyle.Alignment" /> property value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000CF RID: 207
		// (add) Token: 0x06000D1E RID: 3358 RVA: 0x00035FF8 File Offset: 0x000341F8
		// (remove) Token: 0x06000D1F RID: 3359 RVA: 0x0003600C File Offset: 0x0003420C
		public event EventHandler AlignmentChanged
		{
			add
			{
				base.Events.AddHandler(DataGridColumnStyle.AlignmentChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridColumnStyle.AlignmentChangedEvent, value);
			}
		}

		/// <summary>Occurs when the column's font changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000D0 RID: 208
		// (add) Token: 0x06000D20 RID: 3360 RVA: 0x00036020 File Offset: 0x00034220
		// (remove) Token: 0x06000D21 RID: 3361 RVA: 0x00036034 File Offset: 0x00034234
		public event EventHandler FontChanged
		{
			add
			{
				base.Events.AddHandler(DataGridColumnStyle.FontChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridColumnStyle.FontChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridColumnStyle.HeaderText" /> property value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000D1 RID: 209
		// (add) Token: 0x06000D22 RID: 3362 RVA: 0x00036048 File Offset: 0x00034248
		// (remove) Token: 0x06000D23 RID: 3363 RVA: 0x0003605C File Offset: 0x0003425C
		public event EventHandler HeaderTextChanged
		{
			add
			{
				base.Events.AddHandler(DataGridColumnStyle.HeaderTextChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridColumnStyle.HeaderTextChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridColumnStyle.MappingName" /> value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000D2 RID: 210
		// (add) Token: 0x06000D24 RID: 3364 RVA: 0x00036070 File Offset: 0x00034270
		// (remove) Token: 0x06000D25 RID: 3365 RVA: 0x00036084 File Offset: 0x00034284
		public event EventHandler MappingNameChanged
		{
			add
			{
				base.Events.AddHandler(DataGridColumnStyle.MappingNameChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridColumnStyle.MappingNameChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridColumnStyle.NullText" /> value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000D3 RID: 211
		// (add) Token: 0x06000D26 RID: 3366 RVA: 0x00036098 File Offset: 0x00034298
		// (remove) Token: 0x06000D27 RID: 3367 RVA: 0x000360AC File Offset: 0x000342AC
		public event EventHandler NullTextChanged
		{
			add
			{
				base.Events.AddHandler(DataGridColumnStyle.NullTextChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridColumnStyle.NullTextChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridColumnStyle.PropertyDescriptor" /> property value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000D4 RID: 212
		// (add) Token: 0x06000D28 RID: 3368 RVA: 0x000360C0 File Offset: 0x000342C0
		// (remove) Token: 0x06000D29 RID: 3369 RVA: 0x000360D4 File Offset: 0x000342D4
		[EditorBrowsable(2)]
		[Browsable(false)]
		public event EventHandler PropertyDescriptorChanged
		{
			add
			{
				base.Events.AddHandler(DataGridColumnStyle.PropertyDescriptorChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridColumnStyle.PropertyDescriptorChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridColumnStyle.ReadOnly" /> property value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000D5 RID: 213
		// (add) Token: 0x06000D2A RID: 3370 RVA: 0x000360E8 File Offset: 0x000342E8
		// (remove) Token: 0x06000D2B RID: 3371 RVA: 0x000360FC File Offset: 0x000342FC
		public event EventHandler ReadOnlyChanged
		{
			add
			{
				base.Events.AddHandler(DataGridColumnStyle.ReadOnlyChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridColumnStyle.ReadOnlyChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridColumnStyle.Width" /> property value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000D6 RID: 214
		// (add) Token: 0x06000D2C RID: 3372 RVA: 0x00036110 File Offset: 0x00034310
		// (remove) Token: 0x06000D2D RID: 3373 RVA: 0x00036124 File Offset: 0x00034324
		public event EventHandler WidthChanged
		{
			add
			{
				base.Events.AddHandler(DataGridColumnStyle.WidthChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridColumnStyle.WidthChangedEvent, value);
			}
		}

		/// <summary>Informs the <see cref="T:System.Windows.Forms.DataGrid" /> control that the user has begun editing the column.</summary>
		/// <param name="editingControl">The <see cref="T:System.Windows.Forms.Control" /> that is editing the column.</param>
		// Token: 0x06000D2E RID: 3374 RVA: 0x00036138 File Offset: 0x00034338
		void IDataGridColumnStyleEditingNotificationService.ColumnStartedEditing(Control editingControl)
		{
			this.ColumnStartedEditing(editingControl);
		}

		/// <summary>Gets or sets the alignment of text in a column.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values. The default is Left. Valid options include Left, Center, and Right.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x00036144 File Offset: 0x00034344
		// (set) Token: 0x06000D30 RID: 3376 RVA: 0x0003614C File Offset: 0x0003434C
		[Localizable(true)]
		[DefaultValue(HorizontalAlignment.Left)]
		public virtual HorizontalAlignment Alignment
		{
			get
			{
				return this.alignment;
			}
			set
			{
				if (value != this.alignment)
				{
					this.alignment = value;
					if (this.table_style != null && this.table_style.DataGrid != null)
					{
						this.table_style.DataGrid.Invalidate();
					}
					EventHandler eventHandler = (EventHandler)base.Events[DataGridColumnStyle.AlignmentChangedEvent];
					if (eventHandler != null)
					{
						eventHandler.Invoke(this, EventArgs.Empty);
					}
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> for the column.</summary>
		/// <returns>The <see cref="P:System.Windows.Forms.DataGridColumnStyle.DataGridTableStyle" /> that contains the current <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000D31 RID: 3377 RVA: 0x000361C0 File Offset: 0x000343C0
		[Browsable(false)]
		public virtual DataGridTableStyle DataGridTableStyle
		{
			get
			{
				return this.table_style;
			}
		}

		/// <summary>Gets the height of the column's font.</summary>
		/// <returns>The height of the font, in pixels. If no font height has been set, the property returns the <see cref="T:System.Windows.Forms.DataGrid" /> control's font height; if that property hasn't been set, the default font height value for the <see cref="T:System.Windows.Forms.DataGrid" /> control is returned.</returns>
		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x000361C8 File Offset: 0x000343C8
		protected int FontHeight
		{
			get
			{
				if (this.fontheight != -1)
				{
					return this.fontheight;
				}
				if (this.table_style != null)
				{
					return -1;
				}
				return -1;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.AccessibleObject" /> for the column.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> for the column.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000D33 RID: 3379 RVA: 0x000361EC File Offset: 0x000343EC
		[Browsable(false)]
		public AccessibleObject HeaderAccessibleObject
		{
			get
			{
				return this.accesible_object;
			}
		}

		/// <summary>Gets or sets the text of the column header.</summary>
		/// <returns>A string that is displayed as the column header. If it is created by the <see cref="T:System.Windows.Forms.DataGrid" />, the default value is the name of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> used to create the column. If it is created by the user, the default is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x000361F4 File Offset: 0x000343F4
		// (set) Token: 0x06000D35 RID: 3381 RVA: 0x000361FC File Offset: 0x000343FC
		[Localizable(true)]
		public virtual string HeaderText
		{
			get
			{
				return this.header_text;
			}
			set
			{
				if (value != this.header_text)
				{
					this.header_text = value;
					this.Invalidate();
					EventHandler eventHandler = (EventHandler)base.Events[DataGridColumnStyle.HeaderTextChangedEvent];
					if (eventHandler != null)
					{
						eventHandler.Invoke(this, EventArgs.Empty);
					}
				}
			}
		}

		/// <summary>Gets or sets the name of the data member to map the column style to.</summary>
		/// <returns>The name of the data member to map the column style to.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x00036250 File Offset: 0x00034450
		// (set) Token: 0x06000D37 RID: 3383 RVA: 0x00036258 File Offset: 0x00034458
		[Editor("System.Windows.Forms.Design.DataGridColumnStyleMappingNameEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Localizable(true)]
		public string MappingName
		{
			get
			{
				return this.mapping_name;
			}
			set
			{
				if (value != this.mapping_name)
				{
					this.mapping_name = value;
					EventHandler eventHandler = (EventHandler)base.Events[DataGridColumnStyle.MappingNameChangedEvent];
					if (eventHandler != null)
					{
						eventHandler.Invoke(this, EventArgs.Empty);
					}
				}
			}
		}

		/// <summary>Gets or sets the text that is displayed when the column contains null.</summary>
		/// <returns>A string displayed in a column containing a <see cref="F:System.DBNull.Value" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x000362A8 File Offset: 0x000344A8
		// (set) Token: 0x06000D39 RID: 3385 RVA: 0x000362B0 File Offset: 0x000344B0
		[Localizable(true)]
		public virtual string NullText
		{
			get
			{
				return this.null_text;
			}
			set
			{
				if (value != this.null_text)
				{
					this.null_text = value;
					if (this.table_style != null && this.table_style.DataGrid != null)
					{
						this.table_style.DataGrid.Invalidate();
					}
					EventHandler eventHandler = (EventHandler)base.Events[DataGridColumnStyle.NullTextChangedEvent];
					if (eventHandler != null)
					{
						eventHandler.Invoke(this, EventArgs.Empty);
					}
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> that determines the attributes of data displayed by the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that contains data about the attributes of the column.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x00036328 File Offset: 0x00034528
		// (set) Token: 0x06000D3B RID: 3387 RVA: 0x00036330 File Offset: 0x00034530
		[Browsable(false)]
		[EditorBrowsable(2)]
		[DefaultValue(null)]
		public virtual PropertyDescriptor PropertyDescriptor
		{
			get
			{
				return this.property_descriptor;
			}
			set
			{
				if (value != this.property_descriptor)
				{
					this.property_descriptor = value;
					EventHandler eventHandler = (EventHandler)base.Events[DataGridColumnStyle.PropertyDescriptorChangedEvent];
					if (eventHandler != null)
					{
						eventHandler.Invoke(this, EventArgs.Empty);
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the data in the column can be edited.</summary>
		/// <returns>true, if the data cannot be edited; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000D3C RID: 3388 RVA: 0x00036378 File Offset: 0x00034578
		// (set) Token: 0x06000D3D RID: 3389 RVA: 0x00036380 File Offset: 0x00034580
		[DefaultValue(false)]
		public virtual bool ReadOnly
		{
			get
			{
				return this._readonly;
			}
			set
			{
				if (value != this._readonly)
				{
					this._readonly = value;
					if (this.table_style != null && this.table_style.DataGrid != null)
					{
						this.table_style.DataGrid.CalcAreasAndInvalidate();
					}
					EventHandler eventHandler = (EventHandler)base.Events[DataGridColumnStyle.ReadOnlyChangedEvent];
					if (eventHandler != null)
					{
						eventHandler.Invoke(this, EventArgs.Empty);
					}
				}
			}
		}

		/// <summary>Gets or sets the width of the column.</summary>
		/// <returns>The width of the column, in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x000363F4 File Offset: 0x000345F4
		// (set) Token: 0x06000D3F RID: 3391 RVA: 0x000363FC File Offset: 0x000345FC
		[Localizable(true)]
		[DefaultValue(100)]
		public virtual int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				if (value != this.width)
				{
					this.width = value;
					if (this.table_style != null && this.table_style.DataGrid != null)
					{
						this.table_style.DataGrid.CalcAreasAndInvalidate();
					}
					EventHandler eventHandler = (EventHandler)base.Events[DataGridColumnStyle.WidthChangedEvent];
					if (eventHandler != null)
					{
						eventHandler.Invoke(this, EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x00036470 File Offset: 0x00034670
		// (set) Token: 0x06000D41 RID: 3393 RVA: 0x00036478 File Offset: 0x00034678
		internal DataGridColumnStyle.ArrowDrawing ArrowDrawingMode
		{
			get
			{
				return this.arrow_drawing;
			}
			set
			{
				this.arrow_drawing = value;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x00036484 File Offset: 0x00034684
		internal bool TableStyleReadOnly
		{
			get
			{
				return this.table_style != null && this.table_style.ReadOnly;
			}
		}

		// Token: 0x170002FF RID: 767
		// (set) Token: 0x06000D43 RID: 3395 RVA: 0x000364A0 File Offset: 0x000346A0
		internal DataGridTableStyle TableStyle
		{
			set
			{
				this.table_style = value;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x000364AC File Offset: 0x000346AC
		internal bool IsDefault
		{
			get
			{
				return this.is_default;
			}
		}

		/// <summary>When overridden in a derived class, initiates a request to interrupt an edit procedure.</summary>
		/// <param name="rowNum">The row number upon which an operation is being interrupted. </param>
		// Token: 0x06000D45 RID: 3397
		protected internal abstract void Abort(int rowNum);

		/// <summary>Suspends the painting of the column until the <see cref="M:System.Windows.Forms.DataGridColumnStyle.EndUpdate" /> method is called.</summary>
		// Token: 0x06000D46 RID: 3398 RVA: 0x000364B4 File Offset: 0x000346B4
		[MonoTODO("Will not suspend updates")]
		protected void BeginUpdate()
		{
		}

		/// <summary>Throws an exception if the <see cref="T:System.Windows.Forms.DataGrid" /> does not have a valid data source, or if this column is not mapped to a valid property in the data source.</summary>
		/// <param name="value">A <see cref="T:System.Windows.Forms.CurrencyManager" /> to check. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ApplicationException">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> for this column is null. </exception>
		// Token: 0x06000D47 RID: 3399 RVA: 0x000364B8 File Offset: 0x000346B8
		protected void CheckValidDataSource(CurrencyManager value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("CurrencyManager cannot be null");
			}
			if (this.property_descriptor == null)
			{
				this.property_descriptor = value.GetItemProperties()[this.mapping_name];
				if (this.property_descriptor == null)
				{
					throw new InvalidOperationException("The PropertyDescriptor for this column is a null reference");
				}
			}
		}

		/// <summary>Informs the <see cref="T:System.Windows.Forms.DataGrid" /> that the user has begun editing the column.</summary>
		/// <param name="editingControl">The <see cref="T:System.Windows.Forms.Control" /> that hosted by the column. </param>
		// Token: 0x06000D48 RID: 3400 RVA: 0x00036510 File Offset: 0x00034710
		protected internal virtual void ColumnStartedEditing(Control editingControl)
		{
		}

		/// <summary>When overridden in a derived class, initiates a request to complete an editing procedure.</summary>
		/// <returns>true if the editing procedure committed successfully; otherwise, false.</returns>
		/// <param name="dataSource">The <see cref="T:System.Windows.Forms.CurrencyManager" /> for the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />. </param>
		/// <param name="rowNum">The number of the row being edited. </param>
		// Token: 0x06000D49 RID: 3401
		protected internal abstract bool Commit(CurrencyManager dataSource, int rowNum);

		/// <summary>Notifies a column that it must relinquish the focus to the control it is hosting.</summary>
		// Token: 0x06000D4A RID: 3402 RVA: 0x00036514 File Offset: 0x00034714
		protected internal virtual void ConcedeFocus()
		{
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.AccessibleObject" /> for the column.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> for the column.</returns>
		// Token: 0x06000D4B RID: 3403 RVA: 0x00036518 File Offset: 0x00034718
		protected virtual AccessibleObject CreateHeaderAccessibleObject()
		{
			return new DataGridColumnStyle.DataGridColumnHeaderAccessibleObject(this);
		}

		/// <summary>Prepares a cell for editing.</summary>
		/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> for the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />. </param>
		/// <param name="rowNum">The row number to edit. </param>
		/// <param name="bounds">The bounding <see cref="T:System.Drawing.Rectangle" /> in which the control is to be sited. </param>
		/// <param name="readOnly">A value indicating whether the column is a read-only. true if the value is read-only; otherwise, false. </param>
		// Token: 0x06000D4C RID: 3404 RVA: 0x00036520 File Offset: 0x00034720
		protected internal virtual void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly)
		{
			this.Edit(source, rowNum, bounds, readOnly, string.Empty);
		}

		/// <summary>Prepares the cell for editing using the specified <see cref="T:System.Windows.Forms.CurrencyManager" />, row number, and <see cref="T:System.Drawing.Rectangle" /> parameters.</summary>
		/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> for the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />. </param>
		/// <param name="rowNum">The row number in this column which is being edited. </param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> in which the control is to be sited. </param>
		/// <param name="readOnly">A value indicating whether the column is a read-only. true if the value is read-only; otherwise, false. </param>
		/// <param name="displayText">The text to display in the control. </param>
		// Token: 0x06000D4D RID: 3405 RVA: 0x00036534 File Offset: 0x00034734
		protected internal virtual void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly, string displayText)
		{
			this.Edit(source, rowNum, bounds, readOnly, displayText, true);
		}

		/// <summary>When overridden in a deriving class, prepares a cell for editing.</summary>
		/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> for the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />. </param>
		/// <param name="rowNum">The row number in this column which is being edited. </param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> in which the control is to be sited. </param>
		/// <param name="readOnly">A value indicating whether the column is a read-only. true if the value is read-only; otherwise, false. </param>
		/// <param name="displayText">The text to display in the control. </param>
		/// <param name="cellIsVisible">A value indicating whether the cell is visible. true if the cell is visible; otherwise, false. </param>
		// Token: 0x06000D4E RID: 3406
		protected internal abstract void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly, string displayText, bool cellIsVisible);

		/// <summary>Resumes the painting of columns suspended by calling the <see cref="M:System.Windows.Forms.DataGridColumnStyle.BeginUpdate" /> method.</summary>
		// Token: 0x06000D4F RID: 3407 RVA: 0x00036554 File Offset: 0x00034754
		protected void EndUpdate()
		{
		}

		/// <summary>Enters a <see cref="F:System.DBNull.Value" /> into the column.</summary>
		// Token: 0x06000D50 RID: 3408 RVA: 0x00036558 File Offset: 0x00034758
		protected internal virtual void EnterNullValue()
		{
		}

		/// <summary>Gets the value in the specified row from the specified <see cref="T:System.Windows.Forms.CurrencyManager" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> containing the value.</returns>
		/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> containing the data. </param>
		/// <param name="rowNum">The row number containing the data. </param>
		/// <exception cref="T:System.ApplicationException">The <see cref="T:System.Data.DataColumn" /> for this <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> hasn't been set yet. </exception>
		// Token: 0x06000D51 RID: 3409 RVA: 0x0003655C File Offset: 0x0003475C
		protected internal virtual object GetColumnValueAtRow(CurrencyManager source, int rowNum)
		{
			this.CheckValidDataSource(source);
			if (rowNum >= source.Count)
			{
				return DBNull.Value;
			}
			return this.property_descriptor.GetValue(source[rowNum]);
		}

		/// <summary>When overridden in a derived class, gets the minimum height of a row.</summary>
		/// <returns>The minimum height of a row.</returns>
		// Token: 0x06000D52 RID: 3410
		protected internal abstract int GetMinimumHeight();

		/// <summary>When overridden in a derived class, gets the height used for automatically resizing columns.</summary>
		/// <returns>The height used for auto resizing a cell.</returns>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> object. </param>
		/// <param name="value">An object value for which you want to know the screen height and width. </param>
		// Token: 0x06000D53 RID: 3411
		protected internal abstract int GetPreferredHeight(Graphics g, object value);

		/// <summary>When overridden in a derived class, gets the width and height of the specified value. The width and height are used when the user navigates to <see cref="T:System.Windows.Forms.DataGridTableStyle" /> using the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that contains the dimensions of the cell.</returns>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> object. </param>
		/// <param name="value">An object value for which you want to know the screen height and width. </param>
		// Token: 0x06000D54 RID: 3412
		protected internal abstract Size GetPreferredSize(Graphics g, object value);

		/// <summary>Redraws the column and causes a paint message to be sent to the control.</summary>
		// Token: 0x06000D55 RID: 3413 RVA: 0x00036594 File Offset: 0x00034794
		protected virtual void Invalidate()
		{
			if (this.grid != null)
			{
				this.grid.InvalidateColumn(this);
			}
		}

		/// <summary>Paints the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified <see cref="T:System.Drawing.Graphics" />, <see cref="T:System.Drawing.Rectangle" />, <see cref="T:System.Windows.Forms.CurrencyManager" />, and row number.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to draw to. </param>
		/// <param name="bounds">The bounding <see cref="T:System.Drawing.Rectangle" /> to paint into. </param>
		/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> of the <see cref="T:System.Windows.Forms.DataGrid" /> control the column belongs to. </param>
		/// <param name="rowNum">The number of the row in the underlying data being referred to. </param>
		// Token: 0x06000D56 RID: 3414
		protected internal abstract void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum);

		/// <summary>When overridden in a derived class, paints a <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified <see cref="T:System.Drawing.Graphics" />, <see cref="T:System.Drawing.Rectangle" />, <see cref="T:System.Windows.Forms.CurrencyManager" />, row number, and alignment.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to draw to. </param>
		/// <param name="bounds">The bounding <see cref="T:System.Drawing.Rectangle" /> to paint into. </param>
		/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> of the <see cref="T:System.Windows.Forms.DataGrid" /> control the column belongs to. </param>
		/// <param name="rowNum">The number of the row in the underlying data being referred to. </param>
		/// <param name="alignToRight">A value indicating whether to align the column's content to the right. true if the content should be aligned to the right; otherwise false. </param>
		// Token: 0x06000D57 RID: 3415
		protected internal abstract void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, bool alignToRight);

		/// <summary>Paints a <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified <see cref="T:System.Drawing.Graphics" />, <see cref="T:System.Drawing.Rectangle" />, <see cref="T:System.Windows.Forms.CurrencyManager" />, row number, background color, foreground color, and alignment.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to draw to. </param>
		/// <param name="bounds">The bounding <see cref="T:System.Drawing.Rectangle" /> to paint into. </param>
		/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> of the <see cref="T:System.Windows.Forms.DataGrid" /> control the column belongs to. </param>
		/// <param name="rowNum">The number of the row in the underlying data table being referred to. </param>
		/// <param name="backBrush">A <see cref="T:System.Drawing.Brush" /> used to paint the background color. </param>
		/// <param name="foreBrush">A <see cref="T:System.Drawing.Color" /> used to paint the foreground color. </param>
		/// <param name="alignToRight">A value indicating whether to align the content to the right. true if the content is aligned to the right, otherwise, false. </param>
		// Token: 0x06000D58 RID: 3416 RVA: 0x000365B0 File Offset: 0x000347B0
		protected internal virtual void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, Brush backBrush, Brush foreBrush, bool alignToRight)
		{
		}

		/// <summary>Allows the column to free resources when the control it hosts is not needed.</summary>
		// Token: 0x06000D59 RID: 3417 RVA: 0x000365B4 File Offset: 0x000347B4
		protected internal virtual void ReleaseHostedControl()
		{
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridColumnStyle.HeaderText" /> to its default value, null.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D5A RID: 3418 RVA: 0x000365B8 File Offset: 0x000347B8
		public void ResetHeaderText()
		{
			this.HeaderText = string.Empty;
		}

		/// <summary>Sets the value in a specified row with the value from a specified <see cref="T:System.Windows.Forms.CurrencyManager" />.</summary>
		/// <param name="source">A <see cref="T:System.Windows.Forms.CurrencyManager" /> associated with the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />. </param>
		/// <param name="rowNum">The number of the row. </param>
		/// <param name="value">The value to set. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Windows.Forms.CurrencyManager" /> object's <see cref="P:System.Windows.Forms.BindingManagerBase.Position" /> does not match <paramref name="rowNum" />. </exception>
		// Token: 0x06000D5B RID: 3419 RVA: 0x000365C8 File Offset: 0x000347C8
		protected internal virtual void SetColumnValueAtRow(CurrencyManager source, int rowNum, object value)
		{
			this.CheckValidDataSource(source);
			IEditableObject editableObject = source[rowNum] as IEditableObject;
			if (editableObject != null)
			{
				editableObject.BeginEdit();
			}
			this.property_descriptor.SetValue(source[rowNum], value);
		}

		/// <summary>Sets the <see cref="T:System.Windows.Forms.DataGrid" /> control that this column belongs to.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.DataGrid" /> control that this column belongs to. </param>
		// Token: 0x06000D5C RID: 3420 RVA: 0x00036608 File Offset: 0x00034808
		protected virtual void SetDataGrid(DataGrid value)
		{
			this.grid = value;
			this.property_descriptor = null;
		}

		/// <summary>Sets the <see cref="T:System.Windows.Forms.DataGrid" /> for the column.</summary>
		/// <param name="value">A <see cref="T:System.Windows.Forms.DataGrid" />. </param>
		// Token: 0x06000D5D RID: 3421 RVA: 0x00036618 File Offset: 0x00034818
		protected virtual void SetDataGridInColumn(DataGrid value)
		{
			this.SetDataGrid(value);
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x00036624 File Offset: 0x00034824
		internal void SetDataGridInternal(DataGrid value)
		{
			this.SetDataGridInColumn(value);
		}

		/// <summary>Updates the value of a specified row with the given text.</summary>
		/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> associated with the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />. </param>
		/// <param name="rowNum">The row to update. </param>
		/// <param name="displayText">The new value. </param>
		// Token: 0x06000D5F RID: 3423 RVA: 0x00036630 File Offset: 0x00034830
		protected internal virtual void UpdateUI(CurrencyManager source, int rowNum, string displayText)
		{
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x00036634 File Offset: 0x00034834
		internal virtual void OnMouseDown(MouseEventArgs e, int row, int column)
		{
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x00036638 File Offset: 0x00034838
		internal virtual void OnKeyDown(KeyEventArgs ke, int row, int column)
		{
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x0003663C File Offset: 0x0003483C
		internal void PaintHeader(Graphics g, Rectangle bounds, int colNum)
		{
			ThemeEngine.Current.DataGridPaintColumnHeader(g, bounds, this.grid, colNum);
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x00036654 File Offset: 0x00034854
		internal void PaintNewRow(Graphics g, Rectangle bounds, Brush backBrush, Brush foreBrush)
		{
			g.FillRectangle(backBrush, bounds);
			this.PaintGridLine(g, bounds);
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x00036668 File Offset: 0x00034868
		internal void PaintGridLine(Graphics g, Rectangle bounds)
		{
			if (this.table_style.CurrentGridLineStyle != DataGridLineStyle.Solid)
			{
				return;
			}
			g.DrawLine(ThemeEngine.Current.ResPool.GetPen(this.table_style.CurrentGridLineColor), bounds.X, bounds.Y + bounds.Height - 1, bounds.X + bounds.Width - 1, bounds.Y + bounds.Height - 1);
			g.DrawLine(ThemeEngine.Current.ResPool.GetPen(this.table_style.CurrentGridLineColor), bounds.X + bounds.Width - 1, bounds.Y, bounds.X + bounds.Width - 1, bounds.Y + bounds.Height);
		}

		// Token: 0x04000958 RID: 2392
		internal HorizontalAlignment alignment;

		// Token: 0x04000959 RID: 2393
		private int fontheight;

		// Token: 0x0400095A RID: 2394
		internal DataGridTableStyle table_style;

		// Token: 0x0400095B RID: 2395
		private string header_text;

		// Token: 0x0400095C RID: 2396
		private string mapping_name;

		// Token: 0x0400095D RID: 2397
		private string null_text;

		// Token: 0x0400095E RID: 2398
		private PropertyDescriptor property_descriptor;

		// Token: 0x0400095F RID: 2399
		private bool _readonly;

		// Token: 0x04000960 RID: 2400
		private int width;

		// Token: 0x04000961 RID: 2401
		internal bool is_default;

		// Token: 0x04000962 RID: 2402
		internal DataGrid grid;

		// Token: 0x04000963 RID: 2403
		private DataGridColumnStyle.DataGridColumnHeaderAccessibleObject accesible_object;

		// Token: 0x04000964 RID: 2404
		private static string def_null_text = "(null)";

		// Token: 0x04000965 RID: 2405
		private DataGridColumnStyle.ArrowDrawing arrow_drawing;

		// Token: 0x04000966 RID: 2406
		internal bool bound;

		/// <summary>Provides an implementation for an object that can be inspected by an accessibility application.</summary>
		// Token: 0x020000C6 RID: 198
		[ComVisible(true)]
		protected class DataGridColumnHeaderAccessibleObject : AccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridColumnStyle.DataGridColumnHeaderAccessibleObject" /> class without specifying a <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> host for the object. </summary>
			// Token: 0x06000D65 RID: 3429 RVA: 0x0003673C File Offset: 0x0003493C
			public DataGridColumnHeaderAccessibleObject()
			{
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridColumnStyle.DataGridColumnHeaderAccessibleObject" /> class and specifies the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> that hosts the object.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> that hosts the object. </param>
			// Token: 0x06000D66 RID: 3430 RVA: 0x00036744 File Offset: 0x00034944
			public DataGridColumnHeaderAccessibleObject(DataGridColumnStyle owner)
			{
				this.owner = owner;
			}

			/// <summary>Gets the bounding rectangle of a column.</summary>
			/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that contains the bounding values of the column.</returns>
			// Token: 0x17000301 RID: 769
			// (get) Token: 0x06000D67 RID: 3431 RVA: 0x00036754 File Offset: 0x00034954
			[MonoTODO("Not implemented, will throw NotImplementedException")]
			public override Rectangle Bounds
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			/// <summary>Gets the name of the column that owns the accessibility object.</summary>
			/// <returns>The name of the column that owns the accessibility object.</returns>
			// Token: 0x17000302 RID: 770
			// (get) Token: 0x06000D68 RID: 3432 RVA: 0x0003675C File Offset: 0x0003495C
			public override string Name
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			/// <summary>Gets the column style object that owns the accessibility object.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> that owns the accessibility object.</returns>
			// Token: 0x17000303 RID: 771
			// (get) Token: 0x06000D69 RID: 3433 RVA: 0x00036764 File Offset: 0x00034964
			protected DataGridColumnStyle Owner
			{
				get
				{
					return this.owner;
				}
			}

			/// <summary>Gets the parent accessibility object.</summary>
			/// <returns>The parent <see cref="T:System.Windows.Forms.AccessibleObject" /> of the column style object.</returns>
			// Token: 0x17000304 RID: 772
			// (get) Token: 0x06000D6A RID: 3434 RVA: 0x0003676C File Offset: 0x0003496C
			public override AccessibleObject Parent
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			/// <summary>Gets the role of the accessibility object.</summary>
			/// <returns>The AccessibleRole object of the accessibility object.</returns>
			// Token: 0x17000305 RID: 773
			// (get) Token: 0x06000D6B RID: 3435 RVA: 0x00036774 File Offset: 0x00034974
			public override AccessibleRole Role
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			/// <summary>Enables navigation to another object.</summary>
			/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> specified by the <paramref name="navdir" /> parameter.</returns>
			/// <param name="navdir">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation" /> values. </param>
			// Token: 0x06000D6C RID: 3436 RVA: 0x0003677C File Offset: 0x0003497C
			[MonoTODO("Not implemented, will throw NotImplementedException")]
			public override AccessibleObject Navigate(AccessibleNavigation navdir)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0400096F RID: 2415
			private new DataGridColumnStyle owner;
		}

		/// <summary>Contains a <see cref="T:System.Diagnostics.TraceSwitch" /> that is used by the .NET Framework infrastructure.</summary>
		// Token: 0x020000C7 RID: 199
		protected class CompModSwitches
		{
			/// <summary>Gets a <see cref="T:System.Diagnostics.TraceSwitch" />.</summary>
			/// <returns>A <see cref="T:System.Diagnostics.TraceSwitch" /> used by the .NET Framework infrastructure.</returns>
			// Token: 0x17000306 RID: 774
			// (get) Token: 0x06000D6E RID: 3438 RVA: 0x0003678C File Offset: 0x0003498C
			[MonoTODO("Not implemented, will throw NotImplementedException")]
			public static TraceSwitch DGEditColumnEditing
			{
				get
				{
					throw new NotImplementedException();
				}
			}
		}

		// Token: 0x020000C8 RID: 200
		internal enum ArrowDrawing
		{
			// Token: 0x04000971 RID: 2417
			No,
			// Token: 0x04000972 RID: 2418
			Ascending,
			// Token: 0x04000973 RID: 2419
			Descending
		}
	}
}
