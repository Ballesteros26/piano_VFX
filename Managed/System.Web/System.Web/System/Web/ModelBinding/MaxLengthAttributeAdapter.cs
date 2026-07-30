using System;
using System.ComponentModel.DataAnnotations;
using Unity;

namespace System.Web.ModelBinding
{
	// Token: 0x0200071F RID: 1823
	public sealed class MaxLengthAttributeAdapter : DataAnnotationsModelValidator<MaxLengthAttribute>
	{
		// Token: 0x06004C09 RID: 19465 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public MaxLengthAttributeAdapter(ModelMetadata metadata, ModelBindingExecutionContext context, MaxLengthAttribute attribute)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004C0A RID: 19466 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected override string GetLocalizedErrorMessage(string errorMessage)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
