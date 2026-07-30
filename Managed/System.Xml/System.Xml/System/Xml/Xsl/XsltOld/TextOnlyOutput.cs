using System;
using System.IO;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200054B RID: 1355
	internal class TextOnlyOutput : RecordOutput
	{
		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x060036B3 RID: 14003 RVA: 0x001321D8 File Offset: 0x001303D8
		internal XsltOutput Output
		{
			get
			{
				return this.processor.Output;
			}
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x060036B4 RID: 14004 RVA: 0x001321E5 File Offset: 0x001303E5
		public TextWriter Writer
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x060036B5 RID: 14005 RVA: 0x001321ED File Offset: 0x001303ED
		internal TextOnlyOutput(Processor processor, Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.processor = processor;
			this.writer = new StreamWriter(stream, this.Output.Encoding);
		}

		// Token: 0x060036B6 RID: 14006 RVA: 0x00132221 File Offset: 0x00130421
		internal TextOnlyOutput(Processor processor, TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this.processor = processor;
			this.writer = writer;
		}

		// Token: 0x060036B7 RID: 14007 RVA: 0x00132248 File Offset: 0x00130448
		public Processor.OutputResult RecordDone(RecordBuilder record)
		{
			BuilderInfo mainNode = record.MainNode;
			XmlNodeType nodeType = mainNode.NodeType;
			if (nodeType == XmlNodeType.Text || nodeType - XmlNodeType.Whitespace <= 1)
			{
				this.writer.Write(mainNode.Value);
			}
			record.Reset();
			return Processor.OutputResult.Continue;
		}

		// Token: 0x060036B8 RID: 14008 RVA: 0x00132286 File Offset: 0x00130486
		public void TheEnd()
		{
			this.writer.Flush();
		}

		// Token: 0x04002310 RID: 8976
		private Processor processor;

		// Token: 0x04002311 RID: 8977
		private TextWriter writer;
	}
}
