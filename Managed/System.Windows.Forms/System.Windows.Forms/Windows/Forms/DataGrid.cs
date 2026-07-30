using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Displays ADO.NET data in a scrollable grid. The <see cref="T:System.Windows.Forms.DataGridView" /> control replaces and adds functionality to the <see cref="T:System.Windows.Forms.DataGrid" /> control; however, the <see cref="T:System.Windows.Forms.DataGrid" /> control is retained for both backward compatibility and future use, if you choose. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000BF RID: 191
	[Designer("System.Windows.Forms.Design.DataGridDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultEvent("Navigate")]
	[ComplexBindingProperties("DataSource", "DataMember")]
	[ClassInterface(1)]
	[ComVisible(true)]
	[DefaultProperty("DataSource")]
	public class DataGrid : Control, ISupportInitialize, IDataGridEditingService
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGrid" /> class.</summary>
		// Token: 0x06000BA4 RID: 2980 RVA: 0x0002FC64 File Offset: 0x0002DE64
		public DataGrid()
		{
			this.allow_navigation = true;
			this.background_color = DataGrid.def_background_color;
			this.border_style = BorderStyle.Fixed3D;
			this.caption_backcolor = DataGrid.def_caption_backcolor;
			this.caption_forecolor = DataGrid.def_caption_forecolor;
			this.caption_text = string.Empty;
			this.caption_visible = true;
			this.datamember = string.Empty;
			this.parent_rows_backcolor = DataGrid.def_parent_rows_backcolor;
			this.parent_rows_forecolor = DataGrid.def_parent_rows_forecolor;
			this.parent_rows_visible = true;
			this.current_cell = default(DataGridCell);
			this.parent_rows_label_style = DataGridParentRowsLabelStyle.Both;
			this.selected_rows = new Hashtable();
			this.selection_start = -1;
			this.rows = new DataGridRelationshipRow[0];
			this.default_style = new DataGridTableStyle(true);
			this.grid_style = new DataGridTableStyle();
			this.styles_collection = new GridTableStylesCollection(this);
			this.styles_collection.CollectionChanged += new CollectionChangeEventHandler(this.OnTableStylesCollectionChanged);
			this.CurrentTableStyle = this.grid_style;
			this.horiz_scrollbar = new ImplicitHScrollBar();
			this.horiz_scrollbar.Scroll += this.GridHScrolled;
			this.vert_scrollbar = new ImplicitVScrollBar();
			this.vert_scrollbar.Scroll += this.GridVScrolled;
			base.SetStyle(ControlStyles.UserMouse, true);
			this.data_source_stack = new Stack();
			this.back_button_image = ResourceImageLoader.Get("go-previous.png");
			this.back_button_image.MakeTransparent(Color.Transparent);
			this.parent_rows_button_image = ResourceImageLoader.Get("go-top.png");
			this.parent_rows_button_image.MakeTransparent(Color.Transparent);
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0002FE08 File Offset: 0x0002E008
		// Note: this type is marked as 'beforefieldinit'.
		static DataGrid()
		{
			DataGrid.AllowNavigationChangedEvent = new object();
			DataGrid.BackButtonClickEvent = new object();
			DataGrid.BackgroundColorChangedEvent = new object();
			DataGrid.BorderStyleChangedEvent = new object();
			DataGrid.CaptionVisibleChangedEvent = new object();
			DataGrid.CurrentCellChangedEvent = new object();
			DataGrid.DataSourceChangedEvent = new object();
			DataGrid.FlatModeChangedEvent = new object();
			DataGrid.NavigateEvent = new object();
			DataGrid.ParentRowsLabelStyleChangedEvent = new object();
			DataGrid.ParentRowsVisibleChangedEvent = new object();
			DataGrid.ReadOnlyChangedEvent = new object();
			DataGrid.RowHeaderClickEvent = new object();
			DataGrid.ScrollEvent = new object();
			DataGrid.ShowParentDetailsButtonClickEvent = new object();
			DataGrid.UIACollectionChangedEvent = new object();
			DataGrid.UIASelectionChangedEvent = new object();
			DataGrid.UIAColumnHeadersVisibleChangedEvent = new object();
			DataGrid.UIAGridCellChangedEvent = new object();
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGrid.AllowNavigation" /> property has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000B5 RID: 181
		// (add) Token: 0x06000BA6 RID: 2982 RVA: 0x0002FF20 File Offset: 0x0002E120
		// (remove) Token: 0x06000BA7 RID: 2983 RVA: 0x0002FF34 File Offset: 0x0002E134
		public event EventHandler AllowNavigationChanged
		{
			add
			{
				base.Events.AddHandler(DataGrid.AllowNavigationChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.AllowNavigationChangedEvent, value);
			}
		}

		/// <summary>Occurs when the Back button on a child table is clicked.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000B6 RID: 182
		// (add) Token: 0x06000BA8 RID: 2984 RVA: 0x0002FF48 File Offset: 0x0002E148
		// (remove) Token: 0x06000BA9 RID: 2985 RVA: 0x0002FF5C File Offset: 0x0002E15C
		public event EventHandler BackButtonClick
		{
			add
			{
				base.Events.AddHandler(DataGrid.BackButtonClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.BackButtonClickEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGrid.BackgroundColor" /> has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000B7 RID: 183
		// (add) Token: 0x06000BAA RID: 2986 RVA: 0x0002FF70 File Offset: 0x0002E170
		// (remove) Token: 0x06000BAB RID: 2987 RVA: 0x0002FF84 File Offset: 0x0002E184
		public event EventHandler BackgroundColorChanged
		{
			add
			{
				base.Events.AddHandler(DataGrid.BackgroundColorChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.BackgroundColorChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGrid.BackgroundImage" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000B8 RID: 184
		// (add) Token: 0x06000BAC RID: 2988 RVA: 0x0002FF98 File Offset: 0x0002E198
		// (remove) Token: 0x06000BAD RID: 2989 RVA: 0x0002FFA4 File Offset: 0x0002E1A4
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGrid.BackgroundImageLayout" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000B9 RID: 185
		// (add) Token: 0x06000BAE RID: 2990 RVA: 0x0002FFB0 File Offset: 0x0002E1B0
		// (remove) Token: 0x06000BAF RID: 2991 RVA: 0x0002FFBC File Offset: 0x0002E1BC
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGrid.Text" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000BA RID: 186
		// (add) Token: 0x06000BB0 RID: 2992 RVA: 0x0002FFC8 File Offset: 0x0002E1C8
		// (remove) Token: 0x06000BB1 RID: 2993 RVA: 0x0002FFD4 File Offset: 0x0002E1D4
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler TextChanged
		{
			add
			{
				base.TextChanged += value;
			}
			remove
			{
				base.TextChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGrid.Cursor" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000BB RID: 187
		// (add) Token: 0x06000BB2 RID: 2994 RVA: 0x0002FFE0 File Offset: 0x0002E1E0
		// (remove) Token: 0x06000BB3 RID: 2995 RVA: 0x0002FFEC File Offset: 0x0002E1EC
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler CursorChanged
		{
			add
			{
				base.CursorChanged += value;
			}
			remove
			{
				base.CursorChanged -= value;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGrid.BorderStyle" /> has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000BC RID: 188
		// (add) Token: 0x06000BB4 RID: 2996 RVA: 0x0002FFF8 File Offset: 0x0002E1F8
		// (remove) Token: 0x06000BB5 RID: 2997 RVA: 0x0003000C File Offset: 0x0002E20C
		public event EventHandler BorderStyleChanged
		{
			add
			{
				base.Events.AddHandler(DataGrid.BorderStyleChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.BorderStyleChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGrid.CaptionVisible" /> property has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000BD RID: 189
		// (add) Token: 0x06000BB6 RID: 2998 RVA: 0x00030020 File Offset: 0x0002E220
		// (remove) Token: 0x06000BB7 RID: 2999 RVA: 0x00030034 File Offset: 0x0002E234
		public event EventHandler CaptionVisibleChanged
		{
			add
			{
				base.Events.AddHandler(DataGrid.CaptionVisibleChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.CaptionVisibleChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGrid.CurrentCell" /> property has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000BE RID: 190
		// (add) Token: 0x06000BB8 RID: 3000 RVA: 0x00030048 File Offset: 0x0002E248
		// (remove) Token: 0x06000BB9 RID: 3001 RVA: 0x0003005C File Offset: 0x0002E25C
		public event EventHandler CurrentCellChanged
		{
			add
			{
				base.Events.AddHandler(DataGrid.CurrentCellChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.CurrentCellChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGrid.DataSource" /> property value has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000BF RID: 191
		// (add) Token: 0x06000BBA RID: 3002 RVA: 0x00030070 File Offset: 0x0002E270
		// (remove) Token: 0x06000BBB RID: 3003 RVA: 0x00030084 File Offset: 0x0002E284
		public event EventHandler DataSourceChanged
		{
			add
			{
				base.Events.AddHandler(DataGrid.DataSourceChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.DataSourceChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGrid.FlatMode" /> has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000C0 RID: 192
		// (add) Token: 0x06000BBC RID: 3004 RVA: 0x00030098 File Offset: 0x0002E298
		// (remove) Token: 0x06000BBD RID: 3005 RVA: 0x000300AC File Offset: 0x0002E2AC
		public event EventHandler FlatModeChanged
		{
			add
			{
				base.Events.AddHandler(DataGrid.FlatModeChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.FlatModeChangedEvent, value);
			}
		}

		/// <summary>Occurs when the user navigates to a new table.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000C1 RID: 193
		// (add) Token: 0x06000BBE RID: 3006 RVA: 0x000300C0 File Offset: 0x0002E2C0
		// (remove) Token: 0x06000BBF RID: 3007 RVA: 0x000300D4 File Offset: 0x0002E2D4
		public event NavigateEventHandler Navigate
		{
			add
			{
				base.Events.AddHandler(DataGrid.NavigateEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.NavigateEvent, value);
			}
		}

		/// <summary>Occurs when the label style of the parent row is changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000C2 RID: 194
		// (add) Token: 0x06000BC0 RID: 3008 RVA: 0x000300E8 File Offset: 0x0002E2E8
		// (remove) Token: 0x06000BC1 RID: 3009 RVA: 0x000300FC File Offset: 0x0002E2FC
		public event EventHandler ParentRowsLabelStyleChanged
		{
			add
			{
				base.Events.AddHandler(DataGrid.ParentRowsLabelStyleChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.ParentRowsLabelStyleChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGrid.ParentRowsVisible" /> property value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000C3 RID: 195
		// (add) Token: 0x06000BC2 RID: 3010 RVA: 0x00030110 File Offset: 0x0002E310
		// (remove) Token: 0x06000BC3 RID: 3011 RVA: 0x00030124 File Offset: 0x0002E324
		public event EventHandler ParentRowsVisibleChanged
		{
			add
			{
				base.Events.AddHandler(DataGrid.ParentRowsVisibleChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.ParentRowsVisibleChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGrid.ReadOnly" /> property value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000C4 RID: 196
		// (add) Token: 0x06000BC4 RID: 3012 RVA: 0x00030138 File Offset: 0x0002E338
		// (remove) Token: 0x06000BC5 RID: 3013 RVA: 0x0003014C File Offset: 0x0002E34C
		public event EventHandler ReadOnlyChanged
		{
			add
			{
				base.Events.AddHandler(DataGrid.ReadOnlyChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.ReadOnlyChangedEvent, value);
			}
		}

		/// <summary>Occurs when a row header is clicked.</summary>
		// Token: 0x140000C5 RID: 197
		// (add) Token: 0x06000BC6 RID: 3014 RVA: 0x00030160 File Offset: 0x0002E360
		// (remove) Token: 0x06000BC7 RID: 3015 RVA: 0x00030174 File Offset: 0x0002E374
		protected event EventHandler RowHeaderClick
		{
			add
			{
				base.Events.AddHandler(DataGrid.RowHeaderClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.RowHeaderClickEvent, value);
			}
		}

		/// <summary>Occurs when the user scrolls the <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000C6 RID: 198
		// (add) Token: 0x06000BC8 RID: 3016 RVA: 0x00030188 File Offset: 0x0002E388
		// (remove) Token: 0x06000BC9 RID: 3017 RVA: 0x0003019C File Offset: 0x0002E39C
		public event EventHandler Scroll
		{
			add
			{
				base.Events.AddHandler(DataGrid.ScrollEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.ScrollEvent, value);
			}
		}

		/// <summary>Occurs when the ShowParentDetails button is clicked.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000C7 RID: 199
		// (add) Token: 0x06000BCA RID: 3018 RVA: 0x000301B0 File Offset: 0x0002E3B0
		// (remove) Token: 0x06000BCB RID: 3019 RVA: 0x000301C4 File Offset: 0x0002E3C4
		public event EventHandler ShowParentDetailsButtonClick
		{
			add
			{
				base.Events.AddHandler(DataGrid.ShowParentDetailsButtonClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.ShowParentDetailsButtonClickEvent, value);
			}
		}

		// Token: 0x140000C8 RID: 200
		// (add) Token: 0x06000BCC RID: 3020 RVA: 0x000301D8 File Offset: 0x0002E3D8
		// (remove) Token: 0x06000BCD RID: 3021 RVA: 0x000301EC File Offset: 0x0002E3EC
		internal event CollectionChangeEventHandler UIACollectionChanged
		{
			add
			{
				base.Events.AddHandler(DataGrid.UIACollectionChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.UIACollectionChangedEvent, value);
			}
		}

		// Token: 0x140000C9 RID: 201
		// (add) Token: 0x06000BCE RID: 3022 RVA: 0x00030200 File Offset: 0x0002E400
		// (remove) Token: 0x06000BCF RID: 3023 RVA: 0x00030214 File Offset: 0x0002E414
		internal event CollectionChangeEventHandler UIASelectionChanged
		{
			add
			{
				base.Events.AddHandler(DataGrid.UIASelectionChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.UIASelectionChangedEvent, value);
			}
		}

		// Token: 0x140000CA RID: 202
		// (add) Token: 0x06000BD0 RID: 3024 RVA: 0x00030228 File Offset: 0x0002E428
		// (remove) Token: 0x06000BD1 RID: 3025 RVA: 0x0003023C File Offset: 0x0002E43C
		internal event EventHandler UIAColumnHeadersVisibleChanged
		{
			add
			{
				base.Events.AddHandler(DataGrid.UIAColumnHeadersVisibleChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.UIAColumnHeadersVisibleChangedEvent, value);
			}
		}

		// Token: 0x140000CB RID: 203
		// (add) Token: 0x06000BD2 RID: 3026 RVA: 0x00030250 File Offset: 0x0002E450
		// (remove) Token: 0x06000BD3 RID: 3027 RVA: 0x00030264 File Offset: 0x0002E464
		internal event CollectionChangeEventHandler UIAGridCellChanged
		{
			add
			{
				base.Events.AddHandler(DataGrid.UIAGridCellChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.UIAGridCellChangedEvent, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether navigation is allowed.</summary>
		/// <returns>true if navigation is allowed; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x00030278 File Offset: 0x0002E478
		// (set) Token: 0x06000BD5 RID: 3029 RVA: 0x00030280 File Offset: 0x0002E480
		[DefaultValue(true)]
		public bool AllowNavigation
		{
			get
			{
				return this.allow_navigation;
			}
			set
			{
				if (this.allow_navigation != value)
				{
					this.allow_navigation = value;
					this.OnAllowNavigationChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the grid can be resorted by clicking on a column header.</summary>
		/// <returns>true if columns can be sorted; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x000302A0 File Offset: 0x0002E4A0
		// (set) Token: 0x06000BD7 RID: 3031 RVA: 0x000302B0 File Offset: 0x0002E4B0
		[DefaultValue(true)]
		public bool AllowSorting
		{
			get
			{
				return this.grid_style.AllowSorting;
			}
			set
			{
				this.grid_style.AllowSorting = value;
			}
		}

		/// <summary>Gets or sets the background color of odd-numbered rows of the grid.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the alternating background color. The default is the system color for windows (<see cref="P:System.Drawing.SystemColors.Window" />).</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x000302C0 File Offset: 0x0002E4C0
		// (set) Token: 0x06000BD9 RID: 3033 RVA: 0x000302D0 File Offset: 0x0002E4D0
		public Color AlternatingBackColor
		{
			get
			{
				return this.grid_style.AlternatingBackColor;
			}
			set
			{
				this.grid_style.AlternatingBackColor = value;
			}
		}

		/// <summary>Gets or sets the background color of even-numbered rows of the grid.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color of rows in the grid. The default is the system color for windows (<see cref="P:System.Drawing.SystemColors.Window" />).</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x000302E0 File Offset: 0x0002E4E0
		// (set) Token: 0x06000BDB RID: 3035 RVA: 0x000302F0 File Offset: 0x0002E4F0
		public override Color BackColor
		{
			get
			{
				return this.grid_style.BackColor;
			}
			set
			{
				this.grid_style.BackColor = value;
			}
		}

		/// <summary>Gets or sets the color of the non-row area of the grid.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color of the grid's background. The default is the <see cref="P:System.Drawing.SystemColors.AppWorkspace" /> color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x00030300 File Offset: 0x0002E500
		// (set) Token: 0x06000BDD RID: 3037 RVA: 0x00030308 File Offset: 0x0002E508
		public Color BackgroundColor
		{
			get
			{
				return this.background_color;
			}
			set
			{
				if (this.background_color != value)
				{
					this.background_color = value;
					this.OnBackgroundColorChanged(EventArgs.Empty);
					base.Invalidate();
				}
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x00030334 File Offset: 0x0002E534
		// (set) Token: 0x06000BDF RID: 3039 RVA: 0x0003033C File Offset: 0x0002E53C
		[EditorBrowsable(1)]
		[Browsable(false)]
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
				base.Invalidate();
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageLayout" /> value.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x00030358 File Offset: 0x0002E558
		// (set) Token: 0x06000BE1 RID: 3041 RVA: 0x00030360 File Offset: 0x0002E560
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

		/// <summary>Gets or sets the grid's border style.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> enumeration values. The default is FixedSingle.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x0003036C File Offset: 0x0002E56C
		// (set) Token: 0x06000BE3 RID: 3043 RVA: 0x00030374 File Offset: 0x0002E574
		[DefaultValue(BorderStyle.Fixed3D)]
		[DispId(-504)]
		public BorderStyle BorderStyle
		{
			get
			{
				return base.InternalBorderStyle;
			}
			set
			{
				base.InternalBorderStyle = value;
				this.CalcAreasAndInvalidate();
				this.OnBorderStyleChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the background color of the caption area.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the caption's background color. The default is <see cref="P:System.Drawing.SystemColors.ActiveCaption" /> color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x00030390 File Offset: 0x0002E590
		// (set) Token: 0x06000BE5 RID: 3045 RVA: 0x00030398 File Offset: 0x0002E598
		public Color CaptionBackColor
		{
			get
			{
				return this.caption_backcolor;
			}
			set
			{
				if (this.caption_backcolor != value)
				{
					this.caption_backcolor = value;
					this.InvalidateCaption();
				}
			}
		}

		/// <summary>Gets or sets the font of the grid's caption.</summary>
		/// <returns>A <see cref="T:System.Drawing.Font" /> that represents the caption's font.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x000303B8 File Offset: 0x0002E5B8
		// (set) Token: 0x06000BE7 RID: 3047 RVA: 0x000303D8 File Offset: 0x0002E5D8
		[Localizable(true)]
		[AmbientValue(null)]
		public Font CaptionFont
		{
			get
			{
				if (this.caption_font == null)
				{
					return new Font(this.Font, 1);
				}
				return this.caption_font;
			}
			set
			{
				if (this.caption_font != null && this.caption_font.Equals(value))
				{
					return;
				}
				this.caption_font = value;
				this.CalcAreasAndInvalidate();
			}
		}

		/// <summary>Gets or sets the foreground color of the caption area.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the caption area. The default is <see cref="P:System.Drawing.SystemColors.ActiveCaptionText" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x00030410 File Offset: 0x0002E610
		// (set) Token: 0x06000BE9 RID: 3049 RVA: 0x00030418 File Offset: 0x0002E618
		public Color CaptionForeColor
		{
			get
			{
				return this.caption_forecolor;
			}
			set
			{
				if (this.caption_forecolor != value)
				{
					this.caption_forecolor = value;
					this.InvalidateCaption();
				}
			}
		}

		/// <summary>Gets or sets the text of the grid's window caption.</summary>
		/// <returns>A string to be displayed as the window caption of the grid. The default is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x00030438 File Offset: 0x0002E638
		// (set) Token: 0x06000BEB RID: 3051 RVA: 0x00030440 File Offset: 0x0002E640
		[Localizable(true)]
		[DefaultValue("")]
		public string CaptionText
		{
			get
			{
				return this.caption_text;
			}
			set
			{
				if (this.caption_text != value)
				{
					this.caption_text = value;
					this.InvalidateCaption();
				}
			}
		}

		/// <summary>Gets or sets a value that indicates whether the grid's caption is visible.</summary>
		/// <returns>true if the caption is visible; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x00030460 File Offset: 0x0002E660
		// (set) Token: 0x06000BED RID: 3053 RVA: 0x00030468 File Offset: 0x0002E668
		[DefaultValue(true)]
		public bool CaptionVisible
		{
			get
			{
				return this.caption_visible;
			}
			set
			{
				if (this.caption_visible != value)
				{
					this.EndEdit();
					this.caption_visible = value;
					this.CalcAreasAndInvalidate();
					this.OnCaptionVisibleChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the column headers of a table are visible.</summary>
		/// <returns>true if the column headers are visible; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x000304A0 File Offset: 0x0002E6A0
		// (set) Token: 0x06000BEF RID: 3055 RVA: 0x000304B0 File Offset: 0x0002E6B0
		[DefaultValue(true)]
		public bool ColumnHeadersVisible
		{
			get
			{
				return this.grid_style.ColumnHeadersVisible;
			}
			set
			{
				if (this.grid_style.ColumnHeadersVisible != value)
				{
					this.grid_style.ColumnHeadersVisible = value;
					this.OnUIAColumnHeadersVisibleChanged();
				}
			}
		}

		/// <summary>Gets or sets which cell has the focus. Not available at design time.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridCell" /> with the focus.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x000304D8 File Offset: 0x0002E6D8
		// (set) Token: 0x06000BF1 RID: 3057 RVA: 0x000304E0 File Offset: 0x0002E6E0
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public DataGridCell CurrentCell
		{
			get
			{
				return this.current_cell;
			}
			set
			{
				if (this.setting_current_cell)
				{
					return;
				}
				this.setting_current_cell = true;
				if (!base.IsHandleCreated)
				{
					this.setting_current_cell = false;
					throw new Exception("CurrentCell cannot be set at this time.");
				}
				if (this.current_cell.Equals(value))
				{
					this.setting_current_cell = false;
					return;
				}
				if (this.ReadOnly && value.RowNumber > this.RowsCount - 1)
				{
					value.RowNumber = this.RowsCount - 1;
				}
				else if (value.RowNumber > this.RowsCount)
				{
					value.RowNumber = this.RowsCount;
				}
				if (value.ColumnNumber >= this.CurrentTableStyle.GridColumnStyles.Count)
				{
					value.ColumnNumber = ((this.CurrentTableStyle.GridColumnStyles.Count != 0) ? (this.CurrentTableStyle.GridColumnStyles.Count - 1) : 0);
				}
				if (value.RowNumber < 0)
				{
					value.RowNumber = 0;
				}
				if (value.ColumnNumber < 0)
				{
					value.ColumnNumber = 0;
				}
				bool flag = this.is_changing;
				this.add_row_changed = this.add_row_changed || flag;
				this.EndEdit();
				if (value.RowNumber != this.current_cell.RowNumber)
				{
					if (!this.from_positionchanged_handler)
					{
						try
						{
							if (this.commit_row_changes)
							{
								this.ListManager.EndCurrentEdit();
							}
							else
							{
								this.ListManager.CancelCurrentEdit();
							}
						}
						catch (Exception ex)
						{
							DialogResult dialogResult = MessageBox.Show(string.Format("{0} Do you wish to correct the value?", ex.Message), "Error when committing the row to the original data source", MessageBoxButtons.YesNo);
							if (dialogResult == DialogResult.Yes)
							{
								this.InvalidateRowHeader(value.RowNumber);
								this.InvalidateRowHeader(this.current_cell.RowNumber);
								this.setting_current_cell = false;
								this.Edit();
								return;
							}
							this.ListManager.CancelCurrentEdit();
						}
					}
					if (value.RowNumber == this.RowsCount && !this.ListManager.AllowNew)
					{
						value.RowNumber--;
					}
				}
				int rowNumber = this.current_cell.RowNumber;
				this.current_cell = value;
				this.EnsureCellVisibility(value);
				if (this.CurrentRow == this.RowsCount && this.ListManager.AllowNew)
				{
					this.commit_row_changes = false;
					this.cursor_in_add_row = true;
					this.add_row_changed = false;
					this.adding_new_row = true;
					this.AddNewRow();
					this.adding_new_row = false;
				}
				else
				{
					this.cursor_in_add_row = false;
					this.commit_row_changes = true;
				}
				this.InvalidateRowHeader(rowNumber);
				this.InvalidateRowHeader(this.current_cell.RowNumber);
				this.list_manager.Position = this.current_cell.RowNumber;
				this.OnCurrentCellChanged(EventArgs.Empty);
				if (!this.from_positionchanged_handler)
				{
					this.Edit();
				}
				this.setting_current_cell = false;
			}
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x000307F0 File Offset: 0x0002E9F0
		internal void EditRowChanged(DataGridColumnStyle column_style)
		{
			if (this.cursor_in_add_row && !this.commit_row_changes)
			{
				this.commit_row_changes = true;
				this.RecreateDataGridRows(true);
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x00030824 File Offset: 0x0002EA24
		// (set) Token: 0x06000BF4 RID: 3060 RVA: 0x00030834 File Offset: 0x0002EA34
		private int CurrentRow
		{
			get
			{
				return this.current_cell.RowNumber;
			}
			set
			{
				this.CurrentCell = new DataGridCell(value, this.current_cell.ColumnNumber);
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x00030850 File Offset: 0x0002EA50
		// (set) Token: 0x06000BF6 RID: 3062 RVA: 0x00030860 File Offset: 0x0002EA60
		private int CurrentColumn
		{
			get
			{
				return this.current_cell.ColumnNumber;
			}
			set
			{
				this.CurrentCell = new DataGridCell(this.current_cell.RowNumber, value);
			}
		}

		/// <summary>Gets or sets index of the row that currently has focus.</summary>
		/// <returns>The zero-based index of the current row.</returns>
		/// <exception cref="T:System.Exception">There is no <see cref="T:System.Windows.Forms.CurrencyManager" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x0003087C File Offset: 0x0002EA7C
		// (set) Token: 0x06000BF8 RID: 3064 RVA: 0x00030894 File Offset: 0x0002EA94
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public int CurrentRowIndex
		{
			get
			{
				if (this.ListManager == null)
				{
					return -1;
				}
				return this.CurrentRow;
			}
			set
			{
				this.CurrentRow = value;
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Cursor" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x000308A0 File Offset: 0x0002EAA0
		// (set) Token: 0x06000BFA RID: 3066 RVA: 0x000308A8 File Offset: 0x0002EAA8
		[Browsable(false)]
		[EditorBrowsable(1)]
		public override Cursor Cursor
		{
			get
			{
				return base.Cursor;
			}
			set
			{
				base.Cursor = value;
			}
		}

		/// <summary>Gets or sets the specific list in a <see cref="P:System.Windows.Forms.DataGrid.DataSource" /> for which the <see cref="T:System.Windows.Forms.DataGrid" /> control displays a grid.</summary>
		/// <returns>A list in a <see cref="P:System.Windows.Forms.DataGrid.DataSource" />. The default is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x000308B4 File Offset: 0x0002EAB4
		// (set) Token: 0x06000BFC RID: 3068 RVA: 0x000308BC File Offset: 0x0002EABC
		[DefaultValue(null)]
		[Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string DataMember
		{
			get
			{
				return this.datamember;
			}
			set
			{
				if (this.BindingContext != null)
				{
					this.SetDataSource(this.datasource, value);
				}
				else
				{
					if (this.list_manager != null)
					{
						this.list_manager = null;
					}
					this.datamember = value;
					this.refetch_list_manager = true;
				}
			}
		}

		/// <summary>Gets or sets the data source that the grid is displaying data for.</summary>
		/// <returns>An object that functions as a data source.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x000308FC File Offset: 0x0002EAFC
		// (set) Token: 0x06000BFE RID: 3070 RVA: 0x00030904 File Offset: 0x0002EB04
		[AttributeProvider(typeof(IListSource))]
		[DefaultValue(null)]
		[RefreshProperties(2)]
		public object DataSource
		{
			get
			{
				return this.datasource;
			}
			set
			{
				if (this.BindingContext != null)
				{
					this.SetDataSource(value, (this.ListManager != null) ? string.Empty : this.datamember);
				}
				else
				{
					this.datasource = value;
					if (this.list_manager != null)
					{
						this.datamember = string.Empty;
					}
					if (this.list_manager != null)
					{
						this.list_manager = null;
					}
					this.refetch_list_manager = true;
				}
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default size of the control.</returns>
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x0003097C File Offset: 0x0002EB7C
		protected override Size DefaultSize
		{
			get
			{
				return new Size(130, 80);
			}
		}

		/// <summary>Gets the index of the first visible column in a grid.</summary>
		/// <returns>The index of a <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x0003098C File Offset: 0x0002EB8C
		[Browsable(false)]
		public int FirstVisibleColumn
		{
			get
			{
				return this.first_visible_column;
			}
		}

		/// <summary>Gets or sets a value indicating whether the grid displays in flat mode.</summary>
		/// <returns>true if the grid is displayed flat; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x00030994 File Offset: 0x0002EB94
		// (set) Token: 0x06000C02 RID: 3074 RVA: 0x0003099C File Offset: 0x0002EB9C
		[DefaultValue(false)]
		public bool FlatMode
		{
			get
			{
				return this.flatmode;
			}
			set
			{
				if (this.flatmode != value)
				{
					this.flatmode = value;
					this.OnFlatModeChanged(EventArgs.Empty);
					this.Refresh();
				}
			}
		}

		/// <summary>Gets or sets the foreground color (typically the color of the text) property of the <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color. The default is <see cref="P:System.Drawing.SystemBrushes.WindowText" /> color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x000309D0 File Offset: 0x0002EBD0
		// (set) Token: 0x06000C04 RID: 3076 RVA: 0x000309E0 File Offset: 0x0002EBE0
		public override Color ForeColor
		{
			get
			{
				return this.grid_style.ForeColor;
			}
			set
			{
				this.grid_style.ForeColor = value;
			}
		}

		/// <summary>Gets or sets the color of the grid lines.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color of the grid lines. The default is the system color for controls (<see cref="P:System.Drawing.SystemColors.Control" />).</returns>
		/// <exception cref="T:System.ArgumentException">The value is not set. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x000309F0 File Offset: 0x0002EBF0
		// (set) Token: 0x06000C06 RID: 3078 RVA: 0x00030A00 File Offset: 0x0002EC00
		public Color GridLineColor
		{
			get
			{
				return this.grid_style.GridLineColor;
			}
			set
			{
				if (value == Color.Empty)
				{
					throw new ArgumentException("Color.Empty value is invalid.");
				}
				this.grid_style.GridLineColor = value;
			}
		}

		/// <summary>Gets or sets the line style of the grid.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridLineStyle" /> values. The default is Solid.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x00030A2C File Offset: 0x0002EC2C
		// (set) Token: 0x06000C08 RID: 3080 RVA: 0x00030A3C File Offset: 0x0002EC3C
		[DefaultValue(DataGridLineStyle.Solid)]
		public DataGridLineStyle GridLineStyle
		{
			get
			{
				return this.grid_style.GridLineStyle;
			}
			set
			{
				this.grid_style.GridLineStyle = value;
			}
		}

		/// <summary>Gets or sets the background color of all row and column headers.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of row and column headers. The default is the system color for controls, <see cref="P:System.Drawing.SystemColors.Control" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">While trying to set the property, a Color.Empty was passed. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x00030A4C File Offset: 0x0002EC4C
		// (set) Token: 0x06000C0A RID: 3082 RVA: 0x00030A5C File Offset: 0x0002EC5C
		public Color HeaderBackColor
		{
			get
			{
				return this.grid_style.HeaderBackColor;
			}
			set
			{
				if (value == Color.Empty)
				{
					throw new ArgumentException("Color.Empty value is invalid.");
				}
				this.grid_style.HeaderBackColor = value;
			}
		}

		/// <summary>Gets or sets the font used for column headers.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> that represents the header text.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x00030A88 File Offset: 0x0002EC88
		// (set) Token: 0x06000C0C RID: 3084 RVA: 0x00030A98 File Offset: 0x0002EC98
		public Font HeaderFont
		{
			get
			{
				return this.grid_style.HeaderFont;
			}
			set
			{
				this.grid_style.HeaderFont = value;
			}
		}

		/// <summary>Gets or sets the foreground color of headers.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the grid's column headers, including the column header text and the plus/minus glyphs. The default is <see cref="P:System.Drawing.SystemColors.ControlText" /> color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x00030AA8 File Offset: 0x0002ECA8
		// (set) Token: 0x06000C0E RID: 3086 RVA: 0x00030AB8 File Offset: 0x0002ECB8
		public Color HeaderForeColor
		{
			get
			{
				return this.grid_style.HeaderForeColor;
			}
			set
			{
				this.grid_style.HeaderForeColor = value;
			}
		}

		/// <summary>Gets the horizontal scroll bar for the grid.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ScrollBar" /> for the grid.</returns>
		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x00030AC8 File Offset: 0x0002ECC8
		protected ScrollBar HorizScrollBar
		{
			get
			{
				return this.horiz_scrollbar;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x00030AD0 File Offset: 0x0002ECD0
		internal ScrollBar HScrollBar
		{
			get
			{
				return this.horiz_scrollbar;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x00030AD8 File Offset: 0x0002ECD8
		internal int HorizPixelOffset
		{
			get
			{
				return this.horiz_pixeloffset;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x00030AE0 File Offset: 0x0002ECE0
		internal bool IsChanging
		{
			get
			{
				return this.is_changing;
			}
		}

		/// <summary>Gets or sets the value of a specified <see cref="T:System.Windows.Forms.DataGridCell" />.</summary>
		/// <returns>The value, typed as <see cref="T:System.Object" />, of the cell.</returns>
		/// <param name="cell">A <see cref="T:System.Windows.Forms.DataGridCell" /> that represents a cell in the grid. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002BE RID: 702
		public object this[DataGridCell cell]
		{
			get
			{
				return this[cell.RowNumber, cell.ColumnNumber];
			}
			set
			{
				this[cell.RowNumber, cell.ColumnNumber] = value;
			}
		}

		/// <summary>Gets or sets the value of the cell at the specified the row and column.</summary>
		/// <returns>The value, typed as <see cref="T:System.Object" />, of the cell.</returns>
		/// <param name="rowIndex">The zero-based index of the row containing the value. </param>
		/// <param name="columnIndex">The zero-based index of the column containing the value. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">While getting or setting, the <paramref name="rowIndex" /> is out of range.While getting or setting, the <paramref name="columnIndex" /> is out of range. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002BF RID: 703
		public object this[int rowIndex, int columnIndex]
		{
			get
			{
				return this.CurrentTableStyle.GridColumnStyles[columnIndex].GetColumnValueAtRow(this.ListManager, rowIndex);
			}
			set
			{
				this.CurrentTableStyle.GridColumnStyles[columnIndex].SetColumnValueAtRow(this.ListManager, rowIndex, value);
				this.OnUIAGridCellChanged(new CollectionChangeEventArgs(3, new DataGridCell(rowIndex, columnIndex)));
			}
		}

		/// <summary>Gets or sets the color of the text that you can click to navigate to a child table.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color of text that is clicked to navigate to a child table. The default is <see cref="P:System.Drawing.SystemColors.HotTrack" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x00030B94 File Offset: 0x0002ED94
		// (set) Token: 0x06000C18 RID: 3096 RVA: 0x00030BA4 File Offset: 0x0002EDA4
		public Color LinkColor
		{
			get
			{
				return this.grid_style.LinkColor;
			}
			set
			{
				this.grid_style.LinkColor = value;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000C19 RID: 3097 RVA: 0x00030BB4 File Offset: 0x0002EDB4
		internal Font LinkFont
		{
			get
			{
				return new Font(this.Font, 4);
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x00030BC4 File Offset: 0x0002EDC4
		// (set) Token: 0x06000C1B RID: 3099 RVA: 0x00030BD4 File Offset: 0x0002EDD4
		[Browsable(false)]
		[EditorBrowsable(1)]
		public Color LinkHoverColor
		{
			get
			{
				return this.grid_style.LinkHoverColor;
			}
			set
			{
				this.grid_style.LinkHoverColor = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.CurrencyManager" /> for this <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.CurrencyManager" /> for this <see cref="T:System.Windows.Forms.DataGrid" /> control.</returns>
		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x00030BE4 File Offset: 0x0002EDE4
		// (set) Token: 0x06000C1D RID: 3101 RVA: 0x00030C1C File Offset: 0x0002EE1C
		[Browsable(false)]
		[EditorBrowsable(2)]
		protected internal CurrencyManager ListManager
		{
			get
			{
				if (this.list_manager == null && this.refetch_list_manager)
				{
					this.SetDataSource(this.datasource, this.datamember);
					this.refetch_list_manager = false;
				}
				return this.list_manager;
			}
			set
			{
				throw new NotSupportedException("Operation is not supported.");
			}
		}

		/// <summary>Gets or sets the background color of parent rows.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color of parent rows. The default is the <see cref="P:System.Drawing.SystemColors.Control" /> color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x00030C28 File Offset: 0x0002EE28
		// (set) Token: 0x06000C1F RID: 3103 RVA: 0x00030C30 File Offset: 0x0002EE30
		public Color ParentRowsBackColor
		{
			get
			{
				return this.parent_rows_backcolor;
			}
			set
			{
				if (this.parent_rows_backcolor != value)
				{
					this.parent_rows_backcolor = value;
					if (this.parent_rows_visible)
					{
						this.Refresh();
					}
				}
			}
		}

		/// <summary>Gets or sets the foreground color of parent rows.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of parent rows. The default is the <see cref="P:System.Drawing.SystemColors.WindowText" /> color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x00030C5C File Offset: 0x0002EE5C
		// (set) Token: 0x06000C21 RID: 3105 RVA: 0x00030C64 File Offset: 0x0002EE64
		public Color ParentRowsForeColor
		{
			get
			{
				return this.parent_rows_forecolor;
			}
			set
			{
				if (this.parent_rows_forecolor != value)
				{
					this.parent_rows_forecolor = value;
					if (this.parent_rows_visible)
					{
						this.Refresh();
					}
				}
			}
		}

		/// <summary>Gets or sets the way parent row labels are displayed.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridParentRowsLabelStyle" /> values. The default is Both.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The enumerator was not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x00030C90 File Offset: 0x0002EE90
		// (set) Token: 0x06000C23 RID: 3107 RVA: 0x00030C98 File Offset: 0x0002EE98
		[DefaultValue(DataGridParentRowsLabelStyle.Both)]
		[DesignerSerializationVisibility(0)]
		public DataGridParentRowsLabelStyle ParentRowsLabelStyle
		{
			get
			{
				return this.parent_rows_label_style;
			}
			set
			{
				if (this.parent_rows_label_style != value)
				{
					this.parent_rows_label_style = value;
					if (this.parent_rows_visible)
					{
						this.Refresh();
					}
					this.OnParentRowsLabelStyleChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the parent rows of a table are visible.</summary>
		/// <returns>true if the parent rows are visible; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x00030CCC File Offset: 0x0002EECC
		// (set) Token: 0x06000C25 RID: 3109 RVA: 0x00030CD4 File Offset: 0x0002EED4
		[DefaultValue(true)]
		public bool ParentRowsVisible
		{
			get
			{
				return this.parent_rows_visible;
			}
			set
			{
				if (this.parent_rows_visible != value)
				{
					this.parent_rows_visible = value;
					this.CalcAreasAndInvalidate();
					this.OnParentRowsVisibleChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the default width of the grid columns in pixels.</summary>
		/// <returns>The default width (in pixels) of columns in the grid.</returns>
		/// <exception cref="T:System.ArgumentException">The property value is less than 0. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x00030D08 File Offset: 0x0002EF08
		// (set) Token: 0x06000C27 RID: 3111 RVA: 0x00030D18 File Offset: 0x0002EF18
		[TypeConverter(typeof(DataGridPreferredColumnWidthTypeConverter))]
		[DefaultValue(75)]
		public int PreferredColumnWidth
		{
			get
			{
				return this.grid_style.PreferredColumnWidth;
			}
			set
			{
				this.grid_style.PreferredColumnWidth = value;
			}
		}

		/// <summary>Gets or sets the preferred row height for the <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
		/// <returns>The height of a row.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x00030D28 File Offset: 0x0002EF28
		// (set) Token: 0x06000C29 RID: 3113 RVA: 0x00030D38 File Offset: 0x0002EF38
		public int PreferredRowHeight
		{
			get
			{
				return this.grid_style.PreferredRowHeight;
			}
			set
			{
				this.grid_style.PreferredRowHeight = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the grid is in read-only mode.</summary>
		/// <returns>true if the grid is in read-only mode; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x00030D48 File Offset: 0x0002EF48
		// (set) Token: 0x06000C2B RID: 3115 RVA: 0x00030D50 File Offset: 0x0002EF50
		[DefaultValue(false)]
		public bool ReadOnly
		{
			get
			{
				return this._readonly;
			}
			set
			{
				if (this._readonly != value)
				{
					this._readonly = value;
					this.OnReadOnlyChanged(EventArgs.Empty);
					this.CalcAreasAndInvalidate();
				}
			}
		}

		/// <summary>Gets or sets a value that specifies whether row headers are visible.</summary>
		/// <returns>true if row headers are visible; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x00030D84 File Offset: 0x0002EF84
		// (set) Token: 0x06000C2D RID: 3117 RVA: 0x00030D94 File Offset: 0x0002EF94
		[DefaultValue(true)]
		public bool RowHeadersVisible
		{
			get
			{
				return this.grid_style.RowHeadersVisible;
			}
			set
			{
				this.grid_style.RowHeadersVisible = value;
			}
		}

		/// <summary>Gets or sets the width of row headers.</summary>
		/// <returns>The width of row headers in the <see cref="T:System.Windows.Forms.DataGrid" />. The default is 35.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x00030DA4 File Offset: 0x0002EFA4
		// (set) Token: 0x06000C2F RID: 3119 RVA: 0x00030DB4 File Offset: 0x0002EFB4
		[DefaultValue(35)]
		public int RowHeaderWidth
		{
			get
			{
				return this.grid_style.RowHeaderWidth;
			}
			set
			{
				this.grid_style.RowHeaderWidth = value;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x00030DC4 File Offset: 0x0002EFC4
		internal DataGridRelationshipRow[] DataGridRows
		{
			get
			{
				return this.rows;
			}
		}

		/// <summary>Gets or sets the background color of selected rows.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of selected rows. The default is the <see cref="P:System.Drawing.SystemBrushes.ActiveCaption" /> color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x00030DCC File Offset: 0x0002EFCC
		// (set) Token: 0x06000C32 RID: 3122 RVA: 0x00030DDC File Offset: 0x0002EFDC
		public Color SelectionBackColor
		{
			get
			{
				return this.grid_style.SelectionBackColor;
			}
			set
			{
				this.grid_style.SelectionBackColor = value;
			}
		}

		/// <summary>Gets or set the foreground color of selected rows.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing the foreground color of selected rows. The default is the <see cref="P:System.Drawing.SystemBrushes.ActiveCaptionText" /> color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x00030DEC File Offset: 0x0002EFEC
		// (set) Token: 0x06000C34 RID: 3124 RVA: 0x00030DFC File Offset: 0x0002EFFC
		public Color SelectionForeColor
		{
			get
			{
				return this.grid_style.SelectionForeColor;
			}
			set
			{
				this.grid_style.SelectionForeColor = value;
			}
		}

		/// <returns>The <see cref="T:System.ComponentModel.ISite" /> associated with the <see cref="T:System.Windows.Forms.Control" />, if any.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x00030E0C File Offset: 0x0002F00C
		// (set) Token: 0x06000C36 RID: 3126 RVA: 0x00030E14 File Offset: 0x0002F014
		public override ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				base.Site = value;
			}
		}

		/// <summary>Gets the collection of <see cref="T:System.Windows.Forms.DataGridTableStyle" /> objects for the grid.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.GridTableStylesCollection" /> that represents the collection of <see cref="T:System.Windows.Forms.DataGridTableStyle" /> objects.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x00030E20 File Offset: 0x0002F020
		[DesignerSerializationVisibility(2)]
		[Localizable(true)]
		public GridTableStylesCollection TableStyles
		{
			get
			{
				return this.styles_collection;
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>A <see cref="T:System.String" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x00030E28 File Offset: 0x0002F028
		// (set) Token: 0x06000C39 RID: 3129 RVA: 0x00030E30 File Offset: 0x0002F030
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		[Browsable(false)]
		[Bindable(false)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		/// <summary>Gets the vertical scroll bar of the control.</summary>
		/// <returns>The vertical <see cref="T:System.Windows.Forms.ScrollBar" /> of the grid.</returns>
		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000C3A RID: 3130 RVA: 0x00030E3C File Offset: 0x0002F03C
		[EditorBrowsable(2)]
		[Browsable(false)]
		protected ScrollBar VertScrollBar
		{
			get
			{
				return this.vert_scrollbar;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x00030E44 File Offset: 0x0002F044
		internal ScrollBar VScrollBar
		{
			get
			{
				return this.vert_scrollbar;
			}
		}

		/// <summary>Gets the number of visible columns.</summary>
		/// <returns>The number of columns visible in the viewport. The viewport is the rectangular area through which the grid is visible. The size of the viewport depends on the size of the <see cref="T:System.Windows.Forms.DataGrid" /> control; if you allow users to resize the control, the viewport will also be affected.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x00030E4C File Offset: 0x0002F04C
		[Browsable(false)]
		public int VisibleColumnCount
		{
			get
			{
				return this.visible_column_count;
			}
		}

		/// <summary>Gets the number of rows visible.</summary>
		/// <returns>The number of rows visible in the viewport. The viewport is the rectangular area through which the grid is visible. The size of the viewport depends on the size of the <see cref="T:System.Windows.Forms.DataGrid" /> control; if you allow users to resize the control, the viewport will also be affected.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000C3D RID: 3133 RVA: 0x00030E54 File Offset: 0x0002F054
		[Browsable(false)]
		public int VisibleRowCount
		{
			get
			{
				return this.visible_row_count;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x00030E5C File Offset: 0x0002F05C
		// (set) Token: 0x06000C3F RID: 3135 RVA: 0x00030E64 File Offset: 0x0002F064
		internal DataGridTableStyle CurrentTableStyle
		{
			get
			{
				return this.current_style;
			}
			set
			{
				if (this.current_style != value)
				{
					if (this.current_style != null)
					{
						this.DisconnectTableStyleEvents();
					}
					this.current_style = value;
					if (this.current_style != null)
					{
						this.current_style.DataGrid = this;
						this.ConnectTableStyleEvents();
					}
					this.CalcAreasAndInvalidate();
				}
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x00030EB8 File Offset: 0x0002F0B8
		internal int FirstVisibleRow
		{
			get
			{
				return this.first_visible_row;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x00030EC0 File Offset: 0x0002F0C0
		internal int RowsCount
		{
			get
			{
				return (this.ListManager == null) ? 0 : this.ListManager.Count;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x00030EE0 File Offset: 0x0002F0E0
		internal int RowHeight
		{
			get
			{
				if (this.CurrentTableStyle.CurrentPreferredRowHeight > this.Font.Height + 3 + 1)
				{
					return this.CurrentTableStyle.CurrentPreferredRowHeight;
				}
				return this.Font.Height + 3 + 1;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000C43 RID: 3139 RVA: 0x00030F28 File Offset: 0x0002F128
		internal override bool ScaleChildrenInternal
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x00030F2C File Offset: 0x0002F12C
		internal bool ShowEditRow
		{
			get
			{
				return (this.ListManager == null || this.ListManager.AllowNew) && !this._readonly;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000C45 RID: 3141 RVA: 0x00030F60 File Offset: 0x0002F160
		internal bool ShowParentRows
		{
			get
			{
				return this.ParentRowsVisible && this.data_source_stack.Count > 0;
			}
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x00030F80 File Offset: 0x0002F180
		private void AbortEditing()
		{
			if (this.is_changing)
			{
				this.CurrentTableStyle.GridColumnStyles[this.current_cell.ColumnNumber].Abort(this.current_cell.RowNumber);
				this.is_changing = false;
				this.InvalidateRowHeader(this.current_cell.RowNumber);
			}
		}

		/// <summary>Attempts to put the grid into a state where editing is allowed.</summary>
		/// <returns>true if the method is successful; otherwise, false.</returns>
		/// <param name="gridColumn">A <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to edit. </param>
		/// <param name="rowNumber">The number of the row to edit. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C47 RID: 3143 RVA: 0x00030FDC File Offset: 0x0002F1DC
		public bool BeginEdit(DataGridColumnStyle gridColumn, int rowNumber)
		{
			if (this.is_changing)
			{
				return false;
			}
			int num = this.CurrentTableStyle.GridColumnStyles.IndexOf(gridColumn);
			if (num < 0)
			{
				return false;
			}
			this.CurrentCell = new DataGridCell(rowNumber, num);
			this.Edit();
			return true;
		}

		/// <summary>Begins the initialization of a <see cref="T:System.Windows.Forms.DataGrid" /> that is used on a form or used by another component. The initialization occurs at run time.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C48 RID: 3144 RVA: 0x00031028 File Offset: 0x0002F228
		public void BeginInit()
		{
		}

		/// <summary>Cancels the current edit operation and rolls back all changes.</summary>
		// Token: 0x06000C49 RID: 3145 RVA: 0x0003102C File Offset: 0x0002F22C
		protected virtual void CancelEditing()
		{
			if (this.CurrentTableStyle.GridColumnStyles.Count == 0)
			{
				return;
			}
			this.CurrentTableStyle.GridColumnStyles[this.current_cell.ColumnNumber].ConcedeFocus();
			if (this.is_changing)
			{
				if (this.current_cell.ColumnNumber < this.CurrentTableStyle.GridColumnStyles.Count)
				{
					this.CurrentTableStyle.GridColumnStyles[this.current_cell.ColumnNumber].Abort(this.current_cell.RowNumber);
				}
				this.InvalidateRowHeader(this.current_cell.RowNumber);
			}
			if (this.cursor_in_add_row && !this.is_changing)
			{
				this.ListManager.CancelCurrentEdit();
			}
			this.is_changing = false;
			this.is_editing = false;
		}

		/// <summary>Collapses child relations, if any exist for all rows, or for a specified row.</summary>
		/// <param name="row">The number of the row to collapse. If set to -1, all rows are collapsed. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C4A RID: 3146 RVA: 0x00031108 File Offset: 0x0002F308
		public void Collapse(int row)
		{
			if (!this.rows[row].IsExpanded)
			{
				return;
			}
			base.SuspendLayout();
			this.rows[row].IsExpanded = false;
			for (int i = 1; i < this.rows.Length - row; i++)
			{
				this.rows[row + i].VerticalOffset -= this.rows[row].RelationHeight;
			}
			this.rows[row].height -= this.rows[row].RelationHeight;
			this.rows[row].RelationHeight = 0;
			base.ResumeLayout(false);
			this.CalcAreasAndInvalidate();
		}

		/// <summary>Informs the <see cref="T:System.Windows.Forms.DataGrid" /> control when the user begins to edit a column using the specified control.</summary>
		/// <param name="editingControl">The <see cref="T:System.Windows.Forms.Control" /> used to edit the column. </param>
		// Token: 0x06000C4B RID: 3147 RVA: 0x000311B8 File Offset: 0x0002F3B8
		protected internal virtual void ColumnStartedEditing(Control editingControl)
		{
			this.ColumnStartedEditing(editingControl.Bounds);
		}

		/// <summary>Informs the <see cref="T:System.Windows.Forms.DataGrid" /> control when the user begins to edit the column at the specified location.</summary>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that defines the location of the edited column. </param>
		// Token: 0x06000C4C RID: 3148 RVA: 0x000311C8 File Offset: 0x0002F3C8
		protected internal virtual void ColumnStartedEditing(Rectangle bounds)
		{
			bool flag = !this.is_changing;
			this.is_changing = true;
			if (this.cursor_in_add_row && flag)
			{
				this.RecreateDataGridRows(true);
			}
			if (flag)
			{
				this.InvalidateRowHeader(this.CurrentRow);
			}
		}

		/// <summary>Constructs a new instance of the accessibility object for this control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control.ControlAccessibleObject" /> for this control.</returns>
		// Token: 0x06000C4D RID: 3149 RVA: 0x00031210 File Offset: 0x0002F410
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return base.CreateAccessibilityInstance();
		}

		/// <summary>Creates a new <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
		/// <returns>The new <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</returns>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to use for creating the grid column style. </param>
		// Token: 0x06000C4E RID: 3150 RVA: 0x00031218 File Offset: 0x0002F418
		protected virtual DataGridColumnStyle CreateGridColumn(PropertyDescriptor prop)
		{
			return this.CreateGridColumn(prop, false);
		}

		/// <summary>Creates a <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> using the specified <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
		/// <returns>The new <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</returns>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to use for creating the grid column style. </param>
		/// <param name="isDefault">true to set the column style as the default; otherwise, false. </param>
		// Token: 0x06000C4F RID: 3151 RVA: 0x00031224 File Offset: 0x0002F424
		[MonoTODO("Not implemented, will throw NotImplementedException")]
		protected virtual DataGridColumnStyle CreateGridColumn(PropertyDescriptor prop, bool isDefault)
		{
			throw new NotImplementedException();
		}

		/// <summary>Disposes of the resources (other than memory) used by the <see cref="T:System.Windows.Forms.DataGrid" />.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06000C50 RID: 3152 RVA: 0x0003122C File Offset: 0x0002F42C
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		/// <summary>Requests an end to an edit operation taking place on the <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
		/// <returns>true if the editing operation ceases; otherwise, false.</returns>
		/// <param name="gridColumn">The <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to cease editing. </param>
		/// <param name="rowNumber">The number of the row to cease editing. </param>
		/// <param name="shouldAbort">Set to true if the current operation should be stopped. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C51 RID: 3153 RVA: 0x00031238 File Offset: 0x0002F438
		public bool EndEdit(DataGridColumnStyle gridColumn, int rowNumber, bool shouldAbort)
		{
			if (shouldAbort || this._readonly || gridColumn.TableStyleReadOnly || gridColumn.ReadOnly)
			{
				gridColumn.Abort(rowNumber);
			}
			else
			{
				gridColumn.Commit(this.ListManager, rowNumber);
				gridColumn.ConcedeFocus();
			}
			if (this.is_editing || this.is_changing)
			{
				this.is_editing = false;
				this.is_changing = false;
				this.InvalidateRowHeader(rowNumber);
			}
			return true;
		}

		/// <summary>Ends the initialization of a <see cref="T:System.Windows.Forms.DataGrid" /> that is used on a form or used by another component. The initialization occurs at run time.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C52 RID: 3154 RVA: 0x000312B8 File Offset: 0x0002F4B8
		public void EndInit()
		{
			if (this.grid_style != null)
			{
				this.grid_style.DataGrid = this;
			}
		}

		/// <summary>Displays child relations, if any exist, for all rows or a specific row.</summary>
		/// <param name="row">The number of the row to expand. If set to -1, all rows are expanded. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C53 RID: 3155 RVA: 0x000312D4 File Offset: 0x0002F4D4
		public void Expand(int row)
		{
			if (this.rows[row].IsExpanded)
			{
				return;
			}
			this.rows[row].IsExpanded = true;
			string[] relations = this.CurrentTableStyle.Relations;
			StringBuilder stringBuilder = new StringBuilder(string.Empty);
			for (int i = 0; i < relations.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append("\n");
				}
				stringBuilder.Append(relations[i]);
			}
			string text = stringBuilder.ToString();
			SizeF sizeF = TextRenderer.MeasureString(text, this.LinkFont);
			this.rows[row].relation_area = new Rectangle(this.cells_area.X + 1, 0, (int)sizeF.Width + 4, this.Font.Height * relations.Length);
			for (int i = 1; i < this.rows.Length - row; i++)
			{
				this.rows[row + i].VerticalOffset += this.rows[row].relation_area.Height;
			}
			this.rows[row].height += this.rows[row].relation_area.Height;
			this.rows[row].RelationHeight = this.rows[row].relation_area.Height;
			this.CalcAreasAndInvalidate();
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Rectangle" /> of the cell specified by <see cref="T:System.Windows.Forms.DataGridCell" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that defines the current cell's corners.</returns>
		/// <param name="dgc">The <see cref="T:System.Windows.Forms.DataGridCell" /> to look up. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C54 RID: 3156 RVA: 0x00031428 File Offset: 0x0002F628
		public Rectangle GetCellBounds(DataGridCell dgc)
		{
			return this.GetCellBounds(dgc.RowNumber, dgc.ColumnNumber);
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Rectangle" /> of the cell specified by row and column number.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that defines the current cell's corners.</returns>
		/// <param name="row">The number of the cell's row. </param>
		/// <param name="col">The number of the cell's column. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C55 RID: 3157 RVA: 0x00031440 File Offset: 0x0002F640
		public Rectangle GetCellBounds(int row, int col)
		{
			Rectangle rectangle = default(Rectangle);
			rectangle.Width = this.CurrentTableStyle.GridColumnStyles[col].Width;
			rectangle.Height = this.rows[row].Height - this.rows[row].RelationHeight;
			rectangle.Y = this.cells_area.Y + this.rows[row].VerticalOffset - this.rows[this.FirstVisibleRow].VerticalOffset;
			int columnStartingPixel = this.GetColumnStartingPixel(col);
			rectangle.X = this.cells_area.X + columnStartingPixel - this.horiz_pixeloffset;
			return rectangle;
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Rectangle" /> that specifies the four corners of the selected cell.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that defines the current cell's corners.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C56 RID: 3158 RVA: 0x000314EC File Offset: 0x0002F6EC
		public Rectangle GetCurrentCellBounds()
		{
			return this.GetCellBounds(this.current_cell.RowNumber, this.current_cell.ColumnNumber);
		}

		/// <summary>Gets the string that is the delimiter between columns when row contents are copied to the Clipboard.</summary>
		/// <returns>The string value "\t", which represents a tab used to separate columns in a row. </returns>
		// Token: 0x06000C57 RID: 3159 RVA: 0x0003150C File Offset: 0x0002F70C
		protected virtual string GetOutputTextDelimiter()
		{
			return string.Empty;
		}

		/// <summary>Listens for the scroll event of the horizontal scroll bar.</summary>
		/// <param name="sender">An <see cref="T:System.Object" /> that contains data about the control. </param>
		/// <param name="se">A <see cref="T:System.Windows.Forms.ScrollEventArgs" /> that contains the event data. </param>
		// Token: 0x06000C58 RID: 3160 RVA: 0x00031514 File Offset: 0x0002F714
		protected virtual void GridHScrolled(object sender, ScrollEventArgs se)
		{
			if (se.NewValue == this.horiz_pixeloffset || se.Type == ScrollEventType.EndScroll)
			{
				return;
			}
			this.ScrollToColumnInPixels(se.NewValue);
		}

		/// <summary>Listens for the scroll event of the vertical scroll bar.</summary>
		/// <param name="sender">An <see cref="T:System.Object" /> that contains data about the control. </param>
		/// <param name="se">A <see cref="T:System.Windows.Forms.ScrollEventArgs" /> that contains the event data. </param>
		// Token: 0x06000C59 RID: 3161 RVA: 0x0003154C File Offset: 0x0002F74C
		protected virtual void GridVScrolled(object sender, ScrollEventArgs se)
		{
			int num = this.first_visible_row;
			this.first_visible_row = se.NewValue;
			if (this.first_visible_row == num)
			{
				return;
			}
			this.UpdateVisibleRowCount();
			if (this.first_visible_row == num)
			{
				return;
			}
			this.ScrollToRow(num, this.first_visible_row);
		}

		/// <summary>Gets information, such as row and column number of a clicked point on the grid, about the grid using a specific <see cref="T:System.Drawing.Point" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGrid.HitTestInfo" /> that contains specific information about the grid.</returns>
		/// <param name="position">A <see cref="T:System.Drawing.Point" /> that represents single x,y coordinate. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C5A RID: 3162 RVA: 0x0003159C File Offset: 0x0002F79C
		public DataGrid.HitTestInfo HitTest(Point position)
		{
			return this.HitTest(position.X, position.Y);
		}

		/// <summary>Gets information, such as row and column number of a clicked point on the grid, using the x and y coordinate passed to the method.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGrid.HitTestInfo" /> that contains information about the clicked part of the grid.</returns>
		/// <param name="x">The horizontal position of the coordinate. </param>
		/// <param name="y">The vertical position of the coordinate. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C5B RID: 3163 RVA: 0x000315B4 File Offset: 0x0002F7B4
		public DataGrid.HitTestInfo HitTest(int x, int y)
		{
			if (this.column_headers_area.Contains(x, y))
			{
				int num = x + this.horiz_pixeloffset;
				int num3;
				int num2 = this.FromPixelToColumn(num, out num3);
				if (num2 == -1)
				{
					return new DataGrid.HitTestInfo(-1, -1, DataGrid.HitTestType.None);
				}
				if (num3 + this.CurrentTableStyle.GridColumnStyles[num2].Width - num < 5 && num2 < this.CurrentTableStyle.GridColumnStyles.Count)
				{
					return new DataGrid.HitTestInfo(-1, num2, DataGrid.HitTestType.ColumnResize);
				}
				return new DataGrid.HitTestInfo(-1, num2, DataGrid.HitTestType.ColumnHeader);
			}
			else
			{
				if (this.row_headers_area.Contains(x, y))
				{
					int num4 = this.FirstVisibleRow + this.VisibleRowCount;
					int i = this.FirstVisibleRow;
					while (i < num4)
					{
						int num5 = this.cells_area.Y + this.rows[i].VerticalOffset - this.rows[this.FirstVisibleRow].VerticalOffset;
						if (y <= num5 + this.rows[i].Height)
						{
							if (num5 + this.rows[i].Height - y < 3)
							{
								return new DataGrid.HitTestInfo(i, -1, DataGrid.HitTestType.RowResize);
							}
							return new DataGrid.HitTestInfo(i, -1, DataGrid.HitTestType.RowHeader);
						}
						else
						{
							i++;
						}
					}
				}
				if (this.caption_area.Contains(x, y))
				{
					return new DataGrid.HitTestInfo(-1, -1, DataGrid.HitTestType.Caption);
				}
				if (this.parent_rows.Contains(x, y))
				{
					return new DataGrid.HitTestInfo(-1, -1, DataGrid.HitTestType.ParentRows);
				}
				int num6 = this.FirstVisibleRow + this.VisibleRowCount;
				for (int j = this.FirstVisibleRow; j < num6; j++)
				{
					int num7 = this.cells_area.Y + this.rows[j].VerticalOffset - this.rows[this.FirstVisibleRow].VerticalOffset;
					if (y <= num7 + this.rows[j].Height)
					{
						int num8 = this.first_visible_column + this.visible_column_count;
						if (num8 > 0)
						{
							for (int k = this.first_visible_column; k < num8; k++)
							{
								if (this.CurrentTableStyle.GridColumnStyles[k].bound)
								{
									int columnStartingPixel = this.GetColumnStartingPixel(k);
									int num9 = this.cells_area.X + columnStartingPixel - this.horiz_pixeloffset;
									int width = this.CurrentTableStyle.GridColumnStyles[k].Width;
									if (x <= num9 + width)
									{
										return new DataGrid.HitTestInfo(j, k, DataGrid.HitTestType.Cell);
									}
								}
							}
						}
						else if (this.CurrentTableStyle.HasRelations && x < this.rows[j].relation_area.X + this.rows[j].relation_area.Width)
						{
							return new DataGrid.HitTestInfo(j, 0, DataGrid.HitTestType.Cell);
						}
						break;
					}
				}
				return new DataGrid.HitTestInfo();
			}
		}

		/// <summary>Gets a value that indicates whether the node of a specified row is expanded or collapsed.</summary>
		/// <returns>true if the node is expanded; otherwise, false.</returns>
		/// <param name="rowNumber">The number of the row in question. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C5C RID: 3164 RVA: 0x00031890 File Offset: 0x0002FA90
		public bool IsExpanded(int rowNumber)
		{
			return this.rows[rowNumber].IsExpanded;
		}

		/// <summary>Gets a value indicating whether a specified row is selected.</summary>
		/// <returns>true if the row is selected; otherwise, false.</returns>
		/// <param name="row">The number of the row you are interested in. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C5D RID: 3165 RVA: 0x000318A0 File Offset: 0x0002FAA0
		public bool IsSelected(int row)
		{
			return this.rows[row].IsSelected;
		}

		/// <summary>Navigates back to the table previously displayed in the grid.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C5E RID: 3166 RVA: 0x000318B0 File Offset: 0x0002FAB0
		public void NavigateBack()
		{
			if (this.data_source_stack.Count == 0)
			{
				return;
			}
			DataGridDataSource dataGridDataSource = (DataGridDataSource)this.data_source_stack.Pop();
			this.list_manager = dataGridDataSource.list_manager;
			this.rows = dataGridDataSource.Rows;
			this.selected_rows = dataGridDataSource.SelectedRows;
			this.selection_start = dataGridDataSource.SelectionStart;
			this.SetDataSource(dataGridDataSource.data_source, dataGridDataSource.data_member);
			this.CurrentCell = dataGridDataSource.current;
		}

		/// <summary>Navigates to the table specified by row and relation name.</summary>
		/// <param name="rowNumber">The number of the row to navigate to. </param>
		/// <param name="relationName">The name of the child relation to navigate to. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C5F RID: 3167 RVA: 0x00031930 File Offset: 0x0002FB30
		public void NavigateTo(int rowNumber, string relationName)
		{
			if (!this.allow_navigation)
			{
				return;
			}
			DataGridDataSource dataGridDataSource = new DataGridDataSource(this, this.list_manager, this.datasource, this.datamember, this.list_manager.Current, this.CurrentCell);
			dataGridDataSource.Rows = this.rows;
			dataGridDataSource.SelectedRows = this.selected_rows;
			dataGridDataSource.SelectionStart = this.selection_start;
			this.data_source_stack.Push(dataGridDataSource);
			this.rows = null;
			this.selected_rows = new Hashtable();
			this.selection_start = -1;
			this.DataMember = string.Format("{0}.{1}", this.DataMember, relationName);
			this.OnDataSourceChanged(EventArgs.Empty);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGrid.AllowNavigationChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000C60 RID: 3168 RVA: 0x000319E0 File Offset: 0x0002FBE0
		protected virtual void OnAllowNavigationChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGrid.AllowNavigationChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Listens for the caption's back button clicked event.</summary>
		/// <param name="sender">An <see cref="T:System.Object" /> that contains data about the control. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains data about the event. </param>
		// Token: 0x06000C61 RID: 3169 RVA: 0x00031A14 File Offset: 0x0002FC14
		protected void OnBackButtonClicked(object sender, EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGrid.BackButtonClickEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000C62 RID: 3170 RVA: 0x00031A48 File Offset: 0x0002FC48
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGrid.BackgroundColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000C63 RID: 3171 RVA: 0x00031A54 File Offset: 0x0002FC54
		protected virtual void OnBackgroundColorChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGrid.BackgroundColorChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BindingContextChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000C64 RID: 3172 RVA: 0x00031A88 File Offset: 0x0002FC88
		protected override void OnBindingContextChanged(EventArgs e)
		{
			base.OnBindingContextChanged(e);
			this.SetDataSource(this.datasource, this.datamember);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGrid.BorderStyleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000C65 RID: 3173 RVA: 0x00031AA4 File Offset: 0x0002FCA4
		protected virtual void OnBorderStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGrid.BorderStyleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGrid.CaptionVisibleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000C66 RID: 3174 RVA: 0x00031AD8 File Offset: 0x0002FCD8
		protected virtual void OnCaptionVisibleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGrid.CaptionVisibleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGrid.CurrentCellChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000C67 RID: 3175 RVA: 0x00031B0C File Offset: 0x0002FD0C
		protected virtual void OnCurrentCellChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGrid.CurrentCellChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGrid.DataSourceChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000C68 RID: 3176 RVA: 0x00031B40 File Offset: 0x0002FD40
		protected virtual void OnDataSourceChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGrid.DataSourceChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Enter" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000C69 RID: 3177 RVA: 0x00031B74 File Offset: 0x0002FD74
		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);
			this.Edit();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGrid.FlatModeChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000C6A RID: 3178 RVA: 0x00031B84 File Offset: 0x0002FD84
		protected virtual void OnFlatModeChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGrid.FlatModeChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000C6B RID: 3179 RVA: 0x00031BB8 File Offset: 0x0002FDB8
		protected override void OnFontChanged(EventArgs e)
		{
			this.CalcGridAreas();
			base.OnFontChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ForeColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000C6C RID: 3180 RVA: 0x00031BC8 File Offset: 0x0002FDC8
		protected override void OnForeColorChanged(EventArgs e)
		{
			base.OnForeColorChanged(e);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.CreateHandle" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000C6D RID: 3181 RVA: 0x00031BD4 File Offset: 0x0002FDD4
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			this.SetDataSource(this.datasource, this.datamember);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.DestroyHandle" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> containing the event data. </param>
		// Token: 0x06000C6E RID: 3182 RVA: 0x00031BF0 File Offset: 0x0002FDF0
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.</summary>
		/// <param name="ke">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that provides data about the <see cref="M:System.Windows.Forms.Control.OnKeyDown(System.Windows.Forms.KeyEventArgs)" /> event. </param>
		// Token: 0x06000C6F RID: 3183 RVA: 0x00031BFC File Offset: 0x0002FDFC
		protected override void OnKeyDown(KeyEventArgs ke)
		{
			base.OnKeyDown(ke);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.</summary>
		/// <param name="kpe">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains data about the <see cref="M:System.Windows.Forms.Control.OnKeyPress(System.Windows.Forms.KeyPressEventArgs)" /> event </param>
		// Token: 0x06000C70 RID: 3184 RVA: 0x00031C08 File Offset: 0x0002FE08
		protected override void OnKeyPress(KeyPressEventArgs kpe)
		{
			base.OnKeyPress(kpe);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event, which repositions controls and updates scroll bars.</summary>
		/// <param name="levent">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data. </param>
		// Token: 0x06000C71 RID: 3185 RVA: 0x00031C14 File Offset: 0x0002FE14
		protected override void OnLayout(LayoutEventArgs levent)
		{
			base.OnLayout(levent);
			this.CalcAreasAndInvalidate();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Leave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000C72 RID: 3186 RVA: 0x00031C24 File Offset: 0x0002FE24
		protected override void OnLeave(EventArgs e)
		{
			base.OnLeave(e);
			this.EndEdit();
			if (this.cursor_in_add_row)
			{
				this.ListManager.CancelCurrentEdit();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains data about the <see cref="M:System.Windows.Forms.Control.OnMouseDown(System.Windows.Forms.MouseEventArgs)" /> event. </param>
		// Token: 0x06000C73 RID: 3187 RVA: 0x00031C4C File Offset: 0x0002FE4C
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			bool flag = (Control.ModifierKeys & Keys.Control) != Keys.None;
			bool flag2 = (Control.ModifierKeys & Keys.Shift) != Keys.None;
			DataGrid.HitTestInfo hitTestInfo = this.HitTest(e.X, e.Y);
			DataGrid.HitTestType type = hitTestInfo.Type;
			switch (type)
			{
			case DataGrid.HitTestType.Cell:
				if (hitTestInfo.Row >= 0 && hitTestInfo.Column >= 0)
				{
					if (this.rows[hitTestInfo.Row].IsExpanded)
					{
						Rectangle relation_area = this.rows[hitTestInfo.Row].relation_area;
						relation_area.Y = this.rows[hitTestInfo.Row].VerticalOffset + this.cells_area.Y + this.rows[hitTestInfo.Row].Height - this.rows[hitTestInfo.Row].RelationHeight;
						if (relation_area.Contains(e.X, e.Y))
						{
							int num = e.Y - relation_area.Y;
							this.NavigateTo(hitTestInfo.Row, this.CurrentTableStyle.Relations[num / this.LinkFont.Height]);
							return;
						}
					}
					DataGridCell dataGridCell = new DataGridCell(hitTestInfo.Row, hitTestInfo.Column);
					if (!dataGridCell.Equals(this.current_cell) || !this.is_editing)
					{
						this.ResetSelection();
						this.CurrentCell = dataGridCell;
						this.Edit();
					}
					else
					{
						this.CurrentTableStyle.GridColumnStyles[hitTestInfo.Column].OnMouseDown(e, hitTestInfo.Row, hitTestInfo.Column);
					}
				}
				break;
			case DataGrid.HitTestType.ColumnHeader:
				if (this.CurrentTableStyle.GridColumnStyles.Count != 0)
				{
					if (this.AllowSorting)
					{
						if (this.ListManager.List is IBindingList)
						{
							if (this.ListManager.Count == 0)
							{
								return;
							}
							ListSortDirection listSortDirection = 0;
							PropertyDescriptor propertyDescriptor = this.CurrentTableStyle.GridColumnStyles[hitTestInfo.Column].PropertyDescriptor;
							IBindingList bindingList = (IBindingList)this.ListManager.List;
							if (bindingList.SortProperty != null)
							{
								this.CurrentTableStyle.GridColumnStyles[bindingList.SortProperty].ArrowDrawingMode = DataGridColumnStyle.ArrowDrawing.No;
							}
							if (propertyDescriptor == bindingList.SortProperty && bindingList.SortDirection == null)
							{
								listSortDirection = 1;
							}
							this.CurrentTableStyle.GridColumnStyles[hitTestInfo.Column].ArrowDrawingMode = ((listSortDirection != null) ? DataGridColumnStyle.ArrowDrawing.Descending : DataGridColumnStyle.ArrowDrawing.Ascending);
							bindingList.ApplySort(propertyDescriptor, listSortDirection);
							this.Refresh();
							if (this.is_editing)
							{
								this.InvalidateColumn(this.CurrentTableStyle.GridColumnStyles[this.CurrentColumn]);
							}
						}
					}
				}
				break;
			default:
				if (type != DataGrid.HitTestType.RowResize)
				{
					if (type == DataGrid.HitTestType.Caption)
					{
						if (this.back_button_rect.Contains(e.X, e.Y))
						{
							this.back_button_active = true;
							base.Invalidate(this.back_button_rect);
						}
						if (this.parent_rows_button_rect.Contains(e.X, e.Y))
						{
							this.parent_rows_button_active = true;
							base.Invalidate(this.parent_rows_button_rect);
						}
					}
				}
				else if (e.Clicks == 2)
				{
					this.EndEdit();
					this.RowResize(hitTestInfo.Row);
				}
				else
				{
					this.resize_row = hitTestInfo.Row;
					this.row_resize_active = true;
					this.resize_row_y = e.Y;
					this.resize_row_height_delta = 0;
					this.EndEdit();
					this.DrawResizeLineHoriz(this.resize_row_y);
				}
				break;
			case DataGrid.HitTestType.RowHeader:
			{
				bool flag3 = false;
				if (this.CurrentTableStyle.HasRelations && e.X > this.row_headers_area.X + this.row_headers_area.Width / 2)
				{
					if (this.IsExpanded(hitTestInfo.Row))
					{
						this.Collapse(hitTestInfo.Row);
					}
					else
					{
						this.Expand(hitTestInfo.Row);
					}
					flag3 = true;
				}
				this.CancelEditing();
				this.CurrentRow = hitTestInfo.Row;
				if (!flag && !flag2 && !flag3)
				{
					this.ResetSelection();
				}
				if ((flag2 || flag3) && this.selection_start != -1)
				{
					this.ShiftSelection(hitTestInfo.Row);
				}
				else
				{
					this.selection_start = hitTestInfo.Row;
					this.Select(hitTestInfo.Row);
				}
				this.OnRowHeaderClick(EventArgs.Empty);
				break;
			}
			case DataGrid.HitTestType.ColumnResize:
				if (e.Clicks == 2)
				{
					this.EndEdit();
					this.ColumnResize(hitTestInfo.Column);
				}
				else
				{
					this.resize_column = hitTestInfo.Column;
					this.column_resize_active = true;
					this.resize_column_x = e.X;
					this.resize_column_width_delta = 0;
					this.EndEdit();
					this.DrawResizeLineVert(this.resize_column_x);
				}
				break;
			}
		}

		/// <summary>Creates the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains data about the <see cref="M:System.Windows.Forms.Control.OnMouseLeave(System.EventArgs)" /> event. </param>
		// Token: 0x06000C74 RID: 3188 RVA: 0x0003217C File Offset: 0x0003037C
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains data about the <see cref="M:System.Windows.Forms.Control.OnMouseMove(System.Windows.Forms.MouseEventArgs)" /> event. </param>
		// Token: 0x06000C75 RID: 3189 RVA: 0x00032188 File Offset: 0x00030388
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (this.column_resize_active)
			{
				this.DrawResizeLineVert(this.resize_column_x + this.resize_column_width_delta);
				this.resize_column_width_delta = e.X - this.resize_column_x;
				this.DrawResizeLineVert(this.resize_column_x + this.resize_column_width_delta);
				return;
			}
			if (this.row_resize_active)
			{
				this.DrawResizeLineHoriz(this.resize_row_y + this.resize_row_height_delta);
				this.resize_row_height_delta = e.Y - this.resize_row_y;
				this.DrawResizeLineHoriz(this.resize_row_y + this.resize_row_height_delta);
				return;
			}
			DataGrid.HitTestInfo hitTestInfo = this.HitTest(e.X, e.Y);
			DataGrid.HitTestType type = hitTestInfo.Type;
			switch (type)
			{
			case DataGrid.HitTestType.Cell:
				if (this.rows[hitTestInfo.Row].IsExpanded)
				{
					Rectangle relation_area = this.rows[hitTestInfo.Row].relation_area;
					relation_area.Y = this.rows[hitTestInfo.Row].VerticalOffset + this.cells_area.Y + this.rows[hitTestInfo.Row].Height - this.rows[hitTestInfo.Row].RelationHeight;
					if (relation_area.Contains(e.X, e.Y))
					{
						this.Cursor = Cursors.Hand;
						break;
					}
				}
				this.Cursor = Cursors.Default;
				break;
			default:
				if (type != DataGrid.HitTestType.ColumnResize)
				{
					if (type != DataGrid.HitTestType.RowResize)
					{
						if (type != DataGrid.HitTestType.Caption)
						{
							this.Cursor = Cursors.Default;
						}
						else
						{
							this.Cursor = Cursors.Default;
							if (this.back_button_rect.Contains(e.X, e.Y))
							{
								if (!this.back_button_mouseover)
								{
									base.Invalidate(this.back_button_rect);
								}
								this.back_button_mouseover = true;
							}
							else if (this.back_button_mouseover)
							{
								base.Invalidate(this.back_button_rect);
								this.back_button_mouseover = false;
							}
							if (this.parent_rows_button_rect.Contains(e.X, e.Y))
							{
								if (this.parent_rows_button_mouseover)
								{
									base.Invalidate(this.parent_rows_button_rect);
								}
								this.parent_rows_button_mouseover = true;
							}
							else if (this.parent_rows_button_mouseover)
							{
								base.Invalidate(this.parent_rows_button_rect);
								this.parent_rows_button_mouseover = false;
							}
						}
					}
					else
					{
						this.Cursor = Cursors.HSplit;
					}
				}
				else
				{
					this.Cursor = Cursors.VSplit;
				}
				break;
			case DataGrid.HitTestType.RowHeader:
				if (e.Button == MouseButtons.Left)
				{
					this.ShiftSelection(hitTestInfo.Row);
				}
				this.Cursor = Cursors.Default;
				break;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains data about the <see cref="M:System.Windows.Forms.Control.OnMouseUp(System.Windows.Forms.MouseEventArgs)" /> event. </param>
		// Token: 0x06000C76 RID: 3190 RVA: 0x00032448 File Offset: 0x00030648
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (this.column_resize_active)
			{
				this.column_resize_active = false;
				if (this.resize_column_width_delta + this.CurrentTableStyle.GridColumnStyles[this.resize_column].Width < 0)
				{
					this.resize_column_width_delta = -this.CurrentTableStyle.GridColumnStyles[this.resize_column].Width;
				}
				this.CurrentTableStyle.GridColumnStyles[this.resize_column].Width += this.resize_column_width_delta;
				this.width_of_all_columns += this.resize_column_width_delta;
				this.Edit();
				base.Invalidate();
			}
			else if (this.row_resize_active)
			{
				this.row_resize_active = false;
				if (this.resize_row_height_delta + this.rows[this.resize_row].Height < 0)
				{
					this.resize_row_height_delta = -this.rows[this.resize_row].Height;
				}
				this.rows[this.resize_row].height = this.rows[this.resize_row].Height + this.resize_row_height_delta;
				for (int i = this.resize_row + 1; i < this.rows.Length; i++)
				{
					this.rows[i].VerticalOffset += this.resize_row_height_delta;
				}
				this.Edit();
				this.CalcAreasAndInvalidate();
			}
			else if (this.back_button_active)
			{
				if (this.back_button_rect.Contains(e.X, e.Y))
				{
					base.Invalidate(this.back_button_rect);
					this.NavigateBack();
					this.OnBackButtonClicked(this, EventArgs.Empty);
				}
				this.back_button_active = false;
			}
			else if (this.parent_rows_button_active)
			{
				if (this.parent_rows_button_rect.Contains(e.X, e.Y))
				{
					base.Invalidate(this.parent_rows_button_rect);
					this.ParentRowsVisible = !this.ParentRowsVisible;
					this.OnShowParentDetailsButtonClicked(this, EventArgs.Empty);
				}
				this.parent_rows_button_active = false;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseWheel" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains data about the <see cref="M:System.Windows.Forms.Control.OnMouseUp(System.Windows.Forms.MouseEventArgs)" /> event. </param>
		// Token: 0x06000C77 RID: 3191 RVA: 0x0003266C File Offset: 0x0003086C
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);
			bool flag = (Control.ModifierKeys & Keys.Control) != Keys.None;
			if (flag)
			{
				if (!this.horiz_scrollbar.Visible)
				{
					return;
				}
				int num;
				if (e.Delta > 0)
				{
					num = Math.Max(this.horiz_scrollbar.Minimum, this.horiz_scrollbar.Value - this.horiz_scrollbar.LargeChange);
				}
				else
				{
					num = Math.Min(this.horiz_scrollbar.Maximum - this.horiz_scrollbar.LargeChange + 1, this.horiz_scrollbar.Value + this.horiz_scrollbar.LargeChange);
				}
				this.GridHScrolled(this, new ScrollEventArgs(ScrollEventType.ThumbPosition, num));
				this.horiz_scrollbar.Value = num;
			}
			else
			{
				if (!this.vert_scrollbar.Visible)
				{
					return;
				}
				int num;
				if (e.Delta > 0)
				{
					num = Math.Max(this.vert_scrollbar.Minimum, this.vert_scrollbar.Value - this.vert_scrollbar.LargeChange);
				}
				else
				{
					num = Math.Min(this.vert_scrollbar.Maximum - this.vert_scrollbar.LargeChange + 1, this.vert_scrollbar.Value + this.vert_scrollbar.LargeChange);
				}
				this.GridVScrolled(this, new ScrollEventArgs(ScrollEventType.ThumbPosition, num));
				this.vert_scrollbar.Value = num;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGrid.Navigate" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.NavigateEventArgs" /> that contains the event data. </param>
		// Token: 0x06000C78 RID: 3192 RVA: 0x000327D4 File Offset: 0x000309D4
		protected void OnNavigate(NavigateEventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGrid.NavigateEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
		/// <param name="pe">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> which contains data about the event. </param>
		// Token: 0x06000C79 RID: 3193 RVA: 0x00032808 File Offset: 0x00030A08
		protected override void OnPaint(PaintEventArgs pe)
		{
			ThemeEngine.Current.DataGridPaint(pe, this);
		}

		/// <summary>Overrides <see cref="M:System.Windows.Forms.Control.OnPaintBackground(System.Windows.Forms.PaintEventArgs)" /> to prevent painting the background of the <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
		/// <param name="ebe">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains information about the control to paint. </param>
		// Token: 0x06000C7A RID: 3194 RVA: 0x00032818 File Offset: 0x00030A18
		protected override void OnPaintBackground(PaintEventArgs ebe)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGrid.ParentRowsLabelStyleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000C7B RID: 3195 RVA: 0x0003281C File Offset: 0x00030A1C
		protected virtual void OnParentRowsLabelStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGrid.ParentRowsLabelStyleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGrid.ParentRowsVisibleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000C7C RID: 3196 RVA: 0x00032850 File Offset: 0x00030A50
		protected virtual void OnParentRowsVisibleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGrid.ParentRowsVisibleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGrid.ReadOnlyChanged" /> event </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000C7D RID: 3197 RVA: 0x00032884 File Offset: 0x00030A84
		protected virtual void OnReadOnlyChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGrid.ReadOnlyChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000C7E RID: 3198 RVA: 0x000328B8 File Offset: 0x00030AB8
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGrid.RowHeaderClick" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000C7F RID: 3199 RVA: 0x000328C4 File Offset: 0x00030AC4
		protected void OnRowHeaderClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGrid.RowHeaderClickEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGrid.Scroll" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000C80 RID: 3200 RVA: 0x000328F8 File Offset: 0x00030AF8
		protected void OnScroll(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGrid.ScrollEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGrid.ShowParentDetailsButtonClick" /> event.</summary>
		/// <param name="sender">The source of the event. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000C81 RID: 3201 RVA: 0x0003292C File Offset: 0x00030B2C
		protected void OnShowParentDetailsButtonClicked(object sender, EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGrid.ShowParentDetailsButtonClickEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Gets or sets a value that indicates whether a key should be processed further.</summary>
		/// <returns>true, the key should be processed; otherwise, false.</returns>
		/// <param name="keyData">A <see cref="T:System.Windows.Forms.Keys" /> that contains data about the pressed key. </param>
		// Token: 0x06000C82 RID: 3202 RVA: 0x00032960 File Offset: 0x00030B60
		protected override bool ProcessDialogKey(Keys keyData)
		{
			return this.ProcessGridKey(new KeyEventArgs(keyData));
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x00032970 File Offset: 0x00030B70
		private void UpdateSelectionAfterCursorMove(bool extend_selection)
		{
			if (extend_selection)
			{
				this.CancelEditing();
				this.ShiftSelection(this.CurrentRow);
			}
			else
			{
				this.ResetSelection();
				this.selection_start = this.CurrentRow;
			}
		}

		/// <summary>Processes keys for grid navigation.</summary>
		/// <returns>true, if the key was processed; otherwise false.</returns>
		/// <param name="ke">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains data about the key up or key down event. </param>
		// Token: 0x06000C84 RID: 3204 RVA: 0x000329AC File Offset: 0x00030BAC
		protected bool ProcessGridKey(KeyEventArgs ke)
		{
			bool flag = (ke.Modifiers & Keys.Control) != Keys.None;
			bool flag2 = (ke.Modifiers & Keys.Shift) != Keys.None;
			Keys keyCode = ke.KeyCode;
			switch (keyCode)
			{
			case Keys.Escape:
				if (this.is_changing)
				{
					this.AbortEditing();
				}
				else
				{
					this.CancelEditing();
					if (this.cursor_in_add_row && this.CurrentRow > 0)
					{
						this.CurrentRow--;
					}
				}
				this.Edit();
				return true;
			default:
				if (keyCode == Keys.Tab)
				{
					if (flag2)
					{
						if (this.CurrentColumn > 0)
						{
							this.CurrentColumn--;
						}
						else if (this.CurrentRow > 0 && this.CurrentColumn == 0)
						{
							this.CurrentCell = new DataGridCell(this.CurrentRow - 1, this.CurrentTableStyle.GridColumnStyles.Count - 1);
						}
					}
					else if (this.CurrentColumn < this.CurrentTableStyle.GridColumnStyles.Count - 1)
					{
						this.CurrentColumn++;
					}
					else if (this.CurrentRow <= this.RowsCount && this.CurrentColumn == this.CurrentTableStyle.GridColumnStyles.Count - 1)
					{
						this.CurrentCell = new DataGridCell(this.CurrentRow + 1, 0);
					}
					this.UpdateSelectionAfterCursorMove(false);
					return true;
				}
				if (keyCode != Keys.Return)
				{
					return false;
				}
				if (this.is_changing)
				{
					this.CurrentRow++;
				}
				return true;
			case Keys.PageUp:
				if (this.CurrentRow > this.VLargeChange)
				{
					this.CurrentRow -= this.VLargeChange;
				}
				else
				{
					this.CurrentRow = 0;
				}
				this.UpdateSelectionAfterCursorMove(flag2);
				return true;
			case Keys.PageDown:
				if (this.CurrentRow < this.RowsCount - this.VLargeChange)
				{
					this.CurrentRow += this.VLargeChange;
				}
				else
				{
					this.CurrentRow = this.RowsCount - 1;
				}
				this.UpdateSelectionAfterCursorMove(flag2);
				return true;
			case Keys.End:
				if (flag)
				{
					this.CurrentCell = new DataGridCell(this.RowsCount - 1, this.CurrentTableStyle.GridColumnStyles.Count - 1);
				}
				else
				{
					this.CurrentColumn = this.CurrentTableStyle.GridColumnStyles.Count - 1;
				}
				this.UpdateSelectionAfterCursorMove(flag && flag2);
				return true;
			case Keys.Home:
				if (flag)
				{
					this.CurrentCell = new DataGridCell(0, 0);
				}
				else
				{
					this.CurrentColumn = 0;
				}
				this.UpdateSelectionAfterCursorMove(flag && flag2);
				return true;
			case Keys.Left:
				if (flag)
				{
					this.CurrentColumn = 0;
				}
				else if (this.current_cell.ColumnNumber > 0)
				{
					this.CurrentColumn--;
				}
				else if (this.CurrentRow > 0)
				{
					this.CurrentCell = new DataGridCell(this.CurrentRow - 1, this.CurrentTableStyle.GridColumnStyles.Count - 1);
				}
				this.UpdateSelectionAfterCursorMove(false);
				return true;
			case Keys.Up:
				if (flag)
				{
					this.CurrentRow = 0;
				}
				else if (this.CurrentRow > 0)
				{
					this.CurrentRow--;
				}
				this.UpdateSelectionAfterCursorMove(flag2);
				return true;
			case Keys.Right:
				if (flag)
				{
					this.CurrentColumn = this.CurrentTableStyle.GridColumnStyles.Count - 1;
				}
				else if (this.CurrentColumn < this.CurrentTableStyle.GridColumnStyles.Count - 1)
				{
					this.CurrentColumn++;
				}
				else if (this.CurrentRow < this.RowsCount - 1 || (this.CurrentRow == this.RowsCount - 1 && !this.cursor_in_add_row))
				{
					this.CurrentCell = new DataGridCell(this.CurrentRow + 1, 0);
				}
				this.UpdateSelectionAfterCursorMove(false);
				return true;
			case Keys.Down:
				if (flag)
				{
					this.CurrentRow = this.RowsCount - 1;
				}
				else if (this.CurrentRow < this.RowsCount - 1)
				{
					this.CurrentRow++;
				}
				else if (this.CurrentRow == this.RowsCount - 1 && this.cursor_in_add_row && (this.add_row_changed || this.is_changing))
				{
					this.CurrentRow++;
				}
				else if (this.CurrentRow == this.RowsCount - 1 && !this.cursor_in_add_row && !flag2)
				{
					this.CurrentRow++;
				}
				this.UpdateSelectionAfterCursorMove(flag2);
				return true;
			case Keys.Delete:
				if (this.is_editing)
				{
					return false;
				}
				if (this.selected_rows.Keys.Count > 0)
				{
					int[] array = new int[this.selected_rows.Keys.Count];
					this.selected_rows.Keys.CopyTo(array, 0);
					for (int i = array.Length - 1; i >= 0; i--)
					{
						this.ListManager.RemoveAt(array[i]);
					}
					this.CalcAreasAndInvalidate();
				}
				return true;
			case Keys.D0:
				if (flag)
				{
					if (this.is_editing)
					{
						this.CurrentTableStyle.GridColumnStyles[this.CurrentColumn].EnterNullValue();
					}
					return true;
				}
				return false;
			}
		}

		/// <summary>Previews a keyboard message and returns a value indicating if the key was consumed.</summary>
		/// <returns>true, if the key was consumed; otherwise, false.</returns>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" /> that contains data about the event. The parameter is passed by reference. </param>
		// Token: 0x06000C85 RID: 3205 RVA: 0x00032F60 File Offset: 0x00031160
		protected override bool ProcessKeyPreview(ref Message m)
		{
			if (m.Msg == 256)
			{
				Keys keys = (Keys)m.WParam.ToInt32();
				KeyEventArgs keyEventArgs = new KeyEventArgs(keys);
				if (this.ProcessGridKey(keyEventArgs))
				{
					return true;
				}
				if (!this.is_editing)
				{
					this.Edit();
					this.InvalidateRow(this.current_cell.RowNumber);
					return true;
				}
			}
			return base.ProcessKeyPreview(ref m);
		}

		/// <summary>Gets a value indicating whether the Tab key should be processed.</summary>
		/// <returns>true if the TAB key should be processed; otherwise, false.</returns>
		/// <param name="keyData">A <see cref="T:System.Windows.Forms.Keys" /> that contains data about which the pressed key. </param>
		// Token: 0x06000C86 RID: 3206 RVA: 0x00032FCC File Offset: 0x000311CC
		protected bool ProcessTabKey(Keys keyData)
		{
			return false;
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGrid.AlternatingBackColor" /> property to its default color.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C87 RID: 3207 RVA: 0x00032FD0 File Offset: 0x000311D0
		public void ResetAlternatingBackColor()
		{
			this.grid_style.AlternatingBackColor = this.default_style.AlternatingBackColor;
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGrid.BackColor" /> property to its default value.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C88 RID: 3208 RVA: 0x00032FE8 File Offset: 0x000311E8
		public override void ResetBackColor()
		{
			this.grid_style.BackColor = this.default_style.BackColor;
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGrid.ForeColor" /> property to its default value.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C89 RID: 3209 RVA: 0x00033000 File Offset: 0x00031200
		public override void ResetForeColor()
		{
			this.grid_style.ForeColor = this.default_style.ForeColor;
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGrid.GridLineColor" /> property to its default value.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C8A RID: 3210 RVA: 0x00033018 File Offset: 0x00031218
		public void ResetGridLineColor()
		{
			this.grid_style.GridLineColor = this.default_style.GridLineColor;
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGrid.HeaderBackColor" /> property to its default value.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C8B RID: 3211 RVA: 0x00033030 File Offset: 0x00031230
		public void ResetHeaderBackColor()
		{
			this.grid_style.HeaderBackColor = this.default_style.HeaderBackColor;
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGrid.HeaderFont" /> property to its default value.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C8C RID: 3212 RVA: 0x00033048 File Offset: 0x00031248
		public void ResetHeaderFont()
		{
			this.grid_style.HeaderFont = null;
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGrid.HeaderForeColor" /> property to its default value.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C8D RID: 3213 RVA: 0x00033058 File Offset: 0x00031258
		public void ResetHeaderForeColor()
		{
			this.grid_style.HeaderForeColor = this.default_style.HeaderForeColor;
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGrid.LinkColor" /> property to its default value.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C8E RID: 3214 RVA: 0x00033070 File Offset: 0x00031270
		public void ResetLinkColor()
		{
			this.grid_style.LinkColor = this.default_style.LinkColor;
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGrid.LinkHoverColor" /> property to its default value.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C8F RID: 3215 RVA: 0x00033088 File Offset: 0x00031288
		public void ResetLinkHoverColor()
		{
			this.grid_style.LinkHoverColor = this.default_style.LinkHoverColor;
		}

		/// <summary>Turns off selection for all rows that are selected.</summary>
		// Token: 0x06000C90 RID: 3216 RVA: 0x000330A0 File Offset: 0x000312A0
		protected void ResetSelection()
		{
			this.InvalidateSelection();
			this.selected_rows.Clear();
			this.selection_start = -1;
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x000330BC File Offset: 0x000312BC
		private void InvalidateSelection()
		{
			foreach (object obj in this.selected_rows.Keys)
			{
				int num = (int)obj;
				this.rows[num].IsSelected = false;
				this.InvalidateRow(num);
			}
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGrid.SelectionBackColor" /> property to its default value.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C92 RID: 3218 RVA: 0x00033140 File Offset: 0x00031340
		public void ResetSelectionBackColor()
		{
			this.grid_style.SelectionBackColor = this.default_style.SelectionBackColor;
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGrid.SelectionForeColor" /> property to its default value.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C93 RID: 3219 RVA: 0x00033158 File Offset: 0x00031358
		public void ResetSelectionForeColor()
		{
			this.grid_style.SelectionForeColor = this.default_style.SelectionForeColor;
		}

		/// <summary>Selects a specified row.</summary>
		/// <param name="row">The index of the row to select. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C94 RID: 3220 RVA: 0x00033170 File Offset: 0x00031370
		public void Select(int row)
		{
			this.EndEdit();
			if (this.selected_rows.Count == 0)
			{
				this.selection_start = row;
			}
			bool isSelected = this.rows[row].IsSelected;
			this.selected_rows[row] = true;
			this.rows[row].IsSelected = true;
			this.InvalidateRow(row);
			if (!isSelected)
			{
				this.OnUIASelectionChangedEvent(new CollectionChangeEventArgs(1, row));
			}
		}

		/// <summary>Sets the <see cref="P:System.Windows.Forms.DataGrid.DataSource" /> and <see cref="P:System.Windows.Forms.DataGrid.DataMember" /> properties at run time.</summary>
		/// <param name="dataSource">The data source for the <see cref="T:System.Windows.Forms.DataGrid" /> control. </param>
		/// <param name="dataMember">The <see cref="P:System.Windows.Forms.DataGrid.DataMember" /> string that specifies the table to bind to within the object returned by the <see cref="P:System.Windows.Forms.DataGrid.DataSource" /> property. </param>
		/// <exception cref="T:System.ArgumentException">One or more of the arguments are invalid. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="dataSource" /> argument is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C95 RID: 3221 RVA: 0x000331EC File Offset: 0x000313EC
		public void SetDataBinding(object dataSource, string dataMember)
		{
			this.SetDataSource(dataSource, dataMember);
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGrid.AlternatingBackColor" /> property should be persisted.</summary>
		/// <returns>true if the property value has changed from its default; otherwise, false.</returns>
		// Token: 0x06000C96 RID: 3222 RVA: 0x000331F8 File Offset: 0x000313F8
		protected virtual bool ShouldSerializeAlternatingBackColor()
		{
			return this.grid_style.AlternatingBackColor != this.default_style.AlternatingBackColor;
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGrid.BackgroundColor" /> property should be persisted.</summary>
		/// <returns>true if the property value has changed from its default; otherwise, false.</returns>
		// Token: 0x06000C97 RID: 3223 RVA: 0x00033218 File Offset: 0x00031418
		protected virtual bool ShouldSerializeBackgroundColor()
		{
			return this.background_color != DataGrid.def_background_color;
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Windows.Forms.DataGrid.CaptionBackColor" /> property should be persisted.</summary>
		/// <returns>true if the property value has been changed from its default; otherwise, false.</returns>
		// Token: 0x06000C98 RID: 3224 RVA: 0x0003322C File Offset: 0x0003142C
		protected virtual bool ShouldSerializeCaptionBackColor()
		{
			return this.caption_backcolor != DataGrid.def_caption_backcolor;
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Windows.Forms.DataGrid.CaptionForeColor" /> property should be persisted.</summary>
		/// <returns>true if the property value has been changed from its default; otherwise, false.</returns>
		// Token: 0x06000C99 RID: 3225 RVA: 0x00033240 File Offset: 0x00031440
		protected virtual bool ShouldSerializeCaptionForeColor()
		{
			return this.caption_forecolor != DataGrid.def_caption_forecolor;
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGrid.GridLineColor" /> property should be persisted.</summary>
		/// <returns>true if the property value has changed from its default; otherwise, false.</returns>
		// Token: 0x06000C9A RID: 3226 RVA: 0x00033254 File Offset: 0x00031454
		protected virtual bool ShouldSerializeGridLineColor()
		{
			return this.grid_style.GridLineColor != this.default_style.GridLineColor;
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGrid.HeaderBackColor" /> property should be persisted.</summary>
		/// <returns>true if the property value has changed from its default; otherwise, false.</returns>
		// Token: 0x06000C9B RID: 3227 RVA: 0x00033274 File Offset: 0x00031474
		protected virtual bool ShouldSerializeHeaderBackColor()
		{
			return this.grid_style.HeaderBackColor != this.default_style.HeaderBackColor;
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGrid.HeaderFont" /> property should be persisted.</summary>
		/// <returns>true if the property value has changed from its default; otherwise, false.</returns>
		// Token: 0x06000C9C RID: 3228 RVA: 0x00033294 File Offset: 0x00031494
		protected bool ShouldSerializeHeaderFont()
		{
			return this.grid_style.HeaderFont != this.default_style.HeaderFont;
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGrid.HeaderForeColor" /> property should be persisted.</summary>
		/// <returns>true if the property value has changed from its default; otherwise, false.</returns>
		// Token: 0x06000C9D RID: 3229 RVA: 0x000332B4 File Offset: 0x000314B4
		protected virtual bool ShouldSerializeHeaderForeColor()
		{
			return this.grid_style.HeaderForeColor != this.default_style.HeaderForeColor;
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGrid.LinkHoverColor" /> property should be persisted.</summary>
		/// <returns>true if the property value has changed from its default; otherwise, false.</returns>
		// Token: 0x06000C9E RID: 3230 RVA: 0x000332D4 File Offset: 0x000314D4
		protected virtual bool ShouldSerializeLinkHoverColor()
		{
			return this.grid_style.LinkHoverColor != this.grid_style.LinkHoverColor;
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGrid.ParentRowsBackColor" /> property should be persisted.</summary>
		/// <returns>true if the property value has been changed from its default; otherwise, false.</returns>
		// Token: 0x06000C9F RID: 3231 RVA: 0x000332F4 File Offset: 0x000314F4
		protected virtual bool ShouldSerializeParentRowsBackColor()
		{
			return this.parent_rows_backcolor != DataGrid.def_parent_rows_backcolor;
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGrid.ParentRowsForeColor" /> property should be persisted.</summary>
		/// <returns>true if the property value has been changed from its default; otherwise, false.</returns>
		// Token: 0x06000CA0 RID: 3232 RVA: 0x00033308 File Offset: 0x00031508
		protected virtual bool ShouldSerializeParentRowsForeColor()
		{
			return this.parent_rows_backcolor != DataGrid.def_parent_rows_backcolor;
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGrid.PreferredRowHeight" /> property should be persisted.</summary>
		/// <returns>true if the property value has changed from its default; otherwise, false.</returns>
		// Token: 0x06000CA1 RID: 3233 RVA: 0x0003331C File Offset: 0x0003151C
		protected bool ShouldSerializePreferredRowHeight()
		{
			return this.grid_style.PreferredRowHeight != this.default_style.PreferredRowHeight;
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGrid.SelectionBackColor" /> property should be persisted.</summary>
		/// <returns>true if the property value has changed from its default; otherwise, false.</returns>
		// Token: 0x06000CA2 RID: 3234 RVA: 0x0003333C File Offset: 0x0003153C
		protected bool ShouldSerializeSelectionBackColor()
		{
			return this.grid_style.SelectionBackColor != this.default_style.SelectionBackColor;
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGrid.SelectionForeColor" /> property should be persisted.</summary>
		/// <returns>true if the property value has changed from its default; otherwise, false.</returns>
		// Token: 0x06000CA3 RID: 3235 RVA: 0x0003335C File Offset: 0x0003155C
		protected virtual bool ShouldSerializeSelectionForeColor()
		{
			return this.grid_style.SelectionForeColor != this.default_style.SelectionForeColor;
		}

		/// <summary>Adds or removes the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> objects from the container that is associated with the <see cref="T:System.Windows.Forms.DataGrid" />.</summary>
		/// <param name="site">true to add the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> objects to a container; false to remove them.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CA4 RID: 3236 RVA: 0x0003337C File Offset: 0x0003157C
		public void SubObjectsSiteChange(bool site)
		{
		}

		/// <summary>Unselects a specified row.</summary>
		/// <param name="row">The index of the row to deselect. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000CA5 RID: 3237 RVA: 0x00033380 File Offset: 0x00031580
		public void UnSelect(int row)
		{
			bool isSelected = this.rows[row].IsSelected;
			this.rows[row].IsSelected = false;
			this.selected_rows.Remove(row);
			this.InvalidateRow(row);
			if (!isSelected)
			{
				this.OnUIASelectionChangedEvent(new CollectionChangeEventArgs(2, row));
			}
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x000333DC File Offset: 0x000315DC
		internal void CalcAreasAndInvalidate()
		{
			this.CalcGridAreas();
			base.Invalidate();
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x000333EC File Offset: 0x000315EC
		private void ConnectListManagerEvents()
		{
			this.list_manager.MetaDataChanged += new EventHandler(this.OnListManagerMetaDataChanged);
			this.list_manager.PositionChanged += new EventHandler(this.OnListManagerPositionChanged);
			this.list_manager.ItemChanged += this.OnListManagerItemChanged;
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x00033440 File Offset: 0x00031640
		private void DisconnectListManagerEvents()
		{
			this.list_manager.MetaDataChanged -= new EventHandler(this.OnListManagerMetaDataChanged);
			this.list_manager.PositionChanged -= new EventHandler(this.OnListManagerPositionChanged);
			this.list_manager.ItemChanged -= this.OnListManagerItemChanged;
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x00033494 File Offset: 0x00031694
		private void DisconnectTableStyleEvents()
		{
			this.current_style.AllowSortingChanged -= new EventHandler(this.TableStyleChanged);
			this.current_style.AlternatingBackColorChanged -= new EventHandler(this.TableStyleChanged);
			this.current_style.BackColorChanged -= new EventHandler(this.TableStyleChanged);
			this.current_style.ColumnHeadersVisibleChanged -= new EventHandler(this.TableStyleChanged);
			this.current_style.ForeColorChanged -= new EventHandler(this.TableStyleChanged);
			this.current_style.GridLineColorChanged -= new EventHandler(this.TableStyleChanged);
			this.current_style.GridLineStyleChanged -= new EventHandler(this.TableStyleChanged);
			this.current_style.HeaderBackColorChanged -= new EventHandler(this.TableStyleChanged);
			this.current_style.HeaderFontChanged -= new EventHandler(this.TableStyleChanged);
			this.current_style.HeaderForeColorChanged -= new EventHandler(this.TableStyleChanged);
			this.current_style.LinkColorChanged -= new EventHandler(this.TableStyleChanged);
			this.current_style.LinkHoverColorChanged -= new EventHandler(this.TableStyleChanged);
			this.current_style.MappingNameChanged -= new EventHandler(this.TableStyleChanged);
			this.current_style.PreferredColumnWidthChanged -= new EventHandler(this.TableStyleChanged);
			this.current_style.PreferredRowHeightChanged -= new EventHandler(this.TableStyleChanged);
			this.current_style.ReadOnlyChanged -= new EventHandler(this.TableStyleChanged);
			this.current_style.RowHeadersVisibleChanged -= new EventHandler(this.TableStyleChanged);
			this.current_style.RowHeaderWidthChanged -= new EventHandler(this.TableStyleChanged);
			this.current_style.SelectionBackColorChanged -= new EventHandler(this.TableStyleChanged);
			this.current_style.SelectionForeColorChanged -= new EventHandler(this.TableStyleChanged);
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x00033670 File Offset: 0x00031870
		private void ConnectTableStyleEvents()
		{
			this.current_style.AllowSortingChanged += new EventHandler(this.TableStyleChanged);
			this.current_style.AlternatingBackColorChanged += new EventHandler(this.TableStyleChanged);
			this.current_style.BackColorChanged += new EventHandler(this.TableStyleChanged);
			this.current_style.ColumnHeadersVisibleChanged += new EventHandler(this.TableStyleChanged);
			this.current_style.ForeColorChanged += new EventHandler(this.TableStyleChanged);
			this.current_style.GridLineColorChanged += new EventHandler(this.TableStyleChanged);
			this.current_style.GridLineStyleChanged += new EventHandler(this.TableStyleChanged);
			this.current_style.HeaderBackColorChanged += new EventHandler(this.TableStyleChanged);
			this.current_style.HeaderFontChanged += new EventHandler(this.TableStyleChanged);
			this.current_style.HeaderForeColorChanged += new EventHandler(this.TableStyleChanged);
			this.current_style.LinkColorChanged += new EventHandler(this.TableStyleChanged);
			this.current_style.LinkHoverColorChanged += new EventHandler(this.TableStyleChanged);
			this.current_style.MappingNameChanged += new EventHandler(this.TableStyleChanged);
			this.current_style.PreferredColumnWidthChanged += new EventHandler(this.TableStyleChanged);
			this.current_style.PreferredRowHeightChanged += new EventHandler(this.TableStyleChanged);
			this.current_style.ReadOnlyChanged += new EventHandler(this.TableStyleChanged);
			this.current_style.RowHeadersVisibleChanged += new EventHandler(this.TableStyleChanged);
			this.current_style.RowHeaderWidthChanged += new EventHandler(this.TableStyleChanged);
			this.current_style.SelectionBackColorChanged += new EventHandler(this.TableStyleChanged);
			this.current_style.SelectionForeColorChanged += new EventHandler(this.TableStyleChanged);
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0003384C File Offset: 0x00031A4C
		private void TableStyleChanged(object sender, EventArgs args)
		{
			this.EndEdit();
			this.CalcAreasAndInvalidate();
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0003385C File Offset: 0x00031A5C
		private void EnsureCellVisibility(DataGridCell cell)
		{
			if (cell.ColumnNumber <= this.first_visible_column || cell.ColumnNumber + 1 >= this.first_visible_column + this.visible_column_count)
			{
				this.first_visible_column = this.GetFirstColumnForColumnVisibility(this.first_visible_column, cell.ColumnNumber);
				int columnStartingPixel = this.GetColumnStartingPixel(this.first_visible_column);
				this.ScrollToColumnInPixels(columnStartingPixel);
				this.horiz_scrollbar.Value = columnStartingPixel;
				base.Update();
			}
			if (cell.RowNumber < this.first_visible_row || cell.RowNumber + 1 >= this.first_visible_row + this.visible_row_count)
			{
				if (cell.RowNumber + 1 >= this.first_visible_row + this.visible_row_count)
				{
					int num = this.first_visible_row;
					this.first_visible_row = 1 + cell.RowNumber - this.visible_row_count;
					this.UpdateVisibleRowCount();
					this.ScrollToRow(num, this.first_visible_row);
				}
				else
				{
					int num2 = this.first_visible_row;
					this.first_visible_row = cell.RowNumber;
					this.UpdateVisibleRowCount();
					this.ScrollToRow(num2, this.first_visible_row);
				}
				this.vert_scrollbar.Value = this.first_visible_row;
			}
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0003398C File Offset: 0x00031B8C
		private void SetDataSource(object source, string member)
		{
			this.SetDataSource(source, member, true);
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00033998 File Offset: 0x00031B98
		private void SetDataSource(object source, string member, bool recreate_rows)
		{
			CurrencyManager currencyManager = this.list_manager;
			if (this.in_setdatasource)
			{
				return;
			}
			this.in_setdatasource = true;
			if (source != null && source is IListSource && source is IList)
			{
				throw new Exception("Wrong complex data binding source");
			}
			this.datasource = source;
			this.datamember = member;
			if (this.is_editing)
			{
				this.CancelEditing();
			}
			this.current_cell = default(DataGridCell);
			if (this.list_manager != null)
			{
				this.DisconnectListManagerEvents();
			}
			this.list_manager = null;
			if (this.BindingContext != null && this.datasource != null)
			{
				this.list_manager = (CurrencyManager)this.BindingContext[this.datasource, this.datamember];
			}
			if (this.list_manager != null)
			{
				this.ConnectListManagerEvents();
			}
			if (currencyManager != this.list_manager)
			{
				this.BindColumns();
				this.vert_scrollbar.Value = 0;
				this.horiz_scrollbar.Value = 0;
				this.first_visible_row = 0;
				if (recreate_rows)
				{
					this.RecreateDataGridRows(false);
				}
			}
			this.CalcAreasAndInvalidate();
			this.in_setdatasource = false;
			this.OnDataSourceChanged(EventArgs.Empty);
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x00033ACC File Offset: 0x00031CCC
		private void RecreateDataGridRows(bool recalc)
		{
			DataGridRelationshipRow[] array = new DataGridRelationshipRow[this.RowsCount + ((!this.ShowEditRow) ? 0 : 1)];
			int num = 0;
			if (this.rows != null)
			{
				num = this.rows.Length;
				Array.Copy(this.rows, 0, array, 0, (this.rows.Length >= array.Length) ? array.Length : this.rows.Length);
			}
			for (int i = num; i < array.Length; i++)
			{
				array[i] = new DataGridRelationshipRow(this);
				array[i].height = this.RowHeight;
				if (i > 0)
				{
					array[i].VerticalOffset = array[i - 1].VerticalOffset + array[i - 1].Height;
				}
			}
			CollectionChangeAction collectionChangeAction = 3;
			if (this.rows != null)
			{
				if (array.Length - this.rows.Length > 0)
				{
					collectionChangeAction = 1;
				}
				else
				{
					collectionChangeAction = 2;
				}
			}
			this.rows = array;
			if (recalc)
			{
				this.CalcAreasAndInvalidate();
			}
			this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(collectionChangeAction, -1));
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00033BD8 File Offset: 0x00031DD8
		internal void UpdateRowsFrom(DataGridRelationshipRow row)
		{
			int num = Array.IndexOf<DataGridRelationshipRow>(this.rows, row);
			if (num == -1)
			{
				return;
			}
			for (int i = num + 1; i < this.rows.Length; i++)
			{
				this.rows[i].VerticalOffset = this.rows[i - 1].VerticalOffset + this.rows[i - 1].Height;
			}
			this.CalcAreasAndInvalidate();
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00033C48 File Offset: 0x00031E48
		private void BindColumns()
		{
			if (this.list_manager != null)
			{
				string listName = this.list_manager.GetListName(null);
				if (this.TableStyles[listName] == null)
				{
					this.current_style.GridColumnStyles.Clear();
					this.current_style.CreateColumnsForTable(false);
				}
				else if (this.CurrentTableStyle == this.grid_style || this.CurrentTableStyle.MappingName != listName)
				{
					this.CurrentTableStyle = this.styles_collection[listName];
					this.current_style.CreateColumnsForTable(this.current_style.GridColumnStyles.Count > 0);
				}
				else
				{
					this.current_style.CreateColumnsForTable(true);
				}
			}
			else
			{
				this.current_style.CreateColumnsForTable(false);
			}
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00033D18 File Offset: 0x00031F18
		private void OnListManagerMetaDataChanged(object sender, EventArgs e)
		{
			this.BindColumns();
			this.CalcAreasAndInvalidate();
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00033D28 File Offset: 0x00031F28
		private void OnListManagerPositionChanged(object sender, EventArgs e)
		{
			this.from_positionchanged_handler = true;
			this.CurrentRow = this.list_manager.Position;
			this.from_positionchanged_handler = false;
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x00033D4C File Offset: 0x00031F4C
		private void OnListManagerItemChanged(object sender, ItemChangedEventArgs e)
		{
			if (this.adding_new_row)
			{
				return;
			}
			if (e.Index == -1)
			{
				this.ResetSelection();
				if (this.rows == null || this.RowsCount != this.rows.Length - ((!this.ShowEditRow) ? 0 : 1))
				{
					this.RecreateDataGridRows(true);
				}
			}
			else
			{
				this.InvalidateRow(e.Index);
			}
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00033DC0 File Offset: 0x00031FC0
		private void OnTableStylesCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			if (this.ListManager == null)
			{
				return;
			}
			string listName = this.ListManager.GetListName(null);
			switch (e.Action)
			{
			case 1:
				if (e.Element != null && string.Compare(listName, ((DataGridTableStyle)e.Element).MappingName, true) == 0)
				{
					this.CurrentTableStyle = (DataGridTableStyle)e.Element;
					((DataGridTableStyle)e.Element).CreateColumnsForTable(this.CurrentTableStyle.GridColumnStyles.Count > 0);
				}
				break;
			case 2:
				if (e.Element != null && string.Compare(listName, ((DataGridTableStyle)e.Element).MappingName, true) == 0)
				{
					this.CurrentTableStyle = this.default_style;
					this.current_style.GridColumnStyles.Clear();
					this.current_style.CreateColumnsForTable(false);
				}
				break;
			case 3:
				if (this.CurrentTableStyle == this.default_style || string.Compare(listName, this.CurrentTableStyle.MappingName, true) != 0)
				{
					DataGridTableStyle dataGridTableStyle = this.styles_collection[listName];
					if (dataGridTableStyle != null)
					{
						this.CurrentTableStyle = dataGridTableStyle;
						this.current_style.CreateColumnsForTable(false);
					}
					else
					{
						this.CurrentTableStyle = this.default_style;
						this.current_style.GridColumnStyles.Clear();
						this.current_style.CreateColumnsForTable(false);
					}
				}
				break;
			}
			this.CalcAreasAndInvalidate();
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00033F40 File Offset: 0x00032140
		private void AddNewRow()
		{
			this.ListManager.EndCurrentEdit();
			this.ListManager.AddNew();
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00033F58 File Offset: 0x00032158
		private void Edit()
		{
			if (this.CurrentTableStyle.GridColumnStyles.Count == 0)
			{
				return;
			}
			if (!this.CurrentTableStyle.GridColumnStyles[this.CurrentColumn].bound)
			{
				return;
			}
			if (this.ListManager != null && this.ListManager.Count == 0)
			{
				return;
			}
			this.is_editing = true;
			this.is_changing = false;
			this.CurrentTableStyle.GridColumnStyles[this.CurrentColumn].Edit(this.ListManager, this.CurrentRow, this.GetCellBounds(this.CurrentRow, this.CurrentColumn), this._readonly, null, true);
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x00034008 File Offset: 0x00032208
		private void EndEdit()
		{
			if (this.CurrentTableStyle.GridColumnStyles.Count == 0)
			{
				return;
			}
			if (!this.CurrentTableStyle.GridColumnStyles[this.current_cell.ColumnNumber].bound)
			{
				return;
			}
			this.EndEdit(this.CurrentTableStyle.GridColumnStyles[this.current_cell.ColumnNumber], this.current_cell.RowNumber, false);
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x00034080 File Offset: 0x00032280
		private void ShiftSelection(int index)
		{
			int num = this.selection_start;
			this.ResetSelection();
			this.selection_start = num;
			int num2;
			int num3;
			if (index >= this.selection_start)
			{
				num2 = this.selection_start;
				num3 = index;
			}
			else
			{
				num2 = index;
				num3 = this.selection_start;
			}
			if (num2 == -1)
			{
				num2 = 0;
			}
			for (int i = num2; i <= num3; i++)
			{
				this.Select(i);
			}
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x000340E8 File Offset: 0x000322E8
		private void ScrollToColumnInPixels(int pixel)
		{
			int num;
			if (pixel > this.horiz_pixeloffset)
			{
				num = -1 * (pixel - this.horiz_pixeloffset);
			}
			else
			{
				num = this.horiz_pixeloffset - pixel;
			}
			Rectangle rectangle = this.cells_area;
			if (this.ColumnHeadersVisible)
			{
				rectangle.Y -= this.ColumnHeadersArea.Height;
				rectangle.Height += this.ColumnHeadersArea.Height;
			}
			this.horiz_pixeloffset = pixel;
			this.UpdateVisibleColumn();
			this.EndEdit();
			XplatUI.ScrollWindow(this.Handle, rectangle, num, 0, false);
			int columnStartingPixel = this.GetColumnStartingPixel(this.CurrentColumn);
			int num2 = columnStartingPixel + this.CurrentTableStyle.GridColumnStyles[this.CurrentColumn].Width;
			if (columnStartingPixel >= this.horiz_pixeloffset && num2 < this.horiz_pixeloffset + this.cells_area.Width)
			{
				this.Edit();
			}
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x000341E0 File Offset: 0x000323E0
		private void ScrollToRow(int old_row, int new_row)
		{
			int num = 0;
			if (new_row > old_row)
			{
				for (int i = old_row; i < new_row; i++)
				{
					num -= this.rows[i].Height;
				}
			}
			else
			{
				for (int i = new_row; i < old_row; i++)
				{
					num += this.rows[i].Height;
				}
			}
			if (num == 0)
			{
				return;
			}
			Rectangle rectangle = this.cells_area;
			if (this.RowHeadersVisible)
			{
				rectangle.X -= this.RowHeaderWidth;
				rectangle.Width += this.RowHeaderWidth;
			}
			XplatUI.ScrollWindow(this.Handle, rectangle, 0, num, false);
			if (this.CurrentRow >= this.first_visible_row && this.CurrentRow < this.first_visible_row + this.visible_row_count)
			{
				this.Edit();
			}
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x000342C0 File Offset: 0x000324C0
		private void ColumnResize(int column)
		{
			CurrencyManager listManager = this.ListManager;
			DataGridColumnStyle dataGridColumnStyle = this.CurrentTableStyle.GridColumnStyles[column];
			string headerText = dataGridColumnStyle.HeaderText;
			using (Graphics graphics = base.CreateGraphics())
			{
				int count = listManager.Count;
				int num = (int)graphics.MeasureString(headerText, this.CurrentTableStyle.HeaderFont).Width + 4;
				for (int i = 0; i < count; i++)
				{
					int width = dataGridColumnStyle.GetPreferredSize(graphics, dataGridColumnStyle.GetColumnValueAtRow(listManager, i)).Width;
					if (width > num)
					{
						num = width;
					}
				}
				if (dataGridColumnStyle.Width != num)
				{
					dataGridColumnStyle.Width = num;
				}
			}
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x000343A0 File Offset: 0x000325A0
		private void RowResize(int row)
		{
			CurrencyManager listManager = this.ListManager;
			using (Graphics graphics = base.CreateGraphics())
			{
				GridColumnStylesCollection gridColumnStyles = this.CurrentTableStyle.GridColumnStyles;
				int count = gridColumnStyles.Count;
				int num = 0;
				for (int i = 0; i < count; i++)
				{
					object columnValueAtRow = gridColumnStyles[i].GetColumnValueAtRow(listManager, row);
					num = Math.Max(gridColumnStyles[i].GetPreferredHeight(graphics, columnValueAtRow), num);
				}
				if (this.DataGridRows[row].Height != num)
				{
					this.DataGridRows[row].Height = num;
				}
			}
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00034464 File Offset: 0x00032664
		private int CalcAllColumnsWidth()
		{
			int num = 0;
			int count = this.CurrentTableStyle.GridColumnStyles.Count;
			for (int i = 0; i < count; i++)
			{
				if (this.CurrentTableStyle.GridColumnStyles[i].bound)
				{
					num += this.CurrentTableStyle.GridColumnStyles[i].Width;
				}
			}
			return num;
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x000344D0 File Offset: 0x000326D0
		private int FromPixelToColumn(int pixel, out int column_x)
		{
			int num = 0;
			int count = this.CurrentTableStyle.GridColumnStyles.Count;
			column_x = 0;
			if (count == 0)
			{
				return -1;
			}
			if (this.CurrentTableStyle.CurrentRowHeadersVisible)
			{
				num += this.row_headers_area.X + this.row_headers_area.Width;
				column_x += this.row_headers_area.X + this.row_headers_area.Width;
				if (pixel < num)
				{
					return -1;
				}
			}
			for (int i = 0; i < count; i++)
			{
				if (this.CurrentTableStyle.GridColumnStyles[i].bound)
				{
					num += this.CurrentTableStyle.GridColumnStyles[i].Width;
					if (pixel < num)
					{
						return i;
					}
					column_x += this.CurrentTableStyle.GridColumnStyles[i].Width;
				}
			}
			return count - 1;
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x000345BC File Offset: 0x000327BC
		internal int GetColumnStartingPixel(int my_col)
		{
			int num = 0;
			int count = this.CurrentTableStyle.GridColumnStyles.Count;
			for (int i = 0; i < count; i++)
			{
				if (this.CurrentTableStyle.GridColumnStyles[i].bound)
				{
					if (my_col == i)
					{
						return num;
					}
					num += this.CurrentTableStyle.GridColumnStyles[i].Width;
				}
			}
			return 0;
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x00034634 File Offset: 0x00032834
		private int GetFirstColumnForColumnVisibility(int current_first_visible_column, int column)
		{
			int num = 0;
			if (column > current_first_visible_column)
			{
				for (int i = column; i >= 0; i--)
				{
					if (this.CurrentTableStyle.GridColumnStyles[i].bound)
					{
						num += this.CurrentTableStyle.GridColumnStyles[i].Width;
						if (num >= this.cells_area.Width)
						{
							return i + 1;
						}
					}
				}
				return 0;
			}
			return column;
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x000346B0 File Offset: 0x000328B0
		private void CalcGridAreas()
		{
			if (!base.IsHandleCreated)
			{
				return;
			}
			if (this.in_calc_grid_areas)
			{
				return;
			}
			this.in_calc_grid_areas = true;
			this.horiz_pixeloffset = 0;
			this.CalcCaption();
			this.CalcParentRows();
			this.CalcParentButtons();
			this.UpdateVisibleRowCount();
			this.CalcRowHeaders();
			this.width_of_all_columns = this.CalcAllColumnsWidth();
			this.CalcColumnHeaders();
			this.CalcCellsArea();
			bool flag = false;
			bool flag2 = false;
			int num = this.cells_area.Width;
			int num2 = this.cells_area.Height;
			int num3 = this.RowsCount;
			if (this.ShowEditRow && this.RowsCount > 0)
			{
				num3++;
			}
			for (int i = 0; i < 3; i++)
			{
				if (flag2)
				{
					num = this.cells_area.Width - this.vert_scrollbar.Width;
				}
				if (flag)
				{
					num2 = this.cells_area.Height - this.horiz_scrollbar.Height;
				}
				this.UpdateVisibleRowCount();
				flag = this.width_of_all_columns > num;
				flag2 = num3 > this.visible_row_count;
			}
			int num4 = base.ClientRectangle.Width;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			if (flag2)
			{
				this.SetUpVerticalScrollBar(out num6, out num7);
			}
			if (flag)
			{
				this.SetUpHorizontalScrollBar(out num5);
			}
			this.cells_area.Width = num;
			this.cells_area.Height = num2;
			if (flag2 && flag)
			{
				if (this.ShowParentRows)
				{
					this.parent_rows.Width = this.parent_rows.Width - this.vert_scrollbar.Width;
				}
				if (!this.ColumnHeadersVisible && this.column_headers_area.X + this.column_headers_area.Width > this.vert_scrollbar.Location.X)
				{
					this.column_headers_area.Width = this.column_headers_area.Width - this.vert_scrollbar.Width;
				}
				num4 -= this.vert_scrollbar.Width;
				num6 -= this.horiz_scrollbar.Height;
			}
			if (flag2)
			{
				if (this.row_headers_area.Y + this.row_headers_area.Height > base.ClientRectangle.Y + base.ClientRectangle.Height)
				{
					this.row_headers_area.Height = this.row_headers_area.Height - this.horiz_scrollbar.Height;
				}
				this.vert_scrollbar.Size = new Size(this.vert_scrollbar.Width, num6);
				this.vert_scrollbar.Maximum = num7;
				base.Controls.Add(this.vert_scrollbar);
				this.vert_scrollbar.Visible = true;
			}
			else
			{
				base.Controls.Remove(this.vert_scrollbar);
				this.vert_scrollbar.Visible = false;
			}
			if (flag)
			{
				this.horiz_scrollbar.Size = new Size(num4, this.horiz_scrollbar.Height);
				this.horiz_scrollbar.Maximum = num5;
				base.Controls.Add(this.horiz_scrollbar);
				this.horiz_scrollbar.Visible = true;
			}
			else
			{
				base.Controls.Remove(this.horiz_scrollbar);
				this.horiz_scrollbar.Visible = false;
			}
			this.UpdateVisibleColumn();
			this.UpdateVisibleRowCount();
			this.in_calc_grid_areas = false;
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x00034A0C File Offset: 0x00032C0C
		private void CalcCaption()
		{
			this.caption_area.X = base.ClientRectangle.X;
			this.caption_area.Y = base.ClientRectangle.Y;
			this.caption_area.Width = base.ClientRectangle.Width;
			if (this.caption_visible)
			{
				this.caption_area.Height = this.CaptionFont.Height;
				if (this.caption_area.Height < this.back_button_image.Height)
				{
					this.caption_area.Height = this.back_button_image.Height;
				}
				this.caption_area.Height = this.caption_area.Height + 2;
			}
			else
			{
				this.caption_area.Height = 0;
			}
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x00034ADC File Offset: 0x00032CDC
		private void CalcCellsArea()
		{
			this.cells_area.X = base.ClientRectangle.X + this.row_headers_area.Width;
			this.cells_area.Y = this.column_headers_area.Y + this.column_headers_area.Height;
			this.cells_area.Width = base.ClientRectangle.X + base.ClientRectangle.Width - this.cells_area.X;
			if (this.cells_area.Width < 0)
			{
				this.cells_area.Width = 0;
			}
			this.cells_area.Height = base.ClientRectangle.Y + base.ClientRectangle.Height - this.cells_area.Y;
			if (this.cells_area.Height < 0)
			{
				this.cells_area.Height = 0;
			}
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00034BD4 File Offset: 0x00032DD4
		private void CalcColumnHeaders()
		{
			this.column_headers_area.X = base.ClientRectangle.X;
			this.column_headers_area.Y = this.parent_rows.Y + this.parent_rows.Height;
			this.column_headers_max_width = base.ClientRectangle.X + base.ClientRectangle.Width - this.column_headers_area.X;
			int num = this.column_headers_max_width;
			if (this.CurrentTableStyle.CurrentRowHeadersVisible)
			{
				num -= this.RowHeaderWidth;
			}
			if (this.width_of_all_columns > num)
			{
				this.column_headers_area.Width = this.column_headers_max_width;
			}
			else
			{
				this.column_headers_area.Width = this.width_of_all_columns;
				if (this.CurrentTableStyle.CurrentRowHeadersVisible)
				{
					this.column_headers_area.Width = this.column_headers_area.Width + this.RowHeaderWidth;
				}
			}
			if (this.ColumnHeadersVisible)
			{
				this.column_headers_area.Height = this.CurrentTableStyle.HeaderFont.Height + 6;
			}
			else
			{
				this.column_headers_area.Height = 0;
			}
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x00034D00 File Offset: 0x00032F00
		private void CalcParentRows()
		{
			this.parent_rows.X = base.ClientRectangle.X;
			this.parent_rows.Y = this.caption_area.Y + this.caption_area.Height;
			this.parent_rows.Width = base.ClientRectangle.Width;
			if (this.ShowParentRows)
			{
				this.parent_rows.Height = (this.CaptionFont.Height + 3) * this.data_source_stack.Count;
			}
			else
			{
				this.parent_rows.Height = 0;
			}
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x00034DA4 File Offset: 0x00032FA4
		private void CalcParentButtons()
		{
			if (this.data_source_stack.Count > 0 && this.CaptionVisible)
			{
				this.back_button_rect = new Rectangle(base.ClientRectangle.X + base.ClientRectangle.Width - 2 * (this.caption_area.Height - 2) - 8, this.caption_area.Height / 2 - this.back_button_image.Height / 2, this.back_button_image.Width, this.back_button_image.Height);
				this.parent_rows_button_rect = new Rectangle(base.ClientRectangle.X + base.ClientRectangle.Width - (this.caption_area.Height - 2) - 4, this.caption_area.Height / 2 - this.parent_rows_button_image.Height / 2, this.parent_rows_button_image.Width, this.parent_rows_button_image.Height);
			}
			else
			{
				this.back_button_rect = (this.parent_rows_button_rect = Rectangle.Empty);
			}
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00034EBC File Offset: 0x000330BC
		private void CalcRowHeaders()
		{
			this.row_headers_area.X = base.ClientRectangle.X;
			this.row_headers_area.Y = this.column_headers_area.Y + this.column_headers_area.Height;
			this.row_headers_area.Height = base.ClientRectangle.Height + base.ClientRectangle.Y - this.row_headers_area.Y;
			if (this.CurrentTableStyle.CurrentRowHeadersVisible)
			{
				this.row_headers_area.Width = this.RowHeaderWidth;
			}
			else
			{
				this.row_headers_area.Width = 0;
			}
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00034F6C File Offset: 0x0003316C
		private int GetVisibleRowCount(int visibleHeight)
		{
			int num = 0;
			int i;
			for (i = this.FirstVisibleRow; i < this.rows.Length; i++)
			{
				if (num + this.rows[i].Height >= visibleHeight)
				{
					break;
				}
				num += this.rows[i].Height;
			}
			if (i <= this.rows.Length - 1)
			{
				i++;
			}
			return i - this.FirstVisibleRow;
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x00034FE0 File Offset: 0x000331E0
		private void UpdateVisibleColumn()
		{
			this.visible_column_count = 0;
			if (this.CurrentTableStyle.GridColumnStyles.Count == 0)
			{
				return;
			}
			int num = this.horiz_pixeloffset;
			if (this.CurrentTableStyle.CurrentRowHeadersVisible)
			{
				num += this.row_headers_area.X + this.row_headers_area.Width;
			}
			int num2 = num + this.cells_area.Width;
			int num3;
			this.first_visible_column = this.FromPixelToColumn(num, out num3);
			int num4 = this.FromPixelToColumn(num2, out num3);
			for (int i = this.first_visible_column; i <= num4; i++)
			{
				if (this.CurrentTableStyle.GridColumnStyles[i].bound)
				{
					this.visible_column_count++;
				}
			}
			if (this.first_visible_column + this.visible_column_count < this.CurrentTableStyle.GridColumnStyles.Count)
			{
				this.visible_column_count++;
			}
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x000350D8 File Offset: 0x000332D8
		private void UpdateVisibleRowCount()
		{
			this.visible_row_count = this.GetVisibleRowCount(this.cells_area.Height);
			this.CalcRowHeaders();
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x000350F8 File Offset: 0x000332F8
		private void InvalidateCaption()
		{
			if (this.caption_area.IsEmpty)
			{
				return;
			}
			base.Invalidate(this.caption_area);
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x00035118 File Offset: 0x00033318
		private void InvalidateRow(int row)
		{
			if (row < this.FirstVisibleRow || row > this.FirstVisibleRow + this.VisibleRowCount)
			{
				return;
			}
			Rectangle rectangle = default(Rectangle);
			rectangle.X = this.cells_area.X;
			rectangle.Width = this.width_of_all_columns;
			if (rectangle.Width > this.cells_area.Width)
			{
				rectangle.Width = this.cells_area.Width;
			}
			rectangle.Height = this.rows[row].Height;
			rectangle.Y = this.cells_area.Y + this.rows[row].VerticalOffset - this.rows[this.FirstVisibleRow].VerticalOffset;
			base.Invalidate(rectangle);
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x000351E4 File Offset: 0x000333E4
		private void InvalidateRowHeader(int row)
		{
			Rectangle rectangle = default(Rectangle);
			rectangle.X = this.row_headers_area.X;
			rectangle.Width = this.row_headers_area.Width;
			rectangle.Height = this.rows[row].Height;
			rectangle.Y = this.row_headers_area.Y + this.rows[row].VerticalOffset - this.rows[this.FirstVisibleRow].VerticalOffset;
			base.Invalidate(rectangle);
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x0003526C File Offset: 0x0003346C
		internal void InvalidateColumn(DataGridColumnStyle column)
		{
			Rectangle rectangle = default(Rectangle);
			int num = this.CurrentTableStyle.GridColumnStyles.IndexOf(column);
			if (num == -1)
			{
				return;
			}
			rectangle.Width = column.Width;
			int columnStartingPixel = this.GetColumnStartingPixel(num);
			rectangle.X = this.cells_area.X + columnStartingPixel - this.horiz_pixeloffset;
			rectangle.Y = this.cells_area.Y;
			rectangle.Height = this.cells_area.Height;
			base.Invalidate(rectangle);
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x000352F8 File Offset: 0x000334F8
		private void DrawResizeLineVert(int x)
		{
			XplatUI.DrawReversibleRectangle(this.Handle, new Rectangle(x, this.cells_area.Y, 1, this.cells_area.Height - 3), 2);
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x00035330 File Offset: 0x00033530
		private void DrawResizeLineHoriz(int y)
		{
			XplatUI.DrawReversibleRectangle(this.Handle, new Rectangle(this.cells_area.X, y, this.cells_area.Width - 3, 1), 2);
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00035368 File Offset: 0x00033568
		private void SetUpHorizontalScrollBar(out int maximum)
		{
			maximum = this.width_of_all_columns;
			this.horiz_scrollbar.Location = new Point(base.ClientRectangle.X, base.ClientRectangle.Y + base.ClientRectangle.Height - this.horiz_scrollbar.Height);
			this.horiz_scrollbar.LargeChange = this.cells_area.Width;
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x000353DC File Offset: 0x000335DC
		private void SetUpVerticalScrollBar(out int height, out int maximum)
		{
			int num = base.ClientRectangle.Y + this.parent_rows.Y + this.parent_rows.Height;
			height = base.ClientRectangle.Height - this.parent_rows.Y - this.parent_rows.Height;
			this.vert_scrollbar.Location = new Point(base.ClientRectangle.X + base.ClientRectangle.Width - this.vert_scrollbar.Width, num);
			maximum = this.RowsCount;
			if (this.ShowEditRow && this.RowsCount > 0)
			{
				maximum++;
			}
			this.vert_scrollbar.LargeChange = this.VLargeChange;
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x000354AC File Offset: 0x000336AC
		internal Rectangle ColumnHeadersArea
		{
			get
			{
				Rectangle rectangle = this.column_headers_area;
				if (this.CurrentTableStyle.CurrentRowHeadersVisible)
				{
					rectangle.X += this.RowHeaderWidth;
					rectangle.Width -= this.RowHeaderWidth;
				}
				return rectangle;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x000354FC File Offset: 0x000336FC
		internal Rectangle RowHeadersArea
		{
			get
			{
				return this.row_headers_area;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x00035504 File Offset: 0x00033704
		internal Rectangle ParentRowsArea
		{
			get
			{
				return this.parent_rows;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x0003550C File Offset: 0x0003370C
		private int VLargeChange
		{
			get
			{
				return this.cells_area.Height / this.RowHeight;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x00035520 File Offset: 0x00033720
		internal ScrollBar UIAHScrollBar
		{
			get
			{
				return this.horiz_scrollbar;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x00035528 File Offset: 0x00033728
		internal ScrollBar UIAVScrollBar
		{
			get
			{
				return this.vert_scrollbar;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x00035530 File Offset: 0x00033730
		internal DataGridTableStyle UIACurrentTableStyle
		{
			get
			{
				return this.current_style;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x00035538 File Offset: 0x00033738
		internal int UIASelectedRows
		{
			get
			{
				return this.selected_rows.Count;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x00035548 File Offset: 0x00033748
		internal Rectangle UIAColumnHeadersArea
		{
			get
			{
				return this.ColumnHeadersArea;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x00035550 File Offset: 0x00033750
		internal Rectangle UIACaptionArea
		{
			get
			{
				return this.caption_area;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x00035558 File Offset: 0x00033758
		internal Rectangle UIACellsArea
		{
			get
			{
				return this.cells_area;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x00035560 File Offset: 0x00033760
		internal int UIARowHeight
		{
			get
			{
				return this.RowHeight;
			}
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x00035568 File Offset: 0x00033768
		internal void OnUIACollectionChangedEvent(CollectionChangeEventArgs args)
		{
			CollectionChangeEventHandler collectionChangeEventHandler = (CollectionChangeEventHandler)base.Events[DataGrid.UIACollectionChangedEvent];
			if (collectionChangeEventHandler != null)
			{
				collectionChangeEventHandler.Invoke(this, args);
			}
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0003559C File Offset: 0x0003379C
		internal void OnUIASelectionChangedEvent(CollectionChangeEventArgs args)
		{
			CollectionChangeEventHandler collectionChangeEventHandler = (CollectionChangeEventHandler)base.Events[DataGrid.UIASelectionChangedEvent];
			if (collectionChangeEventHandler != null)
			{
				collectionChangeEventHandler.Invoke(this, args);
			}
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x000355D0 File Offset: 0x000337D0
		internal void OnUIAColumnHeadersVisibleChanged()
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGrid.UIAColumnHeadersVisibleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x00035608 File Offset: 0x00033808
		internal void OnUIAGridCellChanged(CollectionChangeEventArgs args)
		{
			CollectionChangeEventHandler collectionChangeEventHandler = (CollectionChangeEventHandler)base.Events[DataGrid.UIAGridCellChangedEvent];
			if (collectionChangeEventHandler != null)
			{
				collectionChangeEventHandler.Invoke(this, args);
			}
		}

		// Token: 0x040008DD RID: 2269
		private const int RESIZE_HANDLE_HORIZ_SIZE = 5;

		// Token: 0x040008DE RID: 2270
		private const int RESIZE_HANDLE_VERT_SIZE = 3;

		// Token: 0x040008DF RID: 2271
		private static readonly Color def_background_color = ThemeEngine.Current.DataGridBackgroundColor;

		// Token: 0x040008E0 RID: 2272
		private static readonly Color def_caption_backcolor = ThemeEngine.Current.DataGridCaptionBackColor;

		// Token: 0x040008E1 RID: 2273
		private static readonly Color def_caption_forecolor = ThemeEngine.Current.DataGridCaptionForeColor;

		// Token: 0x040008E2 RID: 2274
		private static readonly Color def_parent_rows_backcolor = ThemeEngine.Current.DataGridParentRowsBackColor;

		// Token: 0x040008E3 RID: 2275
		private static readonly Color def_parent_rows_forecolor = ThemeEngine.Current.DataGridParentRowsForeColor;

		// Token: 0x040008E4 RID: 2276
		private new Color background_color;

		// Token: 0x040008E5 RID: 2277
		private Color caption_backcolor;

		// Token: 0x040008E6 RID: 2278
		private Color caption_forecolor;

		// Token: 0x040008E7 RID: 2279
		private Color parent_rows_backcolor;

		// Token: 0x040008E8 RID: 2280
		private Color parent_rows_forecolor;

		// Token: 0x040008E9 RID: 2281
		private bool caption_visible;

		// Token: 0x040008EA RID: 2282
		private bool parent_rows_visible;

		// Token: 0x040008EB RID: 2283
		private GridTableStylesCollection styles_collection;

		// Token: 0x040008EC RID: 2284
		private DataGridParentRowsLabelStyle parent_rows_label_style;

		// Token: 0x040008ED RID: 2285
		private DataGridTableStyle default_style;

		// Token: 0x040008EE RID: 2286
		private DataGridTableStyle grid_style;

		// Token: 0x040008EF RID: 2287
		private DataGridTableStyle current_style;

		// Token: 0x040008F0 RID: 2288
		private DataGridCell current_cell;

		// Token: 0x040008F1 RID: 2289
		private Hashtable selected_rows;

		// Token: 0x040008F2 RID: 2290
		private int selection_start;

		// Token: 0x040008F3 RID: 2291
		private bool allow_navigation;

		// Token: 0x040008F4 RID: 2292
		private int first_visible_row;

		// Token: 0x040008F5 RID: 2293
		private int first_visible_column;

		// Token: 0x040008F6 RID: 2294
		private int visible_row_count;

		// Token: 0x040008F7 RID: 2295
		private int visible_column_count;

		// Token: 0x040008F8 RID: 2296
		private Font caption_font;

		// Token: 0x040008F9 RID: 2297
		private string caption_text;

		// Token: 0x040008FA RID: 2298
		private bool flatmode;

		// Token: 0x040008FB RID: 2299
		private HScrollBar horiz_scrollbar;

		// Token: 0x040008FC RID: 2300
		private VScrollBar vert_scrollbar;

		// Token: 0x040008FD RID: 2301
		private int horiz_pixeloffset;

		// Token: 0x040008FE RID: 2302
		internal Bitmap back_button_image;

		// Token: 0x040008FF RID: 2303
		internal Rectangle back_button_rect;

		// Token: 0x04000900 RID: 2304
		internal bool back_button_mouseover;

		// Token: 0x04000901 RID: 2305
		internal bool back_button_active;

		// Token: 0x04000902 RID: 2306
		internal Bitmap parent_rows_button_image;

		// Token: 0x04000903 RID: 2307
		internal Rectangle parent_rows_button_rect;

		// Token: 0x04000904 RID: 2308
		internal bool parent_rows_button_mouseover;

		// Token: 0x04000905 RID: 2309
		internal bool parent_rows_button_active;

		// Token: 0x04000906 RID: 2310
		private object datasource;

		// Token: 0x04000907 RID: 2311
		private string datamember;

		// Token: 0x04000908 RID: 2312
		private CurrencyManager list_manager;

		// Token: 0x04000909 RID: 2313
		private bool refetch_list_manager = true;

		// Token: 0x0400090A RID: 2314
		private bool _readonly;

		// Token: 0x0400090B RID: 2315
		private DataGridRelationshipRow[] rows;

		// Token: 0x0400090C RID: 2316
		private bool column_resize_active;

		// Token: 0x0400090D RID: 2317
		private int resize_column_x;

		// Token: 0x0400090E RID: 2318
		private int resize_column_width_delta;

		// Token: 0x0400090F RID: 2319
		private int resize_column;

		// Token: 0x04000910 RID: 2320
		private bool row_resize_active;

		// Token: 0x04000911 RID: 2321
		private int resize_row_y;

		// Token: 0x04000912 RID: 2322
		private int resize_row_height_delta;

		// Token: 0x04000913 RID: 2323
		private int resize_row;

		// Token: 0x04000914 RID: 2324
		private bool from_positionchanged_handler;

		// Token: 0x04000915 RID: 2325
		private bool cursor_in_add_row;

		// Token: 0x04000916 RID: 2326
		private bool add_row_changed;

		// Token: 0x04000917 RID: 2327
		internal bool is_editing;

		// Token: 0x04000918 RID: 2328
		private bool is_changing;

		// Token: 0x04000919 RID: 2329
		private bool commit_row_changes = true;

		// Token: 0x0400091A RID: 2330
		private bool adding_new_row;

		// Token: 0x0400091B RID: 2331
		internal Stack data_source_stack;

		// Token: 0x0400091C RID: 2332
		private bool setting_current_cell;

		// Token: 0x0400091D RID: 2333
		private bool in_setdatasource;

		// Token: 0x0400092D RID: 2349
		private Rectangle parent_rows;

		// Token: 0x0400092E RID: 2350
		private int width_of_all_columns;

		// Token: 0x0400092F RID: 2351
		internal Rectangle caption_area;

		// Token: 0x04000930 RID: 2352
		internal Rectangle column_headers_area;

		// Token: 0x04000931 RID: 2353
		internal int column_headers_max_width;

		// Token: 0x04000932 RID: 2354
		internal Rectangle row_headers_area;

		// Token: 0x04000933 RID: 2355
		internal Rectangle cells_area;

		// Token: 0x04000934 RID: 2356
		private bool in_calc_grid_areas;

		/// <summary>Specifies the part of the <see cref="T:System.Windows.Forms.DataGrid" /> control the user has clicked.</summary>
		// Token: 0x020000C0 RID: 192
		[Flags]
		public enum HitTestType
		{
			/// <summary>The background area, visible when the control contains no table, few rows, or when a table is scrolled to its bottom.</summary>
			// Token: 0x0400093A RID: 2362
			None = 0,
			/// <summary>A cell in the <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
			// Token: 0x0400093B RID: 2363
			Cell = 1,
			/// <summary>A column header in the <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
			// Token: 0x0400093C RID: 2364
			ColumnHeader = 2,
			/// <summary>A row header in the <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
			// Token: 0x0400093D RID: 2365
			RowHeader = 4,
			/// <summary>The column border, which is the line between column headers. It can be dragged to resize a column's width.</summary>
			// Token: 0x0400093E RID: 2366
			ColumnResize = 8,
			/// <summary>The row border, which is the line between grid row headers. It can be dragged to resize a row's height.</summary>
			// Token: 0x0400093F RID: 2367
			RowResize = 16,
			/// <summary>The caption of the <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
			// Token: 0x04000940 RID: 2368
			Caption = 32,
			/// <summary>The parent row section of the <see cref="T:System.Windows.Forms.DataGrid" /> control. The parent row displays information from or about the parent table of the currently displayed child table, such as the name of the parent table, column names and values of the parent record.</summary>
			// Token: 0x04000941 RID: 2369
			ParentRows = 64
		}

		/// <summary>Contains information about a part of the <see cref="T:System.Windows.Forms.DataGrid" /> at a specified coordinate. This class cannot be inherited.</summary>
		// Token: 0x020000C1 RID: 193
		public sealed class HitTestInfo
		{
			// Token: 0x06000CE4 RID: 3300 RVA: 0x0003563C File Offset: 0x0003383C
			internal HitTestInfo()
				: this(-1, -1, DataGrid.HitTestType.None)
			{
			}

			// Token: 0x06000CE5 RID: 3301 RVA: 0x00035648 File Offset: 0x00033848
			internal HitTestInfo(int row, int column, DataGrid.HitTestType type)
			{
				this.row = row;
				this.column = column;
				this.type = type;
			}

			/// <summary>Gets the number of the column the user has clicked.</summary>
			/// <returns>The number of the column.</returns>
			// Token: 0x170002EA RID: 746
			// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x0003566C File Offset: 0x0003386C
			public int Column
			{
				get
				{
					return this.column;
				}
			}

			/// <summary>Gets the number of the row the user has clicked.</summary>
			/// <returns>The number of the clicked row.</returns>
			// Token: 0x170002EB RID: 747
			// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x00035674 File Offset: 0x00033874
			public int Row
			{
				get
				{
					return this.row;
				}
			}

			/// <summary>Gets the part of the <see cref="T:System.Windows.Forms.DataGrid" /> control, other than the row or column, that was clicked.</summary>
			/// <returns>One of the <see cref="T:System.Windows.Forms.DataGrid.HitTestType" /> enumerations.</returns>
			// Token: 0x170002EC RID: 748
			// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x0003567C File Offset: 0x0003387C
			public DataGrid.HitTestType Type
			{
				get
				{
					return this.type;
				}
			}

			/// <summary>Indicates whether two objects are identical.</summary>
			/// <returns>true if the objects are equal; otherwise, false.</returns>
			/// <param name="value">The second object to compare, typed as <see cref="T:System.Object" />. </param>
			// Token: 0x06000CEA RID: 3306 RVA: 0x00035684 File Offset: 0x00033884
			public override bool Equals(object value)
			{
				if (!(value is DataGrid.HitTestInfo))
				{
					return false;
				}
				DataGrid.HitTestInfo hitTestInfo = (DataGrid.HitTestInfo)value;
				return hitTestInfo.Column == this.column && hitTestInfo.Row == this.row && hitTestInfo.Type == this.type;
			}

			/// <summary>Gets the hash code for the <see cref="T:System.Windows.Forms.DataGrid.HitTestInfo" /> instance.</summary>
			/// <returns>The hash code for this instance.</returns>
			// Token: 0x06000CEB RID: 3307 RVA: 0x000356D8 File Offset: 0x000338D8
			public override int GetHashCode()
			{
				return this.row ^ this.column;
			}

			/// <summary>Gets the type, row number, and column number.</summary>
			/// <returns>The type, row number, and column number.</returns>
			// Token: 0x06000CEC RID: 3308 RVA: 0x000356E8 File Offset: 0x000338E8
			public override string ToString()
			{
				return string.Concat(new object[] { "{ ", this.type, ",", this.row, ",", this.column, "}" });
			}

			/// <summary>Indicates that a coordinate corresponds to part of the <see cref="T:System.Windows.Forms.DataGrid" /> control that is not functioning.</summary>
			// Token: 0x04000942 RID: 2370
			public static readonly DataGrid.HitTestInfo Nowhere;

			// Token: 0x04000943 RID: 2371
			private int row;

			// Token: 0x04000944 RID: 2372
			private int column;

			// Token: 0x04000945 RID: 2373
			private DataGrid.HitTestType type;
		}
	}
}
