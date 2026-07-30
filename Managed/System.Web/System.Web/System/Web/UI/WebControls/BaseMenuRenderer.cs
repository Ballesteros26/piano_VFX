using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.HtmlControls;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000338 RID: 824
	internal abstract class BaseMenuRenderer : IMenuRenderer
	{
		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06001CE9 RID: 7401
		public abstract HtmlTextWriterTag Tag { get; }

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06001CEA RID: 7402 RVA: 0x00048066 File Offset: 0x00046266
		// (set) Token: 0x06001CEB RID: 7403 RVA: 0x0004806E File Offset: 0x0004626E
		private protected Menu Owner { protected get; private set; }

		// Token: 0x06001CEC RID: 7404 RVA: 0x00048077 File Offset: 0x00046277
		public BaseMenuRenderer(Menu owner)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this.Owner = owner;
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x0004809C File Offset: 0x0004629C
		public virtual void AddAttributesToRender(HtmlTextWriter writer)
		{
			Menu owner = this.Owner;
			Page page = owner.Page;
			SubMenuStyle staticMenuStyleInternal = owner.StaticMenuStyleInternal;
			SubMenuStyleCollection levelSubMenuStylesInternal = owner.LevelSubMenuStylesInternal;
			bool flag = levelSubMenuStylesInternal != null && levelSubMenuStylesInternal.Count > 0;
			Style style = ((flag || staticMenuStyleInternal != null) ? owner.ControlStyle : null);
			if (page != null && page.Header != null)
			{
				if (staticMenuStyleInternal != null)
				{
					this.AddCssClass(style, staticMenuStyleInternal.CssClass);
					this.AddCssClass(style, staticMenuStyleInternal.RegisteredCssClass);
				}
				if (flag)
				{
					this.AddCssClass(style, levelSubMenuStylesInternal[0].CssClass);
					this.AddCssClass(style, levelSubMenuStylesInternal[0].RegisteredCssClass);
					return;
				}
			}
			else
			{
				if (staticMenuStyleInternal != null)
				{
					style.CopyFrom(staticMenuStyleInternal);
				}
				if (flag)
				{
					style.CopyFrom(levelSubMenuStylesInternal[0]);
				}
			}
		}

		// Token: 0x06001CEE RID: 7406
		public abstract void PreRender(Page page, HtmlHead head, ClientScriptManager csm, string cmenu, StringBuilder script);

		// Token: 0x06001CEF RID: 7407
		public abstract void RenderMenuBeginTag(HtmlTextWriter writer, bool dynamic, int menuLevel);

		// Token: 0x06001CF0 RID: 7408
		public abstract void RenderMenuBody(HtmlTextWriter writer, MenuItemCollection items, bool vertical, bool dynamic, bool notLast);

		// Token: 0x06001CF1 RID: 7409
		public abstract void RenderBeginTag(HtmlTextWriter writer, string skipLinkText);

		// Token: 0x06001CF2 RID: 7410
		public abstract void RenderEndTag(HtmlTextWriter writer);

		// Token: 0x06001CF3 RID: 7411
		public abstract void RenderContents(HtmlTextWriter writer);

		// Token: 0x06001CF4 RID: 7412
		public abstract bool IsDynamicItem(Menu owner, MenuItem item);

		// Token: 0x06001CF5 RID: 7413
		protected abstract void RenderMenuItem(HtmlTextWriter writer, MenuItem item, bool vertical, bool notLast, bool isFirst, BaseMenuRenderer.OwnerContext oc);

		// Token: 0x06001CF6 RID: 7414 RVA: 0x0004815C File Offset: 0x0004635C
		public virtual void RenderMenuItem(HtmlTextWriter writer, MenuItem item, bool notLast, bool isFirst)
		{
			BaseMenuRenderer.OwnerContext ownerContext = new BaseMenuRenderer.OwnerContext(this);
			this.RenderMenuItem(writer, item, ownerContext.IsVertical, notLast, isFirst, ownerContext);
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x00045C5D File Offset: 0x00043E5D
		public virtual void RenderMenuEndTag(HtmlTextWriter writer, bool dynamic, int menuLevel)
		{
			writer.RenderEndTag();
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x00048184 File Offset: 0x00046384
		public virtual void RenderItemContent(HtmlTextWriter writer, MenuItem item, bool isDynamicItem)
		{
			Menu owner = this.Owner;
			if (!string.IsNullOrEmpty(item.ImageUrl))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Src, owner.ResolveClientUrl(item.ImageUrl));
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, item.ToolTip);
				writer.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, "none");
				writer.AddStyleAttribute(HtmlTextWriterStyle.VerticalAlign, "middle");
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
			}
			string text;
			if (isDynamicItem && (text = owner.DynamicItemFormatString).Length > 0)
			{
				writer.Write(string.Format(text, item.Text));
				return;
			}
			if (!isDynamicItem && (text = owner.StaticItemFormatString).Length > 0)
			{
				writer.Write(string.Format(text, item.Text));
				return;
			}
			writer.Write(item.Text);
		}

		// Token: 0x06001CF9 RID: 7417 RVA: 0x00048245 File Offset: 0x00046445
		public void AddCssClass(Style style, string cssClass)
		{
			style.AddCssClass(cssClass);
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x0004824E File Offset: 0x0004644E
		public string GetItemClientId(string ownerClientID, MenuItem item, string suffix)
		{
			return ownerClientID + "_" + item.Path + suffix;
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x00048264 File Offset: 0x00046464
		public virtual void RenderItemHref(Menu owner, HtmlTextWriter writer, MenuItem item)
		{
			if (!item.BranchEnabled)
			{
				writer.AddAttribute("disabled", "true", false);
				return;
			}
			if (!item.Selectable)
			{
				writer.AddAttribute("href", "#", false);
				writer.AddStyleAttribute("cursor", "text");
				return;
			}
			if (item.NavigateUrl != string.Empty)
			{
				string text = ((item.Target != string.Empty) ? item.Target : owner.Target);
				string text2 = owner.ResolveClientUrl(item.NavigateUrl);
				writer.AddAttribute("href", text2);
				if (text != string.Empty)
				{
					writer.AddAttribute("target", text);
					return;
				}
			}
			else
			{
				writer.AddAttribute("href", this.GetClientEvent(owner, item));
			}
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x00048330 File Offset: 0x00046530
		public string GetPopOutImage(Menu owner, MenuItem item, bool isDynamicItem)
		{
			if (owner == null)
			{
				owner = this.Owner;
			}
			if (item.PopOutImageUrl != string.Empty)
			{
				return item.PopOutImageUrl;
			}
			bool flag = false;
			if (isDynamicItem)
			{
				if (owner.DynamicPopOutImageUrl != string.Empty)
				{
					return owner.DynamicPopOutImageUrl;
				}
				if (owner.DynamicEnableDefaultPopOutImage)
				{
					flag = true;
				}
			}
			else
			{
				if (owner.StaticPopOutImageUrl != string.Empty)
				{
					return owner.StaticPopOutImageUrl;
				}
				if (owner.StaticEnableDefaultPopOutImage)
				{
					flag = true;
				}
			}
			if (flag)
			{
				return this.GetArrowResourceUrl(owner);
			}
			return null;
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x000483BC File Offset: 0x000465BC
		public string GetArrowResourceUrl(Menu owner)
		{
			Page page = owner.Page;
			ClientScriptManager clientScriptManager = ((page != null) ? page.ClientScript : null);
			if (clientScriptManager != null)
			{
				return clientScriptManager.GetWebResourceUrl(typeof(Menu), "arrow_plus.gif");
			}
			return null;
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x000483F8 File Offset: 0x000465F8
		public void FillMenuStyle(HtmlHead header, bool dynamic, int menuLevel, SubMenuStyle style)
		{
			Menu owner = this.Owner;
			if (header == null)
			{
				Page page = owner.Page;
				header = ((page != null) ? page.Header : null);
			}
			SubMenuStyle staticMenuStyleInternal = owner.StaticMenuStyleInternal;
			SubMenuStyle dynamicMenuStyleInternal = owner.DynamicMenuStyleInternal;
			SubMenuStyleCollection levelSubMenuStylesInternal = owner.LevelSubMenuStylesInternal;
			if (header != null)
			{
				if (!dynamic && staticMenuStyleInternal != null)
				{
					this.AddCssClass(style, staticMenuStyleInternal.CssClass);
					this.AddCssClass(style, staticMenuStyleInternal.RegisteredCssClass);
				}
				if (dynamic && dynamicMenuStyleInternal != null)
				{
					this.AddCssClass(style, dynamicMenuStyleInternal.CssClass);
					this.AddCssClass(style, dynamicMenuStyleInternal.RegisteredCssClass);
				}
				if (levelSubMenuStylesInternal != null && levelSubMenuStylesInternal.Count > menuLevel)
				{
					this.AddCssClass(style, levelSubMenuStylesInternal[menuLevel].CssClass);
					this.AddCssClass(style, levelSubMenuStylesInternal[menuLevel].RegisteredCssClass);
					return;
				}
			}
			else
			{
				if (!dynamic && staticMenuStyleInternal != null)
				{
					style.CopyFrom(staticMenuStyleInternal);
				}
				if (dynamic && dynamicMenuStyleInternal != null)
				{
					style.CopyFrom(dynamicMenuStyleInternal);
				}
				if (levelSubMenuStylesInternal != null && levelSubMenuStylesInternal.Count > menuLevel)
				{
					style.CopyFrom(levelSubMenuStylesInternal[menuLevel]);
				}
			}
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x000484EE File Offset: 0x000466EE
		public void RegisterStyle(Style baseStyle, Style linkStyle, HtmlHead head)
		{
			this.RegisterStyle(baseStyle, linkStyle, null, head);
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x000484FA File Offset: 0x000466FA
		public void RegisterStyle(Style baseStyle, Style linkStyle, string className, HtmlHead head)
		{
			if (head == null)
			{
				return;
			}
			linkStyle.CopyTextStylesFrom(baseStyle);
			linkStyle.BorderStyle = BorderStyle.None;
			this.RegisterStyle(linkStyle, className, head);
			this.RegisterStyle(baseStyle, className, head);
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x00048523 File Offset: 0x00046723
		public void RegisterStyle(Style baseStyle, HtmlHead head)
		{
			this.RegisterStyle(baseStyle, null, head);
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x0004852E File Offset: 0x0004672E
		public void RegisterStyle(Style baseStyle, string className, HtmlHead head)
		{
			if (head == null)
			{
				return;
			}
			if (string.IsNullOrEmpty(className))
			{
				className = this.IncrementStyleClassName();
			}
			baseStyle.SetRegisteredCssClass(className);
			head.StyleSheet.CreateStyleRule(baseStyle, this.Owner, "." + className);
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x00048568 File Offset: 0x00046768
		public void RenderSeparatorImage(Menu owner, HtmlTextWriter writer, string url, bool standardsCompliant)
		{
			if (string.IsNullOrEmpty(url))
			{
				return;
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Src, owner.ResolveClientUrl(url));
			if (standardsCompliant)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, string.Empty);
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "separator");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Img);
			writer.RenderEndTag();
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x000485B8 File Offset: 0x000467B8
		public bool IsDynamicItem(MenuItem item)
		{
			return this.IsDynamicItem(this.Owner, item);
		}

		// Token: 0x06001D05 RID: 7429 RVA: 0x000485C8 File Offset: 0x000467C8
		private string GetClientEvent(Menu owner, MenuItem item)
		{
			if (owner == null)
			{
				owner = this.Owner;
			}
			Page page = owner.Page;
			ClientScriptManager clientScriptManager = ((page != null) ? page.ClientScript : null);
			if (clientScriptManager == null)
			{
				return string.Empty;
			}
			return clientScriptManager.GetPostBackClientHyperlink(owner, item.Path, true);
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x0004860B File Offset: 0x0004680B
		private string IncrementStyleClassName()
		{
			this.registeredStylesCounter++;
			return this.Owner.ClientID + "_" + this.registeredStylesCounter;
		}

		// Token: 0x04001802 RID: 6146
		private int registeredStylesCounter = -1;

		// Token: 0x02000339 RID: 825
		protected sealed class OwnerContext
		{
			// Token: 0x170008E3 RID: 2275
			// (get) Token: 0x06001D07 RID: 7431 RVA: 0x0004863B File Offset: 0x0004683B
			public string StaticPopOutImageTextFormatString
			{
				get
				{
					if (this.staticPopOutImageTextFormatString == null)
					{
						this.staticPopOutImageTextFormatString = this.container.Owner.StaticPopOutImageTextFormatString;
					}
					return this.staticPopOutImageTextFormatString;
				}
			}

			// Token: 0x170008E4 RID: 2276
			// (get) Token: 0x06001D08 RID: 7432 RVA: 0x00048661 File Offset: 0x00046861
			public string DynamicPopOutImageTextFormatString
			{
				get
				{
					if (this.dynamicPopOutImageTextFormatString == null)
					{
						this.dynamicPopOutImageTextFormatString = this.container.Owner.DynamicPopOutImageTextFormatString;
					}
					return this.dynamicPopOutImageTextFormatString;
				}
			}

			// Token: 0x170008E5 RID: 2277
			// (get) Token: 0x06001D09 RID: 7433 RVA: 0x00048687 File Offset: 0x00046887
			public string DynamicTopSeparatorImageUrl
			{
				get
				{
					if (this.dynamicTopSeparatorImageUrl == null)
					{
						this.dynamicTopSeparatorImageUrl = this.container.Owner.DynamicTopSeparatorImageUrl;
					}
					return this.dynamicTopSeparatorImageUrl;
				}
			}

			// Token: 0x170008E6 RID: 2278
			// (get) Token: 0x06001D0A RID: 7434 RVA: 0x000486AD File Offset: 0x000468AD
			public string DynamicBottomSeparatorImageUrl
			{
				get
				{
					if (this.dynamicBottomSeparatorImageUrl == null)
					{
						this.dynamicBottomSeparatorImageUrl = this.container.Owner.DynamicBottomSeparatorImageUrl;
					}
					return this.dynamicBottomSeparatorImageUrl;
				}
			}

			// Token: 0x170008E7 RID: 2279
			// (get) Token: 0x06001D0B RID: 7435 RVA: 0x000486D3 File Offset: 0x000468D3
			public string StaticTopSeparatorImageUrl
			{
				get
				{
					if (this.staticTopSeparatorImageUrl == null)
					{
						this.staticTopSeparatorImageUrl = this.container.Owner.StaticTopSeparatorImageUrl;
					}
					return this.staticBottomSeparatorImageUrl;
				}
			}

			// Token: 0x170008E8 RID: 2280
			// (get) Token: 0x06001D0C RID: 7436 RVA: 0x000486F9 File Offset: 0x000468F9
			public string StaticBottomSeparatorImageUrl
			{
				get
				{
					if (this.staticBottomSeparatorImageUrl == null)
					{
						this.staticBottomSeparatorImageUrl = this.container.Owner.StaticBottomSeparatorImageUrl;
					}
					return this.staticBottomSeparatorImageUrl;
				}
			}

			// Token: 0x170008E9 RID: 2281
			// (get) Token: 0x06001D0D RID: 7437 RVA: 0x0004871F File Offset: 0x0004691F
			public List<Style> LevelMenuItemLinkStyles
			{
				get
				{
					if (this.levelMenuItemLinkStyles == null)
					{
						this.levelMenuItemLinkStyles = this.container.Owner.LevelMenuItemLinkStyles;
					}
					return this.levelMenuItemLinkStyles;
				}
			}

			// Token: 0x170008EA RID: 2282
			// (get) Token: 0x06001D0E RID: 7438 RVA: 0x00048745 File Offset: 0x00046945
			public List<Style> LevelSelectedLinkStyles
			{
				get
				{
					if (this.levelSelectedLinkStyles == null)
					{
						this.levelSelectedLinkStyles = this.container.Owner.LevelSelectedLinkStyles;
					}
					return this.levelSelectedLinkStyles;
				}
			}

			// Token: 0x170008EB RID: 2283
			// (get) Token: 0x06001D0F RID: 7439 RVA: 0x0004876B File Offset: 0x0004696B
			public Style StaticMenuItemLinkStyle
			{
				get
				{
					if (this.staticMenuItemLinkStyle == null)
					{
						this.staticMenuItemLinkStyle = this.container.Owner.StaticMenuItemLinkStyle;
					}
					return this.staticMenuItemLinkStyle;
				}
			}

			// Token: 0x170008EC RID: 2284
			// (get) Token: 0x06001D10 RID: 7440 RVA: 0x00048791 File Offset: 0x00046991
			public Style DynamicMenuItemLinkStyle
			{
				get
				{
					if (this.dynamicMenuItemLinkStyle == null)
					{
						this.dynamicMenuItemLinkStyle = this.container.Owner.DynamicMenuItemLinkStyle;
					}
					return this.dynamicMenuItemLinkStyle;
				}
			}

			// Token: 0x170008ED RID: 2285
			// (get) Token: 0x06001D11 RID: 7441 RVA: 0x000487B7 File Offset: 0x000469B7
			public MenuItemStyle StaticSelectedStyle
			{
				get
				{
					if (this.staticSelectedStyle == null)
					{
						this.staticSelectedStyle = this.container.Owner.StaticSelectedStyle;
					}
					return this.staticSelectedStyle;
				}
			}

			// Token: 0x170008EE RID: 2286
			// (get) Token: 0x06001D12 RID: 7442 RVA: 0x000487DD File Offset: 0x000469DD
			public MenuItemStyle DynamicSelectedStyle
			{
				get
				{
					if (this.dynamicSelectedStyle == null)
					{
						this.dynamicSelectedStyle = this.container.Owner.DynamicSelectedStyle;
					}
					return this.dynamicSelectedStyle;
				}
			}

			// Token: 0x170008EF RID: 2287
			// (get) Token: 0x06001D13 RID: 7443 RVA: 0x00048803 File Offset: 0x00046A03
			public Style StaticSelectedLinkStyle
			{
				get
				{
					if (this.staticSelectedLinkStyle == null)
					{
						this.staticSelectedLinkStyle = this.container.Owner.StaticSelectedLinkStyle;
					}
					return this.staticSelectedLinkStyle;
				}
			}

			// Token: 0x170008F0 RID: 2288
			// (get) Token: 0x06001D14 RID: 7444 RVA: 0x00048829 File Offset: 0x00046A29
			public Style DynamicSelectedLinkStyle
			{
				get
				{
					if (this.dynamicSelectedLinkStyle == null)
					{
						this.dynamicSelectedLinkStyle = this.container.Owner.DynamicSelectedLinkStyle;
					}
					return this.dynamicSelectedLinkStyle;
				}
			}

			// Token: 0x170008F1 RID: 2289
			// (get) Token: 0x06001D15 RID: 7445 RVA: 0x0004884F File Offset: 0x00046A4F
			public MenuItemStyleCollection LevelSelectedStyles
			{
				get
				{
					if (this.levelSelectedStyles == null)
					{
						this.levelSelectedStyles = this.container.Owner.LevelSelectedStyles;
					}
					return this.levelSelectedStyles;
				}
			}

			// Token: 0x170008F2 RID: 2290
			// (get) Token: 0x06001D16 RID: 7446 RVA: 0x00048875 File Offset: 0x00046A75
			public ITemplate DynamicItemTemplate
			{
				get
				{
					if (!this.dynamicItemTemplateQueried && this.dynamicItemTemplate == null)
					{
						this.dynamicItemTemplate = this.container.Owner.DynamicItemTemplate;
						this.dynamicItemTemplateQueried = true;
					}
					return this.dynamicItemTemplate;
				}
			}

			// Token: 0x06001D17 RID: 7447 RVA: 0x000488AC File Offset: 0x00046AAC
			public OwnerContext(BaseMenuRenderer container)
			{
				if (container == null)
				{
					throw new ArgumentNullException("container");
				}
				this.container = container;
				Menu owner = container.Owner;
				Page page = owner.Page;
				this.Header = ((page != null) ? page.Header : null);
				this.ClientID = owner.ClientID;
				this.IsVertical = owner.Orientation == Orientation.Vertical;
				this.StaticSubMenuIndent = owner.StaticSubMenuIndent;
				this.SelectedItem = owner.SelectedItem;
				this.ControlLinkStyle = owner.ControlLinkStyle;
				this.StaticDisplayLevels = owner.StaticDisplayLevels;
				this.StaticMenuItemStyle = owner.StaticMenuItemStyleInternal;
				this.DynamicMenuItemStyle = owner.DynamicMenuItemStyleInternal;
				this.LevelMenuItemStyles = owner.LevelMenuItemStyles;
			}

			// Token: 0x04001804 RID: 6148
			private BaseMenuRenderer container;

			// Token: 0x04001805 RID: 6149
			private string staticPopOutImageTextFormatString;

			// Token: 0x04001806 RID: 6150
			private string dynamicPopOutImageTextFormatString;

			// Token: 0x04001807 RID: 6151
			private string dynamicTopSeparatorImageUrl;

			// Token: 0x04001808 RID: 6152
			private string dynamicBottomSeparatorImageUrl;

			// Token: 0x04001809 RID: 6153
			private string staticTopSeparatorImageUrl;

			// Token: 0x0400180A RID: 6154
			private string staticBottomSeparatorImageUrl;

			// Token: 0x0400180B RID: 6155
			private List<Style> levelMenuItemLinkStyles;

			// Token: 0x0400180C RID: 6156
			private List<Style> levelSelectedLinkStyles;

			// Token: 0x0400180D RID: 6157
			private Style staticMenuItemLinkStyle;

			// Token: 0x0400180E RID: 6158
			private Style dynamicMenuItemLinkStyle;

			// Token: 0x0400180F RID: 6159
			private MenuItemStyle staticSelectedStyle;

			// Token: 0x04001810 RID: 6160
			private Style staticSelectedLinkStyle;

			// Token: 0x04001811 RID: 6161
			private MenuItemStyle dynamicSelectedStyle;

			// Token: 0x04001812 RID: 6162
			private Style dynamicSelectedLinkStyle;

			// Token: 0x04001813 RID: 6163
			private MenuItemStyleCollection levelSelectedStyles;

			// Token: 0x04001814 RID: 6164
			private ITemplate dynamicItemTemplate;

			// Token: 0x04001815 RID: 6165
			private bool dynamicItemTemplateQueried;

			// Token: 0x04001816 RID: 6166
			public readonly MenuItemStyle StaticMenuItemStyle;

			// Token: 0x04001817 RID: 6167
			public readonly MenuItemStyle DynamicMenuItemStyle;

			// Token: 0x04001818 RID: 6168
			public readonly MenuItemStyleCollection LevelMenuItemStyles;

			// Token: 0x04001819 RID: 6169
			public readonly Style ControlLinkStyle;

			// Token: 0x0400181A RID: 6170
			public readonly HtmlHead Header;

			// Token: 0x0400181B RID: 6171
			public readonly string ClientID;

			// Token: 0x0400181C RID: 6172
			public readonly int StaticDisplayLevels;

			// Token: 0x0400181D RID: 6173
			public readonly bool IsVertical;

			// Token: 0x0400181E RID: 6174
			public readonly MenuItem SelectedItem;

			// Token: 0x0400181F RID: 6175
			public readonly Unit StaticSubMenuIndent;
		}
	}
}
