using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000084 RID: 132
	[AttributeUsage(32767, AllowMultiple = false, Inherited = true)]
	public sealed class UsedImplicitlyAttribute : Attribute
	{
		// Token: 0x060001AF RID: 431 RVA: 0x00004085 File Offset: 0x00002285
		public UsedImplicitlyAttribute()
			: this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00004091 File Offset: 0x00002291
		public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags)
			: this(useKindFlags, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000409D File Offset: 0x0000229D
		public UsedImplicitlyAttribute(ImplicitUseTargetFlags targetFlags)
			: this(ImplicitUseKindFlags.Default, targetFlags)
		{
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000040A9 File Offset: 0x000022A9
		public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
		{
			this.UseKindFlags = useKindFlags;
			this.TargetFlags = targetFlags;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x000040C3 File Offset: 0x000022C3
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x000040CB File Offset: 0x000022CB
		public ImplicitUseKindFlags UseKindFlags { get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x000040D4 File Offset: 0x000022D4
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x000040DC File Offset: 0x000022DC
		public ImplicitUseTargetFlags TargetFlags { get; private set; }
	}
}
