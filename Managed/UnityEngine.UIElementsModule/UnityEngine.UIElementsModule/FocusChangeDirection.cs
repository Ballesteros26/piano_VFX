using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200001E RID: 30
	public class FocusChangeDirection
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003F85 File Offset: 0x00002185
		public static FocusChangeDirection unspecified { get; } = new FocusChangeDirection(-1);

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003F8C File Offset: 0x0000218C
		public static FocusChangeDirection none { get; } = new FocusChangeDirection(0);

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00003F93 File Offset: 0x00002193
		protected static FocusChangeDirection lastValue { get; } = FocusChangeDirection.none;

		// Token: 0x0600009C RID: 156 RVA: 0x00003F9A File Offset: 0x0000219A
		protected FocusChangeDirection(int value)
		{
			this.m_Value = value;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003FAC File Offset: 0x000021AC
		public static implicit operator int(FocusChangeDirection fcd)
		{
			return (fcd != null) ? fcd.m_Value : 0;
		}

		// Token: 0x0400004D RID: 77
		private readonly int m_Value;
	}
}
