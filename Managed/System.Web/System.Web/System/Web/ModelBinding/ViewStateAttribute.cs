using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents an attribute that specifies that values for model binding are provided by view state.</summary>
	// Token: 0x0200073F RID: 1855
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
	public sealed class ViewStateAttribute : ValueProviderSourceAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ViewStateAttribute" /> class.</summary>
		// Token: 0x06004C78 RID: 19576 RVA: 0x0000393A File Offset: 0x00001B3A
		public ViewStateAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ViewStateAttribute" /> class using the specified view state key.</summary>
		/// <param name="key">The key.</param>
		// Token: 0x06004C79 RID: 19577 RVA: 0x0000393A File Offset: 0x00001B3A
		public ViewStateAttribute(string key)
		{
		}

		/// <summary>Gets the view state key.</summary>
		/// <returns>The view state key.</returns>
		// Token: 0x17001786 RID: 6022
		// (get) Token: 0x06004C7A RID: 19578 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string Key
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Returns the model name.</summary>
		/// <returns>The model name.</returns>
		// Token: 0x06004C7B RID: 19579 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override string GetModelName()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns the value provider.</summary>
		/// <returns>The value provider.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="modelBindingExecutionContext" /> parameter is null.</exception>
		// Token: 0x06004C7C RID: 19580 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
