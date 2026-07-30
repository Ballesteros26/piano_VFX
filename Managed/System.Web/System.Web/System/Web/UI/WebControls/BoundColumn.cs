using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>A column type for the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control that is bound to a field in a data source. </summary>
	// Token: 0x0200033B RID: 827
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class BoundColumn : DataGridColumn
	{
		/// <summary>Gets or sets the field name from the data source to bind to the <see cref="T:System.Web.UI.WebControls.BoundColumn" />.</summary>
		/// <returns>The name of the field to bind to the <see cref="T:System.Web.UI.WebControls.BoundColumn" />. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06001D45 RID: 7493 RVA: 0x0004923F File Offset: 0x0004743F
		// (set) Token: 0x06001D46 RID: 7494 RVA: 0x00049256 File Offset: 0x00047456
		[WebCategory("Misc")]
		[WebSysDescription("")]
		[DefaultValue("")]
		public virtual string DataField
		{
			get
			{
				return base.ViewState.GetString("DataField", string.Empty);
			}
			set
			{
				base.ViewState["DataField"] = value;
			}
		}

		/// <summary>Gets or sets the string that specifies the display format for items in the column.</summary>
		/// <returns>A formatting string that specifies the display format of items in the column. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06001D47 RID: 7495 RVA: 0x00049269 File Offset: 0x00047469
		// (set) Token: 0x06001D48 RID: 7496 RVA: 0x00049280 File Offset: 0x00047480
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		public virtual string DataFormatString
		{
			get
			{
				return base.ViewState.GetString("DataFormatString", string.Empty);
			}
			set
			{
				base.ViewState["DataFormatString"] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the items in the <see cref="T:System.Web.UI.WebControls.BoundColumn" /> can be edited.</summary>
		/// <returns>true if the items in the <see cref="T:System.Web.UI.WebControls.BoundColumn" /> cannot be edited; otherwise, false. The default value is false.</returns>
		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06001D49 RID: 7497 RVA: 0x00049293 File Offset: 0x00047493
		// (set) Token: 0x06001D4A RID: 7498 RVA: 0x000492A6 File Offset: 0x000474A6
		[DefaultValue(false)]
		[WebCategory("Misc")]
		[WebSysDescription("")]
		public virtual bool ReadOnly
		{
			get
			{
				return base.ViewState.GetBool("ReadOnly", false);
			}
			set
			{
				base.ViewState["ReadOnly"] = value;
			}
		}

		/// <summary>Resets the <see cref="T:System.Web.UI.WebControls.BoundColumn" /> to its initial state.</summary>
		// Token: 0x06001D4B RID: 7499 RVA: 0x000492BE File Offset: 0x000474BE
		public override void Initialize()
		{
			this.data_format_string = this.DataFormatString;
		}

		/// <summary>Resets the specified cell in the <see cref="T:System.Web.UI.WebControls.BoundColumn" /> to its initial state.</summary>
		/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.TableCell" /> object that represents the cell to reset. </param>
		/// <param name="columnIndex">The column number where the cell is located. </param>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values. </param>
		// Token: 0x06001D4C RID: 7500 RVA: 0x000492CC File Offset: 0x000474CC
		public override void InitializeCell(TableCell cell, int columnIndex, ListItemType itemType)
		{
			base.InitializeCell(cell, columnIndex, itemType);
			string dataField = this.DataField;
			if (itemType - ListItemType.Item > 2)
			{
				if (itemType != ListItemType.EditItem)
				{
					return;
				}
				if (this.ReadOnly && dataField != null && dataField.Length != 0)
				{
					cell.DataBinding += this.ItemDataBinding;
					return;
				}
				TextBox textBox = new TextBox();
				if (dataField != null && dataField.Length != 0)
				{
					textBox.DataBinding += this.ItemDataBinding;
				}
				cell.Controls.Add(textBox);
			}
			else if (dataField != null && dataField.Length != 0)
			{
				cell.DataBinding += this.ItemDataBinding;
				return;
			}
		}

		/// <summary>Converts the specified value to the format indicated by the <see cref="P:System.Web.UI.WebControls.BoundColumn.DataFormatString" /> property.</summary>
		/// <returns>The specified value converted to the format indicated by the <see cref="P:System.Web.UI.WebControls.BoundColumn.DataFormatString" /> property.</returns>
		/// <param name="dataValue">The value to format. </param>
		// Token: 0x06001D4D RID: 7501 RVA: 0x00049367 File Offset: 0x00047567
		protected virtual string FormatDataValue(object dataValue)
		{
			if (dataValue == null)
			{
				return "";
			}
			if (this.data_format_string == string.Empty)
			{
				return dataValue.ToString();
			}
			return string.Format(this.data_format_string, dataValue);
		}

		// Token: 0x06001D4E RID: 7502 RVA: 0x00049398 File Offset: 0x00047598
		private string GetValueFromItem(DataGridItem item)
		{
			object obj;
			if (this.DataField != BoundColumn.thisExpr)
			{
				obj = DataBinder.Eval(item.DataItem, this.DataField);
			}
			else
			{
				obj = item.DataItem;
			}
			string text = this.FormatDataValue(obj);
			if (!(text != ""))
			{
				return "&nbsp;";
			}
			return text;
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x000493F0 File Offset: 0x000475F0
		private void ItemDataBinding(object sender, EventArgs e)
		{
			Control control = (Control)sender;
			string valueFromItem = this.GetValueFromItem((DataGridItem)control.NamingContainer);
			TableCell tableCell = sender as TableCell;
			if (tableCell == null)
			{
				((TextBox)sender).Text = valueFromItem;
				return;
			}
			tableCell.Text = valueFromItem;
		}

		// Token: 0x04001824 RID: 6180
		private string data_format_string;

		/// <summary>Represents the string "!". This field is read-only.</summary>
		// Token: 0x04001825 RID: 6181
		public static readonly string thisExpr = "!";
	}
}
