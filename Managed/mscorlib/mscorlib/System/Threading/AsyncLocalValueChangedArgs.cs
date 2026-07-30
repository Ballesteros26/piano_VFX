using System;

namespace System.Threading
{
	// Token: 0x0200046D RID: 1133
	public struct AsyncLocalValueChangedArgs<T>
	{
		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x060035BD RID: 13757 RVA: 0x000C6CCE File Offset: 0x000C4ECE
		// (set) Token: 0x060035BE RID: 13758 RVA: 0x000C6CD6 File Offset: 0x000C4ED6
		public T PreviousValue { get; private set; }

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x060035BF RID: 13759 RVA: 0x000C6CDF File Offset: 0x000C4EDF
		// (set) Token: 0x060035C0 RID: 13760 RVA: 0x000C6CE7 File Offset: 0x000C4EE7
		public T CurrentValue { get; private set; }

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x060035C1 RID: 13761 RVA: 0x000C6CF0 File Offset: 0x000C4EF0
		// (set) Token: 0x060035C2 RID: 13762 RVA: 0x000C6CF8 File Offset: 0x000C4EF8
		public bool ThreadContextChanged { get; private set; }

		// Token: 0x060035C3 RID: 13763 RVA: 0x000C6D01 File Offset: 0x000C4F01
		internal AsyncLocalValueChangedArgs(T previousValue, T currentValue, bool contextChanged)
		{
			this = default(AsyncLocalValueChangedArgs<T>);
			this.PreviousValue = previousValue;
			this.CurrentValue = currentValue;
			this.ThreadContextChanged = contextChanged;
		}
	}
}
