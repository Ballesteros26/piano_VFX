using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200002B RID: 43
	[NativeHeader("Modules/Physics2D/RelativeJoint2D.h")]
	public sealed class RelativeJoint2D : Joint2D
	{
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060003BE RID: 958
		// (set) Token: 0x060003BF RID: 959
		public extern float maxForce
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060003C0 RID: 960
		// (set) Token: 0x060003C1 RID: 961
		public extern float maxTorque
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060003C2 RID: 962
		// (set) Token: 0x060003C3 RID: 963
		public extern float correctionScale
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060003C4 RID: 964
		// (set) Token: 0x060003C5 RID: 965
		public extern bool autoConfigureOffset
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00007148 File Offset: 0x00005348
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x0000715E File Offset: 0x0000535E
		public Vector2 linearOffset
		{
			get
			{
				Vector2 vector;
				this.get_linearOffset_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_linearOffset_Injected(ref value);
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060003C8 RID: 968
		// (set) Token: 0x060003C9 RID: 969
		public extern float angularOffset
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00007168 File Offset: 0x00005368
		public Vector2 target
		{
			get
			{
				Vector2 vector;
				this.get_target_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x060003CC RID: 972
		[MethodImpl(4096)]
		private extern void get_linearOffset_Injected(out Vector2 ret);

		// Token: 0x060003CD RID: 973
		[MethodImpl(4096)]
		private extern void set_linearOffset_Injected(ref Vector2 value);

		// Token: 0x060003CE RID: 974
		[MethodImpl(4096)]
		private extern void get_target_Injected(out Vector2 ret);
	}
}
