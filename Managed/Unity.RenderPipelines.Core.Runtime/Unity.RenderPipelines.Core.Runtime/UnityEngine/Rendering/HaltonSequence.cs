using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200005C RID: 92
	public static class HaltonSequence
	{
		// Token: 0x060002BE RID: 702 RVA: 0x0000BBD8 File Offset: 0x00009DD8
		public static float Get(int index, int radix)
		{
			float num = 0f;
			float num2 = 1f / (float)radix;
			while (index > 0)
			{
				num += (float)(index % radix) * num2;
				index /= radix;
				num2 /= (float)radix;
			}
			return num;
		}
	}
}
