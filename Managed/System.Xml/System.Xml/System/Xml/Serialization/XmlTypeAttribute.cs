using System;

namespace System.Xml.Serialization
{
	/// <summary>Controls the XML schema that is generated when the attribute target is serialized by the <see cref="T:System.Xml.Serialization.XmlSerializer" />.</summary>
	// Token: 0x02000369 RID: 873
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface)]
	public class XmlTypeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlTypeAttribute" /> class.</summary>
		// Token: 0x060023B6 RID: 9142 RVA: 0x000DC13E File Offset: 0x000DA33E
		public XmlTypeAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlTypeAttribute" /> class and specifies the name of the XML type.</summary>
		/// <param name="typeName">The name of the XML type that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> generates when it serializes the class instance (and recognizes when it deserializes the class instance). </param>
		// Token: 0x060023B7 RID: 9143 RVA: 0x000DC14D File Offset: 0x000DA34D
		public XmlTypeAttribute(string typeName)
		{
			this.typeName = typeName;
		}

		/// <summary>Gets or sets a value that determines whether the resulting schema type is an XSD anonymous type.</summary>
		/// <returns>true, if the resulting schema type is an XSD anonymous type; otherwise, false.</returns>
		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x060023B8 RID: 9144 RVA: 0x000DC163 File Offset: 0x000DA363
		// (set) Token: 0x060023B9 RID: 9145 RVA: 0x000DC16B File Offset: 0x000DA36B
		public bool AnonymousType
		{
			get
			{
				return this.anonymousType;
			}
			set
			{
				this.anonymousType = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to include the type in XML schema documents.</summary>
		/// <returns>true to include the type in XML schema documents; otherwise, false.</returns>
		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x060023BA RID: 9146 RVA: 0x000DC174 File Offset: 0x000DA374
		// (set) Token: 0x060023BB RID: 9147 RVA: 0x000DC17C File Offset: 0x000DA37C
		public bool IncludeInSchema
		{
			get
			{
				return this.includeInSchema;
			}
			set
			{
				this.includeInSchema = value;
			}
		}

		/// <summary>Gets or sets the name of the XML type.</summary>
		/// <returns>The name of the XML type.</returns>
		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x060023BC RID: 9148 RVA: 0x000DC185 File Offset: 0x000DA385
		// (set) Token: 0x060023BD RID: 9149 RVA: 0x000DC19B File Offset: 0x000DA39B
		public string TypeName
		{
			get
			{
				if (this.typeName != null)
				{
					return this.typeName;
				}
				return string.Empty;
			}
			set
			{
				this.typeName = value;
			}
		}

		/// <summary>Gets or sets the namespace of the XML type.</summary>
		/// <returns>The namespace of the XML type.</returns>
		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x060023BE RID: 9150 RVA: 0x000DC1A4 File Offset: 0x000DA3A4
		// (set) Token: 0x060023BF RID: 9151 RVA: 0x000DC1AC File Offset: 0x000DA3AC
		public string Namespace
		{
			get
			{
				return this.ns;
			}
			set
			{
				this.ns = value;
			}
		}

		// Token: 0x0400186F RID: 6255
		private bool includeInSchema = true;

		// Token: 0x04001870 RID: 6256
		private bool anonymousType;

		// Token: 0x04001871 RID: 6257
		private string ns;

		// Token: 0x04001872 RID: 6258
		private string typeName;
	}
}
