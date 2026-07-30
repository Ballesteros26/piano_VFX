using System;
using System.Collections.Generic;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a collection of model validators.</summary>
	// Token: 0x020006FE RID: 1790
	public abstract class ModelValidatorProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelValidatorProvider" /> class.</summary>
		// Token: 0x06004B93 RID: 19347 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected ModelValidatorProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a collection of validators for the model.</summary>
		/// <returns>The validators.</returns>
		/// <param name="metadata">The metadata.</param>
		/// <param name="context">The execution context.</param>
		// Token: 0x06004B94 RID: 19348
		public abstract IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ModelBindingExecutionContext context);
	}
}
