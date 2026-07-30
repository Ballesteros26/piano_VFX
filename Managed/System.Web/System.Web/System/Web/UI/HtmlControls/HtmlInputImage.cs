using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access to the HTML &lt;input type= image&gt; element on the server.</summary>
	// Token: 0x02000266 RID: 614
	[SupportsEventValidation]
	[DefaultEvent("ServerClick")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlInputImage : HtmlInputControl, IPostBackDataHandler, IPostBackEventHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> class.</summary>
		// Token: 0x06001913 RID: 6419 RVA: 0x000437D4 File Offset: 0x000419D4
		public HtmlInputImage()
			: base("image")
		{
		}

		/// <summary>Gets or sets a value indicating whether validation is performed when the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control is clicked.</summary>
		/// <returns>true if validation is performed when the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control is clicked; otherwise, false. The default value is true.</returns>
		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06001914 RID: 6420 RVA: 0x0004199F File Offset: 0x0003FB9F
		// (set) Token: 0x06001915 RID: 6421 RVA: 0x000419B2 File Offset: 0x0003FBB2
		[WebSysDescription("")]
		[DefaultValue(true)]
		[WebCategory("Behavior")]
		public virtual bool CausesValidation
		{
			get
			{
				return this.ViewState.GetBool("CausesValidation", true);
			}
			set
			{
				this.ViewState["CausesValidation"] = value;
			}
		}

		/// <summary>Gets or sets the alignment of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control in relation to other elements on the Web page.</summary>
		/// <returns>The alignment of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control in relation to other elements on the Web page.</returns>
		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06001916 RID: 6422 RVA: 0x000437E1 File Offset: 0x000419E1
		// (set) Token: 0x06001917 RID: 6423 RVA: 0x000437EE File Offset: 0x000419EE
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		[WebCategory("Appearance")]
		public string Align
		{
			get
			{
				return this.GetAtt("align");
			}
			set
			{
				this.SetAtt("align", value);
			}
		}

		/// <summary>Gets or sets the alternative text that the browser displays if the image is unavailable or has not been downloaded.</summary>
		/// <returns>The alternative text for the specified image. The default value is an empty string ("").</returns>
		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06001918 RID: 6424 RVA: 0x000437FC File Offset: 0x000419FC
		// (set) Token: 0x06001919 RID: 6425 RVA: 0x00043809 File Offset: 0x00041A09
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[Localizable(true)]
		public string Alt
		{
			get
			{
				return this.GetAtt("alt");
			}
			set
			{
				this.SetAtt("alt", value);
			}
		}

		/// <summary>Gets or sets the location of the image file.</summary>
		/// <returns>The location of the image file. The default value is an empty string ("").</returns>
		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x0600191A RID: 6426 RVA: 0x00043817 File Offset: 0x00041A17
		// (set) Token: 0x0600191B RID: 6427 RVA: 0x00043824 File Offset: 0x00041A24
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[UrlProperty]
		[DefaultValue("")]
		[WebCategory("Appearance")]
		public string Src
		{
			get
			{
				return this.GetAtt("src");
			}
			set
			{
				this.SetAtt("src", value);
			}
		}

		/// <summary>Gets or sets the border width for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control.</summary>
		/// <returns>The border width, in pixels, for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control.</returns>
		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x0600191C RID: 6428 RVA: 0x00043834 File Offset: 0x00041A34
		// (set) Token: 0x0600191D RID: 6429 RVA: 0x00043862 File Offset: 0x00041A62
		[WebCategory("Appearance")]
		[DefaultValue("-1")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		public int Border
		{
			get
			{
				string text = base.Attributes["border"];
				if (text == null)
				{
					return -1;
				}
				return int.Parse(text, Helpers.InvariantCulture);
			}
			set
			{
				if (value == -1)
				{
					base.Attributes.Remove("border");
					return;
				}
				base.Attributes["border"] = value.ToString(Helpers.InvariantCulture);
			}
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x00043898 File Offset: 0x00041A98
		private bool LoadPostDataInternal(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[this.UniqueID + ".x"];
			string text2 = postCollection[this.UniqueID + ".y"];
			if (text != null && text.Length != 0 && text2 != null && text2.Length != 0)
			{
				this.clicked_x = int.Parse(text, Helpers.InvariantCulture);
				this.clicked_y = int.Parse(text2, Helpers.InvariantCulture);
				this.Page.RegisterRequiresRaiseEvent(this);
				return true;
			}
			return false;
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x0004391A File Offset: 0x00041B1A
		private void RaisePostBackEventInternal(string eventArgument)
		{
			if (this.CausesValidation)
			{
				this.Page.Validate(this.ValidationGroup);
			}
			this.OnServerClick(new ImageClickEventArgs(this.clicked_x, this.clicked_y));
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x0000393A File Offset: 0x00001B3A
		private void RaisePostDataChangedEventInternal()
		{
		}

		/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control causes validation when it posts back to the server.</summary>
		/// <returns>The group of controls for which the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control causes validation when it posts back to the server. The default value is an empty string (""), indicating that this property is not set. </returns>
		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06001921 RID: 6433 RVA: 0x00041BB3 File Offset: 0x0003FDB3
		// (set) Token: 0x06001922 RID: 6434 RVA: 0x000419E1 File Offset: 0x0003FBE1
		[DefaultValue("")]
		public virtual string ValidationGroup
		{
			get
			{
				return this.ViewState.GetString("ValidationGroup", "");
			}
			set
			{
				this.ViewState["ValidationGroup"] = value;
			}
		}

		/// <summary>Processes the postback data for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control's state has changed as a result of the postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values.</param>
		// Token: 0x06001923 RID: 6435 RVA: 0x0004394C File Offset: 0x00041B4C
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostDataInternal(postDataKey, postCollection);
		}

		/// <summary>Raises events for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control when it posts back to the server.</summary>
		/// <param name="eventArgument">The argument for the event.</param>
		// Token: 0x06001924 RID: 6436 RVA: 0x00043956 File Offset: 0x00041B56
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEventInternal(eventArgument);
		}

		/// <summary>Notifies the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control that the state of the control has changed.</summary>
		// Token: 0x06001925 RID: 6437 RVA: 0x0004395F File Offset: 0x00041B5F
		protected virtual void RaisePostDataChangedEvent()
		{
			base.ValidateEvent(this.UniqueID, string.Empty);
			this.RaisePostDataChangedEventInternal();
		}

		/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackDataHandler.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> interface method by calling the <see cref="M:System.Web.UI.HtmlControls.HtmlInputImage.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> method.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control's state has changed as a result of the postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values.</param>
		// Token: 0x06001926 RID: 6438 RVA: 0x00043978 File Offset: 0x00041B78
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent" /> interface method by calling the <see cref="M:System.Web.UI.HtmlControls.HtmlInputImage.RaisePostDataChangedEvent" /> method.</summary>
		// Token: 0x06001927 RID: 6439 RVA: 0x00043982 File Offset: 0x00041B82
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		/// <summary>Enables the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control to raise events on postback.</summary>
		/// <param name="eventArgument">The argument for the event.</param>
		// Token: 0x06001928 RID: 6440 RVA: 0x0004398A File Offset: 0x00041B8A
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001929 RID: 6441 RVA: 0x00043994 File Offset: 0x00041B94
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

		/// <summary>Raises the <see cref="E:System.Web.UI.HtmlControls.HtmlInputImage.ServerClick" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Web.UI.ImageClickEventArgs" /> that contains event data. </param>
		// Token: 0x0600192A RID: 6442 RVA: 0x000439C8 File Offset: 0x00041BC8
		protected virtual void OnServerClick(ImageClickEventArgs e)
		{
			ImageClickEventHandler imageClickEventHandler = base.Events[HtmlInputImage.ServerClickEvent] as ImageClickEventHandler;
			if (imageClickEventHandler != null)
			{
				imageClickEventHandler(this, e);
			}
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client.</param>
		/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.UI.HtmlControls.HtmlInputImage.Src" /> property contains a malformed URL.</exception>
		// Token: 0x0600192B RID: 6443 RVA: 0x000439F8 File Offset: 0x00041BF8
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.ClientScript.RegisterForEventValidation(this.UniqueID);
			}
			if (this.CausesValidation && page != null && page.AreValidatorsUplevel(this.ValidationGroup))
			{
				ClientScriptManager clientScript = page.ClientScript;
				AttributeCollection attributes = base.Attributes;
				attributes["onclick"] = attributes["onclick"] + clientScript.GetClientValidationEvent(this.ValidationGroup);
			}
			base.PreProcessRelativeReference(writer, "src");
			base.RenderAttributes(writer);
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x00043A81 File Offset: 0x00041C81
		private void SetAtt(string name, string value)
		{
			if (value == null || value.Length == 0)
			{
				base.Attributes.Remove(name);
				return;
			}
			base.Attributes[name] = value;
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x00043AA8 File Offset: 0x00041CA8
		private string GetAtt(string name)
		{
			string text = base.Attributes[name];
			if (text == null)
			{
				return string.Empty;
			}
			return text;
		}

		/// <summary>Occurs on the server when the user clicks an <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control.</summary>
		// Token: 0x1400003B RID: 59
		// (add) Token: 0x0600192E RID: 6446 RVA: 0x00043ACC File Offset: 0x00041CCC
		// (remove) Token: 0x0600192F RID: 6447 RVA: 0x00043ACC File Offset: 0x00041CCC
		[WebCategory("Action")]
		[WebSysDescription("")]
		public event ImageClickEventHandler ServerClick
		{
			add
			{
				base.Events.AddHandler(HtmlInputImage.ServerClickEvent, value);
			}
			remove
			{
				base.Events.AddHandler(HtmlInputImage.ServerClickEvent, value);
			}
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x00043ADF File Offset: 0x00041CDF
		// Note: this type is marked as 'beforefieldinit'.
		static HtmlInputImage()
		{
			HtmlInputImage.ServerClickEvent = new object();
		}

		// Token: 0x0400163B RID: 5691
		private int clicked_x;

		// Token: 0x0400163C RID: 5692
		private int clicked_y;
	}
}
