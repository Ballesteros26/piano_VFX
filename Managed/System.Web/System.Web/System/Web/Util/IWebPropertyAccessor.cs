using System;

namespace System.Web.Util
{
	/// <summary>Provides the interface for implementing property accessors.</summary>
	// Token: 0x02000141 RID: 321
	public interface IWebPropertyAccessor
	{
		/// <summary>Gets the value of a specified property.</summary>
		/// <returns>The value of the specified property.</returns>
		/// <param name="target">The property from which the value is retrieved.</param>
		// Token: 0x06000EA0 RID: 3744
		object GetProperty(object target);

		/// <summary>Sets the specified property with the given value.</summary>
		/// <param name="target">The property for which <paramref name="value" /> is set.</param>
		/// <param name="value">The object containing the value of the property.</param>
		// Token: 0x06000EA1 RID: 3745
		void SetProperty(object target, object value);
	}
}
