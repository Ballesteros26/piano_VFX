using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001F6 RID: 502
	[NativeClass("UI::RectTransform")]
	[NativeHeader("Runtime/Transform/RectTransform.h")]
	public sealed class RectTransform : Transform
	{
		// Token: 0x1400000E RID: 14
		// (add) Token: 0x0600162A RID: 5674 RVA: 0x00024560 File Offset: 0x00022760
		// (remove) Token: 0x0600162B RID: 5675 RVA: 0x00024594 File Offset: 0x00022794
		[field: DebuggerBrowsable(0)]
		public static event RectTransform.ReapplyDrivenProperties reapplyDrivenProperties;

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x0600162C RID: 5676 RVA: 0x000245C8 File Offset: 0x000227C8
		public Rect rect
		{
			get
			{
				Rect rect;
				this.get_rect_Injected(out rect);
				return rect;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x0600162D RID: 5677 RVA: 0x000245E0 File Offset: 0x000227E0
		// (set) Token: 0x0600162E RID: 5678 RVA: 0x000245F6 File Offset: 0x000227F6
		public Vector2 anchorMin
		{
			get
			{
				Vector2 vector;
				this.get_anchorMin_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_anchorMin_Injected(ref value);
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x0600162F RID: 5679 RVA: 0x00024600 File Offset: 0x00022800
		// (set) Token: 0x06001630 RID: 5680 RVA: 0x00024616 File Offset: 0x00022816
		public Vector2 anchorMax
		{
			get
			{
				Vector2 vector;
				this.get_anchorMax_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_anchorMax_Injected(ref value);
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001631 RID: 5681 RVA: 0x00024620 File Offset: 0x00022820
		// (set) Token: 0x06001632 RID: 5682 RVA: 0x00024636 File Offset: 0x00022836
		public Vector2 anchoredPosition
		{
			get
			{
				Vector2 vector;
				this.get_anchoredPosition_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_anchoredPosition_Injected(ref value);
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001633 RID: 5683 RVA: 0x00024640 File Offset: 0x00022840
		// (set) Token: 0x06001634 RID: 5684 RVA: 0x00024656 File Offset: 0x00022856
		public Vector2 sizeDelta
		{
			get
			{
				Vector2 vector;
				this.get_sizeDelta_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_sizeDelta_Injected(ref value);
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001635 RID: 5685 RVA: 0x00024660 File Offset: 0x00022860
		// (set) Token: 0x06001636 RID: 5686 RVA: 0x00024676 File Offset: 0x00022876
		public Vector2 pivot
		{
			get
			{
				Vector2 vector;
				this.get_pivot_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_pivot_Injected(ref value);
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001637 RID: 5687 RVA: 0x00024680 File Offset: 0x00022880
		// (set) Token: 0x06001638 RID: 5688 RVA: 0x000246B8 File Offset: 0x000228B8
		public Vector3 anchoredPosition3D
		{
			get
			{
				Vector2 anchoredPosition = this.anchoredPosition;
				return new Vector3(anchoredPosition.x, anchoredPosition.y, base.localPosition.z);
			}
			set
			{
				this.anchoredPosition = new Vector2(value.x, value.y);
				Vector3 localPosition = base.localPosition;
				localPosition.z = value.z;
				base.localPosition = localPosition;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001639 RID: 5689 RVA: 0x000246FC File Offset: 0x000228FC
		// (set) Token: 0x0600163A RID: 5690 RVA: 0x0002472C File Offset: 0x0002292C
		public Vector2 offsetMin
		{
			get
			{
				return this.anchoredPosition - Vector2.Scale(this.sizeDelta, this.pivot);
			}
			set
			{
				Vector2 vector = value - (this.anchoredPosition - Vector2.Scale(this.sizeDelta, this.pivot));
				this.sizeDelta -= vector;
				this.anchoredPosition += Vector2.Scale(vector, Vector2.one - this.pivot);
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x0600163B RID: 5691 RVA: 0x00024798 File Offset: 0x00022998
		// (set) Token: 0x0600163C RID: 5692 RVA: 0x000247D0 File Offset: 0x000229D0
		public Vector2 offsetMax
		{
			get
			{
				return this.anchoredPosition + Vector2.Scale(this.sizeDelta, Vector2.one - this.pivot);
			}
			set
			{
				Vector2 vector = value - (this.anchoredPosition + Vector2.Scale(this.sizeDelta, Vector2.one - this.pivot));
				this.sizeDelta += vector;
				this.anchoredPosition += Vector2.Scale(vector, this.pivot);
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x0600163D RID: 5693
		// (set) Token: 0x0600163E RID: 5694
		internal extern Object drivenByObject
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x0600163F RID: 5695
		// (set) Token: 0x06001640 RID: 5696
		internal extern DrivenTransformProperties drivenProperties
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06001641 RID: 5697
		[NativeMethod("UpdateIfTransformDispatchIsDirty")]
		[MethodImpl(4096)]
		public extern void ForceUpdateRectTransforms();

		// Token: 0x06001642 RID: 5698 RVA: 0x0002483C File Offset: 0x00022A3C
		public void GetLocalCorners(Vector3[] fourCornersArray)
		{
			bool flag = fourCornersArray == null || fourCornersArray.Length < 4;
			if (flag)
			{
				Debug.LogError("Calling GetLocalCorners with an array that is null or has less than 4 elements.");
			}
			else
			{
				Rect rect = this.rect;
				float x = rect.x;
				float y = rect.y;
				float xMax = rect.xMax;
				float yMax = rect.yMax;
				fourCornersArray[0] = new Vector3(x, y, 0f);
				fourCornersArray[1] = new Vector3(x, yMax, 0f);
				fourCornersArray[2] = new Vector3(xMax, yMax, 0f);
				fourCornersArray[3] = new Vector3(xMax, y, 0f);
			}
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x000248E0 File Offset: 0x00022AE0
		public void GetWorldCorners(Vector3[] fourCornersArray)
		{
			bool flag = fourCornersArray == null || fourCornersArray.Length < 4;
			if (flag)
			{
				Debug.LogError("Calling GetWorldCorners with an array that is null or has less than 4 elements.");
			}
			else
			{
				this.GetLocalCorners(fourCornersArray);
				Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
				for (int i = 0; i < 4; i++)
				{
					fourCornersArray[i] = localToWorldMatrix.MultiplyPoint(fourCornersArray[i]);
				}
			}
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x00024948 File Offset: 0x00022B48
		public void SetInsetAndSizeFromParentEdge(RectTransform.Edge edge, float inset, float size)
		{
			int num = ((edge == RectTransform.Edge.Top || edge == RectTransform.Edge.Bottom) ? 1 : 0);
			bool flag = edge == RectTransform.Edge.Top || edge == RectTransform.Edge.Right;
			float num2 = (float)(flag ? 1 : 0);
			Vector2 vector = this.anchorMin;
			vector[num] = num2;
			this.anchorMin = vector;
			vector = this.anchorMax;
			vector[num] = num2;
			this.anchorMax = vector;
			Vector2 sizeDelta = this.sizeDelta;
			sizeDelta[num] = size;
			this.sizeDelta = sizeDelta;
			Vector2 anchoredPosition = this.anchoredPosition;
			anchoredPosition[num] = (flag ? (-inset - size * (1f - this.pivot[num])) : (inset + size * this.pivot[num]));
			this.anchoredPosition = anchoredPosition;
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x00024A14 File Offset: 0x00022C14
		public void SetSizeWithCurrentAnchors(RectTransform.Axis axis, float size)
		{
			Vector2 sizeDelta = this.sizeDelta;
			sizeDelta[(int)axis] = size - this.GetParentSize()[(int)axis] * (this.anchorMax[(int)axis] - this.anchorMin[(int)axis]);
			this.sizeDelta = sizeDelta;
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x00024A6D File Offset: 0x00022C6D
		[RequiredByNativeCode]
		internal static void SendReapplyDrivenProperties(RectTransform driven)
		{
			RectTransform.ReapplyDrivenProperties reapplyDrivenProperties = RectTransform.reapplyDrivenProperties;
			if (reapplyDrivenProperties != null)
			{
				reapplyDrivenProperties(driven);
			}
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x00024A84 File Offset: 0x00022C84
		internal Rect GetRectInParentSpace()
		{
			Rect rect = this.rect;
			Vector2 vector = this.offsetMin + Vector2.Scale(this.pivot, rect.size);
			bool flag = base.transform.parent;
			if (flag)
			{
				RectTransform component = base.transform.parent.GetComponent<RectTransform>();
				bool flag2 = component;
				if (flag2)
				{
					vector += Vector2.Scale(this.anchorMin, component.rect.size);
				}
			}
			rect.x += vector.x;
			rect.y += vector.y;
			return rect;
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x00024B3C File Offset: 0x00022D3C
		private Vector2 GetParentSize()
		{
			RectTransform rectTransform = base.parent as RectTransform;
			bool flag = !rectTransform;
			Vector2 vector;
			if (flag)
			{
				vector = Vector2.zero;
			}
			else
			{
				vector = rectTransform.rect.size;
			}
			return vector;
		}

		// Token: 0x0600164A RID: 5706
		[MethodImpl(4096)]
		private extern void get_rect_Injected(out Rect ret);

		// Token: 0x0600164B RID: 5707
		[MethodImpl(4096)]
		private extern void get_anchorMin_Injected(out Vector2 ret);

		// Token: 0x0600164C RID: 5708
		[MethodImpl(4096)]
		private extern void set_anchorMin_Injected(ref Vector2 value);

		// Token: 0x0600164D RID: 5709
		[MethodImpl(4096)]
		private extern void get_anchorMax_Injected(out Vector2 ret);

		// Token: 0x0600164E RID: 5710
		[MethodImpl(4096)]
		private extern void set_anchorMax_Injected(ref Vector2 value);

		// Token: 0x0600164F RID: 5711
		[MethodImpl(4096)]
		private extern void get_anchoredPosition_Injected(out Vector2 ret);

		// Token: 0x06001650 RID: 5712
		[MethodImpl(4096)]
		private extern void set_anchoredPosition_Injected(ref Vector2 value);

		// Token: 0x06001651 RID: 5713
		[MethodImpl(4096)]
		private extern void get_sizeDelta_Injected(out Vector2 ret);

		// Token: 0x06001652 RID: 5714
		[MethodImpl(4096)]
		private extern void set_sizeDelta_Injected(ref Vector2 value);

		// Token: 0x06001653 RID: 5715
		[MethodImpl(4096)]
		private extern void get_pivot_Injected(out Vector2 ret);

		// Token: 0x06001654 RID: 5716
		[MethodImpl(4096)]
		private extern void set_pivot_Injected(ref Vector2 value);

		// Token: 0x020001F7 RID: 503
		public enum Edge
		{
			// Token: 0x040006F9 RID: 1785
			Left,
			// Token: 0x040006FA RID: 1786
			Right,
			// Token: 0x040006FB RID: 1787
			Top,
			// Token: 0x040006FC RID: 1788
			Bottom
		}

		// Token: 0x020001F8 RID: 504
		public enum Axis
		{
			// Token: 0x040006FE RID: 1790
			Horizontal,
			// Token: 0x040006FF RID: 1791
			Vertical
		}

		// Token: 0x020001F9 RID: 505
		// (Invoke) Token: 0x06001656 RID: 5718
		public delegate void ReapplyDrivenProperties(RectTransform driven);
	}
}
