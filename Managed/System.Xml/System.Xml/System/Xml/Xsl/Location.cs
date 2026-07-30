using System;
using System.Diagnostics;

namespace System.Xml.Xsl
{
	// Token: 0x020004C0 RID: 1216
	[DebuggerDisplay("({Line},{Pos})")]
	internal struct Location
	{
		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x0600314F RID: 12623 RVA: 0x0011CC49 File Offset: 0x0011AE49
		public int Line
		{
			get
			{
				return (int)(this.value >> 32);
			}
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06003150 RID: 12624 RVA: 0x0011CC55 File Offset: 0x0011AE55
		public int Pos
		{
			get
			{
				return (int)this.value;
			}
		}

		// Token: 0x06003151 RID: 12625 RVA: 0x0011CC5E File Offset: 0x0011AE5E
		public Location(int line, int pos)
		{
			this.value = (ulong)(((long)line << 32) | (long)((ulong)pos));
		}

		// Token: 0x06003152 RID: 12626 RVA: 0x0011CC6E File Offset: 0x0011AE6E
		public Location(Location that)
		{
			this.value = that.value;
		}

		// Token: 0x06003153 RID: 12627 RVA: 0x0011CC7C File Offset: 0x0011AE7C
		public bool LessOrEqual(Location that)
		{
			return this.value <= that.value;
		}

		// Token: 0x0400203D RID: 8253
		private ulong value;
	}
}
