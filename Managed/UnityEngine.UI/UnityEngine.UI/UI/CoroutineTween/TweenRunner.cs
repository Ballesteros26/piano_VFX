using System;
using System.Collections;

namespace UnityEngine.UI.CoroutineTween
{
	// Token: 0x0200004B RID: 75
	internal class TweenRunner<T> where T : struct, ITweenValue
	{
		// Token: 0x060004D8 RID: 1240 RVA: 0x00016369 File Offset: 0x00014569
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

		// Token: 0x060004D9 RID: 1241 RVA: 0x00016378 File Offset: 0x00014578
		public void Init(MonoBehaviour coroutineContainer)
		{
			this.m_CoroutineContainer = coroutineContainer;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00016384 File Offset: 0x00014584
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

		// Token: 0x060004DB RID: 1243 RVA: 0x000163F3 File Offset: 0x000145F3
		public void StopTween()
		{
			if (this.m_Tween != null)
			{
				this.m_CoroutineContainer.StopCoroutine(this.m_Tween);
				this.m_Tween = null;
			}
		}

		// Token: 0x04000199 RID: 409
		protected MonoBehaviour m_CoroutineContainer;

		// Token: 0x0400019A RID: 410
		protected IEnumerator m_Tween;
	}
}
