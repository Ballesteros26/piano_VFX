using System;
using System.IO;
using System.Xml;

namespace System.Data
{
	// Token: 0x02000114 RID: 276
	internal sealed class DataTextWriter : XmlWriter
	{
		// Token: 0x06000DFF RID: 3583 RVA: 0x0004B4A3 File Offset: 0x000496A3
		internal static XmlWriter CreateWriter(XmlWriter xw)
		{
			return new DataTextWriter(xw);
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x0004B4AB File Offset: 0x000496AB
		private DataTextWriter(XmlWriter w)
		{
			this._xmltextWriter = w;
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x0004B4BC File Offset: 0x000496BC
		internal Stream BaseStream
		{
			get
			{
				XmlTextWriter xmlTextWriter = this._xmltextWriter as XmlTextWriter;
				if (xmlTextWriter != null)
				{
					return xmlTextWriter.BaseStream;
				}
				return null;
			}
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x0004B4E0 File Offset: 0x000496E0
		public override void WriteStartDocument()
		{
			this._xmltextWriter.WriteStartDocument();
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x0004B4ED File Offset: 0x000496ED
		public override void WriteStartDocument(bool standalone)
		{
			this._xmltextWriter.WriteStartDocument(standalone);
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0004B4FB File Offset: 0x000496FB
		public override void WriteEndDocument()
		{
			this._xmltextWriter.WriteEndDocument();
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x0004B508 File Offset: 0x00049708
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this._xmltextWriter.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x0004B51A File Offset: 0x0004971A
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this._xmltextWriter.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0004B52A File Offset: 0x0004972A
		public override void WriteEndElement()
		{
			this._xmltextWriter.WriteEndElement();
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0004B537 File Offset: 0x00049737
		public override void WriteFullEndElement()
		{
			this._xmltextWriter.WriteFullEndElement();
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0004B544 File Offset: 0x00049744
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this._xmltextWriter.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x0004B554 File Offset: 0x00049754
		public override void WriteEndAttribute()
		{
			this._xmltextWriter.WriteEndAttribute();
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x0004B561 File Offset: 0x00049761
		public override void WriteCData(string text)
		{
			this._xmltextWriter.WriteCData(text);
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x0004B56F File Offset: 0x0004976F
		public override void WriteComment(string text)
		{
			this._xmltextWriter.WriteComment(text);
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x0004B57D File Offset: 0x0004977D
		public override void WriteProcessingInstruction(string name, string text)
		{
			this._xmltextWriter.WriteProcessingInstruction(name, text);
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x0004B58C File Offset: 0x0004978C
		public override void WriteEntityRef(string name)
		{
			this._xmltextWriter.WriteEntityRef(name);
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x0004B59A File Offset: 0x0004979A
		public override void WriteCharEntity(char ch)
		{
			this._xmltextWriter.WriteCharEntity(ch);
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x0004B5A8 File Offset: 0x000497A8
		public override void WriteWhitespace(string ws)
		{
			this._xmltextWriter.WriteWhitespace(ws);
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x0004B5B6 File Offset: 0x000497B6
		public override void WriteString(string text)
		{
			this._xmltextWriter.WriteString(text);
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x0004B5C4 File Offset: 0x000497C4
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this._xmltextWriter.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x0004B5D3 File Offset: 0x000497D3
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this._xmltextWriter.WriteChars(buffer, index, count);
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x0004B5E3 File Offset: 0x000497E3
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this._xmltextWriter.WriteRaw(buffer, index, count);
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x0004B5F3 File Offset: 0x000497F3
		public override void WriteRaw(string data)
		{
			this._xmltextWriter.WriteRaw(data);
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x0004B601 File Offset: 0x00049801
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this._xmltextWriter.WriteBase64(buffer, index, count);
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x0004B611 File Offset: 0x00049811
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			this._xmltextWriter.WriteBinHex(buffer, index, count);
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000E18 RID: 3608 RVA: 0x0004B621 File Offset: 0x00049821
		public override WriteState WriteState
		{
			get
			{
				return this._xmltextWriter.WriteState;
			}
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x0004B62E File Offset: 0x0004982E
		public override void Close()
		{
			this._xmltextWriter.Close();
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x0004B63B File Offset: 0x0004983B
		public override void Flush()
		{
			this._xmltextWriter.Flush();
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x0004B648 File Offset: 0x00049848
		public override void WriteName(string name)
		{
			this._xmltextWriter.WriteName(name);
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x0004B656 File Offset: 0x00049856
		public override void WriteQualifiedName(string localName, string ns)
		{
			this._xmltextWriter.WriteQualifiedName(localName, ns);
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x0004B665 File Offset: 0x00049865
		public override string LookupPrefix(string ns)
		{
			return this._xmltextWriter.LookupPrefix(ns);
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x0004B673 File Offset: 0x00049873
		public override XmlSpace XmlSpace
		{
			get
			{
				return this._xmltextWriter.XmlSpace;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x0004B680 File Offset: 0x00049880
		public override string XmlLang
		{
			get
			{
				return this._xmltextWriter.XmlLang;
			}
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0004B68D File Offset: 0x0004988D
		public override void WriteNmToken(string name)
		{
			this._xmltextWriter.WriteNmToken(name);
		}

		// Token: 0x040009EE RID: 2542
		private XmlWriter _xmltextWriter;
	}
}
