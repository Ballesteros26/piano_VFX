using System;
using System.IO;

namespace System.Xml
{
	// Token: 0x02000096 RID: 150
	internal class HtmlUtf8RawTextWriterIndent : HtmlUtf8RawTextWriter
	{
		// Token: 0x06000514 RID: 1300 RVA: 0x00017FF1 File Offset: 0x000161F1
		public HtmlUtf8RawTextWriterIndent(Stream stream, XmlWriterSettings settings)
			: base(stream, settings)
		{
			this.Init(settings);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00018002 File Offset: 0x00016202
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			base.WriteDocType(name, pubid, sysid, subset);
			this.endBlockPos = this.bufPos;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0001801C File Offset: 0x0001621C
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.elementScope.Push((byte)this.currentElementProperties);
			if (ns.Length == 0)
			{
				this.currentElementProperties = (ElementProperties)HtmlUtf8RawTextWriter.elementPropertySearch.FindCaseInsensitiveString(localName);
				if (this.endBlockPos == this.bufPos && (this.currentElementProperties & ElementProperties.BLOCK_WS) != ElementProperties.DEFAULT)
				{
					this.WriteIndent();
				}
				this.indentLevel++;
				byte[] bufBytes = this.bufBytes;
				int num = this.bufPos;
				this.bufPos = num + 1;
				bufBytes[num] = 60;
			}
			else
			{
				this.currentElementProperties = (ElementProperties)192U;
				if (this.endBlockPos == this.bufPos)
				{
					this.WriteIndent();
				}
				this.indentLevel++;
				byte[] bufBytes2 = this.bufBytes;
				int num = this.bufPos;
				this.bufPos = num + 1;
				bufBytes2[num] = 60;
				if (prefix.Length != 0)
				{
					base.RawText(prefix);
					byte[] bufBytes3 = this.bufBytes;
					num = this.bufPos;
					this.bufPos = num + 1;
					bufBytes3[num] = 58;
				}
			}
			base.RawText(localName);
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00018120 File Offset: 0x00016320
		internal override void StartElementContent()
		{
			byte[] bufBytes = this.bufBytes;
			int bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufBytes[bufPos] = 62;
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

		// Token: 0x06000518 RID: 1304 RVA: 0x00018190 File Offset: 0x00016390
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

		// Token: 0x06000519 RID: 1305 RVA: 0x000181FC File Offset: 0x000163FC
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

		// Token: 0x0600051A RID: 1306 RVA: 0x00018248 File Offset: 0x00016448
		protected override void FlushBuffer()
		{
			this.endBlockPos = ((this.endBlockPos == this.bufPos) ? 1 : 0);
			base.FlushBuffer();
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00018268 File Offset: 0x00016468
		private void Init(XmlWriterSettings settings)
		{
			this.indentLevel = 0;
			this.indentChars = settings.IndentChars;
			this.newLineOnAttributes = settings.NewLineOnAttributes;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0001828C File Offset: 0x0001648C
		private void WriteIndent()
		{
			base.RawText(this.newLineChars);
			for (int i = this.indentLevel; i > 0; i--)
			{
				base.RawText(this.indentChars);
			}
		}

		// Token: 0x04000332 RID: 818
		private int indentLevel;

		// Token: 0x04000333 RID: 819
		private int endBlockPos;

		// Token: 0x04000334 RID: 820
		private string indentChars;

		// Token: 0x04000335 RID: 821
		private bool newLineOnAttributes;
	}
}
