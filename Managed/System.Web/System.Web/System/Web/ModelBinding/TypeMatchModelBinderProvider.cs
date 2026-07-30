using System;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a model binder for a model that does not require type conversion.</summary>
	// Token: 0x0200073A RID: 1850
	[ModelBinderProviderOptions(FrontOfList = true)]
	public sealed class TypeMatchModelBinderProvider : ModelBinderProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.TypeMatchModelBinderProvider" /> class.</summary>
		// Token: 0x06004C69 RID: 19561 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public TypeMatchModelBinderProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns a model binder for a model that does not require type conversion.</summary>
		/// <returns>The model binder, or null if the attempt to get a model binder is unsuccessful.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		// Token: 0x06004C6A RID: 19562 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override IModelBinder GetBinder(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
