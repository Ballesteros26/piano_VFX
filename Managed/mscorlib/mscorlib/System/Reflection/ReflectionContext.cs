using System;

namespace System.Reflection
{
	/// <summary>Represents a context that can provide reflection objects.</summary>
	// Token: 0x020002FD RID: 765
	public abstract class ReflectionContext
	{
		/// <summary>Gets the representation, in this reflection context, of an assembly that is represented by an object from another reflection context.</summary>
		/// <returns>The representation of the assembly in this reflection context.</returns>
		/// <param name="assembly">The external representation of the assembly to represent in this context.</param>
		// Token: 0x06002107 RID: 8455
		public abstract Assembly MapAssembly(Assembly assembly);

		/// <summary>Gets the representation, in this reflection context, of a type represented by an object from another reflection context.</summary>
		/// <returns>The representation of the type in this reflection context..</returns>
		/// <param name="type">The external representation of the type to represent in this context.</param>
		// Token: 0x06002108 RID: 8456
		public abstract TypeInfo MapType(TypeInfo type);

		/// <summary>Gets the representation of the type of the specified object in this reflection context.</summary>
		/// <returns>An object that represents the type of the specified object.</returns>
		/// <param name="value">The object to represent.</param>
		// Token: 0x06002109 RID: 8457 RVA: 0x0007EEEC File Offset: 0x0007D0EC
		public virtual TypeInfo GetTypeForObject(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return this.MapType(value.GetType().GetTypeInfo());
		}
	}
}
