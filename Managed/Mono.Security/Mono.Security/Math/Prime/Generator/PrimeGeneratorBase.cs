using System;

namespace Mono.Math.Prime.Generator
{
	// Token: 0x020000AE RID: 174
	public abstract class PrimeGeneratorBase
	{
		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x0001FCA9 File Offset: 0x0001DEA9
		public virtual ConfidenceFactor Confidence
		{
			get
			{
				return ConfidenceFactor.Medium;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x0001FCAC File Offset: 0x0001DEAC
		public virtual PrimalityTest PrimalityTest
		{
			get
			{
				return new PrimalityTest(PrimalityTests.RabinMillerTest);
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x0001FCBA File Offset: 0x0001DEBA
		public virtual int TrialDivisionBounds
		{
			get
			{
				return 4000;
			}
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0001FCC1 File Offset: 0x0001DEC1
		protected bool PostTrialDivisionTests(BigInteger bi)
		{
			return this.PrimalityTest(bi, this.Confidence);
		}

		// Token: 0x0600069E RID: 1694
		public abstract BigInteger GenerateNewPrime(int bits);
	}
}
