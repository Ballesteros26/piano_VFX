using System;

namespace System.Web.ModelBinding
{
	/// <summary>Defines the method that is required for an unvalidated value provider.</summary>
	// Token: 0x0200070B RID: 1803
	public interface IUnvalidatedValueProvider : IValueProvider
	{
		/// <summary>Returns a value using the specified key and optionally a value that specifies that request validation should be skipped.</summary>
		/// <returns>The value object.</returns>
		/// <param name="key">The key.</param>
		/// <param name="skipValidation">true to skip request validation; otherwise, false.</param>
		// Token: 0x06004BC2 RID: 19394
		ValueProviderResult GetValue(string key, bool skipValidation);
	}
}
