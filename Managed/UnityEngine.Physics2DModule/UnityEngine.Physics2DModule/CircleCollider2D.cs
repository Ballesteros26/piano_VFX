using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200001D RID: 29
	[NativeHeader("Modules/Physics2D/Public/CircleCollider2D.h")]
	public sealed class CircleCollider2D : Collider2D
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600032A RID: 810
		// (set) Token: 0x0600032B RID: 811
		public extern float radius
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}
	}
}
