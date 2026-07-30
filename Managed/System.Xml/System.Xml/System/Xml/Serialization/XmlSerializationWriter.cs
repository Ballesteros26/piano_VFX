using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	/// <summary>Represents an abstract class used for controlling serialization by the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class.</summary>
	// Token: 0x02000358 RID: 856
	public abstract class XmlSerializationWriter : XmlSerializationGeneratedCode
	{
		// Token: 0x0600222B RID: 8747 RVA: 0x000CC347 File Offset: 0x000CA547
		internal void Init(XmlWriter w, XmlSerializerNamespaces namespaces, string encodingStyle, string idBase, TempAssembly tempAssembly)
		{
			this.w = w;
			this.namespaces = namespaces;
			this.soap12 = encodingStyle == "http://www.w3.org/2003/05/soap-encoding";
			this.idBase = idBase;
			base.Init(tempAssembly);
		}

		/// <summary>Gets or sets a value that indicates whether the <see cref="M:System.Xml.XmlConvert.EncodeName(System.String)" /> method is used to write valid XML.</summary>
		/// <returns>true if the <see cref="M:System.Xml.Serialization.XmlSerializationWriter.FromXmlQualifiedName(System.Xml.XmlQualifiedName)" /> method returns an encoded name; otherwise, false.</returns>
		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x0600222C RID: 8748 RVA: 0x000CC378 File Offset: 0x000CA578
		// (set) Token: 0x0600222D RID: 8749 RVA: 0x000CC380 File Offset: 0x000CA580
		protected bool EscapeName
		{
			get
			{
				return this.escapeName;
			}
			set
			{
				this.escapeName = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.XmlWriter" /> that is being used by the <see cref="T:System.Xml.Serialization.XmlSerializationWriter" />.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlWriter" /> used by the class instance.</returns>
		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x0600222E RID: 8750 RVA: 0x000CC389 File Offset: 0x000CA589
		// (set) Token: 0x0600222F RID: 8751 RVA: 0x000CC391 File Offset: 0x000CA591
		protected XmlWriter Writer
		{
			get
			{
				return this.w;
			}
			set
			{
				this.w = value;
			}
		}

		/// <summary>Gets or sets a list of XML qualified name objects that contain the namespaces and prefixes used to produce qualified names in XML documents.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> that contains the namespaces and prefix pairs.</returns>
		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06002230 RID: 8752 RVA: 0x000CC39A File Offset: 0x000CA59A
		// (set) Token: 0x06002231 RID: 8753 RVA: 0x000CC3B4 File Offset: 0x000CA5B4
		protected ArrayList Namespaces
		{
			get
			{
				if (this.namespaces != null)
				{
					return this.namespaces.NamespaceList;
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					this.namespaces = null;
					return;
				}
				XmlQualifiedName[] array = (XmlQualifiedName[])value.ToArray(typeof(XmlQualifiedName));
				this.namespaces = new XmlSerializerNamespaces(array);
			}
		}

		/// <summary>Processes a base-64 byte array.</summary>
		/// <returns>The same byte array that was passed in as an argument.</returns>
		/// <param name="value">A base-64 <see cref="T:System.Byte" /> array.</param>
		// Token: 0x06002232 RID: 8754 RVA: 0x00002068 File Offset: 0x00000268
		protected static byte[] FromByteArrayBase64(byte[] value)
		{
			return value;
		}

		/// <summary>Gets a dynamically generated assembly by name.</summary>
		/// <returns>A dynamically generated assembly.</returns>
		/// <param name="assemblyFullName">The full name of the assembly.</param>
		// Token: 0x06002233 RID: 8755 RVA: 0x000B8CF5 File Offset: 0x000B6EF5
		protected static Assembly ResolveDynamicAssembly(string assemblyFullName)
		{
			return DynamicAssemblies.Get(assemblyFullName);
		}

		/// <summary>Produces a string from an input hexadecimal byte array.</summary>
		/// <returns>The byte array value converted to a string.</returns>
		/// <param name="value">A hexadecimal byte array to translate to a string.</param>
		// Token: 0x06002234 RID: 8756 RVA: 0x000CC3EE File Offset: 0x000CA5EE
		protected static string FromByteArrayHex(byte[] value)
		{
			return XmlCustomFormatter.FromByteArrayHex(value);
		}

		/// <summary>Produces a string from an input <see cref="T:System.DateTime" />.</summary>
		/// <returns>A string representation of the <see cref="T:System.DateTime" /> that shows the date and time.</returns>
		/// <param name="value">A <see cref="T:System.DateTime" /> to translate to a string.</param>
		// Token: 0x06002235 RID: 8757 RVA: 0x000CC3F6 File Offset: 0x000CA5F6
		protected static string FromDateTime(DateTime value)
		{
			return XmlCustomFormatter.FromDateTime(value);
		}

		/// <summary>Produces a string from a <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>A string representation of the <see cref="T:System.DateTime" /> that shows the date but no time.</returns>
		/// <param name="value">A <see cref="T:System.DateTime" /> to translate to a string.</param>
		// Token: 0x06002236 RID: 8758 RVA: 0x000CC3FE File Offset: 0x000CA5FE
		protected static string FromDate(DateTime value)
		{
			return XmlCustomFormatter.FromDate(value);
		}

		/// <summary>Produces a string from a <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>A string representation of the <see cref="T:System.DateTime" /> object that shows the time but no date.</returns>
		/// <param name="value">A <see cref="T:System.DateTime" /> that is translated to a string.</param>
		// Token: 0x06002237 RID: 8759 RVA: 0x000CC406 File Offset: 0x000CA606
		protected static string FromTime(DateTime value)
		{
			return XmlCustomFormatter.FromTime(value);
		}

		/// <summary>Produces a string from an input <see cref="T:System.Char" />.</summary>
		/// <returns>The <see cref="T:System.Char" /> value converted to a string.</returns>
		/// <param name="value">A <see cref="T:System.Char" /> to translate to a string.</param>
		// Token: 0x06002238 RID: 8760 RVA: 0x000CC40E File Offset: 0x000CA60E
		protected static string FromChar(char value)
		{
			return XmlCustomFormatter.FromChar(value);
		}

		/// <summary>Produces a string that consists of delimited identifiers that represent the enumeration members that have been set.</summary>
		/// <returns>A string that consists of delimited identifiers, where each represents a member from the set enumerator list.</returns>
		/// <param name="value">The enumeration value as a series of bitwise OR operations.</param>
		/// <param name="values">The enumeration's name values.</param>
		/// <param name="ids">The enumeration's constant values.</param>
		// Token: 0x06002239 RID: 8761 RVA: 0x000CC416 File Offset: 0x000CA616
		protected static string FromEnum(long value, string[] values, long[] ids)
		{
			return XmlCustomFormatter.FromEnum(value, values, ids, null);
		}

		/// <summary>Takes a numeric enumeration value and the names and constants from the enumerator list for the enumeration and returns a string that consists of delimited identifiers that represent the enumeration members that have been set.</summary>
		/// <returns>A string that consists of delimited identifiers, where each item is one of the values set by the bitwise operation.</returns>
		/// <param name="value">The enumeration value as a series of bitwise OR operations.</param>
		/// <param name="values">The values of the enumeration.</param>
		/// <param name="ids">The constants of the enumeration.</param>
		/// <param name="typeName">The name of the type </param>
		// Token: 0x0600223A RID: 8762 RVA: 0x000CC421 File Offset: 0x000CA621
		protected static string FromEnum(long value, string[] values, long[] ids, string typeName)
		{
			return XmlCustomFormatter.FromEnum(value, values, ids, typeName);
		}

		/// <summary>Encodes a valid XML name by replacing characters that are not valid with escape sequences.</summary>
		/// <returns>An encoded string.</returns>
		/// <param name="name">A string to be used as an XML name.</param>
		// Token: 0x0600223B RID: 8763 RVA: 0x000CC42C File Offset: 0x000CA62C
		protected static string FromXmlName(string name)
		{
			return XmlCustomFormatter.FromXmlName(name);
		}

		/// <summary>Encodes a valid XML local name by replacing characters that are not valid with escape sequences.</summary>
		/// <returns>An encoded string.</returns>
		/// <param name="ncName">A string to be used as a local (unqualified) XML name.</param>
		// Token: 0x0600223C RID: 8764 RVA: 0x000CC434 File Offset: 0x000CA634
		protected static string FromXmlNCName(string ncName)
		{
			return XmlCustomFormatter.FromXmlNCName(ncName);
		}

		/// <summary>Encodes an XML name.</summary>
		/// <returns>An encoded string.</returns>
		/// <param name="nmToken">An XML name to be encoded.</param>
		// Token: 0x0600223D RID: 8765 RVA: 0x000CC43C File Offset: 0x000CA63C
		protected static string FromXmlNmToken(string nmToken)
		{
			return XmlCustomFormatter.FromXmlNmToken(nmToken);
		}

		/// <summary>Encodes a space-delimited sequence of XML names into a single XML name.</summary>
		/// <returns>An encoded string.</returns>
		/// <param name="nmTokens">A space-delimited sequence of XML names to be encoded.</param>
		// Token: 0x0600223E RID: 8766 RVA: 0x000CC444 File Offset: 0x000CA644
		protected static string FromXmlNmTokens(string nmTokens)
		{
			return XmlCustomFormatter.FromXmlNmTokens(nmTokens);
		}

		/// <summary>Writes an xsi:type attribute for an XML element that is being serialized into a document.</summary>
		/// <param name="name">The local name of an XML Schema data type.</param>
		/// <param name="ns">The namespace of an XML Schema data type.</param>
		// Token: 0x0600223F RID: 8767 RVA: 0x000CC44C File Offset: 0x000CA64C
		protected void WriteXsiType(string name, string ns)
		{
			this.WriteAttribute("type", "http://www.w3.org/2001/XMLSchema-instance", this.GetQualifiedName(name, ns));
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x000CC466 File Offset: 0x000CA666
		private XmlQualifiedName GetPrimitiveTypeName(Type type)
		{
			return this.GetPrimitiveTypeName(type, true);
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x000CC470 File Offset: 0x000CA670
		private XmlQualifiedName GetPrimitiveTypeName(Type type, bool throwIfUnknown)
		{
			XmlQualifiedName primitiveTypeNameInternal = XmlSerializationWriter.GetPrimitiveTypeNameInternal(type);
			if (throwIfUnknown && primitiveTypeNameInternal == null)
			{
				throw this.CreateUnknownTypeException(type);
			}
			return primitiveTypeNameInternal;
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x000CC49C File Offset: 0x000CA69C
		internal static XmlQualifiedName GetPrimitiveTypeNameInternal(Type type)
		{
			string text = "http://www.w3.org/2001/XMLSchema";
			string text2;
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
				text2 = "boolean";
				goto IL_0196;
			case TypeCode.Char:
				text2 = "char";
				text = "http://microsoft.com/wsdl/types/";
				goto IL_0196;
			case TypeCode.SByte:
				text2 = "byte";
				goto IL_0196;
			case TypeCode.Byte:
				text2 = "unsignedByte";
				goto IL_0196;
			case TypeCode.Int16:
				text2 = "short";
				goto IL_0196;
			case TypeCode.UInt16:
				text2 = "unsignedShort";
				goto IL_0196;
			case TypeCode.Int32:
				text2 = "int";
				goto IL_0196;
			case TypeCode.UInt32:
				text2 = "unsignedInt";
				goto IL_0196;
			case TypeCode.Int64:
				text2 = "long";
				goto IL_0196;
			case TypeCode.UInt64:
				text2 = "unsignedLong";
				goto IL_0196;
			case TypeCode.Single:
				text2 = "float";
				goto IL_0196;
			case TypeCode.Double:
				text2 = "double";
				goto IL_0196;
			case TypeCode.Decimal:
				text2 = "decimal";
				goto IL_0196;
			case TypeCode.DateTime:
				text2 = "dateTime";
				goto IL_0196;
			case TypeCode.String:
				text2 = "string";
				goto IL_0196;
			}
			if (type == typeof(XmlQualifiedName))
			{
				text2 = "QName";
			}
			else if (type == typeof(byte[]))
			{
				text2 = "base64Binary";
			}
			else if (type == typeof(TimeSpan) && LocalAppContextSwitches.EnableTimeSpanSerialization)
			{
				text2 = "TimeSpan";
			}
			else if (type == typeof(Guid))
			{
				text2 = "guid";
				text = "http://microsoft.com/wsdl/types/";
			}
			else
			{
				if (!(type == typeof(XmlNode[])))
				{
					return null;
				}
				text2 = "anyType";
			}
			IL_0196:
			return new XmlQualifiedName(text2, text);
		}

		/// <summary>Writes an XML element whose text body is a value of a simple XML Schema data type.</summary>
		/// <param name="name">The local name of the element to write.</param>
		/// <param name="ns">The namespace of the element to write.</param>
		/// <param name="o">The object to be serialized in the element body.</param>
		/// <param name="xsiType">true if the XML element explicitly specifies the text value's type using the xsi:type attribute; otherwise, false.</param>
		// Token: 0x06002243 RID: 8771 RVA: 0x000CC648 File Offset: 0x000CA848
		protected void WriteTypedPrimitive(string name, string ns, object o, bool xsiType)
		{
			string text = "http://www.w3.org/2001/XMLSchema";
			bool flag = true;
			bool flag2 = false;
			Type type = o.GetType();
			bool flag3 = false;
			string text2;
			string text3;
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
				text2 = XmlConvert.ToString((bool)o);
				text3 = "boolean";
				goto IL_0322;
			case TypeCode.Char:
				text2 = XmlSerializationWriter.FromChar((char)o);
				text3 = "char";
				text = "http://microsoft.com/wsdl/types/";
				goto IL_0322;
			case TypeCode.SByte:
				text2 = XmlConvert.ToString((sbyte)o);
				text3 = "byte";
				goto IL_0322;
			case TypeCode.Byte:
				text2 = XmlConvert.ToString((byte)o);
				text3 = "unsignedByte";
				goto IL_0322;
			case TypeCode.Int16:
				text2 = XmlConvert.ToString((short)o);
				text3 = "short";
				goto IL_0322;
			case TypeCode.UInt16:
				text2 = XmlConvert.ToString((ushort)o);
				text3 = "unsignedShort";
				goto IL_0322;
			case TypeCode.Int32:
				text2 = XmlConvert.ToString((int)o);
				text3 = "int";
				goto IL_0322;
			case TypeCode.UInt32:
				text2 = XmlConvert.ToString((uint)o);
				text3 = "unsignedInt";
				goto IL_0322;
			case TypeCode.Int64:
				text2 = XmlConvert.ToString((long)o);
				text3 = "long";
				goto IL_0322;
			case TypeCode.UInt64:
				text2 = XmlConvert.ToString((ulong)o);
				text3 = "unsignedLong";
				goto IL_0322;
			case TypeCode.Single:
				text2 = XmlConvert.ToString((float)o);
				text3 = "float";
				goto IL_0322;
			case TypeCode.Double:
				text2 = XmlConvert.ToString((double)o);
				text3 = "double";
				goto IL_0322;
			case TypeCode.Decimal:
				text2 = XmlConvert.ToString((decimal)o);
				text3 = "decimal";
				goto IL_0322;
			case TypeCode.DateTime:
				text2 = XmlSerializationWriter.FromDateTime((DateTime)o);
				text3 = "dateTime";
				goto IL_0322;
			case TypeCode.String:
				text2 = (string)o;
				text3 = "string";
				flag = false;
				goto IL_0322;
			}
			if (type == typeof(XmlQualifiedName))
			{
				text3 = "QName";
				flag3 = true;
				if (name == null)
				{
					this.w.WriteStartElement(text3, text);
				}
				else
				{
					this.w.WriteStartElement(name, ns);
				}
				text2 = this.FromXmlQualifiedName((XmlQualifiedName)o, false);
			}
			else if (type == typeof(byte[]))
			{
				text2 = string.Empty;
				flag2 = true;
				text3 = "base64Binary";
			}
			else if (type == typeof(Guid))
			{
				text2 = XmlConvert.ToString((Guid)o);
				text3 = "guid";
				text = "http://microsoft.com/wsdl/types/";
			}
			else if (type == typeof(TimeSpan) && LocalAppContextSwitches.EnableTimeSpanSerialization)
			{
				text2 = XmlConvert.ToString((TimeSpan)o);
				text3 = "TimeSpan";
			}
			else
			{
				if (typeof(XmlNode[]).IsAssignableFrom(type))
				{
					if (name == null)
					{
						this.w.WriteStartElement("anyType", "http://www.w3.org/2001/XMLSchema");
					}
					else
					{
						this.w.WriteStartElement(name, ns);
					}
					XmlNode[] array = (XmlNode[])o;
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i] != null)
						{
							array[i].WriteTo(this.w);
						}
					}
					this.w.WriteEndElement();
					return;
				}
				throw this.CreateUnknownTypeException(type);
			}
			IL_0322:
			if (!flag3)
			{
				if (name == null)
				{
					this.w.WriteStartElement(text3, text);
				}
				else
				{
					this.w.WriteStartElement(name, ns);
				}
			}
			if (xsiType)
			{
				this.WriteXsiType(text3, text);
			}
			if (text2 == null)
			{
				this.w.WriteAttributeString("nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
			}
			else if (flag2)
			{
				XmlCustomFormatter.WriteArrayBase64(this.w, (byte[])o, 0, ((byte[])o).Length);
			}
			else if (flag)
			{
				this.w.WriteRaw(text2);
			}
			else
			{
				this.w.WriteString(text2);
			}
			this.w.WriteEndElement();
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x000CCA10 File Offset: 0x000CAC10
		private string GetQualifiedName(string name, string ns)
		{
			if (ns == null || ns.Length == 0)
			{
				return name;
			}
			string text = this.w.LookupPrefix(ns);
			if (text == null)
			{
				if (ns == "http://www.w3.org/XML/1998/namespace")
				{
					text = "xml";
				}
				else
				{
					text = this.NextPrefix();
					this.WriteAttribute("xmlns", text, null, ns);
				}
			}
			else if (text.Length == 0)
			{
				return name;
			}
			return text + ":" + name;
		}

		/// <summary>Returns an XML qualified name, with invalid characters replaced by escape sequences.</summary>
		/// <returns>An XML qualified name, with invalid characters replaced by escape sequences.</returns>
		/// <param name="xmlQualifiedName">An <see cref="T:System.Xml.XmlQualifiedName" /> that represents the XML to be written.</param>
		// Token: 0x06002245 RID: 8773 RVA: 0x000CCA7C File Offset: 0x000CAC7C
		protected string FromXmlQualifiedName(XmlQualifiedName xmlQualifiedName)
		{
			return this.FromXmlQualifiedName(xmlQualifiedName, true);
		}

		/// <summary>Produces a string that can be written as an XML qualified name, with invalid characters replaced by escape sequences.</summary>
		/// <returns>An XML qualified name, with invalid characters replaced by escape sequences.</returns>
		/// <param name="xmlQualifiedName">An <see cref="T:System.Xml.XmlQualifiedName" /> that represents the XML to be written.</param>
		/// <param name="ignoreEmpty">true to ignore empty spaces in the string; otherwise, false.</param>
		// Token: 0x06002246 RID: 8774 RVA: 0x000CCA86 File Offset: 0x000CAC86
		protected string FromXmlQualifiedName(XmlQualifiedName xmlQualifiedName, bool ignoreEmpty)
		{
			if (xmlQualifiedName == null)
			{
				return null;
			}
			if (xmlQualifiedName.IsEmpty && ignoreEmpty)
			{
				return null;
			}
			return this.GetQualifiedName(this.EscapeName ? XmlConvert.EncodeLocalName(xmlQualifiedName.Name) : xmlQualifiedName.Name, xmlQualifiedName.Namespace);
		}

		/// <summary>Writes an opening element tag, including any attributes.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		// Token: 0x06002247 RID: 8775 RVA: 0x000CCAC6 File Offset: 0x000CACC6
		protected void WriteStartElement(string name)
		{
			this.WriteStartElement(name, null, null, false, null);
		}

		/// <summary>Writes an opening element tag, including any attributes.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		// Token: 0x06002248 RID: 8776 RVA: 0x000CCAD3 File Offset: 0x000CACD3
		protected void WriteStartElement(string name, string ns)
		{
			this.WriteStartElement(name, ns, null, false, null);
		}

		/// <summary>Writes an opening element tag, including any attributes.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="writePrefixed">true to write the element name with a prefix if none is available for the specified namespace; otherwise, false.</param>
		// Token: 0x06002249 RID: 8777 RVA: 0x000CCAE0 File Offset: 0x000CACE0
		protected void WriteStartElement(string name, string ns, bool writePrefixed)
		{
			this.WriteStartElement(name, ns, null, writePrefixed, null);
		}

		/// <summary>Writes an opening element tag, including any attributes.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="o">The object being serialized as an XML element.</param>
		// Token: 0x0600224A RID: 8778 RVA: 0x000CCAED File Offset: 0x000CACED
		protected void WriteStartElement(string name, string ns, object o)
		{
			this.WriteStartElement(name, ns, o, false, null);
		}

		/// <summary>Writes an opening element tag, including any attributes.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="o">The object being serialized as an XML element.</param>
		/// <param name="writePrefixed">true to write the element name with a prefix if none is available for the specified namespace; otherwise, false.</param>
		// Token: 0x0600224B RID: 8779 RVA: 0x000CCAFA File Offset: 0x000CACFA
		protected void WriteStartElement(string name, string ns, object o, bool writePrefixed)
		{
			this.WriteStartElement(name, ns, o, writePrefixed, null);
		}

		/// <summary>Writes an opening element tag, including any attributes.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="o">The object being serialized as an XML element.</param>
		/// <param name="writePrefixed">true to write the element name with a prefix if none is available for the specified namespace; otherwise, false.</param>
		/// <param name="xmlns">An instance of the <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> class that contains prefix and namespace pairs to be used in the generated XML.</param>
		// Token: 0x0600224C RID: 8780 RVA: 0x000CCB08 File Offset: 0x000CAD08
		protected void WriteStartElement(string name, string ns, object o, bool writePrefixed, XmlSerializerNamespaces xmlns)
		{
			if (o != null && this.objectsInUse != null)
			{
				if (this.objectsInUse.ContainsKey(o))
				{
					throw new InvalidOperationException(Res.GetString("A circular reference was detected while serializing an object of type {0}.", new object[] { o.GetType().FullName }));
				}
				this.objectsInUse.Add(o, o);
			}
			string text = null;
			bool flag = false;
			if (this.namespaces != null)
			{
				foreach (object obj in this.namespaces.Namespaces.Keys)
				{
					string text2 = (string)obj;
					string text3 = (string)this.namespaces.Namespaces[text2];
					if (text2.Length > 0 && text3 == ns)
					{
						text = text2;
					}
					if (text2.Length == 0)
					{
						if (text3 == null || text3.Length == 0)
						{
							flag = true;
						}
						if (ns != text3)
						{
							writePrefixed = true;
						}
					}
				}
				this.usedPrefixes = this.ListUsedPrefixes(this.namespaces.Namespaces, this.aliasBase);
			}
			if (writePrefixed && text == null && ns != null && ns.Length > 0)
			{
				text = this.w.LookupPrefix(ns);
				if (text == null || text.Length == 0)
				{
					text = this.NextPrefix();
				}
			}
			if (text == null && xmlns != null)
			{
				text = xmlns.LookupPrefix(ns);
			}
			if (flag && text == null && ns != null && ns.Length != 0)
			{
				text = this.NextPrefix();
			}
			this.w.WriteStartElement(text, name, ns);
			if (this.namespaces != null)
			{
				foreach (object obj2 in this.namespaces.Namespaces.Keys)
				{
					string text4 = (string)obj2;
					string text5 = (string)this.namespaces.Namespaces[text4];
					if (text4.Length != 0 || (text5 != null && text5.Length != 0))
					{
						if (text5 == null || text5.Length == 0)
						{
							if (text4.Length > 0)
							{
								throw new InvalidOperationException(Res.GetString("Invalid namespace attribute: xmlns:{0}=\"\".", new object[] { text4 }));
							}
							this.WriteAttribute("xmlns", text4, null, text5);
						}
						else if (this.w.LookupPrefix(text5) == null)
						{
							if (text == null && text4.Length == 0)
							{
								break;
							}
							this.WriteAttribute("xmlns", text4, null, text5);
						}
					}
				}
			}
			this.WriteNamespaceDeclarations(xmlns);
		}

		// Token: 0x0600224D RID: 8781 RVA: 0x000CCDA0 File Offset: 0x000CAFA0
		private Hashtable ListUsedPrefixes(Hashtable nsList, string prefix)
		{
			Hashtable hashtable = new Hashtable();
			int length = prefix.Length;
			foreach (object obj in this.namespaces.Namespaces.Keys)
			{
				string text = (string)obj;
				if (text.Length > length)
				{
					string text2 = text;
					int length2 = text2.Length;
					if (text2.Length > length && text2.Length <= length + "2147483647".Length && text2.StartsWith(prefix, StringComparison.Ordinal))
					{
						bool flag = true;
						for (int i = length; i < text2.Length; i++)
						{
							if (!char.IsDigit(text2, i))
							{
								flag = false;
								break;
							}
						}
						if (flag)
						{
							long num = long.Parse(text2.Substring(length), CultureInfo.InvariantCulture);
							if (num <= 2147483647L)
							{
								int num2 = (int)num;
								if (!hashtable.ContainsKey(num2))
								{
									hashtable.Add(num2, num2);
								}
							}
						}
					}
				}
			}
			if (hashtable.Count > 0)
			{
				return hashtable;
			}
			return null;
		}

		/// <summary>Writes an XML element with an xsi:nil='true' attribute.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		// Token: 0x0600224E RID: 8782 RVA: 0x000CCED8 File Offset: 0x000CB0D8
		protected void WriteNullTagEncoded(string name)
		{
			this.WriteNullTagEncoded(name, null);
		}

		/// <summary>Writes an XML element with an xsi:nil='true' attribute.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		// Token: 0x0600224F RID: 8783 RVA: 0x000CCEE2 File Offset: 0x000CB0E2
		protected void WriteNullTagEncoded(string name, string ns)
		{
			if (name == null || name.Length == 0)
			{
				return;
			}
			this.WriteStartElement(name, ns, null, true);
			this.w.WriteAttributeString("nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
			this.w.WriteEndElement();
		}

		/// <summary>Writes an XML element with an xsi:nil='true' attribute.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		// Token: 0x06002250 RID: 8784 RVA: 0x000CCF1F File Offset: 0x000CB11F
		protected void WriteNullTagLiteral(string name)
		{
			this.WriteNullTagLiteral(name, null);
		}

		/// <summary>Writes an XML element with an xsi:nil='true' attribute.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		// Token: 0x06002251 RID: 8785 RVA: 0x000CCF29 File Offset: 0x000CB129
		protected void WriteNullTagLiteral(string name, string ns)
		{
			if (name == null || name.Length == 0)
			{
				return;
			}
			this.WriteStartElement(name, ns, null, false);
			this.w.WriteAttributeString("nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
			this.w.WriteEndElement();
		}

		/// <summary>Writes an XML element whose body is empty.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		// Token: 0x06002252 RID: 8786 RVA: 0x000CCF66 File Offset: 0x000CB166
		protected void WriteEmptyTag(string name)
		{
			this.WriteEmptyTag(name, null);
		}

		/// <summary>Writes an XML element whose body is empty.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		// Token: 0x06002253 RID: 8787 RVA: 0x000CCF70 File Offset: 0x000CB170
		protected void WriteEmptyTag(string name, string ns)
		{
			if (name == null || name.Length == 0)
			{
				return;
			}
			this.WriteStartElement(name, ns, null, false);
			this.w.WriteEndElement();
		}

		/// <summary>Writes a &lt;closing&gt; element tag.</summary>
		// Token: 0x06002254 RID: 8788 RVA: 0x000CCF93 File Offset: 0x000CB193
		protected void WriteEndElement()
		{
			this.w.WriteEndElement();
		}

		/// <summary>Writes a &lt;closing&gt; element tag.</summary>
		/// <param name="o">The object being serialized.</param>
		// Token: 0x06002255 RID: 8789 RVA: 0x000CCFA0 File Offset: 0x000CB1A0
		protected void WriteEndElement(object o)
		{
			this.w.WriteEndElement();
			if (o != null && this.objectsInUse != null)
			{
				this.objectsInUse.Remove(o);
			}
		}

		/// <summary>Writes an object that uses custom XML formatting as an XML element.</summary>
		/// <param name="serializable">An object that implements the <see cref="T:System.Xml.Serialization.IXmlSerializable" /> interface that uses custom XML formatting.</param>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="isNullable">true to write an xsi:nil='true' attribute if the <see cref="T:System.Xml.Serialization.IXmlSerializable" /> class object is null; otherwise, false.</param>
		// Token: 0x06002256 RID: 8790 RVA: 0x000CCFC4 File Offset: 0x000CB1C4
		protected void WriteSerializable(IXmlSerializable serializable, string name, string ns, bool isNullable)
		{
			this.WriteSerializable(serializable, name, ns, isNullable, true);
		}

		/// <summary>Instructs <see cref="T:System.Xml.XmlNode" /> to write an object that uses custom XML formatting as an XML element.</summary>
		/// <param name="serializable">An object that implements the <see cref="T:System.Xml.Serialization.IXmlSerializable" /> interface that uses custom XML formatting.</param>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="isNullable">true to write an xsi:nil='true' attribute if the <see cref="T:System.Xml.Serialization.IXmlSerializable" /> object is null; otherwise, false.</param>
		/// <param name="wrapped">true to ignore writing the opening element tag; otherwise, false to write the opening element tag.</param>
		// Token: 0x06002257 RID: 8791 RVA: 0x000CCFD2 File Offset: 0x000CB1D2
		protected void WriteSerializable(IXmlSerializable serializable, string name, string ns, bool isNullable, bool wrapped)
		{
			if (serializable == null)
			{
				if (isNullable)
				{
					this.WriteNullTagLiteral(name, ns);
				}
				return;
			}
			if (wrapped)
			{
				this.w.WriteStartElement(name, ns);
			}
			serializable.WriteXml(this.w);
			if (wrapped)
			{
				this.w.WriteEndElement();
			}
		}

		/// <summary>Writes an XML element that contains a string as the body. <see cref="T:System.Xml.XmlWriter" /> inserts an xsi:nil='true' attribute if the string's value is null.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="value">The string to write in the body of the XML element.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the xsi:type attribute.</param>
		// Token: 0x06002258 RID: 8792 RVA: 0x000CD010 File Offset: 0x000CB210
		protected void WriteNullableStringEncoded(string name, string ns, string value, XmlQualifiedName xsiType)
		{
			if (value == null)
			{
				this.WriteNullTagEncoded(name, ns);
				return;
			}
			this.WriteElementString(name, ns, value, xsiType);
		}

		/// <summary>Writes an XML element that contains a string as the body. <see cref="T:System.Xml.XmlWriter" /> inserts an xsi:nil='true' attribute if the string's value is null.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="value">The string to write in the body of the XML element.</param>
		// Token: 0x06002259 RID: 8793 RVA: 0x000CD029 File Offset: 0x000CB229
		protected void WriteNullableStringLiteral(string name, string ns, string value)
		{
			if (value == null)
			{
				this.WriteNullTagLiteral(name, ns);
				return;
			}
			this.WriteElementString(name, ns, value, null);
		}

		/// <summary>Writes an XML element that contains a string as the body. <see cref="T:System.Xml.XmlWriter" /> inserts an xsi:nil='true' attribute if the string's value is null.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="value">The string to write in the body of the XML element.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the xsi:type attribute.</param>
		// Token: 0x0600225A RID: 8794 RVA: 0x000CD041 File Offset: 0x000CB241
		protected void WriteNullableStringEncodedRaw(string name, string ns, string value, XmlQualifiedName xsiType)
		{
			if (value == null)
			{
				this.WriteNullTagEncoded(name, ns);
				return;
			}
			this.WriteElementStringRaw(name, ns, value, xsiType);
		}

		/// <summary>Writes a byte array as the body of an XML element. <see cref="T:System.Xml.XmlWriter" /> inserts an xsi:nil='true' attribute if the string's value is null.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="value">The byte array to write in the body of the XML element.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the xsi:type attribute.</param>
		// Token: 0x0600225B RID: 8795 RVA: 0x000CD05A File Offset: 0x000CB25A
		protected void WriteNullableStringEncodedRaw(string name, string ns, byte[] value, XmlQualifiedName xsiType)
		{
			if (value == null)
			{
				this.WriteNullTagEncoded(name, ns);
				return;
			}
			this.WriteElementStringRaw(name, ns, value, xsiType);
		}

		/// <summary>Writes an XML element that contains a string as the body. <see cref="T:System.Xml.XmlWriter" /> inserts a xsi:nil='true' attribute if the string's value is null.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="value">The string to write in the body of the XML element.</param>
		// Token: 0x0600225C RID: 8796 RVA: 0x000CD073 File Offset: 0x000CB273
		protected void WriteNullableStringLiteralRaw(string name, string ns, string value)
		{
			if (value == null)
			{
				this.WriteNullTagLiteral(name, ns);
				return;
			}
			this.WriteElementStringRaw(name, ns, value, null);
		}

		/// <summary>Writes a byte array as the body of an XML element. <see cref="T:System.Xml.XmlWriter" /> inserts an xsi:nil='true' attribute if the string's value is null.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="value">The byte array to write in the body of the XML element.</param>
		// Token: 0x0600225D RID: 8797 RVA: 0x000CD08B File Offset: 0x000CB28B
		protected void WriteNullableStringLiteralRaw(string name, string ns, byte[] value)
		{
			if (value == null)
			{
				this.WriteNullTagLiteral(name, ns);
				return;
			}
			this.WriteElementStringRaw(name, ns, value, null);
		}

		/// <summary>Writes an XML element whose body contains a valid XML qualified name. <see cref="T:System.Xml.XmlWriter" /> inserts an xsi:nil='true' attribute if the string's value is null.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="value">The XML qualified name to write in the body of the XML element.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the xsi:type attribute.</param>
		// Token: 0x0600225E RID: 8798 RVA: 0x000CD0A3 File Offset: 0x000CB2A3
		protected void WriteNullableQualifiedNameEncoded(string name, string ns, XmlQualifiedName value, XmlQualifiedName xsiType)
		{
			if (value == null)
			{
				this.WriteNullTagEncoded(name, ns);
				return;
			}
			this.WriteElementQualifiedName(name, ns, value, xsiType);
		}

		/// <summary>Writes an XML element whose body contains a valid XML qualified name. <see cref="T:System.Xml.XmlWriter" /> inserts an xsi:nil='true' attribute if the string's value is null.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="value">The XML qualified name to write in the body of the XML element.</param>
		// Token: 0x0600225F RID: 8799 RVA: 0x000CD0C2 File Offset: 0x000CB2C2
		protected void WriteNullableQualifiedNameLiteral(string name, string ns, XmlQualifiedName value)
		{
			if (value == null)
			{
				this.WriteNullTagLiteral(name, ns);
				return;
			}
			this.WriteElementQualifiedName(name, ns, value, null);
		}

		/// <summary>Writes an XML node object within the body of a named XML element.</summary>
		/// <param name="node">The XML node to write, possibly a child XML element.</param>
		/// <param name="name">The local name of the parent XML element to write.</param>
		/// <param name="ns">The namespace of the parent XML element to write.</param>
		/// <param name="isNullable">true to write an xsi:nil='true' attribute if the object to serialize is null; otherwise, false.</param>
		/// <param name="any">true to indicate that the node, if an XML element, adheres to an XML Schema any element declaration; otherwise, false.</param>
		// Token: 0x06002260 RID: 8800 RVA: 0x000CD0E0 File Offset: 0x000CB2E0
		protected void WriteElementEncoded(XmlNode node, string name, string ns, bool isNullable, bool any)
		{
			if (node == null)
			{
				if (isNullable)
				{
					this.WriteNullTagEncoded(name, ns);
				}
				return;
			}
			this.WriteElement(node, name, ns, isNullable, any);
		}

		/// <summary>Instructs an <see cref="T:System.Xml.XmlWriter" /> object to write an <see cref="T:System.Xml.XmlNode" /> object within the body of a named XML element.</summary>
		/// <param name="node">The XML node to write, possibly a child XML element.</param>
		/// <param name="name">The local name of the parent XML element to write.</param>
		/// <param name="ns">The namespace of the parent XML element to write.</param>
		/// <param name="isNullable">true to write an xsi:nil='true' attribute if the object to serialize is null; otherwise, false.</param>
		/// <param name="any">true to indicate that the node, if an XML element, adheres to an XML Schema any element declaration; otherwise, false.</param>
		// Token: 0x06002261 RID: 8801 RVA: 0x000CD0FF File Offset: 0x000CB2FF
		protected void WriteElementLiteral(XmlNode node, string name, string ns, bool isNullable, bool any)
		{
			if (node == null)
			{
				if (isNullable)
				{
					this.WriteNullTagLiteral(name, ns);
				}
				return;
			}
			this.WriteElement(node, name, ns, isNullable, any);
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x000CD120 File Offset: 0x000CB320
		private void WriteElement(XmlNode node, string name, string ns, bool isNullable, bool any)
		{
			if (typeof(XmlAttribute).IsAssignableFrom(node.GetType()))
			{
				throw new InvalidOperationException(Res.GetString("Cannot write a node of type XmlAttribute as an element value. Use XmlAnyAttributeAttribute with an array of XmlNode or XmlAttribute to write the node as an attribute."));
			}
			if (node is XmlDocument)
			{
				node = ((XmlDocument)node).DocumentElement;
				if (node == null)
				{
					if (isNullable)
					{
						this.WriteNullTagEncoded(name, ns);
					}
					return;
				}
			}
			if (any)
			{
				if (node is XmlElement && name != null && name.Length > 0 && (node.LocalName != name || node.NamespaceURI != ns))
				{
					throw new InvalidOperationException(Res.GetString("This element was named '{0}' from namespace '{1}' but should have been named '{2}' from namespace '{3}'.", new object[] { node.LocalName, node.NamespaceURI, name, ns }));
				}
			}
			else
			{
				this.w.WriteStartElement(name, ns);
			}
			node.WriteTo(this.w);
			if (!any)
			{
				this.w.WriteEndElement();
			}
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that a type being serialized is not being used in a valid manner or is unexpectedly encountered.</summary>
		/// <returns>The newly created exception.</returns>
		/// <param name="o">The object whose type cannot be serialized.</param>
		// Token: 0x06002263 RID: 8803 RVA: 0x000CD205 File Offset: 0x000CB405
		protected Exception CreateUnknownTypeException(object o)
		{
			return this.CreateUnknownTypeException(o.GetType());
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that a type being serialized is not being used in a valid manner or is unexpectedly encountered.</summary>
		/// <returns>The newly created exception.</returns>
		/// <param name="type">The type that cannot be serialized.</param>
		// Token: 0x06002264 RID: 8804 RVA: 0x000CD214 File Offset: 0x000CB414
		protected Exception CreateUnknownTypeException(Type type)
		{
			if (typeof(IXmlSerializable).IsAssignableFrom(type))
			{
				return new InvalidOperationException(Res.GetString("The type {0} may not be used in this context. To use {0} as a parameter, return type, or member of a class or struct, the parameter, return type, or member must be declared as type {0} (it cannot be object). Objects of type {0} may not be used in un-typed collections, such as ArrayLists.", new object[] { type.FullName }));
			}
			if (!new TypeScope().GetTypeDesc(type).IsStructLike)
			{
				return new InvalidOperationException(Res.GetString("The type {0} may not be used in this context.", new object[] { type.FullName }));
			}
			return new InvalidOperationException(Res.GetString("The type {0} was not expected. Use the XmlInclude or SoapInclude attribute to specify types that are not known statically.", new object[] { type.FullName }));
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that a value for an XML element does not match an enumeration type.</summary>
		/// <returns>The newly created exception.</returns>
		/// <param name="value">The value that is not valid.</param>
		/// <param name="elementName">The name of the XML element with an invalid value.</param>
		/// <param name="enumValue">The valid value.</param>
		// Token: 0x06002265 RID: 8805 RVA: 0x000CD2A1 File Offset: 0x000CB4A1
		protected Exception CreateMismatchChoiceException(string value, string elementName, string enumValue)
		{
			return new InvalidOperationException(Res.GetString("Value of {0} mismatches the type of {1}; you need to set it to {2}.", new object[] { elementName, value, enumValue }));
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that an XML element that should adhere to the XML Schema any element declaration cannot be processed.</summary>
		/// <returns>The newly created exception.</returns>
		/// <param name="name">The XML element that cannot be processed.</param>
		/// <param name="ns">The namespace of the XML element.</param>
		// Token: 0x06002266 RID: 8806 RVA: 0x000CD2C4 File Offset: 0x000CB4C4
		protected Exception CreateUnknownAnyElementException(string name, string ns)
		{
			return new InvalidOperationException(Res.GetString("The XML element '{0}' from namespace '{1}' was not expected. The XML element name and namespace must match those provided via XmlAnyElementAttribute(s).", new object[] { name, ns }));
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates a failure while writing an array where an XML Schema choice element declaration is applied.</summary>
		/// <returns>The newly created exception.</returns>
		/// <param name="type">The type being serialized.</param>
		/// <param name="identifier">A name for the choice element declaration.</param>
		// Token: 0x06002267 RID: 8807 RVA: 0x000CD2E3 File Offset: 0x000CB4E3
		protected Exception CreateInvalidChoiceIdentifierValueException(string type, string identifier)
		{
			return new InvalidOperationException(Res.GetString("Invalid or missing value of the choice identifier '{1}' of type '{0}[]'.", new object[] { type, identifier }));
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates an unexpected name for an element that adheres to an XML Schema choice element declaration.</summary>
		/// <returns>The newly created exception.</returns>
		/// <param name="value">The name that is not valid.</param>
		/// <param name="identifier">The choice element declaration that the name belongs to.</param>
		/// <param name="name">The expected local name of an element.</param>
		/// <param name="ns">The expected namespace of an element.</param>
		// Token: 0x06002268 RID: 8808 RVA: 0x000CD302 File Offset: 0x000CB502
		protected Exception CreateChoiceIdentifierValueException(string value, string identifier, string name, string ns)
		{
			return new InvalidOperationException(Res.GetString("Value '{0}' of the choice identifier '{1}' does not match element '{2}' from namespace '{3}'.", new object[] { value, identifier, name, ns }));
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> for an invalid enumeration value.</summary>
		/// <returns>The newly created exception.</returns>
		/// <param name="value">An object that represents the invalid enumeration.</param>
		/// <param name="typeName">The XML type name.</param>
		// Token: 0x06002269 RID: 8809 RVA: 0x000CD32A File Offset: 0x000CB52A
		protected Exception CreateInvalidEnumValueException(object value, string typeName)
		{
			return new InvalidOperationException(Res.GetString("Instance validation error: '{0}' is not a valid value for {1}.", new object[] { value, typeName }));
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates the <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> which has been invalidly applied to a member; only members that are of type <see cref="T:System.Xml.XmlNode" />, or derived from <see cref="T:System.Xml.XmlNode" />, are valid.</summary>
		/// <returns>The newly created exception.</returns>
		/// <param name="o">The object that represents the invalid member.</param>
		// Token: 0x0600226A RID: 8810 RVA: 0x000CD349 File Offset: 0x000CB549
		protected Exception CreateInvalidAnyTypeException(object o)
		{
			return this.CreateInvalidAnyTypeException(o.GetType());
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates the <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> which has been invalidly applied to a member; only members that are of type <see cref="T:System.Xml.XmlNode" />, or derived from <see cref="T:System.Xml.XmlNode" />, are valid.</summary>
		/// <returns>The newly created exception.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> that is invalid.</param>
		// Token: 0x0600226B RID: 8811 RVA: 0x000CD357 File Offset: 0x000CB557
		protected Exception CreateInvalidAnyTypeException(Type type)
		{
			return new InvalidOperationException(Res.GetString("Cannot serialize member of type {0}: XmlAnyElement can only be used with classes of type XmlNode or a type deriving from XmlNode.", new object[] { type.FullName }));
		}

		/// <summary>Writes a SOAP message XML element that contains a reference to a multiRef element for a given object.</summary>
		/// <param name="n">The local name of the referencing element being written.</param>
		/// <param name="ns">The namespace of the referencing element being written.</param>
		/// <param name="o">The object being serialized.</param>
		// Token: 0x0600226C RID: 8812 RVA: 0x000CD377 File Offset: 0x000CB577
		protected void WriteReferencingElement(string n, string ns, object o)
		{
			this.WriteReferencingElement(n, ns, o, false);
		}

		/// <summary>Writes a SOAP message XML element that contains a reference to a multiRef element for a given object.</summary>
		/// <param name="n">The local name of the referencing element being written.</param>
		/// <param name="ns">The namespace of the referencing element being written.</param>
		/// <param name="o">The object being serialized.</param>
		/// <param name="isNullable">true to write an xsi:nil='true' attribute if the object to serialize is null; otherwise, false.</param>
		// Token: 0x0600226D RID: 8813 RVA: 0x000CD384 File Offset: 0x000CB584
		protected void WriteReferencingElement(string n, string ns, object o, bool isNullable)
		{
			if (o == null)
			{
				if (isNullable)
				{
					this.WriteNullTagEncoded(n, ns);
				}
				return;
			}
			this.WriteStartElement(n, ns, null, true);
			if (this.soap12)
			{
				this.w.WriteAttributeString("ref", "http://www.w3.org/2003/05/soap-encoding", this.GetId(o, true));
			}
			else
			{
				this.w.WriteAttributeString("href", "#" + this.GetId(o, true));
			}
			this.w.WriteEndElement();
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x000CD3FF File Offset: 0x000CB5FF
		private bool IsIdDefined(object o)
		{
			return this.references != null && this.references.Contains(o);
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x000CD418 File Offset: 0x000CB618
		private string GetId(object o, bool addToReferencesList)
		{
			if (this.references == null)
			{
				this.references = new Hashtable();
				this.referencesToWrite = new ArrayList();
			}
			string text = (string)this.references[o];
			if (text == null)
			{
				string text2 = this.idBase;
				string text3 = "id";
				int num = this.nextId + 1;
				this.nextId = num;
				text = text2 + text3 + num.ToString(CultureInfo.InvariantCulture);
				this.references.Add(o, text);
				if (addToReferencesList)
				{
					this.referencesToWrite.Add(o);
				}
			}
			return text;
		}

		/// <summary>Writes an id attribute that appears in a SOAP-encoded multiRef element.</summary>
		/// <param name="o">The object being serialized.</param>
		// Token: 0x06002270 RID: 8816 RVA: 0x000CD4A3 File Offset: 0x000CB6A3
		protected void WriteId(object o)
		{
			this.WriteId(o, true);
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x000CD4AD File Offset: 0x000CB6AD
		private void WriteId(object o, bool addToReferencesList)
		{
			if (this.soap12)
			{
				this.w.WriteAttributeString("id", "http://www.w3.org/2003/05/soap-encoding", this.GetId(o, addToReferencesList));
				return;
			}
			this.w.WriteAttributeString("id", this.GetId(o, addToReferencesList));
		}

		/// <summary>Writes the specified <see cref="T:System.Xml.XmlNode" /> as an XML attribute.</summary>
		/// <param name="node">The XML node to write.</param>
		// Token: 0x06002272 RID: 8818 RVA: 0x000CD4ED File Offset: 0x000CB6ED
		protected void WriteXmlAttribute(XmlNode node)
		{
			this.WriteXmlAttribute(node, null);
		}

		/// <summary>Writes the specified <see cref="T:System.Xml.XmlNode" /> object as an XML attribute.</summary>
		/// <param name="node">The XML node to write.</param>
		/// <param name="container">An <see cref="T:System.Xml.Schema.XmlSchemaObject" /> object (or null) used to generate a qualified name value for an arrayType attribute from the Web Services Description Language (WSDL) namespace ("http://schemas.xmlsoap.org/wsdl/").</param>
		// Token: 0x06002273 RID: 8819 RVA: 0x000CD4F8 File Offset: 0x000CB6F8
		protected void WriteXmlAttribute(XmlNode node, object container)
		{
			XmlAttribute xmlAttribute = node as XmlAttribute;
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(Res.GetString("The node must be either type XmlAttribute or a derived type."));
			}
			if (xmlAttribute.Value != null)
			{
				if (xmlAttribute.NamespaceURI == "http://schemas.xmlsoap.org/wsdl/" && xmlAttribute.LocalName == "arrayType")
				{
					string text;
					XmlQualifiedName xmlQualifiedName = TypeScope.ParseWsdlArrayType(xmlAttribute.Value, out text, (container is XmlSchemaObject) ? ((XmlSchemaObject)container) : null);
					string text2 = this.FromXmlQualifiedName(xmlQualifiedName, true) + text;
					this.WriteAttribute("arrayType", "http://schemas.xmlsoap.org/wsdl/", text2);
					return;
				}
				this.WriteAttribute(xmlAttribute.Name, xmlAttribute.NamespaceURI, xmlAttribute.Value);
			}
		}

		/// <summary>Writes an XML attribute.</summary>
		/// <param name="localName">The local name of the XML attribute.</param>
		/// <param name="ns">The namespace of the XML attribute.</param>
		/// <param name="value">The value of the XML attribute as a string.</param>
		// Token: 0x06002274 RID: 8820 RVA: 0x000CD5A4 File Offset: 0x000CB7A4
		protected void WriteAttribute(string localName, string ns, string value)
		{
			if (value == null)
			{
				return;
			}
			if (!(localName == "xmlns") && !localName.StartsWith("xmlns:", StringComparison.Ordinal))
			{
				int num = localName.IndexOf(':');
				if (num < 0)
				{
					if (ns == "http://www.w3.org/XML/1998/namespace")
					{
						string text = this.w.LookupPrefix(ns);
						if (text == null || text.Length == 0)
						{
							text = "xml";
						}
						this.w.WriteAttributeString(text, localName, ns, value);
						return;
					}
					this.w.WriteAttributeString(localName, ns, value);
					return;
				}
				else
				{
					string text2 = localName.Substring(0, num);
					this.w.WriteAttributeString(text2, localName.Substring(num + 1), ns, value);
				}
			}
		}

		/// <summary>Instructs an <see cref="T:System.Xml.XmlWriter" /> object to write an XML attribute.</summary>
		/// <param name="localName">The local name of the XML attribute.</param>
		/// <param name="ns">The namespace of the XML attribute.</param>
		/// <param name="value">The value of the XML attribute as a byte array.</param>
		// Token: 0x06002275 RID: 8821 RVA: 0x000CD64C File Offset: 0x000CB84C
		protected void WriteAttribute(string localName, string ns, byte[] value)
		{
			if (value == null)
			{
				return;
			}
			if (!(localName == "xmlns") && !localName.StartsWith("xmlns:", StringComparison.Ordinal))
			{
				int num = localName.IndexOf(':');
				if (num < 0)
				{
					if (ns == "http://www.w3.org/XML/1998/namespace")
					{
						string text = this.w.LookupPrefix(ns);
						if (text == null || text.Length == 0)
						{
						}
						this.w.WriteStartAttribute("xml", localName, ns);
					}
					else
					{
						this.w.WriteStartAttribute(null, localName, ns);
					}
				}
				else
				{
					string text2 = localName.Substring(0, num);
					text2 = this.w.LookupPrefix(ns);
					this.w.WriteStartAttribute(text2, localName.Substring(num + 1), ns);
				}
				XmlCustomFormatter.WriteArrayBase64(this.w, value, 0, value.Length);
				this.w.WriteEndAttribute();
			}
		}

		/// <summary>Instructs the <see cref="T:System.Xml.XmlWriter" /> to write an XML attribute that has no namespace specified for its name.</summary>
		/// <param name="localName">The local name of the XML attribute.</param>
		/// <param name="value">The value of the XML attribute as a string.</param>
		// Token: 0x06002276 RID: 8822 RVA: 0x000CD721 File Offset: 0x000CB921
		protected void WriteAttribute(string localName, string value)
		{
			if (value == null)
			{
				return;
			}
			this.w.WriteAttributeString(localName, null, value);
		}

		/// <summary>Instructs an <see cref="T:System.Xml.XmlWriter" /> object to write an XML attribute that has no namespace specified for its name.</summary>
		/// <param name="localName">The local name of the XML attribute.</param>
		/// <param name="value">The value of the XML attribute as a byte array.</param>
		// Token: 0x06002277 RID: 8823 RVA: 0x000CD735 File Offset: 0x000CB935
		protected void WriteAttribute(string localName, byte[] value)
		{
			if (value == null)
			{
				return;
			}
			this.w.WriteStartAttribute(null, localName, null);
			XmlCustomFormatter.WriteArrayBase64(this.w, value, 0, value.Length);
			this.w.WriteEndAttribute();
		}

		/// <summary>Writes an XML attribute where the namespace prefix is provided manually.</summary>
		/// <param name="prefix">The namespace prefix to write.</param>
		/// <param name="localName">The local name of the XML attribute.</param>
		/// <param name="ns">The namespace represented by the prefix.</param>
		/// <param name="value">The value of the XML attribute as a string.</param>
		// Token: 0x06002278 RID: 8824 RVA: 0x000CD764 File Offset: 0x000CB964
		protected void WriteAttribute(string prefix, string localName, string ns, string value)
		{
			if (value == null)
			{
				return;
			}
			this.w.WriteAttributeString(prefix, localName, null, value);
		}

		/// <summary>Writes a specified string value.</summary>
		/// <param name="value">The value of the string to write.</param>
		// Token: 0x06002279 RID: 8825 RVA: 0x000CD77B File Offset: 0x000CB97B
		protected void WriteValue(string value)
		{
			if (value == null)
			{
				return;
			}
			this.w.WriteString(value);
		}

		/// <summary>Writes a base-64 byte array.</summary>
		/// <param name="value">The byte array to write.</param>
		// Token: 0x0600227A RID: 8826 RVA: 0x000CD78D File Offset: 0x000CB98D
		protected void WriteValue(byte[] value)
		{
			if (value == null)
			{
				return;
			}
			XmlCustomFormatter.WriteArrayBase64(this.w, value, 0, value.Length);
		}

		/// <summary>Writes the XML declaration if the writer is positioned at the start of an XML document.</summary>
		// Token: 0x0600227B RID: 8827 RVA: 0x000CD7A3 File Offset: 0x000CB9A3
		protected void WriteStartDocument()
		{
			if (this.w.WriteState == WriteState.Start)
			{
				this.w.WriteStartDocument();
			}
		}

		/// <summary>Writes an XML element with a specified value in its body.</summary>
		/// <param name="localName">The local name of the XML element to be written without namespace qualification.</param>
		/// <param name="value">The text value of the XML element.</param>
		// Token: 0x0600227C RID: 8828 RVA: 0x000CD7BD File Offset: 0x000CB9BD
		protected void WriteElementString(string localName, string value)
		{
			this.WriteElementString(localName, null, value, null);
		}

		/// <summary>Writes an XML element with a specified value in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="ns">The namespace of the XML element.</param>
		/// <param name="value">The text value of the XML element.</param>
		// Token: 0x0600227D RID: 8829 RVA: 0x000CD7C9 File Offset: 0x000CB9C9
		protected void WriteElementString(string localName, string ns, string value)
		{
			this.WriteElementString(localName, ns, value, null);
		}

		/// <summary>Writes an XML element with a specified value in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="value">The text value of the XML element.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the xsi:type attribute.</param>
		// Token: 0x0600227E RID: 8830 RVA: 0x000CD7D5 File Offset: 0x000CB9D5
		protected void WriteElementString(string localName, string value, XmlQualifiedName xsiType)
		{
			this.WriteElementString(localName, null, value, xsiType);
		}

		/// <summary>Writes an XML element with a specified value in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="ns">The namespace of the XML element.</param>
		/// <param name="value">The text value of the XML element.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the xsi:type attribute.</param>
		// Token: 0x0600227F RID: 8831 RVA: 0x000CD7E4 File Offset: 0x000CB9E4
		protected void WriteElementString(string localName, string ns, string value, XmlQualifiedName xsiType)
		{
			if (value == null)
			{
				return;
			}
			if (xsiType == null)
			{
				this.w.WriteElementString(localName, ns, value);
				return;
			}
			this.w.WriteStartElement(localName, ns);
			this.WriteXsiType(xsiType.Name, xsiType.Namespace);
			this.w.WriteString(value);
			this.w.WriteEndElement();
		}

		/// <summary>Writes an XML element with a specified value in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="value">The text value of the XML element.</param>
		// Token: 0x06002280 RID: 8832 RVA: 0x000CD846 File Offset: 0x000CBA46
		protected void WriteElementStringRaw(string localName, string value)
		{
			this.WriteElementStringRaw(localName, null, value, null);
		}

		/// <summary>Writes an XML element with a specified value in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="value">The text value of the XML element.</param>
		// Token: 0x06002281 RID: 8833 RVA: 0x000CD852 File Offset: 0x000CBA52
		protected void WriteElementStringRaw(string localName, byte[] value)
		{
			this.WriteElementStringRaw(localName, null, value, null);
		}

		/// <summary>Writes an XML element with a specified value in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="ns">The namespace of the XML element.</param>
		/// <param name="value">The text value of the XML element.</param>
		// Token: 0x06002282 RID: 8834 RVA: 0x000CD85E File Offset: 0x000CBA5E
		protected void WriteElementStringRaw(string localName, string ns, string value)
		{
			this.WriteElementStringRaw(localName, ns, value, null);
		}

		/// <summary>Writes an XML element with a specified value in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="ns">The namespace of the XML element.</param>
		/// <param name="value">The text value of the XML element.</param>
		// Token: 0x06002283 RID: 8835 RVA: 0x000CD86A File Offset: 0x000CBA6A
		protected void WriteElementStringRaw(string localName, string ns, byte[] value)
		{
			this.WriteElementStringRaw(localName, ns, value, null);
		}

		/// <summary>Writes an XML element with a specified value in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="value">The text value of the XML element.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the xsi:type attribute.</param>
		// Token: 0x06002284 RID: 8836 RVA: 0x000CD876 File Offset: 0x000CBA76
		protected void WriteElementStringRaw(string localName, string value, XmlQualifiedName xsiType)
		{
			this.WriteElementStringRaw(localName, null, value, xsiType);
		}

		/// <summary>Writes an XML element with a specified value in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="value">The text value of the XML element.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the xsi:type attribute.</param>
		// Token: 0x06002285 RID: 8837 RVA: 0x000CD882 File Offset: 0x000CBA82
		protected void WriteElementStringRaw(string localName, byte[] value, XmlQualifiedName xsiType)
		{
			this.WriteElementStringRaw(localName, null, value, xsiType);
		}

		/// <summary>Writes an XML element with a specified value in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="ns">The namespace of the XML element.</param>
		/// <param name="value">The text value of the XML element.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the xsi:type attribute.</param>
		// Token: 0x06002286 RID: 8838 RVA: 0x000CD890 File Offset: 0x000CBA90
		protected void WriteElementStringRaw(string localName, string ns, string value, XmlQualifiedName xsiType)
		{
			if (value == null)
			{
				return;
			}
			this.w.WriteStartElement(localName, ns);
			if (xsiType != null)
			{
				this.WriteXsiType(xsiType.Name, xsiType.Namespace);
			}
			this.w.WriteRaw(value);
			this.w.WriteEndElement();
		}

		/// <summary>Writes an XML element with a specified value in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="ns">The namespace of the XML element.</param>
		/// <param name="value">The text value of the XML element.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the xsi:type attribute.</param>
		// Token: 0x06002287 RID: 8839 RVA: 0x000CD8E4 File Offset: 0x000CBAE4
		protected void WriteElementStringRaw(string localName, string ns, byte[] value, XmlQualifiedName xsiType)
		{
			if (value == null)
			{
				return;
			}
			this.w.WriteStartElement(localName, ns);
			if (xsiType != null)
			{
				this.WriteXsiType(xsiType.Name, xsiType.Namespace);
			}
			XmlCustomFormatter.WriteArrayBase64(this.w, value, 0, value.Length);
			this.w.WriteEndElement();
		}

		/// <summary>Writes a SOAP 1.2 RPC result element with a specified qualified name in its body.</summary>
		/// <param name="name">The local name of the result body.</param>
		/// <param name="ns">The namespace of the result body.</param>
		// Token: 0x06002288 RID: 8840 RVA: 0x000CD93B File Offset: 0x000CBB3B
		protected void WriteRpcResult(string name, string ns)
		{
			if (!this.soap12)
			{
				return;
			}
			this.WriteElementQualifiedName("result", "http://www.w3.org/2003/05/soap-rpc", new XmlQualifiedName(name, ns), null);
		}

		/// <summary>Writes an XML element with a specified qualified name in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="value">The name to write, using its prefix if namespace-qualified, in the element text.</param>
		// Token: 0x06002289 RID: 8841 RVA: 0x000CD95E File Offset: 0x000CBB5E
		protected void WriteElementQualifiedName(string localName, XmlQualifiedName value)
		{
			this.WriteElementQualifiedName(localName, null, value, null);
		}

		/// <summary>Writes an XML element with a specified qualified name in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="value">The name to write, using its prefix if namespace-qualified, in the element text.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the xsi:type attribute.</param>
		// Token: 0x0600228A RID: 8842 RVA: 0x000CD96A File Offset: 0x000CBB6A
		protected void WriteElementQualifiedName(string localName, XmlQualifiedName value, XmlQualifiedName xsiType)
		{
			this.WriteElementQualifiedName(localName, null, value, xsiType);
		}

		/// <summary>Writes an XML element with a specified qualified name in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="ns">The namespace of the XML element.</param>
		/// <param name="value">The name to write, using its prefix if namespace-qualified, in the element text.</param>
		// Token: 0x0600228B RID: 8843 RVA: 0x000CD976 File Offset: 0x000CBB76
		protected void WriteElementQualifiedName(string localName, string ns, XmlQualifiedName value)
		{
			this.WriteElementQualifiedName(localName, ns, value, null);
		}

		/// <summary>Writes an XML element with a specified qualified name in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="ns">The namespace of the XML element.</param>
		/// <param name="value">The name to write, using its prefix if namespace-qualified, in the element text.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the xsi:type attribute.</param>
		// Token: 0x0600228C RID: 8844 RVA: 0x000CD984 File Offset: 0x000CBB84
		protected void WriteElementQualifiedName(string localName, string ns, XmlQualifiedName value, XmlQualifiedName xsiType)
		{
			if (value == null)
			{
				return;
			}
			if (value.Namespace == null || value.Namespace.Length == 0)
			{
				this.WriteStartElement(localName, ns, null, true);
				this.WriteAttribute("xmlns", "");
			}
			else
			{
				this.w.WriteStartElement(localName, ns);
			}
			if (xsiType != null)
			{
				this.WriteXsiType(xsiType.Name, xsiType.Namespace);
			}
			this.w.WriteString(this.FromXmlQualifiedName(value, false));
			this.w.WriteEndElement();
		}

		/// <summary>Stores an implementation of the <see cref="T:System.Xml.Serialization.XmlSerializationWriteCallback" /> delegate and the type it applies to, for a later invocation.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of objects that are serialized.</param>
		/// <param name="typeName">The name of the type of objects that are serialized.</param>
		/// <param name="typeNs">The namespace of the type of objects that are serialized.</param>
		/// <param name="callback">An instance of the <see cref="T:System.Xml.Serialization.XmlSerializationWriteCallback" /> delegate.</param>
		// Token: 0x0600228D RID: 8845 RVA: 0x000CDA18 File Offset: 0x000CBC18
		protected void AddWriteCallback(Type type, string typeName, string typeNs, XmlSerializationWriteCallback callback)
		{
			XmlSerializationWriter.TypeEntry typeEntry = new XmlSerializationWriter.TypeEntry();
			typeEntry.typeName = typeName;
			typeEntry.typeNs = typeNs;
			typeEntry.type = type;
			typeEntry.callback = callback;
			this.typeEntries[type] = typeEntry;
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x000CDA58 File Offset: 0x000CBC58
		private void WriteArray(string name, string ns, object o, Type type)
		{
			Type type2 = TypeScope.GetArrayElementType(type, null);
			StringBuilder stringBuilder = new StringBuilder();
			if (!this.soap12)
			{
				while ((type2.IsArray || typeof(IEnumerable).IsAssignableFrom(type2)) && this.GetPrimitiveTypeName(type2, false) == null)
				{
					type2 = TypeScope.GetArrayElementType(type2, null);
					stringBuilder.Append("[]");
				}
			}
			string text;
			string text2;
			if (type2 == typeof(object))
			{
				text = "anyType";
				text2 = "http://www.w3.org/2001/XMLSchema";
			}
			else
			{
				XmlSerializationWriter.TypeEntry typeEntry = this.GetTypeEntry(type2);
				if (typeEntry != null)
				{
					text = typeEntry.typeName;
					text2 = typeEntry.typeNs;
				}
				else if (this.soap12)
				{
					XmlQualifiedName primitiveTypeName = this.GetPrimitiveTypeName(type2, false);
					if (primitiveTypeName != null)
					{
						text = primitiveTypeName.Name;
						text2 = primitiveTypeName.Namespace;
					}
					else
					{
						Type type3 = type2.BaseType;
						while (type3 != null)
						{
							typeEntry = this.GetTypeEntry(type3);
							if (typeEntry != null)
							{
								break;
							}
							type3 = type3.BaseType;
						}
						if (typeEntry != null)
						{
							text = typeEntry.typeName;
							text2 = typeEntry.typeNs;
						}
						else
						{
							text = "anyType";
							text2 = "http://www.w3.org/2001/XMLSchema";
						}
					}
				}
				else
				{
					XmlQualifiedName primitiveTypeName2 = this.GetPrimitiveTypeName(type2);
					text = primitiveTypeName2.Name;
					text2 = primitiveTypeName2.Namespace;
				}
			}
			if (stringBuilder.Length > 0)
			{
				text += stringBuilder.ToString();
			}
			if (this.soap12 && name != null && name.Length > 0)
			{
				this.WriteStartElement(name, ns, null, false);
			}
			else
			{
				this.WriteStartElement("Array", "http://schemas.xmlsoap.org/soap/encoding/", null, true);
			}
			this.WriteId(o, false);
			if (type.IsArray)
			{
				Array array = (Array)o;
				int length = array.Length;
				if (this.soap12)
				{
					this.w.WriteAttributeString("itemType", "http://www.w3.org/2003/05/soap-encoding", this.GetQualifiedName(text, text2));
					this.w.WriteAttributeString("arraySize", "http://www.w3.org/2003/05/soap-encoding", length.ToString(CultureInfo.InvariantCulture));
				}
				else
				{
					this.w.WriteAttributeString("arrayType", "http://schemas.xmlsoap.org/soap/encoding/", this.GetQualifiedName(text, text2) + "[" + length.ToString(CultureInfo.InvariantCulture) + "]");
				}
				for (int i = 0; i < length; i++)
				{
					this.WritePotentiallyReferencingElement("Item", "", array.GetValue(i), type2, false, true);
				}
			}
			else
			{
				int num = (typeof(ICollection).IsAssignableFrom(type) ? ((ICollection)o).Count : (-1));
				if (this.soap12)
				{
					this.w.WriteAttributeString("itemType", "http://www.w3.org/2003/05/soap-encoding", this.GetQualifiedName(text, text2));
					if (num >= 0)
					{
						this.w.WriteAttributeString("arraySize", "http://www.w3.org/2003/05/soap-encoding", num.ToString(CultureInfo.InvariantCulture));
					}
				}
				else
				{
					string text3 = ((num >= 0) ? ("[" + num + "]") : "[]");
					this.w.WriteAttributeString("arrayType", "http://schemas.xmlsoap.org/soap/encoding/", this.GetQualifiedName(text, text2) + text3);
				}
				IEnumerator enumerator = ((IEnumerable)o).GetEnumerator();
				if (enumerator != null)
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						this.WritePotentiallyReferencingElement("Item", "", obj, type2, false, true);
					}
				}
			}
			this.w.WriteEndElement();
		}

		/// <summary>Writes a SOAP message XML element that can contain a reference to a &lt;multiRef&gt; XML element for a given object.</summary>
		/// <param name="n">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="o">The object being serialized either in the current XML element or a multiRef element that is referenced by the current element.</param>
		// Token: 0x0600228F RID: 8847 RVA: 0x000CDDAA File Offset: 0x000CBFAA
		protected void WritePotentiallyReferencingElement(string n, string ns, object o)
		{
			this.WritePotentiallyReferencingElement(n, ns, o, null, false, false);
		}

		/// <summary>Writes a SOAP message XML element that can contain a reference to a &lt;multiRef&gt; XML element for a given object.</summary>
		/// <param name="n">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="o">The object being serialized either in the current XML element or a multiRef element that referenced by the current element.</param>
		/// <param name="ambientType">The type stored in the object's type mapping (as opposed to the object's type found directly through the typeof operation).</param>
		// Token: 0x06002290 RID: 8848 RVA: 0x000CDDB8 File Offset: 0x000CBFB8
		protected void WritePotentiallyReferencingElement(string n, string ns, object o, Type ambientType)
		{
			this.WritePotentiallyReferencingElement(n, ns, o, ambientType, false, false);
		}

		/// <summary>Writes a SOAP message XML element that can contain a reference to a &lt;multiRef&gt; XML element for a given object.</summary>
		/// <param name="n">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="o">The object being serialized either in the current XML element or a multiRef element that is referenced by the current element.</param>
		/// <param name="ambientType">The type stored in the object's type mapping (as opposed to the object's type found directly through the typeof operation).</param>
		/// <param name="suppressReference">true to serialize the object directly into the XML element rather than make the element reference another element that contains the data; otherwise, false.</param>
		// Token: 0x06002291 RID: 8849 RVA: 0x000CDDC7 File Offset: 0x000CBFC7
		protected void WritePotentiallyReferencingElement(string n, string ns, object o, Type ambientType, bool suppressReference)
		{
			this.WritePotentiallyReferencingElement(n, ns, o, ambientType, suppressReference, false);
		}

		/// <summary>Writes a SOAP message XML element that can contain a reference to a multiRef XML element for a given object.</summary>
		/// <param name="n">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="o">The object being serialized either in the current XML element or a multiRef element that referenced by the current element.</param>
		/// <param name="ambientType">The type stored in the object's type mapping (as opposed to the object's type found directly through the typeof operation).</param>
		/// <param name="suppressReference">true to serialize the object directly into the XML element rather than make the element reference another element that contains the data; otherwise, false.</param>
		/// <param name="isNullable">true to write an xsi:nil='true' attribute if the object to serialize is null; otherwise, false.</param>
		// Token: 0x06002292 RID: 8850 RVA: 0x000CDDD8 File Offset: 0x000CBFD8
		protected void WritePotentiallyReferencingElement(string n, string ns, object o, Type ambientType, bool suppressReference, bool isNullable)
		{
			if (o == null)
			{
				if (isNullable)
				{
					this.WriteNullTagEncoded(n, ns);
				}
				return;
			}
			Type type = o.GetType();
			if (Convert.GetTypeCode(o) == TypeCode.Object && !(o is Guid) && type != typeof(XmlQualifiedName) && !(o is XmlNode[]) && type != typeof(byte[]))
			{
				if ((suppressReference || this.soap12) && !this.IsIdDefined(o))
				{
					this.WriteReferencedElement(n, ns, o, ambientType);
					return;
				}
				if (n == null)
				{
					XmlSerializationWriter.TypeEntry typeEntry = this.GetTypeEntry(type);
					this.WriteReferencingElement(typeEntry.typeName, typeEntry.typeNs, o, isNullable);
					return;
				}
				this.WriteReferencingElement(n, ns, o, isNullable);
				return;
			}
			else
			{
				bool flag = type != ambientType && !type.IsEnum;
				XmlSerializationWriter.TypeEntry typeEntry2 = this.GetTypeEntry(type);
				if (typeEntry2 != null)
				{
					if (n == null)
					{
						this.WriteStartElement(typeEntry2.typeName, typeEntry2.typeNs, null, true);
					}
					else
					{
						this.WriteStartElement(n, ns, null, true);
					}
					if (flag)
					{
						this.WriteXsiType(typeEntry2.typeName, typeEntry2.typeNs);
					}
					typeEntry2.callback(o);
					this.w.WriteEndElement();
					return;
				}
				this.WriteTypedPrimitive(n, ns, o, flag);
				return;
			}
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x000CDF06 File Offset: 0x000CC106
		private void WriteReferencedElement(object o, Type ambientType)
		{
			this.WriteReferencedElement(null, null, o, ambientType);
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x000CDF14 File Offset: 0x000CC114
		private void WriteReferencedElement(string name, string ns, object o, Type ambientType)
		{
			if (name == null)
			{
				name = string.Empty;
			}
			Type type = o.GetType();
			if (type.IsArray || typeof(IEnumerable).IsAssignableFrom(type))
			{
				this.WriteArray(name, ns, o, type);
				return;
			}
			XmlSerializationWriter.TypeEntry typeEntry = this.GetTypeEntry(type);
			if (typeEntry == null)
			{
				throw this.CreateUnknownTypeException(type);
			}
			this.WriteStartElement((name.Length == 0) ? typeEntry.typeName : name, (ns == null) ? typeEntry.typeNs : ns, null, true);
			this.WriteId(o, false);
			if (ambientType != type)
			{
				this.WriteXsiType(typeEntry.typeName, typeEntry.typeNs);
			}
			typeEntry.callback(o);
			this.w.WriteEndElement();
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x000CDFCA File Offset: 0x000CC1CA
		private XmlSerializationWriter.TypeEntry GetTypeEntry(Type t)
		{
			if (this.typeEntries == null)
			{
				this.typeEntries = new Hashtable();
				this.InitCallbacks();
			}
			return (XmlSerializationWriter.TypeEntry)this.typeEntries[t];
		}

		/// <summary>Initializes an instances of the <see cref="T:System.Xml.Serialization.XmlSerializationWriteCallback" /> delegate to serialize SOAP-encoded XML data.</summary>
		// Token: 0x06002296 RID: 8854
		protected abstract void InitCallbacks();

		/// <summary>Serializes objects into SOAP-encoded multiRef XML elements in a SOAP message.</summary>
		// Token: 0x06002297 RID: 8855 RVA: 0x000CDFF8 File Offset: 0x000CC1F8
		protected void WriteReferencedElements()
		{
			if (this.referencesToWrite == null)
			{
				return;
			}
			for (int i = 0; i < this.referencesToWrite.Count; i++)
			{
				this.WriteReferencedElement(this.referencesToWrite[i], null);
			}
		}

		/// <summary>Initializes object references only while serializing a SOAP-encoded SOAP message.</summary>
		// Token: 0x06002298 RID: 8856 RVA: 0x000CE037 File Offset: 0x000CC237
		protected void TopLevelElement()
		{
			this.objectsInUse = new Hashtable();
		}

		/// <summary>Writes the namespace declaration attributes.</summary>
		/// <param name="xmlns">The XML namespaces to declare.</param>
		// Token: 0x06002299 RID: 8857 RVA: 0x000CE044 File Offset: 0x000CC244
		protected void WriteNamespaceDeclarations(XmlSerializerNamespaces xmlns)
		{
			if (xmlns != null)
			{
				foreach (object obj in xmlns.Namespaces)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					string text = (string)dictionaryEntry.Key;
					string text2 = (string)dictionaryEntry.Value;
					if (this.namespaces != null)
					{
						string text3 = this.namespaces.Namespaces[text] as string;
						if (text3 != null && text3 != text2)
						{
							throw new InvalidOperationException(Res.GetString("Illegal namespace declaration xmlns:{0}='{1}'. Namespace alias '{0}' already defined in the current scope.", new object[] { text, text2 }));
						}
					}
					string text4 = ((text2 == null || text2.Length == 0) ? null : this.Writer.LookupPrefix(text2));
					if (text4 == null || text4 != text)
					{
						this.WriteAttribute("xmlns", text, null, text2);
					}
				}
			}
			this.namespaces = null;
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x000CE14C File Offset: 0x000CC34C
		private string NextPrefix()
		{
			int num;
			if (this.usedPrefixes == null)
			{
				object obj = this.aliasBase;
				num = this.tempNamespacePrefix + 1;
				this.tempNamespacePrefix = num;
				return obj + num;
			}
			Hashtable hashtable;
			do
			{
				hashtable = this.usedPrefixes;
				num = this.tempNamespacePrefix + 1;
				this.tempNamespacePrefix = num;
			}
			while (hashtable.ContainsKey(num));
			return this.aliasBase + this.tempNamespacePrefix;
		}

		// Token: 0x04001839 RID: 6201
		private XmlWriter w;

		// Token: 0x0400183A RID: 6202
		private XmlSerializerNamespaces namespaces;

		// Token: 0x0400183B RID: 6203
		private int tempNamespacePrefix;

		// Token: 0x0400183C RID: 6204
		private Hashtable usedPrefixes;

		// Token: 0x0400183D RID: 6205
		private Hashtable references;

		// Token: 0x0400183E RID: 6206
		private string idBase;

		// Token: 0x0400183F RID: 6207
		private int nextId;

		// Token: 0x04001840 RID: 6208
		private Hashtable typeEntries;

		// Token: 0x04001841 RID: 6209
		private ArrayList referencesToWrite;

		// Token: 0x04001842 RID: 6210
		private Hashtable objectsInUse;

		// Token: 0x04001843 RID: 6211
		private string aliasBase = "q";

		// Token: 0x04001844 RID: 6212
		private bool soap12;

		// Token: 0x04001845 RID: 6213
		private bool escapeName = true;

		// Token: 0x02000359 RID: 857
		internal class TypeEntry
		{
			// Token: 0x04001846 RID: 6214
			internal XmlSerializationWriteCallback callback;

			// Token: 0x04001847 RID: 6215
			internal string typeNs;

			// Token: 0x04001848 RID: 6216
			internal string typeName;

			// Token: 0x04001849 RID: 6217
			internal Type type;
		}
	}
}
