using System;

namespace UnityEngine
{
	// Token: 0x02000052 RID: 82
	[Obsolete("ParticleSystemVertexStreams is deprecated. Please use ParticleSystemVertexStream instead.", false)]
	[Flags]
	public enum ParticleSystemVertexStreams
	{
		// Token: 0x04000153 RID: 339
		Position = 1,
		// Token: 0x04000154 RID: 340
		Normal = 2,
		// Token: 0x04000155 RID: 341
		Tangent = 4,
		// Token: 0x04000156 RID: 342
		Color = 8,
		// Token: 0x04000157 RID: 343
		UV = 16,
		// Token: 0x04000158 RID: 344
		UV2BlendAndFrame = 32,
		// Token: 0x04000159 RID: 345
		CenterAndVertexID = 64,
		// Token: 0x0400015A RID: 346
		Size = 128,
		// Token: 0x0400015B RID: 347
		Rotation = 256,
		// Token: 0x0400015C RID: 348
		Velocity = 512,
		// Token: 0x0400015D RID: 349
		Lifetime = 1024,
		// Token: 0x0400015E RID: 350
		Custom1 = 2048,
		// Token: 0x0400015F RID: 351
		Custom2 = 4096,
		// Token: 0x04000160 RID: 352
		Random = 8192,
		// Token: 0x04000161 RID: 353
		None = 0,
		// Token: 0x04000162 RID: 354
		All = 2147483647
	}
}
