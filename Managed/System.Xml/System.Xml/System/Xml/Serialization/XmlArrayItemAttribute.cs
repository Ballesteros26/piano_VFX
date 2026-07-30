using System;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	/// <summary>Represents an attribute that specifies the derived types that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> can place in a serialized array.</summary>
	// Token: 0x02000327 RID: 807
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true)]
	public class XmlArrayItemAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> class.</summary>
		// Token: 0x06001E61 RID: 7777 RVA: 0x0009F79F File Offset: 0x0009D99F
		public XmlArrayItemAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> class and specifies the name of the XML element generated in the XML document.</summary>
		/// <param name="elementName">The name of the XML element. </param>
		// Token: 0x06001E62 RID: 7778 RVA: 0x000A6B89 File Offset: 0x000A4D89
		public XmlArrayItemAttribute(string elementName)
		{
			this.elementName = elementName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> class and specifies the <see cref="T:System.Type" /> that can be inserted into the serialized array.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the object to serialize. </param>
		// Token: 0x06001E63 RID: 7779 RVA: 0x000A6B98 File Offset: 0x000A4D98
		public XmlArrayItemAttribute(Type type)
		{
			this.type = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> class and specifies the name of the XML element generated in the XML document and the <see cref="T:System.Type" /> that can be inserted into the generated XML document.</summary>
		/// <param name="elementName">The name of the XML element. </param>
		/// <param name="type">The <see cref="T:System.Type" /> of the object to serialize. </param>
		// Token: 0x06001E64 RID: 7780 RVA: 0x000A6BA7 File Offset: 0x000A4DA7
		public XmlArrayItemAttribute(string elementName, Type type)
		{
			this.elementName = elementName;
			this.type = type;
		}

		/// <summary>Gets or sets the type allowed in an array.</summary>
		/// <returns>A <see cref="T:System.Type" /> that is allowed in the array.</returns>
		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001E65 RID: 7781 RVA: 0x000A6BBD File Offset: 0x000A4DBD
		// (set) Token: 0x06001E66 RID: 7782 RVA: 0x000A6BC5 File Offset: 0x000A4DC5
		public Type Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		/// <summary>Gets or sets the name of the generated XML element.</summary>
		/// <returns>The name of the generated XML element. The default is the member identifier.</returns>
		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001E67 RID: 7783 RVA: 0x000A6BCE File Offset: 0x000A4DCE
		// (set) Token: 0x06001E68 RID: 7784 RVA: 0x000A6BE4 File Offset: 0x000A4DE4
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

		/// <summary>Gets or sets the namespace of the generated XML element.</summary>
		/// <returns>The namespace of the generated XML element.</returns>
		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001E69 RID: 7785 RVA: 0x000A6BED File Offset: 0x000A4DED
		// (set) Token: 0x06001E6A RID: 7786 RVA: 0x000A6BF5 File Offset: 0x000A4DF5
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

		/// <summary>Gets or sets the level in a hierarchy of XML elements that the <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> affects.</summary>
		/// <returns>The zero-based index of a set of indexes in an array of arrays.</returns>
		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001E6B RID: 7787 RVA: 0x000A6BFE File Offset: 0x000A4DFE
		// (set) Token: 0x06001E6C RID: 7788 RVA: 0x000A6C06 File Offset: 0x000A4E06
		public int NestingLevel
		{
			get
			{
				return this.nestingLevel;
			}
			set
			{
				this.nestingLevel = value;
			}
		}

		/// <summary>Gets or sets the XML data type of the generated XML element.</summary>
		/// <returns>An XML schema definition (XSD) data type, as defined by the World Wide Web Consortium (www.w3.org) document "XML Schema Part 2: DataTypes".</returns>
		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001E6D RID: 7789 RVA: 0x000A6C0F File Offset: 0x000A4E0F
		// (set) Token: 0x06001E6E RID: 7790 RVA: 0x000A6C25 File Offset: 0x000A4E25
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

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Xml.Serialization.XmlSerializer" /> must serialize a member as an empty XML tag with the xsi:nil attribute set to true.</summary>
		/// <returns>true if the <see cref="T:System.Xml.Serialization.XmlSerializer" /> generates the xsi:nil attribute; otherwise, false, and no instance is generated. The default is true.</returns>
		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001E6F RID: 7791 RVA: 0x000A6C2E File Offset: 0x000A4E2E
		// (set) Token: 0x06001E70 RID: 7792 RVA: 0x000A6C36 File Offset: 0x000A4E36
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

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001E71 RID: 7793 RVA: 0x000A6C46 File Offset: 0x000A4E46
		internal bool IsNullableSpecified
		{
			get
			{
				return this.nullableSpecified;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the name of the generated XML element is qualified.</summary>
		/// <returns>One of the <see cref="T:System.Xml.Schema.XmlSchemaForm" /> values. The default is XmlSchemaForm.None.</returns>
		/// <exception cref="T:System.Exception">The <see cref="P:System.Xml.Serialization.XmlArrayItemAttribute.Form" /> property is set to XmlSchemaForm.Unqualified and a <see cref="P:System.Xml.Serialization.XmlArrayItemAttribute.Namespace" /> value is specified. </exception>
		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001E72 RID: 7794 RVA: 0x000A6C4E File Offset: 0x000A4E4E
		// (set) Token: 0x06001E73 RID: 7795 RVA: 0x000A6C56 File Offset: 0x000A4E56
		public XmlSchemaForm Form
		{
			get
			{
				return this.form;
			}
			set
			{
				this.form = value;
			}
		}

		// Token: 0x04001705 RID: 5893
		private string elementName;

		// Token: 0x04001706 RID: 5894
		private Type type;

		// Token: 0x04001707 RID: 5895
		private string ns;

		// Token: 0x04001708 RID: 5896
		private string dataType;

		// Token: 0x04001709 RID: 5897
		private bool nullable;

		// Token: 0x0400170A RID: 5898
		private bool nullableSpecified;

		// Token: 0x0400170B RID: 5899
		private XmlSchemaForm form;

		// Token: 0x0400170C RID: 5900
		private int nestingLevel;
	}
}
