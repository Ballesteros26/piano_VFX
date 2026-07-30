using System;

namespace System.Xml
{
	// Token: 0x02000245 RID: 581
	internal class ReaderPositionInfo : PositionInfo
	{
		// Token: 0x06001687 RID: 5767 RVA: 0x0007C27F File Offset: 0x0007A47F
		public ReaderPositionInfo(IXmlLineInfo lineInfo)
		{
			this.lineInfo = lineInfo;
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x0007C28E File Offset: 0x0007A48E
		public override bool HasLineInfo()
		{
			return this.lineInfo.HasLineInfo();
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001689 RID: 5769 RVA: 0x0007C29B File Offset: 0x0007A49B
		public override int LineNumber
		{
			get
			{
				return this.lineInfo.LineNumber;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x0600168A RID: 5770 RVA: 0x0007C2A8 File Offset: 0x0007A4A8
		public override int LinePosition
		{
			get
			{
				return this.lineInfo.LinePosition;
			}
		}

		// Token: 0x04000E30 RID: 3632
		private IXmlLineInfo lineInfo;
	}
}
