using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x02000046 RID: 70
	[AddComponentMenu("UI/Effects/Shadow", 14)]
	public class Shadow : BaseMeshEffect
	{
		// Token: 0x0600049C RID: 1180 RVA: 0x00015CA8 File Offset: 0x00013EA8
		protected Shadow()
		{
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x00015CF6 File Offset: 0x00013EF6
		// (set) Token: 0x0600049E RID: 1182 RVA: 0x00015CFE File Offset: 0x00013EFE
		public Color effectColor
		{
			get
			{
				return this.m_EffectColor;
			}
			set
			{
				this.m_EffectColor = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty();
				}
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x00015D20 File Offset: 0x00013F20
		// (set) Token: 0x060004A0 RID: 1184 RVA: 0x00015D28 File Offset: 0x00013F28
		public Vector2 effectDistance
		{
			get
			{
				return this.m_EffectDistance;
			}
			set
			{
				if (value.x > 600f)
				{
					value.x = 600f;
				}
				if (value.x < -600f)
				{
					value.x = -600f;
				}
				if (value.y > 600f)
				{
					value.y = 600f;
				}
				if (value.y < -600f)
				{
					value.y = -600f;
				}
				if (this.m_EffectDistance == value)
				{
					return;
				}
				this.m_EffectDistance = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty();
				}
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x00015DC8 File Offset: 0x00013FC8
		// (set) Token: 0x060004A2 RID: 1186 RVA: 0x00015DD0 File Offset: 0x00013FD0
		public bool useGraphicAlpha
		{
			get
			{
				return this.m_UseGraphicAlpha;
			}
			set
			{
				this.m_UseGraphicAlpha = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty();
				}
			}
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00015DF4 File Offset: 0x00013FF4
		protected void ApplyShadowZeroAlloc(List<UIVertex> verts, Color32 color, int start, int end, float x, float y)
		{
			int num = verts.Count + end - start;
			if (verts.Capacity < num)
			{
				verts.Capacity = num;
			}
			for (int i = start; i < end; i++)
			{
				UIVertex uivertex = verts[i];
				verts.Add(uivertex);
				Vector3 position = uivertex.position;
				position.x += x;
				position.y += y;
				uivertex.position = position;
				Color32 color2 = color;
				if (this.m_UseGraphicAlpha)
				{
					color2.a = color2.a * verts[i].color.a / byte.MaxValue;
				}
				uivertex.color = color2;
				verts[i] = uivertex;
			}
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00015EA8 File Offset: 0x000140A8
		protected void ApplyShadow(List<UIVertex> verts, Color32 color, int start, int end, float x, float y)
		{
			this.ApplyShadowZeroAlloc(verts, color, start, end, x, y);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00015EBC File Offset: 0x000140BC
		public override void ModifyMesh(VertexHelper vh)
		{
			if (!this.IsActive())
			{
				return;
			}
			List<UIVertex> list = ListPool<UIVertex>.Get();
			vh.GetUIVertexStream(list);
			this.ApplyShadow(list, this.effectColor, 0, list.Count, this.effectDistance.x, this.effectDistance.y);
			vh.Clear();
			vh.AddUIVertexTriangleStream(list);
			ListPool<UIVertex>.Release(list);
		}

		// Token: 0x04000188 RID: 392
		[SerializeField]
		private Color m_EffectColor = new Color(0f, 0f, 0f, 0.5f);

		// Token: 0x04000189 RID: 393
		[SerializeField]
		private Vector2 m_EffectDistance = new Vector2(1f, -1f);

		// Token: 0x0400018A RID: 394
		[SerializeField]
		private bool m_UseGraphicAlpha = true;

		// Token: 0x0400018B RID: 395
		private const float kMaxEffectDistance = 600f;
	}
}
