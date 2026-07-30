using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Serves as the abstract base class for data listing controls, such as <see cref="T:System.Web.UI.WebControls.DataList" /> and <see cref="T:System.Web.UI.WebControls.DataGrid" />. This class provides the methods and properties common to all data listing controls.</summary>
	// Token: 0x02000337 RID: 823
	[Designer("System.Web.UI.Design.WebControls.BaseDataListDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultProperty("DataSource")]
	[DefaultEvent("SelectedIndexChanged")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class BaseDataList : WebControl
	{
		/// <summary>Gets or sets the text to render in an HTML caption element in the control. This property is provided to make the control more accessible to users of assistive technology devices.</summary>
		/// <returns>A string that represents the text to render in an HTML caption element in the control. The default value is an empty string ("").</returns>
		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06001CAF RID: 7343 RVA: 0x00047A03 File Offset: 0x00045C03
		// (set) Token: 0x06001CB0 RID: 7344 RVA: 0x00047A1A File Offset: 0x00045C1A
		[WebCategory("Accessibility")]
		[WebSysDescription("")]
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string Caption
		{
			get
			{
				return this.ViewState.GetString("Caption", string.Empty);
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("Caption");
					return;
				}
				this.ViewState["Caption"] = value;
			}
		}

		/// <summary>Gets or sets the horizontal or vertical position of the HTML caption element in a control. This property is provided to make the control more accessible to users of assistive technology devices.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.TableCaptionAlign" /> enumeration values. The default value is NotSet.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified type is not one of the <see cref="T:System.Web.UI.WebControls.TableCaptionAlign" /> enumeration values. </exception>
		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06001CB1 RID: 7345 RVA: 0x00047A41 File Offset: 0x00045C41
		// (set) Token: 0x06001CB2 RID: 7346 RVA: 0x00047A54 File Offset: 0x00045C54
		[DefaultValue(TableCaptionAlign.NotSet)]
		public virtual TableCaptionAlign CaptionAlign
		{
			get
			{
				return (TableCaptionAlign)this.ViewState.GetInt("CaptionAlign", 0);
			}
			set
			{
				if (value < TableCaptionAlign.NotSet || value > TableCaptionAlign.Right)
				{
					throw new ArgumentOutOfRangeException(global::Locale.GetText("Invalid TableCaptionAlign value."));
				}
				this.ViewState["CaptionAlign"] = value;
			}
		}

		/// <summary>Gets or sets the amount of space between the contents of a cell and the cell's border.</summary>
		/// <returns>The amount of space (in pixels) between the contents of a cell and the cell's border. The default value is -1, which indicates that this property is not set.</returns>
		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06001CB3 RID: 7347 RVA: 0x00047A84 File Offset: 0x00045C84
		// (set) Token: 0x06001CB4 RID: 7348 RVA: 0x00047A9B File Offset: 0x00045C9B
		[WebCategory("Layout")]
		[WebSysDescription("")]
		[DefaultValue(-1)]
		public virtual int CellPadding
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return -1;
				}
				return this.TableStyle.CellPadding;
			}
			set
			{
				this.TableStyle.CellPadding = value;
			}
		}

		/// <summary>Gets or sets the amount of space between cells.</summary>
		/// <returns>The amount of space (in pixels) between cells. The default value is 0.</returns>
		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06001CB5 RID: 7349 RVA: 0x00047AA9 File Offset: 0x00045CA9
		// (set) Token: 0x06001CB6 RID: 7350 RVA: 0x00047AC0 File Offset: 0x00045CC0
		[WebCategory("Layout")]
		[DefaultValue(0)]
		[WebSysDescription("")]
		public virtual int CellSpacing
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return 0;
				}
				return this.TableStyle.CellSpacing;
			}
			set
			{
				this.TableStyle.CellSpacing = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.ControlCollection" /> object that contains a collection of child controls in a data listing control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> that contains a collection of child controls in a data listing control.</returns>
		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06001CB7 RID: 7351 RVA: 0x00047ACE File Offset: 0x00045CCE
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		/// <summary>Gets or sets the key field in the data source specified by the <see cref="P:System.Web.UI.WebControls.BaseDataList.DataSource" /> property.</summary>
		/// <returns>The name of the key field in the data source specified by <see cref="P:System.Web.UI.WebControls.BaseDataList.DataSource" />.</returns>
		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06001CB8 RID: 7352 RVA: 0x00047ADC File Offset: 0x00045CDC
		// (set) Token: 0x06001CB9 RID: 7353 RVA: 0x00047AF3 File Offset: 0x00045CF3
		[DefaultValue("")]
		[Themeable(false)]
		[global::System.MonoTODO("incomplete")]
		[WebCategory("Data")]
		[WebSysDescription("")]
		public virtual string DataKeyField
		{
			get
			{
				return this.ViewState.GetString("DataKeyField", string.Empty);
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("DataKeyField");
					return;
				}
				this.ViewState["DataKeyField"] = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.DataKeyCollection" /> object that stores the key values of each record in a data listing control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.DataKeyCollection" /> that stores the key values of each record in a data listing control.</returns>
		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06001CBA RID: 7354 RVA: 0x00047B1A File Offset: 0x00045D1A
		[WebCategory("Data")]
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DataKeyCollection DataKeys
		{
			get
			{
				if (this.keycoll == null)
				{
					this.keycoll = new DataKeyCollection(this.DataKeysArray);
				}
				return this.keycoll;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ArrayList" /> object that contains the key values of each record in a data listing control.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> that contains the key values of each record in a data listing control.</returns>
		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06001CBB RID: 7355 RVA: 0x00047B3C File Offset: 0x00045D3C
		protected ArrayList DataKeysArray
		{
			get
			{
				ArrayList arrayList = (ArrayList)this.ViewState["DataKeys"];
				if (arrayList == null)
				{
					arrayList = new ArrayList();
					this.ViewState["DataKeys"] = arrayList;
				}
				return arrayList;
			}
		}

		/// <summary>Gets or sets the specific data member in a multimember data source to bind to a data listing control.</summary>
		/// <returns>A data member from a multimember data source. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06001CBC RID: 7356 RVA: 0x00047B7A File Offset: 0x00045D7A
		// (set) Token: 0x06001CBD RID: 7357 RVA: 0x00047B91 File Offset: 0x00045D91
		[WebSysDescription("")]
		[WebCategory("Data")]
		[DefaultValue("")]
		[Themeable(false)]
		public string DataMember
		{
			get
			{
				return this.ViewState.GetString("DataMember", string.Empty);
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("DataMember");
				}
				else
				{
					this.ViewState["DataMember"] = value;
				}
				this.OnDataPropertyChanged();
			}
		}

		/// <summary>Gets or sets the source containing a list of values used to populate the items within the control.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" /> or <see cref="T:System.ComponentModel.IListSource" /> that contains a collection of values used to supply data to this control. The default value is null.</returns>
		/// <exception cref="T:System.Web.HttpException">The data source cannot be resolved because a value is specified for both the <see cref="P:System.Web.UI.WebControls.BaseDataList.DataSource" /> property and the <see cref="P:System.Web.UI.WebControls.BaseDataList.DataSourceID" /> property. </exception>
		/// <exception cref="T:System.ArgumentException">The data source is of an invalid type. The data source must be null or implement either the <see cref="T:System.Collections.IEnumerable" /> or the <see cref="T:System.ComponentModel.IListSource" /> interface.</exception>
		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06001CBE RID: 7358 RVA: 0x00047BBF File Offset: 0x00045DBF
		// (set) Token: 0x06001CBF RID: 7359 RVA: 0x00047BC8 File Offset: 0x00045DC8
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Bindable(true)]
		[Themeable(false)]
		[WebSysDescription("")]
		[WebCategory("Data")]
		public virtual object DataSource
		{
			get
			{
				return this.source;
			}
			set
			{
				if (value == null || value is IEnumerable || value is IListSource)
				{
					this.source = value;
					this.OnDataPropertyChanged();
					return;
				}
				throw new ArgumentException(global::Locale.GetText("Invalid data source. This requires an object implementing {0} or {1}.", new object[] { "IEnumerable", "IListSource" }));
			}
		}

		/// <summary>Gets or sets a value that specifies whether the border between the cells of a data listing control is displayed.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.GridLines" /> values. The default value is Both.</returns>
		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06001CC0 RID: 7360 RVA: 0x00047C1B File Offset: 0x00045E1B
		// (set) Token: 0x06001CC1 RID: 7361 RVA: 0x00047C32 File Offset: 0x00045E32
		[DefaultValue(GridLines.Both)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual GridLines GridLines
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return GridLines.Both;
				}
				return this.TableStyle.GridLines;
			}
			set
			{
				this.TableStyle.GridLines = value;
			}
		}

		/// <summary>Gets or sets the horizontal alignment of a data listing control within its container.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> values. The default value is NotSet.</returns>
		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06001CC2 RID: 7362 RVA: 0x00047C40 File Offset: 0x00045E40
		// (set) Token: 0x06001CC3 RID: 7363 RVA: 0x00047C57 File Offset: 0x00045E57
		[Category("Layout")]
		[DefaultValue(HorizontalAlign.NotSet)]
		[WebSysDescription("")]
		public virtual HorizontalAlign HorizontalAlign
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return HorizontalAlign.NotSet;
				}
				return this.TableStyle.HorizontalAlign;
			}
			set
			{
				this.TableStyle.HorizontalAlign = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the data listing control renders its header in an accessible format. This property is provided to make the control more accessible to users of assistive technology devices.</summary>
		/// <returns>true if the control renders its header in an accessible format; otherwise, false. The default is false.</returns>
		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06001CC4 RID: 7364 RVA: 0x00047C65 File Offset: 0x00045E65
		// (set) Token: 0x06001CC5 RID: 7365 RVA: 0x00047C78 File Offset: 0x00045E78
		[DefaultValue(false)]
		public virtual bool UseAccessibleHeader
		{
			get
			{
				return this.ViewState.GetBool("UseAccessibleHeader", false);
			}
			set
			{
				this.ViewState["UseAccessibleHeader"] = value;
			}
		}

		/// <summary>Gets or sets the <see cref="P:System.Web.UI.Control.ID" /> property of the data source control that the data listing control should use to retrieve its data source.</summary>
		/// <returns>The programmatic identifier assigned to the data source control.</returns>
		/// <exception cref="T:System.Web.HttpException">The data source cannot be resolved because a value is specified for both the <see cref="P:System.Web.UI.WebControls.BaseDataList.DataSource" /> property and the <see cref="P:System.Web.UI.WebControls.BaseDataList.DataSourceID" /> property. </exception>
		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06001CC6 RID: 7366 RVA: 0x0004780E File Offset: 0x00045A0E
		// (set) Token: 0x06001CC7 RID: 7367 RVA: 0x00047C90 File Offset: 0x00045E90
		[Themeable(false)]
		[IDReferenceProperty(typeof(DataSourceControl))]
		[DefaultValue("")]
		public virtual string DataSourceID
		{
			get
			{
				return this.ViewState.GetString("DataSourceID", string.Empty);
			}
			set
			{
				if (this.source != null)
				{
					throw new InvalidOperationException(global::Locale.GetText("DataSource is already set."));
				}
				this.ViewState["DataSourceID"] = value;
				this.OnDataPropertyChanged();
			}
		}

		/// <summary>Gets a value indicating whether the control has been initialized.</summary>
		/// <returns>true if the control has been initialized; otherwise, false.</returns>
		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06001CC8 RID: 7368 RVA: 0x00047CC1 File Offset: 0x00045EC1
		protected bool Initialized
		{
			get
			{
				return this.initialized;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Web.UI.WebControls.BaseDataList.DataSourceID" /> property is set.</summary>
		/// <returns>true if <see cref="P:System.Web.UI.WebControls.BaseDataList.DataSourceID" /> is set to a value other than <see cref="F:System.String.Empty" />; otherwise, false.</returns>
		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06001CC9 RID: 7369 RVA: 0x00047CC9 File Offset: 0x00045EC9
		protected bool IsBoundUsingDataSourceID
		{
			get
			{
				return this.DataSourceID.Length != 0;
			}
		}

		/// <summary>Gets or sets a value indicating whether the data listing control needs to bind to its specified data source.</summary>
		/// <returns>true if the control needs to bind to a data source; otherwise, false.</returns>
		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06001CCA RID: 7370 RVA: 0x00047CD9 File Offset: 0x00045ED9
		// (set) Token: 0x06001CCB RID: 7371 RVA: 0x00047CE1 File Offset: 0x00045EE1
		protected bool RequiresDataBinding
		{
			get
			{
				return this.requiresDataBinding;
			}
			set
			{
				this.requiresDataBinding = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object that the data-bound control uses when retrieving data from a data source control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> used by the data-bound control to retrieve data. The default is to return the value from <see cref="M:System.Web.UI.WebControls.BaseDataList.CreateDataSourceSelectArguments" />.</returns>
		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06001CCC RID: 7372 RVA: 0x00047CEA File Offset: 0x00045EEA
		protected DataSourceSelectArguments SelectArguments
		{
			get
			{
				if (this.selectArguments == null)
				{
					this.selectArguments = this.CreateDataSourceSelectArguments();
				}
				return this.selectArguments;
			}
		}

		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, false.</returns>
		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06001CCD RID: 7373 RVA: 0x0004789D File Offset: 0x00045A9D
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return base.RenderingCompatibilityLessThan40;
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06001CCE RID: 7374 RVA: 0x00047D06 File Offset: 0x00045F06
		private TableStyle TableStyle
		{
			get
			{
				return (TableStyle)base.ControlStyle;
			}
		}

		/// <summary>Notifies the server control that an element, either XML or HTML, was parsed, and adds the element to the server control's <see cref="T:System.Web.UI.ControlCollection" /> collection.</summary>
		/// <param name="obj">An <see cref="T:System.Object" /> that represents the parsed element.</param>
		// Token: 0x06001CCF RID: 7375 RVA: 0x0000393A File Offset: 0x00001B3A
		protected override void AddParsedSubObject(object obj)
		{
		}

		/// <summary>Creates a child control using the view state.</summary>
		// Token: 0x06001CD0 RID: 7376 RVA: 0x00047D13 File Offset: 0x00045F13
		protected internal override void CreateChildControls()
		{
			if (this.HasControls())
			{
				base.Controls.Clear();
			}
			if (this.IsDataBound)
			{
				this.CreateControlHierarchy(false);
				return;
			}
			if (this.RequiresDataBinding)
			{
				this.EnsureDataBound();
			}
		}

		/// <summary>When overridden in a derived class, creates the control hierarchy for the data listing control with or without the specified data source.</summary>
		/// <param name="useDataSource">true to use the control's data source; otherwise, false.</param>
		// Token: 0x06001CD1 RID: 7377
		protected abstract void CreateControlHierarchy(bool useDataSource);

		/// <summary>Binds the control and all its child controls to the specified data source.</summary>
		// Token: 0x06001CD2 RID: 7378 RVA: 0x00047D48 File Offset: 0x00045F48
		public override void DataBind()
		{
			this.OnDataBinding(EventArgs.Empty);
			if (this.HasControls())
			{
				this.Controls.Clear();
			}
			if (base.HasChildViewState)
			{
				base.ClearChildViewState();
			}
			if (!base.IsTrackingViewState)
			{
				this.TrackViewState();
			}
			this.CreateControlHierarchy(true);
			base.ChildControlsCreated = true;
			this.RequiresDataBinding = false;
			this.IsDataBound = true;
		}

		/// <summary>Creates a default <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object used by the data-bound control if no arguments are specified.</summary>
		/// <returns>A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> initialized to <see cref="P:System.Web.UI.DataSourceSelectArguments.Empty" />.</returns>
		// Token: 0x06001CD3 RID: 7379 RVA: 0x00047DAB File Offset: 0x00045FAB
		protected virtual DataSourceSelectArguments CreateDataSourceSelectArguments()
		{
			return DataSourceSelectArguments.Empty;
		}

		/// <summary>Verifies that the data listing control requires data binding and that a valid data source control is specified before calling the <see cref="M:System.Web.UI.WebControls.BaseDataList.DataBind" /> method.</summary>
		// Token: 0x06001CD4 RID: 7380 RVA: 0x00047DB2 File Offset: 0x00045FB2
		protected void EnsureDataBound()
		{
			if (this.IsBoundUsingDataSourceID && this.RequiresDataBinding)
			{
				this.DataBind();
			}
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x00047DCA File Offset: 0x00045FCA
		private void SelectCallback(IEnumerable data)
		{
			this.data = data;
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerable" />-implemented object that represents the data source.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" />-implemented object that represents the data source.</returns>
		// Token: 0x06001CD6 RID: 7382 RVA: 0x00047DD4 File Offset: 0x00045FD4
		protected virtual IEnumerable GetData()
		{
			if (this.DataSourceID.Length == 0)
			{
				return null;
			}
			if (this.boundDataSource == null)
			{
				this.ConnectToDataSource();
			}
			this.boundDataSource.GetView(string.Empty).Select(this.SelectArguments, new DataSourceViewSelectCallback(this.SelectCallback));
			return this.data;
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06001CD7 RID: 7383 RVA: 0x00044915 File Offset: 0x00042B15
		// (set) Token: 0x06001CD8 RID: 7384 RVA: 0x00044928 File Offset: 0x00042B28
		private bool IsDataBound
		{
			get
			{
				return this.ViewState.GetBool("_DataBound", false);
			}
			set
			{
				this.ViewState["_DataBound"] = value;
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.DataBinding" /> event of a <see cref="T:System.Web.UI.WebControls.BaseDataList" /> control.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001CD9 RID: 7385 RVA: 0x00047E2B File Offset: 0x0004602B
		protected override void OnDataBinding(EventArgs e)
		{
			base.OnDataBinding(e);
		}

		/// <summary>Called when one of the base data source identification properties is changed, to rebind the data-bound control to its data.</summary>
		/// <exception cref="T:System.Web.HttpException">An attempt was made to change the property value during the data-binding phase of the control.</exception>
		// Token: 0x06001CDA RID: 7386 RVA: 0x00047E34 File Offset: 0x00046034
		protected virtual void OnDataPropertyChanged()
		{
			if (this.Initialized)
			{
				this.RequiresDataBinding = true;
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.DataSourceView.DataSourceViewChanged" /> event.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001CDB RID: 7387 RVA: 0x00047E45 File Offset: 0x00046045
		protected virtual void OnDataSourceViewChanged(object sender, EventArgs e)
		{
			this.RequiresDataBinding = true;
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event for the <see cref="T:System.Web.UI.WebControls.BaseDataList" /> control.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001CDC RID: 7388 RVA: 0x00047E50 File Offset: 0x00046050
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			Page page = this.Page;
			if (page != null)
			{
				page.PreLoad += this.OnPagePreLoad;
				if (!base.IsViewStateEnabled && page.IsPostBack)
				{
					this.RequiresDataBinding = true;
				}
			}
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x00047E97 File Offset: 0x00046097
		private void OnPagePreLoad(object sender, EventArgs e)
		{
			if (!this.Initialized)
			{
				this.Initialize();
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Load" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001CDE RID: 7390 RVA: 0x00047EA7 File Offset: 0x000460A7
		protected internal override void OnLoad(EventArgs e)
		{
			if (!this.Initialized)
			{
				this.Initialize();
			}
			base.OnLoad(e);
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x00047EC0 File Offset: 0x000460C0
		private void Initialize()
		{
			Page page = this.Page;
			if (page != null && (!page.IsPostBack || (base.IsViewStateEnabled && !this.IsDataBound)))
			{
				this.RequiresDataBinding = true;
			}
			if (this.IsBoundUsingDataSourceID)
			{
				this.ConnectToDataSource();
			}
			this.initialized = true;
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001CE0 RID: 7392 RVA: 0x00047F0B File Offset: 0x0004610B
		protected internal override void OnPreRender(EventArgs e)
		{
			this.EnsureDataBound();
			base.OnPreRender(e);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.BaseDataList.SelectedIndexChanged" /> event of a <see cref="T:System.Web.UI.WebControls.BaseDataList" /> control. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001CE1 RID: 7393 RVA: 0x00047F1C File Offset: 0x0004611C
		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[BaseDataList.selectedIndexChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Sets up the control hierarchy for the data-bound control.</summary>
		// Token: 0x06001CE2 RID: 7394
		protected abstract void PrepareControlHierarchy();

		/// <summary>Renders the control to the specified HTML writer.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client. </param>
		// Token: 0x06001CE3 RID: 7395 RVA: 0x00047F4A File Offset: 0x0004614A
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.PrepareControlHierarchy();
			this.RenderContents(writer);
		}

		/// <summary>Occurs when a different item is selected in a data listing control between posts to the server.</summary>
		// Token: 0x14000047 RID: 71
		// (add) Token: 0x06001CE4 RID: 7396 RVA: 0x00047F59 File Offset: 0x00046159
		// (remove) Token: 0x06001CE5 RID: 7397 RVA: 0x00047F6C File Offset: 0x0004616C
		[WebCategory("Action")]
		[WebSysDescription("")]
		public event EventHandler SelectedIndexChanged
		{
			add
			{
				base.Events.AddHandler(BaseDataList.selectedIndexChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(BaseDataList.selectedIndexChangedEvent, value);
			}
		}

		/// <summary>Determines whether the specified data type can be bound to a list control that derives from the <see cref="T:System.Web.UI.WebControls.BaseDataList" /> class.</summary>
		/// <returns>true if the specified data type can be bound to a list control that derives from the <see cref="T:System.Web.UI.WebControls.BaseDataList" /> class; otherwise, false.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> that contains the data type to test. </param>
		// Token: 0x06001CE6 RID: 7398 RVA: 0x00047F80 File Offset: 0x00046180
		public static bool IsBindableType(Type type)
		{
			if (type == null)
			{
				throw new NullReferenceException();
			}
			TypeCode typeCode = Type.GetTypeCode(type);
			return typeCode - TypeCode.Boolean <= 13 || typeCode == TypeCode.String;
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x00047FB4 File Offset: 0x000461B4
		private void ConnectToDataSource()
		{
			if (this.NamingContainer != null)
			{
				this.boundDataSource = this.NamingContainer.FindControl(this.DataSourceID) as IDataSource;
			}
			if (this.boundDataSource == null)
			{
				if (this.Parent != null)
				{
					this.boundDataSource = this.Parent.FindControl(this.DataSourceID) as IDataSource;
				}
				if (this.boundDataSource == null)
				{
					throw new HttpException(global::Locale.GetText("Coulnd't find a DataSource named '{0}'.", new object[] { this.DataSourceID }));
				}
			}
			this.boundDataSource.GetView(string.Empty).DataSourceViewChanged += this.OnDataSourceViewChanged;
		}

		// Token: 0x040017FA RID: 6138
		private static readonly object selectedIndexChangedEvent = new object();

		// Token: 0x040017FB RID: 6139
		private DataKeyCollection keycoll;

		// Token: 0x040017FC RID: 6140
		private object source;

		// Token: 0x040017FD RID: 6141
		private IDataSource boundDataSource;

		// Token: 0x040017FE RID: 6142
		private bool initialized;

		// Token: 0x040017FF RID: 6143
		private bool requiresDataBinding;

		// Token: 0x04001800 RID: 6144
		private DataSourceSelectArguments selectArguments;

		// Token: 0x04001801 RID: 6145
		private IEnumerable data;
	}
}
