using System;
using UnityEngine;
using UnityEngine.Events;

namespace TMPro
{
	// Token: 0x02000013 RID: 19
	internal struct ColorTween : ITweenValue
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002B83 File Offset: 0x00000D83
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002B8B File Offset: 0x00000D8B
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

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002B94 File Offset: 0x00000D94
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002B9C File Offset: 0x00000D9C
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

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002BA5 File Offset: 0x00000DA5
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002BAD File Offset: 0x00000DAD
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

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002BB6 File Offset: 0x00000DB6
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002BBE File Offset: 0x00000DBE
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

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002BC7 File Offset: 0x00000DC7
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002BCF File Offset: 0x00000DCF
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

		// Token: 0x06000061 RID: 97 RVA: 0x00002BD8 File Offset: 0x00000DD8
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

		// Token: 0x06000062 RID: 98 RVA: 0x00002C69 File Offset: 0x00000E69
		public void AddOnChangedCallback(UnityAction<Color> callback)
		{
			if (this.m_Target == null)
			{
				this.m_Target = new ColorTween.ColorTweenCallback();
			}
			this.m_Target.AddListener(callback);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002BC7 File Offset: 0x00000DC7
		public bool GetIgnoreTimescale()
		{
			return this.m_IgnoreTimeScale;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002BB6 File Offset: 0x00000DB6
		public float GetDuration()
		{
			return this.m_Duration;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002C8A File Offset: 0x00000E8A
		public bool ValidTarget()
		{
			return this.m_Target != null;
		}

		// Token: 0x0400005D RID: 93
		private ColorTween.ColorTweenCallback m_Target;

		// Token: 0x0400005E RID: 94
		private Color m_StartColor;

		// Token: 0x0400005F RID: 95
		private Color m_TargetColor;

		// Token: 0x04000060 RID: 96
		private ColorTween.ColorTweenMode m_TweenMode;

		// Token: 0x04000061 RID: 97
		private float m_Duration;

		// Token: 0x04000062 RID: 98
		private bool m_IgnoreTimeScale;

		// Token: 0x02000076 RID: 118
		public enum ColorTweenMode
		{
			// Token: 0x04000519 RID: 1305
			All,
			// Token: 0x0400051A RID: 1306
			RGB,
			// Token: 0x0400051B RID: 1307
			Alpha
		}

		// Token: 0x02000077 RID: 119
		public class ColorTweenCallback : UnityEvent<Color>
		{
		}
	}
}
