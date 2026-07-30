using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Displays a single column header in a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000089 RID: 137
	[ToolboxItem(false)]
	[DefaultProperty("Text")]
	[DesignTimeVisible(false)]
	[TypeConverter(typeof(ColumnHeaderConverter))]
	public class ColumnHeader : Component, ICloneable
	{
		// Token: 0x0600062A RID: 1578 RVA: 0x0001CDD4 File Offset: 0x0001AFD4
		internal ColumnHeader(ListView owner, string text, HorizontalAlignment alignment, int width)
		{
			this.owner = owner;
			this.text = text;
			this.width = width;
			this.text_alignment = alignment;
			this.CalcColumnHeader();
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0001CE60 File Offset: 0x0001B060
		internal ColumnHeader(string key, string text, int width, HorizontalAlignment textAlign)
		{
			this.Name = key;
			this.Text = text;
			this.width = width;
			this.text_alignment = textAlign;
			this.CalcColumnHeader();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ColumnHeader" /> class.</summary>
		// Token: 0x0600062C RID: 1580 RVA: 0x0001CEEC File Offset: 0x0001B0EC
		public ColumnHeader()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ColumnHeader" /> class with the image specified.</summary>
		/// <param name="imageIndex">The index of the image to display in the <see cref="T:System.Windows.Forms.ColumnHeader" />.</param>
		// Token: 0x0600062D RID: 1581 RVA: 0x0001CF54 File Offset: 0x0001B154
		public ColumnHeader(int imageIndex)
		{
			this.ImageIndex = imageIndex;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ColumnHeader" /> class with the image specified.</summary>
		/// <param name="imageKey">The key of the image to display in the <see cref="T:System.Windows.Forms.ColumnHeader" />.</param>
		// Token: 0x0600062E RID: 1582 RVA: 0x0001CFC4 File Offset: 0x0001B1C4
		public ColumnHeader(string imageKey)
		{
			this.ImageKey = imageKey;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0001D034 File Offset: 0x0001B234
		// Note: this type is marked as 'beforefieldinit'.
		static ColumnHeader()
		{
			ColumnHeader.UIATextChangedEvent = new object();
		}

		// Token: 0x1400005A RID: 90
		// (add) Token: 0x06000630 RID: 1584 RVA: 0x0001D040 File Offset: 0x0001B240
		// (remove) Token: 0x06000631 RID: 1585 RVA: 0x0001D054 File Offset: 0x0001B254
		internal event EventHandler UIATextChanged
		{
			add
			{
				base.Events.AddHandler(ColumnHeader.UIATextChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ColumnHeader.UIATextChangedEvent, value);
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x0001D068 File Offset: 0x0001B268
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x0001D070 File Offset: 0x0001B270
		internal bool Pressed
		{
			get
			{
				return this.pressed;
			}
			set
			{
				this.pressed = value;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0001D07C File Offset: 0x0001B27C
		// (set) Token: 0x06000635 RID: 1589 RVA: 0x0001D08C File Offset: 0x0001B28C
		internal int X
		{
			get
			{
				return this.column_rect.X;
			}
			set
			{
				this.column_rect.X = value;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x0001D09C File Offset: 0x0001B29C
		// (set) Token: 0x06000637 RID: 1591 RVA: 0x0001D0AC File Offset: 0x0001B2AC
		internal int Y
		{
			get
			{
				return this.column_rect.Y;
			}
			set
			{
				this.column_rect.Y = value;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x0001D0BC File Offset: 0x0001B2BC
		// (set) Token: 0x06000639 RID: 1593 RVA: 0x0001D0CC File Offset: 0x0001B2CC
		internal int Wd
		{
			get
			{
				return this.column_rect.Width;
			}
			set
			{
				this.column_rect.Width = value;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x0001D0DC File Offset: 0x0001B2DC
		// (set) Token: 0x0600063B RID: 1595 RVA: 0x0001D0EC File Offset: 0x0001B2EC
		internal int Ht
		{
			get
			{
				return this.column_rect.Height;
			}
			set
			{
				this.column_rect.Height = value;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x0001D0FC File Offset: 0x0001B2FC
		// (set) Token: 0x0600063D RID: 1597 RVA: 0x0001D104 File Offset: 0x0001B304
		internal Rectangle Rect
		{
			get
			{
				return this.column_rect;
			}
			set
			{
				this.column_rect = value;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x0001D110 File Offset: 0x0001B310
		internal StringFormat Format
		{
			get
			{
				return this.format;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x0001D118 File Offset: 0x0001B318
		// (set) Token: 0x06000640 RID: 1600 RVA: 0x0001D120 File Offset: 0x0001B320
		internal int InternalDisplayIndex
		{
			get
			{
				return this.display_index;
			}
			set
			{
				this.display_index = value;
			}
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0001D12C File Offset: 0x0001B32C
		internal void CalcColumnHeader()
		{
			if (this.text_alignment == HorizontalAlignment.Center)
			{
				this.format.Alignment = 1;
			}
			else if (this.text_alignment == HorizontalAlignment.Right)
			{
				this.format.Alignment = 2;
			}
			else
			{
				this.format.Alignment = 0;
			}
			this.format.LineAlignment = 1;
			this.format.Trimming = 3;
			this.format.FormatFlags = 4096;
			if (this.owner != null)
			{
				this.column_rect.Height = ThemeEngine.Current.ListViewGetHeaderHeight(this.owner, this.owner.Font);
			}
			else
			{
				this.column_rect.Height = ThemeEngine.Current.ListViewGetHeaderHeight(null, ThemeEngine.Current.DefaultFont);
			}
			if (this.width >= 0)
			{
				this.column_rect.Width = this.width;
			}
			else if (this.Index != -1)
			{
				this.column_rect.Width = this.owner.GetChildColumnSize(this.Index).Width;
				this.width = this.column_rect.Width;
			}
			else
			{
				this.column_rect.Width = 0;
			}
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0001D274 File Offset: 0x0001B474
		internal void SetListView(ListView list_view)
		{
			this.owner = list_view;
		}

		/// <summary>Gets the display order of the column relative to the currently displayed columns.</summary>
		/// <returns>The display order of the column, relative to the currently displayed columns.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x0001D280 File Offset: 0x0001B480
		// (set) Token: 0x06000644 RID: 1604 RVA: 0x0001D2A0 File Offset: 0x0001B4A0
		[Localizable(true)]
		[RefreshProperties(2)]
		public int DisplayIndex
		{
			get
			{
				if (this.owner == null)
				{
					return this.display_index;
				}
				return this.owner.GetReorderedColumnIndex(this);
			}
			set
			{
				if (this.owner == null)
				{
					this.display_index = value;
					return;
				}
				if (value < 0 || value >= this.owner.Columns.Count)
				{
					throw new ArgumentOutOfRangeException("DisplayIndex");
				}
				this.owner.ReorderColumn(this, value, false);
			}
		}

		/// <summary>Gets or sets the index of the image displayed in the <see cref="T:System.Windows.Forms.ColumnHeader" />. </summary>
		/// <returns>The index of the image displayed in the <see cref="T:System.Windows.Forms.ColumnHeader" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Windows.Forms.ColumnHeader.ImageIndex" /> is less than -1.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x0001D2F8 File Offset: 0x0001B4F8
		// (set) Token: 0x06000646 RID: 1606 RVA: 0x0001D300 File Offset: 0x0001B500
		[DesignerSerializationVisibility(0)]
		[TypeConverter(typeof(ImageIndexConverter))]
		[RefreshProperties(2)]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue(-1)]
		public int ImageIndex
		{
			get
			{
				return this.image_index;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("ImageIndex");
				}
				this.image_index = value;
				this.image_key = string.Empty;
				if (this.owner != null)
				{
					this.owner.header_control.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the key of the image displayed in the column.</summary>
		/// <returns>The key of the image displayed in the column.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x0001D34C File Offset: 0x0001B54C
		// (set) Token: 0x06000648 RID: 1608 RVA: 0x0001D354 File Offset: 0x0001B554
		[DesignerSerializationVisibility(0)]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[RefreshProperties(2)]
		[TypeConverter(typeof(ImageKeyConverter))]
		[DefaultValue("")]
		public string ImageKey
		{
			get
			{
				return this.image_key;
			}
			set
			{
				this.image_key = ((value != null) ? value : string.Empty);
				this.image_index = -1;
				if (this.owner != null)
				{
					this.owner.header_control.Invalidate();
				}
			}
		}

		/// <summary>Gets the image list associated with the <see cref="T:System.Windows.Forms.ColumnHeader" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ImageList" /> associated with the <see cref="T:System.Windows.Forms.ColumnHeader" />. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x0001D390 File Offset: 0x0001B590
		[Browsable(false)]
		public ImageList ImageList
		{
			get
			{
				if (this.owner == null)
				{
					return null;
				}
				return this.owner.SmallImageList;
			}
		}

		/// <summary>Gets the location with the <see cref="T:System.Windows.Forms.ListView" /> control's <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" /> of this column.</summary>
		/// <returns>The zero-based index of the column header within the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" /> of the <see cref="T:System.Windows.Forms.ListView" /> control it is contained in.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x0001D3AC File Offset: 0x0001B5AC
		[Browsable(false)]
		public int Index
		{
			get
			{
				if (this.owner != null && this.owner.Columns != null && this.owner.Columns.Contains(this))
				{
					return this.owner.Columns.IndexOf(this);
				}
				return -1;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ListView" /> control the <see cref="T:System.Windows.Forms.ColumnHeader" /> is located in.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListView" /> control that represents the control that contains the <see cref="T:System.Windows.Forms.ColumnHeader" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x0001D400 File Offset: 0x0001B600
		[Browsable(false)]
		public ListView ListView
		{
			get
			{
				return this.owner;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Windows.Forms.ColumnHeader" />. </summary>
		/// <returns>The name of the <see cref="T:System.Windows.Forms.ColumnHeader" />. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x0001D408 File Offset: 0x0001B608
		// (set) Token: 0x0600064D RID: 1613 RVA: 0x0001D410 File Offset: 0x0001B610
		[Browsable(false)]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = ((value != null) ? value : string.Empty);
			}
		}

		/// <summary>Gets or sets an object that contains data to associate with the <see cref="T:System.Windows.Forms.ColumnHeader" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains data to associate with the <see cref="T:System.Windows.Forms.ColumnHeader" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x0001D42C File Offset: 0x0001B62C
		// (set) Token: 0x0600064F RID: 1615 RVA: 0x0001D434 File Offset: 0x0001B634
		[TypeConverter(typeof(StringConverter))]
		[Localizable(false)]
		[Bindable(true)]
		[DefaultValue(null)]
		public object Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		/// <summary>Gets or sets the text displayed in the column header.</summary>
		/// <returns>The text displayed in the column header.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x0001D440 File Offset: 0x0001B640
		// (set) Token: 0x06000651 RID: 1617 RVA: 0x0001D448 File Offset: 0x0001B648
		[Localizable(true)]
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				if (this.text != value)
				{
					this.text = value;
					if (this.owner != null)
					{
						this.owner.Redraw(true);
					}
					this.OnUIATextChanged();
				}
			}
		}

		/// <summary>Gets or sets the horizontal alignment of the text displayed in the <see cref="T:System.Windows.Forms.ColumnHeader" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values. The default is <see cref="F:System.Windows.Forms.HorizontalAlignment.Left" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x0001D480 File Offset: 0x0001B680
		// (set) Token: 0x06000653 RID: 1619 RVA: 0x0001D488 File Offset: 0x0001B688
		[Localizable(true)]
		[DefaultValue(HorizontalAlignment.Left)]
		public HorizontalAlignment TextAlign
		{
			get
			{
				return this.text_alignment;
			}
			set
			{
				this.text_alignment = value;
				if (this.owner != null)
				{
					this.owner.Redraw(true);
				}
			}
		}

		/// <summary>Gets or sets the width of the column.</summary>
		/// <returns>The width of the column, in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x0001D4A8 File Offset: 0x0001B6A8
		// (set) Token: 0x06000655 RID: 1621 RVA: 0x0001D4B0 File Offset: 0x0001B6B0
		[Localizable(true)]
		[DefaultValue(60)]
		public int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				if (this.width != value)
				{
					this.width = value;
					if (this.owner != null)
					{
						this.owner.Redraw(true);
						this.owner.RaiseColumnWidthChanged(this);
					}
				}
			}
		}

		/// <summary>Resizes the width of the column as indicated by the resize style.</summary>
		/// <param name="headerAutoResize">One of the <see cref="T:System.Windows.Forms.ColumnHeaderAutoResizeStyle" />  values.</param>
		/// <exception cref="T:System.InvalidOperationException">A value other than <see cref="F:System.Windows.Forms.ColumnHeaderAutoResizeStyle.None" /> is passed to the <see cref="M:System.Windows.Forms.ColumnHeader.AutoResize(System.Windows.Forms.ColumnHeaderAutoResizeStyle)" /> method when the <see cref="P:System.Windows.Forms.ListView.View" /> property is a value other than <see cref="F:System.Windows.Forms.View.Details" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000656 RID: 1622 RVA: 0x0001D4F4 File Offset: 0x0001B6F4
		public void AutoResize(ColumnHeaderAutoResizeStyle headerAutoResize)
		{
			switch (headerAutoResize)
			{
			case ColumnHeaderAutoResizeStyle.None:
				break;
			case ColumnHeaderAutoResizeStyle.HeaderSize:
				this.Width = -2;
				break;
			case ColumnHeaderAutoResizeStyle.ColumnContent:
				this.Width = -1;
				break;
			default:
				throw new InvalidEnumArgumentException("headerAutoResize", (int)headerAutoResize, typeof(ColumnHeaderAutoResizeStyle));
			}
		}

		/// <summary>Creates an identical copy of the current <see cref="T:System.Windows.Forms.ColumnHeader" /> that is not attached to any list view control.</summary>
		/// <returns>An object representing a copy of this <see cref="T:System.Windows.Forms.ColumnHeader" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000657 RID: 1623 RVA: 0x0001D550 File Offset: 0x0001B750
		public object Clone()
		{
			return new ColumnHeader
			{
				text = this.text,
				text_alignment = this.text_alignment,
				width = this.width,
				owner = this.owner,
				format = (StringFormat)this.Format.Clone(),
				column_rect = Rectangle.Empty
			};
		}

		/// <summary>Returns a string representation of this column header.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any, or null if the <see cref="T:System.ComponentModel.Component" /> is unnamed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000658 RID: 1624 RVA: 0x0001D5B8 File Offset: 0x0001B7B8
		public override string ToString()
		{
			return string.Format("ColumnHeader: Text: {0}", this.text);
		}

		/// <summary>Disposes of the resources (other than memory) used by the <see cref="T:System.Windows.Forms.ColumnHeader" />.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06000659 RID: 1625 RVA: 0x0001D5CC File Offset: 0x0001B7CC
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0001D5D8 File Offset: 0x0001B7D8
		private void OnUIATextChanged()
		{
			EventHandler eventHandler = (EventHandler)base.Events[ColumnHeader.UIATextChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x04000725 RID: 1829
		private StringFormat format = new StringFormat();

		// Token: 0x04000726 RID: 1830
		private string text = "ColumnHeader";

		// Token: 0x04000727 RID: 1831
		private HorizontalAlignment text_alignment;

		// Token: 0x04000728 RID: 1832
		private int width = ThemeEngine.Current.ListViewDefaultColumnWidth;

		// Token: 0x04000729 RID: 1833
		private int image_index = -1;

		// Token: 0x0400072A RID: 1834
		private string image_key = string.Empty;

		// Token: 0x0400072B RID: 1835
		private string name = string.Empty;

		// Token: 0x0400072C RID: 1836
		private object tag;

		// Token: 0x0400072D RID: 1837
		private int display_index = -1;

		// Token: 0x0400072E RID: 1838
		private Rectangle column_rect = Rectangle.Empty;

		// Token: 0x0400072F RID: 1839
		private bool pressed;

		// Token: 0x04000730 RID: 1840
		private ListView owner;
	}
}
