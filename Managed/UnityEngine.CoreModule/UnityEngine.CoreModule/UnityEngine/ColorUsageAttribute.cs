using System;

namespace UnityEngine
{
	// Token: 0x02000188 RID: 392
	[AttributeUsage(256, Inherited = true, AllowMultiple = false)]
	public sealed class ColorUsageAttribute : PropertyAttribute
	{
		// Token: 0x06001298 RID: 4760 RVA: 0x0001E884 File Offset: 0x0001CA84
		public ColorUsageAttribute(bool showAlpha)
		{
			this.showAlpha = showAlpha;
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x0001E8DC File Offset: 0x0001CADC
		public ColorUsageAttribute(bool showAlpha, bool hdr)
		{
			this.showAlpha = showAlpha;
			this.hdr = hdr;
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x0001E93C File Offset: 0x0001CB3C
		[Obsolete("Brightness and exposure parameters are no longer used for anything. Use ColorUsageAttribute(bool showAlpha, bool hdr)")]
		public ColorUsageAttribute(bool showAlpha, bool hdr, float minBrightness, float maxBrightness, float minExposureValue, float maxExposureValue)
		{
			this.showAlpha = showAlpha;
			this.hdr = hdr;
			this.minBrightness = minBrightness;
			this.maxBrightness = maxBrightness;
			this.minExposureValue = minExposureValue;
			this.maxExposureValue = maxExposureValue;
		}

		// Token: 0x04000628 RID: 1576
		public readonly bool showAlpha = true;

		// Token: 0x04000629 RID: 1577
		public readonly bool hdr = false;

		// Token: 0x0400062A RID: 1578
		[Obsolete("This field is no longer used for anything.")]
		public readonly float minBrightness = 0f;

		// Token: 0x0400062B RID: 1579
		[Obsolete("This field is no longer used for anything.")]
		public readonly float maxBrightness = 8f;

		// Token: 0x0400062C RID: 1580
		[Obsolete("This field is no longer used for anything.")]
		public readonly float minExposureValue = 0.125f;

		// Token: 0x0400062D RID: 1581
		[Obsolete("This field is no longer used for anything.")]
		public readonly float maxExposureValue = 3f;
	}
}
