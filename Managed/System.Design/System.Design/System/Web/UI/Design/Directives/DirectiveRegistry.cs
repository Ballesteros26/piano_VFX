using System;
using System.Collections.ObjectModel;
using Unity;

namespace System.Web.UI.Design.Directives
{
	/// <summary>Provides support when creating directive IntelliSense at design time.</summary>
	// Token: 0x020001CE RID: 462
	public static class DirectiveRegistry
	{
		/// <summary>Returns a collection of directives based on the <paramref name="frameworkVersion" /> parameter and the <paramref name="extension" /> parameter.</summary>
		/// <returns>A read only collection.</returns>
		/// <param name="frameworkVersion">The .NET Framework version.</param>
		/// <param name="extension">The file name extension.</param>
		// Token: 0x06000BE9 RID: 3049 RVA: 0x000168C7 File Offset: 0x00014AC7
		public static ReadOnlyCollection<Type> GetDirectives(Version frameworkVersion, string extension)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}
	}
}
