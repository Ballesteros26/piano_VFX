using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Specifies what type to use as a converter for the object this attribute is bound to.</summary>
	// Token: 0x020002DD RID: 733
	[AttributeUsage(AttributeTargets.All)]
	public sealed class TypeConverterAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeConverterAttribute" /> class with the default type converter, which is an empty string ("").</summary>
		// Token: 0x06001767 RID: 5991 RVA: 0x0005C4B3 File Offset: 0x0005A6B3
		public TypeConverterAttribute()
		{
			this.typeName = string.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeConverterAttribute" /> class, using the specified type as the data converter for the object this attribute is bound to.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of the converter class to use for data conversion for the object this attribute is bound to. </param>
		// Token: 0x06001768 RID: 5992 RVA: 0x0005C4C6 File Offset: 0x0005A6C6
		public TypeConverterAttribute(Type type)
		{
			this.typeName = type.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeConverterAttribute" /> class, using the specified type name as the data converter for the object this attribute is bound to.</summary>
		/// <param name="typeName">The fully qualified name of the class to use for data conversion for the object this attribute is bound to. </param>
		// Token: 0x06001769 RID: 5993 RVA: 0x0005C4DA File Offset: 0x0005A6DA
		public TypeConverterAttribute(string typeName)
		{
			typeName.ToUpper(CultureInfo.InvariantCulture);
			this.typeName = typeName;
		}

		/// <summary>Gets the fully qualified type name of the <see cref="T:System.Type" /> to use as a converter for the object this attribute is bound to.</summary>
		/// <returns>The fully qualified type name of the <see cref="T:System.Type" /> to use as a converter for the object this attribute is bound to, or an empty string ("") if none exists. The default value is an empty string ("").</returns>
		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x0600176A RID: 5994 RVA: 0x0005C4F5 File Offset: 0x0005A6F5
		public string ConverterTypeName
		{
			get
			{
				return this.typeName;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.TypeConverterAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current <see cref="T:System.ComponentModel.TypeConverterAttribute" />; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x0600176B RID: 5995 RVA: 0x0005C500 File Offset: 0x0005A700
		public override bool Equals(object obj)
		{
			TypeConverterAttribute typeConverterAttribute = obj as TypeConverterAttribute;
			return typeConverterAttribute != null && typeConverterAttribute.ConverterTypeName == this.typeName;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.TypeConverterAttribute" />.</returns>
		// Token: 0x0600176C RID: 5996 RVA: 0x0005C52A File Offset: 0x0005A72A
		public override int GetHashCode()
		{
			return this.typeName.GetHashCode();
		}

		// Token: 0x040013F7 RID: 5111
		private string typeName;

		/// <summary>Specifies the type to use as a converter for the object this attribute is bound to. </summary>
		// Token: 0x040013F8 RID: 5112
		public static readonly TypeConverterAttribute Default = new TypeConverterAttribute();
	}
}
