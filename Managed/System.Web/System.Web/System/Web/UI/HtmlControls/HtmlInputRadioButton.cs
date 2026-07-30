using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access to the HTML &lt;input type= radio&gt; element on the server.</summary>
	// Token: 0x02000268 RID: 616
	[SupportsEventValidation]
	[DefaultEvent("ServerChange")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlInputRadioButton : HtmlInputControl, IPostBackDataHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> class.</summary>
		// Token: 0x06001935 RID: 6453 RVA: 0x00043B73 File Offset: 0x00041D73
		public HtmlInputRadioButton()
			: base("radio")
		{
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> control is selected.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> control is selected; otherwise, false.</returns>
		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06001936 RID: 6454 RVA: 0x00043B80 File Offset: 0x00041D80
		// (set) Token: 0x06001937 RID: 6455 RVA: 0x00043B9C File Offset: 0x00041D9C
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Checked
		{
			get
			{
				return base.Attributes["checked"] == "checked";
			}
			set
			{
				if (value)
				{
					base.Attributes["checked"] = "checked";
					return;
				}
				base.Attributes.Remove("checked");
			}
		}

		/// <summary>Gets or sets the name of the group that the instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> class is associated with.</summary>
		/// <returns>The group of check box controls that the instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> class is a member of.</returns>
		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06001938 RID: 6456 RVA: 0x00043BC8 File Offset: 0x00041DC8
		// (set) Token: 0x06001939 RID: 6457 RVA: 0x00043BF0 File Offset: 0x00041DF0
		public override string Name
		{
			get
			{
				string text = base.Attributes["name"];
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
					base.Attributes.Remove("name");
					return;
				}
				base.Attributes["name"] = value;
			}
		}

		/// <summary>Gets or sets the value associated with the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> control.</summary>
		/// <returns>The value associated with the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> control.</returns>
		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x0600193A RID: 6458 RVA: 0x00043C18 File Offset: 0x00041E18
		// (set) Token: 0x0600193B RID: 6459 RVA: 0x000433FC File Offset: 0x000415FC
		public override string Value
		{
			get
			{
				string text = base.Attributes["value"];
				if (text == null || text.Length == 0)
				{
					text = this.ID;
					if (text != null && text.Length == 0)
					{
						text = null;
					}
				}
				return text;
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

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event and registers the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> control as one that requires postback handling.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600193C RID: 6460 RVA: 0x00043C58 File Offset: 0x00041E58
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

		/// <summary>Raises the <see cref="E:System.Web.UI.HtmlControls.HtmlInputRadioButton.ServerChange" /> event. This allows you to create a custom event handler when the event is raised.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600193D RID: 6461 RVA: 0x00043C8C File Offset: 0x00041E8C
		protected virtual void OnServerChange(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HtmlInputRadioButton.serverChangeEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> control attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object. </summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered output.</param>
		// Token: 0x0600193E RID: 6462 RVA: 0x00043CBC File Offset: 0x00041EBC
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.ClientScript.RegisterForEventValidation(this.UniqueID, this.Value);
			}
			writer.WriteAttribute("value", this.Value, true);
			base.Attributes.Remove("value");
			base.RenderAttributes(writer);
		}

		/// <summary>Processes the postback data for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> control.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> control's state has changed as a result of the postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values.</param>
		// Token: 0x0600193F RID: 6463 RVA: 0x00043D14 File Offset: 0x00041F14
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			bool flag = postCollection[this.Name] == this.Value;
			if (this.Checked == flag)
			{
				return false;
			}
			base.ValidateEvent(this.UniqueID, this.Value);
			this.Checked = flag;
			return flag;
		}

		/// <summary>Calls the <see cref="M:System.Web.UI.HtmlControls.HtmlInputRadioButton.OnServerChange(System.EventArgs)" /> method to signal the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> control that the state of the control has changed.</summary>
		// Token: 0x06001940 RID: 6464 RVA: 0x00043D5E File Offset: 0x00041F5E
		protected virtual void RaisePostDataChangedEvent()
		{
			this.OnServerChange(EventArgs.Empty);
		}

		/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackDataHandler.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> method by calling the <see cref="M:System.Web.UI.HtmlControls.HtmlInputRadioButton.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> method.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> control's state has changed as a result of the postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values</param>
		// Token: 0x06001941 RID: 6465 RVA: 0x00043D6B File Offset: 0x00041F6B
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent" /> method by calling the <see cref="M:System.Web.UI.HtmlControls.HtmlInputRadioButton.RaisePostDataChangedEvent" /> method.</summary>
		// Token: 0x06001942 RID: 6466 RVA: 0x00043D75 File Offset: 0x00041F75
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Web.UI.HtmlControls.HtmlInputRadioButton.Checked" /> property of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> control changes between posts to the server.</summary>
		// Token: 0x1400003C RID: 60
		// (add) Token: 0x06001943 RID: 6467 RVA: 0x00043D7D File Offset: 0x00041F7D
		// (remove) Token: 0x06001944 RID: 6468 RVA: 0x00043D90 File Offset: 0x00041F90
		[WebCategory("Action")]
		[WebSysDescription("")]
		public event EventHandler ServerChange
		{
			add
			{
				base.Events.AddHandler(HtmlInputRadioButton.serverChangeEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(HtmlInputRadioButton.serverChangeEvent, value);
			}
		}

		// Token: 0x0400163D RID: 5693
		private static readonly object serverChangeEvent = new object();
	}
}
