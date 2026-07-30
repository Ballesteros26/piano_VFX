using System;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a model binder for a model that requires type conversion.</summary>
	// Token: 0x02000738 RID: 1848
	public sealed class TypeConverterModelBinderProvider : ModelBinderProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.TypeConverterModelBinderProvider" /> class.</summary>
		// Token: 0x06004C65 RID: 19557 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public TypeConverterModelBinderProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns a model binder for a model that requires type conversion.</summary>
		/// <returns>The model binder, or null if the type cannot be converted or there is no value to convert.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		// Token: 0x06004C66 RID: 19558 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override IModelBinder GetBinder(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
