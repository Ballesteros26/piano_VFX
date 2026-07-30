using System;

namespace System.Threading
{
	// Token: 0x0200044C RID: 1100
	internal struct AtomicBooleanValue
	{
		// Token: 0x060034A5 RID: 13477 RVA: 0x000C31E8 File Offset: 0x000C13E8
		public bool CompareAndExchange(bool expected, bool newVal)
		{
			int num = (newVal ? 1 : 0);
			int num2 = (expected ? 1 : 0);
			return Interlocked.CompareExchange(ref this.flag, num, num2) == num2;
		}

		// Token: 0x060034A6 RID: 13478 RVA: 0x000C3218 File Offset: 0x000C1418
		public static AtomicBooleanValue FromValue(bool value)
		{
			return new AtomicBooleanValue
			{
				Value = value
			};
		}

		// Token: 0x060034A7 RID: 13479 RVA: 0x000C3236 File Offset: 0x000C1436
		public bool TrySet()
		{
			return !this.Exchange(true);
		}

		// Token: 0x060034A8 RID: 13480 RVA: 0x000C3242 File Offset: 0x000C1442
		public bool TryRelaxedSet()
		{
			return this.flag == 0 && !this.Exchange(true);
		}

		// Token: 0x060034A9 RID: 13481 RVA: 0x000C3258 File Offset: 0x000C1458
		public bool Exchange(bool newVal)
		{
			int num = (newVal ? 1 : 0);
			return Interlocked.Exchange(ref this.flag, num) == 1;
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x060034AA RID: 13482 RVA: 0x000C327C File Offset: 0x000C147C
		// (set) Token: 0x060034AB RID: 13483 RVA: 0x000C3287 File Offset: 0x000C1487
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

		// Token: 0x060034AC RID: 13484 RVA: 0x000C3291 File Offset: 0x000C1491
		public bool Equals(AtomicBooleanValue rhs)
		{
			return this.flag == rhs.flag;
		}

		// Token: 0x060034AD RID: 13485 RVA: 0x000C32A1 File Offset: 0x000C14A1
		public override bool Equals(object rhs)
		{
			return rhs is AtomicBooleanValue && this.Equals((AtomicBooleanValue)rhs);
		}

		// Token: 0x060034AE RID: 13486 RVA: 0x000C32B9 File Offset: 0x000C14B9
		public override int GetHashCode()
		{
			return this.flag.GetHashCode();
		}

		// Token: 0x060034AF RID: 13487 RVA: 0x000C32C6 File Offset: 0x000C14C6
		public static explicit operator bool(AtomicBooleanValue rhs)
		{
			return rhs.Value;
		}

		// Token: 0x060034B0 RID: 13488 RVA: 0x000C32CF File Offset: 0x000C14CF
		public static implicit operator AtomicBooleanValue(bool rhs)
		{
			return AtomicBooleanValue.FromValue(rhs);
		}

		// Token: 0x04001C22 RID: 7202
		private int flag;

		// Token: 0x04001C23 RID: 7203
		private const int UnSet = 0;

		// Token: 0x04001C24 RID: 7204
		private const int Set = 1;
	}
}
