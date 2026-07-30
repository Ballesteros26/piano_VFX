using System;
using System.ComponentModel.DataAnnotations;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents the method that creates a <see cref="T:System.Web.ModelBinding.DataAnnotationsModelValidatorProvider" /> instance.</summary>
	// Token: 0x0200070E RID: 1806
	public sealed class DataAnnotationsModelValidationFactory : MulticastDelegate
	{
		// Token: 0x06004BC7 RID: 19399 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public DataAnnotationsModelValidationFactory(object @object, IntPtr method)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004BC8 RID: 19400 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public ModelValidator Invoke(ModelMetadata metadata, ModelBindingExecutionContext context, ValidationAttribute attribute)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06004BC9 RID: 19401 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public IAsyncResult BeginInvoke(ModelMetadata metadata, ModelBindingExecutionContext context, ValidationAttribute attribute, AsyncCallback callback, object @object)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06004BCA RID: 19402 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public ModelValidator EndInvoke(IAsyncResult result)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
