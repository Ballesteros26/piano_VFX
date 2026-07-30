using System;

namespace System.Web.ModelBinding
{
	/// <summary>Defines the methods that are required for a value provider. </summary>
	// Token: 0x0200051E RID: 1310
	public interface IValueProvider
	{
		/// <summary>Returns a value that specifies whether the collection contains the specified prefix.</summary>
		/// <returns>true if the collection contains the specified prefix; otherwise, false.</returns>
		/// <param name="prefix">The prefix.</param>
		// Token: 0x060039E8 RID: 14824
		bool ContainsPrefix(string prefix);

		/// <summary>Returns a value object using the specified key.</summary>
		/// <returns>The value object.</returns>
		/// <param name="key">The key.</param>
		// Token: 0x060039E9 RID: 14825
		ValueProviderResult GetValue(string key);
	}
}
