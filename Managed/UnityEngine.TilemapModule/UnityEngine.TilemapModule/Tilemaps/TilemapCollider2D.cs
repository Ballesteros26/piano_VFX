using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Tilemaps
{
	// Token: 0x02000014 RID: 20
	[RequireComponent(typeof(Tilemap))]
	[NativeType(Header = "Modules/Tilemap/Public/TilemapCollider2D.h")]
	public sealed class TilemapCollider2D : Collider2D
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000C5 RID: 197
		// (set) Token: 0x060000C6 RID: 198
		public extern uint maximumTileChangeCount
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000C7 RID: 199
		// (set) Token: 0x060000C8 RID: 200
		public extern float extrusionFactor
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}
	}
}
