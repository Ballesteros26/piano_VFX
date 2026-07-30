using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000F RID: 15
	[RequiredByNativeCode]
	[StructLayout(0)]
	public class ControllerColliderHit
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000023AC File Offset: 0x000005AC
		public CharacterController controller
		{
			get
			{
				return this.m_Controller;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000023C4 File Offset: 0x000005C4
		public Collider collider
		{
			get
			{
				return this.m_Collider;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000023DC File Offset: 0x000005DC
		public Rigidbody rigidbody
		{
			get
			{
				return this.m_Collider.attachedRigidbody;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000023FC File Offset: 0x000005FC
		public GameObject gameObject
		{
			get
			{
				return this.m_Collider.gameObject;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000241C File Offset: 0x0000061C
		public Transform transform
		{
			get
			{
				return this.m_Collider.transform;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000038 RID: 56 RVA: 0x0000243C File Offset: 0x0000063C
		public Vector3 point
		{
			get
			{
				return this.m_Point;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002454 File Offset: 0x00000654
		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600003A RID: 58 RVA: 0x0000246C File Offset: 0x0000066C
		public Vector3 moveDirection
		{
			get
			{
				return this.m_MoveDirection;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002484 File Offset: 0x00000684
		public float moveLength
		{
			get
			{
				return this.m_MoveLength;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0000249C File Offset: 0x0000069C
		// (set) Token: 0x0600003D RID: 61 RVA: 0x000024B7 File Offset: 0x000006B7
		private bool push
		{
			get
			{
				return this.m_Push != 0;
			}
			set
			{
				this.m_Push = (value ? 1 : 0);
			}
		}

		// Token: 0x0400003F RID: 63
		internal CharacterController m_Controller;

		// Token: 0x04000040 RID: 64
		internal Collider m_Collider;

		// Token: 0x04000041 RID: 65
		internal Vector3 m_Point;

		// Token: 0x04000042 RID: 66
		internal Vector3 m_Normal;

		// Token: 0x04000043 RID: 67
		internal Vector3 m_MoveDirection;

		// Token: 0x04000044 RID: 68
		internal float m_MoveLength;

		// Token: 0x04000045 RID: 69
		internal int m_Push;
	}
}
