using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents a value provider for single key/value lookups.</summary>
	// Token: 0x02000708 RID: 1800
	public abstract class SimpleValueProvider : IValueProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.SimpleValueProvider" /> class by using the specified execution context.</summary>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		// Token: 0x06004BB1 RID: 19377 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected SimpleValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.SimpleValueProvider" /> class by using the specified execution context and culture information.</summary>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="cultureInfo">The culture information.</param>
		// Token: 0x06004BB2 RID: 19378 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected SimpleValueProvider(ModelBindingExecutionContext modelBindingExecutionContext, CultureInfo cultureInfo)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the execution context.</summary>
		/// <returns>The execution context.</returns>
		// Token: 0x17001766 RID: 5990
		// (get) Token: 0x06004BB3 RID: 19379 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected ModelBindingExecutionContext ModelBindingExecutionContext
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a value that indicates whether the value provider contains the specified prefix.</summary>
		/// <returns>true if the value provider contains the specified prefix; otherwise, false.</returns>
		/// <param name="prefix">The prefix.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="prefix" /> parameter is null.</exception>
		// Token: 0x06004BB4 RID: 19380 RVA: 0x000CABEC File Offset: 0x000C8DEC
		public virtual bool ContainsPrefix(string prefix)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>When implemented in a derived class, returns the value for the specified key.</summary>
		/// <returns>The value.</returns>
		/// <param name="key">The key of the value to retrieve.</param>
		// Token: 0x06004BB5 RID: 19381
		protected abstract object FetchValue(string key);

		/// <summary>Calls the <see cref="M:System.Web.ModelBinding.SimpleValueProvider.FetchValue(System.String)" /> method and then uses the returned value to instantiate a new instance of the <see cref="T:System.Web.ModelBinding.ValueProviderResult" /> class.</summary>
		/// <returns>The value.</returns>
		/// <param name="key">The key of the value to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> parameter is null.</exception>
		// Token: 0x06004BB6 RID: 19382 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual ValueProviderResult GetValue(string key)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
