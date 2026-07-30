using System;

namespace System.Xml
{
	// Token: 0x02000247 RID: 583
	internal struct LineInfo
	{
		// Token: 0x0600168E RID: 5774 RVA: 0x0007C2B5 File Offset: 0x0007A4B5
		public LineInfo(int lineNo, int linePos)
		{
			this.lineNo = lineNo;
			this.linePos = linePos;
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x0007C2B5 File Offset: 0x0007A4B5
		public void Set(int lineNo, int linePos)
		{
			this.lineNo = lineNo;
			this.linePos = linePos;
		}

		// Token: 0x04000E31 RID: 3633
		internal int lineNo;

		// Token: 0x04000E32 RID: 3634
		internal int linePos;
	}
}
