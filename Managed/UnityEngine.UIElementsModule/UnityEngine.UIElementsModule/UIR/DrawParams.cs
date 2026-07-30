using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200024E RID: 590
	internal class DrawParams
	{
		// Token: 0x06001159 RID: 4441 RVA: 0x000489AC File Offset: 0x00046BAC
		public void Reset()
		{
			this.view.Clear();
			this.view.Push(new ViewTransform
			{
				transform = Matrix4x4.identity,
				clipRect = UIRUtility.ToVector4(DrawParams.k_FullNormalizedRect)
			});
			this.scissor.Clear();
			this.scissor.Push(DrawParams.k_UnlimitedRect);
		}

		// Token: 0x0400084D RID: 2125
		internal static readonly Rect k_UnlimitedRect = new Rect(-100000f, -100000f, 200000f, 200000f);

		// Token: 0x0400084E RID: 2126
		internal static readonly Rect k_FullNormalizedRect = new Rect(-1f, -1f, 2f, 2f);

		// Token: 0x0400084F RID: 2127
		internal readonly Stack<ViewTransform> view = new Stack<ViewTransform>(8);

		// Token: 0x04000850 RID: 2128
		internal readonly Stack<Rect> scissor = new Stack<Rect>(8);
	}
}
