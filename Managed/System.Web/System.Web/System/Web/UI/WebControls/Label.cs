using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a label control, which displays text on a Web page.</summary>
	// Token: 0x020003BB RID: 955
	[ControlValueProperty("Text", null)]
	[ToolboxData("<{0}:Label runat=\"server\" Text=\"Label\"></{0}:Label>")]
	[ParseChildren(false)]
	[Designer("System.Web.UI.Design.WebControls.LabelDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultProperty("Text")]
	[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ControlBuilder(typeof(LabelControlBuilder))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Label : WebControl, ITextControl
	{
		/// <summary>Gets or sets the text content of the <see cref="T:System.Web.UI.WebControls.Label" /> control.</summary>
		/// <returns>The text content of the control. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x06002763 RID: 10083 RVA: 0x0004A013 File Offset: 0x00048213
		// (set) Token: 0x06002764 RID: 10084 RVA: 0x0006514D File Offset: 0x0006334D
		[WebSysDescription("")]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[DefaultValue("")]
		[Bindable(true)]
		[WebCategory("Appearance")]
		[Localizable(true)]
		public virtual string Text
		{
			get
			{
				return this.ViewState.GetString("Text", string.Empty);
			}
			set
			{
				this.ViewState["Text"] = value;
				if (this.HasControls())
				{
					this.Controls.Clear();
				}
			}
		}

		/// <summary>Gets or sets the identifier for a server control that the <see cref="T:System.Web.UI.WebControls.Label" /> control is associated with.</summary>
		/// <returns>A string value corresponding to the <see cref="P:System.Web.UI.Control.ID" /> for a server control contained in the Web form. The default is an empty string (""), indicating that the <see cref="T:System.Web.UI.WebControls.Label" /> control is not associated with another server control.</returns>
		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x06002765 RID: 10085 RVA: 0x00066A76 File Offset: 0x00064C76
		// (set) Token: 0x06002766 RID: 10086 RVA: 0x00066A8D File Offset: 0x00064C8D
		[IDReferenceProperty(typeof(Control))]
		[TypeConverter(typeof(AssociatedControlConverter))]
		[Themeable(false)]
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Accessibility")]
		public virtual string AssociatedControlID
		{
			get
			{
				return this.ViewState.GetString("AssociatedControlID", string.Empty);
			}
			set
			{
				this.ViewState["AssociatedControlID"] = value;
			}
		}

		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, false.</returns>
		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x06002767 RID: 10087 RVA: 0x0004789D File Offset: 0x00045A9D
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return base.RenderingCompatibilityLessThan40;
			}
		}

		/// <summary>Loads the previously saved state for the control. </summary>
		/// <param name="savedState">An object that contains the saved view state values for the control. </param>
		// Token: 0x06002768 RID: 10088 RVA: 0x00066AA0 File Offset: 0x00064CA0
		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
			if (this.ViewState["Text"] != null)
			{
				this.Text = (string)this.ViewState["Text"];
			}
		}

		/// <summary>Notifies the control that an element was parsed and adds the element to the <see cref="T:System.Web.UI.WebControls.Label" /> control.</summary>
		/// <param name="obj">An object that represents the parsed element.</param>
		// Token: 0x06002769 RID: 10089 RVA: 0x00066AD8 File Offset: 0x00064CD8
		protected override void AddParsedSubObject(object obj)
		{
			if (this.HasControls())
			{
				base.AddParsedSubObject(obj);
				return;
			}
			LiteralControl literalControl = obj as LiteralControl;
			if (literalControl == null)
			{
				string text = this.Text;
				if (text.Length != 0)
				{
					this.Text = null;
					this.Controls.Add(new LiteralControl(text));
				}
				base.AddParsedSubObject(obj);
				return;
			}
			this.Text = literalControl.Text;
		}

		/// <summary>Renders the contents of the <see cref="T:System.Web.UI.WebControls.Label" /> into the specified writer.</summary>
		/// <param name="writer">The output stream that renders HTML content to the client. </param>
		// Token: 0x0600276A RID: 10090 RVA: 0x00066B3A File Offset: 0x00064D3A
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			if (this.HasControls() || base.HasRenderMethodDelegate())
			{
				base.RenderContents(writer);
				return;
			}
			writer.Write(this.Text);
		}

		/// <summary>Gets the HTML tag that is used to render the <see cref="T:System.Web.UI.WebControls.Label" /> control.</summary>
		/// <returns>The <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value used to render the <see cref="T:System.Web.UI.WebControls.Label" />.</returns>
		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x0600276B RID: 10091 RVA: 0x00066B60 File Offset: 0x00064D60
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				if (!string.IsNullOrEmpty(this.AssociatedControlID))
				{
					return HtmlTextWriterTag.Label;
				}
				return HtmlTextWriterTag.Span;
			}
		}

		/// <summary>Adds the HTML attributes and styles of a <see cref="T:System.Web.UI.WebControls.Label" /> control to render to the specified output stream. </summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		/// <exception cref="T:System.Web.HttpException">The control specified in the <see cref="P:System.Web.UI.WebControls.Label.AssociatedControlID" /> property cannot be found.</exception>
		// Token: 0x0600276C RID: 10092 RVA: 0x00066B74 File Offset: 0x00064D74
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (!string.IsNullOrEmpty(this.AssociatedControlID))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.For, this.NamingContainer.FindControl(this.AssociatedControlID).ClientID);
			}
		}
	}
}
