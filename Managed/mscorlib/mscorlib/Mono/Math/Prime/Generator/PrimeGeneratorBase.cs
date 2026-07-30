using System;

namespace Mono.Math.Prime.Generator
{
	// Token: 0x0200009F RID: 159
	internal abstract class PrimeGeneratorBase
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x0001EFC9 File Offset: 0x0001D1C9
		public virtual ConfidenceFactor Confidence
		{
			get
			{
				return ConfidenceFactor.Medium;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x0001EFCC File Offset: 0x0001D1CC
		public virtual PrimalityTest PrimalityTest
		{
			get
			{
				return new PrimalityTest(PrimalityTests.RabinMillerTest);
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x0001EFDA File Offset: 0x0001D1DA
		public virtual int TrialDivisionBounds
		{
			get
			{
				return 4000;
			}
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0001EFE1 File Offset: 0x0001D1E1
		protected bool PostTrialDivisionTests(BigInteger bi)
		{
			return this.PrimalityTest(bi, this.Confidence);
		}

		// Token: 0x06000554 RID: 1364
		public abstract BigInteger GenerateNewPrime(int bits);
	}
}
