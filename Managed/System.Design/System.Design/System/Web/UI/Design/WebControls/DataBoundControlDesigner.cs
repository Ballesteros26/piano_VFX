using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides a base class for design-time support for controls that derive from <see cref="T:System.Web.UI.WebControls.DataBoundControl" />.</summary>
	// Token: 0x020000CD RID: 205
	public class DataBoundControlDesigner : BaseDataBoundControlDesigner, IDataBindingSchemaProvider, IDataSourceProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.DataBoundControlDesigner" /> class. </summary>
		// Token: 0x060005F5 RID: 1525 RVA: 0x0000973C File Offset: 0x0000793C
		[MonoNotSupported("")]
		public DataBoundControlDesigner()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> object for this designer.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> object associated with this designer.</returns>
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public override DesignerActionListCollection ActionLists
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the shadowed <see cref="P:System.Web.UI.WebControls.DataBoundControl.DataMember" /> property of the underlying data-bound control.</summary>
		/// <returns>The shadowed <see cref="P:System.Web.UI.WebControls.DataBoundControl.DataMember" /> of the underlying data-bound control.</returns>
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060005F8 RID: 1528 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public string DataMember
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
			[MonoNotSupported("")]
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the designer of the data source of the underlying data-bound control.</summary>
		/// <returns>The designer of the data source of the underlying data-bound control.</returns>
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public IDataSourceDesigner DataSourceDesigner
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.Design.DesignerDataSourceView" /> object associated with the data source of this designer.</summary>
		/// <returns>The <see cref="T:System.Web.UI.Design.DesignerDataSourceView" /> object associated with the data source of this designer.</returns>
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public DesignerDataSourceView DesignerView
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the number of rows that the data-bound control displays on the design surface.</summary>
		/// <returns>The number of rows that the data-bound control displays on the design surface.</returns>
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		protected virtual int SampleRowCount
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating whether the designer should include "Choose a data source" in its action list.</summary>
		/// <returns>true.</returns>
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		protected virtual bool UseDataSourcePickerActionList
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Web.UI.Design.WebControls.DataBoundControlDesigner" /> object and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x060005FD RID: 1533 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		protected override void Dispose(bool disposing)
		{
			throw new NotImplementedException();
		}

		/// <summary>Disconnects events from the previous data source and connects them to the current data source.</summary>
		/// <returns>true if the data-bound control connected to a new data source; false if the data source did not change.</returns>
		// Token: 0x060005FE RID: 1534 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		protected override bool ConnectToDataSource()
		{
			throw new NotImplementedException();
		}

		/// <summary>Invokes the standard dialog box to create a new data source control, and sets the new data source control's ID to the <see cref="P:System.Web.UI.WebControls.BaseDataBoundControl.DataSourceID" /> property of the data-bound control.</summary>
		// Token: 0x060005FF RID: 1535 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		protected override void CreateDataSource()
		{
			throw new NotImplementedException();
		}

		/// <summary>Binds the <see cref="T:System.Web.UI.WebControls.DataBoundControl" /> object to the data source.</summary>
		/// <param name="dataBoundControl">The <see cref="T:System.Web.UI.WebControls.DataBoundControl" /> object to bind to the data source.</param>
		// Token: 0x06000600 RID: 1536 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		protected override void DataBind(BaseDataBoundControl dataBoundControl)
		{
			throw new NotImplementedException();
		}

		/// <summary>Disconnects the data-bound control from data source events.</summary>
		// Token: 0x06000601 RID: 1537 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		protected override void DisconnectFromDataSource()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the design-time data source from either the associated DataSourceDesigner or the DataSource property.</summary>
		/// <returns>An object that implements an <see cref="T:System.Collections.IEnumerable" /> interface referencing the design-time data source.</returns>
		// Token: 0x06000602 RID: 1538 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		protected virtual IEnumerable GetDesignTimeDataSource()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets dummy sample data to render the data-bound control on the design surface if sample data cannot be created from the DataSourceDesigner or DataSource properties.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerable" /> interface containing dummy sample data used to render the data-bound control on the design surface.</returns>
		// Token: 0x06000603 RID: 1539 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		protected virtual IEnumerable GetSampleDataSource()
		{
			throw new NotImplementedException();
		}

		/// <summary>Overridden by the designer to shadow run-time properties of the data-bound control with corresponding properties implemented by the designer.</summary>
		/// <param name="properties">An <see cref="T:System.Collections.IDictionary" /> object containing the properties to filter.</param>
		// Token: 0x06000604 RID: 1540 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		protected override void PreFilterProperties(IDictionary properties)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.Design.IDataBindingSchemaProvider.RefreshSchema(System.Boolean)" />. </summary>
		/// <param name="preferSilent">Indicates whether to suppress any events raised while refreshing the schema.</param>
		// Token: 0x06000605 RID: 1541 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		void IDataBindingSchemaProvider.RefreshSchema(bool preferSilent)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.Design.IDataSourceProvider.GetResolvedSelectedDataSource" />.</summary>
		/// <returns>The selected data member from the selected data source, if the control allows the user to select an IListSource object (such as a <see cref="T:System.Data.DataSet" /> object) for the data source, and provides a <see cref="P:System.Web.UI.WebControls.DataBoundControl.DataMember" /> property to select a particular list (or <see cref="T:System.Data.DataTable" /> object) within the data source.</returns>
		// Token: 0x06000606 RID: 1542 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		IEnumerable IDataSourceProvider.GetResolvedSelectedDataSource()
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.Design.IDataSourceProvider.GetSelectedDataSource" />.</summary>
		/// <returns>An object implementing an <see cref="T:System.Collections.IEnumerable" /> interface containing the design-time <see cref="P:System.Web.UI.WebControls.BaseDataBoundControl.DataSource" /> property of the associated control, or null if a data source is not found.</returns>
		// Token: 0x06000607 RID: 1543 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		object IDataSourceProvider.GetSelectedDataSource()
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.Design.IDataBindingSchemaProvider.CanRefreshSchema" />.</summary>
		/// <returns>true if the designer can refresh the data source; otherwise, false.</returns>
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		bool IDataBindingSchemaProvider.CanRefreshSchema
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.Design.IDataBindingSchemaProvider.Schema" />.</summary>
		/// <returns>An <see cref="T:System.Web.UI.Design.IDataSourceViewSchema" /> object that describes the data source.</returns>
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		IDataSourceViewSchema IDataBindingSchemaProvider.Schema
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
