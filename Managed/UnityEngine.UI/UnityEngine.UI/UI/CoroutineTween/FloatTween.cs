using System;
using UnityEngine.Events;

namespace UnityEngine.UI.CoroutineTween
{
	// Token: 0x0200004A RID: 74
	internal struct FloatTween : ITweenValue
	{
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x000162C1 File Offset: 0x000144C1
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x000162C9 File Offset: 0x000144C9
		public float startValue
		{
			get
			{
				return this.m_StartValue;
			}
			set
			{
				this.m_StartValue = value;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x000162D2 File Offset: 0x000144D2
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x000162DA File Offset: 0x000144DA
		public float targetValue
		{
			get
			{
				return this.m_TargetValue;
			}
			set
			{
				this.m_TargetValue = value;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x000162E3 File Offset: 0x000144E3
		// (set) Token: 0x060004D0 RID: 1232 RVA: 0x000162EB File Offset: 0x000144EB
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

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x000162F4 File Offset: 0x000144F4
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x000162FC File Offset: 0x000144FC
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

		// Token: 0x060004D3 RID: 1235 RVA: 0x00016308 File Offset: 0x00014508
		public void TweenValue(float floatPercentage)
		{
			if (!this.ValidTarget())
			{
				return;
			}
			float num = Mathf.Lerp(this.m_StartValue, this.m_TargetValue, floatPercentage);
			this.m_Target.Invoke(num);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0001633D File Offset: 0x0001453D
		public void AddOnChangedCallback(UnityAction<float> callback)
		{
			if (this.m_Target == null)
			{
				this.m_Target = new FloatTween.FloatTweenCallback();
			}
			this.m_Target.AddListener(callback);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x000162F4 File Offset: 0x000144F4
		public bool GetIgnoreTimescale()
		{
			return this.m_IgnoreTimeScale;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x000162E3 File Offset: 0x000144E3
		public float GetDuration()
		{
			return this.m_Duration;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0001635E File Offset: 0x0001455E
		public bool ValidTarget()
		{
			return this.m_Target != null;
		}

		// Token: 0x04000194 RID: 404
		private FloatTween.FloatTweenCallback m_Target;

		// Token: 0x04000195 RID: 405
		private float m_StartValue;

		// Token: 0x04000196 RID: 406
		private float m_TargetValue;

		// Token: 0x04000197 RID: 407
		private float m_Duration;

		// Token: 0x04000198 RID: 408
		private bool m_IgnoreTimeScale;

		// Token: 0x020000B7 RID: 183
		public class FloatTweenCallback : UnityEvent<float>
		{
		}
	}
}
