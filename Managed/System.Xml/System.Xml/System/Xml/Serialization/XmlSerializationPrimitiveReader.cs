using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000303 RID: 771
	internal class XmlSerializationPrimitiveReader : XmlSerializationReader
	{
		// Token: 0x06001CB3 RID: 7347 RVA: 0x0009C294 File Offset: 0x0009A494
		internal object Read_string()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id1_string || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				if (base.ReadNull())
				{
					obj = null;
				}
				else
				{
					obj = base.Reader.ReadElementString();
				}
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x0009C30C File Offset: 0x0009A50C
		internal object Read_int()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id3_int || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				obj = XmlConvert.ToInt32(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x0009C384 File Offset: 0x0009A584
		internal object Read_boolean()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id4_boolean || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				obj = XmlConvert.ToBoolean(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x0009C3FC File Offset: 0x0009A5FC
		internal object Read_short()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id5_short || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				obj = XmlConvert.ToInt16(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x0009C474 File Offset: 0x0009A674
		internal object Read_long()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id6_long || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				obj = XmlConvert.ToInt64(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x0009C4EC File Offset: 0x0009A6EC
		internal object Read_float()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id7_float || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				obj = XmlConvert.ToSingle(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x0009C564 File Offset: 0x0009A764
		internal object Read_double()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id8_double || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				obj = XmlConvert.ToDouble(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x0009C5DC File Offset: 0x0009A7DC
		internal object Read_decimal()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id9_decimal || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				obj = XmlConvert.ToDecimal(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x0009C654 File Offset: 0x0009A854
		internal object Read_dateTime()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id10_dateTime || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				obj = XmlSerializationReader.ToDateTime(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x0009C6CC File Offset: 0x0009A8CC
		internal object Read_unsignedByte()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id11_unsignedByte || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				obj = XmlConvert.ToByte(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x0009C744 File Offset: 0x0009A944
		internal object Read_byte()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id12_byte || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				obj = XmlConvert.ToSByte(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x0009C7BC File Offset: 0x0009A9BC
		internal object Read_unsignedShort()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id13_unsignedShort || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				obj = XmlConvert.ToUInt16(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x0009C834 File Offset: 0x0009AA34
		internal object Read_unsignedInt()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id14_unsignedInt || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				obj = XmlConvert.ToUInt32(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x0009C8AC File Offset: 0x0009AAAC
		internal object Read_unsignedLong()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id15_unsignedLong || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				obj = XmlConvert.ToUInt64(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x0009C924 File Offset: 0x0009AB24
		internal object Read_base64Binary()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id16_base64Binary || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				if (base.ReadNull())
				{
					obj = null;
				}
				else
				{
					obj = base.ToByteArrayBase64(false);
				}
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x0009C998 File Offset: 0x0009AB98
		internal object Read_guid()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id17_guid || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				obj = XmlConvert.ToGuid(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x0009CA10 File Offset: 0x0009AC10
		internal object Read_TimeSpan()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id19_TimeSpan || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				if (base.Reader.IsEmptyElement)
				{
					base.Reader.Skip();
					obj = default(TimeSpan);
				}
				else
				{
					obj = XmlConvert.ToTimeSpan(base.Reader.ReadElementString());
				}
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x0009CAB0 File Offset: 0x0009ACB0
		internal object Read_char()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id18_char || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				obj = XmlSerializationReader.ToChar(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x0009CB28 File Offset: 0x0009AD28
		internal object Read_QName()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id1_QName || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				if (base.ReadNull())
				{
					obj = null;
				}
				else
				{
					obj = base.ReadElementQualifiedName();
				}
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x00002F50 File Offset: 0x00001150
		protected override void InitCallbacks()
		{
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x0009CB9C File Offset: 0x0009AD9C
		protected override void InitIDs()
		{
			this.id4_boolean = base.Reader.NameTable.Add("boolean");
			this.id14_unsignedInt = base.Reader.NameTable.Add("unsignedInt");
			this.id15_unsignedLong = base.Reader.NameTable.Add("unsignedLong");
			this.id7_float = base.Reader.NameTable.Add("float");
			this.id10_dateTime = base.Reader.NameTable.Add("dateTime");
			this.id6_long = base.Reader.NameTable.Add("long");
			this.id9_decimal = base.Reader.NameTable.Add("decimal");
			this.id8_double = base.Reader.NameTable.Add("double");
			this.id17_guid = base.Reader.NameTable.Add("guid");
			if (LocalAppContextSwitches.EnableTimeSpanSerialization)
			{
				this.id19_TimeSpan = base.Reader.NameTable.Add("TimeSpan");
			}
			this.id2_Item = base.Reader.NameTable.Add("");
			this.id13_unsignedShort = base.Reader.NameTable.Add("unsignedShort");
			this.id18_char = base.Reader.NameTable.Add("char");
			this.id3_int = base.Reader.NameTable.Add("int");
			this.id12_byte = base.Reader.NameTable.Add("byte");
			this.id16_base64Binary = base.Reader.NameTable.Add("base64Binary");
			this.id11_unsignedByte = base.Reader.NameTable.Add("unsignedByte");
			this.id5_short = base.Reader.NameTable.Add("short");
			this.id1_string = base.Reader.NameTable.Add("string");
			this.id1_QName = base.Reader.NameTable.Add("QName");
		}

		// Token: 0x04001665 RID: 5733
		private string id4_boolean;

		// Token: 0x04001666 RID: 5734
		private string id14_unsignedInt;

		// Token: 0x04001667 RID: 5735
		private string id15_unsignedLong;

		// Token: 0x04001668 RID: 5736
		private string id7_float;

		// Token: 0x04001669 RID: 5737
		private string id10_dateTime;

		// Token: 0x0400166A RID: 5738
		private string id6_long;

		// Token: 0x0400166B RID: 5739
		private string id9_decimal;

		// Token: 0x0400166C RID: 5740
		private string id8_double;

		// Token: 0x0400166D RID: 5741
		private string id17_guid;

		// Token: 0x0400166E RID: 5742
		private string id19_TimeSpan;

		// Token: 0x0400166F RID: 5743
		private string id2_Item;

		// Token: 0x04001670 RID: 5744
		private string id13_unsignedShort;

		// Token: 0x04001671 RID: 5745
		private string id18_char;

		// Token: 0x04001672 RID: 5746
		private string id3_int;

		// Token: 0x04001673 RID: 5747
		private string id12_byte;

		// Token: 0x04001674 RID: 5748
		private string id16_base64Binary;

		// Token: 0x04001675 RID: 5749
		private string id11_unsignedByte;

		// Token: 0x04001676 RID: 5750
		private string id5_short;

		// Token: 0x04001677 RID: 5751
		private string id1_string;

		// Token: 0x04001678 RID: 5752
		private string id1_QName;
	}
}
