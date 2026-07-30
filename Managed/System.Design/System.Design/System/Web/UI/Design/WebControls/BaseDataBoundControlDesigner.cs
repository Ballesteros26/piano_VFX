using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides design-time support in a visual designer for controls that are derived from the <see cref="T:System.Web.UI.WebControls.BaseDataBoundControl" /> class.</summary>
	// Token: 0x020000C4 RID: 196
	public abstract class BaseDataBoundControlDesigner : ControlDesigner
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.BaseDataBoundControlDesigner" /> class.</summary>
		// Token: 0x060005AA RID: 1450 RVA: 0x000092B3 File Offset: 0x000074B3
		[MonoNotSupported("")]
		protected BaseDataBoundControlDesigner()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets or sets the value of the <see cref="P:System.Web.UI.WebControls.BaseDataBoundControl.DataSource" /> property for the associated control.</summary>
		/// <returns>The data-binding expression used by the associated control derived from <see cref="T:System.Web.UI.WebControls.BaseDataBoundControl" />.</returns>
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060005AC RID: 1452 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public string DataSource
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

		/// <summary>Gets or sets the value of the <see cref="P:System.Web.UI.WebControls.BaseDataBoundControl.DataSourceID" /> property of the underlying <see cref="T:System.Web.UI.WebControls.BaseDataBoundControl" /> object.</summary>
		/// <returns>The ID of the <see cref="T:System.Web.UI.DataSourceControl" /> associated with the underlying <see cref="T:System.Web.UI.WebControls.BaseDataBoundControl" />.</returns>
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060005AE RID: 1454 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public string DataSourceID
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

		/// <summary>Releases the unmanaged resources that are used by the <see cref="T:System.Web.UI.Design.WebControls.BaseDataBoundControlDesigner" /> object, and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x060005AF RID: 1455 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		protected override void Dispose(bool disposing)
		{
			throw new NotImplementedException();
		}

		/// <summary>Generates the markup that is used to render the control at design time. </summary>
		/// <returns>The markup used to render the control at design time.</returns>
		// Token: 0x060005B0 RID: 1456 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public override string GetDesignTimeHtml()
		{
			throw new NotImplementedException();
		}

		/// <summary>Prepares the designer to view, edit, and design the associated control.</summary>
		/// <param name="component">A control derived from <see cref="T:System.Web.UI.WebControls.BaseDataBoundControl" />, which implements <see cref="T:System.ComponentModel.IComponent" />.</param>
		// Token: 0x060005B1 RID: 1457 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public override void Initialize(IComponent component)
		{
			throw new NotImplementedException();
		}

		/// <summary>In a design host, such as Visual Studio 2005, displays a dialog box to assist the user in creating a data source.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DialogResult" /> object.</returns>
		/// <param name="controlDesigner">A reference to this designer.</param>
		/// <param name="dataSourceType">The type of data source.</param>
		/// <param name="configure">true to enable editing of the configuration, or false to disable configuration editing.</param>
		/// <param name="dataSourceID">The ID of a <see cref="T:System.Web.UI.DataSourceControl" /> control on the page.</param>
		// Token: 0x060005B2 RID: 1458 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public static DialogResult ShowCreateDataSourceDialog(ControlDesigner controlDesigner, Type dataSourceType, bool configure, out string dataSourceID)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, typically unhooks events from the previous data source, and then attaches new events to the new data source. </summary>
		/// <returns>true if a connection to a new data source was performed, typically; false if the old and new data sources are the same.</returns>
		// Token: 0x060005B3 RID: 1459
		protected abstract bool ConnectToDataSource();

		/// <summary>When overridden in a derived class, creates a new data source for the associated <see cref="T:System.Web.UI.WebControls.BaseDataBoundControl" /> object.</summary>
		// Token: 0x060005B4 RID: 1460
		protected abstract void CreateDataSource();

		/// <summary>When overridden in a derived class, performs the necessary actions to set up the associated control that is derived from the <see cref="T:System.Web.UI.WebControls.BaseDataBoundControl" /> class.</summary>
		/// <param name="dataBoundControl">The <see cref="T:System.Web.UI.WebControls.BaseDataBoundControl" /> with which this designer is associated.</param>
		// Token: 0x060005B5 RID: 1461
		protected abstract void DataBind(BaseDataBoundControl dataBoundControl);

		/// <summary>When overridden in a derived class, unhooks events from the current data source. </summary>
		// Token: 0x060005B6 RID: 1462
		protected abstract void DisconnectFromDataSource();

		/// <summary>Provides the markup that is used to render the control at design time if the control is empty or if the data source cannot be retrieved. </summary>
		/// <returns>The markup used to render the control at design time with an empty data source.</returns>
		// Token: 0x060005B7 RID: 1463 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		protected override string GetEmptyDesignTimeHtml()
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides the markup that is used to render the control at design time when an error has occurred.</summary>
		/// <returns>The markup used to render the control at design time when an error has occurred.</returns>
		/// <param name="e">The <see cref="T:System.Exception" /> that was thrown.</param>
		// Token: 0x060005B8 RID: 1464 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		protected override string GetErrorDesignTimeHtml(Exception e)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called when the data source of the associated <see cref="T:System.Web.UI.WebControls.BaseDataBoundControl" /> object changes. </summary>
		/// <param name="forceUpdateView">true to force the update of design-time markup; otherwise, false.</param>
		// Token: 0x060005B9 RID: 1465 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		protected virtual void OnDataSourceChanged(bool forceUpdateView)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called when the data source of the associated <see cref="T:System.Web.UI.WebControls.BaseDataBoundControl" /> object loads a new schema. </summary>
		// Token: 0x060005BA RID: 1466 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		protected virtual void OnSchemaRefreshed()
		{
			throw new NotImplementedException();
		}

		/// <summary>Used by the designer to remove or add additional properties for display in the Properties grid or to shadow properties of the associated control.</summary>
		/// <param name="properties">The <see cref="T:System.Collections.IDictionary" /> containing the properties to filter.</param>
		// Token: 0x060005BB RID: 1467 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		protected override void PreFilterProperties(IDictionary properties)
		{
			throw new NotImplementedException();
		}
	}
}
