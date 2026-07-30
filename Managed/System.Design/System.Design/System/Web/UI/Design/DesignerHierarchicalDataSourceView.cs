using System;

namespace System.Web.UI.Design
{
	/// <summary>Provides a base class for designers for data views based on hierarchical data. This class must be inherited.</summary>
	// Token: 0x02000073 RID: 115
	public abstract class DesignerHierarchicalDataSourceView
	{
		/// <summary>Initiates a new instance of the <see cref="T:System.Web.UI.Design.DesignerHierarchicalDataSourceView" /> class.</summary>
		/// <param name="owner">The <see cref="T:System.Web.UI.Design.IHierarchicalDataSourceDesigner" /> that is the designer for the associated control.</param>
		/// <param name="viewPath">A unique path to the block of data to use for the view.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="owner" /> is null-or-<paramref name="viewPath" /> is null.</exception>
		// Token: 0x060003A3 RID: 931 RVA: 0x00002364 File Offset: 0x00000564
		[MonoTODO]
		protected DesignerHierarchicalDataSourceView(IHierarchicalDataSourceDesigner owner, string viewPath)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the associated designer.</summary>
		/// <returns>The parent <see cref="T:System.Web.UI.Design.IHierarchicalDataSourceDesigner" />.</returns>
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public IHierarchicalDataSourceDesigner DataSourceDesigner
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the path to the block of data that is presented in the view.</summary>
		/// <returns>The path provided when creating the <see cref="T:System.Web.UI.Design.DesignerHierarchicalDataSourceView" />.</returns>
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public string Path
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a schema that describes the data source view for the associated control.</summary>
		/// <returns>This implementation always returns null.</returns>
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual IDataSourceSchema Schema
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Generates design-time data that matches the schema of the associated data source control and returns a value indicating whether the data is sample or real data.</summary>
		/// <returns>This implementation always returns null.</returns>
		/// <param name="isSampleData">true to indicate the returned data is sample data; false to indicate the returned data is live data.</param>
		// Token: 0x060003A7 RID: 935 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual IHierarchicalEnumerable GetDesignTimeData(out bool isSampleData)
		{
			throw new NotImplementedException();
		}
	}
}
