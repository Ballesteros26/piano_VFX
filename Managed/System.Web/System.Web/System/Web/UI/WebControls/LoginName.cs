using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Displays the value of the System.Web.UI.Page.User.Identity.Name property. </summary>
	// Token: 0x020003CA RID: 970
	[Designer("System.Web.UI.Design.WebControls.LoginNameDesigner,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultProperty("FormatString")]
	[Bindable(false)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class LoginName : WebControl
	{
		/// <summary>Provides a format item string to display.</summary>
		/// <returns>A string containing format items for displaying the user's name. The default value is "{0}".</returns>
		/// <exception cref="T:System.FormatException">The format string is not valid. </exception>
		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x060028A9 RID: 10409 RVA: 0x0006A2AC File Offset: 0x000684AC
		// (set) Token: 0x060028AA RID: 10410 RVA: 0x0006A2D9 File Offset: 0x000684D9
		[DefaultValue("{0}")]
		[Localizable(true)]
		public virtual string FormatString
		{
			get
			{
				object obj = this.ViewState["FormatString"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "{0}";
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("FormatString");
					return;
				}
				this.ViewState["FormatString"] = value;
			}
		}

		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, false.</returns>
		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x060028AB RID: 10411 RVA: 0x0004789D File Offset: 0x00045A9D
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return base.RenderingCompatibilityLessThan40;
			}
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.WebControls.LoginName" /> control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> control. </summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered output.</param>
		// Token: 0x060028AC RID: 10412 RVA: 0x0006A300 File Offset: 0x00068500
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (!this.Anonymous)
			{
				this.RenderBeginTag(writer);
				this.RenderContents(writer);
				this.RenderEndTag(writer);
			}
		}

		/// <summary>Renders the HTML opening tag of the control to the specified writer.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> object that represents the output stream that renders HTML content to the client.</param>
		// Token: 0x060028AD RID: 10413 RVA: 0x0006A31F File Offset: 0x0006851F
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			if (!this.Anonymous)
			{
				base.RenderBeginTag(writer);
			}
		}

		/// <summary>Renders the contents of the control to the specified writer. This method is used primarily by control developers.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> object that represents the output stream that renders HTML content to the client.</param>
		/// <exception cref="T:System.FormatException">The <see cref="P:System.Web.UI.WebControls.LoginName.FormatString" /> property is not set to a valid format string.</exception>
		// Token: 0x060028AE RID: 10414 RVA: 0x0006A330 File Offset: 0x00068530
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			if (!this.Anonymous)
			{
				string text = (string)this.ViewState["FormatString"];
				if (text == null || text.Length == 0)
				{
					writer.Write(this.User);
					return;
				}
				writer.Write(text, this.User);
			}
		}

		/// <summary>Renders the HTML closing tag of the control to the specified writer.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> object that represents the output stream that renders HTML content to the client.</param>
		// Token: 0x060028AF RID: 10415 RVA: 0x0006A380 File Offset: 0x00068580
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			if (!this.Anonymous)
			{
				base.RenderEndTag(writer);
			}
		}

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x060028B0 RID: 10416 RVA: 0x0006A391 File Offset: 0x00068591
		private bool Anonymous
		{
			get
			{
				return this.User.Length == 0;
			}
		}

		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x060028B1 RID: 10417 RVA: 0x0006A3A1 File Offset: 0x000685A1
		private string User
		{
			get
			{
				if (this.Page == null || this.Page.User == null)
				{
					return string.Empty;
				}
				return this.Page.User.Identity.Name;
			}
		}
	}
}
