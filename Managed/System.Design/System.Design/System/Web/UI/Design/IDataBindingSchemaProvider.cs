using System;

namespace System.Web.UI.Design
{
	/// <summary>Provides an interface for design-time access to a schema provider in a design host.</summary>
	// Token: 0x02000086 RID: 134
	public interface IDataBindingSchemaProvider
	{
		/// <summary>Gets a value indicating whether the provider can refresh the schema.</summary>
		/// <returns>true, if the schema can be refreshed; otherwise, false.</returns>
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600043D RID: 1085
		bool CanRefreshSchema { get; }

		/// <summary>Gets the current schema object for the designer.</summary>
		/// <returns>The current schema object for the designer.</returns>
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600043E RID: 1086
		IDataSourceViewSchema Schema { get; }

		/// <summary>Refreshes the schema for the data source.</summary>
		/// <param name="preferSilent">true to disable data-binding events until after the schema has been refreshed; false to enable the events.</param>
		// Token: 0x0600043F RID: 1087
		void RefreshSchema(bool preferSilent);
	}
}
