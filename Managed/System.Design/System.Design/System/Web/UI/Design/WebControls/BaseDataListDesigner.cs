using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using Unity;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides a base class for components that provide design-time support in a designer host for Web server controls that are derived from the <see cref="T:System.Web.UI.WebControls.BaseDataList" /> class. </summary>
	// Token: 0x020000C6 RID: 198
	public abstract class BaseDataListDesigner : TemplatedControlDesigner, IDataSourceProvider, IDataBindingSchemaProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.BaseDataListDesigner" /> class.</summary>
		// Token: 0x060005BF RID: 1471 RVA: 0x000096D0 File Offset: 0x000078D0
		public BaseDataListDesigner()
		{
		}

		/// <summary>Gets or sets the value of the data key field of the associated control.</summary>
		/// <returns>The <see cref="P:System.Web.UI.WebControls.BaseDataList.DataKeyField" /> value of the associated control.</returns>
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x000096D8 File Offset: 0x000078D8
		// (set) Token: 0x060005C1 RID: 1473 RVA: 0x000096E0 File Offset: 0x000078E0
		public string DataKeyField
		{
			get
			{
				return this.data_key_field;
			}
			set
			{
				this.data_key_field = value;
			}
		}

		/// <summary>Gets or sets the value of the data member field of the associated control.</summary>
		/// <returns>The <see cref="P:System.Web.UI.WebControls.BaseDataList.DataMember" /> value of the associated control.</returns>
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x000096E9 File Offset: 0x000078E9
		// (set) Token: 0x060005C3 RID: 1475 RVA: 0x000096F1 File Offset: 0x000078F1
		public string DataMember
		{
			get
			{
				return this.data_member;
			}
			set
			{
				this.data_member = value;
			}
		}

		/// <summary>Gets or sets the value of the data source property of the associated control.</summary>
		/// <returns>The <see cref="P:System.Web.UI.WebControls.BaseDataList.DataSource" /> value of the associated control.</returns>
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x000096FA File Offset: 0x000078FA
		// (set) Token: 0x060005C5 RID: 1477 RVA: 0x00009702 File Offset: 0x00007902
		public string DataSource
		{
			get
			{
				return this.data_source;
			}
			set
			{
				this.data_source = value;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0000234B File Offset: 0x0000054B
		public override bool DesignTimeHtmlRequiresLoadComplete
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x0000234B File Offset: 0x0000054B
		public override DesignerVerbCollection Verbs
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Releases the unmanaged resources that are used by the designer and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x060005C8 RID: 1480 RVA: 0x0000234B File Offset: 0x0000054B
		protected override void Dispose(bool disposing)
		{
			throw new NotImplementedException();
		}

		/// <summary>Generates an object that can be used as a data source at design time.</summary>
		/// <returns>An object implementing an <see cref="T:System.Collections.IEnumerable" /> interface that serves as a data source for use at design time.</returns>
		/// <param name="minimumRows">The minimum number of rows of sample data that the data source should contain. </param>
		/// <param name="dummyDataSource">true if the returned data source contains dummy data; false if the returned data source contains data from an actual data source. </param>
		// Token: 0x060005C9 RID: 1481 RVA: 0x0000234B File Offset: 0x0000054B
		protected IEnumerable GetDesignTimeDataSource(int minimumRows, out bool dummyDataSource)
		{
			throw new NotImplementedException();
		}

		/// <summary>Generates an object that can be used as a data source at design time.</summary>
		/// <returns>An object implementing an <see cref="T:System.Collections.IEnumerable" /> interface that serves as a data source for use at design time.</returns>
		/// <param name="selectedDataSource">An object implementing an <see cref="T:System.Collections.IEnumerable" /> that is used as a template for the data format. </param>
		/// <param name="minimumRows">The minimum number of rows of sample data that the data source data should contain. </param>
		/// <param name="dummyDataSource">true if the returned data source contains dummy data; false if the returned data source contains data from an actual data source. </param>
		// Token: 0x060005CA RID: 1482 RVA: 0x0000234B File Offset: 0x0000054B
		protected IEnumerable GetDesignTimeDataSource(IEnumerable selectedDataSource, int minimumRows, out bool dummyDataSource)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the data source component from the associated control container, resolved to a specific data member.</summary>
		/// <returns>An object implementing an <see cref="T:System.Collections.IEnumerable" /> interface containing the design-time <see cref="P:System.Web.UI.WebControls.BaseDataList.DataSource" /> of the associated control, resolved to the <see cref="P:System.Web.UI.WebControls.BaseDataList.DataMember" /> parameter; otherwise, null if a data source is not found.</returns>
		// Token: 0x060005CB RID: 1483 RVA: 0x0000234B File Offset: 0x0000054B
		public virtual IEnumerable GetResolvedSelectedDataSource()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the data source component from the associated control container.</summary>
		/// <returns>An object implementing an <see cref="T:System.Collections.IEnumerable" /> interface containing the design-time <see cref="P:System.Web.UI.WebControls.BaseDataList.DataSource" /> property of the associated control; otherwise, null if a data source is not found.</returns>
		// Token: 0x060005CC RID: 1484 RVA: 0x0000234B File Offset: 0x0000054B
		public virtual object GetSelectedDataSource()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the data source of the template's container.</summary>
		/// <returns>An object that implements an <see cref="T:System.Collections.IEnumerable" /> interface containing a design-time <see cref="P:System.Web.UI.WebControls.BaseDataList.DataSource" /> property.</returns>
		/// <param name="templateName">A <see cref="T:System.String" /> that specifies the name of the template for which to get the data source.</param>
		// Token: 0x060005CD RID: 1485 RVA: 0x0000234B File Offset: 0x0000054B
		public override IEnumerable GetTemplateContainerDataSource(string templateName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Prepares the designer to view, edit, and design the associated control.</summary>
		/// <param name="component">A control derived from the <see cref="T:System.Web.UI.WebControls.BaseDataList" />, which implements an <see cref="T:System.ComponentModel.IComponent" />. </param>
		// Token: 0x060005CE RID: 1486 RVA: 0x0000234B File Offset: 0x0000054B
		public override void Initialize(IComponent component)
		{
			throw new NotImplementedException();
		}

		/// <summary>Invokes a component editor for the associated control.</summary>
		/// <param name="initialPage">The index of the page with which to initialize the component editor. </param>
		// Token: 0x060005CF RID: 1487 RVA: 0x0000234B File Offset: 0x0000054B
		protected internal void InvokePropertyBuilder(int initialPage)
		{
			throw new NotImplementedException();
		}

		/// <summary>Handles the AutoFormat event.</summary>
		/// <param name="sender">The <see cref="T:System.Object" /> that raised the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x060005D0 RID: 1488 RVA: 0x0000234B File Offset: 0x0000054B
		protected void OnAutoFormat(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called when there is a change to the associated control.</summary>
		/// <param name="sender">The <see cref="T:System.Object" /> that is the source of the event.</param>
		/// <param name="e">A <see cref="T:System.ComponentModel.Design.ComponentChangedEventArgs" /> that contains the event data.</param>
		// Token: 0x060005D1 RID: 1489 RVA: 0x0000234B File Offset: 0x0000054B
		public override void OnComponentChanged(object sender, ComponentChangedEventArgs e)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called when the data source for the associated control has changed.</summary>
		// Token: 0x060005D2 RID: 1490 RVA: 0x0000234B File Offset: 0x0000054B
		protected internal virtual void OnDataSourceChanged()
		{
			throw new NotImplementedException();
		}

		/// <summary>Represents the method that handles the property-builder event.</summary>
		/// <param name="sender">An <see cref="T:System.Object" /> that is the source of the event. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x060005D3 RID: 1491 RVA: 0x0000234B File Offset: 0x0000054B
		protected void OnPropertyBuilder(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		/// <summary>Can be overridden to implement functionality that should occur when a style of the associated control has changed.</summary>
		// Token: 0x060005D4 RID: 1492 RVA: 0x0000234B File Offset: 0x0000054B
		protected internal void OnStylesChanged()
		{
			throw new NotImplementedException();
		}

		/// <summary>Can be overridden to implement functionality that should occur when the designer template-editing verbs have changed.</summary>
		// Token: 0x060005D5 RID: 1493
		protected abstract void OnTemplateEditingVerbsChanged();

		/// <summary>Used by the designer to remove properties from or add additional properties to the display in the Properties grid or to shadow properties of the associated control. </summary>
		/// <param name="properties">A collection implementing an <see cref="T:System.Collections.IDictionary" /> interface of the added and shadowed properties. </param>
		// Token: 0x060005D6 RID: 1494 RVA: 0x0000234B File Offset: 0x0000054B
		protected override void PreFilterProperties(IDictionary properties)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the designer of the data source, when one is selected for data binding.</summary>
		/// <returns>The designer for the data source of the associated control.</returns>
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x0000970B File Offset: 0x0000790B
		public IDataSourceDesigner DataSourceDesigner
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the designer's version of the data source ID property and is used to shadow the corresponding property of the associated control.</summary>
		/// <returns>A <see cref="T:System.String" /> that is the data source ID of the associated control.</returns>
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x0000970B File Offset: 0x0000790B
		// (set) Token: 0x060005D9 RID: 1497 RVA: 0x00009519 File Offset: 0x00007719
		public string DataSourceID
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

		/// <summary>Gets the default view of the data source that is bound to the associated control. </summary>
		/// <returns>The <see cref="T:System.Web.UI.Design.DesignerDataSourceView" /> representing the default view of the data source. </returns>
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x0000970B File Offset: 0x0000790B
		public DesignerDataSourceView DesignerView
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00009714 File Offset: 0x00007914
		bool IDataBindingSchemaProvider.get_CanRefreshSchema()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0000970B File Offset: 0x0000790B
		IDataSourceViewSchema IDataBindingSchemaProvider.get_Schema()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Called when the schema of the data source for the associated control changes.</summary>
		// Token: 0x060005DD RID: 1501 RVA: 0x00009519 File Offset: 0x00007719
		protected virtual void OnSchemaRefreshed()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.Design.IDataBindingSchemaProvider.RefreshSchema(System.Boolean)" />.</summary>
		/// <param name="preferSilent">true to disable data-binding events until after the schema has been refreshed; false to enable the events.</param>
		// Token: 0x060005DE RID: 1502 RVA: 0x00009519 File Offset: 0x00007719
		void IDataBindingSchemaProvider.RefreshSchema(bool preferSilent)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400014C RID: 332
		private string data_key_field;

		// Token: 0x0400014D RID: 333
		private string data_member;

		// Token: 0x0400014E RID: 334
		private string data_source;
	}
}
