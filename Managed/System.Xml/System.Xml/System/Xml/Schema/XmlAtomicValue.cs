using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml.XPath;
using Unity;

namespace System.Xml.Schema
{
	/// <summary>Represents the typed value of a validated XML element or attribute. The <see cref="T:System.Xml.Schema.XmlAtomicValue" /> class cannot be inherited.</summary>
	// Token: 0x02000431 RID: 1073
	public sealed class XmlAtomicValue : XPathItem, ICloneable
	{
		// Token: 0x06002A59 RID: 10841 RVA: 0x001037EF File Offset: 0x001019EF
		internal XmlAtomicValue(XmlSchemaType xmlType, bool value)
		{
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlType = xmlType;
			this.clrType = TypeCode.Boolean;
			this.unionVal.boolVal = value;
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x0010381F File Offset: 0x00101A1F
		internal XmlAtomicValue(XmlSchemaType xmlType, DateTime value)
		{
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlType = xmlType;
			this.clrType = TypeCode.DateTime;
			this.unionVal.dtVal = value;
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x00103850 File Offset: 0x00101A50
		internal XmlAtomicValue(XmlSchemaType xmlType, double value)
		{
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlType = xmlType;
			this.clrType = TypeCode.Double;
			this.unionVal.dblVal = value;
		}

		// Token: 0x06002A5C RID: 10844 RVA: 0x00103881 File Offset: 0x00101A81
		internal XmlAtomicValue(XmlSchemaType xmlType, int value)
		{
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlType = xmlType;
			this.clrType = TypeCode.Int32;
			this.unionVal.i32Val = value;
		}

		// Token: 0x06002A5D RID: 10845 RVA: 0x001038B2 File Offset: 0x00101AB2
		internal XmlAtomicValue(XmlSchemaType xmlType, long value)
		{
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlType = xmlType;
			this.clrType = TypeCode.Int64;
			this.unionVal.i64Val = value;
		}

		// Token: 0x06002A5E RID: 10846 RVA: 0x001038E3 File Offset: 0x00101AE3
		internal XmlAtomicValue(XmlSchemaType xmlType, string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlType = xmlType;
			this.objVal = value;
		}

		// Token: 0x06002A5F RID: 10847 RVA: 0x00103918 File Offset: 0x00101B18
		internal XmlAtomicValue(XmlSchemaType xmlType, string value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlType = xmlType;
			this.objVal = value;
			if (nsResolver != null && (this.xmlType.TypeCode == XmlTypeCode.QName || this.xmlType.TypeCode == XmlTypeCode.Notation))
			{
				string prefixFromQName = this.GetPrefixFromQName(value);
				this.nsPrefix = new XmlAtomicValue.NamespacePrefixForQName(prefixFromQName, nsResolver.LookupNamespace(prefixFromQName));
			}
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x001038E3 File Offset: 0x00101AE3
		internal XmlAtomicValue(XmlSchemaType xmlType, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlType = xmlType;
			this.objVal = value;
		}

		// Token: 0x06002A61 RID: 10849 RVA: 0x00103994 File Offset: 0x00101B94
		internal XmlAtomicValue(XmlSchemaType xmlType, object value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlType = xmlType;
			this.objVal = value;
			if (nsResolver != null && (this.xmlType.TypeCode == XmlTypeCode.QName || this.xmlType.TypeCode == XmlTypeCode.Notation))
			{
				string @namespace = (this.objVal as XmlQualifiedName).Namespace;
				this.nsPrefix = new XmlAtomicValue.NamespacePrefixForQName(nsResolver.LookupPrefix(@namespace), @namespace);
			}
		}

		/// <summary>Returns a copy of this <see cref="T:System.Xml.Schema.XmlAtomicValue" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlAtomicValue" /> object copy of this <see cref="T:System.Xml.Schema.XmlAtomicValue" /> object.</returns>
		// Token: 0x06002A62 RID: 10850 RVA: 0x00002068 File Offset: 0x00000268
		public XmlAtomicValue Clone()
		{
			return this;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Schema.XmlAtomicValue.Clone" />.</summary>
		/// <returns>Returns a copy of this <see cref="T:System.Xml.Schema.XmlAtomicValue" /> object.</returns>
		// Token: 0x06002A63 RID: 10851 RVA: 0x00002068 File Offset: 0x00000268
		object ICloneable.Clone()
		{
			return this;
		}

		/// <summary>Gets a value indicating whether the validated XML element or attribute is an XPath node or an atomic value.</summary>
		/// <returns>true if the validated XML element or attribute is an XPath node; false if the validated XML element or attribute is an atomic value.</returns>
		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06002A64 RID: 10852 RVA: 0x0000226C File Offset: 0x0000046C
		public override bool IsNode
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaType" /> for the validated XML element or attribute.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaType" /> for the validated XML element or attribute.</returns>
		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06002A65 RID: 10853 RVA: 0x00103A16 File Offset: 0x00101C16
		public override XmlSchemaType XmlType
		{
			get
			{
				return this.xmlType;
			}
		}

		/// <summary>Gets the Microsoft .NET Framework type of the validated XML element or attribute.</summary>
		/// <returns>The .NET Framework type of the validated XML element or attribute. The default value is <see cref="T:System.String" />.</returns>
		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06002A66 RID: 10854 RVA: 0x00103A1E File Offset: 0x00101C1E
		public override Type ValueType
		{
			get
			{
				return this.xmlType.Datatype.ValueType;
			}
		}

		/// <summary>Gets the current validated XML element or attribute as a boxed object of the most appropriate Microsoft .NET Framework type according to its schema type.</summary>
		/// <returns>The current validated XML element or attribute as a boxed object of the most appropriate .NET Framework type.</returns>
		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06002A67 RID: 10855 RVA: 0x00103A30 File Offset: 0x00101C30
		public override object TypedValue
		{
			get
			{
				XmlValueConverter valueConverter = this.xmlType.ValueConverter;
				if (this.objVal == null)
				{
					TypeCode typeCode = this.clrType;
					if (typeCode <= TypeCode.Int32)
					{
						if (typeCode == TypeCode.Boolean)
						{
							return valueConverter.ChangeType(this.unionVal.boolVal, this.ValueType);
						}
						if (typeCode == TypeCode.Int32)
						{
							return valueConverter.ChangeType(this.unionVal.i32Val, this.ValueType);
						}
					}
					else
					{
						if (typeCode == TypeCode.Int64)
						{
							return valueConverter.ChangeType(this.unionVal.i64Val, this.ValueType);
						}
						if (typeCode == TypeCode.Double)
						{
							return valueConverter.ChangeType(this.unionVal.dblVal, this.ValueType);
						}
						if (typeCode == TypeCode.DateTime)
						{
							return valueConverter.ChangeType(this.unionVal.dtVal, this.ValueType);
						}
					}
				}
				return valueConverter.ChangeType(this.objVal, this.ValueType, this.nsPrefix);
			}
		}

		/// <summary>Gets the validated XML element or attribute's value as a <see cref="T:System.Boolean" />.</summary>
		/// <returns>The validated XML element or attribute's value as a <see cref="T:System.Boolean" />.</returns>
		/// <exception cref="T:System.FormatException">The validated XML element or attribute's value is not in the correct format for the <see cref="T:System.Boolean" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Boolean" /> is not valid.</exception>
		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06002A68 RID: 10856 RVA: 0x00103B10 File Offset: 0x00101D10
		public override bool ValueAsBoolean
		{
			get
			{
				XmlValueConverter valueConverter = this.xmlType.ValueConverter;
				if (this.objVal == null)
				{
					TypeCode typeCode = this.clrType;
					if (typeCode <= TypeCode.Int32)
					{
						if (typeCode == TypeCode.Boolean)
						{
							return this.unionVal.boolVal;
						}
						if (typeCode == TypeCode.Int32)
						{
							return valueConverter.ToBoolean(this.unionVal.i32Val);
						}
					}
					else
					{
						if (typeCode == TypeCode.Int64)
						{
							return valueConverter.ToBoolean(this.unionVal.i64Val);
						}
						if (typeCode == TypeCode.Double)
						{
							return valueConverter.ToBoolean(this.unionVal.dblVal);
						}
						if (typeCode == TypeCode.DateTime)
						{
							return valueConverter.ToBoolean(this.unionVal.dtVal);
						}
					}
				}
				return valueConverter.ToBoolean(this.objVal);
			}
		}

		/// <summary>Gets the validated XML element or attribute's value as a <see cref="T:System.DateTime" />.</summary>
		/// <returns>The validated XML element or attribute's value as a <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.FormatException">The validated XML element or attribute's value is not in the correct format for the <see cref="T:System.DateTime" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.DateTime" /> is not valid.</exception>
		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06002A69 RID: 10857 RVA: 0x00103BBC File Offset: 0x00101DBC
		public override DateTime ValueAsDateTime
		{
			get
			{
				XmlValueConverter valueConverter = this.xmlType.ValueConverter;
				if (this.objVal == null)
				{
					TypeCode typeCode = this.clrType;
					if (typeCode <= TypeCode.Int32)
					{
						if (typeCode == TypeCode.Boolean)
						{
							return valueConverter.ToDateTime(this.unionVal.boolVal);
						}
						if (typeCode == TypeCode.Int32)
						{
							return valueConverter.ToDateTime(this.unionVal.i32Val);
						}
					}
					else
					{
						if (typeCode == TypeCode.Int64)
						{
							return valueConverter.ToDateTime(this.unionVal.i64Val);
						}
						if (typeCode == TypeCode.Double)
						{
							return valueConverter.ToDateTime(this.unionVal.dblVal);
						}
						if (typeCode == TypeCode.DateTime)
						{
							return this.unionVal.dtVal;
						}
					}
				}
				return valueConverter.ToDateTime(this.objVal);
			}
		}

		/// <summary>Gets the validated XML element or attribute's value as a <see cref="T:System.Double" />.</summary>
		/// <returns>The validated XML element or attribute's value as a <see cref="T:System.Double" />.</returns>
		/// <exception cref="T:System.FormatException">The validated XML element or attribute's value is not in the correct format for the <see cref="T:System.Double" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Double" /> is not valid.</exception>
		/// <exception cref="T:System.OverflowException">The attempted cast resulted in an overflow.</exception>
		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06002A6A RID: 10858 RVA: 0x00103C68 File Offset: 0x00101E68
		public override double ValueAsDouble
		{
			get
			{
				XmlValueConverter valueConverter = this.xmlType.ValueConverter;
				if (this.objVal == null)
				{
					TypeCode typeCode = this.clrType;
					if (typeCode <= TypeCode.Int32)
					{
						if (typeCode == TypeCode.Boolean)
						{
							return valueConverter.ToDouble(this.unionVal.boolVal);
						}
						if (typeCode == TypeCode.Int32)
						{
							return valueConverter.ToDouble(this.unionVal.i32Val);
						}
					}
					else
					{
						if (typeCode == TypeCode.Int64)
						{
							return valueConverter.ToDouble(this.unionVal.i64Val);
						}
						if (typeCode == TypeCode.Double)
						{
							return this.unionVal.dblVal;
						}
						if (typeCode == TypeCode.DateTime)
						{
							return valueConverter.ToDouble(this.unionVal.dtVal);
						}
					}
				}
				return valueConverter.ToDouble(this.objVal);
			}
		}

		/// <summary>Gets the validated XML element or attribute's value as an <see cref="T:System.Int32" />.</summary>
		/// <returns>The validated XML element or attribute's value as an <see cref="T:System.Int32" />.</returns>
		/// <exception cref="T:System.FormatException">The validated XML element or attribute's value is not in the correct format for the <see cref="T:System.Int32" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Int32" /> is not valid.</exception>
		/// <exception cref="T:System.OverflowException">The attempted cast resulted in an overflow.</exception>
		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06002A6B RID: 10859 RVA: 0x00103D14 File Offset: 0x00101F14
		public override int ValueAsInt
		{
			get
			{
				XmlValueConverter valueConverter = this.xmlType.ValueConverter;
				if (this.objVal == null)
				{
					TypeCode typeCode = this.clrType;
					if (typeCode <= TypeCode.Int32)
					{
						if (typeCode == TypeCode.Boolean)
						{
							return valueConverter.ToInt32(this.unionVal.boolVal);
						}
						if (typeCode == TypeCode.Int32)
						{
							return this.unionVal.i32Val;
						}
					}
					else
					{
						if (typeCode == TypeCode.Int64)
						{
							return valueConverter.ToInt32(this.unionVal.i64Val);
						}
						if (typeCode == TypeCode.Double)
						{
							return valueConverter.ToInt32(this.unionVal.dblVal);
						}
						if (typeCode == TypeCode.DateTime)
						{
							return valueConverter.ToInt32(this.unionVal.dtVal);
						}
					}
				}
				return valueConverter.ToInt32(this.objVal);
			}
		}

		/// <summary>Gets the validated XML element or attribute's value as an <see cref="T:System.Int64" />.</summary>
		/// <returns>The validated XML element or attribute's value as an <see cref="T:System.Int64" />.</returns>
		/// <exception cref="T:System.FormatException">The validated XML element or attribute's value is not in the correct format for the <see cref="T:System.Int64" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Int64" /> is not valid.</exception>
		/// <exception cref="T:System.OverflowException">The attempted cast resulted in an overflow.</exception>
		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06002A6C RID: 10860 RVA: 0x00103DC0 File Offset: 0x00101FC0
		public override long ValueAsLong
		{
			get
			{
				XmlValueConverter valueConverter = this.xmlType.ValueConverter;
				if (this.objVal == null)
				{
					TypeCode typeCode = this.clrType;
					if (typeCode <= TypeCode.Int32)
					{
						if (typeCode == TypeCode.Boolean)
						{
							return valueConverter.ToInt64(this.unionVal.boolVal);
						}
						if (typeCode == TypeCode.Int32)
						{
							return valueConverter.ToInt64(this.unionVal.i32Val);
						}
					}
					else
					{
						if (typeCode == TypeCode.Int64)
						{
							return this.unionVal.i64Val;
						}
						if (typeCode == TypeCode.Double)
						{
							return valueConverter.ToInt64(this.unionVal.dblVal);
						}
						if (typeCode == TypeCode.DateTime)
						{
							return valueConverter.ToInt64(this.unionVal.dtVal);
						}
					}
				}
				return valueConverter.ToInt64(this.objVal);
			}
		}

		/// <summary>Returns the validated XML element or attribute's value as the type specified using the <see cref="T:System.Xml.IXmlNamespaceResolver" /> object specified to resolve namespace prefixes.</summary>
		/// <returns>The value of the validated XML element or attribute as the type requested.</returns>
		/// <param name="type">The type to return the validated XML element or attribute's value as.</param>
		/// <param name="nsResolver">The <see cref="T:System.Xml.IXmlNamespaceResolver" /> object used to resolve namespace prefixes.</param>
		/// <exception cref="T:System.FormatException">The validated XML element or attribute's value is not in the correct format for the target type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast is not valid.</exception>
		/// <exception cref="T:System.OverflowException">The attempted cast resulted in an overflow.</exception>
		// Token: 0x06002A6D RID: 10861 RVA: 0x00103E6C File Offset: 0x0010206C
		public override object ValueAs(Type type, IXmlNamespaceResolver nsResolver)
		{
			XmlValueConverter valueConverter = this.xmlType.ValueConverter;
			if (type == typeof(XPathItem) || type == typeof(XmlAtomicValue))
			{
				return this;
			}
			if (this.objVal == null)
			{
				TypeCode typeCode = this.clrType;
				if (typeCode <= TypeCode.Int32)
				{
					if (typeCode == TypeCode.Boolean)
					{
						return valueConverter.ChangeType(this.unionVal.boolVal, type);
					}
					if (typeCode == TypeCode.Int32)
					{
						return valueConverter.ChangeType(this.unionVal.i32Val, type);
					}
				}
				else
				{
					if (typeCode == TypeCode.Int64)
					{
						return valueConverter.ChangeType(this.unionVal.i64Val, type);
					}
					if (typeCode == TypeCode.Double)
					{
						return valueConverter.ChangeType(this.unionVal.dblVal, type);
					}
					if (typeCode == TypeCode.DateTime)
					{
						return valueConverter.ChangeType(this.unionVal.dtVal, type);
					}
				}
			}
			return valueConverter.ChangeType(this.objVal, type, nsResolver);
		}

		/// <summary>Gets the string value of the validated XML element or attribute.</summary>
		/// <returns>The string value of the validated XML element or attribute.</returns>
		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06002A6E RID: 10862 RVA: 0x00103F4C File Offset: 0x0010214C
		public override string Value
		{
			get
			{
				XmlValueConverter valueConverter = this.xmlType.ValueConverter;
				if (this.objVal == null)
				{
					TypeCode typeCode = this.clrType;
					if (typeCode <= TypeCode.Int32)
					{
						if (typeCode == TypeCode.Boolean)
						{
							return valueConverter.ToString(this.unionVal.boolVal);
						}
						if (typeCode == TypeCode.Int32)
						{
							return valueConverter.ToString(this.unionVal.i32Val);
						}
					}
					else
					{
						if (typeCode == TypeCode.Int64)
						{
							return valueConverter.ToString(this.unionVal.i64Val);
						}
						if (typeCode == TypeCode.Double)
						{
							return valueConverter.ToString(this.unionVal.dblVal);
						}
						if (typeCode == TypeCode.DateTime)
						{
							return valueConverter.ToString(this.unionVal.dtVal);
						}
					}
				}
				return valueConverter.ToString(this.objVal, this.nsPrefix);
			}
		}

		/// <summary>Gets the string value of the validated XML element or attribute.</summary>
		/// <returns>The string value of the validated XML element or attribute.</returns>
		// Token: 0x06002A6F RID: 10863 RVA: 0x00090BB2 File Offset: 0x0008EDB2
		public override string ToString()
		{
			return this.Value;
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x00104004 File Offset: 0x00102204
		private string GetPrefixFromQName(string value)
		{
			int num2;
			int num = ValidateNames.ParseQName(value, 0, out num2);
			if (num == 0 || num != value.Length)
			{
				return null;
			}
			if (num2 != 0)
			{
				return value.Substring(0, num2);
			}
			return string.Empty;
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x000728B0 File Offset: 0x00070AB0
		internal XmlAtomicValue()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001CF1 RID: 7409
		private XmlSchemaType xmlType;

		// Token: 0x04001CF2 RID: 7410
		private object objVal;

		// Token: 0x04001CF3 RID: 7411
		private TypeCode clrType;

		// Token: 0x04001CF4 RID: 7412
		private XmlAtomicValue.Union unionVal;

		// Token: 0x04001CF5 RID: 7413
		private XmlAtomicValue.NamespacePrefixForQName nsPrefix;

		// Token: 0x02000432 RID: 1074
		[StructLayout(LayoutKind.Explicit, Size = 8)]
		private struct Union
		{
			// Token: 0x04001CF6 RID: 7414
			[FieldOffset(0)]
			public bool boolVal;

			// Token: 0x04001CF7 RID: 7415
			[FieldOffset(0)]
			public double dblVal;

			// Token: 0x04001CF8 RID: 7416
			[FieldOffset(0)]
			public long i64Val;

			// Token: 0x04001CF9 RID: 7417
			[FieldOffset(0)]
			public int i32Val;

			// Token: 0x04001CFA RID: 7418
			[FieldOffset(0)]
			public DateTime dtVal;
		}

		// Token: 0x02000433 RID: 1075
		private class NamespacePrefixForQName : IXmlNamespaceResolver
		{
			// Token: 0x06002A72 RID: 10866 RVA: 0x0010403A File Offset: 0x0010223A
			public NamespacePrefixForQName(string prefix, string ns)
			{
				this.ns = ns;
				this.prefix = prefix;
			}

			// Token: 0x06002A73 RID: 10867 RVA: 0x00104050 File Offset: 0x00102250
			public string LookupNamespace(string prefix)
			{
				if (prefix == this.prefix)
				{
					return this.ns;
				}
				return null;
			}

			// Token: 0x06002A74 RID: 10868 RVA: 0x00104068 File Offset: 0x00102268
			public string LookupPrefix(string namespaceName)
			{
				if (this.ns == namespaceName)
				{
					return this.prefix;
				}
				return null;
			}

			// Token: 0x06002A75 RID: 10869 RVA: 0x00104080 File Offset: 0x00102280
			public IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>(1);
				dictionary[this.prefix] = this.ns;
				return dictionary;
			}

			// Token: 0x04001CFB RID: 7419
			public string prefix;

			// Token: 0x04001CFC RID: 7420
			public string ns;
		}
	}
}
