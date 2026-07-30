using System;

namespace UnityEngine
{
	// Token: 0x02000034 RID: 52
	public enum ParticleSystemShapeType
	{
		// Token: 0x0400009A RID: 154
		Sphere,
		// Token: 0x0400009B RID: 155
		[Obsolete("SphereShell is deprecated and does nothing. Please use ShapeModule.radiusThickness instead, to control edge emission.", false)]
		SphereShell,
		// Token: 0x0400009C RID: 156
		Hemisphere,
		// Token: 0x0400009D RID: 157
		[Obsolete("HemisphereShell is deprecated and does nothing. Please use ShapeModule.radiusThickness instead, to control edge emission.", false)]
		HemisphereShell,
		// Token: 0x0400009E RID: 158
		Cone,
		// Token: 0x0400009F RID: 159
		Box,
		// Token: 0x040000A0 RID: 160
		Mesh,
		// Token: 0x040000A1 RID: 161
		[Obsolete("ConeShell is deprecated and does nothing. Please use ShapeModule.radiusThickness instead, to control edge emission.", false)]
		ConeShell,
		// Token: 0x040000A2 RID: 162
		ConeVolume,
		// Token: 0x040000A3 RID: 163
		[Obsolete("ConeVolumeShell is deprecated and does nothing. Please use ShapeModule.radiusThickness instead, to control edge emission.", false)]
		ConeVolumeShell,
		// Token: 0x040000A4 RID: 164
		Circle,
		// Token: 0x040000A5 RID: 165
		[Obsolete("CircleEdge is deprecated and does nothing. Please use ShapeModule.radiusThickness instead, to control edge emission.", false)]
		CircleEdge,
		// Token: 0x040000A6 RID: 166
		SingleSidedEdge,
		// Token: 0x040000A7 RID: 167
		MeshRenderer,
		// Token: 0x040000A8 RID: 168
		SkinnedMeshRenderer,
		// Token: 0x040000A9 RID: 169
		BoxShell,
		// Token: 0x040000AA RID: 170
		BoxEdge,
		// Token: 0x040000AB RID: 171
		Donut,
		// Token: 0x040000AC RID: 172
		Rectangle,
		// Token: 0x040000AD RID: 173
		Sprite,
		// Token: 0x040000AE RID: 174
		SpriteRenderer
	}
}
