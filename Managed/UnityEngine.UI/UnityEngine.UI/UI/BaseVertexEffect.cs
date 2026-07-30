using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace UnityEngine.UI
{
	// Token: 0x02000040 RID: 64
	[Obsolete("Use BaseMeshEffect instead", true)]
	public abstract class BaseVertexEffect
	{
		// Token: 0x0600048C RID: 1164
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use BaseMeshEffect.ModifyMeshes instead", true)]
		public abstract void ModifyVertices(List<UIVertex> vertices);
	}
}
