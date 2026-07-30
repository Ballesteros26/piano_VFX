using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x020000CB RID: 203
	public class Image : VisualElement
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x00015FE4 File Offset: 0x000141E4
		// (set) Token: 0x060005BE RID: 1470 RVA: 0x00015FFC File Offset: 0x000141FC
		public Texture image
		{
			get
			{
				return this.m_Image;
			}
			set
			{
				bool flag = value != null && this.vectorImage != null;
				if (flag)
				{
					Debug.LogError("Both image and vectorImage are set on Image object");
					this.m_VectorImage = null;
				}
				this.m_ImageIsInline = value != null;
				bool flag2 = this.m_Image != value;
				if (flag2)
				{
					this.m_Image = value;
					base.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Repaint);
					bool flag3 = this.m_Image == null;
					if (flag3)
					{
						this.m_UV = new Rect(0f, 0f, 1f, 1f);
					}
				}
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x000160A0 File Offset: 0x000142A0
		// (set) Token: 0x060005C0 RID: 1472 RVA: 0x000160B8 File Offset: 0x000142B8
		public VectorImage vectorImage
		{
			get
			{
				return this.m_VectorImage;
			}
			set
			{
				bool flag = value != null && this.image != null;
				if (flag)
				{
					Debug.LogError("Both image and vectorImage are set on Image object");
					this.m_Image = null;
				}
				this.m_ImageIsInline = value != null;
				bool flag2 = this.m_VectorImage != value;
				if (flag2)
				{
					this.m_VectorImage = value;
					base.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Repaint);
					bool flag3 = this.m_VectorImage == null;
					if (flag3)
					{
						this.m_UV = new Rect(0f, 0f, 1f, 1f);
					}
				}
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x0001615C File Offset: 0x0001435C
		// (set) Token: 0x060005C2 RID: 1474 RVA: 0x00016174 File Offset: 0x00014374
		public Rect sourceRect
		{
			get
			{
				return this.GetSourceRect();
			}
			set
			{
				this.CalculateUV(value);
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x00016180 File Offset: 0x00014380
		// (set) Token: 0x060005C4 RID: 1476 RVA: 0x00016198 File Offset: 0x00014398
		public Rect uv
		{
			get
			{
				return this.m_UV;
			}
			set
			{
				this.m_UV = value;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x000161A4 File Offset: 0x000143A4
		// (set) Token: 0x060005C6 RID: 1478 RVA: 0x000161BC File Offset: 0x000143BC
		public ScaleMode scaleMode
		{
			get
			{
				return this.m_ScaleMode;
			}
			set
			{
				this.m_ScaleModeIsInline = true;
				bool flag = this.m_ScaleMode != value;
				if (flag)
				{
					this.m_ScaleMode = value;
					base.IncrementVersion(VersionChangeType.Layout);
				}
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x000161F4 File Offset: 0x000143F4
		// (set) Token: 0x060005C8 RID: 1480 RVA: 0x0001620C File Offset: 0x0001440C
		public Color tintColor
		{
			get
			{
				return this.m_TintColor;
			}
			set
			{
				this.m_TintColorIsInline = true;
				bool flag = this.m_TintColor != value;
				if (flag)
				{
					this.m_TintColor = value;
					base.IncrementVersion(VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00016248 File Offset: 0x00014448
		public Image()
		{
			base.AddToClassList(Image.ussClassName);
			this.m_ScaleMode = ScaleMode.ScaleAndCrop;
			this.m_TintColor = Color.white;
			this.m_UV = new Rect(0f, 0f, 1f, 1f);
			base.requireMeasureFunction = true;
			base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnCustomStyleResolved), TrickleDown.NoTrickleDown);
			base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, new Action<MeshGenerationContext>(this.OnGenerateVisualContent));
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x000162DC File Offset: 0x000144DC
		private Vector2 GetTextureDisplaySize(Texture texture)
		{
			Vector2 zero = Vector2.zero;
			bool flag = texture != null;
			if (flag)
			{
				zero = new Vector2((float)texture.width, (float)texture.height);
			}
			return zero;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00016318 File Offset: 0x00014518
		protected internal override Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
		{
			float num = float.NaN;
			float num2 = float.NaN;
			bool flag = this.image == null && this.vectorImage == null;
			Vector2 vector;
			if (flag)
			{
				vector = new Vector2(num, num2);
			}
			else
			{
				Vector2 vector2 = Vector2.zero;
				bool flag2 = this.image != null;
				if (flag2)
				{
					vector2 = this.GetTextureDisplaySize(this.image);
				}
				else
				{
					vector2 = this.vectorImage.size;
				}
				Rect sourceRect = this.sourceRect;
				bool flag3 = sourceRect != Rect.zero;
				num = (flag3 ? sourceRect.width : vector2.x);
				num2 = (flag3 ? sourceRect.height : vector2.y);
				bool flag4 = widthMode == VisualElement.MeasureMode.AtMost;
				if (flag4)
				{
					num = Mathf.Min(num, desiredWidth);
				}
				bool flag5 = heightMode == VisualElement.MeasureMode.AtMost;
				if (flag5)
				{
					num2 = Mathf.Min(num2, desiredHeight);
				}
				vector = new Vector2(num, num2);
			}
			return vector;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00016408 File Offset: 0x00014608
		private void OnGenerateVisualContent(MeshGenerationContext mgc)
		{
			bool flag = this.image == null && this.vectorImage == null;
			if (!flag)
			{
				MeshGenerationContextUtils.RectangleParams rectangleParams = default(MeshGenerationContextUtils.RectangleParams);
				bool flag2 = this.image != null;
				if (flag2)
				{
					rectangleParams = MeshGenerationContextUtils.RectangleParams.MakeTextured(base.contentRect, this.uv, this.image, this.scaleMode, base.panel.contextType);
				}
				else
				{
					bool flag3 = this.vectorImage != null;
					if (flag3)
					{
						rectangleParams = MeshGenerationContextUtils.RectangleParams.MakeVectorTextured(base.contentRect, this.uv, this.vectorImage, this.scaleMode, base.panel.contextType);
					}
				}
				rectangleParams.color = this.tintColor;
				mgc.Rectangle(rectangleParams);
			}
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x000164D0 File Offset: 0x000146D0
		private void OnCustomStyleResolved(CustomStyleResolvedEvent e)
		{
			Texture2D texture2D = null;
			VectorImage vectorImage = null;
			Color white = Color.white;
			ICustomStyle customStyle = e.customStyle;
			bool flag = !this.m_ImageIsInline && customStyle.TryGetValue(Image.s_ImageProperty, out texture2D);
			if (flag)
			{
				this.m_Image = texture2D;
				bool flag2 = this.m_Image != null;
				if (flag2)
				{
					this.m_VectorImage = null;
				}
			}
			bool flag3 = !this.m_ImageIsInline && customStyle.TryGetValue(Image.s_VectorImageProperty, out vectorImage);
			if (flag3)
			{
				this.m_VectorImage = vectorImage;
				bool flag4 = this.m_VectorImage != null;
				if (flag4)
				{
					this.m_Image = null;
				}
			}
			string text;
			bool flag5 = !this.m_ScaleModeIsInline && customStyle.TryGetValue(Image.s_ScaleModeProperty, out text);
			if (flag5)
			{
				this.m_ScaleMode = (ScaleMode)StylePropertyUtil.GetEnumIntValue(StyleEnumType.ScaleMode, text);
			}
			bool flag6 = !this.m_TintColorIsInline && customStyle.TryGetValue(Image.s_TintColorProperty, out white);
			if (flag6)
			{
				this.m_TintColor = white;
			}
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x000165C8 File Offset: 0x000147C8
		private void CalculateUV(Rect srcRect)
		{
			this.m_UV = new Rect(0f, 0f, 1f, 1f);
			Vector2 vector = Vector2.zero;
			Texture image = this.image;
			bool flag = image != null;
			if (flag)
			{
				vector = this.GetTextureDisplaySize(image);
			}
			VectorImage vectorImage = this.vectorImage;
			bool flag2 = vectorImage != null;
			if (flag2)
			{
				vector = vectorImage.size;
			}
			bool flag3 = vector != Vector2.zero;
			if (flag3)
			{
				this.m_UV.x = srcRect.x / vector.x;
				this.m_UV.width = srcRect.width / vector.x;
				this.m_UV.height = srcRect.height / vector.y;
				this.m_UV.y = 1f - this.m_UV.height - srcRect.y / vector.y;
			}
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x000166C0 File Offset: 0x000148C0
		private Rect GetSourceRect()
		{
			Rect zero = Rect.zero;
			Vector2 vector = Vector2.zero;
			Texture image = this.image;
			bool flag = image != null;
			if (flag)
			{
				vector = this.GetTextureDisplaySize(image);
			}
			VectorImage vectorImage = this.vectorImage;
			bool flag2 = vectorImage != null;
			if (flag2)
			{
				vector = vectorImage.size;
			}
			bool flag3 = vector != Vector2.zero;
			if (flag3)
			{
				zero.x = this.uv.x * vector.x;
				zero.width = this.uv.width * vector.x;
				zero.y = (1f - this.uv.y - this.uv.height) * vector.y;
				zero.height = this.uv.height * vector.y;
			}
			return zero;
		}

		// Token: 0x04000286 RID: 646
		private ScaleMode m_ScaleMode;

		// Token: 0x04000287 RID: 647
		private Texture m_Image;

		// Token: 0x04000288 RID: 648
		private VectorImage m_VectorImage;

		// Token: 0x04000289 RID: 649
		private Rect m_UV;

		// Token: 0x0400028A RID: 650
		private Color m_TintColor;

		// Token: 0x0400028B RID: 651
		private bool m_ImageIsInline;

		// Token: 0x0400028C RID: 652
		private bool m_ScaleModeIsInline;

		// Token: 0x0400028D RID: 653
		private bool m_TintColorIsInline;

		// Token: 0x0400028E RID: 654
		public static readonly string ussClassName = "unity-image";

		// Token: 0x0400028F RID: 655
		private static CustomStyleProperty<Texture2D> s_ImageProperty = new CustomStyleProperty<Texture2D>("--unity-image");

		// Token: 0x04000290 RID: 656
		private static CustomStyleProperty<VectorImage> s_VectorImageProperty = new CustomStyleProperty<VectorImage>("--unity-image");

		// Token: 0x04000291 RID: 657
		private static CustomStyleProperty<string> s_ScaleModeProperty = new CustomStyleProperty<string>("--unity-image-size");

		// Token: 0x04000292 RID: 658
		private static CustomStyleProperty<Color> s_TintColorProperty = new CustomStyleProperty<Color>("--unity-image-tint-color");

		// Token: 0x020000CC RID: 204
		public new class UxmlFactory : UxmlFactory<Image, Image.UxmlTraits>
		{
		}

		// Token: 0x020000CD RID: 205
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x17000151 RID: 337
			// (get) Token: 0x060005D2 RID: 1490 RVA: 0x00016818 File Offset: 0x00014A18
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}
		}
	}
}
