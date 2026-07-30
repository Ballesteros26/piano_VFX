using System;
using System.IO;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x02000197 RID: 407
	internal class XmlUtf8RawTextWriterIndent : XmlUtf8RawTextWriter
	{
		// Token: 0x06000E73 RID: 3699 RVA: 0x00056E6E File Offset: 0x0005506E
		public XmlUtf8RawTextWriterIndent(Stream stream, XmlWriterSettings settings)
			: base(stream, settings)
		{
			this.Init(settings);
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000E74 RID: 3700 RVA: 0x00056E7F File Offset: 0x0005507F
		public override XmlWriterSettings Settings
		{
			get
			{
				XmlWriterSettings settings = base.Settings;
				settings.ReadOnly = false;
				settings.Indent = true;
				settings.IndentChars = this.indentChars;
				settings.NewLineOnAttributes = this.newLineOnAttributes;
				settings.ReadOnly = true;
				return settings;
			}
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x00056EB4 File Offset: 0x000550B4
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			base.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x00056EE0 File Offset: 0x000550E0
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			this.indentLevel++;
			this.mixedContentStack.PushBit(this.mixedContent);
			base.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x00056F31 File Offset: 0x00055131
		internal override void StartElementContent()
		{
			if (this.indentLevel == 1 && this.conformanceLevel == ConformanceLevel.Document)
			{
				this.mixedContent = false;
			}
			else
			{
				this.mixedContent = this.mixedContentStack.PeekBit();
			}
			base.StartElementContent();
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x00056F65 File Offset: 0x00055165
		internal override void OnRootElement(ConformanceLevel currentConformanceLevel)
		{
			this.conformanceLevel = currentConformanceLevel;
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x00056F70 File Offset: 0x00055170
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			this.indentLevel--;
			if (!this.mixedContent && this.contentPos != this.bufPos && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			this.mixedContent = this.mixedContentStack.PopBit();
			base.WriteEndElement(prefix, localName, ns);
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x00056FD0 File Offset: 0x000551D0
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			this.indentLevel--;
			if (!this.mixedContent && this.contentPos != this.bufPos && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			this.mixedContent = this.mixedContentStack.PopBit();
			base.WriteFullEndElement(prefix, localName, ns);
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0005702F File Offset: 0x0005522F
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (this.newLineOnAttributes)
			{
				this.WriteIndent();
			}
			base.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x00057048 File Offset: 0x00055248
		public override void WriteCData(string text)
		{
			this.mixedContent = true;
			base.WriteCData(text);
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x00057058 File Offset: 0x00055258
		public override void WriteComment(string text)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			base.WriteComment(text);
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x0005707D File Offset: 0x0005527D
		public override void WriteProcessingInstruction(string target, string text)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			base.WriteProcessingInstruction(target, text);
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x000570A3 File Offset: 0x000552A3
		public override void WriteEntityRef(string name)
		{
			this.mixedContent = true;
			base.WriteEntityRef(name);
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x000570B3 File Offset: 0x000552B3
		public override void WriteCharEntity(char ch)
		{
			this.mixedContent = true;
			base.WriteCharEntity(ch);
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x000570C3 File Offset: 0x000552C3
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.mixedContent = true;
			base.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x000570D4 File Offset: 0x000552D4
		public override void WriteWhitespace(string ws)
		{
			this.mixedContent = true;
			base.WriteWhitespace(ws);
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x000570E4 File Offset: 0x000552E4
		public override void WriteString(string text)
		{
			this.mixedContent = true;
			base.WriteString(text);
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x000570F4 File Offset: 0x000552F4
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.mixedContent = true;
			base.WriteChars(buffer, index, count);
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x00057106 File Offset: 0x00055306
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.mixedContent = true;
			base.WriteRaw(buffer, index, count);
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x00057118 File Offset: 0x00055318
		public override void WriteRaw(string data)
		{
			this.mixedContent = true;
			base.WriteRaw(data);
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x00057128 File Offset: 0x00055328
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.mixedContent = true;
			base.WriteBase64(buffer, index, count);
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x0005713C File Offset: 0x0005533C
		private void Init(XmlWriterSettings settings)
		{
			this.indentLevel = 0;
			this.indentChars = settings.IndentChars;
			this.newLineOnAttributes = settings.NewLineOnAttributes;
			this.mixedContentStack = new BitStack();
			if (this.checkCharacters)
			{
				if (this.newLineOnAttributes)
				{
					base.ValidateContentChars(this.indentChars, "IndentChars", true);
					base.ValidateContentChars(this.newLineChars, "NewLineChars", true);
					return;
				}
				base.ValidateContentChars(this.indentChars, "IndentChars", false);
				if (this.newLineHandling != NewLineHandling.Replace)
				{
					base.ValidateContentChars(this.newLineChars, "NewLineChars", false);
				}
			}
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x000571D4 File Offset: 0x000553D4
		private void WriteIndent()
		{
			base.RawText(this.newLineChars);
			for (int i = this.indentLevel; i > 0; i--)
			{
				base.RawText(this.indentChars);
			}
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x0005720C File Offset: 0x0005540C
		public override async Task WriteDocTypeAsync(string name, string pubid, string sysid, string subset)
		{
			base.CheckAsyncCall();
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				await this.WriteIndentAsync().ConfigureAwait(false);
			}
			await base.WriteDocTypeAsync(name, pubid, sysid, subset).ConfigureAwait(false);
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x00057274 File Offset: 0x00055474
		public override async Task WriteStartElementAsync(string prefix, string localName, string ns)
		{
			base.CheckAsyncCall();
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				await this.WriteIndentAsync().ConfigureAwait(false);
			}
			this.indentLevel++;
			this.mixedContentStack.PushBit(this.mixedContent);
			await base.WriteStartElementAsync(prefix, localName, ns).ConfigureAwait(false);
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x000572D4 File Offset: 0x000554D4
		internal override async Task WriteEndElementAsync(string prefix, string localName, string ns)
		{
			base.CheckAsyncCall();
			this.indentLevel--;
			if (!this.mixedContent && this.contentPos != this.bufPos && this.textPos != this.bufPos)
			{
				await this.WriteIndentAsync().ConfigureAwait(false);
			}
			this.mixedContent = this.mixedContentStack.PopBit();
			await base.WriteEndElementAsync(prefix, localName, ns).ConfigureAwait(false);
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x00057334 File Offset: 0x00055534
		internal override async Task WriteFullEndElementAsync(string prefix, string localName, string ns)
		{
			base.CheckAsyncCall();
			this.indentLevel--;
			if (!this.mixedContent && this.contentPos != this.bufPos && this.textPos != this.bufPos)
			{
				await this.WriteIndentAsync().ConfigureAwait(false);
			}
			this.mixedContent = this.mixedContentStack.PopBit();
			await base.WriteFullEndElementAsync(prefix, localName, ns).ConfigureAwait(false);
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x00057394 File Offset: 0x00055594
		protected internal override async Task WriteStartAttributeAsync(string prefix, string localName, string ns)
		{
			base.CheckAsyncCall();
			if (this.newLineOnAttributes)
			{
				await this.WriteIndentAsync().ConfigureAwait(false);
			}
			await base.WriteStartAttributeAsync(prefix, localName, ns).ConfigureAwait(false);
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x000573F1 File Offset: 0x000555F1
		public override Task WriteCDataAsync(string text)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteCDataAsync(text);
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x00057408 File Offset: 0x00055608
		public override async Task WriteCommentAsync(string text)
		{
			base.CheckAsyncCall();
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				await this.WriteIndentAsync().ConfigureAwait(false);
			}
			await base.WriteCommentAsync(text).ConfigureAwait(false);
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x00057458 File Offset: 0x00055658
		public override async Task WriteProcessingInstructionAsync(string target, string text)
		{
			base.CheckAsyncCall();
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				await this.WriteIndentAsync().ConfigureAwait(false);
			}
			await base.WriteProcessingInstructionAsync(target, text).ConfigureAwait(false);
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x000574AD File Offset: 0x000556AD
		public override Task WriteEntityRefAsync(string name)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteEntityRefAsync(name);
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x000574C3 File Offset: 0x000556C3
		public override Task WriteCharEntityAsync(char ch)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteCharEntityAsync(ch);
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x000574D9 File Offset: 0x000556D9
		public override Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteSurrogateCharEntityAsync(lowChar, highChar);
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x000574F0 File Offset: 0x000556F0
		public override Task WriteWhitespaceAsync(string ws)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteWhitespaceAsync(ws);
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x00057506 File Offset: 0x00055706
		public override Task WriteStringAsync(string text)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteStringAsync(text);
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0005751C File Offset: 0x0005571C
		public override Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteCharsAsync(buffer, index, count);
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x00057534 File Offset: 0x00055734
		public override Task WriteRawAsync(char[] buffer, int index, int count)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteRawAsync(buffer, index, count);
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x0005754C File Offset: 0x0005574C
		public override Task WriteRawAsync(string data)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteRawAsync(data);
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x00057562 File Offset: 0x00055762
		public override Task WriteBase64Async(byte[] buffer, int index, int count)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteBase64Async(buffer, index, count);
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0005757C File Offset: 0x0005577C
		private async Task WriteIndentAsync()
		{
			base.CheckAsyncCall();
			await base.RawTextAsync(this.newLineChars).ConfigureAwait(false);
			for (int i = this.indentLevel; i > 0; i--)
			{
				await base.RawTextAsync(this.indentChars).ConfigureAwait(false);
			}
		}

		// Token: 0x04000A29 RID: 2601
		protected int indentLevel;

		// Token: 0x04000A2A RID: 2602
		protected bool newLineOnAttributes;

		// Token: 0x04000A2B RID: 2603
		protected string indentChars;

		// Token: 0x04000A2C RID: 2604
		protected bool mixedContent;

		// Token: 0x04000A2D RID: 2605
		private BitStack mixedContentStack;

		// Token: 0x04000A2E RID: 2606
		protected ConformanceLevel conformanceLevel;
	}
}
