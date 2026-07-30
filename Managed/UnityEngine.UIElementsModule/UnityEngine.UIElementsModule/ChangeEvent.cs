using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200012A RID: 298
	public class ChangeEvent<T> : EventBase<ChangeEvent<T>>, IChangeEvent
	{
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x00022A99 File Offset: 0x00020C99
		// (set) Token: 0x06000899 RID: 2201 RVA: 0x00022AA1 File Offset: 0x00020CA1
		public T previousValue { get; protected set; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x00022AAA File Offset: 0x00020CAA
		// (set) Token: 0x0600089B RID: 2203 RVA: 0x00022AB2 File Offset: 0x00020CB2
		public T newValue { get; protected set; }

		// Token: 0x0600089C RID: 2204 RVA: 0x00022ABB File Offset: 0x00020CBB
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00022ACC File Offset: 0x00020CCC
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown;
			this.previousValue = default(T);
			this.newValue = default(T);
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00022B04 File Offset: 0x00020D04
		public static ChangeEvent<T> GetPooled(T previousValue, T newValue)
		{
			ChangeEvent<T> pooled = EventBase<ChangeEvent<T>>.GetPooled();
			pooled.previousValue = previousValue;
			pooled.newValue = newValue;
			return pooled;
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00022B2D File Offset: 0x00020D2D
		public ChangeEvent()
		{
			this.LocalInit();
		}
	}
}
