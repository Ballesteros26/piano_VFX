using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.TextCore.LowLevel;

namespace UnityEngine.TextCore
{
	// Token: 0x02000010 RID: 16
	[Serializable]
	internal class KerningTable
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x00005248 File Offset: 0x00003448
		public KerningTable()
		{
			this.kerningPairs = new List<KerningPair>();
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00005260 File Offset: 0x00003460
		public int AddGlyphPairAdjustmentRecord(uint first, GlyphValueRecord firstAdjustments, uint second, GlyphValueRecord secondAdjustments)
		{
			int num = this.kerningPairs.FindIndex((KerningPair item) => item.firstGlyph == first && item.secondGlyph == second);
			bool flag = num == -1;
			int num2;
			if (flag)
			{
				this.kerningPairs.Add(new KerningPair(first, firstAdjustments, second, secondAdjustments));
				num2 = 0;
			}
			else
			{
				num2 = -1;
			}
			return num2;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000052CC File Offset: 0x000034CC
		public void RemoveKerningPair(int left, int right)
		{
			int num = this.kerningPairs.FindIndex((KerningPair item) => (ulong)item.firstGlyph == (ulong)((long)left) && (ulong)item.secondGlyph == (ulong)((long)right));
			bool flag = num != -1;
			if (flag)
			{
				this.kerningPairs.RemoveAt(num);
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000531E File Offset: 0x0000351E
		public void RemoveKerningPair(int index)
		{
			this.kerningPairs.RemoveAt(index);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00005330 File Offset: 0x00003530
		public void SortKerningPairs()
		{
			bool flag = this.kerningPairs.Count > 0;
			if (flag)
			{
				this.kerningPairs = Enumerable.ToList<KerningPair>(Enumerable.ThenBy<KerningPair, uint>(Enumerable.OrderBy<KerningPair, uint>(this.kerningPairs, (KerningPair s) => s.firstGlyph), (KerningPair s) => s.secondGlyph));
			}
		}

		// Token: 0x0400006B RID: 107
		public List<KerningPair> kerningPairs;
	}
}
