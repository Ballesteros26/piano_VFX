using System;
using System.Globalization;
using System.Resources;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface for designers to access resource readers and writers for specific <see cref="T:System.Globalization.CultureInfo" /> resource types.</summary>
	// Token: 0x02000335 RID: 821
	public interface IResourceService
	{
		/// <summary>Locates the resource reader for the specified culture and returns it.</summary>
		/// <returns>An <see cref="T:System.Resources.IResourceReader" /> interface that contains the resources for the culture, or null if no resources for the culture exist.</returns>
		/// <param name="info">The <see cref="T:System.Globalization.CultureInfo" /> of the resource for which to retrieve a resource reader. </param>
		// Token: 0x060019EB RID: 6635
		IResourceReader GetResourceReader(CultureInfo info);

		/// <summary>Locates the resource writer for the specified culture and returns it.</summary>
		/// <returns>An <see cref="T:System.Resources.IResourceWriter" /> interface for the specified culture.</returns>
		/// <param name="info">The <see cref="T:System.Globalization.CultureInfo" /> of the resource for which to create a resource writer. </param>
		// Token: 0x060019EC RID: 6636
		IResourceWriter GetResourceWriter(CultureInfo info);
	}
}
