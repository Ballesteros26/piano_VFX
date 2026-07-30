using System;
using System.Collections;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.Adapters;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003D9 RID: 985
	internal sealed class MenuListRenderer : BaseMenuRenderer
	{
		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x06002A65 RID: 10853 RVA: 0x0006F12F File Offset: 0x0006D32F
		public override HtmlTextWriterTag Tag
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x0006F133 File Offset: 0x0006D333
		public MenuListRenderer(Menu owner)
			: base(owner)
		{
		}

		// Token: 0x06002A67 RID: 10855 RVA: 0x0006F13C File Offset: 0x0006D33C
		public override void PreRender(Page page, HtmlHead head, ClientScriptManager csm, string cmenu, StringBuilder script)
		{
			Menu owner = base.Owner;
			script.AppendFormat("new Sys.WebForms.Menu ({{ element: '{0}', disappearAfter: {1}, orientation: '{2}', tabIndex: {3}, disabled: {4} }});", new object[]
			{
				owner.ClientID,
				ClientScriptManager.GetScriptLiteral(owner.DisappearAfter),
				owner.Orientation.ToString().ToLowerInvariant(),
				ClientScriptManager.GetScriptLiteral(owner.TabIndex),
				(!owner.Enabled).ToString().ToLowerInvariant()
			});
			Type typeFromHandle = typeof(Menu);
			if (!csm.IsClientScriptIncludeRegistered(typeFromHandle, "MenuModern.js"))
			{
				string webResourceUrl = csm.GetWebResourceUrl(typeFromHandle, "MenuModern.js");
				csm.RegisterClientScriptInclude(typeFromHandle, "MenuModern.js", webResourceUrl);
			}
			if (!owner.IncludeStyleBlock)
			{
				return;
			}
			if (head == null)
			{
				throw new InvalidOperationException("Using Menu.IncludeStyleBlock requires Page.Header to be non-null (e.g. <head runat=\"server\" />).");
			}
			StyleBlock styleBlock = new StyleBlock(owner.ClientID);
			Style style = owner.ControlStyle;
			bool flag = owner.Orientation == Orientation.Horizontal;
			if (style != null)
			{
				styleBlock.RegisterStyle(style, null);
			}
			styleBlock.RegisterStyle(HtmlTextWriterStyle.BorderStyle, "none", "img.icon").Add(HtmlTextWriterStyle.VerticalAlign, "middle");
			styleBlock.RegisterStyle(HtmlTextWriterStyle.BorderStyle, "none", "img.separator").Add(HtmlTextWriterStyle.Display, "block");
			if (flag)
			{
				styleBlock.RegisterStyle(HtmlTextWriterStyle.BorderStyle, "none", "img.horizontal-separator").Add(HtmlTextWriterStyle.VerticalAlign, "middle");
			}
			styleBlock.RegisterStyle(HtmlTextWriterStyle.ListStyleType, "none", "ul").Add(HtmlTextWriterStyle.Margin, "0").Add(HtmlTextWriterStyle.Padding, "0")
				.Add(HtmlTextWriterStyle.Width, "auto");
			SubMenuStyle subMenuStyle = owner.StaticMenuStyleInternal;
			if (subMenuStyle != null)
			{
				styleBlock.RegisterStyle(subMenuStyle, "ul.static");
			}
			NamedCssStyleCollection namedCssStyleCollection = styleBlock.RegisterStyle("ul.dynamic");
			subMenuStyle = owner.DynamicMenuStyleInternal;
			if (subMenuStyle != null)
			{
				subMenuStyle.ForeColor = Color.Empty;
				namedCssStyleCollection.Add(subMenuStyle);
			}
			namedCssStyleCollection.Add(HtmlTextWriterStyle.ZIndex, "1");
			int num = owner.DynamicHorizontalOffset;
			if (num != 0)
			{
				namedCssStyleCollection.Add(HtmlTextWriterStyle.MarginLeft, num + "px");
			}
			num = owner.DynamicVerticalOffset;
			if (num != 0)
			{
				namedCssStyleCollection.Add(HtmlTextWriterStyle.MarginTop, num + "px");
			}
			this.RenderLevelStyles(styleBlock, num, owner.LevelSubMenuStyles, "ul.level", null, 0.0);
			styleBlock.RegisterStyle(HtmlTextWriterStyle.TextDecoration, "none", "a").Add(HtmlTextWriterStyle.WhiteSpace, "nowrap").Add(HtmlTextWriterStyle.Display, "block");
			this.RenderAnchorStyle(styleBlock, owner.StaticMenuItemStyleInternal, "a.static");
			bool flag2 = false;
			string text = owner.StaticPopOutImageUrl;
			namedCssStyleCollection = null;
			string text2 = "url(\"{0}\")";
			if (string.IsNullOrEmpty(text))
			{
				if (owner.StaticEnableDefaultPopOutImage)
				{
					namedCssStyleCollection = styleBlock.RegisterStyle(HtmlTextWriterStyle.BackgroundImage, string.Format(text2, base.GetArrowResourceUrl(owner)), "a.popout");
				}
				else
				{
					flag2 = true;
				}
			}
			else
			{
				namedCssStyleCollection = styleBlock.RegisterStyle(HtmlTextWriterStyle.BackgroundImage, string.Format(text2, text), "a.popout");
				flag2 = true;
			}
			if (namedCssStyleCollection != null)
			{
				namedCssStyleCollection.Add("background-repeat", "no-repeat").Add("background-position", "right center").Add(HtmlTextWriterStyle.PaddingRight, "14px");
			}
			text = owner.DynamicPopOutImageUrl;
			bool flag3 = !string.IsNullOrEmpty(text);
			namedCssStyleCollection = null;
			if (flag2 || flag3)
			{
				text2 = "url(\"{0}\") no-repeat right center";
				if (!flag3)
				{
					if (owner.DynamicEnableDefaultPopOutImage)
					{
						namedCssStyleCollection = styleBlock.RegisterStyle(HtmlTextWriterStyle.BackgroundImage, string.Format(text2, base.GetArrowResourceUrl(owner)), "a.popout-dynamic");
					}
				}
				else
				{
					namedCssStyleCollection = styleBlock.RegisterStyle(HtmlTextWriterStyle.BackgroundImage, string.Format(text2, text), "a.popout-dynamic");
				}
			}
			if (namedCssStyleCollection != null)
			{
				this.haveDynamicPopOut = true;
				namedCssStyleCollection.Add(HtmlTextWriterStyle.PaddingRight, "14px");
			}
			this.RenderAnchorStyle(styleBlock, owner.DynamicMenuItemStyleInternal, "a.dynamic");
			num = owner.StaticDisplayLevels;
			Unit staticSubMenuIndent = owner.StaticSubMenuIndent;
			string text3;
			double num2;
			if (staticSubMenuIndent == Unit.Empty)
			{
				text3 = "em";
				num2 = 1.0;
			}
			else
			{
				text3 = Unit.GetExtension(staticSubMenuIndent.Type);
				num2 = staticSubMenuIndent.Value;
			}
			this.RenderLevelStyles(styleBlock, num, owner.LevelMenuItemStyles, "a.level", text3, num2);
			this.RenderLevelStyles(styleBlock, num, owner.LevelSelectedStyles, "a.selected.level", null, 0.0);
			this.RenderAnchorStyle(styleBlock, owner.StaticSelectedStyleInternal, "a.static.selected");
			this.RenderAnchorStyle(styleBlock, owner.DynamicSelectedStyleInternal, "a.dynamic.selected");
			style = owner.StaticHoverStyleInternal;
			if (style != null)
			{
				styleBlock.RegisterStyle(style, "a.static.highlighted");
			}
			style = owner.DynamicHoverStyleInternal;
			if (style != null)
			{
				styleBlock.RegisterStyle(style, "a.dynamic.highlighted");
			}
			head.Controls.Add(styleBlock);
		}

		// Token: 0x06002A68 RID: 10856 RVA: 0x0006F5D8 File Offset: 0x0006D7D8
		public override void RenderBeginTag(HtmlTextWriter writer, string skipLinkText)
		{
			Menu owner = base.Owner;
			writer.AddAttribute(HtmlTextWriterAttribute.Href, "#" + owner.ClientID + "_SkipLink");
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			writer.AddAttribute(HtmlTextWriterAttribute.Alt, skipLinkText);
			Page page = owner.Page;
			ClientScriptManager clientScriptManager = ((page != null) ? page.ClientScript : new ClientScriptManager(null));
			writer.AddAttribute(HtmlTextWriterAttribute.Src, clientScriptManager.GetWebResourceUrl(typeof(SiteMapPath), "transparent.gif"));
			writer.AddAttribute(HtmlTextWriterAttribute.Width, "0");
			writer.AddAttribute(HtmlTextWriterAttribute.Height, "0");
			writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "0px");
			writer.RenderBeginTag(HtmlTextWriterTag.Img);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06002A69 RID: 10857 RVA: 0x0000393A File Offset: 0x00001B3A
		public override void RenderEndTag(HtmlTextWriter writer)
		{
		}

		// Token: 0x06002A6A RID: 10858 RVA: 0x0000393A File Offset: 0x00001B3A
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
		}

		// Token: 0x06002A6B RID: 10859 RVA: 0x0006F688 File Offset: 0x0006D888
		public override void RenderContents(HtmlTextWriter writer)
		{
			Menu owner = base.Owner;
			MenuItemCollection items = owner.Items;
			owner.RenderMenu(writer, items, owner.Orientation == Orientation.Vertical, false, 0, items.Count > 1);
		}

		// Token: 0x06002A6C RID: 10860 RVA: 0x0006F6C0 File Offset: 0x0006D8C0
		public override void RenderMenuBeginTag(HtmlTextWriter writer, bool dynamic, int menuLevel)
		{
			if (dynamic || menuLevel == 0)
			{
				SubMenuStyle subMenuStyle = new SubMenuStyle();
				base.AddCssClass(subMenuStyle, "level" + (menuLevel + 1));
				base.FillMenuStyle(null, dynamic, menuLevel, subMenuStyle);
				subMenuStyle.AddAttributesToRender(writer);
				writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			}
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x0006F70B File Offset: 0x0006D90B
		public override void RenderMenuEndTag(HtmlTextWriter writer, bool dynamic, int menuLevel)
		{
			if (dynamic || menuLevel == 0)
			{
				base.RenderMenuEndTag(writer, dynamic, menuLevel);
			}
		}

		// Token: 0x06002A6E RID: 10862 RVA: 0x0006F71C File Offset: 0x0006D91C
		public override void RenderMenuBody(HtmlTextWriter writer, MenuItemCollection items, bool vertical, bool dynamic, bool notLast)
		{
			Menu owner = base.Owner;
			int count = items.Count;
			BaseMenuRenderer.OwnerContext ownerContext = new BaseMenuRenderer.OwnerContext(this);
			for (int i = 0; i < count; i++)
			{
				MenuItem menuItem = items[i];
				MenuAdapter menuAdapter = owner.Adapter as MenuAdapter;
				if (menuAdapter != null)
				{
					menuAdapter.RenderItem(writer, menuItem, i);
				}
				else
				{
					this.RenderMenuItem(writer, menuItem, vertical, i + 1 != count || notLast, i == 0, ownerContext);
				}
			}
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x0006F78C File Offset: 0x0006D98C
		protected override void RenderMenuItem(HtmlTextWriter writer, MenuItem item, bool vertical, bool notLast, bool isFirst, BaseMenuRenderer.OwnerContext oc)
		{
			Menu owner = base.Owner;
			bool flag = owner.DisplayChildren(item);
			bool flag2 = this.IsDynamicItem(owner, item);
			int num = item.Depth + 1;
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			if (flag2)
			{
				base.RenderSeparatorImage(owner, writer, oc.DynamicTopSeparatorImageUrl, true);
			}
			else
			{
				base.RenderSeparatorImage(owner, writer, oc.StaticTopSeparatorImageUrl, true);
			}
			Style style = new Style();
			if (flag && (flag2 || num >= oc.StaticDisplayLevels))
			{
				base.AddCssClass(style, (flag2 && this.haveDynamicPopOut) ? "popout-dynamic" : "popout");
			}
			base.AddCssClass(style, "level" + num);
			MenuItemStyleCollection levelMenuItemStyles = oc.LevelMenuItemStyles;
			if (levelMenuItemStyles != null && levelMenuItemStyles.Count >= num)
			{
				string cssClass = levelMenuItemStyles[num - 1].CssClass;
				if (!string.IsNullOrEmpty(cssClass))
				{
					base.AddCssClass(style, cssClass);
				}
			}
			if (owner.SelectedItem == item)
			{
				base.AddCssClass(style, "selected");
			}
			string text = item.ToolTip;
			if (!string.IsNullOrEmpty(text))
			{
				writer.AddAttribute("title", text);
			}
			style.AddAttributesToRender(writer);
			this.RenderItemHref(owner, writer, item);
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			owner.RenderItemContent(writer, item, flag2);
			writer.RenderEndTag();
			text = item.SeparatorImageUrl;
			if (string.IsNullOrEmpty(text))
			{
				if (flag2)
				{
					text = oc.DynamicBottomSeparatorImageUrl;
				}
				else
				{
					text = oc.StaticBottomSeparatorImageUrl;
				}
			}
			base.RenderSeparatorImage(owner, writer, text, true);
			if (flag)
			{
				owner.RenderMenu(writer, item.ChildItems, vertical, flag2, num, notLast);
			}
			if (num > 0)
			{
				writer.RenderEndTag();
			}
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x0006F913 File Offset: 0x0006DB13
		public override bool IsDynamicItem(Menu owner, MenuItem item)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			return item.Depth + 1 >= base.Owner.StaticDisplayLevels;
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x0006F94C File Offset: 0x0006DB4C
		private NamedCssStyleCollection RenderAnchorStyle(StyleBlock block, Style style, string styleName)
		{
			if (style == null || block == null)
			{
				return null;
			}
			style.AlwaysRenderTextDecoration = true;
			NamedCssStyleCollection namedCssStyleCollection = block.RegisterStyle(style, styleName);
			if (style.BorderStyle == BorderStyle.NotSet)
			{
				namedCssStyleCollection.Add(HtmlTextWriterStyle.BorderStyle, "none");
			}
			return namedCssStyleCollection;
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x0006F988 File Offset: 0x0006DB88
		private void RenderLevelStyles(StyleBlock block, int num, IList levelStyles, string name, string unitName = null, double indent = 0.0)
		{
			int num2 = ((levelStyles != null) ? levelStyles.Count : 0);
			bool flag = num2 > 0;
			if (!flag || block == null)
			{
				return;
			}
			bool flag2 = !string.IsNullOrEmpty(unitName) && indent != 0.0;
			for (int i = 0; i < num2; i++)
			{
				if (i != 0 || flag)
				{
					NamedCssStyleCollection namedCssStyleCollection = block.RegisterStyle(name + (i + 1));
					if (flag && num2 > i)
					{
						Style style = levelStyles[i] as Style;
						if (style != null)
						{
							style.AlwaysRenderTextDecoration = true;
							namedCssStyleCollection.CopyFrom(style.GetStyleAttributes(null));
						}
					}
					if (flag2 && i > 0 && i < num)
					{
						namedCssStyleCollection.Add(HtmlTextWriterStyle.PaddingLeft, indent.ToString(CultureInfo.InvariantCulture) + unitName);
						indent += indent;
					}
				}
			}
		}

		// Token: 0x04001ADD RID: 6877
		private bool haveDynamicPopOut;
	}
}
