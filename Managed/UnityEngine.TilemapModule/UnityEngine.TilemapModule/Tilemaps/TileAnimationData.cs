using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Tilemaps
{
	// Token: 0x02000013 RID: 19
	[NativeType(Header = "Modules/Tilemap/TilemapScripting.h")]
	[RequiredByNativeCode]
	public struct TileAnimationData
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00002AE0 File Offset: 0x00000CE0
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public Sprite[] animatedSprites
		{
			get
			{
				return this.m_AnimatedSprites;
			}
			set
			{
				this.m_AnimatedSprites = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00002B04 File Offset: 0x00000D04
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00002B1C File Offset: 0x00000D1C
		public float animationSpeed
		{
			get
			{
				return this.m_AnimationSpeed;
			}
			set
			{
				this.m_AnimationSpeed = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00002B28 File Offset: 0x00000D28
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00002B40 File Offset: 0x00000D40
		public float animationStartTime
		{
			get
			{
				return this.m_AnimationStartTime;
			}
			set
			{
				this.m_AnimationStartTime = value;
			}
		}

		// Token: 0x0400003E RID: 62
		private Sprite[] m_AnimatedSprites;

		// Token: 0x0400003F RID: 63
		private float m_AnimationSpeed;

		// Token: 0x04000040 RID: 64
		private float m_AnimationStartTime;
	}
}
