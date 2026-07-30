using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Contains the parameters defining the flush buffer characteristics.</summary>
	// Token: 0x02000743 RID: 1859
	public sealed class WebEventBufferFlushInfo
	{
		// Token: 0x06004C8A RID: 19594 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal WebEventBufferFlushInfo()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the events collection in the current message.</summary>
		/// <returns>The <see cref="T:System.Web.Management.WebBaseEventCollection" /> events contained in the current message.</returns>
		// Token: 0x17001789 RID: 6025
		// (get) Token: 0x06004C8B RID: 19595 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebBaseEventCollection Events
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the number of events dropped since the last notification.</summary>
		/// <returns>The number of events dropped by the buffering mechanism since the last notification.</returns>
		// Token: 0x1700178A RID: 6026
		// (get) Token: 0x06004C8C RID: 19596 RVA: 0x000CAEFC File Offset: 0x000C90FC
		public int EventsDiscardedSinceLastNotification
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the number of events in the buffer.</summary>
		/// <returns>The number of events in the buffer. </returns>
		// Token: 0x1700178B RID: 6027
		// (get) Token: 0x06004C8D RID: 19597 RVA: 0x000CAF18 File Offset: 0x000C9118
		public int EventsInBuffer
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the date and the time of the last notification.</summary>
		/// <returns>The date and the time of the last notification.</returns>
		// Token: 0x1700178C RID: 6028
		// (get) Token: 0x06004C8E RID: 19598 RVA: 0x000CAF34 File Offset: 0x000C9134
		public DateTime LastNotificationUtc
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(DateTime);
			}
		}

		/// <summary>Gets the message sequence in the current notification.</summary>
		/// <returns>The number indicating the message sequence order in the current notification, beginning with an index of zero.</returns>
		// Token: 0x1700178D RID: 6029
		// (get) Token: 0x06004C8F RID: 19599 RVA: 0x000CAF50 File Offset: 0x000C9150
		public int NotificationSequence
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the current notification type.</summary>
		/// <returns>One of the <see cref="T:System.Web.Management.EventNotificationType" /> values.</returns>
		// Token: 0x1700178E RID: 6030
		// (get) Token: 0x06004C90 RID: 19600 RVA: 0x000CAF6C File Offset: 0x000C916C
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
