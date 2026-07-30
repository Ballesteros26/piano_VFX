using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies the style for the pager of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. This class cannot be inherited.</summary>
	// Token: 0x0200037D RID: 893
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class DataGridPagerStyle : TableItemStyle
	{
		// Token: 0x06002224 RID: 8740 RVA: 0x00057B9E File Offset: 0x00055D9E
		internal DataGridPagerStyle()
		{
		}

		/// <summary>Gets or sets a value that specifies whether the pager element displays buttons that link to the next and previous page, or numeric buttons that link directly to a page.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.PagerMode" /> values. The default value is NextPrev.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is not one of the <see cref="T:System.Web.UI.WebControls.PagerMode" /> values. </exception>
		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06002225 RID: 8741 RVA: 0x00057BA6 File Offset: 0x00055DA6
		// (set) Token: 0x06002226 RID: 8742 RVA: 0x00057BCC File Offset: 0x00055DCC
		[WebCategory("Misc")]
		[NotifyParentProperty(true)]
		[WebSysDescription("")]
		[DefaultValue(PagerMode.NextPrev)]
		public PagerMode Mode
		{
			get
			{
				if (!base.CheckBit(1048576))
				{
					return PagerMode.NextPrev;
				}
				return (PagerMode)base.ViewState["Mode"];
			}
			set
			{
				base.ViewState["Mode"] = value;
				this.SetBit(1048576);
			}
		}

		/// <summary>Gets or sets the text displayed for the next page button.</summary>
		/// <returns>The text to display for the next page button. The default value is "&amp;gt;", which is rendered as the greater than sign (&gt;).</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is less than 1.</exception>
		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x06002227 RID: 8743 RVA: 0x00057BEF File Offset: 0x00055DEF
		// (set) Token: 0x06002228 RID: 8744 RVA: 0x00057C19 File Offset: 0x00055E19
		[WebCategory("Misc")]
		[DefaultValue("&gt;")]
		[Localizable(true)]
		[WebSysDescription("")]
		[NotifyParentProperty(true)]
		public string NextPageText
		{
			get
			{
				if (!base.CheckBit(2097152))
				{
					return "&gt;";
				}
				return base.ViewState.GetString("NextPageText", "&gt;");
			}
			set
			{
				base.ViewState["NextPageText"] = value;
				this.SetBit(2097152);
			}
		}

		/// <summary>Gets or sets the number of numeric buttons to display concurrently in the pager element of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>The number of numeric buttons to display concurrently in the pager element of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. The default value is 10.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is less than 1.</exception>
		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x06002229 RID: 8745 RVA: 0x00057C37 File Offset: 0x00055E37
		// (set) Token: 0x0600222A RID: 8746 RVA: 0x00057C5B File Offset: 0x00055E5B
		[NotifyParentProperty(true)]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		[DefaultValue(10)]
		public int PageButtonCount
		{
			get
			{
				if (!base.CheckBit(4194304))
				{
					return 10;
				}
				return base.ViewState.GetInt("PageButtonCount", 10);
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("value", "PageButtonCount must be greater than 0");
				}
				base.ViewState["PageButtonCount"] = value;
				this.SetBit(4194304);
			}
		}

		/// <summary>Gets or sets the position of the pager element in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.PagerPosition" /> values. The default value is Bottom.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is not one of the <see cref="T:System.Web.UI.WebControls.PagerPosition" /> values. </exception>
		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x0600222B RID: 8747 RVA: 0x00057C92 File Offset: 0x00055E92
		// (set) Token: 0x0600222C RID: 8748 RVA: 0x00057CB8 File Offset: 0x00055EB8
		[DefaultValue(PagerPosition.Bottom)]
		[NotifyParentProperty(true)]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		public PagerPosition Position
		{
			get
			{
				if (!base.CheckBit(8388608))
				{
					return PagerPosition.Bottom;
				}
				return (PagerPosition)base.ViewState["Position"];
			}
			set
			{
				base.ViewState["Position"] = value;
				this.SetBit(8388608);
			}
		}

		/// <summary>Gets or sets the text displayed for the previous page button.</summary>
		/// <returns>The text to display for the previous page button. The default value is "&amp;lt;", which is rendered as the less than sign (&lt;).</returns>
		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x0600222D RID: 8749 RVA: 0x00057CDB File Offset: 0x00055EDB
		// (set) Token: 0x0600222E RID: 8750 RVA: 0x00057D05 File Offset: 0x00055F05
		[WebCategory("Misc")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("&lt;")]
		[WebSysDescription("")]
		public string PrevPageText
		{
			get
			{
				if (!base.CheckBit(16777216))
				{
					return "&lt;";
				}
				return base.ViewState.GetString("PrevPageText", "&lt;");
			}
			set
			{
				base.ViewState["PrevPageText"] = value;
				this.SetBit(16777216);
			}
		}

		/// <summary>Gets or sets a value indicating whether the pager is displayed in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>true to display the pager; otherwise, false. The default value is true.</returns>
		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x0600222F RID: 8751 RVA: 0x00057D23 File Offset: 0x00055F23
		// (set) Token: 0x06002230 RID: 8752 RVA: 0x00057D45 File Offset: 0x00055F45
		[NotifyParentProperty(true)]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		[DefaultValue(true)]
		public bool Visible
		{
			get
			{
				return !base.CheckBit(33554432) || base.ViewState.GetBool("Visible", true);
			}
			set
			{
				base.ViewState["Visible"] = value;
				this.SetBit(33554432);
			}
		}

		/// <summary>Copies the style of the specified <see cref="T:System.Web.UI.WebControls.Style" /> object into this instance of the <see cref="T:System.Web.UI.WebControls.DataGridPagerStyle" /> class.</summary>
		/// <param name="s">The <see cref="T:System.Web.UI.WebControls.Style" /> to copy from. </param>
		// Token: 0x06002231 RID: 8753 RVA: 0x00057D68 File Offset: 0x00055F68
		public override void CopyFrom(Style s)
		{
			base.CopyFrom(s);
			if (s == null || s.IsEmpty)
			{
				return;
			}
			if (s.CheckBit(1048576) && ((DataGridPagerStyle)s).Mode != PagerMode.NextPrev)
			{
				this.Mode = ((DataGridPagerStyle)s).Mode;
			}
			if (s.CheckBit(2097152) && ((DataGridPagerStyle)s).NextPageText != "&gt;")
			{
				this.NextPageText = ((DataGridPagerStyle)s).NextPageText;
			}
			if (s.CheckBit(4194304) && ((DataGridPagerStyle)s).PageButtonCount != 10)
			{
				this.PageButtonCount = ((DataGridPagerStyle)s).PageButtonCount;
			}
			if (s.CheckBit(8388608) && ((DataGridPagerStyle)s).Position != PagerPosition.Bottom)
			{
				this.Position = ((DataGridPagerStyle)s).Position;
			}
			if (s.CheckBit(16777216) && ((DataGridPagerStyle)s).PrevPageText != "&lt;")
			{
				this.PrevPageText = ((DataGridPagerStyle)s).PrevPageText;
			}
			if (s.CheckBit(33554432) && !((DataGridPagerStyle)s).Visible)
			{
				this.Visible = ((DataGridPagerStyle)s).Visible;
			}
		}

		/// <summary>Merges the style of the specified <see cref="T:System.Web.UI.WebControls.Style" /> object with this instance of the <see cref="T:System.Web.UI.WebControls.DataGridPagerStyle" /> class.</summary>
		/// <param name="s">The <see cref="T:System.Web.UI.WebControls.Style" /> to merge with. </param>
		// Token: 0x06002232 RID: 8754 RVA: 0x00057EA0 File Offset: 0x000560A0
		public override void MergeWith(Style s)
		{
			base.MergeWith(s);
			if (s == null || s.IsEmpty)
			{
				return;
			}
			if (!base.CheckBit(1048576) && s.CheckBit(1048576) && ((DataGridPagerStyle)s).Mode != PagerMode.NextPrev)
			{
				this.Mode = ((DataGridPagerStyle)s).Mode;
			}
			if (!base.CheckBit(2097152) && s.CheckBit(2097152) && ((DataGridPagerStyle)s).NextPageText != "&gt;")
			{
				this.NextPageText = ((DataGridPagerStyle)s).NextPageText;
			}
			if (!base.CheckBit(4194304) && s.CheckBit(4194304) && ((DataGridPagerStyle)s).PageButtonCount != 10)
			{
				this.PageButtonCount = ((DataGridPagerStyle)s).PageButtonCount;
			}
			if (!base.CheckBit(8388608) && s.CheckBit(8388608) && ((DataGridPagerStyle)s).Position != PagerPosition.Bottom)
			{
				this.Position = ((DataGridPagerStyle)s).Position;
			}
			if (!base.CheckBit(16777216) && s.CheckBit(16777216) && ((DataGridPagerStyle)s).PrevPageText != "&lt;")
			{
				this.PrevPageText = ((DataGridPagerStyle)s).PrevPageText;
			}
			if (!base.CheckBit(33554432) && s.CheckBit(33554432) && !((DataGridPagerStyle)s).Visible)
			{
				this.Visible = ((DataGridPagerStyle)s).Visible;
			}
		}

		/// <summary>Restores the <see cref="T:System.Web.UI.WebControls.DataGridPagerStyle" /> object to its default values.</summary>
		// Token: 0x06002233 RID: 8755 RVA: 0x00058028 File Offset: 0x00056228
		public override void Reset()
		{
			base.ViewState.Remove("Mode");
			base.ViewState.Remove("NextPageText");
			base.ViewState.Remove("PageButtonCount");
			base.ViewState.Remove("Position");
			base.ViewState.Remove("PrevPageText");
			base.ViewState.Remove("Visible");
			base.Reset();
		}

		// Token: 0x0200037E RID: 894
		[Flags]
		private enum DataGridPagerStyles
		{
			// Token: 0x04001909 RID: 6409
			Mode = 1048576,
			// Token: 0x0400190A RID: 6410
			NextPageText = 2097152,
			// Token: 0x0400190B RID: 6411
			PageButtonCount = 4194304,
			// Token: 0x0400190C RID: 6412
			Position = 8388608,
			// Token: 0x0400190D RID: 6413
			PrevPageText = 16777216,
			// Token: 0x0400190E RID: 6414
			Visible = 33554432
		}
	}
}
