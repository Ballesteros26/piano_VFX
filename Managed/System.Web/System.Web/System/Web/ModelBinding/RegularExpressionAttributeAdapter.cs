using System;
using System.ComponentModel.DataAnnotations;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides an adapter for the <see cref="T:System.ComponentModel.DataAnnotations.RegularExpressionAttribute" /> attribute.</summary>
	// Token: 0x02000730 RID: 1840
	public sealed class RegularExpressionAttributeAdapter : DataAnnotationsModelValidator<RegularExpressionAttribute>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.RegularExpressionAttributeAdapter" /> class.</summary>
		/// <param name="metadata">The metadata.</param>
		/// <param name="context">The context.</param>
		/// <param name="attribute">The attribute.</param>
		// Token: 0x06004C4D RID: 19533 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public RegularExpressionAttributeAdapter(ModelMetadata metadata, ModelBindingExecutionContext context, RegularExpressionAttribute attribute)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004C4E RID: 19534 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected override string GetLocalizedErrorMessage(string errorMessage)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
