using System;
using Unity.Collections;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x0200005C RID: 92
	public struct ParticleSystemNativeArray3
	{
		// Token: 0x170001E1 RID: 481
		public Vector3 this[int index]
		{
			get
			{
				return new Vector3(this.x[index], this.y[index], this.z[index]);
			}
			set
			{
				this.x[index] = value.x;
				this.y[index] = value.y;
				this.z[index] = value.z;
			}
		}

		// Token: 0x0400016C RID: 364
		public NativeArray<float> x;

		// Token: 0x0400016D RID: 365
		public NativeArray<float> y;

		// Token: 0x0400016E RID: 366
		public NativeArray<float> z;
	}
}
