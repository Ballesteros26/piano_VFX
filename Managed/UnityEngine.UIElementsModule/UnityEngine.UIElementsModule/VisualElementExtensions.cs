using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200009A RID: 154
	public static class VisualElementExtensions
	{
		// Token: 0x060004B2 RID: 1202 RVA: 0x00011DAC File Offset: 0x0000FFAC
		public static Vector2 WorldToLocal(this VisualElement ele, Vector2 p)
		{
			bool flag = ele == null;
			if (flag)
			{
				throw new ArgumentNullException("ele");
			}
			return VisualElement.MultiplyMatrix44Point2(ele.worldTransformInverse, p);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00011DE0 File Offset: 0x0000FFE0
		public static Vector2 LocalToWorld(this VisualElement ele, Vector2 p)
		{
			bool flag = ele == null;
			if (flag)
			{
				throw new ArgumentNullException("ele");
			}
			return VisualElement.MultiplyMatrix44Point2(ele.worldTransform, p);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00011E14 File Offset: 0x00010014
		public static Rect WorldToLocal(this VisualElement ele, Rect r)
		{
			bool flag = ele == null;
			if (flag)
			{
				throw new ArgumentNullException("ele");
			}
			Vector2 vector = VisualElement.MultiplyMatrix44Point2(ele.worldTransformInverse, r.position);
			r.position = vector;
			r.size = ele.worldTransformInverse.MultiplyVector(r.size);
			return r;
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00011E80 File Offset: 0x00010080
		public static Rect LocalToWorld(this VisualElement ele, Rect r)
		{
			bool flag = ele == null;
			if (flag)
			{
				throw new ArgumentNullException("ele");
			}
			Matrix4x4 worldTransform = ele.worldTransform;
			r.position = VisualElement.MultiplyMatrix44Point2(worldTransform, r.position);
			r.size = worldTransform.MultiplyVector(r.size);
			return r;
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00011EE4 File Offset: 0x000100E4
		public static Vector2 ChangeCoordinatesTo(this VisualElement src, VisualElement dest, Vector2 point)
		{
			return dest.WorldToLocal(src.LocalToWorld(point));
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00011F04 File Offset: 0x00010104
		public static Rect ChangeCoordinatesTo(this VisualElement src, VisualElement dest, Rect rect)
		{
			return dest.WorldToLocal(src.LocalToWorld(rect));
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00011F24 File Offset: 0x00010124
		public static void StretchToParentSize(this VisualElement elem)
		{
			bool flag = elem == null;
			if (flag)
			{
				throw new ArgumentNullException("elem");
			}
			IStyle style = elem.style;
			style.position = Position.Absolute;
			style.left = 0f;
			style.top = 0f;
			style.right = 0f;
			style.bottom = 0f;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00011FA0 File Offset: 0x000101A0
		public static void StretchToParentWidth(this VisualElement elem)
		{
			bool flag = elem == null;
			if (flag)
			{
				throw new ArgumentNullException("elem");
			}
			IStyle style = elem.style;
			style.position = Position.Absolute;
			style.left = 0f;
			style.right = 0f;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00011FF8 File Offset: 0x000101F8
		public static void AddManipulator(this VisualElement ele, IManipulator manipulator)
		{
			bool flag = manipulator != null;
			if (flag)
			{
				manipulator.target = ele;
			}
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00012018 File Offset: 0x00010218
		public static void RemoveManipulator(this VisualElement ele, IManipulator manipulator)
		{
			bool flag = manipulator != null;
			if (flag)
			{
				manipulator.target = null;
			}
		}
	}
}
