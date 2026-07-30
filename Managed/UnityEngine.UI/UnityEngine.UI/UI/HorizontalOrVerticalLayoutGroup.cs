using System;

namespace UnityEngine.UI
{
	// Token: 0x0200001E RID: 30
	[ExecuteAlways]
	public abstract class HorizontalOrVerticalLayoutGroup : LayoutGroup
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000D55E File Offset: 0x0000B75E
		// (set) Token: 0x0600023E RID: 574 RVA: 0x0000D566 File Offset: 0x0000B766
		public float spacing
		{
			get
			{
				return this.m_Spacing;
			}
			set
			{
				base.SetProperty<float>(ref this.m_Spacing, value);
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000D575 File Offset: 0x0000B775
		// (set) Token: 0x06000240 RID: 576 RVA: 0x0000D57D File Offset: 0x0000B77D
		public bool childForceExpandWidth
		{
			get
			{
				return this.m_ChildForceExpandWidth;
			}
			set
			{
				base.SetProperty<bool>(ref this.m_ChildForceExpandWidth, value);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000D58C File Offset: 0x0000B78C
		// (set) Token: 0x06000242 RID: 578 RVA: 0x0000D594 File Offset: 0x0000B794
		public bool childForceExpandHeight
		{
			get
			{
				return this.m_ChildForceExpandHeight;
			}
			set
			{
				base.SetProperty<bool>(ref this.m_ChildForceExpandHeight, value);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000D5A3 File Offset: 0x0000B7A3
		// (set) Token: 0x06000244 RID: 580 RVA: 0x0000D5AB File Offset: 0x0000B7AB
		public bool childControlWidth
		{
			get
			{
				return this.m_ChildControlWidth;
			}
			set
			{
				base.SetProperty<bool>(ref this.m_ChildControlWidth, value);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000D5BA File Offset: 0x0000B7BA
		// (set) Token: 0x06000246 RID: 582 RVA: 0x0000D5C2 File Offset: 0x0000B7C2
		public bool childControlHeight
		{
			get
			{
				return this.m_ChildControlHeight;
			}
			set
			{
				base.SetProperty<bool>(ref this.m_ChildControlHeight, value);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000D5D1 File Offset: 0x0000B7D1
		// (set) Token: 0x06000248 RID: 584 RVA: 0x0000D5D9 File Offset: 0x0000B7D9
		public bool childScaleWidth
		{
			get
			{
				return this.m_ChildScaleWidth;
			}
			set
			{
				base.SetProperty<bool>(ref this.m_ChildScaleWidth, value);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000D5E8 File Offset: 0x0000B7E8
		// (set) Token: 0x0600024A RID: 586 RVA: 0x0000D5F0 File Offset: 0x0000B7F0
		public bool childScaleHeight
		{
			get
			{
				return this.m_ChildScaleHeight;
			}
			set
			{
				base.SetProperty<bool>(ref this.m_ChildScaleHeight, value);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000D5FF File Offset: 0x0000B7FF
		// (set) Token: 0x0600024C RID: 588 RVA: 0x0000D607 File Offset: 0x0000B807
		public bool reverseArrangement
		{
			get
			{
				return this.m_ReverseArrangement;
			}
			set
			{
				base.SetProperty<bool>(ref this.m_ReverseArrangement, value);
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000D618 File Offset: 0x0000B818
		protected void CalcAlongAxis(int axis, bool isVertical)
		{
			float num = (float)((axis == 0) ? base.padding.horizontal : base.padding.vertical);
			bool flag = ((axis == 0) ? this.m_ChildControlWidth : this.m_ChildControlHeight);
			bool flag2 = ((axis == 0) ? this.m_ChildScaleWidth : this.m_ChildScaleHeight);
			bool flag3 = ((axis == 0) ? this.m_ChildForceExpandWidth : this.m_ChildForceExpandHeight);
			float num2 = num;
			float num3 = num;
			float num4 = 0f;
			bool flag4 = isVertical ^ (axis == 1);
			for (int i = 0; i < base.rectChildren.Count; i++)
			{
				RectTransform rectTransform = base.rectChildren[i];
				float num5;
				float num6;
				float num7;
				this.GetChildSizes(rectTransform, axis, flag, flag3, out num5, out num6, out num7);
				if (flag2)
				{
					float num8 = rectTransform.localScale[axis];
					num5 *= num8;
					num6 *= num8;
					num7 *= num8;
				}
				if (flag4)
				{
					num2 = Mathf.Max(num5 + num, num2);
					num3 = Mathf.Max(num6 + num, num3);
					num4 = Mathf.Max(num7, num4);
				}
				else
				{
					num2 += num5 + this.spacing;
					num3 += num6 + this.spacing;
					num4 += num7;
				}
			}
			if (!flag4 && base.rectChildren.Count > 0)
			{
				num2 -= this.spacing;
				num3 -= this.spacing;
			}
			num3 = Mathf.Max(num2, num3);
			base.SetLayoutInputForAxis(num2, num3, num4, axis);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000D788 File Offset: 0x0000B988
		protected void SetChildrenAlongAxis(int axis, bool isVertical)
		{
			float num = base.rectTransform.rect.size[axis];
			bool flag = ((axis == 0) ? this.m_ChildControlWidth : this.m_ChildControlHeight);
			bool flag2 = ((axis == 0) ? this.m_ChildScaleWidth : this.m_ChildScaleHeight);
			bool flag3 = ((axis == 0) ? this.m_ChildForceExpandWidth : this.m_ChildForceExpandHeight);
			float alignmentOnAxis = base.GetAlignmentOnAxis(axis);
			bool flag4 = isVertical ^ (axis == 1);
			int num2 = (this.m_ReverseArrangement ? (base.rectChildren.Count - 1) : 0);
			int num3 = (this.m_ReverseArrangement ? 0 : base.rectChildren.Count);
			int num4 = (this.m_ReverseArrangement ? (-1) : 1);
			if (flag4)
			{
				float num5 = num - (float)((axis == 0) ? base.padding.horizontal : base.padding.vertical);
				int num6 = num2;
				while (this.m_ReverseArrangement ? (num6 >= num3) : (num6 < num3))
				{
					RectTransform rectTransform = base.rectChildren[num6];
					float num7;
					float num8;
					float num9;
					this.GetChildSizes(rectTransform, axis, flag, flag3, out num7, out num8, out num9);
					float num10 = (flag2 ? rectTransform.localScale[axis] : 1f);
					float num11 = Mathf.Clamp(num5, num7, (num9 > 0f) ? num : num8);
					float startOffset = base.GetStartOffset(axis, num11 * num10);
					if (flag)
					{
						base.SetChildAlongAxisWithScale(rectTransform, axis, startOffset, num11, num10);
					}
					else
					{
						float num12 = (num11 - rectTransform.sizeDelta[axis]) * alignmentOnAxis;
						base.SetChildAlongAxisWithScale(rectTransform, axis, startOffset + num12, num10);
					}
					num6 += num4;
				}
				return;
			}
			float num13 = (float)((axis == 0) ? base.padding.left : base.padding.top);
			float num14 = 0f;
			float num15 = num - base.GetTotalPreferredSize(axis);
			if (num15 > 0f)
			{
				if (base.GetTotalFlexibleSize(axis) == 0f)
				{
					num13 = base.GetStartOffset(axis, base.GetTotalPreferredSize(axis) - (float)((axis == 0) ? base.padding.horizontal : base.padding.vertical));
				}
				else if (base.GetTotalFlexibleSize(axis) > 0f)
				{
					num14 = num15 / base.GetTotalFlexibleSize(axis);
				}
			}
			float num16 = 0f;
			if (base.GetTotalMinSize(axis) != base.GetTotalPreferredSize(axis))
			{
				num16 = Mathf.Clamp01((num - base.GetTotalMinSize(axis)) / (base.GetTotalPreferredSize(axis) - base.GetTotalMinSize(axis)));
			}
			int num17 = num2;
			while (this.m_ReverseArrangement ? (num17 >= num3) : (num17 < num3))
			{
				RectTransform rectTransform2 = base.rectChildren[num17];
				float num18;
				float num19;
				float num20;
				this.GetChildSizes(rectTransform2, axis, flag, flag3, out num18, out num19, out num20);
				float num21 = (flag2 ? rectTransform2.localScale[axis] : 1f);
				float num22 = Mathf.Lerp(num18, num19, num16);
				num22 += num20 * num14;
				if (flag)
				{
					base.SetChildAlongAxisWithScale(rectTransform2, axis, num13, num22, num21);
				}
				else
				{
					float num23 = (num22 - rectTransform2.sizeDelta[axis]) * alignmentOnAxis;
					base.SetChildAlongAxisWithScale(rectTransform2, axis, num13 + num23, num21);
				}
				num13 += num22 * num21 + this.spacing;
				num17 += num4;
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000DAC8 File Offset: 0x0000BCC8
		private void GetChildSizes(RectTransform child, int axis, bool controlSize, bool childForceExpand, out float min, out float preferred, out float flexible)
		{
			if (!controlSize)
			{
				min = child.sizeDelta[axis];
				preferred = min;
				flexible = 0f;
			}
			else
			{
				min = LayoutUtility.GetMinSize(child, axis);
				preferred = LayoutUtility.GetPreferredSize(child, axis);
				flexible = LayoutUtility.GetFlexibleSize(child, axis);
			}
			if (childForceExpand)
			{
				flexible = Mathf.Max(flexible, 1f);
			}
		}

		// Token: 0x040000D0 RID: 208
		[SerializeField]
		protected float m_Spacing;

		// Token: 0x040000D1 RID: 209
		[SerializeField]
		protected bool m_ChildForceExpandWidth = true;

		// Token: 0x040000D2 RID: 210
		[SerializeField]
		protected bool m_ChildForceExpandHeight = true;

		// Token: 0x040000D3 RID: 211
		[SerializeField]
		protected bool m_ChildControlWidth = true;

		// Token: 0x040000D4 RID: 212
		[SerializeField]
		protected bool m_ChildControlHeight = true;

		// Token: 0x040000D5 RID: 213
		[SerializeField]
		protected bool m_ChildScaleWidth;

		// Token: 0x040000D6 RID: 214
		[SerializeField]
		protected bool m_ChildScaleHeight;

		// Token: 0x040000D7 RID: 215
		[SerializeField]
		protected bool m_ReverseArrangement;
	}
}
