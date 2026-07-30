using System;

namespace System.Xml.Serialization
{
	/// <summary>Represents certain attributes of a XSD &lt;part&gt; element in a WSDL document for generating classes from the document. </summary>
	// Token: 0x02000316 RID: 790
	public class SoapSchemaMember
	{
		/// <summary>Gets or sets a value that corresponds to the type attribute of the WSDL part element.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlQualifiedName" /> that corresponds to the XML type.</returns>
		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001DB5 RID: 7605 RVA: 0x000A3D35 File Offset: 0x000A1F35
		// (set) Token: 0x06001DB6 RID: 7606 RVA: 0x000A3D3D File Offset: 0x000A1F3D
		public XmlQualifiedName MemberType
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

		/// <summary>Gets or sets a value that corresponds to the name attribute of the WSDL part element. </summary>
		/// <returns>The element name.</returns>
		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001DB7 RID: 7607 RVA: 0x000A3D46 File Offset: 0x000A1F46
		// (set) Token: 0x06001DB8 RID: 7608 RVA: 0x000A3D5C File Offset: 0x000A1F5C
		public string MemberName
		{
			get
			{
				if (this.memberName != null)
				{
					return this.memberName;
				}
				return string.Empty;
			}
			set
			{
				this.memberName = value;
			}
		}

		// Token: 0x040016A9 RID: 5801
		private string memberName;

		// Token: 0x040016AA RID: 5802
		private XmlQualifiedName type = XmlQualifiedName.Empty;
	}
}
