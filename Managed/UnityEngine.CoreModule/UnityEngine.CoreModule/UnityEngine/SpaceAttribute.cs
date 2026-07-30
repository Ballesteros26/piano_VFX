using System;

namespace UnityEngine
{
	// Token: 0x02000182 RID: 386
	[AttributeUsage(256, Inherited = true, AllowMultiple = true)]
	public class SpaceAttribute : PropertyAttribute
	{
		// Token: 0x0600128F RID: 4751 RVA: 0x0001E7D0 File Offset: 0x0001C9D0
		public SpaceAttribute()
		{
			this.height = 8f;
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x0001E7E5 File Offset: 0x0001C9E5
		public SpaceAttribute(float height)
		{
			this.height = height;
		}

		// Token: 0x04000620 RID: 1568
		public readonly float height;
	}
}
