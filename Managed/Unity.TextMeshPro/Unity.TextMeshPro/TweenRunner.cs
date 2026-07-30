using System;
using System.Collections;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000015 RID: 21
	internal class TweenRunner<T> where T : struct, ITweenValue
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00002D3D File Offset: 0x00000F3D
		private static IEnumerator Start(T tweenInfo)
		{
			if (!tweenInfo.ValidTarget())
			{
				yield break;
			}
			float elapsedTime = 0f;
			while (elapsedTime < tweenInfo.duration)
			{
				elapsedTime += (tweenInfo.ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime);
				float num = Mathf.Clamp01(elapsedTime / tweenInfo.duration);
				tweenInfo.TweenValue(num);
				yield return null;
			}
			tweenInfo.TweenValue(1f);
			yield break;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002D4C File Offset: 0x00000F4C
		public void Init(MonoBehaviour coroutineContainer)
		{
			this.m_CoroutineContainer = coroutineContainer;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002D58 File Offset: 0x00000F58
		public void StartTween(T info)
		{
			if (this.m_CoroutineContainer == null)
			{
				Debug.LogWarning("Coroutine container not configured... did you forget to call Init?");
				return;
			}
			this.StopTween();
			if (!this.m_CoroutineContainer.gameObject.activeInHierarchy)
			{
				info.TweenValue(1f);
				return;
			}
			this.m_Tween = TweenRunner<T>.Start(info);
			this.m_CoroutineContainer.StartCoroutine(this.m_Tween);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002DC7 File Offset: 0x00000FC7
		public void StopTween()
		{
			if (this.m_Tween != null)
			{
				this.m_CoroutineContainer.StopCoroutine(this.m_Tween);
				this.m_Tween = null;
			}
		}

		// Token: 0x04000068 RID: 104
		protected MonoBehaviour m_CoroutineContainer;

		// Token: 0x04000069 RID: 105
		protected IEnumerator m_Tween;
	}
}
