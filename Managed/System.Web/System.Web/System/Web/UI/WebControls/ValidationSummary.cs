using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Displays a summary of all validation errors inline on a Web page, in a message box, or both. </summary>
	// Token: 0x0200043A RID: 1082
	[Designer("System.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ValidationSummary : WebControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> class.</summary>
		// Token: 0x060031D5 RID: 12757 RVA: 0x000853D2 File Offset: 0x000835D2
		public ValidationSummary()
			: base(HtmlTextWriterTag.Div)
		{
			this.ForeColor = Color.Red;
		}

		/// <summary>Gets or sets the display mode of the validation summary.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ValidationSummaryDisplayMode" /> values. The default is BulletList.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The display mode is not one of the <see cref="T:System.Web.UI.WebControls.ValidationSummaryDisplayMode" /> values. </exception>
		// Token: 0x17000FBE RID: 4030
		// (get) Token: 0x060031D6 RID: 12758 RVA: 0x000853E8 File Offset: 0x000835E8
		// (set) Token: 0x060031D7 RID: 12759 RVA: 0x00085411 File Offset: 0x00083611
		[WebSysDescription("")]
		[DefaultValue(ValidationSummaryDisplayMode.BulletList)]
		[WebCategory("Appearance")]
		public ValidationSummaryDisplayMode DisplayMode
		{
			get
			{
				object obj = this.ViewState["DisplayMode"];
				if (obj != null)
				{
					return (ValidationSummaryDisplayMode)obj;
				}
				return ValidationSummaryDisplayMode.BulletList;
			}
			set
			{
				this.ViewState["DisplayMode"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> control updates itself using client-side script.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> control updates itself using client-side script; otherwise, false. The default is true.</returns>
		// Token: 0x17000FBF RID: 4031
		// (get) Token: 0x060031D8 RID: 12760 RVA: 0x00048A1F File Offset: 0x00046C1F
		// (set) Token: 0x060031D9 RID: 12761 RVA: 0x00048A32 File Offset: 0x00046C32
		[DefaultValue(true)]
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		[Themeable(false)]
		public bool EnableClientScript
		{
			get
			{
				return this.ViewState.GetBool("EnableClientScript", true);
			}
			set
			{
				this.ViewState["EnableClientScript"] = value;
			}
		}

		/// <summary>Gets or sets the foreground color of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the control. The default is Red.</returns>
		// Token: 0x17000FC0 RID: 4032
		// (get) Token: 0x060031DA RID: 12762 RVA: 0x00085429 File Offset: 0x00083629
		// (set) Token: 0x060031DB RID: 12763 RVA: 0x00085431 File Offset: 0x00083631
		[DefaultValue(typeof(Color), "Red")]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		/// <summary>Gets or sets the header text displayed at the top of the summary.</summary>
		/// <returns>The header text displayed at the top of the summary. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000FC1 RID: 4033
		// (get) Token: 0x060031DC RID: 12764 RVA: 0x0008543A File Offset: 0x0008363A
		// (set) Token: 0x060031DD RID: 12765 RVA: 0x00085451 File Offset: 0x00083651
		[WebCategory("Appearance")]
		[Localizable(true)]
		[DefaultValue("")]
		[WebSysDescription("")]
		public string HeaderText
		{
			get
			{
				return this.ViewState.GetString("HeaderText", string.Empty);
			}
			set
			{
				this.ViewState["HeaderText"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the validation summary is displayed in a message box.</summary>
		/// <returns>true if the validation summary is to be displayed in a message box; otherwise, false. The default is false.</returns>
		// Token: 0x17000FC2 RID: 4034
		// (get) Token: 0x060031DE RID: 12766 RVA: 0x00085464 File Offset: 0x00083664
		// (set) Token: 0x060031DF RID: 12767 RVA: 0x00085477 File Offset: 0x00083677
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		[DefaultValue(false)]
		public bool ShowMessageBox
		{
			get
			{
				return this.ViewState.GetBool("ShowMessageBox", false);
			}
			set
			{
				this.ViewState["ShowMessageBox"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the validation summary is displayed inline.</summary>
		/// <returns>true if the validation summary is displayed inline; otherwise, false. The default is true.</returns>
		// Token: 0x17000FC3 RID: 4035
		// (get) Token: 0x060031E0 RID: 12768 RVA: 0x0008548F File Offset: 0x0008368F
		// (set) Token: 0x060031E1 RID: 12769 RVA: 0x000854A2 File Offset: 0x000836A2
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		[DefaultValue(true)]
		public bool ShowSummary
		{
			get
			{
				return this.ViewState.GetBool("ShowSummary", true);
			}
			set
			{
				this.ViewState["ShowSummary"] = value;
			}
		}

		/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> object displays validation messages.</summary>
		/// <returns>The group of controls for which the <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> object displays validation messages.</returns>
		// Token: 0x17000FC4 RID: 4036
		// (get) Token: 0x060031E2 RID: 12770 RVA: 0x000419CA File Offset: 0x0003FBCA
		// (set) Token: 0x060031E3 RID: 12771 RVA: 0x000419E1 File Offset: 0x0003FBE1
		[Themeable(false)]
		[DefaultValue("")]
		public virtual string ValidationGroup
		{
			get
			{
				return this.ViewState.GetString("ValidationGroup", string.Empty);
			}
			set
			{
				this.ViewState["ValidationGroup"] = value;
			}
		}

		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, false.</returns>
		// Token: 0x17000FC5 RID: 4037
		// (get) Token: 0x060031E4 RID: 12772 RVA: 0x0004789D File Offset: 0x00045A9D
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return base.RenderingCompatibilityLessThan40;
			}
		}

		/// <summary>Adds attributes to the HTML tags generated for this control.</summary>
		/// <param name="writer">The output stream that renders HTML content to the client. </param>
		// Token: 0x060031E5 RID: 12773 RVA: 0x000854BC File Offset: 0x000836BC
		[global::System.MonoTODO]
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (this.EnableClientScript && this.pre_render_called && this.Page.AreValidatorsUplevel(this.ValidationGroup))
			{
				if (this.ID == null)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
				}
				if (this.ValidationGroup != string.Empty)
				{
					this.RegisterExpandoAttribute(this.ClientID, "validationGroup", this.ValidationGroup);
				}
				if (this.HeaderText.Length > 0)
				{
					this.RegisterExpandoAttribute(this.ClientID, "headertext", this.HeaderText);
				}
				if (this.ShowMessageBox)
				{
					this.RegisterExpandoAttribute(this.ClientID, "showmessagebox", "True");
				}
				if (!this.ShowSummary)
				{
					this.RegisterExpandoAttribute(this.ClientID, "showsummary", "False");
				}
				if (this.DisplayMode != ValidationSummaryDisplayMode.BulletList)
				{
					this.RegisterExpandoAttribute(this.ClientID, "displaymode", this.DisplayMode.ToString());
				}
				if (!this.has_errors)
				{
					writer.AddStyleAttribute("display", "none");
				}
			}
		}

		// Token: 0x060031E6 RID: 12774 RVA: 0x000855E2 File Offset: 0x000837E2
		internal void RegisterExpandoAttribute(string controlId, string attributeName, string attributeValue)
		{
			this.RegisterExpandoAttribute(controlId, attributeName, attributeValue, false);
		}

		// Token: 0x060031E7 RID: 12775 RVA: 0x000855EE File Offset: 0x000837EE
		internal void RegisterExpandoAttribute(string controlId, string attributeName, string attributeValue, bool encode)
		{
			if (this.Page.ScriptManager != null)
			{
				this.Page.ScriptManager.RegisterExpandoAttributeExternal(this, controlId, attributeName, attributeValue, encode);
				return;
			}
			this.Page.ClientScript.RegisterExpandoAttribute(controlId, attributeName, attributeValue, encode);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event.</summary>
		/// <param name="e">The event data.</param>
		// Token: 0x060031E8 RID: 12776 RVA: 0x00085629 File Offset: 0x00083829
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (!base.RenderingCompatibilityLessThan40)
			{
				return;
			}
			if (this.ForeColor == Color.Empty)
			{
				this.ForeColor = Color.Red;
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060031E9 RID: 12777 RVA: 0x00085658 File Offset: 0x00083858
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.pre_render_called = true;
		}

		/// <summary>Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter" /> object, which writes the content to be rendered on the client.</summary>
		/// <param name="writer">The output stream that renders HTML content to the client.</param>
		// Token: 0x060031EA RID: 12778 RVA: 0x00085668 File Offset: 0x00083868
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (!base.IsEnabled)
			{
				return;
			}
			ValidatorCollection validators = this.Page.GetValidators(this.ValidationGroup);
			ArrayList arrayList = new ArrayList(validators.Count);
			for (int i = 0; i < validators.Count; i++)
			{
				if (!validators[i].IsValid)
				{
					arrayList.Add(validators[i].ErrorMessage);
				}
			}
			this.has_errors = arrayList.Count > 0;
			if (this.EnableClientScript && this.pre_render_called && this.Page.AreValidatorsUplevel(this.ValidationGroup))
			{
				if (this.Page.ScriptManager != null)
				{
					this.Page.ScriptManager.RegisterArrayDeclarationExternal(this, "Page_ValidationSummaries", "document.getElementById ('" + this.ClientID + "')");
					this.Page.ScriptManager.RegisterStartupScriptExternal(this, typeof(BaseValidator), this.ClientID + "DisposeScript", string.Concat(new string[] { "\ndocument.getElementById('", this.ClientID, "').dispose = function() {\n\tArray.remove(Page_ValidationSummaries, document.getElementById('", this.ClientID, "'));\n}\n" }), true);
				}
				else
				{
					this.Page.ClientScript.RegisterArrayDeclaration("Page_ValidationSummaries", "document.getElementById ('" + this.ClientID + "')");
				}
			}
			if ((this.ShowSummary && this.has_errors) || (this.EnableClientScript && this.pre_render_called))
			{
				base.RenderBeginTag(writer);
			}
			if (this.ShowSummary && this.has_errors)
			{
				switch (this.DisplayMode)
				{
				case ValidationSummaryDisplayMode.List:
				{
					if (this.HeaderText.Length > 0)
					{
						writer.Write(this.HeaderText);
						writer.Write("<br />");
					}
					for (int j = 0; j < arrayList.Count; j++)
					{
						writer.Write(arrayList[j]);
						writer.Write("<br />");
					}
					break;
				}
				case ValidationSummaryDisplayMode.BulletList:
				{
					if (this.HeaderText.Length > 0)
					{
						writer.Write(this.HeaderText);
					}
					writer.Write("<ul>");
					for (int k = 0; k < arrayList.Count; k++)
					{
						writer.Write("<li>");
						writer.Write(arrayList[k]);
						writer.Write("</li>");
					}
					writer.Write("</ul>");
					break;
				}
				case ValidationSummaryDisplayMode.SingleParagraph:
				{
					if (this.HeaderText.Length > 0)
					{
						writer.Write(this.HeaderText);
						writer.Write(" ");
					}
					for (int l = 0; l < arrayList.Count; l++)
					{
						writer.Write(arrayList[l]);
						writer.Write(" ");
					}
					writer.Write("<br />");
					break;
				}
				}
			}
			if ((this.ShowSummary && this.has_errors) || (this.EnableClientScript && this.pre_render_called))
			{
				base.RenderEndTag(writer);
			}
		}

		/// <summary>Gets or sets a value that specifies whether model-state errors should be displayed.</summary>
		/// <returns>true if model state errors should be displayed; otherwise, false. The default is true.</returns>
		// Token: 0x17000FC6 RID: 4038
		// (get) Token: 0x060031EB RID: 12779 RVA: 0x00085974 File Offset: 0x00083B74
		// (set) Token: 0x060031EC RID: 12780 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public bool ShowModelStateErrors
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that specifies whether the validation summary from validator controls should be displayed.</summary>
		/// <returns>true if the validation summary from validator controls should be displayed; otherwise, false. The default is true.</returns>
		// Token: 0x17000FC7 RID: 4039
		// (get) Token: 0x060031ED RID: 12781 RVA: 0x00085990 File Offset: 0x00083B90
		// (set) Token: 0x060031EE RID: 12782 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public bool ShowValidationErrors
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x04001C61 RID: 7265
		private bool pre_render_called;

		// Token: 0x04001C62 RID: 7266
		private bool has_errors;
	}
}
