using System;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x020000A9 RID: 169
	internal class QueryOutputWriterV1 : XmlWriter
	{
		// Token: 0x060005A7 RID: 1447 RVA: 0x000188F0 File Offset: 0x00016AF0
		public QueryOutputWriterV1(XmlWriter writer, XmlWriterSettings settings)
		{
			this.wrapped = writer;
			this.systemId = settings.DocTypeSystem;
			this.publicId = settings.DocTypePublic;
			if (settings.OutputMethod == XmlOutputMethod.Xml)
			{
				bool flag = false;
				if (this.systemId != null)
				{
					flag = true;
					this.outputDocType = true;
				}
				if (settings.Standalone == XmlStandalone.Yes)
				{
					flag = true;
					this.standalone = settings.Standalone;
				}
				if (flag)
				{
					if (settings.Standalone == XmlStandalone.Yes)
					{
						this.wrapped.WriteStartDocument(true);
					}
					else
					{
						this.wrapped.WriteStartDocument();
					}
				}
				if (settings.CDataSectionElements != null && settings.CDataSectionElements.Count > 0)
				{
					this.bitsCData = new BitStack();
					this.lookupCDataElems = new Dictionary<XmlQualifiedName, XmlQualifiedName>();
					this.qnameCData = new XmlQualifiedName();
					foreach (XmlQualifiedName xmlQualifiedName in settings.CDataSectionElements)
					{
						this.lookupCDataElems[xmlQualifiedName] = null;
					}
					this.bitsCData.PushBit(false);
					return;
				}
			}
			else if (settings.OutputMethod == XmlOutputMethod.Html && (this.systemId != null || this.publicId != null))
			{
				this.outputDocType = true;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x00018A34 File Offset: 0x00016C34
		public override WriteState WriteState
		{
			get
			{
				return this.wrapped.WriteState;
			}
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00018A41 File Offset: 0x00016C41
		public override void WriteStartDocument()
		{
			this.wrapped.WriteStartDocument();
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00018A4E File Offset: 0x00016C4E
		public override void WriteStartDocument(bool standalone)
		{
			this.wrapped.WriteStartDocument(standalone);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00018A5C File Offset: 0x00016C5C
		public override void WriteEndDocument()
		{
			this.wrapped.WriteEndDocument();
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00018A69 File Offset: 0x00016C69
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			if (this.publicId == null && this.systemId == null)
			{
				this.wrapped.WriteDocType(name, pubid, sysid, subset);
			}
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00018A8C File Offset: 0x00016C8C
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.EndCDataSection();
			if (this.outputDocType)
			{
				WriteState writeState = this.wrapped.WriteState;
				if (writeState == WriteState.Start || writeState == WriteState.Prolog)
				{
					this.wrapped.WriteDocType((prefix.Length != 0) ? (prefix + ":" + localName) : localName, this.publicId, this.systemId, null);
				}
				this.outputDocType = false;
			}
			this.wrapped.WriteStartElement(prefix, localName, ns);
			if (this.lookupCDataElems != null)
			{
				this.qnameCData.Init(localName, ns);
				this.bitsCData.PushBit(this.lookupCDataElems.ContainsKey(this.qnameCData));
			}
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00018B2F File Offset: 0x00016D2F
		public override void WriteEndElement()
		{
			this.EndCDataSection();
			this.wrapped.WriteEndElement();
			if (this.lookupCDataElems != null)
			{
				this.bitsCData.PopBit();
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00018B56 File Offset: 0x00016D56
		public override void WriteFullEndElement()
		{
			this.EndCDataSection();
			this.wrapped.WriteFullEndElement();
			if (this.lookupCDataElems != null)
			{
				this.bitsCData.PopBit();
			}
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00018B7D File Offset: 0x00016D7D
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.inAttr = true;
			this.wrapped.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00018B94 File Offset: 0x00016D94
		public override void WriteEndAttribute()
		{
			this.inAttr = false;
			this.wrapped.WriteEndAttribute();
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00018BA8 File Offset: 0x00016DA8
		public override void WriteCData(string text)
		{
			this.wrapped.WriteCData(text);
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00018BB6 File Offset: 0x00016DB6
		public override void WriteComment(string text)
		{
			this.EndCDataSection();
			this.wrapped.WriteComment(text);
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00018BCA File Offset: 0x00016DCA
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.EndCDataSection();
			this.wrapped.WriteProcessingInstruction(name, text);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00018BDF File Offset: 0x00016DDF
		public override void WriteWhitespace(string ws)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(ws);
				return;
			}
			this.wrapped.WriteWhitespace(ws);
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00018C12 File Offset: 0x00016E12
		public override void WriteString(string text)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(text);
				return;
			}
			this.wrapped.WriteString(text);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00018C45 File Offset: 0x00016E45
		public override void WriteChars(char[] buffer, int index, int count)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(new string(buffer, index, count));
				return;
			}
			this.wrapped.WriteChars(buffer, index, count);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00018C81 File Offset: 0x00016E81
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteBase64(buffer, index, count);
				return;
			}
			this.wrapped.WriteBase64(buffer, index, count);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00018CB8 File Offset: 0x00016EB8
		public override void WriteEntityRef(string name)
		{
			this.EndCDataSection();
			this.wrapped.WriteEntityRef(name);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00018CCC File Offset: 0x00016ECC
		public override void WriteCharEntity(char ch)
		{
			this.EndCDataSection();
			this.wrapped.WriteCharEntity(ch);
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00018CE0 File Offset: 0x00016EE0
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.EndCDataSection();
			this.wrapped.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00018CF5 File Offset: 0x00016EF5
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(new string(buffer, index, count));
				return;
			}
			this.wrapped.WriteRaw(buffer, index, count);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00018D31 File Offset: 0x00016F31
		public override void WriteRaw(string data)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(data);
				return;
			}
			this.wrapped.WriteRaw(data);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00018D64 File Offset: 0x00016F64
		public override void Close()
		{
			this.wrapped.Close();
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00018D71 File Offset: 0x00016F71
		public override void Flush()
		{
			this.wrapped.Flush();
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00018D7E File Offset: 0x00016F7E
		public override string LookupPrefix(string ns)
		{
			return this.wrapped.LookupPrefix(ns);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00018D8C File Offset: 0x00016F8C
		private bool StartCDataSection()
		{
			if (this.lookupCDataElems != null && this.bitsCData.PeekBit())
			{
				this.inCDataSection = true;
				return true;
			}
			return false;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00018DAD File Offset: 0x00016FAD
		private void EndCDataSection()
		{
			this.inCDataSection = false;
		}

		// Token: 0x0400034D RID: 845
		private XmlWriter wrapped;

		// Token: 0x0400034E RID: 846
		private bool inCDataSection;

		// Token: 0x0400034F RID: 847
		private Dictionary<XmlQualifiedName, XmlQualifiedName> lookupCDataElems;

		// Token: 0x04000350 RID: 848
		private BitStack bitsCData;

		// Token: 0x04000351 RID: 849
		private XmlQualifiedName qnameCData;

		// Token: 0x04000352 RID: 850
		private bool outputDocType;

		// Token: 0x04000353 RID: 851
		private bool inAttr;

		// Token: 0x04000354 RID: 852
		private string systemId;

		// Token: 0x04000355 RID: 853
		private string publicId;

		// Token: 0x04000356 RID: 854
		private XmlStandalone standalone;
	}
}
