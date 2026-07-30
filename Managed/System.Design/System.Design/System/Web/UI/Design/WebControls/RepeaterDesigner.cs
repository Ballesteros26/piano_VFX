using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using Unity;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Extends design-time behavior for the <see cref="T:System.Web.UI.WebControls.Repeater" /> control.</summary>
	// Token: 0x020000DA RID: 218
	public class RepeaterDesigner : ControlDesigner, IDataSourceProvider
	{
		/// <summary>Gets or sets the name of a specific table or view in the data source object to bind the <see cref="T:System.Web.UI.WebControls.Repeater" /> control to.</summary>
		/// <returns>The name of a table or view in the data source.</returns>
		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x000098A2 File Offset: 0x00007AA2
		// (set) Token: 0x06000654 RID: 1620 RVA: 0x000098AA File Offset: 0x00007AAA
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

		/// <summary>A data-binding expression that identifies the source of data for the associated <see cref="T:System.Web.UI.WebControls.Repeater" /> control.</summary>
		/// <returns>A data binding expression.</returns>
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x000098B3 File Offset: 0x00007AB3
		// (set) Token: 0x06000656 RID: 1622 RVA: 0x000098BB File Offset: 0x00007ABB
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

		/// <summary>Gets a value indicating whether the associated control has any templates defined.</summary>
		/// <returns>A value that indicates whether any templates are defined for the associated control.</returns>
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x0000234B File Offset: 0x0000054B
		protected bool TemplatesExist
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Web.UI.Design.WebControls.RepeaterDesigner" /> object and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both the managed and unmanaged resources; false to release only the unmanaged resources.</param>
		// Token: 0x06000658 RID: 1624 RVA: 0x0000234B File Offset: 0x0000054B
		protected override void Dispose(bool disposing)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns design-time sample data based on the <see cref="M:System.Web.UI.Design.WebControls.RepeaterDesigner.GetResolvedSelectedDataSource" /> method and using the specified number of rows.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" /> object containing sample data for use at design time.</returns>
		/// <param name="minimumRows">The minimum number of rows of sample data that the data source should contain. </param>
		// Token: 0x06000659 RID: 1625 RVA: 0x0000234B File Offset: 0x0000054B
		protected IEnumerable GetDesignTimeDataSource(int minimumRows)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns design-time sample data based on the provided data and using the specified number of rows.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" /> object containing sample data for use at design time.</returns>
		/// <param name="selectedDataSource">An <see cref="T:System.Collections.IEnumerable" /> object containing data to use in creating similar sample data at design time.</param>
		/// <param name="minimumRows">The minimum number of rows of sample data that the data source should contain. </param>
		// Token: 0x0600065A RID: 1626 RVA: 0x0000234B File Offset: 0x0000054B
		protected IEnumerable GetDesignTimeDataSource(IEnumerable selectedDataSource, int minimumRows)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the HTML markup to be used for the design-time representation of the control.</summary>
		/// <returns>Design-time HTML markup.</returns>
		// Token: 0x0600065B RID: 1627 RVA: 0x0000234B File Offset: 0x0000054B
		public override string GetDesignTimeHtml()
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the HTML markup to represent a Web server control at design time that will have no visual representation at run time.</summary>
		/// <returns>The HTML markup used to represent a control at design time that would otherwise have no visual representation. The default is a rectangle that contains the type and ID of the component.</returns>
		// Token: 0x0600065C RID: 1628 RVA: 0x0000234B File Offset: 0x0000054B
		protected override string GetEmptyDesignTimeHtml()
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the HTML markup that provides information about the specified exception.</summary>
		/// <returns>The design-time HTML markup for the specified exception.</returns>
		/// <param name="e">The exception that occurred.</param>
		// Token: 0x0600065D RID: 1629 RVA: 0x0000234B File Offset: 0x0000054B
		protected override string GetErrorDesignTimeHtml(Exception e)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the selected data member from the selected data source.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" /> that contains a collection of values used to supply design-time data. The default value is null.</returns>
		// Token: 0x0600065E RID: 1630 RVA: 0x0000234B File Offset: 0x0000054B
		public virtual IEnumerable GetResolvedSelectedDataSource()
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the selected data source component from the container of the associated <see cref="T:System.Web.UI.WebControls.Repeater" /> control.</summary>
		/// <returns>The selected data source; null if a data source is not found or if a data source with the selected name does not exist.</returns>
		// Token: 0x0600065F RID: 1631 RVA: 0x0000234B File Offset: 0x0000054B
		public virtual object GetSelectedDataSource()
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes the designer with the provided <see cref="T:System.Web.UI.WebControls.Repeater" /> control.</summary>
		/// <param name="component">The associated <see cref="T:System.Web.UI.WebControls.Repeater" /> control. </param>
		// Token: 0x06000660 RID: 1632 RVA: 0x0000234B File Offset: 0x0000054B
		public override void Initialize(IComponent component)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called when the associated control changes.</summary>
		/// <param name="source">The source of the event.</param>
		/// <param name="ce">The event data.</param>
		// Token: 0x06000661 RID: 1633 RVA: 0x0000234B File Offset: 0x0000054B
		public override void OnComponentChanged(object source, ComponentChangedEventArgs ce)
		{
			throw new NotImplementedException();
		}

		/// <summary>Handles changes made to the data source </summary>
		// Token: 0x06000662 RID: 1634 RVA: 0x0000234B File Offset: 0x0000054B
		protected internal virtual void OnDataSourceChanged()
		{
			throw new NotImplementedException();
		}

		/// <summary>Filters the properties to replace the runtime data source property descriptor with the designer's property descriptor.</summary>
		/// <param name="properties">The properties for the class of the component. </param>
		// Token: 0x06000663 RID: 1635 RVA: 0x0000234B File Offset: 0x0000054B
		protected override void PreFilterProperties(IDictionary properties)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides access to the associated designer component for the data source.</summary>
		/// <returns>An <see cref="T:System.Web.UI.Design.IDataSourceDesigner" /> object.</returns>
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x0000970B File Offset: 0x0000790B
		public IDataSourceDesigner DataSourceDesigner
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the <see cref="P:System.Web.UI.WebControls.Repeater.DataSourceID" /> property of the associated control.</summary>
		/// <returns>The ID of the associated control's data source control.</returns>
		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x0000970B File Offset: 0x0000790B
		// (set) Token: 0x06000666 RID: 1638 RVA: 0x00009519 File Offset: 0x00007719
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

		/// <summary>Gets the <see cref="T:System.Web.UI.Design.DesignerDataSourceView" /> object associated with the data source of this designer.</summary>
		/// <returns>The <see cref="T:System.Web.UI.Design.DesignerDataSourceView" /> associated with the data source of this designer.</returns>
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x0000970B File Offset: 0x0000790B
		public DesignerDataSourceView DesignerView
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Called by a design host such as Visual Studio 2005 after a user selects a data source at design time.</summary>
		// Token: 0x06000668 RID: 1640 RVA: 0x00009519 File Offset: 0x00007719
		protected virtual void ExecuteChooseDataSourcePostSteps()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000153 RID: 339
		private string data_member;

		// Token: 0x04000154 RID: 340
		private string data_source;
	}
}
