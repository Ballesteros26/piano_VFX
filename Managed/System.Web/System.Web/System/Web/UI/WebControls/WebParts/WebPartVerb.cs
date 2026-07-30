using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Provides an interactive user interface (UI) element that enables users to perform actions on a Web Parts page.</summary>
	// Token: 0x0200048E RID: 1166
	[TypeConverter("System.Web.UI.WebControls.WebParts.WebPartVerbConverter, System.Web")]
	public class WebPartVerb : IStateManager
	{
		/// <summary>Gets a string containing a unique ID for a verb.</summary>
		/// <returns>A string containing the ID for a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" />.</returns>
		// Token: 0x170010AB RID: 4267
		// (get) Token: 0x060034E2 RID: 13538 RVA: 0x0008B2E2 File Offset: 0x000894E2
		public string ID
		{
			get
			{
				return this.id;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> class and associates a client-side click event handler with the instance.</summary>
		/// <param name="id">A <see cref="T:System.String" /> that is the unique identifier for a verb.</param>
		/// <param name="clientClickHandler">A <see cref="T:System.String" /> that refers to the client-side handler for click events.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="clientClickHandler" /> parameter is null.</exception>
		// Token: 0x060034E3 RID: 13539 RVA: 0x0008B2EC File Offset: 0x000894EC
		public WebPartVerb(string id, string clientClickHandler)
		{
			this.id = id;
			this.clientClickHandler = clientClickHandler;
			this.stateBag = new StateBag();
			this.stateBag.Add("clientClickHandler", clientClickHandler);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> class and associates a server-side click event handler with the instance.</summary>
		/// <param name="id">A <see cref="T:System.String" /> that is the unique identifier for a verb.</param>
		/// <param name="serverClickHandler">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartEventHandler" /> that handles click events on the server.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="serverClickHandler" /> parameter is null.</exception>
		// Token: 0x060034E4 RID: 13540 RVA: 0x0008B35C File Offset: 0x0008955C
		public WebPartVerb(string id, WebPartEventHandler serverClickHandler)
		{
			this.id = id;
			this.serverClickHandler = serverClickHandler;
			this.stateBag = new StateBag();
			this.stateBag.Add("serverClickHandler", serverClickHandler);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> class and associates both client and server-side click event handlers with the instance.</summary>
		/// <param name="id">A <see cref="T:System.String" /> that is the unique identifier for a verb.</param>
		/// <param name="serverClickHandler">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartEventHandler" /> that handles click events on the server.</param>
		/// <param name="clientClickHandler">A <see cref="T:System.String" /> that refers to the client-side handler for click events.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="clientClickHandler" /> parameter is null.- or -The <paramref name="serverClickHandler" /> parameter is null.</exception>
		// Token: 0x060034E5 RID: 13541 RVA: 0x0008B3CC File Offset: 0x000895CC
		public WebPartVerb(string id, WebPartEventHandler serverClickHandler, string clientClickHandler)
		{
			this.id = id;
			this.serverClickHandler = serverClickHandler;
			this.clientClickHandler = clientClickHandler;
			this.stateBag = new StateBag();
			this.stateBag.Add("serverClickHandler", serverClickHandler);
			this.stateBag.Add("clientClickHandler", clientClickHandler);
		}

		/// <summary>Restores view-state information from a previous page request that was saved by the <see cref="M:System.Web.UI.WebControls.WebParts.WebPartVerb.SaveViewState" /> method.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that contains the view state to be restored.</param>
		// Token: 0x060034E6 RID: 13542 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		protected virtual void LoadViewState(object savedState)
		{
			throw new NotImplementedException();
		}

		/// <summary>Saves a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> object's view-state changes that occurred since the page was last posted back to the server.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the state data to be saved.</returns>
		// Token: 0x060034E7 RID: 13543 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		protected virtual object SaveViewState()
		{
			throw new NotImplementedException();
		}

		/// <summary>Tracks view-state changes to a verb so the changes can be stored in the verb's <see cref="T:System.Web.UI.StateBag" /> object.</summary>
		// Token: 0x060034E8 RID: 13544 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		protected virtual void TrackViewState()
		{
			throw new NotImplementedException();
		}

		/// <summary>Implements the <see cref="M:System.Web.UI.IStateManager.LoadViewState(System.Object)" /> method of the <see cref="T:System.Web.UI.IStateManager" /> interface by calling the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> class's own <see cref="M:System.Web.UI.WebControls.WebParts.WebPartVerb.LoadViewState(System.Object)" /> method.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that contains the view state to be restored. </param>
		// Token: 0x060034E9 RID: 13545 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		void IStateManager.LoadViewState(object savedState)
		{
			throw new NotImplementedException();
		}

		/// <summary>Implements the <see cref="M:System.Web.UI.IStateManager.SaveViewState" /> method by calling the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> class's own <see cref="M:System.Web.UI.WebControls.WebParts.WebPartVerb.SaveViewState" /> method.</summary>
		/// <returns>Returns an <see cref="T:System.Object" /> containing the control's current view state. If no view state is associated with the control, this method returns null.</returns>
		// Token: 0x060034EA RID: 13546 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		object IStateManager.SaveViewState()
		{
			throw new NotImplementedException();
		}

		/// <summary>Implements the <see cref="M:System.Web.UI.IStateManager.TrackViewState" /> method by calling the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> class's own <see cref="M:System.Web.UI.WebControls.WebParts.WebPartVerb.TrackViewState" /> method.</summary>
		// Token: 0x060034EB RID: 13547 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		void IStateManager.TrackViewState()
		{
			throw new NotImplementedException();
		}

		/// <summary>Implements the <see cref="P:System.Web.UI.IStateManager.IsTrackingViewState" /> property by calling the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> class's own <see cref="P:System.Web.UI.WebControls.WebParts.WebPartVerb.IsTrackingViewState" /> property.</summary>
		/// <returns>true if view state is being tracked for a verb; otherwise, false.</returns>
		// Token: 0x170010AC RID: 4268
		// (get) Token: 0x060034EC RID: 13548 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value indicating that some state associated with a custom verb is currently active or selected.</summary>
		/// <returns>true if a state associated with a custom verb is currently active; otherwise, false. The default is false.</returns>
		// Token: 0x170010AD RID: 4269
		// (get) Token: 0x060034ED RID: 13549 RVA: 0x0008B452 File Offset: 0x00089652
		// (set) Token: 0x060034EE RID: 13550 RVA: 0x0008B45A File Offset: 0x0008965A
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[WebSysDescription("Denotes verb is checked or not.")]
		public virtual bool Checked
		{
			get
			{
				return this.isChecked;
			}
			set
			{
				this.isChecked = value;
			}
		}

		/// <summary>Gets the string containing the method name of the client-side event handler defined in the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> constructor.</summary>
		/// <returns>A string that contains the name of the method that handles client-side click events. The default is an empty string ("").</returns>
		// Token: 0x170010AE RID: 4270
		// (get) Token: 0x060034EF RID: 13551 RVA: 0x0008B463 File Offset: 0x00089663
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public string ClientClickHandler
		{
			get
			{
				return this.clientClickHandler;
			}
		}

		/// <summary>Gets or sets a short description of the verb.</summary>
		/// <returns>A string containing a description of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" />. </returns>
		// Token: 0x170010AF RID: 4271
		// (get) Token: 0x060034F0 RID: 13552 RVA: 0x0008B46B File Offset: 0x0008966B
		// (set) Token: 0x060034F1 RID: 13553 RVA: 0x0008B473 File Offset: 0x00089673
		[Localizable(true)]
		[WebSysDescription("Gives descriptive information about the verb")]
		[NotifyParentProperty(true)]
		public virtual string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether a verb is enabled.</summary>
		/// <returns>true if the verb is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x170010B0 RID: 4272
		// (get) Token: 0x060034F2 RID: 13554 RVA: 0x0008B47C File Offset: 0x0008967C
		// (set) Token: 0x060034F3 RID: 13555 RVA: 0x0008B484 File Offset: 0x00089684
		[WebSysDescription("Determines whether verb is enabled.")]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public virtual bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		/// <summary>Gets or sets a string containing a URL to an image that represents a verb in the user interface (UI).</summary>
		/// <returns>A string that contains the URL to an image. The default is an empty string ("").</returns>
		// Token: 0x170010B1 RID: 4273
		// (get) Token: 0x060034F4 RID: 13556 RVA: 0x0008B48D File Offset: 0x0008968D
		// (set) Token: 0x060034F5 RID: 13557 RVA: 0x0008B495 File Offset: 0x00089695
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[WebSysDescription("Denotes URL of the image to be displayed for the verb")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design", "UITypeEditor, System.Drawing")]
		public string ImageUrl
		{
			get
			{
				return this.imageUrl;
			}
			set
			{
				this.imageUrl = value;
			}
		}

		/// <summary>Gets a value that indicates whether view state is currently being tracked for a verb.</summary>
		/// <returns>true if view state is being tracked; otherwise, false.</returns>
		// Token: 0x170010B2 RID: 4274
		// (get) Token: 0x060034F6 RID: 13558 RVA: 0x00003A1F File Offset: 0x00001C1F
		protected virtual bool IsTrackingViewState
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a reference to the method that handles server-side click events for the verb.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartEventHandler" /> that handles server-side click events.</returns>
		// Token: 0x170010B3 RID: 4275
		// (get) Token: 0x060034F7 RID: 13559 RVA: 0x0008B49E File Offset: 0x0008969E
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public WebPartEventHandler ServerClickHandler
		{
			get
			{
				return this.serverClickHandler;
			}
		}

		/// <summary>Gets or sets the text label for a verb that is displayed in the user interface (UI).</summary>
		/// <returns>A string containing the text label for a verb. The default is an empty string ("").</returns>
		// Token: 0x170010B4 RID: 4276
		// (get) Token: 0x060034F8 RID: 13560 RVA: 0x0008B4A6 File Offset: 0x000896A6
		// (set) Token: 0x060034F9 RID: 13561 RVA: 0x0008B4AE File Offset: 0x000896AE
		[NotifyParentProperty(true)]
		[WebSysDescription("Denotes text to be displayed for the verb")]
		[Localizable(true)]
		public virtual string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		/// <summary>Gets a dictionary of state information that allows you to save and restore the view state of a server control across multiple requests for the same page.</summary>
		/// <returns>An instance of <see cref="T:System.Web.UI.StateBag" /> that contains the server control's view-state information.</returns>
		// Token: 0x170010B5 RID: 4277
		// (get) Token: 0x060034FA RID: 13562 RVA: 0x0008B4B7 File Offset: 0x000896B7
		protected StateBag ViewState
		{
			get
			{
				return this.stateBag;
			}
		}

		/// <summary>Gets or sets a value that indicates whether a verb is visible to users.</summary>
		/// <returns>true if the verb is visible; otherwise, false. The default is true.</returns>
		// Token: 0x170010B6 RID: 4278
		// (get) Token: 0x060034FB RID: 13563 RVA: 0x0008B4BF File Offset: 0x000896BF
		// (set) Token: 0x060034FC RID: 13564 RVA: 0x0008B4C7 File Offset: 0x000896C7
		[DefaultValue(true)]
		[WebSysDescription("Denotes whether the verb is visible")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public bool Visible
		{
			get
			{
				return this.visible;
			}
			set
			{
				this.visible = value;
			}
		}

		// Token: 0x04001D3B RID: 7483
		private string clientClickHandler;

		// Token: 0x04001D3C RID: 7484
		private WebPartEventHandler serverClickHandler;

		// Token: 0x04001D3D RID: 7485
		private StateBag stateBag;

		// Token: 0x04001D3E RID: 7486
		private bool isChecked;

		// Token: 0x04001D3F RID: 7487
		private string description = string.Empty;

		// Token: 0x04001D40 RID: 7488
		private bool enabled = true;

		// Token: 0x04001D41 RID: 7489
		private string imageUrl = string.Empty;

		// Token: 0x04001D42 RID: 7490
		private string text = string.Empty;

		// Token: 0x04001D43 RID: 7491
		private bool visible = true;

		// Token: 0x04001D44 RID: 7492
		private string id;
	}
}
