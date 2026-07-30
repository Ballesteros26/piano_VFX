using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.UI.WebControls.Adapters;
using System.Web.Util;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Serves as the base class for all ASP.NET version 2.0 data-bound controls that display their data in list or tabular form.</summary>
	// Token: 0x0200036B RID: 875
	[Designer("System.Web.UI.Design.WebControls.DataBoundControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class DataBoundControl : BaseDataBoundControl
	{
		/// <summary>Initializes the <see cref="T:System.Web.UI.WebControls.DataBoundControl" /> class for use by an inherited class instance. This constructor can only be called by an inherited class.</summary>
		// Token: 0x060020D3 RID: 8403 RVA: 0x0005464A File Offset: 0x0005284A
		protected DataBoundControl()
		{
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x00054652 File Offset: 0x00052852
		internal DataBoundControl(HtmlTextWriterTag tag)
			: base(tag)
		{
		}

		/// <summary>Retrieves the <see cref="T:System.Web.UI.IDataSource" /> interface that the data-bound control is associated with, if any.</summary>
		/// <returns>An <see cref="T:System.Web.UI.IDataSource" /> that represents the data source identified by <see cref="P:System.Web.UI.WebControls.DataBoundControl.DataSourceID" />. </returns>
		/// <exception cref="T:System.Web.HttpException">The control identified by the <see cref="P:System.Web.UI.WebControls.DataBoundControl.DataSourceID" /> property does not exist in the current container.- or -The control identified by the <see cref="P:System.Web.UI.WebControls.DataBoundControl.DataSourceID" /> property does not implement the <see cref="T:System.Web.UI.IDataSource" /> interface.</exception>
		// Token: 0x060020D5 RID: 8405 RVA: 0x0005465C File Offset: 0x0005285C
		protected virtual IDataSource GetDataSource()
		{
			if (base.IsBoundUsingDataSourceID)
			{
				Control control = base.FindDataSource();
				if (control == null)
				{
					throw new HttpException(string.Format("A control with ID '{0}' could not be found.", this.DataSourceID));
				}
				if (!(control is IDataSource))
				{
					throw new HttpException(string.Format("The control with ID '{0}' is not a control of type IDataSource.", this.DataSourceID));
				}
				return (IDataSource)control;
			}
			else
			{
				IDataSource dataSource = this.DataSource as IDataSource;
				if (dataSource != null)
				{
					return dataSource;
				}
				return new CollectionDataSource(DataSourceResolver.ResolveDataSource(this.DataSource, this.DataMember));
			}
		}

		/// <summary>Retrieves a <see cref="T:System.Web.UI.DataSourceView" /> object that the data-bound control uses to perform data operations.</summary>
		/// <returns>The <see cref="T:System.Web.UI.DataSourceView" /> that the data-bound control uses to perform data operations. If the <see cref="P:System.Web.UI.WebControls.DataBoundControl.DataMember" /> property is set, a specific, named <see cref="T:System.Web.UI.DataSourceView" /> is returned; otherwise, the default <see cref="T:System.Web.UI.DataSourceView" /> is returned.</returns>
		/// <exception cref="T:System.InvalidOperationException">Both the <see cref="P:System.Web.UI.WebControls.BaseDataBoundControl.DataSource" /> and <see cref="P:System.Web.UI.WebControls.BaseDataBoundControl.DataSourceID" /> properties are set.- or -The <see cref="P:System.Web.UI.WebControls.DataBoundControl.DataMember" /> property is set but a <see cref="T:System.Web.UI.DataSourceView" /> object by that name does not exist.</exception>
		// Token: 0x060020D6 RID: 8406 RVA: 0x000546DB File Offset: 0x000528DB
		protected virtual DataSourceView GetData()
		{
			if (this.currentView == null)
			{
				this.UpdateViewData();
			}
			return this.currentView;
		}

		// Token: 0x060020D7 RID: 8407 RVA: 0x000546F4 File Offset: 0x000528F4
		private DataSourceView InternalGetData()
		{
			if (this.currentView != null)
			{
				return this.currentView;
			}
			if (this.DataSource != null && base.IsBoundUsingDataSourceID)
			{
				throw new HttpException("Control bound using both DataSourceID and DataSource properties.");
			}
			IDataSource dataSource = this.GetDataSource();
			if (dataSource != null)
			{
				return dataSource.GetView(this.DataMember);
			}
			return null;
		}

		/// <summary>Rebinds the data-bound control to its data after one of the base data source identification properties changes.</summary>
		// Token: 0x060020D8 RID: 8408 RVA: 0x00054743 File Offset: 0x00052943
		protected override void OnDataPropertyChanged()
		{
			base.OnDataPropertyChanged();
			this.currentView = null;
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.DataSourceView.DataSourceViewChanged" /> event.</summary>
		/// <param name="sender">The source of the event, the <see cref="T:System.Web.UI.DataSourceView" />.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data.</param>
		// Token: 0x060020D9 RID: 8409 RVA: 0x00054752 File Offset: 0x00052952
		protected virtual void OnDataSourceViewChanged(object sender, EventArgs e)
		{
			base.RequiresDataBinding = true;
		}

		/// <summary>Sets the initialized state of the data-bound control before the control is loaded.</summary>
		/// <param name="sender">The <see cref="T:System.Web.UI.Page" /> that raised the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060020DA RID: 8410 RVA: 0x0005475B File Offset: 0x0005295B
		protected override void OnPagePreLoad(object sender, EventArgs e)
		{
			base.OnPagePreLoad(sender, e);
			this.Initialize();
		}

		// Token: 0x060020DB RID: 8411 RVA: 0x0005476C File Offset: 0x0005296C
		private void Initialize()
		{
			Page page = this.Page;
			if (page != null && !this.IsDataBound)
			{
				if (!page.IsPostBack)
				{
					base.RequiresDataBinding = true;
					return;
				}
				if (base.IsViewStateEnabled)
				{
					base.RequiresDataBinding = true;
				}
			}
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x000547AC File Offset: 0x000529AC
		private void UpdateViewData()
		{
			if (this.currentView != null)
			{
				this.currentView.DataSourceViewChanged -= this.OnDataSourceViewChanged;
			}
			DataSourceView dataSourceView = this.InternalGetData();
			if (dataSourceView != this.currentView)
			{
				this.currentView = dataSourceView;
			}
			if (this.currentView != null)
			{
				this.currentView.DataSourceViewChanged += this.OnDataSourceViewChanged;
			}
		}

		/// <summary>Handles the <see cref="E:System.Web.UI.Control.Load" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains event data.</param>
		// Token: 0x060020DD RID: 8413 RVA: 0x00054810 File Offset: 0x00052A10
		protected internal override void OnLoad(EventArgs e)
		{
			this.UpdateViewData();
			if (!base.Initialized)
			{
				this.Initialize();
				base.ConfirmInitState();
			}
			base.OnLoad(e);
		}

		/// <summary>When overridden in a derived class, binds data from the data source to the control. </summary>
		/// <param name="data">The <see cref="T:System.Collections.IEnumerable" /> list of data returned from a <see cref="M:System.Web.UI.WebControls.DataBoundControl.PerformSelect" /> method call.</param>
		// Token: 0x060020DE RID: 8414 RVA: 0x0000393A File Offset: 0x00001B3A
		protected internal virtual void PerformDataBinding(IEnumerable data)
		{
		}

		/// <summary>Verifies that the object a data-bound control binds to is one it can work with.</summary>
		/// <param name="dataSource">An object set to the <see cref="P:System.Web.UI.WebControls.BaseDataBoundControl.DataSource" /> property.</param>
		/// <exception cref="T:System.InvalidOperationException">The object passed by the <paramref name="dataSource" /> parameter is not null or a recognized type.</exception>
		// Token: 0x060020DF RID: 8415 RVA: 0x00054833 File Offset: 0x00052A33
		protected override void ValidateDataSource(object dataSource)
		{
			if (dataSource == null || dataSource is IListSource || dataSource is IEnumerable || dataSource is IDataSource)
			{
				return;
			}
			throw new ArgumentException("Invalid data source source type. The data source must be of type IListSource, IEnumerable or IDataSource.");
		}

		/// <summary>Gets or sets the name of the list of data that the data-bound control binds to, in cases where the data source contains more than one distinct list of data items.</summary>
		/// <returns>The name of the specific list of data that the data-bound control binds to, if more than one list is supplied by a data source control. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x060020E0 RID: 8416 RVA: 0x00047B7A File Offset: 0x00045D7A
		// (set) Token: 0x060020E1 RID: 8417 RVA: 0x0005485B File Offset: 0x00052A5B
		[DefaultValue("")]
		[WebCategory("Data")]
		[Themeable(false)]
		public virtual string DataMember
		{
			get
			{
				return this.ViewState.GetString("DataMember", string.Empty);
			}
			set
			{
				this.ViewState["DataMember"] = value;
			}
		}

		/// <summary>Gets or sets the ID of the control from which the data-bound control retrieves its list of data items.</summary>
		/// <returns>The ID of a control that represents the data source from which the data-bound control retrieves its data.</returns>
		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x060020E2 RID: 8418 RVA: 0x0004780E File Offset: 0x00045A0E
		// (set) Token: 0x060020E3 RID: 8419 RVA: 0x0005486E File Offset: 0x00052A6E
		[IDReferenceProperty(typeof(DataSourceControl))]
		public override string DataSourceID
		{
			get
			{
				return this.ViewState.GetString("DataSourceID", string.Empty);
			}
			set
			{
				this.ViewState["DataSourceID"] = value;
				base.DataSourceID = value;
			}
		}

		/// <summary>Gets an object that implements the <see cref="T:System.Web.UI.IDataSource" /> interface, which provides access to the object's data content.</summary>
		/// <returns>An object with access to its data content.</returns>
		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x060020E4 RID: 8420 RVA: 0x00054888 File Offset: 0x00052A88
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IDataSource DataSourceObject
		{
			get
			{
				return this.GetDataSource();
			}
		}

		/// <summary>Retrieves data from the associated data source.</summary>
		// Token: 0x060020E5 RID: 8421 RVA: 0x00054890 File Offset: 0x00052A90
		protected override void PerformSelect()
		{
			if (!base.IsBoundUsingDataSourceID)
			{
				this.OnDataBinding(EventArgs.Empty);
			}
			base.RequiresDataBinding = false;
			this.SelectArguments = this.CreateDataSourceSelectArguments();
			this.GetData().Select(this.SelectArguments, new DataSourceViewSelectCallback(this.OnSelect));
			this.MarkAsDataBound();
			this.OnDataBound(EventArgs.Empty);
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x000548F1 File Offset: 0x00052AF1
		private void OnSelect(IEnumerable data)
		{
			if (base.IsBoundUsingDataSourceID)
			{
				this.OnDataBinding(EventArgs.Empty);
			}
			this.InternalPerformDataBinding(data);
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x00054910 File Offset: 0x00052B10
		internal void InternalPerformDataBinding(IEnumerable data)
		{
			DataBoundControlAdapter dataBoundControlAdapter = base.Adapter as DataBoundControlAdapter;
			if (dataBoundControlAdapter != null)
			{
				dataBoundControlAdapter.PerformDataBinding(data);
				return;
			}
			this.PerformDataBinding(data);
		}

		/// <summary>Creates a default <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object used by the data-bound control if no arguments are specified.</summary>
		/// <returns>A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> initialized to <see cref="P:System.Web.UI.DataSourceSelectArguments.Empty" />. </returns>
		// Token: 0x060020E8 RID: 8424 RVA: 0x00047DAB File Offset: 0x00045FAB
		protected virtual DataSourceSelectArguments CreateDataSourceSelectArguments()
		{
			return DataSourceSelectArguments.Empty;
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object that the data-bound control uses when retrieving data from a data source control. </summary>
		/// <returns>A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> value used by the data-bound control to retrieve data. The default is <see cref="P:System.Web.UI.DataSourceSelectArguments.Empty" />. </returns>
		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x060020E9 RID: 8425 RVA: 0x0005493B File Offset: 0x00052B3B
		// (set) Token: 0x060020EA RID: 8426 RVA: 0x00054957 File Offset: 0x00052B57
		private protected DataSourceSelectArguments SelectArguments
		{
			protected get
			{
				if (this.selectArguments == null)
				{
					this.selectArguments = this.CreateDataSourceSelectArguments();
				}
				return this.selectArguments;
			}
			private set
			{
				this.selectArguments = value;
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x060020EB RID: 8427 RVA: 0x00054960 File Offset: 0x00052B60
		// (set) Token: 0x060020EC RID: 8428 RVA: 0x00054989 File Offset: 0x00052B89
		private bool IsDataBound
		{
			get
			{
				object obj = this.ViewState["DataBound"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["DataBound"] = value;
			}
		}

		/// <summary>Sets the state of the control in view state as successfully bound to data.</summary>
		// Token: 0x060020ED RID: 8429 RVA: 0x000549A1 File Offset: 0x00052BA1
		protected void MarkAsDataBound()
		{
			this.IsDataBound = true;
		}

		/// <summary>Gets a value that indicates whether model binding is in use.</summary>
		/// <returns>true if model binding is in use; otherwise, false.</returns>
		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x060020EE RID: 8430 RVA: 0x000549AC File Offset: 0x00052BAC
		protected override bool IsUsingModelBinders
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets or sets the name of the data item type for strongly typed data binding.</summary>
		/// <returns>The name of the model type.</returns>
		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x060020EF RID: 8431 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x060020F0 RID: 8432 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string ItemType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>The name of the method to call in order to read data.</summary>
		/// <returns>The name of the method.</returns>
		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x060020F1 RID: 8433 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x060020F2 RID: 8434 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string SelectMethod
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Occurs when data methods are being called.</summary>
		// Token: 0x14000064 RID: 100
		// (add) Token: 0x060020F3 RID: 8435 RVA: 0x0000B3E4 File Offset: 0x000095E4
		// (remove) Token: 0x060020F4 RID: 8436 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public event CallingDataMethodsEventHandler CallingDataMethods
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Web.UI.WebControls.ModelDataSource" /> object is being created.</summary>
		// Token: 0x14000065 RID: 101
		// (add) Token: 0x060020F5 RID: 8437 RVA: 0x0000B3E4 File Offset: 0x000095E4
		// (remove) Token: 0x060020F6 RID: 8438 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public event CreatingModelDataSourceEventHandler CreatingModelDataSource
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataBoundControl.CreatingModelDataSource" /> event.</summary>
		/// <param name="e">An object that provides access to the <see cref="T:System.Web.UI.WebControls.ModelDataSource" /> object that is being created.</param>
		// Token: 0x060020F7 RID: 8439 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void OnCreatingModelDataSource(CreatingModelDataSourceEventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040018AF RID: 6319
		private DataSourceSelectArguments selectArguments;

		// Token: 0x040018B0 RID: 6320
		private DataSourceView currentView;
	}
}
