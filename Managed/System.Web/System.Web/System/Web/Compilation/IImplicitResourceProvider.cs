using System;
using System.Collections;
using System.Globalization;

namespace System.Web.Compilation
{
	/// <summary>Defines methods a class implements to act as an implicit resource provider.</summary>
	// Token: 0x02000609 RID: 1545
	public interface IImplicitResourceProvider
	{
		/// <summary>Gets an object representing the value of the specified resource key.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the localized value of an implicit resource key.</returns>
		/// <param name="key">The resource key containing the prefix, filter, and property.</param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> that represents the culture for which the resource is localized.</param>
		// Token: 0x060042AA RID: 17066
		object GetObject(ImplicitResourceKey key, CultureInfo culture);

		/// <summary>Gets a collection of implicit resource keys as specified by the prefix.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> of implicit resource keys.</returns>
		/// <param name="keyPrefix">The prefix of the implicit resource keys to be collected.</param>
		// Token: 0x060042AB RID: 17067
		ICollection GetImplicitResourceKeys(string keyPrefix);
	}
}
