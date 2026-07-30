using System;
using System.Net.Mail;
using Unity;

namespace System.Web.Management
{
	/// <summary>Provides information to the <see cref="T:System.Web.Management.TemplatedMailWebEventProvider" /> object about the current event notification.</summary>
	// Token: 0x02000747 RID: 1863
	public sealed class MailEventNotificationInfo
	{
		// Token: 0x06004C9D RID: 19613 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal MailEventNotificationInfo()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a collection of events for the current message.</summary>
		/// <returns>A <see cref="T:System.Web.Management.WebBaseEventCollection" /> of events for the current message.</returns>
		// Token: 0x17001790 RID: 6032
		// (get) Token: 0x06004C9E RID: 19614 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebBaseEventCollection Events
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the number of events discarded by the buffer since the last notification.</summary>
		/// <returns>The number of events discarded by the buffer since the last notification.</returns>
		// Token: 0x17001791 RID: 6033
		// (get) Token: 0x06004C9F RID: 19615 RVA: 0x000CAFC0 File Offset: 0x000C91C0
		public int EventsDiscardedByBuffer
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the number of events that are discarded by the buffer because the buffer has exceeded the message limit for the current notification.</summary>
		/// <returns>The number of events discarded by the buffer because the buffer has exceeded the message limit for the current notification.</returns>
		// Token: 0x17001792 RID: 6034
		// (get) Token: 0x06004CA0 RID: 19616 RVA: 0x000CAFDC File Offset: 0x000C91DC
		public int EventsDiscardedDueToMessageLimit
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the number of events remaining in the buffer after the current notification.</summary>
		/// <returns>The number of events remaining in the buffer after the current notification.</returns>
		// Token: 0x17001793 RID: 6035
		// (get) Token: 0x06004CA1 RID: 19617 RVA: 0x000CAFF8 File Offset: 0x000C91F8
		public int EventsInBuffer
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the number of events being processed for the current notification.</summary>
		/// <returns>The number of events being processed for the current notification.</returns>
		// Token: 0x17001794 RID: 6036
		// (get) Token: 0x06004CA2 RID: 19618 RVA: 0x000CB014 File Offset: 0x000C9214
		public int EventsInNotification
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the number of events remaining in the buffer after the current notification.</summary>
		/// <returns>The number of events remaining in the buffer after the current notification.</returns>
		// Token: 0x17001795 RID: 6037
		// (get) Token: 0x06004CA3 RID: 19619 RVA: 0x000CB030 File Offset: 0x000C9230
		public int EventsRemaining
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the time of the previous notification.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object that specifies the time of the previous notification.</returns>
		// Token: 0x17001796 RID: 6038
		// (get) Token: 0x06004CA4 RID: 19620 RVA: 0x000CB04C File Offset: 0x000C924C
		public DateTime LastNotificationUtc
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(DateTime);
			}
		}

		/// <summary>Gets the e-mail message that will be sent as the current message.</summary>
		/// <returns>A <see cref="T:System.Web.Mail.MailMessage" /> that specifies the e-mail message to send.</returns>
		// Token: 0x17001797 RID: 6039
		// (get) Token: 0x06004CA5 RID: 19621 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public MailMessage Message
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the position of this message in the sequence for the current notification.</summary>
		/// <returns>The position of this message in the sequence for the current notification.</returns>
		// Token: 0x17001798 RID: 6040
		// (get) Token: 0x06004CA6 RID: 19622 RVA: 0x000CB068 File Offset: 0x000C9268
		public int MessageSequence
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the total number of messages in the current notification.</summary>
		/// <returns>The total number of messages in the current notification.</returns>
		// Token: 0x17001799 RID: 6041
		// (get) Token: 0x06004CA7 RID: 19623 RVA: 0x000CB084 File Offset: 0x000C9284
		public int MessagesInNotification
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the position of this notification within the total number of notifications made to date.</summary>
		/// <returns>The position of this notification within the total number of notifications made to date.</returns>
		// Token: 0x1700179A RID: 6042
		// (get) Token: 0x06004CA8 RID: 19624 RVA: 0x000CB0A0 File Offset: 0x000C92A0
		public int NotificationSequence
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the type of the current notification.</summary>
		/// <returns>The <see cref="T:System.Web.Management.EventNotificationType" /> for the current notification.</returns>
		// Token: 0x1700179B RID: 6043
		// (get) Token: 0x06004CA9 RID: 19625 RVA: 0x000CB0BC File Offset: 0x000C92BC
		public EventNotificationType NotificationType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return EventNotificationType.Regular;
			}
		}
	}
}
