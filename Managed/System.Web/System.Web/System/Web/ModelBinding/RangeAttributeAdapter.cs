using System;
using System.ComponentModel.DataAnnotations;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides an adapter for the <see cref="T:System.ComponentModel.DataAnnotations.RangeAttribute" /> attribute.</summary>
	// Token: 0x0200072F RID: 1839
	public sealed class RangeAttributeAdapter : DataAnnotationsModelValidator<RangeAttribute>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.RangeAttributeAdapter" /> class.</summary>
		/// <param name="metadata">The metadata.</param>
		/// <param name="context">The context.</param>
		/// <param name="attribute">The attribute.</param>
		// Token: 0x06004C4B RID: 19531 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public RangeAttributeAdapter(ModelMetadata metadata, ModelBindingExecutionContext context, RangeAttribute attribute)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004C4C RID: 19532 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected override string GetLocalizedErrorMessage(string errorMessage)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
