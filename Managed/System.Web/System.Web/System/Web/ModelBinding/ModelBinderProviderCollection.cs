using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a container for a collection of model binder providers.</summary>
	// Token: 0x020006F1 RID: 1777
	public sealed class ModelBinderProviderCollection : Collection<ModelBinderProvider>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelBinderProviderCollection" /> class.</summary>
		// Token: 0x06004B1F RID: 19231 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ModelBinderProviderCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelBinderProviderCollection" /> class by using the specified list of model binder providers.</summary>
		/// <param name="list">The list of model binder providers.</param>
		// Token: 0x06004B20 RID: 19232 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ModelBinderProviderCollection(IList<ModelBinderProvider> list)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns the appropriate model binder for the specified execution context and binding context.</summary>
		/// <returns>The model binder.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="modelBindingExecutionContext" /> or the <paramref name="bindingContext" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">No model binder or model binder provider exists for the specified model type.</exception>
		// Token: 0x06004B21 RID: 19233 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public IModelBinder GetBinder(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06004B22 RID: 19234 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected override void InsertItem(int index, ModelBinderProvider item)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Adds a model binder to the collection for a generic type by using the specified model type and model binder factory.</summary>
		/// <param name="modelType">The model type.</param>
		/// <param name="modelBinderFactory">The model binder factory.</param>
		// Token: 0x06004B23 RID: 19235 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void RegisterBinderForGenericType(Type modelType, Func<Type[], IModelBinder> modelBinderFactory)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Adds a model binder provider to the collection for a generic type by using the specified model type and model binder type.</summary>
		/// <param name="modelType">The model type.</param>
		/// <param name="modelBinderType">The model binder type.</param>
		// Token: 0x06004B24 RID: 19236 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void RegisterBinderForGenericType(Type modelType, Type modelBinderType)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Adds a model binder provider to the collection for a generic type by using the specified model type and model binder.</summary>
		/// <param name="modelType">The model type.</param>
		/// <param name="modelBinder">The model binder.</param>
		// Token: 0x06004B25 RID: 19237 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void RegisterBinderForGenericType(Type modelType, IModelBinder modelBinder)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Adds a model binder provider to the collection for a non-generic type by using the specified model type and model binder factory.</summary>
		/// <param name="modelType">The model type.</param>
		/// <param name="modelBinderFactory">The model binder factory.</param>
		// Token: 0x06004B26 RID: 19238 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void RegisterBinderForType(Type modelType, Func<IModelBinder> modelBinderFactory)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Adds a model binder provider to the collection for a non-generic type by using the specified model type and model binder.</summary>
		/// <param name="modelType">The model type.</param>
		/// <param name="modelBinder">The model binder.</param>
		// Token: 0x06004B27 RID: 19239 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void RegisterBinderForType(Type modelType, IModelBinder modelBinder)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004B28 RID: 19240 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected override void SetItem(int index, ModelBinderProvider item)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
