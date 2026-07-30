using System;

namespace UnityEngine.UI
{
	// Token: 0x02000045 RID: 69
	[AddComponentMenu("UI/Effects/Position As UV1", 16)]
	public class PositionAsUV1 : BaseMeshEffect
	{
		// Token: 0x0600049A RID: 1178 RVA: 0x00015C41 File Offset: 0x00013E41
		protected PositionAsUV1()
		{
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00015C4C File Offset: 0x00013E4C
		public override void ModifyMesh(VertexHelper vh)
		{
			UIVertex uivertex = default(UIVertex);
			for (int i = 0; i < vh.currentVertCount; i++)
			{
				vh.PopulateUIVertex(ref uivertex, i);
				uivertex.uv1 = new Vector2(uivertex.position.x, uivertex.position.y);
				vh.SetUIVertex(uivertex, i);
			}
		}
	}
}
