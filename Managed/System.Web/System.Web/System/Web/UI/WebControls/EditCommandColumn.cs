using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>A special column type for the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control that contains the Edit buttons for editing data items in each row.</summary>
	// Token: 0x02000392 RID: 914
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class EditCommandColumn : DataGridColumn
	{
		/// <summary>Gets or sets the button type for the column.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonColumnType" /> values. The default value is LinkButton.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified button type is not one of the <see cref="T:System.Web.UI.WebControls.ButtonColumnType" /> values. </exception>
		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x060023E5 RID: 9189 RVA: 0x0005D244 File Offset: 0x0005B444
		// (set) Token: 0x060023E6 RID: 9190 RVA: 0x0005D26D File Offset: 0x0005B46D
		[DefaultValue(ButtonColumnType.LinkButton)]
		public virtual ButtonColumnType ButtonType
		{
			get
			{
				object obj = base.ViewState["ButtonType"];
				if (obj != null)
				{
					return (ButtonColumnType)obj;
				}
				return ButtonColumnType.LinkButton;
			}
			set
			{
				base.ViewState["ButtonType"] = value;
			}
		}

		/// <summary>Gets or sets the text to display for the Cancel command button in the <see cref="T:System.Web.UI.WebControls.EditCommandColumn" />.</summary>
		/// <returns>The caption to display for the Cancel command button in the <see cref="T:System.Web.UI.WebControls.EditCommandColumn" />.</returns>
		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x060023E7 RID: 9191 RVA: 0x0005D285 File Offset: 0x0005B485
		// (set) Token: 0x060023E8 RID: 9192 RVA: 0x0005D29C File Offset: 0x0005B49C
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string CancelText
		{
			get
			{
				return base.ViewState.GetString("CancelText", string.Empty);
			}
			set
			{
				base.ViewState["CancelText"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether validation is performed when an Update button in the <see cref="T:System.Web.UI.WebControls.EditCommandColumn" /> object is clicked.</summary>
		/// <returns>true if validation is performed when an Update button in the <see cref="T:System.Web.UI.WebControls.EditCommandColumn" /> is clicked; otherwise, false. The default is true.</returns>
		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x060023E9 RID: 9193 RVA: 0x0005D2AF File Offset: 0x0005B4AF
		// (set) Token: 0x060023EA RID: 9194 RVA: 0x0004A3E2 File Offset: 0x000485E2
		[DefaultValue(true)]
		public virtual bool CausesValidation
		{
			get
			{
				return base.ViewState.GetBool("CausesValidation", true);
			}
			set
			{
				base.ViewState["CausesValidation"] = value;
			}
		}

		/// <summary>Gets or sets the group of validation controls for which the <see cref="T:System.Web.UI.WebControls.EditCommandColumn" /> object causes validation when it posts back to the server.</summary>
		/// <returns>The group of validation controls for which the Update button in an <see cref="T:System.Web.UI.WebControls.EditCommandColumn" /> causes validation when it posts back to the server. The default is an empty string ("").</returns>
		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x060023EB RID: 9195 RVA: 0x0004A47F File Offset: 0x0004867F
		// (set) Token: 0x060023EC RID: 9196 RVA: 0x0004A496 File Offset: 0x00048696
		[DefaultValue("")]
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

		/// <summary>Gets or sets the text to display for the Edit button in the <see cref="T:System.Web.UI.WebControls.EditCommandColumn" />.</summary>
		/// <returns>The caption to display for the Edit button in the <see cref="T:System.Web.UI.WebControls.EditCommandColumn" />.</returns>
		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x060023ED RID: 9197 RVA: 0x0005D2C2 File Offset: 0x0005B4C2
		// (set) Token: 0x060023EE RID: 9198 RVA: 0x0005D2D9 File Offset: 0x0005B4D9
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string EditText
		{
			get
			{
				return base.ViewState.GetString("EditText", string.Empty);
			}
			set
			{
				base.ViewState["EditText"] = value;
			}
		}

		/// <summary>Gets or sets the text to display for the Update command button in the <see cref="T:System.Web.UI.WebControls.EditCommandColumn" />.</summary>
		/// <returns>The caption to display for the Update command button in the <see cref="T:System.Web.UI.WebControls.EditCommandColumn" />.</returns>
		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x060023EF RID: 9199 RVA: 0x0005D2EC File Offset: 0x0005B4EC
		// (set) Token: 0x060023F0 RID: 9200 RVA: 0x0005D303 File Offset: 0x0005B503
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string UpdateText
		{
			get
			{
				return base.ViewState.GetString("UpdateText", string.Empty);
			}
			set
			{
				base.ViewState["UpdateText"] = value;
			}
		}

		/// <summary>Initializes a cell within the column.</summary>
		/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.TableCell" /> that contains information about the cell to initialize. </param>
		/// <param name="columnIndex">The column number where the cell is located. </param>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values. </param>
		// Token: 0x060023F1 RID: 9201 RVA: 0x0005D318 File Offset: 0x0005B518
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
			case ListItemType.SelectedItem:
				cell.Controls.Add(this.CreateButton(this.ButtonType, this.EditText, "Edit", false));
				return;
			case ListItemType.EditItem:
				cell.Controls.Add(this.CreateButton(this.ButtonType, this.UpdateText, "Update", this.CausesValidation));
				cell.Controls.Add(new LiteralControl("&nbsp;"));
				cell.Controls.Add(this.CreateButton(this.ButtonType, this.CancelText, "Cancel", false));
				return;
			default:
				return;
			}
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x0005D3DC File Offset: 0x0005B5DC
		private Control CreateButton(ButtonColumnType type, string text, string command, bool valid)
		{
			if (type == ButtonColumnType.LinkButton)
			{
				LinkButton linkButton = new DataGridColumn.ForeColorLinkButton();
				linkButton.Text = text;
				linkButton.CommandName = command;
				linkButton.CausesValidation = valid;
				if (valid)
				{
					linkButton.ValidationGroup = this.ValidationGroup;
				}
				return linkButton;
			}
			Button button = new Button();
			button.Text = text;
			button.CommandName = command;
			button.CausesValidation = valid;
			if (valid)
			{
				button.ValidationGroup = this.ValidationGroup;
			}
			return button;
		}
	}
}
