using System;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Maps a browser request to a data object. This class is used when model binding does not require type conversion. </summary>
	// Token: 0x02000739 RID: 1849
	public sealed class TypeMatchModelBinder : IModelBinder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.TypeMatchModelBinder" /> class.</summary>
		// Token: 0x06004C67 RID: 19559 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public TypeMatchModelBinder()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Binds the model by using the specified execution context and binding context.</summary>
		/// <returns>true if model binding is successful; otherwise, false.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		// Token: 0x06004C68 RID: 19560 RVA: 0x000CAEA8 File Offset: 0x000C90A8
		public bool BindModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}
	}
}
