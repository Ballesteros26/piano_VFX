using System;
using System.IO;
using System.Text;
using System.Xml.Schema;

namespace System.Xml.Xsl
{
	// Token: 0x020004CC RID: 1228
	internal abstract class XmlQueryType : ListBase<XmlQueryType>
	{
		// Token: 0x060031D9 RID: 12761 RVA: 0x001212F0 File Offset: 0x0011F4F0
		static XmlQueryType()
		{
			for (int i = 0; i < XmlQueryType.BaseTypeCodes.Length; i++)
			{
				int num = i;
				for (;;)
				{
					XmlQueryType.TypeCodeDerivation[i, num] = true;
					if (XmlQueryType.BaseTypeCodes[num] == (XmlTypeCode)num)
					{
						break;
					}
					num = (int)XmlQueryType.BaseTypeCodes[num];
				}
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x060031DA RID: 12762
		public abstract XmlTypeCode TypeCode { get; }

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x060031DB RID: 12763
		public abstract XmlQualifiedNameTest NameTest { get; }

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x060031DC RID: 12764
		public abstract XmlSchemaType SchemaType { get; }

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x060031DD RID: 12765
		public abstract bool IsNillable { get; }

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x060031DE RID: 12766
		public abstract XmlNodeKindFlags NodeKinds { get; }

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x060031DF RID: 12767
		public abstract bool IsStrict { get; }

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x060031E0 RID: 12768
		public abstract XmlQueryCardinality Cardinality { get; }

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x060031E1 RID: 12769
		public abstract XmlQueryType Prime { get; }

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x060031E2 RID: 12770
		public abstract bool IsNotRtf { get; }

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x060031E3 RID: 12771
		public abstract bool IsDod { get; }

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x060031E4 RID: 12772
		public abstract XmlValueConverter ClrMapping { get; }

		// Token: 0x060031E5 RID: 12773 RVA: 0x00121564 File Offset: 0x0011F764
		public bool IsSubtypeOf(XmlQueryType baseType)
		{
			if (!(this.Cardinality <= baseType.Cardinality) || (!this.IsDod && baseType.IsDod))
			{
				return false;
			}
			if (!this.IsDod && baseType.IsDod)
			{
				return false;
			}
			XmlQueryType prime = this.Prime;
			XmlQueryType prime2 = baseType.Prime;
			if (prime == prime2)
			{
				return true;
			}
			if (prime.Count == 1 && prime2.Count == 1)
			{
				return prime.IsSubtypeOfItemType(prime2);
			}
			foreach (XmlQueryType xmlQueryType in prime)
			{
				bool flag = false;
				foreach (XmlQueryType xmlQueryType2 in prime2)
				{
					if (xmlQueryType.IsSubtypeOfItemType(xmlQueryType2))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060031E6 RID: 12774 RVA: 0x00121668 File Offset: 0x0011F868
		public bool NeverSubtypeOf(XmlQueryType baseType)
		{
			if (this.Cardinality.NeverSubset(baseType.Cardinality))
			{
				return true;
			}
			if (this.MaybeEmpty && baseType.MaybeEmpty)
			{
				return false;
			}
			if (this.Count == 0)
			{
				return false;
			}
			foreach (XmlQueryType xmlQueryType in this)
			{
				foreach (XmlQueryType xmlQueryType2 in baseType)
				{
					if (xmlQueryType.HasIntersectionItemType(xmlQueryType2))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060031E7 RID: 12775 RVA: 0x00121730 File Offset: 0x0011F930
		public bool Equals(XmlQueryType that)
		{
			if (that == null)
			{
				return false;
			}
			if (this.Cardinality != that.Cardinality || this.IsDod != that.IsDod)
			{
				return false;
			}
			XmlQueryType prime = this.Prime;
			XmlQueryType prime2 = that.Prime;
			if (prime == prime2)
			{
				return true;
			}
			if (prime.Count != prime2.Count)
			{
				return false;
			}
			if (prime.Count == 1)
			{
				return prime.TypeCode == prime2.TypeCode && prime.NameTest == prime2.NameTest && prime.SchemaType == prime2.SchemaType && prime.IsStrict == prime2.IsStrict && prime.IsNotRtf == prime2.IsNotRtf;
			}
			foreach (XmlQueryType xmlQueryType in this)
			{
				bool flag = false;
				foreach (XmlQueryType xmlQueryType2 in that)
				{
					if (xmlQueryType.TypeCode == xmlQueryType2.TypeCode && xmlQueryType.NameTest == xmlQueryType2.NameTest && xmlQueryType.SchemaType == xmlQueryType2.SchemaType && xmlQueryType.IsStrict == xmlQueryType2.IsStrict && xmlQueryType.IsNotRtf == xmlQueryType2.IsNotRtf)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060031E8 RID: 12776 RVA: 0x001218C4 File Offset: 0x0011FAC4
		public static bool operator ==(XmlQueryType left, XmlQueryType right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x001218D5 File Offset: 0x0011FAD5
		public static bool operator !=(XmlQueryType left, XmlQueryType right)
		{
			if (left == null)
			{
				return right != null;
			}
			return !left.Equals(right);
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x060031EA RID: 12778 RVA: 0x001218E9 File Offset: 0x0011FAE9
		public bool IsEmpty
		{
			get
			{
				return this.Cardinality <= XmlQueryCardinality.Zero;
			}
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x060031EB RID: 12779 RVA: 0x001218FB File Offset: 0x0011FAFB
		public bool IsSingleton
		{
			get
			{
				return this.Cardinality <= XmlQueryCardinality.One;
			}
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x060031EC RID: 12780 RVA: 0x0012190D File Offset: 0x0011FB0D
		public bool MaybeEmpty
		{
			get
			{
				return XmlQueryCardinality.Zero <= this.Cardinality;
			}
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x060031ED RID: 12781 RVA: 0x0012191F File Offset: 0x0011FB1F
		public bool MaybeMany
		{
			get
			{
				return XmlQueryCardinality.More <= this.Cardinality;
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x060031EE RID: 12782 RVA: 0x00121931 File Offset: 0x0011FB31
		public bool IsNode
		{
			get
			{
				return (XmlQueryType.TypeCodeToFlags[(int)this.TypeCode] & XmlQueryType.TypeFlags.IsNode) > XmlQueryType.TypeFlags.None;
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x060031EF RID: 12783 RVA: 0x00121944 File Offset: 0x0011FB44
		public bool IsAtomicValue
		{
			get
			{
				return (XmlQueryType.TypeCodeToFlags[(int)this.TypeCode] & XmlQueryType.TypeFlags.IsAtomicValue) > XmlQueryType.TypeFlags.None;
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x060031F0 RID: 12784 RVA: 0x00121957 File Offset: 0x0011FB57
		public bool IsNumeric
		{
			get
			{
				return (XmlQueryType.TypeCodeToFlags[(int)this.TypeCode] & XmlQueryType.TypeFlags.IsNumeric) > XmlQueryType.TypeFlags.None;
			}
		}

		// Token: 0x060031F1 RID: 12785 RVA: 0x0012196C File Offset: 0x0011FB6C
		public override bool Equals(object obj)
		{
			XmlQueryType xmlQueryType = obj as XmlQueryType;
			return !(xmlQueryType == null) && this.Equals(xmlQueryType);
		}

		// Token: 0x060031F2 RID: 12786 RVA: 0x00121994 File Offset: 0x0011FB94
		public override int GetHashCode()
		{
			if (this.hashCode == 0)
			{
				int num = (int)this.TypeCode;
				XmlSchemaType schemaType = this.SchemaType;
				if (schemaType != null)
				{
					num += (num << 7) ^ schemaType.GetHashCode();
				}
				num += (num << 7) ^ (int)this.NodeKinds;
				num += (num << 7) ^ this.Cardinality.GetHashCode();
				num += (num << 7) ^ (this.IsStrict ? 1 : 0);
				num -= num >> 17;
				num -= num >> 11;
				num -= num >> 5;
				this.hashCode = ((num == 0) ? 1 : num);
			}
			return this.hashCode;
		}

		// Token: 0x060031F3 RID: 12787 RVA: 0x00121A29 File Offset: 0x0011FC29
		public override string ToString()
		{
			return this.ToString("G");
		}

		// Token: 0x060031F4 RID: 12788 RVA: 0x00121A38 File Offset: 0x0011FC38
		public string ToString(string format)
		{
			StringBuilder stringBuilder;
			if (format == "S")
			{
				stringBuilder = new StringBuilder();
				stringBuilder.Append(this.Cardinality.ToString(format));
				stringBuilder.Append(';');
				for (int i = 0; i < this.Count; i++)
				{
					if (i != 0)
					{
						stringBuilder.Append("|");
					}
					stringBuilder.Append(this[i].TypeCode.ToString());
				}
				stringBuilder.Append(';');
				stringBuilder.Append(this.IsStrict);
				return stringBuilder.ToString();
			}
			bool flag = format == "X";
			if (this.Cardinality == XmlQueryCardinality.None)
			{
				return "none";
			}
			if (this.Cardinality == XmlQueryCardinality.Zero)
			{
				return "empty";
			}
			stringBuilder = new StringBuilder();
			int count = this.Count;
			if (count != 0)
			{
				if (count != 1)
				{
					string[] array = new string[this.Count];
					for (int j = 0; j < this.Count; j++)
					{
						array[j] = this[j].ItemTypeToString(flag);
					}
					Array.Sort<string>(array);
					stringBuilder = new StringBuilder();
					stringBuilder.Append('(');
					stringBuilder.Append(array[0]);
					for (int k = 1; k < array.Length; k++)
					{
						stringBuilder.Append(" | ");
						stringBuilder.Append(array[k]);
					}
					stringBuilder.Append(')');
				}
				else
				{
					stringBuilder.Append(this[0].ItemTypeToString(flag));
				}
			}
			else
			{
				stringBuilder.Append("none");
			}
			stringBuilder.Append(this.Cardinality.ToString());
			if (!flag && this.IsDod)
			{
				stringBuilder.Append('#');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060031F5 RID: 12789
		public abstract void GetObjectData(BinaryWriter writer);

		// Token: 0x060031F6 RID: 12790 RVA: 0x00121C18 File Offset: 0x0011FE18
		private bool IsSubtypeOfItemType(XmlQueryType baseType)
		{
			XmlSchemaType schemaType = baseType.SchemaType;
			if (this.TypeCode != baseType.TypeCode)
			{
				if (baseType.IsStrict)
				{
					return false;
				}
				XmlSchemaType builtInSimpleType = XmlSchemaType.GetBuiltInSimpleType(baseType.TypeCode);
				return (builtInSimpleType == null || schemaType == builtInSimpleType) && XmlQueryType.TypeCodeDerivation[this.TypeCode, baseType.TypeCode];
			}
			else
			{
				if (baseType.IsStrict)
				{
					return this.IsStrict && this.SchemaType == schemaType;
				}
				return (this.IsNotRtf || !baseType.IsNotRtf) && this.NameTest.IsSubsetOf(baseType.NameTest) && (schemaType == XmlSchemaComplexType.AnyType || XmlSchemaType.IsDerivedFrom(this.SchemaType, schemaType, XmlSchemaDerivationMethod.Empty)) && (!this.IsNillable || baseType.IsNillable);
			}
		}

		// Token: 0x060031F7 RID: 12791 RVA: 0x00121CD8 File Offset: 0x0011FED8
		private bool HasIntersectionItemType(XmlQueryType other)
		{
			if (this.TypeCode == other.TypeCode && (this.NodeKinds & (XmlNodeKindFlags.Document | XmlNodeKindFlags.Element | XmlNodeKindFlags.Attribute)) != XmlNodeKindFlags.None)
			{
				return this.TypeCode == XmlTypeCode.Node || (this.NameTest.HasIntersection(other.NameTest) && (XmlSchemaType.IsDerivedFrom(this.SchemaType, other.SchemaType, XmlSchemaDerivationMethod.Empty) || XmlSchemaType.IsDerivedFrom(other.SchemaType, this.SchemaType, XmlSchemaDerivationMethod.Empty)));
			}
			return this.IsSubtypeOf(other) || other.IsSubtypeOf(this);
		}

		// Token: 0x060031F8 RID: 12792 RVA: 0x00121D60 File Offset: 0x0011FF60
		private string ItemTypeToString(bool isXQ)
		{
			string text;
			if (this.IsNode)
			{
				text = XmlQueryType.TypeNames[(int)this.TypeCode];
				XmlTypeCode typeCode = this.TypeCode;
				if (typeCode != XmlTypeCode.Document)
				{
					if (typeCode - XmlTypeCode.Element > 1)
					{
						goto IL_00B0;
					}
				}
				else if (isXQ)
				{
					text = text + "{(element" + this.NameAndType(true) + "?&text?&comment?&processing-instruction?)*}";
					goto IL_00B0;
				}
				text += this.NameAndType(isXQ);
			}
			else if (this.SchemaType != XmlSchemaComplexType.AnyType)
			{
				if (this.SchemaType.QualifiedName.IsEmpty)
				{
					text = "<:" + XmlQueryType.TypeNames[(int)this.TypeCode];
				}
				else
				{
					text = XmlQueryType.QNameToString(this.SchemaType.QualifiedName);
				}
			}
			else
			{
				text = XmlQueryType.TypeNames[(int)this.TypeCode];
			}
			IL_00B0:
			if (!isXQ && this.IsStrict)
			{
				text += "=";
			}
			return text;
		}

		// Token: 0x060031F9 RID: 12793 RVA: 0x00121E38 File Offset: 0x00120038
		private string NameAndType(bool isXQ)
		{
			string text = this.NameTest.ToString();
			string text2 = "*";
			if (this.SchemaType.QualifiedName.IsEmpty)
			{
				text2 = "typeof(" + text + ")";
			}
			else if (isXQ || (this.SchemaType != XmlSchemaComplexType.AnyType && this.SchemaType != DatatypeImplementation.AnySimpleType))
			{
				text2 = XmlQueryType.QNameToString(this.SchemaType.QualifiedName);
			}
			if (this.IsNillable)
			{
				text2 += " nillable";
			}
			if (text == "*" && text2 == "*")
			{
				return "";
			}
			return string.Concat(new string[] { "(", text, ", ", text2, ")" });
		}

		// Token: 0x060031FA RID: 12794 RVA: 0x00121F0C File Offset: 0x0012010C
		private static string QNameToString(XmlQualifiedName name)
		{
			if (name.IsEmpty)
			{
				return "*";
			}
			if (name.Namespace.Length == 0)
			{
				return name.Name;
			}
			if (name.Namespace == "http://www.w3.org/2001/XMLSchema")
			{
				return "xs:" + name.Name;
			}
			if (name.Namespace == "http://www.w3.org/2003/11/xpath-datatypes")
			{
				return "xdt:" + name.Name;
			}
			return "{" + name.Namespace + "}" + name.Name;
		}

		// Token: 0x04002070 RID: 8304
		private static readonly XmlQueryType.BitMatrix TypeCodeDerivation = new XmlQueryType.BitMatrix(XmlQueryType.BaseTypeCodes.Length);

		// Token: 0x04002071 RID: 8305
		private int hashCode;

		// Token: 0x04002072 RID: 8306
		private static readonly XmlQueryType.TypeFlags[] TypeCodeToFlags = new XmlQueryType.TypeFlags[]
		{
			(XmlQueryType.TypeFlags)7,
			XmlQueryType.TypeFlags.None,
			XmlQueryType.TypeFlags.IsNode,
			XmlQueryType.TypeFlags.IsNode,
			XmlQueryType.TypeFlags.IsNode,
			XmlQueryType.TypeFlags.IsNode,
			XmlQueryType.TypeFlags.IsNode,
			XmlQueryType.TypeFlags.IsNode,
			XmlQueryType.TypeFlags.IsNode,
			XmlQueryType.TypeFlags.IsNode,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			(XmlQueryType.TypeFlags)6,
			(XmlQueryType.TypeFlags)6,
			(XmlQueryType.TypeFlags)6,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			(XmlQueryType.TypeFlags)6,
			(XmlQueryType.TypeFlags)6,
			(XmlQueryType.TypeFlags)6,
			(XmlQueryType.TypeFlags)6,
			(XmlQueryType.TypeFlags)6,
			(XmlQueryType.TypeFlags)6,
			(XmlQueryType.TypeFlags)6,
			(XmlQueryType.TypeFlags)6,
			(XmlQueryType.TypeFlags)6,
			(XmlQueryType.TypeFlags)6,
			(XmlQueryType.TypeFlags)6,
			(XmlQueryType.TypeFlags)6,
			(XmlQueryType.TypeFlags)6,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue
		};

		// Token: 0x04002073 RID: 8307
		private static readonly XmlTypeCode[] BaseTypeCodes = new XmlTypeCode[]
		{
			XmlTypeCode.None,
			XmlTypeCode.Item,
			XmlTypeCode.Item,
			XmlTypeCode.Node,
			XmlTypeCode.Node,
			XmlTypeCode.Node,
			XmlTypeCode.Node,
			XmlTypeCode.Node,
			XmlTypeCode.Node,
			XmlTypeCode.Node,
			XmlTypeCode.Item,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.String,
			XmlTypeCode.NormalizedString,
			XmlTypeCode.Token,
			XmlTypeCode.Token,
			XmlTypeCode.Token,
			XmlTypeCode.Name,
			XmlTypeCode.NCName,
			XmlTypeCode.NCName,
			XmlTypeCode.NCName,
			XmlTypeCode.Decimal,
			XmlTypeCode.Integer,
			XmlTypeCode.NonPositiveInteger,
			XmlTypeCode.Integer,
			XmlTypeCode.Long,
			XmlTypeCode.Int,
			XmlTypeCode.Short,
			XmlTypeCode.Integer,
			XmlTypeCode.NonNegativeInteger,
			XmlTypeCode.UnsignedLong,
			XmlTypeCode.UnsignedInt,
			XmlTypeCode.UnsignedShort,
			XmlTypeCode.NonNegativeInteger,
			XmlTypeCode.Duration,
			XmlTypeCode.Duration
		};

		// Token: 0x04002074 RID: 8308
		private static readonly string[] TypeNames = new string[]
		{
			"none", "item", "node", "document", "element", "attribute", "namespace", "processing-instruction", "comment", "text",
			"xdt:anyAtomicType", "xdt:untypedAtomic", "xs:string", "xs:boolean", "xs:decimal", "xs:float", "xs:double", "xs:duration", "xs:dateTime", "xs:time",
			"xs:date", "xs:gYearMonth", "xs:gYear", "xs:gMonthDay", "xs:gDay", "xs:gMonth", "xs:hexBinary", "xs:base64Binary", "xs:anyUri", "xs:QName",
			"xs:NOTATION", "xs:normalizedString", "xs:token", "xs:language", "xs:NMTOKEN", "xs:Name", "xs:NCName", "xs:ID", "xs:IDREF", "xs:ENTITY",
			"xs:integer", "xs:nonPositiveInteger", "xs:negativeInteger", "xs:long", "xs:int", "xs:short", "xs:byte", "xs:nonNegativeInteger", "xs:unsignedLong", "xs:unsignedInt",
			"xs:unsignedShort", "xs:unsignedByte", "xs:positiveInteger", "xdt:yearMonthDuration", "xdt:dayTimeDuration"
		};

		// Token: 0x020004CD RID: 1229
		private enum TypeFlags
		{
			// Token: 0x04002076 RID: 8310
			None,
			// Token: 0x04002077 RID: 8311
			IsNode,
			// Token: 0x04002078 RID: 8312
			IsAtomicValue,
			// Token: 0x04002079 RID: 8313
			IsNumeric = 4
		}

		// Token: 0x020004CE RID: 1230
		private sealed class BitMatrix
		{
			// Token: 0x060031FC RID: 12796 RVA: 0x00121FA4 File Offset: 0x001201A4
			public BitMatrix(int count)
			{
				this.bits = new ulong[count];
			}

			// Token: 0x17000A9C RID: 2716
			public bool this[int index1, int index2]
			{
				get
				{
					return (this.bits[index1] & (1UL << index2)) > 0UL;
				}
				set
				{
					if (value)
					{
						this.bits[index1] |= 1UL << index2;
						return;
					}
					this.bits[index1] &= ~(1UL << index2);
				}
			}

			// Token: 0x17000A9D RID: 2717
			public bool this[XmlTypeCode index1, XmlTypeCode index2]
			{
				get
				{
					return this[(int)index1, (int)index2];
				}
			}

			// Token: 0x0400207A RID: 8314
			private ulong[] bits;
		}
	}
}
