using System;
using System.Collections.Generic;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Maps a browser request to an array.</summary>
	/// <typeparam name="TElement">The type of the array.</typeparam>
	// Token: 0x020006FA RID: 1786
	public class ArrayModelBinder<TElement> : CollectionModelBinder<TElement>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ArrayModelBinder`1" /> class.</summary>
		// Token: 0x06004B82 RID: 19330 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ArrayModelBinder()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Converts the collection to an array.</summary>
		/// <returns>true in all cases.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		/// <param name="newCollection">The new collection.</param>
		// Token: 0x06004B83 RID: 19331 RVA: 0x000CAB7C File Offset: 0x000C8D7C
		protected override bool CreateOrReplaceCollection(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext, IList<TElement> newCollection)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}
	}
}
