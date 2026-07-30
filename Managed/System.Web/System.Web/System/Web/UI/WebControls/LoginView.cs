using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using System.Security.Principal;
using System.Web.Security;

namespace System.Web.UI.WebControls
{
	/// <summary>Displays the appropriate content template for a given user, based on the user's authentication status and role membership.</summary>
	// Token: 0x020003CC RID: 972
	[Bindable(true)]
	[DefaultEvent("ViewChanged")]
	[DefaultProperty("CurrentView")]
	[Designer("System.Web.UI.Design.WebControls.LoginViewDesigner,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ParseChildren(true)]
	[PersistChildren(false)]
	[Themeable(true)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class LoginView : Control, INamingContainer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.LoginView" /> control.</summary>
		// Token: 0x060028CE RID: 10446 RVA: 0x0006AA53 File Offset: 0x00068C53
		public LoginView()
		{
			this.theming = true;
		}

		/// <summary>Gets or sets the template to display to users who are not logged in to the Web site.</summary>
		/// <returns>The <see cref="T:System.Web.UI.ITemplate" /> to display.</returns>
		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x060028CF RID: 10447 RVA: 0x0006AA62 File Offset: 0x00068C62
		// (set) Token: 0x060028D0 RID: 10448 RVA: 0x0006AA6A File Offset: 0x00068C6A
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(LoginView))]
		[Browsable(false)]
		public virtual ITemplate AnonymousTemplate
		{
			get
			{
				return this.anonymousTemplate;
			}
			set
			{
				this.anonymousTemplate = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.ControlCollection" /> object that contains the child controls for the <see cref="T:System.Web.UI.WebControls.LoginView" /> control.</summary>
		/// <returns>The collection of child controls for the <see cref="T:System.Web.UI.WebControls.LoginView" /> control.</returns>
		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x060028D1 RID: 10449 RVA: 0x00047ACE File Offset: 0x00045CCE
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		/// <summary>Gets or sets a value indicating whether themes can be applied to the <see cref="T:System.Web.UI.WebControls.LoginView" /> control. </summary>
		/// <returns>true to use themes; otherwise, false. The default is true.</returns>
		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x060028D2 RID: 10450 RVA: 0x0006AA73 File Offset: 0x00068C73
		// (set) Token: 0x060028D3 RID: 10451 RVA: 0x0006AA7B File Offset: 0x00068C7B
		[Browsable(true)]
		public override bool EnableTheming
		{
			get
			{
				return this.theming;
			}
			set
			{
				this.theming = value;
			}
		}

		/// <summary>Gets or sets the template to display to Web site users who are logged in to the Web site but are not members of one of the role groups specified in the <see cref="P:System.Web.UI.WebControls.LoginView.RoleGroups" /> property.</summary>
		/// <returns>The <see cref="T:System.Web.UI.ITemplate" /> to display.</returns>
		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x060028D4 RID: 10452 RVA: 0x0006AA84 File Offset: 0x00068C84
		// (set) Token: 0x060028D5 RID: 10453 RVA: 0x0006AA8C File Offset: 0x00068C8C
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(LoginView))]
		public virtual ITemplate LoggedInTemplate
		{
			get
			{
				return this.loggedInTemplate;
			}
			set
			{
				this.loggedInTemplate = value;
			}
		}

		/// <summary>Gets a collection of role groups that associate content templates with particular roles.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.RoleGroupCollection" /> object that contains the defined role-group templates.</returns>
		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x060028D6 RID: 10454 RVA: 0x0006AA95 File Offset: 0x00068C95
		[Themeable(false)]
		[Filterable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		public RoleGroupCollection RoleGroups
		{
			get
			{
				if (this.coll == null)
				{
					this.coll = new RoleGroupCollection();
				}
				return this.coll;
			}
		}

		/// <summary>Gets or sets the skin to apply to the <see cref="T:System.Web.UI.WebControls.LoginView" /> control.</summary>
		/// <returns>The name of the skin to apply to the <see cref="T:System.Web.UI.WebControls.LoginView" /> control. The default value is an empty string ("").</returns>
		/// <exception cref="T:System.ArgumentException">The skin specified in the <see cref="P:System.Web.UI.WebControls.LoginView.SkinID" /> property does not exist in the theme. </exception>
		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x060028D7 RID: 10455 RVA: 0x00032ACF File Offset: 0x00030CCF
		// (set) Token: 0x060028D8 RID: 10456 RVA: 0x00032AD7 File Offset: 0x00030CD7
		[Browsable(true)]
		public override string SkinID
		{
			get
			{
				return base.SkinID;
			}
			set
			{
				base.SkinID = value;
			}
		}

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x060028D9 RID: 10457 RVA: 0x0006AAB0 File Offset: 0x00068CB0
		// (set) Token: 0x060028DA RID: 10458 RVA: 0x0006AAB8 File Offset: 0x00068CB8
		private bool IsAuthenticated
		{
			get
			{
				return this.isAuthenticated;
			}
			set
			{
				if (value == this.isAuthenticated)
				{
					return;
				}
				this.isAuthenticated = value;
				this.OnViewChanging(EventArgs.Empty);
				base.ChildControlsCreated = false;
				this.OnViewChanged(EventArgs.Empty);
			}
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x0006AAE8 File Offset: 0x00068CE8
		private ITemplate GetTemplateFromRoleGroup(RoleGroup rg, IPrincipal user)
		{
			if (user == null)
			{
				return null;
			}
			foreach (string text in rg.Roles)
			{
				if (user.IsInRole(text))
				{
					return rg.ContentTemplate;
				}
			}
			return null;
		}

		/// <summary>Creates the child controls that make up the <see cref="T:System.Web.UI.WebControls.LoginView" /> control.</summary>
		// Token: 0x060028DC RID: 10460 RVA: 0x0006AB24 File Offset: 0x00068D24
		protected internal override void CreateChildControls()
		{
			this.Controls.Clear();
			Control control = new Control();
			ITemplate template = null;
			if (this.Page != null && this.Page.Request.IsAuthenticated)
			{
				this.isAuthenticated = true;
				HttpContext httpContext = HttpContext.Current;
				IPrincipal principal = ((httpContext != null) ? httpContext.User : null);
				RoleGroupCollection roleGroups;
				if (Roles.Enabled && (roleGroups = this.RoleGroups) != null && roleGroups.Count > 0)
				{
					foreach (object obj in roleGroups)
					{
						RoleGroup roleGroup = (RoleGroup)obj;
						template = this.GetTemplateFromRoleGroup(roleGroup, principal);
						if (template != null)
						{
							break;
						}
					}
				}
				if (template == null)
				{
					template = this.LoggedInTemplate;
				}
			}
			else
			{
				this.isAuthenticated = false;
				template = this.AnonymousTemplate;
			}
			if (template != null)
			{
				template.InstantiateIn(control);
			}
			this.Controls.Add(control);
		}

		/// <summary>Binds a data source to <see cref="T:System.Web.UI.WebControls.LoginView" /> and all its child controls.</summary>
		// Token: 0x060028DD RID: 10461 RVA: 0x0006AC24 File Offset: 0x00068E24
		public override void DataBind()
		{
			EventArgs empty = EventArgs.Empty;
			this.OnDataBinding(empty);
			this.EnsureChildControls();
			this.DataBindChildren();
		}

		/// <summary>Sets input focus to a control.</summary>
		/// <exception cref="T:System.NotSupportedException">You call the <see cref="M:System.Web.UI.WebControls.LoginView.Focus" /> method.</exception>
		// Token: 0x060028DE RID: 10462 RVA: 0x00003A01 File Offset: 0x00001C01
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void Focus()
		{
			throw new NotSupportedException();
		}

		/// <summary>This method implements <see cref="M:System.Web.UI.Control.LoadControlState(System.Object)" />.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that represents the control state to be restored.</param>
		// Token: 0x060028DF RID: 10463 RVA: 0x0006AC4A File Offset: 0x00068E4A
		protected internal override void LoadControlState(object savedState)
		{
			if (savedState == null)
			{
				return;
			}
			this.isAuthenticated = (bool)savedState;
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x060028E0 RID: 10464 RVA: 0x0006AC5C File Offset: 0x00068E5C
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.Page != null)
			{
				this.Page.RegisterRequiresControlState(this);
			}
		}

		/// <summary>Determines which role-group template to display, based on the roles of the logged-in user.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x060028E1 RID: 10465 RVA: 0x0006AC79 File Offset: 0x00068E79
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.Page != null)
			{
				this.IsAuthenticated = this.Page.Request.IsAuthenticated;
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.LoginView.ViewChanged" /> event after the <see cref="T:System.Web.UI.WebControls.LoginView" /> control switches views.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x060028E2 RID: 10466 RVA: 0x0006ACA0 File Offset: 0x00068EA0
		protected virtual void OnViewChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[LoginView.viewChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.LoginView.ViewChanging" /> event before the <see cref="T:System.Web.UI.WebControls.LoginView" /> control switches views.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x060028E3 RID: 10467 RVA: 0x0006ACD0 File Offset: 0x00068ED0
		protected virtual void OnViewChanging(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[LoginView.viewChangingEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Renders the Web server control content to the client's browser using the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> used to render the server control content on the client's browser.</param>
		// Token: 0x060028E4 RID: 10468 RVA: 0x0006ACFE File Offset: 0x00068EFE
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.EnsureChildControls();
			base.Render(writer);
		}

		/// <summary>This method implements <see cref="M:System.Web.UI.Control.SaveControlState" />.</summary>
		// Token: 0x060028E5 RID: 10469 RVA: 0x0006AD0D File Offset: 0x00068F0D
		protected internal override object SaveControlState()
		{
			if (this.isAuthenticated)
			{
				return this.isAuthenticated;
			}
			return null;
		}

		/// <param name="data">An <see cref="T:System.Collections.IDictionary" /> object containing the state of the <see cref="T:System.Web.UI.WebControls.LoginView" /> control. </param>
		// Token: 0x060028E6 RID: 10470 RVA: 0x000524CC File Offset: 0x000506CC
		[global::System.MonoTODO("for design-time usage - no more details available")]
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override void SetDesignModeState(IDictionary data)
		{
			base.SetDesignModeState(data);
		}

		/// <summary>Occurs after the view is changed.</summary>
		// Token: 0x140000AE RID: 174
		// (add) Token: 0x060028E7 RID: 10471 RVA: 0x0006AD24 File Offset: 0x00068F24
		// (remove) Token: 0x060028E8 RID: 10472 RVA: 0x0006AD37 File Offset: 0x00068F37
		public event EventHandler ViewChanged
		{
			add
			{
				base.Events.AddHandler(LoginView.viewChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(LoginView.viewChangedEvent, value);
			}
		}

		/// <summary>Occurs before the view is changed.</summary>
		// Token: 0x140000AF RID: 175
		// (add) Token: 0x060028E9 RID: 10473 RVA: 0x0006AD4A File Offset: 0x00068F4A
		// (remove) Token: 0x060028EA RID: 10474 RVA: 0x0006AD5D File Offset: 0x00068F5D
		public event EventHandler ViewChanging
		{
			add
			{
				base.Events.AddHandler(LoginView.viewChangingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(LoginView.viewChangingEvent, value);
			}
		}

		// Token: 0x04001A8F RID: 6799
		private static readonly object viewChangedEvent = new object();

		// Token: 0x04001A90 RID: 6800
		private static readonly object viewChangingEvent = new object();

		// Token: 0x04001A91 RID: 6801
		private ITemplate anonymousTemplate;

		// Token: 0x04001A92 RID: 6802
		private ITemplate loggedInTemplate;

		// Token: 0x04001A93 RID: 6803
		private bool isAuthenticated;

		// Token: 0x04001A94 RID: 6804
		private bool theming;

		// Token: 0x04001A95 RID: 6805
		private RoleGroupCollection coll;
	}
}
