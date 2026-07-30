using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x0200001A RID: 26
	[RequireComponent(typeof(Canvas))]
	[ExecuteAlways]
	[AddComponentMenu("Layout/Canvas Scaler", 101)]
	[DisallowMultipleComponent]
	public class CanvasScaler : UIBehaviour
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000C84A File Offset: 0x0000AA4A
		// (set) Token: 0x060001FB RID: 507 RVA: 0x0000C852 File Offset: 0x0000AA52
		public CanvasScaler.ScaleMode uiScaleMode
		{
			get
			{
				return this.m_UiScaleMode;
			}
			set
			{
				this.m_UiScaleMode = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000C85B File Offset: 0x0000AA5B
		// (set) Token: 0x060001FD RID: 509 RVA: 0x0000C863 File Offset: 0x0000AA63
		public float referencePixelsPerUnit
		{
			get
			{
				return this.m_ReferencePixelsPerUnit;
			}
			set
			{
				this.m_ReferencePixelsPerUnit = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000C86C File Offset: 0x0000AA6C
		// (set) Token: 0x060001FF RID: 511 RVA: 0x0000C874 File Offset: 0x0000AA74
		public float scaleFactor
		{
			get
			{
				return this.m_ScaleFactor;
			}
			set
			{
				this.m_ScaleFactor = Mathf.Max(0.01f, value);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000200 RID: 512 RVA: 0x0000C887 File Offset: 0x0000AA87
		// (set) Token: 0x06000201 RID: 513 RVA: 0x0000C890 File Offset: 0x0000AA90
		public Vector2 referenceResolution
		{
			get
			{
				return this.m_ReferenceResolution;
			}
			set
			{
				this.m_ReferenceResolution = value;
				if (this.m_ReferenceResolution.x > -1E-05f && this.m_ReferenceResolution.x < 1E-05f)
				{
					this.m_ReferenceResolution.x = 1E-05f * Mathf.Sign(this.m_ReferenceResolution.x);
				}
				if (this.m_ReferenceResolution.y > -1E-05f && this.m_ReferenceResolution.y < 1E-05f)
				{
					this.m_ReferenceResolution.y = 1E-05f * Mathf.Sign(this.m_ReferenceResolution.y);
				}
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000C92E File Offset: 0x0000AB2E
		// (set) Token: 0x06000203 RID: 515 RVA: 0x0000C936 File Offset: 0x0000AB36
		public CanvasScaler.ScreenMatchMode screenMatchMode
		{
			get
			{
				return this.m_ScreenMatchMode;
			}
			set
			{
				this.m_ScreenMatchMode = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000204 RID: 516 RVA: 0x0000C93F File Offset: 0x0000AB3F
		// (set) Token: 0x06000205 RID: 517 RVA: 0x0000C947 File Offset: 0x0000AB47
		public float matchWidthOrHeight
		{
			get
			{
				return this.m_MatchWidthOrHeight;
			}
			set
			{
				this.m_MatchWidthOrHeight = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000C950 File Offset: 0x0000AB50
		// (set) Token: 0x06000207 RID: 519 RVA: 0x0000C958 File Offset: 0x0000AB58
		public CanvasScaler.Unit physicalUnit
		{
			get
			{
				return this.m_PhysicalUnit;
			}
			set
			{
				this.m_PhysicalUnit = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000C961 File Offset: 0x0000AB61
		// (set) Token: 0x06000209 RID: 521 RVA: 0x0000C969 File Offset: 0x0000AB69
		public float fallbackScreenDPI
		{
			get
			{
				return this.m_FallbackScreenDPI;
			}
			set
			{
				this.m_FallbackScreenDPI = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000C972 File Offset: 0x0000AB72
		// (set) Token: 0x0600020B RID: 523 RVA: 0x0000C97A File Offset: 0x0000AB7A
		public float defaultSpriteDPI
		{
			get
			{
				return this.m_DefaultSpriteDPI;
			}
			set
			{
				this.m_DefaultSpriteDPI = Mathf.Max(1f, value);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000C98D File Offset: 0x0000AB8D
		// (set) Token: 0x0600020D RID: 525 RVA: 0x0000C995 File Offset: 0x0000AB95
		public float dynamicPixelsPerUnit
		{
			get
			{
				return this.m_DynamicPixelsPerUnit;
			}
			set
			{
				this.m_DynamicPixelsPerUnit = value;
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000C9A0 File Offset: 0x0000ABA0
		protected CanvasScaler()
		{
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000CA1C File Offset: 0x0000AC1C
		protected override void OnEnable()
		{
			base.OnEnable();
			this.m_Canvas = base.GetComponent<Canvas>();
			this.Handle();
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000CA36 File Offset: 0x0000AC36
		protected override void OnDisable()
		{
			this.SetScaleFactor(1f);
			this.SetReferencePixelsPerUnit(100f);
			base.OnDisable();
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000CA54 File Offset: 0x0000AC54
		protected virtual void Update()
		{
			this.Handle();
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000CA5C File Offset: 0x0000AC5C
		protected virtual void Handle()
		{
			if (this.m_Canvas == null || !this.m_Canvas.isRootCanvas)
			{
				return;
			}
			if (this.m_Canvas.renderMode == RenderMode.WorldSpace)
			{
				this.HandleWorldCanvas();
				return;
			}
			switch (this.m_UiScaleMode)
			{
			case CanvasScaler.ScaleMode.ConstantPixelSize:
				this.HandleConstantPixelSize();
				return;
			case CanvasScaler.ScaleMode.ScaleWithScreenSize:
				this.HandleScaleWithScreenSize();
				return;
			case CanvasScaler.ScaleMode.ConstantPhysicalSize:
				this.HandleConstantPhysicalSize();
				return;
			default:
				return;
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000CAC8 File Offset: 0x0000ACC8
		protected virtual void HandleWorldCanvas()
		{
			this.SetScaleFactor(this.m_DynamicPixelsPerUnit);
			this.SetReferencePixelsPerUnit(this.m_ReferencePixelsPerUnit);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000CAE2 File Offset: 0x0000ACE2
		protected virtual void HandleConstantPixelSize()
		{
			this.SetScaleFactor(this.m_ScaleFactor);
			this.SetReferencePixelsPerUnit(this.m_ReferencePixelsPerUnit);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000CAFC File Offset: 0x0000ACFC
		protected virtual void HandleScaleWithScreenSize()
		{
			Vector2 vector = new Vector2((float)Screen.width, (float)Screen.height);
			int targetDisplay = this.m_Canvas.targetDisplay;
			if (targetDisplay > 0 && targetDisplay < Display.displays.Length)
			{
				Display display = Display.displays[targetDisplay];
				vector = new Vector2((float)display.renderingWidth, (float)display.renderingHeight);
			}
			float num = 0f;
			switch (this.m_ScreenMatchMode)
			{
			case CanvasScaler.ScreenMatchMode.MatchWidthOrHeight:
			{
				float num2 = Mathf.Log(vector.x / this.m_ReferenceResolution.x, 2f);
				float num3 = Mathf.Log(vector.y / this.m_ReferenceResolution.y, 2f);
				float num4 = Mathf.Lerp(num2, num3, this.m_MatchWidthOrHeight);
				num = Mathf.Pow(2f, num4);
				break;
			}
			case CanvasScaler.ScreenMatchMode.Expand:
				num = Mathf.Min(vector.x / this.m_ReferenceResolution.x, vector.y / this.m_ReferenceResolution.y);
				break;
			case CanvasScaler.ScreenMatchMode.Shrink:
				num = Mathf.Max(vector.x / this.m_ReferenceResolution.x, vector.y / this.m_ReferenceResolution.y);
				break;
			}
			this.SetScaleFactor(num);
			this.SetReferencePixelsPerUnit(this.m_ReferencePixelsPerUnit);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000CC3C File Offset: 0x0000AE3C
		protected virtual void HandleConstantPhysicalSize()
		{
			float dpi = Screen.dpi;
			float num = ((dpi == 0f) ? this.m_FallbackScreenDPI : dpi);
			float num2 = 1f;
			switch (this.m_PhysicalUnit)
			{
			case CanvasScaler.Unit.Centimeters:
				num2 = 2.54f;
				break;
			case CanvasScaler.Unit.Millimeters:
				num2 = 25.4f;
				break;
			case CanvasScaler.Unit.Inches:
				num2 = 1f;
				break;
			case CanvasScaler.Unit.Points:
				num2 = 72f;
				break;
			case CanvasScaler.Unit.Picas:
				num2 = 6f;
				break;
			}
			this.SetScaleFactor(num / num2);
			this.SetReferencePixelsPerUnit(this.m_ReferencePixelsPerUnit * num2 / this.m_DefaultSpriteDPI);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000CCCE File Offset: 0x0000AECE
		protected void SetScaleFactor(float scaleFactor)
		{
			if (scaleFactor == this.m_PrevScaleFactor)
			{
				return;
			}
			this.m_Canvas.scaleFactor = scaleFactor;
			this.m_PrevScaleFactor = scaleFactor;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000CCED File Offset: 0x0000AEED
		protected void SetReferencePixelsPerUnit(float referencePixelsPerUnit)
		{
			if (referencePixelsPerUnit == this.m_PrevReferencePixelsPerUnit)
			{
				return;
			}
			this.m_Canvas.referencePixelsPerUnit = referencePixelsPerUnit;
			this.m_PrevReferencePixelsPerUnit = referencePixelsPerUnit;
		}

		// Token: 0x040000B8 RID: 184
		[Tooltip("Determines how UI elements in the Canvas are scaled.")]
		[SerializeField]
		private CanvasScaler.ScaleMode m_UiScaleMode;

		// Token: 0x040000B9 RID: 185
		[Tooltip("If a sprite has this 'Pixels Per Unit' setting, then one pixel in the sprite will cover one unit in the UI.")]
		[SerializeField]
		protected float m_ReferencePixelsPerUnit = 100f;

		// Token: 0x040000BA RID: 186
		[Tooltip("Scales all UI elements in the Canvas by this factor.")]
		[SerializeField]
		protected float m_ScaleFactor = 1f;

		// Token: 0x040000BB RID: 187
		[Tooltip("The resolution the UI layout is designed for. If the screen resolution is larger, the UI will be scaled up, and if it's smaller, the UI will be scaled down. This is done in accordance with the Screen Match Mode.")]
		[SerializeField]
		protected Vector2 m_ReferenceResolution = new Vector2(800f, 600f);

		// Token: 0x040000BC RID: 188
		[Tooltip("A mode used to scale the canvas area if the aspect ratio of the current resolution doesn't fit the reference resolution.")]
		[SerializeField]
		protected CanvasScaler.ScreenMatchMode m_ScreenMatchMode;

		// Token: 0x040000BD RID: 189
		[Tooltip("Determines if the scaling is using the width or height as reference, or a mix in between.")]
		[Range(0f, 1f)]
		[SerializeField]
		protected float m_MatchWidthOrHeight;

		// Token: 0x040000BE RID: 190
		private const float kLogBase = 2f;

		// Token: 0x040000BF RID: 191
		[Tooltip("The physical unit to specify positions and sizes in.")]
		[SerializeField]
		protected CanvasScaler.Unit m_PhysicalUnit = CanvasScaler.Unit.Points;

		// Token: 0x040000C0 RID: 192
		[Tooltip("The DPI to assume if the screen DPI is not known.")]
		[SerializeField]
		protected float m_FallbackScreenDPI = 96f;

		// Token: 0x040000C1 RID: 193
		[Tooltip("The pixels per inch to use for sprites that have a 'Pixels Per Unit' setting that matches the 'Reference Pixels Per Unit' setting.")]
		[SerializeField]
		protected float m_DefaultSpriteDPI = 96f;

		// Token: 0x040000C2 RID: 194
		[Tooltip("The amount of pixels per unit to use for dynamically created bitmaps in the UI, such as Text.")]
		[SerializeField]
		protected float m_DynamicPixelsPerUnit = 1f;

		// Token: 0x040000C3 RID: 195
		private Canvas m_Canvas;

		// Token: 0x040000C4 RID: 196
		[NonSerialized]
		private float m_PrevScaleFactor = 1f;

		// Token: 0x040000C5 RID: 197
		[NonSerialized]
		private float m_PrevReferencePixelsPerUnit = 100f;

		// Token: 0x02000093 RID: 147
		public enum ScaleMode
		{
			// Token: 0x0400028E RID: 654
			ConstantPixelSize,
			// Token: 0x0400028F RID: 655
			ScaleWithScreenSize,
			// Token: 0x04000290 RID: 656
			ConstantPhysicalSize
		}

		// Token: 0x02000094 RID: 148
		public enum ScreenMatchMode
		{
			// Token: 0x04000292 RID: 658
			MatchWidthOrHeight,
			// Token: 0x04000293 RID: 659
			Expand,
			// Token: 0x04000294 RID: 660
			Shrink
		}

		// Token: 0x02000095 RID: 149
		public enum Unit
		{
			// Token: 0x04000296 RID: 662
			Centimeters,
			// Token: 0x04000297 RID: 663
			Millimeters,
			// Token: 0x04000298 RID: 664
			Inches,
			// Token: 0x04000299 RID: 665
			Points,
			// Token: 0x0400029A RID: 666
			Picas
		}
	}
}
