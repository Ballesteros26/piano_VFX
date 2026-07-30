using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200005C RID: 92
	public static class HaltonSeq
	{
		// Token: 0x060001BA RID: 442 RVA: 0x0000E138 File Offset: 0x0000C338
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
