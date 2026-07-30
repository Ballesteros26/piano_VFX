using System;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000037 RID: 55
	[Serializable]
	public struct SpriteState : IEquatable<SpriteState>
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x00013B0A File Offset: 0x00011D0A
		// (set) Token: 0x06000408 RID: 1032 RVA: 0x00013B12 File Offset: 0x00011D12
		public Sprite highlightedSprite
		{
			get
			{
				return this.m_HighlightedSprite;
			}
			set
			{
				this.m_HighlightedSprite = value;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x00013B1B File Offset: 0x00011D1B
		// (set) Token: 0x0600040A RID: 1034 RVA: 0x00013B23 File Offset: 0x00011D23
		public Sprite pressedSprite
		{
			get
			{
				return this.m_PressedSprite;
			}
			set
			{
				this.m_PressedSprite = value;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00013B2C File Offset: 0x00011D2C
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x00013B34 File Offset: 0x00011D34
		public Sprite selectedSprite
		{
			get
			{
				return this.m_SelectedSprite;
			}
			set
			{
				this.m_SelectedSprite = value;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x00013B3D File Offset: 0x00011D3D
		// (set) Token: 0x0600040E RID: 1038 RVA: 0x00013B45 File Offset: 0x00011D45
		public Sprite disabledSprite
		{
			get
			{
				return this.m_DisabledSprite;
			}
			set
			{
				this.m_DisabledSprite = value;
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00013B50 File Offset: 0x00011D50
		public bool Equals(SpriteState other)
		{
			return this.highlightedSprite == other.highlightedSprite && this.pressedSprite == other.pressedSprite && this.selectedSprite == other.selectedSprite && this.disabledSprite == other.disabledSprite;
		}

		// Token: 0x0400015C RID: 348
		[SerializeField]
		private Sprite m_HighlightedSprite;

		// Token: 0x0400015D RID: 349
		[SerializeField]
		private Sprite m_PressedSprite;

		// Token: 0x0400015E RID: 350
		[FormerlySerializedAs("m_HighlightedSprite")]
		[SerializeField]
		private Sprite m_SelectedSprite;

		// Token: 0x0400015F RID: 351
		[SerializeField]
		private Sprite m_DisabledSprite;
	}
}
