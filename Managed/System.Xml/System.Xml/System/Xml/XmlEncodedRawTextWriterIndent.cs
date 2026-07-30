using System;
using System.IO;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x020000ED RID: 237
	internal class XmlEncodedRawTextWriterIndent : XmlEncodedRawTextWriter
	{
		// Token: 0x0600084F RID: 2127 RVA: 0x0002731A File Offset: 0x0002551A
		public XmlEncodedRawTextWriterIndent(TextWriter writer, XmlWriterSettings settings)
			: base(writer, settings)
		{
			this.Init(settings);
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0002732B File Offset: 0x0002552B
		public XmlEncodedRawTextWriterIndent(Stream stream, XmlWriterSettings settings)
			: base(stream, settings)
		{
			this.Init(settings);
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x0002733C File Offset: 0x0002553C
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

		// Token: 0x06000852 RID: 2130 RVA: 0x00027371 File Offset: 0x00025571
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			base.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0002739C File Offset: 0x0002559C
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

		// Token: 0x06000854 RID: 2132 RVA: 0x000273ED File Offset: 0x000255ED
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

		// Token: 0x06000855 RID: 2133 RVA: 0x00027421 File Offset: 0x00025621
		internal override void OnRootElement(ConformanceLevel currentConformanceLevel)
		{
			this.conformanceLevel = currentConformanceLevel;
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0002742C File Offset: 0x0002562C
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

		// Token: 0x06000857 RID: 2135 RVA: 0x0002748C File Offset: 0x0002568C
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

		// Token: 0x06000858 RID: 2136 RVA: 0x000274EB File Offset: 0x000256EB
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (this.newLineOnAttributes)
			{
				this.WriteIndent();
			}
			base.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00027504 File Offset: 0x00025704
		public override void WriteCData(string text)
		{
			this.mixedContent = true;
			base.WriteCData(text);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00027514 File Offset: 0x00025714
		public override void WriteComment(string text)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			base.WriteComment(text);
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00027539 File Offset: 0x00025739
		public override void WriteProcessingInstruction(string target, string text)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			base.WriteProcessingInstruction(target, text);
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0002755F File Offset: 0x0002575F
		public override void WriteEntityRef(string name)
		{
			this.mixedContent = true;
			base.WriteEntityRef(name);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0002756F File Offset: 0x0002576F
		public override void WriteCharEntity(char ch)
		{
			this.mixedContent = true;
			base.WriteCharEntity(ch);
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0002757F File Offset: 0x0002577F
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.mixedContent = true;
			base.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00027590 File Offset: 0x00025790
		public override void WriteWhitespace(string ws)
		{
			this.mixedContent = true;
			base.WriteWhitespace(ws);
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x000275A0 File Offset: 0x000257A0
		public override void WriteString(string text)
		{
			this.mixedContent = true;
			base.WriteString(text);
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x000275B0 File Offset: 0x000257B0
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.mixedContent = true;
			base.WriteChars(buffer, index, count);
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x000275C2 File Offset: 0x000257C2
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.mixedContent = true;
			base.WriteRaw(buffer, index, count);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x000275D4 File Offset: 0x000257D4
		public override void WriteRaw(string data)
		{
			this.mixedContent = true;
			base.WriteRaw(data);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x000275E4 File Offset: 0x000257E4
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.mixedContent = true;
			base.WriteBase64(buffer, index, count);
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x000275F8 File Offset: 0x000257F8
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

		// Token: 0x06000866 RID: 2150 RVA: 0x00027690 File Offset: 0x00025890
		private void WriteIndent()
		{
			base.RawText(this.newLineChars);
			for (int i = this.indentLevel; i > 0; i--)
			{
				base.RawText(this.indentChars);
			}
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x000276C8 File Offset: 0x000258C8
		public override async Task WriteDocTypeAsync(string name, string pubid, string sysid, string subset)
		{
			base.CheckAsyncCall();
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				await this.WriteIndentAsync().ConfigureAwait(false);
			}
			await base.WriteDocTypeAsync(name, pubid, sysid, subset).ConfigureAwait(false);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00027730 File Offset: 0x00025930
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

		// Token: 0x06000869 RID: 2153 RVA: 0x00027790 File Offset: 0x00025990
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

		// Token: 0x0600086A RID: 2154 RVA: 0x000277F0 File Offset: 0x000259F0
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

		// Token: 0x0600086B RID: 2155 RVA: 0x00027850 File Offset: 0x00025A50
		protected internal override async Task WriteStartAttributeAsync(string prefix, string localName, string ns)
		{
			base.CheckAsyncCall();
			if (this.newLineOnAttributes)
			{
				await this.WriteIndentAsync().ConfigureAwait(false);
			}
			await base.WriteStartAttributeAsync(prefix, localName, ns).ConfigureAwait(false);
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x000278AD File Offset: 0x00025AAD
		public override Task WriteCDataAsync(string text)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteCDataAsync(text);
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x000278C4 File Offset: 0x00025AC4
		public override async Task WriteCommentAsync(string text)
		{
			base.CheckAsyncCall();
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				await this.WriteIndentAsync().ConfigureAwait(false);
			}
			await base.WriteCommentAsync(text).ConfigureAwait(false);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00027914 File Offset: 0x00025B14
		public override async Task WriteProcessingInstructionAsync(string target, string text)
		{
			base.CheckAsyncCall();
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				await this.WriteIndentAsync().ConfigureAwait(false);
			}
			await base.WriteProcessingInstructionAsync(target, text).ConfigureAwait(false);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00027969 File Offset: 0x00025B69
		public override Task WriteEntityRefAsync(string name)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteEntityRefAsync(name);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0002797F File Offset: 0x00025B7F
		public override Task WriteCharEntityAsync(char ch)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteCharEntityAsync(ch);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00027995 File Offset: 0x00025B95
		public override Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteSurrogateCharEntityAsync(lowChar, highChar);
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x000279AC File Offset: 0x00025BAC
		public override Task WriteWhitespaceAsync(string ws)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteWhitespaceAsync(ws);
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x000279C2 File Offset: 0x00025BC2
		public override Task WriteStringAsync(string text)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteStringAsync(text);
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x000279D8 File Offset: 0x00025BD8
		public override Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteCharsAsync(buffer, index, count);
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x000279F0 File Offset: 0x00025BF0
		public override Task WriteRawAsync(char[] buffer, int index, int count)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteRawAsync(buffer, index, count);
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00027A08 File Offset: 0x00025C08
		public override Task WriteRawAsync(string data)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteRawAsync(data);
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00027A1E File Offset: 0x00025C1E
		public override Task WriteBase64Async(byte[] buffer, int index, int count)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteBase64Async(buffer, index, count);
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00027A38 File Offset: 0x00025C38
		private async Task WriteIndentAsync()
		{
			base.CheckAsyncCall();
			await base.RawTextAsync(this.newLineChars).ConfigureAwait(false);
			for (int i = this.indentLevel; i > 0; i--)
			{
				await base.RawTextAsync(this.indentChars).ConfigureAwait(false);
			}
		}

		// Token: 0x040004F7 RID: 1271
		protected int indentLevel;

		// Token: 0x040004F8 RID: 1272
		protected bool newLineOnAttributes;

		// Token: 0x040004F9 RID: 1273
		protected string indentChars;

		// Token: 0x040004FA RID: 1274
		protected bool mixedContent;

		// Token: 0x040004FB RID: 1275
		private BitStack mixedContentStack;

		// Token: 0x040004FC RID: 1276
		protected ConformanceLevel conformanceLevel;
	}
}
