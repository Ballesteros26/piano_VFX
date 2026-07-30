using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Hosts a collection of <see cref="T:System.Windows.Forms.DataGridViewButtonCell" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000E2 RID: 226
	[ToolboxBitmap("")]
	public class DataGridViewButtonColumn : DataGridViewColumn
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewButtonColumn" /> class to the default state.</summary>
		// Token: 0x06001175 RID: 4469 RVA: 0x0004572C File Offset: 0x0004392C
		public DataGridViewButtonColumn()
		{
			base.CellTemplate = new DataGridViewButtonCell();
			this.flatStyle = FlatStyle.Standard;
			this.text = string.Empty;
		}

		/// <summary>Gets or sets the template used to create new cells.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCell" /> that all other cells in the column are modeled after.</returns>
		/// <exception cref="T:System.InvalidCastException">The specified value when setting this property could not be cast to a <see cref="T:System.Windows.Forms.DataGridViewButtonCell" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x00045754 File Offset: 0x00043954
		// (set) Token: 0x06001177 RID: 4471 RVA: 0x0004575C File Offset: 0x0004395C
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
				base.CellTemplate = value as DataGridViewButtonCell;
			}
		}

		/// <summary>Gets or sets the column's default cell style.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied as the default style.</returns>
		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x0004576C File Offset: 0x0004396C
		// (set) Token: 0x06001179 RID: 4473 RVA: 0x00045774 File Offset: 0x00043974
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

		/// <summary>Gets or sets the flat-style appearance of the button cells in the column.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.FlatStyle" /> value indicating the appearance of the buttons in the column. The default is <see cref="F:System.Windows.Forms.FlatStyle.Standard" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewButtonColumn.CellTemplate" /> property is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003BC RID: 956
		// (get) Token: 0x0600117A RID: 4474 RVA: 0x00045780 File Offset: 0x00043980
		// (set) Token: 0x0600117B RID: 4475 RVA: 0x00045788 File Offset: 0x00043988
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

		/// <summary>Gets or sets the default text displayed on the button cell.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the text. The default is <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">When setting this property, the value of the <see cref="P:System.Windows.Forms.DataGridViewButtonColumn.CellTemplate" /> property is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003BD RID: 957
		// (get) Token: 0x0600117C RID: 4476 RVA: 0x00045794 File Offset: 0x00043994
		// (set) Token: 0x0600117D RID: 4477 RVA: 0x0004579C File Offset: 0x0004399C
		[DefaultValue(null)]
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="P:System.Windows.Forms.DataGridViewButtonColumn.Text" /> property value is displayed as the button text for cells in this column.</summary>
		/// <returns>true if the <see cref="P:System.Windows.Forms.DataGridViewButtonColumn.Text" /> property value is displayed on buttons in the column; false if the <see cref="P:System.Windows.Forms.DataGridViewCell.FormattedValue" /> property value of each cell is displayed on its button. The default is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewButtonColumn.CellTemplate" /> property is null.</exception>
		// Token: 0x170003BE RID: 958
		// (get) Token: 0x0600117E RID: 4478 RVA: 0x000457A8 File Offset: 0x000439A8
		// (set) Token: 0x0600117F RID: 4479 RVA: 0x000457DC File Offset: 0x000439DC
		[DefaultValue(false)]
		public bool UseColumnTextForButtonValue
		{
			get
			{
				if (base.CellTemplate == null)
				{
					throw new InvalidOperationException("CellTemplate is null when getting this property.");
				}
				return (base.CellTemplate as DataGridViewButtonCell).UseColumnTextForButtonValue;
			}
			set
			{
				if (base.CellTemplate == null)
				{
					throw new InvalidOperationException("CellTemplate is null when setting this property.");
				}
				(base.CellTemplate as DataGridViewButtonCell).UseColumnTextForButtonValue = value;
			}
		}

		/// <summary>Creates an exact copy of this column.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewButtonColumn" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewButtonColumn.CellTemplate" /> property is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001180 RID: 4480 RVA: 0x00045808 File Offset: 0x00043A08
		public override object Clone()
		{
			DataGridViewButtonColumn dataGridViewButtonColumn = (DataGridViewButtonColumn)base.Clone();
			dataGridViewButtonColumn.flatStyle = this.flatStyle;
			dataGridViewButtonColumn.text = this.text;
			return dataGridViewButtonColumn;
		}

		/// <returns>A <see cref="T:System.String" /> that describes the column.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001181 RID: 4481 RVA: 0x0004583C File Offset: 0x00043A3C
		public override string ToString()
		{
			return string.Format("DataGridViewButtonColumn {{ Name={0}, Index={1} }}", base.Name, base.Index);
		}

		// Token: 0x04000AD7 RID: 2775
		private FlatStyle flatStyle;

		// Token: 0x04000AD8 RID: 2776
		private string text;
	}
}
