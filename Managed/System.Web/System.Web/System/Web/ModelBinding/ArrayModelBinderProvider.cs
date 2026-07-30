using System;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a model binder for arrays.</summary>
	// Token: 0x020006EE RID: 1774
	public sealed class ArrayModelBinderProvider : ModelBinderProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ArrayModelBinderProvider" /> class.</summary>
		// Token: 0x06004B07 RID: 19207 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ArrayModelBinderProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns a model binder for arrays.</summary>
		/// <returns>A model binder object, or null if the attempt to get a model binder is unsuccessful.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		// Token: 0x06004B08 RID: 19208 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override IModelBinder GetBinder(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
