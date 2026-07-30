using System;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents a method that provides a model-binding error message.</summary>
	// Token: 0x02000722 RID: 1826
	public sealed class ModelBinderErrorMessageProvider : MulticastDelegate
	{
		// Token: 0x06004C21 RID: 19489 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ModelBinderErrorMessageProvider(object @object, IntPtr method)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004C22 RID: 19490 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string Invoke(ModelBindingExecutionContext modelBindingExecutionContext, ModelMetadata modelMetadata, object incomingValue)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06004C23 RID: 19491 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public IAsyncResult BeginInvoke(ModelBindingExecutionContext modelBindingExecutionContext, ModelMetadata modelMetadata, object incomingValue, AsyncCallback callback, object @object)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06004C24 RID: 19492 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string EndInvoke(IAsyncResult result)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
