using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Represents a column of cells that contain links in a <see cref="T:System.Windows.Forms.DataGridView" /> control. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200011D RID: 285
	[ToolboxBitmap("")]
	public class DataGridViewLinkColumn : DataGridViewColumn
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewLinkColumn" /> class. </summary>
		// Token: 0x06001494 RID: 5268 RVA: 0x0004D62C File Offset: 0x0004B82C
		public DataGridViewLinkColumn()
		{
			base.CellTemplate = new DataGridViewLinkCell();
		}

		/// <summary>Creates an exact copy of this column.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewLinkColumn" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.CellTemplate" /> property is null. </exception>
		// Token: 0x06001495 RID: 5269 RVA: 0x0004D64C File Offset: 0x0004B84C
		public override object Clone()
		{
			DataGridViewLinkColumn dataGridViewLinkColumn = (DataGridViewLinkColumn)base.Clone();
			dataGridViewLinkColumn.CellTemplate = (DataGridViewCell)this.CellTemplate.Clone();
			return dataGridViewLinkColumn;
		}

		/// <returns>A <see cref="T:System.String" /> that describes the column.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001496 RID: 5270 RVA: 0x0004D67C File Offset: 0x0004B87C
		public override string ToString()
		{
			return base.ToString();
		}

		/// <summary>Gets or sets the color used to display an active link within cells in the column. </summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color used to display a link that is being selected. The default value is the user's Internet Explorer setting for the color of links in the hover state.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.CellTemplate" /> property is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001497 RID: 5271 RVA: 0x0004D684 File Offset: 0x0004B884
		// (set) Token: 0x06001498 RID: 5272 RVA: 0x0004D6B4 File Offset: 0x0004B8B4
		public Color ActiveLinkColor
		{
			get
			{
				DataGridViewLinkCell dataGridViewLinkCell = this.CellTemplate as DataGridViewLinkCell;
				if (dataGridViewLinkCell == null)
				{
					throw new InvalidOperationException("CellTemplate is null when getting this property.");
				}
				return dataGridViewLinkCell.ActiveLinkColor;
			}
			set
			{
				if (this.ActiveLinkColor == value)
				{
					return;
				}
				DataGridViewLinkCell dataGridViewLinkCell = this.CellTemplate as DataGridViewLinkCell;
				if (dataGridViewLinkCell == null)
				{
					throw new InvalidOperationException("CellTemplate is null when getting this property.");
				}
				dataGridViewLinkCell.ActiveLinkColor = value;
				foreach (object obj in base.DataGridView.Rows)
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
					DataGridViewLinkCell dataGridViewLinkCell2 = dataGridViewRow.Cells[base.Index] as DataGridViewLinkCell;
					if (dataGridViewLinkCell2 != null)
					{
						dataGridViewLinkCell2.ActiveLinkColor = value;
					}
				}
				base.DataGridView.InvalidateColumn(base.Index);
			}
		}

		/// <summary>Gets or sets the template used to create new cells.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCell" /> that all other cells in the column are modeled after. The default value is a new <see cref="T:System.Windows.Forms.DataGridViewLinkCell" /> instance.</returns>
		/// <exception cref="T:System.InvalidCastException">When setting this property to a value that is not of type <see cref="T:System.Windows.Forms.DataGridViewLinkCell" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001499 RID: 5273 RVA: 0x0004D790 File Offset: 0x0004B990
		// (set) Token: 0x0600149A RID: 5274 RVA: 0x0004D798 File Offset: 0x0004B998
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
				base.CellTemplate = value as DataGridViewLinkCell;
			}
		}

		/// <summary>Gets or sets a value that represents the behavior of links within cells in the column.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.LinkBehavior" /> value indicating the link behavior. The default is <see cref="F:System.Windows.Forms.LinkBehavior.SystemDefault" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.CellTemplate" /> property is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x0004D7A8 File Offset: 0x0004B9A8
		// (set) Token: 0x0600149C RID: 5276 RVA: 0x0004D7D8 File Offset: 0x0004B9D8
		[DefaultValue(LinkBehavior.SystemDefault)]
		public LinkBehavior LinkBehavior
		{
			get
			{
				DataGridViewLinkCell dataGridViewLinkCell = this.CellTemplate as DataGridViewLinkCell;
				if (dataGridViewLinkCell == null)
				{
					throw new InvalidOperationException("CellTemplate is null when getting this property.");
				}
				return dataGridViewLinkCell.LinkBehavior;
			}
			set
			{
				if (this.LinkBehavior == value)
				{
					return;
				}
				DataGridViewLinkCell dataGridViewLinkCell = this.CellTemplate as DataGridViewLinkCell;
				if (dataGridViewLinkCell == null)
				{
					throw new InvalidOperationException("CellTemplate is null when getting this property.");
				}
				dataGridViewLinkCell.LinkBehavior = value;
				foreach (object obj in base.DataGridView.Rows)
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
					DataGridViewLinkCell dataGridViewLinkCell2 = dataGridViewRow.Cells[base.Index] as DataGridViewLinkCell;
					if (dataGridViewLinkCell2 != null)
					{
						dataGridViewLinkCell2.LinkBehavior = value;
					}
				}
				base.DataGridView.InvalidateColumn(base.Index);
			}
		}

		/// <summary>Gets or sets the color used to display an unselected link within cells in the column.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color used to initially display a link. The default value is the user's Internet Explorer setting for the link color. </returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.CellTemplate" /> property is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x0600149D RID: 5277 RVA: 0x0004D8B0 File Offset: 0x0004BAB0
		// (set) Token: 0x0600149E RID: 5278 RVA: 0x0004D8E0 File Offset: 0x0004BAE0
		public Color LinkColor
		{
			get
			{
				DataGridViewLinkCell dataGridViewLinkCell = this.CellTemplate as DataGridViewLinkCell;
				if (dataGridViewLinkCell == null)
				{
					throw new InvalidOperationException("CellTemplate is null when getting this property.");
				}
				return dataGridViewLinkCell.LinkColor;
			}
			set
			{
				if (this.LinkColor == value)
				{
					return;
				}
				DataGridViewLinkCell dataGridViewLinkCell = this.CellTemplate as DataGridViewLinkCell;
				if (dataGridViewLinkCell == null)
				{
					throw new InvalidOperationException("CellTemplate is null when getting this property.");
				}
				dataGridViewLinkCell.LinkColor = value;
				foreach (object obj in base.DataGridView.Rows)
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
					DataGridViewLinkCell dataGridViewLinkCell2 = dataGridViewRow.Cells[base.Index] as DataGridViewLinkCell;
					if (dataGridViewLinkCell2 != null)
					{
						dataGridViewLinkCell2.LinkColor = value;
					}
				}
				base.DataGridView.InvalidateColumn(base.Index);
			}
		}

		/// <summary>Gets or sets the link text displayed in a column's cells if <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.UseColumnTextForLinkValue" /> is true.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the link text.</returns>
		/// <exception cref="T:System.InvalidOperationException">When setting this property, the value of the <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.CellTemplate" /> property is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x0600149F RID: 5279 RVA: 0x0004D9BC File Offset: 0x0004BBBC
		// (set) Token: 0x060014A0 RID: 5280 RVA: 0x0004D9EC File Offset: 0x0004BBEC
		[MonoInternalNote("")]
		[DefaultValue(null)]
		public string Text
		{
			get
			{
				if (!(this.CellTemplate is DataGridViewLinkCell))
				{
					throw new InvalidOperationException("CellTemplate is null when getting this property.");
				}
				return this.text;
			}
			set
			{
				if (this.Text == value)
				{
					return;
				}
				if (!(this.CellTemplate is DataGridViewLinkCell))
				{
					throw new InvalidOperationException("CellTemplate is null when getting this property.");
				}
				this.text = value;
				base.DataGridView.InvalidateColumn(base.Index);
			}
		}

		/// <summary>Gets or sets a value indicating whether the link changes color if it has been visited.</summary>
		/// <returns>true if the link changes color when it is selected; otherwise, false. The default is true.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.CellTemplate" /> property is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x060014A1 RID: 5281 RVA: 0x0004DA40 File Offset: 0x0004BC40
		// (set) Token: 0x060014A2 RID: 5282 RVA: 0x0004DA70 File Offset: 0x0004BC70
		[DefaultValue(true)]
		public bool TrackVisitedState
		{
			get
			{
				DataGridViewLinkCell dataGridViewLinkCell = this.CellTemplate as DataGridViewLinkCell;
				if (dataGridViewLinkCell == null)
				{
					throw new InvalidOperationException("CellTemplate is null when getting this property.");
				}
				return dataGridViewLinkCell.TrackVisitedState;
			}
			set
			{
				if (this.TrackVisitedState == value)
				{
					return;
				}
				DataGridViewLinkCell dataGridViewLinkCell = this.CellTemplate as DataGridViewLinkCell;
				if (dataGridViewLinkCell == null)
				{
					throw new InvalidOperationException("CellTemplate is null when getting this property.");
				}
				dataGridViewLinkCell.TrackVisitedState = value;
				foreach (object obj in base.DataGridView.Rows)
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
					DataGridViewLinkCell dataGridViewLinkCell2 = dataGridViewRow.Cells[base.Index] as DataGridViewLinkCell;
					if (dataGridViewLinkCell2 != null)
					{
						dataGridViewLinkCell2.TrackVisitedState = value;
					}
				}
				base.DataGridView.InvalidateColumn(base.Index);
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.Text" /> property value is displayed as the link text.</summary>
		/// <returns>true if the <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.Text" /> property value is displayed as the link text; false if the cell <see cref="P:System.Windows.Forms.DataGridViewCell.FormattedValue" /> property value is displayed as the link text. The default is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.CellTemplate" /> property is null.</exception>
		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060014A3 RID: 5283 RVA: 0x0004DB48 File Offset: 0x0004BD48
		// (set) Token: 0x060014A4 RID: 5284 RVA: 0x0004DB78 File Offset: 0x0004BD78
		[DefaultValue(false)]
		public bool UseColumnTextForLinkValue
		{
			get
			{
				DataGridViewLinkCell dataGridViewLinkCell = this.CellTemplate as DataGridViewLinkCell;
				if (dataGridViewLinkCell == null)
				{
					throw new InvalidOperationException("CellTemplate is null when getting this property.");
				}
				return dataGridViewLinkCell.UseColumnTextForLinkValue;
			}
			set
			{
				if (this.UseColumnTextForLinkValue == value)
				{
					return;
				}
				DataGridViewLinkCell dataGridViewLinkCell = this.CellTemplate as DataGridViewLinkCell;
				if (dataGridViewLinkCell == null)
				{
					throw new InvalidOperationException("CellTemplate is null when getting this property.");
				}
				dataGridViewLinkCell.UseColumnTextForLinkValue = value;
				foreach (object obj in base.DataGridView.Rows)
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
					DataGridViewLinkCell dataGridViewLinkCell2 = dataGridViewRow.Cells[base.Index] as DataGridViewLinkCell;
					if (dataGridViewLinkCell2 != null)
					{
						dataGridViewLinkCell2.UseColumnTextForLinkValue = value;
					}
				}
				base.DataGridView.InvalidateColumn(base.Index);
			}
		}

		/// <summary>Gets or sets the color used to display a link that has been previously visited.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color used to display a link that has been visited. The default value is the user's Internet Explorer setting for the visited link color. </returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.CellTemplate" /> property is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060014A5 RID: 5285 RVA: 0x0004DC50 File Offset: 0x0004BE50
		// (set) Token: 0x060014A6 RID: 5286 RVA: 0x0004DC80 File Offset: 0x0004BE80
		public Color VisitedLinkColor
		{
			get
			{
				DataGridViewLinkCell dataGridViewLinkCell = this.CellTemplate as DataGridViewLinkCell;
				if (dataGridViewLinkCell == null)
				{
					throw new InvalidOperationException("CellTemplate is null when getting this property.");
				}
				return dataGridViewLinkCell.VisitedLinkColor;
			}
			set
			{
				if (this.VisitedLinkColor == value)
				{
					return;
				}
				DataGridViewLinkCell dataGridViewLinkCell = this.CellTemplate as DataGridViewLinkCell;
				if (dataGridViewLinkCell == null)
				{
					throw new InvalidOperationException("CellTemplate is null when getting this property.");
				}
				dataGridViewLinkCell.VisitedLinkColor = value;
				foreach (object obj in base.DataGridView.Rows)
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
					DataGridViewLinkCell dataGridViewLinkCell2 = dataGridViewRow.Cells[base.Index] as DataGridViewLinkCell;
					if (dataGridViewLinkCell2 != null)
					{
						dataGridViewLinkCell2.VisitedLinkColor = value;
					}
				}
				base.DataGridView.InvalidateColumn(base.Index);
			}
		}

		// Token: 0x04000BDC RID: 3036
		private string text = string.Empty;
	}
}
