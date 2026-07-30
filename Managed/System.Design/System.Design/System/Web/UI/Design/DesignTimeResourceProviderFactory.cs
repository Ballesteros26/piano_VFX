using System;
using System.Web.Compilation;

namespace System.Web.UI.Design
{
	/// <summary>Used by control localization to read and write resources at design time. </summary>
	// Token: 0x0200006E RID: 110
	public abstract class DesignTimeResourceProviderFactory
	{
		/// <summary>When overridden in a derived class, creates a global resource provider using the provided <see cref="T:System.IServiceProvider" /> interface and resource class name.</summary>
		/// <returns>Either an <see cref="T:System.Web.Compilation.IResourceProvider" /> or an <see cref="T:System.Web.UI.Design.IDesignTimeResourceWriter" />.</returns>
		/// <param name="serviceProvider">A reference to the design host.</param>
		/// <param name="classKey">The name of the resource class.</param>
		// Token: 0x06000372 RID: 882
		public abstract IResourceProvider CreateDesignTimeGlobalResourceProvider(IServiceProvider serviceProvider, string classKey);

		/// <summary>When overridden in a derived class, creates a local resource provider using the provided reference to the design host.</summary>
		/// <returns>An <see cref="T:System.Web.Compilation.IResourceProvider" /> or a class derived from <see cref="T:System.Web.Compilation.IResourceProvider" />.</returns>
		/// <param name="serviceProvider">A reference to the design host.</param>
		// Token: 0x06000373 RID: 883
		public abstract IResourceProvider CreateDesignTimeLocalResourceProvider(IServiceProvider serviceProvider);

		/// <summary>When overridden in a derived class, creates a local resource writer for using the provided reference to the design host.</summary>
		/// <returns>A local resource writer for using the provided reference to the design host.</returns>
		/// <param name="serviceProvider">A reference to the design host.</param>
		// Token: 0x06000374 RID: 884
		public abstract IDesignTimeResourceWriter CreateDesignTimeLocalResourceWriter(IServiceProvider serviceProvider);
	}
}
