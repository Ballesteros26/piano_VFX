using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows combo box control. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000091 RID: 145
	[DefaultEvent("SelectedIndexChanged")]
	[ComVisible(true)]
	[ClassInterface(1)]
	[DefaultProperty("Items")]
	[Designer("System.Windows.Forms.Design.ComboBoxDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultBindingProperty("Text")]
	public class ComboBox : ListControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ComboBox" /> class.</summary>
		// Token: 0x0600066E RID: 1646 RVA: 0x0001D89C File Offset: 0x0001BA9C
		public ComboBox()
		{
			this.items = new ComboBox.ObjectCollection(this);
			this.DropDownStyle = ComboBoxStyle.DropDown;
			this.item_height = base.FontHeight + 2;
			this.background_color = ThemeEngine.Current.ColorWindow;
			this.border_style = BorderStyle.None;
			this.drop_down_height = 106;
			this.flat_style = FlatStyle.Standard;
			base.MouseDown += this.OnMouseDownCB;
			base.MouseUp += this.OnMouseUpCB;
			base.MouseMove += this.OnMouseMoveCB;
			base.MouseWheel += this.OnMouseWheelCB;
			base.MouseEnter += new EventHandler(this.OnMouseEnter);
			base.MouseLeave += new EventHandler(this.OnMouseLeave);
			base.KeyDown += this.OnKeyDownCB;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0001D9B0 File Offset: 0x0001BBB0
		// Note: this type is marked as 'beforefieldinit'.
		static ComboBox()
		{
			ComboBox.DrawItemEvent = new object();
			ComboBox.DropDownEvent = new object();
			ComboBox.DropDownStyleChangedEvent = new object();
			ComboBox.MeasureItemEvent = new object();
			ComboBox.SelectedIndexChangedEvent = new object();
			ComboBox.SelectionChangeCommittedEvent = new object();
			ComboBox.DropDownClosedEvent = new object();
			ComboBox.TextUpdateEvent = new object();
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ComboBox.BackgroundImage" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400005B RID: 91
		// (add) Token: 0x06000670 RID: 1648 RVA: 0x0001DA10 File Offset: 0x0001BC10
		// (remove) Token: 0x06000671 RID: 1649 RVA: 0x0001DA1C File Offset: 0x0001BC1C
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler BackgroundImageChanged
		{
			add
			{
				base.BackgroundImageChanged += value;
			}
			remove
			{
				base.BackgroundImageChanged -= value;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ComboBox.BackgroundImageLayout" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400005C RID: 92
		// (add) Token: 0x06000672 RID: 1650 RVA: 0x0001DA28 File Offset: 0x0001BC28
		// (remove) Token: 0x06000673 RID: 1651 RVA: 0x0001DA34 File Offset: 0x0001BC34
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler BackgroundImageLayoutChanged
		{
			add
			{
				base.BackgroundImageLayoutChanged += value;
			}
			remove
			{
				base.BackgroundImageLayoutChanged -= value;
			}
		}

		/// <summary>This event is not relevant for this class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400005D RID: 93
		// (add) Token: 0x06000674 RID: 1652 RVA: 0x0001DA40 File Offset: 0x0001BC40
		// (remove) Token: 0x06000675 RID: 1653 RVA: 0x0001DA4C File Offset: 0x0001BC4C
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler DoubleClick
		{
			add
			{
				base.DoubleClick += value;
			}
			remove
			{
				base.DoubleClick -= value;
			}
		}

		/// <summary>Occurs when a visual aspect of an owner-drawn <see cref="T:System.Windows.Forms.ComboBox" /> changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400005E RID: 94
		// (add) Token: 0x06000676 RID: 1654 RVA: 0x0001DA58 File Offset: 0x0001BC58
		// (remove) Token: 0x06000677 RID: 1655 RVA: 0x0001DA6C File Offset: 0x0001BC6C
		public event DrawItemEventHandler DrawItem
		{
			add
			{
				base.Events.AddHandler(ComboBox.DrawItemEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ComboBox.DrawItemEvent, value);
			}
		}

		/// <summary>Occurs when the drop-down portion of a <see cref="T:System.Windows.Forms.ComboBox" /> is shown.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400005F RID: 95
		// (add) Token: 0x06000678 RID: 1656 RVA: 0x0001DA80 File Offset: 0x0001BC80
		// (remove) Token: 0x06000679 RID: 1657 RVA: 0x0001DA94 File Offset: 0x0001BC94
		public event EventHandler DropDown
		{
			add
			{
				base.Events.AddHandler(ComboBox.DropDownEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ComboBox.DropDownEvent, value);
			}
		}

		/// <summary>Occurs when the drop-down portion of the <see cref="T:System.Windows.Forms.ComboBox" /> is no longer visible.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000060 RID: 96
		// (add) Token: 0x0600067A RID: 1658 RVA: 0x0001DAA8 File Offset: 0x0001BCA8
		// (remove) Token: 0x0600067B RID: 1659 RVA: 0x0001DABC File Offset: 0x0001BCBC
		public event EventHandler DropDownClosed
		{
			add
			{
				base.Events.AddHandler(ComboBox.DropDownClosedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ComboBox.DropDownClosedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ComboBox.DropDownStyle" /> property has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000061 RID: 97
		// (add) Token: 0x0600067C RID: 1660 RVA: 0x0001DAD0 File Offset: 0x0001BCD0
		// (remove) Token: 0x0600067D RID: 1661 RVA: 0x0001DAE4 File Offset: 0x0001BCE4
		public event EventHandler DropDownStyleChanged
		{
			add
			{
				base.Events.AddHandler(ComboBox.DropDownStyleChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ComboBox.DropDownStyleChangedEvent, value);
			}
		}

		/// <summary>Occurs each time an owner-drawn <see cref="T:System.Windows.Forms.ComboBox" /> item needs to be drawn and when the sizes of the list items are determined.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000062 RID: 98
		// (add) Token: 0x0600067E RID: 1662 RVA: 0x0001DAF8 File Offset: 0x0001BCF8
		// (remove) Token: 0x0600067F RID: 1663 RVA: 0x0001DB0C File Offset: 0x0001BD0C
		public event MeasureItemEventHandler MeasureItem
		{
			add
			{
				base.Events.AddHandler(ComboBox.MeasureItemEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ComboBox.MeasureItemEvent, value);
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x14000063 RID: 99
		// (add) Token: 0x06000680 RID: 1664 RVA: 0x0001DB20 File Offset: 0x0001BD20
		// (remove) Token: 0x06000681 RID: 1665 RVA: 0x0001DB2C File Offset: 0x0001BD2C
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler PaddingChanged
		{
			add
			{
				base.PaddingChanged += value;
			}
			remove
			{
				base.PaddingChanged -= value;
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ComboBox" /> control is redrawn.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000064 RID: 100
		// (add) Token: 0x06000682 RID: 1666 RVA: 0x0001DB38 File Offset: 0x0001BD38
		// (remove) Token: 0x06000683 RID: 1667 RVA: 0x0001DB44 File Offset: 0x0001BD44
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event PaintEventHandler Paint
		{
			add
			{
				base.Paint += value;
			}
			remove
			{
				base.Paint -= value;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ComboBox.SelectedIndex" /> property has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000065 RID: 101
		// (add) Token: 0x06000684 RID: 1668 RVA: 0x0001DB50 File Offset: 0x0001BD50
		// (remove) Token: 0x06000685 RID: 1669 RVA: 0x0001DB64 File Offset: 0x0001BD64
		public event EventHandler SelectedIndexChanged
		{
			add
			{
				base.Events.AddHandler(ComboBox.SelectedIndexChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ComboBox.SelectedIndexChangedEvent, value);
			}
		}

		/// <summary>Occurs when the selected item has changed and that change is displayed in the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000066 RID: 102
		// (add) Token: 0x06000686 RID: 1670 RVA: 0x0001DB78 File Offset: 0x0001BD78
		// (remove) Token: 0x06000687 RID: 1671 RVA: 0x0001DB8C File Offset: 0x0001BD8C
		public event EventHandler SelectionChangeCommitted
		{
			add
			{
				base.Events.AddHandler(ComboBox.SelectionChangeCommittedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ComboBox.SelectionChangeCommittedEvent, value);
			}
		}

		/// <summary>Occurs when the control has formatted the text, but before the text is displayed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000067 RID: 103
		// (add) Token: 0x06000688 RID: 1672 RVA: 0x0001DBA0 File Offset: 0x0001BDA0
		// (remove) Token: 0x06000689 RID: 1673 RVA: 0x0001DBB4 File Offset: 0x0001BDB4
		public event EventHandler TextUpdate
		{
			add
			{
				base.Events.AddHandler(ComboBox.TextUpdateEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ComboBox.TextUpdateEvent, value);
			}
		}

		/// <summary>Gets or sets a custom <see cref="T:System.Collections.Specialized.StringCollection" /> to use when the <see cref="P:System.Windows.Forms.ComboBox.AutoCompleteSource" /> property is set to CustomSource.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> to use with <see cref="P:System.Windows.Forms.ComboBox.AutoCompleteSource" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x0001DBC8 File Offset: 0x0001BDC8
		// (set) Token: 0x0600068B RID: 1675 RVA: 0x0001DC00 File Offset: 0x0001BE00
		[Localizable(true)]
		[MonoTODO("AutoCompletion algorithm is currently not implemented.")]
		[Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DesignerSerializationVisibility(2)]
		[Browsable(true)]
		[EditorBrowsable(0)]
		public AutoCompleteStringCollection AutoCompleteCustomSource
		{
			get
			{
				if (this.auto_complete_custom_source == null)
				{
					this.auto_complete_custom_source = new AutoCompleteStringCollection();
					this.auto_complete_custom_source.CollectionChanged += new CollectionChangeEventHandler(this.OnAutoCompleteCustomSourceChanged);
				}
				return this.auto_complete_custom_source;
			}
			set
			{
				if (this.auto_complete_custom_source == value)
				{
					return;
				}
				if (this.auto_complete_custom_source != null)
				{
					this.auto_complete_custom_source.CollectionChanged -= new CollectionChangeEventHandler(this.OnAutoCompleteCustomSourceChanged);
				}
				this.auto_complete_custom_source = value;
				if (this.auto_complete_custom_source != null)
				{
					this.auto_complete_custom_source.CollectionChanged += new CollectionChangeEventHandler(this.OnAutoCompleteCustomSourceChanged);
				}
				this.SetTextBoxAutoCompleteData();
			}
		}

		/// <summary>Gets or sets an option that controls how automatic completion works for the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.AutoCompleteMode" />. The values are <see cref="F:System.Windows.Forms.AutoCompleteMode.Append" />, <see cref="F:System.Windows.Forms.AutoCompleteMode.None" />, <see cref="F:System.Windows.Forms.AutoCompleteMode.Suggest" />, and <see cref="F:System.Windows.Forms.AutoCompleteMode.SuggestAppend" />. The default is <see cref="F:System.Windows.Forms.AutoCompleteMode.None" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not one of the values of <see cref="T:System.Windows.Forms.AutoCompleteMode" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x0001DC6C File Offset: 0x0001BE6C
		// (set) Token: 0x0600068D RID: 1677 RVA: 0x0001DC74 File Offset: 0x0001BE74
		[DefaultValue(AutoCompleteMode.None)]
		[MonoTODO("AutoCompletion algorithm is currently not implemented.")]
		[Browsable(true)]
		[EditorBrowsable(0)]
		public AutoCompleteMode AutoCompleteMode
		{
			get
			{
				return this.auto_complete_mode;
			}
			set
			{
				if (this.auto_complete_mode == value)
				{
					return;
				}
				if (value < AutoCompleteMode.None || value > AutoCompleteMode.SuggestAppend)
				{
					throw new InvalidEnumArgumentException(Locale.GetText("Enum argument value '{0}' is not valid for AutoCompleteMode", new object[] { value }));
				}
				this.auto_complete_mode = value;
				this.SetTextBoxAutoCompleteData();
			}
		}

		/// <summary>Gets or sets a value specifying the source of complete strings used for automatic completion.</summary>
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.AutoCompleteSource" />. The options are AllSystemSources, AllUrl, FileSystem, HistoryList, RecentlyUsedList, CustomSource, and None. The default is None.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not one of the values of <see cref="T:System.Windows.Forms.AutoCompleteSource" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x0001DCC8 File Offset: 0x0001BEC8
		// (set) Token: 0x0600068F RID: 1679 RVA: 0x0001DCD0 File Offset: 0x0001BED0
		[DefaultValue(AutoCompleteSource.None)]
		[EditorBrowsable(0)]
		[Browsable(true)]
		[MonoTODO("AutoCompletion algorithm is currently not implemented.")]
		public AutoCompleteSource AutoCompleteSource
		{
			get
			{
				return this.auto_complete_source;
			}
			set
			{
				if (this.auto_complete_source == value)
				{
					return;
				}
				if (!Enum.IsDefined(typeof(AutoCompleteSource), value))
				{
					throw new InvalidEnumArgumentException(Locale.GetText("Enum argument value '{0}' is not valid for AutoCompleteSource", new object[] { value }));
				}
				this.auto_complete_source = value;
				this.SetTextBoxAutoCompleteData();
			}
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0001DD30 File Offset: 0x0001BF30
		private void SetTextBoxAutoCompleteData()
		{
			if (this.textbox_ctrl == null)
			{
				return;
			}
			this.textbox_ctrl.AutoCompleteMode = this.auto_complete_mode;
			if (this.auto_complete_source == AutoCompleteSource.ListItems)
			{
				this.textbox_ctrl.AutoCompleteSource = AutoCompleteSource.CustomSource;
				this.textbox_ctrl.AutoCompleteCustomSource = null;
				this.textbox_ctrl.AutoCompleteInternalSource = this;
			}
			else
			{
				this.textbox_ctrl.AutoCompleteSource = this.auto_complete_source;
				this.textbox_ctrl.AutoCompleteCustomSource = this.auto_complete_custom_source;
				this.textbox_ctrl.AutoCompleteInternalSource = null;
			}
		}

		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x0001DDC4 File Offset: 0x0001BFC4
		// (set) Token: 0x06000692 RID: 1682 RVA: 0x0001DDCC File Offset: 0x0001BFCC
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				if (base.BackColor == value)
				{
					return;
				}
				base.BackColor = value;
				this.Refresh();
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x0001DDF0 File Offset: 0x0001BFF0
		// (set) Token: 0x06000694 RID: 1684 RVA: 0x0001DDF8 File Offset: 0x0001BFF8
		[Browsable(false)]
		[EditorBrowsable(1)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{
				if (base.BackgroundImage == value)
				{
					return;
				}
				base.BackgroundImage = value;
				this.Refresh();
			}
		}

		/// <summary>Gets or sets the background image layout as defined in the <see cref="T:System.Windows.Forms.ImageLayout" /> enumeration.</summary>
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.ImageLayout" /> (Center, None, Stretch, Tile, or Zoom).</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not one of the values of <see cref="T:System.Windows.Forms.ImageLayout" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x0001DE14 File Offset: 0x0001C014
		// (set) Token: 0x06000696 RID: 1686 RVA: 0x0001DE1C File Offset: 0x0001C01C
		[Browsable(false)]
		[EditorBrowsable(1)]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return base.BackgroundImageLayout;
			}
			set
			{
				base.BackgroundImageLayout = value;
			}
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x0001DE28 File Offset: 0x0001C028
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <summary>Gets or sets the data source for this <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IList" /> interface, such as a <see cref="T:System.Data.DataSet" /> or an <see cref="T:System.Array" />. The default is null.</returns>
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x0001DE30 File Offset: 0x0001C030
		// (set) Token: 0x06000699 RID: 1689 RVA: 0x0001DE38 File Offset: 0x0001C038
		[MWFCategory("Data")]
		[DefaultValue(null)]
		[AttributeProvider(typeof(IListSource))]
		[RefreshProperties(2)]
		public new object DataSource
		{
			get
			{
				return base.DataSource;
			}
			set
			{
				base.DataSource = value;
			}
		}

		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x0001DE44 File Offset: 0x0001C044
		protected override Size DefaultSize
		{
			get
			{
				return new Size(121, 21);
			}
		}

		/// <summary>Gets or sets a value indicating whether your code or the operating system will handle drawing of elements in the list.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DrawMode" /> enumeration values. The default is <see cref="F:System.Windows.Forms.DrawMode.Normal" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value is not a valid <see cref="T:System.Windows.Forms.DrawMode" /> enumeration value. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x0001DE50 File Offset: 0x0001C050
		// (set) Token: 0x0600069C RID: 1692 RVA: 0x0001DE58 File Offset: 0x0001C058
		[DefaultValue(DrawMode.Normal)]
		[MWFCategory("Behavior")]
		[RefreshProperties(2)]
		public DrawMode DrawMode
		{
			get
			{
				return this.draw_mode;
			}
			set
			{
				if (!Enum.IsDefined(typeof(DrawMode), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for DrawMode", value));
				}
				if (this.draw_mode == value)
				{
					return;
				}
				if (this.draw_mode == DrawMode.OwnerDrawVariable)
				{
					this.item_heights = null;
				}
				this.draw_mode = value;
				if (this.draw_mode == DrawMode.OwnerDrawVariable)
				{
					this.item_heights = new Hashtable();
				}
				this.Refresh();
			}
		}

		/// <summary>Gets or sets the height in pixels of the drop-down portion of the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <returns>The height, in pixels, of the drop-down box.</returns>
		/// <exception cref="T:System.ArgumentException">The specified value is less than one. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x0001DEDC File Offset: 0x0001C0DC
		// (set) Token: 0x0600069E RID: 1694 RVA: 0x0001DEE4 File Offset: 0x0001C0E4
		[Browsable(true)]
		[DefaultValue(106)]
		[EditorBrowsable(0)]
		[MWFCategory("Behavior")]
		public int DropDownHeight
		{
			get
			{
				return this.drop_down_height;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("DropDownHeight", "DropDownHeight must be greater than 0.");
				}
				if (value == this.drop_down_height)
				{
					return;
				}
				this.drop_down_height = value;
				this.IntegralHeight = false;
			}
		}

		/// <summary>Gets or sets a value specifying the style of the combo box.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ComboBoxStyle" /> values. The default is DropDown.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.ComboBoxStyle" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x0001DF24 File Offset: 0x0001C124
		// (set) Token: 0x060006A0 RID: 1696 RVA: 0x0001DF2C File Offset: 0x0001C12C
		[MWFCategory("Appearance")]
		[DefaultValue(ComboBoxStyle.DropDown)]
		[RefreshProperties(2)]
		public ComboBoxStyle DropDownStyle
		{
			get
			{
				return this.dropdown_style;
			}
			set
			{
				if (!Enum.IsDefined(typeof(ComboBoxStyle), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for ComboBoxStyle", value));
				}
				if (this.dropdown_style == value)
				{
					return;
				}
				base.SuspendLayout();
				if (this.dropdown_style == ComboBoxStyle.Simple && this.listbox_ctrl != null)
				{
					base.Controls.RemoveImplicit(this.listbox_ctrl);
					this.listbox_ctrl.Dispose();
					this.listbox_ctrl = null;
				}
				this.dropdown_style = value;
				if (this.dropdown_style == ComboBoxStyle.DropDownList && this.textbox_ctrl != null)
				{
					base.Controls.RemoveImplicit(this.textbox_ctrl);
					this.textbox_ctrl.Dispose();
					this.textbox_ctrl = null;
				}
				if (this.dropdown_style == ComboBoxStyle.Simple)
				{
					this.show_dropdown_button = false;
					this.CreateComboListBox();
					base.Controls.AddImplicit(this.listbox_ctrl);
					this.listbox_ctrl.Visible = true;
					if (this.requested_height == -1)
					{
						this.requested_height = 150;
					}
				}
				else
				{
					this.show_dropdown_button = true;
					this.button_state = ButtonState.Normal;
				}
				if (this.dropdown_style != ComboBoxStyle.DropDownList && this.textbox_ctrl == null)
				{
					this.textbox_ctrl = new ComboBox.ComboTextBox(this);
					object selectedItem = this.SelectedItem;
					if (selectedItem != null)
					{
						this.textbox_ctrl.Text = base.GetItemText(selectedItem);
					}
					this.textbox_ctrl.BorderStyle = BorderStyle.None;
					this.textbox_ctrl.TextChanged += new EventHandler(this.OnTextChangedEdit);
					this.textbox_ctrl.KeyPress += this.OnTextKeyPress;
					this.textbox_ctrl.Click += new EventHandler(this.OnTextBoxClick);
					this.textbox_ctrl.ContextMenu = this.ContextMenu;
					this.textbox_ctrl.TopMargin = 1;
					if (base.IsHandleCreated)
					{
						base.Controls.AddImplicit(this.textbox_ctrl);
					}
					this.SetTextBoxAutoCompleteData();
				}
				base.ResumeLayout();
				this.OnDropDownStyleChanged(EventArgs.Empty);
				this.LayoutComboBox();
				this.UpdateComboBoxBounds();
				this.Refresh();
			}
		}

		/// <summary>Gets or sets the width of the of the drop-down portion of a combo box.</summary>
		/// <returns>The width, in pixels, of the drop-down box.</returns>
		/// <exception cref="T:System.ArgumentException">The specified value is less than one. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x0001E14C File Offset: 0x0001C34C
		// (set) Token: 0x060006A2 RID: 1698 RVA: 0x0001E168 File Offset: 0x0001C368
		[MWFCategory("Behavior")]
		public int DropDownWidth
		{
			get
			{
				if (this.dropdown_width == -1)
				{
					return base.Width;
				}
				return this.dropdown_width;
			}
			set
			{
				if (this.dropdown_width == value)
				{
					return;
				}
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("DropDownWidth", "The DropDownWidth value is less than one.");
				}
				this.dropdown_width = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the combo box is displaying its drop-down portion.</summary>
		/// <returns>true if the drop-down portion is displayed; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x0001E198 File Offset: 0x0001C398
		// (set) Token: 0x060006A4 RID: 1700 RVA: 0x0001E1B0 File Offset: 0x0001C3B0
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public bool DroppedDown
		{
			get
			{
				return this.dropdown_style == ComboBoxStyle.Simple || this.dropped_down;
			}
			set
			{
				if (this.dropdown_style == ComboBoxStyle.Simple || this.dropped_down == value)
				{
					return;
				}
				if (value)
				{
					this.DropDownListBox();
				}
				else
				{
					this.listbox_ctrl.HideWindow();
				}
			}
		}

		/// <summary>Gets or sets the appearance of the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.FlatStyle" />. The options are Flat, Popup, Standard, and System. The default is Standard.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not one of the values of <see cref="T:System.Windows.Forms.FlatStyle" />. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0001E1F4 File Offset: 0x0001C3F4
		// (set) Token: 0x060006A6 RID: 1702 RVA: 0x0001E1FC File Offset: 0x0001C3FC
		[Localizable(true)]
		[MWFCategory("Appearance")]
		[DefaultValue(FlatStyle.Standard)]
		public FlatStyle FlatStyle
		{
			get
			{
				return this.flat_style;
			}
			set
			{
				if (!Enum.IsDefined(typeof(FlatStyle), value))
				{
					throw new InvalidEnumArgumentException("FlatStyle", (int)value, typeof(FlatStyle));
				}
				this.flat_style = value;
				this.LayoutComboBox();
				base.Invalidate();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ComboBox" /> has focus.</summary>
		/// <returns>true if this control has focus; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0001E24C File Offset: 0x0001C44C
		public override bool Focused
		{
			get
			{
				return base.Focused;
			}
		}

		/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultForeColor" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x0001E254 File Offset: 0x0001C454
		// (set) Token: 0x060006A9 RID: 1705 RVA: 0x0001E25C File Offset: 0x0001C45C
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				if (base.ForeColor == value)
				{
					return;
				}
				base.ForeColor = value;
				this.Refresh();
			}
		}

		/// <summary>Gets or sets a value indicating whether the control should resize to avoid showing partial items.</summary>
		/// <returns>true if the list portion can contain only complete items; otherwise, false. The default is true.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x0001E280 File Offset: 0x0001C480
		// (set) Token: 0x060006AB RID: 1707 RVA: 0x0001E288 File Offset: 0x0001C488
		[Localizable(true)]
		[DefaultValue(true)]
		[MWFCategory("Behavior")]
		public bool IntegralHeight
		{
			get
			{
				return this.integral_height;
			}
			set
			{
				if (this.integral_height == value)
				{
					return;
				}
				this.integral_height = value;
				this.UpdateComboBoxBounds();
				this.Refresh();
			}
		}

		/// <summary>Gets or sets the height of an item in the combo box.</summary>
		/// <returns>The height, in pixels, of an item in the combo box.</returns>
		/// <exception cref="T:System.ArgumentException">The item height value is less than zero. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x0001E2B8 File Offset: 0x0001C4B8
		// (set) Token: 0x060006AD RID: 1709 RVA: 0x0001E2F8 File Offset: 0x0001C4F8
		[Localizable(true)]
		[MWFCategory("Behavior")]
		public int ItemHeight
		{
			get
			{
				if (this.item_height == -1)
				{
					this.item_height = (int)TextRenderer.MeasureString("The quick brown Fox", this.Font).Height;
				}
				return this.item_height;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("ItemHeight", "The item height value is less than one.");
				}
				this.item_height_specified = true;
				this.item_height = value;
				if (this.IntegralHeight)
				{
					this.UpdateComboBoxBounds();
				}
				this.LayoutComboBox();
				this.Refresh();
			}
		}

		/// <summary>Gets an object representing the collection of the items contained in this <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ComboBox.ObjectCollection" /> representing the items in the <see cref="T:System.Windows.Forms.ComboBox" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x0001E348 File Offset: 0x0001C548
		[Localizable(true)]
		[DesignerSerializationVisibility(2)]
		[MergableProperty(false)]
		[MWFCategory("Data")]
		[Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public ComboBox.ObjectCollection Items
		{
			get
			{
				return this.items;
			}
		}

		/// <summary>Gets or sets the maximum number of items to be shown in the drop-down portion of the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <returns>The maximum number of items of in the drop-down portion. The minimum for this property is 1 and the maximum is 100.</returns>
		/// <exception cref="T:System.ArgumentException">The maximum number is set less than one or greater than 100. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x0001E350 File Offset: 0x0001C550
		// (set) Token: 0x060006B0 RID: 1712 RVA: 0x0001E358 File Offset: 0x0001C558
		[DefaultValue(8)]
		[MWFCategory("Behavior")]
		[Localizable(true)]
		public int MaxDropDownItems
		{
			get
			{
				return this.maxdrop_items;
			}
			set
			{
				if (this.maxdrop_items == value)
				{
					return;
				}
				this.maxdrop_items = value;
			}
		}

		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x0001E370 File Offset: 0x0001C570
		// (set) Token: 0x060006B2 RID: 1714 RVA: 0x0001E378 File Offset: 0x0001C578
		public override Size MaximumSize
		{
			get
			{
				return base.MaximumSize;
			}
			set
			{
				base.MaximumSize = new Size(value.Width, 0);
			}
		}

		/// <summary>Gets or sets the number of characters a user can type into the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <returns>The maximum number of characters a user can enter. Values of less than zero are reset to zero, which is the default value.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0001E390 File Offset: 0x0001C590
		// (set) Token: 0x060006B4 RID: 1716 RVA: 0x0001E398 File Offset: 0x0001C598
		[DefaultValue(0)]
		[Localizable(true)]
		[MWFCategory("Behavior")]
		public int MaxLength
		{
			get
			{
				return this.max_length;
			}
			set
			{
				if (this.max_length == value)
				{
					return;
				}
				this.max_length = value;
				if (this.dropdown_style != ComboBoxStyle.DropDownList)
				{
					if (value < 0)
					{
						value = 0;
					}
					this.textbox_ctrl.MaxLength = value;
				}
			}
		}

		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x0001E3DC File Offset: 0x0001C5DC
		// (set) Token: 0x060006B6 RID: 1718 RVA: 0x0001E3E4 File Offset: 0x0001C5E4
		public override Size MinimumSize
		{
			get
			{
				return base.MinimumSize;
			}
			set
			{
				base.MinimumSize = new Size(value.Width, 0);
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value.</returns>
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x0001E3FC File Offset: 0x0001C5FC
		// (set) Token: 0x060006B8 RID: 1720 RVA: 0x0001E404 File Offset: 0x0001C604
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public new Padding Padding
		{
			get
			{
				return base.Padding;
			}
			set
			{
				base.Padding = value;
			}
		}

		/// <summary>Gets the preferred height of the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <returns>The preferred height, in pixels, of the item area of the combo box.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x0001E410 File Offset: 0x0001C610
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public int PreferredHeight
		{
			get
			{
				return this.Font.Height + 8;
			}
		}

		/// <summary>Gets or sets the index specifying the currently selected item.</summary>
		/// <returns>A zero-based index of the currently selected item. A value of negative one (-1) is returned if no item is selected.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified index is less than or equal to -2.-or- The specified index is greater than or equal to the number of items in the combo box. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x0001E420 File Offset: 0x0001C620
		// (set) Token: 0x060006BB RID: 1723 RVA: 0x0001E428 File Offset: 0x0001C628
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public override int SelectedIndex
		{
			get
			{
				return this.selected_index;
			}
			set
			{
				this.SetSelectedIndex(value, false);
			}
		}

		/// <summary>Gets or sets currently selected item in the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <returns>The object that is the currently selected item or null if there is no currently selected item.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x0001E434 File Offset: 0x0001C634
		// (set) Token: 0x060006BD RID: 1725 RVA: 0x0001E45C File Offset: 0x0001C65C
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[Bindable(true)]
		public object SelectedItem
		{
			get
			{
				return (this.selected_index != -1) ? this.Items[this.selected_index] : null;
			}
			set
			{
				object obj = ((this.selected_index != -1) ? this.Items[this.selected_index] : null);
				if (obj == value)
				{
					return;
				}
				if (value == null)
				{
					this.SelectedIndex = -1;
				}
				else
				{
					this.SelectedIndex = this.Items.IndexOf(value);
				}
			}
		}

		/// <summary>Gets or sets the text that is selected in the editable portion of a <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <returns>A string that represents the currently selected text in the combo box. If <see cref="P:System.Windows.Forms.ComboBox.DropDownStyle" /> is set to <see cref="F:System.Windows.Forms.ComboBoxStyle.DropDownList" />, the return value is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x0001E4BC File Offset: 0x0001C6BC
		// (set) Token: 0x060006BF RID: 1727 RVA: 0x0001E4E8 File Offset: 0x0001C6E8
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public string SelectedText
		{
			get
			{
				if (this.dropdown_style == ComboBoxStyle.DropDownList)
				{
					return string.Empty;
				}
				return this.textbox_ctrl.SelectedText;
			}
			set
			{
				if (this.dropdown_style == ComboBoxStyle.DropDownList)
				{
					return;
				}
				this.textbox_ctrl.SelectedText = value;
			}
		}

		/// <summary>Gets or sets the number of characters selected in the editable portion of the combo box.</summary>
		/// <returns>The number of characters selected in the combo box.</returns>
		/// <exception cref="T:System.ArgumentException">The value was less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0001E504 File Offset: 0x0001C704
		// (set) Token: 0x060006C1 RID: 1729 RVA: 0x0001E53C File Offset: 0x0001C73C
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public int SelectionLength
		{
			get
			{
				if (this.dropdown_style == ComboBoxStyle.DropDownList)
				{
					return 0;
				}
				int selectionLength = this.textbox_ctrl.SelectionLength;
				return (selectionLength != -1) ? selectionLength : 0;
			}
			set
			{
				if (this.dropdown_style == ComboBoxStyle.DropDownList)
				{
					return;
				}
				if (this.textbox_ctrl.SelectionLength == value)
				{
					return;
				}
				this.textbox_ctrl.SelectionLength = value;
			}
		}

		/// <summary>Gets or sets the starting index of text selected in the combo box.</summary>
		/// <returns>The zero-based index of the first character in the string of the current text selection.</returns>
		/// <exception cref="T:System.ArgumentException">The value is less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x0001E56C File Offset: 0x0001C76C
		// (set) Token: 0x060006C3 RID: 1731 RVA: 0x0001E588 File Offset: 0x0001C788
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public int SelectionStart
		{
			get
			{
				if (this.dropdown_style == ComboBoxStyle.DropDownList)
				{
					return 0;
				}
				return this.textbox_ctrl.SelectionStart;
			}
			set
			{
				if (this.dropdown_style == ComboBoxStyle.DropDownList)
				{
					return;
				}
				if (this.textbox_ctrl.SelectionStart == value)
				{
					return;
				}
				this.textbox_ctrl.SelectionStart = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the items in the combo box are sorted.</summary>
		/// <returns>true if the combo box is sorted; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt was made to sort a <see cref="T:System.Windows.Forms.ComboBox" /> that is attached to a data source. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x0001E5B8 File Offset: 0x0001C7B8
		// (set) Token: 0x060006C5 RID: 1733 RVA: 0x0001E5C0 File Offset: 0x0001C7C0
		[MWFCategory("Behavior")]
		[DefaultValue(false)]
		public bool Sorted
		{
			get
			{
				return this.sorted;
			}
			set
			{
				if (this.sorted == value)
				{
					return;
				}
				this.sorted = value;
				this.SelectedIndex = -1;
				if (this.sorted)
				{
					this.Items.Sort();
					this.LayoutComboBox();
				}
			}
		}

		/// <summary>Gets or sets the text associated with this control.</summary>
		/// <returns>The text associated with this control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x0001E5FC File Offset: 0x0001C7FC
		// (set) Token: 0x060006C7 RID: 1735 RVA: 0x0001E64C File Offset: 0x0001C84C
		[Bindable(true)]
		[Localizable(true)]
		public override string Text
		{
			get
			{
				if (this.dropdown_style != ComboBoxStyle.DropDownList && this.textbox_ctrl != null)
				{
					return this.textbox_ctrl.Text;
				}
				if (this.SelectedItem != null)
				{
					return base.GetItemText(this.SelectedItem);
				}
				return base.Text;
			}
			set
			{
				if (value == null)
				{
					if (this.SelectedIndex == -1)
					{
						if (this.dropdown_style != ComboBoxStyle.DropDownList)
						{
							this.SetControlText(string.Empty, false);
						}
					}
					else
					{
						this.SelectedIndex = -1;
					}
					return;
				}
				if (this.SelectedItem != null && string.Compare(value, base.GetItemText(this.SelectedItem), false, CultureInfo.CurrentCulture) == 0)
				{
					return;
				}
				int num = this.FindStringExact(value, -1, false);
				if (num == -1)
				{
					num = this.FindStringExact(value, -1, true);
				}
				if (num != -1)
				{
					this.SelectedIndex = num;
					return;
				}
				if (this.dropdown_style != ComboBoxStyle.DropDownList)
				{
					this.textbox_ctrl.Text = value;
				}
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x0001E6FC File Offset: 0x0001C8FC
		internal Rectangle ButtonArea
		{
			get
			{
				return this.button_area;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x0001E704 File Offset: 0x0001C904
		internal Rectangle TextArea
		{
			get
			{
				return this.text_area;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x0001E70C File Offset: 0x0001C90C
		internal TextBox UIATextBox
		{
			get
			{
				return this.textbox_ctrl;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x0001E714 File Offset: 0x0001C914
		internal ComboBox.ComboListBox UIAComboListBox
		{
			get
			{
				return this.listbox_ctrl;
			}
		}

		/// <summary>Adds the specified items to the combo box.</summary>
		/// <param name="value">The items to add.</param>
		// Token: 0x060006CC RID: 1740 RVA: 0x0001E71C File Offset: 0x0001C91C
		[Obsolete("This method has been deprecated")]
		protected virtual void AddItemsCore(object[] value)
		{
		}

		/// <summary>Maintains performance when items are added to the <see cref="T:System.Windows.Forms.ComboBox" /> one at a time.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060006CD RID: 1741 RVA: 0x0001E720 File Offset: 0x0001C920
		public void BeginUpdate()
		{
			this.suspend_ctrlupdate = true;
		}

		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the control.</returns>
		// Token: 0x060006CE RID: 1742 RVA: 0x0001E72C File Offset: 0x0001C92C
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return base.CreateAccessibilityInstance();
		}

		/// <summary>Creates a handle for the control.</summary>
		// Token: 0x060006CF RID: 1743 RVA: 0x0001E734 File Offset: 0x0001C934
		protected override void CreateHandle()
		{
			base.CreateHandle();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ComboBox" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060006D0 RID: 1744 RVA: 0x0001E73C File Offset: 0x0001C93C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.listbox_ctrl != null)
				{
					this.listbox_ctrl.Dispose();
					base.Controls.RemoveImplicit(this.listbox_ctrl);
					this.listbox_ctrl = null;
				}
				if (this.textbox_ctrl != null)
				{
					base.Controls.RemoveImplicit(this.textbox_ctrl);
					this.textbox_ctrl.Dispose();
					this.textbox_ctrl = null;
				}
			}
			base.Dispose(disposing);
		}

		/// <summary>Resumes painting the <see cref="T:System.Windows.Forms.ComboBox" /> control after painting is suspended by the <see cref="M:System.Windows.Forms.ComboBox.BeginUpdate" /> method.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060006D1 RID: 1745 RVA: 0x0001E7B4 File Offset: 0x0001C9B4
		public void EndUpdate()
		{
			this.suspend_ctrlupdate = false;
			this.UpdatedItems();
			this.Refresh();
		}

		/// <summary>Returns the index of the first item in the <see cref="T:System.Windows.Forms.ComboBox" /> that starts with the specified string.</summary>
		/// <returns>The zero-based index of the first item found; returns -1 if no match is found.</returns>
		/// <param name="s">The <see cref="T:System.String" /> to search for. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006D2 RID: 1746 RVA: 0x0001E7CC File Offset: 0x0001C9CC
		public int FindString(string s)
		{
			return this.FindString(s, -1);
		}

		/// <summary>Returns the index of the first item in the <see cref="T:System.Windows.Forms.ComboBox" /> beyond the specified index that contains the specified string. The search is not case sensitive.</summary>
		/// <returns>The zero-based index of the first item found; returns -1 if no match is found, or 0 if the <paramref name="s" /> parameter specifies <see cref="F:System.String.Empty" />.</returns>
		/// <param name="s">The <see cref="T:System.String" /> to search for. </param>
		/// <param name="startIndex">The zero-based index of the item before the first item to be searched. Set to -1 to search from the beginning of the control. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="startIndex" /> is less than -1.-or- The <paramref name="startIndex" /> is greater than the last index in the collection. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006D3 RID: 1747 RVA: 0x0001E7D8 File Offset: 0x0001C9D8
		public int FindString(string s, int startIndex)
		{
			if (s == null || this.Items.Count == 0)
			{
				return -1;
			}
			if (startIndex < -1 || startIndex >= this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			int num = startIndex;
			if (num == this.Items.Count - 1)
			{
				num = -1;
			}
			for (;;)
			{
				num++;
				if (string.Compare(s, 0, base.GetItemText(this.Items[num]), 0, s.Length, true) == 0)
				{
					break;
				}
				if (num == this.Items.Count - 1)
				{
					num = -1;
				}
				if (num == startIndex)
				{
					return -1;
				}
			}
			return num;
		}

		/// <summary>Finds the first item in the combo box that matches the specified string.</summary>
		/// <returns>The zero-based index of the first item found; returns -1 if no match is found, or 0 if the <paramref name="s" /> parameter specifies <see cref="F:System.String.Empty" />.</returns>
		/// <param name="s">The <see cref="T:System.String" /> to search for. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006D4 RID: 1748 RVA: 0x0001E880 File Offset: 0x0001CA80
		public int FindStringExact(string s)
		{
			return this.FindStringExact(s, -1);
		}

		/// <summary>Finds the first item after the specified index that matches the specified string.</summary>
		/// <returns>The zero-based index of the first item found; returns -1 if no match is found, or 0 if the <paramref name="s" /> parameter specifies <see cref="F:System.String.Empty" />.</returns>
		/// <param name="s">The <see cref="T:System.String" /> to search for. </param>
		/// <param name="startIndex">The zero-based index of the item before the first item to be searched. Set to -1 to search from the beginning of the control. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="startIndex" /> is less than -1.-or- The <paramref name="startIndex" /> is equal to the last index in the collection. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006D5 RID: 1749 RVA: 0x0001E88C File Offset: 0x0001CA8C
		public int FindStringExact(string s, int startIndex)
		{
			return this.FindStringExact(s, startIndex, true);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0001E898 File Offset: 0x0001CA98
		private int FindStringExact(string s, int startIndex, bool ignoreCase)
		{
			if (s == null || this.Items.Count == 0)
			{
				return -1;
			}
			if (startIndex < -1 || startIndex >= this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			int num = startIndex;
			if (num == this.Items.Count - 1)
			{
				num = -1;
			}
			for (;;)
			{
				num++;
				if (string.Compare(s, base.GetItemText(this.Items[num]), ignoreCase, CultureInfo.CurrentCulture) == 0)
				{
					break;
				}
				if (num == this.Items.Count - 1)
				{
					num = -1;
				}
				if (num == startIndex)
				{
					return -1;
				}
			}
			return num;
		}

		/// <summary>Returns the height of an item in the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <returns>The height, in pixels, of the item at the specified index.</returns>
		/// <param name="index">The index of the item to return the height of. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> is less than zero.-or- The <paramref name="index" /> is greater than count of items in the list. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060006D7 RID: 1751 RVA: 0x0001E940 File Offset: 0x0001CB40
		public int GetItemHeight(int index)
		{
			if (this.DrawMode != DrawMode.OwnerDrawVariable || !base.IsHandleCreated)
			{
				return this.ItemHeight;
			}
			if (index < 0 || index >= this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("The item height value is less than zero");
			}
			object obj = this.Items[index];
			if (this.item_heights.Contains(obj))
			{
				return (int)this.item_heights[obj];
			}
			MeasureItemEventArgs measureItemEventArgs = new MeasureItemEventArgs(base.DeviceContext, index, this.ItemHeight);
			this.OnMeasureItem(measureItemEventArgs);
			this.item_heights[obj] = measureItemEventArgs.ItemHeight;
			return measureItemEventArgs.ItemHeight;
		}

		/// <summary>Determines whether the specified key is a regular input key or a special key that requires preprocessing.</summary>
		/// <returns>true if the specified key is a regular input key; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values.</param>
		// Token: 0x060006D8 RID: 1752 RVA: 0x0001E9F8 File Offset: 0x0001CBF8
		protected override bool IsInputKey(Keys keyData)
		{
			switch (keyData & Keys.KeyCode)
			{
			case Keys.PageUp:
			case Keys.PageDown:
			case Keys.End:
			case Keys.Home:
			case Keys.Left:
			case Keys.Up:
			case Keys.Right:
			case Keys.Down:
				return true;
			default:
				return false;
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060006D9 RID: 1753 RVA: 0x0001EA40 File Offset: 0x0001CC40
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
			if (this.textbox_ctrl != null)
			{
				this.textbox_ctrl.BackColor = this.BackColor;
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060006DA RID: 1754 RVA: 0x0001EA68 File Offset: 0x0001CC68
		protected override void OnDataSourceChanged(EventArgs e)
		{
			base.OnDataSourceChanged(e);
			base.BindDataItems();
			if (this.DataSource == null || base.DataManager == null)
			{
				this.SelectedIndex = -1;
			}
			else
			{
				this.SelectedIndex = base.DataManager.Position;
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060006DB RID: 1755 RVA: 0x0001EAB8 File Offset: 0x0001CCB8
		protected override void OnDisplayMemberChanged(EventArgs e)
		{
			base.OnDisplayMemberChanged(e);
			if (base.DataManager == null)
			{
				return;
			}
			this.SelectedIndex = base.DataManager.Position;
			if (this.selected_index != -1 && this.DropDownStyle != ComboBoxStyle.DropDownList)
			{
				this.SetControlText(base.GetItemText(this.Items[this.selected_index]), true);
			}
			if (!base.IsHandleCreated)
			{
				return;
			}
			base.Invalidate();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ComboBox.DrawItem" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DrawItemEventArgs" /> that contains the event data. </param>
		// Token: 0x060006DC RID: 1756 RVA: 0x0001EB34 File Offset: 0x0001CD34
		protected virtual void OnDrawItem(DrawItemEventArgs e)
		{
			DrawItemEventHandler drawItemEventHandler = (DrawItemEventHandler)base.Events[ComboBox.DrawItemEvent];
			if (drawItemEventHandler != null)
			{
				drawItemEventHandler(this, e);
			}
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0001EB68 File Offset: 0x0001CD68
		internal void HandleDrawItem(DrawItemEventArgs e)
		{
			DrawMode drawMode = this.DrawMode;
			if (drawMode != DrawMode.OwnerDrawFixed && drawMode != DrawMode.OwnerDrawVariable)
			{
				ThemeEngine.Current.DrawComboBoxItem(this, e);
			}
			else
			{
				this.OnDrawItem(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ComboBox.DropDown" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060006DE RID: 1758 RVA: 0x0001EBAC File Offset: 0x0001CDAC
		protected virtual void OnDropDown(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ComboBox.DropDownEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ComboBox.DropDownClosed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060006DF RID: 1759 RVA: 0x0001EBE0 File Offset: 0x0001CDE0
		protected virtual void OnDropDownClosed(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ComboBox.DropDownClosedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ComboBox.DropDownStyleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060006E0 RID: 1760 RVA: 0x0001EC14 File Offset: 0x0001CE14
		protected virtual void OnDropDownStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ComboBox.DropDownStyleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060006E1 RID: 1761 RVA: 0x0001EC48 File Offset: 0x0001CE48
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			if (this.textbox_ctrl != null)
			{
				this.textbox_ctrl.Font = this.Font;
			}
			if (!this.item_height_specified)
			{
				this.item_height = this.Font.Height + 2;
			}
			if (this.IntegralHeight)
			{
				this.UpdateComboBoxBounds();
			}
			this.LayoutComboBox();
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060006E2 RID: 1762 RVA: 0x0001ECB0 File Offset: 0x0001CEB0
		protected override void OnForeColorChanged(EventArgs e)
		{
			base.OnForeColorChanged(e);
			if (this.textbox_ctrl != null)
			{
				this.textbox_ctrl.ForeColor = this.ForeColor;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060006E3 RID: 1763 RVA: 0x0001ECD8 File Offset: 0x0001CED8
		[EditorBrowsable(2)]
		protected override void OnGotFocus(EventArgs e)
		{
			if (this.dropdown_style == ComboBoxStyle.DropDownList)
			{
				base.Invalidate();
			}
			if (this.textbox_ctrl != null)
			{
				this.textbox_ctrl.SetSelectable(false);
				this.textbox_ctrl.ShowSelection = true;
				this.textbox_ctrl.ActivateCaret(true);
				this.textbox_ctrl.SelectAll();
			}
			base.OnGotFocus(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060006E4 RID: 1764 RVA: 0x0001ED38 File Offset: 0x0001CF38
		[EditorBrowsable(2)]
		protected override void OnLostFocus(EventArgs e)
		{
			if (this.dropdown_style == ComboBoxStyle.DropDownList)
			{
				base.Invalidate();
			}
			if (this.listbox_ctrl != null && this.dropped_down)
			{
				this.listbox_ctrl.HideWindow();
			}
			if (this.textbox_ctrl != null)
			{
				this.textbox_ctrl.SetSelectable(true);
				this.textbox_ctrl.ActivateCaret(false);
				this.textbox_ctrl.ShowSelection = false;
				this.textbox_ctrl.SelectionLength = 0;
				this.textbox_ctrl.HideAutoCompleteList();
			}
			base.OnLostFocus(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060006E5 RID: 1765 RVA: 0x0001EDC8 File Offset: 0x0001CFC8
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			base.SetBoundsInternal(base.Left, base.Top, base.Width, this.PreferredHeight, BoundsSpecified.None);
			if (this.textbox_ctrl != null)
			{
				base.Controls.AddImplicit(this.textbox_ctrl);
			}
			this.LayoutComboBox();
			this.UpdateComboBoxBounds();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060006E6 RID: 1766 RVA: 0x0001EE24 File Offset: 0x0001D024
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data.</param>
		// Token: 0x060006E7 RID: 1767 RVA: 0x0001EE30 File Offset: 0x0001D030
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			if (this.dropdown_style == ComboBoxStyle.DropDownList)
			{
				int num = this.FindStringCaseInsensitive(e.KeyChar.ToString(), this.SelectedIndex + 1);
				if (num != -1)
				{
					this.SelectedIndex = num;
					if (this.DroppedDown)
					{
						if (this.SelectedIndex >= this.listbox_ctrl.LastVisibleItem())
						{
							this.listbox_ctrl.Scroll(this.SelectedIndex - this.listbox_ctrl.LastVisibleItem() + 1);
						}
						if (this.SelectedIndex < this.listbox_ctrl.FirstVisibleItem())
						{
							this.listbox_ctrl.Scroll(this.SelectedIndex - this.listbox_ctrl.FirstVisibleItem());
						}
					}
				}
			}
			base.OnKeyPress(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ComboBox.MeasureItem" /> event.</summary>
		/// <param name="e">The <see cref="T:System.Windows.Forms.MeasureItemEventArgs" /> that was raised. </param>
		// Token: 0x060006E8 RID: 1768 RVA: 0x0001EEF0 File Offset: 0x0001D0F0
		protected virtual void OnMeasureItem(MeasureItemEventArgs e)
		{
			MeasureItemEventHandler measureItemEventHandler = (MeasureItemEventHandler)base.Events[ComboBox.MeasureItemEvent];
			if (measureItemEventHandler != null)
			{
				measureItemEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.  </param>
		// Token: 0x060006E9 RID: 1769 RVA: 0x0001EF24 File Offset: 0x0001D124
		protected override void OnParentBackColorChanged(EventArgs e)
		{
			base.OnParentBackColorChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060006EA RID: 1770 RVA: 0x0001EF30 File Offset: 0x0001D130
		protected override void OnResize(EventArgs e)
		{
			this.LayoutComboBox();
			if (this.listbox_ctrl != null)
			{
				this.listbox_ctrl.CalcListBoxArea();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ComboBox.SelectedIndexChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060006EB RID: 1771 RVA: 0x0001EF50 File Offset: 0x0001D150
		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			base.OnSelectedIndexChanged(e);
			EventHandler eventHandler = (EventHandler)base.Events[ComboBox.SelectedIndexChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DomainUpDown.SelectedItemChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060006EC RID: 1772 RVA: 0x0001EF88 File Offset: 0x0001D188
		protected virtual void OnSelectedItemChanged(EventArgs e)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.SelectedValueChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060006ED RID: 1773 RVA: 0x0001EF8C File Offset: 0x0001D18C
		protected override void OnSelectedValueChanged(EventArgs e)
		{
			base.OnSelectedValueChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ComboBox.SelectionChangeCommitted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060006EE RID: 1774 RVA: 0x0001EF98 File Offset: 0x0001D198
		protected virtual void OnSelectionChangeCommitted(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ComboBox.SelectionChangeCommittedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Refreshes the item contained at the specified location.</summary>
		/// <param name="index">The location of the item to refresh.</param>
		// Token: 0x060006EF RID: 1775 RVA: 0x0001EFCC File Offset: 0x0001D1CC
		protected override void RefreshItem(int index)
		{
			if (index < 0 || index >= this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (this.draw_mode == DrawMode.OwnerDrawVariable)
			{
				this.item_heights.Remove(this.Items[index]);
			}
		}

		/// <summary>Refreshes all <see cref="T:System.Windows.Forms.ComboBox" /> items.</summary>
		// Token: 0x060006F0 RID: 1776 RVA: 0x0001F020 File Offset: 0x0001D220
		protected override void RefreshItems()
		{
			for (int i = 0; i < this.Items.Count; i++)
			{
				this.RefreshItem(i);
			}
			this.LayoutComboBox();
			this.Refresh();
			if (this.selected_index != -1 && this.DropDownStyle != ComboBoxStyle.DropDownList)
			{
				this.SetControlText(base.GetItemText(this.Items[this.selected_index]), false);
			}
		}

		/// <filterpriority>1</filterpriority>
		// Token: 0x060006F1 RID: 1777 RVA: 0x0001F094 File Offset: 0x0001D294
		public override void ResetText()
		{
			this.Text = string.Empty;
		}

		/// <returns>true if the message was processed by the control; otherwise, false.</returns>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process. </param>
		// Token: 0x060006F2 RID: 1778 RVA: 0x0001F0A4 File Offset: 0x0001D2A4
		protected override bool ProcessKeyEventArgs(ref Message m)
		{
			return base.ProcessKeyEventArgs(ref m);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
		// Token: 0x060006F3 RID: 1779 RVA: 0x0001F0B0 File Offset: 0x0001D2B0
		[EditorBrowsable(2)]
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Validating" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data. </param>
		// Token: 0x060006F4 RID: 1780 RVA: 0x0001F0BC File Offset: 0x0001D2BC
		[EditorBrowsable(2)]
		protected override void OnValidating(CancelEventArgs e)
		{
			base.OnValidating(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060006F5 RID: 1781 RVA: 0x0001F0C8 File Offset: 0x0001D2C8
		[EditorBrowsable(2)]
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ComboBox.TextUpdate" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060006F6 RID: 1782 RVA: 0x0001F0D4 File Offset: 0x0001D2D4
		protected virtual void OnTextUpdate(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ComboBox.TextUpdateEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060006F7 RID: 1783 RVA: 0x0001F108 File Offset: 0x0001D308
		protected override void OnMouseLeave(EventArgs e)
		{
			if (this.flat_style == FlatStyle.Popup)
			{
				base.Invalidate();
			}
			base.OnMouseLeave(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060006F8 RID: 1784 RVA: 0x0001F124 File Offset: 0x0001D324
		protected override void OnMouseEnter(EventArgs e)
		{
			if (this.flat_style == FlatStyle.Popup)
			{
				base.Invalidate();
			}
			base.OnMouseEnter(e);
		}

		/// <param name="factor">The factor by which the height and width of the control will be scaled.</param>
		/// <param name="specified">A <see cref="T:System.Windows.Forms.BoundsSpecified" /> value that specifies the bounds of the control to use when defining its size and position.</param>
		// Token: 0x060006F9 RID: 1785 RVA: 0x0001F140 File Offset: 0x0001D340
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			base.ScaleControl(factor, specified);
		}

		/// <summary>Selects a range of text in the editable portion of the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <param name="start">The position of the first character in the current text selection within the text box. </param>
		/// <param name="length">The number of characters to select. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="start" /> is less than zero.-or- <paramref name="start" /> plus <paramref name="length" /> is less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060006FA RID: 1786 RVA: 0x0001F14C File Offset: 0x0001D34C
		public void Select(int start, int length)
		{
			if (start < 0)
			{
				throw new ArgumentException("Start cannot be less than zero");
			}
			if (length < 0)
			{
				throw new ArgumentException("length cannot be less than zero");
			}
			if (this.dropdown_style == ComboBoxStyle.DropDownList)
			{
				return;
			}
			this.textbox_ctrl.Select(start, length);
		}

		/// <summary>Selects all the text in the editable portion of the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060006FB RID: 1787 RVA: 0x0001F198 File Offset: 0x0001D398
		public void SelectAll()
		{
			if (this.dropdown_style == ComboBoxStyle.DropDownList)
			{
				return;
			}
			if (this.textbox_ctrl != null)
			{
				this.textbox_ctrl.ShowSelection = true;
				this.textbox_ctrl.SelectAll();
			}
		}

		/// <summary>Sets the size and location of the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <param name="x">The horizontal location in pixels of the control. </param>
		/// <param name="y">The vertical location in pixels of the control. </param>
		/// <param name="width">The width in pixels of the control. </param>
		/// <param name="height">The height in pixels of the control. </param>
		/// <param name="specified">One of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values. </param>
		// Token: 0x060006FC RID: 1788 RVA: 0x0001F1CC File Offset: 0x0001D3CC
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			bool flag = (this.Anchor & AnchorStyles.Top) != AnchorStyles.None && (this.Anchor & AnchorStyles.Bottom) != AnchorStyles.None;
			bool flag2 = this.Dock == DockStyle.Left || this.Dock == DockStyle.Right || this.Dock == DockStyle.Fill;
			if ((specified & BoundsSpecified.Height) != BoundsSpecified.None || (specified == BoundsSpecified.None && (flag || flag2)))
			{
				this.requested_height = height;
				height = this.SnapHeight(height);
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		/// <summary>When overridden in a derived class, sets the object with the specified index in the derived class.</summary>
		/// <param name="index">The array index of the object.</param>
		/// <param name="value">The object.</param>
		// Token: 0x060006FD RID: 1789 RVA: 0x0001F258 File Offset: 0x0001D458
		protected override void SetItemCore(int index, object value)
		{
			if (index < 0 || index >= this.Items.Count)
			{
				return;
			}
			this.Items[index] = value;
		}

		/// <summary>When overridden in a derived class, sets the specified array of objects in a collection in the derived class.</summary>
		/// <param name="value">An array of items.</param>
		// Token: 0x060006FE RID: 1790 RVA: 0x0001F28C File Offset: 0x0001D48C
		protected override void SetItemsCore(IList value)
		{
			this.BeginUpdate();
			try
			{
				this.Items.Clear();
				this.Items.AddRange(value);
			}
			finally
			{
				this.EndUpdate();
			}
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.ComboBox" /> control.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Windows.Forms.ComboBox" />. The string includes the type and the number of items in the <see cref="T:System.Windows.Forms.ComboBox" /> control.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006FF RID: 1791 RVA: 0x0001F2E0 File Offset: 0x0001D4E0
		public override string ToString()
		{
			return base.ToString() + ", Items.Count:" + this.Items.Count;
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process. </param>
		// Token: 0x06000700 RID: 1792 RVA: 0x0001F310 File Offset: 0x0001D510
		protected override void WndProc(ref Message m)
		{
			Msg msg = (Msg)m.Msg;
			switch (msg)
			{
			case Msg.WM_KEYDOWN:
			case Msg.WM_KEYUP:
			{
				Keys keys = (Keys)m.WParam.ToInt32();
				if (this.textbox_ctrl != null && this.textbox_ctrl.CanNavigateAutoCompleteList)
				{
					XplatUI.SendMessage(this.textbox_ctrl.Handle, (Msg)m.Msg, m.WParam, m.LParam);
					return;
				}
				if (keys == Keys.Up || keys == Keys.Down)
				{
					goto IL_00FE;
				}
				break;
			}
			case Msg.WM_CHAR:
				break;
			default:
			{
				if (msg != Msg.WM_MOUSELEAVE)
				{
					goto IL_00FE;
				}
				Point point = base.PointToClient(Control.MousePosition);
				if (base.ClientRectangle.Contains(point))
				{
					return;
				}
				goto IL_00FE;
			}
			}
			if (!this.ProcessKeyMessage(ref m) && this.textbox_ctrl != null)
			{
				XplatUI.SendMessage(this.textbox_ctrl.Handle, (Msg)m.Msg, m.WParam, m.LParam);
			}
			return;
			IL_00FE:
			base.WndProc(ref m);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0001F424 File Offset: 0x0001D624
		private void OnAutoCompleteCustomSourceChanged(object sender, CollectionChangeEventArgs e)
		{
			if (this.auto_complete_source == AutoCompleteSource.CustomSource)
			{
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x0001F434 File Offset: 0x0001D634
		// (set) Token: 0x06000703 RID: 1795 RVA: 0x0001F43C File Offset: 0x0001D63C
		internal override bool InternalCapture
		{
			get
			{
				return base.Capture;
			}
			set
			{
			}
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0001F440 File Offset: 0x0001D640
		private void LayoutComboBox()
		{
			int width = ThemeEngine.Current.Border3DSize.Width;
			this.text_area = base.ClientRectangle;
			this.text_area.Height = this.PreferredHeight;
			this.listbox_area = base.ClientRectangle;
			this.listbox_area.Y = this.text_area.Bottom + 3;
			this.listbox_area.Height = this.listbox_area.Height - (this.text_area.Height + 2);
			Rectangle rectangle = this.button_area;
			if (this.DropDownStyle == ComboBoxStyle.Simple)
			{
				this.button_area = Rectangle.Empty;
			}
			else
			{
				this.button_area = this.text_area;
				this.button_area.X = this.text_area.Right - 16 - width;
				this.button_area.Y = this.text_area.Y + width;
				this.button_area.Width = 16;
				this.button_area.Height = this.text_area.Height - 2 * width;
				if (this.flat_style == FlatStyle.Popup || this.flat_style == FlatStyle.Flat)
				{
					this.button_area.Inflate(1, 1);
					this.button_area.X = this.button_area.X + 2;
					this.button_area.Width = this.button_area.Width - 2;
				}
			}
			if (this.button_area != rectangle)
			{
				rectangle.Y -= width;
				rectangle.Width += width;
				rectangle.Height += 2 * width;
				base.Invalidate(rectangle);
				base.Invalidate(this.button_area);
			}
			if (this.textbox_ctrl != null)
			{
				int num = width + 1;
				this.textbox_ctrl.Location = new Point(this.text_area.X + num, this.text_area.Y + num);
				this.textbox_ctrl.Width = this.text_area.Width - this.button_area.Width - num * 2;
				this.textbox_ctrl.Height = this.text_area.Height - num * 2;
			}
			if (this.listbox_ctrl != null && this.dropdown_style == ComboBoxStyle.Simple)
			{
				this.listbox_ctrl.Location = this.listbox_area.Location;
				this.listbox_ctrl.CalcListBoxArea();
			}
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0001F698 File Offset: 0x0001D898
		private void CreateComboListBox()
		{
			this.listbox_ctrl = new ComboBox.ComboListBox(this);
			this.listbox_ctrl.HighlightedIndex = this.SelectedIndex;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0001F6B8 File Offset: 0x0001D8B8
		internal void Draw(Rectangle clip, Graphics dc)
		{
			Theme theme = ThemeEngine.Current;
			FlatStyle flatStyle = this.FlatStyle;
			bool flag = flatStyle == FlatStyle.Flat || flatStyle == FlatStyle.Popup;
			theme.ComboBoxDrawBackground(this, dc, clip, flatStyle);
			int width = theme.Border3DSize.Width;
			if (this.dropdown_style == ComboBoxStyle.DropDownList)
			{
				DrawItemState drawItemState = DrawItemState.None;
				Color color = this.BackColor;
				Color color2 = this.ForeColor;
				Rectangle rectangle = this.text_area;
				rectangle.X += width;
				rectangle.Y += width;
				rectangle.Width -= this.button_area.Width + 2 * width;
				rectangle.Height -= 2 * width;
				if (this.Focused)
				{
					drawItemState = DrawItemState.Selected;
					drawItemState |= DrawItemState.Focus;
					color = SystemColors.Highlight;
					color2 = SystemColors.HighlightText;
				}
				drawItemState |= DrawItemState.ComboBoxEdit;
				this.HandleDrawItem(new DrawItemEventArgs(dc, this.Font, rectangle, this.SelectedIndex, drawItemState, color2, color));
			}
			if (this.show_dropdown_button)
			{
				ButtonState buttonState;
				if (this.is_enabled)
				{
					buttonState = this.button_state;
				}
				else
				{
					buttonState = ButtonState.Inactive;
				}
				if (flag || theme.ComboBoxNormalDropDownButtonHasTransparentBackground(this, buttonState))
				{
					dc.FillRectangle(theme.ResPool.GetSolidBrush(theme.ColorControl), this.button_area);
				}
				if (flag)
				{
					theme.DrawFlatStyleComboButton(dc, this.button_area, buttonState);
				}
				else
				{
					theme.ComboBoxDrawNormalDropDownButton(this, dc, clip, this.button_area, buttonState);
				}
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x0001F844 File Offset: 0x0001DA44
		// (set) Token: 0x06000708 RID: 1800 RVA: 0x0001F84C File Offset: 0x0001DA4C
		internal bool DropDownButtonEntered
		{
			get
			{
				return this.drop_down_button_entered;
			}
			private set
			{
				if (this.drop_down_button_entered == value)
				{
					return;
				}
				this.drop_down_button_entered = value;
				if (ThemeEngine.Current.ComboBoxDropDownButtonHasHotElementStyle(this))
				{
					base.Invalidate(this.button_area);
				}
			}
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0001F88C File Offset: 0x0001DA8C
		internal void DropDownListBox()
		{
			this.DropDownButtonEntered = false;
			if (this.DropDownStyle == ComboBoxStyle.Simple)
			{
				return;
			}
			if (this.listbox_ctrl == null)
			{
				this.CreateComboListBox();
			}
			this.listbox_ctrl.Location = base.PointToScreen(new Point(this.text_area.X, this.text_area.Y + this.text_area.Height));
			this.FindMatchOrSetIndex(this.SelectedIndex);
			if (this.textbox_ctrl != null)
			{
				this.textbox_ctrl.HideAutoCompleteList();
			}
			if (this.listbox_ctrl.ShowWindow())
			{
				this.dropped_down = true;
			}
			this.button_state = ButtonState.Pushed;
			if (this.dropdown_style == ComboBoxStyle.DropDownList)
			{
				base.Invalidate(this.text_area);
			}
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001F954 File Offset: 0x0001DB54
		internal void DropDownListBoxFinished()
		{
			if (this.DropDownStyle == ComboBoxStyle.Simple)
			{
				return;
			}
			this.FindMatchOrSetIndex(this.SelectedIndex);
			this.button_state = ButtonState.Normal;
			base.Invalidate(this.button_area);
			this.dropped_down = false;
			this.OnDropDownClosed(EventArgs.Empty);
			if (this.listbox_ctrl != null)
			{
				this.listbox_ctrl.Dispose();
				this.listbox_ctrl = null;
			}
			if (this.textbox_ctrl != null)
			{
				this.textbox_ctrl.HideAutoCompleteList();
			}
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001F9D4 File Offset: 0x0001DBD4
		private int FindStringCaseInsensitive(string search)
		{
			if (search.Length == 0)
			{
				return -1;
			}
			for (int i = 0; i < this.Items.Count; i++)
			{
				if (string.Compare(base.GetItemText(this.Items[i]), 0, search, 0, search.Length, true) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001FA34 File Offset: 0x0001DC34
		internal int FindStringCaseInsensitive(string search, int start_index)
		{
			if (search.Length == 0)
			{
				return -1;
			}
			if (start_index < 0 || start_index > this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("start_index");
			}
			for (int i = 0; i < this.Items.Count; i++)
			{
				int num = (i + start_index) % this.Items.Count;
				if (string.Compare(base.GetItemText(this.Items[num]), 0, search, 0, search.Length, true) == 0)
				{
					return num;
				}
			}
			return -1;
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0001FAC8 File Offset: 0x0001DCC8
		internal override bool IsInputCharInternal(char charCode)
		{
			return true;
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x0001FACC File Offset: 0x0001DCCC
		// (set) Token: 0x0600070F RID: 1807 RVA: 0x0001FAD4 File Offset: 0x0001DCD4
		internal override ContextMenu ContextMenuInternal
		{
			get
			{
				return base.ContextMenuInternal;
			}
			set
			{
				base.ContextMenuInternal = value;
				if (this.textbox_ctrl != null)
				{
					this.textbox_ctrl.ContextMenu = value;
				}
			}
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0001FAF4 File Offset: 0x0001DCF4
		internal void RestoreContextMenu()
		{
			this.textbox_ctrl.RestoreContextMenu();
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0001FB04 File Offset: 0x0001DD04
		private void OnKeyDownCB(object sender, KeyEventArgs e)
		{
			if (this.Items.Count == 0)
			{
				return;
			}
			Keys keyCode = e.KeyCode;
			switch (keyCode)
			{
			case Keys.Escape:
				break;
			default:
				if (keyCode != Keys.Return)
				{
					return;
				}
				break;
			case Keys.PageUp:
			{
				int num = ((this.listbox_ctrl != null) ? (this.listbox_ctrl.page_size - 1) : (this.MaxDropDownItems - 1));
				if (num < 1)
				{
					num = 1;
				}
				this.SetSelectedIndex(Math.Max(this.SelectedIndex - num, 0), true);
				if (this.DroppedDown && this.SelectedIndex < this.listbox_ctrl.FirstVisibleItem())
				{
					this.listbox_ctrl.Scroll(this.SelectedIndex - this.listbox_ctrl.FirstVisibleItem());
				}
				return;
			}
			case Keys.PageDown:
			{
				if (this.SelectedIndex == -1)
				{
					this.SelectedIndex = 0;
					if (this.dropdown_style != ComboBoxStyle.Simple)
					{
						return;
					}
				}
				int num = ((this.listbox_ctrl != null) ? (this.listbox_ctrl.page_size - 1) : (this.MaxDropDownItems - 1));
				if (num < 1)
				{
					num = 1;
				}
				this.SetSelectedIndex(Math.Min(this.SelectedIndex + num, this.Items.Count - 1), true);
				if (this.DroppedDown && this.SelectedIndex >= this.listbox_ctrl.LastVisibleItem())
				{
					this.listbox_ctrl.Scroll(this.SelectedIndex - this.listbox_ctrl.LastVisibleItem() + 1);
				}
				return;
			}
			case Keys.End:
				if (this.dropdown_style == ComboBoxStyle.DropDownList)
				{
					this.SetSelectedIndex(this.Items.Count - 1, true);
					if (this.DroppedDown && this.SelectedIndex >= this.listbox_ctrl.LastVisibleItem())
					{
						this.listbox_ctrl.Scroll(this.SelectedIndex - this.listbox_ctrl.LastVisibleItem() + 1);
					}
				}
				return;
			case Keys.Home:
				if (this.dropdown_style == ComboBoxStyle.DropDownList)
				{
					this.SelectedIndex = 0;
					if (this.DroppedDown && this.SelectedIndex < this.listbox_ctrl.FirstVisibleItem())
					{
						this.listbox_ctrl.Scroll(this.SelectedIndex - this.listbox_ctrl.FirstVisibleItem());
					}
				}
				return;
			case Keys.Up:
				this.FindMatchOrSetIndex(Math.Max(this.SelectedIndex - 1, 0));
				if (this.DroppedDown && this.SelectedIndex < this.listbox_ctrl.FirstVisibleItem())
				{
					this.listbox_ctrl.Scroll(this.SelectedIndex - this.listbox_ctrl.FirstVisibleItem());
				}
				return;
			case Keys.Down:
				if ((e.Modifiers & Keys.Alt) == Keys.Alt)
				{
					this.DropDownListBox();
				}
				else
				{
					this.FindMatchOrSetIndex(Math.Min(this.SelectedIndex + 1, this.Items.Count - 1));
				}
				if (this.DroppedDown && this.SelectedIndex >= this.listbox_ctrl.LastVisibleItem())
				{
					this.listbox_ctrl.Scroll(this.SelectedIndex - this.listbox_ctrl.LastVisibleItem() + 1);
				}
				return;
			}
			if (this.listbox_ctrl != null && this.listbox_ctrl.Visible)
			{
				this.DropDownListBoxFinished();
			}
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0001FE78 File Offset: 0x0001E078
		private void SetSelectedIndex(int value, bool supressAutoScroll)
		{
			if (this.selected_index == value)
			{
				return;
			}
			if (value <= -2 || value >= this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("SelectedIndex");
			}
			this.selected_index = value;
			if (this.dropdown_style != ComboBoxStyle.DropDownList)
			{
				if (value == -1)
				{
					this.SetControlText(string.Empty, false, supressAutoScroll);
				}
				else
				{
					this.SetControlText(base.GetItemText(this.Items[value]), false, supressAutoScroll);
				}
			}
			if (this.DropDownStyle == ComboBoxStyle.DropDownList)
			{
				base.Invalidate();
			}
			if (this.listbox_ctrl != null)
			{
				this.listbox_ctrl.HighlightedIndex = value;
			}
			this.OnSelectedValueChanged(EventArgs.Empty);
			this.OnSelectedIndexChanged(EventArgs.Empty);
			this.OnSelectedItemChanged(EventArgs.Empty);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0001FF48 File Offset: 0x0001E148
		private void FindMatchOrSetIndex(int index)
		{
			int num = -1;
			if (this.SelectedIndex == -1 && this.Text.Length != 0)
			{
				num = this.FindStringCaseInsensitive(this.Text);
			}
			if (num != -1)
			{
				this.SetSelectedIndex(num, true);
			}
			else
			{
				this.SetSelectedIndex(index, true);
			}
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0001FF9C File Offset: 0x0001E19C
		private void OnMouseDownCB(object sender, MouseEventArgs e)
		{
			Rectangle clientRectangle;
			if (this.DropDownStyle == ComboBoxStyle.DropDownList)
			{
				clientRectangle = base.ClientRectangle;
			}
			else
			{
				clientRectangle = this.button_area;
			}
			if (clientRectangle.Contains(e.X, e.Y))
			{
				if (this.Items.Count > 0)
				{
					this.DropDownListBox();
				}
				else
				{
					this.button_state = ButtonState.Pushed;
					this.OnDropDown(EventArgs.Empty);
				}
				base.Invalidate(this.button_area);
				base.Update();
			}
			base.Capture = true;
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0002002C File Offset: 0x0001E22C
		private void OnMouseEnter(object sender, EventArgs e)
		{
			if (ThemeEngine.Current.CombBoxBackgroundHasHotElementStyle(this))
			{
				base.Invalidate();
			}
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00020044 File Offset: 0x0001E244
		private void OnMouseLeave(object sender, EventArgs e)
		{
			if (ThemeEngine.Current.CombBoxBackgroundHasHotElementStyle(this))
			{
				this.drop_down_button_entered = false;
				base.Invalidate();
			}
			else if (this.show_dropdown_button)
			{
				this.DropDownButtonEntered = false;
			}
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00020088 File Offset: 0x0001E288
		private void OnMouseMoveCB(object sender, MouseEventArgs e)
		{
			if (this.show_dropdown_button && !this.dropped_down)
			{
				this.DropDownButtonEntered = this.button_area.Contains(e.Location);
			}
			if (this.DropDownStyle == ComboBoxStyle.Simple)
			{
				return;
			}
			if (this.listbox_ctrl != null && this.listbox_ctrl.Visible)
			{
				Point point = this.listbox_ctrl.PointToClient(Control.MousePosition);
				if (this.listbox_ctrl.ClientRectangle.Contains(point))
				{
					this.listbox_ctrl.Capture = true;
				}
			}
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00020120 File Offset: 0x0001E320
		private void OnMouseUpCB(object sender, MouseEventArgs e)
		{
			base.Capture = false;
			this.button_state = ButtonState.Normal;
			base.Invalidate(this.button_area);
			this.OnClick(EventArgs.Empty);
			if (this.dropped_down)
			{
				this.listbox_ctrl.Capture = true;
			}
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0002016C File Offset: 0x0001E36C
		private void OnMouseWheelCB(object sender, MouseEventArgs me)
		{
			if (this.Items.Count == 0)
			{
				return;
			}
			if (this.listbox_ctrl != null && this.listbox_ctrl.Visible)
			{
				int num = me.Delta / 120 * SystemInformation.MouseWheelScrollLines;
				this.listbox_ctrl.Scroll(-num);
			}
			else
			{
				int num2 = me.Delta / 120;
				int num3 = this.SelectedIndex - num2;
				if (num3 < 0)
				{
					num3 = 0;
				}
				else if (num3 >= this.Items.Count)
				{
					num3 = this.Items.Count - 1;
				}
				this.SelectedIndex = num3;
			}
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00020210 File Offset: 0x0001E410
		internal override void OnPaintInternal(PaintEventArgs pevent)
		{
			if (this.suspend_ctrlupdate)
			{
				return;
			}
			this.Draw(base.ClientRectangle, pevent.Graphics);
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00020230 File Offset: 0x0001E430
		private void OnTextBoxClick(object sender, EventArgs e)
		{
			this.OnClick(e);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0002023C File Offset: 0x0001E43C
		private void OnTextChangedEdit(object sender, EventArgs e)
		{
			if (!this.process_textchanged_event)
			{
				return;
			}
			int num = this.FindStringCaseInsensitive(this.textbox_ctrl.Text);
			if (num == -1)
			{
				this.OnTextChanged(EventArgs.Empty);
				return;
			}
			if (this.listbox_ctrl != null && this.process_texchanged_autoscroll)
			{
				this.listbox_ctrl.EnsureTop(num);
			}
			base.Text = this.textbox_ctrl.Text;
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x000202B0 File Offset: 0x0001E4B0
		private void OnTextKeyPress(object sender, KeyPressEventArgs e)
		{
			this.selected_index = -1;
			if (this.listbox_ctrl != null)
			{
				this.listbox_ctrl.HighlightedIndex = -1;
			}
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x000202D0 File Offset: 0x0001E4D0
		internal void SetControlText(string s, bool suppressTextChanged)
		{
			this.SetControlText(s, suppressTextChanged, false);
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x000202DC File Offset: 0x0001E4DC
		internal void SetControlText(string s, bool suppressTextChanged, bool supressAutoScroll)
		{
			if (suppressTextChanged)
			{
				this.process_textchanged_event = false;
			}
			if (supressAutoScroll)
			{
				this.process_texchanged_autoscroll = false;
			}
			this.textbox_ctrl.Text = s;
			this.textbox_ctrl.SelectAll();
			this.process_textchanged_event = true;
			this.process_texchanged_autoscroll = true;
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00020328 File Offset: 0x0001E528
		private void UpdateComboBoxBounds()
		{
			if (this.requested_height == -1)
			{
				return;
			}
			int num = this.requested_height;
			base.SetBounds(this.bounds.X, this.bounds.Y, this.bounds.Width, this.SnapHeight(this.requested_height), BoundsSpecified.Height);
			this.requested_height = num;
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00020384 File Offset: 0x0001E584
		private int SnapHeight(int height)
		{
			if (this.DropDownStyle == ComboBoxStyle.Simple && height > this.PreferredHeight)
			{
				if (this.IntegralHeight)
				{
					int height2 = ThemeEngine.Current.Border3DSize.Height;
					int num = height - this.PreferredHeight - 2 - height2 * 2;
					if (num > this.ItemHeight)
					{
						int num2 = num % this.ItemHeight;
						height -= num2;
					}
					else if (num < this.ItemHeight)
					{
						height = this.PreferredHeight;
					}
				}
			}
			else
			{
				height = this.PreferredHeight;
			}
			return height;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00020418 File Offset: 0x0001E618
		private void UpdatedItems()
		{
			if (this.listbox_ctrl != null)
			{
				this.listbox_ctrl.UpdateLastVisibleItem();
				this.listbox_ctrl.CalcListBoxArea();
				this.listbox_ctrl.Refresh();
			}
		}

		// Token: 0x04000741 RID: 1857
		private const int button_width = 16;

		// Token: 0x04000742 RID: 1858
		private const int default_drop_down_height = 106;

		// Token: 0x04000743 RID: 1859
		private DrawMode draw_mode;

		// Token: 0x04000744 RID: 1860
		private ComboBoxStyle dropdown_style;

		// Token: 0x04000745 RID: 1861
		private int dropdown_width = -1;

		// Token: 0x04000746 RID: 1862
		private int selected_index = -1;

		// Token: 0x04000747 RID: 1863
		private ComboBox.ObjectCollection items;

		// Token: 0x04000748 RID: 1864
		private bool suspend_ctrlupdate;

		// Token: 0x04000749 RID: 1865
		private int maxdrop_items = 8;

		// Token: 0x0400074A RID: 1866
		private bool integral_height = true;

		// Token: 0x0400074B RID: 1867
		private bool sorted;

		// Token: 0x0400074C RID: 1868
		private int max_length;

		// Token: 0x0400074D RID: 1869
		private ComboBox.ComboListBox listbox_ctrl;

		// Token: 0x0400074E RID: 1870
		private ComboBox.ComboTextBox textbox_ctrl;

		// Token: 0x0400074F RID: 1871
		private bool process_textchanged_event = true;

		// Token: 0x04000750 RID: 1872
		private bool process_texchanged_autoscroll = true;

		// Token: 0x04000751 RID: 1873
		private bool item_height_specified;

		// Token: 0x04000752 RID: 1874
		private int item_height;

		// Token: 0x04000753 RID: 1875
		private int requested_height = -1;

		// Token: 0x04000754 RID: 1876
		private Hashtable item_heights;

		// Token: 0x04000755 RID: 1877
		private bool show_dropdown_button;

		// Token: 0x04000756 RID: 1878
		private ButtonState button_state;

		// Token: 0x04000757 RID: 1879
		private bool dropped_down;

		// Token: 0x04000758 RID: 1880
		private Rectangle text_area;

		// Token: 0x04000759 RID: 1881
		private Rectangle button_area;

		// Token: 0x0400075A RID: 1882
		private Rectangle listbox_area;

		// Token: 0x0400075B RID: 1883
		private bool drop_down_button_entered;

		// Token: 0x0400075C RID: 1884
		private AutoCompleteStringCollection auto_complete_custom_source;

		// Token: 0x0400075D RID: 1885
		private AutoCompleteMode auto_complete_mode;

		// Token: 0x0400075E RID: 1886
		private AutoCompleteSource auto_complete_source = AutoCompleteSource.None;

		// Token: 0x0400075F RID: 1887
		private FlatStyle flat_style;

		// Token: 0x04000760 RID: 1888
		private int drop_down_height;

		/// <summary>Provides information about the <see cref="T:System.Windows.Forms.ComboBox" /> control to accessibility client applications.</summary>
		// Token: 0x02000092 RID: 146
		[ComVisible(true)]
		public class ChildAccessibleObject : AccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ComboBox.ChildAccessibleObject" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ComboBox" /> control that owns the <see cref="T:System.Windows.Forms.ComboBox.ChildAccessibleObject" />.</param>
			/// <param name="handle">A handle to part of the <see cref="T:System.Windows.Forms.ComboBox" />.</param>
			// Token: 0x06000723 RID: 1827 RVA: 0x00020454 File Offset: 0x0001E654
			public ChildAccessibleObject(ComboBox owner, IntPtr handle)
				: base(owner)
			{
			}

			/// <summary>Gets the name of the object.</summary>
			/// <returns>The value of the <see cref="P:System.Windows.Forms.ComboBox.ChildAccessibleObject.Name" /> property is the same as the <see cref="P:System.Windows.Forms.AccessibleObject.Name" /> property for the <see cref="T:System.Windows.Forms.AccessibleObject" /> of the <see cref="T:System.Windows.Forms.ComboBox" />.</returns>
			// Token: 0x170001AA RID: 426
			// (get) Token: 0x06000724 RID: 1828 RVA: 0x00020460 File Offset: 0x0001E660
			public override string Name
			{
				get
				{
					return base.Name;
				}
			}
		}

		/// <summary>Represents the collection of items in a <see cref="T:System.Windows.Forms.ComboBox" />. </summary>
		// Token: 0x02000093 RID: 147
		[ListBindable(false)]
		public class ObjectCollection : ICollection, IEnumerable, IList
		{
			/// <summary>Initializes a new instance of <see cref="T:System.Windows.Forms.ComboBox.ObjectCollection" />.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ComboBox" /> that owns this object collection. </param>
			// Token: 0x06000725 RID: 1829 RVA: 0x00020468 File Offset: 0x0001E668
			public ObjectCollection(ComboBox owner)
			{
				this.owner = owner;
			}

			// Token: 0x06000726 RID: 1830 RVA: 0x00020484 File Offset: 0x0001E684
			// Note: this type is marked as 'beforefieldinit'.
			static ObjectCollection()
			{
				ComboBox.ObjectCollection.UIACollectionChangedEvent = new object();
			}

			// Token: 0x14000068 RID: 104
			// (add) Token: 0x06000727 RID: 1831 RVA: 0x00020490 File Offset: 0x0001E690
			// (remove) Token: 0x06000728 RID: 1832 RVA: 0x000204A8 File Offset: 0x0001E6A8
			internal event CollectionChangeEventHandler UIACollectionChanged
			{
				add
				{
					this.owner.Events.AddHandler(ComboBox.ObjectCollection.UIACollectionChangedEvent, value);
				}
				remove
				{
					this.owner.Events.RemoveHandler(ComboBox.ObjectCollection.UIACollectionChangedEvent, value);
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x170001AB RID: 427
			// (get) Token: 0x06000729 RID: 1833 RVA: 0x000204C0 File Offset: 0x0001E6C0
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.ComboBox.ObjectCollection" />.</returns>
			// Token: 0x170001AC RID: 428
			// (get) Token: 0x0600072A RID: 1834 RVA: 0x000204C4 File Offset: 0x0001E6C4
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x170001AD RID: 429
			// (get) Token: 0x0600072B RID: 1835 RVA: 0x000204C8 File Offset: 0x0001E6C8
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
			/// <param name="destination">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in the array at which copying begins.</param>
			// Token: 0x0600072C RID: 1836 RVA: 0x000204CC File Offset: 0x0001E6CC
			void ICollection.CopyTo(Array destination, int index)
			{
				this.object_items.CopyTo(destination, index);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
			/// <returns>The zero-based index of the item in the collection.</returns>
			/// <param name="item">An object that represents the item to add to the collection.</param>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="item" /> parameter is null.</exception>
			/// <exception cref="T:System.SystemException">There is insufficient space available to store the new item.</exception>
			// Token: 0x0600072D RID: 1837 RVA: 0x000204DC File Offset: 0x0001E6DC
			int IList.Add(object item)
			{
				return this.Add(item);
			}

			// Token: 0x0600072E RID: 1838 RVA: 0x000204E8 File Offset: 0x0001E6E8
			internal void OnUIACollectionChangedEvent(CollectionChangeEventArgs args)
			{
				CollectionChangeEventHandler collectionChangeEventHandler = (CollectionChangeEventHandler)this.owner.Events[ComboBox.ObjectCollection.UIACollectionChangedEvent];
				if (collectionChangeEventHandler != null)
				{
					collectionChangeEventHandler.Invoke(this.owner, args);
				}
			}

			/// <summary>Gets the number of items in the collection.</summary>
			/// <returns>The number of items in the collection.</returns>
			// Token: 0x170001AE RID: 430
			// (get) Token: 0x0600072F RID: 1839 RVA: 0x00020524 File Offset: 0x0001E724
			public int Count
			{
				get
				{
					return this.object_items.Count;
				}
			}

			/// <summary>Gets a value indicating whether this collection can be modified.</summary>
			/// <returns>Always false.</returns>
			// Token: 0x170001AF RID: 431
			// (get) Token: 0x06000730 RID: 1840 RVA: 0x00020534 File Offset: 0x0001E734
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Retrieves the item at the specified index within the collection.</summary>
			/// <returns>An object representing the item located at the specified index within the collection.</returns>
			/// <param name="index">The index of the item in the collection to retrieve. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The index was less than zero.-or- The <paramref name="index" /> was greater than or equal to the count of items in the collection. </exception>
			// Token: 0x170001B0 RID: 432
			[Browsable(false)]
			[DesignerSerializationVisibility(0)]
			public virtual object this[int index]
			{
				get
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					return this.object_items[index];
				}
				set
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					if (value == null)
					{
						throw new ArgumentNullException("value");
					}
					this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(2, this.object_items[index]));
					this.object_items[index] = value;
					this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(1, value));
					if (this.owner.listbox_ctrl != null)
					{
						this.owner.listbox_ctrl.InvalidateItem(index);
					}
					if (index == this.owner.SelectedIndex)
					{
						if (this.owner.textbox_ctrl == null)
						{
							this.owner.Refresh();
						}
						else
						{
							this.owner.textbox_ctrl.SelectedText = value.ToString();
						}
					}
				}
			}

			/// <summary>Adds an item to the list of items for a <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
			/// <returns>The zero-based index of the item in the collection.</returns>
			/// <param name="item">An object representing the item to add to the collection. </param>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="item" /> parameter was null. </exception>
			// Token: 0x06000733 RID: 1843 RVA: 0x00020648 File Offset: 0x0001E848
			public int Add(object item)
			{
				int num = this.AddItem(item, false);
				this.owner.UpdatedItems();
				return num;
			}

			/// <summary>Adds an array of items to the list of items for a <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
			/// <param name="items">An array of objects to add to the list. </param>
			/// <exception cref="T:System.ArgumentNullException">An item in the <paramref name="items" /> parameter was null. </exception>
			// Token: 0x06000734 RID: 1844 RVA: 0x0002066C File Offset: 0x0001E86C
			public void AddRange(object[] items)
			{
				if (items == null)
				{
					throw new ArgumentNullException("items");
				}
				foreach (object obj in items)
				{
					this.AddItem(obj, true);
				}
				if (this.owner.sorted)
				{
					this.Sort();
				}
				this.owner.UpdatedItems();
			}

			/// <summary>Removes all items from the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
			// Token: 0x06000735 RID: 1845 RVA: 0x000206D0 File Offset: 0x0001E8D0
			public void Clear()
			{
				this.owner.selected_index = -1;
				this.object_items.Clear();
				this.owner.UpdatedItems();
				this.owner.Refresh();
				this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(3, null));
			}

			/// <summary>Determines if the specified item is located within the collection.</summary>
			/// <returns>true if the item is located within the collection; otherwise, false.</returns>
			/// <param name="value">An object representing the item to locate in the collection. </param>
			// Token: 0x06000736 RID: 1846 RVA: 0x00020718 File Offset: 0x0001E918
			public bool Contains(object value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				return this.object_items.Contains(value);
			}

			/// <summary>Copies the entire collection into an existing array of objects at a specified location within the array.</summary>
			/// <param name="destination">The object array to copy the collection to. </param>
			/// <param name="arrayIndex">The location in the destination array to copy the collection to. </param>
			// Token: 0x06000737 RID: 1847 RVA: 0x00020738 File Offset: 0x0001E938
			public void CopyTo(object[] destination, int arrayIndex)
			{
				this.object_items.CopyTo(destination, arrayIndex);
			}

			/// <summary>Returns an enumerator that can be used to iterate through the item collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the item collection.</returns>
			// Token: 0x06000738 RID: 1848 RVA: 0x00020748 File Offset: 0x0001E948
			public IEnumerator GetEnumerator()
			{
				return this.object_items.GetEnumerator();
			}

			/// <summary>Retrieves the index within the collection of the specified item.</summary>
			/// <returns>The zero-based index where the item is located within the collection; otherwise, -1.</returns>
			/// <param name="value">An object representing the item to locate in the collection. </param>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter was null. </exception>
			// Token: 0x06000739 RID: 1849 RVA: 0x00020758 File Offset: 0x0001E958
			public int IndexOf(object value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				return this.object_items.IndexOf(value);
			}

			/// <summary>Inserts an item into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the item is inserted. </param>
			/// <param name="item">An object representing the item to insert. </param>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="item" /> was null. </exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> was less than zero.-or- The <paramref name="index" /> was greater than the count of items in the collection. </exception>
			// Token: 0x0600073A RID: 1850 RVA: 0x00020778 File Offset: 0x0001E978
			public void Insert(int index, object item)
			{
				if (index < 0 || index > this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (item == null)
				{
					throw new ArgumentNullException("item");
				}
				this.owner.BeginUpdate();
				if (this.owner.Sorted)
				{
					this.AddItem(item, false);
				}
				else
				{
					this.object_items.Insert(index, item);
					this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(1, item));
				}
				this.owner.EndUpdate();
			}

			/// <summary>Removes the specified item from the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
			/// <param name="value">The <see cref="T:System.Object" /> to remove from the list. </param>
			// Token: 0x0600073B RID: 1851 RVA: 0x00020804 File Offset: 0x0001EA04
			public void Remove(object value)
			{
				if (value == null)
				{
					return;
				}
				if (this.IndexOf(value) == this.owner.SelectedIndex)
				{
					this.owner.SelectedIndex = -1;
				}
				this.RemoveAt(this.IndexOf(value));
			}

			/// <summary>Removes an item from the <see cref="T:System.Windows.Forms.ComboBox" /> at the specified index.</summary>
			/// <param name="index">The index of the item to remove. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="value" /> parameter was less than zero.-or- The <paramref name="value" /> parameter was greater than or equal to the count of items in the collection. </exception>
			// Token: 0x0600073C RID: 1852 RVA: 0x00020848 File Offset: 0x0001EA48
			public void RemoveAt(int index)
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (index == this.owner.SelectedIndex)
				{
					this.owner.SelectedIndex = -1;
				}
				object obj = this.object_items[index];
				this.object_items.RemoveAt(index);
				this.owner.UpdatedItems();
				this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(2, obj));
			}

			// Token: 0x0600073D RID: 1853 RVA: 0x000208C4 File Offset: 0x0001EAC4
			private int AddItem(object item, bool suspend)
			{
				if (item == null)
				{
					throw new ArgumentNullException("item");
				}
				if (this.owner.Sorted && !suspend)
				{
					int num = 0;
					foreach (object obj in this.object_items)
					{
						if (string.Compare(item.ToString(), obj.ToString()) < 0)
						{
							this.object_items.Insert(num, item);
							if (num <= this.owner.selected_index && this.owner.IsHandleCreated)
							{
								this.owner.selected_index++;
							}
							this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(1, item));
							return num;
						}
						num++;
					}
				}
				this.object_items.Add(item);
				this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(1, item));
				return this.object_items.Count - 1;
			}

			// Token: 0x0600073E RID: 1854 RVA: 0x000209EC File Offset: 0x0001EBEC
			internal void AddRange(IList items)
			{
				foreach (object obj in items)
				{
					this.AddItem(obj, false);
				}
				if (this.owner.sorted)
				{
					this.Sort();
				}
				this.owner.UpdatedItems();
			}

			// Token: 0x0600073F RID: 1855 RVA: 0x00020A74 File Offset: 0x0001EC74
			internal void Sort()
			{
				if (this.object_items.Count > 0 && this.object_items[0] is IComparer)
				{
					this.object_items.Sort();
				}
				else
				{
					this.object_items.Sort(new ComboBox.ObjectCollection.ObjectComparer(this.owner));
				}
			}

			// Token: 0x04000769 RID: 1897
			private ComboBox owner;

			// Token: 0x0400076A RID: 1898
			internal ArrayList object_items = new ArrayList();

			// Token: 0x02000094 RID: 148
			private class ObjectComparer : IComparer
			{
				// Token: 0x06000740 RID: 1856 RVA: 0x00020AD0 File Offset: 0x0001ECD0
				public ObjectComparer(ListControl owner)
				{
					this.owner = owner;
				}

				// Token: 0x06000741 RID: 1857 RVA: 0x00020AE0 File Offset: 0x0001ECE0
				public int Compare(object x, object y)
				{
					return string.Compare(this.owner.GetItemText(x), this.owner.GetItemText(y));
				}

				// Token: 0x0400076C RID: 1900
				private ListControl owner;
			}
		}

		// Token: 0x02000095 RID: 149
		internal class ComboTextBox : TextBox
		{
			// Token: 0x06000742 RID: 1858 RVA: 0x00020B00 File Offset: 0x0001ED00
			public ComboTextBox(ComboBox owner)
			{
				this.owner = owner;
				base.ShowSelection = false;
				base.HideSelection = false;
				owner.LostFocus += new EventHandler(this.OwnerLostFocusHandler);
			}

			// Token: 0x06000743 RID: 1859 RVA: 0x00020B3C File Offset: 0x0001ED3C
			private void OwnerLostFocusHandler(object o, EventArgs args)
			{
				if (base.IsAutoCompleteAvailable)
				{
					this.owner.Text = this.Text;
				}
			}

			// Token: 0x06000744 RID: 1860 RVA: 0x00020B5C File Offset: 0x0001ED5C
			protected override void OnKeyDown(KeyEventArgs args)
			{
				if (args.KeyCode == Keys.Return && base.IsAutoCompleteAvailable)
				{
					this.owner.Text = this.Text;
				}
				base.OnKeyDown(args);
			}

			// Token: 0x06000745 RID: 1861 RVA: 0x00020B9C File Offset: 0x0001ED9C
			internal override void OnAutoCompleteValueSelected(EventArgs args)
			{
				base.OnAutoCompleteValueSelected(args);
				this.owner.Text = this.Text;
			}

			// Token: 0x06000746 RID: 1862 RVA: 0x00020BB8 File Offset: 0x0001EDB8
			internal void SetSelectable(bool selectable)
			{
				base.SetStyle(ControlStyles.Selectable, selectable);
			}

			// Token: 0x06000747 RID: 1863 RVA: 0x00020BC8 File Offset: 0x0001EDC8
			internal void ActivateCaret(bool active)
			{
				if (active)
				{
					this.document.CaretHasFocus();
				}
				else
				{
					this.document.CaretLostFocus();
				}
			}

			// Token: 0x06000748 RID: 1864 RVA: 0x00020BEC File Offset: 0x0001EDEC
			internal override void OnTextUpdate()
			{
				base.OnTextUpdate();
				this.owner.OnTextUpdate(EventArgs.Empty);
			}

			// Token: 0x06000749 RID: 1865 RVA: 0x00020C04 File Offset: 0x0001EE04
			protected override void OnGotFocus(EventArgs e)
			{
				this.owner.Select(false, true);
			}

			// Token: 0x0600074A RID: 1866 RVA: 0x00020C14 File Offset: 0x0001EE14
			protected override void OnLostFocus(EventArgs e)
			{
				this.owner.Select(false, true);
			}

			// Token: 0x170001B1 RID: 433
			// (get) Token: 0x0600074B RID: 1867 RVA: 0x00020C24 File Offset: 0x0001EE24
			public override bool Focused
			{
				get
				{
					return this.owner.Focused;
				}
			}

			// Token: 0x170001B2 RID: 434
			// (get) Token: 0x0600074C RID: 1868 RVA: 0x00020C34 File Offset: 0x0001EE34
			internal override bool ActivateOnShow
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0400076D RID: 1901
			private ComboBox owner;
		}

		// Token: 0x02000096 RID: 150
		internal class ComboListBox : Control
		{
			// Token: 0x0600074D RID: 1869 RVA: 0x00020C38 File Offset: 0x0001EE38
			public ComboListBox(ComboBox owner)
			{
				this.owner = owner;
				this.top_item = 0;
				this.last_item = 0;
				this.page_size = 0;
				base.MouseWheel += this.OnMouseWheelCLB;
				base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
				base.SetStyle(ControlStyles.Opaque | ControlStyles.ResizeRedraw, true);
				this.is_visible = false;
				if (owner.DropDownStyle == ComboBoxStyle.Simple)
				{
					base.InternalBorderStyle = BorderStyle.Fixed3D;
				}
				else
				{
					base.InternalBorderStyle = BorderStyle.FixedSingle;
				}
			}

			// Token: 0x170001B3 RID: 435
			// (get) Token: 0x0600074E RID: 1870 RVA: 0x00020CBC File Offset: 0x0001EEBC
			internal int UIATopItem
			{
				get
				{
					return this.top_item;
				}
			}

			// Token: 0x170001B4 RID: 436
			// (get) Token: 0x0600074F RID: 1871 RVA: 0x00020CC4 File Offset: 0x0001EEC4
			internal int UIALastItem
			{
				get
				{
					return this.last_item;
				}
			}

			// Token: 0x170001B5 RID: 437
			// (get) Token: 0x06000750 RID: 1872 RVA: 0x00020CCC File Offset: 0x0001EECC
			internal ScrollBar UIAVScrollBar
			{
				get
				{
					return this.vscrollbar_ctrl;
				}
			}

			// Token: 0x170001B6 RID: 438
			// (get) Token: 0x06000751 RID: 1873 RVA: 0x00020CD4 File Offset: 0x0001EED4
			protected override CreateParams CreateParams
			{
				get
				{
					CreateParams createParams = base.CreateParams;
					if (this.owner == null || this.owner.DropDownStyle == ComboBoxStyle.Simple)
					{
						return createParams;
					}
					createParams.Style ^= 1073741824;
					createParams.Style ^= 268435456;
					createParams.Style |= int.MinValue;
					createParams.ExStyle |= 136;
					return createParams;
				}
			}

			// Token: 0x170001B7 RID: 439
			// (get) Token: 0x06000752 RID: 1874 RVA: 0x00020D50 File Offset: 0x0001EF50
			// (set) Token: 0x06000753 RID: 1875 RVA: 0x00020D58 File Offset: 0x0001EF58
			internal override bool InternalCapture
			{
				get
				{
					return base.Capture;
				}
				set
				{
				}
			}

			// Token: 0x170001B8 RID: 440
			// (get) Token: 0x06000754 RID: 1876 RVA: 0x00020D5C File Offset: 0x0001EF5C
			internal override bool ActivateOnShow
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000755 RID: 1877 RVA: 0x00020D60 File Offset: 0x0001EF60
			internal void CalcListBoxArea()
			{
				int num;
				int num2;
				bool flag;
				if (this.owner.DropDownStyle == ComboBoxStyle.Simple)
				{
					Rectangle listbox_area = this.owner.listbox_area;
					num = listbox_area.Width;
					num2 = listbox_area.Height;
					flag = this.owner.Items.Count * this.owner.ItemHeight > num2;
					if (num2 <= 0 || num <= 0)
					{
						return;
					}
				}
				else
				{
					num = this.owner.DropDownWidth;
					int num3 = ((this.owner.Items.Count > this.owner.MaxDropDownItems) ? this.owner.MaxDropDownItems : this.owner.Items.Count);
					if (this.owner.DrawMode == DrawMode.OwnerDrawVariable)
					{
						num2 = 0;
						for (int i = 0; i < num3; i++)
						{
							num2 += this.owner.GetItemHeight(i);
						}
						flag = this.owner.Items.Count > this.owner.MaxDropDownItems;
					}
					else if (this.owner.DropDownHeight == 106)
					{
						num2 = this.owner.ItemHeight * num3;
						flag = this.owner.Items.Count > this.owner.MaxDropDownItems;
					}
					else
					{
						num2 = this.owner.DropDownHeight;
						flag = this.owner.Items.Count * this.owner.ItemHeight > num2;
					}
				}
				this.page_size = Math.Max(num2 / this.owner.ItemHeight, 1);
				ComboBoxStyle dropDownStyle = this.owner.DropDownStyle;
				if (!flag)
				{
					if (this.vscrollbar_ctrl != null)
					{
						this.vscrollbar_ctrl.Visible = false;
					}
					if (dropDownStyle != ComboBoxStyle.Simple)
					{
						num2 = this.owner.ItemHeight * this.owner.items.Count;
					}
				}
				else
				{
					if (this.vscrollbar_ctrl == null)
					{
						this.vscrollbar_ctrl = new ComboBox.ComboListBox.VScrollBarLB();
						this.vscrollbar_ctrl.Minimum = 0;
						this.vscrollbar_ctrl.SmallChange = 1;
						this.vscrollbar_ctrl.LargeChange = 1;
						this.vscrollbar_ctrl.Maximum = 0;
						this.vscrollbar_ctrl.ValueChanged += new EventHandler(this.VerticalScrollEvent);
						base.Controls.AddImplicit(this.vscrollbar_ctrl);
					}
					this.vscrollbar_ctrl.Dock = DockStyle.Right;
					this.vscrollbar_ctrl.Maximum = this.owner.Items.Count - 1;
					int num4 = this.page_size;
					if (num4 < 1)
					{
						num4 = 1;
					}
					this.vscrollbar_ctrl.LargeChange = num4;
					this.vscrollbar_ctrl.Visible = true;
					int num5 = this.HighlightedIndex;
					if (num5 > 0)
					{
						num5 = Math.Min(num5, this.vscrollbar_ctrl.Maximum);
						this.vscrollbar_ctrl.Value = num5;
					}
				}
				base.Size = new Size(num, num2);
				this.textarea_drawable = base.ClientRectangle;
				this.textarea_drawable.Width = num;
				this.textarea_drawable.Height = num2;
				if (this.vscrollbar_ctrl != null && flag)
				{
					this.textarea_drawable.Width = this.textarea_drawable.Width - this.vscrollbar_ctrl.Width;
				}
				this.last_item = this.LastVisibleItem();
			}

			// Token: 0x06000756 RID: 1878 RVA: 0x000210B4 File Offset: 0x0001F2B4
			private void Draw(Rectangle clip, Graphics dc)
			{
				dc.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.owner.BackColor), clip);
				if (this.owner.Items.Count > 0)
				{
					for (int i = this.top_item; i <= this.last_item; i++)
					{
						Rectangle itemDisplayRectangle = this.GetItemDisplayRectangle(i, this.top_item);
						if (clip.IntersectsWith(itemDisplayRectangle))
						{
							DrawItemState drawItemState = DrawItemState.None;
							Color color = this.owner.BackColor;
							Color color2 = this.owner.ForeColor;
							if (i == this.HighlightedIndex)
							{
								drawItemState |= DrawItemState.Selected;
								color = SystemColors.Highlight;
								color2 = SystemColors.HighlightText;
								if (this.owner.DropDownStyle == ComboBoxStyle.DropDownList)
								{
									drawItemState |= DrawItemState.Focus;
								}
							}
							this.owner.HandleDrawItem(new DrawItemEventArgs(dc, this.owner.Font, itemDisplayRectangle, i, drawItemState, color2, color));
						}
					}
				}
			}

			// Token: 0x170001B9 RID: 441
			// (get) Token: 0x06000757 RID: 1879 RVA: 0x000211A4 File Offset: 0x0001F3A4
			// (set) Token: 0x06000758 RID: 1880 RVA: 0x000211AC File Offset: 0x0001F3AC
			public int HighlightedIndex
			{
				get
				{
					return this.highlighted_index;
				}
				set
				{
					if (this.highlighted_index == value)
					{
						return;
					}
					if (this.highlighted_index != -1 && this.highlighted_index < this.owner.Items.Count)
					{
						base.Invalidate(this.GetItemDisplayRectangle(this.highlighted_index, this.top_item));
					}
					this.highlighted_index = value;
					if (this.highlighted_index != -1)
					{
						base.Invalidate(this.GetItemDisplayRectangle(this.highlighted_index, this.top_item));
					}
				}
			}

			// Token: 0x06000759 RID: 1881 RVA: 0x00021230 File Offset: 0x0001F430
			private Rectangle GetItemDisplayRectangle(int index, int top_index)
			{
				if (index < 0 || index >= this.owner.Items.Count)
				{
					throw new ArgumentOutOfRangeException("GetItemRectangle index out of range.");
				}
				Rectangle rectangle = default(Rectangle);
				int itemHeight = this.owner.GetItemHeight(index);
				rectangle.X = 0;
				rectangle.Width = this.textarea_drawable.Width;
				if (this.owner.DrawMode == DrawMode.OwnerDrawVariable)
				{
					rectangle.Y = 0;
					for (int i = top_index; i < index; i++)
					{
						rectangle.Y += this.owner.GetItemHeight(i);
					}
				}
				else
				{
					rectangle.Y = itemHeight * (index - top_index);
				}
				rectangle.Height = itemHeight;
				return rectangle;
			}

			// Token: 0x0600075A RID: 1882 RVA: 0x000212F4 File Offset: 0x0001F4F4
			public void HideWindow()
			{
				if (this.owner.DropDownStyle == ComboBoxStyle.Simple)
				{
					return;
				}
				base.Capture = false;
				base.Hide();
				this.owner.DropDownListBoxFinished();
			}

			// Token: 0x0600075B RID: 1883 RVA: 0x00021320 File Offset: 0x0001F520
			private int IndexFromPointDisplayRectangle(int x, int y)
			{
				for (int i = this.top_item; i <= this.last_item; i++)
				{
					if (this.GetItemDisplayRectangle(i, this.top_item).Contains(x, y))
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x0600075C RID: 1884 RVA: 0x00021368 File Offset: 0x0001F568
			public void InvalidateItem(int index)
			{
				if (base.Visible)
				{
					base.Invalidate(this.GetItemDisplayRectangle(index, this.top_item));
				}
			}

			// Token: 0x0600075D RID: 1885 RVA: 0x00021388 File Offset: 0x0001F588
			public int LastVisibleItem()
			{
				int num = this.textarea_drawable.Y + this.textarea_drawable.Height;
				int i;
				for (i = this.top_item; i < this.owner.Items.Count; i++)
				{
					Rectangle itemDisplayRectangle = this.GetItemDisplayRectangle(i, this.top_item);
					if (itemDisplayRectangle.Y + itemDisplayRectangle.Height > num)
					{
						return i;
					}
				}
				return i - 1;
			}

			// Token: 0x0600075E RID: 1886 RVA: 0x00021400 File Offset: 0x0001F600
			public void SetTopItem(int item)
			{
				if (this.top_item == item)
				{
					return;
				}
				this.top_item = item;
				this.UpdateLastVisibleItem();
				base.Invalidate();
			}

			// Token: 0x0600075F RID: 1887 RVA: 0x00021430 File Offset: 0x0001F630
			public int FirstVisibleItem()
			{
				return this.top_item;
			}

			// Token: 0x06000760 RID: 1888 RVA: 0x00021438 File Offset: 0x0001F638
			public void EnsureTop(int item)
			{
				if (this.owner.Items.Count == 0)
				{
					return;
				}
				if (this.vscrollbar_ctrl == null || !this.vscrollbar_ctrl.Visible)
				{
					return;
				}
				int num = this.vscrollbar_ctrl.Maximum - this.page_size + 1;
				if (item > num)
				{
					item = num;
				}
				else if (item < this.vscrollbar_ctrl.Minimum)
				{
					item = this.vscrollbar_ctrl.Minimum;
				}
				this.vscrollbar_ctrl.Value = item;
			}

			// Token: 0x170001BA RID: 442
			// (get) Token: 0x06000761 RID: 1889 RVA: 0x000214C8 File Offset: 0x0001F6C8
			private bool InScrollBar
			{
				get
				{
					return this.vscrollbar_ctrl != null && this.vscrollbar_ctrl.is_visible && this.vscrollbar_ctrl.Bounds.Contains(base.PointToClient(Control.MousePosition));
				}
			}

			// Token: 0x06000762 RID: 1890 RVA: 0x00021510 File Offset: 0x0001F710
			protected override void OnMouseDown(MouseEventArgs e)
			{
				if (this.InScrollBar)
				{
					this.vscrollbar_ctrl.FireMouseDown(e);
					this.scrollbar_grabbed = true;
				}
			}

			// Token: 0x06000763 RID: 1891 RVA: 0x00021530 File Offset: 0x0001F730
			protected override void OnMouseMove(MouseEventArgs e)
			{
				if (this.owner.DropDownStyle == ComboBoxStyle.Simple)
				{
					return;
				}
				if (this.scrollbar_grabbed || (!base.Capture && this.InScrollBar))
				{
					this.vscrollbar_ctrl.FireMouseMove(e);
					return;
				}
				Point point = base.PointToClient(Control.MousePosition);
				int num = this.IndexFromPointDisplayRectangle(point.X, point.Y);
				if (num != -1)
				{
					this.HighlightedIndex = num;
				}
			}

			// Token: 0x06000764 RID: 1892 RVA: 0x000215AC File Offset: 0x0001F7AC
			protected override void OnMouseUp(MouseEventArgs e)
			{
				int num = this.IndexFromPointDisplayRectangle(e.X, e.Y);
				if (this.scrollbar_grabbed)
				{
					this.vscrollbar_ctrl.FireMouseUp(e);
					this.scrollbar_grabbed = false;
					if (num != -1)
					{
						this.HighlightedIndex = num;
					}
					return;
				}
				if (num == -1)
				{
					this.HideWindow();
					return;
				}
				bool flag = this.owner.SelectedIndex != num;
				this.owner.SetSelectedIndex(num, true);
				this.owner.OnSelectionChangeCommitted(new EventArgs());
				if (!flag)
				{
					this.owner.OnSelectedValueChanged(EventArgs.Empty);
					this.owner.OnSelectedIndexChanged(EventArgs.Empty);
				}
				this.HideWindow();
			}

			// Token: 0x06000765 RID: 1893 RVA: 0x00021664 File Offset: 0x0001F864
			internal override void OnPaintInternal(PaintEventArgs pevent)
			{
				this.Draw(pevent.ClipRectangle, pevent.Graphics);
			}

			// Token: 0x06000766 RID: 1894 RVA: 0x00021678 File Offset: 0x0001F878
			public bool ShowWindow()
			{
				if (this.owner.DropDownStyle == ComboBoxStyle.Simple && this.owner.Items.Count == 0)
				{
					return false;
				}
				this.HighlightedIndex = this.owner.SelectedIndex;
				this.CalcListBoxArea();
				base.Show();
				this.Refresh();
				this.owner.OnDropDown(EventArgs.Empty);
				return true;
			}

			// Token: 0x06000767 RID: 1895 RVA: 0x000216E0 File Offset: 0x0001F8E0
			public void UpdateLastVisibleItem()
			{
				this.last_item = this.LastVisibleItem();
			}

			// Token: 0x06000768 RID: 1896 RVA: 0x000216F0 File Offset: 0x0001F8F0
			public void Scroll(int delta)
			{
				if (delta == 0 || this.vscrollbar_ctrl == null || !this.vscrollbar_ctrl.Visible)
				{
					return;
				}
				int num = this.vscrollbar_ctrl.Maximum - this.page_size + 1;
				int num2 = this.vscrollbar_ctrl.Value + delta;
				if (num2 > num)
				{
					num2 = num;
				}
				else if (num2 < this.vscrollbar_ctrl.Minimum)
				{
					num2 = this.vscrollbar_ctrl.Minimum;
				}
				this.vscrollbar_ctrl.Value = num2;
			}

			// Token: 0x06000769 RID: 1897 RVA: 0x0002177C File Offset: 0x0001F97C
			private void OnMouseWheelCLB(object sender, MouseEventArgs me)
			{
				if (this.owner.Items.Count == 0)
				{
					return;
				}
				int num = me.Delta / 120 * SystemInformation.MouseWheelScrollLines;
				this.Scroll(-num);
			}

			// Token: 0x0600076A RID: 1898 RVA: 0x000217B8 File Offset: 0x0001F9B8
			private void VerticalScrollEvent(object sender, EventArgs e)
			{
				if (this.top_item == this.vscrollbar_ctrl.Value)
				{
					return;
				}
				this.top_item = this.vscrollbar_ctrl.Value;
				this.UpdateLastVisibleItem();
				base.Invalidate();
			}

			// Token: 0x0600076B RID: 1899 RVA: 0x000217FC File Offset: 0x0001F9FC
			protected override void WndProc(ref Message m)
			{
				if (m.Msg == 7)
				{
					this.owner.Select(false, true);
				}
				base.WndProc(ref m);
			}

			// Token: 0x0400076E RID: 1902
			private ComboBox owner;

			// Token: 0x0400076F RID: 1903
			private ComboBox.ComboListBox.VScrollBarLB vscrollbar_ctrl;

			// Token: 0x04000770 RID: 1904
			private int top_item;

			// Token: 0x04000771 RID: 1905
			private int last_item;

			// Token: 0x04000772 RID: 1906
			internal int page_size;

			// Token: 0x04000773 RID: 1907
			private Rectangle textarea_drawable;

			// Token: 0x04000774 RID: 1908
			private int highlighted_index = -1;

			// Token: 0x04000775 RID: 1909
			private bool scrollbar_grabbed;

			// Token: 0x02000097 RID: 151
			internal enum ItemNavigation
			{
				// Token: 0x04000777 RID: 1911
				First,
				// Token: 0x04000778 RID: 1912
				Last,
				// Token: 0x04000779 RID: 1913
				Next,
				// Token: 0x0400077A RID: 1914
				Previous,
				// Token: 0x0400077B RID: 1915
				NextPage,
				// Token: 0x0400077C RID: 1916
				PreviousPage
			}

			// Token: 0x02000098 RID: 152
			private class VScrollBarLB : VScrollBar
			{
				// Token: 0x170001BB RID: 443
				// (get) Token: 0x0600076D RID: 1901 RVA: 0x00021834 File Offset: 0x0001FA34
				// (set) Token: 0x0600076E RID: 1902 RVA: 0x0002183C File Offset: 0x0001FA3C
				internal override bool InternalCapture
				{
					get
					{
						return base.Capture;
					}
					set
					{
					}
				}

				// Token: 0x0600076F RID: 1903 RVA: 0x00021840 File Offset: 0x0001FA40
				public void FireMouseDown(MouseEventArgs e)
				{
					if (!base.Visible)
					{
						return;
					}
					e = this.TranslateEvent(e);
					this.OnMouseDown(e);
				}

				// Token: 0x06000770 RID: 1904 RVA: 0x00021860 File Offset: 0x0001FA60
				public void FireMouseUp(MouseEventArgs e)
				{
					if (!base.Visible)
					{
						return;
					}
					e = this.TranslateEvent(e);
					this.OnMouseUp(e);
				}

				// Token: 0x06000771 RID: 1905 RVA: 0x00021880 File Offset: 0x0001FA80
				public void FireMouseMove(MouseEventArgs e)
				{
					if (!base.Visible)
					{
						return;
					}
					e = this.TranslateEvent(e);
					this.OnMouseMove(e);
				}

				// Token: 0x06000772 RID: 1906 RVA: 0x000218A0 File Offset: 0x0001FAA0
				private MouseEventArgs TranslateEvent(MouseEventArgs e)
				{
					Point point = base.PointToClient(Control.MousePosition);
					return new MouseEventArgs(e.Button, e.Clicks, point.X, point.Y, e.Delta);
				}
			}
		}
	}
}
