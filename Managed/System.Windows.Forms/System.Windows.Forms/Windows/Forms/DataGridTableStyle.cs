using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace System.Windows.Forms
{
	/// <summary>Represents the table drawn by the <see cref="T:System.Windows.Forms.DataGrid" /> control at run time.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000CC RID: 204
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	public class DataGridTableStyle : Component, IDataGridEditingService
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> class.</summary>
		// Token: 0x06000D73 RID: 3443 RVA: 0x00036860 File Offset: 0x00034A60
		public DataGridTableStyle()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> class using the specified value to determine whether the grid table is the default style.</summary>
		/// <param name="isDefaultTableStyle">true to specify the table as the default; otherwise, false. </param>
		// Token: 0x06000D74 RID: 3444 RVA: 0x0003686C File Offset: 0x00034A6C
		public DataGridTableStyle(bool isDefaultTableStyle)
		{
			this.is_default = isDefaultTableStyle;
			this.allow_sorting = true;
			this.datagrid = null;
			this.header_forecolor = DataGridTableStyle.def_header_forecolor;
			this.mapping_name = string.Empty;
			this.table_relations = new ArrayList();
			this.column_styles = new GridColumnStylesCollection(this);
			this.alternating_backcolor = DataGridTableStyle.def_alternating_backcolor;
			this.columnheaders_visible = true;
			this.gridline_color = DataGridTableStyle.def_gridline_color;
			this.gridline_style = DataGridLineStyle.Solid;
			this.header_backcolor = DataGridTableStyle.def_header_backcolor;
			this.header_font = null;
			this.link_color = DataGridTableStyle.def_link_color;
			this.link_hovercolor = DataGridTableStyle.def_link_hovercolor;
			this.preferredcolumn_width = ThemeEngine.Current.DataGridPreferredColumnWidth;
			this.preferredrow_height = ThemeEngine.Current.DefaultFont.Height + 3;
			this._readonly = false;
			this.rowheaders_visible = true;
			this.selection_backcolor = DataGridTableStyle.def_selection_backcolor;
			this.selection_forecolor = DataGridTableStyle.def_selection_forecolor;
			this.rowheaders_width = 35;
			this.backcolor = DataGridTableStyle.def_backcolor;
			this.forecolor = DataGridTableStyle.def_forecolor;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> class with the specified <see cref="T:System.Windows.Forms.CurrencyManager" />.</summary>
		/// <param name="listManager">The <see cref="T:System.Windows.Forms.CurrencyManager" /> to use. </param>
		// Token: 0x06000D75 RID: 3445 RVA: 0x00036978 File Offset: 0x00034B78
		public DataGridTableStyle(CurrencyManager listManager)
			: this(false)
		{
			this.manager = listManager;
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x00036988 File Offset: 0x00034B88
		// Note: this type is marked as 'beforefieldinit'.
		static DataGridTableStyle()
		{
			DataGridTableStyle.AllowSortingChangedEvent = new object();
			DataGridTableStyle.AlternatingBackColorChangedEvent = new object();
			DataGridTableStyle.BackColorChangedEvent = new object();
			DataGridTableStyle.ColumnHeadersVisibleChangedEvent = new object();
			DataGridTableStyle.ForeColorChangedEvent = new object();
			DataGridTableStyle.GridLineColorChangedEvent = new object();
			DataGridTableStyle.GridLineStyleChangedEvent = new object();
			DataGridTableStyle.HeaderBackColorChangedEvent = new object();
			DataGridTableStyle.HeaderFontChangedEvent = new object();
			DataGridTableStyle.HeaderForeColorChangedEvent = new object();
			DataGridTableStyle.LinkColorChangedEvent = new object();
			DataGridTableStyle.LinkHoverColorChangedEvent = new object();
			DataGridTableStyle.MappingNameChangedEvent = new object();
			DataGridTableStyle.PreferredColumnWidthChangedEvent = new object();
			DataGridTableStyle.PreferredRowHeightChangedEvent = new object();
			DataGridTableStyle.ReadOnlyChangedEvent = new object();
			DataGridTableStyle.RowHeadersVisibleChangedEvent = new object();
			DataGridTableStyle.RowHeaderWidthChangedEvent = new object();
			DataGridTableStyle.SelectionBackColorChangedEvent = new object();
			DataGridTableStyle.SelectionForeColorChangedEvent = new object();
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.AllowSorting" /> property value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000D7 RID: 215
		// (add) Token: 0x06000D77 RID: 3447 RVA: 0x00036B20 File Offset: 0x00034D20
		// (remove) Token: 0x06000D78 RID: 3448 RVA: 0x00036B34 File Offset: 0x00034D34
		public event EventHandler AllowSortingChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.AllowSortingChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.AllowSortingChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.AlternatingBackColor" /> value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000D8 RID: 216
		// (add) Token: 0x06000D79 RID: 3449 RVA: 0x00036B48 File Offset: 0x00034D48
		// (remove) Token: 0x06000D7A RID: 3450 RVA: 0x00036B5C File Offset: 0x00034D5C
		public event EventHandler AlternatingBackColorChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.AlternatingBackColorChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.AlternatingBackColorChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.BackColor" /> value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000D9 RID: 217
		// (add) Token: 0x06000D7B RID: 3451 RVA: 0x00036B70 File Offset: 0x00034D70
		// (remove) Token: 0x06000D7C RID: 3452 RVA: 0x00036B84 File Offset: 0x00034D84
		public event EventHandler BackColorChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.BackColorChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.BackColorChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.ColumnHeadersVisible" /> value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000DA RID: 218
		// (add) Token: 0x06000D7D RID: 3453 RVA: 0x00036B98 File Offset: 0x00034D98
		// (remove) Token: 0x06000D7E RID: 3454 RVA: 0x00036BAC File Offset: 0x00034DAC
		public event EventHandler ColumnHeadersVisibleChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.ColumnHeadersVisibleChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.ColumnHeadersVisibleChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.ForeColor" /> value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000DB RID: 219
		// (add) Token: 0x06000D7F RID: 3455 RVA: 0x00036BC0 File Offset: 0x00034DC0
		// (remove) Token: 0x06000D80 RID: 3456 RVA: 0x00036BD4 File Offset: 0x00034DD4
		public event EventHandler ForeColorChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.ForeColorChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.ForeColorChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.GridLineColor" /> value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000DC RID: 220
		// (add) Token: 0x06000D81 RID: 3457 RVA: 0x00036BE8 File Offset: 0x00034DE8
		// (remove) Token: 0x06000D82 RID: 3458 RVA: 0x00036BFC File Offset: 0x00034DFC
		public event EventHandler GridLineColorChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.GridLineColorChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.GridLineColorChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.GridLineStyle" /> value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000DD RID: 221
		// (add) Token: 0x06000D83 RID: 3459 RVA: 0x00036C10 File Offset: 0x00034E10
		// (remove) Token: 0x06000D84 RID: 3460 RVA: 0x00036C24 File Offset: 0x00034E24
		public event EventHandler GridLineStyleChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.GridLineStyleChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.GridLineStyleChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.HeaderBackColor" /> value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000DE RID: 222
		// (add) Token: 0x06000D85 RID: 3461 RVA: 0x00036C38 File Offset: 0x00034E38
		// (remove) Token: 0x06000D86 RID: 3462 RVA: 0x00036C4C File Offset: 0x00034E4C
		public event EventHandler HeaderBackColorChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.HeaderBackColorChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.HeaderBackColorChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.HeaderFont" /> value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000DF RID: 223
		// (add) Token: 0x06000D87 RID: 3463 RVA: 0x00036C60 File Offset: 0x00034E60
		// (remove) Token: 0x06000D88 RID: 3464 RVA: 0x00036C74 File Offset: 0x00034E74
		public event EventHandler HeaderFontChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.HeaderFontChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.HeaderFontChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.HeaderForeColor" /> value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000E0 RID: 224
		// (add) Token: 0x06000D89 RID: 3465 RVA: 0x00036C88 File Offset: 0x00034E88
		// (remove) Token: 0x06000D8A RID: 3466 RVA: 0x00036C9C File Offset: 0x00034E9C
		public event EventHandler HeaderForeColorChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.HeaderForeColorChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.HeaderForeColorChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.LinkColor" /> value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000E1 RID: 225
		// (add) Token: 0x06000D8B RID: 3467 RVA: 0x00036CB0 File Offset: 0x00034EB0
		// (remove) Token: 0x06000D8C RID: 3468 RVA: 0x00036CC4 File Offset: 0x00034EC4
		public event EventHandler LinkColorChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.LinkColorChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.LinkColorChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.LinkHoverColor" /> value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000E2 RID: 226
		// (add) Token: 0x06000D8D RID: 3469 RVA: 0x00036CD8 File Offset: 0x00034ED8
		// (remove) Token: 0x06000D8E RID: 3470 RVA: 0x00036CEC File Offset: 0x00034EEC
		public event EventHandler LinkHoverColorChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.LinkHoverColorChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.LinkHoverColorChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.MappingName" /> value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000E3 RID: 227
		// (add) Token: 0x06000D8F RID: 3471 RVA: 0x00036D00 File Offset: 0x00034F00
		// (remove) Token: 0x06000D90 RID: 3472 RVA: 0x00036D14 File Offset: 0x00034F14
		public event EventHandler MappingNameChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.MappingNameChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.MappingNameChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.PreferredColumnWidth" /> property value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000E4 RID: 228
		// (add) Token: 0x06000D91 RID: 3473 RVA: 0x00036D28 File Offset: 0x00034F28
		// (remove) Token: 0x06000D92 RID: 3474 RVA: 0x00036D3C File Offset: 0x00034F3C
		public event EventHandler PreferredColumnWidthChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.PreferredColumnWidthChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.PreferredColumnWidthChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.PreferredRowHeight" /> value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000E5 RID: 229
		// (add) Token: 0x06000D93 RID: 3475 RVA: 0x00036D50 File Offset: 0x00034F50
		// (remove) Token: 0x06000D94 RID: 3476 RVA: 0x00036D64 File Offset: 0x00034F64
		public event EventHandler PreferredRowHeightChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.PreferredRowHeightChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.PreferredRowHeightChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.ReadOnly" /> value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000E6 RID: 230
		// (add) Token: 0x06000D95 RID: 3477 RVA: 0x00036D78 File Offset: 0x00034F78
		// (remove) Token: 0x06000D96 RID: 3478 RVA: 0x00036D8C File Offset: 0x00034F8C
		public event EventHandler ReadOnlyChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.ReadOnlyChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.ReadOnlyChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.RowHeadersVisible" /> value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000E7 RID: 231
		// (add) Token: 0x06000D97 RID: 3479 RVA: 0x00036DA0 File Offset: 0x00034FA0
		// (remove) Token: 0x06000D98 RID: 3480 RVA: 0x00036DB4 File Offset: 0x00034FB4
		public event EventHandler RowHeadersVisibleChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.RowHeadersVisibleChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.RowHeadersVisibleChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.RowHeaderWidth" /> value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000E8 RID: 232
		// (add) Token: 0x06000D99 RID: 3481 RVA: 0x00036DC8 File Offset: 0x00034FC8
		// (remove) Token: 0x06000D9A RID: 3482 RVA: 0x00036DDC File Offset: 0x00034FDC
		public event EventHandler RowHeaderWidthChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.RowHeaderWidthChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.RowHeaderWidthChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.SelectionBackColor" /> value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000E9 RID: 233
		// (add) Token: 0x06000D9B RID: 3483 RVA: 0x00036DF0 File Offset: 0x00034FF0
		// (remove) Token: 0x06000D9C RID: 3484 RVA: 0x00036E04 File Offset: 0x00035004
		public event EventHandler SelectionBackColorChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.SelectionBackColorChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.SelectionBackColorChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.SelectionForeColor" /> value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000EA RID: 234
		// (add) Token: 0x06000D9D RID: 3485 RVA: 0x00036E18 File Offset: 0x00035018
		// (remove) Token: 0x06000D9E RID: 3486 RVA: 0x00036E2C File Offset: 0x0003502C
		public event EventHandler SelectionForeColorChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.SelectionForeColorChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.SelectionForeColorChangedEvent, value);
			}
		}

		/// <summary>Indicates whether sorting is allowed on the grid table when this <see cref="T:System.Windows.Forms.DataGridTableStyle" /> is used.</summary>
		/// <returns>true if sorting is allowed; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x00036E40 File Offset: 0x00035040
		// (set) Token: 0x06000DA0 RID: 3488 RVA: 0x00036E48 File Offset: 0x00035048
		[DefaultValue(true)]
		public bool AllowSorting
		{
			get
			{
				return this.allow_sorting;
			}
			set
			{
				if (this.is_default)
				{
					throw new ArgumentException("Cannot change the value of this property on the default DataGridTableStyle.");
				}
				if (this.allow_sorting != value)
				{
					this.allow_sorting = value;
					this.OnAllowSortingChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the background color of odd-numbered rows of the grid.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of odd-numbered rows. The default is <see cref="P:System.Drawing.SystemBrushes.Window" /></returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x00036E8C File Offset: 0x0003508C
		// (set) Token: 0x06000DA2 RID: 3490 RVA: 0x00036E94 File Offset: 0x00035094
		public Color AlternatingBackColor
		{
			get
			{
				return this.alternating_backcolor;
			}
			set
			{
				if (this.is_default)
				{
					throw new ArgumentException("Cannot change the value of this property on the default DataGridTableStyle.");
				}
				if (this.alternating_backcolor != value)
				{
					this.alternating_backcolor = value;
					this.OnAlternatingBackColorChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the background color of even-numbered rows of the grid.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of odd-numbered rows.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x00036ED0 File Offset: 0x000350D0
		// (set) Token: 0x06000DA4 RID: 3492 RVA: 0x00036ED8 File Offset: 0x000350D8
		public Color BackColor
		{
			get
			{
				return this.backcolor;
			}
			set
			{
				if (this.is_default)
				{
					throw new ArgumentException("Cannot change the value of this property on the default DataGridTableStyle.");
				}
				if (this.backcolor != value)
				{
					this.backcolor = value;
					this.OnForeColorChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether column headers are visible.</summary>
		/// <returns>true if column headers are visible; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x00036F14 File Offset: 0x00035114
		// (set) Token: 0x06000DA6 RID: 3494 RVA: 0x00036F1C File Offset: 0x0003511C
		[DefaultValue(true)]
		public bool ColumnHeadersVisible
		{
			get
			{
				return this.columnheaders_visible;
			}
			set
			{
				if (this.columnheaders_visible != value)
				{
					this.columnheaders_visible = value;
					this.OnColumnHeadersVisibleChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.DataGrid" /> control for the drawn table.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGrid" /> control that displays the table.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x00036F3C File Offset: 0x0003513C
		// (set) Token: 0x06000DA8 RID: 3496 RVA: 0x00036F44 File Offset: 0x00035144
		[Browsable(false)]
		public virtual DataGrid DataGrid
		{
			get
			{
				return this.datagrid;
			}
			set
			{
				if (this.datagrid != value)
				{
					this.datagrid = value;
					for (int i = 0; i < this.column_styles.Count; i++)
					{
						this.column_styles[i].SetDataGridInternal(this.datagrid);
					}
				}
			}
		}

		/// <summary>Gets or sets the foreground color of the grid table.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the grid table.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x00036F98 File Offset: 0x00035198
		// (set) Token: 0x06000DAA RID: 3498 RVA: 0x00036FA0 File Offset: 0x000351A0
		public Color ForeColor
		{
			get
			{
				return this.forecolor;
			}
			set
			{
				if (this.is_default)
				{
					throw new ArgumentException("Cannot change the value of this property on the default DataGridTableStyle.");
				}
				if (this.forecolor != value)
				{
					this.forecolor = value;
					this.OnBackColorChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets the collection of columns drawn for this table.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.GridColumnStylesCollection" /> that contains all <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> objects for the table.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x00036FDC File Offset: 0x000351DC
		[Localizable(true)]
		[DesignerSerializationVisibility(2)]
		public virtual GridColumnStylesCollection GridColumnStyles
		{
			get
			{
				return this.column_styles;
			}
		}

		/// <summary>Gets or sets the color of grid lines.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the grid line color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x00036FE4 File Offset: 0x000351E4
		// (set) Token: 0x06000DAD RID: 3501 RVA: 0x00036FEC File Offset: 0x000351EC
		public Color GridLineColor
		{
			get
			{
				return this.gridline_color;
			}
			set
			{
				if (this.is_default)
				{
					throw new ArgumentException("Cannot change the value of this property on the default DataGridTableStyle.");
				}
				if (this.gridline_color != value)
				{
					this.gridline_color = value;
					this.OnGridLineColorChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the style of grid lines.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridLineStyle" /> values. The default is DataGridLineStyle.Solid.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x00037028 File Offset: 0x00035228
		// (set) Token: 0x06000DAF RID: 3503 RVA: 0x00037030 File Offset: 0x00035230
		[DefaultValue(DataGridLineStyle.Solid)]
		public DataGridLineStyle GridLineStyle
		{
			get
			{
				return this.gridline_style;
			}
			set
			{
				if (this.is_default)
				{
					throw new ArgumentException("Cannot change the value of this property on the default DataGridTableStyle.");
				}
				if (this.gridline_style != value)
				{
					this.gridline_style = value;
					this.OnGridLineStyleChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the background color of headers.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of headers.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x00037074 File Offset: 0x00035274
		// (set) Token: 0x06000DB1 RID: 3505 RVA: 0x0003707C File Offset: 0x0003527C
		public Color HeaderBackColor
		{
			get
			{
				return this.header_backcolor;
			}
			set
			{
				if (this.is_default)
				{
					throw new ArgumentException("Cannot change the value of this property on the default DataGridTableStyle.");
				}
				if (value == Color.Empty)
				{
					throw new ArgumentNullException("Color.Empty value is invalid.");
				}
				if (this.header_backcolor != value)
				{
					this.header_backcolor = value;
					this.OnHeaderBackColorChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the font used for header captions.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> used for captions.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x000370E0 File Offset: 0x000352E0
		// (set) Token: 0x06000DB3 RID: 3507 RVA: 0x0003711C File Offset: 0x0003531C
		[AmbientValue(null)]
		[Localizable(true)]
		public Font HeaderFont
		{
			get
			{
				if (this.header_font != null)
				{
					return this.header_font;
				}
				if (this.DataGrid != null)
				{
					return this.DataGrid.Font;
				}
				return DataGridTableStyle.def_header_font;
			}
			set
			{
				if (this.is_default)
				{
					throw new ArgumentException("Cannot change the value of this property on the default DataGridTableStyle.");
				}
				if (this.header_font != value)
				{
					this.header_font = value;
					this.OnHeaderFontChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the foreground color of headers.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of headers.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000DB4 RID: 3508 RVA: 0x00037160 File Offset: 0x00035360
		// (set) Token: 0x06000DB5 RID: 3509 RVA: 0x00037168 File Offset: 0x00035368
		public Color HeaderForeColor
		{
			get
			{
				return this.header_forecolor;
			}
			set
			{
				if (this.is_default)
				{
					throw new ArgumentException("Cannot change the value of this property on the default DataGridTableStyle.");
				}
				if (this.header_forecolor != value)
				{
					this.header_forecolor = value;
					this.OnHeaderForeColorChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the color of link text.</summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> of link text.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x000371A4 File Offset: 0x000353A4
		// (set) Token: 0x06000DB7 RID: 3511 RVA: 0x000371AC File Offset: 0x000353AC
		public Color LinkColor
		{
			get
			{
				return this.link_color;
			}
			set
			{
				if (this.is_default)
				{
					throw new ArgumentException("Cannot change the value of this property on the default DataGridTableStyle.");
				}
				if (this.link_color != value)
				{
					this.link_color = value;
					this.OnLinkColorChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the color displayed when hovering over link text.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the hover color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x000371E8 File Offset: 0x000353E8
		// (set) Token: 0x06000DB9 RID: 3513 RVA: 0x000371F0 File Offset: 0x000353F0
		[Browsable(false)]
		[EditorBrowsable(1)]
		public Color LinkHoverColor
		{
			get
			{
				return this.link_hovercolor;
			}
			set
			{
				if (this.link_hovercolor != value)
				{
					this.link_hovercolor = value;
				}
			}
		}

		/// <summary>Gets or sets the name used to map this table to a specific data source.</summary>
		/// <returns>The name used to map this grid to a specific data source.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000DBA RID: 3514 RVA: 0x0003720C File Offset: 0x0003540C
		// (set) Token: 0x06000DBB RID: 3515 RVA: 0x00037214 File Offset: 0x00035414
		[DefaultValue("")]
		[Editor("System.Windows.Forms.Design.DataGridTableStyleMappingNameEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string MappingName
		{
			get
			{
				return this.mapping_name;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (this.mapping_name != value)
				{
					this.mapping_name = value;
					this.OnMappingNameChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the width used to create columns when a new grid is displayed.</summary>
		/// <returns>The width used to create columns when a new grid is displayed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000DBC RID: 3516 RVA: 0x00037254 File Offset: 0x00035454
		// (set) Token: 0x06000DBD RID: 3517 RVA: 0x0003725C File Offset: 0x0003545C
		[DefaultValue(75)]
		[Localizable(true)]
		[TypeConverter(typeof(DataGridPreferredColumnWidthTypeConverter))]
		public int PreferredColumnWidth
		{
			get
			{
				return this.preferredcolumn_width;
			}
			set
			{
				if (this.is_default)
				{
					throw new ArgumentException("Cannot change the value of this property on the default DataGridTableStyle.");
				}
				if (value < 0)
				{
					throw new ArgumentException("PreferredColumnWidth is less than 0");
				}
				if (this.preferredcolumn_width != value)
				{
					this.preferredcolumn_width = value;
					this.OnPreferredColumnWidthChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the height used to create a row when a new grid is displayed.</summary>
		/// <returns>The height of a row, in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000DBE RID: 3518 RVA: 0x000372B0 File Offset: 0x000354B0
		// (set) Token: 0x06000DBF RID: 3519 RVA: 0x000372B8 File Offset: 0x000354B8
		[Localizable(true)]
		public int PreferredRowHeight
		{
			get
			{
				return this.preferredrow_height;
			}
			set
			{
				if (this.is_default)
				{
					throw new ArgumentException("Cannot change the value of this property on the default DataGridTableStyle.");
				}
				if (this.preferredrow_height != value)
				{
					this.preferredrow_height = value;
					this.OnPreferredRowHeightChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether columns can be edited.</summary>
		/// <returns>true, if columns cannot be edited; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x000372FC File Offset: 0x000354FC
		// (set) Token: 0x06000DC1 RID: 3521 RVA: 0x00037304 File Offset: 0x00035504
		[DefaultValue(false)]
		public virtual bool ReadOnly
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
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether row headers are visible.</summary>
		/// <returns>true if row headers are visible; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x00037324 File Offset: 0x00035524
		// (set) Token: 0x06000DC3 RID: 3523 RVA: 0x0003732C File Offset: 0x0003552C
		[DefaultValue(true)]
		public bool RowHeadersVisible
		{
			get
			{
				return this.rowheaders_visible;
			}
			set
			{
				if (this.rowheaders_visible != value)
				{
					this.rowheaders_visible = value;
					this.OnRowHeadersVisibleChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the width of row headers.</summary>
		/// <returns>The width of row headers, in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x0003734C File Offset: 0x0003554C
		// (set) Token: 0x06000DC5 RID: 3525 RVA: 0x00037354 File Offset: 0x00035554
		[Localizable(true)]
		[DefaultValue(35)]
		public int RowHeaderWidth
		{
			get
			{
				return this.rowheaders_width;
			}
			set
			{
				if (this.rowheaders_width != value)
				{
					this.rowheaders_width = value;
					this.OnRowHeaderWidthChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the background color of selected cells.</summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> that represents the background color of selected cells.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x00037374 File Offset: 0x00035574
		// (set) Token: 0x06000DC7 RID: 3527 RVA: 0x0003737C File Offset: 0x0003557C
		public Color SelectionBackColor
		{
			get
			{
				return this.selection_backcolor;
			}
			set
			{
				if (this.is_default)
				{
					throw new ArgumentException("Cannot change the value of this property on the default DataGridTableStyle.");
				}
				if (this.selection_backcolor != value)
				{
					this.selection_backcolor = value;
					this.OnSelectionBackColorChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the foreground color of selected cells.</summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> that represents the foreground color of selected cells.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x000373B8 File Offset: 0x000355B8
		// (set) Token: 0x06000DC9 RID: 3529 RVA: 0x000373C0 File Offset: 0x000355C0
		[Description("The foreground color for the current data grid row")]
		public Color SelectionForeColor
		{
			get
			{
				return this.selection_forecolor;
			}
			set
			{
				if (this.is_default)
				{
					throw new ArgumentException("Cannot change the value of this property on the default DataGridTableStyle.");
				}
				if (this.selection_forecolor != value)
				{
					this.selection_forecolor = value;
					this.OnSelectionForeColorChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x000373FC File Offset: 0x000355FC
		internal DataGridLineStyle CurrentGridLineStyle
		{
			get
			{
				if (this.is_default && this.datagrid != null)
				{
					return this.datagrid.GridLineStyle;
				}
				return this.gridline_style;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x00037434 File Offset: 0x00035634
		internal Color CurrentGridLineColor
		{
			get
			{
				if (this.is_default && this.datagrid != null)
				{
					return this.datagrid.GridLineColor;
				}
				return this.gridline_color;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x0003746C File Offset: 0x0003566C
		internal Color CurrentHeaderBackColor
		{
			get
			{
				if (this.is_default && this.datagrid != null)
				{
					return this.datagrid.HeaderBackColor;
				}
				return this.header_backcolor;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000DCD RID: 3533 RVA: 0x000374A4 File Offset: 0x000356A4
		internal Color CurrentHeaderForeColor
		{
			get
			{
				if (this.is_default && this.datagrid != null)
				{
					return this.datagrid.HeaderForeColor;
				}
				return this.header_forecolor;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x000374DC File Offset: 0x000356DC
		internal int CurrentPreferredColumnWidth
		{
			get
			{
				if (this.is_default && this.datagrid != null)
				{
					return this.datagrid.PreferredColumnWidth;
				}
				return this.preferredcolumn_width;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000DCF RID: 3535 RVA: 0x00037514 File Offset: 0x00035714
		internal int CurrentPreferredRowHeight
		{
			get
			{
				if (this.is_default && this.datagrid != null)
				{
					return this.datagrid.PreferredRowHeight;
				}
				return this.preferredrow_height;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x0003754C File Offset: 0x0003574C
		internal bool CurrentRowHeadersVisible
		{
			get
			{
				if (this.is_default && this.datagrid != null)
				{
					return this.datagrid.RowHeadersVisible;
				}
				return this.rowheaders_visible;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x00037584 File Offset: 0x00035784
		internal bool HasRelations
		{
			get
			{
				return this.table_relations.Count > 0;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x00037594 File Offset: 0x00035794
		internal string[] Relations
		{
			get
			{
				string[] array = new string[this.table_relations.Count];
				this.table_relations.CopyTo(array, 0);
				return array;
			}
		}

		/// <summary>Requests an edit operation.</summary>
		/// <returns>true, if the operation succeeds; otherwise, false.</returns>
		/// <param name="gridColumn">The <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to edit. </param>
		/// <param name="rowNumber">The number of the edited row. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000DD3 RID: 3539 RVA: 0x000375C0 File Offset: 0x000357C0
		[MonoTODO("Not implemented, will throw NotImplementedException")]
		public bool BeginEdit(DataGridColumnStyle gridColumn, int rowNumber)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a <see cref="T:System.Windows.Forms.DataGridColumnStyle" />, using the specified property descriptor.</summary>
		/// <returns>The newly created <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</returns>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> used to create the column style object. </param>
		// Token: 0x06000DD4 RID: 3540 RVA: 0x000375C8 File Offset: 0x000357C8
		protected internal virtual DataGridColumnStyle CreateGridColumn(PropertyDescriptor prop)
		{
			return this.CreateGridColumn(prop, false);
		}

		/// <summary>Creates a <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> using the specified property descriptor. Specifies whether the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> is a default column style.</summary>
		/// <returns>The newly created <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</returns>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> used to create the column style object. </param>
		/// <param name="isDefault">Specifies whether the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> is a default column style. This parameter is read-only. </param>
		// Token: 0x06000DD5 RID: 3541 RVA: 0x000375D4 File Offset: 0x000357D4
		protected internal virtual DataGridColumnStyle CreateGridColumn(PropertyDescriptor prop, bool isDefault)
		{
			if (prop.PropertyType == typeof(bool))
			{
				return new DataGridBoolColumn(prop, isDefault);
			}
			if (prop.PropertyType.Equals(typeof(DateTime)))
			{
				return new DataGridTextBoxColumn(prop, "d", isDefault);
			}
			if (prop.PropertyType.Equals(typeof(int)) || prop.PropertyType.Equals(typeof(short)))
			{
				return new DataGridTextBoxColumn(prop, "G", isDefault);
			}
			return new DataGridTextBoxColumn(prop, isDefault);
		}

		/// <summary>Disposes of the resources (other than memory) used by the <see cref="T:System.Windows.Forms.DataGridTableStyle" />.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06000DD6 RID: 3542 RVA: 0x00037670 File Offset: 0x00035870
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		/// <summary>Requests an end to an edit operation.</summary>
		/// <returns>true if the edit operation ends successfully; otherwise, false.</returns>
		/// <param name="gridColumn">The <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to edit. </param>
		/// <param name="rowNumber">The number of the edited row. </param>
		/// <param name="shouldAbort">A value indicating whether the operation should be stopped; true if it should stop; otherwise, false. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000DD7 RID: 3543 RVA: 0x0003767C File Offset: 0x0003587C
		[MonoTODO("Not implemented, will throw NotImplementedException")]
		public bool EndEdit(DataGridColumnStyle gridColumn, int rowNumber, bool shouldAbort)
		{
			throw new NotImplementedException();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.AllowSortingChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000DD8 RID: 3544 RVA: 0x00037684 File Offset: 0x00035884
		protected virtual void OnAllowSortingChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridTableStyle.AllowSortingChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.AlternatingBackColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000DD9 RID: 3545 RVA: 0x000376B8 File Offset: 0x000358B8
		protected virtual void OnAlternatingBackColorChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridTableStyle.AlternatingBackColorChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.BackColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000DDA RID: 3546 RVA: 0x000376EC File Offset: 0x000358EC
		protected virtual void OnBackColorChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridTableStyle.BackColorChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.ColumnHeadersVisibleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000DDB RID: 3547 RVA: 0x00037720 File Offset: 0x00035920
		protected virtual void OnColumnHeadersVisibleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridTableStyle.ColumnHeadersVisibleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.ForeColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000DDC RID: 3548 RVA: 0x00037754 File Offset: 0x00035954
		protected virtual void OnForeColorChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridTableStyle.ForeColorChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.GridLineColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000DDD RID: 3549 RVA: 0x00037788 File Offset: 0x00035988
		protected virtual void OnGridLineColorChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridTableStyle.GridLineColorChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.GridLineStyleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000DDE RID: 3550 RVA: 0x000377BC File Offset: 0x000359BC
		protected virtual void OnGridLineStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridTableStyle.GridLineStyleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.HeaderBackColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000DDF RID: 3551 RVA: 0x000377F0 File Offset: 0x000359F0
		protected virtual void OnHeaderBackColorChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridTableStyle.HeaderBackColorChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.HeaderFontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000DE0 RID: 3552 RVA: 0x00037824 File Offset: 0x00035A24
		protected virtual void OnHeaderFontChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridTableStyle.HeaderFontChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.HeaderForeColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000DE1 RID: 3553 RVA: 0x00037858 File Offset: 0x00035A58
		protected virtual void OnHeaderForeColorChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridTableStyle.HeaderForeColorChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.LinkColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000DE2 RID: 3554 RVA: 0x0003788C File Offset: 0x00035A8C
		protected virtual void OnLinkColorChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridTableStyle.LinkColorChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the LinkHoverColorChanged event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000DE3 RID: 3555 RVA: 0x000378C0 File Offset: 0x00035AC0
		protected virtual void OnLinkHoverColorChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridTableStyle.LinkHoverColorChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.MappingNameChanged" /> event </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000DE4 RID: 3556 RVA: 0x000378F4 File Offset: 0x00035AF4
		protected virtual void OnMappingNameChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridTableStyle.MappingNameChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.PreferredColumnWidthChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000DE5 RID: 3557 RVA: 0x00037928 File Offset: 0x00035B28
		protected virtual void OnPreferredColumnWidthChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridTableStyle.PreferredColumnWidthChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.PreferredRowHeightChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000DE6 RID: 3558 RVA: 0x0003795C File Offset: 0x00035B5C
		protected virtual void OnPreferredRowHeightChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridTableStyle.PreferredRowHeightChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.ReadOnlyChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000DE7 RID: 3559 RVA: 0x00037990 File Offset: 0x00035B90
		protected virtual void OnReadOnlyChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridTableStyle.ReadOnlyChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.RowHeadersVisibleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000DE8 RID: 3560 RVA: 0x000379C4 File Offset: 0x00035BC4
		protected virtual void OnRowHeadersVisibleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridTableStyle.RowHeadersVisibleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.RowHeaderWidthChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000DE9 RID: 3561 RVA: 0x000379F8 File Offset: 0x00035BF8
		protected virtual void OnRowHeaderWidthChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridTableStyle.RowHeaderWidthChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.SelectionBackColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000DEA RID: 3562 RVA: 0x00037A2C File Offset: 0x00035C2C
		protected virtual void OnSelectionBackColorChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridTableStyle.SelectionBackColorChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.SelectionForeColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000DEB RID: 3563 RVA: 0x00037A60 File Offset: 0x00035C60
		protected virtual void OnSelectionForeColorChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridTableStyle.SelectionForeColorChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridTableStyle.AlternatingBackColor" /> property to its default value.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000DEC RID: 3564 RVA: 0x00037A94 File Offset: 0x00035C94
		public void ResetAlternatingBackColor()
		{
			this.AlternatingBackColor = DataGridTableStyle.def_alternating_backcolor;
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridTableStyle.BackColor" /> property to its default value.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000DED RID: 3565 RVA: 0x00037AA4 File Offset: 0x00035CA4
		public void ResetBackColor()
		{
			this.BackColor = DataGridTableStyle.def_backcolor;
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridTableStyle.ForeColor" /> property to its default value.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000DEE RID: 3566 RVA: 0x00037AB4 File Offset: 0x00035CB4
		public void ResetForeColor()
		{
			this.ForeColor = DataGridTableStyle.def_forecolor;
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridTableStyle.GridLineColor" /> property to its default value.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000DEF RID: 3567 RVA: 0x00037AC4 File Offset: 0x00035CC4
		public void ResetGridLineColor()
		{
			this.GridLineColor = DataGridTableStyle.def_gridline_color;
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridTableStyle.HeaderBackColor" /> property to its default value.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000DF0 RID: 3568 RVA: 0x00037AD4 File Offset: 0x00035CD4
		public void ResetHeaderBackColor()
		{
			this.HeaderBackColor = DataGridTableStyle.def_header_backcolor;
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridTableStyle.HeaderFont" /> property to its default value.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DF1 RID: 3569 RVA: 0x00037AE4 File Offset: 0x00035CE4
		public void ResetHeaderFont()
		{
			this.HeaderFont = DataGridTableStyle.def_header_font;
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridTableStyle.HeaderForeColor" /> property to its default value.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000DF2 RID: 3570 RVA: 0x00037AF4 File Offset: 0x00035CF4
		public void ResetHeaderForeColor()
		{
			this.HeaderForeColor = DataGridTableStyle.def_header_forecolor;
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridTableStyle.LinkColor" /> property to its default value.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000DF3 RID: 3571 RVA: 0x00037B04 File Offset: 0x00035D04
		public void ResetLinkColor()
		{
			this.LinkColor = DataGridTableStyle.def_link_color;
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridTableStyle.LinkHoverColor" /> property to its default value.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DF4 RID: 3572 RVA: 0x00037B14 File Offset: 0x00035D14
		public void ResetLinkHoverColor()
		{
			this.LinkHoverColor = DataGridTableStyle.def_link_hovercolor;
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridTableStyle.SelectionBackColor" /> property to its default value.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000DF5 RID: 3573 RVA: 0x00037B24 File Offset: 0x00035D24
		public void ResetSelectionBackColor()
		{
			this.SelectionBackColor = DataGridTableStyle.def_selection_backcolor;
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridTableStyle.SelectionForeColor" /> property to its default value.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000DF6 RID: 3574 RVA: 0x00037B34 File Offset: 0x00035D34
		public void ResetSelectionForeColor()
		{
			this.SelectionForeColor = DataGridTableStyle.def_selection_forecolor;
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGridTableStyle.AlternatingBackColor" /> property should be persisted.</summary>
		/// <returns>true if the property value has changed from its default; otherwise, false.</returns>
		// Token: 0x06000DF7 RID: 3575 RVA: 0x00037B44 File Offset: 0x00035D44
		protected virtual bool ShouldSerializeAlternatingBackColor()
		{
			return this.alternating_backcolor != DataGridTableStyle.def_alternating_backcolor;
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGridTableStyle.BackColor" /> property should be persisted.</summary>
		/// <returns>true if the property value has changed from its default; otherwise, false.</returns>
		// Token: 0x06000DF8 RID: 3576 RVA: 0x00037B58 File Offset: 0x00035D58
		protected bool ShouldSerializeBackColor()
		{
			return this.backcolor != DataGridTableStyle.def_backcolor;
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGridTableStyle.ForeColor" /> property should be persisted.</summary>
		/// <returns>true if the property value has changed from its default; otherwise, false.</returns>
		// Token: 0x06000DF9 RID: 3577 RVA: 0x00037B6C File Offset: 0x00035D6C
		protected bool ShouldSerializeForeColor()
		{
			return this.forecolor != DataGridTableStyle.def_forecolor;
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGridTableStyle.GridLineColor" /> property should be persisted.</summary>
		/// <returns>true if the property value has changed from its default; otherwise, false.</returns>
		// Token: 0x06000DFA RID: 3578 RVA: 0x00037B80 File Offset: 0x00035D80
		protected virtual bool ShouldSerializeGridLineColor()
		{
			return this.gridline_color != DataGridTableStyle.def_gridline_color;
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGridTableStyle.HeaderBackColor" /> property should be persisted.</summary>
		/// <returns>true if the property value has changed from its default; otherwise, false.</returns>
		// Token: 0x06000DFB RID: 3579 RVA: 0x00037B94 File Offset: 0x00035D94
		protected virtual bool ShouldSerializeHeaderBackColor()
		{
			return this.header_backcolor != DataGridTableStyle.def_header_backcolor;
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGridTableStyle.HeaderForeColor" /> property should be persisted.</summary>
		/// <returns>true if the property value has changed from its default; otherwise, false.</returns>
		// Token: 0x06000DFC RID: 3580 RVA: 0x00037BA8 File Offset: 0x00035DA8
		protected virtual bool ShouldSerializeHeaderForeColor()
		{
			return this.header_forecolor != DataGridTableStyle.def_header_forecolor;
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGridTableStyle.LinkColor" /> property should be persisted.</summary>
		/// <returns>true if the property value has changed from its default; otherwise, false.</returns>
		// Token: 0x06000DFD RID: 3581 RVA: 0x00037BBC File Offset: 0x00035DBC
		protected virtual bool ShouldSerializeLinkColor()
		{
			return this.link_color != DataGridTableStyle.def_link_color;
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGridTableStyle.LinkHoverColor" /> property should be persisted.</summary>
		/// <returns>true if the property value has changed from its default; otherwise, false.</returns>
		// Token: 0x06000DFE RID: 3582 RVA: 0x00037BD0 File Offset: 0x00035DD0
		protected virtual bool ShouldSerializeLinkHoverColor()
		{
			return this.link_hovercolor != DataGridTableStyle.def_link_hovercolor;
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGridTableStyle.PreferredRowHeight" /> property should be persisted.</summary>
		/// <returns>true if the property value has changed from its default; otherwise, false.</returns>
		// Token: 0x06000DFF RID: 3583 RVA: 0x00037BE4 File Offset: 0x00035DE4
		protected bool ShouldSerializePreferredRowHeight()
		{
			return this.preferredrow_height != DataGridTableStyle.def_preferredrow_height;
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGridTableStyle.SelectionBackColor" /> property should be persisted.</summary>
		/// <returns>true if the property value has changed from its default; otherwise, false.</returns>
		// Token: 0x06000E00 RID: 3584 RVA: 0x00037BF8 File Offset: 0x00035DF8
		protected bool ShouldSerializeSelectionBackColor()
		{
			return this.selection_backcolor != DataGridTableStyle.def_selection_backcolor;
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGridTableStyle.SelectionForeColor" /> property should be persisted.</summary>
		/// <returns>true if the property value has changed from its default; otherwise, false.</returns>
		// Token: 0x06000E01 RID: 3585 RVA: 0x00037C0C File Offset: 0x00035E0C
		protected virtual bool ShouldSerializeSelectionForeColor()
		{
			return this.selection_forecolor != DataGridTableStyle.def_selection_forecolor;
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x00037C20 File Offset: 0x00035E20
		internal void CreateColumnsForTable(bool onlyBind)
		{
			CurrencyManager listManager = this.manager;
			if (listManager == null)
			{
				listManager = this.datagrid.ListManager;
				if (listManager == null)
				{
					return;
				}
			}
			for (int i = 0; i < this.column_styles.Count; i++)
			{
				this.column_styles[i].bound = false;
			}
			this.table_relations.Clear();
			PropertyDescriptorCollection itemProperties = listManager.GetItemProperties();
			for (int j = 0; j < itemProperties.Count; j++)
			{
				DataGridColumnStyle dataGridColumnStyle = this.column_styles[itemProperties[j].Name];
				if (dataGridColumnStyle != null)
				{
					if (dataGridColumnStyle.Width == -1)
					{
						dataGridColumnStyle.Width = this.CurrentPreferredColumnWidth;
					}
					dataGridColumnStyle.PropertyDescriptor = itemProperties[j];
					dataGridColumnStyle.bound = true;
				}
				else if (!onlyBind)
				{
					if (typeof(IBindingList).IsAssignableFrom(itemProperties[j].PropertyType))
					{
						this.table_relations.Add(itemProperties[j].Name);
					}
					else
					{
						dataGridColumnStyle = this.CreateGridColumn(itemProperties[j], true);
						dataGridColumnStyle.bound = true;
						dataGridColumnStyle.grid = this.datagrid;
						dataGridColumnStyle.MappingName = itemProperties[j].Name;
						dataGridColumnStyle.HeaderText = itemProperties[j].Name;
						dataGridColumnStyle.Width = this.CurrentPreferredColumnWidth;
						this.column_styles.Add(dataGridColumnStyle);
					}
				}
			}
		}

		/// <summary>Gets the default table style.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0400097C RID: 2428
		public static readonly DataGridTableStyle DefaultTableStyle = new DataGridTableStyle(true);

		// Token: 0x0400097D RID: 2429
		private static readonly Color def_alternating_backcolor = ThemeEngine.Current.DataGridAlternatingBackColor;

		// Token: 0x0400097E RID: 2430
		private static readonly Color def_backcolor = ThemeEngine.Current.DataGridBackColor;

		// Token: 0x0400097F RID: 2431
		private static readonly Color def_forecolor = SystemColors.WindowText;

		// Token: 0x04000980 RID: 2432
		private static readonly Color def_gridline_color = ThemeEngine.Current.DataGridGridLineColor;

		// Token: 0x04000981 RID: 2433
		private static readonly Color def_header_backcolor = ThemeEngine.Current.DataGridHeaderBackColor;

		// Token: 0x04000982 RID: 2434
		private static readonly Font def_header_font = ThemeEngine.Current.DefaultFont;

		// Token: 0x04000983 RID: 2435
		private static readonly Color def_header_forecolor = ThemeEngine.Current.DataGridHeaderForeColor;

		// Token: 0x04000984 RID: 2436
		private static readonly Color def_link_color = ThemeEngine.Current.DataGridLinkColor;

		// Token: 0x04000985 RID: 2437
		private static readonly Color def_link_hovercolor = ThemeEngine.Current.DataGridLinkHoverColor;

		// Token: 0x04000986 RID: 2438
		private static readonly Color def_selection_backcolor = ThemeEngine.Current.DataGridSelectionBackColor;

		// Token: 0x04000987 RID: 2439
		private static readonly Color def_selection_forecolor = ThemeEngine.Current.DataGridSelectionForeColor;

		// Token: 0x04000988 RID: 2440
		private static readonly int def_preferredrow_height = ThemeEngine.Current.DefaultFont.Height + 3;

		// Token: 0x04000989 RID: 2441
		private bool allow_sorting;

		// Token: 0x0400098A RID: 2442
		private DataGrid datagrid;

		// Token: 0x0400098B RID: 2443
		private Color header_forecolor;

		// Token: 0x0400098C RID: 2444
		private string mapping_name;

		// Token: 0x0400098D RID: 2445
		private Color alternating_backcolor;

		// Token: 0x0400098E RID: 2446
		private bool columnheaders_visible;

		// Token: 0x0400098F RID: 2447
		private GridColumnStylesCollection column_styles;

		// Token: 0x04000990 RID: 2448
		private Color gridline_color;

		// Token: 0x04000991 RID: 2449
		private DataGridLineStyle gridline_style;

		// Token: 0x04000992 RID: 2450
		private Color header_backcolor;

		// Token: 0x04000993 RID: 2451
		private Font header_font;

		// Token: 0x04000994 RID: 2452
		private Color link_color;

		// Token: 0x04000995 RID: 2453
		private Color link_hovercolor;

		// Token: 0x04000996 RID: 2454
		private int preferredcolumn_width;

		// Token: 0x04000997 RID: 2455
		private int preferredrow_height;

		// Token: 0x04000998 RID: 2456
		private bool _readonly;

		// Token: 0x04000999 RID: 2457
		private bool rowheaders_visible;

		// Token: 0x0400099A RID: 2458
		private Color selection_backcolor;

		// Token: 0x0400099B RID: 2459
		private Color selection_forecolor;

		// Token: 0x0400099C RID: 2460
		private int rowheaders_width;

		// Token: 0x0400099D RID: 2461
		private Color backcolor;

		// Token: 0x0400099E RID: 2462
		private Color forecolor;

		// Token: 0x0400099F RID: 2463
		private bool is_default;

		// Token: 0x040009A0 RID: 2464
		internal ArrayList table_relations;

		// Token: 0x040009A1 RID: 2465
		private CurrencyManager manager;
	}
}
