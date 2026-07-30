using System;

namespace System.Web.ModelBinding
{
	/// <summary>Defines the method that is required for a value provider source.</summary>
	// Token: 0x0200051F RID: 1311
	public interface IValueProviderSource
	{
		/// <summary>Returns a value provider.</summary>
		/// <returns>The value provider.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		// Token: 0x060039EA RID: 14826
		IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext);
	}
}
