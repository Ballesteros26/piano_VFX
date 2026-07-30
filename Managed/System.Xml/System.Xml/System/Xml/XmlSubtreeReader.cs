using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x0200010C RID: 268
	internal sealed class XmlSubtreeReader : XmlWrappingReader, IXmlLineInfo, IXmlNamespaceResolver
	{
		// Token: 0x06000A2B RID: 2603 RVA: 0x0002D664 File Offset: 0x0002B864
		internal XmlSubtreeReader(XmlReader reader)
			: base(reader)
		{
			this.initialDepth = reader.Depth;
			this.state = XmlSubtreeReader.State.Initial;
			this.nsManager = new XmlNamespaceManager(reader.NameTable);
			this.xmlns = reader.NameTable.Add("xmlns");
			this.xmlnsUri = reader.NameTable.Add("http://www.w3.org/2000/xmlns/");
			this.tmpNode = new XmlSubtreeReader.NodeData();
			this.tmpNode.Set(XmlNodeType.None, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
			this.SetCurrentNode(this.tmpNode);
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x0002D712 File Offset: 0x0002B912
		public override XmlNodeType NodeType
		{
			get
			{
				if (!this.useCurNode)
				{
					return this.reader.NodeType;
				}
				return this.curNode.type;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x0002D733 File Offset: 0x0002B933
		public override string Name
		{
			get
			{
				if (!this.useCurNode)
				{
					return this.reader.Name;
				}
				return this.curNode.name;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x0002D754 File Offset: 0x0002B954
		public override string LocalName
		{
			get
			{
				if (!this.useCurNode)
				{
					return this.reader.LocalName;
				}
				return this.curNode.localName;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x0002D775 File Offset: 0x0002B975
		public override string NamespaceURI
		{
			get
			{
				if (!this.useCurNode)
				{
					return this.reader.NamespaceURI;
				}
				return this.curNode.namespaceUri;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x0002D796 File Offset: 0x0002B996
		public override string Prefix
		{
			get
			{
				if (!this.useCurNode)
				{
					return this.reader.Prefix;
				}
				return this.curNode.prefix;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x0002D7B7 File Offset: 0x0002B9B7
		public override string Value
		{
			get
			{
				if (!this.useCurNode)
				{
					return this.reader.Value;
				}
				return this.curNode.value;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x0002D7D8 File Offset: 0x0002B9D8
		public override int Depth
		{
			get
			{
				int num = this.reader.Depth - this.initialDepth;
				if (this.curNsAttr != -1)
				{
					if (this.curNode.type == XmlNodeType.Text)
					{
						num += 2;
					}
					else
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x0002D81A File Offset: 0x0002BA1A
		public override string BaseURI
		{
			get
			{
				return this.reader.BaseURI;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000A34 RID: 2612 RVA: 0x0002D827 File Offset: 0x0002BA27
		public override bool IsEmptyElement
		{
			get
			{
				return this.reader.IsEmptyElement;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x0002D834 File Offset: 0x0002BA34
		public override bool EOF
		{
			get
			{
				return this.state == XmlSubtreeReader.State.EndOfFile || this.state == XmlSubtreeReader.State.Closed;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x0002D84A File Offset: 0x0002BA4A
		public override ReadState ReadState
		{
			get
			{
				if (this.reader.ReadState == ReadState.Error)
				{
					return ReadState.Error;
				}
				if (this.state <= XmlSubtreeReader.State.Closed)
				{
					return (ReadState)this.state;
				}
				return ReadState.Interactive;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x0002D86D File Offset: 0x0002BA6D
		public override XmlNameTable NameTable
		{
			get
			{
				return this.reader.NameTable;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x0002D87A File Offset: 0x0002BA7A
		public override int AttributeCount
		{
			get
			{
				if (!this.InAttributeActiveState)
				{
					return 0;
				}
				return this.reader.AttributeCount + this.nsAttrCount;
			}
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0002D898 File Offset: 0x0002BA98
		public override string GetAttribute(string name)
		{
			if (!this.InAttributeActiveState)
			{
				return null;
			}
			string attribute = this.reader.GetAttribute(name);
			if (attribute != null)
			{
				return attribute;
			}
			for (int i = 0; i < this.nsAttrCount; i++)
			{
				if (name == this.nsAttributes[i].name)
				{
					return this.nsAttributes[i].value;
				}
			}
			return null;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0002D8F8 File Offset: 0x0002BAF8
		public override string GetAttribute(string name, string namespaceURI)
		{
			if (!this.InAttributeActiveState)
			{
				return null;
			}
			string attribute = this.reader.GetAttribute(name, namespaceURI);
			if (attribute != null)
			{
				return attribute;
			}
			for (int i = 0; i < this.nsAttrCount; i++)
			{
				if (name == this.nsAttributes[i].localName && namespaceURI == this.xmlnsUri)
				{
					return this.nsAttributes[i].value;
				}
			}
			return null;
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0002D968 File Offset: 0x0002BB68
		public override string GetAttribute(int i)
		{
			if (!this.InAttributeActiveState)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			int attributeCount = this.reader.AttributeCount;
			if (i < attributeCount)
			{
				return this.reader.GetAttribute(i);
			}
			if (i - attributeCount < this.nsAttrCount)
			{
				return this.nsAttributes[i - attributeCount].value;
			}
			throw new ArgumentOutOfRangeException("i");
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0002D9CC File Offset: 0x0002BBCC
		public override bool MoveToAttribute(string name)
		{
			if (!this.InAttributeActiveState)
			{
				return false;
			}
			if (this.reader.MoveToAttribute(name))
			{
				this.curNsAttr = -1;
				this.useCurNode = false;
				return true;
			}
			for (int i = 0; i < this.nsAttrCount; i++)
			{
				if (name == this.nsAttributes[i].name)
				{
					this.MoveToNsAttribute(i);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0002DA34 File Offset: 0x0002BC34
		public override bool MoveToAttribute(string name, string ns)
		{
			if (!this.InAttributeActiveState)
			{
				return false;
			}
			if (this.reader.MoveToAttribute(name, ns))
			{
				this.curNsAttr = -1;
				this.useCurNode = false;
				return true;
			}
			for (int i = 0; i < this.nsAttrCount; i++)
			{
				if (name == this.nsAttributes[i].localName && ns == this.xmlnsUri)
				{
					this.MoveToNsAttribute(i);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0002DAA8 File Offset: 0x0002BCA8
		public override void MoveToAttribute(int i)
		{
			if (!this.InAttributeActiveState)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			int attributeCount = this.reader.AttributeCount;
			if (i < attributeCount)
			{
				this.reader.MoveToAttribute(i);
				this.curNsAttr = -1;
				this.useCurNode = false;
				return;
			}
			if (i - attributeCount < this.nsAttrCount)
			{
				this.MoveToNsAttribute(i - attributeCount);
				return;
			}
			throw new ArgumentOutOfRangeException("i");
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0002DB12 File Offset: 0x0002BD12
		public override bool MoveToFirstAttribute()
		{
			if (!this.InAttributeActiveState)
			{
				return false;
			}
			if (this.reader.MoveToFirstAttribute())
			{
				this.useCurNode = false;
				return true;
			}
			if (this.nsAttrCount > 0)
			{
				this.MoveToNsAttribute(0);
				return true;
			}
			return false;
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0002DB48 File Offset: 0x0002BD48
		public override bool MoveToNextAttribute()
		{
			if (!this.InAttributeActiveState)
			{
				return false;
			}
			if (this.curNsAttr == -1 && this.reader.MoveToNextAttribute())
			{
				return true;
			}
			if (this.curNsAttr + 1 < this.nsAttrCount)
			{
				this.MoveToNsAttribute(this.curNsAttr + 1);
				return true;
			}
			return false;
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0002DB98 File Offset: 0x0002BD98
		public override bool MoveToElement()
		{
			if (!this.InAttributeActiveState)
			{
				return false;
			}
			this.useCurNode = false;
			if (this.curNsAttr >= 0)
			{
				this.curNsAttr = -1;
				return true;
			}
			return this.reader.MoveToElement();
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0002DBC8 File Offset: 0x0002BDC8
		public override bool ReadAttributeValue()
		{
			if (!this.InAttributeActiveState)
			{
				return false;
			}
			if (this.curNsAttr == -1)
			{
				return this.reader.ReadAttributeValue();
			}
			if (this.curNode.type == XmlNodeType.Text)
			{
				return false;
			}
			this.tmpNode.type = XmlNodeType.Text;
			this.tmpNode.value = this.curNode.value;
			this.SetCurrentNode(this.tmpNode);
			return true;
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0002DC34 File Offset: 0x0002BE34
		public override bool Read()
		{
			switch (this.state)
			{
			case XmlSubtreeReader.State.Initial:
				this.useCurNode = false;
				this.state = XmlSubtreeReader.State.Interactive;
				this.ProcessNamespaces();
				return true;
			case XmlSubtreeReader.State.Interactive:
				break;
			case XmlSubtreeReader.State.Error:
			case XmlSubtreeReader.State.EndOfFile:
			case XmlSubtreeReader.State.Closed:
				return false;
			case XmlSubtreeReader.State.PopNamespaceScope:
				this.nsManager.PopScope();
				goto IL_00E5;
			case XmlSubtreeReader.State.ClearNsAttributes:
				goto IL_00E5;
			case XmlSubtreeReader.State.ReadElementContentAsBase64:
			case XmlSubtreeReader.State.ReadElementContentAsBinHex:
				return this.FinishReadElementContentAsBinary() && this.Read();
			case XmlSubtreeReader.State.ReadContentAsBase64:
			case XmlSubtreeReader.State.ReadContentAsBinHex:
				return this.FinishReadContentAsBinary() && this.Read();
			default:
				return false;
			}
			IL_0054:
			this.curNsAttr = -1;
			this.useCurNode = false;
			this.reader.MoveToElement();
			if (this.reader.Depth == this.initialDepth && (this.reader.NodeType == XmlNodeType.EndElement || (this.reader.NodeType == XmlNodeType.Element && this.reader.IsEmptyElement)))
			{
				this.state = XmlSubtreeReader.State.EndOfFile;
				this.SetEmptyNode();
				return false;
			}
			if (this.reader.Read())
			{
				this.ProcessNamespaces();
				return true;
			}
			this.SetEmptyNode();
			return false;
			IL_00E5:
			this.nsAttrCount = 0;
			this.state = XmlSubtreeReader.State.Interactive;
			goto IL_0054;
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0002DD5C File Offset: 0x0002BF5C
		public override void Close()
		{
			if (this.state == XmlSubtreeReader.State.Closed)
			{
				return;
			}
			try
			{
				if (this.state != XmlSubtreeReader.State.EndOfFile)
				{
					this.reader.MoveToElement();
					if (this.reader.Depth == this.initialDepth && this.reader.NodeType == XmlNodeType.Element && !this.reader.IsEmptyElement)
					{
						this.reader.Read();
					}
					while (this.reader.Depth > this.initialDepth && this.reader.Read())
					{
					}
				}
			}
			catch
			{
			}
			finally
			{
				this.curNsAttr = -1;
				this.useCurNode = false;
				this.state = XmlSubtreeReader.State.Closed;
				this.SetEmptyNode();
			}
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0002DE20 File Offset: 0x0002C020
		public override void Skip()
		{
			switch (this.state)
			{
			case XmlSubtreeReader.State.Initial:
				this.Read();
				return;
			case XmlSubtreeReader.State.Interactive:
				break;
			case XmlSubtreeReader.State.Error:
				return;
			case XmlSubtreeReader.State.EndOfFile:
			case XmlSubtreeReader.State.Closed:
				return;
			case XmlSubtreeReader.State.PopNamespaceScope:
				this.nsManager.PopScope();
				goto IL_011A;
			case XmlSubtreeReader.State.ClearNsAttributes:
				goto IL_011A;
			case XmlSubtreeReader.State.ReadElementContentAsBase64:
			case XmlSubtreeReader.State.ReadElementContentAsBinHex:
				if (this.FinishReadElementContentAsBinary())
				{
					this.Skip();
					return;
				}
				return;
			case XmlSubtreeReader.State.ReadContentAsBase64:
			case XmlSubtreeReader.State.ReadContentAsBinHex:
				if (this.FinishReadContentAsBinary())
				{
					this.Skip();
					return;
				}
				return;
			default:
				return;
			}
			IL_0042:
			this.curNsAttr = -1;
			this.useCurNode = false;
			this.reader.MoveToElement();
			if (this.reader.Depth == this.initialDepth)
			{
				if (this.reader.NodeType == XmlNodeType.Element && !this.reader.IsEmptyElement && this.reader.Read())
				{
					while (this.reader.NodeType != XmlNodeType.EndElement && this.reader.Depth > this.initialDepth)
					{
						this.reader.Skip();
					}
				}
				this.state = XmlSubtreeReader.State.EndOfFile;
				this.SetEmptyNode();
				return;
			}
			if (this.reader.NodeType == XmlNodeType.Element && !this.reader.IsEmptyElement)
			{
				this.nsManager.PopScope();
			}
			this.reader.Skip();
			this.ProcessNamespaces();
			return;
			IL_011A:
			this.nsAttrCount = 0;
			this.state = XmlSubtreeReader.State.Interactive;
			goto IL_0042;
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0002DF7C File Offset: 0x0002C17C
		public override object ReadContentAsObject()
		{
			object obj2;
			try
			{
				this.InitReadContentAsType("ReadContentAsObject");
				object obj = this.reader.ReadContentAsObject();
				this.FinishReadContentAsType();
				obj2 = obj;
			}
			catch
			{
				this.state = XmlSubtreeReader.State.Error;
				throw;
			}
			return obj2;
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0002DFC4 File Offset: 0x0002C1C4
		public override bool ReadContentAsBoolean()
		{
			bool flag2;
			try
			{
				this.InitReadContentAsType("ReadContentAsBoolean");
				bool flag = this.reader.ReadContentAsBoolean();
				this.FinishReadContentAsType();
				flag2 = flag;
			}
			catch
			{
				this.state = XmlSubtreeReader.State.Error;
				throw;
			}
			return flag2;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0002E00C File Offset: 0x0002C20C
		public override DateTime ReadContentAsDateTime()
		{
			DateTime dateTime2;
			try
			{
				this.InitReadContentAsType("ReadContentAsDateTime");
				DateTime dateTime = this.reader.ReadContentAsDateTime();
				this.FinishReadContentAsType();
				dateTime2 = dateTime;
			}
			catch
			{
				this.state = XmlSubtreeReader.State.Error;
				throw;
			}
			return dateTime2;
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0002E054 File Offset: 0x0002C254
		public override double ReadContentAsDouble()
		{
			double num2;
			try
			{
				this.InitReadContentAsType("ReadContentAsDouble");
				double num = this.reader.ReadContentAsDouble();
				this.FinishReadContentAsType();
				num2 = num;
			}
			catch
			{
				this.state = XmlSubtreeReader.State.Error;
				throw;
			}
			return num2;
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0002E09C File Offset: 0x0002C29C
		public override float ReadContentAsFloat()
		{
			float num2;
			try
			{
				this.InitReadContentAsType("ReadContentAsFloat");
				float num = this.reader.ReadContentAsFloat();
				this.FinishReadContentAsType();
				num2 = num;
			}
			catch
			{
				this.state = XmlSubtreeReader.State.Error;
				throw;
			}
			return num2;
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0002E0E4 File Offset: 0x0002C2E4
		public override decimal ReadContentAsDecimal()
		{
			decimal num2;
			try
			{
				this.InitReadContentAsType("ReadContentAsDecimal");
				decimal num = this.reader.ReadContentAsDecimal();
				this.FinishReadContentAsType();
				num2 = num;
			}
			catch
			{
				this.state = XmlSubtreeReader.State.Error;
				throw;
			}
			return num2;
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0002E12C File Offset: 0x0002C32C
		public override int ReadContentAsInt()
		{
			int num2;
			try
			{
				this.InitReadContentAsType("ReadContentAsInt");
				int num = this.reader.ReadContentAsInt();
				this.FinishReadContentAsType();
				num2 = num;
			}
			catch
			{
				this.state = XmlSubtreeReader.State.Error;
				throw;
			}
			return num2;
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0002E174 File Offset: 0x0002C374
		public override long ReadContentAsLong()
		{
			long num2;
			try
			{
				this.InitReadContentAsType("ReadContentAsLong");
				long num = this.reader.ReadContentAsLong();
				this.FinishReadContentAsType();
				num2 = num;
			}
			catch
			{
				this.state = XmlSubtreeReader.State.Error;
				throw;
			}
			return num2;
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0002E1BC File Offset: 0x0002C3BC
		public override string ReadContentAsString()
		{
			string text2;
			try
			{
				this.InitReadContentAsType("ReadContentAsString");
				string text = this.reader.ReadContentAsString();
				this.FinishReadContentAsType();
				text2 = text;
			}
			catch
			{
				this.state = XmlSubtreeReader.State.Error;
				throw;
			}
			return text2;
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0002E204 File Offset: 0x0002C404
		public override object ReadContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			object obj2;
			try
			{
				this.InitReadContentAsType("ReadContentAs");
				object obj = this.reader.ReadContentAs(returnType, namespaceResolver);
				this.FinishReadContentAsType();
				obj2 = obj;
			}
			catch
			{
				this.state = XmlSubtreeReader.State.Error;
				throw;
			}
			return obj2;
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x0002E250 File Offset: 0x0002C450
		public override bool CanReadBinaryContent
		{
			get
			{
				return this.reader.CanReadBinaryContent;
			}
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0002E260 File Offset: 0x0002C460
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			switch (this.state)
			{
			case XmlSubtreeReader.State.Initial:
			case XmlSubtreeReader.State.Error:
			case XmlSubtreeReader.State.EndOfFile:
			case XmlSubtreeReader.State.Closed:
				return 0;
			case XmlSubtreeReader.State.Interactive:
				this.state = XmlSubtreeReader.State.ReadContentAsBase64;
				break;
			case XmlSubtreeReader.State.PopNamespaceScope:
			case XmlSubtreeReader.State.ClearNsAttributes:
			{
				XmlNodeType nodeType = this.NodeType;
				switch (nodeType)
				{
				case XmlNodeType.Element:
					throw base.CreateReadContentAsException("ReadContentAsBase64");
				case XmlNodeType.Attribute:
					if (this.curNsAttr != -1 && this.reader.CanReadBinaryContent)
					{
						this.CheckBuffer(buffer, index, count);
						if (count == 0)
						{
							return 0;
						}
						if (this.nsIncReadOffset == 0)
						{
							if (this.binDecoder != null && this.binDecoder is Base64Decoder)
							{
								this.binDecoder.Reset();
							}
							else
							{
								this.binDecoder = new Base64Decoder();
							}
						}
						if (this.nsIncReadOffset == this.curNode.value.Length)
						{
							return 0;
						}
						this.binDecoder.SetNextOutputBuffer(buffer, index, count);
						this.nsIncReadOffset += this.binDecoder.Decode(this.curNode.value, this.nsIncReadOffset, this.curNode.value.Length - this.nsIncReadOffset);
						return this.binDecoder.DecodedCount;
					}
					break;
				case XmlNodeType.Text:
					break;
				default:
					if (nodeType != XmlNodeType.EndElement)
					{
						return 0;
					}
					return 0;
				}
				return this.reader.ReadContentAsBase64(buffer, index, count);
			}
			case XmlSubtreeReader.State.ReadElementContentAsBase64:
			case XmlSubtreeReader.State.ReadElementContentAsBinHex:
			case XmlSubtreeReader.State.ReadContentAsBinHex:
				throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadElementContentAsBase64 and ReadElementContentAsBinHex."));
			case XmlSubtreeReader.State.ReadContentAsBase64:
				break;
			default:
				return 0;
			}
			int num = this.reader.ReadContentAsBase64(buffer, index, count);
			if (num == 0)
			{
				this.state = XmlSubtreeReader.State.Interactive;
				this.ProcessNamespaces();
			}
			return num;
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0002E3FC File Offset: 0x0002C5FC
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			switch (this.state)
			{
			case XmlSubtreeReader.State.Initial:
			case XmlSubtreeReader.State.Error:
			case XmlSubtreeReader.State.EndOfFile:
			case XmlSubtreeReader.State.Closed:
				return 0;
			case XmlSubtreeReader.State.Interactive:
			case XmlSubtreeReader.State.PopNamespaceScope:
			case XmlSubtreeReader.State.ClearNsAttributes:
				if (!this.InitReadElementContentAsBinary(XmlSubtreeReader.State.ReadElementContentAsBase64))
				{
					return 0;
				}
				break;
			case XmlSubtreeReader.State.ReadElementContentAsBase64:
				break;
			case XmlSubtreeReader.State.ReadElementContentAsBinHex:
			case XmlSubtreeReader.State.ReadContentAsBase64:
			case XmlSubtreeReader.State.ReadContentAsBinHex:
				throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadElementContentAsBase64 and ReadElementContentAsBinHex."));
			default:
				return 0;
			}
			int num = this.reader.ReadContentAsBase64(buffer, index, count);
			if (num > 0 || count == 0)
			{
				return num;
			}
			if (this.NodeType != XmlNodeType.EndElement)
			{
				throw new XmlException("'{0}' is an invalid XmlNodeType.", this.reader.NodeType.ToString(), this.reader as IXmlLineInfo);
			}
			this.state = XmlSubtreeReader.State.Interactive;
			this.ProcessNamespaces();
			if (this.reader.Depth == this.initialDepth)
			{
				this.state = XmlSubtreeReader.State.EndOfFile;
				this.SetEmptyNode();
			}
			else
			{
				this.Read();
			}
			return 0;
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0002E4F0 File Offset: 0x0002C6F0
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			switch (this.state)
			{
			case XmlSubtreeReader.State.Initial:
			case XmlSubtreeReader.State.Error:
			case XmlSubtreeReader.State.EndOfFile:
			case XmlSubtreeReader.State.Closed:
				return 0;
			case XmlSubtreeReader.State.Interactive:
				this.state = XmlSubtreeReader.State.ReadContentAsBinHex;
				break;
			case XmlSubtreeReader.State.PopNamespaceScope:
			case XmlSubtreeReader.State.ClearNsAttributes:
			{
				XmlNodeType nodeType = this.NodeType;
				switch (nodeType)
				{
				case XmlNodeType.Element:
					throw base.CreateReadContentAsException("ReadContentAsBinHex");
				case XmlNodeType.Attribute:
					if (this.curNsAttr != -1 && this.reader.CanReadBinaryContent)
					{
						this.CheckBuffer(buffer, index, count);
						if (count == 0)
						{
							return 0;
						}
						if (this.nsIncReadOffset == 0)
						{
							if (this.binDecoder != null && this.binDecoder is BinHexDecoder)
							{
								this.binDecoder.Reset();
							}
							else
							{
								this.binDecoder = new BinHexDecoder();
							}
						}
						if (this.nsIncReadOffset == this.curNode.value.Length)
						{
							return 0;
						}
						this.binDecoder.SetNextOutputBuffer(buffer, index, count);
						this.nsIncReadOffset += this.binDecoder.Decode(this.curNode.value, this.nsIncReadOffset, this.curNode.value.Length - this.nsIncReadOffset);
						return this.binDecoder.DecodedCount;
					}
					break;
				case XmlNodeType.Text:
					break;
				default:
					if (nodeType != XmlNodeType.EndElement)
					{
						return 0;
					}
					return 0;
				}
				return this.reader.ReadContentAsBinHex(buffer, index, count);
			}
			case XmlSubtreeReader.State.ReadElementContentAsBase64:
			case XmlSubtreeReader.State.ReadElementContentAsBinHex:
			case XmlSubtreeReader.State.ReadContentAsBase64:
				throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadElementContentAsBase64 and ReadElementContentAsBinHex."));
			case XmlSubtreeReader.State.ReadContentAsBinHex:
				break;
			default:
				return 0;
			}
			int num = this.reader.ReadContentAsBinHex(buffer, index, count);
			if (num == 0)
			{
				this.state = XmlSubtreeReader.State.Interactive;
				this.ProcessNamespaces();
			}
			return num;
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0002E68C File Offset: 0x0002C88C
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			switch (this.state)
			{
			case XmlSubtreeReader.State.Initial:
			case XmlSubtreeReader.State.Error:
			case XmlSubtreeReader.State.EndOfFile:
			case XmlSubtreeReader.State.Closed:
				return 0;
			case XmlSubtreeReader.State.Interactive:
			case XmlSubtreeReader.State.PopNamespaceScope:
			case XmlSubtreeReader.State.ClearNsAttributes:
				if (!this.InitReadElementContentAsBinary(XmlSubtreeReader.State.ReadElementContentAsBinHex))
				{
					return 0;
				}
				break;
			case XmlSubtreeReader.State.ReadElementContentAsBase64:
			case XmlSubtreeReader.State.ReadContentAsBase64:
			case XmlSubtreeReader.State.ReadContentAsBinHex:
				throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadElementContentAsBase64 and ReadElementContentAsBinHex."));
			case XmlSubtreeReader.State.ReadElementContentAsBinHex:
				break;
			default:
				return 0;
			}
			int num = this.reader.ReadContentAsBinHex(buffer, index, count);
			if (num > 0 || count == 0)
			{
				return num;
			}
			if (this.NodeType != XmlNodeType.EndElement)
			{
				throw new XmlException("'{0}' is an invalid XmlNodeType.", this.reader.NodeType.ToString(), this.reader as IXmlLineInfo);
			}
			this.state = XmlSubtreeReader.State.Interactive;
			this.ProcessNamespaces();
			if (this.reader.Depth == this.initialDepth)
			{
				this.state = XmlSubtreeReader.State.EndOfFile;
				this.SetEmptyNode();
			}
			else
			{
				this.Read();
			}
			return 0;
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x0002E77E File Offset: 0x0002C97E
		public override bool CanReadValueChunk
		{
			get
			{
				return this.reader.CanReadValueChunk;
			}
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0002E78C File Offset: 0x0002C98C
		public override int ReadValueChunk(char[] buffer, int index, int count)
		{
			switch (this.state)
			{
			case XmlSubtreeReader.State.Initial:
			case XmlSubtreeReader.State.Error:
			case XmlSubtreeReader.State.EndOfFile:
			case XmlSubtreeReader.State.Closed:
				return 0;
			case XmlSubtreeReader.State.Interactive:
				break;
			case XmlSubtreeReader.State.PopNamespaceScope:
			case XmlSubtreeReader.State.ClearNsAttributes:
				if (this.curNsAttr != -1 && this.reader.CanReadValueChunk)
				{
					this.CheckBuffer(buffer, index, count);
					int num = this.curNode.value.Length - this.nsIncReadOffset;
					if (num > count)
					{
						num = count;
					}
					if (num > 0)
					{
						this.curNode.value.CopyTo(this.nsIncReadOffset, buffer, index, num);
					}
					this.nsIncReadOffset += num;
					return num;
				}
				break;
			case XmlSubtreeReader.State.ReadElementContentAsBase64:
			case XmlSubtreeReader.State.ReadElementContentAsBinHex:
			case XmlSubtreeReader.State.ReadContentAsBase64:
			case XmlSubtreeReader.State.ReadContentAsBinHex:
				throw new InvalidOperationException(Res.GetString("ReadValueChunk calls cannot be mixed with ReadContentAsBase64 or ReadContentAsBinHex."));
			default:
				return 0;
			}
			return this.reader.ReadValueChunk(buffer, index, count);
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0002E863 File Offset: 0x0002CA63
		public override string LookupNamespace(string prefix)
		{
			return ((IXmlNamespaceResolver)this).LookupNamespace(prefix);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0002E86C File Offset: 0x0002CA6C
		protected override void Dispose(bool disposing)
		{
			this.Close();
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x0002E874 File Offset: 0x0002CA74
		int IXmlLineInfo.LineNumber
		{
			get
			{
				if (!this.useCurNode)
				{
					IXmlLineInfo xmlLineInfo = this.reader as IXmlLineInfo;
					if (xmlLineInfo != null)
					{
						return xmlLineInfo.LineNumber;
					}
				}
				return 0;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x0002E8A0 File Offset: 0x0002CAA0
		int IXmlLineInfo.LinePosition
		{
			get
			{
				if (!this.useCurNode)
				{
					IXmlLineInfo xmlLineInfo = this.reader as IXmlLineInfo;
					if (xmlLineInfo != null)
					{
						return xmlLineInfo.LinePosition;
					}
				}
				return 0;
			}
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0002E8CC File Offset: 0x0002CACC
		bool IXmlLineInfo.HasLineInfo()
		{
			return this.reader is IXmlLineInfo;
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0002E8DC File Offset: 0x0002CADC
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			if (!this.InNamespaceActiveState)
			{
				return new Dictionary<string, string>();
			}
			return this.nsManager.GetNamespacesInScope(scope);
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0002E8F8 File Offset: 0x0002CAF8
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			if (!this.InNamespaceActiveState)
			{
				return null;
			}
			return this.nsManager.LookupNamespace(prefix);
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0002E910 File Offset: 0x0002CB10
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			if (!this.InNamespaceActiveState)
			{
				return null;
			}
			return this.nsManager.LookupPrefix(namespaceName);
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0002E928 File Offset: 0x0002CB28
		private void ProcessNamespaces()
		{
			XmlNodeType nodeType = this.reader.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType != XmlNodeType.EndElement)
				{
					return;
				}
				this.state = XmlSubtreeReader.State.PopNamespaceScope;
			}
			else
			{
				this.nsManager.PushScope();
				string text = this.reader.Prefix;
				string text2 = this.reader.NamespaceURI;
				if (this.nsManager.LookupNamespace(text) != text2)
				{
					this.AddNamespace(text, text2);
				}
				if (this.reader.MoveToFirstAttribute())
				{
					do
					{
						text = this.reader.Prefix;
						text2 = this.reader.NamespaceURI;
						if (Ref.Equal(text2, this.xmlnsUri))
						{
							if (text.Length == 0)
							{
								this.nsManager.AddNamespace(string.Empty, this.reader.Value);
								this.RemoveNamespace(string.Empty, this.xmlns);
							}
							else
							{
								text = this.reader.LocalName;
								this.nsManager.AddNamespace(text, this.reader.Value);
								this.RemoveNamespace(this.xmlns, text);
							}
						}
						else if (text.Length != 0 && this.nsManager.LookupNamespace(text) != text2)
						{
							this.AddNamespace(text, text2);
						}
					}
					while (this.reader.MoveToNextAttribute());
					this.reader.MoveToElement();
				}
				if (this.reader.IsEmptyElement)
				{
					this.state = XmlSubtreeReader.State.PopNamespaceScope;
					return;
				}
			}
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0002EA88 File Offset: 0x0002CC88
		private void AddNamespace(string prefix, string ns)
		{
			this.nsManager.AddNamespace(prefix, ns);
			int num = this.nsAttrCount;
			this.nsAttrCount = num + 1;
			int num2 = num;
			if (this.nsAttributes == null)
			{
				this.nsAttributes = new XmlSubtreeReader.NodeData[this.InitialNamespaceAttributeCount];
			}
			if (num2 == this.nsAttributes.Length)
			{
				XmlSubtreeReader.NodeData[] array = new XmlSubtreeReader.NodeData[this.nsAttributes.Length * 2];
				Array.Copy(this.nsAttributes, 0, array, 0, num2);
				this.nsAttributes = array;
			}
			if (this.nsAttributes[num2] == null)
			{
				this.nsAttributes[num2] = new XmlSubtreeReader.NodeData();
			}
			if (prefix.Length == 0)
			{
				this.nsAttributes[num2].Set(XmlNodeType.Attribute, this.xmlns, string.Empty, this.xmlns, this.xmlnsUri, ns);
			}
			else
			{
				this.nsAttributes[num2].Set(XmlNodeType.Attribute, prefix, this.xmlns, this.reader.NameTable.Add(this.xmlns + ":" + prefix), this.xmlnsUri, ns);
			}
			this.state = XmlSubtreeReader.State.ClearNsAttributes;
			this.curNsAttr = -1;
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0002EB90 File Offset: 0x0002CD90
		private void RemoveNamespace(string prefix, string localName)
		{
			for (int i = 0; i < this.nsAttrCount; i++)
			{
				if (Ref.Equal(prefix, this.nsAttributes[i].prefix) && Ref.Equal(localName, this.nsAttributes[i].localName))
				{
					if (i < this.nsAttrCount - 1)
					{
						XmlSubtreeReader.NodeData nodeData = this.nsAttributes[i];
						this.nsAttributes[i] = this.nsAttributes[this.nsAttrCount - 1];
						this.nsAttributes[this.nsAttrCount - 1] = nodeData;
					}
					this.nsAttrCount--;
					return;
				}
			}
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0002EC25 File Offset: 0x0002CE25
		private void MoveToNsAttribute(int index)
		{
			this.reader.MoveToElement();
			this.curNsAttr = index;
			this.nsIncReadOffset = 0;
			this.SetCurrentNode(this.nsAttributes[index]);
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0002EC50 File Offset: 0x0002CE50
		private bool InitReadElementContentAsBinary(XmlSubtreeReader.State binaryState)
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw this.reader.CreateReadElementContentAsException("ReadElementContentAsBase64");
			}
			bool isEmptyElement = this.IsEmptyElement;
			if (!this.Read() || isEmptyElement)
			{
				return false;
			}
			XmlNodeType nodeType = this.NodeType;
			if (nodeType == XmlNodeType.Element)
			{
				throw new XmlException("'{0}' is an invalid XmlNodeType.", this.reader.NodeType.ToString(), this.reader as IXmlLineInfo);
			}
			if (nodeType != XmlNodeType.EndElement)
			{
				this.state = binaryState;
				return true;
			}
			this.ProcessNamespaces();
			this.Read();
			return false;
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0002ECE8 File Offset: 0x0002CEE8
		private bool FinishReadElementContentAsBinary()
		{
			byte[] array = new byte[256];
			if (this.state == XmlSubtreeReader.State.ReadElementContentAsBase64)
			{
				while (this.reader.ReadContentAsBase64(array, 0, 256) > 0)
				{
				}
			}
			else
			{
				while (this.reader.ReadContentAsBinHex(array, 0, 256) > 0)
				{
				}
			}
			if (this.NodeType != XmlNodeType.EndElement)
			{
				throw new XmlException("'{0}' is an invalid XmlNodeType.", this.reader.NodeType.ToString(), this.reader as IXmlLineInfo);
			}
			this.state = XmlSubtreeReader.State.Interactive;
			this.ProcessNamespaces();
			if (this.reader.Depth == this.initialDepth)
			{
				this.state = XmlSubtreeReader.State.EndOfFile;
				this.SetEmptyNode();
				return false;
			}
			return this.Read();
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0002EDA4 File Offset: 0x0002CFA4
		private bool FinishReadContentAsBinary()
		{
			byte[] array = new byte[256];
			if (this.state == XmlSubtreeReader.State.ReadContentAsBase64)
			{
				while (this.reader.ReadContentAsBase64(array, 0, 256) > 0)
				{
				}
			}
			else
			{
				while (this.reader.ReadContentAsBinHex(array, 0, 256) > 0)
				{
				}
			}
			this.state = XmlSubtreeReader.State.Interactive;
			this.ProcessNamespaces();
			if (this.reader.Depth == this.initialDepth)
			{
				this.state = XmlSubtreeReader.State.EndOfFile;
				this.SetEmptyNode();
				return false;
			}
			return true;
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x0002EE22 File Offset: 0x0002D022
		private bool InAttributeActiveState
		{
			get
			{
				return (98 & (1 << (int)this.state)) != 0;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x0002EE35 File Offset: 0x0002D035
		private bool InNamespaceActiveState
		{
			get
			{
				return (2018 & (1 << (int)this.state)) != 0;
			}
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0002EE4B File Offset: 0x0002D04B
		private void SetEmptyNode()
		{
			this.tmpNode.type = XmlNodeType.None;
			this.tmpNode.value = string.Empty;
			this.curNode = this.tmpNode;
			this.useCurNode = true;
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0002EE7C File Offset: 0x0002D07C
		private void SetCurrentNode(XmlSubtreeReader.NodeData node)
		{
			this.curNode = node;
			this.useCurNode = true;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0002EE8C File Offset: 0x0002D08C
		private void InitReadContentAsType(string methodName)
		{
			switch (this.state)
			{
			case XmlSubtreeReader.State.Initial:
			case XmlSubtreeReader.State.Error:
			case XmlSubtreeReader.State.EndOfFile:
			case XmlSubtreeReader.State.Closed:
				throw new InvalidOperationException(Res.GetString("The XmlReader is closed or in error state."));
			case XmlSubtreeReader.State.Interactive:
				return;
			case XmlSubtreeReader.State.PopNamespaceScope:
			case XmlSubtreeReader.State.ClearNsAttributes:
				return;
			case XmlSubtreeReader.State.ReadElementContentAsBase64:
			case XmlSubtreeReader.State.ReadElementContentAsBinHex:
			case XmlSubtreeReader.State.ReadContentAsBase64:
			case XmlSubtreeReader.State.ReadContentAsBinHex:
				throw new InvalidOperationException(Res.GetString("ReadValueChunk calls cannot be mixed with ReadContentAsBase64 or ReadContentAsBinHex."));
			default:
				throw base.CreateReadContentAsException(methodName);
			}
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0002EF00 File Offset: 0x0002D100
		private void FinishReadContentAsType()
		{
			XmlNodeType nodeType = this.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType != XmlNodeType.Attribute)
				{
					if (nodeType != XmlNodeType.EndElement)
					{
						return;
					}
					this.state = XmlSubtreeReader.State.PopNamespaceScope;
				}
				return;
			}
			this.ProcessNamespaces();
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0002EF30 File Offset: 0x0002D130
		private void CheckBuffer(Array buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0002EF7F File Offset: 0x0002D17F
		public override Task<string> GetValueAsync()
		{
			if (this.useCurNode)
			{
				return Task.FromResult<string>(this.curNode.value);
			}
			return this.reader.GetValueAsync();
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0002EFA8 File Offset: 0x0002D1A8
		public override async Task<bool> ReadAsync()
		{
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter;
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			switch (this.state)
			{
			case XmlSubtreeReader.State.Initial:
				this.useCurNode = false;
				this.state = XmlSubtreeReader.State.Interactive;
				this.ProcessNamespaces();
				return true;
			case XmlSubtreeReader.State.Interactive:
				break;
			case XmlSubtreeReader.State.Error:
			case XmlSubtreeReader.State.EndOfFile:
			case XmlSubtreeReader.State.Closed:
				return false;
			case XmlSubtreeReader.State.PopNamespaceScope:
				this.nsManager.PopScope();
				goto IL_0188;
			case XmlSubtreeReader.State.ClearNsAttributes:
				goto IL_0188;
			case XmlSubtreeReader.State.ReadElementContentAsBase64:
			case XmlSubtreeReader.State.ReadElementContentAsBinHex:
				configuredTaskAwaiter = this.FinishReadElementContentAsBinaryAsync().ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (!configuredTaskAwaiter.GetResult())
				{
					return false;
				}
				return await this.ReadAsync().ConfigureAwait(false);
			case XmlSubtreeReader.State.ReadContentAsBase64:
			case XmlSubtreeReader.State.ReadContentAsBinHex:
				configuredTaskAwaiter = this.FinishReadContentAsBinaryAsync().ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (!configuredTaskAwaiter.GetResult())
				{
					return false;
				}
				return await this.ReadAsync().ConfigureAwait(false);
			default:
				return false;
			}
			IL_0081:
			this.curNsAttr = -1;
			this.useCurNode = false;
			this.reader.MoveToElement();
			if (this.reader.Depth == this.initialDepth && (this.reader.NodeType == XmlNodeType.EndElement || (this.reader.NodeType == XmlNodeType.Element && this.reader.IsEmptyElement)))
			{
				this.state = XmlSubtreeReader.State.EndOfFile;
				this.SetEmptyNode();
				return false;
			}
			configuredTaskAwaiter = this.reader.ReadAsync().ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (configuredTaskAwaiter.GetResult())
			{
				this.ProcessNamespaces();
				return true;
			}
			this.SetEmptyNode();
			return false;
			IL_0188:
			this.nsAttrCount = 0;
			this.state = XmlSubtreeReader.State.Interactive;
			goto IL_0081;
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0002EFF0 File Offset: 0x0002D1F0
		public override async Task SkipAsync()
		{
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			switch (this.state)
			{
			case XmlSubtreeReader.State.Initial:
				await this.ReadAsync().ConfigureAwait(false);
				return;
			case XmlSubtreeReader.State.Interactive:
				break;
			case XmlSubtreeReader.State.Error:
				return;
			case XmlSubtreeReader.State.EndOfFile:
			case XmlSubtreeReader.State.Closed:
				goto IL_02DD;
			case XmlSubtreeReader.State.PopNamespaceScope:
				this.nsManager.PopScope();
				goto IL_02EE;
			case XmlSubtreeReader.State.ClearNsAttributes:
				goto IL_02EE;
			case XmlSubtreeReader.State.ReadElementContentAsBase64:
			case XmlSubtreeReader.State.ReadElementContentAsBinHex:
			{
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.FinishReadElementContentAsBinaryAsync().ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (configuredTaskAwaiter.GetResult())
				{
					await this.SkipAsync().ConfigureAwait(false);
					return;
				}
				return;
			}
			case XmlSubtreeReader.State.ReadContentAsBase64:
			case XmlSubtreeReader.State.ReadContentAsBinHex:
			{
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.FinishReadContentAsBinaryAsync().ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (configuredTaskAwaiter.GetResult())
				{
					await this.SkipAsync().ConfigureAwait(false);
					return;
				}
				return;
			}
			default:
				return;
			}
			IL_00DD:
			this.curNsAttr = -1;
			this.useCurNode = false;
			this.reader.MoveToElement();
			if (this.reader.Depth == this.initialDepth)
			{
				if (this.reader.NodeType == XmlNodeType.Element && !this.reader.IsEmptyElement)
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.reader.ReadAsync().ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
					}
					if (configuredTaskAwaiter.GetResult())
					{
						while (this.reader.NodeType != XmlNodeType.EndElement && this.reader.Depth > this.initialDepth)
						{
							await this.reader.SkipAsync().ConfigureAwait(false);
						}
					}
				}
				this.state = XmlSubtreeReader.State.EndOfFile;
				this.SetEmptyNode();
			}
			else
			{
				if (this.reader.NodeType == XmlNodeType.Element && !this.reader.IsEmptyElement)
				{
					this.nsManager.PopScope();
				}
				await this.reader.SkipAsync().ConfigureAwait(false);
				this.ProcessNamespaces();
			}
			IL_02DD:
			return;
			IL_02EE:
			this.nsAttrCount = 0;
			this.state = XmlSubtreeReader.State.Interactive;
			goto IL_00DD;
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0002F038 File Offset: 0x0002D238
		public override async Task<object> ReadContentAsObjectAsync()
		{
			object obj2;
			try
			{
				this.InitReadContentAsType("ReadContentAsObject");
				object obj = await this.reader.ReadContentAsObjectAsync().ConfigureAwait(false);
				this.FinishReadContentAsType();
				obj2 = obj;
			}
			catch
			{
				this.state = XmlSubtreeReader.State.Error;
				throw;
			}
			return obj2;
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0002F080 File Offset: 0x0002D280
		public override async Task<string> ReadContentAsStringAsync()
		{
			string text2;
			try
			{
				this.InitReadContentAsType("ReadContentAsString");
				string text = await this.reader.ReadContentAsStringAsync().ConfigureAwait(false);
				this.FinishReadContentAsType();
				text2 = text;
			}
			catch
			{
				this.state = XmlSubtreeReader.State.Error;
				throw;
			}
			return text2;
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0002F0C8 File Offset: 0x0002D2C8
		public override async Task<object> ReadContentAsAsync(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			object obj2;
			try
			{
				this.InitReadContentAsType("ReadContentAs");
				object obj = await this.reader.ReadContentAsAsync(returnType, namespaceResolver).ConfigureAwait(false);
				this.FinishReadContentAsType();
				obj2 = obj;
			}
			catch
			{
				this.state = XmlSubtreeReader.State.Error;
				throw;
			}
			return obj2;
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0002F120 File Offset: 0x0002D320
		public override async Task<int> ReadContentAsBase64Async(byte[] buffer, int index, int count)
		{
			switch (this.state)
			{
			case XmlSubtreeReader.State.Initial:
			case XmlSubtreeReader.State.Error:
			case XmlSubtreeReader.State.EndOfFile:
			case XmlSubtreeReader.State.Closed:
				return 0;
			case XmlSubtreeReader.State.Interactive:
				this.state = XmlSubtreeReader.State.ReadContentAsBase64;
				break;
			case XmlSubtreeReader.State.PopNamespaceScope:
			case XmlSubtreeReader.State.ClearNsAttributes:
			{
				XmlNodeType nodeType = this.NodeType;
				switch (nodeType)
				{
				case XmlNodeType.Element:
					throw base.CreateReadContentAsException("ReadContentAsBase64");
				case XmlNodeType.Attribute:
					if (this.curNsAttr != -1 && this.reader.CanReadBinaryContent)
					{
						this.CheckBuffer(buffer, index, count);
						if (count == 0)
						{
							return 0;
						}
						if (this.nsIncReadOffset == 0)
						{
							if (this.binDecoder != null && this.binDecoder is Base64Decoder)
							{
								this.binDecoder.Reset();
							}
							else
							{
								this.binDecoder = new Base64Decoder();
							}
						}
						if (this.nsIncReadOffset == this.curNode.value.Length)
						{
							return 0;
						}
						this.binDecoder.SetNextOutputBuffer(buffer, index, count);
						this.nsIncReadOffset += this.binDecoder.Decode(this.curNode.value, this.nsIncReadOffset, this.curNode.value.Length - this.nsIncReadOffset);
						return this.binDecoder.DecodedCount;
					}
					break;
				case XmlNodeType.Text:
					break;
				default:
					if (nodeType != XmlNodeType.EndElement)
					{
						return 0;
					}
					return 0;
				}
				return await this.reader.ReadContentAsBase64Async(buffer, index, count).ConfigureAwait(false);
			}
			case XmlSubtreeReader.State.ReadElementContentAsBase64:
			case XmlSubtreeReader.State.ReadElementContentAsBinHex:
			case XmlSubtreeReader.State.ReadContentAsBinHex:
				throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadElementContentAsBase64 and ReadElementContentAsBinHex."));
			case XmlSubtreeReader.State.ReadContentAsBase64:
				break;
			default:
				return 0;
			}
			object obj = await this.reader.ReadContentAsBase64Async(buffer, index, count).ConfigureAwait(false);
			if (obj == null)
			{
				this.state = XmlSubtreeReader.State.Interactive;
				this.ProcessNamespaces();
			}
			return obj;
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0002F180 File Offset: 0x0002D380
		public override async Task<int> ReadElementContentAsBase64Async(byte[] buffer, int index, int count)
		{
			switch (this.state)
			{
			case XmlSubtreeReader.State.Initial:
			case XmlSubtreeReader.State.Error:
			case XmlSubtreeReader.State.EndOfFile:
			case XmlSubtreeReader.State.Closed:
				return 0;
			case XmlSubtreeReader.State.Interactive:
			case XmlSubtreeReader.State.PopNamespaceScope:
			case XmlSubtreeReader.State.ClearNsAttributes:
			{
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.InitReadElementContentAsBinaryAsync(XmlSubtreeReader.State.ReadElementContentAsBase64).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (!configuredTaskAwaiter.GetResult())
				{
					return 0;
				}
				break;
			}
			case XmlSubtreeReader.State.ReadElementContentAsBase64:
				break;
			case XmlSubtreeReader.State.ReadElementContentAsBinHex:
			case XmlSubtreeReader.State.ReadContentAsBase64:
			case XmlSubtreeReader.State.ReadContentAsBinHex:
				throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadElementContentAsBase64 and ReadElementContentAsBinHex."));
			default:
				return 0;
			}
			int num = await this.reader.ReadContentAsBase64Async(buffer, index, count).ConfigureAwait(false);
			int num2;
			if (num > 0 || count == 0)
			{
				num2 = num;
			}
			else
			{
				if (this.NodeType != XmlNodeType.EndElement)
				{
					throw new XmlException("'{0}' is an invalid XmlNodeType.", this.reader.NodeType.ToString(), this.reader as IXmlLineInfo);
				}
				this.state = XmlSubtreeReader.State.Interactive;
				this.ProcessNamespaces();
				if (this.reader.Depth == this.initialDepth)
				{
					this.state = XmlSubtreeReader.State.EndOfFile;
					this.SetEmptyNode();
				}
				else
				{
					await this.ReadAsync().ConfigureAwait(false);
				}
				num2 = 0;
			}
			return num2;
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0002F1E0 File Offset: 0x0002D3E0
		public override async Task<int> ReadContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			switch (this.state)
			{
			case XmlSubtreeReader.State.Initial:
			case XmlSubtreeReader.State.Error:
			case XmlSubtreeReader.State.EndOfFile:
			case XmlSubtreeReader.State.Closed:
				return 0;
			case XmlSubtreeReader.State.Interactive:
				this.state = XmlSubtreeReader.State.ReadContentAsBinHex;
				break;
			case XmlSubtreeReader.State.PopNamespaceScope:
			case XmlSubtreeReader.State.ClearNsAttributes:
			{
				XmlNodeType nodeType = this.NodeType;
				switch (nodeType)
				{
				case XmlNodeType.Element:
					throw base.CreateReadContentAsException("ReadContentAsBinHex");
				case XmlNodeType.Attribute:
					if (this.curNsAttr != -1 && this.reader.CanReadBinaryContent)
					{
						this.CheckBuffer(buffer, index, count);
						if (count == 0)
						{
							return 0;
						}
						if (this.nsIncReadOffset == 0)
						{
							if (this.binDecoder != null && this.binDecoder is BinHexDecoder)
							{
								this.binDecoder.Reset();
							}
							else
							{
								this.binDecoder = new BinHexDecoder();
							}
						}
						if (this.nsIncReadOffset == this.curNode.value.Length)
						{
							return 0;
						}
						this.binDecoder.SetNextOutputBuffer(buffer, index, count);
						this.nsIncReadOffset += this.binDecoder.Decode(this.curNode.value, this.nsIncReadOffset, this.curNode.value.Length - this.nsIncReadOffset);
						return this.binDecoder.DecodedCount;
					}
					break;
				case XmlNodeType.Text:
					break;
				default:
					if (nodeType != XmlNodeType.EndElement)
					{
						return 0;
					}
					return 0;
				}
				return await this.reader.ReadContentAsBinHexAsync(buffer, index, count).ConfigureAwait(false);
			}
			case XmlSubtreeReader.State.ReadElementContentAsBase64:
			case XmlSubtreeReader.State.ReadElementContentAsBinHex:
			case XmlSubtreeReader.State.ReadContentAsBase64:
				throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadElementContentAsBase64 and ReadElementContentAsBinHex."));
			case XmlSubtreeReader.State.ReadContentAsBinHex:
				break;
			default:
				return 0;
			}
			object obj = await this.reader.ReadContentAsBinHexAsync(buffer, index, count).ConfigureAwait(false);
			if (obj == null)
			{
				this.state = XmlSubtreeReader.State.Interactive;
				this.ProcessNamespaces();
			}
			return obj;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0002F240 File Offset: 0x0002D440
		public override async Task<int> ReadElementContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			switch (this.state)
			{
			case XmlSubtreeReader.State.Initial:
			case XmlSubtreeReader.State.Error:
			case XmlSubtreeReader.State.EndOfFile:
			case XmlSubtreeReader.State.Closed:
				return 0;
			case XmlSubtreeReader.State.Interactive:
			case XmlSubtreeReader.State.PopNamespaceScope:
			case XmlSubtreeReader.State.ClearNsAttributes:
			{
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.InitReadElementContentAsBinaryAsync(XmlSubtreeReader.State.ReadElementContentAsBinHex).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (!configuredTaskAwaiter.GetResult())
				{
					return 0;
				}
				break;
			}
			case XmlSubtreeReader.State.ReadElementContentAsBase64:
			case XmlSubtreeReader.State.ReadContentAsBase64:
			case XmlSubtreeReader.State.ReadContentAsBinHex:
				throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadElementContentAsBase64 and ReadElementContentAsBinHex."));
			case XmlSubtreeReader.State.ReadElementContentAsBinHex:
				break;
			default:
				return 0;
			}
			int num = await this.reader.ReadContentAsBinHexAsync(buffer, index, count).ConfigureAwait(false);
			int num2;
			if (num > 0 || count == 0)
			{
				num2 = num;
			}
			else
			{
				if (this.NodeType != XmlNodeType.EndElement)
				{
					throw new XmlException("'{0}' is an invalid XmlNodeType.", this.reader.NodeType.ToString(), this.reader as IXmlLineInfo);
				}
				this.state = XmlSubtreeReader.State.Interactive;
				this.ProcessNamespaces();
				if (this.reader.Depth == this.initialDepth)
				{
					this.state = XmlSubtreeReader.State.EndOfFile;
					this.SetEmptyNode();
				}
				else
				{
					await this.ReadAsync().ConfigureAwait(false);
				}
				num2 = 0;
			}
			return num2;
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0002F2A0 File Offset: 0x0002D4A0
		public override Task<int> ReadValueChunkAsync(char[] buffer, int index, int count)
		{
			switch (this.state)
			{
			case XmlSubtreeReader.State.Initial:
			case XmlSubtreeReader.State.Error:
			case XmlSubtreeReader.State.EndOfFile:
			case XmlSubtreeReader.State.Closed:
				return Task.FromResult<int>(0);
			case XmlSubtreeReader.State.Interactive:
				break;
			case XmlSubtreeReader.State.PopNamespaceScope:
			case XmlSubtreeReader.State.ClearNsAttributes:
				if (this.curNsAttr != -1 && this.reader.CanReadValueChunk)
				{
					this.CheckBuffer(buffer, index, count);
					int num = this.curNode.value.Length - this.nsIncReadOffset;
					if (num > count)
					{
						num = count;
					}
					if (num > 0)
					{
						this.curNode.value.CopyTo(this.nsIncReadOffset, buffer, index, num);
					}
					this.nsIncReadOffset += num;
					return Task.FromResult<int>(num);
				}
				break;
			case XmlSubtreeReader.State.ReadElementContentAsBase64:
			case XmlSubtreeReader.State.ReadElementContentAsBinHex:
			case XmlSubtreeReader.State.ReadContentAsBase64:
			case XmlSubtreeReader.State.ReadContentAsBinHex:
				throw new InvalidOperationException(Res.GetString("ReadValueChunk calls cannot be mixed with ReadContentAsBase64 or ReadContentAsBinHex."));
			default:
				return Task.FromResult<int>(0);
			}
			return this.reader.ReadValueChunkAsync(buffer, index, count);
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0002F388 File Offset: 0x0002D588
		private async Task<bool> InitReadElementContentAsBinaryAsync(XmlSubtreeReader.State binaryState)
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw this.reader.CreateReadElementContentAsException("ReadElementContentAsBase64");
			}
			bool isEmpty = this.IsEmptyElement;
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadAsync().ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			bool flag;
			if (!configuredTaskAwaiter.GetResult() || isEmpty)
			{
				flag = false;
			}
			else
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType == XmlNodeType.Element)
				{
					throw new XmlException("'{0}' is an invalid XmlNodeType.", this.reader.NodeType.ToString(), this.reader as IXmlLineInfo);
				}
				if (nodeType != XmlNodeType.EndElement)
				{
					this.state = binaryState;
					flag = true;
				}
				else
				{
					this.ProcessNamespaces();
					await this.ReadAsync().ConfigureAwait(false);
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0002F3D8 File Offset: 0x0002D5D8
		private async Task<bool> FinishReadElementContentAsBinaryAsync()
		{
			byte[] bytes = new byte[256];
			if (this.state == XmlSubtreeReader.State.ReadElementContentAsBase64)
			{
				ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter;
				do
				{
					configuredTaskAwaiter = this.reader.ReadContentAsBase64Async(bytes, 0, 256).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
					}
				}
				while (configuredTaskAwaiter.GetResult() > 0);
			}
			else
			{
				ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter;
				do
				{
					configuredTaskAwaiter = this.reader.ReadContentAsBinHexAsync(bytes, 0, 256).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
					}
				}
				while (configuredTaskAwaiter.GetResult() > 0);
			}
			if (this.NodeType != XmlNodeType.EndElement)
			{
				throw new XmlException("'{0}' is an invalid XmlNodeType.", this.reader.NodeType.ToString(), this.reader as IXmlLineInfo);
			}
			this.state = XmlSubtreeReader.State.Interactive;
			this.ProcessNamespaces();
			bool flag;
			if (this.reader.Depth == this.initialDepth)
			{
				this.state = XmlSubtreeReader.State.EndOfFile;
				this.SetEmptyNode();
				flag = false;
			}
			else
			{
				flag = await this.ReadAsync().ConfigureAwait(false);
			}
			return flag;
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0002F420 File Offset: 0x0002D620
		private async Task<bool> FinishReadContentAsBinaryAsync()
		{
			byte[] bytes = new byte[256];
			if (this.state == XmlSubtreeReader.State.ReadContentAsBase64)
			{
				ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter;
				do
				{
					configuredTaskAwaiter = this.reader.ReadContentAsBase64Async(bytes, 0, 256).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
					}
				}
				while (configuredTaskAwaiter.GetResult() > 0);
			}
			else
			{
				ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter;
				do
				{
					configuredTaskAwaiter = this.reader.ReadContentAsBinHexAsync(bytes, 0, 256).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
					}
				}
				while (configuredTaskAwaiter.GetResult() > 0);
			}
			this.state = XmlSubtreeReader.State.Interactive;
			this.ProcessNamespaces();
			bool flag;
			if (this.reader.Depth == this.initialDepth)
			{
				this.state = XmlSubtreeReader.State.EndOfFile;
				this.SetEmptyNode();
				flag = false;
			}
			else
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x040005D8 RID: 1496
		private const int AttributeActiveStates = 98;

		// Token: 0x040005D9 RID: 1497
		private const int NamespaceActiveStates = 2018;

		// Token: 0x040005DA RID: 1498
		private int initialDepth;

		// Token: 0x040005DB RID: 1499
		private XmlSubtreeReader.State state;

		// Token: 0x040005DC RID: 1500
		private XmlNamespaceManager nsManager;

		// Token: 0x040005DD RID: 1501
		private XmlSubtreeReader.NodeData[] nsAttributes;

		// Token: 0x040005DE RID: 1502
		private int nsAttrCount;

		// Token: 0x040005DF RID: 1503
		private int curNsAttr = -1;

		// Token: 0x040005E0 RID: 1504
		private string xmlns;

		// Token: 0x040005E1 RID: 1505
		private string xmlnsUri;

		// Token: 0x040005E2 RID: 1506
		private int nsIncReadOffset;

		// Token: 0x040005E3 RID: 1507
		private IncrementalReadDecoder binDecoder;

		// Token: 0x040005E4 RID: 1508
		private bool useCurNode;

		// Token: 0x040005E5 RID: 1509
		private XmlSubtreeReader.NodeData curNode;

		// Token: 0x040005E6 RID: 1510
		private XmlSubtreeReader.NodeData tmpNode;

		// Token: 0x040005E7 RID: 1511
		internal int InitialNamespaceAttributeCount = 4;

		// Token: 0x0200010D RID: 269
		private class NodeData
		{
			// Token: 0x06000A7B RID: 2683 RVA: 0x000020FD File Offset: 0x000002FD
			internal NodeData()
			{
			}

			// Token: 0x06000A7C RID: 2684 RVA: 0x0002F465 File Offset: 0x0002D665
			internal void Set(XmlNodeType nodeType, string localName, string prefix, string name, string namespaceUri, string value)
			{
				this.type = nodeType;
				this.localName = localName;
				this.prefix = prefix;
				this.name = name;
				this.namespaceUri = namespaceUri;
				this.value = value;
			}

			// Token: 0x040005E8 RID: 1512
			internal XmlNodeType type;

			// Token: 0x040005E9 RID: 1513
			internal string localName;

			// Token: 0x040005EA RID: 1514
			internal string prefix;

			// Token: 0x040005EB RID: 1515
			internal string name;

			// Token: 0x040005EC RID: 1516
			internal string namespaceUri;

			// Token: 0x040005ED RID: 1517
			internal string value;
		}

		// Token: 0x0200010E RID: 270
		private enum State
		{
			// Token: 0x040005EF RID: 1519
			Initial,
			// Token: 0x040005F0 RID: 1520
			Interactive,
			// Token: 0x040005F1 RID: 1521
			Error,
			// Token: 0x040005F2 RID: 1522
			EndOfFile,
			// Token: 0x040005F3 RID: 1523
			Closed,
			// Token: 0x040005F4 RID: 1524
			PopNamespaceScope,
			// Token: 0x040005F5 RID: 1525
			ClearNsAttributes,
			// Token: 0x040005F6 RID: 1526
			ReadElementContentAsBase64,
			// Token: 0x040005F7 RID: 1527
			ReadElementContentAsBinHex,
			// Token: 0x040005F8 RID: 1528
			ReadContentAsBase64,
			// Token: 0x040005F9 RID: 1529
			ReadContentAsBinHex
		}
	}
}
