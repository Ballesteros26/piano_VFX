using System;

namespace System.Web.UI.Design
{
	/// <summary>Defines the basic functionality for a data source designer.</summary>
	// Token: 0x02000087 RID: 135
	public interface IDataSourceDesigner
	{
		/// <summary>Occurs when a data source has changed in a way that affects data-bound controls.</summary>
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000440 RID: 1088
		// (remove) Token: 0x06000441 RID: 1089
		event EventHandler DataSourceChanged;

		/// <summary>Occurs when the fields or data of the underlying data source have changed.</summary>
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000442 RID: 1090
		// (remove) Token: 0x06000443 RID: 1091
		event EventHandler SchemaRefreshed;

		/// <summary>Gets a value that indicates whether the <see cref="M:System.Web.UI.Design.IDataSourceDesigner.Configure" /> method can be called.</summary>
		/// <returns>true if the underlying data source has a configuration wizard that can be launched with the <see cref="M:System.Web.UI.Design.IDataSourceDesigner.Configure" /> method; otherwise, false.</returns>
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000444 RID: 1092
		bool CanConfigure { get; }

		/// <summary>Gets a value that indicates whether the <see cref="M:System.Web.UI.Design.IDataSourceDesigner.RefreshSchema(System.Boolean)" /> method can be called.</summary>
		/// <returns>true if the underlying data source can refresh its schema; otherwise, false.</returns>
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000445 RID: 1093
		bool CanRefreshSchema { get; }

		/// <summary>Launches the underlying data source's configuration wizard.</summary>
		// Token: 0x06000446 RID: 1094
		void Configure();

		/// <summary>Gets the <see cref="T:System.Web.UI.Design.DesignerDataSourceView" /> for the specified view.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Design.DesignerDataSourceView" /> containing information about the identified view, or null if a view with the specified name is not found.</returns>
		/// <param name="viewName">The name of a view in the underlying data source.</param>
		// Token: 0x06000447 RID: 1095
		DesignerDataSourceView GetView(string viewName);

		/// <summary>Gets the names of the views in the underlying data source.</summary>
		/// <returns>An array of type <see cref="T:System.String" />.</returns>
		// Token: 0x06000448 RID: 1096
		string[] GetViewNames();

		/// <summary>Refreshes the schema of the underlying data source.</summary>
		/// <param name="preferSilent">Indicates whether to suppress any events raised while refreshing the schema.</param>
		// Token: 0x06000449 RID: 1097
		void RefreshSchema(bool preferSilent);

		/// <summary>Resumes raising data source events after calling the <see cref="M:System.Web.UI.Design.IDataSourceDesigner.SuppressDataSourceEvents" /> method.</summary>
		// Token: 0x0600044A RID: 1098
		void ResumeDataSourceEvents();

		/// <summary>Suppresses all events raised by a data source until the <see cref="M:System.Web.UI.Design.IDataSourceDesigner.ResumeDataSourceEvents" /> method is called.</summary>
		// Token: 0x0600044B RID: 1099
		void SuppressDataSourceEvents();
	}
}
