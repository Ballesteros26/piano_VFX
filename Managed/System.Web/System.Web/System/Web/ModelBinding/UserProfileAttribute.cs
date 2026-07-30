using System;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents an attribute that specifies that values for model binding are provided by a user profile.</summary>
	// Token: 0x0200073B RID: 1851
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
	public sealed class UserProfileAttribute : Attribute, IValueProviderSource
	{
		/// <summary>Gets the value provider.</summary>
		/// <returns>A new instance of the <see cref="T:System.Web.ModelBinding.UserProfileValueProvider" /> class.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		// Token: 0x06004C6C RID: 19564 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
