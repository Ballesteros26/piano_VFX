using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200001F RID: 31
	[NativeHeader("Modules/Physics/SphereCollider.h")]
	[RequiredByNativeCode]
	public class SphereCollider : Collider
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00003148 File Offset: 0x00001348
		// (set) Token: 0x06000152 RID: 338 RVA: 0x0000315E File Offset: 0x0000135E
		public Vector3 center
		{
			get
			{
				Vector3 vector;
				this.get_center_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_center_Injected(ref value);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000153 RID: 339
		// (set) Token: 0x06000154 RID: 340
		public extern float radius
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000156 RID: 342
		[MethodImpl(4096)]
		private extern void get_center_Injected(out Vector3 ret);

		// Token: 0x06000157 RID: 343
		[MethodImpl(4096)]
		private extern void set_center_Injected(ref Vector3 value);
	}
}
