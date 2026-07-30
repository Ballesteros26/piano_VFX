using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x02000039 RID: 57
	[RequireComponent(typeof(CanvasRenderer))]
	[AddComponentMenu("UI/Text", 10)]
	public class Text : MaskableGraphic, ILayoutElement
	{
		// Token: 0x06000416 RID: 1046 RVA: 0x00013FC8 File Offset: 0x000121C8
		protected Text()
		{
			base.useLegacyMeshGeneration = false;
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x00013FFC File Offset: 0x000121FC
		public TextGenerator cachedTextGenerator
		{
			get
			{
				TextGenerator textGenerator;
				if ((textGenerator = this.m_TextCache) == null)
				{
					textGenerator = (this.m_TextCache = ((this.m_Text.Length != 0) ? new TextGenerator(this.m_Text.Length) : new TextGenerator()));
				}
				return textGenerator;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x00014040 File Offset: 0x00012240
		public TextGenerator cachedTextGeneratorForLayout
		{
			get
			{
				TextGenerator textGenerator;
				if ((textGenerator = this.m_TextCacheForLayout) == null)
				{
					textGenerator = (this.m_TextCacheForLayout = new TextGenerator());
				}
				return textGenerator;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x00014068 File Offset: 0x00012268
		public override Texture mainTexture
		{
			get
			{
				if (this.font != null && this.font.material != null && this.font.material.mainTexture != null)
				{
					return this.font.material.mainTexture;
				}
				if (this.m_Material != null)
				{
					return this.m_Material.mainTexture;
				}
				return base.mainTexture;
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x000140E0 File Offset: 0x000122E0
		public void FontTextureChanged()
		{
			if (!this)
			{
				return;
			}
			if (this.m_DisableFontTextureRebuiltCallback)
			{
				return;
			}
			this.cachedTextGenerator.Invalidate();
			if (!this.IsActive())
			{
				return;
			}
			if (CanvasUpdateRegistry.IsRebuildingGraphics() || CanvasUpdateRegistry.IsRebuildingLayout())
			{
				this.UpdateGeometry();
				return;
			}
			this.SetAllDirty();
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0001412E File Offset: 0x0001232E
		// (set) Token: 0x0600041C RID: 1052 RVA: 0x0001413B File Offset: 0x0001233B
		public Font font
		{
			get
			{
				return this.m_FontData.font;
			}
			set
			{
				if (this.m_FontData.font == value)
				{
					return;
				}
				FontUpdateTracker.UntrackText(this);
				this.m_FontData.font = value;
				FontUpdateTracker.TrackText(this);
				this.SetAllDirty();
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0001416F File Offset: 0x0001236F
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x00014178 File Offset: 0x00012378
		public virtual string text
		{
			get
			{
				return this.m_Text;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					if (this.m_Text != value)
					{
						this.m_Text = value;
						this.SetVerticesDirty();
						this.SetLayoutDirty();
					}
					return;
				}
				if (string.IsNullOrEmpty(this.m_Text))
				{
					return;
				}
				this.m_Text = "";
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x000141CE File Offset: 0x000123CE
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x000141DB File Offset: 0x000123DB
		public bool supportRichText
		{
			get
			{
				return this.m_FontData.richText;
			}
			set
			{
				if (this.m_FontData.richText == value)
				{
					return;
				}
				this.m_FontData.richText = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x00014204 File Offset: 0x00012404
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x00014211 File Offset: 0x00012411
		public bool resizeTextForBestFit
		{
			get
			{
				return this.m_FontData.bestFit;
			}
			set
			{
				if (this.m_FontData.bestFit == value)
				{
					return;
				}
				this.m_FontData.bestFit = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0001423A File Offset: 0x0001243A
		// (set) Token: 0x06000424 RID: 1060 RVA: 0x00014247 File Offset: 0x00012447
		public int resizeTextMinSize
		{
			get
			{
				return this.m_FontData.minSize;
			}
			set
			{
				if (this.m_FontData.minSize == value)
				{
					return;
				}
				this.m_FontData.minSize = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x00014270 File Offset: 0x00012470
		// (set) Token: 0x06000426 RID: 1062 RVA: 0x0001427D File Offset: 0x0001247D
		public int resizeTextMaxSize
		{
			get
			{
				return this.m_FontData.maxSize;
			}
			set
			{
				if (this.m_FontData.maxSize == value)
				{
					return;
				}
				this.m_FontData.maxSize = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x000142A6 File Offset: 0x000124A6
		// (set) Token: 0x06000428 RID: 1064 RVA: 0x000142B3 File Offset: 0x000124B3
		public TextAnchor alignment
		{
			get
			{
				return this.m_FontData.alignment;
			}
			set
			{
				if (this.m_FontData.alignment == value)
				{
					return;
				}
				this.m_FontData.alignment = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x000142DC File Offset: 0x000124DC
		// (set) Token: 0x0600042A RID: 1066 RVA: 0x000142E9 File Offset: 0x000124E9
		public bool alignByGeometry
		{
			get
			{
				return this.m_FontData.alignByGeometry;
			}
			set
			{
				if (this.m_FontData.alignByGeometry == value)
				{
					return;
				}
				this.m_FontData.alignByGeometry = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x0001430C File Offset: 0x0001250C
		// (set) Token: 0x0600042C RID: 1068 RVA: 0x00014319 File Offset: 0x00012519
		public int fontSize
		{
			get
			{
				return this.m_FontData.fontSize;
			}
			set
			{
				if (this.m_FontData.fontSize == value)
				{
					return;
				}
				this.m_FontData.fontSize = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x00014342 File Offset: 0x00012542
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x0001434F File Offset: 0x0001254F
		public HorizontalWrapMode horizontalOverflow
		{
			get
			{
				return this.m_FontData.horizontalOverflow;
			}
			set
			{
				if (this.m_FontData.horizontalOverflow == value)
				{
					return;
				}
				this.m_FontData.horizontalOverflow = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x00014378 File Offset: 0x00012578
		// (set) Token: 0x06000430 RID: 1072 RVA: 0x00014385 File Offset: 0x00012585
		public VerticalWrapMode verticalOverflow
		{
			get
			{
				return this.m_FontData.verticalOverflow;
			}
			set
			{
				if (this.m_FontData.verticalOverflow == value)
				{
					return;
				}
				this.m_FontData.verticalOverflow = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x000143AE File Offset: 0x000125AE
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x000143BB File Offset: 0x000125BB
		public float lineSpacing
		{
			get
			{
				return this.m_FontData.lineSpacing;
			}
			set
			{
				if (this.m_FontData.lineSpacing == value)
				{
					return;
				}
				this.m_FontData.lineSpacing = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x000143E4 File Offset: 0x000125E4
		// (set) Token: 0x06000434 RID: 1076 RVA: 0x000143F1 File Offset: 0x000125F1
		public FontStyle fontStyle
		{
			get
			{
				return this.m_FontData.fontStyle;
			}
			set
			{
				if (this.m_FontData.fontStyle == value)
				{
					return;
				}
				this.m_FontData.fontStyle = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x0001441C File Offset: 0x0001261C
		public float pixelsPerUnit
		{
			get
			{
				Canvas canvas = base.canvas;
				if (!canvas)
				{
					return 1f;
				}
				if (!this.font || this.font.dynamic)
				{
					return canvas.scaleFactor;
				}
				if (this.m_FontData.fontSize <= 0 || this.font.fontSize <= 0)
				{
					return 1f;
				}
				return (float)this.font.fontSize / (float)this.m_FontData.fontSize;
			}
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0001449A File Offset: 0x0001269A
		protected override void OnEnable()
		{
			base.OnEnable();
			this.cachedTextGenerator.Invalidate();
			FontUpdateTracker.TrackText(this);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x000144B3 File Offset: 0x000126B3
		protected override void OnDisable()
		{
			FontUpdateTracker.UntrackText(this);
			base.OnDisable();
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x000144C1 File Offset: 0x000126C1
		protected override void UpdateGeometry()
		{
			if (this.font != null)
			{
				base.UpdateGeometry();
			}
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x000144D7 File Offset: 0x000126D7
		internal void AssignDefaultFont()
		{
			this.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x000144EC File Offset: 0x000126EC
		public TextGenerationSettings GetGenerationSettings(Vector2 extents)
		{
			TextGenerationSettings textGenerationSettings = default(TextGenerationSettings);
			textGenerationSettings.generationExtents = extents;
			if (this.font != null && this.font.dynamic)
			{
				textGenerationSettings.fontSize = this.m_FontData.fontSize;
				textGenerationSettings.resizeTextMinSize = this.m_FontData.minSize;
				textGenerationSettings.resizeTextMaxSize = this.m_FontData.maxSize;
			}
			textGenerationSettings.textAnchor = this.m_FontData.alignment;
			textGenerationSettings.alignByGeometry = this.m_FontData.alignByGeometry;
			textGenerationSettings.scaleFactor = this.pixelsPerUnit;
			textGenerationSettings.color = this.color;
			textGenerationSettings.font = this.font;
			textGenerationSettings.pivot = base.rectTransform.pivot;
			textGenerationSettings.richText = this.m_FontData.richText;
			textGenerationSettings.lineSpacing = this.m_FontData.lineSpacing;
			textGenerationSettings.fontStyle = this.m_FontData.fontStyle;
			textGenerationSettings.resizeTextForBestFit = this.m_FontData.bestFit;
			textGenerationSettings.updateBounds = false;
			textGenerationSettings.horizontalOverflow = this.m_FontData.horizontalOverflow;
			textGenerationSettings.verticalOverflow = this.m_FontData.verticalOverflow;
			return textGenerationSettings;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0001462C File Offset: 0x0001282C
		public static Vector2 GetTextAnchorPivot(TextAnchor anchor)
		{
			switch (anchor)
			{
			case TextAnchor.UpperLeft:
				return new Vector2(0f, 1f);
			case TextAnchor.UpperCenter:
				return new Vector2(0.5f, 1f);
			case TextAnchor.UpperRight:
				return new Vector2(1f, 1f);
			case TextAnchor.MiddleLeft:
				return new Vector2(0f, 0.5f);
			case TextAnchor.MiddleCenter:
				return new Vector2(0.5f, 0.5f);
			case TextAnchor.MiddleRight:
				return new Vector2(1f, 0.5f);
			case TextAnchor.LowerLeft:
				return new Vector2(0f, 0f);
			case TextAnchor.LowerCenter:
				return new Vector2(0.5f, 0f);
			case TextAnchor.LowerRight:
				return new Vector2(1f, 0f);
			default:
				return Vector2.zero;
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00014700 File Offset: 0x00012900
		protected override void OnPopulateMesh(VertexHelper toFill)
		{
			if (this.font == null)
			{
				return;
			}
			this.m_DisableFontTextureRebuiltCallback = true;
			Vector2 size = base.rectTransform.rect.size;
			TextGenerationSettings generationSettings = this.GetGenerationSettings(size);
			this.cachedTextGenerator.PopulateWithErrors(this.text, generationSettings, base.gameObject);
			IList<UIVertex> verts = this.cachedTextGenerator.verts;
			float num = 1f / this.pixelsPerUnit;
			int count = verts.Count;
			if (count <= 0)
			{
				toFill.Clear();
				return;
			}
			Vector2 vector = new Vector2(verts[0].position.x, verts[0].position.y) * num;
			vector = base.PixelAdjustPoint(vector) - vector;
			toFill.Clear();
			if (vector != Vector2.zero)
			{
				for (int i = 0; i < count; i++)
				{
					int num2 = i & 3;
					this.m_TempVerts[num2] = verts[i];
					UIVertex[] tempVerts = this.m_TempVerts;
					int num3 = num2;
					tempVerts[num3].position = tempVerts[num3].position * num;
					UIVertex[] tempVerts2 = this.m_TempVerts;
					int num4 = num2;
					tempVerts2[num4].position.x = tempVerts2[num4].position.x + vector.x;
					UIVertex[] tempVerts3 = this.m_TempVerts;
					int num5 = num2;
					tempVerts3[num5].position.y = tempVerts3[num5].position.y + vector.y;
					if (num2 == 3)
					{
						toFill.AddUIVertexQuad(this.m_TempVerts);
					}
				}
			}
			else
			{
				for (int j = 0; j < count; j++)
				{
					int num6 = j & 3;
					this.m_TempVerts[num6] = verts[j];
					UIVertex[] tempVerts4 = this.m_TempVerts;
					int num7 = num6;
					tempVerts4[num7].position = tempVerts4[num7].position * num;
					if (num6 == 3)
					{
						toFill.AddUIVertexQuad(this.m_TempVerts);
					}
				}
			}
			this.m_DisableFontTextureRebuiltCallback = false;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00004C7A File Offset: 0x00002E7A
		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00004C7A File Offset: 0x00002E7A
		public virtual void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x00008BDA File Offset: 0x00006DDA
		public virtual float minWidth
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x000148F0 File Offset: 0x00012AF0
		public virtual float preferredWidth
		{
			get
			{
				TextGenerationSettings generationSettings = this.GetGenerationSettings(Vector2.zero);
				return this.cachedTextGeneratorForLayout.GetPreferredWidth(this.m_Text, generationSettings) / this.pixelsPerUnit;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x00008C4E File Offset: 0x00006E4E
		public virtual float flexibleWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x00008BDA File Offset: 0x00006DDA
		public virtual float minHeight
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x00014924 File Offset: 0x00012B24
		public virtual float preferredHeight
		{
			get
			{
				TextGenerationSettings generationSettings = this.GetGenerationSettings(new Vector2(base.GetPixelAdjustedRect().size.x, 0f));
				return this.cachedTextGeneratorForLayout.GetPreferredHeight(this.m_Text, generationSettings) / this.pixelsPerUnit;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x00008C4E File Offset: 0x00006E4E
		public virtual float flexibleHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x00008CC2 File Offset: 0x00006EC2
		public virtual int layoutPriority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x04000161 RID: 353
		[SerializeField]
		private FontData m_FontData = FontData.defaultFontData;

		// Token: 0x04000162 RID: 354
		[TextArea(3, 10)]
		[SerializeField]
		protected string m_Text = string.Empty;

		// Token: 0x04000163 RID: 355
		private TextGenerator m_TextCache;

		// Token: 0x04000164 RID: 356
		private TextGenerator m_TextCacheForLayout;

		// Token: 0x04000165 RID: 357
		protected static Material s_DefaultText;

		// Token: 0x04000166 RID: 358
		[NonSerialized]
		protected bool m_DisableFontTextureRebuiltCallback;

		// Token: 0x04000167 RID: 359
		private readonly UIVertex[] m_TempVerts = new UIVertex[4];
	}
}
