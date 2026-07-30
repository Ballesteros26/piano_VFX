using System;

namespace System.Web.Compilation
{
	/// <summary>Serves as the base class for classes that create resource providers.</summary>
	// Token: 0x02000668 RID: 1640
	public abstract class ResourceProviderFactory
	{
		/// <summary>When overridden in a derived class, creates a global resource provider. </summary>
		/// <returns>A global resource provider.</returns>
		/// <param name="classKey">The name of the resource class.</param>
		// Token: 0x0600462B RID: 17963
		public abstract IResourceProvider CreateGlobalResourceProvider(string classKey);

		/// <summary>When overridden in a derived class, creates a local resource provider. </summary>
		/// <returns>A local resource provider.</returns>
		/// <param name="virtualPath">The path to a resource file.</param>
		// Token: 0x0600462C RID: 17964
		public abstract IResourceProvider CreateLocalResourceProvider(string virtualPath);
	}
}
