using System;
using System.ComponentModel;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Serves as the base class for controls that bind to data using an ASP.NET data source control.</summary>
	// Token: 0x02000336 RID: 822
	[DefaultProperty("DataSourceID")]
	[Designer("System.Web.UI.Design.WebControls.BaseDataBoundControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class BaseDataBoundControl : WebControl
	{
		/// <summary>Occurs after the server control binds to a data source.</summary>
		// Token: 0x14000046 RID: 70
		// (add) Token: 0x06001C93 RID: 7315 RVA: 0x000477A0 File Offset: 0x000459A0
		// (remove) Token: 0x06001C94 RID: 7316 RVA: 0x000477B3 File Offset: 0x000459B3
		public event EventHandler DataBound
		{
			add
			{
				this.events.AddHandler(BaseDataBoundControl.dataBoundEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(BaseDataBoundControl.dataBoundEvent, value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.BaseDataBoundControl" /> class.</summary>
		// Token: 0x06001C95 RID: 7317 RVA: 0x000477C6 File Offset: 0x000459C6
		protected BaseDataBoundControl()
		{
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x000477D9 File Offset: 0x000459D9
		internal BaseDataBoundControl(HtmlTextWriterTag tag)
			: base(tag)
		{
		}

		/// <summary>Gets or sets the object from which the data-bound control retrieves its list of data items.</summary>
		/// <returns>An object that represents the data source from which the data-bound control retrieves its data. The default is null.</returns>
		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06001C97 RID: 7319 RVA: 0x000477ED File Offset: 0x000459ED
		// (set) Token: 0x06001C98 RID: 7320 RVA: 0x000477F5 File Offset: 0x000459F5
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(null)]
		[Themeable(false)]
		[Bindable(true)]
		public virtual object DataSource
		{
			get
			{
				return this.dataSource;
			}
			set
			{
				if (value != null)
				{
					this.ValidateDataSource(value);
				}
				this.dataSource = value;
				this.OnDataPropertyChanged();
			}
		}

		/// <summary>Gets or sets the ID of the control from which the data-bound control retrieves its list of data items.</summary>
		/// <returns>The ID of a control that represents the data source from which the data-bound control retrieves its data. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06001C99 RID: 7321 RVA: 0x0004780E File Offset: 0x00045A0E
		// (set) Token: 0x06001C9A RID: 7322 RVA: 0x00047825 File Offset: 0x00045A25
		[Themeable(false)]
		[DefaultValue("")]
		public virtual string DataSourceID
		{
			get
			{
				return this.ViewState.GetString("DataSourceID", string.Empty);
			}
			set
			{
				this.ViewState["DataSourceID"] = value;
				this.OnDataPropertyChanged();
			}
		}

		/// <summary>Gets a value indicating whether the data-bound control has been initialized.</summary>
		/// <returns>true if the data-bound control has been initialized; otherwise, false.</returns>
		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06001C9B RID: 7323 RVA: 0x0004783E File Offset: 0x00045A3E
		protected bool Initialized
		{
			get
			{
				return this.initialized;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Web.UI.WebControls.BaseDataBoundControl.DataSourceID" /> property is set.</summary>
		/// <returns>The value true is returned if the <see cref="P:System.Web.UI.WebControls.BaseDataBoundControl.DataSourceID" /> property is set to value other than <see cref="F:System.String.Empty" />; otherwise, the value is false.</returns>
		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06001C9C RID: 7324 RVA: 0x00047846 File Offset: 0x00045A46
		protected bool IsBoundUsingDataSourceID
		{
			get
			{
				return this.DataSourceID.Length > 0;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="M:System.Web.UI.WebControls.BaseDataBoundControl.DataBind" /> method should be called. </summary>
		/// <returns>The returned value is true if the data-bound control's <see cref="M:System.Web.UI.WebControls.BaseDataBoundControl.DataBind" /> method should be called before the control is rendered; otherwise, the value is false.</returns>
		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06001C9D RID: 7325 RVA: 0x00047856 File Offset: 0x00045A56
		// (set) Token: 0x06001C9E RID: 7326 RVA: 0x0004785E File Offset: 0x00045A5E
		protected bool RequiresDataBinding
		{
			get
			{
				return this.requiresDataBinding;
			}
			set
			{
				if (value && this.preRendered && this.IsBoundUsingDataSourceID && this.Page != null && !this.Page.IsCallback)
				{
					this.requiresDataBinding = true;
					this.EnsureDataBound();
					return;
				}
				this.requiresDataBinding = value;
			}
		}

		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, false.</returns>
		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06001C9F RID: 7327 RVA: 0x0004789D File Offset: 0x00045A9D
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return base.RenderingCompatibilityLessThan40;
			}
		}

		/// <summary>Sets the initialized state of the data-bound control.</summary>
		// Token: 0x06001CA0 RID: 7328 RVA: 0x000478A5 File Offset: 0x00045AA5
		protected void ConfirmInitState()
		{
			this.initialized = true;
		}

		/// <summary>Binds a data source to the invoked server control and all its child controls.</summary>
		// Token: 0x06001CA1 RID: 7329 RVA: 0x000478AE File Offset: 0x00045AAE
		public override void DataBind()
		{
			this.PerformSelect();
		}

		/// <summary>Calls the <see cref="M:System.Web.UI.WebControls.BaseDataBoundControl.DataBind" /> method if the <see cref="P:System.Web.UI.WebControls.BaseDataBoundControl.DataSourceID" /> property is set and the data-bound control is marked to require binding.</summary>
		// Token: 0x06001CA2 RID: 7330 RVA: 0x000478B6 File Offset: 0x00045AB6
		protected virtual void EnsureDataBound()
		{
			if (this.RequiresDataBinding && this.IsBoundUsingDataSourceID)
			{
				this.DataBind();
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.BaseDataBoundControl.DataBound" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x06001CA3 RID: 7331 RVA: 0x000478D0 File Offset: 0x00045AD0
		protected virtual void OnDataBound(EventArgs e)
		{
			EventHandler eventHandler = this.events[BaseDataBoundControl.dataBoundEvent] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Rebinds the data-bound control to its data after one of the base data source identification properties changes.</summary>
		/// <exception cref="T:System.Web.HttpException">An attempt was made to change the property value during the data-binding phase of the control.</exception>
		// Token: 0x06001CA4 RID: 7332 RVA: 0x000478FE File Offset: 0x00045AFE
		protected virtual void OnDataPropertyChanged()
		{
			if (this.Initialized)
			{
				this.RequiresDataBinding = true;
			}
		}

		/// <summary>Handles the <see cref="E:System.Web.UI.Control.Init" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x06001CA5 RID: 7333 RVA: 0x00047910 File Offset: 0x00045B10
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.Page.PreLoad += this.OnPagePreLoad;
			if (!base.IsViewStateEnabled && this.Page != null && this.Page.IsPostBack)
			{
				this.RequiresDataBinding = true;
			}
		}

		/// <summary>Sets the initialized state of the data-bound control before the control is loaded.</summary>
		/// <param name="sender">The <see cref="T:System.Web.UI.Page" /> that raised the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x06001CA6 RID: 7334 RVA: 0x00047960 File Offset: 0x00045B60
		protected virtual void OnPagePreLoad(object sender, EventArgs e)
		{
			this.ConfirmInitState();
		}

		/// <summary>Handles the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x06001CA7 RID: 7335 RVA: 0x00047968 File Offset: 0x00045B68
		protected internal override void OnPreRender(EventArgs e)
		{
			this.preRendered = true;
			this.EnsureDataBound();
			base.OnPreRender(e);
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x00047980 File Offset: 0x00045B80
		internal Control FindDataSource()
		{
			Control control = this.NamingContainer;
			string dataSourceID = this.DataSourceID;
			while (control != null)
			{
				Control control2 = control.FindControl(dataSourceID);
				if (control2 != null)
				{
					return control2;
				}
				control = control.NamingContainer;
			}
			return null;
		}

		/// <summary>When overridden in a derived class, controls how data is retrieved and bound to the control.</summary>
		// Token: 0x06001CA9 RID: 7337
		protected abstract void PerformSelect();

		/// <summary>When overridden in a derived class, verifies that the object a data-bound control binds to is one it can work with.</summary>
		/// <param name="dataSource">The object to verify. Typically an instance of <see cref="T:System.Collections.IEnumerable" />, <see cref="T:System.ComponentModel.IListSource" />, <see cref="T:System.Web.UI.IDataSource" />, or <see cref="T:System.Web.UI.IHierarchicalDataSource" />.</param>
		// Token: 0x06001CAA RID: 7338
		protected abstract void ValidateDataSource(object dataSource);

		/// <summary>Gets a value that indicates whether data binding is automatic.</summary>
		/// <returns>true if data binding is automatic; otherwise, false.</returns>
		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06001CAC RID: 7340 RVA: 0x000479C4 File Offset: 0x00045BC4
		protected internal bool IsDataBindingAutomatic
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>When implemented in a derived class, gets a value that indicates whether the control is using model binders.</summary>
		/// <returns>true if the control is using model binders; otherwise, false. The default is false.</returns>
		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06001CAD RID: 7341 RVA: 0x000479E0 File Offset: 0x00045BE0
		protected virtual bool IsUsingModelBinders
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		// Token: 0x040017F4 RID: 6132
		private static readonly object dataBoundEvent = new object();

		// Token: 0x040017F5 RID: 6133
		private EventHandlerList events = new EventHandlerList();

		// Token: 0x040017F6 RID: 6134
		private object dataSource;

		// Token: 0x040017F7 RID: 6135
		private bool initialized;

		// Token: 0x040017F8 RID: 6136
		private bool preRendered;

		// Token: 0x040017F9 RID: 6137
		private bool requiresDataBinding;
	}
}
