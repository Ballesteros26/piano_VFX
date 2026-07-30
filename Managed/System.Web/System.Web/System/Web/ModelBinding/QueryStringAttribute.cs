using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents an attribute that specifies that model binding values are provided by a query string value.</summary>
	// Token: 0x0200072D RID: 1837
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
	public sealed class QueryStringAttribute : ValueProviderSourceAttribute, IUnvalidatedValueProviderSource
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.QueryStringAttribute" /> class.</summary>
		// Token: 0x06004C43 RID: 19523 RVA: 0x0000393A File Offset: 0x00001B3A
		public QueryStringAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.QueryStringAttribute" /> class using the specified query string key.</summary>
		/// <param name="key">The key.</param>
		// Token: 0x06004C44 RID: 19524 RVA: 0x0000393A File Offset: 0x00001B3A
		public QueryStringAttribute(string key)
		{
		}

		/// <summary>Gets the key of the query string value.</summary>
		/// <returns>The key of the query string value.</returns>
		// Token: 0x17001780 RID: 6016
		// (get) Token: 0x06004C45 RID: 19525 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string Key
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets a value that indicates whether query string values are validated.</summary>
		/// <returns>true if query string values are validated; otherwise, false. The default is true.</returns>
		// Token: 0x17001781 RID: 6017
		// (get) Token: 0x06004C46 RID: 19526 RVA: 0x000CAE54 File Offset: 0x000C9054
		// (set) Token: 0x06004C47 RID: 19527 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public bool ValidateInput
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Returns the name of the model.</summary>
		/// <returns>The name of the model.</returns>
		// Token: 0x06004C48 RID: 19528 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override string GetModelName()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns the value provider.</summary>
		/// <returns>The value provider.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="modelBindingExecutionContext" /> parameter is null.</exception>
		// Token: 0x06004C49 RID: 19529 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
