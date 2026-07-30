using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Represents a column of <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200010A RID: 266
	[Designer("System.Windows.Forms.Design.DataGridViewComboBoxColumnDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ToolboxBitmap("")]
	public class DataGridViewComboBoxColumn : DataGridViewColumn
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewTextBoxColumn" /> class to the default state.</summary>
		// Token: 0x060013DD RID: 5085 RVA: 0x0004BE48 File Offset: 0x0004A048
		public DataGridViewComboBoxColumn()
		{
			this.CellTemplate = new DataGridViewComboBoxCell();
			((DataGridViewComboBoxCell)this.CellTemplate).OwningColumnTemplate = this;
			base.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.autoComplete = true;
			this.displayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
			this.displayStyleForCurrentCellOnly = false;
		}

		/// <summary>Gets or sets a value indicating whether cells in the column will match the characters being entered in the cell with one from the possible selections. </summary>
		/// <returns>true if auto completion is activated; otherwise, false. The default is true.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.CellTemplate" /> property is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x060013DE RID: 5086 RVA: 0x0004BE94 File Offset: 0x0004A094
		// (set) Token: 0x060013DF RID: 5087 RVA: 0x0004BE9C File Offset: 0x0004A09C
		[Browsable(true)]
		[DefaultValue(true)]
		public bool AutoComplete
		{
			get
			{
				return this.autoComplete;
			}
			set
			{
				this.autoComplete = value;
			}
		}

		/// <summary>Gets or sets the template used to create cells.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCell" /> that all other cells in the column are modeled after. The default value is a new <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" />.</returns>
		/// <exception cref="T:System.InvalidCastException">When setting this property to a value that is not of type <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x060013E0 RID: 5088 RVA: 0x0004BEA8 File Offset: 0x0004A0A8
		// (set) Token: 0x060013E1 RID: 5089 RVA: 0x0004BEB0 File Offset: 0x0004A0B0
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public override DataGridViewCell CellTemplate
		{
			get
			{
				return base.CellTemplate;
			}
			set
			{
				DataGridViewComboBoxCell dataGridViewComboBoxCell = value as DataGridViewComboBoxCell;
				if (dataGridViewComboBoxCell == null)
				{
					throw new InvalidCastException("Invalid cell tempalte type.");
				}
				dataGridViewComboBoxCell.OwningColumnTemplate = this;
				base.CellTemplate = dataGridViewComboBoxCell;
			}
		}

		/// <summary>Gets or sets the data source that populates the selections for the combo boxes.</summary>
		/// <returns>An object that represents a data source. The default is null.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.CellTemplate" /> property is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x060013E2 RID: 5090 RVA: 0x0004BEE4 File Offset: 0x0004A0E4
		// (set) Token: 0x060013E3 RID: 5091 RVA: 0x0004BF18 File Offset: 0x0004A118
		[AttributeProvider(typeof(IListSource))]
		[DefaultValue(null)]
		[RefreshProperties(2)]
		public object DataSource
		{
			get
			{
				if (base.CellTemplate == null)
				{
					throw new InvalidOperationException("CellTemplate is null.");
				}
				return (base.CellTemplate as DataGridViewComboBoxCell).DataSource;
			}
			set
			{
				if (base.CellTemplate == null)
				{
					throw new InvalidOperationException("CellTemplate is null.");
				}
				(base.CellTemplate as DataGridViewComboBoxCell).DataSource = value;
			}
		}

		/// <summary>Gets or sets a string that specifies the property or column from which to retrieve strings for display in the combo boxes.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the name of a property or column in the data source specified in the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.DataSource" /> property. The default is <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.CellTemplate" /> property is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x060013E4 RID: 5092 RVA: 0x0004BF44 File Offset: 0x0004A144
		// (set) Token: 0x060013E5 RID: 5093 RVA: 0x0004BF78 File Offset: 0x0004A178
		[Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		[TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string DisplayMember
		{
			get
			{
				if (base.CellTemplate == null)
				{
					throw new InvalidOperationException("CellTemplate is null.");
				}
				return (base.CellTemplate as DataGridViewComboBoxCell).DisplayMember;
			}
			set
			{
				if (base.CellTemplate == null)
				{
					throw new InvalidOperationException("CellTemplate is null.");
				}
				(base.CellTemplate as DataGridViewComboBoxCell).DisplayMember = value;
			}
		}

		/// <summary>Gets or sets a value that determines how the combo box is displayed when not editing.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewComboBoxDisplayStyle" /> value indicating the combo box appearance. The default is <see cref="F:System.Windows.Forms.DataGridViewComboBoxDisplayStyle.DropDownButton" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.CellTemplate" /> property is null.</exception>
		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x060013E6 RID: 5094 RVA: 0x0004BFA4 File Offset: 0x0004A1A4
		// (set) Token: 0x060013E7 RID: 5095 RVA: 0x0004BFAC File Offset: 0x0004A1AC
		[DefaultValue(DataGridViewComboBoxDisplayStyle.DropDownButton)]
		public DataGridViewComboBoxDisplayStyle DisplayStyle
		{
			get
			{
				return this.displayStyle;
			}
			set
			{
				this.displayStyle = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.DisplayStyle" /> property value applies only to the current cell in the <see cref="T:System.Windows.Forms.DataGridView" /> control when the current cell is in this column.</summary>
		/// <returns>true if the display style applies only to the current cell; otherwise false. The default is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.CellTemplate" /> property is null.</exception>
		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x060013E8 RID: 5096 RVA: 0x0004BFB8 File Offset: 0x0004A1B8
		// (set) Token: 0x060013E9 RID: 5097 RVA: 0x0004BFC0 File Offset: 0x0004A1C0
		[DefaultValue(false)]
		public bool DisplayStyleForCurrentCellOnly
		{
			get
			{
				return this.displayStyleForCurrentCellOnly;
			}
			set
			{
				this.displayStyleForCurrentCellOnly = value;
			}
		}

		/// <summary>Gets or sets the width of the drop-down lists of the combo boxes.</summary>
		/// <returns>The width, in pixels, of the drop-down lists. The default is 1.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.CellTemplate" /> property is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x060013EA RID: 5098 RVA: 0x0004BFCC File Offset: 0x0004A1CC
		// (set) Token: 0x060013EB RID: 5099 RVA: 0x0004C000 File Offset: 0x0004A200
		[DefaultValue(1)]
		public int DropDownWidth
		{
			get
			{
				if (base.CellTemplate == null)
				{
					throw new InvalidOperationException("CellTemplate is null.");
				}
				return (base.CellTemplate as DataGridViewComboBoxCell).DropDownWidth;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentException("Value is less than 1.");
				}
				if (base.CellTemplate == null)
				{
					throw new InvalidOperationException("CellTemplate is null.");
				}
				(base.CellTemplate as DataGridViewComboBoxCell).DropDownWidth = value;
			}
		}

		/// <summary>Gets or sets the flat style appearance of the column's cells.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.FlatStyle" /> value indicating the cell appearance. The default is <see cref="F:System.Windows.Forms.FlatStyle.Standard" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.CellTemplate" /> property is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x060013EC RID: 5100 RVA: 0x0004C03C File Offset: 0x0004A23C
		// (set) Token: 0x060013ED RID: 5101 RVA: 0x0004C044 File Offset: 0x0004A244
		[DefaultValue(FlatStyle.Standard)]
		public FlatStyle FlatStyle
		{
			get
			{
				return this.flatStyle;
			}
			set
			{
				this.flatStyle = value;
			}
		}

		/// <summary>Gets the collection of objects used as selections in the combo boxes.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell.ObjectCollection" /> that represents the selections in the combo boxes. </returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.CellTemplate" /> property is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x060013EE RID: 5102 RVA: 0x0004C050 File Offset: 0x0004A250
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DesignerSerializationVisibility(2)]
		public DataGridViewComboBoxCell.ObjectCollection Items
		{
			get
			{
				if (base.CellTemplate == null)
				{
					throw new InvalidOperationException("CellTemplate is null.");
				}
				return (base.CellTemplate as DataGridViewComboBoxCell).Items;
			}
		}

		/// <summary>Gets or sets the maximum number of items in the drop-down list of the cells in the column.</summary>
		/// <returns>The maximum number of drop-down list items, from 1 to 100. The default is 8.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.CellTemplate" /> property is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x060013EF RID: 5103 RVA: 0x0004C084 File Offset: 0x0004A284
		// (set) Token: 0x060013F0 RID: 5104 RVA: 0x0004C0B8 File Offset: 0x0004A2B8
		[DefaultValue(8)]
		public int MaxDropDownItems
		{
			get
			{
				if (base.CellTemplate == null)
				{
					throw new InvalidOperationException("CellTemplate is null.");
				}
				return (base.CellTemplate as DataGridViewComboBoxCell).MaxDropDownItems;
			}
			set
			{
				if (base.CellTemplate == null)
				{
					throw new InvalidOperationException("CellTemplate is null.");
				}
				(base.CellTemplate as DataGridViewComboBoxCell).MaxDropDownItems = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the items in the combo box are sorted.</summary>
		/// <returns>true if the combo box is sorted; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.CellTemplate" /> property is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x060013F1 RID: 5105 RVA: 0x0004C0E4 File Offset: 0x0004A2E4
		// (set) Token: 0x060013F2 RID: 5106 RVA: 0x0004C118 File Offset: 0x0004A318
		[DefaultValue(false)]
		public bool Sorted
		{
			get
			{
				if (base.CellTemplate == null)
				{
					throw new InvalidOperationException("CellTemplate is null.");
				}
				return (base.CellTemplate as DataGridViewComboBoxCell).Sorted;
			}
			set
			{
				if (base.CellTemplate == null)
				{
					throw new InvalidOperationException("CellTemplate is null.");
				}
				(base.CellTemplate as DataGridViewComboBoxCell).Sorted = value;
			}
		}

		/// <summary>Gets or sets a string that specifies the property or column from which to get values that correspond to the selections in the drop-down list.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the name of a property or column used in the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.DataSource" /> property. The default is <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.CellTemplate" /> property is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x060013F3 RID: 5107 RVA: 0x0004C144 File Offset: 0x0004A344
		// (set) Token: 0x060013F4 RID: 5108 RVA: 0x0004C178 File Offset: 0x0004A378
		[TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string ValueMember
		{
			get
			{
				if (base.CellTemplate == null)
				{
					throw new InvalidOperationException("CellTemplate is null.");
				}
				return (base.CellTemplate as DataGridViewComboBoxCell).ValueMember;
			}
			set
			{
				if (base.CellTemplate == null)
				{
					throw new InvalidOperationException("CellTemplate is null.");
				}
				(base.CellTemplate as DataGridViewComboBoxCell).ValueMember = value;
			}
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x0004C1A4 File Offset: 0x0004A3A4
		internal void SyncItems(IList items)
		{
			if (this.DataSource != null || base.DataGridView == null)
			{
				return;
			}
			for (int i = 0; i < base.DataGridView.RowCount; i++)
			{
				DataGridViewComboBoxCell dataGridViewComboBoxCell = base.DataGridView.Rows[i].Cells[base.Index] as DataGridViewComboBoxCell;
				if (dataGridViewComboBoxCell != null)
				{
					dataGridViewComboBoxCell.Items.ClearInternal();
					dataGridViewComboBoxCell.Items.AddRangeInternal(this.Items);
				}
			}
		}

		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013F6 RID: 5110 RVA: 0x0004C230 File Offset: 0x0004A430
		public override object Clone()
		{
			DataGridViewComboBoxColumn dataGridViewComboBoxColumn = (DataGridViewComboBoxColumn)base.Clone();
			dataGridViewComboBoxColumn.autoComplete = this.autoComplete;
			dataGridViewComboBoxColumn.displayStyle = this.displayStyle;
			dataGridViewComboBoxColumn.displayStyleForCurrentCellOnly = this.displayStyleForCurrentCellOnly;
			dataGridViewComboBoxColumn.flatStyle = this.flatStyle;
			dataGridViewComboBoxColumn.CellTemplate = (DataGridViewComboBoxCell)this.CellTemplate.Clone();
			return dataGridViewComboBoxColumn;
		}

		/// <returns>A <see cref="T:System.String" /> that describes the column.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013F7 RID: 5111 RVA: 0x0004C290 File Offset: 0x0004A490
		public override string ToString()
		{
			return base.GetType().Name;
		}

		// Token: 0x04000B7E RID: 2942
		private bool autoComplete;

		// Token: 0x04000B7F RID: 2943
		private DataGridViewComboBoxDisplayStyle displayStyle;

		// Token: 0x04000B80 RID: 2944
		private bool displayStyleForCurrentCellOnly;

		// Token: 0x04000B81 RID: 2945
		private FlatStyle flatStyle;
	}
}
