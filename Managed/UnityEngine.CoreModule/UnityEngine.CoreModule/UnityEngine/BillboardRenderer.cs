using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000D5 RID: 213
	[NativeHeader("Runtime/Graphics/Billboard/BillboardRenderer.h")]
	public sealed class BillboardRenderer : Renderer
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000622 RID: 1570
		// (set) Token: 0x06000623 RID: 1571
		public extern BillboardAsset billboard
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}
	}
}
