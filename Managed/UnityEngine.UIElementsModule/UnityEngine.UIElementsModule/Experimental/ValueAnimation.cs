using System;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x02000288 RID: 648
	public sealed class ValueAnimation<T> : IValueAnimationUpdate, IValueAnimation
	{
		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001335 RID: 4917 RVA: 0x00055824 File Offset: 0x00053A24
		// (set) Token: 0x06001336 RID: 4918 RVA: 0x0005583C File Offset: 0x00053A3C
		public int durationMs
		{
			get
			{
				return this.m_DurationMs;
			}
			set
			{
				bool flag = value < 1;
				if (flag)
				{
					value = 1;
				}
				this.m_DurationMs = value;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001337 RID: 4919 RVA: 0x0005585E File Offset: 0x00053A5E
		// (set) Token: 0x06001338 RID: 4920 RVA: 0x00055866 File Offset: 0x00053A66
		public Func<float, float> easingCurve { get; set; }

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001339 RID: 4921 RVA: 0x0005586F File Offset: 0x00053A6F
		// (set) Token: 0x0600133A RID: 4922 RVA: 0x00055877 File Offset: 0x00053A77
		public bool isRunning { get; private set; }

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x0600133B RID: 4923 RVA: 0x00055880 File Offset: 0x00053A80
		// (set) Token: 0x0600133C RID: 4924 RVA: 0x00055888 File Offset: 0x00053A88
		public Action onAnimationCompleted { get; set; }

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x0600133D RID: 4925 RVA: 0x00055891 File Offset: 0x00053A91
		// (set) Token: 0x0600133E RID: 4926 RVA: 0x00055899 File Offset: 0x00053A99
		public bool autoRecycle { get; set; }

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x0600133F RID: 4927 RVA: 0x000558A2 File Offset: 0x00053AA2
		// (set) Token: 0x06001340 RID: 4928 RVA: 0x000558AA File Offset: 0x00053AAA
		private bool recycled { get; set; }

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001341 RID: 4929 RVA: 0x000558B3 File Offset: 0x00053AB3
		// (set) Token: 0x06001342 RID: 4930 RVA: 0x000558BB File Offset: 0x00053ABB
		private VisualElement owner { get; set; }

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06001343 RID: 4931 RVA: 0x000558C4 File Offset: 0x00053AC4
		// (set) Token: 0x06001344 RID: 4932 RVA: 0x000558CC File Offset: 0x00053ACC
		public Action<VisualElement, T> valueUpdated { get; set; }

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001345 RID: 4933 RVA: 0x000558D5 File Offset: 0x00053AD5
		// (set) Token: 0x06001346 RID: 4934 RVA: 0x000558DD File Offset: 0x00053ADD
		public Func<VisualElement, T> initialValue { get; set; }

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x000558E6 File Offset: 0x00053AE6
		// (set) Token: 0x06001348 RID: 4936 RVA: 0x000558EE File Offset: 0x00053AEE
		public Func<T, T, float, T> interpolator { get; set; }

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x000558F8 File Offset: 0x00053AF8
		// (set) Token: 0x0600134A RID: 4938 RVA: 0x00055946 File Offset: 0x00053B46
		public T from
		{
			get
			{
				bool flag = !this.fromValueSet;
				if (flag)
				{
					bool flag2 = this.initialValue != null;
					if (flag2)
					{
						this.from = this.initialValue.Invoke(this.owner);
					}
				}
				return this._from;
			}
			set
			{
				this.fromValueSet = true;
				this._from = value;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x0600134B RID: 4939 RVA: 0x00055957 File Offset: 0x00053B57
		// (set) Token: 0x0600134C RID: 4940 RVA: 0x0005595F File Offset: 0x00053B5F
		public T to { get; set; }

		// Token: 0x0600134D RID: 4941 RVA: 0x00055968 File Offset: 0x00053B68
		public ValueAnimation()
		{
			this.SetDefaultValues();
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x00055980 File Offset: 0x00053B80
		public void Start()
		{
			this.CheckNotRecycled();
			bool flag = this.owner != null;
			if (flag)
			{
				this.m_StartTimeMs = Panel.TimeSinceStartupMs();
				this.Register();
				this.isRunning = true;
			}
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x000559C0 File Offset: 0x00053BC0
		public void Stop()
		{
			this.CheckNotRecycled();
			bool isRunning = this.isRunning;
			if (isRunning)
			{
				this.Unregister();
				this.isRunning = false;
				Action onAnimationCompleted = this.onAnimationCompleted;
				if (onAnimationCompleted != null)
				{
					onAnimationCompleted.Invoke();
				}
				bool autoRecycle = this.autoRecycle;
				if (autoRecycle)
				{
					bool flag = !this.recycled;
					if (flag)
					{
						this.Recycle();
					}
				}
			}
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x00055A24 File Offset: 0x00053C24
		public void Recycle()
		{
			this.CheckNotRecycled();
			bool isRunning = this.isRunning;
			if (isRunning)
			{
				bool flag = !this.autoRecycle;
				if (!flag)
				{
					this.Stop();
					return;
				}
				this.Stop();
			}
			this.SetDefaultValues();
			this.recycled = true;
			ValueAnimation<T>.sObjectPool.Release(this);
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x00055A84 File Offset: 0x00053C84
		void IValueAnimationUpdate.Tick(long currentTimeMs)
		{
			this.CheckNotRecycled();
			long num = currentTimeMs - this.m_StartTimeMs;
			float num2 = (float)num / (float)this.durationMs;
			bool flag = false;
			bool flag2 = num2 >= 1f;
			if (flag2)
			{
				num2 = 1f;
				flag = true;
			}
			Func<float, float> easingCurve = this.easingCurve;
			num2 = ((easingCurve != null) ? easingCurve.Invoke(num2) : num2);
			bool flag3 = this.interpolator != null;
			if (flag3)
			{
				T t = this.interpolator.Invoke(this.from, this.to, num2);
				Action<VisualElement, T> valueUpdated = this.valueUpdated;
				if (valueUpdated != null)
				{
					valueUpdated.Invoke(this.owner, t);
				}
			}
			bool flag4 = flag;
			if (flag4)
			{
				this.Stop();
			}
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x00055B34 File Offset: 0x00053D34
		private void SetDefaultValues()
		{
			this.m_DurationMs = 400;
			this.autoRecycle = true;
			this.owner = null;
			this.m_StartTimeMs = 0L;
			this.onAnimationCompleted = null;
			this.valueUpdated = null;
			this.initialValue = null;
			this.interpolator = null;
			this.to = default(T);
			this.from = default(T);
			this.fromValueSet = false;
			this.easingCurve = new Func<float, float>(Easing.OutQuad);
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x00055BC0 File Offset: 0x00053DC0
		private void Unregister()
		{
			bool flag = this.owner != null;
			if (flag)
			{
				this.owner.UnregisterAnimation(this);
			}
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x00055BEC File Offset: 0x00053DEC
		private void Register()
		{
			bool flag = this.owner != null;
			if (flag)
			{
				this.owner.RegisterAnimation(this);
			}
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x00055C18 File Offset: 0x00053E18
		internal void SetOwner(VisualElement e)
		{
			bool isRunning = this.isRunning;
			if (isRunning)
			{
				this.Unregister();
			}
			this.owner = e;
			bool isRunning2 = this.isRunning;
			if (isRunning2)
			{
				this.Register();
			}
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x00055C54 File Offset: 0x00053E54
		private void CheckNotRecycled()
		{
			bool recycled = this.recycled;
			if (recycled)
			{
				throw new InvalidOperationException("Animation object has been recycled. Use KeepAlive() to keep a reference to an animation after it has been stopped.");
			}
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x00055C78 File Offset: 0x00053E78
		public static ValueAnimation<T> Create(VisualElement e, Func<T, T, float, T> interpolator)
		{
			ValueAnimation<T> valueAnimation = ValueAnimation<T>.sObjectPool.Get();
			valueAnimation.recycled = false;
			valueAnimation.SetOwner(e);
			valueAnimation.interpolator = interpolator;
			return valueAnimation;
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x00055CB0 File Offset: 0x00053EB0
		public ValueAnimation<T> Ease(Func<float, float> easing)
		{
			this.easingCurve = easing;
			return this;
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x00055CCC File Offset: 0x00053ECC
		public ValueAnimation<T> OnCompleted(Action callback)
		{
			this.onAnimationCompleted = callback;
			return this;
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x00055CE8 File Offset: 0x00053EE8
		public ValueAnimation<T> KeepAlive()
		{
			this.autoRecycle = false;
			return this;
		}

		// Token: 0x0400098E RID: 2446
		private const int k_DefaultDurationMs = 400;

		// Token: 0x0400098F RID: 2447
		private const int k_DefaultMaxPoolSize = 100;

		// Token: 0x04000990 RID: 2448
		private long m_StartTimeMs;

		// Token: 0x04000991 RID: 2449
		private int m_DurationMs;

		// Token: 0x04000997 RID: 2455
		private static ObjectPool<ValueAnimation<T>> sObjectPool = new ObjectPool<ValueAnimation<T>>(100);

		// Token: 0x0400099C RID: 2460
		private T _from;

		// Token: 0x0400099D RID: 2461
		private bool fromValueSet = false;
	}
}
