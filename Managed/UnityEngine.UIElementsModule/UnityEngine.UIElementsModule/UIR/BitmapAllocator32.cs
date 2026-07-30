using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000235 RID: 565
	internal struct BitmapAllocator32
	{
		// Token: 0x060010EB RID: 4331 RVA: 0x00044678 File Offset: 0x00042878
		public void Construct(int pageHeight, int entryWidth = 1, int entryHeight = 1)
		{
			this.m_PageHeight = pageHeight;
			this.m_Pages = new List<BitmapAllocator32.Page>(1);
			this.m_AllocMap = new List<uint>(this.m_PageHeight * this.m_Pages.Capacity);
			this.m_EntryWidth = entryWidth;
			this.m_EntryHeight = entryHeight;
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x000446C4 File Offset: 0x000428C4
		public void ForceFirstAlloc(ushort firstPageX, ushort firstPageY)
		{
			this.m_AllocMap.Add(4294967294U);
			for (int i = 1; i < this.m_PageHeight; i++)
			{
				this.m_AllocMap.Add(uint.MaxValue);
			}
			this.m_Pages.Add(new BitmapAllocator32.Page
			{
				x = firstPageX,
				y = firstPageY,
				freeSlots = 32 * this.m_PageHeight - 1
			});
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x0004473C File Offset: 0x0004293C
		public BMPAlloc Allocate(UIRAtlasManager atlasManager)
		{
			int count = this.m_Pages.Count;
			for (int i = 0; i < count; i++)
			{
				BitmapAllocator32.Page page = this.m_Pages[i];
				bool flag = page.freeSlots == 0;
				if (!flag)
				{
					int j = i * this.m_PageHeight;
					int num = j + this.m_PageHeight;
					while (j < num)
					{
						uint num2 = this.m_AllocMap[j];
						bool flag2 = num2 == 0U;
						if (!flag2)
						{
							byte b = BitmapAllocator32.CountTrailingZeroes(num2);
							this.m_AllocMap[j] = num2 & ~(1U << (int)b);
							page.freeSlots--;
							this.m_Pages[i] = page;
							return new BMPAlloc
							{
								page = i,
								pageLine = (ushort)(j - i * this.m_PageHeight),
								bitIndex = b,
								ownedState = OwnedState.Owned
							};
						}
						j++;
					}
				}
			}
			RectInt rectInt;
			bool flag3 = atlasManager == null || !atlasManager.AllocateRect(32 * this.m_EntryWidth, this.m_PageHeight * this.m_EntryHeight, out rectInt);
			if (flag3)
			{
				return BMPAlloc.Invalid;
			}
			this.m_AllocMap.Capacity += this.m_PageHeight;
			this.m_AllocMap.Add(4294967294U);
			for (int k = 1; k < this.m_PageHeight; k++)
			{
				this.m_AllocMap.Add(uint.MaxValue);
			}
			this.m_Pages.Add(new BitmapAllocator32.Page
			{
				x = (ushort)rectInt.xMin,
				y = (ushort)rectInt.yMin,
				freeSlots = 32 * this.m_PageHeight - 1
			});
			return new BMPAlloc
			{
				page = this.m_Pages.Count - 1,
				ownedState = OwnedState.Owned
			};
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x00044950 File Offset: 0x00042B50
		public void Free(BMPAlloc alloc)
		{
			Debug.Assert(alloc.ownedState == OwnedState.Owned);
			int num = alloc.page * this.m_PageHeight + (int)alloc.pageLine;
			this.m_AllocMap[num] = this.m_AllocMap[num] | (1U << (int)alloc.bitIndex);
			BitmapAllocator32.Page page = this.m_Pages[alloc.page];
			page.freeSlots++;
			this.m_Pages[alloc.page] = page;
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x060010EF RID: 4335 RVA: 0x000449D8 File Offset: 0x00042BD8
		public int entryWidth
		{
			get
			{
				return this.m_EntryWidth;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060010F0 RID: 4336 RVA: 0x000449F0 File Offset: 0x00042BF0
		public int entryHeight
		{
			get
			{
				return this.m_EntryHeight;
			}
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x00044A08 File Offset: 0x00042C08
		internal void GetAllocPageAtlasLocation(int page, out ushort x, out ushort y)
		{
			BitmapAllocator32.Page page2 = this.m_Pages[page];
			x = page2.x;
			y = page2.y;
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x00044A34 File Offset: 0x00042C34
		private static byte CountTrailingZeroes(uint val)
		{
			byte b = 0;
			bool flag = (val & 65535U) == 0U;
			if (flag)
			{
				val >>= 16;
				b = 16;
			}
			bool flag2 = (val & 255U) == 0U;
			if (flag2)
			{
				val >>= 8;
				b += 8;
			}
			bool flag3 = (val & 15U) == 0U;
			if (flag3)
			{
				val >>= 4;
				b += 4;
			}
			bool flag4 = (val & 3U) == 0U;
			if (flag4)
			{
				val >>= 2;
				b += 2;
			}
			bool flag5 = (val & 1U) == 0U;
			if (flag5)
			{
				b += 1;
			}
			return b;
		}

		// Token: 0x0400079D RID: 1949
		public const int kPageWidth = 32;

		// Token: 0x0400079E RID: 1950
		private int m_PageHeight;

		// Token: 0x0400079F RID: 1951
		private List<BitmapAllocator32.Page> m_Pages;

		// Token: 0x040007A0 RID: 1952
		private List<uint> m_AllocMap;

		// Token: 0x040007A1 RID: 1953
		private int m_EntryWidth;

		// Token: 0x040007A2 RID: 1954
		private int m_EntryHeight;

		// Token: 0x02000236 RID: 566
		private struct Page
		{
			// Token: 0x040007A3 RID: 1955
			public ushort x;

			// Token: 0x040007A4 RID: 1956
			public ushort y;

			// Token: 0x040007A5 RID: 1957
			public int freeSlots;
		}
	}
}
