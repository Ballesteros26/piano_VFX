using System;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a model binder for mutable objects.</summary>
	// Token: 0x0200072A RID: 1834
	public sealed class MutableObjectModelBinderProvider : ModelBinderProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.MutableObjectModelBinderProvider" /> class.</summary>
		// Token: 0x06004C3A RID: 19514 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public MutableObjectModelBinderProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns a model binder for a mutable object.</summary>
		/// <returns>The model binder.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		// Token: 0x06004C3B RID: 19515 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override IModelBinder GetBinder(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
