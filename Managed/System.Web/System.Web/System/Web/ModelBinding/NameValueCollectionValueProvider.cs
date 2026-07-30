using System;
using System.Collections.Specialized;
using System.Globalization;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents the base class for value providers whose values come from a name/value collection.</summary>
	// Token: 0x0200071A RID: 1818
	public class NameValueCollectionValueProvider : IUnvalidatedValueProvider, IValueProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.NameValueCollectionValueProvider" /> class by using a collection, an unvalidated version of the collection, and culture information.</summary>
		/// <param name="collection">The collection.</param>
		/// <param name="unvalidatedCollection">The unvalidated collection.</param>
		/// <param name="culture">The culture information.</param>
		// Token: 0x06004BF8 RID: 19448 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public NameValueCollectionValueProvider(NameValueCollection collection, NameValueCollection unvalidatedCollection, CultureInfo culture)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.NameValueCollectionValueProvider" /> class by using a collection and culture information.</summary>
		/// <param name="collection">The collection.</param>
		/// <param name="culture">The culture information.</param>
		// Token: 0x06004BF9 RID: 19449 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public NameValueCollectionValueProvider(NameValueCollection collection, CultureInfo culture)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns a value that indicates whether the value provider contains the specified prefix.</summary>
		/// <returns>true if the value provider contains the specified prefix; otherwise, false.</returns>
		/// <param name="prefix">The prefix.</param>
		// Token: 0x06004BFA RID: 19450 RVA: 0x000CAD04 File Offset: 0x000C8F04
		public virtual bool ContainsPrefix(string prefix)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Returns the value for the specified key.</summary>
		/// <returns>The value for the specified key.</returns>
		/// <param name="key">The key.</param>
		// Token: 0x06004BFB RID: 19451 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual ValueProviderResult GetValue(string key)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns the value for the specified key and optionally enables the caller to specify whether request validation should be skipped.</summary>
		/// <returns>The value for the specified key.</returns>
		/// <param name="key">The key.</param>
		/// <param name="skipValidation">true to skip validation; otherwise, false. </param>
		// Token: 0x06004BFC RID: 19452 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual ValueProviderResult GetValue(string key, bool skipValidation)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
