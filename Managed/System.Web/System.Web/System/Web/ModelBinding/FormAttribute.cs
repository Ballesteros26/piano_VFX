using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents an attribute that specifies that values for model binding are provided by a form field.</summary>
	// Token: 0x02000718 RID: 1816
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
	public sealed class FormAttribute : ValueProviderSourceAttribute, IUnvalidatedValueProviderSource
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.FormAttribute" /> class.</summary>
		// Token: 0x06004BF0 RID: 19440 RVA: 0x0000393A File Offset: 0x00001B3A
		public FormAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.FormAttribute" /> class using the specified form field name.</summary>
		/// <param name="fieldName">The name of the form field.</param>
		// Token: 0x06004BF1 RID: 19441 RVA: 0x0000393A File Offset: 0x00001B3A
		public FormAttribute(string fieldName)
		{
		}

		/// <summary>Gets the name of the form field.</summary>
		/// <returns>The name of the form field.</returns>
		// Token: 0x1700176F RID: 5999
		// (get) Token: 0x06004BF2 RID: 19442 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string FieldName
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the form data is validated.</summary>
		/// <returns>true if the form data is validated; otherwise, false.</returns>
		// Token: 0x17001770 RID: 6000
		// (get) Token: 0x06004BF3 RID: 19443 RVA: 0x000CACE8 File Offset: 0x000C8EE8
		// (set) Token: 0x06004BF4 RID: 19444 RVA: 0x0000B3E4 File Offset: 0x000095E4
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

		/// <summary>Gets the model name.</summary>
		/// <returns>The model name.</returns>
		// Token: 0x06004BF5 RID: 19445 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override string GetModelName()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the value provider.</summary>
		/// <returns>The value provider.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		// Token: 0x06004BF6 RID: 19446 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
