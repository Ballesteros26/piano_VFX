using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a model validator.</summary>
	// Token: 0x0200070F RID: 1807
	public class DataAnnotationsModelValidator : ModelValidator
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.DataAnnotationsModelValidator" /> class.</summary>
		/// <param name="metadata">The metadata.</param>
		/// <param name="context">The execution context.</param>
		/// <param name="attribute">The validation attribute.</param>
		// Token: 0x06004BCB RID: 19403 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public DataAnnotationsModelValidator(ModelMetadata metadata, ModelBindingExecutionContext context, ValidationAttribute attribute)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the validation attribute for the model validator.</summary>
		/// <returns>The validation attribute.</returns>
		// Token: 0x17001769 RID: 5993
		// (get) Token: 0x06004BCC RID: 19404 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected internal ValidationAttribute Attribute
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the error message for a validation failure.</summary>
		/// <returns>The error message.</returns>
		// Token: 0x1700176A RID: 5994
		// (get) Token: 0x06004BCD RID: 19405 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected internal string ErrorMessage
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a value that indicates whether the model is required (that is, whether the validation attribute in the <see cref="P:System.Web.ModelBinding.DataAnnotationsModelValidator.Attribute" /> property is a <see cref="T:System.ComponentModel.DataAnnotations.RequiredAttribute" /> attribute).</summary>
		/// <returns>true if the model is required; otherwise, false.</returns>
		// Token: 0x1700176B RID: 5995
		// (get) Token: 0x06004BCE RID: 19406 RVA: 0x000CAC40 File Offset: 0x000C8E40
		public override bool IsRequired
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		// Token: 0x06004BCF RID: 19407 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected virtual string GetLocalizedErrorMessage(string errorMessage)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06004BD0 RID: 19408 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected string GetLocalizedString(string name, object[] arguments)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a list of validation error messages for the model.</summary>
		/// <returns>The list of validation error messages, or an empty list if no errors are found.</returns>
		/// <param name="container">The container for the model.</param>
		// Token: 0x06004BD1 RID: 19409 RVA: 0x0000FAB7 File Offset: 0x0000DCB7
		public override IEnumerable<ModelValidationResult> Validate(object container)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}
	}
}
