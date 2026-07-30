using System;

namespace UnityEngine.UI
{
	// Token: 0x02000043 RID: 67
	public interface IMeshModifier
	{
		// Token: 0x06000496 RID: 1174
		[Obsolete("use IMeshModifier.ModifyMesh (VertexHelper verts) instead", false)]
		void ModifyMesh(Mesh mesh);

		// Token: 0x06000497 RID: 1175
		void ModifyMesh(VertexHelper verts);
	}
}
