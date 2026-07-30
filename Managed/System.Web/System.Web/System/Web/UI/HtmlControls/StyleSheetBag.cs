using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x0200025D RID: 605
	internal class StyleSheetBag : IStyleSheet
	{
		// Token: 0x060018B5 RID: 6325 RVA: 0x000428BC File Offset: 0x00040ABC
		public void CreateStyleRule(Style style, IUrlResolutionService urlResolver, string selection)
		{
			StyleSheetBag.StyleEntry styleEntry = new StyleSheetBag.StyleEntry();
			styleEntry.Style = style;
			styleEntry.UrlResolver = urlResolver;
			styleEntry.Selection = selection;
			this.entries.Add(styleEntry);
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x000428F4 File Offset: 0x00040AF4
		public void RegisterStyle(Style style, IUrlResolutionService urlResolver)
		{
			for (int i = 0; i < this.entries.Count; i++)
			{
				if (((StyleSheetBag.StyleEntry)this.entries[i]).Style == style)
				{
					return;
				}
			}
			string text = "aspnet_" + this.entries.Count;
			style.SetRegisteredCssClass(text);
			this.CreateStyleRule(style, urlResolver, "." + text);
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x00042968 File Offset: 0x00040B68
		public void Render(HtmlTextWriter writer)
		{
			writer.AddAttribute("type", "text/css", false);
			writer.RenderBeginTag(HtmlTextWriterTag.Style);
			foreach (object obj in this.entries)
			{
				StyleSheetBag.StyleEntry styleEntry = (StyleSheetBag.StyleEntry)obj;
				CssStyleCollection styleAttributes = styleEntry.Style.GetStyleAttributes(styleEntry.UrlResolver);
				writer.Write(string.Concat(new string[] { "\n", styleEntry.Selection, " {", styleAttributes.Value, "}" }));
			}
			writer.RenderEndTag();
		}

		// Token: 0x04001632 RID: 5682
		private ArrayList entries = new ArrayList();

		// Token: 0x0200025E RID: 606
		internal class StyleEntry
		{
			// Token: 0x04001633 RID: 5683
			public Style Style;

			// Token: 0x04001634 RID: 5684
			public string Selection;

			// Token: 0x04001635 RID: 5685
			public IUrlResolutionService UrlResolver;
		}
	}
}
