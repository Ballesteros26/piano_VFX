using System;
using System.IO;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200054C RID: 1356
	internal class TextOutput : SequentialOutput
	{
		// Token: 0x060036B9 RID: 14009 RVA: 0x00132293 File Offset: 0x00130493
		internal TextOutput(Processor processor, Stream stream)
			: base(processor)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.encoding = processor.Output.Encoding;
			this.writer = new StreamWriter(stream, this.encoding);
		}

		// Token: 0x060036BA RID: 14010 RVA: 0x001322CD File Offset: 0x001304CD
		internal TextOutput(Processor processor, TextWriter writer)
			: base(processor)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this.encoding = writer.Encoding;
			this.writer = writer;
		}

		// Token: 0x060036BB RID: 14011 RVA: 0x001322F7 File Offset: 0x001304F7
		internal override void Write(char outputChar)
		{
			this.writer.Write(outputChar);
		}

		// Token: 0x060036BC RID: 14012 RVA: 0x00132305 File Offset: 0x00130505
		internal override void Write(string outputText)
		{
			this.writer.Write(outputText);
		}

		// Token: 0x060036BD RID: 14013 RVA: 0x00132313 File Offset: 0x00130513
		internal override void Close()
		{
			this.writer.Flush();
			this.writer = null;
		}

		// Token: 0x04002312 RID: 8978
		private TextWriter writer;
	}
}
