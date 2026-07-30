using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a model binder for a simple type.</summary>
	// Token: 0x02000735 RID: 1845
	public sealed class SimpleModelBinderProvider : ModelBinderProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.SimpleModelBinderProvider" /> class by using the specified model type and the model binder factory.</summary>
		/// <param name="modelType">The model type.</param>
		/// <param name="modelBinderFactory">The model binder factory.</param>
		// Token: 0x06004C5B RID: 19547 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public SimpleModelBinderProvider(Type modelType, Func<IModelBinder> modelBinderFactory)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.SimpleModelBinderProvider" /> class by using the specified model type and the model binder.</summary>
		/// <param name="modelType">The model type.</param>
		/// <param name="modelBinder">The model binder.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="modelType" /> or <paramref name="modelBinder" /> parameter is null.</exception>
		// Token: 0x06004C5C RID: 19548 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public SimpleModelBinderProvider(Type modelType, IModelBinder modelBinder)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the type of the model.</summary>
		/// <returns>The type of the model.</returns>
		// Token: 0x17001784 RID: 6020
		// (get) Token: 0x06004C5D RID: 19549 RVA: 0x0000E80B File Offset: 0x0000CA0B
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
		// Token: 0x17001785 RID: 6021
		// (get) Token: 0x06004C5E RID: 19550 RVA: 0x000CAE70 File Offset: 0x000C9070
		// (set) Token: 0x06004C5F RID: 19551 RVA: 0x0000B3E4 File Offset: 0x000095E4
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

		/// <summary>Returns a model binder by using the specified execution context and binding context.</summary>
		/// <returns>The model binder, or null if the attempt to get a model binder is unsuccessful.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="modelBindingExecutionContext" /> or <paramref name="bindingContext" /> is null.</exception>
		// Token: 0x06004C60 RID: 19552 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override IModelBinder GetBinder(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
