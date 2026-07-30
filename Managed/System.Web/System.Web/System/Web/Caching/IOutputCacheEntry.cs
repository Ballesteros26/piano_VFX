using System;
using System.Collections.Generic;

namespace System.Web.Caching
{
	/// <summary>Defines collections of HTTP header and response elements that together make up one kind of output-cached data that ASP.NET can pass to a provider. </summary>
	// Token: 0x02000679 RID: 1657
	public interface IOutputCacheEntry
	{
		/// <summary>Gets the collection of HTTP header elements in an output-cache entry.</summary>
		/// <returns>A list of HTTP header elements.</returns>
		// Token: 0x170015ED RID: 5613
		// (get) Token: 0x060046D9 RID: 18137
		// (set) Token: 0x060046DA RID: 18138
		List<HeaderElement> HeaderElements { get; set; }

		/// <summary>Gets the collection of HTTP response elements in an output-cache entry.</summary>
		/// <returns>A list of HTTP response elements.</returns>
		// Token: 0x170015EE RID: 5614
		// (get) Token: 0x060046DB RID: 18139
		// (set) Token: 0x060046DC RID: 18140
		List<ResponseElement> ResponseElements { get; set; }
	}
}
