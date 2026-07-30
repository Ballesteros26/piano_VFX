using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents an attribute that specifies that values for model binding are provided by route data.</summary>
	// Token: 0x02000732 RID: 1842
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
	public sealed class RouteDataAttribute : ValueProviderSourceAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.RouteDataAttribute" /> class.</summary>
		// Token: 0x06004C50 RID: 19536 RVA: 0x0000393A File Offset: 0x00001B3A
		public RouteDataAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.RouteDataAttribute" /> class, using the route data key.</summary>
		/// <param name="key">The key.</param>
		// Token: 0x06004C51 RID: 19537 RVA: 0x0000393A File Offset: 0x00001B3A
		public RouteDataAttribute(string key)
		{
		}

		/// <summary>Gets the route data key.</summary>
		/// <returns>The route data key.</returns>
		// Token: 0x17001782 RID: 6018
		// (get) Token: 0x06004C52 RID: 19538 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string Key
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the model name.</summary>
		/// <returns>The model name.</returns>
		// Token: 0x06004C53 RID: 19539 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override string GetModelName()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the value provider.</summary>
		/// <returns>A new instance of the <see cref="T:System.Web.ModelBinding.RouteDataValueProvider" /> class.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		// Token: 0x06004C54 RID: 19540 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
