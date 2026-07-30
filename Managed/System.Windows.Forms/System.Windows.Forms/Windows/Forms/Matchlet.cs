using System;
using System.Collections;

namespace System.Windows.Forms
{
	// Token: 0x02000260 RID: 608
	internal class Matchlet
	{
		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x060027C0 RID: 10176 RVA: 0x00098B98 File Offset: 0x00096D98
		// (set) Token: 0x060027BF RID: 10175 RVA: 0x00098B8C File Offset: 0x00096D8C
		public byte[] ByteValue
		{
			get
			{
				return this.byteValue;
			}
			set
			{
				this.byteValue = value;
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x060027C2 RID: 10178 RVA: 0x00098BAC File Offset: 0x00096DAC
		// (set) Token: 0x060027C1 RID: 10177 RVA: 0x00098BA0 File Offset: 0x00096DA0
		public byte[] Mask
		{
			get
			{
				return this.mask;
			}
			set
			{
				this.mask = value;
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x060027C4 RID: 10180 RVA: 0x00098BC0 File Offset: 0x00096DC0
		// (set) Token: 0x060027C3 RID: 10179 RVA: 0x00098BB4 File Offset: 0x00096DB4
		public int Offset
		{
			get
			{
				return this.offset;
			}
			set
			{
				this.offset = value;
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x060027C6 RID: 10182 RVA: 0x00098BD4 File Offset: 0x00096DD4
		// (set) Token: 0x060027C5 RID: 10181 RVA: 0x00098BC8 File Offset: 0x00096DC8
		public int OffsetLength
		{
			get
			{
				return this.offsetLength;
			}
			set
			{
				this.offsetLength = value;
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x060027C8 RID: 10184 RVA: 0x00098BE8 File Offset: 0x00096DE8
		// (set) Token: 0x060027C7 RID: 10183 RVA: 0x00098BDC File Offset: 0x00096DDC
		public int WordSize
		{
			get
			{
				return this.wordSize;
			}
			set
			{
				this.wordSize = value;
			}
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x060027C9 RID: 10185 RVA: 0x00098BF0 File Offset: 0x00096DF0
		public ArrayList Matchlets
		{
			get
			{
				return this.matchlets;
			}
		}

		// Token: 0x040013DF RID: 5087
		private byte[] byteValue;

		// Token: 0x040013E0 RID: 5088
		private byte[] mask;

		// Token: 0x040013E1 RID: 5089
		private int offset;

		// Token: 0x040013E2 RID: 5090
		private int offsetLength;

		// Token: 0x040013E3 RID: 5091
		private int wordSize = 1;

		// Token: 0x040013E4 RID: 5092
		private ArrayList matchlets = new ArrayList();
	}
}
