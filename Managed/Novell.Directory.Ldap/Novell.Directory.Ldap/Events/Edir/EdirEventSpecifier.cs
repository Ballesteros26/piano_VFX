using System;

namespace Novell.Directory.Ldap.Events.Edir
{
	// Token: 0x020000B7 RID: 183
	public class EdirEventSpecifier
	{
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x00014AEA File Offset: 0x00012CEA
		public EdirEventType EventType
		{
			get
			{
				return this.event_type;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x00014AF2 File Offset: 0x00012CF2
		public EdirEventResultType EventResultType
		{
			get
			{
				return this.event_result_type;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x00014AFA File Offset: 0x00012CFA
		public string EventFilter
		{
			get
			{
				return this.event_filter;
			}
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00014B02 File Offset: 0x00012D02
		public EdirEventSpecifier(EdirEventType eventType, EdirEventResultType eventResultType)
			: this(eventType, eventResultType, null)
		{
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00014B0D File Offset: 0x00012D0D
		public EdirEventSpecifier(EdirEventType eventType, EdirEventResultType eventResultType, string filter)
		{
			this.event_type = eventType;
			this.event_result_type = eventResultType;
			this.event_filter = filter;
		}

		// Token: 0x04000429 RID: 1065
		private EdirEventType event_type;

		// Token: 0x0400042A RID: 1066
		private EdirEventResultType event_result_type;

		// Token: 0x0400042B RID: 1067
		private string event_filter;
	}
}
