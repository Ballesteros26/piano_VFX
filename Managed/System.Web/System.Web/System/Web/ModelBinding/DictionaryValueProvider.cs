using System;
using System.Collections.Generic;
using System.Globalization;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents the base class for value providers whose values come from a collection that implements the <see cref="T:System.Collections.Generic.IDictionary`2" /> interface.</summary>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	// Token: 0x02000716 RID: 1814
	public class DictionaryValueProvider<TValue> : IValueProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.DictionaryValueProvider`1" /> class.</summary>
		/// <param name="dictionary">The dictionary.</param>
		/// <param name="culture">The culture information.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="dictionary" /> parameter is null.</exception>
		// Token: 0x06004BEB RID: 19435 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public DictionaryValueProvider(IDictionary<string, TValue> dictionary, CultureInfo culture)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns a value that indicates whether the dictionary contains the specified prefix.</summary>
		/// <returns>true if the dictionary contains the specified prefix; otherwise, false.</returns>
		/// <param name="prefix">The prefix.</param>
		// Token: 0x06004BEC RID: 19436 RVA: 0x000CACCC File Offset: 0x000C8ECC
		public virtual bool ContainsPrefix(string prefix)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Retrieves a value object using the specified key.</summary>
		/// <returns>The value object.</returns>
		/// <param name="key">The key.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> parameter is null.</exception>
		// Token: 0x06004BED RID: 19437 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual ValueProviderResult GetValue(string key)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
