using System;
using System.Collections.Generic;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides an adapter for objects that implement the <see cref="T:System.ComponentModel.DataAnnotations.IValidatableObject" /> interface.</summary>
	// Token: 0x0200073D RID: 1853
	public class ValidatableObjectAdapter : ModelValidator
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ValidatableObjectAdapter" /> class.</summary>
		/// <param name="metadata">The metadata.</param>
		/// <param name="context">The execution context.</param>
		// Token: 0x06004C6F RID: 19567 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ValidatableObjectAdapter(ModelMetadata metadata, ModelBindingExecutionContext context)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Validates the specified object.</summary>
		/// <returns>The validation results.</returns>
		/// <param name="container">The container for the object to be validated.</param>
		// Token: 0x06004C70 RID: 19568 RVA: 0x0000FAB7 File Offset: 0x0000DCB7
		public override IEnumerable<ModelValidationResult> Validate(object container)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}
	}
}
