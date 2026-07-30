using System;

namespace System.Xml.Serialization
{
	/// <summary>Provides mappings between code entities in .NET Framework Web service methods and the content of Web Services Description Language (WSDL) messages that are defined for SOAP Web services. </summary>
	// Token: 0x0200033F RID: 831
	public class XmlReflectionMember
	{
		/// <summary>Gets or sets the type of the Web service method member code entity that is represented by this mapping. </summary>
		/// <returns>The <see cref="T:System.Type" /> of the Web service method member code entity that is represented by this mapping.</returns>
		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001FE5 RID: 8165 RVA: 0x000AEFD6 File Offset: 0x000AD1D6
		// (set) Token: 0x06001FE6 RID: 8166 RVA: 0x000AEFDE File Offset: 0x000AD1DE
		public Type MemberType
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

		/// <summary>Gets or sets an <see cref="T:System.Xml.Serialization.XmlAttributes" /> with the collection of <see cref="T:System.Xml.Serialization.XmlSerializer" />-related attributes that have been applied to the member code entity. </summary>
		/// <returns>An <see cref="T:System.XML.Serialization.XmlAttributes" /> that represents XML attributes that have been applied to the member code.</returns>
		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001FE7 RID: 8167 RVA: 0x000AEFE7 File Offset: 0x000AD1E7
		// (set) Token: 0x06001FE8 RID: 8168 RVA: 0x000AEFEF File Offset: 0x000AD1EF
		public XmlAttributes XmlAttributes
		{
			get
			{
				return this.xmlAttributes;
			}
			set
			{
				this.xmlAttributes = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Xml.Serialization.SoapAttributes" /> with the collection of SOAP-related attributes that have been applied to the member code entity. </summary>
		/// <returns>A <see cref="T:System.Xml.Serialization.SoapAttributes" /> that contains the objects that represent SOAP attributes applied to the member.</returns>
		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001FE9 RID: 8169 RVA: 0x000AEFF8 File Offset: 0x000AD1F8
		// (set) Token: 0x06001FEA RID: 8170 RVA: 0x000AF000 File Offset: 0x000AD200
		public SoapAttributes SoapAttributes
		{
			get
			{
				return this.soapAttributes;
			}
			set
			{
				this.soapAttributes = value;
			}
		}

		/// <summary>Gets or sets the name of the Web service method member for this mapping. </summary>
		/// <returns>The name of the Web service method.</returns>
		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001FEB RID: 8171 RVA: 0x000AF009 File Offset: 0x000AD209
		// (set) Token: 0x06001FEC RID: 8172 RVA: 0x000AF01F File Offset: 0x000AD21F
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

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Xml.Serialization.XmlReflectionMember" /> represents a Web service method return value, as opposed to an output parameter. </summary>
		/// <returns>true, if the member represents a Web service return value; otherwise, false.</returns>
		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001FED RID: 8173 RVA: 0x000AF028 File Offset: 0x000AD228
		// (set) Token: 0x06001FEE RID: 8174 RVA: 0x000AF030 File Offset: 0x000AD230
		public bool IsReturnValue
		{
			get
			{
				return this.isReturnValue;
			}
			set
			{
				this.isReturnValue = value;
			}
		}

		/// <summary>Gets or sets a value that indicates that the value of the corresponding XML element definition's isNullable attribute is false.</summary>
		/// <returns>True to override the <see cref="P:System.Xml.Serialization.XmlElementAttribute.IsNullable" /> property; otherwise, false.</returns>
		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001FEF RID: 8175 RVA: 0x000AF039 File Offset: 0x000AD239
		// (set) Token: 0x06001FF0 RID: 8176 RVA: 0x000AF041 File Offset: 0x000AD241
		public bool OverrideIsNullable
		{
			get
			{
				return this.overrideIsNullable;
			}
			set
			{
				this.overrideIsNullable = value;
			}
		}

		// Token: 0x04001765 RID: 5989
		private string memberName;

		// Token: 0x04001766 RID: 5990
		private Type type;

		// Token: 0x04001767 RID: 5991
		private XmlAttributes xmlAttributes = new XmlAttributes();

		// Token: 0x04001768 RID: 5992
		private SoapAttributes soapAttributes = new SoapAttributes();

		// Token: 0x04001769 RID: 5993
		private bool isReturnValue;

		// Token: 0x0400176A RID: 5994
		private bool overrideIsNullable;
	}
}
