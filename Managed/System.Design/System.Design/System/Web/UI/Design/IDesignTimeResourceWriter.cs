using System;
using System.Resources;

namespace System.Web.UI.Design
{
	/// <summary>Used by the <see cref="T:System.Web.UI.Design.DesignTimeResourceProviderFactory" /> class to localize data at design time.</summary>
	// Token: 0x0200008D RID: 141
	public interface IDesignTimeResourceWriter : IResourceWriter, IDisposable
	{
		/// <summary>Creates a key, using the provided string, to use to retrieve data from the given resource.</summary>
		/// <returns>The key used to write or retrieve <paramref name="obj" /> from <paramref name="resourceName" />.</returns>
		/// <param name="resourceName">The name of the resource.</param>
		/// <param name="obj">The object to localize.</param>
		// Token: 0x0600045D RID: 1117
		string CreateResourceKey(string resourceName, object obj);
	}
}
