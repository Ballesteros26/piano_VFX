using System;
using System.IO;

namespace System.Xml
{
	// Token: 0x02000093 RID: 147
	internal class HtmlEncodedRawTextWriterIndent : HtmlEncodedRawTextWriter
	{
		// Token: 0x060004F1 RID: 1265 RVA: 0x000171E5 File Offset: 0x000153E5
		public HtmlEncodedRawTextWriterIndent(TextWriter writer, XmlWriterSettings settings)
			: base(writer, settings)
		{
			this.Init(settings);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x000171F6 File Offset: 0x000153F6
		public HtmlEncodedRawTextWriterIndent(Stream stream, XmlWriterSettings settings)
			: base(stream, settings)
		{
			this.Init(settings);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00017207 File Offset: 0x00015407
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			base.WriteDocType(name, pubid, sysid, subset);
			this.endBlockPos = this.bufPos;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00017220 File Offset: 0x00015420
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				base.ChangeTextContentMark(false);
			}
			this.elementScope.Push((byte)this.currentElementProperties);
			if (ns.Length == 0)
			{
				this.currentElementProperties = (ElementProperties)HtmlEncodedRawTextWriter.elementPropertySearch.FindCaseInsensitiveString(localName);
				if (this.endBlockPos == this.bufPos && (this.currentElementProperties & ElementProperties.BLOCK_WS) != ElementProperties.DEFAULT)
				{
					this.WriteIndent();
				}
				this.indentLevel++;
				char[] bufChars = this.bufChars;
				int num = this.bufPos;
				this.bufPos = num + 1;
				bufChars[num] = 60;
			}
			else
			{
				this.currentElementProperties = (ElementProperties)192U;
				if (this.endBlockPos == this.bufPos)
				{
					this.WriteIndent();
				}
				this.indentLevel++;
				char[] bufChars2 = this.bufChars;
				int num = this.bufPos;
				this.bufPos = num + 1;
				bufChars2[num] = 60;
				if (prefix.Length != 0)
				{
					base.RawText(prefix);
					char[] bufChars3 = this.bufChars;
					num = this.bufPos;
					this.bufPos = num + 1;
					bufChars3[num] = 58;
				}
			}
			base.RawText(localName);
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0001733C File Offset: 0x0001553C
		internal override void StartElementContent()
		{
			char[] bufChars = this.bufChars;
			int bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufChars[bufPos] = 62;
			this.contentPos = this.bufPos;
			if ((this.currentElementProperties & ElementProperties.HEAD) != ElementProperties.DEFAULT)
			{
				this.WriteIndent();
				base.WriteMetaElement();
				this.endBlockPos = this.bufPos;
				return;
			}
			if ((this.currentElementProperties & ElementProperties.BLOCK_WS) != ElementProperties.DEFAULT)
			{
				this.endBlockPos = this.bufPos;
			}
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x000173AC File Offset: 0x000155AC
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			this.indentLevel--;
			bool flag = (this.currentElementProperties & ElementProperties.BLOCK_WS) > ElementProperties.DEFAULT;
			if (flag && this.endBlockPos == this.bufPos && this.contentPos != this.bufPos)
			{
				this.WriteIndent();
			}
			base.WriteEndElement(prefix, localName, ns);
			this.contentPos = 0;
			if (flag)
			{
				this.endBlockPos = this.bufPos;
			}
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00017418 File Offset: 0x00015618
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (this.newLineOnAttributes)
			{
				base.RawText(this.newLineChars);
				this.indentLevel++;
				this.WriteIndent();
				this.indentLevel--;
			}
			base.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00017464 File Offset: 0x00015664
		protected override void FlushBuffer()
		{
			this.endBlockPos = ((this.endBlockPos == this.bufPos) ? 1 : 0);
			base.FlushBuffer();
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00017484 File Offset: 0x00015684
		private void Init(XmlWriterSettings settings)
		{
			this.indentLevel = 0;
			this.indentChars = settings.IndentChars;
			this.newLineOnAttributes = settings.NewLineOnAttributes;
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x000174A8 File Offset: 0x000156A8
		private void WriteIndent()
		{
			base.RawText(this.newLineChars);
			for (int i = this.indentLevel; i > 0; i--)
			{
				base.RawText(this.indentChars);
			}
		}

		// Token: 0x04000322 RID: 802
		private int indentLevel;

		// Token: 0x04000323 RID: 803
		private int endBlockPos;

		// Token: 0x04000324 RID: 804
		private string indentChars;

		// Token: 0x04000325 RID: 805
		private bool newLineOnAttributes;
	}
}
