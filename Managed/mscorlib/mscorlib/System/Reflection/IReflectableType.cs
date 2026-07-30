using System;

namespace System.Reflection
{
	/// <summary>Represents a type that you can reflect over.</summary>
	// Token: 0x020002E4 RID: 740
	public interface IReflectableType
	{
		/// <summary>Retrieves an object that represents this type.</summary>
		/// <returns>An object that represents this type.</returns>
		// Token: 0x0600206A RID: 8298
		TypeInfo GetTypeInfo();
	}
}
