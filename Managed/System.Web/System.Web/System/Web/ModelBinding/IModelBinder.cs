using System;

namespace System.Web.ModelBinding
{
	/// <summary>Defines the method that is required for a model binder.</summary>
	// Token: 0x020006F2 RID: 1778
	public interface IModelBinder
	{
		/// <summary>Binds the model to a value by using the specified execution context and binding context.</summary>
		/// <returns>true if model binding is successful; otherwise, false.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		// Token: 0x06004B29 RID: 19241
		bool BindModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext);
	}
}
