using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access to the HTML &lt;input type= checkbox&gt; element on the server.</summary>
	// Token: 0x02000262 RID: 610
	[SupportsEventValidation]
	[DefaultEvent("ServerChange")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlInputCheckBox : HtmlInputControl, IPostBackDataHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputCheckBox" /> class.</summary>
		// Token: 0x060018DE RID: 6366 RVA: 0x0004320B File Offset: 0x0004140B
		public HtmlInputCheckBox()
			: base("checkbox")
		{
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.HtmlControls.HtmlInputCheckBox" /> is selected.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputCheckBox" /> control is selected; otherwise, false.</returns>
		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x060018DF RID: 6367 RVA: 0x00043218 File Offset: 0x00041418
		// (set) Token: 0x060018E0 RID: 6368 RVA: 0x0004322F File Offset: 0x0004142F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[TypeConverter(typeof(MinimizableAttributeTypeConverter))]
		[WebCategory("Misc")]
		[WebSysDescription("")]
		[DefaultValue("")]
		public bool Checked
		{
			get
			{
				return base.Attributes["checked"] != null;
			}
			set
			{
				if (!value)
				{
					base.Attributes.Remove("checked");
					return;
				}
				base.Attributes["checked"] = "checked";
			}
		}

		/// <summary>Occurs when the Web page is submitted to the server and the <see cref="T:System.Web.UI.HtmlControls.HtmlInputCheckBox" /> control changes state from the previous post.</summary>
		// Token: 0x14000039 RID: 57
		// (add) Token: 0x060018E1 RID: 6369 RVA: 0x0004325A File Offset: 0x0004145A
		// (remove) Token: 0x060018E2 RID: 6370 RVA: 0x0004326D File Offset: 0x0004146D
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event EventHandler ServerChange
		{
			add
			{
				base.Events.AddHandler(HtmlInputCheckBox.EventServerChange, value);
			}
			remove
			{
				base.Events.RemoveHandler(HtmlInputCheckBox.EventServerChange, value);
			}
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlInputCheckBox" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> instance.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> instance that contains the output stream to render on the client.</param>
		// Token: 0x060018E3 RID: 6371 RVA: 0x00043280 File Offset: 0x00041480
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.ClientScript.RegisterForEventValidation(this.UniqueID);
			}
			base.RenderAttributes(writer);
		}

		/// <summary>Raises the <see cref="M:System.Web.UI.Control.OnPreRender(System.EventArgs)" /> event and registers the control as one that requires postback handling.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
		// Token: 0x060018E4 RID: 6372 RVA: 0x000432B0 File Offset: 0x000414B0
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

		/// <summary>Raises the <see cref="E:System.Web.UI.HtmlControls.HtmlInputCheckBox.ServerChange" /> event. This method allows you to handle the event directly.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains event information. </param>
		// Token: 0x060018E5 RID: 6373 RVA: 0x000432E4 File Offset: 0x000414E4
		protected virtual void OnServerChange(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HtmlInputCheckBox.EventServerChange];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x00043314 File Offset: 0x00041514
		private bool LoadPostDataInternal(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[postDataKey];
			bool flag = text != null && text.Length > 0;
			if (this.Checked != flag)
			{
				this.Checked = flag;
				return true;
			}
			return false;
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x0004334C File Offset: 0x0004154C
		private void RaisePostDataChangedEventInternal()
		{
			this.OnServerChange(EventArgs.Empty);
		}

		/// <summary>Processes the postback data for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputCheckBox" /> control. </summary>
		/// <returns>true if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputCheckBox" /> control's state has changed as a result of the postback event; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values.</param>
		// Token: 0x060018E8 RID: 6376 RVA: 0x00043359 File Offset: 0x00041559
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostDataInternal(postDataKey, postCollection);
		}

		/// <summary>Calls the <see cref="M:System.Web.UI.HtmlControls.HtmlInputCheckBox.OnServerChange(System.EventArgs)" /> method to signal the <see cref="T:System.Web.UI.HtmlControls.HtmlInputCheckBox" /> control that the state of the control has changed.</summary>
		// Token: 0x060018E9 RID: 6377 RVA: 0x00043363 File Offset: 0x00041563
		protected virtual void RaisePostDataChangedEvent()
		{
			base.ValidateEvent(this.UniqueID, string.Empty);
			this.RaisePostDataChangedEventInternal();
		}

		/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackDataHandler.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> method by calling the <see cref="M:System.Web.UI.HtmlControls.HtmlInputCheckBox.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> method.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputCheckBox" /> control's state has changed as a result of the postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values.</param>
		// Token: 0x060018EA RID: 6378 RVA: 0x0004337C File Offset: 0x0004157C
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent" /> method by calling the <see cref="M:System.Web.UI.HtmlControls.HtmlInputCheckBox.RaisePostDataChangedEvent" /> method.</summary>
		// Token: 0x060018EB RID: 6379 RVA: 0x00043386 File Offset: 0x00041586
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x04001637 RID: 5687
		private static readonly object EventServerChange = new object();
	}
}
