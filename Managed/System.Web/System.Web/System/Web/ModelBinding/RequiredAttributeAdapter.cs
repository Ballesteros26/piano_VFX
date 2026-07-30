using System;
using System.ComponentModel.DataAnnotations;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides an adapter for the <see cref="T:System.ComponentModel.DataAnnotations.RequiredAttribute" /> attribute.</summary>
	// Token: 0x02000731 RID: 1841
	public sealed class RequiredAttributeAdapter : DataAnnotationsModelValidator<RequiredAttribute>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.RequiredAttributeAdapter" /> class.</summary>
		/// <param name="metadata">The metadata.</param>
		/// <param name="context">The execution context.</param>
		/// <param name="attribute">The attribute.</param>
		// Token: 0x06004C4F RID: 19535 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public RequiredAttributeAdapter(ModelMetadata metadata, ModelBindingExecutionContext context, RequiredAttribute attribute)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
