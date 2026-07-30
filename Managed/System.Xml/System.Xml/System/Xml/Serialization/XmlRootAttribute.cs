using System;

namespace System.Xml.Serialization
{
	/// <summary>Controls XML serialization of the attribute target as an XML root element.</summary>
	// Token: 0x02000340 RID: 832
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.ReturnValue)]
	public class XmlRootAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlRootAttribute" /> class.</summary>
		// Token: 0x06001FF2 RID: 8178 RVA: 0x000AF068 File Offset: 0x000AD268
		public XmlRootAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlRootAttribute" /> class and specifies the name of the XML root element.</summary>
		/// <param name="elementName">The name of the XML root element. </param>
		// Token: 0x06001FF3 RID: 8179 RVA: 0x000AF077 File Offset: 0x000AD277
		public XmlRootAttribute(string elementName)
		{
			this.elementName = elementName;
		}

		/// <summary>Gets or sets the name of the XML element that is generated and recognized by the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class's <see cref="M:System.Xml.Serialization.XmlSerializer.Serialize(System.IO.TextWriter,System.Object)" /> and <see cref="M:System.Xml.Serialization.XmlSerializer.Deserialize(System.IO.Stream)" /> methods, respectively.</summary>
		/// <returns>The name of the XML root element that is generated and recognized in an XML-document instance. The default is the name of the serialized class.</returns>
		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001FF4 RID: 8180 RVA: 0x000AF08D File Offset: 0x000AD28D
		// (set) Token: 0x06001FF5 RID: 8181 RVA: 0x000AF0A3 File Offset: 0x000AD2A3
		public string ElementName
		{
			get
			{
				if (this.elementName != null)
				{
					return this.elementName;
				}
				return string.Empty;
			}
			set
			{
				this.elementName = value;
			}
		}

		/// <summary>Gets or sets the namespace for the XML root element.</summary>
		/// <returns>The namespace for the XML element.</returns>
		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001FF6 RID: 8182 RVA: 0x000AF0AC File Offset: 0x000AD2AC
		// (set) Token: 0x06001FF7 RID: 8183 RVA: 0x000AF0B4 File Offset: 0x000AD2B4
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

		/// <summary>Gets or sets the XSD data type of the XML root element.</summary>
		/// <returns>An XSD (XML Schema Document) data type, as defined by the World Wide Web Consortium (www.w3.org) document named "XML Schema: DataTypes".</returns>
		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001FF8 RID: 8184 RVA: 0x000AF0BD File Offset: 0x000AD2BD
		// (set) Token: 0x06001FF9 RID: 8185 RVA: 0x000AF0D3 File Offset: 0x000AD2D3
		public string DataType
		{
			get
			{
				if (this.dataType != null)
				{
					return this.dataType;
				}
				return string.Empty;
			}
			set
			{
				this.dataType = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Xml.Serialization.XmlSerializer" /> must serialize a member that is set to null into the xsi:nil attribute set to true.</summary>
		/// <returns>true if the <see cref="T:System.Xml.Serialization.XmlSerializer" /> generates the xsi:nil attribute; otherwise, false.</returns>
		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001FFA RID: 8186 RVA: 0x000AF0DC File Offset: 0x000AD2DC
		// (set) Token: 0x06001FFB RID: 8187 RVA: 0x000AF0E4 File Offset: 0x000AD2E4
		public bool IsNullable
		{
			get
			{
				return this.nullable;
			}
			set
			{
				this.nullable = value;
				this.nullableSpecified = true;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001FFC RID: 8188 RVA: 0x000AF0F4 File Offset: 0x000AD2F4
		internal bool IsNullableSpecified
		{
			get
			{
				return this.nullableSpecified;
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001FFD RID: 8189 RVA: 0x000AF0FC File Offset: 0x000AD2FC
		internal string Key
		{
			get
			{
				return string.Concat(new string[]
				{
					(this.ns == null) ? string.Empty : this.ns,
					":",
					this.ElementName,
					":",
					this.nullable.ToString()
				});
			}
		}

		// Token: 0x0400176B RID: 5995
		private string elementName;

		// Token: 0x0400176C RID: 5996
		private string ns;

		// Token: 0x0400176D RID: 5997
		private string dataType;

		// Token: 0x0400176E RID: 5998
		private bool nullable = true;

		// Token: 0x0400176F RID: 5999
		private bool nullableSpecified;
	}
}
