using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents an attribute that specifies that the values for model binding are provided by a cookie.</summary>
	// Token: 0x02000709 RID: 1801
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
	public sealed class CookieAttribute : ValueProviderSourceAttribute, IUnvalidatedValueProviderSource
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.CookieAttribute" /> class.</summary>
		// Token: 0x06004BB7 RID: 19383 RVA: 0x0000393A File Offset: 0x00001B3A
		public CookieAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.CookieAttribute" /> class using the specified cookie name.</summary>
		/// <param name="name">The name of the cookie.</param>
		// Token: 0x06004BB8 RID: 19384 RVA: 0x0000393A File Offset: 0x00001B3A
		public CookieAttribute(string name)
		{
		}

		/// <summary>Gets the name of the cookie.</summary>
		/// <returns>The name of the cookie.</returns>
		// Token: 0x17001767 RID: 5991
		// (get) Token: 0x06004BB9 RID: 19385 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string Name
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets a value that specifies whether cookie values should be validated.</summary>
		/// <returns>true if cookie values are validated; otherwise, false. The default is true.</returns>
		// Token: 0x17001768 RID: 5992
		// (get) Token: 0x06004BBA RID: 19386 RVA: 0x000CAC08 File Offset: 0x000C8E08
		// (set) Token: 0x06004BBB RID: 19387 RVA: 0x0000B3E4 File Offset: 0x000095E4
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

		/// <summary>Returns the model name.</summary>
		/// <returns>The model name.</returns>
		// Token: 0x06004BBC RID: 19388 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override string GetModelName()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns the value provider.</summary>
		/// <returns>The value provider.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="modelBindingExecutionContext" /> parameter is null.</exception>
		// Token: 0x06004BBD RID: 19389 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
