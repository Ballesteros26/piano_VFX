using System;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents a value provider for cookie values.</summary>
	// Token: 0x0200070A RID: 1802
	public sealed class CookieValueProvider : IUnvalidatedValueProvider, IValueProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.CookieValueProvider" /> class.</summary>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		// Token: 0x06004BBE RID: 19390 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public CookieValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns a value that indicates whether the cookie collection contains the specified prefix.</summary>
		/// <returns>true if the cookie collection contains the specified prefix; otherwise, false.</returns>
		/// <param name="prefix">The prefix.</param>
		// Token: 0x06004BBF RID: 19391 RVA: 0x000CAC24 File Offset: 0x000C8E24
		public bool ContainsPrefix(string prefix)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Retrieves a value object using the specified key.</summary>
		/// <returns>The value object.</returns>
		/// <param name="key">The key.</param>
		// Token: 0x06004BC0 RID: 19392 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public ValueProviderResult GetValue(string key)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Retrieves a value object using the specified key and a parameter that specifies whether validation should be skipped.</summary>
		/// <returns>The value object.</returns>
		/// <param name="key">The key.</param>
		/// <param name="skipValidation">true to skip validation; otherwise, false.</param>
		// Token: 0x06004BC1 RID: 19393 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public ValueProviderResult GetValue(string key, bool skipValidation)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
