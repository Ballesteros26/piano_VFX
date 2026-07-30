using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents an attribute that specifies that values for model binding are provided by a profile.</summary>
	// Token: 0x0200072B RID: 1835
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
	public sealed class ProfileAttribute : ValueProviderSourceAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ProfileAttribute" /> class.</summary>
		// Token: 0x06004C3C RID: 19516 RVA: 0x0000393A File Offset: 0x00001B3A
		public ProfileAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ProfileAttribute" /> class, using the profile key.</summary>
		/// <param name="key">The key.</param>
		// Token: 0x06004C3D RID: 19517 RVA: 0x0000393A File Offset: 0x00001B3A
		public ProfileAttribute(string key)
		{
		}

		/// <summary>Gets the profile key.</summary>
		/// <returns>The profile key.</returns>
		// Token: 0x1700177F RID: 6015
		// (get) Token: 0x06004C3E RID: 19518 RVA: 0x0000E80B File Offset: 0x0000CA0B
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
		// Token: 0x06004C3F RID: 19519 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override string GetModelName()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the value provider.</summary>
		/// <returns>A new instance of the <see cref="T:System.Web.ModelBinding.ProfileValueProvider" /> class.</returns>
		/// <param name="modelBindingExecutionContext">the execution context.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="modelBindingExecutionContext" /> parameter is null.</exception>
		// Token: 0x06004C40 RID: 19520 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
