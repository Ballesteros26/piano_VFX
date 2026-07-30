using System;

namespace System.Net.Mime
{
	// Token: 0x02000594 RID: 1428
	internal class Base64WriteStateInfo : WriteStateInfoBase
	{
		// Token: 0x06002C6C RID: 11372 RVA: 0x000AF44B File Offset: 0x000AD64B
		internal Base64WriteStateInfo()
		{
		}

		// Token: 0x06002C6D RID: 11373 RVA: 0x000AF453 File Offset: 0x000AD653
		internal Base64WriteStateInfo(int bufferSize, byte[] header, byte[] footer, int maxLineLength, int mimeHeaderLength)
			: base(bufferSize, header, footer, maxLineLength, mimeHeaderLength)
		{
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06002C6E RID: 11374 RVA: 0x000AF462 File Offset: 0x000AD662
		// (set) Token: 0x06002C6F RID: 11375 RVA: 0x000AF46A File Offset: 0x000AD66A
		internal int Padding { get; set; }

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06002C70 RID: 11376 RVA: 0x000AF473 File Offset: 0x000AD673
		// (set) Token: 0x06002C71 RID: 11377 RVA: 0x000AF47B File Offset: 0x000AD67B
		internal byte LastBits { get; set; }
	}
}
