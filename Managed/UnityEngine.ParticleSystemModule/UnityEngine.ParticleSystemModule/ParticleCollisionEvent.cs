using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000054 RID: 84
	[RequiredByNativeCode(Optional = true)]
	public struct ParticleCollisionEvent
	{
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x00006128 File Offset: 0x00004328
		public Vector3 intersection
		{
			get
			{
				return this.m_Intersection;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x00006140 File Offset: 0x00004340
		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x00006158 File Offset: 0x00004358
		public Vector3 velocity
		{
			get
			{
				return this.m_Velocity;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x00006170 File Offset: 0x00004370
		public Component colliderComponent
		{
			get
			{
				return ParticleCollisionEvent.InstanceIDToColliderComponent(this.m_ColliderInstanceID);
			}
		}

		// Token: 0x060006C3 RID: 1731
		[FreeFunction(Name = "ParticleSystemScriptBindings::InstanceIDToColliderComponent")]
		[MethodImpl(4096)]
		private static extern Component InstanceIDToColliderComponent(int instanceID);

		// Token: 0x04000163 RID: 355
		internal Vector3 m_Intersection;

		// Token: 0x04000164 RID: 356
		internal Vector3 m_Normal;

		// Token: 0x04000165 RID: 357
		internal Vector3 m_Velocity;

		// Token: 0x04000166 RID: 358
		internal int m_ColliderInstanceID;
	}
}
