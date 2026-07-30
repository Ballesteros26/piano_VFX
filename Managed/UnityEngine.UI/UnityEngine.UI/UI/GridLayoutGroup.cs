using System;

namespace UnityEngine.UI
{
	// Token: 0x0200001C RID: 28
	[AddComponentMenu("Layout/Grid Layout Group", 152)]
	public class GridLayoutGroup : LayoutGroup
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000CE67 File Offset: 0x0000B067
		// (set) Token: 0x06000227 RID: 551 RVA: 0x0000CE6F File Offset: 0x0000B06F
		public GridLayoutGroup.Corner startCorner
		{
			get
			{
				return this.m_StartCorner;
			}
			set
			{
				base.SetProperty<GridLayoutGroup.Corner>(ref this.m_StartCorner, value);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000CE7E File Offset: 0x0000B07E
		// (set) Token: 0x06000229 RID: 553 RVA: 0x0000CE86 File Offset: 0x0000B086
		public GridLayoutGroup.Axis startAxis
		{
			get
			{
				return this.m_StartAxis;
			}
			set
			{
				base.SetProperty<GridLayoutGroup.Axis>(ref this.m_StartAxis, value);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000CE95 File Offset: 0x0000B095
		// (set) Token: 0x0600022B RID: 555 RVA: 0x0000CE9D File Offset: 0x0000B09D
		public Vector2 cellSize
		{
			get
			{
				return this.m_CellSize;
			}
			set
			{
				base.SetProperty<Vector2>(ref this.m_CellSize, value);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000CEAC File Offset: 0x0000B0AC
		// (set) Token: 0x0600022D RID: 557 RVA: 0x0000CEB4 File Offset: 0x0000B0B4
		public Vector2 spacing
		{
			get
			{
				return this.m_Spacing;
			}
			set
			{
				base.SetProperty<Vector2>(ref this.m_Spacing, value);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000CEC3 File Offset: 0x0000B0C3
		// (set) Token: 0x0600022F RID: 559 RVA: 0x0000CECB File Offset: 0x0000B0CB
		public GridLayoutGroup.Constraint constraint
		{
			get
			{
				return this.m_Constraint;
			}
			set
			{
				base.SetProperty<GridLayoutGroup.Constraint>(ref this.m_Constraint, value);
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000CEDA File Offset: 0x0000B0DA
		// (set) Token: 0x06000231 RID: 561 RVA: 0x0000CEE2 File Offset: 0x0000B0E2
		public int constraintCount
		{
			get
			{
				return this.m_ConstraintCount;
			}
			set
			{
				base.SetProperty<int>(ref this.m_ConstraintCount, Mathf.Max(1, value));
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000CEF7 File Offset: 0x0000B0F7
		protected GridLayoutGroup()
		{
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000CF28 File Offset: 0x0000B128
		public override void CalculateLayoutInputHorizontal()
		{
			base.CalculateLayoutInputHorizontal();
			int num2;
			int num;
			if (this.m_Constraint == GridLayoutGroup.Constraint.FixedColumnCount)
			{
				num = (num2 = this.m_ConstraintCount);
			}
			else if (this.m_Constraint == GridLayoutGroup.Constraint.FixedRowCount)
			{
				num = (num2 = Mathf.CeilToInt((float)base.rectChildren.Count / (float)this.m_ConstraintCount - 0.001f));
			}
			else
			{
				num2 = 1;
				num = Mathf.CeilToInt(Mathf.Sqrt((float)base.rectChildren.Count));
			}
			base.SetLayoutInputForAxis((float)base.padding.horizontal + (this.cellSize.x + this.spacing.x) * (float)num2 - this.spacing.x, (float)base.padding.horizontal + (this.cellSize.x + this.spacing.x) * (float)num - this.spacing.x, -1f, 0);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000D00C File Offset: 0x0000B20C
		public override void CalculateLayoutInputVertical()
		{
			int num;
			if (this.m_Constraint == GridLayoutGroup.Constraint.FixedColumnCount)
			{
				num = Mathf.CeilToInt((float)base.rectChildren.Count / (float)this.m_ConstraintCount - 0.001f);
			}
			else if (this.m_Constraint == GridLayoutGroup.Constraint.FixedRowCount)
			{
				num = this.m_ConstraintCount;
			}
			else
			{
				float width = base.rectTransform.rect.width;
				int num2 = Mathf.Max(1, Mathf.FloorToInt((width - (float)base.padding.horizontal + this.spacing.x + 0.001f) / (this.cellSize.x + this.spacing.x)));
				num = Mathf.CeilToInt((float)base.rectChildren.Count / (float)num2);
			}
			float num3 = (float)base.padding.vertical + (this.cellSize.y + this.spacing.y) * (float)num - this.spacing.y;
			base.SetLayoutInputForAxis(num3, num3, -1f, 1);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000D10B File Offset: 0x0000B30B
		public override void SetLayoutHorizontal()
		{
			this.SetCellsAlongAxis(0);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000D114 File Offset: 0x0000B314
		public override void SetLayoutVertical()
		{
			this.SetCellsAlongAxis(1);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000D120 File Offset: 0x0000B320
		private void SetCellsAlongAxis(int axis)
		{
			if (axis == 0)
			{
				for (int i = 0; i < base.rectChildren.Count; i++)
				{
					RectTransform rectTransform = base.rectChildren[i];
					this.m_Tracker.Add(this, rectTransform, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaX | DrivenTransformProperties.SizeDeltaY);
					rectTransform.anchorMin = Vector2.up;
					rectTransform.anchorMax = Vector2.up;
					rectTransform.sizeDelta = this.cellSize;
				}
				return;
			}
			float x = base.rectTransform.rect.size.x;
			float y = base.rectTransform.rect.size.y;
			int num = 1;
			int num2 = 1;
			if (this.m_Constraint == GridLayoutGroup.Constraint.FixedColumnCount)
			{
				num = this.m_ConstraintCount;
				if (base.rectChildren.Count > num)
				{
					num2 = base.rectChildren.Count / num + ((base.rectChildren.Count % num > 0) ? 1 : 0);
				}
			}
			else if (this.m_Constraint == GridLayoutGroup.Constraint.FixedRowCount)
			{
				num2 = this.m_ConstraintCount;
				if (base.rectChildren.Count > num2)
				{
					num = base.rectChildren.Count / num2 + ((base.rectChildren.Count % num2 > 0) ? 1 : 0);
				}
			}
			else
			{
				if (this.cellSize.x + this.spacing.x <= 0f)
				{
					num = int.MaxValue;
				}
				else
				{
					num = Mathf.Max(1, Mathf.FloorToInt((x - (float)base.padding.horizontal + this.spacing.x + 0.001f) / (this.cellSize.x + this.spacing.x)));
				}
				if (this.cellSize.y + this.spacing.y <= 0f)
				{
					num2 = int.MaxValue;
				}
				else
				{
					num2 = Mathf.Max(1, Mathf.FloorToInt((y - (float)base.padding.vertical + this.spacing.y + 0.001f) / (this.cellSize.y + this.spacing.y)));
				}
			}
			int num3 = (int)(this.startCorner % GridLayoutGroup.Corner.LowerLeft);
			int num4 = (int)(this.startCorner / GridLayoutGroup.Corner.LowerLeft);
			int num5;
			int num6;
			int num7;
			if (this.startAxis == GridLayoutGroup.Axis.Horizontal)
			{
				num5 = num;
				num6 = Mathf.Clamp(num, 1, base.rectChildren.Count);
				num7 = Mathf.Clamp(num2, 1, Mathf.CeilToInt((float)base.rectChildren.Count / (float)num5));
			}
			else
			{
				num5 = num2;
				num7 = Mathf.Clamp(num2, 1, base.rectChildren.Count);
				num6 = Mathf.Clamp(num, 1, Mathf.CeilToInt((float)base.rectChildren.Count / (float)num5));
			}
			Vector2 vector = new Vector2((float)num6 * this.cellSize.x + (float)(num6 - 1) * this.spacing.x, (float)num7 * this.cellSize.y + (float)(num7 - 1) * this.spacing.y);
			Vector2 vector2 = new Vector2(base.GetStartOffset(0, vector.x), base.GetStartOffset(1, vector.y));
			for (int j = 0; j < base.rectChildren.Count; j++)
			{
				int num8;
				int num9;
				if (this.startAxis == GridLayoutGroup.Axis.Horizontal)
				{
					num8 = j % num5;
					num9 = j / num5;
				}
				else
				{
					num8 = j / num5;
					num9 = j % num5;
				}
				if (num3 == 1)
				{
					num8 = num6 - 1 - num8;
				}
				if (num4 == 1)
				{
					num9 = num7 - 1 - num9;
				}
				base.SetChildAlongAxis(base.rectChildren[j], 0, vector2.x + (this.cellSize[0] + this.spacing[0]) * (float)num8, this.cellSize[0]);
				base.SetChildAlongAxis(base.rectChildren[j], 1, vector2.y + (this.cellSize[1] + this.spacing[1]) * (float)num9, this.cellSize[1]);
			}
		}

		// Token: 0x040000CA RID: 202
		[SerializeField]
		protected GridLayoutGroup.Corner m_StartCorner;

		// Token: 0x040000CB RID: 203
		[SerializeField]
		protected GridLayoutGroup.Axis m_StartAxis;

		// Token: 0x040000CC RID: 204
		[SerializeField]
		protected Vector2 m_CellSize = new Vector2(100f, 100f);

		// Token: 0x040000CD RID: 205
		[SerializeField]
		protected Vector2 m_Spacing = Vector2.zero;

		// Token: 0x040000CE RID: 206
		[SerializeField]
		protected GridLayoutGroup.Constraint m_Constraint;

		// Token: 0x040000CF RID: 207
		[SerializeField]
		protected int m_ConstraintCount = 2;

		// Token: 0x02000097 RID: 151
		public enum Corner
		{
			// Token: 0x040002A0 RID: 672
			UpperLeft,
			// Token: 0x040002A1 RID: 673
			UpperRight,
			// Token: 0x040002A2 RID: 674
			LowerLeft,
			// Token: 0x040002A3 RID: 675
			LowerRight
		}

		// Token: 0x02000098 RID: 152
		public enum Axis
		{
			// Token: 0x040002A5 RID: 677
			Horizontal,
			// Token: 0x040002A6 RID: 678
			Vertical
		}

		// Token: 0x02000099 RID: 153
		public enum Constraint
		{
			// Token: 0x040002A8 RID: 680
			Flexible,
			// Token: 0x040002A9 RID: 681
			FixedColumnCount,
			// Token: 0x040002AA RID: 682
			FixedRowCount
		}
	}
}
