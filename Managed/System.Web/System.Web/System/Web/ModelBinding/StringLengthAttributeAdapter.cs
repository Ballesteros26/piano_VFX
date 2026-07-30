using System;
using System.ComponentModel.DataAnnotations;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides an adapter for the <see cref="T:System.ComponentModel.DataAnnotations.StringLengthAttribute" /> attribute.</summary>
	// Token: 0x02000736 RID: 1846
	public sealed class StringLengthAttributeAdapter : DataAnnotationsModelValidator<StringLengthAttribute>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.StringLengthAttributeAdapter" /> class.</summary>
		/// <param name="metadata">The metadata.</param>
		/// <param name="context">The execution context.</param>
		/// <param name="attribute">The attribute.</param>
		// Token: 0x06004C61 RID: 19553 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public StringLengthAttributeAdapter(ModelMetadata metadata, ModelBindingExecutionContext context, StringLengthAttribute attribute)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004C62 RID: 19554 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected override string GetLocalizedErrorMessage(string errorMessage)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
