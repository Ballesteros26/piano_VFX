using System;
using System.Collections;
using System.Xml.Xsl.Runtime;

namespace System.Xml.Xsl
{
	// Token: 0x020004C6 RID: 1222
	internal class XmlILCommand
	{
		// Token: 0x0600319F RID: 12703 RVA: 0x0011FED9 File Offset: 0x0011E0D9
		public XmlILCommand(ExecuteDelegate delExec, XmlQueryStaticData staticData)
		{
			this.delExec = delExec;
			this.staticData = staticData;
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x060031A0 RID: 12704 RVA: 0x0011FEEF File Offset: 0x0011E0EF
		public ExecuteDelegate ExecuteDelegate
		{
			get
			{
				return this.delExec;
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x060031A1 RID: 12705 RVA: 0x0011FEF7 File Offset: 0x0011E0F7
		public XmlQueryStaticData StaticData
		{
			get
			{
				return this.staticData;
			}
		}

		// Token: 0x060031A2 RID: 12706 RVA: 0x0011FF00 File Offset: 0x0011E100
		public IList Evaluate(string contextDocumentUri, XmlResolver dataSources, XsltArgumentList argumentList)
		{
			XmlCachedSequenceWriter xmlCachedSequenceWriter = new XmlCachedSequenceWriter();
			this.Execute(contextDocumentUri, dataSources, argumentList, xmlCachedSequenceWriter);
			return xmlCachedSequenceWriter.ResultSequence;
		}

		// Token: 0x060031A3 RID: 12707 RVA: 0x0011FF24 File Offset: 0x0011E124
		public void Execute(object defaultDocument, XmlResolver dataSources, XsltArgumentList argumentList, XmlWriter writer)
		{
			try
			{
				if (writer is XmlAsyncCheckWriter)
				{
					writer = ((XmlAsyncCheckWriter)writer).CoreWriter;
				}
				XmlWellFormedWriter xmlWellFormedWriter = writer as XmlWellFormedWriter;
				if (xmlWellFormedWriter != null && xmlWellFormedWriter.RawWriter != null && xmlWellFormedWriter.WriteState == WriteState.Start && xmlWellFormedWriter.Settings.ConformanceLevel != ConformanceLevel.Document)
				{
					this.Execute(defaultDocument, dataSources, argumentList, new XmlMergeSequenceWriter(xmlWellFormedWriter.RawWriter));
				}
				else
				{
					this.Execute(defaultDocument, dataSources, argumentList, new XmlMergeSequenceWriter(new XmlRawWriterWrapper(writer)));
				}
			}
			finally
			{
				writer.Flush();
			}
		}

		// Token: 0x060031A4 RID: 12708 RVA: 0x0011FFB8 File Offset: 0x0011E1B8
		private void Execute(object defaultDocument, XmlResolver dataSources, XsltArgumentList argumentList, XmlSequenceWriter results)
		{
			if (dataSources == null)
			{
				dataSources = XmlNullResolver.Singleton;
			}
			this.delExec(new XmlQueryRuntime(this.staticData, defaultDocument, dataSources, argumentList, results));
		}

		// Token: 0x04002056 RID: 8278
		private ExecuteDelegate delExec;

		// Token: 0x04002057 RID: 8279
		private XmlQueryStaticData staticData;
	}
}
