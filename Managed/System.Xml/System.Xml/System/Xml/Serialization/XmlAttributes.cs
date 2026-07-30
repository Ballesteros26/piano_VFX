using System;
using System.ComponentModel;
using System.Reflection;

namespace System.Xml.Serialization
{
	/// <summary>Represents a collection of attribute objects that control how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes and deserializes an object.</summary>
	// Token: 0x0200032C RID: 812
	public class XmlAttributes
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlAttributes" /> class.</summary>
		// Token: 0x06001E90 RID: 7824 RVA: 0x000A6DE7 File Offset: 0x000A4FE7
		public XmlAttributes()
		{
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001E91 RID: 7825 RVA: 0x000A6E10 File Offset: 0x000A5010
		internal XmlAttributeFlags XmlFlags
		{
			get
			{
				XmlAttributeFlags xmlAttributeFlags = (XmlAttributeFlags)0;
				if (this.xmlElements.Count > 0)
				{
					xmlAttributeFlags |= XmlAttributeFlags.Elements;
				}
				if (this.xmlArrayItems.Count > 0)
				{
					xmlAttributeFlags |= XmlAttributeFlags.ArrayItems;
				}
				if (this.xmlAnyElements.Count > 0)
				{
					xmlAttributeFlags |= XmlAttributeFlags.AnyElements;
				}
				if (this.xmlArray != null)
				{
					xmlAttributeFlags |= XmlAttributeFlags.Array;
				}
				if (this.xmlAttribute != null)
				{
					xmlAttributeFlags |= XmlAttributeFlags.Attribute;
				}
				if (this.xmlText != null)
				{
					xmlAttributeFlags |= XmlAttributeFlags.Text;
				}
				if (this.xmlEnum != null)
				{
					xmlAttributeFlags |= XmlAttributeFlags.Enum;
				}
				if (this.xmlRoot != null)
				{
					xmlAttributeFlags |= XmlAttributeFlags.Root;
				}
				if (this.xmlType != null)
				{
					xmlAttributeFlags |= XmlAttributeFlags.Type;
				}
				if (this.xmlAnyAttribute != null)
				{
					xmlAttributeFlags |= XmlAttributeFlags.AnyAttribute;
				}
				if (this.xmlChoiceIdentifier != null)
				{
					xmlAttributeFlags |= XmlAttributeFlags.ChoiceIdentifier;
				}
				if (this.xmlns)
				{
					xmlAttributeFlags |= XmlAttributeFlags.XmlnsDeclarations;
				}
				return xmlAttributeFlags;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001E92 RID: 7826 RVA: 0x000A6EDC File Offset: 0x000A50DC
		private static Type IgnoreAttribute
		{
			get
			{
				if (XmlAttributes.ignoreAttributeType == null)
				{
					XmlAttributes.ignoreAttributeType = typeof(object).Assembly.GetType("System.XmlIgnoreMemberAttribute");
					if (XmlAttributes.ignoreAttributeType == null)
					{
						XmlAttributes.ignoreAttributeType = typeof(XmlIgnoreAttribute);
					}
				}
				return XmlAttributes.ignoreAttributeType;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlAttributes" /> class and customizes how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes and deserializes an object. </summary>
		/// <param name="provider">A class that can provide alternative implementations of attributes that control XML serialization.</param>
		// Token: 0x06001E93 RID: 7827 RVA: 0x000A6F40 File Offset: 0x000A5140
		public XmlAttributes(ICustomAttributeProvider provider)
		{
			object[] customAttributes = provider.GetCustomAttributes(false);
			XmlAnyElementAttribute xmlAnyElementAttribute = null;
			for (int i = 0; i < customAttributes.Length; i++)
			{
				if (customAttributes[i] is XmlIgnoreAttribute || customAttributes[i] is ObsoleteAttribute || customAttributes[i].GetType() == XmlAttributes.IgnoreAttribute)
				{
					this.xmlIgnore = true;
					break;
				}
				if (customAttributes[i] is XmlElementAttribute)
				{
					this.xmlElements.Add((XmlElementAttribute)customAttributes[i]);
				}
				else if (customAttributes[i] is XmlArrayItemAttribute)
				{
					this.xmlArrayItems.Add((XmlArrayItemAttribute)customAttributes[i]);
				}
				else if (customAttributes[i] is XmlAnyElementAttribute)
				{
					XmlAnyElementAttribute xmlAnyElementAttribute2 = (XmlAnyElementAttribute)customAttributes[i];
					if ((xmlAnyElementAttribute2.Name == null || xmlAnyElementAttribute2.Name.Length == 0) && xmlAnyElementAttribute2.NamespaceSpecified && xmlAnyElementAttribute2.Namespace == null)
					{
						xmlAnyElementAttribute = xmlAnyElementAttribute2;
					}
					else
					{
						this.xmlAnyElements.Add((XmlAnyElementAttribute)customAttributes[i]);
					}
				}
				else if (customAttributes[i] is DefaultValueAttribute)
				{
					this.xmlDefaultValue = ((DefaultValueAttribute)customAttributes[i]).Value;
				}
				else if (customAttributes[i] is XmlAttributeAttribute)
				{
					this.xmlAttribute = (XmlAttributeAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is XmlArrayAttribute)
				{
					this.xmlArray = (XmlArrayAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is XmlTextAttribute)
				{
					this.xmlText = (XmlTextAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is XmlEnumAttribute)
				{
					this.xmlEnum = (XmlEnumAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is XmlRootAttribute)
				{
					this.xmlRoot = (XmlRootAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is XmlTypeAttribute)
				{
					this.xmlType = (XmlTypeAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is XmlAnyAttributeAttribute)
				{
					this.xmlAnyAttribute = (XmlAnyAttributeAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is XmlChoiceIdentifierAttribute)
				{
					this.xmlChoiceIdentifier = (XmlChoiceIdentifierAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is XmlNamespaceDeclarationsAttribute)
				{
					this.xmlns = true;
				}
			}
			if (this.xmlIgnore)
			{
				this.xmlElements.Clear();
				this.xmlArrayItems.Clear();
				this.xmlAnyElements.Clear();
				this.xmlDefaultValue = null;
				this.xmlAttribute = null;
				this.xmlArray = null;
				this.xmlText = null;
				this.xmlEnum = null;
				this.xmlType = null;
				this.xmlAnyAttribute = null;
				this.xmlChoiceIdentifier = null;
				this.xmlns = false;
				return;
			}
			if (xmlAnyElementAttribute != null)
			{
				this.xmlAnyElements.Add(xmlAnyElementAttribute);
			}
		}

		// Token: 0x06001E94 RID: 7828 RVA: 0x000A71EC File Offset: 0x000A53EC
		internal static object GetAttr(ICustomAttributeProvider provider, Type attrType)
		{
			object[] customAttributes = provider.GetCustomAttributes(attrType, false);
			if (customAttributes.Length == 0)
			{
				return null;
			}
			return customAttributes[0];
		}

		/// <summary>Gets a collection of objects that specify how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes a public field or read/write property as an XML element.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlElementAttributes" /> that contains a collection of <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> objects.</returns>
		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001E95 RID: 7829 RVA: 0x000A720B File Offset: 0x000A540B
		public XmlElementAttributes XmlElements
		{
			get
			{
				return this.xmlElements;
			}
		}

		/// <summary>Gets or sets an object that specifies how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes a public field or public read/write property as an XML attribute.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlAttributeAttribute" /> that controls the serialization of a public field or read/write property as an XML attribute.</returns>
		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001E96 RID: 7830 RVA: 0x000A7213 File Offset: 0x000A5413
		// (set) Token: 0x06001E97 RID: 7831 RVA: 0x000A721B File Offset: 0x000A541B
		public XmlAttributeAttribute XmlAttribute
		{
			get
			{
				return this.xmlAttribute;
			}
			set
			{
				this.xmlAttribute = value;
			}
		}

		/// <summary>Gets or sets an object that specifies how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes an enumeration member.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlEnumAttribute" /> that specifies how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes an enumeration member.</returns>
		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001E98 RID: 7832 RVA: 0x000A7224 File Offset: 0x000A5424
		// (set) Token: 0x06001E99 RID: 7833 RVA: 0x000A722C File Offset: 0x000A542C
		public XmlEnumAttribute XmlEnum
		{
			get
			{
				return this.xmlEnum;
			}
			set
			{
				this.xmlEnum = value;
			}
		}

		/// <summary>Gets or sets an object that instructs the <see cref="T:System.Xml.Serialization.XmlSerializer" /> to serialize a public field or public read/write property as XML text.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlTextAttribute" /> that overrides the default serialization of a public property or field.</returns>
		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001E9A RID: 7834 RVA: 0x000A7235 File Offset: 0x000A5435
		// (set) Token: 0x06001E9B RID: 7835 RVA: 0x000A723D File Offset: 0x000A543D
		public XmlTextAttribute XmlText
		{
			get
			{
				return this.xmlText;
			}
			set
			{
				this.xmlText = value;
			}
		}

		/// <summary>Gets or sets an object that specifies how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes a public field or read/write property that returns an array.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlArrayAttribute" /> that specifies how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes a public field or read/write property that returns an array.</returns>
		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001E9C RID: 7836 RVA: 0x000A7246 File Offset: 0x000A5446
		// (set) Token: 0x06001E9D RID: 7837 RVA: 0x000A724E File Offset: 0x000A544E
		public XmlArrayAttribute XmlArray
		{
			get
			{
				return this.xmlArray;
			}
			set
			{
				this.xmlArray = value;
			}
		}

		/// <summary>Gets or sets a collection of objects that specify how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes items inserted into an array returned by a public field or read/write property.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlArrayItemAttributes" /> object that contains a collection of <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> objects.</returns>
		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001E9E RID: 7838 RVA: 0x000A7257 File Offset: 0x000A5457
		public XmlArrayItemAttributes XmlArrayItems
		{
			get
			{
				return this.xmlArrayItems;
			}
		}

		/// <summary>Gets or sets the default value of an XML element or attribute.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the default value of an XML element or attribute.</returns>
		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001E9F RID: 7839 RVA: 0x000A725F File Offset: 0x000A545F
		// (set) Token: 0x06001EA0 RID: 7840 RVA: 0x000A7267 File Offset: 0x000A5467
		public object XmlDefaultValue
		{
			get
			{
				return this.xmlDefaultValue;
			}
			set
			{
				this.xmlDefaultValue = value;
			}
		}

		/// <summary>Gets or sets a value that specifies whether or not the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes a public field or public read/write property.</summary>
		/// <returns>true if the <see cref="T:System.Xml.Serialization.XmlSerializer" /> must not serialize the field or property; otherwise, false.</returns>
		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001EA1 RID: 7841 RVA: 0x000A7270 File Offset: 0x000A5470
		// (set) Token: 0x06001EA2 RID: 7842 RVA: 0x000A7278 File Offset: 0x000A5478
		public bool XmlIgnore
		{
			get
			{
				return this.xmlIgnore;
			}
			set
			{
				this.xmlIgnore = value;
			}
		}

		/// <summary>Gets or sets an object that specifies how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes a class to which the <see cref="T:System.Xml.Serialization.XmlTypeAttribute" /> has been applied.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlTypeAttribute" /> that overrides an <see cref="T:System.Xml.Serialization.XmlTypeAttribute" /> applied to a class declaration.</returns>
		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001EA3 RID: 7843 RVA: 0x000A7281 File Offset: 0x000A5481
		// (set) Token: 0x06001EA4 RID: 7844 RVA: 0x000A7289 File Offset: 0x000A5489
		public XmlTypeAttribute XmlType
		{
			get
			{
				return this.xmlType;
			}
			set
			{
				this.xmlType = value;
			}
		}

		/// <summary>Gets or sets an object that specifies how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes a class as an XML root element.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlRootAttribute" /> that overrides a class attributed as an XML root element.</returns>
		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001EA5 RID: 7845 RVA: 0x000A7292 File Offset: 0x000A5492
		// (set) Token: 0x06001EA6 RID: 7846 RVA: 0x000A729A File Offset: 0x000A549A
		public XmlRootAttribute XmlRoot
		{
			get
			{
				return this.xmlRoot;
			}
			set
			{
				this.xmlRoot = value;
			}
		}

		/// <summary>Gets the collection of <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> objects to override.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlAnyElementAttributes" /> object that represents the collection of <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> objects.</returns>
		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001EA7 RID: 7847 RVA: 0x000A72A3 File Offset: 0x000A54A3
		public XmlAnyElementAttributes XmlAnyElements
		{
			get
			{
				return this.xmlAnyElements;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Serialization.XmlAnyAttributeAttribute" /> to override.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.XmlAnyAttributeAttribute" /> to override.</returns>
		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001EA8 RID: 7848 RVA: 0x000A72AB File Offset: 0x000A54AB
		// (set) Token: 0x06001EA9 RID: 7849 RVA: 0x000A72B3 File Offset: 0x000A54B3
		public XmlAnyAttributeAttribute XmlAnyAttribute
		{
			get
			{
				return this.xmlAnyAttribute;
			}
			set
			{
				this.xmlAnyAttribute = value;
			}
		}

		/// <summary>Gets or sets an object that allows you to distinguish between a set of choices.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlChoiceIdentifierAttribute" /> that can be applied to a class member that is serialized as an xsi:choice element.</returns>
		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001EAA RID: 7850 RVA: 0x000A72BC File Offset: 0x000A54BC
		public XmlChoiceIdentifierAttribute XmlChoiceIdentifier
		{
			get
			{
				return this.xmlChoiceIdentifier;
			}
		}

		/// <summary>Gets or sets a value that specifies whether to keep all namespace declarations when an object containing a member that returns an <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> object is overridden.</summary>
		/// <returns>true if the namespace declarations should be kept; otherwise, false.</returns>
		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001EAB RID: 7851 RVA: 0x000A72C4 File Offset: 0x000A54C4
		// (set) Token: 0x06001EAC RID: 7852 RVA: 0x000A72CC File Offset: 0x000A54CC
		public bool Xmlns
		{
			get
			{
				return this.xmlns;
			}
			set
			{
				this.xmlns = value;
			}
		}

		// Token: 0x04001720 RID: 5920
		private XmlElementAttributes xmlElements = new XmlElementAttributes();

		// Token: 0x04001721 RID: 5921
		private XmlArrayItemAttributes xmlArrayItems = new XmlArrayItemAttributes();

		// Token: 0x04001722 RID: 5922
		private XmlAnyElementAttributes xmlAnyElements = new XmlAnyElementAttributes();

		// Token: 0x04001723 RID: 5923
		private XmlArrayAttribute xmlArray;

		// Token: 0x04001724 RID: 5924
		private XmlAttributeAttribute xmlAttribute;

		// Token: 0x04001725 RID: 5925
		private XmlTextAttribute xmlText;

		// Token: 0x04001726 RID: 5926
		private XmlEnumAttribute xmlEnum;

		// Token: 0x04001727 RID: 5927
		private bool xmlIgnore;

		// Token: 0x04001728 RID: 5928
		private bool xmlns;

		// Token: 0x04001729 RID: 5929
		private object xmlDefaultValue;

		// Token: 0x0400172A RID: 5930
		private XmlRootAttribute xmlRoot;

		// Token: 0x0400172B RID: 5931
		private XmlTypeAttribute xmlType;

		// Token: 0x0400172C RID: 5932
		private XmlAnyAttributeAttribute xmlAnyAttribute;

		// Token: 0x0400172D RID: 5933
		private XmlChoiceIdentifierAttribute xmlChoiceIdentifier;

		// Token: 0x0400172E RID: 5934
		private static volatile Type ignoreAttributeType;
	}
}
