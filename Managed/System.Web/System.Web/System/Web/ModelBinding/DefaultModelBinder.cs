using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Maps a browser request to a data object. This class provides a concrete implementation of a model binder.</summary>
	// Token: 0x02000713 RID: 1811
	public class DefaultModelBinder : IModelBinder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.DefaultModelBinder" /> class.</summary>
		// Token: 0x06004BE4 RID: 19428 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public DefaultModelBinder()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns the collection of model binder providers.</summary>
		/// <returns>A collection of model binder providers.</returns>
		// Token: 0x1700176E RID: 5998
		// (get) Token: 0x06004BE5 RID: 19429 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public ModelBinderProviderCollection Providers
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Binds the model using the specified execution context and binding context.</summary>
		/// <returns>true if model binding is successful; otherwise, false.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		// Token: 0x06004BE6 RID: 19430 RVA: 0x000CAC94 File Offset: 0x000C8E94
		public bool BindModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}
	}
}
