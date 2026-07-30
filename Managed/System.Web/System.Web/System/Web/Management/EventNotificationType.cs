using System;

namespace System.Web.Management
{
	/// <summary>Specifies the type of event notification.</summary>
	// Token: 0x02000529 RID: 1321
	public enum EventNotificationType
	{
		/// <summary>The notification of an event is triggered on a regularly scheduled interval.</summary>
		// Token: 0x04001F62 RID: 8034
		Regular,
		/// <summary>Notification triggered by exceeding the urgent event threshold.</summary>
		// Token: 0x04001F63 RID: 8035
		Urgent,
		/// <summary>The notification of an event is triggered by a requested flush.</summary>
		// Token: 0x04001F64 RID: 8036
		Flush,
		/// <summary>Every event is treated as if a flush has occurred.</summary>
		// Token: 0x04001F65 RID: 8037
		Unbuffered
	}
}
