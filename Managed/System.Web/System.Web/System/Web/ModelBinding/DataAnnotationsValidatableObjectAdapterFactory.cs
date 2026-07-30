using System;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents a factory for validators that are based on the <see cref="T:System.ComponentModel.DataAnnotations.IValidatableObject" /> interface.</summary>
	// Token: 0x02000711 RID: 1809
	public sealed class DataAnnotationsValidatableObjectAdapterFactory : MulticastDelegate
	{
		// Token: 0x06004BDE RID: 19422 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public DataAnnotationsValidatableObjectAdapterFactory(object @object, IntPtr method)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004BDF RID: 19423 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public ModelValidator Invoke(ModelMetadata metadata, ModelBindingExecutionContext context)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06004BE0 RID: 19424 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public IAsyncResult BeginInvoke(ModelMetadata metadata, ModelBindingExecutionContext context, AsyncCallback callback, object @object)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06004BE1 RID: 19425 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public ModelValidator EndInvoke(IAsyncResult result)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
