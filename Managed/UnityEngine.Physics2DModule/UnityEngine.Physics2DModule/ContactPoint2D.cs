using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000014 RID: 20
	[RequiredByNativeCode(Optional = false, GenerateProxy = true)]
	[NativeClass("ScriptingContactPoint2D", "struct ScriptingContactPoint2D;")]
	[NativeHeader("Modules/Physics2D/Public/PhysicsScripting2D.h")]
	public struct ContactPoint2D
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00005BDC File Offset: 0x00003DDC
		public Vector2 point
		{
			get
			{
				return this.m_Point;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00005BF4 File Offset: 0x00003DF4
		public Vector2 normal
		{
			get
			{
				return this.m_Normal;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00005C0C File Offset: 0x00003E0C
		public float separation
		{
			get
			{
				return this.m_Separation;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00005C24 File Offset: 0x00003E24
		public float normalImpulse
		{
			get
			{
				return this.m_NormalImpulse;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00005C3C File Offset: 0x00003E3C
		public float tangentImpulse
		{
			get
			{
				return this.m_TangentImpulse;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00005C54 File Offset: 0x00003E54
		public Vector2 relativeVelocity
		{
			get
			{
				return this.m_RelativeVelocity;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00005C6C File Offset: 0x00003E6C
		public Collider2D collider
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_Collider) as Collider2D;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00005C90 File Offset: 0x00003E90
		public Collider2D otherCollider
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_OtherCollider) as Collider2D;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00005CB4 File Offset: 0x00003EB4
		public Rigidbody2D rigidbody
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_Rigidbody) as Rigidbody2D;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00005CD8 File Offset: 0x00003ED8
		public Rigidbody2D otherRigidbody
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_OtherRigidbody) as Rigidbody2D;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00005CFC File Offset: 0x00003EFC
		public bool enabled
		{
			get
			{
				return this.m_Enabled == 1;
			}
		}

		// Token: 0x04000051 RID: 81
		[NativeName("point")]
		private Vector2 m_Point;

		// Token: 0x04000052 RID: 82
		[NativeName("normal")]
		private Vector2 m_Normal;

		// Token: 0x04000053 RID: 83
		[NativeName("relativeVelocity")]
		private Vector2 m_RelativeVelocity;

		// Token: 0x04000054 RID: 84
		[NativeName("separation")]
		private float m_Separation;

		// Token: 0x04000055 RID: 85
		[NativeName("normalImpulse")]
		private float m_NormalImpulse;

		// Token: 0x04000056 RID: 86
		[NativeName("tangentImpulse")]
		private float m_TangentImpulse;

		// Token: 0x04000057 RID: 87
		[NativeName("collider")]
		private int m_Collider;

		// Token: 0x04000058 RID: 88
		[NativeName("otherCollider")]
		private int m_OtherCollider;

		// Token: 0x04000059 RID: 89
		[NativeName("rigidbody")]
		private int m_Rigidbody;

		// Token: 0x0400005A RID: 90
		[NativeName("otherRigidbody")]
		private int m_OtherRigidbody;

		// Token: 0x0400005B RID: 91
		[NativeName("enabled")]
		private int m_Enabled;
	}
}
