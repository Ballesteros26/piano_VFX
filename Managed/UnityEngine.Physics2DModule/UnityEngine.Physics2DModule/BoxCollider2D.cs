using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000020 RID: 32
	[NativeHeader("Modules/Physics2D/Public/BoxCollider2D.h")]
	public sealed class BoxCollider2D : Collider2D
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600034A RID: 842 RVA: 0x00006D64 File Offset: 0x00004F64
		// (set) Token: 0x0600034B RID: 843 RVA: 0x00006D7A File Offset: 0x00004F7A
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

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600034C RID: 844
		// (set) Token: 0x0600034D RID: 845
		public extern float edgeRadius
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600034E RID: 846
		// (set) Token: 0x0600034F RID: 847
		public extern bool autoTiling
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000351 RID: 849
		[MethodImpl(4096)]
		private extern void get_size_Injected(out Vector2 ret);

		// Token: 0x06000352 RID: 850
		[MethodImpl(4096)]
		private extern void set_size_Injected(ref Vector2 value);
	}
}
