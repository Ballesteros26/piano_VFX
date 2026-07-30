using System;
using UnityEngine;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x0200003B RID: 59
	public class TMP_SelectionCaret : MaskableGraphic
	{
		// Token: 0x06000275 RID: 629 RVA: 0x0000FAAA File Offset: 0x0000DCAA
		public override void Cull(Rect clipRect, bool validRect)
		{
			if (validRect)
			{
				base.canvasRenderer.cull = false;
				CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
				return;
			}
			base.Cull(clipRect, validRect);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x000027BA File Offset: 0x000009BA
		protected override void UpdateGeometry()
		{
		}
	}
}
