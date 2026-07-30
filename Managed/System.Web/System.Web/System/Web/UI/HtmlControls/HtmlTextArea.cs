using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access to the &lt;textarea&gt; HTML element on the server.</summary>
	// Token: 0x02000278 RID: 632
	[SupportsEventValidation]
	[DefaultEvent("ServerChange")]
	[ValidationProperty("Value")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlTextArea : HtmlContainerControl, IPostBackDataHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> class.</summary>
		// Token: 0x06001A1B RID: 6683 RVA: 0x000456F3 File Offset: 0x000438F3
		public HtmlTextArea()
			: base("textarea")
		{
		}

		/// <summary>Gets or sets the width (in characters) of the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control.</summary>
		/// <returns>The width (in characters) of the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control. The default value is -1, which indicates that this property is not set.</returns>
		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06001A1C RID: 6684 RVA: 0x00045700 File Offset: 0x00043900
		// (set) Token: 0x06001A1D RID: 6685 RVA: 0x00045729 File Offset: 0x00043929
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		public int Cols
		{
			get
			{
				string text = base.Attributes["cols"];
				if (text != null)
				{
					return Convert.ToInt32(text);
				}
				return -1;
			}
			set
			{
				if (value == -1)
				{
					base.Attributes.Remove("cols");
					return;
				}
				base.Attributes["cols"] = value.ToString(Helpers.InvariantCulture);
			}
		}

		/// <summary>Gets or sets the unique identifier name for the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control.</summary>
		/// <returns>A string that represents the value of the <see cref="P:System.Web.UI.Control.UniqueID" /> property.</returns>
		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06001A1E RID: 6686 RVA: 0x00042187 File Offset: 0x00040387
		// (set) Token: 0x06001A1F RID: 6687 RVA: 0x0000393A File Offset: 0x00001B3A
		[WebCategory("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[DefaultValue("")]
		public virtual string Name
		{
			get
			{
				return this.UniqueID;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the height (in characters) of the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control.</summary>
		/// <returns>The height (in characters) of the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control. The default value is -1, which indicates that this property is not set.</returns>
		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06001A20 RID: 6688 RVA: 0x0004575C File Offset: 0x0004395C
		// (set) Token: 0x06001A21 RID: 6689 RVA: 0x00045785 File Offset: 0x00043985
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		public int Rows
		{
			get
			{
				string text = base.Attributes["rows"];
				if (text != null)
				{
					return Convert.ToInt32(text);
				}
				return -1;
			}
			set
			{
				if (value == -1)
				{
					base.Attributes.Remove("rows");
					return;
				}
				base.Attributes["rows"] = value.ToString(Helpers.InvariantCulture);
			}
		}

		/// <summary>Gets or sets the text entered in the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control.</summary>
		/// <returns>The text entered in the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06001A22 RID: 6690 RVA: 0x000457B8 File Offset: 0x000439B8
		// (set) Token: 0x06001A23 RID: 6691 RVA: 0x000457C0 File Offset: 0x000439C0
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		[WebCategory("Appearance")]
		public string Value
		{
			get
			{
				return this.InnerText;
			}
			set
			{
				this.InnerText = value;
			}
		}

		/// <summary>Notifies the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control that an object was parsed and adds the object to the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control's <see cref="T:System.Web.UI.ControlCollection" /> object. </summary>
		/// <param name="obj">An <see cref="T:System.Object" /> that represents the parsed element. </param>
		/// <exception cref="T:System.Web.HttpException">The object specified by the <paramref name="obj" /> parameter can only be of the type <see cref="T:System.Web.UI.LiteralControl" /> or <see cref="T:System.Web.UI.DataBoundLiteralControl" />.</exception>
		// Token: 0x06001A24 RID: 6692 RVA: 0x000457C9 File Offset: 0x000439C9
		protected override void AddParsedSubObject(object obj)
		{
			if (!(obj is LiteralControl) && !(obj is DataBoundLiteralControl))
			{
				throw new HttpException(global::Locale.GetText("Wrong type."));
			}
			base.AddParsedSubObject(obj);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x06001A25 RID: 6693 RVA: 0x000457F4 File Offset: 0x000439F4
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			Page page = this.Page;
			if (page != null && !base.Disabled)
			{
				page.RegisterRequiresPostBack(this);
				page.RegisterEnabledControl(this);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.HtmlControls.HtmlTextArea.ServerChange" /> event of the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control. This allows you to provide a custom handler for the event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001A26 RID: 6694 RVA: 0x00045828 File Offset: 0x00043A28
		protected virtual void OnServerChange(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HtmlTextArea.serverChangeEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
		// Token: 0x06001A27 RID: 6695 RVA: 0x00045858 File Offset: 0x00043A58
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.ClientScript.RegisterForEventValidation(this.UniqueID);
			}
			if (base.Attributes["name"] == null)
			{
				writer.WriteAttribute("name", this.Name);
			}
			base.RenderAttributes(writer);
		}

		/// <summary>Processes the postback data for the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control's state has changed as a result of the postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values.</param>
		// Token: 0x06001A28 RID: 6696 RVA: 0x000458AA File Offset: 0x00043AAA
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.DefaultLoadPostData(postDataKey, postCollection);
		}

		/// <summary>Calls the <see cref="M:System.Web.UI.HtmlControls.HtmlTextArea.OnServerChange(System.EventArgs)" /> method to signal the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control that the state of the control has changed.</summary>
		// Token: 0x06001A29 RID: 6697 RVA: 0x000458B4 File Offset: 0x00043AB4
		protected virtual void RaisePostDataChangedEvent()
		{
			base.ValidateEvent(this.UniqueID, string.Empty);
			this.OnServerChange(EventArgs.Empty);
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x000458D4 File Offset: 0x00043AD4
		internal bool DefaultLoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[postDataKey];
			if (text != null && text != this.Value)
			{
				this.Value = text;
				return true;
			}
			return false;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackDataHandler.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" />. </summary>
		/// <returns>true if the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control's state has changed as a result of postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values.</param>
		// Token: 0x06001A2B RID: 6699 RVA: 0x00045904 File Offset: 0x00043B04
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent" />.</summary>
		// Token: 0x06001A2C RID: 6700 RVA: 0x0004590E File Offset: 0x00043B0E
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		/// <summary>Occurs when the content of the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control changes between posts to the server.</summary>
		// Token: 0x14000040 RID: 64
		// (add) Token: 0x06001A2D RID: 6701 RVA: 0x00045916 File Offset: 0x00043B16
		// (remove) Token: 0x06001A2E RID: 6702 RVA: 0x00045929 File Offset: 0x00043B29
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event EventHandler ServerChange
		{
			add
			{
				base.Events.AddHandler(HtmlTextArea.serverChangeEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(HtmlTextArea.serverChangeEvent, value);
			}
		}

		// Token: 0x0400164B RID: 5707
		private static readonly object serverChangeEvent = new object();
	}
}
