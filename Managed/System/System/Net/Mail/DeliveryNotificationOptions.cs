using System;

namespace System.Net.Mail
{
	/// <summary>Describes the delivery notification options for e-mail.</summary>
	// Token: 0x0200057B RID: 1403
	[Flags]
	public enum DeliveryNotificationOptions
	{
		/// <summary>No notification information will be sent. The mail server will utilize its configured behavior to determine whether it should generate a delivery notification.</summary>
		// Token: 0x04002461 RID: 9313
		None = 0,
		/// <summary>Notify if the delivery is successful.</summary>
		// Token: 0x04002462 RID: 9314
		OnSuccess = 1,
		/// <summary>Notify if the delivery is unsuccessful.</summary>
		// Token: 0x04002463 RID: 9315
		OnFailure = 2,
		/// <summary>Notify if the delivery is delayed.</summary>
		// Token: 0x04002464 RID: 9316
		Delay = 4,
		/// <summary>A notification should not be generated under any circumstances.</summary>
		// Token: 0x04002465 RID: 9317
		Never = 134217728
	}
}
