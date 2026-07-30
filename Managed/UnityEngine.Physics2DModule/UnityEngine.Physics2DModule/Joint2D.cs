using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000025 RID: 37
	[NativeHeader("Modules/Physics2D/Joint2D.h")]
	[RequireComponent(typeof(Transform), typeof(Rigidbody2D))]
	public class Joint2D : Behaviour
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600037C RID: 892
		public extern Rigidbody2D attachedRigidbody
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600037D RID: 893
		// (set) Token: 0x0600037E RID: 894
		public extern Rigidbody2D connectedBody
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600037F RID: 895
		// (set) Token: 0x06000380 RID: 896
		public extern bool enableCollision
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000381 RID: 897
		// (set) Token: 0x06000382 RID: 898
		public extern float breakForce
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000383 RID: 899
		// (set) Token: 0x06000384 RID: 900
		public extern float breakTorque
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000385 RID: 901 RVA: 0x00007084 File Offset: 0x00005284
		public Vector2 reactionForce
		{
			[NativeMethod("GetReactionForceFixedTime")]
			get
			{
				Vector2 vector;
				this.get_reactionForce_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000386 RID: 902
		public extern float reactionTorque
		{
			[NativeMethod("GetReactionTorqueFixedTime")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000709C File Offset: 0x0000529C
		public Vector2 GetReactionForce(float timeStep)
		{
			Vector2 vector;
			this.GetReactionForce_Injected(timeStep, out vector);
			return vector;
		}

		// Token: 0x06000388 RID: 904
		[MethodImpl(4096)]
		public extern float GetReactionTorque(float timeStep);

		// Token: 0x0600038A RID: 906
		[MethodImpl(4096)]
		private extern void get_reactionForce_Injected(out Vector2 ret);

		// Token: 0x0600038B RID: 907
		[MethodImpl(4096)]
		private extern void GetReactionForce_Injected(float timeStep, out Vector2 ret);
	}
}
