using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000B10 RID: 2832
	internal struct SessionMask
	{
		// Token: 0x060065C0 RID: 26048 RVA: 0x0014E0A4 File Offset: 0x0014C2A4
		public SessionMask(SessionMask m)
		{
			this.m_mask = m.m_mask;
		}

		// Token: 0x060065C1 RID: 26049 RVA: 0x0014E0B2 File Offset: 0x0014C2B2
		public SessionMask(uint mask = 0U)
		{
			this.m_mask = mask & 15U;
		}

		// Token: 0x060065C2 RID: 26050 RVA: 0x0014E0BE File Offset: 0x0014C2BE
		public bool IsEqualOrSupersetOf(SessionMask m)
		{
			return (this.m_mask | m.m_mask) == this.m_mask;
		}

		// Token: 0x17001225 RID: 4645
		// (get) Token: 0x060065C3 RID: 26051 RVA: 0x0014E0D5 File Offset: 0x0014C2D5
		public static SessionMask All
		{
			get
			{
				return new SessionMask(15U);
			}
		}

		// Token: 0x060065C4 RID: 26052 RVA: 0x0014E0DE File Offset: 0x0014C2DE
		public static SessionMask FromId(int perEventSourceSessionId)
		{
			return new SessionMask(1U << perEventSourceSessionId);
		}

		// Token: 0x060065C5 RID: 26053 RVA: 0x0014E0EB File Offset: 0x0014C2EB
		public ulong ToEventKeywords()
		{
			return (ulong)this.m_mask << 44;
		}

		// Token: 0x060065C6 RID: 26054 RVA: 0x0014E0F7 File Offset: 0x0014C2F7
		public static SessionMask FromEventKeywords(ulong m)
		{
			return new SessionMask((uint)(m >> 44));
		}

		// Token: 0x17001226 RID: 4646
		public bool this[int perEventSourceSessionId]
		{
			get
			{
				return ((ulong)this.m_mask & (ulong)(1L << (perEventSourceSessionId & 31))) > 0UL;
			}
			set
			{
				if (value)
				{
					this.m_mask |= 1U << perEventSourceSessionId;
					return;
				}
				this.m_mask &= ~(1U << perEventSourceSessionId);
			}
		}

		// Token: 0x060065C9 RID: 26057 RVA: 0x0014E145 File Offset: 0x0014C345
		public static SessionMask operator |(SessionMask m1, SessionMask m2)
		{
			return new SessionMask(m1.m_mask | m2.m_mask);
		}

		// Token: 0x060065CA RID: 26058 RVA: 0x0014E159 File Offset: 0x0014C359
		public static SessionMask operator &(SessionMask m1, SessionMask m2)
		{
			return new SessionMask(m1.m_mask & m2.m_mask);
		}

		// Token: 0x060065CB RID: 26059 RVA: 0x0014E16D File Offset: 0x0014C36D
		public static SessionMask operator ^(SessionMask m1, SessionMask m2)
		{
			return new SessionMask(m1.m_mask ^ m2.m_mask);
		}

		// Token: 0x060065CC RID: 26060 RVA: 0x0014E181 File Offset: 0x0014C381
		public static SessionMask operator ~(SessionMask m)
		{
			return new SessionMask(15U & ~m.m_mask);
		}

		// Token: 0x060065CD RID: 26061 RVA: 0x0014E192 File Offset: 0x0014C392
		public static explicit operator ulong(SessionMask m)
		{
			return (ulong)m.m_mask;
		}

		// Token: 0x060065CE RID: 26062 RVA: 0x0014E19B File Offset: 0x0014C39B
		public static explicit operator uint(SessionMask m)
		{
			return m.m_mask;
		}

		// Token: 0x040032AD RID: 12973
		private uint m_mask;

		// Token: 0x040032AE RID: 12974
		internal const int SHIFT_SESSION_TO_KEYWORD = 44;

		// Token: 0x040032AF RID: 12975
		internal const uint MASK = 15U;

		// Token: 0x040032B0 RID: 12976
		internal const uint MAX = 4U;
	}
}
