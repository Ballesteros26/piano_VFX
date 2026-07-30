using System;

namespace System.Xml.Serialization
{
	/// <summary>Specifies that the public member value be serialized by the <see cref="T:System.Xml.Serialization.XmlSerializer" /> as an encoded SOAP XML element.</summary>
	// Token: 0x0200030F RID: 783
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	public class SoapElementAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapElementAttribute" /> class.</summary>
		// Token: 0x06001D49 RID: 7497 RVA: 0x0009F79F File Offset: 0x0009D99F
		public SoapElementAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapElementAttribute" /> class and specifies the name of the XML element.</summary>
		/// <param name="elementName">The XML element name of the serialized member. </param>
		// Token: 0x06001D4A RID: 7498 RVA: 0x000A023D File Offset: 0x0009E43D
		public SoapElementAttribute(string elementName)
		{
			this.elementName = elementName;
		}

		/// <summary>Gets or sets the name of the generated XML element.</summary>
		/// <returns>The name of the generated XML element. The default is the member identifier.</returns>
		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001D4B RID: 7499 RVA: 0x000A024C File Offset: 0x0009E44C
		// (set) Token: 0x06001D4C RID: 7500 RVA: 0x000A0262 File Offset: 0x0009E462
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

		/// <summary>Gets or sets the XML Schema definition language (XSD) data type of the generated XML element.</summary>
		/// <returns>One of the XML Schema data types.</returns>
		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001D4D RID: 7501 RVA: 0x000A026B File Offset: 0x0009E46B
		// (set) Token: 0x06001D4E RID: 7502 RVA: 0x000A0281 File Offset: 0x0009E481
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

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Xml.Serialization.XmlSerializer" /> must serialize a member that has the xsi:null attribute set to "1".</summary>
		/// <returns>true if the <see cref="T:System.Xml.Serialization.XmlSerializer" /> generates the xsi:null attribute; otherwise, false.</returns>
		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001D4F RID: 7503 RVA: 0x000A028A File Offset: 0x0009E48A
		// (set) Token: 0x06001D50 RID: 7504 RVA: 0x000A0292 File Offset: 0x0009E492
		public bool IsNullable
		{
			get
			{
				return this.nullable;
			}
			set
			{
				this.nullable = value;
			}
		}

		// Token: 0x04001695 RID: 5781
		private string elementName;

		// Token: 0x04001696 RID: 5782
		private string dataType;

		// Token: 0x04001697 RID: 5783
		private bool nullable;
	}
}
