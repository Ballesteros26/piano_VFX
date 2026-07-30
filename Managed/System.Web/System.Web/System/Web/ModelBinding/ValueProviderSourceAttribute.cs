using System;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a base class for value-provider attributes that can be used in method parameters to specify the source of values for model binding, such as <see cref="T:System.Web.ModelBinding.FormAttribute" />, <see cref="T:System.Web.ModelBinding.QueryStringAttribute" />, and <see cref="T:System.Web.ModelBinding.ViewStateAttribute" />.</summary>
	// Token: 0x02000706 RID: 1798
	public abstract class ValueProviderSourceAttribute : Attribute, IModelNameProvider, IValueProviderSource
	{
		/// <summary>When implemented in a derived class, returns the name of the model. The default is null.</summary>
		/// <returns>The name of the model.</returns>
		// Token: 0x06004BAC RID: 19372 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual string GetModelName()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns the value provider.</summary>
		/// <returns>The value provider.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		// Token: 0x06004BAD RID: 19373
		public abstract IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext);
	}
}
