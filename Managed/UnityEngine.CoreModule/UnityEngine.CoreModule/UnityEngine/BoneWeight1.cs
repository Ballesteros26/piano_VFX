using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000149 RID: 329
	[UsedByNativeCode]
	[Serializable]
	public struct BoneWeight1 : IEquatable<BoneWeight1>
	{
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x000123B0 File Offset: 0x000105B0
		// (set) Token: 0x06000D92 RID: 3474 RVA: 0x000123C8 File Offset: 0x000105C8
		public float weight
		{
			get
			{
				return this.m_Weight;
			}
			set
			{
				this.m_Weight = value;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x000123D4 File Offset: 0x000105D4
		// (set) Token: 0x06000D94 RID: 3476 RVA: 0x000123EC File Offset: 0x000105EC
		public int boneIndex
		{
			get
			{
				return this.m_BoneIndex;
			}
			set
			{
				this.m_BoneIndex = value;
			}
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x000123F8 File Offset: 0x000105F8
		public override bool Equals(object other)
		{
			return other is BoneWeight1 && this.Equals((BoneWeight1)other);
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x00012424 File Offset: 0x00010624
		public bool Equals(BoneWeight1 other)
		{
			return this.boneIndex.Equals(other.boneIndex) && this.weight.Equals(other.weight);
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00012468 File Offset: 0x00010668
		public override int GetHashCode()
		{
			return this.boneIndex.GetHashCode() ^ this.weight.GetHashCode();
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x00012498 File Offset: 0x00010698
		public static bool operator ==(BoneWeight1 lhs, BoneWeight1 rhs)
		{
			return lhs.boneIndex == rhs.boneIndex && lhs.weight == rhs.weight;
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x000124D0 File Offset: 0x000106D0
		public static bool operator !=(BoneWeight1 lhs, BoneWeight1 rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x04000426 RID: 1062
		[SerializeField]
		private float m_Weight;

		// Token: 0x04000427 RID: 1063
		[SerializeField]
		private int m_BoneIndex;
	}
}
