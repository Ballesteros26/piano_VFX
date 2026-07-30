using System;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x02000284 RID: 644
	public interface ITransitionAnimations
	{
		// Token: 0x06001312 RID: 4882
		ValueAnimation<float> Start(float from, float to, int durationMs, Action<VisualElement, float> onValueChanged);

		// Token: 0x06001313 RID: 4883
		ValueAnimation<Rect> Start(Rect from, Rect to, int durationMs, Action<VisualElement, Rect> onValueChanged);

		// Token: 0x06001314 RID: 4884
		ValueAnimation<Color> Start(Color from, Color to, int durationMs, Action<VisualElement, Color> onValueChanged);

		// Token: 0x06001315 RID: 4885
		ValueAnimation<Vector3> Start(Vector3 from, Vector3 to, int durationMs, Action<VisualElement, Vector3> onValueChanged);

		// Token: 0x06001316 RID: 4886
		ValueAnimation<Vector2> Start(Vector2 from, Vector2 to, int durationMs, Action<VisualElement, Vector2> onValueChanged);

		// Token: 0x06001317 RID: 4887
		ValueAnimation<Quaternion> Start(Quaternion from, Quaternion to, int durationMs, Action<VisualElement, Quaternion> onValueChanged);

		// Token: 0x06001318 RID: 4888
		ValueAnimation<StyleValues> Start(StyleValues from, StyleValues to, int durationMs);

		// Token: 0x06001319 RID: 4889
		ValueAnimation<StyleValues> Start(StyleValues to, int durationMs);

		// Token: 0x0600131A RID: 4890
		ValueAnimation<float> Start(Func<VisualElement, float> fromValueGetter, float to, int durationMs, Action<VisualElement, float> onValueChanged);

		// Token: 0x0600131B RID: 4891
		ValueAnimation<Rect> Start(Func<VisualElement, Rect> fromValueGetter, Rect to, int durationMs, Action<VisualElement, Rect> onValueChanged);

		// Token: 0x0600131C RID: 4892
		ValueAnimation<Color> Start(Func<VisualElement, Color> fromValueGetter, Color to, int durationMs, Action<VisualElement, Color> onValueChanged);

		// Token: 0x0600131D RID: 4893
		ValueAnimation<Vector3> Start(Func<VisualElement, Vector3> fromValueGetter, Vector3 to, int durationMs, Action<VisualElement, Vector3> onValueChanged);

		// Token: 0x0600131E RID: 4894
		ValueAnimation<Vector2> Start(Func<VisualElement, Vector2> fromValueGetter, Vector2 to, int durationMs, Action<VisualElement, Vector2> onValueChanged);

		// Token: 0x0600131F RID: 4895
		ValueAnimation<Quaternion> Start(Func<VisualElement, Quaternion> fromValueGetter, Quaternion to, int durationMs, Action<VisualElement, Quaternion> onValueChanged);

		// Token: 0x06001320 RID: 4896
		ValueAnimation<Rect> Layout(Rect to, int durationMs);

		// Token: 0x06001321 RID: 4897
		ValueAnimation<Vector2> TopLeft(Vector2 to, int durationMs);

		// Token: 0x06001322 RID: 4898
		ValueAnimation<Vector2> Size(Vector2 to, int durationMs);

		// Token: 0x06001323 RID: 4899
		ValueAnimation<float> Scale(float to, int duration);

		// Token: 0x06001324 RID: 4900
		ValueAnimation<Vector3> Position(Vector3 to, int duration);

		// Token: 0x06001325 RID: 4901
		ValueAnimation<Quaternion> Rotation(Quaternion to, int duration);
	}
}
