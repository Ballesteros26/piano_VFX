using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AA0 RID: 2720
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
	public class EventDataAttribute : Attribute
	{
		// Token: 0x170011BE RID: 4542
		// (get) Token: 0x060062E5 RID: 25317 RVA: 0x001426B0 File Offset: 0x001408B0
		// (set) Token: 0x060062E6 RID: 25318 RVA: 0x001426B8 File Offset: 0x001408B8
		public string Name { get; set; }

		// Token: 0x170011BF RID: 4543
		// (get) Token: 0x060062E7 RID: 25319 RVA: 0x001426C1 File Offset: 0x001408C1
		// (set) Token: 0x060062E8 RID: 25320 RVA: 0x001426C9 File Offset: 0x001408C9
		internal EventLevel Level
		{
			get
			{
				return this.level;
			}
			set
			{
				this.level = value;
			}
		}

		// Token: 0x170011C0 RID: 4544
		// (get) Token: 0x060062E9 RID: 25321 RVA: 0x001426D2 File Offset: 0x001408D2
		// (set) Token: 0x060062EA RID: 25322 RVA: 0x001426DA File Offset: 0x001408DA
		internal EventOpcode Opcode
		{
			get
			{
				return this.opcode;
			}
			set
			{
				this.opcode = value;
			}
		}

		// Token: 0x170011C1 RID: 4545
		// (get) Token: 0x060062EB RID: 25323 RVA: 0x001426E3 File Offset: 0x001408E3
		// (set) Token: 0x060062EC RID: 25324 RVA: 0x001426EB File Offset: 0x001408EB
		internal EventKeywords Keywords { get; set; }

		// Token: 0x170011C2 RID: 4546
		// (get) Token: 0x060062ED RID: 25325 RVA: 0x001426F4 File Offset: 0x001408F4
		// (set) Token: 0x060062EE RID: 25326 RVA: 0x001426FC File Offset: 0x001408FC
		internal EventTags Tags { get; set; }

		// Token: 0x04003141 RID: 12609
		private EventLevel level = (EventLevel)(-1);

		// Token: 0x04003142 RID: 12610
		private EventOpcode opcode = (EventOpcode)(-1);
	}
}
