using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Hosts a collection of <see cref="T:System.Windows.Forms.DataGridViewTextBoxCell" /> cells.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000139 RID: 313
	[ToolboxBitmap("")]
	public class DataGridViewTextBoxColumn : DataGridViewColumn
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewTextBoxColumn" /> class to the default state.</summary>
		// Token: 0x060015ED RID: 5613 RVA: 0x000516EC File Offset: 0x0004F8EC
		public DataGridViewTextBoxColumn()
		{
			base.CellTemplate = new DataGridViewTextBoxCell();
			this.maxInputLength = 32767;
			base.SortMode = DataGridViewColumnSortMode.Automatic;
		}

		/// <summary>Gets or sets the template used to model cell appearance.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCell" /> that all other cells in the column are modeled after.</returns>
		/// <exception cref="T:System.InvalidCastException">The set type is not compatible with type <see cref="T:System.Windows.Forms.DataGridViewTextBoxCell" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060015EE RID: 5614 RVA: 0x00051714 File Offset: 0x0004F914
		// (set) Token: 0x060015EF RID: 5615 RVA: 0x0005171C File Offset: 0x0004F91C
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
				base.CellTemplate = value as DataGridViewTextBoxCell;
			}
		}

		/// <summary>Gets or sets the maximum number of characters that can be entered into the text box.</summary>
		/// <returns>The maximum number of characters that can be entered into the text box; the default value is 32767.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewTextBoxColumn.CellTemplate" /> property is null.</exception>
		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060015F0 RID: 5616 RVA: 0x0005172C File Offset: 0x0004F92C
		// (set) Token: 0x060015F1 RID: 5617 RVA: 0x00051734 File Offset: 0x0004F934
		[DefaultValue(32767)]
		public int MaxInputLength
		{
			get
			{
				return this.maxInputLength;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("Value is less than 0.");
				}
				this.maxInputLength = value;
			}
		}

		/// <summary>Gets or sets the sort mode for the column.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewColumnSortMode" /> that specifies the criteria used to order the rows based on the cell values in a column.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060015F2 RID: 5618 RVA: 0x00051750 File Offset: 0x0004F950
		// (set) Token: 0x060015F3 RID: 5619 RVA: 0x00051758 File Offset: 0x0004F958
		[DefaultValue(DataGridViewColumnSortMode.Automatic)]
		public new DataGridViewColumnSortMode SortMode
		{
			get
			{
				return base.SortMode;
			}
			set
			{
				base.SortMode = value;
			}
		}

		/// <returns>A <see cref="T:System.String" /> that describes the column.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015F4 RID: 5620 RVA: 0x00051764 File Offset: 0x0004F964
		public override string ToString()
		{
			return string.Format("DataGridViewTextBoxColumn {{ Name={0}, Index={1} }}", base.Name, base.Index);
		}

		// Token: 0x04000C33 RID: 3123
		private int maxInputLength;
	}
}
