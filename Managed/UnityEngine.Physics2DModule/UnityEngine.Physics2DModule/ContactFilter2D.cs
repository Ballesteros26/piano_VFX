using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000012 RID: 18
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeHeader("Modules/Physics2D/Public/Collider2D.h")]
	[NativeClass("ContactFilter", "struct ContactFilter;")]
	[Serializable]
	public struct ContactFilter2D
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x000056BC File Offset: 0x000038BC
		public ContactFilter2D NoFilter()
		{
			this.useTriggers = true;
			this.useLayerMask = false;
			this.layerMask = -1;
			this.useDepth = false;
			this.useOutsideDepth = false;
			this.minDepth = float.NegativeInfinity;
			this.maxDepth = float.PositiveInfinity;
			this.useNormalAngle = false;
			this.useOutsideNormalAngle = false;
			this.minNormalAngle = 0f;
			this.maxNormalAngle = 359.9999f;
			return this;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00005736 File Offset: 0x00003936
		private void CheckConsistency()
		{
			ContactFilter2D.CheckConsistency_Injected(ref this);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000573E File Offset: 0x0000393E
		public void ClearLayerMask()
		{
			this.useLayerMask = false;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00005748 File Offset: 0x00003948
		public void SetLayerMask(LayerMask layerMask)
		{
			this.layerMask = layerMask;
			this.useLayerMask = true;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00005759 File Offset: 0x00003959
		public void ClearDepth()
		{
			this.useDepth = false;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00005763 File Offset: 0x00003963
		public void SetDepth(float minDepth, float maxDepth)
		{
			this.minDepth = minDepth;
			this.maxDepth = maxDepth;
			this.useDepth = true;
			this.CheckConsistency();
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00005782 File Offset: 0x00003982
		public void ClearNormalAngle()
		{
			this.useNormalAngle = false;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000578C File Offset: 0x0000398C
		public void SetNormalAngle(float minNormalAngle, float maxNormalAngle)
		{
			this.minNormalAngle = minNormalAngle;
			this.maxNormalAngle = maxNormalAngle;
			this.useNormalAngle = true;
			this.CheckConsistency();
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060001ED RID: 493 RVA: 0x000057AC File Offset: 0x000039AC
		public bool isFiltering
		{
			get
			{
				return !this.useTriggers || this.useLayerMask || this.useDepth || this.useNormalAngle;
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000057E0 File Offset: 0x000039E0
		public bool IsFilteringTrigger([Writable] Collider2D collider)
		{
			return !this.useTriggers && collider.isTrigger;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00005804 File Offset: 0x00003A04
		public bool IsFilteringLayerMask(GameObject obj)
		{
			return this.useLayerMask && (this.layerMask & (1 << obj.layer)) == 0;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000583C File Offset: 0x00003A3C
		public bool IsFilteringDepth(GameObject obj)
		{
			bool flag = !this.useDepth;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this.minDepth > this.maxDepth;
				if (flag3)
				{
					float num = this.minDepth;
					this.minDepth = this.maxDepth;
					this.maxDepth = num;
				}
				float z = obj.transform.position.z;
				bool flag4 = z < this.minDepth || z > this.maxDepth;
				bool flag5 = this.useOutsideDepth;
				if (flag5)
				{
					flag2 = !flag4;
				}
				else
				{
					flag2 = flag4;
				}
			}
			return flag2;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000058CC File Offset: 0x00003ACC
		public bool IsFilteringNormalAngle(Vector2 normal)
		{
			return ContactFilter2D.IsFilteringNormalAngle_Injected(ref this, ref normal);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000058D8 File Offset: 0x00003AD8
		public bool IsFilteringNormalAngle(float angle)
		{
			return this.IsFilteringNormalAngleUsingAngle(angle);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x000058F1 File Offset: 0x00003AF1
		private bool IsFilteringNormalAngleUsingAngle(float angle)
		{
			return ContactFilter2D.IsFilteringNormalAngleUsingAngle_Injected(ref this, angle);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000058FC File Offset: 0x00003AFC
		internal static ContactFilter2D CreateLegacyFilter(int layerMask, float minDepth, float maxDepth)
		{
			ContactFilter2D contactFilter2D = default(ContactFilter2D);
			contactFilter2D.useTriggers = Physics2D.queriesHitTriggers;
			contactFilter2D.SetLayerMask(layerMask);
			contactFilter2D.SetDepth(minDepth, maxDepth);
			return contactFilter2D;
		}

		// Token: 0x060001F5 RID: 501
		[MethodImpl(4096)]
		private static extern void CheckConsistency_Injected(ref ContactFilter2D _unity_self);

		// Token: 0x060001F6 RID: 502
		[MethodImpl(4096)]
		private static extern bool IsFilteringNormalAngle_Injected(ref ContactFilter2D _unity_self, ref Vector2 normal);

		// Token: 0x060001F7 RID: 503
		[MethodImpl(4096)]
		private static extern bool IsFilteringNormalAngleUsingAngle_Injected(ref ContactFilter2D _unity_self, float angle);

		// Token: 0x0400003C RID: 60
		[NativeName("m_UseTriggers")]
		public bool useTriggers;

		// Token: 0x0400003D RID: 61
		[NativeName("m_UseLayerMask")]
		public bool useLayerMask;

		// Token: 0x0400003E RID: 62
		[NativeName("m_UseDepth")]
		public bool useDepth;

		// Token: 0x0400003F RID: 63
		[NativeName("m_UseOutsideDepth")]
		public bool useOutsideDepth;

		// Token: 0x04000040 RID: 64
		[NativeName("m_UseNormalAngle")]
		public bool useNormalAngle;

		// Token: 0x04000041 RID: 65
		[NativeName("m_UseOutsideNormalAngle")]
		public bool useOutsideNormalAngle;

		// Token: 0x04000042 RID: 66
		[NativeName("m_LayerMask")]
		public LayerMask layerMask;

		// Token: 0x04000043 RID: 67
		[NativeName("m_MinDepth")]
		public float minDepth;

		// Token: 0x04000044 RID: 68
		[NativeName("m_MaxDepth")]
		public float maxDepth;

		// Token: 0x04000045 RID: 69
		[NativeName("m_MinNormalAngle")]
		public float minNormalAngle;

		// Token: 0x04000046 RID: 70
		[NativeName("m_MaxNormalAngle")]
		public float maxNormalAngle;

		// Token: 0x04000047 RID: 71
		public const float NormalAngleUpperLimit = 359.9999f;
	}
}
