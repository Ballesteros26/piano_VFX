using System;

namespace Mono.Math.Prime.Generator
{
	// Token: 0x0200009E RID: 158
	internal class NextPrimeFinder : SequentialSearchPrimeGeneratorBase
	{
		// Token: 0x0600054E RID: 1358 RVA: 0x0001EF9F File Offset: 0x0001D19F
		protected override BigInteger GenerateSearchBase(int bits, object Context)
		{
			if (Context == null)
			{
				throw new ArgumentNullException("Context");
			}
			BigInteger bigInteger = new BigInteger((BigInteger)Context);
			bigInteger.SetBit(0U);
			return bigInteger;
		}
	}
}
