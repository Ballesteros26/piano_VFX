using System;
using System.Collections.Generic;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Maps a browser request to a collection.</summary>
	/// <typeparam name="TElement">The type of the collection.</typeparam>
	// Token: 0x020006FB RID: 1787
	public class CollectionModelBinder<TElement> : IModelBinder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.CollectionModelBinder`1" /> class.</summary>
		// Token: 0x06004B84 RID: 19332 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public CollectionModelBinder()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Binds the model by using the specified execution context and binding context.</summary>
		/// <returns>true if model binding is successful; otherwise, false.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		// Token: 0x06004B85 RID: 19333 RVA: 0x000CAB98 File Offset: 0x000C8D98
		public virtual bool BindModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Provides a way for derived classes to manipulate the collection before returning it from the binder.</summary>
		/// <returns>true in all cases.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		/// <param name="newCollection">The new collection.</param>
		// Token: 0x06004B86 RID: 19334 RVA: 0x000CABB4 File Offset: 0x000C8DB4
		protected virtual bool CreateOrReplaceCollection(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext, IList<TElement> newCollection)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}
	}
}
