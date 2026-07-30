using System;

namespace System.Xml
{
	// Token: 0x020000C4 RID: 196
	internal class XmlAsyncCheckReaderWithLineInfo : XmlAsyncCheckReader, IXmlLineInfo
	{
		// Token: 0x060006C7 RID: 1735 RVA: 0x0001BE8A File Offset: 0x0001A08A
		public XmlAsyncCheckReaderWithLineInfo(XmlReader reader)
			: base(reader)
		{
			this.readerAsIXmlLineInfo = (IXmlLineInfo)reader;
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0001BE9F File Offset: 0x0001A09F
		public virtual bool HasLineInfo()
		{
			return this.readerAsIXmlLineInfo.HasLineInfo();
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x0001BEAC File Offset: 0x0001A0AC
		public virtual int LineNumber
		{
			get
			{
				return this.readerAsIXmlLineInfo.LineNumber;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x0001BEB9 File Offset: 0x0001A0B9
		public virtual int LinePosition
		{
			get
			{
				return this.readerAsIXmlLineInfo.LinePosition;
			}
		}

		// Token: 0x040003DE RID: 990
		private readonly IXmlLineInfo readerAsIXmlLineInfo;
	}
}
