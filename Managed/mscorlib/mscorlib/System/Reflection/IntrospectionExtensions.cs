using System;

namespace System.Reflection
{
	/// <summary>Contains methods for converting <see cref="T:System.Type" /> objects.</summary>
	// Token: 0x020002E1 RID: 737
	public static class IntrospectionExtensions
	{
		/// <summary>Returns the <see cref="T:System.Reflection.TypeInfo" /> representation of the specified type.</summary>
		/// <returns>The converted object.</returns>
		/// <param name="type">The type to convert.</param>
		// Token: 0x06002059 RID: 8281 RVA: 0x0007DFE4 File Offset: 0x0007C1E4
		public static TypeInfo GetTypeInfo(this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			IReflectableType reflectableType = (IReflectableType)type;
			if (reflectableType == null)
			{
				return null;
			}
			return reflectableType.GetTypeInfo();
		}
	}
}
