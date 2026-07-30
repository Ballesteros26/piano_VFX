using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides design-time support in a designer host for the <see cref="T:System.Web.UI.WebControls.HierarchicalDataBoundControl" /> control.</summary>
	// Token: 0x020000D1 RID: 209
	public class HierarchicalDataBoundControlDesigner : BaseDataBoundControlDesigner
	{
		/// <summary>Gets the designer action list collection for this designer.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> associated with this designer.</returns>
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x0000234B File Offset: 0x0000054B
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Provides access to the designer of the data source, when one is selected for data binding.</summary>
		/// <returns>The designer for the data source of the associated control derived from the <see cref="T:System.Web.UI.WebControls.HierarchicalDataBoundControl" />, which implements the <see cref="T:System.Web.UI.Design.IHierarchicalDataSourceDesigner" />.</returns>
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x0000234B File Offset: 0x0000054B
		public IHierarchicalDataSourceDesigner DataSourceDesigner
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the default view of the data source that is bound to the associated control. </summary>
		/// <returns>The <see cref="T:System.Web.UI.Design.DesignerHierarchicalDataSourceView" /> representing the default view of the data source. </returns>
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x0000234B File Offset: 0x0000054B
		public DesignerHierarchicalDataSourceView DesignerView
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value that indicates whether the control should render its default action lists, which contain a data source ID drop-down list and related tasks.</summary>
		/// <returns>Always true.</returns>
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x0000234B File Offset: 0x0000054B
		protected virtual bool UseDataSourcePickerActionList
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Performs the actions that are necessary to connect to the current data source.</summary>
		/// <returns>true if a connection to a new data source was performed; false if the old and new data source are the same.</returns>
		// Token: 0x0600061E RID: 1566 RVA: 0x0000234B File Offset: 0x0000054B
		protected override bool ConnectToDataSource()
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a new data source for the associated control.</summary>
		// Token: 0x0600061F RID: 1567 RVA: 0x0000234B File Offset: 0x0000054B
		protected override void CreateDataSource()
		{
			throw new NotImplementedException();
		}

		/// <summary>Binds the associated control to the design-time data source.</summary>
		/// <param name="dataBoundControl">The <see cref="T:System.Web.UI.WebControls.HierarchicalDataBoundControl" /> to bind to the design-time data source.</param>
		// Token: 0x06000620 RID: 1568 RVA: 0x0000234B File Offset: 0x0000054B
		protected override void DataBind(BaseDataBoundControl dataBoundControl)
		{
			throw new NotImplementedException();
		}

		/// <summary>Performs the actions that are necessary to disconnect from the current data source.</summary>
		// Token: 0x06000621 RID: 1569 RVA: 0x0000234B File Offset: 0x0000054B
		protected override void DisconnectFromDataSource()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a data source that can be used at design time for the associated control.</summary>
		/// <returns>An object implementing the <see cref="T:System.Web.UI.IHierarchicalEnumerable" /> interface that can be used as a data source for controls derived from the <see cref="T:System.Web.UI.WebControls.HierarchicalDataBoundControl" />.</returns>
		// Token: 0x06000622 RID: 1570 RVA: 0x0000234B File Offset: 0x0000054B
		protected virtual IHierarchicalEnumerable GetDesignTimeDataSource()
		{
			throw new NotImplementedException();
		}

		/// <summary>Constructs a sample data source that can be used at design time for the associated control.</summary>
		/// <returns>An object implementing the <see cref="T:System.Web.UI.IHierarchicalEnumerable" /> interface that can be used as a data source for controls derived from <see cref="T:System.Web.UI.WebControls.HierarchicalDataBoundControl" />.</returns>
		// Token: 0x06000623 RID: 1571 RVA: 0x0000234B File Offset: 0x0000054B
		protected virtual IHierarchicalEnumerable GetSampleDataSource()
		{
			throw new NotImplementedException();
		}

		/// <summary>Used by the designer to remove properties from or add additional properties to the display in the Properties grid or to shadow properties of the associated control.</summary>
		/// <param name="properties">A collection implementing the <see cref="T:System.Collections.IDictionary" /> of the added and shadowed properties. </param>
		// Token: 0x06000624 RID: 1572 RVA: 0x0000234B File Offset: 0x0000054B
		protected override void PreFilterProperties(IDictionary properties)
		{
			throw new NotImplementedException();
		}
	}
}
