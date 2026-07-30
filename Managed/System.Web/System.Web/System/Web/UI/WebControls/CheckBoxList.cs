using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Util;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Creates a multi selection check box group that can be dynamically created by binding the control to a data source.</summary>
	// Token: 0x0200034E RID: 846
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class CheckBoxList : ListControl, IRepeatInfoUser, INamingContainer, IPostBackDataHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> class.</summary>
		// Token: 0x06001F3C RID: 7996 RVA: 0x0004F2BE File Offset: 0x0004D4BE
		public CheckBoxList()
		{
			this.check_box = new CheckBox();
			this.Controls.Add(this.check_box);
		}

		/// <summary>Gets or sets the distance (in pixels) between the border and contents of the cell.</summary>
		/// <returns>The distance (in pixels) between the border and contents of the cell. The default is -1, which indicates this property is not set.</returns>
		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06001F3D RID: 7997 RVA: 0x0004F2E2 File Offset: 0x0004D4E2
		// (set) Token: 0x06001F3E RID: 7998 RVA: 0x0004F2EF File Offset: 0x0004D4EF
		[WebSysDescription("")]
		[WebCategory("Layout")]
		[DefaultValue(-1)]
		public virtual int CellPadding
		{
			get
			{
				return this.TableStyle.CellPadding;
			}
			set
			{
				this.TableStyle.CellPadding = value;
			}
		}

		/// <summary>Gets or sets the distance (in pixels) between cells.</summary>
		/// <returns>The distance (in pixels) between cells. The default is -1, which indicates that this property is not set.</returns>
		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06001F3F RID: 7999 RVA: 0x0004F2FD File Offset: 0x0004D4FD
		// (set) Token: 0x06001F40 RID: 8000 RVA: 0x0004F30A File Offset: 0x0004D50A
		[DefaultValue(-1)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		public virtual int CellSpacing
		{
			get
			{
				return this.TableStyle.CellSpacing;
			}
			set
			{
				this.TableStyle.CellSpacing = value;
			}
		}

		/// <summary>Gets or sets the number of columns to display in the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control.</summary>
		/// <returns>The number of columns to display in the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control. The default is 0, which indicates this property is not set.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of columns is set to a negative value. </exception>
		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06001F41 RID: 8001 RVA: 0x0004F318 File Offset: 0x0004D518
		// (set) Token: 0x06001F42 RID: 8002 RVA: 0x0004F32B File Offset: 0x0004D52B
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
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["RepeatColumns"] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the control displays vertically or horizontally.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.RepeatDirection" /> values. The default is Vertical.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified display direction of the list is not one of the <see cref="T:System.Web.UI.WebControls.RepeatDirection" /> values. </exception>
		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06001F43 RID: 8003 RVA: 0x0004F352 File Offset: 0x0004D552
		// (set) Token: 0x06001F44 RID: 8004 RVA: 0x0004F365 File Offset: 0x0004D565
		[WebCategory("Layout")]
		[DefaultValue(RepeatDirection.Vertical)]
		[WebSysDescription("")]
		public virtual RepeatDirection RepeatDirection
		{
			get
			{
				return (RepeatDirection)this.ViewState.GetInt("RepeatDirection", 1);
			}
			set
			{
				if (value < RepeatDirection.Horizontal || value > RepeatDirection.Vertical)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["RepeatDirection"] = value;
			}
		}

		/// <summary>Gets or sets a value that specifies whether the list will be rendered by using a table element, a ul element, an ol element, or a span element.</summary>
		/// <returns>A value that specifies whether the list will be rendered by using a table element, a ul element, an ol element, or a span element. The default is <see cref="F:System.Web.UI.WebControls.RepeatLayout.Table" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified layout is not one of the <see cref="T:System.Web.UI.WebControls.RepeatLayout" /> values. </exception>
		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06001F45 RID: 8005 RVA: 0x0004F390 File Offset: 0x0004D590
		// (set) Token: 0x06001F46 RID: 8006 RVA: 0x0004F3A3 File Offset: 0x0004D5A3
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
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["RepeatLayout"] = value;
			}
		}

		/// <summary>Gets or sets the text alignment for the check boxes within the group.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.TextAlign" /> values. The default value is Right.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified label text alignment is not one of the <see cref="T:System.Web.UI.WebControls.TextAlign" /> values. </exception>
		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06001F47 RID: 8007 RVA: 0x0004E627 File Offset: 0x0004C827
		// (set) Token: 0x06001F48 RID: 8008 RVA: 0x0004F3D3 File Offset: 0x0004D5D3
		[DefaultValue(TextAlign.Right)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual TextAlign TextAlign
		{
			get
			{
				return (TextAlign)this.ViewState.GetInt("TextAlign", 2);
			}
			set
			{
				if (value < TextAlign.Left || value > TextAlign.Right)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["TextAlign"] = value;
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06001F49 RID: 8009 RVA: 0x00047D06 File Offset: 0x00045F06
		private TableStyle TableStyle
		{
			get
			{
				return (TableStyle)base.ControlStyle;
			}
		}

		/// <summary>Creates a style object that is used internally by the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control to implement all style related properties.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that contains the style properties of the control.</returns>
		// Token: 0x06001F4A RID: 8010 RVA: 0x0004F3FE File Offset: 0x0004D5FE
		protected override Style CreateControlStyle()
		{
			return new TableStyle(this.ViewState);
		}

		/// <summary>Searches the current naming container for a server control with the specified ID and path offset. The <see cref="M:System.Web.UI.WebControls.CheckBoxList.FindControl(System.String,System.Int32)" /> method always returns the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> object. </summary>
		/// <returns>The current <see cref="T:System.Web.UI.WebControls.CheckBoxList" />.</returns>
		/// <param name="id">The identifier for the control to find.</param>
		/// <param name="pathOffset">The number of controls up the page control hierarchy needed to reach a naming container. </param>
		// Token: 0x06001F4B RID: 8011 RVA: 0x00002058 File Offset: 0x00000258
		protected override Control FindControl(string id, int pathOffset)
		{
			return this;
		}

		/// <summary>Configures the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control prior to rendering on the client.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001F4C RID: 8012 RVA: 0x0004F40C File Offset: 0x0004D60C
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			Page page = this.Page;
			for (int i = 0; i < this.Items.Count; i++)
			{
				if (this.Items[i].Selected)
				{
					this.check_box.ID = i.ToString(Helpers.InvariantCulture);
					if (page != null)
					{
						page.RegisterRequiresPostBack(this.check_box);
					}
				}
			}
		}

		/// <summary>Displays the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> on the client.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream for rendering on the client. </param>
		// Token: 0x06001F4D RID: 8013 RVA: 0x0004F478 File Offset: 0x0004D678
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this.Items.Count == 0)
			{
				return;
			}
			RepeatInfo repeatInfo = new RepeatInfo();
			repeatInfo.RepeatColumns = this.RepeatColumns;
			repeatInfo.RepeatDirection = this.RepeatDirection;
			repeatInfo.RepeatLayout = this.RepeatLayout;
			short num = 0;
			if (this.TabIndex != 0)
			{
				this.check_box.TabIndex = this.TabIndex;
				num = this.TabIndex;
				this.TabIndex = 0;
			}
			string accessKey = this.AccessKey;
			this.check_box.AccessKey = accessKey;
			this.AccessKey = null;
			repeatInfo.RenderRepeater(writer, this, this.TableStyle, this);
			if (num != 0)
			{
				this.TabIndex = num;
			}
			this.AccessKey = accessKey;
		}

		/// <summary>Processes the posted data for the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control.</summary>
		/// <returns>true if the state of the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> is different from the last posting; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control, used to index the <see cref="T:System.Collections.Specialized.NameValueCollection" /> specified in the <paramref name="postCollection" /> parameter.</param>
		/// <param name="postCollection">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains value information indexed by control identifiers. </param>
		// Token: 0x06001F4E RID: 8014 RVA: 0x0004F520 File Offset: 0x0004D720
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			if (!base.IsEnabled)
			{
				return false;
			}
			this.EnsureDataBound();
			int num = -1;
			try
			{
				string text = postDataKey.Substring(this.ClientID.Length + 1);
				if (char.IsDigit(text[0]))
				{
					num = int.Parse(text, Helpers.InvariantCulture);
				}
			}
			catch
			{
				return false;
			}
			if (num == -1)
			{
				return false;
			}
			bool flag = postCollection[postDataKey] == "on";
			ListItem listItem = this.Items[num];
			if (listItem.Enabled)
			{
				if (flag && !listItem.Selected)
				{
					listItem.Selected = true;
					return true;
				}
				if (!flag && listItem.Selected)
				{
					listItem.Selected = false;
					return true;
				}
			}
			return false;
		}

		/// <summary>Notifies the ASP.NET application that the state of the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control has changed.</summary>
		// Token: 0x06001F4F RID: 8015 RVA: 0x0004F5E0 File Offset: 0x0004D7E0
		protected virtual void RaisePostDataChangedEvent()
		{
			if (this.CausesValidation)
			{
				Page page = this.Page;
				if (page != null)
				{
					page.Validate(this.ValidationGroup);
				}
			}
			this.OnSelectedIndexChanged(EventArgs.Empty);
		}

		/// <summary>Processes posted data for the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control.</summary>
		/// <returns>true if the server control's state changes as a result of the postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control, used to index the <paramref name="postCollection" />.</param>
		/// <param name="postCollection">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> object that contains value information indexed by control identifiers. </param>
		// Token: 0x06001F50 RID: 8016 RVA: 0x0004F616 File Offset: 0x0004D816
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		/// <summary>Raised when posted data for a control has changed.</summary>
		// Token: 0x06001F51 RID: 8017 RVA: 0x0004F620 File Offset: 0x0004D820
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control contains a footer section.</summary>
		/// <returns>false, indicating that a <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> does not contain a footer section.</returns>
		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06001F52 RID: 8018 RVA: 0x00008A69 File Offset: 0x00006C69
		protected virtual bool HasFooter
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the list control contains a footer section.</summary>
		/// <returns>true if the list control contains a footer section; otherwise, false.</returns>
		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06001F53 RID: 8019 RVA: 0x0004F628 File Offset: 0x0004D828
		bool IRepeatInfoUser.HasFooter
		{
			get
			{
				return this.HasFooter;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control contains a heading section. </summary>
		/// <returns>false, indicating that a <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> does not contain a heading section.</returns>
		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06001F54 RID: 8020 RVA: 0x00008A69 File Offset: 0x00006C69
		protected virtual bool HasHeader
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the list control contains a heading section.</summary>
		/// <returns>true if the list control contains a heading section; otherwise, false.</returns>
		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06001F55 RID: 8021 RVA: 0x0004F630 File Offset: 0x0004D830
		bool IRepeatInfoUser.HasHeader
		{
			get
			{
				return this.HasHeader;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control contains a separator between items in the list. </summary>
		/// <returns>false, indicating that a <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> does not contain separators.</returns>
		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06001F56 RID: 8022 RVA: 0x00008A69 File Offset: 0x00006C69
		protected virtual bool HasSeparators
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the list control contains a separator between items in the list.</summary>
		/// <returns>true if the list control contains a separator; otherwise, false.</returns>
		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06001F57 RID: 8023 RVA: 0x0004F638 File Offset: 0x0004D838
		bool IRepeatInfoUser.HasSeparators
		{
			get
			{
				return this.HasSeparators;
			}
		}

		/// <summary>Gets the number of list items in the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control. </summary>
		/// <returns>The number of items in the <see cref="T:System.Web.UI.WebControls.CheckBoxList" />.</returns>
		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06001F58 RID: 8024 RVA: 0x0004F640 File Offset: 0x0004D840
		protected virtual int RepeatedItemCount
		{
			get
			{
				return this.Items.Count;
			}
		}

		/// <summary>Gets the number of items in the list control.</summary>
		/// <returns>The number of items in the list.</returns>
		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06001F59 RID: 8025 RVA: 0x0004F64D File Offset: 0x0004D84D
		int IRepeatInfoUser.RepeatedItemCount
		{
			get
			{
				return this.RepeatedItemCount;
			}
		}

		/// <summary>Retrieves the style of the specified item type at the specified index in the list control.</summary>
		/// <returns>null, indicating that style attributes are not set on individual list items in a <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control.</returns>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> enumeration values. </param>
		/// <param name="repeatIndex">An ordinal index that specifies the location of the item in the list control. </param>
		// Token: 0x06001F5A RID: 8026 RVA: 0x00003BEA File Offset: 0x00001DEA
		protected virtual Style GetItemStyle(ListItemType itemType, int repeatIndex)
		{
			return null;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.WebControls.IRepeatInfoUser.GetItemStyle(System.Web.UI.WebControls.ListItemType,System.Int32)" />. </summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> object that represents the style of the specified item type at the specified index in the list control.</returns>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> enumeration values. </param>
		/// <param name="repeatIndex">An ordinal index that specifies the location of the item in the list control. </param>
		// Token: 0x06001F5B RID: 8027 RVA: 0x0004F655 File Offset: 0x0004D855
		Style IRepeatInfoUser.GetItemStyle(ListItemType itemType, int repeatIndex)
		{
			return this.GetItemStyle(itemType, repeatIndex);
		}

		/// <summary>Renders a list item in the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control.</summary>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> enumeration values. </param>
		/// <param name="repeatIndex">An ordinal index that specifies the location of the item in the list control. </param>
		/// <param name="repeatInfo">A <see cref="T:System.Web.UI.WebControls.RepeatInfo" /> object that represents the information used to render the item in the list. </param>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> object that represents the output stream to render HTML content on the client. </param>
		// Token: 0x06001F5C RID: 8028 RVA: 0x0004F660 File Offset: 0x0004D860
		protected virtual void RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, HtmlTextWriter writer)
		{
			ListItem listItem = this.Items[repeatIndex];
			if (!string.IsNullOrEmpty(this.check_box.CssClass))
			{
				this.check_box.CssClass = string.Empty;
			}
			this.check_box.ID = repeatIndex.ToString(Helpers.InvariantCulture);
			this.check_box.Text = listItem.Text;
			this.check_box.AutoPostBack = this.AutoPostBack;
			this.check_box.Checked = listItem.Selected;
			this.check_box.TextAlign = this.TextAlign;
			if (!base.IsEnabled)
			{
				this.check_box.Enabled = false;
			}
			else
			{
				this.check_box.Enabled = listItem.Enabled;
			}
			this.check_box.ValidationGroup = this.ValidationGroup;
			this.check_box.CausesValidation = this.CausesValidation;
			if (this.check_box.HasAttributes)
			{
				this.check_box.Attributes.Clear();
			}
			if (listItem.HasAttributes)
			{
				this.check_box.Attributes.CopyFrom(listItem.Attributes);
			}
			if (!base.RenderingCompatibilityLessThan40)
			{
				AttributeCollection inputAttributes = this.check_box.InputAttributes;
				inputAttributes.Clear();
				inputAttributes.Add("value", listItem.Value);
			}
			this.check_box.RenderControl(writer);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.WebControls.IRepeatInfoUser.RenderItem(System.Web.UI.WebControls.ListItemType,System.Int32,System.Web.UI.WebControls.RepeatInfo,System.Web.UI.HtmlTextWriter)" />. </summary>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> enumeration values. </param>
		/// <param name="repeatIndex">An ordinal index that specifies the location of the item in the list control. </param>
		/// <param name="repeatInfo">A <see cref="T:System.Web.UI.WebControls.RepeatInfo" /> object that represents the information used to render the item in the list. </param>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> object that represents the output stream to render HTML content on the client. </param>
		// Token: 0x06001F5D RID: 8029 RVA: 0x0004F7B1 File Offset: 0x0004D9B1
		void IRepeatInfoUser.RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, HtmlTextWriter writer)
		{
			this.RenderItem(itemType, repeatIndex, repeatInfo, writer);
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x00008B66 File Offset: 0x00006D66
		internal override bool MultiSelectOk()
		{
			return true;
		}

		/// <summary>Gets or sets a value that indicates whether the control is rendered when the data source has no data or the control is not data-bound.</summary>
		/// <returns>true if the control is rendered when the data source has no data or the control is not data-bound; otherwise false.</returns>
		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06001F5F RID: 8031 RVA: 0x0004F7C0 File Offset: 0x0004D9C0
		// (set) Token: 0x06001F60 RID: 8032 RVA: 0x0000B3E4 File Offset: 0x000095E4
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

		// Token: 0x04001886 RID: 6278
		private CheckBox check_box;
	}
}
