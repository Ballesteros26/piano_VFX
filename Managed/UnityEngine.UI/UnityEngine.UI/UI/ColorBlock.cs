using System;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000007 RID: 7
	[Serializable]
	public struct ColorBlock : IEquatable<ColorBlock>
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002619 File Offset: 0x00000819
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002621 File Offset: 0x00000821
		public Color normalColor
		{
			get
			{
				return this.m_NormalColor;
			}
			set
			{
				this.m_NormalColor = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000262A File Offset: 0x0000082A
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002632 File Offset: 0x00000832
		public Color highlightedColor
		{
			get
			{
				return this.m_HighlightedColor;
			}
			set
			{
				this.m_HighlightedColor = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000263B File Offset: 0x0000083B
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002643 File Offset: 0x00000843
		public Color pressedColor
		{
			get
			{
				return this.m_PressedColor;
			}
			set
			{
				this.m_PressedColor = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000264C File Offset: 0x0000084C
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002654 File Offset: 0x00000854
		public Color selectedColor
		{
			get
			{
				return this.m_SelectedColor;
			}
			set
			{
				this.m_SelectedColor = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000265D File Offset: 0x0000085D
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002665 File Offset: 0x00000865
		public Color disabledColor
		{
			get
			{
				return this.m_DisabledColor;
			}
			set
			{
				this.m_DisabledColor = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000035 RID: 53 RVA: 0x0000266E File Offset: 0x0000086E
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002676 File Offset: 0x00000876
		public float colorMultiplier
		{
			get
			{
				return this.m_ColorMultiplier;
			}
			set
			{
				this.m_ColorMultiplier = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000267F File Offset: 0x0000087F
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002687 File Offset: 0x00000887
		public float fadeDuration
		{
			get
			{
				return this.m_FadeDuration;
			}
			set
			{
				this.m_FadeDuration = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002690 File Offset: 0x00000890
		public static ColorBlock defaultColorBlock
		{
			get
			{
				return new ColorBlock
				{
					m_NormalColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue),
					m_HighlightedColor = new Color32(245, 245, 245, byte.MaxValue),
					m_PressedColor = new Color32(200, 200, 200, byte.MaxValue),
					m_SelectedColor = new Color32(245, 245, 245, byte.MaxValue),
					m_DisabledColor = new Color32(200, 200, 200, 128),
					colorMultiplier = 1f,
					fadeDuration = 0.1f
				};
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002777 File Offset: 0x00000977
		public override bool Equals(object obj)
		{
			return obj is ColorBlock && this.Equals((ColorBlock)obj);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002790 File Offset: 0x00000990
		public bool Equals(ColorBlock other)
		{
			return this.normalColor == other.normalColor && this.highlightedColor == other.highlightedColor && this.pressedColor == other.pressedColor && this.selectedColor == other.selectedColor && this.disabledColor == other.disabledColor && this.colorMultiplier == other.colorMultiplier && this.fadeDuration == other.fadeDuration;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002821 File Offset: 0x00000A21
		public static bool operator ==(ColorBlock point1, ColorBlock point2)
		{
			return point1.Equals(point2);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000282B File Offset: 0x00000A2B
		public static bool operator !=(ColorBlock point1, ColorBlock point2)
		{
			return !point1.Equals(point2);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002838 File Offset: 0x00000A38
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0400001B RID: 27
		[FormerlySerializedAs("normalColor")]
		[SerializeField]
		private Color m_NormalColor;

		// Token: 0x0400001C RID: 28
		[FormerlySerializedAs("highlightedColor")]
		[SerializeField]
		private Color m_HighlightedColor;

		// Token: 0x0400001D RID: 29
		[FormerlySerializedAs("pressedColor")]
		[SerializeField]
		private Color m_PressedColor;

		// Token: 0x0400001E RID: 30
		[FormerlySerializedAs("m_HighlightedColor")]
		[SerializeField]
		private Color m_SelectedColor;

		// Token: 0x0400001F RID: 31
		[FormerlySerializedAs("disabledColor")]
		[SerializeField]
		private Color m_DisabledColor;

		// Token: 0x04000020 RID: 32
		[Range(1f, 5f)]
		[SerializeField]
		private float m_ColorMultiplier;

		// Token: 0x04000021 RID: 33
		[FormerlySerializedAs("fadeDuration")]
		[SerializeField]
		private float m_FadeDuration;
	}
}
