using System;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000030 RID: 48
	[RequireComponent(typeof(CanvasRenderer))]
	[AddComponentMenu("UI/Raw Image", 12)]
	public class RawImage : MaskableGraphic
	{
		// Token: 0x060002F7 RID: 759 RVA: 0x0000F710 File Offset: 0x0000D910
		protected RawImage()
		{
			base.useLegacyMeshGeneration = false;
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000F740 File Offset: 0x0000D940
		public override Texture mainTexture
		{
			get
			{
				if (!(this.m_Texture == null))
				{
					return this.m_Texture;
				}
				if (this.material != null && this.material.mainTexture != null)
				{
					return this.material.mainTexture;
				}
				return Graphic.s_WhiteTexture;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000F794 File Offset: 0x0000D994
		// (set) Token: 0x060002FA RID: 762 RVA: 0x0000F79C File Offset: 0x0000D99C
		public Texture texture
		{
			get
			{
				return this.m_Texture;
			}
			set
			{
				if (this.m_Texture == value)
				{
					return;
				}
				this.m_Texture = value;
				this.SetVerticesDirty();
				this.SetMaterialDirty();
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000F7C0 File Offset: 0x0000D9C0
		// (set) Token: 0x060002FC RID: 764 RVA: 0x0000F7C8 File Offset: 0x0000D9C8
		public Rect uvRect
		{
			get
			{
				return this.m_UVRect;
			}
			set
			{
				if (this.m_UVRect == value)
				{
					return;
				}
				this.m_UVRect = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000F7E8 File Offset: 0x0000D9E8
		public override void SetNativeSize()
		{
			Texture mainTexture = this.mainTexture;
			if (mainTexture != null)
			{
				int num = Mathf.RoundToInt((float)mainTexture.width * this.uvRect.width);
				int num2 = Mathf.RoundToInt((float)mainTexture.height * this.uvRect.height);
				base.rectTransform.anchorMax = base.rectTransform.anchorMin;
				base.rectTransform.sizeDelta = new Vector2((float)num, (float)num2);
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000F868 File Offset: 0x0000DA68
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			Texture mainTexture = this.mainTexture;
			vh.Clear();
			if (mainTexture != null)
			{
				Rect pixelAdjustedRect = base.GetPixelAdjustedRect();
				Vector4 vector = new Vector4(pixelAdjustedRect.x, pixelAdjustedRect.y, pixelAdjustedRect.x + pixelAdjustedRect.width, pixelAdjustedRect.y + pixelAdjustedRect.height);
				float num = (float)mainTexture.width * mainTexture.texelSize.x;
				float num2 = (float)mainTexture.height * mainTexture.texelSize.y;
				Color color = this.color;
				vh.AddVert(new Vector3(vector.x, vector.y), color, new Vector2(this.m_UVRect.xMin * num, this.m_UVRect.yMin * num2));
				vh.AddVert(new Vector3(vector.x, vector.w), color, new Vector2(this.m_UVRect.xMin * num, this.m_UVRect.yMax * num2));
				vh.AddVert(new Vector3(vector.z, vector.w), color, new Vector2(this.m_UVRect.xMax * num, this.m_UVRect.yMax * num2));
				vh.AddVert(new Vector3(vector.z, vector.y), color, new Vector2(this.m_UVRect.xMax * num, this.m_UVRect.yMin * num2));
				vh.AddTriangle(0, 1, 2);
				vh.AddTriangle(2, 3, 0);
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000909C File Offset: 0x0000729C
		protected override void OnDidApplyAnimationProperties()
		{
			this.SetMaterialDirty();
			this.SetVerticesDirty();
		}

		// Token: 0x040000FE RID: 254
		[FormerlySerializedAs("m_Tex")]
		[SerializeField]
		private Texture m_Texture;

		// Token: 0x040000FF RID: 255
		[SerializeField]
		private Rect m_UVRect = new Rect(0f, 0f, 1f, 1f);
	}
}
