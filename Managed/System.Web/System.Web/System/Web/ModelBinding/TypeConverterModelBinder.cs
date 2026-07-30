using System;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Maps a browser request to a data object. This type is used when model binding requires conversions using a .NET Framework type converter.</summary>
	// Token: 0x02000737 RID: 1847
	public sealed class TypeConverterModelBinder : IModelBinder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.TypeConverterModelBinder" /> class.</summary>
		// Token: 0x06004C63 RID: 19555 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public TypeConverterModelBinder()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Binds the model by using the specified controller context and binding context.</summary>
		/// <returns>true if model binding is successful; otherwise, false.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		// Token: 0x06004C64 RID: 19556 RVA: 0x000CAE8C File Offset: 0x000C908C
		public bool BindModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}
	}
}
