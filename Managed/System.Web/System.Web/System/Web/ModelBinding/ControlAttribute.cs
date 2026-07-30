using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents an attribute that specifies that values for model binding are provided by a control.</summary>
	// Token: 0x02000705 RID: 1797
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
	public sealed class ControlAttribute : ValueProviderSourceAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ControlAttribute" /> class.</summary>
		// Token: 0x06004BA4 RID: 19364 RVA: 0x0000393A File Offset: 0x00001B3A
		public ControlAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ControlAttribute" /> class using the specified control ID.</summary>
		/// <param name="controlID">The control ID.</param>
		// Token: 0x06004BA5 RID: 19365 RVA: 0x0000393A File Offset: 0x00001B3A
		public ControlAttribute(string controlID)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ControlAttribute" /> class using the specified control ID and property name.</summary>
		/// <param name="controlID">The control ID.</param>
		/// <param name="propertyName">The property name.</param>
		// Token: 0x06004BA6 RID: 19366 RVA: 0x0000393A File Offset: 0x00001B3A
		public ControlAttribute(string controlID, string propertyName)
		{
		}

		/// <summary>Gets the control ID.</summary>
		/// <returns>The control ID.</returns>
		// Token: 0x17001763 RID: 5987
		// (get) Token: 0x06004BA7 RID: 19367 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string ControlID
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the property name.</summary>
		/// <returns>The property name.</returns>
		// Token: 0x17001764 RID: 5988
		// (get) Token: 0x06004BA8 RID: 19368 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string PropertyName
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
		// Token: 0x06004BA9 RID: 19369 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override string GetModelName()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the value provider.</summary>
		/// <returns>The value provider.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="modelBindingExecutionContext" /> parameter is null.</exception>
		// Token: 0x06004BAA RID: 19370 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
