using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>A column type for the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control that contains a user-defined button.</summary>
	// Token: 0x02000341 RID: 833
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ButtonColumn : DataGridColumn
	{
		/// <summary>Gets or sets the type of button to display in the <see cref="T:System.Web.UI.WebControls.ButtonColumn" /> object.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonColumnType" /> values. The default is LinkButton.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified column type is not one of the <see cref="T:System.Web.UI.WebControls.ButtonColumnType" /> values. </exception>
		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06001DB8 RID: 7608 RVA: 0x0004A37A File Offset: 0x0004857A
		// (set) Token: 0x06001DB9 RID: 7609 RVA: 0x0004A38D File Offset: 0x0004858D
		[WebCategory("Misc")]
		[DefaultValue(ButtonColumnType.LinkButton)]
		[WebSysDescription("The type of button contained within the column.")]
		public virtual ButtonColumnType ButtonType
		{
			get
			{
				return (ButtonColumnType)base.ViewState.GetInt("LinkButton", 0);
			}
			set
			{
				base.ViewState["LinkButton"] = value;
			}
		}

		/// <summary>Gets or sets a string that represents the command to perform when a button in the <see cref="T:System.Web.UI.WebControls.ButtonColumn" /> object is clicked.</summary>
		/// <returns>A string that represents the command to perform when a button in the <see cref="T:System.Web.UI.WebControls.ButtonColumn" /> is clicked. The default is an empty string ("").</returns>
		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06001DBA RID: 7610 RVA: 0x0004A3A5 File Offset: 0x000485A5
		// (set) Token: 0x06001DBB RID: 7611 RVA: 0x0004A3BC File Offset: 0x000485BC
		[DefaultValue("")]
		[WebSysDescription("The command associated with the button.")]
		[WebCategory("Misc")]
		public virtual string CommandName
		{
			get
			{
				return base.ViewState.GetString("CommandName", string.Empty);
			}
			set
			{
				base.ViewState["CommandName"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether validation is performed when a button in the <see cref="T:System.Web.UI.WebControls.ButtonColumn" /> object is clicked.</summary>
		/// <returns>true if validation is performed when a button in the <see cref="T:System.Web.UI.WebControls.ButtonColumn" /> is clicked; otherwise, false. The default is false.</returns>
		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06001DBC RID: 7612 RVA: 0x0004A3CF File Offset: 0x000485CF
		// (set) Token: 0x06001DBD RID: 7613 RVA: 0x0004A3E2 File Offset: 0x000485E2
		[DefaultValue(false)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual bool CausesValidation
		{
			get
			{
				return base.ViewState.GetBool("CausesValidation", false);
			}
			set
			{
				base.ViewState["CausesValidation"] = value;
			}
		}

		/// <summary>Gets or sets the field name from a data source to bind to the <see cref="T:System.Web.UI.WebControls.ButtonColumn" /> object.</summary>
		/// <returns>The field name to bind to the <see cref="T:System.Web.UI.WebControls.ButtonColumn" />. The default is an empty string ("").</returns>
		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06001DBE RID: 7614 RVA: 0x0004A3FA File Offset: 0x000485FA
		// (set) Token: 0x06001DBF RID: 7615 RVA: 0x0004A411 File Offset: 0x00048611
		[WebSysDescription("The field bound to the text property of the button.")]
		[DefaultValue("")]
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

		/// <summary>Gets or sets the string that specifies the display format for the caption in each button.</summary>
		/// <returns>The string that specifies the display format for the caption in each button. The default is an empty string ("").</returns>
		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06001DC0 RID: 7616 RVA: 0x0004A424 File Offset: 0x00048624
		// (set) Token: 0x06001DC1 RID: 7617 RVA: 0x0004A43B File Offset: 0x0004863B
		[WebSysDescription("The formatting applied to the value bound to the Text property.")]
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
				this.format = null;
			}
		}

		/// <summary>Gets or sets the caption that is displayed in the buttons of the <see cref="T:System.Web.UI.WebControls.ButtonColumn" /> object.</summary>
		/// <returns>The caption displayed in the buttons of the <see cref="T:System.Web.UI.WebControls.ButtonColumn" />. The default is an empty string ("").</returns>
		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06001DC2 RID: 7618 RVA: 0x0004A455 File Offset: 0x00048655
		// (set) Token: 0x06001DC3 RID: 7619 RVA: 0x0004A46C File Offset: 0x0004866C
		[DefaultValue("")]
		[WebSysDescription("The text used for the button.")]
		[WebCategory("Misc")]
		[Localizable(true)]
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

		/// <summary>Gets or sets the group of validation controls for which the <see cref="T:System.Web.UI.WebControls.ButtonColumn" /> object causes validation when it posts back to the server.</summary>
		/// <returns>The group of validation controls for which the <see cref="T:System.Web.UI.WebControls.ButtonColumn" /> object causes validation when it posts back to the server. The default is an empty string ("").</returns>
		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06001DC4 RID: 7620 RVA: 0x0004A47F File Offset: 0x0004867F
		// (set) Token: 0x06001DC5 RID: 7621 RVA: 0x0004A496 File Offset: 0x00048696
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual string ValidationGroup
		{
			get
			{
				return base.ViewState.GetString("ValidationGroup", string.Empty);
			}
			set
			{
				base.ViewState["ValidationGroup"] = value;
			}
		}

		/// <summary>Resets the <see cref="T:System.Web.UI.WebControls.ButtonColumn" /> object to its initial state.</summary>
		// Token: 0x06001DC6 RID: 7622 RVA: 0x0004A4A9 File Offset: 0x000486A9
		public override void Initialize()
		{
			base.Initialize();
		}

		/// <summary>Resets a cell in the <see cref="T:System.Web.UI.WebControls.ButtonColumn" /> object to its initial state.</summary>
		/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.TableCell" /> that represents the cell to reset. </param>
		/// <param name="columnIndex">The column number where the cell is located. </param>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values. </param>
		// Token: 0x06001DC7 RID: 7623 RVA: 0x0004A4B4 File Offset: 0x000486B4
		public override void InitializeCell(TableCell cell, int columnIndex, ListItemType itemType)
		{
			base.InitializeCell(cell, columnIndex, itemType);
			if (itemType != ListItemType.Header && itemType != ListItemType.Footer)
			{
				ButtonColumnType buttonType = this.ButtonType;
				if (buttonType == ButtonColumnType.LinkButton)
				{
					LinkButton linkButton = new DataGridColumn.ForeColorLinkButton();
					linkButton.Text = this.Text;
					linkButton.CommandName = this.CommandName;
					if (!string.IsNullOrEmpty(this.DataTextField))
					{
						linkButton.DataBinding += this.DoDataBind;
					}
					cell.Controls.Add(linkButton);
					return;
				}
				if (buttonType != ButtonColumnType.PushButton)
				{
					return;
				}
				Button button = new Button();
				button.Text = this.Text;
				button.CommandName = this.CommandName;
				if (!string.IsNullOrEmpty(this.DataTextField))
				{
					button.DataBinding += this.DoDataBind;
				}
				cell.Controls.Add(button);
			}
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x0004A57C File Offset: 0x0004877C
		private string GetValueFromItem(DataGridItem item)
		{
			object obj = null;
			if (this.text_field == null)
			{
				this.text_field = this.DataTextField;
			}
			if (!string.IsNullOrEmpty(this.text_field))
			{
				obj = DataBinder.Eval(item.DataItem, this.text_field);
			}
			return this.FormatDataTextValue(obj);
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x0004A5C8 File Offset: 0x000487C8
		private void DoDataBind(object sender, EventArgs e)
		{
			Control control = (Control)sender;
			string valueFromItem = this.GetValueFromItem((DataGridItem)control.NamingContainer);
			LinkButton linkButton = sender as LinkButton;
			if (linkButton == null)
			{
				((Button)sender).Text = valueFromItem;
				return;
			}
			linkButton.Text = valueFromItem;
		}

		/// <summary>Converts the specified value to the format that is indicated by the <see cref="P:System.Web.UI.WebControls.ButtonColumn.DataTextFormatString" /> property.</summary>
		/// <returns>The <paramref name="dataTextValue" /> converted to the format indicated by the <see cref="P:System.Web.UI.WebControls.ButtonColumn.DataTextFormatString" />.</returns>
		/// <param name="dataTextValue">The value to format. </param>
		// Token: 0x06001DCA RID: 7626 RVA: 0x0004A60C File Offset: 0x0004880C
		protected virtual string FormatDataTextValue(object dataTextValue)
		{
			if (dataTextValue == null)
			{
				return string.Empty;
			}
			if (this.format == null)
			{
				this.format = this.DataTextFormatString;
			}
			if (string.IsNullOrEmpty(this.format))
			{
				return dataTextValue.ToString();
			}
			return string.Format(this.format, dataTextValue);
		}

		// Token: 0x0400183A RID: 6202
		private string text_field;

		// Token: 0x0400183B RID: 6203
		private string format;
	}
}
