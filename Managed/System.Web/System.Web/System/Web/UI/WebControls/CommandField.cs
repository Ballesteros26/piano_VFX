using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a special field that displays command buttons to perform selecting, editing, inserting, or deleting operations in a data-bound control.</summary>
	// Token: 0x02000353 RID: 851
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class CommandField : ButtonFieldBase
	{
		/// <summary>Gets or sets the URL to an image to display for the Cancel button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>The URL to an image to display for the Cancel button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06001F74 RID: 8052 RVA: 0x0004F94B File Offset: 0x0004DB4B
		// (set) Token: 0x06001F75 RID: 8053 RVA: 0x0004F962 File Offset: 0x0004DB62
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		[UrlProperty]
		public virtual string CancelImageUrl
		{
			get
			{
				return base.ViewState.GetString("CancelImageUrl", string.Empty);
			}
			set
			{
				base.ViewState["CancelImageUrl"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the caption for the Cancel button displayed in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>The caption for the Cancel button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is "Cancel".</returns>
		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06001F76 RID: 8054 RVA: 0x0004F97B File Offset: 0x0004DB7B
		// (set) Token: 0x06001F77 RID: 8055 RVA: 0x0004F992 File Offset: 0x0004DB92
		[Localizable(true)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual string CancelText
		{
			get
			{
				return base.ViewState.GetString("CancelText", "Cancel");
			}
			set
			{
				base.ViewState["CancelText"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets a value indicating whether validation is performed when the user clicks a button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>true to perform validation when the user clicks a button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field; otherwise, false. The default is true.</returns>
		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06001F78 RID: 8056 RVA: 0x0004F9AB File Offset: 0x0004DBAB
		// (set) Token: 0x06001F79 RID: 8057 RVA: 0x0004A979 File Offset: 0x00048B79
		[DefaultValue(true)]
		public override bool CausesValidation
		{
			get
			{
				return base.ViewState.GetBool("CausesValidation", true);
			}
			set
			{
				base.ViewState["CausesValidation"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the URL to an image to display for a Delete button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>The URL to an image to display for a Delete button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06001F7A RID: 8058 RVA: 0x0004F9BE File Offset: 0x0004DBBE
		// (set) Token: 0x06001F7B RID: 8059 RVA: 0x0004F9D5 File Offset: 0x0004DBD5
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebSysDescription("")]
		[UrlProperty]
		public virtual string DeleteImageUrl
		{
			get
			{
				return base.ViewState.GetString("DeleteImageUrl", string.Empty);
			}
			set
			{
				base.ViewState["DeleteImageUrl"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the caption for a Delete button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>The caption for a Delete button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is "Delete".</returns>
		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06001F7C RID: 8060 RVA: 0x0004F9EE File Offset: 0x0004DBEE
		// (set) Token: 0x06001F7D RID: 8061 RVA: 0x0004FA05 File Offset: 0x0004DC05
		[Localizable(true)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual string DeleteText
		{
			get
			{
				return base.ViewState.GetString("DeleteText", "Delete");
			}
			set
			{
				base.ViewState["DeleteText"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the URL to an image to display for an Edit button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>The URL to an image to display for an Edit button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06001F7E RID: 8062 RVA: 0x0004FA1E File Offset: 0x0004DC1E
		// (set) Token: 0x06001F7F RID: 8063 RVA: 0x0004FA35 File Offset: 0x0004DC35
		[WebSysDescription("")]
		[DefaultValue("")]
		[WebCategory("Appearance")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		public virtual string EditImageUrl
		{
			get
			{
				return base.ViewState.GetString("EditImageUrl", string.Empty);
			}
			set
			{
				base.ViewState["EditImageUrl"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the caption for an Edit button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>The caption for an Edit button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is "Edit".</returns>
		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06001F80 RID: 8064 RVA: 0x0004FA4E File Offset: 0x0004DC4E
		// (set) Token: 0x06001F81 RID: 8065 RVA: 0x0004FA65 File Offset: 0x0004DC65
		[Localizable(true)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual string EditText
		{
			get
			{
				return base.ViewState.GetString("EditText", "Edit");
			}
			set
			{
				base.ViewState["EditText"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the URL to an image to display for the Insert button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>The URL to an image to display for the Insert button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06001F82 RID: 8066 RVA: 0x0004FA7E File Offset: 0x0004DC7E
		// (set) Token: 0x06001F83 RID: 8067 RVA: 0x0004FA95 File Offset: 0x0004DC95
		[WebCategory("Appearance")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		[UrlProperty]
		[WebSysDescription("")]
		public virtual string InsertImageUrl
		{
			get
			{
				return base.ViewState.GetString("InsertImageUrl", string.Empty);
			}
			set
			{
				base.ViewState["InsertImageUrl"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the caption for the Insert button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>The caption for the Insert button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is "Insert".</returns>
		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06001F84 RID: 8068 RVA: 0x0004FAAE File Offset: 0x0004DCAE
		// (set) Token: 0x06001F85 RID: 8069 RVA: 0x0004FAC5 File Offset: 0x0004DCC5
		[Localizable(true)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual string InsertText
		{
			get
			{
				return base.ViewState.GetString("InsertText", "Insert");
			}
			set
			{
				base.ViewState["InsertText"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the URL to an image to display for the New button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>The URL to an image to display for the New button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06001F86 RID: 8070 RVA: 0x0004FADE File Offset: 0x0004DCDE
		// (set) Token: 0x06001F87 RID: 8071 RVA: 0x0004FAF5 File Offset: 0x0004DCF5
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual string NewImageUrl
		{
			get
			{
				return base.ViewState.GetString("NewImageUrl", string.Empty);
			}
			set
			{
				base.ViewState["NewImageUrl"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the caption for the New button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>The caption for the New button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field. The default is "New".</returns>
		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06001F88 RID: 8072 RVA: 0x0004FB0E File Offset: 0x0004DD0E
		// (set) Token: 0x06001F89 RID: 8073 RVA: 0x0004FB25 File Offset: 0x0004DD25
		[Localizable(true)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual string NewText
		{
			get
			{
				return base.ViewState.GetString("NewText", "New");
			}
			set
			{
				base.ViewState["NewText"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the URL to an image to display for a Select button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>The URL to an image to display for a Select button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06001F8A RID: 8074 RVA: 0x0004FB3E File Offset: 0x0004DD3E
		// (set) Token: 0x06001F8B RID: 8075 RVA: 0x0004FB55 File Offset: 0x0004DD55
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public virtual string SelectImageUrl
		{
			get
			{
				return base.ViewState.GetString("SelectImageUrl", string.Empty);
			}
			set
			{
				base.ViewState["SelectImageUrl"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the caption for a Select button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>The caption for a Select button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is "Select".</returns>
		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06001F8C RID: 8076 RVA: 0x0004FB6E File Offset: 0x0004DD6E
		// (set) Token: 0x06001F8D RID: 8077 RVA: 0x0004FB85 File Offset: 0x0004DD85
		[Localizable(true)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual string SelectText
		{
			get
			{
				return base.ViewState.GetString("SelectText", "Select");
			}
			set
			{
				base.ViewState["SelectText"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets a value indicating whether a Cancel button is displayed in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>true to display a Cancel button in a <see cref="T:System.Web.UI.WebControls.CommandField" />; otherwise, false. The default is true.</returns>
		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06001F8E RID: 8078 RVA: 0x0004FB9E File Offset: 0x0004DD9E
		// (set) Token: 0x06001F8F RID: 8079 RVA: 0x0004FBB1 File Offset: 0x0004DDB1
		[DefaultValue(true)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual bool ShowCancelButton
		{
			get
			{
				return base.ViewState.GetBool("ShowCancelButton", true);
			}
			set
			{
				base.ViewState["ShowCancelButton"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets a value indicating whether a Delete button is displayed in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>true to display a Delete button in a <see cref="T:System.Web.UI.WebControls.CommandField" />; otherwise, false. The default is false.</returns>
		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06001F90 RID: 8080 RVA: 0x0004FBCF File Offset: 0x0004DDCF
		// (set) Token: 0x06001F91 RID: 8081 RVA: 0x0004FBE2 File Offset: 0x0004DDE2
		[DefaultValue(false)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual bool ShowDeleteButton
		{
			get
			{
				return base.ViewState.GetBool("ShowDeleteButton", false);
			}
			set
			{
				base.ViewState["ShowDeleteButton"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets a value indicating whether an Edit button is displayed in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>true to display an Edit button in a <see cref="T:System.Web.UI.WebControls.CommandField" />; otherwise, false. The default is false.</returns>
		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06001F92 RID: 8082 RVA: 0x0004FC00 File Offset: 0x0004DE00
		// (set) Token: 0x06001F93 RID: 8083 RVA: 0x0004FC13 File Offset: 0x0004DE13
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		[DefaultValue(false)]
		public virtual bool ShowEditButton
		{
			get
			{
				return base.ViewState.GetBool("ShowEditButton", false);
			}
			set
			{
				base.ViewState["ShowEditButton"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets a value indicating whether a Select button is displayed in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>true to display a Select button in a <see cref="T:System.Web.UI.WebControls.CommandField" />; otherwise, false. The default is false.</returns>
		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06001F94 RID: 8084 RVA: 0x0004FC31 File Offset: 0x0004DE31
		// (set) Token: 0x06001F95 RID: 8085 RVA: 0x0004FC44 File Offset: 0x0004DE44
		[DefaultValue(false)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual bool ShowSelectButton
		{
			get
			{
				return base.ViewState.GetBool("ShowSelectButton", false);
			}
			set
			{
				base.ViewState["ShowSelectButton"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets a value indicating whether a New button is displayed in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>true to display a New button in a <see cref="T:System.Web.UI.WebControls.CommandField" />; otherwise, false. The default is false.</returns>
		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06001F96 RID: 8086 RVA: 0x0004FC62 File Offset: 0x0004DE62
		// (set) Token: 0x06001F97 RID: 8087 RVA: 0x0004FC75 File Offset: 0x0004DE75
		[DefaultValue(false)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual bool ShowInsertButton
		{
			get
			{
				return base.ViewState.GetBool("ShowInsertButton", false);
			}
			set
			{
				base.ViewState["ShowInsertButton"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the URL to an image to display for an Update button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>The URL for an image to display for an Update button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06001F98 RID: 8088 RVA: 0x0004FC93 File Offset: 0x0004DE93
		// (set) Token: 0x06001F99 RID: 8089 RVA: 0x0004FCAA File Offset: 0x0004DEAA
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual string UpdateImageUrl
		{
			get
			{
				return base.ViewState.GetString("UpdateImageUrl", string.Empty);
			}
			set
			{
				base.ViewState["UpdateImageUrl"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the caption for an Update button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>The caption for an Update button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is "Update".</returns>
		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06001F9A RID: 8090 RVA: 0x0004FCC3 File Offset: 0x0004DEC3
		// (set) Token: 0x06001F9B RID: 8091 RVA: 0x0004FCDA File Offset: 0x0004DEDA
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		public virtual string UpdateText
		{
			get
			{
				return base.ViewState.GetString("UpdateText", "Update");
			}
			set
			{
				base.ViewState["UpdateText"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Initializes the specified <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> object to the specified row state.</summary>
		/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> to initialize.</param>
		/// <param name="cellType">One of the <see cref="T:System.Web.UI.WebControls.DataControlCellType" /> values.</param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values.</param>
		/// <param name="rowIndex">The zero-based index of the row that contains the cell.</param>
		// Token: 0x06001F9C RID: 8092 RVA: 0x0004FCF4 File Offset: 0x0004DEF4
		public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
		{
			string text = rowIndex.ToString();
			if (cellType == DataControlCellType.DataCell)
			{
				if ((rowState & DataControlRowState.Edit) != DataControlRowState.Normal)
				{
					if (this.ShowEditButton)
					{
						cell.Controls.Add(this.CreateButton(this.UpdateText, this.UpdateImageUrl, "Update", text));
						if (this.ShowCancelButton)
						{
							this.AddSeparator(cell);
							cell.Controls.Add(this.CreateButton(this.CancelText, this.CancelImageUrl, "Cancel", text));
							return;
						}
					}
				}
				else if ((rowState & DataControlRowState.Insert) != DataControlRowState.Normal)
				{
					if (this.ShowInsertButton)
					{
						cell.Controls.Add(this.CreateButton(this.InsertText, this.InsertImageUrl, "Insert", text));
						if (this.ShowCancelButton)
						{
							this.AddSeparator(cell);
							cell.Controls.Add(this.CreateButton(this.CancelText, this.CancelImageUrl, "Cancel", text));
							return;
						}
					}
				}
				else
				{
					if (this.ShowEditButton)
					{
						this.AddSeparator(cell);
						cell.Controls.Add(this.CreateButton(this.EditText, this.EditImageUrl, "Edit", text));
					}
					if (this.ShowDeleteButton)
					{
						this.AddSeparator(cell);
						cell.Controls.Add(this.CreateButton(this.DeleteText, this.DeleteImageUrl, "Delete", text));
					}
					if (this.ShowInsertButton)
					{
						this.AddSeparator(cell);
						cell.Controls.Add(this.CreateButton(this.NewText, this.NewImageUrl, "New", text));
					}
					if (this.ShowSelectButton)
					{
						this.AddSeparator(cell);
						cell.Controls.Add(this.CreateButton(this.SelectText, this.SelectImageUrl, "Select", text));
						return;
					}
				}
			}
			else
			{
				base.InitializeCell(cell, cellType, rowState, rowIndex);
			}
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x0004FEB8 File Offset: 0x0004E0B8
		private Control CreateButton(string text, string image, string command, string arg)
		{
			IDataControlButton dataControlButton = DataControlButton.CreateButton(this.ButtonType, base.Control, text, image, command, arg, false);
			if (this.CausesValidation && (command == "Update" || command == "Insert"))
			{
				dataControlButton.Container = null;
				dataControlButton.CausesValidation = true;
				dataControlButton.ValidationGroup = this.ValidationGroup;
			}
			return (Control)dataControlButton;
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x0004FF20 File Offset: 0x0004E120
		private void AddSeparator(DataControlFieldCell cell)
		{
			if (cell.Controls.Count > 0)
			{
				Literal literal = new Literal();
				literal.Text = "&nbsp;";
				cell.Controls.Add(literal);
			}
		}

		/// <summary>Creates an empty <see cref="T:System.Web.UI.WebControls.CommandField" /> object.</summary>
		/// <returns>An empty <see cref="T:System.Web.UI.WebControls.CommandField" />.</returns>
		// Token: 0x06001F9F RID: 8095 RVA: 0x0004FF58 File Offset: 0x0004E158
		protected override DataControlField CreateField()
		{
			return new CommandField();
		}

		/// <summary>Copies the properties of the current <see cref="T:System.Web.UI.WebControls.CommandField" /> object to the specified <see cref="T:System.Web.UI.WebControls.DataControlField" /> object.</summary>
		/// <param name="newField">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> to copy the properties of the current <see cref="T:System.Web.UI.WebControls.CommandField" /> to.</param>
		// Token: 0x06001FA0 RID: 8096 RVA: 0x0004FF60 File Offset: 0x0004E160
		protected override void CopyProperties(DataControlField newField)
		{
			base.CopyProperties(newField);
			CommandField commandField = (CommandField)newField;
			commandField.CancelImageUrl = this.CancelImageUrl;
			commandField.CancelText = this.CancelText;
			commandField.DeleteImageUrl = this.DeleteImageUrl;
			commandField.DeleteText = this.DeleteText;
			commandField.EditImageUrl = this.EditImageUrl;
			commandField.EditText = this.EditText;
			commandField.InsertImageUrl = this.InsertImageUrl;
			commandField.InsertText = this.InsertText;
			commandField.NewImageUrl = this.NewImageUrl;
			commandField.NewText = this.NewText;
			commandField.SelectImageUrl = this.SelectImageUrl;
			commandField.SelectText = this.SelectText;
			commandField.ShowCancelButton = this.ShowCancelButton;
			commandField.ShowDeleteButton = this.ShowDeleteButton;
			commandField.ShowEditButton = this.ShowEditButton;
			commandField.ShowSelectButton = this.ShowSelectButton;
			commandField.ShowInsertButton = this.ShowInsertButton;
			commandField.UpdateImageUrl = this.UpdateImageUrl;
			commandField.UpdateText = this.UpdateText;
		}

		/// <summary>Determines whether the controls contained in a <see cref="T:System.Web.UI.WebControls.CommandField" /> object support callbacks.</summary>
		/// <exception cref="T:System.NotSupportedException">The Select button is displayed in the <see cref="T:System.Web.UI.WebControls.CommandField" /> object. The <see cref="T:System.Web.UI.WebControls.CommandField" />  class does support callbacks when the Select button is displayed.</exception>
		// Token: 0x06001FA1 RID: 8097 RVA: 0x00050060 File Offset: 0x0004E260
		public override void ValidateSupportsCallback()
		{
			if (this.ShowSelectButton)
			{
				throw new NotSupportedException(string.Concat(new string[]
				{
					"Callbacks are not supported on CommandField when the select button is enabled because other controls on your page that are dependent on the selected value of '",
					base.Control.ID,
					"' for their rendering will not update in a callback.  Turn callbacks off on '",
					base.Control.ID,
					"'."
				}));
			}
		}
	}
}
