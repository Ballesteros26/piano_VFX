using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Hosts a collection of <see cref="T:System.Windows.Forms.DataGridViewImageCell" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200011A RID: 282
	[ToolboxBitmap("")]
	public class DataGridViewImageColumn : DataGridViewColumn
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewImageColumn" /> class, configuring it for use with cell values of type <see cref="T:System.Drawing.Image" />.</summary>
		// Token: 0x06001459 RID: 5209 RVA: 0x0004CEF4 File Offset: 0x0004B0F4
		public DataGridViewImageColumn()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewImageColumn" /> class, optionally configuring it for use with <see cref="T:System.Drawing.Icon" /> cell values.</summary>
		/// <param name="valuesAreIcons">true to indicate that the <see cref="P:System.Windows.Forms.DataGridViewCell.Value" /> property of cells in this column will be set to values of type <see cref="T:System.Drawing.Icon" />; false to indicate that they will be set to values of type <see cref="T:System.Drawing.Image" />.</param>
		// Token: 0x0600145A RID: 5210 RVA: 0x0004CF00 File Offset: 0x0004B100
		public DataGridViewImageColumn(bool valuesAreIcons)
		{
			this.valuesAreIcons = valuesAreIcons;
			base.CellTemplate = new DataGridViewImageCell(valuesAreIcons);
			(base.CellTemplate as DataGridViewImageCell).ImageLayout = DataGridViewImageCellLayout.Normal;
			this.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.icon = null;
			this.image = null;
		}

		/// <summary>Gets or sets the template used to create new cells.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCell" /> that all other cells in the column are modeled after.</returns>
		/// <exception cref="T:System.InvalidCastException">The set type is not compatible with type <see cref="T:System.Windows.Forms.DataGridViewImageCell" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x0600145B RID: 5211 RVA: 0x0004CF54 File Offset: 0x0004B154
		// (set) Token: 0x0600145C RID: 5212 RVA: 0x0004CF5C File Offset: 0x0004B15C
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public override DataGridViewCell CellTemplate
		{
			get
			{
				return base.CellTemplate;
			}
			set
			{
				base.CellTemplate = value as DataGridViewImageCell;
			}
		}

		/// <summary>Gets or sets the column's default cell style.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied as the default style.</returns>
		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x0004CF6C File Offset: 0x0004B16C
		// (set) Token: 0x0600145E RID: 5214 RVA: 0x0004CF74 File Offset: 0x0004B174
		[Browsable(true)]
		public override DataGridViewCellStyle DefaultCellStyle
		{
			get
			{
				return base.DefaultCellStyle;
			}
			set
			{
				base.DefaultCellStyle = value;
			}
		}

		/// <summary>Gets or sets a string that describes the column's image. </summary>
		/// <returns>The textual description of the column image. The default is <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewImageColumn.CellTemplate" /> property is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x0004CF80 File Offset: 0x0004B180
		// (set) Token: 0x06001460 RID: 5216 RVA: 0x0004CF94 File Offset: 0x0004B194
		[DefaultValue("")]
		[Browsable(true)]
		public string Description
		{
			get
			{
				return (base.CellTemplate as DataGridViewImageCell).Description;
			}
			set
			{
				(base.CellTemplate as DataGridViewImageCell).Description = value;
			}
		}

		/// <summary>Gets or sets the icon displayed in the cells of this column when the cell's <see cref="P:System.Windows.Forms.DataGridViewCell.Value" /> property is not set and the cell's <see cref="P:System.Windows.Forms.DataGridViewImageCell.ValueIsIcon" /> property is set to true.</summary>
		/// <returns>The <see cref="T:System.Drawing.Icon" /> to display. The default is null.</returns>
		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001461 RID: 5217 RVA: 0x0004CFA8 File Offset: 0x0004B1A8
		// (set) Token: 0x06001462 RID: 5218 RVA: 0x0004CFB0 File Offset: 0x0004B1B0
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public Icon Icon
		{
			get
			{
				return this.icon;
			}
			set
			{
				this.icon = value;
			}
		}

		/// <summary>Gets or sets the image displayed in the cells of this column when the cell's <see cref="P:System.Windows.Forms.DataGridViewCell.Value" /> property is not set and the cell's <see cref="P:System.Windows.Forms.DataGridViewImageCell.ValueIsIcon" /> property is set to false.</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> to display. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001463 RID: 5219 RVA: 0x0004CFBC File Offset: 0x0004B1BC
		// (set) Token: 0x06001464 RID: 5220 RVA: 0x0004CFC4 File Offset: 0x0004B1C4
		[DefaultValue(null)]
		public Image Image
		{
			get
			{
				return this.image;
			}
			set
			{
				this.image = value;
			}
		}

		/// <summary>Gets or sets the image layout in the cells for this column.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewImageCellLayout" /> that specifies the cell layout. The default is <see cref="F:System.Windows.Forms.DataGridViewImageCellLayout.Normal" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewImageColumn.CellTemplate" /> property is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001465 RID: 5221 RVA: 0x0004CFD0 File Offset: 0x0004B1D0
		// (set) Token: 0x06001466 RID: 5222 RVA: 0x0004CFE4 File Offset: 0x0004B1E4
		[DefaultValue(DataGridViewImageCellLayout.Normal)]
		public DataGridViewImageCellLayout ImageLayout
		{
			get
			{
				return (base.CellTemplate as DataGridViewImageCell).ImageLayout;
			}
			set
			{
				(base.CellTemplate as DataGridViewImageCell).ImageLayout = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether cells in this column display <see cref="T:System.Drawing.Icon" /> values.</summary>
		/// <returns>true if cells display values of type <see cref="T:System.Drawing.Icon" />; false if cells display values of type <see cref="T:System.Drawing.Image" />. The default is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewImageColumn.CellTemplate" /> property is null.</exception>
		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x0004CFF8 File Offset: 0x0004B1F8
		// (set) Token: 0x06001468 RID: 5224 RVA: 0x0004D000 File Offset: 0x0004B200
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public bool ValuesAreIcons
		{
			get
			{
				return this.valuesAreIcons;
			}
			set
			{
				this.valuesAreIcons = value;
			}
		}

		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001469 RID: 5225 RVA: 0x0004D00C File Offset: 0x0004B20C
		public override object Clone()
		{
			DataGridViewImageColumn dataGridViewImageColumn = (DataGridViewImageColumn)base.Clone();
			dataGridViewImageColumn.icon = this.icon;
			dataGridViewImageColumn.image = this.image;
			return dataGridViewImageColumn;
		}

		/// <returns>A <see cref="T:System.String" /> that describes the column.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600146A RID: 5226 RVA: 0x0004D040 File Offset: 0x0004B240
		public override string ToString()
		{
			return base.GetType().Name;
		}

		// Token: 0x04000BD0 RID: 3024
		private Icon icon;

		// Token: 0x04000BD1 RID: 3025
		private Image image;

		// Token: 0x04000BD2 RID: 3026
		private bool valuesAreIcons;
	}
}
