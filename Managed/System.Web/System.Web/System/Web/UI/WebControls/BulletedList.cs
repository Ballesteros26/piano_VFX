using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Util;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Creates a control that generates a list of items in a bulleted format.</summary>
	// Token: 0x0200033E RID: 830
	[Designer("System.Web.UI.Design.WebControls.BulletedListDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultEvent("Click")]
	[DefaultProperty("BulletStyle")]
	[SupportsEventValidation]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class BulletedList : ListControl, IPostBackEventHandler
	{
		/// <summary>Occurs when a link button in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control is clicked.</summary>
		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06001D76 RID: 7542 RVA: 0x00049987 File Offset: 0x00047B87
		// (remove) Token: 0x06001D77 RID: 7543 RVA: 0x0004999A File Offset: 0x00047B9A
		public event BulletedListEventHandler Click
		{
			add
			{
				base.Events.AddHandler(BulletedList.ClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(BulletedList.ClickEvent, value);
			}
		}

		/// <summary>Adds the HTML attributes and styles for a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control to render to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.  </param>
		// Token: 0x06001D78 RID: 7544 RVA: 0x000499B0 File Offset: 0x00047BB0
		[global::System.MonoTODO("we are missing a new style enum, we should be using it")]
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			bool flag = false;
			switch (this.BulletStyle)
			{
			case BulletStyle.Numbered:
				writer.AddStyleAttribute("list-style-type", "decimal");
				flag = true;
				break;
			case BulletStyle.LowerAlpha:
				writer.AddStyleAttribute("list-style-type", "lower-alpha");
				flag = true;
				break;
			case BulletStyle.UpperAlpha:
				writer.AddStyleAttribute("list-style-type", "upper-alpha");
				flag = true;
				break;
			case BulletStyle.LowerRoman:
				writer.AddStyleAttribute("list-style-type", "lower-roman");
				flag = true;
				break;
			case BulletStyle.UpperRoman:
				writer.AddStyleAttribute("list-style-type", "upper-roman");
				flag = true;
				break;
			case BulletStyle.Disc:
				writer.AddStyleAttribute("list-style-type", "disc");
				break;
			case BulletStyle.Circle:
				writer.AddStyleAttribute("list-style-type", "circle");
				break;
			case BulletStyle.Square:
				writer.AddStyleAttribute("list-style-type", "square");
				break;
			case BulletStyle.CustomImage:
				writer.AddStyleAttribute("list-style-image", "url(" + base.ResolveClientUrl(this.BulletImageUrl) + ")");
				break;
			}
			if (flag && this.FirstBulletNumber != 1)
			{
				writer.AddAttribute("start", this.FirstBulletNumber.ToString());
			}
			base.AddAttributesToRender(writer);
		}

		/// <summary>Renders the bulleted text for each list item in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
		/// <param name="item">A collection of <see cref="T:System.Web.UI.WebControls.ListItem" /> objects in a <see cref="T:System.Web.UI.WebControls.BulletedList" />. </param>
		/// <param name="index">The zero-based index of the <see cref="T:System.Web.UI.WebControls.ListItem" /> to retrieve from the collection. </param>
		/// <param name="writer">The output stream that renders HTML content to the client. </param>
		// Token: 0x06001D79 RID: 7545 RVA: 0x00049AF0 File Offset: 0x00047CF0
		protected virtual void RenderBulletText(ListItem item, int index, HtmlTextWriter writer)
		{
			string text = HttpUtility.HtmlEncode(item.Text);
			switch (this.DisplayMode)
			{
			case BulletedListDisplayMode.Text:
				if (!item.Enabled)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled", false);
					writer.RenderBeginTag(HtmlTextWriterTag.Span);
				}
				writer.Write(text);
				if (!item.Enabled)
				{
					writer.RenderEndTag();
					return;
				}
				break;
			case BulletedListDisplayMode.HyperLink:
				if (base.IsEnabled && item.Enabled)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Href, item.Value);
					if (this.Target.Length > 0)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Target, this.Target);
					}
				}
				else
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled", false);
				}
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				writer.Write(text);
				writer.RenderEndTag();
				return;
			case BulletedListDisplayMode.LinkButton:
				if (base.IsEnabled && item.Enabled)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Href, this.Page.ClientScript.GetPostBackEventReference(this.GetPostBackOptions(index.ToString(Helpers.InvariantCulture)), true));
				}
				else
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled", false);
				}
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				writer.Write(text);
				writer.RenderEndTag();
				break;
			default:
				return;
			}
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x00049C18 File Offset: 0x00047E18
		private PostBackOptions GetPostBackOptions(string argument)
		{
			if (this.postBackOptions == null)
			{
				this.postBackOptions = new PostBackOptions(this);
				this.postBackOptions.ActionUrl = null;
				this.postBackOptions.ValidationGroup = null;
				this.postBackOptions.RequiresJavaScriptProtocol = true;
				this.postBackOptions.ClientSubmit = true;
				this.postBackOptions.PerformValidation = this.CausesValidation && this.Page != null && this.Page.AreValidatorsUplevel(this.ValidationGroup);
				if (this.postBackOptions.PerformValidation)
				{
					this.postBackOptions.ValidationGroup = this.ValidationGroup;
				}
			}
			this.postBackOptions.Argument = argument;
			return this.postBackOptions;
		}

		/// <summary>Renders the list items of a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control as bullets into the specified <see cref="T:System.Web.UI.HtmlTextWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
		// Token: 0x06001D7B RID: 7547 RVA: 0x00049CCC File Offset: 0x00047ECC
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			int num = 0;
			Page page = this.Page;
			ClientScriptManager clientScriptManager = ((page != null) ? page.ClientScript : null);
			foreach (object obj in this.Items)
			{
				ListItem listItem = (ListItem)obj;
				if (page != null)
				{
					clientScriptManager.RegisterForEventValidation(this.UniqueID, listItem.Value);
				}
				if (listItem.HasAttributes)
				{
					listItem.Attributes.AddAttributes(writer);
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Li);
				this.RenderBulletText(listItem, num++, writer);
				writer.RenderEndTag();
			}
		}

		/// <summary>Writes the <see cref="T:System.Web.UI.WebControls.BulletedList" /> control content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object for display on the client.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x06001D7C RID: 7548 RVA: 0x00049D80 File Offset: 0x00047F80
		protected internal override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
		}

		/// <summary>For a description of this method, see <see cref="M:System.Web.UI.IPostBackEventHandler.RaisePostBackEvent(System.String)" />.</summary>
		/// <param name="eventArgument">A string that represents an optional event argument to pass to the event handler. </param>
		// Token: 0x06001D7D RID: 7549 RVA: 0x00049D89 File Offset: 0x00047F89
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		/// <summary>Raises events for the <see cref="T:System.Web.UI.WebControls.BulletedList" /> control when a form is posted back to the server.</summary>
		/// <param name="eventArgument">The string representation for the index of the list item that raised the event.</param>
		// Token: 0x06001D7E RID: 7550 RVA: 0x00049D92 File Offset: 0x00047F92
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			base.ValidateEvent(this.UniqueID, eventArgument);
			if (this.CausesValidation)
			{
				this.Page.Validate(this.ValidationGroup);
			}
			this.OnClick(new BulletedListEventArgs(int.Parse(eventArgument, Helpers.InvariantCulture)));
		}

		/// <summary>Gets or sets the value of the <see cref="P:System.Web.UI.WebControls.ListControl.AutoPostBack" /> property for the base class.</summary>
		/// <returns>false.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to assign a value to the <see cref="P:System.Web.UI.WebControls.BulletedList.AutoPostBack" />. </exception>
		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06001D7F RID: 7551 RVA: 0x00049DD0 File Offset: 0x00047FD0
		// (set) Token: 0x06001D80 RID: 7552 RVA: 0x00049DD8 File Offset: 0x00047FD8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public override bool AutoPostBack
		{
			get
			{
				return base.AutoPostBack;
			}
			set
			{
				throw new NotSupportedException(string.Format("This property is not supported in {0}", base.GetType()));
			}
		}

		/// <summary>Gets or sets the zero-based index of the currently selected item in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
		/// <returns>Always returns -1.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to assign a value to the <see cref="P:System.Web.UI.WebControls.BulletedList.SelectedIndex" />. </exception>
		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06001D81 RID: 7553 RVA: 0x00049DEF File Offset: 0x00047FEF
		// (set) Token: 0x06001D82 RID: 7554 RVA: 0x00049DD8 File Offset: 0x00047FD8
		[Bindable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int SelectedIndex
		{
			get
			{
				return -1;
			}
			set
			{
				throw new NotSupportedException(string.Format("This property is not supported in {0}", base.GetType()));
			}
		}

		/// <summary>Gets the currently selected item in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
		/// <returns>null.</returns>
		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06001D83 RID: 7555 RVA: 0x00003BEA File Offset: 0x00001DEA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ListItem SelectedItem
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets or sets the <see cref="P:System.Web.UI.WebControls.ListItem.Value" /> property of the selected <see cref="T:System.Web.UI.WebControls.ListItem" /> object in the <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
		/// <returns>The <see cref="P:System.Web.UI.WebControls.ListItem.Value" /> of the selected <see cref="T:System.Web.UI.WebControls.ListItem" /> in the <see cref="T:System.Web.UI.WebControls.BulletedList" />; otherwise, an empty string (""), if no item is selected.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to assign a value to the <see cref="P:System.Web.UI.WebControls.BulletedList.SelectedValue" />. </exception>
		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06001D84 RID: 7556 RVA: 0x0000EE9B File Offset: 0x0000D09B
		// (set) Token: 0x06001D85 RID: 7557 RVA: 0x00003A01 File Offset: 0x00001C01
		[Bindable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string SelectedValue
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets or sets the path to an image to display for each bullet in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
		/// <returns>The path to an image to display as each bullet in a <see cref="T:System.Web.UI.WebControls.BulletedList" />.</returns>
		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06001D86 RID: 7558 RVA: 0x00049DF2 File Offset: 0x00047FF2
		// (set) Token: 0x06001D87 RID: 7559 RVA: 0x00049E09 File Offset: 0x00048009
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public virtual string BulletImageUrl
		{
			get
			{
				return this.ViewState.GetString("BulletImageUrl", string.Empty);
			}
			set
			{
				this.ViewState["BulletImageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the bullet style for the <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.BulletStyle" /> values. The default is <see cref="F:System.Web.UI.WebControls.BulletStyle.NotSet" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified type is not one of the <see cref="T:System.Web.UI.WebControls.BulletStyle" /> values. </exception>
		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06001D88 RID: 7560 RVA: 0x00049E1C File Offset: 0x0004801C
		// (set) Token: 0x06001D89 RID: 7561 RVA: 0x00049E2F File Offset: 0x0004802F
		[DefaultValue(BulletStyle.NotSet)]
		public virtual BulletStyle BulletStyle
		{
			get
			{
				return (BulletStyle)this.ViewState.GetInt("BulletStyle", 0);
			}
			set
			{
				if (value < BulletStyle.NotSet || value > BulletStyle.CustomImage)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["BulletStyle"] = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.ControlCollection" /> collection for the control.</summary>
		/// <returns>A control collection for the control.</returns>
		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06001D8A RID: 7562 RVA: 0x00032889 File Offset: 0x00030A89
		public override ControlCollection Controls
		{
			get
			{
				return new EmptyControlCollection(this);
			}
		}

		/// <summary>Gets or sets the display mode of the list content in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.BulletedListDisplayMode" /> values. The default is <see cref="F:System.Web.UI.WebControls.BulletedListDisplayMode.Text" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified type is not one of the <see cref="T:System.Web.UI.WebControls.BulletedListDisplayMode" /> values. </exception>
		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06001D8B RID: 7563 RVA: 0x00049E5B File Offset: 0x0004805B
		// (set) Token: 0x06001D8C RID: 7564 RVA: 0x00049E6E File Offset: 0x0004806E
		[DefaultValue(BulletedListDisplayMode.Text)]
		public virtual BulletedListDisplayMode DisplayMode
		{
			get
			{
				return (BulletedListDisplayMode)this.ViewState.GetInt("DisplayMode", 0);
			}
			set
			{
				if (value < BulletedListDisplayMode.Text || value > BulletedListDisplayMode.LinkButton)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["DisplayMode"] = value;
			}
		}

		/// <summary>Gets or sets the value that starts the numbering of list items in an ordered <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
		/// <returns>The value that starts the numbering of list items in an ordered <see cref="T:System.Web.UI.WebControls.BulletedList" /> control. The default is 1.</returns>
		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06001D8D RID: 7565 RVA: 0x00049E99 File Offset: 0x00048099
		// (set) Token: 0x06001D8E RID: 7566 RVA: 0x00049EAC File Offset: 0x000480AC
		[DefaultValue(1)]
		public virtual int FirstBulletNumber
		{
			get
			{
				return this.ViewState.GetInt("FirstBulletNumber", 1);
			}
			set
			{
				this.ViewState["FirstBulletNumber"] = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value for the specified <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
		/// <returns>The HTML text writer tag value.</returns>
		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06001D8F RID: 7567 RVA: 0x00049EC4 File Offset: 0x000480C4
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				switch (this.BulletStyle)
				{
				case BulletStyle.Numbered:
				case BulletStyle.LowerAlpha:
				case BulletStyle.UpperAlpha:
				case BulletStyle.LowerRoman:
				case BulletStyle.UpperRoman:
					return HtmlTextWriterTag.Ol;
				}
				return HtmlTextWriterTag.Ul;
			}
		}

		/// <summary>Gets or sets the target window or frame in which to display the Web page content that is linked to when a hyperlink in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control is clicked.</summary>
		/// <returns>The target window or frame in which to load the Web page linked to when a hyperlink in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> is clicked. The default is an empty string ("").</returns>
		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06001D90 RID: 7568 RVA: 0x00049F0D File Offset: 0x0004810D
		// (set) Token: 0x06001D91 RID: 7569 RVA: 0x00046F16 File Offset: 0x00045116
		[TypeConverter(typeof(TargetConverter))]
		[DefaultValue("")]
		public virtual string Target
		{
			get
			{
				return this.ViewState.GetString("Target", string.Empty);
			}
			set
			{
				this.ViewState["Target"] = value;
			}
		}

		/// <summary>Gets or sets the text for the <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
		/// <returns>The <see cref="P:System.Web.UI.WebControls.ListControl.SelectedValue" /> of the <see cref="T:System.Web.UI.WebControls.BulletedList" />, if one of the items in the <see cref="T:System.Web.UI.WebControls.BulletedList" /> is selected; otherwise, an empty string ("").</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to assign a value to the <see cref="P:System.Web.UI.WebControls.BulletedList.Text" />. </exception>
		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06001D92 RID: 7570 RVA: 0x0000EE9B File Offset: 0x0000D09B
		// (set) Token: 0x06001D93 RID: 7571 RVA: 0x00003A01 File Offset: 0x00001C01
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string Text
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.BulletedList.Click" /> event for the <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.BulletedListEventArgs" /> that contains the event data. </param>
		// Token: 0x06001D94 RID: 7572 RVA: 0x00049F24 File Offset: 0x00048124
		protected virtual void OnClick(BulletedListEventArgs e)
		{
			if (base.Events != null)
			{
				BulletedListEventHandler bulletedListEventHandler = (BulletedListEventHandler)base.Events[BulletedList.ClickEvent];
				if (bulletedListEventHandler != null)
				{
					bulletedListEventHandler(this, e);
				}
			}
		}

		// Token: 0x06001D96 RID: 7574 RVA: 0x00049F62 File Offset: 0x00048162
		// Note: this type is marked as 'beforefieldinit'.
		static BulletedList()
		{
			BulletedList.ClickEvent = new object();
		}

		/// <summary>Gets or sets a value that indicates whether the control is rendered if the data source has no data or if the control is not data-bound.</summary>
		/// <returns>true if the control is rendered if the data source has no data or if the control is not data-bound; otherwise false.</returns>
		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06001D97 RID: 7575 RVA: 0x00049F70 File Offset: 0x00048170
		// (set) Token: 0x06001D98 RID: 7576 RVA: 0x0000B3E4 File Offset: 0x000095E4
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

		// Token: 0x04001833 RID: 6195
		private PostBackOptions postBackOptions;
	}
}
