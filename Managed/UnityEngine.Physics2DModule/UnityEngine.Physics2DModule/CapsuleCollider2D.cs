using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200001E RID: 30
	[NativeHeader("Modules/Physics2D/Public/CapsuleCollider2D.h")]
	public sealed class CapsuleCollider2D : Collider2D
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00006D04 File Offset: 0x00004F04
		// (set) Token: 0x0600032E RID: 814 RVA: 0x00006D1A File Offset: 0x00004F1A
		public Vector2 size
		{
			get
			{
				Vector2 vector;
				this.get_size_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_size_Injected(ref value);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600032F RID: 815
		// (set) Token: 0x06000330 RID: 816
		public extern CapsuleDirection2D direction
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000332 RID: 818
		[MethodImpl(4096)]
		private extern void get_size_Injected(out Vector2 ret);

		// Token: 0x06000333 RID: 819
		[MethodImpl(4096)]
		private extern void set_size_Injected(ref Vector2 value);
	}
}
