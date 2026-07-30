using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.Adapters;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003DA RID: 986
	internal sealed class MenuTableRenderer : BaseMenuRenderer
	{
		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x06002A73 RID: 10867 RVA: 0x0004D090 File Offset: 0x0004B290
		public override HtmlTextWriterTag Tag
		{
			get
			{
				return HtmlTextWriterTag.Table;
			}
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x0006F133 File Offset: 0x0006D333
		public MenuTableRenderer(Menu owner)
			: base(owner)
		{
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x0006FA5F File Offset: 0x0006DC5F
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddAttribute("cellpadding", "0", false);
			writer.AddAttribute("cellspacing", "0", false);
			writer.AddAttribute("border", "0", false);
			base.AddAttributesToRender(writer);
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x0006FA9C File Offset: 0x0006DC9C
		public override void PreRender(Page page, HtmlHead head, ClientScriptManager csm, string cmenu, StringBuilder script)
		{
			Menu owner = base.Owner;
			MenuItemStyle staticMenuItemStyleInternal = owner.StaticMenuItemStyleInternal;
			SubMenuStyle staticMenuStyleInternal = owner.StaticMenuStyleInternal;
			MenuItemStyle dynamicMenuItemStyleInternal = owner.DynamicMenuItemStyleInternal;
			SubMenuStyle dynamicMenuStyleInternal = owner.DynamicMenuStyleInternal;
			MenuItemStyleCollection levelMenuItemStyles = owner.LevelMenuItemStyles;
			List<Style> list = owner.LevelMenuItemLinkStyles;
			SubMenuStyleCollection levelSubMenuStylesInternal = owner.LevelSubMenuStylesInternal;
			MenuItemStyle staticSelectedStyleInternal = owner.StaticSelectedStyleInternal;
			MenuItemStyle dynamicSelectedStyleInternal = owner.DynamicSelectedStyleInternal;
			MenuItemStyleCollection levelSelectedStylesInternal = owner.LevelSelectedStylesInternal;
			List<Style> list2 = owner.LevelSelectedLinkStyles;
			Style staticHoverStyleInternal = owner.StaticHoverStyleInternal;
			Style dynamicHoverStyleInternal = owner.DynamicHoverStyleInternal;
			if (!csm.IsClientScriptIncludeRegistered(typeof(Menu), "Menu.js"))
			{
				string webResourceUrl = csm.GetWebResourceUrl(typeof(Menu), "Menu.js");
				csm.RegisterClientScriptInclude(typeof(Menu), "Menu.js", webResourceUrl);
			}
			script.AppendFormat("var {0} = new Object ();\n{0}.webForm = {1};\n{0}.disappearAfter = {2};\n{0}.vertical = {3};", new object[]
			{
				cmenu,
				page.IsMultiForm ? page.theForm : "window",
				ClientScriptManager.GetScriptLiteral(owner.DisappearAfter),
				ClientScriptManager.GetScriptLiteral(owner.Orientation == Orientation.Vertical)
			});
			if (owner.DynamicHorizontalOffset != 0)
			{
				script.Append(cmenu + ".dho = " + ClientScriptManager.GetScriptLiteral(owner.DynamicHorizontalOffset) + ";\n");
			}
			if (owner.DynamicVerticalOffset != 0)
			{
				script.Append(cmenu + ".dvo = " + ClientScriptManager.GetScriptLiteral(owner.DynamicVerticalOffset) + ";\n");
			}
			base.RegisterStyle(owner.PopOutBoxStyle, head);
			base.RegisterStyle(owner.ControlStyle, owner.ControlLinkStyle, head);
			if (staticMenuItemStyleInternal != null)
			{
				base.RegisterStyle(owner.StaticMenuItemStyle, owner.StaticMenuItemLinkStyle, head);
			}
			if (staticMenuStyleInternal != null)
			{
				base.RegisterStyle(owner.StaticMenuStyle, head);
			}
			if (dynamicMenuItemStyleInternal != null)
			{
				base.RegisterStyle(owner.DynamicMenuItemStyle, owner.DynamicMenuItemLinkStyle, head);
			}
			if (dynamicMenuStyleInternal != null)
			{
				base.RegisterStyle(owner.DynamicMenuStyle, head);
			}
			if (levelMenuItemStyles != null && levelMenuItemStyles.Count > 0)
			{
				list = new List<Style>(levelMenuItemStyles.Count);
				foreach (object obj in levelMenuItemStyles)
				{
					Style style = (Style)obj;
					Style style2 = new Style();
					list.Add(style2);
					base.RegisterStyle(style, style2, head);
				}
			}
			if (levelSubMenuStylesInternal != null)
			{
				foreach (object obj2 in levelSubMenuStylesInternal)
				{
					Style style3 = (Style)obj2;
					base.RegisterStyle(style3, head);
				}
			}
			if (staticSelectedStyleInternal != null)
			{
				base.RegisterStyle(staticSelectedStyleInternal, owner.StaticSelectedLinkStyle, head);
			}
			if (dynamicSelectedStyleInternal != null)
			{
				base.RegisterStyle(dynamicSelectedStyleInternal, owner.DynamicSelectedLinkStyle, head);
			}
			if (levelSelectedStylesInternal != null && levelSelectedStylesInternal.Count > 0)
			{
				list2 = new List<Style>(levelSelectedStylesInternal.Count);
				foreach (object obj3 in levelSelectedStylesInternal)
				{
					Style style4 = (Style)obj3;
					Style style5 = new Style();
					list2.Add(style5);
					base.RegisterStyle(style4, style5, head);
				}
			}
			if (staticHoverStyleInternal != null)
			{
				if (head == null)
				{
					throw new InvalidOperationException("Using Menu.StaticHoverStyle requires Page.Header to be non-null (e.g. <head runat=\"server\" />).");
				}
				base.RegisterStyle(staticHoverStyleInternal, owner.StaticHoverLinkStyle, head);
				script.Append(cmenu + ".staticHover = " + ClientScriptManager.GetScriptLiteral(staticHoverStyleInternal.RegisteredCssClass) + ";\n");
				script.Append(cmenu + ".staticLinkHover = " + ClientScriptManager.GetScriptLiteral(owner.StaticHoverLinkStyle.RegisteredCssClass) + ";\n");
			}
			if (dynamicHoverStyleInternal != null)
			{
				if (head == null)
				{
					throw new InvalidOperationException("Using Menu.DynamicHoverStyle requires Page.Header to be non-null (e.g. <head runat=\"server\" />).");
				}
				base.RegisterStyle(dynamicHoverStyleInternal, owner.DynamicHoverLinkStyle, head);
				script.Append(cmenu + ".dynamicHover = " + ClientScriptManager.GetScriptLiteral(dynamicHoverStyleInternal.RegisteredCssClass) + ";\n");
				script.Append(cmenu + ".dynamicLinkHover = " + ClientScriptManager.GetScriptLiteral(owner.DynamicHoverLinkStyle.RegisteredCssClass) + ";\n");
			}
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x0006FED4 File Offset: 0x0006E0D4
		public override void RenderBeginTag(HtmlTextWriter writer, string skipLinkText)
		{
			Menu owner = base.Owner;
			writer.AddAttribute(HtmlTextWriterAttribute.Href, "#" + owner.ClientID + "_SkipLink");
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			writer.AddAttribute(HtmlTextWriterAttribute.Alt, skipLinkText);
			writer.AddAttribute(HtmlTextWriterAttribute.Height, "0");
			writer.AddAttribute(HtmlTextWriterAttribute.Width, "0");
			Page page = owner.Page;
			ClientScriptManager clientScriptManager = ((page != null) ? page.ClientScript : new ClientScriptManager(null));
			writer.AddAttribute(HtmlTextWriterAttribute.Src, clientScriptManager.GetWebResourceUrl(typeof(SiteMapPath), "transparent.gif"));
			writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "0px");
			writer.RenderBeginTag(HtmlTextWriterTag.Img);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x0006FF84 File Offset: 0x0006E184
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			Menu owner = base.Owner;
			if (owner.StaticDisplayLevels == 1 && owner.MaximumDynamicDisplayLevels > 0)
			{
				owner.RenderDynamicMenu(writer, owner.Items);
			}
		}

		// Token: 0x06002A79 RID: 10873 RVA: 0x0006FFB8 File Offset: 0x0006E1B8
		public override void RenderContents(HtmlTextWriter writer)
		{
			Menu owner = base.Owner;
			this.RenderMenuBody(writer, owner.Items, owner.Orientation == Orientation.Vertical, false, false);
		}

		// Token: 0x06002A7A RID: 10874 RVA: 0x0006FFE4 File Offset: 0x0006E1E4
		private void RenderMenuBeginTagAttributes(HtmlTextWriter writer, bool dynamic, int menuLevel)
		{
			writer.AddAttribute("cellpadding", "0", false);
			writer.AddAttribute("cellspacing", "0", false);
			writer.AddAttribute("border", "0", false);
			if (!dynamic)
			{
				SubMenuStyle subMenuStyle = new SubMenuStyle();
				base.FillMenuStyle(null, dynamic, menuLevel, subMenuStyle);
				subMenuStyle.AddAttributesToRender(writer);
			}
		}

		// Token: 0x06002A7B RID: 10875 RVA: 0x0007003E File Offset: 0x0006E23E
		public override void RenderMenuBeginTag(HtmlTextWriter writer, bool dynamic, int menuLevel)
		{
			this.RenderMenuBeginTagAttributes(writer, dynamic, menuLevel);
			writer.RenderBeginTag(HtmlTextWriterTag.Table);
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x00070054 File Offset: 0x0006E254
		private void RenderMenuItemSpacing(HtmlTextWriter writer, Unit itemSpacing, bool vertical)
		{
			if (vertical)
			{
				writer.AddStyleAttribute("height", itemSpacing.ToString());
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				writer.RenderEndTag();
				writer.RenderEndTag();
				return;
			}
			writer.AddStyleAttribute("width", itemSpacing.ToString());
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			writer.RenderEndTag();
		}

		// Token: 0x06002A7D RID: 10877 RVA: 0x000700C0 File Offset: 0x0006E2C0
		public override void RenderMenuBody(HtmlTextWriter writer, MenuItemCollection items, bool vertical, bool dynamic, bool notLast)
		{
			Menu owner = base.Owner;
			if (!vertical)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			}
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
			if (!vertical)
			{
				writer.RenderEndTag();
			}
		}

		// Token: 0x06002A7E RID: 10878 RVA: 0x00070144 File Offset: 0x0006E344
		protected override void RenderMenuItem(HtmlTextWriter writer, MenuItem item, bool vertical, bool notLast, bool isFirst, BaseMenuRenderer.OwnerContext oc)
		{
			Menu owner = base.Owner;
			string clientID = oc.ClientID;
			bool flag = owner.DisplayChildren(item);
			bool flag2 = flag && item.Depth + 1 >= oc.StaticDisplayLevels;
			bool flag3 = this.IsDynamicItem(owner, item);
			bool flag4 = oc.IsVertical || flag3;
			Unit itemSpacing = owner.GetItemSpacing(item, flag3);
			if (itemSpacing != Unit.Empty && (item.Depth > 0 || !isFirst))
			{
				this.RenderMenuItemSpacing(writer, itemSpacing, flag4);
			}
			if (!string.IsNullOrEmpty(item.ToolTip))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Title, item.ToolTip);
			}
			if (flag4)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			}
			string text = (flag3 ? ("'" + item.Parent.Path + "'") : "null");
			if (flag2)
			{
				writer.AddAttribute("onmouseover", string.Concat(new string[] { "javascript:Menu_OverItem ('", clientID, "','", item.Path, "',", text, ")" }));
				writer.AddAttribute("onmouseout", string.Concat(new string[] { "javascript:Menu_OutItem ('", clientID, "','", item.Path, "')" }));
			}
			else if (flag3)
			{
				writer.AddAttribute("onmouseover", string.Concat(new string[] { "javascript:Menu_OverDynamicLeafItem ('", clientID, "','", item.Path, "',", text, ")" }));
				writer.AddAttribute("onmouseout", string.Concat(new string[] { "javascript:Menu_OutItem ('", clientID, "','", item.Path, "',", text, ")" }));
			}
			else
			{
				writer.AddAttribute("onmouseover", string.Concat(new string[] { "javascript:Menu_OverStaticLeafItem ('", clientID, "','", item.Path, "')" }));
				writer.AddAttribute("onmouseout", string.Concat(new string[] { "javascript:Menu_OutItem ('", clientID, "','", item.Path, "')" }));
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			if (flag3)
			{
				base.RenderSeparatorImage(owner, writer, oc.DynamicTopSeparatorImageUrl, false);
			}
			else
			{
				base.RenderSeparatorImage(owner, writer, oc.StaticTopSeparatorImageUrl, false);
			}
			MenuItemStyle menuItemStyle = new MenuItemStyle();
			if (oc.Header != null)
			{
				if (!flag3 && oc.StaticMenuItemStyle != null)
				{
					base.AddCssClass(menuItemStyle, oc.StaticMenuItemStyle.CssClass);
					base.AddCssClass(menuItemStyle, oc.StaticMenuItemStyle.RegisteredCssClass);
				}
				if (flag3 && oc.DynamicMenuItemStyle != null)
				{
					base.AddCssClass(menuItemStyle, oc.DynamicMenuItemStyle.CssClass);
					base.AddCssClass(menuItemStyle, oc.DynamicMenuItemStyle.RegisteredCssClass);
				}
				if (oc.LevelMenuItemStyles != null && oc.LevelMenuItemStyles.Count > item.Depth)
				{
					base.AddCssClass(menuItemStyle, oc.LevelMenuItemStyles[item.Depth].CssClass);
					base.AddCssClass(menuItemStyle, oc.LevelMenuItemStyles[item.Depth].RegisteredCssClass);
				}
				if (item == oc.SelectedItem)
				{
					if (!flag3 && oc.StaticSelectedStyle != null)
					{
						base.AddCssClass(menuItemStyle, oc.StaticSelectedStyle.CssClass);
						base.AddCssClass(menuItemStyle, oc.StaticSelectedStyle.RegisteredCssClass);
					}
					if (flag3 && oc.DynamicSelectedStyle != null)
					{
						base.AddCssClass(menuItemStyle, oc.DynamicSelectedStyle.CssClass);
						base.AddCssClass(menuItemStyle, oc.DynamicSelectedStyle.RegisteredCssClass);
					}
					if (oc.LevelSelectedStyles != null && oc.LevelSelectedStyles.Count > item.Depth)
					{
						base.AddCssClass(menuItemStyle, oc.LevelSelectedStyles[item.Depth].CssClass);
						base.AddCssClass(menuItemStyle, oc.LevelSelectedStyles[item.Depth].RegisteredCssClass);
					}
				}
			}
			else
			{
				if (!flag3 && oc.StaticMenuItemStyle != null)
				{
					menuItemStyle.CopyFrom(oc.StaticMenuItemStyle);
				}
				if (flag3 && oc.DynamicMenuItemStyle != null)
				{
					menuItemStyle.CopyFrom(oc.DynamicMenuItemStyle);
				}
				if (oc.LevelMenuItemStyles != null && oc.LevelMenuItemStyles.Count > item.Depth)
				{
					menuItemStyle.CopyFrom(oc.LevelMenuItemStyles[item.Depth]);
				}
				if (item == oc.SelectedItem)
				{
					if (!flag3 && oc.StaticSelectedStyle != null)
					{
						menuItemStyle.CopyFrom(oc.StaticSelectedStyle);
					}
					if (flag3 && oc.DynamicSelectedStyle != null)
					{
						menuItemStyle.CopyFrom(oc.DynamicSelectedStyle);
					}
					if (oc.LevelSelectedStyles != null && oc.LevelSelectedStyles.Count > item.Depth)
					{
						menuItemStyle.CopyFrom(oc.LevelSelectedStyles[item.Depth]);
					}
				}
			}
			menuItemStyle.AddAttributesToRender(writer);
			writer.AddAttribute("id", base.GetItemClientId(clientID, item, "i"));
			writer.AddAttribute("cellpadding", "0", false);
			writer.AddAttribute("cellspacing", "0", false);
			writer.AddAttribute("border", "0", false);
			writer.AddAttribute("width", "100%", false);
			writer.RenderBeginTag(HtmlTextWriterTag.Table);
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			if (flag4)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
			}
			if (!owner.ItemWrap)
			{
				writer.AddStyleAttribute("white-space", "nowrap");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			this.RenderItemHref(owner, writer, item);
			Style style = new Style();
			if (oc.Header != null)
			{
				base.AddCssClass(style, oc.ControlLinkStyle.RegisteredCssClass);
				if (!flag3 && oc.StaticMenuItemStyle != null)
				{
					base.AddCssClass(style, oc.StaticMenuItemStyle.CssClass);
					base.AddCssClass(style, oc.StaticMenuItemLinkStyle.RegisteredCssClass);
				}
				if (flag3 && oc.DynamicMenuItemStyle != null)
				{
					base.AddCssClass(style, oc.DynamicMenuItemStyle.CssClass);
					base.AddCssClass(style, oc.DynamicMenuItemLinkStyle.RegisteredCssClass);
				}
				if (oc.LevelMenuItemStyles != null && oc.LevelMenuItemStyles.Count > item.Depth)
				{
					base.AddCssClass(style, oc.LevelMenuItemStyles[item.Depth].CssClass);
					base.AddCssClass(style, oc.LevelMenuItemLinkStyles[item.Depth].RegisteredCssClass);
				}
				if (item == oc.SelectedItem)
				{
					if (!flag3 && oc.StaticSelectedStyle != null)
					{
						base.AddCssClass(style, oc.StaticSelectedStyle.CssClass);
						base.AddCssClass(style, oc.StaticSelectedLinkStyle.RegisteredCssClass);
					}
					if (flag3 && oc.DynamicSelectedStyle != null)
					{
						base.AddCssClass(style, oc.DynamicSelectedStyle.CssClass);
						base.AddCssClass(style, oc.DynamicSelectedLinkStyle.RegisteredCssClass);
					}
					if (oc.LevelSelectedStyles != null && oc.LevelSelectedStyles.Count > item.Depth)
					{
						base.AddCssClass(style, oc.LevelSelectedStyles[item.Depth].CssClass);
						base.AddCssClass(style, oc.LevelSelectedLinkStyles[item.Depth].RegisteredCssClass);
					}
				}
			}
			else
			{
				style.CopyFrom(oc.ControlLinkStyle);
				if (!flag3 && oc.StaticMenuItemStyle != null)
				{
					style.CopyFrom(oc.StaticMenuItemLinkStyle);
				}
				if (flag3 && oc.DynamicMenuItemStyle != null)
				{
					style.CopyFrom(oc.DynamicMenuItemLinkStyle);
				}
				if (oc.LevelMenuItemStyles != null && oc.LevelMenuItemStyles.Count > item.Depth)
				{
					style.CopyFrom(oc.LevelMenuItemLinkStyles[item.Depth]);
				}
				if (item == oc.SelectedItem)
				{
					if (!flag3 && oc.StaticSelectedStyle != null)
					{
						style.CopyFrom(oc.StaticSelectedLinkStyle);
					}
					if (flag3 && oc.DynamicSelectedStyle != null)
					{
						style.CopyFrom(oc.DynamicSelectedLinkStyle);
					}
					if (oc.LevelSelectedStyles != null && oc.LevelSelectedStyles.Count > item.Depth)
					{
						style.CopyFrom(oc.LevelSelectedLinkStyles[item.Depth]);
					}
				}
				style.AlwaysRenderTextDecoration = true;
			}
			style.AddAttributesToRender(writer);
			writer.AddAttribute("id", base.GetItemClientId(clientID, item, "l"));
			if (item.Depth > 0 && !flag3)
			{
				Unit staticSubMenuIndent = oc.StaticSubMenuIndent;
				double num;
				if (staticSubMenuIndent == Unit.Empty)
				{
					num = 16.0;
				}
				else
				{
					num = staticSubMenuIndent.Value;
				}
				Unit unit = new Unit(num * (double)item.Depth, oc.StaticSubMenuIndent.Type);
				writer.AddStyleAttribute(HtmlTextWriterStyle.MarginLeft, unit.ToString());
			}
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			owner.RenderItemContent(writer, item, flag3);
			writer.RenderEndTag();
			writer.RenderEndTag();
			if (flag2)
			{
				string popOutImage = base.GetPopOutImage(owner, item, flag3);
				if (popOutImage != null)
				{
					writer.RenderBeginTag(HtmlTextWriterTag.Td);
					writer.AddAttribute("src", owner.ResolveClientUrl(popOutImage));
					writer.AddAttribute("border", "0");
					string text2 = string.Format(flag3 ? oc.DynamicPopOutImageTextFormatString : oc.StaticPopOutImageTextFormatString, item.Text);
					writer.AddAttribute(HtmlTextWriterAttribute.Alt, text2);
					writer.RenderBeginTag(HtmlTextWriterTag.Img);
					writer.RenderEndTag();
					writer.RenderEndTag();
				}
			}
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
			if (!flag4 && itemSpacing == Unit.Empty && (notLast || (flag && !flag2)))
			{
				writer.AddStyleAttribute("width", "3px");
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				writer.RenderEndTag();
			}
			string text3 = item.SeparatorImageUrl;
			if (text3.Length == 0)
			{
				if (flag3)
				{
					text3 = oc.DynamicBottomSeparatorImageUrl;
				}
				else
				{
					text3 = oc.StaticBottomSeparatorImageUrl;
				}
			}
			if (text3.Length > 0)
			{
				if (!flag4)
				{
					writer.RenderBeginTag(HtmlTextWriterTag.Td);
				}
				base.RenderSeparatorImage(owner, writer, text3, false);
				if (!flag4)
				{
					writer.RenderEndTag();
				}
			}
			if (flag4)
			{
				writer.RenderEndTag();
			}
			if (itemSpacing != Unit.Empty)
			{
				this.RenderMenuItemSpacing(writer, itemSpacing, flag4);
			}
			if (flag && !flag2)
			{
				if (flag4)
				{
					writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				writer.AddAttribute("width", "100%");
				owner.RenderMenu(writer, item.ChildItems, vertical, false, item.Depth + 1, notLast);
				if (item.Depth + 2 == oc.StaticDisplayLevels)
				{
					owner.RenderDynamicMenu(writer, item.ChildItems);
				}
				writer.RenderEndTag();
				if (flag4)
				{
					writer.RenderEndTag();
				}
			}
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x00070C46 File Offset: 0x0006EE46
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
			return item.Depth + 1 > owner.StaticDisplayLevels;
		}

		// Token: 0x04001ADE RID: 6878
		private const string onPreRenderScript = "var {0} = new Object ();\n{0}.webForm = {1};\n{0}.disappearAfter = {2};\n{0}.vertical = {3};";
	}
}
