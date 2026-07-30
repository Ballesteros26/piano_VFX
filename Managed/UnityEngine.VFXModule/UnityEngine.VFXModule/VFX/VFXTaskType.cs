using System;

namespace UnityEngine.VFX
{
	// Token: 0x02000006 RID: 6
	internal enum VFXTaskType
	{
		// Token: 0x040000A0 RID: 160
		None,
		// Token: 0x040000A1 RID: 161
		Spawner = 268435456,
		// Token: 0x040000A2 RID: 162
		Initialize = 536870912,
		// Token: 0x040000A3 RID: 163
		Update = 805306368,
		// Token: 0x040000A4 RID: 164
		Output = 1073741824,
		// Token: 0x040000A5 RID: 165
		CameraSort = 805306369,
		// Token: 0x040000A6 RID: 166
		ParticlePointOutput = 1073741824,
		// Token: 0x040000A7 RID: 167
		ParticleLineOutput,
		// Token: 0x040000A8 RID: 168
		ParticleQuadOutput,
		// Token: 0x040000A9 RID: 169
		ParticleHexahedronOutput,
		// Token: 0x040000AA RID: 170
		ParticleMeshOutput,
		// Token: 0x040000AB RID: 171
		ParticleTriangleOutput,
		// Token: 0x040000AC RID: 172
		ParticleOctagonOutput,
		// Token: 0x040000AD RID: 173
		ConstantRateSpawner = 268435456,
		// Token: 0x040000AE RID: 174
		BurstSpawner,
		// Token: 0x040000AF RID: 175
		PeriodicBurstSpawner,
		// Token: 0x040000B0 RID: 176
		VariableRateSpawner,
		// Token: 0x040000B1 RID: 177
		CustomCallbackSpawner,
		// Token: 0x040000B2 RID: 178
		SetAttributeSpawner,
		// Token: 0x040000B3 RID: 179
		EvaluateExpressionsSpawner
	}
}
