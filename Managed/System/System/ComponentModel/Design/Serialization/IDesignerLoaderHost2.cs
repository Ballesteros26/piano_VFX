using System;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides an interface that extends <see cref="T:System.ComponentModel.Design.Serialization.IDesignerLoaderHost" /> to specify whether errors are tolerated while loading a design document.</summary>
	// Token: 0x0200034E RID: 846
	public interface IDesignerLoaderHost2 : IDesignerLoaderHost, IDesignerHost, IServiceContainer, IServiceProvider
	{
		/// <summary>Gets or sets a value indicating whether errors should be ignored when <see cref="M:System.ComponentModel.Design.Serialization.IDesignerLoaderHost.Reload" /> is called.</summary>
		/// <returns>true if the designer loader will ignore errors when it reloads; otherwise, false. The default is false.</returns>
		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001A5F RID: 6751
		// (set) Token: 0x06001A60 RID: 6752
		bool IgnoreErrorsDuringReload { get; set; }

		/// <summary>Gets or sets a value indicating whether it is possible to reload with errors. </summary>
		/// <returns>true if the designer loader can reload the design document when errors are detected; otherwise, false. The default is false.</returns>
		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001A61 RID: 6753
		// (set) Token: 0x06001A62 RID: 6754
		bool CanReloadWithErrors { get; set; }
	}
}
