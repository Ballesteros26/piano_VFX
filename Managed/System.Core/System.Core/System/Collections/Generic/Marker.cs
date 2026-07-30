using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x0200034D RID: 845
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	internal struct Marker
	{
		// Token: 0x060019AD RID: 6573 RVA: 0x00054515 File Offset: 0x00052715
		public Marker(int count, int index)
		{
			this.Count = count;
			this.Index = index;
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x060019AE RID: 6574 RVA: 0x00054525 File Offset: 0x00052725
		public int Count { get; }

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x060019AF RID: 6575 RVA: 0x0005452D File Offset: 0x0005272D
		public int Index { get; }

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x060019B0 RID: 6576 RVA: 0x00054535 File Offset: 0x00052735
		private string DebuggerDisplay
		{
			get
			{
				return string.Format("{0}: {1}, {2}: {3}", new object[] { "Index", this.Index, "Count", this.Count });
			}
		}
	}
}
