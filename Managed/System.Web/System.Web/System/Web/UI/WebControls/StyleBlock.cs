using System;
using System.Collections.Generic;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000413 RID: 1043
	internal sealed class StyleBlock : Control
	{
		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x06002F03 RID: 12035 RVA: 0x0007C90A File Offset: 0x0007AB0A
		private List<NamedCssStyleCollection> CssStyles
		{
			get
			{
				if (this.cssStyles == null)
				{
					this.cssStyles = new List<NamedCssStyleCollection>();
					this.cssStyleIndex = new Dictionary<string, NamedCssStyleCollection>(StringComparer.Ordinal);
				}
				return this.cssStyles;
			}
		}

		// Token: 0x06002F04 RID: 12036 RVA: 0x0007C935 File Offset: 0x0007AB35
		public StyleBlock(string stylePrefix)
		{
			if (string.IsNullOrEmpty(stylePrefix))
			{
				throw new ArgumentNullException("stylePrefix");
			}
			this.stylePrefix = stylePrefix;
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x0007C957 File Offset: 0x0007AB57
		public NamedCssStyleCollection RegisterStyle(string name = null)
		{
			if (name == null)
			{
				name = string.Empty;
			}
			return this.GetStyle(name);
		}

		// Token: 0x06002F06 RID: 12038 RVA: 0x0007C96A File Offset: 0x0007AB6A
		public NamedCssStyleCollection RegisterStyle(Style style, string name = null)
		{
			if (style == null)
			{
				throw new ArgumentNullException("style");
			}
			if (name == null)
			{
				name = string.Empty;
			}
			NamedCssStyleCollection style2 = this.GetStyle(name);
			style2.CopyFrom(style.GetStyleAttributes(null));
			return style2;
		}

		// Token: 0x06002F07 RID: 12039 RVA: 0x0007C999 File Offset: 0x0007AB99
		public NamedCssStyleCollection RegisterStyle(HtmlTextWriterStyle key, string value, string styleName = null)
		{
			if (styleName == null)
			{
				styleName = string.Empty;
			}
			NamedCssStyleCollection style = this.GetStyle(styleName);
			style.Add(key, value);
			return style;
		}

		// Token: 0x06002F08 RID: 12040 RVA: 0x0007C9B8 File Offset: 0x0007ABB8
		private NamedCssStyleCollection GetStyle(string name)
		{
			List<NamedCssStyleCollection> list = this.CssStyles;
			NamedCssStyleCollection namedCssStyleCollection;
			if (!this.cssStyleIndex.TryGetValue(name, out namedCssStyleCollection))
			{
				namedCssStyleCollection = new NamedCssStyleCollection(name);
				this.cssStyleIndex.Add(name, namedCssStyleCollection);
				list.Add(namedCssStyleCollection);
			}
			if (namedCssStyleCollection == null)
			{
				throw new InvalidOperationException(string.Format("Internal error. Stylesheet for style {0} is null.", name));
			}
			return namedCssStyleCollection;
		}

		// Token: 0x06002F09 RID: 12041 RVA: 0x0007CA0C File Offset: 0x0007AC0C
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this.cssStyles == null || this.cssStyles.Count == 0)
			{
				return;
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/css");
			writer.RenderBeginTag(HtmlTextWriterTag.Style);
			writer.WriteLine("/* <![CDATA[ */");
			foreach (NamedCssStyleCollection namedCssStyleCollection in this.cssStyles)
			{
				string value = namedCssStyleCollection.Collection.Value;
				if (!string.IsNullOrEmpty(value))
				{
					string text = namedCssStyleCollection.Name;
					if (text != string.Empty)
					{
						text += " ";
					}
					writer.WriteLine("#{0} {1}{{ {2} }}", this.stylePrefix, text, value);
				}
			}
			writer.WriteLine("/* ]]> */");
			writer.RenderEndTag();
		}

		// Token: 0x04001BE2 RID: 7138
		private List<NamedCssStyleCollection> cssStyles;

		// Token: 0x04001BE3 RID: 7139
		private Dictionary<string, NamedCssStyleCollection> cssStyleIndex;

		// Token: 0x04001BE4 RID: 7140
		private string stylePrefix;
	}
}
