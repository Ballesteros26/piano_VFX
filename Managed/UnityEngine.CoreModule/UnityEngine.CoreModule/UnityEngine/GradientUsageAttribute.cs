using System;

namespace UnityEngine
{
	// Token: 0x02000189 RID: 393
	[AttributeUsage(256, Inherited = true, AllowMultiple = false)]
	public sealed class GradientUsageAttribute : PropertyAttribute
	{
		// Token: 0x0600129B RID: 4763 RVA: 0x0001E9B8 File Offset: 0x0001CBB8
		public GradientUsageAttribute(bool hdr)
		{
			this.hdr = hdr;
		}

		// Token: 0x0400062E RID: 1582
		public readonly bool hdr = false;
	}
}
