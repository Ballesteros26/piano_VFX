using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000231 RID: 561
	internal class GradientRemap : LinkedPoolItem<GradientRemap>
	{
		// Token: 0x060010D5 RID: 4309 RVA: 0x00043FE0 File Offset: 0x000421E0
		public void Reset()
		{
			this.origIndex = 0;
			this.destIndex = 0;
			this.location = default(RectInt);
			this.isAtlassed = false;
		}

		// Token: 0x04000785 RID: 1925
		public int origIndex;

		// Token: 0x04000786 RID: 1926
		public int destIndex;

		// Token: 0x04000787 RID: 1927
		public RectInt location;

		// Token: 0x04000788 RID: 1928
		public GradientRemap next;

		// Token: 0x04000789 RID: 1929
		public bool isAtlassed;
	}
}
