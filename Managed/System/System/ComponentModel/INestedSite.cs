using System;

namespace System.ComponentModel
{
	/// <summary>Provides the ability to retrieve the full nested name of a component.</summary>
	// Token: 0x02000284 RID: 644
	public interface INestedSite : ISite, IServiceProvider
	{
		/// <summary>Gets the full name of the component in this site.</summary>
		/// <returns>The full name of the component in this site.</returns>
		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x0600146D RID: 5229
		string FullName { get; }
	}
}
