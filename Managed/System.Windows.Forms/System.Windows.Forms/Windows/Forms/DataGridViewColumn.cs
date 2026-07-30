using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms
{
	/// <summary>Represents a column in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000FC RID: 252
	[DesignTimeVisible(false)]
	[ToolboxItem("")]
	[TypeConverter(typeof(DataGridViewColumnConverter))]
	[Designer("System.Windows.Forms.Design.DataGridViewColumnDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public class DataGridViewColumn : DataGridViewBand, IDisposable, IComponent
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewColumn" /> class to the default state.</summary>
		// Token: 0x060012FD RID: 4861 RVA: 0x00049A90 File Offset: 0x00047C90
		public DataGridViewColumn()
		{
			this.cellTemplate = null;
			base.DefaultCellStyle = new DataGridViewCellStyle();
			this.readOnly = false;
			this.headerCell = new DataGridViewColumnHeaderCell();
			this.headerCell.SetColumnIndex(base.Index);
			this.headerCell.Value = string.Empty;
			this.displayIndex = -1;
			this.dataColumnIndex = -1;
			this.dataPropertyName = string.Empty;
			this.fillWeight = 100f;
			this.sortMode = DataGridViewColumnSortMode.NotSortable;
			this.SetState(DataGridViewElementStates.Visible);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewColumn" /> class using an existing <see cref="T:System.Windows.Forms.DataGridViewCell" /> as a template.</summary>
		/// <param name="cellTemplate">An existing <see cref="T:System.Windows.Forms.DataGridViewCell" /> to use as a template. </param>
		// Token: 0x060012FE RID: 4862 RVA: 0x00049B3C File Offset: 0x00047D3C
		public DataGridViewColumn(DataGridViewCell cellTemplate)
			: this()
		{
			this.cellTemplate = (DataGridViewCell)cellTemplate.Clone();
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.DataGridViewColumn" /> is disposed.</summary>
		// Token: 0x1400016B RID: 363
		// (add) Token: 0x060012FF RID: 4863 RVA: 0x00049B58 File Offset: 0x00047D58
		// (remove) Token: 0x06001300 RID: 4864 RVA: 0x00049B74 File Offset: 0x00047D74
		[Browsable(false)]
		[EditorBrowsable(2)]
		public event EventHandler Disposed;

		/// <summary>Gets or sets the mode by which the column automatically adjusts its width.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode" /> value that determines whether the column will automatically adjust its width and how it will determine its preferred width. The default is <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is a <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode" /> that is not valid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The specified value when setting this property results in an <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader" /> for a visible column when column headers are hidden.-or-The specified value when setting this property results in an <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill" /> for a visible column that is frozen.</exception>
		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001301 RID: 4865 RVA: 0x00049B90 File Offset: 0x00047D90
		// (set) Token: 0x06001302 RID: 4866 RVA: 0x00049B98 File Offset: 0x00047D98
		[DefaultValue(DataGridViewAutoSizeColumnMode.NotSet)]
		[RefreshProperties(2)]
		public DataGridViewAutoSizeColumnMode AutoSizeMode
		{
			get
			{
				return this.autoSizeMode;
			}
			set
			{
				if (this.autoSizeMode != value)
				{
					DataGridViewAutoSizeColumnMode dataGridViewAutoSizeColumnMode = this.autoSizeMode;
					this.autoSizeMode = value;
					if (base.DataGridView != null)
					{
						base.DataGridView.OnAutoSizeColumnModeChanged(new DataGridViewAutoSizeColumnModeEventArgs(this, dataGridViewAutoSizeColumnMode));
						base.DataGridView.AutoResizeColumnsInternal();
					}
				}
			}
		}

		/// <summary>Gets or sets the template used to create new cells.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCell" /> that all other cells in the column are modeled after. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001303 RID: 4867 RVA: 0x00049BE8 File Offset: 0x00047DE8
		// (set) Token: 0x06001304 RID: 4868 RVA: 0x00049BF0 File Offset: 0x00047DF0
		[EditorBrowsable(2)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public virtual DataGridViewCell CellTemplate
		{
			get
			{
				return this.cellTemplate;
			}
			set
			{
				this.cellTemplate = value;
			}
		}

		/// <summary>Gets the run-time type of the cell template.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> used as a template for this column. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001305 RID: 4869 RVA: 0x00049BFC File Offset: 0x00047DFC
		[Browsable(false)]
		[EditorBrowsable(2)]
		public Type CellType
		{
			get
			{
				if (this.cellTemplate == null)
				{
					return null;
				}
				return this.cellTemplate.GetType();
			}
		}

		/// <summary>Gets or sets the shortcut menu for the column.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with the current <see cref="T:System.Windows.Forms.DataGridViewColumn" />. The default is null.</returns>
		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001306 RID: 4870 RVA: 0x00049C18 File Offset: 0x00047E18
		// (set) Token: 0x06001307 RID: 4871 RVA: 0x00049C20 File Offset: 0x00047E20
		[DefaultValue(null)]
		public override ContextMenuStrip ContextMenuStrip
		{
			get
			{
				return this.contextMenuStrip;
			}
			set
			{
				if (this.contextMenuStrip != value)
				{
					this.contextMenuStrip = value;
					if (base.DataGridView != null)
					{
						base.DataGridView.OnColumnContextMenuStripChanged(new DataGridViewColumnEventArgs(this));
					}
				}
			}
		}

		/// <summary>Gets or sets the name of the data source property or database column to which the <see cref="T:System.Windows.Forms.DataGridViewColumn" /> is bound.</summary>
		/// <returns>The case-insensitive name of the property or database column associated with the <see cref="T:System.Windows.Forms.DataGridViewColumn" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001308 RID: 4872 RVA: 0x00049C54 File Offset: 0x00047E54
		// (set) Token: 0x06001309 RID: 4873 RVA: 0x00049C5C File Offset: 0x00047E5C
		[DefaultValue("")]
		[TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[Editor("System.Windows.Forms.Design.DataGridViewColumnDataPropertyNameEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Browsable(true)]
		public string DataPropertyName
		{
			get
			{
				return this.dataPropertyName;
			}
			set
			{
				if (this.dataPropertyName != value)
				{
					this.dataPropertyName = value;
					if (base.DataGridView != null)
					{
						base.DataGridView.OnColumnDataPropertyNameChanged(new DataGridViewColumnEventArgs(this));
					}
				}
			}
		}

		/// <summary>Gets or sets the column's default cell style.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the default style of the cells in the column.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x0600130A RID: 4874 RVA: 0x00049CA0 File Offset: 0x00047EA0
		// (set) Token: 0x0600130B RID: 4875 RVA: 0x00049CA8 File Offset: 0x00047EA8
		[Browsable(true)]
		public override DataGridViewCellStyle DefaultCellStyle
		{
			get
			{
				return base.DefaultCellStyle;
			}
			set
			{
				if (this.DefaultCellStyle != value)
				{
					base.DefaultCellStyle = value;
					if (base.DataGridView != null)
					{
						base.DataGridView.OnColumnDefaultCellStyleChanged(new DataGridViewColumnEventArgs(this));
					}
				}
			}
		}

		/// <summary>Gets or sets the display order of the column relative to the currently displayed columns.</summary>
		/// <returns>The zero-based position of the column as it is displayed in the associated <see cref="T:System.Windows.Forms.DataGridView" />, or -1 if the band is not contained within a control. </returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> is not null and the specified value when setting this property is less than 0 or greater than or equal to the number of columns in the control.-or-<see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> is null and the specified value when setting this property is less than -1.-or-The specified value when setting this property is equal to <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x00049CE4 File Offset: 0x00047EE4
		// (set) Token: 0x0600130D RID: 4877 RVA: 0x00049D00 File Offset: 0x00047F00
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public int DisplayIndex
		{
			get
			{
				if (this.displayIndex < 0)
				{
					return base.Index;
				}
				return this.displayIndex;
			}
			set
			{
				if (this.displayIndex != value)
				{
					if (value < 0 || value > 2147483647)
					{
						throw new ArgumentOutOfRangeException("DisplayIndex is out of range");
					}
					this.displayIndex = value;
					if (base.DataGridView != null)
					{
						base.DataGridView.Columns.RegenerateSortedList();
						base.DataGridView.OnColumnDisplayIndexChanged(new DataGridViewColumnEventArgs(this));
					}
				}
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x00049D6C File Offset: 0x00047F6C
		// (set) Token: 0x0600130F RID: 4879 RVA: 0x00049D74 File Offset: 0x00047F74
		internal int DataColumnIndex
		{
			get
			{
				return this.dataColumnIndex;
			}
			set
			{
				this.dataColumnIndex = value;
			}
		}

		/// <summary>Gets or sets the width, in pixels, of the column divider.</summary>
		/// <returns>The thickness, in pixels, of the divider (the column's right margin). </returns>
		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x00049D80 File Offset: 0x00047F80
		// (set) Token: 0x06001311 RID: 4881 RVA: 0x00049D88 File Offset: 0x00047F88
		[DefaultValue(0)]
		public int DividerWidth
		{
			get
			{
				return this.dividerWidth;
			}
			set
			{
				if (this.dividerWidth != value)
				{
					this.dividerWidth = value;
					if (base.DataGridView != null)
					{
						base.DataGridView.OnColumnDividerWidthChanged(new DataGridViewColumnEventArgs(this));
					}
				}
			}
		}

		/// <summary>Gets or sets a value that represents the width of the column when it is in fill mode relative to the widths of other fill-mode columns in the control.</summary>
		/// <returns>A <see cref="T:System.Single" /> representing the width of the column when it is in fill mode relative to the widths of other fill-mode columns. The default is 100.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is less than or equal to 0. </exception>
		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x00049DBC File Offset: 0x00047FBC
		// (set) Token: 0x06001313 RID: 4883 RVA: 0x00049DC4 File Offset: 0x00047FC4
		[DefaultValue(100)]
		public float FillWeight
		{
			get
			{
				return this.fillWeight;
			}
			set
			{
				this.fillWeight = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether a column will move when a user scrolls the <see cref="T:System.Windows.Forms.DataGridView" /> control horizontally.</summary>
		/// <returns>true to freeze the column; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x00049DD0 File Offset: 0x00047FD0
		// (set) Token: 0x06001315 RID: 4885 RVA: 0x00049DD8 File Offset: 0x00047FD8
		[RefreshProperties(1)]
		[DefaultValue(false)]
		public override bool Frozen
		{
			get
			{
				return this.frozen;
			}
			set
			{
				this.frozen = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell" /> that represents the column header.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell" /> that represents the header cell for the column.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x00049DE4 File Offset: 0x00047FE4
		// (set) Token: 0x06001317 RID: 4887 RVA: 0x00049DEC File Offset: 0x00047FEC
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public DataGridViewColumnHeaderCell HeaderCell
		{
			get
			{
				return this.headerCell;
			}
			set
			{
				if (this.headerCell != value)
				{
					this.headerCell = value;
					if (base.DataGridView != null)
					{
						base.DataGridView.OnColumnHeaderCellChanged(new DataGridViewColumnEventArgs(this));
					}
				}
			}
		}

		/// <summary>Gets or sets the caption text on the column's header cell.</summary>
		/// <returns>A <see cref="T:System.String" /> with the desired text. The default is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001318 RID: 4888 RVA: 0x00049E20 File Offset: 0x00048020
		// (set) Token: 0x06001319 RID: 4889 RVA: 0x00049E54 File Offset: 0x00048054
		[Localizable(true)]
		public string HeaderText
		{
			get
			{
				if (this.headerCell.Value == null)
				{
					return string.Empty;
				}
				return (string)this.headerCell.Value;
			}
			set
			{
				this.headerCell.Value = value;
				this.headerTextSet = true;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x0600131A RID: 4890 RVA: 0x00049E6C File Offset: 0x0004806C
		// (set) Token: 0x0600131B RID: 4891 RVA: 0x00049E74 File Offset: 0x00048074
		internal bool AutoGenerated
		{
			get
			{
				return this.auto_generated;
			}
			set
			{
				this.auto_generated = value;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x0600131C RID: 4892 RVA: 0x00049E80 File Offset: 0x00048080
		internal bool HeaderTextSet
		{
			get
			{
				return this.headerTextSet;
			}
		}

		/// <summary>Gets the sizing mode in effect for the column.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode" /> value in effect for the column.</returns>
		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x0600131D RID: 4893 RVA: 0x00049E88 File Offset: 0x00048088
		[Browsable(false)]
		[EditorBrowsable(2)]
		[DesignerSerializationVisibility(0)]
		public DataGridViewAutoSizeColumnMode InheritedAutoSizeMode
		{
			get
			{
				if (base.DataGridView == null)
				{
					return this.autoSizeMode;
				}
				if (this.autoSizeMode != DataGridViewAutoSizeColumnMode.NotSet)
				{
					return this.autoSizeMode;
				}
				DataGridViewAutoSizeColumnsMode autoSizeColumnsMode = base.DataGridView.AutoSizeColumnsMode;
				switch (autoSizeColumnsMode)
				{
				case DataGridViewAutoSizeColumnsMode.ColumnHeader:
					return DataGridViewAutoSizeColumnMode.ColumnHeader;
				default:
					if (autoSizeColumnsMode != DataGridViewAutoSizeColumnsMode.Fill)
					{
						return DataGridViewAutoSizeColumnMode.None;
					}
					return DataGridViewAutoSizeColumnMode.Fill;
				case DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader:
					return DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
				case DataGridViewAutoSizeColumnsMode.AllCells:
					return DataGridViewAutoSizeColumnMode.AllCells;
				case DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader:
					return DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
				case DataGridViewAutoSizeColumnsMode.DisplayedCells:
					return DataGridViewAutoSizeColumnMode.DisplayedCells;
				}
			}
		}

		/// <summary>Gets the cell style currently applied to the column.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the cell style used to display the column.</returns>
		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x0600131E RID: 4894 RVA: 0x00049F10 File Offset: 0x00048110
		[Browsable(false)]
		public override DataGridViewCellStyle InheritedStyle
		{
			get
			{
				if (base.DataGridView == null)
				{
					return base.DefaultCellStyle;
				}
				if (base.DefaultCellStyle == null)
				{
					return base.DataGridView.DefaultCellStyle;
				}
				return base.DefaultCellStyle.Clone();
			}
		}

		/// <summary>Gets a value indicating whether the column is bound to a data source.</summary>
		/// <returns>true if the column is connected to a data source; otherwise, false.</returns>
		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x00049F54 File Offset: 0x00048154
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public bool IsDataBound
		{
			get
			{
				return this.isDataBound;
			}
		}

		/// <summary>Gets or sets the minimum width, in pixels, of the column.</summary>
		/// <returns>The number of pixels, from 2 to <see cref="F:System.Int32.MaxValue" />, that specifies the minimum width of the column. The default is 5.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than 2 or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x00049F5C File Offset: 0x0004815C
		// (set) Token: 0x06001321 RID: 4897 RVA: 0x00049F64 File Offset: 0x00048164
		[DefaultValue(5)]
		[Localizable(true)]
		[RefreshProperties(2)]
		public int MinimumWidth
		{
			get
			{
				return this.minimumWidth;
			}
			set
			{
				if (this.minimumWidth != value)
				{
					if (value < 2 || value > 2147483647)
					{
						throw new ArgumentOutOfRangeException("MinimumWidth is out of range");
					}
					this.minimumWidth = value;
					if (base.DataGridView != null)
					{
						base.DataGridView.OnColumnMinimumWidthChanged(new DataGridViewColumnEventArgs(this));
					}
				}
			}
		}

		/// <summary>Gets or sets the name of the column.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name of the column. The default is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001322 RID: 4898 RVA: 0x00049FC0 File Offset: 0x000481C0
		// (set) Token: 0x06001323 RID: 4899 RVA: 0x00049FC8 File Offset: 0x000481C8
		[Browsable(false)]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (this.name != value)
				{
					if (value == null)
					{
						this.name = string.Empty;
					}
					else
					{
						this.name = value;
					}
					if (!this.headerTextSet)
					{
						this.headerCell.Value = this.name;
					}
					if (base.DataGridView != null)
					{
						base.DataGridView.OnColumnNameChanged(new DataGridViewColumnEventArgs(this));
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the user can edit the column's cells.</summary>
		/// <returns>true if the user cannot edit the column's cells; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">This property is set to false for a column that is bound to a read-only data source. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001324 RID: 4900 RVA: 0x0004A03C File Offset: 0x0004823C
		// (set) Token: 0x06001325 RID: 4901 RVA: 0x0004A06C File Offset: 0x0004826C
		public override bool ReadOnly
		{
			get
			{
				return (base.DataGridView != null && base.DataGridView.ReadOnly) || this.readOnly;
			}
			set
			{
				this.readOnly = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the column is resizable.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewTriState" /> values. The default is <see cref="F:System.Windows.Forms.DataGridViewTriState.True" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x0004A078 File Offset: 0x00048278
		// (set) Token: 0x06001327 RID: 4903 RVA: 0x0004A080 File Offset: 0x00048280
		public override DataGridViewTriState Resizable
		{
			get
			{
				return base.Resizable;
			}
			set
			{
				base.Resizable = value;
			}
		}

		/// <summary>Gets or sets the site of the column.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISite" /> associated with the column, if any.</returns>
		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x0004A08C File Offset: 0x0004828C
		// (set) Token: 0x06001329 RID: 4905 RVA: 0x0004A094 File Offset: 0x00048294
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public ISite Site
		{
			get
			{
				return this.site;
			}
			set
			{
				this.site = value;
			}
		}

		/// <summary>Gets or sets the sort mode for the column.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewColumnSortMode" /> that specifies the criteria used to order the rows based on the cell values in a column.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value assigned to the property conflicts with <see cref="P:System.Windows.Forms.DataGridView.SelectionMode" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x0600132A RID: 4906 RVA: 0x0004A0A0 File Offset: 0x000482A0
		// (set) Token: 0x0600132B RID: 4907 RVA: 0x0004A0A8 File Offset: 0x000482A8
		[DefaultValue(DataGridViewColumnSortMode.NotSortable)]
		public DataGridViewColumnSortMode SortMode
		{
			get
			{
				return this.sortMode;
			}
			set
			{
				if (base.DataGridView != null && value == DataGridViewColumnSortMode.Automatic && (base.DataGridView.SelectionMode == DataGridViewSelectionMode.FullColumnSelect || base.DataGridView.SelectionMode == DataGridViewSelectionMode.ColumnHeaderSelect))
				{
					throw new InvalidOperationException("Column's SortMode cannot be set to Automatic while the DataGridView control's SelectionMode is set to FullColumnSelect or ColumnHeaderSelect.");
				}
				if (this.sortMode != value)
				{
					this.sortMode = value;
					if (base.DataGridView != null)
					{
						base.DataGridView.OnColumnSortModeChanged(new DataGridViewColumnEventArgs(this));
					}
				}
			}
		}

		/// <summary>Gets or sets the text used for ToolTips.</summary>
		/// <returns>The text to display as a ToolTip for the column.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x0600132C RID: 4908 RVA: 0x0004A124 File Offset: 0x00048324
		// (set) Token: 0x0600132D RID: 4909 RVA: 0x0004A140 File Offset: 0x00048340
		[DefaultValue("")]
		[Localizable(true)]
		public string ToolTipText
		{
			get
			{
				if (this.toolTipText == null)
				{
					return string.Empty;
				}
				return this.toolTipText;
			}
			set
			{
				if (this.toolTipText != value)
				{
					this.toolTipText = value;
					if (base.DataGridView != null)
					{
						base.DataGridView.OnColumnToolTipTextChanged(new DataGridViewColumnEventArgs(this));
					}
				}
			}
		}

		/// <summary>Gets or sets the data type of the values in the column's cells.</summary>
		/// <returns>A <see cref="T:System.Type" /> that describes the run-time class of the values stored in the column's cells.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x0600132E RID: 4910 RVA: 0x0004A184 File Offset: 0x00048384
		// (set) Token: 0x0600132F RID: 4911 RVA: 0x0004A18C File Offset: 0x0004838C
		[DefaultValue(null)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public Type ValueType
		{
			get
			{
				return this.valueType;
			}
			set
			{
				this.valueType = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the column is visible.</summary>
		/// <returns>true if the column is visible; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001330 RID: 4912 RVA: 0x0004A198 File Offset: 0x00048398
		// (set) Token: 0x06001331 RID: 4913 RVA: 0x0004A1A0 File Offset: 0x000483A0
		[DefaultValue(true)]
		[Localizable(true)]
		public override bool Visible
		{
			get
			{
				return this.visible;
			}
			set
			{
				this.visible = value;
				if (base.DataGridView != null)
				{
					base.DataGridView.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the current width of the column.</summary>
		/// <returns>The width, in pixels, of the column. The default is 100.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is greater than 65536.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001332 RID: 4914 RVA: 0x0004A1C0 File Offset: 0x000483C0
		// (set) Token: 0x06001333 RID: 4915 RVA: 0x0004A1C8 File Offset: 0x000483C8
		[RefreshProperties(2)]
		[Localizable(true)]
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
					if (value < this.minimumWidth)
					{
						throw new ArgumentOutOfRangeException("Width is less than MinimumWidth");
					}
					this.width = value;
					if (base.DataGridView != null)
					{
						base.DataGridView.Invalidate();
						base.DataGridView.OnColumnWidthChanged(new DataGridViewColumnEventArgs(this));
					}
				}
			}
		}

		/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewBand" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001334 RID: 4916 RVA: 0x0004A228 File Offset: 0x00048428
		public override object Clone()
		{
			return base.MemberwiseClone();
		}

		/// <summary>Calculates the ideal width of the column based on the specified criteria.</summary>
		/// <returns>The ideal width, in pixels, of the column.</returns>
		/// <param name="autoSizeColumnMode">A <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode" /> value that specifies an automatic sizing mode. </param>
		/// <param name="fixedHeight">true to calculate the width of the column based on the current row heights; false to calculate the width with the expectation that the row heights will be adjusted.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="autoSizeColumnMode" /> is <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet" />, <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.None" />, or <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill" />. </exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="autoSizeColumnMode" /> is not a valid <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode" /> value. </exception>
		// Token: 0x06001335 RID: 4917 RVA: 0x0004A230 File Offset: 0x00048430
		public virtual int GetPreferredWidth(DataGridViewAutoSizeColumnMode autoSizeColumnMode, bool fixedHeight)
		{
			if (autoSizeColumnMode == DataGridViewAutoSizeColumnMode.NotSet || autoSizeColumnMode == DataGridViewAutoSizeColumnMode.None || autoSizeColumnMode == DataGridViewAutoSizeColumnMode.Fill)
			{
				throw new ArgumentException("AutoSizeColumnMode is invalid");
			}
			if (fixedHeight)
			{
				return 0;
			}
			return 0;
		}

		/// <summary>Gets a string that describes the column.</summary>
		/// <returns>A <see cref="T:System.String" /> that describes the column.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001336 RID: 4918 RVA: 0x0004A270 File Offset: 0x00048470
		public override string ToString()
		{
			return this.Name + ", Index: " + base.Index.ToString() + ".";
		}

		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06001337 RID: 4919 RVA: 0x0004A2A0 File Offset: 0x000484A0
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
			}
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x0004A2A8 File Offset: 0x000484A8
		internal override void SetDataGridView(DataGridView dataGridView)
		{
			if (this.sortMode == DataGridViewColumnSortMode.Automatic && dataGridView != null && dataGridView.SelectionMode == DataGridViewSelectionMode.FullColumnSelect)
			{
				throw new InvalidOperationException("Column's SortMode cannot be set to Automatic while the DataGridView control's SelectionMode is set to FullColumnSelect.");
			}
			base.SetDataGridView(dataGridView);
			this.headerCell.SetDataGridView(dataGridView);
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x0004A2F4 File Offset: 0x000484F4
		internal override void SetIndex(int index)
		{
			base.SetIndex(index);
			this.headerCell.SetColumnIndex(base.Index);
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x0004A310 File Offset: 0x00048510
		internal void SetIsDataBound(bool value)
		{
			this.isDataBound = value;
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x0004A31C File Offset: 0x0004851C
		internal override void SetState(DataGridViewElementStates state)
		{
			if (this.State != state)
			{
				base.SetState(state);
				if (base.DataGridView != null)
				{
					base.DataGridView.OnColumnStateChanged(new DataGridViewColumnStateChangedEventArgs(this, state));
				}
			}
		}

		// Token: 0x04000B44 RID: 2884
		private bool auto_generated;

		// Token: 0x04000B45 RID: 2885
		private DataGridViewAutoSizeColumnMode autoSizeMode;

		// Token: 0x04000B46 RID: 2886
		private DataGridViewCell cellTemplate;

		// Token: 0x04000B47 RID: 2887
		private ContextMenuStrip contextMenuStrip;

		// Token: 0x04000B48 RID: 2888
		private string dataPropertyName;

		// Token: 0x04000B49 RID: 2889
		private int displayIndex;

		// Token: 0x04000B4A RID: 2890
		private int dividerWidth;

		// Token: 0x04000B4B RID: 2891
		private float fillWeight;

		// Token: 0x04000B4C RID: 2892
		private bool frozen;

		// Token: 0x04000B4D RID: 2893
		private DataGridViewColumnHeaderCell headerCell;

		// Token: 0x04000B4E RID: 2894
		private bool isDataBound;

		// Token: 0x04000B4F RID: 2895
		private int minimumWidth = 5;

		// Token: 0x04000B50 RID: 2896
		private string name = string.Empty;

		// Token: 0x04000B51 RID: 2897
		private bool readOnly;

		// Token: 0x04000B52 RID: 2898
		private ISite site;

		// Token: 0x04000B53 RID: 2899
		private DataGridViewColumnSortMode sortMode;

		// Token: 0x04000B54 RID: 2900
		private string toolTipText;

		// Token: 0x04000B55 RID: 2901
		private Type valueType;

		// Token: 0x04000B56 RID: 2902
		private bool visible = true;

		// Token: 0x04000B57 RID: 2903
		private int width = 100;

		// Token: 0x04000B58 RID: 2904
		private int dataColumnIndex;

		// Token: 0x04000B59 RID: 2905
		private bool headerTextSet;
	}
}
