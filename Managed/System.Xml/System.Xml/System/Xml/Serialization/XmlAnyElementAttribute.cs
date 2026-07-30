using System;

namespace System.Xml.Serialization
{
	/// <summary>Specifies that the member (a field that returns an array of <see cref="T:System.Xml.XmlElement" /> or <see cref="T:System.Xml.XmlNode" /> objects) contains objects that represent any XML element that has no corresponding member in the object being serialized or deserialized.</summary>
	// Token: 0x02000324 RID: 804
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true)]
	public class XmlAnyElementAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> class.</summary>
		// Token: 0x06001E42 RID: 7746 RVA: 0x000A69B6 File Offset: 0x000A4BB6
		public XmlAnyElementAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> class and specifies the XML element name generated in the XML document.</summary>
		/// <param name="name">The name of the XML element that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> generates. </param>
		// Token: 0x06001E43 RID: 7747 RVA: 0x000A69C5 File Offset: 0x000A4BC5
		public XmlAnyElementAttribute(string name)
		{
			this.name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> class and specifies the XML element name generated in the XML document and its XML namespace.</summary>
		/// <param name="name">The name of the XML element that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> generates. </param>
		/// <param name="ns">The XML namespace of the XML element. </param>
		// Token: 0x06001E44 RID: 7748 RVA: 0x000A69DB File Offset: 0x000A4BDB
		public XmlAnyElementAttribute(string name, string ns)
		{
			this.name = name;
			this.ns = ns;
			this.nsSpecified = true;
		}

		/// <summary>Gets or sets the XML element name.</summary>
		/// <returns>The name of the XML element.</returns>
		/// <exception cref="T:System.InvalidOperationException">The element name of an array member does not match the element name specified by the <see cref="P:System.Xml.Serialization.XmlAnyElementAttribute.Name" /> property. </exception>
		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001E45 RID: 7749 RVA: 0x000A69FF File Offset: 0x000A4BFF
		// (set) Token: 0x06001E46 RID: 7750 RVA: 0x000A6A15 File Offset: 0x000A4C15
		public string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the XML namespace generated in the XML document.</summary>
		/// <returns>An XML namespace.</returns>
		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001E47 RID: 7751 RVA: 0x000A6A1E File Offset: 0x000A4C1E
		// (set) Token: 0x06001E48 RID: 7752 RVA: 0x000A6A26 File Offset: 0x000A4C26
		public string Namespace
		{
			get
			{
				return this.ns;
			}
			set
			{
				this.ns = value;
				this.nsSpecified = true;
			}
		}

		/// <summary>Gets or sets the explicit order in which the elements are serialized or deserialized.</summary>
		/// <returns>The order of the code generation.</returns>
		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001E49 RID: 7753 RVA: 0x000A6A36 File Offset: 0x000A4C36
		// (set) Token: 0x06001E4A RID: 7754 RVA: 0x000A6A3E File Offset: 0x000A4C3E
		public int Order
		{
			get
			{
				return this.order;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(Res.GetString("Negative values are prohibited."), "Order");
				}
				this.order = value;
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001E4B RID: 7755 RVA: 0x000A6A60 File Offset: 0x000A4C60
		internal bool NamespaceSpecified
		{
			get
			{
				return this.nsSpecified;
			}
		}

		// Token: 0x040016FC RID: 5884
		private string name;

		// Token: 0x040016FD RID: 5885
		private string ns;

		// Token: 0x040016FE RID: 5886
		private int order = -1;

		// Token: 0x040016FF RID: 5887
		private bool nsSpecified;
	}
}
