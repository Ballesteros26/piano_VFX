using System;

namespace System.Threading.Tasks
{
	// Token: 0x020004EA RID: 1258
	internal class RangeManager
	{
		// Token: 0x060039E7 RID: 14823 RVA: 0x000D1C70 File Offset: 0x000CFE70
		internal RangeManager(long nFromInclusive, long nToExclusive, long nStep, int nNumExpectedWorkers)
		{
			this.m_nCurrentIndexRangeToAssign = 0;
			this.m_nStep = nStep;
			if (nNumExpectedWorkers == 1)
			{
				nNumExpectedWorkers = 2;
			}
			long num = nToExclusive - nFromInclusive;
			ulong num2 = (ulong)(num / (long)nNumExpectedWorkers);
			num2 -= num2 % (ulong)nStep;
			if (num2 == 0UL)
			{
				num2 = (ulong)nStep;
			}
			int num3 = (int)(num / (long)num2);
			if (num % (long)num2 != 0L)
			{
				num3++;
			}
			long num4 = (long)num2;
			this._use32BitCurrentIndex = IntPtr.Size == 4 && num4 <= 2147483647L;
			this.m_indexRanges = new IndexRange[num3];
			long num5 = nFromInclusive;
			for (int i = 0; i < num3; i++)
			{
				this.m_indexRanges[i].m_nFromInclusive = num5;
				this.m_indexRanges[i].m_nSharedCurrentIndexOffset = null;
				this.m_indexRanges[i].m_bRangeFinished = 0;
				num5 += num4;
				if (num5 < num5 - num4 || num5 > nToExclusive)
				{
					num5 = nToExclusive;
				}
				this.m_indexRanges[i].m_nToExclusive = num5;
			}
		}

		// Token: 0x060039E8 RID: 14824 RVA: 0x000D1D58 File Offset: 0x000CFF58
		internal RangeWorker RegisterNewWorker()
		{
			int num = (Interlocked.Increment(ref this.m_nCurrentIndexRangeToAssign) - 1) % this.m_indexRanges.Length;
			return new RangeWorker(this.m_indexRanges, num, this.m_nStep, this._use32BitCurrentIndex);
		}

		// Token: 0x04001E51 RID: 7761
		internal readonly IndexRange[] m_indexRanges;

		// Token: 0x04001E52 RID: 7762
		internal readonly bool _use32BitCurrentIndex;

		// Token: 0x04001E53 RID: 7763
		internal int m_nCurrentIndexRangeToAssign;

		// Token: 0x04001E54 RID: 7764
		internal long m_nStep;
	}
}
