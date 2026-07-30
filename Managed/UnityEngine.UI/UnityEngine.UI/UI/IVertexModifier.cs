using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace UnityEngine.UI
{
	// Token: 0x02000042 RID: 66
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Use IMeshModifier instead", true)]
	public interface IVertexModifier
	{
		// Token: 0x06000495 RID: 1173
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("use IMeshModifier.ModifyMesh (VertexHelper verts)  instead", true)]
		void ModifyVertices(List<UIVertex> verts);
	}
}
