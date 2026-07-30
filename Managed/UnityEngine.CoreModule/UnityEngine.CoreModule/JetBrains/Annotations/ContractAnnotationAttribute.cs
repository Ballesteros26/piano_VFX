using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000080 RID: 128
	[AttributeUsage(64, AllowMultiple = true, Inherited = true)]
	public sealed class ContractAnnotationAttribute : Attribute
	{
		// Token: 0x060001A1 RID: 417 RVA: 0x00003FEC File Offset: 0x000021EC
		public ContractAnnotationAttribute([NotNull] string contract)
			: this(contract, false)
		{
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00003FF8 File Offset: 0x000021F8
		public ContractAnnotationAttribute([NotNull] string contract, bool forceFullStates)
		{
			this.Contract = contract;
			this.ForceFullStates = forceFullStates;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00004012 File Offset: 0x00002212
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x0000401A File Offset: 0x0000221A
		public string Contract { get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00004023 File Offset: 0x00002223
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x0000402B File Offset: 0x0000222B
		public bool ForceFullStates { get; private set; }
	}
}
