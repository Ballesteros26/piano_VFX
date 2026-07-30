using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a base class for implementing validation logic.</summary>
	// Token: 0x020006F5 RID: 1781
	public abstract class ModelValidator
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelValidator" /> class.</summary>
		/// <param name="metadata">The metadata.</param>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="metadata" /> or <paramref name="modelBindingExecutionContext" /> parameter is null.</exception>
		// Token: 0x06004B61 RID: 19297 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected ModelValidator(ModelMetadata metadata, ModelBindingExecutionContext modelBindingExecutionContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>When implemented in a derived class, gets a value that indicates whether the model is required.</summary>
		/// <returns>true if the model is required; otherwise false. The default is false.</returns>
		// Token: 0x17001750 RID: 5968
		// (get) Token: 0x06004B62 RID: 19298 RVA: 0x000CAB28 File Offset: 0x000C8D28
		public virtual bool IsRequired
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>When implemented in a derived class, gets the metadata for the model validator.</summary>
		/// <returns>The metadata.</returns>
		// Token: 0x17001751 RID: 5969
		// (get) Token: 0x06004B63 RID: 19299 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected internal ModelMetadata Metadata
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>When implemented in a derived class, gets the execution context.</summary>
		/// <returns>The execution context.</returns>
		// Token: 0x17001752 RID: 5970
		// (get) Token: 0x06004B64 RID: 19300 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected internal ModelBindingExecutionContext ModelBindingExecutionContext
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Returns a composite model validator for the model.</summary>
		/// <returns>The composite model validator for the model.</returns>
		/// <param name="metadata">The metadata.</param>
		/// <param name="context">The execution context.</param>
		// Token: 0x06004B65 RID: 19301 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public static ModelValidator GetModelValidator(ModelMetadata metadata, ModelBindingExecutionContext context)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>When implemented in a derived class, validates the object.</summary>
		/// <returns>A collection of validation results.</returns>
		/// <param name="container">The container.</param>
		// Token: 0x06004B66 RID: 19302
		public abstract IEnumerable<ModelValidationResult> Validate(object container);
	}
}
