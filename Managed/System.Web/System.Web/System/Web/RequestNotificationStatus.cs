using System;

namespace System.Web
{
	/// <summary>Specifies the state of a notification in the request pipeline.</summary>
	// Token: 0x02000056 RID: 86
	public enum RequestNotificationStatus
	{
		/// <summary>This member supports the ASP.NET infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x04000E28 RID: 3624
		Continue,
		/// <summary>This member supports the ASP.NET infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x04000E29 RID: 3625
		Pending,
		/// <summary>This member supports the ASP.NET infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x04000E2A RID: 3626
		FinishRequest
	}
}
