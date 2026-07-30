using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the custom type description provider for a class. This class cannot be inherited.</summary>
	// Token: 0x020002E0 RID: 736
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	public sealed class TypeDescriptionProviderAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeDescriptionProviderAttribute" /> class using the specified type name.</summary>
		/// <param name="typeName">The qualified name of the type.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null.</exception>
		// Token: 0x0600177E RID: 6014 RVA: 0x0005C74B File Offset: 0x0005A94B
		public TypeDescriptionProviderAttribute(string typeName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			this._typeName = typeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeDescriptionProviderAttribute" /> class using the specified type.</summary>
		/// <param name="type">The type to store in the attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x0600177F RID: 6015 RVA: 0x0005C768 File Offset: 0x0005A968
		public TypeDescriptionProviderAttribute(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this._typeName = type.AssemblyQualifiedName;
		}

		/// <summary>Gets the type name for the type description provider.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the qualified type name for the <see cref="T:System.ComponentModel.TypeDescriptionProvider" />.</returns>
		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001780 RID: 6016 RVA: 0x0005C790 File Offset: 0x0005A990
		public string TypeName
		{
			get
			{
				return this._typeName;
			}
		}

		// Token: 0x040013FB RID: 5115
		private string _typeName;
	}
}
