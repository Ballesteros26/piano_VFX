using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the style for a <see cref="T:System.Web.UI.WebControls.Panel" /> control.</summary>
	// Token: 0x020003E5 RID: 997
	public class PanelStyle : Style
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.PanelStyle" /> class.</summary>
		/// <param name="bag">A <see cref="T:System.Web.UI.StateBag" /> object that represents the state bag in which to store style information.</param>
		// Token: 0x06002BD2 RID: 11218 RVA: 0x0006ED94 File Offset: 0x0006CF94
		public PanelStyle(StateBag bag)
			: base(bag)
		{
		}

		/// <summary>Gets or sets the URL of the background image for the panel control.</summary>
		/// <returns>The URL of the background image for the panel control. The default value is an empty string ("").</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Web.UI.WebControls.PanelStyle.BackImageUrl" /> property is null.</exception>
		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x06002BD3 RID: 11219 RVA: 0x000745A6 File Offset: 0x000727A6
		// (set) Token: 0x06002BD4 RID: 11220 RVA: 0x000745D0 File Offset: 0x000727D0
		[UrlProperty]
		[DefaultValue("")]
		public virtual string BackImageUrl
		{
			get
			{
				if (!base.CheckBit(65536))
				{
					return string.Empty;
				}
				return base.ViewState.GetString("BackImageUrl", string.Empty);
			}
			set
			{
				base.ViewState["BackImageUrl"] = value;
				this.SetBit(65536);
			}
		}

		/// <summary>Gets or sets the direction in which to display controls that include text in a panel control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ContentDirection" /> values. The default is <see cref="F:System.Web.UI.WebControls.ContentDirection.NotSet" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The direction is not one of the <see cref="T:System.Web.UI.WebControls.ContentDirection" /> values.</exception>
		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x06002BD5 RID: 11221 RVA: 0x000745EE File Offset: 0x000727EE
		// (set) Token: 0x06002BD6 RID: 11222 RVA: 0x00074614 File Offset: 0x00072814
		[DefaultValue(ContentDirection.NotSet)]
		public virtual ContentDirection Direction
		{
			get
			{
				if (!base.CheckBit(131072))
				{
					return ContentDirection.NotSet;
				}
				return (ContentDirection)base.ViewState["Direction"];
			}
			set
			{
				base.ViewState["Direction"] = value;
				this.SetBit(131072);
			}
		}

		/// <summary>Gets or sets the horizontal alignment of the contents within a panel control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> values. The default is <see cref="F:System.Web.UI.WebControls.HorizontalAlign.NotSet" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The horizontal alignment is not one of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> values.</exception>
		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x06002BD7 RID: 11223 RVA: 0x00074637 File Offset: 0x00072837
		// (set) Token: 0x06002BD8 RID: 11224 RVA: 0x0007465D File Offset: 0x0007285D
		[DefaultValue(HorizontalAlign.NotSet)]
		public virtual HorizontalAlign HorizontalAlign
		{
			get
			{
				if (!base.CheckBit(262144))
				{
					return HorizontalAlign.NotSet;
				}
				return (HorizontalAlign)base.ViewState["HorizontalAlign"];
			}
			set
			{
				base.ViewState["HorizontalAlign"] = value;
				this.SetBit(262144);
			}
		}

		/// <summary>Gets or sets the visibility and position of scroll bars in a panel control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ScrollBars" /> values. The default is <see cref="F:System.Web.UI.WebControls.ScrollBars.None" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.PanelStyle.ScrollBars" /> property is not one of the <see cref="T:System.Web.UI.WebControls.ScrollBars" /> values.</exception>
		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x06002BD9 RID: 11225 RVA: 0x00074680 File Offset: 0x00072880
		// (set) Token: 0x06002BDA RID: 11226 RVA: 0x000746A6 File Offset: 0x000728A6
		[DefaultValue(ScrollBars.None)]
		public virtual ScrollBars ScrollBars
		{
			get
			{
				if (!base.CheckBit(524288))
				{
					return ScrollBars.None;
				}
				return (ScrollBars)base.ViewState["ScrollBars"];
			}
			set
			{
				base.ViewState["ScrollBars"] = value;
				this.SetBit(524288);
			}
		}

		/// <summary>Gets or sets a value indicating whether the content wraps within the panel.</summary>
		/// <returns>true if the content wraps within the panel; otherwise, false. The default is true.</returns>
		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x06002BDB RID: 11227 RVA: 0x000746C9 File Offset: 0x000728C9
		// (set) Token: 0x06002BDC RID: 11228 RVA: 0x000746EF File Offset: 0x000728EF
		[DefaultValue(true)]
		public virtual bool Wrap
		{
			get
			{
				return !base.CheckBit(1048576) || (bool)base.ViewState["Wrap"];
			}
			set
			{
				base.ViewState["Wrap"] = value;
				this.SetBit(1048576);
			}
		}

		/// <summary>Duplicates the style properties of the specified <see cref="T:System.Web.UI.WebControls.Style" /> object for the current instance of the <see cref="T:System.Web.UI.WebControls.PanelStyle" /> class.</summary>
		/// <param name="s">A <see cref="T:System.Web.UI.WebControls.Style" /> object that represents the style settings to copy.</param>
		// Token: 0x06002BDD RID: 11229 RVA: 0x00074714 File Offset: 0x00072914
		public override void CopyFrom(Style s)
		{
			if (s == null || s.IsEmpty)
			{
				return;
			}
			base.CopyFrom(s);
			PanelStyle panelStyle = s as PanelStyle;
			if (panelStyle == null)
			{
				return;
			}
			if (s.CheckBit(65536))
			{
				this.BackImageUrl = panelStyle.BackImageUrl;
			}
			if (s.CheckBit(131072))
			{
				this.Direction = panelStyle.Direction;
			}
			if (s.CheckBit(262144))
			{
				this.HorizontalAlign = panelStyle.HorizontalAlign;
			}
			if (s.CheckBit(524288))
			{
				this.ScrollBars = panelStyle.ScrollBars;
			}
			if (s.CheckBit(1048576))
			{
				this.Wrap = panelStyle.Wrap;
			}
		}

		/// <summary>Combines the style settings of the specified <see cref="T:System.Web.UI.WebControls.Style" /> object with the current instance of the <see cref="T:System.Web.UI.WebControls.PanelStyle" /> class.</summary>
		/// <param name="s">A <see cref="T:System.Web.UI.WebControls.Style" /> object that represents the style settings to combine with the <see cref="T:System.Web.UI.WebControls.PanelStyle" /> object.</param>
		// Token: 0x06002BDE RID: 11230 RVA: 0x000747BC File Offset: 0x000729BC
		public override void MergeWith(Style s)
		{
			if (s == null || s.IsEmpty)
			{
				return;
			}
			base.MergeWith(s);
			PanelStyle panelStyle = s as PanelStyle;
			if (panelStyle == null)
			{
				return;
			}
			if (!base.CheckBit(65536) && s.CheckBit(65536))
			{
				this.BackImageUrl = panelStyle.BackImageUrl;
			}
			if (!base.CheckBit(131072) && s.CheckBit(131072))
			{
				this.Direction = panelStyle.Direction;
			}
			if (!base.CheckBit(262144) && s.CheckBit(262144))
			{
				this.HorizontalAlign = panelStyle.HorizontalAlign;
			}
			if (!base.CheckBit(524288) && s.CheckBit(524288))
			{
				this.ScrollBars = panelStyle.ScrollBars;
			}
			if (!base.CheckBit(1048576) && s.CheckBit(1048576))
			{
				this.Wrap = panelStyle.Wrap;
			}
		}

		/// <summary>Removes any defined style settings from the <see cref="T:System.Web.UI.WebControls.PanelStyle" /> class.</summary>
		// Token: 0x06002BDF RID: 11231 RVA: 0x000748A8 File Offset: 0x00072AA8
		public override void Reset()
		{
			base.Reset();
			base.ViewState.Remove("BackImageUrl");
			base.ViewState.Remove("Direction");
			base.ViewState.Remove("HorizontalAlign");
			base.ViewState.Remove("ScrollBars");
			base.ViewState.Remove("Wrap");
		}

		// Token: 0x020003E6 RID: 998
		[Flags]
		private enum PanelStyles
		{
			// Token: 0x04001B2F RID: 6959
			BackImageUrl = 65536,
			// Token: 0x04001B30 RID: 6960
			Direction = 131072,
			// Token: 0x04001B31 RID: 6961
			HorizontalAlign = 262144,
			// Token: 0x04001B32 RID: 6962
			ScrollBars = 524288,
			// Token: 0x04001B33 RID: 6963
			Wrap = 1048576
		}
	}
}
