using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200043A RID: 1082
	internal struct InterlockedGate
	{
		// Token: 0x0600209A RID: 8346 RVA: 0x0007EF8C File Offset: 0x0007D18C
		internal void Reset()
		{
			this.m_State = 0;
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x0007EF98 File Offset: 0x0007D198
		internal bool Trigger(bool exclusive)
		{
			int num = Interlocked.CompareExchange(ref this.m_State, 2, 0);
			if (exclusive && (num == 1 || num == 2))
			{
				throw new InternalException();
			}
			return num == 0;
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x0007EFC8 File Offset: 0x0007D1C8
		internal bool StartTriggering(bool exclusive)
		{
			int num = Interlocked.CompareExchange(ref this.m_State, 1, 0);
			if (exclusive && (num == 1 || num == 2))
			{
				throw new InternalException();
			}
			return num == 0;
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x0007EFF8 File Offset: 0x0007D1F8
		internal void FinishTriggering()
		{
			if (Interlocked.CompareExchange(ref this.m_State, 2, 1) != 1)
			{
				throw new InternalException();
			}
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x0007F010 File Offset: 0x0007D210
		internal bool StartSignaling(bool exclusive)
		{
			int num = Interlocked.CompareExchange(ref this.m_State, 3, 2);
			if (exclusive && (num == 3 || num == 4))
			{
				throw new InternalException();
			}
			return num == 2;
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x0007F040 File Offset: 0x0007D240
		internal void FinishSignaling()
		{
			if (Interlocked.CompareExchange(ref this.m_State, 4, 3) != 3)
			{
				throw new InternalException();
			}
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x0007F058 File Offset: 0x0007D258
		internal bool Complete()
		{
			return Interlocked.CompareExchange(ref this.m_State, 5, 4) == 4;
		}

		// Token: 0x04001CBB RID: 7355
		private int m_State;

		// Token: 0x04001CBC RID: 7356
		internal const int Open = 0;

		// Token: 0x04001CBD RID: 7357
		internal const int Triggering = 1;

		// Token: 0x04001CBE RID: 7358
		internal const int Triggered = 2;

		// Token: 0x04001CBF RID: 7359
		internal const int Signaling = 3;

		// Token: 0x04001CC0 RID: 7360
		internal const int Signaled = 4;

		// Token: 0x04001CC1 RID: 7361
		internal const int Completed = 5;
	}
}
