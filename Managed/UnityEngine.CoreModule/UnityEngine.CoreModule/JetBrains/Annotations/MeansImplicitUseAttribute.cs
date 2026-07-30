using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000085 RID: 133
	[AttributeUsage(4, AllowMultiple = false, Inherited = true)]
	public sealed class MeansImplicitUseAttribute : Attribute
	{
		// Token: 0x060001B7 RID: 439 RVA: 0x000040E5 File Offset: 0x000022E5
		public MeansImplicitUseAttribute()
			: this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000040F1 File Offset: 0x000022F1
		public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags)
			: this(useKindFlags, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000040FD File Offset: 0x000022FD
		public MeansImplicitUseAttribute(ImplicitUseTargetFlags targetFlags)
			: this(ImplicitUseKindFlags.Default, targetFlags)
		{
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00004109 File Offset: 0x00002309
		public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
		{
			this.UseKindFlags = useKindFlags;
			this.TargetFlags = targetFlags;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00004123 File Offset: 0x00002323
		// (set) Token: 0x060001BC RID: 444 RVA: 0x0000412B File Offset: 0x0000232B
		[UsedImplicitly]
		public ImplicitUseKindFlags UseKindFlags { get; private set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00004134 File Offset: 0x00002334
		// (set) Token: 0x060001BE RID: 446 RVA: 0x0000413C File Offset: 0x0000233C
		[UsedImplicitly]
		public ImplicitUseTargetFlags TargetFlags { get; private set; }
	}
}
