using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Provides custom attributes for reflection objects that support them.</summary>
	// Token: 0x020002DF RID: 735
	[ComVisible(true)]
	public interface ICustomAttributeProvider
	{
		/// <summary>Returns an array of custom attributes defined on this member, identified by type, or an empty array if there are no custom attributes of that type.</summary>
		/// <returns>An array of Objects representing custom attributes, or an empty array.</returns>
		/// <param name="attributeType">The type of the custom attributes. </param>
		/// <param name="inherit">When true, look up the hierarchy chain for the inherited custom attribute. </param>
		/// <exception cref="T:System.TypeLoadException">The custom attribute type cannot be loaded. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is null.</exception>
		// Token: 0x06002056 RID: 8278
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		/// <summary>Returns an array of all of the custom attributes defined on this member, excluding named attributes, or an empty array if there are no custom attributes.</summary>
		/// <returns>An array of Objects representing custom attributes, or an empty array.</returns>
		/// <param name="inherit">When true, look up the hierarchy chain for the inherited custom attribute. </param>
		/// <exception cref="T:System.TypeLoadException">The custom attribute type cannot be loaded. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">There is more than one attribute of type <paramref name="attributeType" /> defined on this member. </exception>
		// Token: 0x06002057 RID: 8279
		object[] GetCustomAttributes(bool inherit);

		/// <summary>Indicates whether one or more instance of <paramref name="attributeType" /> is defined on this member.</summary>
		/// <returns>true if the <paramref name="attributeType" /> is defined on this member; false otherwise.</returns>
		/// <param name="attributeType">The type of the custom attributes. </param>
		/// <param name="inherit">When true, look up the hierarchy chain for the inherited custom attribute. </param>
		// Token: 0x06002058 RID: 8280
		bool IsDefined(Type attributeType, bool inherit);
	}
}
