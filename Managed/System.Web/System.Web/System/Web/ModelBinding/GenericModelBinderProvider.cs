using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Gets a model binder for a generic type.</summary>
	// Token: 0x0200071B RID: 1819
	public sealed class GenericModelBinderProvider : ModelBinderProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.GenericModelBinderProvider" /> class by using the specified model type and model binder factory.</summary>
		/// <param name="modelType">The model type.</param>
		/// <param name="modelBinderFactory">The model binder factory.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="modelType" /> or <paramref name="modelBinderFactory" /> is null.</exception>
		// Token: 0x06004BFD RID: 19453 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public GenericModelBinderProvider(Type modelType, Func<Type[], IModelBinder> modelBinderFactory)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.GenericModelBinderProvider" /> class by using the specified model type and model binder type.</summary>
		/// <param name="modelType">The model type.</param>
		/// <param name="modelBinderType">The model binder type.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="modelType" /> or <paramref name="modelBinderType" /> is null.</exception>
		// Token: 0x06004BFE RID: 19454 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public GenericModelBinderProvider(Type modelType, Type modelBinderType)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.GenericModelBinderProvider" /> by using the specified model type and model binder.</summary>
		/// <param name="modelType">The model type.</param>
		/// <param name="modelBinder">The model binder.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="modelType" /> or <paramref name="modelBinder" /> is null.</exception>
		// Token: 0x06004BFF RID: 19455 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public GenericModelBinderProvider(Type modelType, IModelBinder modelBinder)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the type of the model.</summary>
		/// <returns>The type of the model.</returns>
		// Token: 0x17001771 RID: 6001
		// (get) Token: 0x06004C00 RID: 19456 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public Type ModelType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets a value that specifies whether the prefix check should be suppressed.</summary>
		/// <returns>true if the prefix check should be suppressed; otherwise, false.</returns>
		// Token: 0x17001772 RID: 6002
		// (get) Token: 0x06004C01 RID: 19457 RVA: 0x000CAD20 File Offset: 0x000C8F20
		// (set) Token: 0x06004C02 RID: 19458 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public bool SuppressPrefixCheck
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a model binder</summary>
		/// <returns>A model binder, or null if the attempt to get a model binder is unsuccessful.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		// Token: 0x06004C03 RID: 19459 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override IModelBinder GetBinder(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
