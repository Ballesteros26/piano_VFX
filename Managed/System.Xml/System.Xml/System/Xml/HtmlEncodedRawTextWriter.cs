using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Xml
{
	// Token: 0x02000092 RID: 146
	internal class HtmlEncodedRawTextWriter : XmlEncodedRawTextWriter
	{
		// Token: 0x060004D9 RID: 1241 RVA: 0x0001660A File Offset: 0x0001480A
		public HtmlEncodedRawTextWriter(TextWriter writer, XmlWriterSettings settings)
			: base(writer, settings)
		{
			this.Init(settings);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0001661B File Offset: 0x0001481B
		public HtmlEncodedRawTextWriter(Stream stream, XmlWriterSettings settings)
			: base(stream, settings)
		{
			this.Init(settings);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00002F50 File Offset: 0x00001150
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00002F50 File Offset: 0x00001150
		internal override void WriteXmlDeclaration(string xmldecl)
		{
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0001662C File Offset: 0x0001482C
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				base.ChangeTextContentMark(false);
			}
			base.RawText("<!DOCTYPE ");
			if (name == "HTML")
			{
				base.RawText("HTML");
			}
			else
			{
				base.RawText("html");
			}
			int num;
			if (pubid != null)
			{
				base.RawText(" PUBLIC \"");
				base.RawText(pubid);
				if (sysid != null)
				{
					base.RawText("\" \"");
					base.RawText(sysid);
				}
				char[] bufChars = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				bufChars[num] = 34;
			}
			else if (sysid != null)
			{
				base.RawText(" SYSTEM \"");
				base.RawText(sysid);
				char[] bufChars2 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				bufChars2[num] = 34;
			}
			else
			{
				char[] bufChars3 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				bufChars3[num] = 32;
			}
			if (subset != null)
			{
				char[] bufChars4 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				bufChars4[num] = 91;
				base.RawText(subset);
				char[] bufChars5 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				bufChars5[num] = 93;
			}
			char[] bufChars6 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			bufChars6[num] = 62;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0001676C File Offset: 0x0001496C
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.elementScope.Push((byte)this.currentElementProperties);
			if (ns.Length == 0)
			{
				if (this.trackTextContent && this.inTextContent)
				{
					base.ChangeTextContentMark(false);
				}
				this.currentElementProperties = (ElementProperties)HtmlEncodedRawTextWriter.elementPropertySearch.FindCaseInsensitiveString(localName);
				char[] bufChars = this.bufChars;
				int bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufChars[bufPos] = 60;
				base.RawText(localName);
				this.attrEndPos = this.bufPos;
				return;
			}
			this.currentElementProperties = ElementProperties.HAS_NS;
			base.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00016800 File Offset: 0x00014A00
		internal override void StartElementContent()
		{
			char[] bufChars = this.bufChars;
			int bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufChars[bufPos] = 62;
			this.contentPos = this.bufPos;
			if ((this.currentElementProperties & ElementProperties.HEAD) != ElementProperties.DEFAULT)
			{
				this.WriteMetaElement();
			}
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00016844 File Offset: 0x00014A44
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			if (ns.Length == 0)
			{
				if (this.trackTextContent && this.inTextContent)
				{
					base.ChangeTextContentMark(false);
				}
				if ((this.currentElementProperties & ElementProperties.EMPTY) == ElementProperties.DEFAULT)
				{
					char[] bufChars = this.bufChars;
					int num = this.bufPos;
					this.bufPos = num + 1;
					bufChars[num] = 60;
					char[] bufChars2 = this.bufChars;
					num = this.bufPos;
					this.bufPos = num + 1;
					bufChars2[num] = 47;
					base.RawText(localName);
					char[] bufChars3 = this.bufChars;
					num = this.bufPos;
					this.bufPos = num + 1;
					bufChars3[num] = 62;
				}
			}
			else
			{
				base.WriteEndElement(prefix, localName, ns);
			}
			this.currentElementProperties = (ElementProperties)this.elementScope.Pop();
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x000168EC File Offset: 0x00014AEC
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			if (ns.Length == 0)
			{
				if (this.trackTextContent && this.inTextContent)
				{
					base.ChangeTextContentMark(false);
				}
				if ((this.currentElementProperties & ElementProperties.EMPTY) == ElementProperties.DEFAULT)
				{
					char[] bufChars = this.bufChars;
					int num = this.bufPos;
					this.bufPos = num + 1;
					bufChars[num] = 60;
					char[] bufChars2 = this.bufChars;
					num = this.bufPos;
					this.bufPos = num + 1;
					bufChars2[num] = 47;
					base.RawText(localName);
					char[] bufChars3 = this.bufChars;
					num = this.bufPos;
					this.bufPos = num + 1;
					bufChars3[num] = 62;
				}
			}
			else
			{
				base.WriteFullEndElement(prefix, localName, ns);
			}
			this.currentElementProperties = (ElementProperties)this.elementScope.Pop();
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00016994 File Offset: 0x00014B94
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (ns.Length == 0)
			{
				if (this.trackTextContent && this.inTextContent)
				{
					base.ChangeTextContentMark(false);
				}
				int num;
				if (this.attrEndPos == this.bufPos)
				{
					char[] bufChars = this.bufChars;
					num = this.bufPos;
					this.bufPos = num + 1;
					bufChars[num] = 32;
				}
				base.RawText(localName);
				if ((this.currentElementProperties & (ElementProperties)7U) != ElementProperties.DEFAULT)
				{
					this.currentAttributeProperties = (AttributeProperties)((ElementProperties)HtmlEncodedRawTextWriter.attributePropertySearch.FindCaseInsensitiveString(localName) & this.currentElementProperties);
					if ((this.currentAttributeProperties & AttributeProperties.BOOLEAN) != AttributeProperties.DEFAULT)
					{
						this.inAttributeValue = true;
						return;
					}
				}
				else
				{
					this.currentAttributeProperties = AttributeProperties.DEFAULT;
				}
				char[] bufChars2 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				bufChars2[num] = 61;
				char[] bufChars3 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				bufChars3[num] = 34;
			}
			else
			{
				base.WriteStartAttribute(prefix, localName, ns);
				this.currentAttributeProperties = AttributeProperties.DEFAULT;
			}
			this.inAttributeValue = true;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00016A7C File Offset: 0x00014C7C
		public override void WriteEndAttribute()
		{
			if ((this.currentAttributeProperties & AttributeProperties.BOOLEAN) != AttributeProperties.DEFAULT)
			{
				this.attrEndPos = this.bufPos;
			}
			else
			{
				if (this.endsWithAmpersand)
				{
					this.OutputRestAmps();
					this.endsWithAmpersand = false;
				}
				if (this.trackTextContent && this.inTextContent)
				{
					base.ChangeTextContentMark(false);
				}
				char[] bufChars = this.bufChars;
				int bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufChars[bufPos] = 34;
			}
			this.inAttributeValue = false;
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00016AFC File Offset: 0x00014CFC
		public override void WriteProcessingInstruction(string target, string text)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				base.ChangeTextContentMark(false);
			}
			char[] bufChars = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			bufChars[num] = 60;
			char[] bufChars2 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			bufChars2[num] = 63;
			base.RawText(target);
			char[] bufChars3 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			bufChars3[num] = 32;
			base.WriteCommentOrPi(text, 63);
			char[] bufChars4 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			bufChars4[num] = 62;
			if (this.bufPos > this.bufLen)
			{
				this.FlushBuffer();
			}
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00016BAC File Offset: 0x00014DAC
		public unsafe override void WriteString(string text)
		{
			if (this.trackTextContent && !this.inTextContent)
			{
				base.ChangeTextContentMark(true);
			}
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr2 = ptr + text.Length;
				if (this.inAttributeValue)
				{
					this.WriteHtmlAttributeTextBlock(ptr, ptr2);
				}
				else
				{
					this.WriteHtmlElementTextBlock(ptr, ptr2);
				}
			}
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00016C08 File Offset: 0x00014E08
		public override void WriteEntityRef(string name)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00016C08 File Offset: 0x00014E08
		public override void WriteCharEntity(char ch)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00016C08 File Offset: 0x00014E08
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00016C1C File Offset: 0x00014E1C
		public unsafe override void WriteChars(char[] buffer, int index, int count)
		{
			if (this.trackTextContent && !this.inTextContent)
			{
				base.ChangeTextContentMark(true);
			}
			fixed (char* ptr = &buffer[index])
			{
				char* ptr2 = ptr;
				if (this.inAttributeValue)
				{
					base.WriteAttributeTextBlock(ptr2, ptr2 + count);
				}
				else
				{
					base.WriteElementTextBlock(ptr2, ptr2 + count);
				}
			}
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00016C74 File Offset: 0x00014E74
		private void Init(XmlWriterSettings settings)
		{
			if (HtmlEncodedRawTextWriter.elementPropertySearch == null)
			{
				HtmlEncodedRawTextWriter.attributePropertySearch = new TernaryTreeReadOnly(HtmlTernaryTree.htmlAttributes);
				HtmlEncodedRawTextWriter.elementPropertySearch = new TernaryTreeReadOnly(HtmlTernaryTree.htmlElements);
			}
			this.elementScope = new ByteStack(10);
			this.uriEscapingBuffer = new byte[5];
			this.currentElementProperties = ElementProperties.DEFAULT;
			this.mediaType = settings.MediaType;
			this.doNotEscapeUriAttributes = settings.DoNotEscapeUriAttributes;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00016CE0 File Offset: 0x00014EE0
		protected void WriteMetaElement()
		{
			base.RawText("<META http-equiv=\"Content-Type\"");
			if (this.mediaType == null)
			{
				this.mediaType = "text/html";
			}
			base.RawText(" content=\"");
			base.RawText(this.mediaType);
			base.RawText("; charset=");
			base.RawText(this.encoding.WebName);
			base.RawText("\">");
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00016D49 File Offset: 0x00014F49
		protected unsafe void WriteHtmlElementTextBlock(char* pSrc, char* pSrcEnd)
		{
			if ((this.currentElementProperties & ElementProperties.NO_ENTITIES) != ElementProperties.DEFAULT)
			{
				base.RawText(pSrc, pSrcEnd);
				return;
			}
			base.WriteElementTextBlock(pSrc, pSrcEnd);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00016D68 File Offset: 0x00014F68
		protected unsafe void WriteHtmlAttributeTextBlock(char* pSrc, char* pSrcEnd)
		{
			if ((this.currentAttributeProperties & (AttributeProperties)7U) != AttributeProperties.DEFAULT)
			{
				if ((this.currentAttributeProperties & AttributeProperties.BOOLEAN) != AttributeProperties.DEFAULT)
				{
					return;
				}
				if ((this.currentAttributeProperties & (AttributeProperties)5U) != AttributeProperties.DEFAULT && !this.doNotEscapeUriAttributes)
				{
					this.WriteUriAttributeText(pSrc, pSrcEnd);
					return;
				}
				this.WriteHtmlAttributeText(pSrc, pSrcEnd);
				return;
			}
			else
			{
				if ((this.currentElementProperties & ElementProperties.HAS_NS) != ElementProperties.DEFAULT)
				{
					base.WriteAttributeTextBlock(pSrc, pSrcEnd);
					return;
				}
				this.WriteHtmlAttributeText(pSrc, pSrcEnd);
				return;
			}
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00016DD0 File Offset: 0x00014FD0
		private unsafe void WriteHtmlAttributeText(char* pSrc, char* pSrcEnd)
		{
			if (this.endsWithAmpersand)
			{
				if ((long)(pSrcEnd - pSrc) > 0L && *pSrc != '{')
				{
					this.OutputRestAmps();
				}
				this.endsWithAmpersand = false;
			}
			char[] array;
			char* ptr;
			if ((array = this.bufChars) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			char* ptr2 = ptr + this.bufPos;
			char c = '\0';
			for (;;)
			{
				char* ptr3 = ptr2 + (long)(pSrcEnd - pSrc) * 2L / 2L;
				if (ptr3 != ptr + this.bufLen)
				{
					ptr3 = ptr + this.bufLen;
				}
				while (ptr2 < ptr3 && (this.xmlCharType.charProperties[(int)(c = *pSrc)] & 128) != 0)
				{
					*(ptr2++) = c;
					pSrc++;
				}
				if (pSrc >= pSrcEnd)
				{
					break;
				}
				if (ptr2 < ptr3)
				{
					if (c <= '&')
					{
						switch (c)
						{
						case '\t':
							goto IL_013B;
						case '\n':
							ptr2 = XmlEncodedRawTextWriter.LineFeedEntity(ptr2);
							goto IL_0166;
						case '\v':
						case '\f':
							break;
						case '\r':
							ptr2 = XmlEncodedRawTextWriter.CarriageReturnEntity(ptr2);
							goto IL_0166;
						default:
							if (c == '"')
							{
								ptr2 = XmlEncodedRawTextWriter.QuoteEntity(ptr2);
								goto IL_0166;
							}
							if (c == '&')
							{
								if (pSrc + 1 == pSrcEnd)
								{
									this.endsWithAmpersand = true;
								}
								else if (pSrc[1] != '{')
								{
									ptr2 = XmlEncodedRawTextWriter.AmpEntity(ptr2);
									goto IL_0166;
								}
								*(ptr2++) = c;
								goto IL_0166;
							}
							break;
						}
					}
					else if (c == '\'' || c == '<' || c == '>')
					{
						goto IL_013B;
					}
					base.EncodeChar(ref pSrc, pSrcEnd, ref ptr2);
					continue;
					IL_0166:
					pSrc++;
					continue;
					IL_013B:
					*(ptr2++) = c;
					goto IL_0166;
				}
				this.bufPos = (int)((long)(ptr2 - ptr));
				this.FlushBuffer();
				ptr2 = ptr + 1;
			}
			this.bufPos = (int)((long)(ptr2 - ptr));
			array = null;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00016F5C File Offset: 0x0001515C
		private unsafe void WriteUriAttributeText(char* pSrc, char* pSrcEnd)
		{
			if (this.endsWithAmpersand)
			{
				if ((long)(pSrcEnd - pSrc) > 0L && *pSrc != '{')
				{
					this.OutputRestAmps();
				}
				this.endsWithAmpersand = false;
			}
			char[] array;
			char* ptr;
			if ((array = this.bufChars) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			char* ptr2 = ptr + this.bufPos;
			char c = '\0';
			for (;;)
			{
				char* ptr3 = ptr2 + (long)(pSrcEnd - pSrc) * 2L / 2L;
				if (ptr3 != ptr + this.bufLen)
				{
					ptr3 = ptr + this.bufLen;
				}
				while (ptr2 < ptr3 && (this.xmlCharType.charProperties[(int)(c = *pSrc)] & 128) != 0 && c < '\u0080')
				{
					*(ptr2++) = c;
					pSrc++;
				}
				if (pSrc >= pSrcEnd)
				{
					break;
				}
				if (ptr2 < ptr3)
				{
					if (c <= '&')
					{
						switch (c)
						{
						case '\t':
							goto IL_014F;
						case '\n':
							ptr2 = XmlEncodedRawTextWriter.LineFeedEntity(ptr2);
							goto IL_01ED;
						case '\v':
						case '\f':
							break;
						case '\r':
							ptr2 = XmlEncodedRawTextWriter.CarriageReturnEntity(ptr2);
							goto IL_01ED;
						default:
							if (c == '"')
							{
								ptr2 = XmlEncodedRawTextWriter.QuoteEntity(ptr2);
								goto IL_01ED;
							}
							if (c == '&')
							{
								if (pSrc + 1 == pSrcEnd)
								{
									this.endsWithAmpersand = true;
								}
								else if (pSrc[1] != '{')
								{
									ptr2 = XmlEncodedRawTextWriter.AmpEntity(ptr2);
									goto IL_01ED;
								}
								*(ptr2++) = c;
								goto IL_01ED;
							}
							break;
						}
					}
					else if (c == '\'' || c == '<' || c == '>')
					{
						goto IL_014F;
					}
					byte[] array2;
					byte* ptr4;
					if ((array2 = this.uriEscapingBuffer) == null || array2.Length == 0)
					{
						ptr4 = null;
					}
					else
					{
						ptr4 = &array2[0];
					}
					byte* ptr5 = ptr4;
					byte* ptr6 = ptr5;
					XmlUtf8RawTextWriter.CharToUTF8(ref pSrc, pSrcEnd, ref ptr6);
					while (ptr5 < ptr6)
					{
						*(ptr2++) = '%';
						*(ptr2++) = "0123456789ABCDEF"[*ptr5 >> 4];
						*(ptr2++) = "0123456789ABCDEF"[(int)(*ptr5 & 15)];
						ptr5++;
					}
					array2 = null;
					continue;
					IL_01ED:
					pSrc++;
					continue;
					IL_014F:
					*(ptr2++) = c;
					goto IL_01ED;
				}
				this.bufPos = (int)((long)(ptr2 - ptr));
				this.FlushBuffer();
				ptr2 = ptr + 1;
			}
			this.bufPos = (int)((long)(ptr2 - ptr));
			array = null;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00017170 File Offset: 0x00015370
		private void OutputRestAmps()
		{
			char[] bufChars = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			bufChars[num] = 97;
			char[] bufChars2 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			bufChars2[num] = 109;
			char[] bufChars3 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			bufChars3[num] = 112;
			char[] bufChars4 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			bufChars4[num] = 59;
		}

		// Token: 0x04000318 RID: 792
		protected ByteStack elementScope;

		// Token: 0x04000319 RID: 793
		protected ElementProperties currentElementProperties;

		// Token: 0x0400031A RID: 794
		private AttributeProperties currentAttributeProperties;

		// Token: 0x0400031B RID: 795
		private bool endsWithAmpersand;

		// Token: 0x0400031C RID: 796
		private byte[] uriEscapingBuffer;

		// Token: 0x0400031D RID: 797
		private string mediaType;

		// Token: 0x0400031E RID: 798
		private bool doNotEscapeUriAttributes;

		// Token: 0x0400031F RID: 799
		protected static TernaryTreeReadOnly elementPropertySearch;

		// Token: 0x04000320 RID: 800
		protected static TernaryTreeReadOnly attributePropertySearch;

		// Token: 0x04000321 RID: 801
		private const int StackIncrement = 10;
	}
}
