using System;
using Unity.Collections;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x0200005D RID: 93
	public struct ParticleSystemNativeArray4
	{
		// Token: 0x170001E2 RID: 482
		public Vector4 this[int index]
		{
			get
			{
				return new Vector4(this.x[index], this.y[index], this.z[index], this.w[index]);
			}
			set
			{
				this.x[index] = value.x;
				this.y[index] = value.y;
				this.z[index] = value.z;
				this.w[index] = value.w;
			}
		}

		// Token: 0x0400016F RID: 367
		public NativeArray<float> x;

		// Token: 0x04000170 RID: 368
		public NativeArray<float> y;

		// Token: 0x04000171 RID: 369
		public NativeArray<float> z;

		// Token: 0x04000172 RID: 370
		public NativeArray<float> w;
	}
}
