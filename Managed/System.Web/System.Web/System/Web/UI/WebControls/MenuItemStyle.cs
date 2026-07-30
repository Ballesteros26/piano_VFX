using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the style of a menu item in a <see cref="T:System.Web.UI.WebControls.Menu" /> control. This class cannot be inherited.</summary>
	// Token: 0x020003D5 RID: 981
	public sealed class MenuItemStyle : Style
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> class.</summary>
		// Token: 0x06002A42 RID: 10818 RVA: 0x0006ED8C File Offset: 0x0006CF8C
		public MenuItemStyle()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> class using the specified state information.</summary>
		/// <param name="bag">A <see cref="T:System.Web.UI.StateBag" /> that represents the state bag in which menu item style information is stored.</param>
		// Token: 0x06002A43 RID: 10819 RVA: 0x0006ED94 File Offset: 0x0006CF94
		public MenuItemStyle(StateBag bag)
			: base(bag)
		{
		}

		/// <summary>Gets or sets the amount of space to the left and right of the menu item's text.</summary>
		/// <returns>The amount of space (in pixels) to the left and right of the menu item's text. The default is 0.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is of type <see cref="F:System.Web.UI.WebControls.UnitType.Percentage" />.- or -The selected value is less than 0.</exception>
		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x06002A44 RID: 10820 RVA: 0x0006ED9D File Offset: 0x0006CF9D
		// (set) Token: 0x06002A45 RID: 10821 RVA: 0x0006EDC7 File Offset: 0x0006CFC7
		[DefaultValue(typeof(Unit), "")]
		[NotifyParentProperty(true)]
		public Unit HorizontalPadding
		{
			get
			{
				if (base.CheckBit(65536))
				{
					return (Unit)base.ViewState["HorizontalPadding"];
				}
				return Unit.Empty;
			}
			set
			{
				base.ViewState["HorizontalPadding"] = value;
				this.SetBit(65536);
			}
		}

		/// <summary>Gets or sets the amount of space above and below a menu item's text.</summary>
		/// <returns>The amount of space (in pixels) above and below a menu item's text. The default is 0.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is of type <see cref="F:System.Web.UI.WebControls.UnitType.Percentage" />.- or -The selected value is less than 0.</exception>
		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x06002A46 RID: 10822 RVA: 0x0006EDEA File Offset: 0x0006CFEA
		// (set) Token: 0x06002A47 RID: 10823 RVA: 0x0006EE14 File Offset: 0x0006D014
		[DefaultValue(typeof(Unit), "")]
		[NotifyParentProperty(true)]
		public Unit VerticalPadding
		{
			get
			{
				if (base.CheckBit(131072))
				{
					return (Unit)base.ViewState["VerticalPadding"];
				}
				return Unit.Empty;
			}
			set
			{
				base.ViewState["VerticalPadding"] = value;
				this.SetBit(131072);
			}
		}

		/// <summary>Gets or sets the amount of vertical spacing between the menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object is applied and its adjacent menu items.</summary>
		/// <returns>The amount of vertical spacing (in pixels) between the menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object is applied and its adjacent menu items. The default is 0.</returns>
		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x06002A48 RID: 10824 RVA: 0x0006EE37 File Offset: 0x0006D037
		// (set) Token: 0x06002A49 RID: 10825 RVA: 0x0006EE61 File Offset: 0x0006D061
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "")]
		public Unit ItemSpacing
		{
			get
			{
				if (base.CheckBit(262144))
				{
					return (Unit)base.ViewState["ItemSpacing"];
				}
				return Unit.Empty;
			}
			set
			{
				base.ViewState["ItemSpacing"] = value;
				this.SetBit(262144);
			}
		}

		/// <summary>Copies the style properties of the specified <see cref="T:System.Web.UI.WebControls.Style" /> object into the current instance of the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> class.</summary>
		/// <param name="s">The <see cref="T:System.Web.UI.WebControls.Style" /> to copy.</param>
		// Token: 0x06002A4A RID: 10826 RVA: 0x0006EE84 File Offset: 0x0006D084
		public override void CopyFrom(Style s)
		{
			if (s == null || s.IsEmpty)
			{
				return;
			}
			base.CopyFrom(s);
			MenuItemStyle menuItemStyle = s as MenuItemStyle;
			if (menuItemStyle == null)
			{
				return;
			}
			if (menuItemStyle.CheckBit(65536))
			{
				this.HorizontalPadding = menuItemStyle.HorizontalPadding;
			}
			if (menuItemStyle.CheckBit(262144))
			{
				this.ItemSpacing = menuItemStyle.ItemSpacing;
			}
			if (menuItemStyle.CheckBit(131072))
			{
				this.VerticalPadding = menuItemStyle.VerticalPadding;
			}
		}

		/// <summary>Combines the style properties of the specified <see cref="T:System.Web.UI.WebControls.Style" /> object with those of the current instance of the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> class.</summary>
		/// <param name="s">The <see cref="T:System.Web.UI.WebControls.Style" /> to combine settings with.</param>
		// Token: 0x06002A4B RID: 10827 RVA: 0x0006EEFC File Offset: 0x0006D0FC
		public override void MergeWith(Style s)
		{
			if (s == null || s.IsEmpty)
			{
				return;
			}
			base.MergeWith(s);
			MenuItemStyle menuItemStyle = s as MenuItemStyle;
			if (menuItemStyle == null)
			{
				return;
			}
			if (!base.CheckBit(65536) && menuItemStyle.CheckBit(65536))
			{
				this.HorizontalPadding = menuItemStyle.HorizontalPadding;
			}
			if (!base.CheckBit(262144) && menuItemStyle.CheckBit(262144))
			{
				this.ItemSpacing = menuItemStyle.ItemSpacing;
			}
			if (!base.CheckBit(131072) && menuItemStyle.CheckBit(131072))
			{
				this.VerticalPadding = menuItemStyle.VerticalPadding;
			}
		}

		/// <summary>Returns the current instance of the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> class to its original state.</summary>
		// Token: 0x06002A4C RID: 10828 RVA: 0x0006EF99 File Offset: 0x0006D199
		public override void Reset()
		{
			base.ViewState.Remove("HorizontalPadding");
			base.ViewState.Remove("ItemSpacing");
			base.ViewState.Remove("VerticalPadding");
			base.Reset();
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x0006EFD4 File Offset: 0x0006D1D4
		protected override void FillStyleAttributes(CssStyleCollection attributes, IUrlResolutionService urlResolver)
		{
			base.FillStyleAttributes(attributes, urlResolver);
			if (base.CheckBit(65536))
			{
				attributes.Add(HtmlTextWriterStyle.PaddingLeft, this.HorizontalPadding.ToString());
				attributes.Add(HtmlTextWriterStyle.PaddingRight, this.HorizontalPadding.ToString());
			}
			if (base.CheckBit(131072))
			{
				attributes.Add(HtmlTextWriterStyle.PaddingTop, this.VerticalPadding.ToString());
				attributes.Add(HtmlTextWriterStyle.PaddingBottom, this.VerticalPadding.ToString());
			}
		}

		// Token: 0x020003D6 RID: 982
		[Flags]
		private enum MenuItemStyles
		{
			// Token: 0x04001AD7 RID: 6871
			HorizontalPadding = 65536,
			// Token: 0x04001AD8 RID: 6872
			VerticalPadding = 131072,
			// Token: 0x04001AD9 RID: 6873
			ItemSpacing = 262144
		}
	}
}
