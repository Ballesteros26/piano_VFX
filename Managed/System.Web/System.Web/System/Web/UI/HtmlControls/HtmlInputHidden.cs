using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access to the HTML &lt;input type=hidden&gt; element on the server.</summary>
	// Token: 0x02000265 RID: 613
	[DefaultEvent("ServerChange")]
	[SupportsEventValidation]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlInputHidden : HtmlInputControl, IPostBackDataHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputHidden" /> class.</summary>
		// Token: 0x06001906 RID: 6406 RVA: 0x00043693 File Offset: 0x00041893
		public HtmlInputHidden()
			: base("hidden")
		{
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x000436A0 File Offset: 0x000418A0
		private bool LoadPostDataInternal(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[postDataKey];
			if (text != null && text != this.Value)
			{
				base.ValidateEvent(postDataKey, string.Empty);
				this.Value = text;
				return true;
			}
			return false;
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x000436DC File Offset: 0x000418DC
		private void RaisePostDataChangedEventInternal()
		{
			this.OnServerChange(EventArgs.Empty);
		}

		/// <summary>Processes the postback data for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputHidden" /> control.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputHidden" /> control's state has changed as a result of the postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values.</param>
		// Token: 0x06001909 RID: 6409 RVA: 0x000436E9 File Offset: 0x000418E9
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostDataInternal(postDataKey, postCollection);
		}

		/// <summary>Calls the <see cref="M:System.Web.UI.HtmlControls.HtmlInputHidden.OnServerChange(System.EventArgs)" /> method to signal the <see cref="T:System.Web.UI.HtmlControls.HtmlInputHidden" /> control that the state of the control has changed.</summary>
		// Token: 0x0600190A RID: 6410 RVA: 0x000436F3 File Offset: 0x000418F3
		protected virtual void RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEventInternal();
		}

		/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackDataHandler.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> interface method by calling the <see cref="M:System.Web.UI.HtmlControls.HtmlInputHidden.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> method.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputHidden" /> control's state has changed as a result of the postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values.</param>
		// Token: 0x0600190B RID: 6411 RVA: 0x000436FB File Offset: 0x000418FB
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent" /> interface method by calling the <see cref="M:System.Web.UI.HtmlControls.HtmlInputHidden.RaisePostDataChangedEvent" /> method.</summary>
		// Token: 0x0600190C RID: 6412 RVA: 0x00043705 File Offset: 0x00041905
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlInputHidden" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client.</param>
		// Token: 0x0600190D RID: 6413 RVA: 0x00043710 File Offset: 0x00041910
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.ClientScript.RegisterForEventValidation(this.Name);
			}
			base.RenderAttributes(writer);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data.</param>
		// Token: 0x0600190E RID: 6414 RVA: 0x00043740 File Offset: 0x00041940
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

		/// <summary>Raises the <see cref="E:System.Web.UI.HtmlControls.HtmlInputHidden.ServerChange" /> event. This method allows you to handle the event directly.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains event data. </param>
		// Token: 0x0600190F RID: 6415 RVA: 0x00043774 File Offset: 0x00041974
		protected virtual void OnServerChange(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HtmlInputHidden.ServerChangeEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Web.UI.HtmlControls.HtmlInputControl.Value" /> property is changed on the server.</summary>
		// Token: 0x1400003A RID: 58
		// (add) Token: 0x06001910 RID: 6416 RVA: 0x000437A2 File Offset: 0x000419A2
		// (remove) Token: 0x06001911 RID: 6417 RVA: 0x000437B5 File Offset: 0x000419B5
		[WebCategory("Action")]
		[WebSysDescription("")]
		public event EventHandler ServerChange
		{
			add
			{
				base.Events.AddHandler(HtmlInputHidden.ServerChangeEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(HtmlInputHidden.ServerChangeEvent, value);
			}
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x000437C8 File Offset: 0x000419C8
		// Note: this type is marked as 'beforefieldinit'.
		static HtmlInputHidden()
		{
			HtmlInputHidden.ServerChangeEvent = new object();
		}
	}
}
