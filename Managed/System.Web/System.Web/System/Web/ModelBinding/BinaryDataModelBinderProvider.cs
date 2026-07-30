using System;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Gets a model binder for binary data.</summary>
	// Token: 0x020006FF RID: 1791
	public sealed class BinaryDataModelBinderProvider : ModelBinderProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.BinaryDataModelBinderProvider" /> class.</summary>
		// Token: 0x06004B95 RID: 19349 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public BinaryDataModelBinderProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns a model binder for binary data.</summary>
		/// <returns>The model binder, or null if the attempt to get a model binder is unsuccessful.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		// Token: 0x06004B96 RID: 19350 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override IModelBinder GetBinder(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
