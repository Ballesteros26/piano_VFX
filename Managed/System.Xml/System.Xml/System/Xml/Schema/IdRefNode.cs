using System;

namespace System.Xml.Schema
{
	// Token: 0x0200048D RID: 1165
	internal class IdRefNode
	{
		// Token: 0x06002DAA RID: 11690 RVA: 0x0010A3B9 File Offset: 0x001085B9
		internal IdRefNode(IdRefNode next, string id, int lineNo, int linePos)
		{
			this.Id = id;
			this.LineNo = lineNo;
			this.LinePos = linePos;
			this.Next = next;
		}

		// Token: 0x04001E4D RID: 7757
		internal string Id;

		// Token: 0x04001E4E RID: 7758
		internal int LineNo;

		// Token: 0x04001E4F RID: 7759
		internal int LinePos;

		// Token: 0x04001E50 RID: 7760
		internal IdRefNode Next;
	}
}
