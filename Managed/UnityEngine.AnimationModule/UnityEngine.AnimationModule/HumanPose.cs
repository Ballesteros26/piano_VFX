using System;

namespace UnityEngine
{
	// Token: 0x02000035 RID: 53
	public struct HumanPose
	{
		// Token: 0x06000250 RID: 592 RVA: 0x00003E6C File Offset: 0x0000206C
		internal void Init()
		{
			bool flag = this.muscles != null;
			if (flag)
			{
				bool flag2 = this.muscles.Length != HumanTrait.MuscleCount;
				if (flag2)
				{
					throw new InvalidOperationException("Bad array size for HumanPose.muscles. Size must equal HumanTrait.MuscleCount");
				}
			}
			bool flag3 = this.muscles == null;
			if (flag3)
			{
				this.muscles = new float[HumanTrait.MuscleCount];
				bool flag4 = this.bodyRotation.x == 0f && this.bodyRotation.y == 0f && this.bodyRotation.z == 0f && this.bodyRotation.w == 0f;
				if (flag4)
				{
					this.bodyRotation.w = 1f;
				}
			}
		}

		// Token: 0x04000132 RID: 306
		public Vector3 bodyPosition;

		// Token: 0x04000133 RID: 307
		public Quaternion bodyRotation;

		// Token: 0x04000134 RID: 308
		public float[] muscles;
	}
}
