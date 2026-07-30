using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Schema;
using MS.Internal.Xml.XPath;

namespace System.Xml.XPath
{
	/// <summary>Provides a cursor model for navigating and editing XML data.</summary>
	// Token: 0x020002B9 RID: 697
	[DebuggerDisplay("{debuggerDisplayProxy}")]
	public abstract class XPathNavigator : XPathItem, ICloneable, IXPathNavigable, IXmlNamespaceResolver
	{
		/// <summary>Gets the text value of the current node.</summary>
		/// <returns>A string that contains the text value of the current node.</returns>
		// Token: 0x06001968 RID: 6504 RVA: 0x00090BB2 File Offset: 0x0008EDB2
		public override string ToString()
		{
			return this.Value;
		}

		/// <summary>Gets a value indicating if the current node represents an XPath node.</summary>
		/// <returns>Always returns true.</returns>
		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001969 RID: 6505 RVA: 0x00003242 File Offset: 0x00001442
		public sealed override bool IsNode
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaType" /> information for the current node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaType" /> object; default is null.</returns>
		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x0600196A RID: 6506 RVA: 0x00090BBC File Offset: 0x0008EDBC
		public override XmlSchemaType XmlType
		{
			get
			{
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo == null || schemaInfo.Validity != XmlSchemaValidity.Valid)
				{
					return null;
				}
				XmlSchemaType memberType = schemaInfo.MemberType;
				if (memberType != null)
				{
					return memberType;
				}
				return schemaInfo.SchemaType;
			}
		}

		/// <summary>Sets the value of the current node.</summary>
		/// <param name="value">The new value of the node.</param>
		/// <exception cref="T:System.ArgumentNullException">The value parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on the root node, a namespace node, or the specified value is invalid.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x0600196B RID: 6507 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public virtual void SetValue(string value)
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets the current node as a boxed object of the most appropriate .NET Framework type.</summary>
		/// <returns>The current node as a boxed object of the most appropriate .NET Framework type.</returns>
		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x0600196C RID: 6508 RVA: 0x00090BF0 File Offset: 0x0008EDF0
		public override object TypedValue
		{
			get
			{
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo != null)
				{
					if (schemaInfo.Validity == XmlSchemaValidity.Valid)
					{
						XmlSchemaType xmlSchemaType = schemaInfo.MemberType;
						if (xmlSchemaType == null)
						{
							xmlSchemaType = schemaInfo.SchemaType;
						}
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype xmlSchemaDatatype = xmlSchemaType.Datatype;
							if (xmlSchemaDatatype != null)
							{
								return xmlSchemaType.ValueConverter.ChangeType(this.Value, xmlSchemaDatatype.ValueType, this);
							}
						}
					}
					else
					{
						XmlSchemaType xmlSchemaType = schemaInfo.SchemaType;
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype xmlSchemaDatatype = xmlSchemaType.Datatype;
							if (xmlSchemaDatatype != null)
							{
								return xmlSchemaType.ValueConverter.ChangeType(xmlSchemaDatatype.ParseValue(this.Value, this.NameTable, this), xmlSchemaDatatype.ValueType, this);
							}
						}
					}
				}
				return this.Value;
			}
		}

		/// <summary>Sets the typed value of the current node.</summary>
		/// <param name="typedValue">The new typed value of the node.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support the type of the object specified.</exception>
		/// <exception cref="T:System.ArgumentNullException">The value specified cannot be null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> is not positioned on an element or attribute node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x0600196D RID: 6509 RVA: 0x00090C88 File Offset: 0x0008EE88
		public virtual void SetTypedValue(object typedValue)
		{
			if (typedValue == null)
			{
				throw new ArgumentNullException("typedValue");
			}
			XPathNodeType nodeType = this.NodeType;
			if (nodeType - XPathNodeType.Element > 1)
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current position of the navigator."));
			}
			string text = null;
			IXmlSchemaInfo schemaInfo = this.SchemaInfo;
			if (schemaInfo != null)
			{
				XmlSchemaType schemaType = schemaInfo.SchemaType;
				if (schemaType != null)
				{
					text = schemaType.ValueConverter.ToString(typedValue, this);
					XmlSchemaDatatype datatype = schemaType.Datatype;
					if (datatype != null)
					{
						datatype.ParseValue(text, this.NameTable, this);
					}
				}
			}
			if (text == null)
			{
				text = XmlUntypedConverter.Untyped.ToString(typedValue, this);
			}
			this.SetValue(text);
		}

		/// <summary>Gets the .NET Framework <see cref="T:System.Type" /> of the current node.</summary>
		/// <returns>The .NET Framework <see cref="T:System.Type" /> of the current node. The default value is <see cref="T:System.String" />.</returns>
		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x0600196E RID: 6510 RVA: 0x00090D18 File Offset: 0x0008EF18
		public override Type ValueType
		{
			get
			{
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo != null)
				{
					if (schemaInfo.Validity == XmlSchemaValidity.Valid)
					{
						XmlSchemaType xmlSchemaType = schemaInfo.MemberType;
						if (xmlSchemaType == null)
						{
							xmlSchemaType = schemaInfo.SchemaType;
						}
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype xmlSchemaDatatype = xmlSchemaType.Datatype;
							if (xmlSchemaDatatype != null)
							{
								return xmlSchemaDatatype.ValueType;
							}
						}
					}
					else
					{
						XmlSchemaType xmlSchemaType = schemaInfo.SchemaType;
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype xmlSchemaDatatype = xmlSchemaType.Datatype;
							if (xmlSchemaDatatype != null)
							{
								return xmlSchemaDatatype.ValueType;
							}
						}
					}
				}
				return typeof(string);
			}
		}

		/// <summary>Gets the current node's value as a <see cref="T:System.Boolean" />.</summary>
		/// <returns>The current node's value as a <see cref="T:System.Boolean" />.</returns>
		/// <exception cref="T:System.FormatException">The current node's string value cannot be converted to a <see cref="T:System.Boolean" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Boolean" /> is not valid.</exception>
		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x0600196F RID: 6511 RVA: 0x00090D84 File Offset: 0x0008EF84
		public override bool ValueAsBoolean
		{
			get
			{
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo != null)
				{
					if (schemaInfo.Validity == XmlSchemaValidity.Valid)
					{
						XmlSchemaType xmlSchemaType = schemaInfo.MemberType;
						if (xmlSchemaType == null)
						{
							xmlSchemaType = schemaInfo.SchemaType;
						}
						if (xmlSchemaType != null)
						{
							return xmlSchemaType.ValueConverter.ToBoolean(this.Value);
						}
					}
					else
					{
						XmlSchemaType xmlSchemaType = schemaInfo.SchemaType;
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype datatype = xmlSchemaType.Datatype;
							if (datatype != null)
							{
								return xmlSchemaType.ValueConverter.ToBoolean(datatype.ParseValue(this.Value, this.NameTable, this));
							}
						}
					}
				}
				return XmlUntypedConverter.Untyped.ToBoolean(this.Value);
			}
		}

		/// <summary>Gets the current node's value as a <see cref="T:System.DateTime" />.</summary>
		/// <returns>The current node's value as a <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.FormatException">The current node's string value cannot be converted to a <see cref="T:System.DateTime" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.DateTime" /> is not valid.</exception>
		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001970 RID: 6512 RVA: 0x00090E10 File Offset: 0x0008F010
		public override DateTime ValueAsDateTime
		{
			get
			{
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo != null)
				{
					if (schemaInfo.Validity == XmlSchemaValidity.Valid)
					{
						XmlSchemaType xmlSchemaType = schemaInfo.MemberType;
						if (xmlSchemaType == null)
						{
							xmlSchemaType = schemaInfo.SchemaType;
						}
						if (xmlSchemaType != null)
						{
							return xmlSchemaType.ValueConverter.ToDateTime(this.Value);
						}
					}
					else
					{
						XmlSchemaType xmlSchemaType = schemaInfo.SchemaType;
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype datatype = xmlSchemaType.Datatype;
							if (datatype != null)
							{
								return xmlSchemaType.ValueConverter.ToDateTime(datatype.ParseValue(this.Value, this.NameTable, this));
							}
						}
					}
				}
				return XmlUntypedConverter.Untyped.ToDateTime(this.Value);
			}
		}

		/// <summary>Gets the current node's value as a <see cref="T:System.Double" />.</summary>
		/// <returns>The current node's value as a <see cref="T:System.Double" />.</returns>
		/// <exception cref="T:System.FormatException">The current node's string value cannot be converted to a <see cref="T:System.Double" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Double" /> is not valid.</exception>
		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001971 RID: 6513 RVA: 0x00090E9C File Offset: 0x0008F09C
		public override double ValueAsDouble
		{
			get
			{
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo != null)
				{
					if (schemaInfo.Validity == XmlSchemaValidity.Valid)
					{
						XmlSchemaType xmlSchemaType = schemaInfo.MemberType;
						if (xmlSchemaType == null)
						{
							xmlSchemaType = schemaInfo.SchemaType;
						}
						if (xmlSchemaType != null)
						{
							return xmlSchemaType.ValueConverter.ToDouble(this.Value);
						}
					}
					else
					{
						XmlSchemaType xmlSchemaType = schemaInfo.SchemaType;
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype datatype = xmlSchemaType.Datatype;
							if (datatype != null)
							{
								return xmlSchemaType.ValueConverter.ToDouble(datatype.ParseValue(this.Value, this.NameTable, this));
							}
						}
					}
				}
				return XmlUntypedConverter.Untyped.ToDouble(this.Value);
			}
		}

		/// <summary>Gets the current node's value as an <see cref="T:System.Int32" />.</summary>
		/// <returns>The current node's value as an <see cref="T:System.Int32" />.</returns>
		/// <exception cref="T:System.FormatException">The current node's string value cannot be converted to a <see cref="T:System.Int32" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Int32" /> is not valid.</exception>
		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001972 RID: 6514 RVA: 0x00090F28 File Offset: 0x0008F128
		public override int ValueAsInt
		{
			get
			{
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo != null)
				{
					if (schemaInfo.Validity == XmlSchemaValidity.Valid)
					{
						XmlSchemaType xmlSchemaType = schemaInfo.MemberType;
						if (xmlSchemaType == null)
						{
							xmlSchemaType = schemaInfo.SchemaType;
						}
						if (xmlSchemaType != null)
						{
							return xmlSchemaType.ValueConverter.ToInt32(this.Value);
						}
					}
					else
					{
						XmlSchemaType xmlSchemaType = schemaInfo.SchemaType;
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype datatype = xmlSchemaType.Datatype;
							if (datatype != null)
							{
								return xmlSchemaType.ValueConverter.ToInt32(datatype.ParseValue(this.Value, this.NameTable, this));
							}
						}
					}
				}
				return XmlUntypedConverter.Untyped.ToInt32(this.Value);
			}
		}

		/// <summary>Gets the current node's value as an <see cref="T:System.Int64" />.</summary>
		/// <returns>The current node's value as an <see cref="T:System.Int64" />.</returns>
		/// <exception cref="T:System.FormatException">The current node's string value cannot be converted to a <see cref="T:System.Int64" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Int64" /> is not valid.</exception>
		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001973 RID: 6515 RVA: 0x00090FB4 File Offset: 0x0008F1B4
		public override long ValueAsLong
		{
			get
			{
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo != null)
				{
					if (schemaInfo.Validity == XmlSchemaValidity.Valid)
					{
						XmlSchemaType xmlSchemaType = schemaInfo.MemberType;
						if (xmlSchemaType == null)
						{
							xmlSchemaType = schemaInfo.SchemaType;
						}
						if (xmlSchemaType != null)
						{
							return xmlSchemaType.ValueConverter.ToInt64(this.Value);
						}
					}
					else
					{
						XmlSchemaType xmlSchemaType = schemaInfo.SchemaType;
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype datatype = xmlSchemaType.Datatype;
							if (datatype != null)
							{
								return xmlSchemaType.ValueConverter.ToInt64(datatype.ParseValue(this.Value, this.NameTable, this));
							}
						}
					}
				}
				return XmlUntypedConverter.Untyped.ToInt64(this.Value);
			}
		}

		/// <summary>Gets the current node's value as the <see cref="T:System.Type" /> specified, using the <see cref="T:System.Xml.IXmlNamespaceResolver" /> object specified to resolve namespace prefixes.</summary>
		/// <returns>The value of the current node as the <see cref="T:System.Type" /> requested.</returns>
		/// <param name="returnType">The <see cref="T:System.Type" /> to return the current node's value as.</param>
		/// <param name="nsResolver">The <see cref="T:System.Xml.IXmlNamespaceResolver" /> object used to resolve namespace prefixes.</param>
		/// <exception cref="T:System.FormatException">The current node's value is not in the correct format for the target type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast is not valid.</exception>
		// Token: 0x06001974 RID: 6516 RVA: 0x00091040 File Offset: 0x0008F240
		public override object ValueAs(Type returnType, IXmlNamespaceResolver nsResolver)
		{
			if (nsResolver == null)
			{
				nsResolver = this;
			}
			IXmlSchemaInfo schemaInfo = this.SchemaInfo;
			if (schemaInfo != null)
			{
				if (schemaInfo.Validity == XmlSchemaValidity.Valid)
				{
					XmlSchemaType xmlSchemaType = schemaInfo.MemberType;
					if (xmlSchemaType == null)
					{
						xmlSchemaType = schemaInfo.SchemaType;
					}
					if (xmlSchemaType != null)
					{
						return xmlSchemaType.ValueConverter.ChangeType(this.Value, returnType, nsResolver);
					}
				}
				else
				{
					XmlSchemaType xmlSchemaType = schemaInfo.SchemaType;
					if (xmlSchemaType != null)
					{
						XmlSchemaDatatype datatype = xmlSchemaType.Datatype;
						if (datatype != null)
						{
							return xmlSchemaType.ValueConverter.ChangeType(datatype.ParseValue(this.Value, this.NameTable, nsResolver), returnType, nsResolver);
						}
					}
				}
			}
			return XmlUntypedConverter.Untyped.ChangeType(this.Value, returnType, nsResolver);
		}

		/// <summary>Creates a new copy of the <see cref="T:System.Xml.XPath.XPathNavigator" /> object.</summary>
		/// <returns>A new copy of the <see cref="T:System.Xml.XPath.XPathNavigator" /> object.</returns>
		// Token: 0x06001975 RID: 6517 RVA: 0x0000BCF5 File Offset: 0x00009EF5
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		/// <summary>Returns a copy of the <see cref="T:System.Xml.XPath.XPathNavigator" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNavigator" /> copy of this <see cref="T:System.Xml.XPath.XPathNavigator" />.</returns>
		// Token: 0x06001976 RID: 6518 RVA: 0x0000BCF5 File Offset: 0x00009EF5
		public virtual XPathNavigator CreateNavigator()
		{
			return this.Clone();
		}

		/// <summary>When overridden in a derived class, gets the <see cref="T:System.Xml.XmlNameTable" /> of the <see cref="T:System.Xml.XPath.XPathNavigator" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlNameTable" /> object enabling you to get the atomized version of a <see cref="T:System.String" /> within the XML document.</returns>
		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001977 RID: 6519
		public abstract XmlNameTable NameTable { get; }

		/// <summary>Gets the namespace URI for the specified prefix.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the namespace URI assigned to the namespace prefix specified; null if no namespace URI is assigned to the prefix specified. The <see cref="T:System.String" /> returned is atomized.</returns>
		/// <param name="prefix">The prefix whose namespace URI you want to resolve. To match the default namespace, pass <see cref="F:System.String.Empty" />.</param>
		// Token: 0x06001978 RID: 6520 RVA: 0x000910D8 File Offset: 0x0008F2D8
		public virtual string LookupNamespace(string prefix)
		{
			if (prefix == null)
			{
				return null;
			}
			if (this.NodeType != XPathNodeType.Element)
			{
				XPathNavigator xpathNavigator = this.Clone();
				if (xpathNavigator.MoveToParent())
				{
					return xpathNavigator.LookupNamespace(prefix);
				}
			}
			else if (this.MoveToNamespace(prefix))
			{
				string value = this.Value;
				this.MoveToParent();
				return value;
			}
			if (prefix.Length == 0)
			{
				return string.Empty;
			}
			if (prefix == "xml")
			{
				return "http://www.w3.org/XML/1998/namespace";
			}
			if (prefix == "xmlns")
			{
				return "http://www.w3.org/2000/xmlns/";
			}
			return null;
		}

		/// <summary>Gets the prefix declared for the specified namespace URI.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the namespace prefix assigned to the namespace URI specified; otherwise, <see cref="F:System.String.Empty" /> if no prefix is assigned to the namespace URI specified. The <see cref="T:System.String" /> returned is atomized.</returns>
		/// <param name="namespaceURI">The namespace URI to resolve for the prefix.</param>
		// Token: 0x06001979 RID: 6521 RVA: 0x00091158 File Offset: 0x0008F358
		public virtual string LookupPrefix(string namespaceURI)
		{
			if (namespaceURI == null)
			{
				return null;
			}
			XPathNavigator xpathNavigator = this.Clone();
			if (this.NodeType != XPathNodeType.Element)
			{
				if (xpathNavigator.MoveToParent())
				{
					return xpathNavigator.LookupPrefix(namespaceURI);
				}
			}
			else if (xpathNavigator.MoveToFirstNamespace(XPathNamespaceScope.All))
			{
				while (!(namespaceURI == xpathNavigator.Value))
				{
					if (!xpathNavigator.MoveToNextNamespace(XPathNamespaceScope.All))
					{
						goto IL_004C;
					}
				}
				return xpathNavigator.LocalName;
			}
			IL_004C:
			if (namespaceURI == this.LookupNamespace(string.Empty))
			{
				return string.Empty;
			}
			if (namespaceURI == "http://www.w3.org/XML/1998/namespace")
			{
				return "xml";
			}
			if (namespaceURI == "http://www.w3.org/2000/xmlns/")
			{
				return "xmlns";
			}
			return null;
		}

		/// <summary>Returns the in-scope namespaces of the current node.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IDictionary`2" /> collection of namespace names keyed by prefix.</returns>
		/// <param name="scope">An <see cref="T:System.Xml.XmlNamespaceScope" /> value specifying the namespaces to return.</param>
		// Token: 0x0600197A RID: 6522 RVA: 0x000911F4 File Offset: 0x0008F3F4
		public virtual IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			XPathNodeType nodeType = this.NodeType;
			if ((nodeType != XPathNodeType.Element && scope != XmlNamespaceScope.Local) || nodeType == XPathNodeType.Attribute || nodeType == XPathNodeType.Namespace)
			{
				XPathNavigator xpathNavigator = this.Clone();
				if (xpathNavigator.MoveToParent())
				{
					return xpathNavigator.GetNamespacesInScope(scope);
				}
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (scope == XmlNamespaceScope.All)
			{
				dictionary["xml"] = "http://www.w3.org/XML/1998/namespace";
			}
			if (this.MoveToFirstNamespace((XPathNamespaceScope)scope))
			{
				do
				{
					string localName = this.LocalName;
					string value = this.Value;
					if (localName.Length != 0 || value.Length != 0 || scope == XmlNamespaceScope.Local)
					{
						dictionary[localName] = value;
					}
				}
				while (this.MoveToNextNamespace((XPathNamespaceScope)scope));
				this.MoveToParent();
			}
			return dictionary;
		}

		/// <summary>Gets an <see cref="T:System.Collections.IEqualityComparer" /> used for equality comparison of <see cref="T:System.Xml.XPath.XPathNavigator" /> objects.</summary>
		/// <returns>An <see cref="T:System.Collections.IEqualityComparer" /> used for equality comparison of <see cref="T:System.Xml.XPath.XPathNavigator" /> objects.</returns>
		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x0600197B RID: 6523 RVA: 0x0009128F File Offset: 0x0008F48F
		public static IEqualityComparer NavigatorComparer
		{
			get
			{
				return XPathNavigator.comparer;
			}
		}

		/// <summary>When overridden in a derived class, creates a new <see cref="T:System.Xml.XPath.XPathNavigator" /> positioned at the same node as this <see cref="T:System.Xml.XPath.XPathNavigator" />.</summary>
		/// <returns>A new <see cref="T:System.Xml.XPath.XPathNavigator" /> positioned at the same node as this <see cref="T:System.Xml.XPath.XPathNavigator" />.</returns>
		// Token: 0x0600197C RID: 6524
		public abstract XPathNavigator Clone();

		/// <summary>When overridden in a derived class, gets the <see cref="T:System.Xml.XPath.XPathNodeType" /> of the current node.</summary>
		/// <returns>One of the <see cref="T:System.Xml.XPath.XPathNodeType" /> values representing the current node.</returns>
		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x0600197D RID: 6525
		public abstract XPathNodeType NodeType { get; }

		/// <summary>When overridden in a derived class, gets the <see cref="P:System.Xml.XPath.XPathNavigator.Name" /> of the current node without any namespace prefix.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the local name of the current node, or <see cref="F:System.String.Empty" /> if the current node does not have a name (for example, text or comment nodes).</returns>
		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x0600197E RID: 6526
		public abstract string LocalName { get; }

		/// <summary>When overridden in a derived class, gets the qualified name of the current node.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the qualified <see cref="P:System.Xml.XPath.XPathNavigator.Name" /> of the current node, or <see cref="F:System.String.Empty" /> if the current node does not have a name (for example, text or comment nodes).</returns>
		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x0600197F RID: 6527
		public abstract string Name { get; }

		/// <summary>When overridden in a derived class, gets the namespace URI of the current node.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the namespace URI of the current node, or <see cref="F:System.String.Empty" /> if the current node has no namespace URI.</returns>
		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001980 RID: 6528
		public abstract string NamespaceURI { get; }

		/// <summary>When overridden in a derived class, gets the namespace prefix associated with the current node.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the namespace prefix associated with the current node.</returns>
		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001981 RID: 6529
		public abstract string Prefix { get; }

		/// <summary>When overridden in a derived class, gets the base URI for the current node.</summary>
		/// <returns>The location from which the node was loaded, or <see cref="F:System.String.Empty" /> if there is no value.</returns>
		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001982 RID: 6530
		public abstract string BaseURI { get; }

		/// <summary>When overridden in a derived class, gets a value indicating whether the current node is an empty element without an end element tag.</summary>
		/// <returns>true if the current node is an empty element; otherwise, false.</returns>
		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001983 RID: 6531
		public abstract bool IsEmptyElement { get; }

		/// <summary>Gets the xml:lang scope for the current node.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the value of the xml:lang scope, or <see cref="F:System.String.Empty" /> if the current node has no xml:lang scope value to return.</returns>
		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001984 RID: 6532 RVA: 0x00091298 File Offset: 0x0008F498
		public virtual string XmlLang
		{
			get
			{
				XPathNavigator xpathNavigator = this.Clone();
				while (!xpathNavigator.MoveToAttribute("lang", "http://www.w3.org/XML/1998/namespace"))
				{
					if (!xpathNavigator.MoveToParent())
					{
						return string.Empty;
					}
				}
				return xpathNavigator.Value;
			}
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlReader" /> object that contains the current node and its child nodes.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlReader" /> object that contains the current node and its child nodes.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> is not positioned on an element node or the root node.</exception>
		// Token: 0x06001985 RID: 6533 RVA: 0x000912D4 File Offset: 0x0008F4D4
		public virtual XmlReader ReadSubtree()
		{
			XPathNodeType nodeType = this.NodeType;
			if (nodeType > XPathNodeType.Element)
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current position of the navigator."));
			}
			return this.CreateReader();
		}

		/// <summary>Streams the current node and its child nodes to the <see cref="T:System.Xml.XmlWriter" /> object specified.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> object to stream to.</param>
		// Token: 0x06001986 RID: 6534 RVA: 0x00091302 File Offset: 0x0008F502
		public virtual void WriteSubtree(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteNode(this, true);
		}

		/// <summary>Used by <see cref="T:System.Xml.XPath.XPathNavigator" /> implementations which provide a "virtualized" XML view over a store, to provide access to underlying objects.</summary>
		/// <returns>The default is null.</returns>
		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001987 RID: 6535 RVA: 0x0000365F File Offset: 0x0000185F
		public virtual object UnderlyingObject
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets a value indicating whether the current node has any attributes.</summary>
		/// <returns>Returns true if the current node has attributes; returns false if the current node has no attributes, or if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is not positioned on an element node.</returns>
		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001988 RID: 6536 RVA: 0x0009131A File Offset: 0x0008F51A
		public virtual bool HasAttributes
		{
			get
			{
				if (!this.MoveToFirstAttribute())
				{
					return false;
				}
				this.MoveToParent();
				return true;
			}
		}

		/// <summary>Gets the value of the attribute with the specified local name and namespace URI.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the value of the specified attribute; <see cref="F:System.String.Empty" /> if a matching attribute is not found, or if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is not positioned on an element node.</returns>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="namespaceURI">The namespace URI of the attribute.</param>
		// Token: 0x06001989 RID: 6537 RVA: 0x0009132E File Offset: 0x0008F52E
		public virtual string GetAttribute(string localName, string namespaceURI)
		{
			if (!this.MoveToAttribute(localName, namespaceURI))
			{
				return "";
			}
			string value = this.Value;
			this.MoveToParent();
			return value;
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the attribute with the matching local name and namespace URI.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the attribute; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="namespaceURI">The namespace URI of the attribute; null for an empty namespace.</param>
		// Token: 0x0600198A RID: 6538 RVA: 0x0009134D File Offset: 0x0008F54D
		public virtual bool MoveToAttribute(string localName, string namespaceURI)
		{
			if (this.MoveToFirstAttribute())
			{
				while (!(localName == this.LocalName) || !(namespaceURI == this.NamespaceURI))
				{
					if (!this.MoveToNextAttribute())
					{
						this.MoveToParent();
						return false;
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the first attribute of the current node.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the first attribute of the current node; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x0600198B RID: 6539
		public abstract bool MoveToFirstAttribute();

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the next attribute.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the next attribute; false if there are no more attributes. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x0600198C RID: 6540
		public abstract bool MoveToNextAttribute();

		/// <summary>Returns the value of the namespace node corresponding to the specified local name.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the value of the namespace node; <see cref="F:System.String.Empty" /> if a matching namespace node is not found, or if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is not positioned on an element node.</returns>
		/// <param name="name">The local name of the namespace node.</param>
		// Token: 0x0600198D RID: 6541 RVA: 0x00091388 File Offset: 0x0008F588
		public virtual string GetNamespace(string name)
		{
			if (this.MoveToNamespace(name))
			{
				string value = this.Value;
				this.MoveToParent();
				return value;
			}
			if (name == "xml")
			{
				return "http://www.w3.org/XML/1998/namespace";
			}
			if (name == "xmlns")
			{
				return "http://www.w3.org/2000/xmlns/";
			}
			return string.Empty;
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the namespace node with the specified namespace prefix.</summary>
		/// <returns>true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the specified namespace; false if a matching namespace node was not found, or if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is not positioned on an element node. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		/// <param name="name">The namespace prefix of the namespace node.</param>
		// Token: 0x0600198E RID: 6542 RVA: 0x000913D7 File Offset: 0x0008F5D7
		public virtual bool MoveToNamespace(string name)
		{
			if (this.MoveToFirstNamespace(XPathNamespaceScope.All))
			{
				while (!(name == this.LocalName))
				{
					if (!this.MoveToNextNamespace(XPathNamespaceScope.All))
					{
						this.MoveToParent();
						return false;
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the first namespace node that matches the <see cref="T:System.Xml.XPath.XPathNamespaceScope" /> specified.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the first namespace node; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		/// <param name="namespaceScope">An <see cref="T:System.Xml.XPath.XPathNamespaceScope" /> value describing the namespace scope. </param>
		// Token: 0x0600198F RID: 6543
		public abstract bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope);

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the next namespace node matching the <see cref="T:System.Xml.XPath.XPathNamespaceScope" /> specified.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the next namespace node; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		/// <param name="namespaceScope">An <see cref="T:System.Xml.XPath.XPathNamespaceScope" /> value describing the namespace scope. </param>
		// Token: 0x06001990 RID: 6544
		public abstract bool MoveToNextNamespace(XPathNamespaceScope namespaceScope);

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to first namespace node of the current node.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the first namespace node; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x06001991 RID: 6545 RVA: 0x00091403 File Offset: 0x0008F603
		public bool MoveToFirstNamespace()
		{
			return this.MoveToFirstNamespace(XPathNamespaceScope.All);
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the next namespace node.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the next namespace node; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x06001992 RID: 6546 RVA: 0x0009140C File Offset: 0x0008F60C
		public bool MoveToNextNamespace()
		{
			return this.MoveToNextNamespace(XPathNamespaceScope.All);
		}

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the next sibling node of the current node.</summary>
		/// <returns>true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the next sibling node; otherwise, false if there are no more siblings or if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is currently positioned on an attribute node. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x06001993 RID: 6547
		public abstract bool MoveToNext();

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the previous sibling node of the current node.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the previous sibling node; otherwise, false if there is no previous sibling node or if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is currently positioned on an attribute node. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x06001994 RID: 6548
		public abstract bool MoveToPrevious();

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the first sibling node of the current node.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the first sibling node of the current node; false if there is no first sibling, or if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is currently positioned on an attribute node. If the <see cref="T:System.Xml.XPath.XPathNavigator" /> is already positioned on the first sibling, <see cref="T:System.Xml.XPath.XPathNavigator" /> will return true and will not move its position.If <see cref="T:System.Xml.XPath.XPathNavigator.MoveToFirst" /> returns false because there is no first sibling, or if <see cref="T:System.Xml.XPath.XPathNavigator" /> is currently positioned on an attribute, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x06001995 RID: 6549 RVA: 0x00091418 File Offset: 0x0008F618
		public virtual bool MoveToFirst()
		{
			XPathNodeType nodeType = this.NodeType;
			return nodeType - XPathNodeType.Attribute > 1 && this.MoveToParent() && this.MoveToFirstChild();
		}

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the first child node of the current node.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the first child node of the current node; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x06001996 RID: 6550
		public abstract bool MoveToFirstChild();

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the parent node of the current node.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the parent node of the current node; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x06001997 RID: 6551
		public abstract bool MoveToParent();

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the root node that the current node belongs to.</summary>
		// Token: 0x06001998 RID: 6552 RVA: 0x00091444 File Offset: 0x0008F644
		public virtual void MoveToRoot()
		{
			while (this.MoveToParent())
			{
			}
		}

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the same position as the specified <see cref="T:System.Xml.XPath.XPathNavigator" />.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the same position as the specified <see cref="T:System.Xml.XPath.XPathNavigator" />; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		/// <param name="other">The <see cref="T:System.Xml.XPath.XPathNavigator" /> positioned on the node that you want to move to. </param>
		// Token: 0x06001999 RID: 6553
		public abstract bool MoveTo(XPathNavigator other);

		/// <summary>When overridden in a derived class, moves to the node that has an attribute of type ID whose value matches the specified <see cref="T:System.String" />.</summary>
		/// <returns>true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving; otherwise, false. If false, the position of the navigator is unchanged.</returns>
		/// <param name="id">A <see cref="T:System.String" /> representing the ID value of the node to which you want to move.</param>
		// Token: 0x0600199A RID: 6554
		public abstract bool MoveToId(string id);

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the child node with the local name and namespace URI specified.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the child node; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		/// <param name="localName">The local name of the child node to move to.</param>
		/// <param name="namespaceURI">The namespace URI of the child node to move to.</param>
		// Token: 0x0600199B RID: 6555 RVA: 0x00091450 File Offset: 0x0008F650
		public virtual bool MoveToChild(string localName, string namespaceURI)
		{
			if (this.MoveToFirstChild())
			{
				while (this.NodeType != XPathNodeType.Element || !(localName == this.LocalName) || !(namespaceURI == this.NamespaceURI))
				{
					if (!this.MoveToNext())
					{
						this.MoveToParent();
						return false;
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the child node of the <see cref="T:System.Xml.XPath.XPathNodeType" /> specified.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the child node; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		/// <param name="type">The <see cref="T:System.Xml.XPath.XPathNodeType" /> of the child node to move to.</param>
		// Token: 0x0600199C RID: 6556 RVA: 0x0009149C File Offset: 0x0008F69C
		public virtual bool MoveToChild(XPathNodeType type)
		{
			if (this.MoveToFirstChild())
			{
				int contentKindMask = XPathNavigator.GetContentKindMask(type);
				while (((1 << (int)this.NodeType) & contentKindMask) == 0)
				{
					if (!this.MoveToNext())
					{
						this.MoveToParent();
						return false;
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the element with the local name and namespace URI specified in document order.</summary>
		/// <returns>true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> moved successfully; otherwise false.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceURI">The namespace URI of the element.</param>
		// Token: 0x0600199D RID: 6557 RVA: 0x000914D9 File Offset: 0x0008F6D9
		public virtual bool MoveToFollowing(string localName, string namespaceURI)
		{
			return this.MoveToFollowing(localName, namespaceURI, null);
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the element with the local name and namespace URI specified, to the boundary specified, in document order.</summary>
		/// <returns>true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> moved successfully; otherwise false.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceURI">The namespace URI of the element.</param>
		/// <param name="end">The <see cref="T:System.Xml.XPath.XPathNavigator" /> object positioned on the element boundary which the current <see cref="T:System.Xml.XPath.XPathNavigator" /> will not move past while searching for the following element.</param>
		// Token: 0x0600199E RID: 6558 RVA: 0x000914E4 File Offset: 0x0008F6E4
		public virtual bool MoveToFollowing(string localName, string namespaceURI, XPathNavigator end)
		{
			XPathNavigator xpathNavigator = this.Clone();
			XPathNodeType xpathNodeType;
			if (end != null)
			{
				xpathNodeType = end.NodeType;
				if (xpathNodeType - XPathNodeType.Attribute <= 1)
				{
					end = end.Clone();
					end.MoveToNonDescendant();
				}
			}
			xpathNodeType = this.NodeType;
			if (xpathNodeType - XPathNodeType.Attribute <= 1 && !this.MoveToParent())
			{
				return false;
			}
			for (;;)
			{
				if (!this.MoveToFirstChild())
				{
					while (!this.MoveToNext())
					{
						if (!this.MoveToParent())
						{
							goto Block_6;
						}
					}
				}
				if (end != null && this.IsSamePosition(end))
				{
					goto Block_8;
				}
				if (this.NodeType == XPathNodeType.Element && !(localName != this.LocalName) && !(namespaceURI != this.NamespaceURI))
				{
					return true;
				}
			}
			Block_6:
			this.MoveTo(xpathNavigator);
			return false;
			Block_8:
			this.MoveTo(xpathNavigator);
			return false;
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the following element of the <see cref="T:System.Xml.XPath.XPathNodeType" /> specified in document order.</summary>
		/// <returns>true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> moved successfully; otherwise false.</returns>
		/// <param name="type">The <see cref="T:System.Xml.XPath.XPathNodeType" /> of the element. The <see cref="T:System.Xml.XPath.XPathNodeType" /> cannot be <see cref="F:System.Xml.XPath.XPathNodeType.Attribute" /> or <see cref="F:System.Xml.XPath.XPathNodeType.Namespace" />.</param>
		// Token: 0x0600199F RID: 6559 RVA: 0x0009158C File Offset: 0x0008F78C
		public virtual bool MoveToFollowing(XPathNodeType type)
		{
			return this.MoveToFollowing(type, null);
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the following element of the <see cref="T:System.Xml.XPath.XPathNodeType" /> specified, to the boundary specified, in document order.</summary>
		/// <returns>true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> moved successfully; otherwise false.</returns>
		/// <param name="type">The <see cref="T:System.Xml.XPath.XPathNodeType" /> of the element. The <see cref="T:System.Xml.XPath.XPathNodeType" /> cannot be <see cref="F:System.Xml.XPath.XPathNodeType.Attribute" /> or <see cref="F:System.Xml.XPath.XPathNodeType.Namespace" />.</param>
		/// <param name="end">The <see cref="T:System.Xml.XPath.XPathNavigator" /> object positioned on the element boundary which the current <see cref="T:System.Xml.XPath.XPathNavigator" /> will not move past while searching for the following element.</param>
		// Token: 0x060019A0 RID: 6560 RVA: 0x00091598 File Offset: 0x0008F798
		public virtual bool MoveToFollowing(XPathNodeType type, XPathNavigator end)
		{
			XPathNavigator xpathNavigator = this.Clone();
			int contentKindMask = XPathNavigator.GetContentKindMask(type);
			XPathNodeType xpathNodeType;
			if (end != null)
			{
				xpathNodeType = end.NodeType;
				if (xpathNodeType - XPathNodeType.Attribute <= 1)
				{
					end = end.Clone();
					end.MoveToNonDescendant();
				}
			}
			xpathNodeType = this.NodeType;
			if (xpathNodeType - XPathNodeType.Attribute <= 1 && !this.MoveToParent())
			{
				return false;
			}
			for (;;)
			{
				if (!this.MoveToFirstChild())
				{
					while (!this.MoveToNext())
					{
						if (!this.MoveToParent())
						{
							goto Block_6;
						}
					}
				}
				if (end != null && this.IsSamePosition(end))
				{
					goto Block_8;
				}
				if (((1 << (int)this.NodeType) & contentKindMask) != 0)
				{
					return true;
				}
			}
			Block_6:
			this.MoveTo(xpathNavigator);
			return false;
			Block_8:
			this.MoveTo(xpathNavigator);
			return false;
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the next sibling node with the local name and namespace URI specified.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the next sibling node; false if there are no more siblings, or if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is currently positioned on an attribute node. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		/// <param name="localName">The local name of the next sibling node to move to.</param>
		/// <param name="namespaceURI">The namespace URI of the next sibling node to move to.</param>
		// Token: 0x060019A1 RID: 6561 RVA: 0x00091634 File Offset: 0x0008F834
		public virtual bool MoveToNext(string localName, string namespaceURI)
		{
			XPathNavigator xpathNavigator = this.Clone();
			while (this.MoveToNext())
			{
				if (this.NodeType == XPathNodeType.Element && localName == this.LocalName && namespaceURI == this.NamespaceURI)
				{
					return true;
				}
			}
			this.MoveTo(xpathNavigator);
			return false;
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the next sibling node of the current node that matches the <see cref="T:System.Xml.XPath.XPathNodeType" /> specified.</summary>
		/// <returns>true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the next sibling node; otherwise, false if there are no more siblings or if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is currently positioned on an attribute node. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		/// <param name="type">The <see cref="T:System.Xml.XPath.XPathNodeType" /> of the sibling node to move to.</param>
		// Token: 0x060019A2 RID: 6562 RVA: 0x00091684 File Offset: 0x0008F884
		public virtual bool MoveToNext(XPathNodeType type)
		{
			XPathNavigator xpathNavigator = this.Clone();
			int contentKindMask = XPathNavigator.GetContentKindMask(type);
			while (this.MoveToNext())
			{
				if (((1 << (int)this.NodeType) & contentKindMask) != 0)
				{
					return true;
				}
			}
			this.MoveTo(xpathNavigator);
			return false;
		}

		/// <summary>Gets a value indicating whether the current node has any child nodes.</summary>
		/// <returns>true if the current node has any child nodes; otherwise, false.</returns>
		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060019A3 RID: 6563 RVA: 0x000916C3 File Offset: 0x0008F8C3
		public virtual bool HasChildren
		{
			get
			{
				if (this.MoveToFirstChild())
				{
					this.MoveToParent();
					return true;
				}
				return false;
			}
		}

		/// <summary>When overridden in a derived class, determines whether the current <see cref="T:System.Xml.XPath.XPathNavigator" /> is at the same position as the specified <see cref="T:System.Xml.XPath.XPathNavigator" />.</summary>
		/// <returns>true if the two <see cref="T:System.Xml.XPath.XPathNavigator" /> objects have the same position; otherwise, false.</returns>
		/// <param name="other">The <see cref="T:System.Xml.XPath.XPathNavigator" /> to compare to this <see cref="T:System.Xml.XPath.XPathNavigator" />.</param>
		// Token: 0x060019A4 RID: 6564
		public abstract bool IsSamePosition(XPathNavigator other);

		/// <summary>Determines whether the specified <see cref="T:System.Xml.XPath.XPathNavigator" /> is a descendant of the current <see cref="T:System.Xml.XPath.XPathNavigator" />.</summary>
		/// <returns>true if the specified <see cref="T:System.Xml.XPath.XPathNavigator" /> is a descendant of the current <see cref="T:System.Xml.XPath.XPathNavigator" />; otherwise, false.</returns>
		/// <param name="nav">The <see cref="T:System.Xml.XPath.XPathNavigator" /> to compare to this <see cref="T:System.Xml.XPath.XPathNavigator" />.</param>
		// Token: 0x060019A5 RID: 6565 RVA: 0x000916D7 File Offset: 0x0008F8D7
		public virtual bool IsDescendant(XPathNavigator nav)
		{
			if (nav != null)
			{
				nav = nav.Clone();
				while (nav.MoveToParent())
				{
					if (nav.IsSamePosition(this))
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>Compares the position of the current <see cref="T:System.Xml.XPath.XPathNavigator" /> with the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> specified.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlNodeOrder" /> value representing the comparative position of the two <see cref="T:System.Xml.XPath.XPathNavigator" /> objects.</returns>
		/// <param name="nav">The <see cref="T:System.Xml.XPath.XPathNavigator" /> to compare against.</param>
		// Token: 0x060019A6 RID: 6566 RVA: 0x000916FC File Offset: 0x0008F8FC
		public virtual XmlNodeOrder ComparePosition(XPathNavigator nav)
		{
			if (nav == null)
			{
				return XmlNodeOrder.Unknown;
			}
			if (this.IsSamePosition(nav))
			{
				return XmlNodeOrder.Same;
			}
			XPathNavigator xpathNavigator = this.Clone();
			XPathNavigator xpathNavigator2 = nav.Clone();
			int i = XPathNavigator.GetDepth(xpathNavigator.Clone());
			int j = XPathNavigator.GetDepth(xpathNavigator2.Clone());
			if (i > j)
			{
				while (i > j)
				{
					xpathNavigator.MoveToParent();
					i--;
				}
				if (xpathNavigator.IsSamePosition(xpathNavigator2))
				{
					return XmlNodeOrder.After;
				}
			}
			if (j > i)
			{
				while (j > i)
				{
					xpathNavigator2.MoveToParent();
					j--;
				}
				if (xpathNavigator.IsSamePosition(xpathNavigator2))
				{
					return XmlNodeOrder.Before;
				}
			}
			XPathNavigator xpathNavigator3 = xpathNavigator.Clone();
			XPathNavigator xpathNavigator4 = xpathNavigator2.Clone();
			while (xpathNavigator3.MoveToParent() && xpathNavigator4.MoveToParent())
			{
				if (xpathNavigator3.IsSamePosition(xpathNavigator4))
				{
					xpathNavigator.GetType().ToString() != "Microsoft.VisualStudio.Modeling.StoreNavigator";
					return this.CompareSiblings(xpathNavigator, xpathNavigator2);
				}
				xpathNavigator.MoveToParent();
				xpathNavigator2.MoveToParent();
			}
			return XmlNodeOrder.Unknown;
		}

		/// <summary>Gets the schema information that has been assigned to the current node as a result of schema validation.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.IXmlSchemaInfo" /> object that contains the schema information for the current node.</returns>
		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060019A7 RID: 6567 RVA: 0x000296BE File Offset: 0x000278BE
		public virtual IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this as IXmlSchemaInfo;
			}
		}

		/// <summary>Verifies that the XML data in the <see cref="T:System.Xml.XPath.XPathNavigator" /> conforms to the XML Schema definition language (XSD) schema provided.</summary>
		/// <returns>true if no schema validation errors occurred; otherwise, false.</returns>
		/// <param name="schemas">The <see cref="T:System.Xml.Schema.XmlSchemaSet" /> containing the schemas used to validate the XML data contained in the <see cref="T:System.Xml.XPath.XPathNavigator" />.</param>
		/// <param name="validationEventHandler">The <see cref="T:System.Xml.Schema.ValidationEventHandler" /> that receives information about schema validation warnings and errors.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">A schema validation error occurred, and no <see cref="T:System.Xml.Schema.ValidationEventHandler" /> was specified to handle validation errors.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on a node that is not an element, attribute, or the root node or there is not type information to perform validation.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="M:System.Xml.XPath.XPathNavigator.CheckValidity(System.Xml.Schema.XmlSchemaSet,System.Xml.Schema.ValidationEventHandler)" /> method was called with an <see cref="T:System.Xml.Schema.XmlSchemaSet" /> parameter when the <see cref="T:System.Xml.XPath.XPathNavigator" /> was not positioned on the root node of the XML data.</exception>
		// Token: 0x060019A8 RID: 6568 RVA: 0x000917DC File Offset: 0x0008F9DC
		public virtual bool CheckValidity(XmlSchemaSet schemas, ValidationEventHandler validationEventHandler)
		{
			XmlSchemaType xmlSchemaType = null;
			XmlSchemaElement xmlSchemaElement = null;
			XmlSchemaAttribute xmlSchemaAttribute = null;
			switch (this.NodeType)
			{
			case XPathNodeType.Root:
				if (schemas == null)
				{
					throw new InvalidOperationException(Res.GetString("An XmlSchemaSet must be provided to validate the document."));
				}
				xmlSchemaType = null;
				break;
			case XPathNodeType.Element:
			{
				if (schemas == null)
				{
					throw new InvalidOperationException(Res.GetString("An XmlSchemaSet must be provided to validate the document."));
				}
				IXmlSchemaInfo xmlSchemaInfo = this.SchemaInfo;
				if (xmlSchemaInfo != null)
				{
					xmlSchemaType = xmlSchemaInfo.SchemaType;
					xmlSchemaElement = xmlSchemaInfo.SchemaElement;
				}
				if (xmlSchemaType == null && xmlSchemaElement == null)
				{
					throw new InvalidOperationException(Res.GetString("Element should have prior schema information to call this method.", null));
				}
				break;
			}
			case XPathNodeType.Attribute:
			{
				if (schemas == null)
				{
					throw new InvalidOperationException(Res.GetString("An XmlSchemaSet must be provided to validate the document."));
				}
				IXmlSchemaInfo xmlSchemaInfo = this.SchemaInfo;
				if (xmlSchemaInfo != null)
				{
					xmlSchemaType = xmlSchemaInfo.SchemaType;
					xmlSchemaAttribute = xmlSchemaInfo.SchemaAttribute;
				}
				if (xmlSchemaType == null && xmlSchemaAttribute == null)
				{
					throw new InvalidOperationException(Res.GetString("Element should have prior schema information to call this method.", null));
				}
				break;
			}
			default:
				throw new InvalidOperationException(Res.GetString("Validate and CheckValidity are only allowed on Root or Element nodes.", null));
			}
			XmlReader xmlReader = this.CreateReader();
			XPathNavigator.CheckValidityHelper checkValidityHelper = new XPathNavigator.CheckValidityHelper(validationEventHandler, xmlReader as XPathNavigatorReader);
			validationEventHandler = new ValidationEventHandler(checkValidityHelper.ValidationCallback);
			XmlReader validatingReader = this.GetValidatingReader(xmlReader, schemas, validationEventHandler, xmlSchemaType, xmlSchemaElement, xmlSchemaAttribute);
			while (validatingReader.Read())
			{
			}
			return checkValidityHelper.IsValid;
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x00091904 File Offset: 0x0008FB04
		private XmlReader GetValidatingReader(XmlReader reader, XmlSchemaSet schemas, ValidationEventHandler validationEvent, XmlSchemaType schemaType, XmlSchemaElement schemaElement, XmlSchemaAttribute schemaAttribute)
		{
			if (schemaAttribute != null)
			{
				return schemaAttribute.Validate(reader, null, schemas, validationEvent);
			}
			if (schemaElement != null)
			{
				return schemaElement.Validate(reader, null, schemas, validationEvent);
			}
			if (schemaType != null)
			{
				return schemaType.Validate(reader, null, schemas, validationEvent);
			}
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.ConformanceLevel = ConformanceLevel.Auto;
			xmlReaderSettings.ValidationType = ValidationType.Schema;
			xmlReaderSettings.Schemas = schemas;
			xmlReaderSettings.ValidationEventHandler += validationEvent;
			return XmlReader.Create(reader, xmlReaderSettings);
		}

		/// <summary>Compiles a string representing an XPath expression and returns an <see cref="T:System.Xml.XPath.XPathExpression" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathExpression" /> object representing the XPath expression.</returns>
		/// <param name="xpath">A string representing an XPath expression.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="xpath" /> parameter contains an XPath expression that is not valid.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x060019AA RID: 6570 RVA: 0x0009196A File Offset: 0x0008FB6A
		public virtual XPathExpression Compile(string xpath)
		{
			return XPathExpression.Compile(xpath);
		}

		/// <summary>Selects a single node in the <see cref="T:System.Xml.XPath.XPathNavigator" /> using the specified XPath query.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNavigator" /> object that contains the first matching node for the XPath query specified; otherwise, null if there are no query results.</returns>
		/// <param name="xpath">A <see cref="T:System.String" /> representing an XPath expression.</param>
		/// <exception cref="T:System.ArgumentException">An error was encountered in the XPath query or the return type of the XPath expression is not a node.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath query is not valid.</exception>
		// Token: 0x060019AB RID: 6571 RVA: 0x00091972 File Offset: 0x0008FB72
		public virtual XPathNavigator SelectSingleNode(string xpath)
		{
			return this.SelectSingleNode(XPathExpression.Compile(xpath));
		}

		/// <summary>Selects a single node in the <see cref="T:System.Xml.XPath.XPathNavigator" /> object using the specified XPath query with the <see cref="T:System.Xml.IXmlNamespaceResolver" /> object specified to resolve namespace prefixes.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNavigator" /> object that contains the first matching node for the XPath query specified; otherwise null if there are no query results.</returns>
		/// <param name="xpath">A <see cref="T:System.String" /> representing an XPath expression.</param>
		/// <param name="resolver">The <see cref="T:System.Xml.IXmlNamespaceResolver" /> object used to resolve namespace prefixes in the XPath query.</param>
		/// <exception cref="T:System.ArgumentException">An error was encountered in the XPath query or the return type of the XPath expression is not a node.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath query is not valid.</exception>
		// Token: 0x060019AC RID: 6572 RVA: 0x00091980 File Offset: 0x0008FB80
		public virtual XPathNavigator SelectSingleNode(string xpath, IXmlNamespaceResolver resolver)
		{
			return this.SelectSingleNode(XPathExpression.Compile(xpath, resolver));
		}

		/// <summary>Selects a single node in the <see cref="T:System.Xml.XPath.XPathNavigator" /> using the specified <see cref="T:System.Xml.XPath.XPathExpression" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNavigator" /> object that contains the first matching node for the XPath query specified; otherwise null if there are no query results.</returns>
		/// <param name="expression">An <see cref="T:System.Xml.XPath.XPathExpression" /> object containing the compiled XPath query.</param>
		/// <exception cref="T:System.ArgumentException">An error was encountered in the XPath query or the return type of the XPath expression is not a node.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath query is not valid.</exception>
		// Token: 0x060019AD RID: 6573 RVA: 0x00091990 File Offset: 0x0008FB90
		public virtual XPathNavigator SelectSingleNode(XPathExpression expression)
		{
			XPathNodeIterator xpathNodeIterator = this.Select(expression);
			if (xpathNodeIterator.MoveNext())
			{
				return xpathNodeIterator.Current;
			}
			return null;
		}

		/// <summary>Selects a node set, using the specified XPath expression.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> pointing to the selected node set.</returns>
		/// <param name="xpath">A <see cref="T:System.String" /> representing an XPath expression.</param>
		/// <exception cref="T:System.ArgumentException">The XPath expression contains an error or its return type is not a node set.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x060019AE RID: 6574 RVA: 0x000919B5 File Offset: 0x0008FBB5
		public virtual XPathNodeIterator Select(string xpath)
		{
			return this.Select(XPathExpression.Compile(xpath));
		}

		/// <summary>Selects a node set using the specified XPath expression with the <see cref="T:System.Xml.IXmlNamespaceResolver" /> object specified to resolve namespace prefixes.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> that points to the selected node set.</returns>
		/// <param name="xpath">A <see cref="T:System.String" /> representing an XPath expression.</param>
		/// <param name="resolver">The <see cref="T:System.Xml.IXmlNamespaceResolver" /> object used to resolve namespace prefixes.</param>
		/// <exception cref="T:System.ArgumentException">The XPath expression contains an error or its return type is not a node set.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x060019AF RID: 6575 RVA: 0x000919C3 File Offset: 0x0008FBC3
		public virtual XPathNodeIterator Select(string xpath, IXmlNamespaceResolver resolver)
		{
			return this.Select(XPathExpression.Compile(xpath, resolver));
		}

		/// <summary>Selects a node set using the specified <see cref="T:System.Xml.XPath.XPathExpression" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> that points to the selected node set.</returns>
		/// <param name="expr">An <see cref="T:System.Xml.XPath.XPathExpression" /> object containing the compiled XPath query.</param>
		/// <exception cref="T:System.ArgumentException">The XPath expression contains an error or its return type is not a node set.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x060019B0 RID: 6576 RVA: 0x000919D2 File Offset: 0x0008FBD2
		public virtual XPathNodeIterator Select(XPathExpression expr)
		{
			XPathNodeIterator xpathNodeIterator = this.Evaluate(expr) as XPathNodeIterator;
			if (xpathNodeIterator == null)
			{
				throw XPathException.Create("Expression must evaluate to a node-set.");
			}
			return xpathNodeIterator;
		}

		/// <summary>Evaluates the specified XPath expression and returns the typed result.</summary>
		/// <returns>The result of the expression (Boolean, number, string, or node set). This maps to <see cref="T:System.Boolean" />, <see cref="T:System.Double" />, <see cref="T:System.String" />, or <see cref="T:System.Xml.XPath.XPathNodeIterator" /> objects respectively.</returns>
		/// <param name="xpath">A string representing an XPath expression that can be evaluated.</param>
		/// <exception cref="T:System.ArgumentException">The return type of the XPath expression is a node set.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x060019B1 RID: 6577 RVA: 0x000919EE File Offset: 0x0008FBEE
		public virtual object Evaluate(string xpath)
		{
			return this.Evaluate(XPathExpression.Compile(xpath), null);
		}

		/// <summary>Evaluates the specified XPath expression and returns the typed result, using the <see cref="T:System.Xml.IXmlNamespaceResolver" /> object specified to resolve namespace prefixes in the XPath expression.</summary>
		/// <returns>The result of the expression (Boolean, number, string, or node set). This maps to <see cref="T:System.Boolean" />, <see cref="T:System.Double" />, <see cref="T:System.String" />, or <see cref="T:System.Xml.XPath.XPathNodeIterator" /> objects respectively.</returns>
		/// <param name="xpath">A string representing an XPath expression that can be evaluated.</param>
		/// <param name="resolver">The <see cref="T:System.Xml.IXmlNamespaceResolver" /> object used to resolve namespace prefixes in the XPath expression.</param>
		/// <exception cref="T:System.ArgumentException">The return type of the XPath expression is a node set.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x060019B2 RID: 6578 RVA: 0x000919FD File Offset: 0x0008FBFD
		public virtual object Evaluate(string xpath, IXmlNamespaceResolver resolver)
		{
			return this.Evaluate(XPathExpression.Compile(xpath, resolver));
		}

		/// <summary>Evaluates the <see cref="T:System.Xml.XPath.XPathExpression" /> and returns the typed result.</summary>
		/// <returns>The result of the expression (Boolean, number, string, or node set). This maps to <see cref="T:System.Boolean" />, <see cref="T:System.Double" />, <see cref="T:System.String" />, or <see cref="T:System.Xml.XPath.XPathNodeIterator" /> objects respectively.</returns>
		/// <param name="expr">An <see cref="T:System.Xml.XPath.XPathExpression" /> that can be evaluated.</param>
		/// <exception cref="T:System.ArgumentException">The return type of the XPath expression is a node set.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x060019B3 RID: 6579 RVA: 0x00091A0C File Offset: 0x0008FC0C
		public virtual object Evaluate(XPathExpression expr)
		{
			return this.Evaluate(expr, null);
		}

		/// <summary>Uses the supplied context to evaluate the <see cref="T:System.Xml.XPath.XPathExpression" />, and returns the typed result.</summary>
		/// <returns>The result of the expression (Boolean, number, string, or node set). This maps to <see cref="T:System.Boolean" />, <see cref="T:System.Double" />, <see cref="T:System.String" />, or <see cref="T:System.Xml.XPath.XPathNodeIterator" /> objects respectively.</returns>
		/// <param name="expr">An <see cref="T:System.Xml.XPath.XPathExpression" /> that can be evaluated.</param>
		/// <param name="context">An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> that points to the selected node set that the evaluation is to be performed on.</param>
		/// <exception cref="T:System.ArgumentException">The return type of the XPath expression is a node set.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x060019B4 RID: 6580 RVA: 0x00091A18 File Offset: 0x0008FC18
		public virtual object Evaluate(XPathExpression expr, XPathNodeIterator context)
		{
			CompiledXpathExpr compiledXpathExpr = expr as CompiledXpathExpr;
			if (compiledXpathExpr == null)
			{
				throw XPathException.Create("This is an invalid object. Only objects returned from Compile() can be passed as input.");
			}
			Query query = Query.Clone(compiledXpathExpr.QueryTree);
			query.Reset();
			if (context == null)
			{
				context = new XPathSingletonIterator(this.Clone(), true);
			}
			object obj = query.Evaluate(context);
			if (obj is XPathNodeIterator)
			{
				return new XPathSelectionIterator(context.Current, query);
			}
			return obj;
		}

		/// <summary>Determines whether the current node matches the specified <see cref="T:System.Xml.XPath.XPathExpression" />.</summary>
		/// <returns>true if the current node matches the <see cref="T:System.Xml.XPath.XPathExpression" />; otherwise, false.</returns>
		/// <param name="expr">An <see cref="T:System.Xml.XPath.XPathExpression" /> object containing the compiled XPath expression.</param>
		/// <exception cref="T:System.ArgumentException">The XPath expression cannot be evaluated.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x060019B5 RID: 6581 RVA: 0x00091A7C File Offset: 0x0008FC7C
		public virtual bool Matches(XPathExpression expr)
		{
			CompiledXpathExpr compiledXpathExpr = expr as CompiledXpathExpr;
			if (compiledXpathExpr == null)
			{
				throw XPathException.Create("This is an invalid object. Only objects returned from Compile() can be passed as input.");
			}
			Query query = Query.Clone(compiledXpathExpr.QueryTree);
			bool flag;
			try
			{
				flag = query.MatchNode(this) != null;
			}
			catch (XPathException)
			{
				throw XPathException.Create("'{0}' is an invalid XSLT pattern.", compiledXpathExpr.Expression);
			}
			return flag;
		}

		/// <summary>Determines whether the current node matches the specified XPath expression.</summary>
		/// <returns>true if the current node matches the specified XPath expression; otherwise, false.</returns>
		/// <param name="xpath">The XPath expression.</param>
		/// <exception cref="T:System.ArgumentException">The XPath expression cannot be evaluated.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x060019B6 RID: 6582 RVA: 0x00091ADC File Offset: 0x0008FCDC
		public virtual bool Matches(string xpath)
		{
			return this.Matches(XPathNavigator.CompileMatchPattern(xpath));
		}

		/// <summary>Selects all the child nodes of the current node that have the matching <see cref="T:System.Xml.XPath.XPathNodeType" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> that contains the selected nodes.</returns>
		/// <param name="type">The <see cref="T:System.Xml.XPath.XPathNodeType" /> of the child nodes.</param>
		// Token: 0x060019B7 RID: 6583 RVA: 0x00091AEA File Offset: 0x0008FCEA
		public virtual XPathNodeIterator SelectChildren(XPathNodeType type)
		{
			return new XPathChildIterator(this.Clone(), type);
		}

		/// <summary>Selects all the child nodes of the current node that have the local name and namespace URI specified.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> that contains the selected nodes.</returns>
		/// <param name="name">The local name of the child nodes. </param>
		/// <param name="namespaceURI">The namespace URI of the child nodes. </param>
		/// <exception cref="T:System.ArgumentNullException">null cannot be passed as a parameter.</exception>
		// Token: 0x060019B8 RID: 6584 RVA: 0x00091AF8 File Offset: 0x0008FCF8
		public virtual XPathNodeIterator SelectChildren(string name, string namespaceURI)
		{
			return new XPathChildIterator(this.Clone(), name, namespaceURI);
		}

		/// <summary>Selects all the ancestor nodes of the current node that have a matching <see cref="T:System.Xml.XPath.XPathNodeType" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> that contains the selected nodes. The returned nodes are in reverse document order.</returns>
		/// <param name="type">The <see cref="T:System.Xml.XPath.XPathNodeType" /> of the ancestor nodes.</param>
		/// <param name="matchSelf">To include the context node in the selection, true; otherwise, false.</param>
		// Token: 0x060019B9 RID: 6585 RVA: 0x00091B07 File Offset: 0x0008FD07
		public virtual XPathNodeIterator SelectAncestors(XPathNodeType type, bool matchSelf)
		{
			return new XPathAncestorIterator(this.Clone(), type, matchSelf);
		}

		/// <summary>Selects all the ancestor nodes of the current node that have the specified local name and namespace URI.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> that contains the selected nodes. The returned nodes are in reverse document order.</returns>
		/// <param name="name">The local name of the ancestor nodes.</param>
		/// <param name="namespaceURI">The namespace URI of the ancestor nodes.</param>
		/// <param name="matchSelf">To include the context node in the selection, true; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">null cannot be passed as a parameter.</exception>
		// Token: 0x060019BA RID: 6586 RVA: 0x00091B16 File Offset: 0x0008FD16
		public virtual XPathNodeIterator SelectAncestors(string name, string namespaceURI, bool matchSelf)
		{
			return new XPathAncestorIterator(this.Clone(), name, namespaceURI, matchSelf);
		}

		/// <summary>Selects all the descendant nodes of the current node that have a matching <see cref="T:System.Xml.XPath.XPathNodeType" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> that contains the selected nodes.</returns>
		/// <param name="type">The <see cref="T:System.Xml.XPath.XPathNodeType" /> of the descendant nodes.</param>
		/// <param name="matchSelf">true to include the context node in the selection; otherwise, false.</param>
		// Token: 0x060019BB RID: 6587 RVA: 0x00091B26 File Offset: 0x0008FD26
		public virtual XPathNodeIterator SelectDescendants(XPathNodeType type, bool matchSelf)
		{
			return new XPathDescendantIterator(this.Clone(), type, matchSelf);
		}

		/// <summary>Selects all the descendant nodes of the current node with the local name and namespace URI specified.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> that contains the selected nodes.</returns>
		/// <param name="name">The local name of the descendant nodes. </param>
		/// <param name="namespaceURI">The namespace URI of the descendant nodes. </param>
		/// <param name="matchSelf">true to include the context node in the selection; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">null cannot be passed as a parameter.</exception>
		// Token: 0x060019BC RID: 6588 RVA: 0x00091B35 File Offset: 0x0008FD35
		public virtual XPathNodeIterator SelectDescendants(string name, string namespaceURI, bool matchSelf)
		{
			return new XPathDescendantIterator(this.Clone(), name, namespaceURI, matchSelf);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Xml.XPath.XPathNavigator" /> can edit the underlying XML data.</summary>
		/// <returns>true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> can edit the underlying XML data; otherwise false.</returns>
		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060019BD RID: 6589 RVA: 0x0000226C File Offset: 0x0000046C
		public virtual bool CanEdit
		{
			get
			{
				return false;
			}
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlWriter" /> object used to create a new child node at the beginning of the list of child nodes of the current node.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object used to create a new child node at the beginning of the list of child nodes of the current node.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current node the <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on does not allow a new child node to be prepended.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x060019BE RID: 6590 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public virtual XmlWriter PrependChild()
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlWriter" /> object used to create one or more new child nodes at the end of the list of child nodes of the current node. </summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object used to create new child nodes at the end of the list of child nodes of the current node.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current node the <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on is not the root node or an element node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x060019BF RID: 6591 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public virtual XmlWriter AppendChild()
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlWriter" /> object used to create a new sibling node after the currently selected node.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object used to create a new sibling node after the currently selected node.</returns>
		/// <exception cref="T:System.InvalidOperationException">The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> does not allow a new sibling node to be inserted after the current node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x060019C0 RID: 6592 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public virtual XmlWriter InsertAfter()
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlWriter" /> object used to create a new sibling node before the currently selected node.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object used to create a new sibling node before the currently selected node.</returns>
		/// <exception cref="T:System.InvalidOperationException">The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> does not allow a new sibling node to be inserted before the current node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x060019C1 RID: 6593 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public virtual XmlWriter InsertBefore()
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlWriter" /> object used to create new attributes on the current element.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object used to create new attributes on the current element.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> is not positioned on an element node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x060019C2 RID: 6594 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public virtual XmlWriter CreateAttributes()
		{
			throw new NotSupportedException();
		}

		/// <summary>Replaces a range of sibling nodes from the current node to the node specified.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object used to specify the replacement range.</returns>
		/// <param name="lastSiblingToReplace">An <see cref="T:System.Xml.XPath.XPathNavigator" /> positioned on the last sibling node in the range to replace.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> specified is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.InvalidOperationException">The last node to replace specified is not a valid sibling node of the current node.</exception>
		// Token: 0x060019C3 RID: 6595 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public virtual XmlWriter ReplaceRange(XPathNavigator lastSiblingToReplace)
		{
			throw new NotSupportedException();
		}

		/// <summary>Replaces the current node with the content of the string specified.</summary>
		/// <param name="newNode">The XML data string for the new node.</param>
		/// <exception cref="T:System.ArgumentNullException">The XML string parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> is not positioned on an element, text, processing instruction, or comment node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML string parameter is not well-formed.</exception>
		// Token: 0x060019C4 RID: 6596 RVA: 0x00091B48 File Offset: 0x0008FD48
		public virtual void ReplaceSelf(string newNode)
		{
			XmlReader xmlReader = this.CreateContextReader(newNode, false);
			this.ReplaceSelf(xmlReader);
		}

		/// <summary>Replaces the current node with the contents of the <see cref="T:System.Xml.XmlReader" /> object specified.</summary>
		/// <param name="newNode">An <see cref="T:System.Xml.XmlReader" /> object positioned on the XML data for the new node.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Xml.XmlReader" /> object is in an error state or closed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XmlReader" /> object parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> is not positioned on an element, text, processing instruction, or comment node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML contents of the <see cref="T:System.Xml.XmlReader" /> object parameter is not well-formed.</exception>
		// Token: 0x060019C5 RID: 6597 RVA: 0x00091B68 File Offset: 0x0008FD68
		public virtual void ReplaceSelf(XmlReader newNode)
		{
			if (newNode == null)
			{
				throw new ArgumentNullException("newNode");
			}
			XPathNodeType nodeType = this.NodeType;
			if (nodeType == XPathNodeType.Root || nodeType == XPathNodeType.Attribute || nodeType == XPathNodeType.Namespace)
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current position of the navigator."));
			}
			XmlWriter xmlWriter = this.ReplaceRange(this);
			this.BuildSubtree(newNode, xmlWriter);
			xmlWriter.Close();
		}

		/// <summary>Replaces the current node with the contents of the <see cref="T:System.Xml.XPath.XPathNavigator" /> object specified.</summary>
		/// <param name="newNode">An <see cref="T:System.Xml.XPath.XPathNavigator" /> object positioned on the new node.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> object parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> is not positioned on an element, text, processing instruction, or comment node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML contents of the <see cref="T:System.Xml.XPath.XPathNavigator" /> object parameter is not well-formed.</exception>
		// Token: 0x060019C6 RID: 6598 RVA: 0x00091BBC File Offset: 0x0008FDBC
		public virtual void ReplaceSelf(XPathNavigator newNode)
		{
			if (newNode == null)
			{
				throw new ArgumentNullException("newNode");
			}
			XmlReader xmlReader = newNode.CreateReader();
			this.ReplaceSelf(xmlReader);
		}

		/// <summary>Gets or sets the markup representing the opening and closing tags of the current node and its child nodes.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the markup representing the opening and closing tags of the current node and its child nodes.</returns>
		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060019C7 RID: 6599 RVA: 0x00091BE8 File Offset: 0x0008FDE8
		// (set) Token: 0x060019C8 RID: 6600 RVA: 0x00091CD4 File Offset: 0x0008FED4
		public virtual string OuterXml
		{
			get
			{
				if (this.NodeType == XPathNodeType.Attribute)
				{
					return this.Name + "=\"" + this.Value + "\"";
				}
				if (this.NodeType != XPathNodeType.Namespace)
				{
					StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
					XmlWriter xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings
					{
						Indent = true,
						OmitXmlDeclaration = true,
						ConformanceLevel = ConformanceLevel.Auto
					});
					try
					{
						xmlWriter.WriteNode(this, true);
					}
					finally
					{
						xmlWriter.Close();
					}
					return stringWriter.ToString();
				}
				if (this.LocalName.Length == 0)
				{
					return "xmlns=\"" + this.Value + "\"";
				}
				return string.Concat(new string[] { "xmlns:", this.LocalName, "=\"", this.Value, "\"" });
			}
			set
			{
				this.ReplaceSelf(value);
			}
		}

		/// <summary>Gets or sets the markup representing the child nodes of the current node.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the markup of the child nodes of the current node.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Xml.XPath.XPathNavigator.InnerXml" /> property cannot be set.</exception>
		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060019C9 RID: 6601 RVA: 0x00091CE0 File Offset: 0x0008FEE0
		// (set) Token: 0x060019CA RID: 6602 RVA: 0x00091D7C File Offset: 0x0008FF7C
		public virtual string InnerXml
		{
			get
			{
				XPathNodeType nodeType = this.NodeType;
				if (nodeType <= XPathNodeType.Element)
				{
					StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
					XmlWriter xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings
					{
						Indent = true,
						OmitXmlDeclaration = true,
						ConformanceLevel = ConformanceLevel.Auto
					});
					try
					{
						if (this.MoveToFirstChild())
						{
							do
							{
								xmlWriter.WriteNode(this, true);
							}
							while (this.MoveToNext());
							this.MoveToParent();
						}
					}
					finally
					{
						xmlWriter.Close();
					}
					return stringWriter.ToString();
				}
				if (nodeType - XPathNodeType.Attribute > 1)
				{
					return string.Empty;
				}
				return this.Value;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				XPathNodeType nodeType = this.NodeType;
				if (nodeType > XPathNodeType.Element)
				{
					if (nodeType != XPathNodeType.Attribute)
					{
						throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current position of the navigator."));
					}
					this.SetValue(value);
					return;
				}
				else
				{
					XPathNavigator xpathNavigator = this.CreateNavigator();
					while (xpathNavigator.MoveToFirstChild())
					{
						xpathNavigator.DeleteSelf();
					}
					if (value.Length != 0)
					{
						xpathNavigator.AppendChild(value);
						return;
					}
					return;
				}
			}
		}

		/// <summary>Creates a new child node at the end of the list of child nodes of the current node using the XML data string specified.</summary>
		/// <param name="newChild">The XML data string for the new child node.</param>
		/// <exception cref="T:System.ArgumentNullException">The XML data string parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node the <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on is not the root node or an element node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML data string parameter is not well-formed.</exception>
		// Token: 0x060019CB RID: 6603 RVA: 0x00091DE8 File Offset: 0x0008FFE8
		public virtual void AppendChild(string newChild)
		{
			XmlReader xmlReader = this.CreateContextReader(newChild, true);
			this.AppendChild(xmlReader);
		}

		/// <summary>Creates a new child node at the end of the list of child nodes of the current node using the XML contents of the <see cref="T:System.Xml.XmlReader" /> object specified.</summary>
		/// <param name="newChild">An <see cref="T:System.Xml.XmlReader" /> object positioned on the XML data for the new child node.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Xml.XmlReader" /> object is in an error state or closed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XmlReader" /> object parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node the <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on is not the root node or an element node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML contents of the <see cref="T:System.Xml.XmlReader" /> object parameter is not well-formed.</exception>
		// Token: 0x060019CC RID: 6604 RVA: 0x00091E08 File Offset: 0x00090008
		public virtual void AppendChild(XmlReader newChild)
		{
			if (newChild == null)
			{
				throw new ArgumentNullException("newChild");
			}
			XmlWriter xmlWriter = this.AppendChild();
			this.BuildSubtree(newChild, xmlWriter);
			xmlWriter.Close();
		}

		/// <summary>Creates a new child node at the end of the list of child nodes of the current node using the nodes in the <see cref="T:System.Xml.XPath.XPathNavigator" /> specified.</summary>
		/// <param name="newChild">An <see cref="T:System.Xml.XPath.XPathNavigator" /> object positioned on the node to add as the new child node.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> object parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node the <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on is not the root node or an element node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x060019CD RID: 6605 RVA: 0x00091E38 File Offset: 0x00090038
		public virtual void AppendChild(XPathNavigator newChild)
		{
			if (newChild == null)
			{
				throw new ArgumentNullException("newChild");
			}
			if (!this.IsValidChildType(newChild.NodeType))
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current position of the navigator."));
			}
			XmlReader xmlReader = newChild.CreateReader();
			this.AppendChild(xmlReader);
		}

		/// <summary>Creates a new child node at the beginning of the list of child nodes of the current node using the XML string specified.</summary>
		/// <param name="newChild">The XML data string for the new child node.</param>
		/// <exception cref="T:System.ArgumentNullException">The XML string parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node the <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on does not allow a new child node to be prepended.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML string parameter is not well-formed.</exception>
		// Token: 0x060019CE RID: 6606 RVA: 0x00091E80 File Offset: 0x00090080
		public virtual void PrependChild(string newChild)
		{
			XmlReader xmlReader = this.CreateContextReader(newChild, true);
			this.PrependChild(xmlReader);
		}

		/// <summary>Creates a new child node at the beginning of the list of child nodes of the current node using the XML contents of the <see cref="T:System.Xml.XmlReader" /> object specified.</summary>
		/// <param name="newChild">An <see cref="T:System.Xml.XmlReader" /> object positioned on the XML data for the new child node.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Xml.XmlReader" /> object is in an error state or closed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XmlReader" /> object parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node the <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on does not allow a new child node to be prepended.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML contents of the <see cref="T:System.Xml.XmlReader" /> object parameter is not well-formed.</exception>
		// Token: 0x060019CF RID: 6607 RVA: 0x00091EA0 File Offset: 0x000900A0
		public virtual void PrependChild(XmlReader newChild)
		{
			if (newChild == null)
			{
				throw new ArgumentNullException("newChild");
			}
			XmlWriter xmlWriter = this.PrependChild();
			this.BuildSubtree(newChild, xmlWriter);
			xmlWriter.Close();
		}

		/// <summary>Creates a new child node at the beginning of the list of child nodes of the current node using the nodes in the <see cref="T:System.Xml.XPath.XPathNavigator" /> object specified.</summary>
		/// <param name="newChild">An <see cref="T:System.Xml.XPath.XPathNavigator" /> object positioned on the node to add as the new child node.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> object parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node the <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on does not allow a new child node to be prepended.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x060019D0 RID: 6608 RVA: 0x00091ED0 File Offset: 0x000900D0
		public virtual void PrependChild(XPathNavigator newChild)
		{
			if (newChild == null)
			{
				throw new ArgumentNullException("newChild");
			}
			if (!this.IsValidChildType(newChild.NodeType))
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current position of the navigator."));
			}
			XmlReader xmlReader = newChild.CreateReader();
			this.PrependChild(xmlReader);
		}

		/// <summary>Creates a new sibling node before the currently selected node using the XML string specified.</summary>
		/// <param name="newSibling">The XML data string for the new sibling node.</param>
		/// <exception cref="T:System.ArgumentNullException">The XML string parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> does not allow a new sibling node to be inserted before the current node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML string parameter is not well-formed.</exception>
		// Token: 0x060019D1 RID: 6609 RVA: 0x00091F18 File Offset: 0x00090118
		public virtual void InsertBefore(string newSibling)
		{
			XmlReader xmlReader = this.CreateContextReader(newSibling, false);
			this.InsertBefore(xmlReader);
		}

		/// <summary>Creates a new sibling node before the currently selected node using the XML contents of the <see cref="T:System.Xml.XmlReader" /> object specified.</summary>
		/// <param name="newSibling">An <see cref="T:System.Xml.XmlReader" /> object positioned on the XML data for the new sibling node.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Xml.XmlReader" /> object is in an error state or closed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XmlReader" /> object parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> does not allow a new sibling node to be inserted before the current node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML contents of the <see cref="T:System.Xml.XmlReader" /> object parameter is not well-formed.</exception>
		// Token: 0x060019D2 RID: 6610 RVA: 0x00091F38 File Offset: 0x00090138
		public virtual void InsertBefore(XmlReader newSibling)
		{
			if (newSibling == null)
			{
				throw new ArgumentNullException("newSibling");
			}
			XmlWriter xmlWriter = this.InsertBefore();
			this.BuildSubtree(newSibling, xmlWriter);
			xmlWriter.Close();
		}

		/// <summary>Creates a new sibling node before the currently selected node using the nodes in the <see cref="T:System.Xml.XPath.XPathNavigator" /> specified.</summary>
		/// <param name="newSibling">An <see cref="T:System.Xml.XPath.XPathNavigator" /> object positioned on the node to add as the new sibling node.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> object parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> does not allow a new sibling node to be inserted before the current node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x060019D3 RID: 6611 RVA: 0x00091F68 File Offset: 0x00090168
		public virtual void InsertBefore(XPathNavigator newSibling)
		{
			if (newSibling == null)
			{
				throw new ArgumentNullException("newSibling");
			}
			if (!this.IsValidSiblingType(newSibling.NodeType))
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current position of the navigator."));
			}
			XmlReader xmlReader = newSibling.CreateReader();
			this.InsertBefore(xmlReader);
		}

		/// <summary>Creates a new sibling node after the currently selected node using the XML string specified.</summary>
		/// <param name="newSibling">The XML data string for the new sibling node.</param>
		/// <exception cref="T:System.ArgumentNullException">The XML string parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> does not allow a new sibling node to be inserted after the current node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML string parameter is not well-formed.</exception>
		// Token: 0x060019D4 RID: 6612 RVA: 0x00091FB0 File Offset: 0x000901B0
		public virtual void InsertAfter(string newSibling)
		{
			XmlReader xmlReader = this.CreateContextReader(newSibling, false);
			this.InsertAfter(xmlReader);
		}

		/// <summary>Creates a new sibling node after the currently selected node using the XML contents of the <see cref="T:System.Xml.XmlReader" /> object specified.</summary>
		/// <param name="newSibling">An <see cref="T:System.Xml.XmlReader" /> object positioned on the XML data for the new sibling node.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Xml.XmlReader" /> object is in an error state or closed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XmlReader" /> object parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> does not allow a new sibling node to be inserted after the current node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML contents of the <see cref="T:System.Xml.XmlReader" /> object parameter is not well-formed.</exception>
		// Token: 0x060019D5 RID: 6613 RVA: 0x00091FD0 File Offset: 0x000901D0
		public virtual void InsertAfter(XmlReader newSibling)
		{
			if (newSibling == null)
			{
				throw new ArgumentNullException("newSibling");
			}
			XmlWriter xmlWriter = this.InsertAfter();
			this.BuildSubtree(newSibling, xmlWriter);
			xmlWriter.Close();
		}

		/// <summary>Creates a new sibling node after the currently selected node using the nodes in the <see cref="T:System.Xml.XPath.XPathNavigator" /> object specified.</summary>
		/// <param name="newSibling">An <see cref="T:System.Xml.XPath.XPathNavigator" /> object positioned on the node to add as the new sibling node.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> object parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> does not allow a new sibling node to be inserted after the current node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x060019D6 RID: 6614 RVA: 0x00092000 File Offset: 0x00090200
		public virtual void InsertAfter(XPathNavigator newSibling)
		{
			if (newSibling == null)
			{
				throw new ArgumentNullException("newSibling");
			}
			if (!this.IsValidSiblingType(newSibling.NodeType))
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current position of the navigator."));
			}
			XmlReader xmlReader = newSibling.CreateReader();
			this.InsertAfter(xmlReader);
		}

		/// <summary>Deletes a range of sibling nodes from the current node to the node specified.</summary>
		/// <param name="lastSiblingToDelete">An <see cref="T:System.Xml.XPath.XPathNavigator" /> positioned on the last sibling node in the range to delete.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> specified is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.InvalidOperationException">The last node to delete specified is not a valid sibling node of the current node.</exception>
		// Token: 0x060019D7 RID: 6615 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public virtual void DeleteRange(XPathNavigator lastSiblingToDelete)
		{
			throw new NotSupportedException();
		}

		/// <summary>Deletes the current node and its child nodes.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on a node that cannot be deleted such as the root node or a namespace node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x060019D8 RID: 6616 RVA: 0x00092047 File Offset: 0x00090247
		public virtual void DeleteSelf()
		{
			this.DeleteRange(this);
		}

		/// <summary>Creates a new child element at the beginning of the list of child nodes of the current node using the namespace prefix, local name, and namespace URI specified with the value specified.</summary>
		/// <param name="prefix">The namespace prefix of the new child element (if any).</param>
		/// <param name="localName">The local name of the new child element (if any).</param>
		/// <param name="namespaceURI">The namespace URI of the new child element (if any). <see cref="F:System.String.Empty" /> and null are equivalent.</param>
		/// <param name="value">The value of the new child element. If <see cref="F:System.String.Empty" /> or null are passed, an empty element is created.</param>
		/// <exception cref="T:System.InvalidOperationException">The current node the <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on does not allow a new child node to be prepended.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x060019D9 RID: 6617 RVA: 0x00092050 File Offset: 0x00090250
		public virtual void PrependChildElement(string prefix, string localName, string namespaceURI, string value)
		{
			XmlWriter xmlWriter = this.PrependChild();
			xmlWriter.WriteStartElement(prefix, localName, namespaceURI);
			if (value != null)
			{
				xmlWriter.WriteString(value);
			}
			xmlWriter.WriteEndElement();
			xmlWriter.Close();
		}

		/// <summary>Creates a new child element node at the end of the list of child nodes of the current node using the namespace prefix, local name and namespace URI specified with the value specified.</summary>
		/// <param name="prefix">The namespace prefix of the new child element node (if any).</param>
		/// <param name="localName">The local name of the new child element node (if any).</param>
		/// <param name="namespaceURI">The namespace URI of the new child element node (if any). <see cref="F:System.String.Empty" /> and null are equivalent.</param>
		/// <param name="value">The value of the new child element node. If <see cref="F:System.String.Empty" /> or null are passed, an empty element is created.</param>
		/// <exception cref="T:System.InvalidOperationException">The current node the <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on is not the root node or an element node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x060019DA RID: 6618 RVA: 0x00092088 File Offset: 0x00090288
		public virtual void AppendChildElement(string prefix, string localName, string namespaceURI, string value)
		{
			XmlWriter xmlWriter = this.AppendChild();
			xmlWriter.WriteStartElement(prefix, localName, namespaceURI);
			if (value != null)
			{
				xmlWriter.WriteString(value);
			}
			xmlWriter.WriteEndElement();
			xmlWriter.Close();
		}

		/// <summary>Creates a new sibling element before the current node using the namespace prefix, local name, and namespace URI specified, with the value specified.</summary>
		/// <param name="prefix">The namespace prefix of the new child element (if any).</param>
		/// <param name="localName">The local name of the new child element (if any).</param>
		/// <param name="namespaceURI">The namespace URI of the new child element (if any). <see cref="F:System.String.Empty" /> and null are equivalent.</param>
		/// <param name="value">The value of the new child element. If <see cref="F:System.String.Empty" /> or null are passed, an empty element is created.</param>
		/// <exception cref="T:System.InvalidOperationException">The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> does not allow a new sibling node to be inserted before the current node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x060019DB RID: 6619 RVA: 0x000920C0 File Offset: 0x000902C0
		public virtual void InsertElementBefore(string prefix, string localName, string namespaceURI, string value)
		{
			XmlWriter xmlWriter = this.InsertBefore();
			xmlWriter.WriteStartElement(prefix, localName, namespaceURI);
			if (value != null)
			{
				xmlWriter.WriteString(value);
			}
			xmlWriter.WriteEndElement();
			xmlWriter.Close();
		}

		/// <summary>Creates a new sibling element after the current node using the namespace prefix, local name and namespace URI specified, with the value specified.</summary>
		/// <param name="prefix">The namespace prefix of the new child element (if any).</param>
		/// <param name="localName">The local name of the new child element (if any).</param>
		/// <param name="namespaceURI">The namespace URI of the new child element (if any). <see cref="F:System.String.Empty" /> and null are equivalent.</param>
		/// <param name="value">The value of the new child element. If <see cref="F:System.String.Empty" /> or null are passed, an empty element is created.</param>
		/// <exception cref="T:System.InvalidOperationException">The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> does not allow a new sibling node to be inserted after the current node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x060019DC RID: 6620 RVA: 0x000920F8 File Offset: 0x000902F8
		public virtual void InsertElementAfter(string prefix, string localName, string namespaceURI, string value)
		{
			XmlWriter xmlWriter = this.InsertAfter();
			xmlWriter.WriteStartElement(prefix, localName, namespaceURI);
			if (value != null)
			{
				xmlWriter.WriteString(value);
			}
			xmlWriter.WriteEndElement();
			xmlWriter.Close();
		}

		/// <summary>Creates an attribute node on the current element node using the namespace prefix, local name and namespace URI specified with the value specified.</summary>
		/// <param name="prefix">The namespace prefix of the new attribute node (if any).</param>
		/// <param name="localName">The local name of the new attribute node which cannot <see cref="F:System.String.Empty" /> or null.</param>
		/// <param name="namespaceURI">The namespace URI for the new attribute node (if any).</param>
		/// <param name="value">The value of the new attribute node. If <see cref="F:System.String.Empty" /> or null are passed, an empty attribute node is created.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> is not positioned on an element node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x060019DD RID: 6621 RVA: 0x00092130 File Offset: 0x00090330
		public virtual void CreateAttribute(string prefix, string localName, string namespaceURI, string value)
		{
			XmlWriter xmlWriter = this.CreateAttributes();
			xmlWriter.WriteStartAttribute(prefix, localName, namespaceURI);
			if (value != null)
			{
				xmlWriter.WriteString(value);
			}
			xmlWriter.WriteEndAttribute();
			xmlWriter.Close();
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x00092168 File Offset: 0x00090368
		internal bool MoveToPrevious(string localName, string namespaceURI)
		{
			XPathNavigator xpathNavigator = this.Clone();
			localName = ((localName != null) ? this.NameTable.Get(localName) : null);
			while (this.MoveToPrevious())
			{
				if (this.NodeType == XPathNodeType.Element && localName == this.LocalName && namespaceURI == this.NamespaceURI)
				{
					return true;
				}
			}
			this.MoveTo(xpathNavigator);
			return false;
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x000921C8 File Offset: 0x000903C8
		internal bool MoveToPrevious(XPathNodeType type)
		{
			XPathNavigator xpathNavigator = this.Clone();
			int contentKindMask = XPathNavigator.GetContentKindMask(type);
			while (this.MoveToPrevious())
			{
				if (((1 << (int)this.NodeType) & contentKindMask) != 0)
				{
					return true;
				}
			}
			this.MoveTo(xpathNavigator);
			return false;
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x00092208 File Offset: 0x00090408
		internal bool MoveToNonDescendant()
		{
			if (this.NodeType == XPathNodeType.Root)
			{
				return false;
			}
			if (this.MoveToNext())
			{
				return true;
			}
			XPathNavigator xpathNavigator = this.Clone();
			if (!this.MoveToParent())
			{
				return false;
			}
			XPathNodeType nodeType = xpathNavigator.NodeType;
			if (nodeType - XPathNodeType.Attribute <= 1 && this.MoveToFirstChild())
			{
				return true;
			}
			while (!this.MoveToNext())
			{
				if (!this.MoveToParent())
				{
					this.MoveTo(xpathNavigator);
					return false;
				}
			}
			return true;
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060019E1 RID: 6625 RVA: 0x0009226C File Offset: 0x0009046C
		internal uint IndexInParent
		{
			get
			{
				XPathNavigator xpathNavigator = this.Clone();
				uint num = 0U;
				XPathNodeType nodeType = this.NodeType;
				if (nodeType != XPathNodeType.Attribute)
				{
					if (nodeType != XPathNodeType.Namespace)
					{
						while (xpathNavigator.MoveToNext())
						{
							num += 1U;
						}
					}
					else
					{
						while (xpathNavigator.MoveToNextNamespace())
						{
							num += 1U;
						}
					}
				}
				else
				{
					while (xpathNavigator.MoveToNextAttribute())
					{
						num += 1U;
					}
				}
				return num;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060019E2 RID: 6626 RVA: 0x000922BC File Offset: 0x000904BC
		internal virtual string UniqueId
		{
			get
			{
				XPathNavigator xpathNavigator = this.Clone();
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(XPathNavigator.NodeTypeLetter[(int)this.NodeType]);
				for (;;)
				{
					uint num = xpathNavigator.IndexInParent;
					if (!xpathNavigator.MoveToParent())
					{
						break;
					}
					if (num <= 31U)
					{
						stringBuilder.Append(XPathNavigator.UniqueIdTbl[(int)num]);
					}
					else
					{
						stringBuilder.Append('0');
						do
						{
							stringBuilder.Append(XPathNavigator.UniqueIdTbl[(int)(num & 31U)]);
							num >>= 5;
						}
						while (num != 0U);
						stringBuilder.Append('0');
					}
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x00092340 File Offset: 0x00090540
		private static XPathExpression CompileMatchPattern(string xpath)
		{
			bool flag;
			return new CompiledXpathExpr(new QueryBuilder().BuildPatternQuery(xpath, out flag), xpath, flag);
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x00092364 File Offset: 0x00090564
		private static int GetDepth(XPathNavigator nav)
		{
			int num = 0;
			while (nav.MoveToParent())
			{
				num++;
			}
			return num;
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x00092384 File Offset: 0x00090584
		private XmlNodeOrder CompareSiblings(XPathNavigator n1, XPathNavigator n2)
		{
			int num = 0;
			XPathNodeType xpathNodeType = n1.NodeType;
			if (xpathNodeType != XPathNodeType.Attribute)
			{
				if (xpathNodeType != XPathNodeType.Namespace)
				{
					num += 2;
				}
			}
			else
			{
				num++;
			}
			xpathNodeType = n2.NodeType;
			if (xpathNodeType != XPathNodeType.Attribute)
			{
				if (xpathNodeType == XPathNodeType.Namespace)
				{
					if (num == 0)
					{
						while (n1.MoveToNextNamespace())
						{
							if (n1.IsSamePosition(n2))
							{
								return XmlNodeOrder.Before;
							}
						}
					}
				}
				else
				{
					num -= 2;
					if (num == 0)
					{
						while (n1.MoveToNext())
						{
							if (n1.IsSamePosition(n2))
							{
								return XmlNodeOrder.Before;
							}
						}
					}
				}
			}
			else
			{
				num--;
				if (num == 0)
				{
					while (n1.MoveToNextAttribute())
					{
						if (n1.IsSamePosition(n2))
						{
							return XmlNodeOrder.Before;
						}
					}
				}
			}
			if (num >= 0)
			{
				return XmlNodeOrder.After;
			}
			return XmlNodeOrder.Before;
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x00092418 File Offset: 0x00090618
		internal static XmlNamespaceManager GetNamespaces(IXmlNamespaceResolver resolver)
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(new NameTable());
			foreach (KeyValuePair<string, string> keyValuePair in resolver.GetNamespacesInScope(XmlNamespaceScope.All))
			{
				if (keyValuePair.Key != "xmlns")
				{
					xmlNamespaceManager.AddNamespace(keyValuePair.Key, keyValuePair.Value);
				}
			}
			return xmlNamespaceManager;
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x00092494 File Offset: 0x00090694
		internal static int GetContentKindMask(XPathNodeType type)
		{
			return XPathNavigator.ContentKindMasks[(int)type];
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x0009249D File Offset: 0x0009069D
		internal static int GetKindMask(XPathNodeType type)
		{
			if (type == XPathNodeType.All)
			{
				return int.MaxValue;
			}
			if (type == XPathNodeType.Text)
			{
				return 112;
			}
			return 1 << (int)type;
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x000924B7 File Offset: 0x000906B7
		internal static bool IsText(XPathNodeType type)
		{
			return type - XPathNodeType.Text <= 2;
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x000924C4 File Offset: 0x000906C4
		private bool IsValidChildType(XPathNodeType type)
		{
			XPathNodeType nodeType = this.NodeType;
			if (nodeType != XPathNodeType.Root)
			{
				if (nodeType == XPathNodeType.Element)
				{
					if (type == XPathNodeType.Element || type - XPathNodeType.Text <= 4)
					{
						return true;
					}
				}
			}
			else if (type == XPathNodeType.Element || type - XPathNodeType.SignificantWhitespace <= 3)
			{
				return true;
			}
			return false;
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x000924FC File Offset: 0x000906FC
		private bool IsValidSiblingType(XPathNodeType type)
		{
			XPathNodeType nodeType = this.NodeType;
			return (nodeType == XPathNodeType.Element || nodeType - XPathNodeType.Text <= 4) && (type == XPathNodeType.Element || type - XPathNodeType.Text <= 4);
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x00092527 File Offset: 0x00090727
		private XmlReader CreateReader()
		{
			return XPathNavigatorReader.Create(this);
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x00092530 File Offset: 0x00090730
		private XmlReader CreateContextReader(string xml, bool fromCurrentNode)
		{
			if (xml == null)
			{
				throw new ArgumentNullException("xml");
			}
			XPathNavigator xpathNavigator = this.CreateNavigator();
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(this.NameTable);
			if (!fromCurrentNode)
			{
				xpathNavigator.MoveToParent();
			}
			if (xpathNavigator.MoveToFirstNamespace(XPathNamespaceScope.All))
			{
				do
				{
					xmlNamespaceManager.AddNamespace(xpathNavigator.LocalName, xpathNavigator.Value);
				}
				while (xpathNavigator.MoveToNextNamespace(XPathNamespaceScope.All));
			}
			XmlParserContext xmlParserContext = new XmlParserContext(this.NameTable, xmlNamespaceManager, null, XmlSpace.Default);
			return new XmlTextReader(xml, XmlNodeType.Element, xmlParserContext)
			{
				WhitespaceHandling = WhitespaceHandling.Significant
			};
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x000925AC File Offset: 0x000907AC
		internal void BuildSubtree(XmlReader reader, XmlWriter writer)
		{
			string text = "http://www.w3.org/2000/xmlns/";
			ReadState readState = reader.ReadState;
			if (readState != ReadState.Initial && readState != ReadState.Interactive)
			{
				throw new ArgumentException(Res.GetString("Operation is not valid due to the current state of the object."), "reader");
			}
			int num = 0;
			if (readState == ReadState.Initial)
			{
				if (!reader.Read())
				{
					return;
				}
				num++;
			}
			do
			{
				switch (reader.NodeType)
				{
				case XmlNodeType.Element:
				{
					writer.WriteStartElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
					bool isEmptyElement = reader.IsEmptyElement;
					while (reader.MoveToNextAttribute())
					{
						if (reader.NamespaceURI == text)
						{
							if (reader.Prefix.Length == 0)
							{
								writer.WriteAttributeString("", "xmlns", text, reader.Value);
							}
							else
							{
								writer.WriteAttributeString("xmlns", reader.LocalName, text, reader.Value);
							}
						}
						else
						{
							writer.WriteStartAttribute(reader.Prefix, reader.LocalName, reader.NamespaceURI);
							writer.WriteString(reader.Value);
							writer.WriteEndAttribute();
						}
					}
					reader.MoveToElement();
					if (isEmptyElement)
					{
						writer.WriteEndElement();
					}
					else
					{
						num++;
					}
					break;
				}
				case XmlNodeType.Attribute:
					if (reader.NamespaceURI == text)
					{
						if (reader.Prefix.Length == 0)
						{
							writer.WriteAttributeString("", "xmlns", text, reader.Value);
						}
						else
						{
							writer.WriteAttributeString("xmlns", reader.LocalName, text, reader.Value);
						}
					}
					else
					{
						writer.WriteStartAttribute(reader.Prefix, reader.LocalName, reader.NamespaceURI);
						writer.WriteString(reader.Value);
						writer.WriteEndAttribute();
					}
					break;
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
					writer.WriteString(reader.Value);
					break;
				case XmlNodeType.EntityReference:
					reader.ResolveEntity();
					break;
				case XmlNodeType.ProcessingInstruction:
					writer.WriteProcessingInstruction(reader.LocalName, reader.Value);
					break;
				case XmlNodeType.Comment:
					writer.WriteComment(reader.Value);
					break;
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					writer.WriteString(reader.Value);
					break;
				case XmlNodeType.EndElement:
					writer.WriteFullEndElement();
					num--;
					break;
				}
			}
			while (reader.Read() && num > 0);
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060019EF RID: 6639 RVA: 0x000927EA File Offset: 0x000909EA
		private object debuggerDisplayProxy
		{
			get
			{
				return new XPathNavigator.DebuggerDisplayProxy(this);
			}
		}

		// Token: 0x04001549 RID: 5449
		internal static readonly XPathNavigatorKeyComparer comparer = new XPathNavigatorKeyComparer();

		// Token: 0x0400154A RID: 5450
		internal static readonly char[] NodeTypeLetter = new char[] { 'R', 'E', 'A', 'N', 'T', 'S', 'W', 'P', 'C', 'X' };

		// Token: 0x0400154B RID: 5451
		internal static readonly char[] UniqueIdTbl = new char[]
		{
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
			'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
			'U', 'V', 'W', 'X', 'Y', 'Z', '1', '2', '3', '4',
			'5', '6'
		};

		// Token: 0x0400154C RID: 5452
		internal const int AllMask = 2147483647;

		// Token: 0x0400154D RID: 5453
		internal const int NoAttrNmspMask = 2147483635;

		// Token: 0x0400154E RID: 5454
		internal const int TextMask = 112;

		// Token: 0x0400154F RID: 5455
		internal static readonly int[] ContentKindMasks = new int[] { 1, 2, 0, 0, 112, 32, 64, 128, 256, 2147483635 };

		// Token: 0x020002BA RID: 698
		private class CheckValidityHelper
		{
			// Token: 0x060019F2 RID: 6642 RVA: 0x0009285C File Offset: 0x00090A5C
			internal CheckValidityHelper(ValidationEventHandler nextEventHandler, XPathNavigatorReader reader)
			{
				this.isValid = true;
				this.nextEventHandler = nextEventHandler;
				this.reader = reader;
			}

			// Token: 0x060019F3 RID: 6643 RVA: 0x0009287C File Offset: 0x00090A7C
			internal void ValidationCallback(object sender, ValidationEventArgs args)
			{
				if (args.Severity == XmlSeverityType.Error)
				{
					this.isValid = false;
				}
				XmlSchemaValidationException ex = args.Exception as XmlSchemaValidationException;
				if (ex != null && this.reader != null)
				{
					ex.SetSourceObject(this.reader.UnderlyingObject);
				}
				if (this.nextEventHandler != null)
				{
					this.nextEventHandler(sender, args);
					return;
				}
				if (ex != null && args.Severity == XmlSeverityType.Error)
				{
					throw ex;
				}
			}

			// Token: 0x170004E3 RID: 1251
			// (get) Token: 0x060019F4 RID: 6644 RVA: 0x000928E3 File Offset: 0x00090AE3
			internal bool IsValid
			{
				get
				{
					return this.isValid;
				}
			}

			// Token: 0x04001550 RID: 5456
			private bool isValid;

			// Token: 0x04001551 RID: 5457
			private ValidationEventHandler nextEventHandler;

			// Token: 0x04001552 RID: 5458
			private XPathNavigatorReader reader;
		}

		// Token: 0x020002BB RID: 699
		[DebuggerDisplay("{ToString()}")]
		internal struct DebuggerDisplayProxy
		{
			// Token: 0x060019F5 RID: 6645 RVA: 0x000928EB File Offset: 0x00090AEB
			public DebuggerDisplayProxy(XPathNavigator nav)
			{
				this.nav = nav;
			}

			// Token: 0x060019F6 RID: 6646 RVA: 0x000928F4 File Offset: 0x00090AF4
			public override string ToString()
			{
				string text = this.nav.NodeType.ToString();
				switch (this.nav.NodeType)
				{
				case XPathNodeType.Element:
					text = text + ", Name=\"" + this.nav.Name + "\"";
					break;
				case XPathNodeType.Attribute:
				case XPathNodeType.Namespace:
				case XPathNodeType.ProcessingInstruction:
					text = text + ", Name=\"" + this.nav.Name + "\"";
					text = text + ", Value=\"" + XmlConvert.EscapeValueForDebuggerDisplay(this.nav.Value) + "\"";
					break;
				case XPathNodeType.Text:
				case XPathNodeType.SignificantWhitespace:
				case XPathNodeType.Whitespace:
				case XPathNodeType.Comment:
					text = text + ", Value=\"" + XmlConvert.EscapeValueForDebuggerDisplay(this.nav.Value) + "\"";
					break;
				}
				return text;
			}

			// Token: 0x04001553 RID: 5459
			private XPathNavigator nav;
		}
	}
}
