using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>A column type for the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control that contains a hyperlink for each item in the column.</summary>
	// Token: 0x020003B2 RID: 946
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HyperLinkColumn : DataGridColumn
	{
		/// <summary>Gets or sets the field from a data source to bind to the URL of the hyperlinks in the <see cref="T:System.Web.UI.WebControls.HyperLinkColumn" />.</summary>
		/// <returns>The field from a data source to bind to the URL of the hyperlinks in the <see cref="T:System.Web.UI.WebControls.HyperLinkColumn" />.</returns>
		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x060026B8 RID: 9912 RVA: 0x000651AB File Offset: 0x000633AB
		// (set) Token: 0x060026B9 RID: 9913 RVA: 0x000651C2 File Offset: 0x000633C2
		[DefaultValue("")]
		[WebCategory("Misc")]
		[WebSysDescription("")]
		public virtual string DataNavigateUrlField
		{
			get
			{
				return base.ViewState.GetString("DataNavigateUrlField", string.Empty);
			}
			set
			{
				base.ViewState["DataNavigateUrlField"] = value;
			}
		}

		/// <summary>Gets or sets the display format for the URL of the hyperlinks in the <see cref="T:System.Web.UI.WebControls.HyperLinkColumn" /> when the URL is data-bound to a field in a data source.</summary>
		/// <returns>The string that specifies the display format for the URL of the hyperlinks in the <see cref="T:System.Web.UI.WebControls.HyperLinkColumn" /> when the URL is data-bound to a field in a data source. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x060026BA RID: 9914 RVA: 0x000651D5 File Offset: 0x000633D5
		// (set) Token: 0x060026BB RID: 9915 RVA: 0x000651EC File Offset: 0x000633EC
		[DefaultValue("")]
		[Description("The formatting applied to the value bound to the NavigateUrl property.")]
		[WebCategory("Misc")]
		public virtual string DataNavigateUrlFormatString
		{
			get
			{
				return base.ViewState.GetString("DataNavigateUrlFormatString", string.Empty);
			}
			set
			{
				base.ViewState["DataNavigateUrlFormatString"] = value;
			}
		}

		/// <summary>Gets or sets the field from a data source to bind to the text caption of the hyperlinks in the <see cref="T:System.Web.UI.WebControls.HyperLinkColumn" />.</summary>
		/// <returns>The field name from a data source to bind to the text caption of the hyperlinks in <see cref="T:System.Web.UI.WebControls.HyperLinkColumn" />.</returns>
		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x060026BC RID: 9916 RVA: 0x0004A3FA File Offset: 0x000485FA
		// (set) Token: 0x060026BD RID: 9917 RVA: 0x0004A411 File Offset: 0x00048611
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		public virtual string DataTextField
		{
			get
			{
				return base.ViewState.GetString("DataTextField", string.Empty);
			}
			set
			{
				base.ViewState["DataTextField"] = value;
			}
		}

		/// <summary>Gets or sets the display format for the text caption of the hyperlinks in the <see cref="T:System.Web.UI.WebControls.HyperLinkColumn" /> column.</summary>
		/// <returns>The string that specifies the display format for the text caption of the hyperlinks in the column. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x060026BE RID: 9918 RVA: 0x0004A424 File Offset: 0x00048624
		// (set) Token: 0x060026BF RID: 9919 RVA: 0x000651FF File Offset: 0x000633FF
		[Description("The formatting applied to the value bound to the Text property.")]
		[DefaultValue("")]
		[WebCategory("Misc")]
		public virtual string DataTextFormatString
		{
			get
			{
				return base.ViewState.GetString("DataTextFormatString", string.Empty);
			}
			set
			{
				base.ViewState["DataTextFormatString"] = value;
			}
		}

		/// <summary>Gets or sets the URL to link to when a hyperlink in the column is clicked.</summary>
		/// <returns>The URL to link to when a hyperlink in the column is clicked.</returns>
		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x060026C0 RID: 9920 RVA: 0x00065212 File Offset: 0x00063412
		// (set) Token: 0x060026C1 RID: 9921 RVA: 0x00065229 File Offset: 0x00063429
		[UrlProperty]
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		public virtual string NavigateUrl
		{
			get
			{
				return base.ViewState.GetString("NavigateUrl", string.Empty);
			}
			set
			{
				base.ViewState["NavigateUrl"] = value;
			}
		}

		/// <summary>Gets or sets the target window or frame to display the Web page content that is linked to when the hyperlink in the column is clicked.</summary>
		/// <returns>The target window or frame to load the Web page linked to when a hyperlink in the column is clicked. The default value is an empty string (""), which refreshes the window or frame with focus.</returns>
		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x060026C2 RID: 9922 RVA: 0x0006523C File Offset: 0x0006343C
		// (set) Token: 0x060026C3 RID: 9923 RVA: 0x00065253 File Offset: 0x00063453
		[WebCategory("Misc")]
		[TypeConverter("System.Web.UI.WebControls.TargetConverter")]
		[DefaultValue("")]
		[WebSysDescription("")]
		public virtual string Target
		{
			get
			{
				return base.ViewState.GetString("Target", string.Empty);
			}
			set
			{
				base.ViewState["Target"] = value;
			}
		}

		/// <summary>Gets or sets the text caption to display for the hyperlinks in the column.</summary>
		/// <returns>The text caption for the hyperlinks in the column. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x060026C4 RID: 9924 RVA: 0x0004A455 File Offset: 0x00048655
		// (set) Token: 0x060026C5 RID: 9925 RVA: 0x0004A46C File Offset: 0x0004866C
		[WebCategory("Misc")]
		[DefaultValue("")]
		[Localizable(true)]
		[WebSysDescription("")]
		public virtual string Text
		{
			get
			{
				return base.ViewState.GetString("Text", string.Empty);
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}

		/// <summary>Formats a data-bound URL using the format specified by the <see cref="P:System.Web.UI.WebControls.HyperLinkColumn.DataNavigateUrlFormatString" /> property.</summary>
		/// <returns>The data-bound URL in the format specified by the <see cref="P:System.Web.UI.WebControls.HyperLinkColumn.DataNavigateUrlFormatString" /> property.</returns>
		/// <param name="dataUrlValue">The data-bound URL to format. </param>
		// Token: 0x060026C6 RID: 9926 RVA: 0x00065268 File Offset: 0x00063468
		protected virtual string FormatDataNavigateUrlValue(object dataUrlValue)
		{
			string text = this.DataNavigateUrlFormatString;
			if (text == "")
			{
				text = null;
			}
			return DataBinder.FormatResult(dataUrlValue, text);
		}

		/// <summary>Formats a data-bound text caption using the format specified by the <see cref="P:System.Web.UI.WebControls.HyperLinkColumn.DataTextFormatString" /> property.</summary>
		/// <returns>The data-bound text caption in the format specified by the <see cref="P:System.Web.UI.WebControls.HyperLinkColumn.DataTextFormatString" /> property.</returns>
		/// <param name="dataTextValue">The data-bound URL to format. </param>
		// Token: 0x060026C7 RID: 9927 RVA: 0x00065294 File Offset: 0x00063494
		protected virtual string FormatDataTextValue(object dataTextValue)
		{
			string text = this.DataTextFormatString;
			if (text == "")
			{
				text = null;
			}
			return DataBinder.FormatResult(dataTextValue, text);
		}

		// Token: 0x060026C8 RID: 9928 RVA: 0x0004A4A9 File Offset: 0x000486A9
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x000652C0 File Offset: 0x000634C0
		private void ItemDataBinding(object sender, EventArgs args)
		{
			TableCell tableCell = (TableCell)sender;
			HyperLink hyperLink = (HyperLink)tableCell.Controls[0];
			DataGridItem dataGridItem = (DataGridItem)tableCell.NamingContainer;
			if (this.DataNavigateUrlField != "")
			{
				hyperLink.NavigateUrl = this.FormatDataNavigateUrlValue(DataBinder.Eval(dataGridItem.DataItem, this.DataNavigateUrlField));
			}
			else
			{
				hyperLink.NavigateUrl = this.NavigateUrl;
			}
			if (this.DataTextField != "")
			{
				hyperLink.Text = this.FormatDataTextValue(DataBinder.Eval(dataGridItem.DataItem, this.DataTextField));
			}
			else
			{
				hyperLink.Text = this.Text;
			}
			hyperLink.Target = this.Target;
		}

		/// <summary>Initializes the cell representing this column with the contained hyperlink.</summary>
		/// <param name="cell">The cell to be initialized. </param>
		/// <param name="columnIndex">The index of the column that contains the cell. </param>
		/// <param name="itemType">The type of item that the cell is part of. </param>
		// Token: 0x060026CA RID: 9930 RVA: 0x00065378 File Offset: 0x00063578
		public override void InitializeCell(TableCell cell, int columnIndex, ListItemType itemType)
		{
			base.InitializeCell(cell, columnIndex, itemType);
			switch (itemType)
			{
			case ListItemType.Header:
			case ListItemType.Footer:
			case ListItemType.Separator:
			case ListItemType.Pager:
				return;
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			case ListItemType.EditItem:
				cell.DataBinding += this.ItemDataBinding;
				cell.Controls.Add(new HyperLink());
				break;
			case ListItemType.SelectedItem:
				break;
			default:
				return;
			}
		}
	}
}
