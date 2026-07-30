using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml.Xsl
{
	// Token: 0x020004CF RID: 1231
	internal static class XmlQueryTypeFactory
	{
		// Token: 0x06003200 RID: 12800 RVA: 0x0012200D File Offset: 0x0012020D
		public static XmlQueryType Type(XmlTypeCode code, bool isStrict)
		{
			return XmlQueryTypeFactory.ItemType.Create(code, isStrict);
		}

		// Token: 0x06003201 RID: 12801 RVA: 0x00122018 File Offset: 0x00120218
		public static XmlQueryType Type(XmlSchemaSimpleType schemaType, bool isStrict)
		{
			if (schemaType.Datatype.Variety == XmlSchemaDatatypeVariety.Atomic)
			{
				if (schemaType == DatatypeImplementation.AnySimpleType)
				{
					return XmlQueryTypeFactory.AnyAtomicTypeS;
				}
				return XmlQueryTypeFactory.ItemType.Create(schemaType, isStrict);
			}
			else
			{
				while (schemaType.DerivedBy == XmlSchemaDerivationMethod.Restriction)
				{
					schemaType = (XmlSchemaSimpleType)schemaType.BaseXmlSchemaType;
				}
				if (schemaType.DerivedBy == XmlSchemaDerivationMethod.List)
				{
					return XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.Type(((XmlSchemaSimpleTypeList)schemaType.Content).BaseItemType, isStrict), XmlQueryCardinality.ZeroOrMore);
				}
				XmlSchemaSimpleType[] baseMemberTypes = ((XmlSchemaSimpleTypeUnion)schemaType.Content).BaseMemberTypes;
				XmlQueryType[] array = new XmlQueryType[baseMemberTypes.Length];
				for (int i = 0; i < baseMemberTypes.Length; i++)
				{
					array[i] = XmlQueryTypeFactory.Type(baseMemberTypes[i], isStrict);
				}
				return XmlQueryTypeFactory.Choice(array);
			}
		}

		// Token: 0x06003202 RID: 12802 RVA: 0x001220C2 File Offset: 0x001202C2
		public static XmlQueryType Choice(XmlQueryType left, XmlQueryType right)
		{
			return XmlQueryTypeFactory.SequenceType.Create(XmlQueryTypeFactory.ChoiceType.Create(XmlQueryTypeFactory.PrimeChoice(new List<XmlQueryType>(left), right)), left.Cardinality | right.Cardinality);
		}

		// Token: 0x06003203 RID: 12803 RVA: 0x001220EC File Offset: 0x001202EC
		public static XmlQueryType Choice(params XmlQueryType[] types)
		{
			if (types.Length == 0)
			{
				return XmlQueryTypeFactory.None;
			}
			if (types.Length == 1)
			{
				return types[0];
			}
			List<XmlQueryType> list = new List<XmlQueryType>(types[0]);
			XmlQueryCardinality xmlQueryCardinality = types[0].Cardinality;
			for (int i = 1; i < types.Length; i++)
			{
				XmlQueryTypeFactory.PrimeChoice(list, types[i]);
				xmlQueryCardinality |= types[i].Cardinality;
			}
			return XmlQueryTypeFactory.SequenceType.Create(XmlQueryTypeFactory.ChoiceType.Create(list), xmlQueryCardinality);
		}

		// Token: 0x06003204 RID: 12804 RVA: 0x00122152 File Offset: 0x00120352
		public static XmlQueryType NodeChoice(XmlNodeKindFlags kinds)
		{
			return XmlQueryTypeFactory.ChoiceType.Create(kinds);
		}

		// Token: 0x06003205 RID: 12805 RVA: 0x0012215A File Offset: 0x0012035A
		public static XmlQueryType Sequence(XmlQueryType left, XmlQueryType right)
		{
			return XmlQueryTypeFactory.SequenceType.Create(XmlQueryTypeFactory.ChoiceType.Create(XmlQueryTypeFactory.PrimeChoice(new List<XmlQueryType>(left), right)), left.Cardinality + right.Cardinality);
		}

		// Token: 0x06003206 RID: 12806 RVA: 0x00122183 File Offset: 0x00120383
		public static XmlQueryType PrimeProduct(XmlQueryType t, XmlQueryCardinality c)
		{
			if (t.Cardinality == c && !t.IsDod)
			{
				return t;
			}
			return XmlQueryTypeFactory.SequenceType.Create(t.Prime, c);
		}

		// Token: 0x06003207 RID: 12807 RVA: 0x001221A9 File Offset: 0x001203A9
		public static XmlQueryType Product(XmlQueryType t, XmlQueryCardinality c)
		{
			return XmlQueryTypeFactory.PrimeProduct(t, t.Cardinality * c);
		}

		// Token: 0x06003208 RID: 12808 RVA: 0x001221BD File Offset: 0x001203BD
		public static XmlQueryType AtMost(XmlQueryType t, XmlQueryCardinality c)
		{
			return XmlQueryTypeFactory.PrimeProduct(t, c.AtMost());
		}

		// Token: 0x06003209 RID: 12809 RVA: 0x001221CC File Offset: 0x001203CC
		private static List<XmlQueryType> PrimeChoice(List<XmlQueryType> accumulator, IList<XmlQueryType> types)
		{
			foreach (XmlQueryType xmlQueryType in types)
			{
				XmlQueryTypeFactory.AddItemToChoice(accumulator, xmlQueryType);
			}
			return accumulator;
		}

		// Token: 0x0600320A RID: 12810 RVA: 0x00122218 File Offset: 0x00120418
		private static void AddItemToChoice(List<XmlQueryType> accumulator, XmlQueryType itemType)
		{
			bool flag = true;
			for (int i = 0; i < accumulator.Count; i++)
			{
				if (itemType.IsSubtypeOf(accumulator[i]))
				{
					return;
				}
				if (accumulator[i].IsSubtypeOf(itemType))
				{
					if (flag)
					{
						flag = false;
						accumulator[i] = itemType;
					}
					else
					{
						accumulator.RemoveAt(i);
						i--;
					}
				}
			}
			if (flag)
			{
				accumulator.Add(itemType);
			}
		}

		// Token: 0x0600320B RID: 12811 RVA: 0x0012227B File Offset: 0x0012047B
		public static XmlQueryType Type(XPathNodeType kind, XmlQualifiedNameTest nameTest, XmlSchemaType contentType, bool isNillable)
		{
			return XmlQueryTypeFactory.ItemType.Create(XmlQueryTypeFactory.NodeKindToTypeCode[(int)kind], nameTest, contentType, isNillable);
		}

		// Token: 0x0600320C RID: 12812 RVA: 0x0012228C File Offset: 0x0012048C
		[Conditional("DEBUG")]
		public static void CheckSerializability(XmlQueryType type)
		{
			type.GetObjectData(new BinaryWriter(Stream.Null));
		}

		// Token: 0x0600320D RID: 12813 RVA: 0x001222A0 File Offset: 0x001204A0
		public static void Serialize(BinaryWriter writer, XmlQueryType type)
		{
			sbyte b;
			if (type.GetType() == typeof(XmlQueryTypeFactory.ItemType))
			{
				b = 0;
			}
			else if (type.GetType() == typeof(XmlQueryTypeFactory.ChoiceType))
			{
				b = 1;
			}
			else if (type.GetType() == typeof(XmlQueryTypeFactory.SequenceType))
			{
				b = 2;
			}
			else
			{
				b = -1;
			}
			writer.Write(b);
			type.GetObjectData(writer);
		}

		// Token: 0x0600320E RID: 12814 RVA: 0x00122310 File Offset: 0x00120510
		public static XmlQueryType Deserialize(BinaryReader reader)
		{
			switch (reader.ReadByte())
			{
			case 0:
				return XmlQueryTypeFactory.ItemType.Create(reader);
			case 1:
				return XmlQueryTypeFactory.ChoiceType.Create(reader);
			case 2:
				return XmlQueryTypeFactory.SequenceType.Create(reader);
			default:
				return null;
			}
		}

		// Token: 0x0400207B RID: 8315
		public static readonly XmlQueryType None = XmlQueryTypeFactory.ChoiceType.None;

		// Token: 0x0400207C RID: 8316
		public static readonly XmlQueryType Empty = XmlQueryTypeFactory.SequenceType.Zero;

		// Token: 0x0400207D RID: 8317
		public static readonly XmlQueryType Item = XmlQueryTypeFactory.Type(XmlTypeCode.Item, false);

		// Token: 0x0400207E RID: 8318
		public static readonly XmlQueryType ItemS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.Item, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x0400207F RID: 8319
		public static readonly XmlQueryType Node = XmlQueryTypeFactory.Type(XmlTypeCode.Node, false);

		// Token: 0x04002080 RID: 8320
		public static readonly XmlQueryType NodeS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.Node, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x04002081 RID: 8321
		public static readonly XmlQueryType Element = XmlQueryTypeFactory.Type(XmlTypeCode.Element, false);

		// Token: 0x04002082 RID: 8322
		public static readonly XmlQueryType ElementS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.Element, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x04002083 RID: 8323
		public static readonly XmlQueryType Document = XmlQueryTypeFactory.Type(XmlTypeCode.Document, false);

		// Token: 0x04002084 RID: 8324
		public static readonly XmlQueryType DocumentS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.Document, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x04002085 RID: 8325
		public static readonly XmlQueryType Attribute = XmlQueryTypeFactory.Type(XmlTypeCode.Attribute, false);

		// Token: 0x04002086 RID: 8326
		public static readonly XmlQueryType AttributeQ = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.Attribute, XmlQueryCardinality.ZeroOrOne);

		// Token: 0x04002087 RID: 8327
		public static readonly XmlQueryType AttributeS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.Attribute, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x04002088 RID: 8328
		public static readonly XmlQueryType Namespace = XmlQueryTypeFactory.Type(XmlTypeCode.Namespace, false);

		// Token: 0x04002089 RID: 8329
		public static readonly XmlQueryType NamespaceS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.Namespace, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x0400208A RID: 8330
		public static readonly XmlQueryType Text = XmlQueryTypeFactory.Type(XmlTypeCode.Text, false);

		// Token: 0x0400208B RID: 8331
		public static readonly XmlQueryType TextS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.Text, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x0400208C RID: 8332
		public static readonly XmlQueryType Comment = XmlQueryTypeFactory.Type(XmlTypeCode.Comment, false);

		// Token: 0x0400208D RID: 8333
		public static readonly XmlQueryType CommentS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.Comment, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x0400208E RID: 8334
		public static readonly XmlQueryType PI = XmlQueryTypeFactory.Type(XmlTypeCode.ProcessingInstruction, false);

		// Token: 0x0400208F RID: 8335
		public static readonly XmlQueryType PIS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.PI, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x04002090 RID: 8336
		public static readonly XmlQueryType DocumentOrElement = XmlQueryTypeFactory.Choice(XmlQueryTypeFactory.Document, XmlQueryTypeFactory.Element);

		// Token: 0x04002091 RID: 8337
		public static readonly XmlQueryType DocumentOrElementQ = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.DocumentOrElement, XmlQueryCardinality.ZeroOrOne);

		// Token: 0x04002092 RID: 8338
		public static readonly XmlQueryType DocumentOrElementS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.DocumentOrElement, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x04002093 RID: 8339
		public static readonly XmlQueryType Content = XmlQueryTypeFactory.Choice(new XmlQueryType[]
		{
			XmlQueryTypeFactory.Element,
			XmlQueryTypeFactory.Comment,
			XmlQueryTypeFactory.PI,
			XmlQueryTypeFactory.Text
		});

		// Token: 0x04002094 RID: 8340
		public static readonly XmlQueryType ContentS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.Content, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x04002095 RID: 8341
		public static readonly XmlQueryType DocumentOrContent = XmlQueryTypeFactory.Choice(XmlQueryTypeFactory.Document, XmlQueryTypeFactory.Content);

		// Token: 0x04002096 RID: 8342
		public static readonly XmlQueryType DocumentOrContentS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.DocumentOrContent, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x04002097 RID: 8343
		public static readonly XmlQueryType AttributeOrContent = XmlQueryTypeFactory.Choice(XmlQueryTypeFactory.Attribute, XmlQueryTypeFactory.Content);

		// Token: 0x04002098 RID: 8344
		public static readonly XmlQueryType AttributeOrContentS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.AttributeOrContent, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x04002099 RID: 8345
		public static readonly XmlQueryType AnyAtomicType = XmlQueryTypeFactory.Type(XmlTypeCode.AnyAtomicType, false);

		// Token: 0x0400209A RID: 8346
		public static readonly XmlQueryType AnyAtomicTypeS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.AnyAtomicType, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x0400209B RID: 8347
		public static readonly XmlQueryType String = XmlQueryTypeFactory.Type(XmlTypeCode.String, false);

		// Token: 0x0400209C RID: 8348
		public static readonly XmlQueryType StringX = XmlQueryTypeFactory.Type(XmlTypeCode.String, true);

		// Token: 0x0400209D RID: 8349
		public static readonly XmlQueryType StringXS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.StringX, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x0400209E RID: 8350
		public static readonly XmlQueryType Boolean = XmlQueryTypeFactory.Type(XmlTypeCode.Boolean, false);

		// Token: 0x0400209F RID: 8351
		public static readonly XmlQueryType BooleanX = XmlQueryTypeFactory.Type(XmlTypeCode.Boolean, true);

		// Token: 0x040020A0 RID: 8352
		public static readonly XmlQueryType Int = XmlQueryTypeFactory.Type(XmlTypeCode.Int, false);

		// Token: 0x040020A1 RID: 8353
		public static readonly XmlQueryType IntX = XmlQueryTypeFactory.Type(XmlTypeCode.Int, true);

		// Token: 0x040020A2 RID: 8354
		public static readonly XmlQueryType IntXS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.IntX, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x040020A3 RID: 8355
		public static readonly XmlQueryType IntegerX = XmlQueryTypeFactory.Type(XmlTypeCode.Integer, true);

		// Token: 0x040020A4 RID: 8356
		public static readonly XmlQueryType LongX = XmlQueryTypeFactory.Type(XmlTypeCode.Long, true);

		// Token: 0x040020A5 RID: 8357
		public static readonly XmlQueryType DecimalX = XmlQueryTypeFactory.Type(XmlTypeCode.Decimal, true);

		// Token: 0x040020A6 RID: 8358
		public static readonly XmlQueryType FloatX = XmlQueryTypeFactory.Type(XmlTypeCode.Float, true);

		// Token: 0x040020A7 RID: 8359
		public static readonly XmlQueryType Double = XmlQueryTypeFactory.Type(XmlTypeCode.Double, false);

		// Token: 0x040020A8 RID: 8360
		public static readonly XmlQueryType DoubleX = XmlQueryTypeFactory.Type(XmlTypeCode.Double, true);

		// Token: 0x040020A9 RID: 8361
		public static readonly XmlQueryType DateTimeX = XmlQueryTypeFactory.Type(XmlTypeCode.DateTime, true);

		// Token: 0x040020AA RID: 8362
		public static readonly XmlQueryType QNameX = XmlQueryTypeFactory.Type(XmlTypeCode.QName, true);

		// Token: 0x040020AB RID: 8363
		public static readonly XmlQueryType UntypedDocument = XmlQueryTypeFactory.ItemType.UntypedDocument;

		// Token: 0x040020AC RID: 8364
		public static readonly XmlQueryType UntypedElement = XmlQueryTypeFactory.ItemType.UntypedElement;

		// Token: 0x040020AD RID: 8365
		public static readonly XmlQueryType UntypedAttribute = XmlQueryTypeFactory.ItemType.UntypedAttribute;

		// Token: 0x040020AE RID: 8366
		public static readonly XmlQueryType UntypedNode = XmlQueryTypeFactory.Choice(new XmlQueryType[]
		{
			XmlQueryTypeFactory.UntypedDocument,
			XmlQueryTypeFactory.UntypedElement,
			XmlQueryTypeFactory.UntypedAttribute,
			XmlQueryTypeFactory.Namespace,
			XmlQueryTypeFactory.Text,
			XmlQueryTypeFactory.Comment,
			XmlQueryTypeFactory.PI
		});

		// Token: 0x040020AF RID: 8367
		public static readonly XmlQueryType UntypedNodeS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.UntypedNode, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x040020B0 RID: 8368
		public static readonly XmlQueryType NodeNotRtf = XmlQueryTypeFactory.ItemType.NodeNotRtf;

		// Token: 0x040020B1 RID: 8369
		public static readonly XmlQueryType NodeNotRtfQ = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.NodeNotRtf, XmlQueryCardinality.ZeroOrOne);

		// Token: 0x040020B2 RID: 8370
		public static readonly XmlQueryType NodeNotRtfS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.NodeNotRtf, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x040020B3 RID: 8371
		public static readonly XmlQueryType NodeSDod = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.NodeNotRtf, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x040020B4 RID: 8372
		private static readonly XmlTypeCode[] NodeKindToTypeCode = new XmlTypeCode[]
		{
			XmlTypeCode.Document,
			XmlTypeCode.Element,
			XmlTypeCode.Attribute,
			XmlTypeCode.Namespace,
			XmlTypeCode.Text,
			XmlTypeCode.Text,
			XmlTypeCode.Text,
			XmlTypeCode.ProcessingInstruction,
			XmlTypeCode.Comment,
			XmlTypeCode.Node
		};

		// Token: 0x020004D0 RID: 1232
		private sealed class ItemType : XmlQueryType
		{
			// Token: 0x06003210 RID: 12816 RVA: 0x0012274C File Offset: 0x0012094C
			static ItemType()
			{
				int num = 55;
				XmlQueryTypeFactory.ItemType.BuiltInItemTypes = new XmlQueryType[num];
				XmlQueryTypeFactory.ItemType.BuiltInItemTypesStrict = new XmlQueryType[num];
				for (int i = 0; i < num; i++)
				{
					XmlTypeCode xmlTypeCode = (XmlTypeCode)i;
					switch (i)
					{
					case 0:
						XmlQueryTypeFactory.ItemType.BuiltInItemTypes[i] = XmlQueryTypeFactory.ChoiceType.None;
						XmlQueryTypeFactory.ItemType.BuiltInItemTypesStrict[i] = XmlQueryTypeFactory.ChoiceType.None;
						break;
					case 1:
					case 2:
						XmlQueryTypeFactory.ItemType.BuiltInItemTypes[i] = new XmlQueryTypeFactory.ItemType(xmlTypeCode, XmlQualifiedNameTest.Wildcard, XmlSchemaComplexType.AnyType, false, false, false);
						XmlQueryTypeFactory.ItemType.BuiltInItemTypesStrict[i] = XmlQueryTypeFactory.ItemType.BuiltInItemTypes[i];
						break;
					case 3:
					case 4:
					case 6:
					case 7:
					case 8:
					case 9:
						XmlQueryTypeFactory.ItemType.BuiltInItemTypes[i] = new XmlQueryTypeFactory.ItemType(xmlTypeCode, XmlQualifiedNameTest.Wildcard, XmlSchemaComplexType.AnyType, false, false, true);
						XmlQueryTypeFactory.ItemType.BuiltInItemTypesStrict[i] = XmlQueryTypeFactory.ItemType.BuiltInItemTypes[i];
						break;
					case 5:
						XmlQueryTypeFactory.ItemType.BuiltInItemTypes[i] = new XmlQueryTypeFactory.ItemType(xmlTypeCode, XmlQualifiedNameTest.Wildcard, DatatypeImplementation.AnySimpleType, false, false, true);
						XmlQueryTypeFactory.ItemType.BuiltInItemTypesStrict[i] = XmlQueryTypeFactory.ItemType.BuiltInItemTypes[i];
						break;
					case 10:
						XmlQueryTypeFactory.ItemType.BuiltInItemTypes[i] = new XmlQueryTypeFactory.ItemType(xmlTypeCode, XmlQualifiedNameTest.Wildcard, DatatypeImplementation.AnyAtomicType, false, false, true);
						XmlQueryTypeFactory.ItemType.BuiltInItemTypesStrict[i] = XmlQueryTypeFactory.ItemType.BuiltInItemTypes[i];
						break;
					case 11:
						XmlQueryTypeFactory.ItemType.BuiltInItemTypes[i] = new XmlQueryTypeFactory.ItemType(xmlTypeCode, XmlQualifiedNameTest.Wildcard, DatatypeImplementation.UntypedAtomicType, false, true, true);
						XmlQueryTypeFactory.ItemType.BuiltInItemTypesStrict[i] = XmlQueryTypeFactory.ItemType.BuiltInItemTypes[i];
						break;
					default:
					{
						XmlSchemaType builtInSimpleType = XmlSchemaType.GetBuiltInSimpleType(xmlTypeCode);
						XmlQueryTypeFactory.ItemType.BuiltInItemTypes[i] = new XmlQueryTypeFactory.ItemType(xmlTypeCode, XmlQualifiedNameTest.Wildcard, builtInSimpleType, false, false, true);
						XmlQueryTypeFactory.ItemType.BuiltInItemTypesStrict[i] = new XmlQueryTypeFactory.ItemType(xmlTypeCode, XmlQualifiedNameTest.Wildcard, builtInSimpleType, false, true, true);
						break;
					}
					}
				}
				XmlQueryTypeFactory.ItemType.UntypedDocument = new XmlQueryTypeFactory.ItemType(XmlTypeCode.Document, XmlQualifiedNameTest.Wildcard, XmlSchemaComplexType.UntypedAnyType, false, false, true);
				XmlQueryTypeFactory.ItemType.UntypedElement = new XmlQueryTypeFactory.ItemType(XmlTypeCode.Element, XmlQualifiedNameTest.Wildcard, XmlSchemaComplexType.UntypedAnyType, false, false, true);
				XmlQueryTypeFactory.ItemType.UntypedAttribute = new XmlQueryTypeFactory.ItemType(XmlTypeCode.Attribute, XmlQualifiedNameTest.Wildcard, DatatypeImplementation.UntypedAtomicType, false, false, true);
				XmlQueryTypeFactory.ItemType.NodeNotRtf = new XmlQueryTypeFactory.ItemType(XmlTypeCode.Node, XmlQualifiedNameTest.Wildcard, XmlSchemaComplexType.AnyType, false, false, true);
				XmlQueryTypeFactory.ItemType.SpecialBuiltInItemTypes = new XmlQueryType[]
				{
					XmlQueryTypeFactory.ItemType.UntypedDocument,
					XmlQueryTypeFactory.ItemType.UntypedElement,
					XmlQueryTypeFactory.ItemType.UntypedAttribute,
					XmlQueryTypeFactory.ItemType.NodeNotRtf
				};
			}

			// Token: 0x06003211 RID: 12817 RVA: 0x0012297C File Offset: 0x00120B7C
			public static XmlQueryType Create(XmlTypeCode code, bool isStrict)
			{
				if (isStrict)
				{
					return XmlQueryTypeFactory.ItemType.BuiltInItemTypesStrict[(int)code];
				}
				return XmlQueryTypeFactory.ItemType.BuiltInItemTypes[(int)code];
			}

			// Token: 0x06003212 RID: 12818 RVA: 0x00122990 File Offset: 0x00120B90
			public static XmlQueryType Create(XmlSchemaSimpleType schemaType, bool isStrict)
			{
				XmlTypeCode typeCode = schemaType.Datatype.TypeCode;
				if (schemaType == XmlSchemaType.GetBuiltInSimpleType(typeCode))
				{
					return XmlQueryTypeFactory.ItemType.Create(typeCode, isStrict);
				}
				return new XmlQueryTypeFactory.ItemType(typeCode, XmlQualifiedNameTest.Wildcard, schemaType, false, isStrict, true);
			}

			// Token: 0x06003213 RID: 12819 RVA: 0x001229CC File Offset: 0x00120BCC
			public static XmlQueryType Create(XmlTypeCode code, XmlQualifiedNameTest nameTest, XmlSchemaType contentType, bool isNillable)
			{
				if (code - XmlTypeCode.Document <= 1)
				{
					if (nameTest.IsWildcard)
					{
						if (contentType == XmlSchemaComplexType.AnyType)
						{
							return XmlQueryTypeFactory.ItemType.Create(code, false);
						}
						if (contentType == XmlSchemaComplexType.UntypedAnyType)
						{
							if (code == XmlTypeCode.Element)
							{
								return XmlQueryTypeFactory.ItemType.UntypedElement;
							}
							if (code == XmlTypeCode.Document)
							{
								return XmlQueryTypeFactory.ItemType.UntypedDocument;
							}
						}
					}
					return new XmlQueryTypeFactory.ItemType(code, nameTest, contentType, isNillable, false, true);
				}
				if (code != XmlTypeCode.Attribute)
				{
					return XmlQueryTypeFactory.ItemType.Create(code, false);
				}
				if (nameTest.IsWildcard)
				{
					if (contentType == DatatypeImplementation.AnySimpleType)
					{
						return XmlQueryTypeFactory.ItemType.Create(code, false);
					}
					if (contentType == DatatypeImplementation.UntypedAtomicType)
					{
						return XmlQueryTypeFactory.ItemType.UntypedAttribute;
					}
				}
				return new XmlQueryTypeFactory.ItemType(code, nameTest, contentType, isNillable, false, true);
			}

			// Token: 0x06003214 RID: 12820 RVA: 0x00122A60 File Offset: 0x00120C60
			private ItemType(XmlTypeCode code, XmlQualifiedNameTest nameTest, XmlSchemaType schemaType, bool isNillable, bool isStrict, bool isNotRtf)
			{
				this.code = code;
				this.nameTest = nameTest;
				this.schemaType = schemaType;
				this.isNillable = isNillable;
				this.isStrict = isStrict;
				this.isNotRtf = isNotRtf;
				switch (code)
				{
				case XmlTypeCode.Item:
					this.nodeKinds = XmlNodeKindFlags.Any;
					return;
				case XmlTypeCode.Node:
					this.nodeKinds = XmlNodeKindFlags.Any;
					return;
				case XmlTypeCode.Document:
					this.nodeKinds = XmlNodeKindFlags.Document;
					return;
				case XmlTypeCode.Element:
					this.nodeKinds = XmlNodeKindFlags.Element;
					return;
				case XmlTypeCode.Attribute:
					this.nodeKinds = XmlNodeKindFlags.Attribute;
					return;
				case XmlTypeCode.Namespace:
					this.nodeKinds = XmlNodeKindFlags.Namespace;
					return;
				case XmlTypeCode.ProcessingInstruction:
					this.nodeKinds = XmlNodeKindFlags.PI;
					return;
				case XmlTypeCode.Comment:
					this.nodeKinds = XmlNodeKindFlags.Comment;
					return;
				case XmlTypeCode.Text:
					this.nodeKinds = XmlNodeKindFlags.Text;
					return;
				default:
					this.nodeKinds = XmlNodeKindFlags.None;
					return;
				}
			}

			// Token: 0x06003215 RID: 12821 RVA: 0x00122B24 File Offset: 0x00120D24
			public override void GetObjectData(BinaryWriter writer)
			{
				sbyte b = (sbyte)this.code;
				for (int i = 0; i < XmlQueryTypeFactory.ItemType.SpecialBuiltInItemTypes.Length; i++)
				{
					if (this == XmlQueryTypeFactory.ItemType.SpecialBuiltInItemTypes[i])
					{
						b = (sbyte)(~(sbyte)i);
						break;
					}
				}
				writer.Write(b);
				if (0 <= b)
				{
					writer.Write(this.isStrict);
				}
			}

			// Token: 0x06003216 RID: 12822 RVA: 0x00122B74 File Offset: 0x00120D74
			public static XmlQueryType Create(BinaryReader reader)
			{
				sbyte b = reader.ReadSByte();
				if (0 <= b)
				{
					return XmlQueryTypeFactory.ItemType.Create((XmlTypeCode)b, reader.ReadBoolean());
				}
				return XmlQueryTypeFactory.ItemType.SpecialBuiltInItemTypes[(int)(~(int)b)];
			}

			// Token: 0x17000A9E RID: 2718
			// (get) Token: 0x06003217 RID: 12823 RVA: 0x00122BA1 File Offset: 0x00120DA1
			public override XmlTypeCode TypeCode
			{
				get
				{
					return this.code;
				}
			}

			// Token: 0x17000A9F RID: 2719
			// (get) Token: 0x06003218 RID: 12824 RVA: 0x00122BA9 File Offset: 0x00120DA9
			public override XmlQualifiedNameTest NameTest
			{
				get
				{
					return this.nameTest;
				}
			}

			// Token: 0x17000AA0 RID: 2720
			// (get) Token: 0x06003219 RID: 12825 RVA: 0x00122BB1 File Offset: 0x00120DB1
			public override XmlSchemaType SchemaType
			{
				get
				{
					return this.schemaType;
				}
			}

			// Token: 0x17000AA1 RID: 2721
			// (get) Token: 0x0600321A RID: 12826 RVA: 0x00122BB9 File Offset: 0x00120DB9
			public override bool IsNillable
			{
				get
				{
					return this.isNillable;
				}
			}

			// Token: 0x17000AA2 RID: 2722
			// (get) Token: 0x0600321B RID: 12827 RVA: 0x00122BC1 File Offset: 0x00120DC1
			public override XmlNodeKindFlags NodeKinds
			{
				get
				{
					return this.nodeKinds;
				}
			}

			// Token: 0x17000AA3 RID: 2723
			// (get) Token: 0x0600321C RID: 12828 RVA: 0x00122BC9 File Offset: 0x00120DC9
			public override bool IsStrict
			{
				get
				{
					return this.isStrict;
				}
			}

			// Token: 0x17000AA4 RID: 2724
			// (get) Token: 0x0600321D RID: 12829 RVA: 0x00122BD1 File Offset: 0x00120DD1
			public override bool IsNotRtf
			{
				get
				{
					return this.isNotRtf;
				}
			}

			// Token: 0x17000AA5 RID: 2725
			// (get) Token: 0x0600321E RID: 12830 RVA: 0x0000226C File Offset: 0x0000046C
			public override bool IsDod
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000AA6 RID: 2726
			// (get) Token: 0x0600321F RID: 12831 RVA: 0x00122BD9 File Offset: 0x00120DD9
			public override XmlQueryCardinality Cardinality
			{
				get
				{
					return XmlQueryCardinality.One;
				}
			}

			// Token: 0x17000AA7 RID: 2727
			// (get) Token: 0x06003220 RID: 12832 RVA: 0x00002068 File Offset: 0x00000268
			public override XmlQueryType Prime
			{
				get
				{
					return this;
				}
			}

			// Token: 0x17000AA8 RID: 2728
			// (get) Token: 0x06003221 RID: 12833 RVA: 0x00122BE0 File Offset: 0x00120DE0
			public override XmlValueConverter ClrMapping
			{
				get
				{
					if (base.IsAtomicValue)
					{
						return this.SchemaType.ValueConverter;
					}
					if (base.IsNode)
					{
						return XmlNodeConverter.Node;
					}
					return XmlAnyConverter.Item;
				}
			}

			// Token: 0x17000AA9 RID: 2729
			// (get) Token: 0x06003222 RID: 12834 RVA: 0x00003242 File Offset: 0x00001442
			public override int Count
			{
				get
				{
					return 1;
				}
			}

			// Token: 0x17000AAA RID: 2730
			public override XmlQueryType this[int index]
			{
				get
				{
					if (index != 0)
					{
						throw new IndexOutOfRangeException();
					}
					return this;
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x040020B5 RID: 8373
			public static readonly XmlQueryType UntypedDocument;

			// Token: 0x040020B6 RID: 8374
			public static readonly XmlQueryType UntypedElement;

			// Token: 0x040020B7 RID: 8375
			public static readonly XmlQueryType UntypedAttribute;

			// Token: 0x040020B8 RID: 8376
			public static readonly XmlQueryType NodeNotRtf;

			// Token: 0x040020B9 RID: 8377
			private static XmlQueryType[] BuiltInItemTypes;

			// Token: 0x040020BA RID: 8378
			private static XmlQueryType[] BuiltInItemTypesStrict;

			// Token: 0x040020BB RID: 8379
			private static XmlQueryType[] SpecialBuiltInItemTypes;

			// Token: 0x040020BC RID: 8380
			private XmlTypeCode code;

			// Token: 0x040020BD RID: 8381
			private XmlQualifiedNameTest nameTest;

			// Token: 0x040020BE RID: 8382
			private XmlSchemaType schemaType;

			// Token: 0x040020BF RID: 8383
			private bool isNillable;

			// Token: 0x040020C0 RID: 8384
			private XmlNodeKindFlags nodeKinds;

			// Token: 0x040020C1 RID: 8385
			private bool isStrict;

			// Token: 0x040020C2 RID: 8386
			private bool isNotRtf;
		}

		// Token: 0x020004D1 RID: 1233
		private sealed class ChoiceType : XmlQueryType
		{
			// Token: 0x06003225 RID: 12837 RVA: 0x00122C18 File Offset: 0x00120E18
			public static XmlQueryType Create(XmlNodeKindFlags nodeKinds)
			{
				if (Bits.ExactlyOne((uint)nodeKinds))
				{
					return XmlQueryTypeFactory.ItemType.Create(XmlQueryTypeFactory.ChoiceType.NodeKindToTypeCode[Bits.LeastPosition((uint)nodeKinds)], false);
				}
				List<XmlQueryType> list = new List<XmlQueryType>();
				while (nodeKinds != XmlNodeKindFlags.None)
				{
					list.Add(XmlQueryTypeFactory.ItemType.Create(XmlQueryTypeFactory.ChoiceType.NodeKindToTypeCode[Bits.LeastPosition((uint)nodeKinds)], false));
					nodeKinds = (XmlNodeKindFlags)Bits.ClearLeast((uint)nodeKinds);
				}
				return XmlQueryTypeFactory.ChoiceType.Create(list);
			}

			// Token: 0x06003226 RID: 12838 RVA: 0x00122C71 File Offset: 0x00120E71
			public static XmlQueryType Create(List<XmlQueryType> members)
			{
				if (members.Count == 0)
				{
					return XmlQueryTypeFactory.ChoiceType.None;
				}
				if (members.Count == 1)
				{
					return members[0];
				}
				return new XmlQueryTypeFactory.ChoiceType(members);
			}

			// Token: 0x06003227 RID: 12839 RVA: 0x00122C98 File Offset: 0x00120E98
			private ChoiceType(List<XmlQueryType> members)
			{
				this.members = members;
				for (int i = 0; i < members.Count; i++)
				{
					XmlQueryType xmlQueryType = members[i];
					if (this.code == XmlTypeCode.None)
					{
						this.code = xmlQueryType.TypeCode;
						this.schemaType = xmlQueryType.SchemaType;
					}
					else if (base.IsNode && xmlQueryType.IsNode)
					{
						if (this.code == xmlQueryType.TypeCode)
						{
							if (this.code == XmlTypeCode.Element)
							{
								this.schemaType = XmlSchemaComplexType.AnyType;
							}
							else if (this.code == XmlTypeCode.Attribute)
							{
								this.schemaType = DatatypeImplementation.AnySimpleType;
							}
						}
						else
						{
							this.code = XmlTypeCode.Node;
							this.schemaType = null;
						}
					}
					else if (base.IsAtomicValue && xmlQueryType.IsAtomicValue)
					{
						this.code = XmlTypeCode.AnyAtomicType;
						this.schemaType = DatatypeImplementation.AnyAtomicType;
					}
					else
					{
						this.code = XmlTypeCode.Item;
						this.schemaType = null;
					}
					this.nodeKinds |= xmlQueryType.NodeKinds;
				}
			}

			// Token: 0x06003228 RID: 12840 RVA: 0x00122D98 File Offset: 0x00120F98
			public override void GetObjectData(BinaryWriter writer)
			{
				writer.Write(this.members.Count);
				for (int i = 0; i < this.members.Count; i++)
				{
					XmlQueryTypeFactory.Serialize(writer, this.members[i]);
				}
			}

			// Token: 0x06003229 RID: 12841 RVA: 0x00122DE0 File Offset: 0x00120FE0
			public static XmlQueryType Create(BinaryReader reader)
			{
				int num = reader.ReadInt32();
				List<XmlQueryType> list = new List<XmlQueryType>(num);
				for (int i = 0; i < num; i++)
				{
					list.Add(XmlQueryTypeFactory.Deserialize(reader));
				}
				return XmlQueryTypeFactory.ChoiceType.Create(list);
			}

			// Token: 0x17000AAB RID: 2731
			// (get) Token: 0x0600322A RID: 12842 RVA: 0x00122E19 File Offset: 0x00121019
			public override XmlTypeCode TypeCode
			{
				get
				{
					return this.code;
				}
			}

			// Token: 0x17000AAC RID: 2732
			// (get) Token: 0x0600322B RID: 12843 RVA: 0x00122E21 File Offset: 0x00121021
			public override XmlQualifiedNameTest NameTest
			{
				get
				{
					return XmlQualifiedNameTest.Wildcard;
				}
			}

			// Token: 0x17000AAD RID: 2733
			// (get) Token: 0x0600322C RID: 12844 RVA: 0x00122E28 File Offset: 0x00121028
			public override XmlSchemaType SchemaType
			{
				get
				{
					return this.schemaType;
				}
			}

			// Token: 0x17000AAE RID: 2734
			// (get) Token: 0x0600322D RID: 12845 RVA: 0x0000226C File Offset: 0x0000046C
			public override bool IsNillable
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000AAF RID: 2735
			// (get) Token: 0x0600322E RID: 12846 RVA: 0x00122E30 File Offset: 0x00121030
			public override XmlNodeKindFlags NodeKinds
			{
				get
				{
					return this.nodeKinds;
				}
			}

			// Token: 0x17000AB0 RID: 2736
			// (get) Token: 0x0600322F RID: 12847 RVA: 0x00122E38 File Offset: 0x00121038
			public override bool IsStrict
			{
				get
				{
					return this.members.Count == 0;
				}
			}

			// Token: 0x17000AB1 RID: 2737
			// (get) Token: 0x06003230 RID: 12848 RVA: 0x00122E48 File Offset: 0x00121048
			public override bool IsNotRtf
			{
				get
				{
					for (int i = 0; i < this.members.Count; i++)
					{
						if (!this.members[i].IsNotRtf)
						{
							return false;
						}
					}
					return true;
				}
			}

			// Token: 0x17000AB2 RID: 2738
			// (get) Token: 0x06003231 RID: 12849 RVA: 0x0000226C File Offset: 0x0000046C
			public override bool IsDod
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000AB3 RID: 2739
			// (get) Token: 0x06003232 RID: 12850 RVA: 0x00122E81 File Offset: 0x00121081
			public override XmlQueryCardinality Cardinality
			{
				get
				{
					if (this.TypeCode != XmlTypeCode.None)
					{
						return XmlQueryCardinality.One;
					}
					return XmlQueryCardinality.None;
				}
			}

			// Token: 0x17000AB4 RID: 2740
			// (get) Token: 0x06003233 RID: 12851 RVA: 0x00002068 File Offset: 0x00000268
			public override XmlQueryType Prime
			{
				get
				{
					return this;
				}
			}

			// Token: 0x17000AB5 RID: 2741
			// (get) Token: 0x06003234 RID: 12852 RVA: 0x00122E96 File Offset: 0x00121096
			public override XmlValueConverter ClrMapping
			{
				get
				{
					if (this.code == XmlTypeCode.None || this.code == XmlTypeCode.Item)
					{
						return XmlAnyConverter.Item;
					}
					if (base.IsAtomicValue)
					{
						return this.SchemaType.ValueConverter;
					}
					return XmlNodeConverter.Node;
				}
			}

			// Token: 0x17000AB6 RID: 2742
			// (get) Token: 0x06003235 RID: 12853 RVA: 0x00122EC8 File Offset: 0x001210C8
			public override int Count
			{
				get
				{
					return this.members.Count;
				}
			}

			// Token: 0x17000AB7 RID: 2743
			public override XmlQueryType this[int index]
			{
				get
				{
					return this.members[index];
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x040020C3 RID: 8387
			public static readonly XmlQueryType None = new XmlQueryTypeFactory.ChoiceType(new List<XmlQueryType>());

			// Token: 0x040020C4 RID: 8388
			private XmlTypeCode code;

			// Token: 0x040020C5 RID: 8389
			private XmlSchemaType schemaType;

			// Token: 0x040020C6 RID: 8390
			private XmlNodeKindFlags nodeKinds;

			// Token: 0x040020C7 RID: 8391
			private List<XmlQueryType> members;

			// Token: 0x040020C8 RID: 8392
			private static readonly XmlTypeCode[] NodeKindToTypeCode = new XmlTypeCode[]
			{
				XmlTypeCode.None,
				XmlTypeCode.Document,
				XmlTypeCode.Element,
				XmlTypeCode.Attribute,
				XmlTypeCode.Text,
				XmlTypeCode.Comment,
				XmlTypeCode.ProcessingInstruction,
				XmlTypeCode.Namespace
			};
		}

		// Token: 0x020004D2 RID: 1234
		private sealed class SequenceType : XmlQueryType
		{
			// Token: 0x06003239 RID: 12857 RVA: 0x00122F0C File Offset: 0x0012110C
			public static XmlQueryType Create(XmlQueryType prime, XmlQueryCardinality card)
			{
				if (prime.TypeCode == XmlTypeCode.None)
				{
					if (!(XmlQueryCardinality.Zero <= card))
					{
						return XmlQueryTypeFactory.None;
					}
					return XmlQueryTypeFactory.SequenceType.Zero;
				}
				else
				{
					if (card == XmlQueryCardinality.None)
					{
						return XmlQueryTypeFactory.None;
					}
					if (card == XmlQueryCardinality.Zero)
					{
						return XmlQueryTypeFactory.SequenceType.Zero;
					}
					if (card == XmlQueryCardinality.One)
					{
						return prime;
					}
					return new XmlQueryTypeFactory.SequenceType(prime, card);
				}
			}

			// Token: 0x0600323A RID: 12858 RVA: 0x00122F76 File Offset: 0x00121176
			private SequenceType(XmlQueryType prime, XmlQueryCardinality card)
			{
				this.prime = prime;
				this.card = card;
			}

			// Token: 0x0600323B RID: 12859 RVA: 0x00122F8C File Offset: 0x0012118C
			public override void GetObjectData(BinaryWriter writer)
			{
				writer.Write(this.IsDod);
				if (this.IsDod)
				{
					return;
				}
				XmlQueryTypeFactory.Serialize(writer, this.prime);
				this.card.GetObjectData(writer);
			}

			// Token: 0x0600323C RID: 12860 RVA: 0x00122FBC File Offset: 0x001211BC
			public static XmlQueryType Create(BinaryReader reader)
			{
				if (reader.ReadBoolean())
				{
					return XmlQueryTypeFactory.NodeSDod;
				}
				XmlQueryType xmlQueryType = XmlQueryTypeFactory.Deserialize(reader);
				XmlQueryCardinality xmlQueryCardinality = new XmlQueryCardinality(reader);
				return XmlQueryTypeFactory.SequenceType.Create(xmlQueryType, xmlQueryCardinality);
			}

			// Token: 0x17000AB8 RID: 2744
			// (get) Token: 0x0600323D RID: 12861 RVA: 0x00122FEB File Offset: 0x001211EB
			public override XmlTypeCode TypeCode
			{
				get
				{
					return this.prime.TypeCode;
				}
			}

			// Token: 0x17000AB9 RID: 2745
			// (get) Token: 0x0600323E RID: 12862 RVA: 0x00122FF8 File Offset: 0x001211F8
			public override XmlQualifiedNameTest NameTest
			{
				get
				{
					return this.prime.NameTest;
				}
			}

			// Token: 0x17000ABA RID: 2746
			// (get) Token: 0x0600323F RID: 12863 RVA: 0x00123005 File Offset: 0x00121205
			public override XmlSchemaType SchemaType
			{
				get
				{
					return this.prime.SchemaType;
				}
			}

			// Token: 0x17000ABB RID: 2747
			// (get) Token: 0x06003240 RID: 12864 RVA: 0x00123012 File Offset: 0x00121212
			public override bool IsNillable
			{
				get
				{
					return this.prime.IsNillable;
				}
			}

			// Token: 0x17000ABC RID: 2748
			// (get) Token: 0x06003241 RID: 12865 RVA: 0x0012301F File Offset: 0x0012121F
			public override XmlNodeKindFlags NodeKinds
			{
				get
				{
					return this.prime.NodeKinds;
				}
			}

			// Token: 0x17000ABD RID: 2749
			// (get) Token: 0x06003242 RID: 12866 RVA: 0x0012302C File Offset: 0x0012122C
			public override bool IsStrict
			{
				get
				{
					return this.prime.IsStrict;
				}
			}

			// Token: 0x17000ABE RID: 2750
			// (get) Token: 0x06003243 RID: 12867 RVA: 0x00123039 File Offset: 0x00121239
			public override bool IsNotRtf
			{
				get
				{
					return this.prime.IsNotRtf;
				}
			}

			// Token: 0x17000ABF RID: 2751
			// (get) Token: 0x06003244 RID: 12868 RVA: 0x00123046 File Offset: 0x00121246
			public override bool IsDod
			{
				get
				{
					return this == XmlQueryTypeFactory.NodeSDod;
				}
			}

			// Token: 0x17000AC0 RID: 2752
			// (get) Token: 0x06003245 RID: 12869 RVA: 0x00123050 File Offset: 0x00121250
			public override XmlQueryCardinality Cardinality
			{
				get
				{
					return this.card;
				}
			}

			// Token: 0x17000AC1 RID: 2753
			// (get) Token: 0x06003246 RID: 12870 RVA: 0x00123058 File Offset: 0x00121258
			public override XmlQueryType Prime
			{
				get
				{
					return this.prime;
				}
			}

			// Token: 0x17000AC2 RID: 2754
			// (get) Token: 0x06003247 RID: 12871 RVA: 0x00123060 File Offset: 0x00121260
			public override XmlValueConverter ClrMapping
			{
				get
				{
					if (this.converter == null)
					{
						this.converter = XmlListConverter.Create(this.prime.ClrMapping);
					}
					return this.converter;
				}
			}

			// Token: 0x17000AC3 RID: 2755
			// (get) Token: 0x06003248 RID: 12872 RVA: 0x00123086 File Offset: 0x00121286
			public override int Count
			{
				get
				{
					return this.prime.Count;
				}
			}

			// Token: 0x17000AC4 RID: 2756
			public override XmlQueryType this[int index]
			{
				get
				{
					return this.prime[index];
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x040020C9 RID: 8393
			public static readonly XmlQueryType Zero = new XmlQueryTypeFactory.SequenceType(XmlQueryTypeFactory.ChoiceType.None, XmlQueryCardinality.Zero);

			// Token: 0x040020CA RID: 8394
			private XmlQueryType prime;

			// Token: 0x040020CB RID: 8395
			private XmlQueryCardinality card;

			// Token: 0x040020CC RID: 8396
			private XmlValueConverter converter;
		}
	}
}
