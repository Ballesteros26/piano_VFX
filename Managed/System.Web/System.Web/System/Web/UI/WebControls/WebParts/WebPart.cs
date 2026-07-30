using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Serves as the base class for custom ASP.NET Web Parts controls, adding to the base <see cref="T:System.Web.UI.WebControls.WebParts.Part" /> class features some additional user interface (UI) properties, the ability to create connections, and personalization behavior. </summary>
	// Token: 0x0200048A RID: 1162
	public abstract class WebPart : Part, IWebPart, IWebActionable, IWebEditable
	{
		/// <summary>Initializes the class for use by an inherited class instance. This constructor can only be called by an inherited class.</summary>
		// Token: 0x06003497 RID: 13463 RVA: 0x0008AF20 File Offset: 0x00089120
		protected WebPart()
		{
			this.verbs = new WebPartVerbCollection();
			this.allow = WebPart.Allow.Close | WebPart.Allow.Connect | WebPart.Allow.Edit | WebPart.Allow.Hide | WebPart.Allow.Minimize | WebPart.Allow.ZoneChange;
			this.auth_filter = "";
			this.catalog_icon_url = "";
			this.titleIconImageUrl = string.Empty;
			this.titleUrl = string.Empty;
			this.helpUrl = string.Empty;
			this.isStatic = false;
			this.hasUserData = false;
			this.hasSharedData = false;
			this.hidden = false;
			this.isClosed = false;
		}

		/// <summary>Sets a flag indicating that personalization data has changed for the current <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control instance. </summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Web.UI.WebControls.WebParts.WebPart.WebPartManager" /> is null.</exception>
		// Token: 0x06003498 RID: 13464 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		protected void SetPersonalizationDirty()
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets a flag indicating that personalization data has changed for the specified server control that resides in a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zone.</summary>
		/// <param name="control">The <see cref="T:System.Web.UI.Control" /> for which the personalization data has changed.</param>
		/// <exception cref="T:System.ArgumentNullException">The object in the <paramref name="control" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The control is not associated with a page.</exception>
		/// <exception cref="T:System.InvalidOperationException">The page associated with the control does not have a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" />.</exception>
		/// <exception cref="T:System.ArgumentException">The control derives from <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" />. Controls that derive from <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> should use the protected <see cref="M:System.Web.UI.WebControls.WebParts.WebPart.SetPersonalizationDirty" /> method instead. </exception>
		// Token: 0x06003499 RID: 13465 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public static void SetPersonalizationDirty(Control control)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600349A RID: 13466 RVA: 0x0008AFB4 File Offset: 0x000891B4
		protected override void TrackViewState()
		{
			base.TrackViewState();
			foreach (object obj in this.verbs)
			{
				((IStateManager)obj).TrackViewState();
			}
		}

		// Token: 0x0600349B RID: 13467 RVA: 0x0008B010 File Offset: 0x00089210
		internal void SetZoneIndex(int index)
		{
			this.zoneIndex = index;
		}

		/// <summary>Enables derived classes to provide custom handling when a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is closed on a Web Parts page.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600349C RID: 13468 RVA: 0x0000393A File Offset: 0x00001B3A
		protected internal virtual void OnClosing(EventArgs e)
		{
		}

		/// <summary>Enables derived classes to provide custom handling when a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is beginning or ending the process of connecting to other controls.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600349D RID: 13469 RVA: 0x0000393A File Offset: 0x00001B3A
		protected internal virtual void OnConnectModeChanged(EventArgs e)
		{
		}

		/// <summary>Enables derived classes to provide custom handling when a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is permanently removed from a Web Parts page.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600349E RID: 13470 RVA: 0x0000393A File Offset: 0x00001B3A
		protected internal virtual void OnDeleting(EventArgs e)
		{
		}

		/// <summary>Enables derived classes to provide custom handling when a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is entering or leaving edit mode.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600349F RID: 13471 RVA: 0x0000393A File Offset: 0x00001B3A
		protected internal virtual void OnEditModeChanged(EventArgs e)
		{
		}

		/// <summary>Gets or sets a value indicating whether an end user can close a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control on a Web page.</summary>
		/// <returns>true if the control can be closed on a Web page; otherwise, false. The default value is true.</returns>
		// Token: 0x17001082 RID: 4226
		// (get) Token: 0x060034A0 RID: 13472 RVA: 0x0008B019 File Offset: 0x00089219
		// (set) Token: 0x060034A1 RID: 13473 RVA: 0x0008B026 File Offset: 0x00089226
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual bool AllowClose
		{
			get
			{
				return (this.allow & WebPart.Allow.Close) > (WebPart.Allow)0;
			}
			set
			{
				if (value)
				{
					this.allow |= WebPart.Allow.Close;
					return;
				}
				this.allow &= ~WebPart.Allow.Close;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control allows other controls to form connections with it.</summary>
		/// <returns>A Boolean value that indicates whether connections can be formed with the control. The default is true.</returns>
		// Token: 0x17001083 RID: 4227
		// (get) Token: 0x060034A2 RID: 13474 RVA: 0x0008B049 File Offset: 0x00089249
		// (set) Token: 0x060034A3 RID: 13475 RVA: 0x0008B056 File Offset: 0x00089256
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual bool AllowConnect
		{
			get
			{
				return (this.allow & WebPart.Allow.Connect) > (WebPart.Allow)0;
			}
			set
			{
				if (value)
				{
					this.allow |= WebPart.Allow.Connect;
					return;
				}
				this.allow &= ~WebPart.Allow.Connect;
			}
		}

		/// <summary>Gets or sets a value indicating whether an end user can modify a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control through the user interface (UI) provided by one or more <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control can be modified; otherwise, false. The default value is true.</returns>
		// Token: 0x17001084 RID: 4228
		// (get) Token: 0x060034A4 RID: 13476 RVA: 0x0008B079 File Offset: 0x00089279
		// (set) Token: 0x060034A5 RID: 13477 RVA: 0x0008B086 File Offset: 0x00089286
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		public virtual bool AllowEdit
		{
			get
			{
				return (this.allow & WebPart.Allow.Edit) > (WebPart.Allow)0;
			}
			set
			{
				if (value)
				{
					this.allow |= WebPart.Allow.Edit;
					return;
				}
				this.allow &= ~WebPart.Allow.Edit;
			}
		}

		/// <summary>Gets or sets a value indicating whether end users are allowed to hide a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control can be hidden; otherwise, false. The default value is true.</returns>
		// Token: 0x17001085 RID: 4229
		// (get) Token: 0x060034A6 RID: 13478 RVA: 0x0008B0A9 File Offset: 0x000892A9
		// (set) Token: 0x060034A7 RID: 13479 RVA: 0x0008B0B6 File Offset: 0x000892B6
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		public virtual bool AllowHide
		{
			get
			{
				return (this.allow & WebPart.Allow.Hide) > (WebPart.Allow)0;
			}
			set
			{
				if (value)
				{
					this.allow |= WebPart.Allow.Hide;
					return;
				}
				this.allow &= ~WebPart.Allow.Hide;
			}
		}

		/// <summary>Gets or sets a value indicating whether end users can minimize a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control can be minimized; otherwise, false. The default value is true.</returns>
		// Token: 0x17001086 RID: 4230
		// (get) Token: 0x060034A8 RID: 13480 RVA: 0x0008B0D9 File Offset: 0x000892D9
		// (set) Token: 0x060034A9 RID: 13481 RVA: 0x0008B0E7 File Offset: 0x000892E7
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		public virtual bool AllowMinimize
		{
			get
			{
				return (this.allow & WebPart.Allow.Minimize) > (WebPart.Allow)0;
			}
			set
			{
				if (value)
				{
					this.allow |= WebPart.Allow.Minimize;
					return;
				}
				this.allow &= ~WebPart.Allow.Minimize;
			}
		}

		/// <summary>Gets or sets a value indicating whether a user can move a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control between <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zones.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control can move between zones; otherwise, false. The default value is true.</returns>
		// Token: 0x17001087 RID: 4231
		// (get) Token: 0x060034AA RID: 13482 RVA: 0x0008B10B File Offset: 0x0008930B
		// (set) Token: 0x060034AB RID: 13483 RVA: 0x0008B119 File Offset: 0x00089319
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual bool AllowZoneChange
		{
			get
			{
				return (this.allow & WebPart.Allow.ZoneChange) > (WebPart.Allow)0;
			}
			set
			{
				if (value)
				{
					this.allow |= WebPart.Allow.ZoneChange;
					return;
				}
				this.allow &= ~WebPart.Allow.ZoneChange;
			}
		}

		/// <summary>Gets or sets an arbitrary string to determine whether a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is authorized to be added to a page. </summary>
		/// <returns>A string that authorizes a control to be added to a Web page. The default value is an empty string ("").</returns>
		// Token: 0x17001088 RID: 4232
		// (get) Token: 0x060034AC RID: 13484 RVA: 0x0008B13D File Offset: 0x0008933D
		// (set) Token: 0x060034AD RID: 13485 RVA: 0x0008B145 File Offset: 0x00089345
		public virtual string AuthorizationFilter
		{
			get
			{
				return this.auth_filter;
			}
			set
			{
				this.auth_filter = value;
			}
		}

		/// <summary>Gets or sets the URL to an image that represents a Web Parts control in a catalog of controls. </summary>
		/// <returns>A string that represents the URL to an image used to represent the control in a catalog. The default value is an empty string ("").</returns>
		/// <exception cref="T:System.ArgumentException">The internal validation system has determined that the URL might contain script attacks.</exception>
		// Token: 0x17001089 RID: 4233
		// (get) Token: 0x060034AE RID: 13486 RVA: 0x0008B14E File Offset: 0x0008934E
		// (set) Token: 0x060034AF RID: 13487 RVA: 0x0008B156 File Offset: 0x00089356
		public virtual string CatalogIconImageUrl
		{
			get
			{
				return this.catalog_icon_url;
			}
			set
			{
				this.catalog_icon_url = value;
			}
		}

		/// <summary>Gets or sets whether a part control is in a minimized or normal state.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeState" /> values. The default is <see cref="F:System.Web.UI.WebControls.WebParts.PartChromeState.Normal" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is not one of the <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeState" /> values. </exception>
		// Token: 0x1700108A RID: 4234
		// (get) Token: 0x060034B0 RID: 13488 RVA: 0x0008B15F File Offset: 0x0008935F
		// (set) Token: 0x060034B1 RID: 13489 RVA: 0x0008B167 File Offset: 0x00089367
		public override PartChromeState ChromeState
		{
			get
			{
				return base.ChromeState;
			}
			set
			{
				base.ChromeState = value;
			}
		}

		/// <summary>Gets or sets the type of border that frames a Web Parts control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeType" /> values. The default is <see cref="F:System.Web.UI.WebControls.WebParts.PartChromeType.Default" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is not one of the <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeType" /> values. </exception>
		// Token: 0x1700108B RID: 4235
		// (get) Token: 0x060034B2 RID: 13490 RVA: 0x0008B170 File Offset: 0x00089370
		// (set) Token: 0x060034B3 RID: 13491 RVA: 0x0008B178 File Offset: 0x00089378
		public override PartChromeType ChromeType
		{
			get
			{
				return base.ChromeType;
			}
			set
			{
				base.ChromeType = value;
			}
		}

		/// <summary>Gets an error message to display to users if errors occur during the connection process.</summary>
		/// <returns>A string that contains the error message.</returns>
		// Token: 0x1700108C RID: 4236
		// (get) Token: 0x060034B4 RID: 13492 RVA: 0x000195AF File Offset: 0x000177AF
		[global::System.MonoTODO("Not implemented")]
		public string ConnectErrorMessage
		{
			get
			{
				return "";
			}
		}

		/// <summary>Gets or sets a brief phrase that summarizes what the part control does, for use in ToolTips and catalogs of part controls.</summary>
		/// <returns>A string that briefly summarizes the part control's functionality. The default value is an empty string ("").</returns>
		// Token: 0x1700108D RID: 4237
		// (get) Token: 0x060034B5 RID: 13493 RVA: 0x0008B181 File Offset: 0x00089381
		// (set) Token: 0x060034B6 RID: 13494 RVA: 0x0008B189 File Offset: 0x00089389
		public override string Description
		{
			get
			{
				return base.Description;
			}
			set
			{
				base.Description = value;
			}
		}

		/// <summary>Gets or sets the horizontal direction that content flows within the control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ContentDirection" /> that indicates the horizontal direction content will flow.</returns>
		// Token: 0x1700108E RID: 4238
		// (get) Token: 0x060034B7 RID: 13495 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x060034B8 RID: 13496 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public override ContentDirection Direction
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a string that contains the full title text actually displayed in the title bar of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control instance.</summary>
		/// <returns>A string that represents the complete, displayed title of the control. The default value is an empty string ("").</returns>
		// Token: 0x1700108F RID: 4239
		// (get) Token: 0x060034B9 RID: 13497 RVA: 0x0008B192 File Offset: 0x00089392
		public string DisplayTitle
		{
			get
			{
				return "Untitled";
			}
		}

		/// <summary>Gets or sets whether all, some, or none of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control's properties can be exported. </summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartExportMode" /> values. The default is <see cref="F:System.Web.UI.WebControls.WebParts.WebPartExportMode.None" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is not one of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartExportMode" /> values.</exception>
		/// <exception cref="T:System.InvalidOperationException">The control is already loaded and the personalization scope of the control is set to the <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.User" /> scope.</exception>
		// Token: 0x17001090 RID: 4240
		// (get) Token: 0x060034BA RID: 13498 RVA: 0x0008B199 File Offset: 0x00089399
		// (set) Token: 0x060034BB RID: 13499 RVA: 0x0008B1A1 File Offset: 0x000893A1
		public virtual WebPartExportMode ExportMode
		{
			get
			{
				return this.exportMode;
			}
			set
			{
				this.exportMode = value;
			}
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control has any shared personalization data associated with it.</summary>
		/// <returns>A Boolean value that indicates whether the control has shared personalization data.</returns>
		// Token: 0x17001091 RID: 4241
		// (get) Token: 0x060034BC RID: 13500 RVA: 0x0008B1AA File Offset: 0x000893AA
		public bool HasSharedData
		{
			get
			{
				return this.hasSharedData;
			}
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control has any user personalization data associated with it.</summary>
		/// <returns>A Boolean value that indicates whether the control has any user personalization data.</returns>
		// Token: 0x17001092 RID: 4242
		// (get) Token: 0x060034BD RID: 13501 RVA: 0x0008B1B2 File Offset: 0x000893B2
		public bool HasUserData
		{
			get
			{
				return this.hasUserData;
			}
		}

		/// <summary>Gets or sets the height of a zone.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> object that indicates the height of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZone" />. The default type of a <see cref="T:System.Web.UI.WebControls.Unit" /> is pixels, as indicated by the <see cref="P:System.Web.UI.WebControls.Unit.Type" /> property.</returns>
		// Token: 0x17001093 RID: 4243
		// (get) Token: 0x060034BE RID: 13502 RVA: 0x0008B1BA File Offset: 0x000893BA
		// (set) Token: 0x060034BF RID: 13503 RVA: 0x0008B1C2 File Offset: 0x000893C2
		public override Unit Height
		{
			get
			{
				return base.Height;
			}
			set
			{
				base.Height = value;
			}
		}

		/// <summary>Gets or sets the type of user interface (UI) used to display Help content for a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartHelpMode" /> values. The default is <see cref="F:System.Web.UI.WebControls.WebParts.WebPartHelpMode.Modal" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is not one of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartHelpMode" /> values.</exception>
		// Token: 0x17001094 RID: 4244
		// (get) Token: 0x060034C0 RID: 13504 RVA: 0x0008B1CB File Offset: 0x000893CB
		// (set) Token: 0x060034C1 RID: 13505 RVA: 0x0008B1D3 File Offset: 0x000893D3
		public virtual WebPartHelpMode HelpMode
		{
			get
			{
				return this.helpMode;
			}
			set
			{
				this.helpMode = value;
			}
		}

		/// <summary>Gets or sets the URL to a Help file for a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <returns>A string that represents the URL to a Help file. The default value is an empty string ("").</returns>
		/// <exception cref="T:System.ArgumentException">The internal validation system has determined that the URL might contain script attacks.</exception>
		// Token: 0x17001095 RID: 4245
		// (get) Token: 0x060034C2 RID: 13506 RVA: 0x0008B1DC File Offset: 0x000893DC
		// (set) Token: 0x060034C3 RID: 13507 RVA: 0x0008B1E4 File Offset: 0x000893E4
		public virtual string HelpUrl
		{
			get
			{
				return this.helpUrl;
			}
			set
			{
				this.helpUrl = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is displayed on a Web page.</summary>
		/// <returns>false if the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is displayed on a Web page; otherwise, true. The default value is false.</returns>
		// Token: 0x17001096 RID: 4246
		// (get) Token: 0x060034C4 RID: 13508 RVA: 0x0008B1ED File Offset: 0x000893ED
		// (set) Token: 0x060034C5 RID: 13509 RVA: 0x0008B1F5 File Offset: 0x000893F5
		public virtual bool Hidden
		{
			get
			{
				return this.hidden;
			}
			set
			{
				this.hidden = value;
			}
		}

		/// <summary>Gets or sets an error message that is used if errors occur when a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is imported.</summary>
		/// <returns>A string that contains the error message. The default value is a standard error message supplied by the Web Parts control set. </returns>
		// Token: 0x17001097 RID: 4247
		// (get) Token: 0x060034C6 RID: 13510 RVA: 0x0008B1FE File Offset: 0x000893FE
		// (set) Token: 0x060034C7 RID: 13511 RVA: 0x0008B215 File Offset: 0x00089415
		public virtual string ImportErrorMessage
		{
			get
			{
				return this.ViewState.GetString("ImportErrorMessage", "Cannot import this Web Part.");
			}
			set
			{
				this.ViewState["ImportErrorMessage"] = value;
			}
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is currently closed on a Web Parts page.</summary>
		/// <returns>A Boolean value that indicates whether the control is closed.</returns>
		// Token: 0x17001098 RID: 4248
		// (get) Token: 0x060034C8 RID: 13512 RVA: 0x0008B228 File Offset: 0x00089428
		public bool IsClosed
		{
			get
			{
				return this.isClosed;
			}
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is shared, meaning that it is visible to all users of a Web Parts page.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control has shared user visibility on a Web page; otherwise, false. The default value is false.</returns>
		// Token: 0x17001099 RID: 4249
		// (get) Token: 0x060034C9 RID: 13513 RVA: 0x00008A69 File Offset: 0x00006C69
		public bool IsShared
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is standalone, meaning that it is not contained within a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zone.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is not contained in a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zone; otherwise, false. </returns>
		// Token: 0x1700109A RID: 4250
		// (get) Token: 0x060034CA RID: 13514 RVA: 0x00008B66 File Offset: 0x00006D66
		public bool IsStandalone
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is a static control, which means the control is declared in the markup of a Web Parts page and not added to the page programmatically.</summary>
		/// <returns>A Boolean value that indicates whether the control is static.</returns>
		// Token: 0x1700109B RID: 4251
		// (get) Token: 0x060034CB RID: 13515 RVA: 0x0008B230 File Offset: 0x00089430
		public bool IsStatic
		{
			get
			{
				return this.isStatic;
			}
		}

		/// <summary>Gets a string that is concatenated with the <see cref="P:System.Web.UI.WebControls.WebParts.WebPart.Title" /> property value to form a complete title for a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control. </summary>
		/// <returns>A string that serves as a subtitle for the control. The default value is an empty string ("").</returns>
		// Token: 0x1700109C RID: 4252
		// (get) Token: 0x060034CC RID: 13516 RVA: 0x0000EE9B File Offset: 0x0000D09B
		public virtual string Subtitle
		{
			get
			{
				return string.Empty;
			}
		}

		/// <summary>Gets or sets the title of a part control.</summary>
		/// <returns>A string that represents the title of the part control. The default value is an empty string ("").</returns>
		// Token: 0x1700109D RID: 4253
		// (get) Token: 0x060034CD RID: 13517 RVA: 0x0008B238 File Offset: 0x00089438
		// (set) Token: 0x060034CE RID: 13518 RVA: 0x0008B240 File Offset: 0x00089440
		public override string Title
		{
			get
			{
				return base.Title;
			}
			set
			{
				base.Title = value;
			}
		}

		/// <summary>Gets or sets the URL to an image used to represent a Web Parts control in the control's title bar.</summary>
		/// <returns>A string that represents the URL to an image used to represent the control in its title bar. The default value is an empty string ("").</returns>
		/// <exception cref="T:System.ArgumentException">The internal validation system has determined that the URL might contain script attacks.</exception>
		// Token: 0x1700109E RID: 4254
		// (get) Token: 0x060034CF RID: 13519 RVA: 0x0008B249 File Offset: 0x00089449
		// (set) Token: 0x060034D0 RID: 13520 RVA: 0x0008B251 File Offset: 0x00089451
		public virtual string TitleIconImageUrl
		{
			get
			{
				return this.titleIconImageUrl;
			}
			set
			{
				this.titleIconImageUrl = value;
			}
		}

		/// <summary>Gets or sets a URL to supplemental information about a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control. </summary>
		/// <returns>A string that represents a URL to more information about the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control. The default value is an empty string ("").</returns>
		/// <exception cref="T:System.ArgumentException">The internal validation system has determined that the URL might contain script attacks.</exception>
		// Token: 0x1700109F RID: 4255
		// (get) Token: 0x060034D1 RID: 13521 RVA: 0x0008B25A File Offset: 0x0008945A
		// (set) Token: 0x060034D2 RID: 13522 RVA: 0x0008B262 File Offset: 0x00089462
		public virtual string TitleUrl
		{
			get
			{
				return this.titleUrl;
			}
			set
			{
				this.titleUrl = value;
			}
		}

		/// <summary>Gets a collection of custom verbs associated with a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerbCollection" /> that contains custom <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> objects associated with a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control. The default value is <see cref="F:System.Web.UI.WebControls.WebParts.WebPartVerbCollection.Empty" />. </returns>
		// Token: 0x170010A0 RID: 4256
		// (get) Token: 0x060034D3 RID: 13523 RVA: 0x0008B26B File Offset: 0x0008946B
		public virtual WebPartVerbCollection Verbs
		{
			get
			{
				return this.verbs;
			}
		}

		// Token: 0x170010A1 RID: 4257
		// (get) Token: 0x060034D4 RID: 13524 RVA: 0x0008B273 File Offset: 0x00089473
		// (set) Token: 0x060034D5 RID: 13525 RVA: 0x0008B27B File Offset: 0x0008947B
		public override Unit Width
		{
			get
			{
				return base.Width;
			}
			set
			{
				base.Width = value;
			}
		}

		/// <summary>Gets the index position of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control within its zone.</summary>
		/// <returns>The numerical order of a control within its zone. The first control in a zone has an index value of zero.</returns>
		// Token: 0x170010A2 RID: 4258
		// (get) Token: 0x060034D6 RID: 13526 RVA: 0x0008B284 File Offset: 0x00089484
		public int ZoneIndex
		{
			get
			{
				return this.zoneIndex;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control to enable it to be edited by custom <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls.</summary>
		/// <returns>A <see cref="T:System.Object" /> that consists of the child control of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</returns>
		// Token: 0x170010A3 RID: 4259
		// (get) Token: 0x060034D7 RID: 13527 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual object WebBrowsableObject
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control associated with a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control instance. </summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> that is associated with a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</returns>
		// Token: 0x170010A4 RID: 4260
		// (get) Token: 0x060034D8 RID: 13528 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected WebPartManager WebPartManager
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zone that currently contains a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> that currently contains a Web Parts control on a Web page. If a Web Parts control is currently closed on a page, the return value is null.</returns>
		// Token: 0x170010A5 RID: 4261
		// (get) Token: 0x060034D9 RID: 13529 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebPartZoneBase Zone
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Returns a collection of custom <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls that can be used to edit a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control when it is in edit mode.</summary>
		/// <returns>An <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartCollection" /> that contains custom <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls associated with a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</returns>
		// Token: 0x060034DA RID: 13530 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual EditorPartCollection CreateEditorParts()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x04001D1D RID: 7453
		private WebPartVerbCollection verbs = new WebPartVerbCollection();

		// Token: 0x04001D1E RID: 7454
		private WebPart.Allow allow;

		// Token: 0x04001D1F RID: 7455
		private string auth_filter;

		// Token: 0x04001D20 RID: 7456
		private string catalog_icon_url;

		// Token: 0x04001D21 RID: 7457
		private WebPartExportMode exportMode;

		// Token: 0x04001D22 RID: 7458
		private string titleIconImageUrl;

		// Token: 0x04001D23 RID: 7459
		private string titleUrl;

		// Token: 0x04001D24 RID: 7460
		private string helpUrl;

		// Token: 0x04001D25 RID: 7461
		private bool isStatic;

		// Token: 0x04001D26 RID: 7462
		private bool hidden;

		// Token: 0x04001D27 RID: 7463
		private bool isClosed;

		// Token: 0x04001D28 RID: 7464
		private bool hasSharedData;

		// Token: 0x04001D29 RID: 7465
		private bool hasUserData;

		// Token: 0x04001D2A RID: 7466
		private WebPartHelpMode helpMode = WebPartHelpMode.Navigate;

		// Token: 0x04001D2B RID: 7467
		private int zoneIndex;

		// Token: 0x0200048B RID: 1163
		[Flags]
		private enum Allow
		{
			// Token: 0x04001D2D RID: 7469
			Close = 1,
			// Token: 0x04001D2E RID: 7470
			Connect = 2,
			// Token: 0x04001D2F RID: 7471
			Edit = 4,
			// Token: 0x04001D30 RID: 7472
			Hide = 8,
			// Token: 0x04001D31 RID: 7473
			Minimize = 16,
			// Token: 0x04001D32 RID: 7474
			ZoneChange = 32
		}
	}
}
