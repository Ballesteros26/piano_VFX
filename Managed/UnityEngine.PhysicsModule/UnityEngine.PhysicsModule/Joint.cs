using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000021 RID: 33
	[NativeClass("Unity::Joint")]
	[NativeHeader("Modules/Physics/Joint.h")]
	[RequireComponent(typeof(Rigidbody))]
	public class Joint : Component
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000169 RID: 361
		// (set) Token: 0x0600016A RID: 362
		public extern Rigidbody connectedBody
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600016B RID: 363 RVA: 0x000031F4 File Offset: 0x000013F4
		// (set) Token: 0x0600016C RID: 364 RVA: 0x0000320A File Offset: 0x0000140A
		public Vector3 axis
		{
			get
			{
				Vector3 vector;
				this.get_axis_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_axis_Injected(ref value);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00003214 File Offset: 0x00001414
		// (set) Token: 0x0600016E RID: 366 RVA: 0x0000322A File Offset: 0x0000142A
		public Vector3 anchor
		{
			get
			{
				Vector3 vector;
				this.get_anchor_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_anchor_Injected(ref value);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00003234 File Offset: 0x00001434
		// (set) Token: 0x06000170 RID: 368 RVA: 0x0000324A File Offset: 0x0000144A
		public Vector3 connectedAnchor
		{
			get
			{
				Vector3 vector;
				this.get_connectedAnchor_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_connectedAnchor_Injected(ref value);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000171 RID: 369
		// (set) Token: 0x06000172 RID: 370
		public extern bool autoConfigureConnectedAnchor
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000173 RID: 371
		// (set) Token: 0x06000174 RID: 372
		public extern float breakForce
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000175 RID: 373
		// (set) Token: 0x06000176 RID: 374
		public extern float breakTorque
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000177 RID: 375
		// (set) Token: 0x06000178 RID: 376
		public extern bool enableCollision
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000179 RID: 377
		// (set) Token: 0x0600017A RID: 378
		public extern bool enablePreprocessing
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600017B RID: 379
		// (set) Token: 0x0600017C RID: 380
		public extern float massScale
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600017D RID: 381
		// (set) Token: 0x0600017E RID: 382
		public extern float connectedMassScale
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x0600017F RID: 383
		[MethodImpl(4096)]
		private extern void GetCurrentForces(ref Vector3 linearForce, ref Vector3 angularForce);

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00003254 File Offset: 0x00001454
		public Vector3 currentForce
		{
			get
			{
				Vector3 zero = Vector3.zero;
				Vector3 zero2 = Vector3.zero;
				this.GetCurrentForces(ref zero, ref zero2);
				return zero;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00003280 File Offset: 0x00001480
		public Vector3 currentTorque
		{
			get
			{
				Vector3 zero = Vector3.zero;
				Vector3 zero2 = Vector3.zero;
				this.GetCurrentForces(ref zero, ref zero2);
				return zero2;
			}
		}

		// Token: 0x06000183 RID: 387
		[MethodImpl(4096)]
		private extern void get_axis_Injected(out Vector3 ret);

		// Token: 0x06000184 RID: 388
		[MethodImpl(4096)]
		private extern void set_axis_Injected(ref Vector3 value);

		// Token: 0x06000185 RID: 389
		[MethodImpl(4096)]
		private extern void get_anchor_Injected(out Vector3 ret);

		// Token: 0x06000186 RID: 390
		[MethodImpl(4096)]
		private extern void set_anchor_Injected(ref Vector3 value);

		// Token: 0x06000187 RID: 391
		[MethodImpl(4096)]
		private extern void get_connectedAnchor_Injected(out Vector3 ret);

		// Token: 0x06000188 RID: 392
		[MethodImpl(4096)]
		private extern void set_connectedAnchor_Injected(ref Vector3 value);
	}
}
