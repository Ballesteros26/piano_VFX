using System;
using System.ComponentModel;
using System.Reflection;

namespace System.Xml.Serialization
{
	/// <summary>Represents a collection of attribute objects that control how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes and deserializes SOAP methods.</summary>
	// Token: 0x0200030D RID: 781
	public class SoapAttributes
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapAttributes" /> class.</summary>
		// Token: 0x06001D28 RID: 7464 RVA: 0x000020FD File Offset: 0x000002FD
		public SoapAttributes()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapAttributes" /> class using the specified custom type.</summary>
		/// <param name="provider">Any object that implements the <see cref="T:System.Reflection.ICustomAttributeProvider" /> interface, such as the <see cref="T:System.Type" /> class.</param>
		// Token: 0x06001D29 RID: 7465 RVA: 0x0009F8D4 File Offset: 0x0009DAD4
		public SoapAttributes(ICustomAttributeProvider provider)
		{
			object[] customAttributes = provider.GetCustomAttributes(false);
			for (int i = 0; i < customAttributes.Length; i++)
			{
				if (customAttributes[i] is SoapIgnoreAttribute || customAttributes[i] is ObsoleteAttribute)
				{
					this.soapIgnore = true;
					break;
				}
				if (customAttributes[i] is SoapElementAttribute)
				{
					this.soapElement = (SoapElementAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is SoapAttributeAttribute)
				{
					this.soapAttribute = (SoapAttributeAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is SoapTypeAttribute)
				{
					this.soapType = (SoapTypeAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is SoapEnumAttribute)
				{
					this.soapEnum = (SoapEnumAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is DefaultValueAttribute)
				{
					this.soapDefaultValue = ((DefaultValueAttribute)customAttributes[i]).Value;
				}
			}
			if (this.soapIgnore)
			{
				this.soapElement = null;
				this.soapAttribute = null;
				this.soapType = null;
				this.soapEnum = null;
				this.soapDefaultValue = null;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001D2A RID: 7466 RVA: 0x0009F9D4 File Offset: 0x0009DBD4
		internal SoapAttributeFlags SoapFlags
		{
			get
			{
				SoapAttributeFlags soapAttributeFlags = (SoapAttributeFlags)0;
				if (this.soapElement != null)
				{
					soapAttributeFlags |= SoapAttributeFlags.Element;
				}
				if (this.soapAttribute != null)
				{
					soapAttributeFlags |= SoapAttributeFlags.Attribute;
				}
				if (this.soapEnum != null)
				{
					soapAttributeFlags |= SoapAttributeFlags.Enum;
				}
				if (this.soapType != null)
				{
					soapAttributeFlags |= SoapAttributeFlags.Type;
				}
				return soapAttributeFlags;
			}
		}

		/// <summary>Gets or sets an object that instructs the <see cref="T:System.Xml.Serialization.XmlSerializer" /> how to serialize an object type into encoded SOAP XML.</summary>
		/// <returns>A <see cref="T:System.Xml.Serialization.SoapTypeAttribute" /> that either overrides a <see cref="T:System.Xml.Serialization.SoapTypeAttribute" /> applied to a class declaration, or is applied to a class declaration.</returns>
		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001D2B RID: 7467 RVA: 0x0009FA14 File Offset: 0x0009DC14
		// (set) Token: 0x06001D2C RID: 7468 RVA: 0x0009FA1C File Offset: 0x0009DC1C
		public SoapTypeAttribute SoapType
		{
			get
			{
				return this.soapType;
			}
			set
			{
				this.soapType = value;
			}
		}

		/// <summary>Gets or sets an object that specifies how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes a SOAP enumeration.</summary>
		/// <returns>An object that specifies how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes an enumeration member.</returns>
		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001D2D RID: 7469 RVA: 0x0009FA25 File Offset: 0x0009DC25
		// (set) Token: 0x06001D2E RID: 7470 RVA: 0x0009FA2D File Offset: 0x0009DC2D
		public SoapEnumAttribute SoapEnum
		{
			get
			{
				return this.soapEnum;
			}
			set
			{
				this.soapEnum = value;
			}
		}

		/// <summary>Gets or sets a value that specifies whether the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes a public field or property as encoded SOAP XML.</summary>
		/// <returns>true if the <see cref="T:System.Xml.Serialization.XmlSerializer" /> must not serialize the field or property; otherwise, false.</returns>
		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001D2F RID: 7471 RVA: 0x0009FA36 File Offset: 0x0009DC36
		// (set) Token: 0x06001D30 RID: 7472 RVA: 0x0009FA3E File Offset: 0x0009DC3E
		public bool SoapIgnore
		{
			get
			{
				return this.soapIgnore;
			}
			set
			{
				this.soapIgnore = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Xml.Serialization.SoapElementAttribute" /> to override.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.SoapElementAttribute" /> to override.</returns>
		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001D31 RID: 7473 RVA: 0x0009FA47 File Offset: 0x0009DC47
		// (set) Token: 0x06001D32 RID: 7474 RVA: 0x0009FA4F File Offset: 0x0009DC4F
		public SoapElementAttribute SoapElement
		{
			get
			{
				return this.soapElement;
			}
			set
			{
				this.soapElement = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Serialization.SoapAttributeAttribute" /> to override.</summary>
		/// <returns>A <see cref="T:System.Xml.Serialization.SoapAttributeAttribute" /> that overrides the behavior of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> when the member is serialized.</returns>
		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001D33 RID: 7475 RVA: 0x0009FA58 File Offset: 0x0009DC58
		// (set) Token: 0x06001D34 RID: 7476 RVA: 0x0009FA60 File Offset: 0x0009DC60
		public SoapAttributeAttribute SoapAttribute
		{
			get
			{
				return this.soapAttribute;
			}
			set
			{
				this.soapAttribute = value;
			}
		}

		/// <summary>Gets or sets the default value of an XML element or attribute.</summary>
		/// <returns>An object that represents the default value of an XML element or attribute.</returns>
		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001D35 RID: 7477 RVA: 0x0009FA69 File Offset: 0x0009DC69
		// (set) Token: 0x06001D36 RID: 7478 RVA: 0x0009FA71 File Offset: 0x0009DC71
		public object SoapDefaultValue
		{
			get
			{
				return this.soapDefaultValue;
			}
			set
			{
				this.soapDefaultValue = value;
			}
		}

		// Token: 0x0400168F RID: 5775
		private bool soapIgnore;

		// Token: 0x04001690 RID: 5776
		private SoapTypeAttribute soapType;

		// Token: 0x04001691 RID: 5777
		private SoapElementAttribute soapElement;

		// Token: 0x04001692 RID: 5778
		private SoapAttributeAttribute soapAttribute;

		// Token: 0x04001693 RID: 5779
		private SoapEnumAttribute soapEnum;

		// Token: 0x04001694 RID: 5780
		private object soapDefaultValue;
	}
}
