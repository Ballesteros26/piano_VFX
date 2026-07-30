using System;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x0200000F RID: 15
	[Serializable]
	public class FontData : ISerializationCallbackReceiver
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00004F88 File Offset: 0x00003188
		public static FontData defaultFontData
		{
			get
			{
				return new FontData
				{
					m_FontSize = 14,
					m_LineSpacing = 1f,
					m_FontStyle = FontStyle.Normal,
					m_BestFit = false,
					m_MinSize = 10,
					m_MaxSize = 40,
					m_Alignment = TextAnchor.UpperLeft,
					m_HorizontalOverflow = HorizontalWrapMode.Wrap,
					m_VerticalOverflow = VerticalWrapMode.Truncate,
					m_RichText = true,
					m_AlignByGeometry = false
				};
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00004FEE File Offset: 0x000031EE
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00004FF6 File Offset: 0x000031F6
		public Font font
		{
			get
			{
				return this.m_Font;
			}
			set
			{
				this.m_Font = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00004FFF File Offset: 0x000031FF
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00005007 File Offset: 0x00003207
		public int fontSize
		{
			get
			{
				return this.m_FontSize;
			}
			set
			{
				this.m_FontSize = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00005010 File Offset: 0x00003210
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00005018 File Offset: 0x00003218
		public FontStyle fontStyle
		{
			get
			{
				return this.m_FontStyle;
			}
			set
			{
				this.m_FontStyle = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00005021 File Offset: 0x00003221
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00005029 File Offset: 0x00003229
		public bool bestFit
		{
			get
			{
				return this.m_BestFit;
			}
			set
			{
				this.m_BestFit = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00005032 File Offset: 0x00003232
		// (set) Token: 0x0600009D RID: 157 RVA: 0x0000503A File Offset: 0x0000323A
		public int minSize
		{
			get
			{
				return this.m_MinSize;
			}
			set
			{
				this.m_MinSize = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00005043 File Offset: 0x00003243
		// (set) Token: 0x0600009F RID: 159 RVA: 0x0000504B File Offset: 0x0000324B
		public int maxSize
		{
			get
			{
				return this.m_MaxSize;
			}
			set
			{
				this.m_MaxSize = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00005054 File Offset: 0x00003254
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x0000505C File Offset: 0x0000325C
		public TextAnchor alignment
		{
			get
			{
				return this.m_Alignment;
			}
			set
			{
				this.m_Alignment = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00005065 File Offset: 0x00003265
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x0000506D File Offset: 0x0000326D
		public bool alignByGeometry
		{
			get
			{
				return this.m_AlignByGeometry;
			}
			set
			{
				this.m_AlignByGeometry = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00005076 File Offset: 0x00003276
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x0000507E File Offset: 0x0000327E
		public bool richText
		{
			get
			{
				return this.m_RichText;
			}
			set
			{
				this.m_RichText = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00005087 File Offset: 0x00003287
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x0000508F File Offset: 0x0000328F
		public HorizontalWrapMode horizontalOverflow
		{
			get
			{
				return this.m_HorizontalOverflow;
			}
			set
			{
				this.m_HorizontalOverflow = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00005098 File Offset: 0x00003298
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x000050A0 File Offset: 0x000032A0
		public VerticalWrapMode verticalOverflow
		{
			get
			{
				return this.m_VerticalOverflow;
			}
			set
			{
				this.m_VerticalOverflow = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000050A9 File Offset: 0x000032A9
		// (set) Token: 0x060000AB RID: 171 RVA: 0x000050B1 File Offset: 0x000032B1
		public float lineSpacing
		{
			get
			{
				return this.m_LineSpacing;
			}
			set
			{
				this.m_LineSpacing = value;
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004C7A File Offset: 0x00002E7A
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000050BC File Offset: 0x000032BC
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			this.m_FontSize = Mathf.Clamp(this.m_FontSize, 0, 300);
			this.m_MinSize = Mathf.Clamp(this.m_MinSize, 0, this.m_FontSize);
			this.m_MaxSize = Mathf.Clamp(this.m_MaxSize, this.m_FontSize, 300);
		}

		// Token: 0x0400003F RID: 63
		[SerializeField]
		[FormerlySerializedAs("font")]
		private Font m_Font;

		// Token: 0x04000040 RID: 64
		[SerializeField]
		[FormerlySerializedAs("fontSize")]
		private int m_FontSize;

		// Token: 0x04000041 RID: 65
		[SerializeField]
		[FormerlySerializedAs("fontStyle")]
		private FontStyle m_FontStyle;

		// Token: 0x04000042 RID: 66
		[SerializeField]
		private bool m_BestFit;

		// Token: 0x04000043 RID: 67
		[SerializeField]
		private int m_MinSize;

		// Token: 0x04000044 RID: 68
		[SerializeField]
		private int m_MaxSize;

		// Token: 0x04000045 RID: 69
		[SerializeField]
		[FormerlySerializedAs("alignment")]
		private TextAnchor m_Alignment;

		// Token: 0x04000046 RID: 70
		[SerializeField]
		private bool m_AlignByGeometry;

		// Token: 0x04000047 RID: 71
		[SerializeField]
		[FormerlySerializedAs("richText")]
		private bool m_RichText;

		// Token: 0x04000048 RID: 72
		[SerializeField]
		private HorizontalWrapMode m_HorizontalOverflow;

		// Token: 0x04000049 RID: 73
		[SerializeField]
		private VerticalWrapMode m_VerticalOverflow;

		// Token: 0x0400004A RID: 74
		[SerializeField]
		private float m_LineSpacing;
	}
}
