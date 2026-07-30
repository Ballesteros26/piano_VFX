using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>Allows users to control class loading and mandate what class to load.</summary>
	// Token: 0x020006EA RID: 1770
	[ComVisible(true)]
	[Serializable]
	public abstract class SerializationBinder
	{
		/// <summary>When overridden in a derived class, controls the binding of a serialized object to a type.</summary>
		/// <param name="serializedType">The type of the object the formatter creates a new instance of.</param>
		/// <param name="assemblyName">Specifies the <see cref="T:System.Reflection.Assembly" /> name of the serialized object. </param>
		/// <param name="typeName">Specifies the <see cref="T:System.Type" /> name of the serialized object. </param>
		// Token: 0x06004A97 RID: 19095 RVA: 0x0010B0CC File Offset: 0x001092CC
		public virtual void BindToName(Type serializedType, out string assemblyName, out string typeName)
		{
			assemblyName = null;
			typeName = null;
		}

		/// <summary>When overridden in a derived class, controls the binding of a serialized object to a type.</summary>
		/// <returns>The type of the object the formatter creates a new instance of.</returns>
		/// <param name="assemblyName">Specifies the <see cref="T:System.Reflection.Assembly" /> name of the serialized object. </param>
		/// <param name="typeName">Specifies the <see cref="T:System.Type" /> name of the serialized object. </param>
		// Token: 0x06004A98 RID: 19096
		public abstract Type BindToType(string assemblyName, string typeName);
	}
}
