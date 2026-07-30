using System;
using UnityEngine.Events;

namespace UnityEngine.UI.CoroutineTween
{
	// Token: 0x02000049 RID: 73
	internal struct ColorTween : ITweenValue
	{
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x000161AF File Offset: 0x000143AF
		// (set) Token: 0x060004BD RID: 1213 RVA: 0x000161B7 File Offset: 0x000143B7
		public Color startColor
		{
			get
			{
				return this.m_StartColor;
			}
			set
			{
				this.m_StartColor = value;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x000161C0 File Offset: 0x000143C0
		// (set) Token: 0x060004BF RID: 1215 RVA: 0x000161C8 File Offset: 0x000143C8
		public Color targetColor
		{
			get
			{
				return this.m_TargetColor;
			}
			set
			{
				this.m_TargetColor = value;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x000161D1 File Offset: 0x000143D1
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x000161D9 File Offset: 0x000143D9
		public ColorTween.ColorTweenMode tweenMode
		{
			get
			{
				return this.m_TweenMode;
			}
			set
			{
				this.m_TweenMode = value;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x000161E2 File Offset: 0x000143E2
		// (set) Token: 0x060004C3 RID: 1219 RVA: 0x000161EA File Offset: 0x000143EA
		public float duration
		{
			get
			{
				return this.m_Duration;
			}
			set
			{
				this.m_Duration = value;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x000161F3 File Offset: 0x000143F3
		// (set) Token: 0x060004C5 RID: 1221 RVA: 0x000161FB File Offset: 0x000143FB
		public bool ignoreTimeScale
		{
			get
			{
				return this.m_IgnoreTimeScale;
			}
			set
			{
				this.m_IgnoreTimeScale = value;
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00016204 File Offset: 0x00014404
		public void TweenValue(float floatPercentage)
		{
			if (!this.ValidTarget())
			{
				return;
			}
			Color color = Color.Lerp(this.m_StartColor, this.m_TargetColor, floatPercentage);
			if (this.m_TweenMode == ColorTween.ColorTweenMode.Alpha)
			{
				color.r = this.m_StartColor.r;
				color.g = this.m_StartColor.g;
				color.b = this.m_StartColor.b;
			}
			else if (this.m_TweenMode == ColorTween.ColorTweenMode.RGB)
			{
				color.a = this.m_StartColor.a;
			}
			this.m_Target.Invoke(color);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00016295 File Offset: 0x00014495
		public void AddOnChangedCallback(UnityAction<Color> callback)
		{
			if (this.m_Target == null)
			{
				this.m_Target = new ColorTween.ColorTweenCallback();
			}
			this.m_Target.AddListener(callback);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x000161F3 File Offset: 0x000143F3
		public bool GetIgnoreTimescale()
		{
			return this.m_IgnoreTimeScale;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x000161E2 File Offset: 0x000143E2
		public float GetDuration()
		{
			return this.m_Duration;
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x000162B6 File Offset: 0x000144B6
		public bool ValidTarget()
		{
			return this.m_Target != null;
		}

		// Token: 0x0400018E RID: 398
		private ColorTween.ColorTweenCallback m_Target;

		// Token: 0x0400018F RID: 399
		private Color m_StartColor;

		// Token: 0x04000190 RID: 400
		private Color m_TargetColor;

		// Token: 0x04000191 RID: 401
		private ColorTween.ColorTweenMode m_TweenMode;

		// Token: 0x04000192 RID: 402
		private float m_Duration;

		// Token: 0x04000193 RID: 403
		private bool m_IgnoreTimeScale;

		// Token: 0x020000B5 RID: 181
		public enum ColorTweenMode
		{
			// Token: 0x040002FB RID: 763
			All,
			// Token: 0x040002FC RID: 764
			RGB,
			// Token: 0x040002FD RID: 765
			Alpha
		}

		// Token: 0x020000B6 RID: 182
		public class ColorTweenCallback : UnityEvent<Color>
		{
		}
	}
}
