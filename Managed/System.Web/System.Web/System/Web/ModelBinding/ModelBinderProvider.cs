using System;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides an abstract base class for model binder providers.</summary>
	// Token: 0x020006EF RID: 1775
	public abstract class ModelBinderProvider
	{
		/// <summary>When overridden in a derived class, initializes a new instance of the class that derives from <see cref="T:System.Web.ModelBinding.ModelBinderProvider" />.</summary>
		// Token: 0x06004B09 RID: 19209 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected ModelBinderProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>When overridden in a derived class, gets a model binder. </summary>
		/// <returns>A model binder.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		// Token: 0x06004B0A RID: 19210
		public abstract IModelBinder GetBinder(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext);
	}
}
