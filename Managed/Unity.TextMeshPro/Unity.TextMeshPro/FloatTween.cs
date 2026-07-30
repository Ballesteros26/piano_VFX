using System;
using UnityEngine;
using UnityEngine.Events;

namespace TMPro
{
	// Token: 0x02000014 RID: 20
	internal struct FloatTween : ITweenValue
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002C95 File Offset: 0x00000E95
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002C9D File Offset: 0x00000E9D
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

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002CA6 File Offset: 0x00000EA6
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00002CAE File Offset: 0x00000EAE
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

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002CB7 File Offset: 0x00000EB7
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00002CBF File Offset: 0x00000EBF
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

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002CC8 File Offset: 0x00000EC8
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00002CD0 File Offset: 0x00000ED0
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

		// Token: 0x0600006E RID: 110 RVA: 0x00002CDC File Offset: 0x00000EDC
		public void TweenValue(float floatPercentage)
		{
			if (!this.ValidTarget())
			{
				return;
			}
			float num = Mathf.Lerp(this.m_StartValue, this.m_TargetValue, floatPercentage);
			this.m_Target.Invoke(num);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002D11 File Offset: 0x00000F11
		public void AddOnChangedCallback(UnityAction<float> callback)
		{
			if (this.m_Target == null)
			{
				this.m_Target = new FloatTween.FloatTweenCallback();
			}
			this.m_Target.AddListener(callback);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002CC8 File Offset: 0x00000EC8
		public bool GetIgnoreTimescale()
		{
			return this.m_IgnoreTimeScale;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002CB7 File Offset: 0x00000EB7
		public float GetDuration()
		{
			return this.m_Duration;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002D32 File Offset: 0x00000F32
		public bool ValidTarget()
		{
			return this.m_Target != null;
		}

		// Token: 0x04000063 RID: 99
		private FloatTween.FloatTweenCallback m_Target;

		// Token: 0x04000064 RID: 100
		private float m_StartValue;

		// Token: 0x04000065 RID: 101
		private float m_TargetValue;

		// Token: 0x04000066 RID: 102
		private float m_Duration;

		// Token: 0x04000067 RID: 103
		private bool m_IgnoreTimeScale;

		// Token: 0x02000078 RID: 120
		public class FloatTweenCallback : UnityEvent<float>
		{
		}
	}
}
