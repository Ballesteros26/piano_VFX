using System;

namespace Mono.Math.Prime.Generator
{
	// Token: 0x020000AD RID: 173
	public class NextPrimeFinder : SequentialSearchPrimeGeneratorBase
	{
		// Token: 0x06000698 RID: 1688 RVA: 0x0001FC7F File Offset: 0x0001DE7F
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
