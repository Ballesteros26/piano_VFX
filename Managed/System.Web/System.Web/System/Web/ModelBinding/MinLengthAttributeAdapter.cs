using System;
using System.ComponentModel.DataAnnotations;
using Unity;

namespace System.Web.ModelBinding
{
	// Token: 0x02000720 RID: 1824
	public sealed class MinLengthAttributeAdapter : DataAnnotationsModelValidator<MinLengthAttribute>
	{
		// Token: 0x06004C0B RID: 19467 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public MinLengthAttributeAdapter(ModelMetadata metadata, ModelBindingExecutionContext context, MinLengthAttribute attribute)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004C0C RID: 19468 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected override string GetLocalizedErrorMessage(string errorMessage)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
