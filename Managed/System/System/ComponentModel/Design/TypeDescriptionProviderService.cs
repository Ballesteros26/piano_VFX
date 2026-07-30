using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a type description provider for a specified type. </summary>
	// Token: 0x02000304 RID: 772
	public abstract class TypeDescriptionProviderService
	{
		/// <summary>Gets a type description provider for the specified object.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> that corresponds with <paramref name="instance" />.</returns>
		/// <param name="instance">The object to get a type description provider for.</param>
		// Token: 0x060018C3 RID: 6339
		public abstract TypeDescriptionProvider GetProvider(object instance);

		/// <summary>Gets a type description provider for the specified type.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> that corresponds with <paramref name="type" />.</returns>
		/// <param name="type">The type to get a type description provider for.</param>
		// Token: 0x060018C4 RID: 6340
		public abstract TypeDescriptionProvider GetProvider(Type type);
	}
}
