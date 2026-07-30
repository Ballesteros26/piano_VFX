using System;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x020003B8 RID: 952
	public struct LinearColor
	{
		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06002163 RID: 8547 RVA: 0x00038050 File Offset: 0x00036250
		// (set) Token: 0x06002164 RID: 8548 RVA: 0x00038068 File Offset: 0x00036268
		public float red
		{
			get
			{
				return this.m_red;
			}
			set
			{
				bool flag = value < 0f || value > 1f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("Red color (" + value + ") must be in range [0;1].");
				}
				this.m_red = value;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06002165 RID: 8549 RVA: 0x000380B0 File Offset: 0x000362B0
		// (set) Token: 0x06002166 RID: 8550 RVA: 0x000380C8 File Offset: 0x000362C8
		public float green
		{
			get
			{
				return this.m_green;
			}
			set
			{
				bool flag = value < 0f || value > 1f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("Green color (" + value + ") must be in range [0;1].");
				}
				this.m_green = value;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06002167 RID: 8551 RVA: 0x00038110 File Offset: 0x00036310
		// (set) Token: 0x06002168 RID: 8552 RVA: 0x00038128 File Offset: 0x00036328
		public float blue
		{
			get
			{
				return this.m_blue;
			}
			set
			{
				bool flag = value < 0f || value > 1f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("Blue color (" + value + ") must be in range [0;1].");
				}
				this.m_blue = value;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06002169 RID: 8553 RVA: 0x00038170 File Offset: 0x00036370
		// (set) Token: 0x0600216A RID: 8554 RVA: 0x00038188 File Offset: 0x00036388
		public float intensity
		{
			get
			{
				return this.m_intensity;
			}
			set
			{
				bool flag = value < 0f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("Intensity (" + value + ") must be positive.");
				}
				this.m_intensity = value;
			}
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x000381C4 File Offset: 0x000363C4
		public static LinearColor Convert(Color color, float intensity)
		{
			Color color2 = (GraphicsSettings.lightsUseLinearIntensity ? color.linear.RGBMultiplied(intensity) : color.RGBMultiplied(intensity).linear);
			float maxColorComponent = color2.maxColorComponent;
			bool flag = maxColorComponent <= 0f;
			LinearColor linearColor;
			if (flag)
			{
				linearColor = LinearColor.Black();
			}
			else
			{
				float num = 1f / color2.maxColorComponent;
				LinearColor linearColor2;
				linearColor2.m_red = color2.r * num;
				linearColor2.m_green = color2.g * num;
				linearColor2.m_blue = color2.b * num;
				linearColor2.m_intensity = maxColorComponent;
				linearColor = linearColor2;
			}
			return linearColor;
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x0003826C File Offset: 0x0003646C
		public static LinearColor Black()
		{
			LinearColor linearColor;
			linearColor.m_red = (linearColor.m_green = (linearColor.m_blue = (linearColor.m_intensity = 0f)));
			return linearColor;
		}

		// Token: 0x04000BE0 RID: 3040
		private float m_red;

		// Token: 0x04000BE1 RID: 3041
		private float m_green;

		// Token: 0x04000BE2 RID: 3042
		private float m_blue;

		// Token: 0x04000BE3 RID: 3043
		private float m_intensity;
	}
}
