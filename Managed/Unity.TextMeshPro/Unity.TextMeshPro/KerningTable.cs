using System;
using System.Collections.Generic;
using System.Linq;

namespace TMPro
{
	// Token: 0x02000021 RID: 33
	[Serializable]
	public class KerningTable
	{
		// Token: 0x06000118 RID: 280 RVA: 0x0000769F File Offset: 0x0000589F
		public KerningTable()
		{
			this.kerningPairs = new List<KerningPair>();
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000076B4 File Offset: 0x000058B4
		public void AddKerningPair()
		{
			if (this.kerningPairs.Count == 0)
			{
				this.kerningPairs.Add(new KerningPair(0U, 0U, 0f));
				return;
			}
			uint firstGlyph = this.kerningPairs.Last<KerningPair>().firstGlyph;
			uint secondGlyph = this.kerningPairs.Last<KerningPair>().secondGlyph;
			float xOffset = this.kerningPairs.Last<KerningPair>().xOffset;
			this.kerningPairs.Add(new KerningPair(firstGlyph, secondGlyph, xOffset));
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000772C File Offset: 0x0000592C
		public int AddKerningPair(uint first, uint second, float offset)
		{
			if (this.kerningPairs.FindIndex((KerningPair item) => item.firstGlyph == first && item.secondGlyph == second) == -1)
			{
				this.kerningPairs.Add(new KerningPair(first, second, offset));
				return 0;
			}
			return -1;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00007788 File Offset: 0x00005988
		public int AddGlyphPairAdjustmentRecord(uint first, GlyphValueRecord_Legacy firstAdjustments, uint second, GlyphValueRecord_Legacy secondAdjustments)
		{
			if (this.kerningPairs.FindIndex((KerningPair item) => item.firstGlyph == first && item.secondGlyph == second) == -1)
			{
				this.kerningPairs.Add(new KerningPair(first, firstAdjustments, second, secondAdjustments));
				return 0;
			}
			return -1;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000077E8 File Offset: 0x000059E8
		public void RemoveKerningPair(int left, int right)
		{
			int num = this.kerningPairs.FindIndex((KerningPair item) => (ulong)item.firstGlyph == (ulong)((long)left) && (ulong)item.secondGlyph == (ulong)((long)right));
			if (num != -1)
			{
				this.kerningPairs.RemoveAt(num);
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00007831 File Offset: 0x00005A31
		public void RemoveKerningPair(int index)
		{
			this.kerningPairs.RemoveAt(index);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00007840 File Offset: 0x00005A40
		public void SortKerningPairs()
		{
			if (this.kerningPairs.Count > 0)
			{
				this.kerningPairs = (from s in this.kerningPairs
					orderby s.firstGlyph, s.secondGlyph
					select s).ToList<KerningPair>();
			}
		}

		// Token: 0x040000EC RID: 236
		public List<KerningPair> kerningPairs;
	}
}
