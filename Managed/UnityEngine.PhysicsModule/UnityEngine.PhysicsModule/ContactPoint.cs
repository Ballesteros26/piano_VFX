using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000027 RID: 39
	[UsedByNativeCode]
	[NativeHeader("Modules/Physics/MessageParameters.h")]
	public struct ContactPoint
	{
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00003658 File Offset: 0x00001858
		public Vector3 point
		{
			get
			{
				return this.m_Point;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00003670 File Offset: 0x00001870
		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00003688 File Offset: 0x00001888
		public Collider thisCollider
		{
			get
			{
				return ContactPoint.GetColliderByInstanceID(this.m_ThisColliderInstanceID);
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000235 RID: 565 RVA: 0x000036A8 File Offset: 0x000018A8
		public Collider otherCollider
		{
			get
			{
				return ContactPoint.GetColliderByInstanceID(this.m_OtherColliderInstanceID);
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000236 RID: 566 RVA: 0x000036C8 File Offset: 0x000018C8
		public float separation
		{
			get
			{
				return this.m_Separation;
			}
		}

		// Token: 0x06000237 RID: 567
		[FreeFunction]
		[MethodImpl(4096)]
		private static extern Collider GetColliderByInstanceID(int instanceID);

		// Token: 0x04000073 RID: 115
		internal Vector3 m_Point;

		// Token: 0x04000074 RID: 116
		internal Vector3 m_Normal;

		// Token: 0x04000075 RID: 117
		internal int m_ThisColliderInstanceID;

		// Token: 0x04000076 RID: 118
		internal int m_OtherColliderInstanceID;

		// Token: 0x04000077 RID: 119
		internal float m_Separation;
	}
}
