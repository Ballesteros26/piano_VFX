using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Specifies additional event schema information for an event.</summary>
	// Token: 0x02000B06 RID: 2822
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class EventAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Tracing.EventAttribute" /> class with the specified event identifier.</summary>
		/// <param name="eventId">The event identifier for the event.</param>
		// Token: 0x06006584 RID: 25988 RVA: 0x0014D56F File Offset: 0x0014B76F
		public EventAttribute(int eventId)
		{
			this.EventId = eventId;
			this.Level = EventLevel.Informational;
			this.m_opcodeSet = false;
		}

		/// <summary>Gets or sets the identifier for the event.</summary>
		/// <returns>The event identifier.</returns>
		// Token: 0x17001218 RID: 4632
		// (get) Token: 0x06006585 RID: 25989 RVA: 0x0014D58C File Offset: 0x0014B78C
		// (set) Token: 0x06006586 RID: 25990 RVA: 0x0014D594 File Offset: 0x0014B794
		public int EventId { get; private set; }

		/// <summary>Gets or sets the level for the event.</summary>
		/// <returns>One of the enumeration values that specifies the level for the event.</returns>
		// Token: 0x17001219 RID: 4633
		// (get) Token: 0x06006587 RID: 25991 RVA: 0x0014D59D File Offset: 0x0014B79D
		// (set) Token: 0x06006588 RID: 25992 RVA: 0x0014D5A5 File Offset: 0x0014B7A5
		public EventLevel Level { get; set; }

		/// <summary>Gets or sets the keywords for the event.</summary>
		/// <returns>A bitwise combination of the enumeration values.</returns>
		// Token: 0x1700121A RID: 4634
		// (get) Token: 0x06006589 RID: 25993 RVA: 0x0014D5AE File Offset: 0x0014B7AE
		// (set) Token: 0x0600658A RID: 25994 RVA: 0x0014D5B6 File Offset: 0x0014B7B6
		public EventKeywords Keywords { get; set; }

		/// <summary>Gets or sets the operation code for the event.</summary>
		/// <returns>One of the enumeration values that specifies the operation code.</returns>
		// Token: 0x1700121B RID: 4635
		// (get) Token: 0x0600658B RID: 25995 RVA: 0x0014D5BF File Offset: 0x0014B7BF
		// (set) Token: 0x0600658C RID: 25996 RVA: 0x0014D5C7 File Offset: 0x0014B7C7
		public EventOpcode Opcode
		{
			get
			{
				return this.m_opcode;
			}
			set
			{
				this.m_opcode = value;
				this.m_opcodeSet = true;
			}
		}

		// Token: 0x1700121C RID: 4636
		// (get) Token: 0x0600658D RID: 25997 RVA: 0x0014D5D7 File Offset: 0x0014B7D7
		internal bool IsOpcodeSet
		{
			get
			{
				return this.m_opcodeSet;
			}
		}

		/// <summary>Gets or sets the task for the event.</summary>
		/// <returns>The task for the event.</returns>
		// Token: 0x1700121D RID: 4637
		// (get) Token: 0x0600658E RID: 25998 RVA: 0x0014D5DF File Offset: 0x0014B7DF
		// (set) Token: 0x0600658F RID: 25999 RVA: 0x0014D5E7 File Offset: 0x0014B7E7
		public EventTask Task { get; set; }

		// Token: 0x1700121E RID: 4638
		// (get) Token: 0x06006590 RID: 26000 RVA: 0x0014D5F0 File Offset: 0x0014B7F0
		// (set) Token: 0x06006591 RID: 26001 RVA: 0x0014D5F8 File Offset: 0x0014B7F8
		public EventChannel Channel { get; set; }

		/// <summary>Gets or sets the version of the event.</summary>
		/// <returns>The version of the event.</returns>
		// Token: 0x1700121F RID: 4639
		// (get) Token: 0x06006592 RID: 26002 RVA: 0x0014D601 File Offset: 0x0014B801
		// (set) Token: 0x06006593 RID: 26003 RVA: 0x0014D609 File Offset: 0x0014B809
		public byte Version { get; set; }

		/// <summary>Gets or sets the message for the event.</summary>
		/// <returns>The message for the event.</returns>
		// Token: 0x17001220 RID: 4640
		// (get) Token: 0x06006594 RID: 26004 RVA: 0x0014D612 File Offset: 0x0014B812
		// (set) Token: 0x06006595 RID: 26005 RVA: 0x0014D61A File Offset: 0x0014B81A
		public string Message { get; set; }

		// Token: 0x17001221 RID: 4641
		// (get) Token: 0x06006596 RID: 26006 RVA: 0x0014D623 File Offset: 0x0014B823
		// (set) Token: 0x06006597 RID: 26007 RVA: 0x0014D62B File Offset: 0x0014B82B
		public EventTags Tags { get; set; }

		// Token: 0x17001222 RID: 4642
		// (get) Token: 0x06006598 RID: 26008 RVA: 0x0014D634 File Offset: 0x0014B834
		// (set) Token: 0x06006599 RID: 26009 RVA: 0x0014D63C File Offset: 0x0014B83C
		public EventActivityOptions ActivityOptions { get; set; }

		// Token: 0x0400328C RID: 12940
		private EventOpcode m_opcode;

		// Token: 0x0400328D RID: 12941
		private bool m_opcodeSet;
	}
}
