using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace System.ComponentModel.Design.Data
{
	/// <summary>Implements the basic functionality required of a service for providing access to a data source at the EnvDTE.Project level.</summary>
	// Token: 0x02000166 RID: 358
	[Guid("ABE5C1F0-C96E-40c4-A22D-4A5CEC899BDC")]
	public abstract class DataSourceProviderService
	{
		/// <summary>When overridden in a derived class, gets the value indicating whether the service supports adding a new data source using <see cref="M:System.ComponentModel.Design.Data.DataSourceProviderService.InvokeAddNewDataSource(System.Windows.Forms.IWin32Window,System.Windows.Forms.FormStartPosition)" />.</summary>
		/// <returns>true if the service supports adding a new data source using <see cref="M:System.ComponentModel.Design.Data.DataSourceProviderService.InvokeAddNewDataSource(System.Windows.Forms.IWin32Window,System.Windows.Forms.FormStartPosition)" />; otherwise, false.</returns>
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000AC1 RID: 2753
		public abstract bool SupportsAddNewDataSource { get; }

		/// <summary>When overridden in a derived class, gets the value indicating whether the service supports configuring data sources using <see cref="M:System.ComponentModel.Design.Data.DataSourceProviderService.InvokeConfigureDataSource(System.Windows.Forms.IWin32Window,System.Windows.Forms.FormStartPosition,System.ComponentModel.Design.Data.DataSourceDescriptor)" />.</summary>
		/// <returns>true if the service supports configuring a data source using <see cref="M:System.ComponentModel.Design.Data.DataSourceProviderService.InvokeConfigureDataSource(System.Windows.Forms.IWin32Window,System.Windows.Forms.FormStartPosition,System.ComponentModel.Design.Data.DataSourceDescriptor)" />; otherwise, false.</returns>
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000AC2 RID: 2754
		public abstract bool SupportsConfigureDataSource { get; }

		/// <summary>When overridden in a derived class, creates and returns an instance of the given data source, and adds it to the design surface.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing an instance of the added data source.</returns>
		/// <param name="host">The designer host.</param>
		/// <param name="dataSourceDescriptor">The data source.</param>
		/// <exception cref="T:System.ArgumentException">The type name cannot be created or resolved.</exception>
		// Token: 0x06000AC3 RID: 2755
		public abstract object AddDataSourceInstance(IDesignerHost host, DataSourceDescriptor dataSourceDescriptor);

		/// <summary>When overridden in a derived class, retrieves the collection of data sources at the EnvDTE.Project level.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.Data.DataSourceGroupCollection" />, or null if there are no data sources at the EnvDTE.Project level.</returns>
		// Token: 0x06000AC4 RID: 2756
		public abstract DataSourceGroupCollection GetDataSources();

		/// <summary>When overridden in a derived class, invokes the Add New Data Source Wizard.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.Data.DataSourceGroup" /> collection of newly added data sources, or null if no data sources are added.</returns>
		/// <param name="parentWindow">The parent window.</param>
		/// <param name="startPosition">The initial position of a form.</param>
		// Token: 0x06000AC5 RID: 2757
		public abstract DataSourceGroup InvokeAddNewDataSource(IWin32Window parentWindow, FormStartPosition startPosition);

		/// <summary>When overridden in a derived class, invokes the Configure Data Source dialog box on the specified data source.</summary>
		/// <returns>true if any changes were made to that data source; otherwise, false.</returns>
		/// <param name="parentWindow">The parent window.</param>
		/// <param name="startPosition">The initial position of a form.</param>
		/// <param name="dataSourceDescriptor">The data source.</param>
		/// <exception cref="T:System.ArgumentException">The specified data source is invalid or null.</exception>
		// Token: 0x06000AC6 RID: 2758
		public abstract bool InvokeConfigureDataSource(IWin32Window parentWindow, FormStartPosition startPosition, DataSourceDescriptor dataSourceDescriptor);

		/// <summary>When overridden in a derived class, notifies the service that a component representing a data source was added to the design surface.</summary>
		/// <param name="dsc">The data source component.</param>
		// Token: 0x06000AC7 RID: 2759
		public abstract void NotifyDataSourceComponentAdded(object dsc);
	}
}
