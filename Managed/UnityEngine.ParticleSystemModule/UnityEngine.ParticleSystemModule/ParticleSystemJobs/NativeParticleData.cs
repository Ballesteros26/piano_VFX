using System;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x0200005F RID: 95
	internal struct NativeParticleData
	{
		// Token: 0x0400017F RID: 383
		internal int count;

		// Token: 0x04000180 RID: 384
		internal NativeParticleData.Array3 positions;

		// Token: 0x04000181 RID: 385
		internal NativeParticleData.Array3 velocities;

		// Token: 0x04000182 RID: 386
		internal NativeParticleData.Array3 rotations;

		// Token: 0x04000183 RID: 387
		internal NativeParticleData.Array3 rotationalSpeeds;

		// Token: 0x04000184 RID: 388
		internal NativeParticleData.Array3 sizes;

		// Token: 0x04000185 RID: 389
		internal unsafe void* startColors;

		// Token: 0x04000186 RID: 390
		internal unsafe void* aliveTimePercent;

		// Token: 0x04000187 RID: 391
		internal unsafe void* inverseStartLifetimes;

		// Token: 0x04000188 RID: 392
		internal unsafe void* randomSeeds;

		// Token: 0x04000189 RID: 393
		internal NativeParticleData.Array4 customData1;

		// Token: 0x0400018A RID: 394
		internal NativeParticleData.Array4 customData2;

		// Token: 0x02000060 RID: 96
		internal struct Array3
		{
			// Token: 0x0400018B RID: 395
			internal unsafe float* x;

			// Token: 0x0400018C RID: 396
			internal unsafe float* y;

			// Token: 0x0400018D RID: 397
			internal unsafe float* z;
		}

		// Token: 0x02000061 RID: 97
		internal struct Array4
		{
			// Token: 0x0400018E RID: 398
			internal unsafe float* x;

			// Token: 0x0400018F RID: 399
			internal unsafe float* y;

			// Token: 0x04000190 RID: 400
			internal unsafe float* z;

			// Token: 0x04000191 RID: 401
			internal unsafe float* w;
		}
	}
}
