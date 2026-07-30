using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TMPro
{
	// Token: 0x02000071 RID: 113
	[RequireComponent(typeof(RectTransform))]
	public class TextContainer : UIBehaviour
	{
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x000366BB File Offset: 0x000348BB
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x000366C3 File Offset: 0x000348C3
		public bool hasChanged
		{
			get
			{
				return this.m_hasChanged;
			}
			set
			{
				this.m_hasChanged = value;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x000366CC File Offset: 0x000348CC
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x000366D4 File Offset: 0x000348D4
		public Vector2 pivot
		{
			get
			{
				return this.m_pivot;
			}
			set
			{
				if (this.m_pivot != value)
				{
					this.m_pivot = value;
					this.m_anchorPosition = this.GetAnchorPosition(this.m_pivot);
					this.m_hasChanged = true;
					this.OnContainerChanged();
				}
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x0003670A File Offset: 0x0003490A
		// (set) Token: 0x0600056A RID: 1386 RVA: 0x00036712 File Offset: 0x00034912
		public TextContainerAnchors anchorPosition
		{
			get
			{
				return this.m_anchorPosition;
			}
			set
			{
				if (this.m_anchorPosition != value)
				{
					this.m_anchorPosition = value;
					this.m_pivot = this.GetPivot(this.m_anchorPosition);
					this.m_hasChanged = true;
					this.OnContainerChanged();
				}
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x00036743 File Offset: 0x00034943
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x0003674B File Offset: 0x0003494B
		public Rect rect
		{
			get
			{
				return this.m_rect;
			}
			set
			{
				if (this.m_rect != value)
				{
					this.m_rect = value;
					this.m_hasChanged = true;
					this.OnContainerChanged();
				}
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x0003676F File Offset: 0x0003496F
		// (set) Token: 0x0600056E RID: 1390 RVA: 0x0003678C File Offset: 0x0003498C
		public Vector2 size
		{
			get
			{
				return new Vector2(this.m_rect.width, this.m_rect.height);
			}
			set
			{
				if (new Vector2(this.m_rect.width, this.m_rect.height) != value)
				{
					this.SetRect(value);
					this.m_hasChanged = true;
					this.m_isDefaultWidth = false;
					this.m_isDefaultHeight = false;
					this.OnContainerChanged();
				}
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x000367DE File Offset: 0x000349DE
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x000367EB File Offset: 0x000349EB
		public float width
		{
			get
			{
				return this.m_rect.width;
			}
			set
			{
				this.SetRect(new Vector2(value, this.m_rect.height));
				this.m_hasChanged = true;
				this.m_isDefaultWidth = false;
				this.OnContainerChanged();
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x00036818 File Offset: 0x00034A18
		// (set) Token: 0x06000572 RID: 1394 RVA: 0x00036825 File Offset: 0x00034A25
		public float height
		{
			get
			{
				return this.m_rect.height;
			}
			set
			{
				this.SetRect(new Vector2(this.m_rect.width, value));
				this.m_hasChanged = true;
				this.m_isDefaultHeight = false;
				this.OnContainerChanged();
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x00036852 File Offset: 0x00034A52
		public bool isDefaultWidth
		{
			get
			{
				return this.m_isDefaultWidth;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x0003685A File Offset: 0x00034A5A
		public bool isDefaultHeight
		{
			get
			{
				return this.m_isDefaultHeight;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00036862 File Offset: 0x00034A62
		// (set) Token: 0x06000576 RID: 1398 RVA: 0x0003686A File Offset: 0x00034A6A
		public bool isAutoFitting
		{
			get
			{
				return this.m_isAutoFitting;
			}
			set
			{
				this.m_isAutoFitting = value;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00036873 File Offset: 0x00034A73
		public Vector3[] corners
		{
			get
			{
				return this.m_corners;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x0003687B File Offset: 0x00034A7B
		public Vector3[] worldCorners
		{
			get
			{
				return this.m_worldCorners;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x00036883 File Offset: 0x00034A83
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x0003688B File Offset: 0x00034A8B
		public Vector4 margins
		{
			get
			{
				return this.m_margins;
			}
			set
			{
				if (this.m_margins != value)
				{
					this.m_margins = value;
					this.m_hasChanged = true;
					this.OnContainerChanged();
				}
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x000368AF File Offset: 0x00034AAF
		public RectTransform rectTransform
		{
			get
			{
				if (this.m_rectTransform == null)
				{
					this.m_rectTransform = base.GetComponent<RectTransform>();
				}
				return this.m_rectTransform;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x000368D1 File Offset: 0x00034AD1
		public TextMeshPro textMeshPro
		{
			get
			{
				if (this.m_textMeshPro == null)
				{
					this.m_textMeshPro = base.GetComponent<TextMeshPro>();
				}
				return this.m_textMeshPro;
			}
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x000368F3 File Offset: 0x00034AF3
		protected override void Awake()
		{
			Debug.LogWarning("The Text Container component is now Obsolete and can safely be removed from [" + base.gameObject.name + "].", this);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00036915 File Offset: 0x00034B15
		protected override void OnEnable()
		{
			this.OnContainerChanged();
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x000027BA File Offset: 0x000009BA
		protected override void OnDisable()
		{
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00036920 File Offset: 0x00034B20
		private void OnContainerChanged()
		{
			this.UpdateCorners();
			if (this.m_rectTransform != null)
			{
				this.m_rectTransform.sizeDelta = this.size;
				this.m_rectTransform.hasChanged = true;
			}
			if (this.textMeshPro != null)
			{
				this.m_textMeshPro.SetVerticesDirty();
				this.m_textMeshPro.margin = this.m_margins;
			}
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00036988 File Offset: 0x00034B88
		protected override void OnRectTransformDimensionsChange()
		{
			if (this.rectTransform == null)
			{
				this.m_rectTransform = base.gameObject.AddComponent<RectTransform>();
			}
			if (this.m_rectTransform.sizeDelta != TextContainer.k_defaultSize)
			{
				this.size = this.m_rectTransform.sizeDelta;
			}
			this.pivot = this.m_rectTransform.pivot;
			this.m_hasChanged = true;
			this.OnContainerChanged();
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x000369FA File Offset: 0x00034BFA
		private void SetRect(Vector2 size)
		{
			this.m_rect = new Rect(this.m_rect.x, this.m_rect.y, size.x, size.y);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00036A2C File Offset: 0x00034C2C
		private void UpdateCorners()
		{
			this.m_corners[0] = new Vector3(-this.m_pivot.x * this.m_rect.width, -this.m_pivot.y * this.m_rect.height);
			this.m_corners[1] = new Vector3(-this.m_pivot.x * this.m_rect.width, (1f - this.m_pivot.y) * this.m_rect.height);
			this.m_corners[2] = new Vector3((1f - this.m_pivot.x) * this.m_rect.width, (1f - this.m_pivot.y) * this.m_rect.height);
			this.m_corners[3] = new Vector3((1f - this.m_pivot.x) * this.m_rect.width, -this.m_pivot.y * this.m_rect.height);
			if (this.m_rectTransform != null)
			{
				this.m_rectTransform.pivot = this.m_pivot;
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00036B70 File Offset: 0x00034D70
		private Vector2 GetPivot(TextContainerAnchors anchor)
		{
			Vector2 zero = Vector2.zero;
			switch (anchor)
			{
			case TextContainerAnchors.TopLeft:
				zero = new Vector2(0f, 1f);
				break;
			case TextContainerAnchors.Top:
				zero = new Vector2(0.5f, 1f);
				break;
			case TextContainerAnchors.TopRight:
				zero = new Vector2(1f, 1f);
				break;
			case TextContainerAnchors.Left:
				zero = new Vector2(0f, 0.5f);
				break;
			case TextContainerAnchors.Middle:
				zero = new Vector2(0.5f, 0.5f);
				break;
			case TextContainerAnchors.Right:
				zero = new Vector2(1f, 0.5f);
				break;
			case TextContainerAnchors.BottomLeft:
				zero = new Vector2(0f, 0f);
				break;
			case TextContainerAnchors.Bottom:
				zero = new Vector2(0.5f, 0f);
				break;
			case TextContainerAnchors.BottomRight:
				zero = new Vector2(1f, 0f);
				break;
			}
			return zero;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00036C64 File Offset: 0x00034E64
		private TextContainerAnchors GetAnchorPosition(Vector2 pivot)
		{
			if (pivot == new Vector2(0f, 1f))
			{
				return TextContainerAnchors.TopLeft;
			}
			if (pivot == new Vector2(0.5f, 1f))
			{
				return TextContainerAnchors.Top;
			}
			if (pivot == new Vector2(1f, 1f))
			{
				return TextContainerAnchors.TopRight;
			}
			if (pivot == new Vector2(0f, 0.5f))
			{
				return TextContainerAnchors.Left;
			}
			if (pivot == new Vector2(0.5f, 0.5f))
			{
				return TextContainerAnchors.Middle;
			}
			if (pivot == new Vector2(1f, 0.5f))
			{
				return TextContainerAnchors.Right;
			}
			if (pivot == new Vector2(0f, 0f))
			{
				return TextContainerAnchors.BottomLeft;
			}
			if (pivot == new Vector2(0.5f, 0f))
			{
				return TextContainerAnchors.Bottom;
			}
			if (pivot == new Vector2(1f, 0f))
			{
				return TextContainerAnchors.BottomRight;
			}
			return TextContainerAnchors.Custom;
		}

		// Token: 0x040004FA RID: 1274
		private bool m_hasChanged;

		// Token: 0x040004FB RID: 1275
		[SerializeField]
		private Vector2 m_pivot;

		// Token: 0x040004FC RID: 1276
		[SerializeField]
		private TextContainerAnchors m_anchorPosition = TextContainerAnchors.Middle;

		// Token: 0x040004FD RID: 1277
		[SerializeField]
		private Rect m_rect;

		// Token: 0x040004FE RID: 1278
		private bool m_isDefaultWidth;

		// Token: 0x040004FF RID: 1279
		private bool m_isDefaultHeight;

		// Token: 0x04000500 RID: 1280
		private bool m_isAutoFitting;

		// Token: 0x04000501 RID: 1281
		private Vector3[] m_corners = new Vector3[4];

		// Token: 0x04000502 RID: 1282
		private Vector3[] m_worldCorners = new Vector3[4];

		// Token: 0x04000503 RID: 1283
		[SerializeField]
		private Vector4 m_margins;

		// Token: 0x04000504 RID: 1284
		private RectTransform m_rectTransform;

		// Token: 0x04000505 RID: 1285
		private static Vector2 k_defaultSize = new Vector2(100f, 100f);

		// Token: 0x04000506 RID: 1286
		private TextMeshPro m_textMeshPro;
	}
}
