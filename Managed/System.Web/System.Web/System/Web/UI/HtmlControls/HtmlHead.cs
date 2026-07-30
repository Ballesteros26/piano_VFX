using System;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Provides programmatic access to the HTML head element in server code.</summary>
	// Token: 0x0200025C RID: 604
	[ControlBuilder(typeof(HtmlHeadBuilder))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HtmlHead : HtmlGenericControl, IParserAccessor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlHead" /> class.</summary>
		// Token: 0x060018A7 RID: 6311 RVA: 0x000425FF File Offset: 0x000407FF
		public HtmlHead()
			: base("head")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlHead" /> class by using the specified tag.</summary>
		/// <param name="tag">A string that specifies the tag name of the control.</param>
		// Token: 0x060018A8 RID: 6312 RVA: 0x0004260C File Offset: 0x0004080C
		public HtmlHead(string tag)
			: base(tag)
		{
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x00042615 File Offset: 0x00040815
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			Page page = this.Page;
			if (page == null)
			{
				throw new HttpException("The <head runat=\"server\"> control requires a page.");
			}
			if (page.Header != null)
			{
				throw new HttpException("You can only have one <head runat=\"server\"> control on a page.");
			}
			page.SetHeader(this);
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x0004264C File Offset: 0x0004084C
		protected internal override void RenderChildren(HtmlTextWriter writer)
		{
			base.RenderChildren(writer);
			if (this.title == null)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Title);
				if (!string.IsNullOrEmpty(this.titleText))
				{
					writer.Write(this.titleText);
				}
				writer.RenderEndTag();
			}
			if (this.descriptionMeta == null && this.descriptionText != null)
			{
				writer.AddAttribute("name", "description");
				writer.AddAttribute("content", HttpUtility.HtmlAttributeEncode(this.descriptionText));
				writer.RenderBeginTag(HtmlTextWriterTag.Meta);
				writer.RenderEndTag();
			}
			if (this.keywordsMeta == null && this.keywordsText != null)
			{
				writer.AddAttribute("name", "keywords");
				writer.AddAttribute("content", HttpUtility.HtmlAttributeEncode(this.keywordsText));
				writer.RenderBeginTag(HtmlTextWriterTag.Meta);
				writer.RenderEndTag();
			}
			if (this.styleSheet != null)
			{
				this.styleSheet.Render(writer);
			}
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x0004272C File Offset: 0x0004092C
		protected internal override void AddedControl(Control control, int index)
		{
			HtmlTitle htmlTitle = control as HtmlTitle;
			if (htmlTitle != null)
			{
				if (this.title != null)
				{
					throw new HttpException("You can only have one <title> element within the <head> element.");
				}
				this.title = htmlTitle;
			}
			HtmlMeta htmlMeta = control as HtmlMeta;
			if (htmlMeta != null)
			{
				if (string.Compare("keywords", htmlMeta.Name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.keywordsMeta = htmlMeta;
				}
				else if (string.Compare("description", htmlMeta.Name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.descriptionMeta = htmlMeta;
				}
			}
			base.AddedControl(control, index);
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x000427A5 File Offset: 0x000409A5
		protected internal override void RemovedControl(Control control)
		{
			if (this.title == control)
			{
				this.title = null;
			}
			if (this.keywordsMeta == control)
			{
				this.keywordsMeta = null;
			}
			else if (this.descriptionMeta == control)
			{
				this.descriptionMeta = null;
			}
			base.RemovedControl(control);
		}

		/// <summary>Gets the content of the "description" meta element.</summary>
		/// <returns>The content of the "description" meta element.</returns>
		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x060018AD RID: 6317 RVA: 0x000427E0 File Offset: 0x000409E0
		// (set) Token: 0x060018AE RID: 6318 RVA: 0x000427FC File Offset: 0x000409FC
		public string Description
		{
			get
			{
				if (this.descriptionMeta != null)
				{
					return this.descriptionMeta.Content;
				}
				return this.descriptionText;
			}
			set
			{
				if (this.descriptionMeta != null)
				{
					this.descriptionMeta.Content = value;
					return;
				}
				this.descriptionText = value;
			}
		}

		/// <summary>Gets the content of the "keywords" meta element.</summary>
		/// <returns>The content of the "keywords" meta element.</returns>
		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x060018AF RID: 6319 RVA: 0x0004281A File Offset: 0x00040A1A
		// (set) Token: 0x060018B0 RID: 6320 RVA: 0x00042836 File Offset: 0x00040A36
		public string Keywords
		{
			get
			{
				if (this.keywordsMeta != null)
				{
					return this.keywordsMeta.Content;
				}
				return this.keywordsText;
			}
			set
			{
				if (this.keywordsMeta != null)
				{
					this.keywordsMeta.Content = value;
					return;
				}
				this.keywordsText = value;
			}
		}

		/// <summary>Gets an <see cref="T:System.Web.UI.IStyleSheet" /> instance that represents the style rules in the <see cref="T:System.Web.UI.HtmlControls.HtmlHead" /> control.</summary>
		/// <returns>An object that represents the style rules in the <see cref="T:System.Web.UI.HtmlControls.HtmlHead" /> control.</returns>
		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x060018B1 RID: 6321 RVA: 0x00042854 File Offset: 0x00040A54
		public IStyleSheet StyleSheet
		{
			get
			{
				if (this.styleSheet == null)
				{
					this.styleSheet = new StyleSheetBag();
				}
				return this.styleSheet;
			}
		}

		/// <summary>Gets the page title.</summary>
		/// <returns>The page title.</returns>
		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x060018B2 RID: 6322 RVA: 0x0004286F File Offset: 0x00040A6F
		// (set) Token: 0x060018B3 RID: 6323 RVA: 0x0004288B File Offset: 0x00040A8B
		public string Title
		{
			get
			{
				if (this.title != null)
				{
					return this.title.Text;
				}
				return this.titleText;
			}
			set
			{
				if (this.title != null)
				{
					this.title.Text = value;
					return;
				}
				this.titleText = value;
			}
		}

		// Token: 0x0400162B RID: 5675
		private string descriptionText;

		// Token: 0x0400162C RID: 5676
		private string keywordsText;

		// Token: 0x0400162D RID: 5677
		private HtmlMeta descriptionMeta;

		// Token: 0x0400162E RID: 5678
		private HtmlMeta keywordsMeta;

		// Token: 0x0400162F RID: 5679
		private string titleText;

		// Token: 0x04001630 RID: 5680
		private HtmlTitle title;

		// Token: 0x04001631 RID: 5681
		private StyleSheetBag styleSheet;
	}
}
