using System;

namespace System.Web.ModelBinding
{
	/// <summary>Defines the methods that are required for a value provider that supports skipping request validation.</summary>
	// Token: 0x0200051D RID: 1309
	public interface IUnvalidatedValueProviderSource : IValueProviderSource
	{
		/// <summary>Gets or sets a value that indicates whether the provider validates input.</summary>
		/// <returns>true if the provider validates input; otherwise, false.</returns>
		// Token: 0x170011E7 RID: 4583
		// (get) Token: 0x060039E6 RID: 14822
		// (set) Token: 0x060039E7 RID: 14823
		bool ValidateInput { get; set; }
	}
}
