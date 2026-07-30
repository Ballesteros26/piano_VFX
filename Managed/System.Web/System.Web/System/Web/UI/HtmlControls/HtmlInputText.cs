using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access to the HTML &lt;input type= text&gt; and &lt;input type= password&gt; elements on the server.</summary>
	// Token: 0x0200026B RID: 619
	[DefaultEvent("ServerChange")]
	[SupportsEventValidation]
	[ValidationProperty("Value")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlInputText : HtmlInputControl, IPostBackDataHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputText" /> class using default values.</summary>
		// Token: 0x06001952 RID: 6482 RVA: 0x00043E0D File Offset: 0x0004200D
		public HtmlInputText()
			: base("text")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputText" /> class using the specified input control type.</summary>
		/// <param name="type">The type of input control. </param>
		// Token: 0x06001953 RID: 6483 RVA: 0x00042CF8 File Offset: 0x00040EF8
		public HtmlInputText(string type)
			: base(type)
		{
		}

		/// <summary>Gets or sets the maximum number of characters that can be entered in the text box.</summary>
		/// <returns>The maximum number of characters that can be entered in the text box.</returns>
		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06001954 RID: 6484 RVA: 0x00043E1C File Offset: 0x0004201C
		// (set) Token: 0x06001955 RID: 6485 RVA: 0x000434E1 File Offset: 0x000416E1
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public int MaxLength
		{
			get
			{
				string text = base.Attributes["maxlength"];
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
					base.Attributes.Remove("maxlength");
					return;
				}
				base.Attributes["maxlength"] = value.ToString();
			}
		}

		/// <summary>Gets or sets the width of the text box.</summary>
		/// <returns>The width, in characters, of the text box.</returns>
		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06001956 RID: 6486 RVA: 0x00043E48 File Offset: 0x00042048
		// (set) Token: 0x06001957 RID: 6487 RVA: 0x00043541 File Offset: 0x00041741
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		[DefaultValue(-1)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Size
		{
			get
			{
				string text = base.Attributes["size"];
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
					base.Attributes.Remove("size");
					return;
				}
				base.Attributes["size"] = value.ToString();
			}
		}

		/// <summary>Gets or sets the contents of the text box.</summary>
		/// <returns>The text contained in the text box. The default is an empty string ("").</returns>
		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06001958 RID: 6488 RVA: 0x00043E74 File Offset: 0x00042074
		// (set) Token: 0x06001959 RID: 6489 RVA: 0x000433FC File Offset: 0x000415FC
		public override string Value
		{
			get
			{
				string text = base.Attributes["value"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("value");
					return;
				}
				base.Attributes["value"] = value;
			}
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x00043E9C File Offset: 0x0004209C
		protected internal override void Render(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.ClientScript.RegisterForEventValidation(this.UniqueID);
			}
			base.Render(writer);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600195B RID: 6491 RVA: 0x00043ECC File Offset: 0x000420CC
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

		/// <summary>Raises the <see cref="E:System.Web.UI.HtmlControls.HtmlInputText.ServerChange" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
		// Token: 0x0600195C RID: 6492 RVA: 0x00043F00 File Offset: 0x00042100
		protected virtual void OnServerChange(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HtmlInputText.serverChangeEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlInputText" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client.</param>
		// Token: 0x0600195D RID: 6493 RVA: 0x00043F2E File Offset: 0x0004212E
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			if (string.Compare(base.Type, 0, "password", 0, 8, true, Helpers.InvariantCulture) == 0)
			{
				base.Attributes.Remove("value");
			}
			base.RenderAttributes(writer);
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x00043F64 File Offset: 0x00042164
		private bool LoadPostDataInternal(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[postDataKey];
			if (this.Value != text)
			{
				this.Value = text;
				return true;
			}
			return false;
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x00043F91 File Offset: 0x00042191
		private void RaisePostDataChangedEventInternal()
		{
			this.OnServerChange(EventArgs.Empty);
		}

		/// <summary>Processes the postback data for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputText" /> control. </summary>
		/// <returns>true if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputText" /> control's state has changed as a result of the postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values.</param>
		// Token: 0x06001960 RID: 6496 RVA: 0x00043F9E File Offset: 0x0004219E
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostDataInternal(postDataKey, postCollection);
		}

		/// <summary>Calls the <see cref="M:System.Web.UI.HtmlControls.HtmlInputText.OnServerChange(System.EventArgs)" /> method to signal the <see cref="T:System.Web.UI.HtmlControls.HtmlInputText" /> control that the state of the control has changed.</summary>
		// Token: 0x06001961 RID: 6497 RVA: 0x00043FA8 File Offset: 0x000421A8
		protected virtual void RaisePostDataChangedEvent()
		{
			base.ValidateEvent(this.UniqueID, string.Empty);
			this.RaisePostDataChangedEventInternal();
		}

		/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackDataHandler.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> interface method by calling the <see cref="M:System.Web.UI.HtmlControls.HtmlInputText.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> method.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputText" /> control's state has changed as a result of the postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values.</param>
		// Token: 0x06001962 RID: 6498 RVA: 0x00043FC1 File Offset: 0x000421C1
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent" /> interface method by calling the <see cref="M:System.Web.UI.HtmlControls.HtmlInputText.RaisePostDataChangedEvent" /> method.</summary>
		// Token: 0x06001963 RID: 6499 RVA: 0x00043FCB File Offset: 0x000421CB
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		/// <summary>Occurs when the <see cref="P:System.Web.UI.HtmlControls.HtmlInputText.Value" /> property is changed on the server.</summary>
		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06001964 RID: 6500 RVA: 0x00043FD3 File Offset: 0x000421D3
		// (remove) Token: 0x06001965 RID: 6501 RVA: 0x00043FE6 File Offset: 0x000421E6
		[WebCategory("Action")]
		[WebSysDescription("")]
		public event EventHandler ServerChange
		{
			add
			{
				base.Events.AddHandler(HtmlInputText.serverChangeEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(HtmlInputText.serverChangeEvent, value);
			}
		}

		// Token: 0x0400163F RID: 5695
		private static readonly object serverChangeEvent = new object();
	}
}
