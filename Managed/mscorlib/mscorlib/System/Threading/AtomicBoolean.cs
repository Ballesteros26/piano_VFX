using System;

namespace System.Threading
{
	// Token: 0x0200044D RID: 1101
	internal class AtomicBoolean
	{
		// Token: 0x060034B1 RID: 13489 RVA: 0x000C32D8 File Offset: 0x000C14D8
		public bool CompareAndExchange(bool expected, bool newVal)
		{
			int num = (newVal ? 1 : 0);
			int num2 = (expected ? 1 : 0);
			return Interlocked.CompareExchange(ref this.flag, num, num2) == num2;
		}

		// Token: 0x060034B2 RID: 13490 RVA: 0x000C3305 File Offset: 0x000C1505
		public static AtomicBoolean FromValue(bool value)
		{
			return new AtomicBoolean
			{
				Value = value
			};
		}

		// Token: 0x060034B3 RID: 13491 RVA: 0x000C3313 File Offset: 0x000C1513
		public bool TrySet()
		{
			return !this.Exchange(true);
		}

		// Token: 0x060034B4 RID: 13492 RVA: 0x000C331F File Offset: 0x000C151F
		public bool TryRelaxedSet()
		{
			return this.flag == 0 && !this.Exchange(true);
		}

		// Token: 0x060034B5 RID: 13493 RVA: 0x000C3338 File Offset: 0x000C1538
		public bool Exchange(bool newVal)
		{
			int num = (newVal ? 1 : 0);
			return Interlocked.Exchange(ref this.flag, num) == 1;
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x060034B6 RID: 13494 RVA: 0x000C335C File Offset: 0x000C155C
		// (set) Token: 0x060034B7 RID: 13495 RVA: 0x000C3367 File Offset: 0x000C1567
		public bool Value
		{
			get
			{
				return this.flag == 1;
			}
			set
			{
				this.Exchange(value);
			}
		}

		// Token: 0x060034B8 RID: 13496 RVA: 0x000C3371 File Offset: 0x000C1571
		public bool Equals(AtomicBoolean rhs)
		{
			return this.flag == rhs.flag;
		}

		// Token: 0x060034B9 RID: 13497 RVA: 0x000C3381 File Offset: 0x000C1581
		public override bool Equals(object rhs)
		{
			return rhs is AtomicBoolean && this.Equals((AtomicBoolean)rhs);
		}

		// Token: 0x060034BA RID: 13498 RVA: 0x000C3399 File Offset: 0x000C1599
		public override int GetHashCode()
		{
			return this.flag.GetHashCode();
		}

		// Token: 0x060034BB RID: 13499 RVA: 0x000C33A6 File Offset: 0x000C15A6
		public static explicit operator bool(AtomicBoolean rhs)
		{
			return rhs.Value;
		}

		// Token: 0x060034BC RID: 13500 RVA: 0x000C33AE File Offset: 0x000C15AE
		public static implicit operator AtomicBoolean(bool rhs)
		{
			return AtomicBoolean.FromValue(rhs);
		}

		// Token: 0x04001C25 RID: 7205
		private int flag;

		// Token: 0x04001C26 RID: 7206
		private const int UnSet = 0;

		// Token: 0x04001C27 RID: 7207
		private const int Set = 1;
	}
}
