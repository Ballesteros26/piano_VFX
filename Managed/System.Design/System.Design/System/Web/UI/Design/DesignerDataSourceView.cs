using System;
using System.Collections;

namespace System.Web.UI.Design
{
	/// <summary>Serves as the base class for design-time data source view classes. </summary>
	// Token: 0x02000072 RID: 114
	public abstract class DesignerDataSourceView
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.DesignerDataSourceView" /> class using the specified data source designer and view name.</summary>
		/// <param name="owner">The parent data source designer.</param>
		/// <param name="viewName">The name of the view in the data source.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="owner" /> is null-or-<paramref name="viewName" /> is null.</exception>
		// Token: 0x06000398 RID: 920 RVA: 0x00002364 File Offset: 0x00000564
		[MonoNotSupported("")]
		protected DesignerDataSourceView(IDataSourceDesigner owner, string viewName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.DataSourceControl" /> object supports the <see cref="M:System.Web.UI.DataSourceView.ExecuteDelete(System.Collections.IDictionary,System.Collections.IDictionary)" /> method.</summary>
		/// <returns>true if the <see cref="M:System.Web.UI.DataSourceView.ExecuteDelete(System.Collections.IDictionary,System.Collections.IDictionary)" /> method is supported; otherwise, false.</returns>
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public virtual bool CanDelete
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.DataSourceControl" /> object supports the <see cref="M:System.Web.UI.DataSourceView.ExecuteInsert(System.Collections.IDictionary)" /> method.</summary>
		/// <returns>true if the <see cref="M:System.Web.UI.DataSourceView.ExecuteInsert(System.Collections.IDictionary)" /> method is supported; otherwise, false.</returns>
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public virtual bool CanInsert
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.DataSourceControl" /> object supports paging through the data that is retrieved by the <see cref="M:System.Web.UI.DataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)" /> method.</summary>
		/// <returns>true if paging through the data retrieved by the <see cref="M:System.Web.UI.DataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)" /> method is supported; otherwise, false.</returns>
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public virtual bool CanPage
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.DataSourceControl" /> object supports retrieving the total number of data rows instead of the data itself.</summary>
		/// <returns>true if retrieving the total number of data rows is supported; otherwise, false.</returns>
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public virtual bool CanRetrieveTotalRowCount
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.DataSourceControl" /> object supports a sorted view on the underlying data source.</summary>
		/// <returns>true if a sorted view on the underlying data source is supported; otherwise, false.</returns>
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public virtual bool CanSort
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.DataSourceControl" /> object supports the <see cref="M:System.Web.UI.DataSourceView.ExecuteUpdate(System.Collections.IDictionary,System.Collections.IDictionary,System.Collections.IDictionary)" /> method.</summary>
		/// <returns>true if the <see cref="M:System.Web.UI.DataSourceView.ExecuteUpdate(System.Collections.IDictionary,System.Collections.IDictionary,System.Collections.IDictionary)" /> method is supported; otherwise, false.</returns>
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public virtual bool CanUpdate
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a reference to the designer that created this <see cref="T:System.Web.UI.Design.DesignerDataSourceView" /> control.</summary>
		/// <returns>The <see cref="T:System.Web.UI.Design.IDataSourceDesigner" /> object provided when the current <see cref="T:System.Web.UI.Design.DesignerDataSourceView" /> instance was created.</returns>
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600039F RID: 927 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public IDataSourceDesigner DataSourceDesigner
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the name of the view as provided when this instance of the <see cref="T:System.Web.UI.Design.DesignerDataSourceView" /> class was created.</summary>
		/// <returns>The view name.</returns>
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public string Name
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a schema that describes the data source view that is represented by this view object.</summary>
		/// <returns>An <see cref="T:System.Web.UI.Design.IDataSourceViewSchema" /> object.</returns>
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public virtual IDataSourceViewSchema Schema
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Generates design-time data that matches the schema of the associated data source control using the specified number of rows, indicating whether it is returning sample data or real data.</summary>
		/// <returns>A <see cref="T:System.Web.UI.DataSourceView" /> object containing data to display at design time.</returns>
		/// <param name="minimumRows">The minimum number of rows to return.</param>
		/// <param name="isSampleData">true to indicate that the returned data is sample data; false to indicate that the returned data is live data.</param>
		// Token: 0x060003A2 RID: 930 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public virtual IEnumerable GetDesignTimeData(int minimumRows, out bool isSampleData)
		{
			throw new NotImplementedException();
		}
	}
}
