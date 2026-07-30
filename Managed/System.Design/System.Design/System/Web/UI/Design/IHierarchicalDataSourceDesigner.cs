using System;

namespace System.Web.UI.Design
{
	/// <summary>Provides design-time support in a visual designer for a class that is derived from the <see cref="T:System.Web.UI.HierarchicalDataSourceControl" /> class.</summary>
	// Token: 0x02000090 RID: 144
	public interface IHierarchicalDataSourceDesigner
	{
		/// <summary>Occurs when a data source control has changed in some way that affects data-bound controls.</summary>
		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000463 RID: 1123
		// (remove) Token: 0x06000464 RID: 1124
		event EventHandler DataSourceChanged;

		/// <summary>Occurs when the fields or data of the underlying data source have changed.</summary>
		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000465 RID: 1125
		// (remove) Token: 0x06000466 RID: 1126
		event EventHandler SchemaRefreshed;

		/// <summary>Gets a value indicating whether the <see cref="M:System.Web.UI.Design.IHierarchicalDataSourceDesigner.Configure" /> method can be called.</summary>
		/// <returns>true if the underlying data source has a configuration wizard that can be launched with <see cref="M:System.Web.UI.Design.IHierarchicalDataSourceDesigner.Configure" />, otherwise, false.</returns>
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000467 RID: 1127
		bool CanConfigure { get; }

		/// <summary>Gets a value indicating whether the <see cref="M:System.Web.UI.Design.IHierarchicalDataSourceDesigner.RefreshSchema(System.Boolean)" /> method can be called.</summary>
		/// <returns>true if <see cref="M:System.Web.UI.Design.IHierarchicalDataSourceDesigner.RefreshSchema(System.Boolean)" /> can be called; otherwise, false.</returns>
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000468 RID: 1128
		bool CanRefreshSchema { get; }

		/// <summary>Launches the configuration wizard for the underlying data source.</summary>
		// Token: 0x06000469 RID: 1129
		void Configure();

		/// <summary>Gets the named data source view that is associated with the data source control.</summary>
		/// <returns>The named data source view that is associated with the data source control.</returns>
		/// <param name="viewPath">The XPath for the part of the data source to retrieve.</param>
		// Token: 0x0600046A RID: 1130
		DesignerHierarchicalDataSourceView GetView(string viewPath);

		/// <summary>Refreshes the schema of the underlying data source.</summary>
		/// <param name="preferSilent">true to suppress events raised while refreshing the schema; otherwise false.</param>
		// Token: 0x0600046B RID: 1131
		void RefreshSchema(bool preferSilent);

		/// <summary>Restores events after calling the <see cref="M:System.Web.UI.Design.IHierarchicalDataSourceDesigner.SuppressDataSourceEvents" /> method.</summary>
		// Token: 0x0600046C RID: 1132
		void ResumeDataSourceEvents();

		/// <summary>Turns off events in the data source control.</summary>
		// Token: 0x0600046D RID: 1133
		void SuppressDataSourceEvents();
	}
}
