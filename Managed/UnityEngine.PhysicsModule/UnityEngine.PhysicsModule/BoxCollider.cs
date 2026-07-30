using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200001E RID: 30
	[NativeHeader("Modules/Physics/BoxCollider.h")]
	[RequiredByNativeCode]
	public class BoxCollider : Collider
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000146 RID: 326 RVA: 0x000030D0 File Offset: 0x000012D0
		// (set) Token: 0x06000147 RID: 327 RVA: 0x000030E6 File Offset: 0x000012E6
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

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000148 RID: 328 RVA: 0x000030F0 File Offset: 0x000012F0
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00003106 File Offset: 0x00001306
		public Vector3 size
		{
			get
			{
				Vector3 vector;
				this.get_size_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_size_Injected(ref value);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00003110 File Offset: 0x00001310
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00003132 File Offset: 0x00001332
		[Obsolete("Use BoxCollider.size instead.")]
		public Vector3 extents
		{
			get
			{
				return this.size * 0.5f;
			}
			set
			{
				this.size = value * 2f;
			}
		}

		// Token: 0x0600014D RID: 333
		[MethodImpl(4096)]
		private extern void get_center_Injected(out Vector3 ret);

		// Token: 0x0600014E RID: 334
		[MethodImpl(4096)]
		private extern void set_center_Injected(ref Vector3 value);

		// Token: 0x0600014F RID: 335
		[MethodImpl(4096)]
		private extern void get_size_Injected(out Vector3 ret);

		// Token: 0x06000150 RID: 336
		[MethodImpl(4096)]
		private extern void set_size_Injected(ref Vector3 value);
	}
}
