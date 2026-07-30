using System;

namespace System.Reflection
{
	/// <summary>Represents an object that provides a custom type.</summary>
	// Token: 0x020007D7 RID: 2007
	public interface ICustomTypeProvider
	{
		/// <summary>Gets the custom type provided by this object.</summary>
		/// <returns>The custom type. </returns>
		// Token: 0x06004028 RID: 16424
		Type GetCustomType();
	}
}
