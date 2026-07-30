using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a list control that encapsulates a group of radio button controls.</summary>
	// Token: 0x020003F9 RID: 1017
	[ValidationProperty("SelectedItem")]
	[SupportsEventValidation]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class RadioButtonList : ListControl, IRepeatInfoUser, INamingContainer, IPostBackDataHandler
	{
		/// <summary>Gets or sets the distance (in pixels) between the border and the contents of the table cell.</summary>
		/// <returns>The distance (in pixels) between the border and the contents of the table cell. The default is -1, which indicates that this property is not set.</returns>
		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x06002CDF RID: 11487 RVA: 0x00077145 File Offset: 0x00075345
		// (set) Token: 0x06002CE0 RID: 11488 RVA: 0x0005A61F File Offset: 0x0005881F
		[DefaultValue(-1)]
		[WebCategory("Layout")]
		[WebSysDescription("")]
		public virtual int CellPadding
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return -1;
				}
				return ((TableStyle)base.ControlStyle).CellPadding;
			}
			set
			{
				((TableStyle)base.ControlStyle).CellPadding = value;
			}
		}

		/// <summary>Gets or sets the distance (in pixels) between adjacent table cells.</summary>
		/// <returns>The distance (in pixels) between adjacent table cells. The default is -1, which indicates that this property is not set.</returns>
		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x06002CE1 RID: 11489 RVA: 0x00077161 File Offset: 0x00075361
		// (set) Token: 0x06002CE2 RID: 11490 RVA: 0x0005A64E File Offset: 0x0005884E
		[WebCategory("Layout")]
		[WebSysDescription("")]
		[DefaultValue(-1)]
		public virtual int CellSpacing
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return -1;
				}
				return ((TableStyle)base.ControlStyle).CellSpacing;
			}
			set
			{
				((TableStyle)base.ControlStyle).CellSpacing = value;
			}
		}

		/// <summary>Gets or sets the number of columns to display in the <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> control.</summary>
		/// <returns>The number of columns to display in the <see cref="T:System.Web.UI.WebControls.RadioButtonList" />. The default is 0, which indicates that this property is not set.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of columns is set to a negative value. </exception>
		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x06002CE3 RID: 11491 RVA: 0x0004F318 File Offset: 0x0004D518
		// (set) Token: 0x06002CE4 RID: 11492 RVA: 0x0007717D File Offset: 0x0007537D
		[DefaultValue(0)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		public virtual int RepeatColumns
		{
			get
			{
				return this.ViewState.GetInt("RepeatColumns", 0);
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("The number of columns is set to a negative value.");
				}
				this.ViewState["RepeatColumns"] = value;
			}
		}

		/// <summary>Gets or sets the direction in which the radio buttons within the group are displayed.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.RepeatDirection" /> values. The default is Vertical.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The display direction of the list is not one of the <see cref="T:System.Web.UI.WebControls.RepeatDirection" /> values. </exception>
		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x06002CE5 RID: 11493 RVA: 0x0004F352 File Offset: 0x0004D552
		// (set) Token: 0x06002CE6 RID: 11494 RVA: 0x000771A4 File Offset: 0x000753A4
		[WebCategory("Layout")]
		[WebSysDescription("")]
		[DefaultValue(RepeatDirection.Vertical)]
		public virtual RepeatDirection RepeatDirection
		{
			get
			{
				return (RepeatDirection)this.ViewState.GetInt("RepeatDirection", 1);
			}
			set
			{
				if (value != RepeatDirection.Horizontal && value != RepeatDirection.Vertical)
				{
					throw new ArgumentOutOfRangeException("he display direction of the list is not one of the RepeatDirection values.");
				}
				this.ViewState["RepeatDirection"] = value;
			}
		}

		/// <summary>Gets or sets a value that specifies whether the list will be rendered by using a table element, a ul element, an ol element, or a span element.</summary>
		/// <returns>A value that specifies whether the list will be rendered by using a table element, a ul element, an ol element, or a span element. The default is <see cref="F:System.Web.UI.WebControls.RepeatLayout.Table" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The radio button layout is not one of the <see cref="T:System.Web.UI.WebControls.RepeatLayout" /> values. </exception>
		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x06002CE7 RID: 11495 RVA: 0x0004F390 File Offset: 0x0004D590
		// (set) Token: 0x06002CE8 RID: 11496 RVA: 0x000771CE File Offset: 0x000753CE
		[DefaultValue(RepeatLayout.Table)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		public virtual RepeatLayout RepeatLayout
		{
			get
			{
				return (RepeatLayout)this.ViewState.GetInt("RepeatLayout", 0);
			}
			set
			{
				if (value < RepeatLayout.Table || value > RepeatLayout.OrderedList)
				{
					throw new ArgumentOutOfRangeException("The radio buttons layout is not one of the RepeatLayout values.");
				}
				this.ViewState["RepeatLayout"] = value;
			}
		}

		/// <summary>Gets or sets the text alignment for the radio buttons within the group.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.TextAlign" /> values. The default is Right.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The label text alignment associated with the radio buttons is not one of the <see cref="T:System.Web.UI.WebControls.TextAlign" /> values. </exception>
		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x06002CE9 RID: 11497 RVA: 0x0004E627 File Offset: 0x0004C827
		// (set) Token: 0x06002CEA RID: 11498 RVA: 0x000771FE File Offset: 0x000753FE
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[DefaultValue(TextAlign.Right)]
		public virtual TextAlign TextAlign
		{
			get
			{
				return (TextAlign)this.ViewState.GetInt("TextAlign", 2);
			}
			set
			{
				if (value != TextAlign.Left && value != TextAlign.Right)
				{
					throw new ArgumentOutOfRangeException("The label text alignment associated with the radio buttons is not one of the TextAlign values.");
				}
				this.ViewState["TextAlign"] = value;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> control contains a footer section.</summary>
		/// <returns>false, indicating that the <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> does not contain a footer section.</returns>
		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x06002CEB RID: 11499 RVA: 0x00008A69 File Offset: 0x00006C69
		protected virtual bool HasFooter
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> control contains a heading section.</summary>
		/// <returns>false, indicating that a <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> control does not contain a heading section.</returns>
		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x06002CEC RID: 11500 RVA: 0x00008A69 File Offset: 0x00006C69
		protected virtual bool HasHeader
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> control contains separators between items in the list.</summary>
		/// <returns>false, indicating that a <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> control does not contain separators.</returns>
		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x06002CED RID: 11501 RVA: 0x00008A69 File Offset: 0x00006C69
		protected virtual bool HasSeparators
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the number of list items in the <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> control.</summary>
		/// <returns>The number of items in the list control.</returns>
		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x06002CEE RID: 11502 RVA: 0x0004F640 File Offset: 0x0004D840
		protected virtual int RepeatedItemCount
		{
			get
			{
				return this.Items.Count;
			}
		}

		/// <summary>Gets a value that indicates whether the list control contains a footer section.</summary>
		/// <returns>true if the list control contains a footer section; otherwise, false. </returns>
		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x06002CEF RID: 11503 RVA: 0x00077229 File Offset: 0x00075429
		bool IRepeatInfoUser.HasFooter
		{
			get
			{
				return this.HasFooter;
			}
		}

		/// <summary>Gets a value that indicates whether the list control contains a heading section.</summary>
		/// <returns>true if the list control contains a header section; otherwise, false. </returns>
		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x06002CF0 RID: 11504 RVA: 0x00077231 File Offset: 0x00075431
		bool IRepeatInfoUser.HasHeader
		{
			get
			{
				return this.HasHeader;
			}
		}

		/// <summary>Gets a value that indicates whether the list control contains a separator between items in the list.</summary>
		/// <returns>true if the list control contains has separators; otherwise, false. </returns>
		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x06002CF1 RID: 11505 RVA: 0x00077239 File Offset: 0x00075439
		bool IRepeatInfoUser.HasSeparators
		{
			get
			{
				return this.HasSeparators;
			}
		}

		/// <summary>Gets the number of items in the list control.</summary>
		/// <returns>The number of items in the control.</returns>
		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x06002CF2 RID: 11506 RVA: 0x00077241 File Offset: 0x00075441
		int IRepeatInfoUser.RepeatedItemCount
		{
			get
			{
				return this.RepeatedItemCount;
			}
		}

		/// <summary>Creates a style object that is used internally by the <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> control to implement all style-related properties.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that contains the style properties of the control.</returns>
		// Token: 0x06002CF3 RID: 11507 RVA: 0x0004F3FE File Offset: 0x0004D5FE
		protected override Style CreateControlStyle()
		{
			return new TableStyle(this.ViewState);
		}

		/// <summary>Searches the current naming container for a server control with the specified ID and path offset. The <see cref="M:System.Web.UI.WebControls.RadioButtonList.FindControl(System.String,System.Int32)" /> method always returns the <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> object.</summary>
		/// <returns>The current <see cref="T:System.Web.UI.WebControls.RadioButtonList" />.</returns>
		/// <param name="id">The identifier for the control to find.</param>
		/// <param name="pathOffset">The number of controls up the page control hierarchy needed to reach a naming container. </param>
		// Token: 0x06002CF4 RID: 11508 RVA: 0x00002058 File Offset: 0x00000258
		protected override Control FindControl(string id, int pathOffset)
		{
			return this;
		}

		/// <summary>Retrieves the style of the specified item type at the specified index in the list control.</summary>
		/// <returns>null, indicating that style attributes are not set on individual list items in a <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> control.</returns>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> enumeration values. </param>
		/// <param name="repeatIndex">An ordinal index that specifies the location of the item in the list control. </param>
		// Token: 0x06002CF5 RID: 11509 RVA: 0x00003BEA File Offset: 0x00001DEA
		protected virtual Style GetItemStyle(ListItemType itemType, int repeatIndex)
		{
			return null;
		}

		/// <summary>Renders a list item in the <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> control.</summary>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> enumeration values. </param>
		/// <param name="repeatIndex">An ordinal index that specifies the location of the item in the list control. </param>
		/// <param name="repeatInfo">A <see cref="T:System.Web.UI.WebControls.RepeatInfo" /> that represents the information used to render the item in the list. </param>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
		// Token: 0x06002CF6 RID: 11510 RVA: 0x0007724C File Offset: 0x0007544C
		protected virtual void RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, HtmlTextWriter writer)
		{
			ListItem listItem = this.Items[repeatIndex];
			RadioButton radioButton = new RadioButton();
			radioButton.Text = listItem.Text;
			radioButton.ID = this.ClientID + "_" + repeatIndex;
			radioButton.TextAlign = this.TextAlign;
			radioButton.GroupName = this.UniqueID;
			radioButton.Page = this.Page;
			radioButton.Checked = listItem.Selected;
			radioButton.ValueAttribute = listItem.Value;
			radioButton.AutoPostBack = this.AutoPostBack;
			radioButton.Enabled = base.IsEnabled;
			radioButton.TabIndex = this.tabIndex;
			radioButton.ValidationGroup = this.ValidationGroup;
			radioButton.CausesValidation = this.CausesValidation;
			if (radioButton.HasAttributes)
			{
				radioButton.Attributes.Clear();
			}
			if (listItem.HasAttributes)
			{
				radioButton.Attributes.CopyFrom(listItem.Attributes);
			}
			radioButton.RenderControl(writer);
		}

		/// <summary>Processes the posted data for the <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> control.</summary>
		/// <returns>true if the state of the <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> is different from the last posting; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control, used to index the <paramref name="postCollection" />.</param>
		/// <param name="postCollection">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains value information indexed by control identifiers. </param>
		// Token: 0x06002CF7 RID: 11511 RVA: 0x00077340 File Offset: 0x00075540
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			this.EnsureDataBound();
			string text = postCollection[postDataKey];
			ListItemCollection items = this.Items;
			int count = items.Count;
			int selectedIndex = this.SelectedIndex;
			for (int i = 0; i < count; i++)
			{
				ListItem listItem = items[i];
				if (listItem != null && !(text != listItem.Value) && i != selectedIndex)
				{
					this.SelectedIndex = i;
					return true;
				}
			}
			return false;
		}

		/// <summary>Notifies the ASP.NET application that the state of the <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> control has changed.</summary>
		// Token: 0x06002CF8 RID: 11512 RVA: 0x000773B0 File Offset: 0x000755B0
		protected virtual void RaisePostDataChangedEvent()
		{
			base.ValidateEvent(this.UniqueID, string.Empty);
			Page page = this.Page;
			if (this.CausesValidation && page != null)
			{
				page.Validate(this.ValidationGroup);
			}
			this.OnSelectedIndexChanged(EventArgs.Empty);
		}

		/// <summary>Processes posted data for the <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> control.</summary>
		/// <returns>true if the server control's state changed as a result of the postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control, used to index the <paramref name="postCollection" />. </param>
		/// <param name="postCollection">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains value information indexed by control identifiers.</param>
		// Token: 0x06002CF9 RID: 11513 RVA: 0x000773F7 File Offset: 0x000755F7
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		/// <summary>Raised when posted data for a control has changed.</summary>
		// Token: 0x06002CFA RID: 11514 RVA: 0x00077401 File Offset: 0x00075601
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		/// <summary>Retrieves the style of the specified item type at the specified index in the list control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that represents the style of the specified item type at the specified index in the list control.</returns>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> enumeration values. </param>
		/// <param name="repeatIndex">An ordinal index that specifies the location of the item in the list. </param>
		// Token: 0x06002CFB RID: 11515 RVA: 0x00077409 File Offset: 0x00075609
		Style IRepeatInfoUser.GetItemStyle(ListItemType itemType, int repeatIndex)
		{
			return this.GetItemStyle(itemType, repeatIndex);
		}

		/// <summary>Renders an item in the list with the specified information.</summary>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> enumeration values. </param>
		/// <param name="repeatIndex">An ordinal index that specifies the location of the item in the list. </param>
		/// <param name="repeatInfo">A <see cref="T:System.Web.UI.WebControls.RepeatInfo" /> that represents the information used to render the item in the list. </param>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
		// Token: 0x06002CFC RID: 11516 RVA: 0x00077413 File Offset: 0x00075613
		void IRepeatInfoUser.RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, HtmlTextWriter writer)
		{
			this.RenderItem(itemType, repeatIndex, repeatInfo, writer);
		}

		/// <summary>Displays the <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> control on the client.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream for rendering on the client. </param>
		// Token: 0x06002CFD RID: 11517 RVA: 0x00077420 File Offset: 0x00075620
		protected internal override void Render(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.ClientScript.RegisterForEventValidation(this.UniqueID);
			}
			if (this.Items.Count == 0)
			{
				return;
			}
			RepeatInfo repeatInfo = new RepeatInfo();
			repeatInfo.RepeatColumns = this.RepeatColumns;
			repeatInfo.RepeatDirection = this.RepeatDirection;
			repeatInfo.RepeatLayout = this.RepeatLayout;
			this.tabIndex = this.TabIndex;
			this.TabIndex = 0;
			repeatInfo.RenderRepeater(writer, this, base.ControlStyle, this);
			this.TabIndex = this.tabIndex;
		}

		/// <summary>Gets or sets a value that indicates whether the control is rendered if the data source has no data or if the control is not data-bound.</summary>
		/// <returns>true if the control is rendered if the data source has no data or if the control is not data-bound; otherwise, false.</returns>
		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x06002CFE RID: 11518 RVA: 0x000774AC File Offset: 0x000756AC
		// (set) Token: 0x06002CFF RID: 11519 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual bool RenderWhenDataEmpty
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x04001B62 RID: 7010
		private short tabIndex;
	}
}
