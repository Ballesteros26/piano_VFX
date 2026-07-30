using System;
using System.ComponentModel;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access to the HTML link element on the server.</summary>
	// Token: 0x0200026C RID: 620
	[ControlBuilder(typeof(HtmlEmptyTagControlBuilder))]
	public class HtmlLink : HtmlControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlLink" /> class.</summary>
		// Token: 0x06001967 RID: 6503 RVA: 0x00044005 File Offset: 0x00042205
		public HtmlLink()
			: base("link")
		{
		}

		/// <summary>Gets or sets the URL target of the link specified in the <see cref="T:System.Web.UI.HtmlControls.HtmlLink" /> control. </summary>
		/// <returns>The URL target of the link.</returns>
		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06001968 RID: 6504 RVA: 0x00044014 File Offset: 0x00042214
		// (set) Token: 0x06001969 RID: 6505 RVA: 0x0004403C File Offset: 0x0004223C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		[UrlProperty]
		public virtual string Href
		{
			get
			{
				string text = base.Attributes["href"];
				if (text == null)
				{
					return "";
				}
				return text;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("href");
					return;
				}
				base.Attributes["href"] = value;
			}
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlLink" /> control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object. </summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
		// Token: 0x0600196A RID: 6506 RVA: 0x00044063 File Offset: 0x00042263
		protected internal override void Render(HtmlTextWriter writer)
		{
			writer.WriteBeginTag(this.TagName);
			this.RenderAttributes(writer);
			writer.Write(" />");
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlLink" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
		// Token: 0x0600196B RID: 6507 RVA: 0x00044083 File Offset: 0x00042283
		[global::System.MonoTODO("why override?")]
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			if (this.Href.Length > 0)
			{
				this.Href = base.ResolveClientUrl(this.Href);
			}
			base.RenderAttributes(writer);
		}
	}
}
