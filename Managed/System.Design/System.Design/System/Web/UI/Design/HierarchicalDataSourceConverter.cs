using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	/// <summary>Provides a type converter that can retrieve a list of the hierarchical data sources that are accessible to the current component.</summary>
	// Token: 0x0200007D RID: 125
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class HierarchicalDataSourceConverter : DataSourceConverter
	{
		/// <summary>Indicates whether the specified component is a valid data source for this converter.</summary>
		/// <returns>true if <paramref name="component" /> implements <see cref="T:System.Web.UI.IHierarchicalEnumerable" />; otherwise, false.</returns>
		/// <param name="component">The component to check as a valid data source.</param>
		// Token: 0x060003FF RID: 1023 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected override bool IsValidDataSource(IComponent component)
		{
			throw new NotImplementedException();
		}
	}
}
