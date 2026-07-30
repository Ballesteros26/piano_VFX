using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000148 RID: 328
	[UsedByNativeCode]
	[Serializable]
	public struct BoneWeight : IEquatable<BoneWeight>
	{
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x00012058 File Offset: 0x00010258
		// (set) Token: 0x06000D7D RID: 3453 RVA: 0x00012070 File Offset: 0x00010270
		public float weight0
		{
			get
			{
				return this.m_Weight0;
			}
			set
			{
				this.m_Weight0 = value;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x0001207C File Offset: 0x0001027C
		// (set) Token: 0x06000D7F RID: 3455 RVA: 0x00012094 File Offset: 0x00010294
		public float weight1
		{
			get
			{
				return this.m_Weight1;
			}
			set
			{
				this.m_Weight1 = value;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000D80 RID: 3456 RVA: 0x000120A0 File Offset: 0x000102A0
		// (set) Token: 0x06000D81 RID: 3457 RVA: 0x000120B8 File Offset: 0x000102B8
		public float weight2
		{
			get
			{
				return this.m_Weight2;
			}
			set
			{
				this.m_Weight2 = value;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000D82 RID: 3458 RVA: 0x000120C4 File Offset: 0x000102C4
		// (set) Token: 0x06000D83 RID: 3459 RVA: 0x000120DC File Offset: 0x000102DC
		public float weight3
		{
			get
			{
				return this.m_Weight3;
			}
			set
			{
				this.m_Weight3 = value;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x000120E8 File Offset: 0x000102E8
		// (set) Token: 0x06000D85 RID: 3461 RVA: 0x00012100 File Offset: 0x00010300
		public int boneIndex0
		{
			get
			{
				return this.m_BoneIndex0;
			}
			set
			{
				this.m_BoneIndex0 = value;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x0001210C File Offset: 0x0001030C
		// (set) Token: 0x06000D87 RID: 3463 RVA: 0x00012124 File Offset: 0x00010324
		public int boneIndex1
		{
			get
			{
				return this.m_BoneIndex1;
			}
			set
			{
				this.m_BoneIndex1 = value;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x00012130 File Offset: 0x00010330
		// (set) Token: 0x06000D89 RID: 3465 RVA: 0x00012148 File Offset: 0x00010348
		public int boneIndex2
		{
			get
			{
				return this.m_BoneIndex2;
			}
			set
			{
				this.m_BoneIndex2 = value;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x00012154 File Offset: 0x00010354
		// (set) Token: 0x06000D8B RID: 3467 RVA: 0x0001216C File Offset: 0x0001036C
		public int boneIndex3
		{
			get
			{
				return this.m_BoneIndex3;
			}
			set
			{
				this.m_BoneIndex3 = value;
			}
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x00012178 File Offset: 0x00010378
		public override int GetHashCode()
		{
			return this.boneIndex0.GetHashCode() ^ (this.boneIndex1.GetHashCode() << 2) ^ (this.boneIndex2.GetHashCode() >> 2) ^ (this.boneIndex3.GetHashCode() >> 1) ^ (this.weight0.GetHashCode() << 5) ^ (this.weight1.GetHashCode() << 4) ^ (this.weight2.GetHashCode() >> 4) ^ (this.weight3.GetHashCode() >> 3);
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x00012210 File Offset: 0x00010410
		public override bool Equals(object other)
		{
			return other is BoneWeight && this.Equals((BoneWeight)other);
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x0001223C File Offset: 0x0001043C
		public bool Equals(BoneWeight other)
		{
			return this.boneIndex0.Equals(other.boneIndex0) && this.boneIndex1.Equals(other.boneIndex1) && this.boneIndex2.Equals(other.boneIndex2) && this.boneIndex3.Equals(other.boneIndex3) && new Vector4(this.weight0, this.weight1, this.weight2, this.weight3).Equals(new Vector4(other.weight0, other.weight1, other.weight2, other.weight3));
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x000122F8 File Offset: 0x000104F8
		public static bool operator ==(BoneWeight lhs, BoneWeight rhs)
		{
			return lhs.boneIndex0 == rhs.boneIndex0 && lhs.boneIndex1 == rhs.boneIndex1 && lhs.boneIndex2 == rhs.boneIndex2 && lhs.boneIndex3 == rhs.boneIndex3 && new Vector4(lhs.weight0, lhs.weight1, lhs.weight2, lhs.weight3) == new Vector4(rhs.weight0, rhs.weight1, rhs.weight2, rhs.weight3);
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x00012394 File Offset: 0x00010594
		public static bool operator !=(BoneWeight lhs, BoneWeight rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0400041E RID: 1054
		[SerializeField]
		private float m_Weight0;

		// Token: 0x0400041F RID: 1055
		[SerializeField]
		private float m_Weight1;

		// Token: 0x04000420 RID: 1056
		[SerializeField]
		private float m_Weight2;

		// Token: 0x04000421 RID: 1057
		[SerializeField]
		private float m_Weight3;

		// Token: 0x04000422 RID: 1058
		[SerializeField]
		private int m_BoneIndex0;

		// Token: 0x04000423 RID: 1059
		[SerializeField]
		private int m_BoneIndex1;

		// Token: 0x04000424 RID: 1060
		[SerializeField]
		private int m_BoneIndex2;

		// Token: 0x04000425 RID: 1061
		[SerializeField]
		private int m_BoneIndex3;
	}
}
