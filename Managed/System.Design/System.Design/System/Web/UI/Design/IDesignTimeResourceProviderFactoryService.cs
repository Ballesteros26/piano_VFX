using System;

namespace System.Web.UI.Design
{
	/// <summary>Provides an interface for creating a custom <see cref="T:System.Web.UI.Design.DesignTimeResourceProviderFactory" /> class.</summary>
	// Token: 0x0200008C RID: 140
	public interface IDesignTimeResourceProviderFactoryService
	{
		/// <summary>Creates a <see cref="T:System.Web.UI.Design.DesignTimeResourceProviderFactory" /> object.</summary>
		/// <returns>A design time resource provider factory.</returns>
		// Token: 0x0600045C RID: 1116
		DesignTimeResourceProviderFactory GetFactory();
	}
}
