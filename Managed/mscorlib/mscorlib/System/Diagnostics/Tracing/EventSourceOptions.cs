using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AA9 RID: 2729
	public struct EventSourceOptions
	{
		// Token: 0x170011D0 RID: 4560
		// (get) Token: 0x06006325 RID: 25381 RVA: 0x00142CDE File Offset: 0x00140EDE
		// (set) Token: 0x06006326 RID: 25382 RVA: 0x00142CE6 File Offset: 0x00140EE6
		public EventLevel Level
		{
			get
			{
				return (EventLevel)this.level;
			}
			set
			{
				this.level = checked((byte)value);
				this.valuesSet |= 4;
			}
		}

		// Token: 0x170011D1 RID: 4561
		// (get) Token: 0x06006327 RID: 25383 RVA: 0x00142CFF File Offset: 0x00140EFF
		// (set) Token: 0x06006328 RID: 25384 RVA: 0x00142D07 File Offset: 0x00140F07
		public EventOpcode Opcode
		{
			get
			{
				return (EventOpcode)this.opcode;
			}
			set
			{
				this.opcode = checked((byte)value);
				this.valuesSet |= 8;
			}
		}

		// Token: 0x170011D2 RID: 4562
		// (get) Token: 0x06006329 RID: 25385 RVA: 0x00142D20 File Offset: 0x00140F20
		internal bool IsOpcodeSet
		{
			get
			{
				return (this.valuesSet & 8) > 0;
			}
		}

		// Token: 0x170011D3 RID: 4563
		// (get) Token: 0x0600632A RID: 25386 RVA: 0x00142D2D File Offset: 0x00140F2D
		// (set) Token: 0x0600632B RID: 25387 RVA: 0x00142D35 File Offset: 0x00140F35
		public EventKeywords Keywords
		{
			get
			{
				return this.keywords;
			}
			set
			{
				this.keywords = value;
				this.valuesSet |= 1;
			}
		}

		// Token: 0x170011D4 RID: 4564
		// (get) Token: 0x0600632C RID: 25388 RVA: 0x00142D4D File Offset: 0x00140F4D
		// (set) Token: 0x0600632D RID: 25389 RVA: 0x00142D55 File Offset: 0x00140F55
		public EventTags Tags
		{
			get
			{
				return this.tags;
			}
			set
			{
				this.tags = value;
				this.valuesSet |= 2;
			}
		}

		// Token: 0x170011D5 RID: 4565
		// (get) Token: 0x0600632E RID: 25390 RVA: 0x00142D6D File Offset: 0x00140F6D
		// (set) Token: 0x0600632F RID: 25391 RVA: 0x00142D75 File Offset: 0x00140F75
		public EventActivityOptions ActivityOptions
		{
			get
			{
				return this.activityOptions;
			}
			set
			{
				this.activityOptions = value;
				this.valuesSet |= 16;
			}
		}

		// Token: 0x04003162 RID: 12642
		internal EventKeywords keywords;

		// Token: 0x04003163 RID: 12643
		internal EventTags tags;

		// Token: 0x04003164 RID: 12644
		internal EventActivityOptions activityOptions;

		// Token: 0x04003165 RID: 12645
		internal byte level;

		// Token: 0x04003166 RID: 12646
		internal byte opcode;

		// Token: 0x04003167 RID: 12647
		internal byte valuesSet;

		// Token: 0x04003168 RID: 12648
		internal const byte keywordsSet = 1;

		// Token: 0x04003169 RID: 12649
		internal const byte tagsSet = 2;

		// Token: 0x0400316A RID: 12650
		internal const byte levelSet = 4;

		// Token: 0x0400316B RID: 12651
		internal const byte opcodeSet = 8;

		// Token: 0x0400316C RID: 12652
		internal const byte activityOptionsSet = 16;
	}
}
