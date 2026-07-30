using System;

namespace System.Web
{
	/// <summary>Indicates when events and other life-cycle events occur while a <see cref="T:System.Web.HttpApplication" /> request is being processed.</summary>
	// Token: 0x02000055 RID: 85
	[Flags]
	public enum RequestNotification
	{
		/// <summary>Indicates that the <see cref="E:System.Web.HttpApplication.BeginRequest" /> event was raised for the request and is processing.</summary>
		// Token: 0x04000E1A RID: 3610
		BeginRequest = 1,
		/// <summary>Indicates that the <see cref="E:System.Web.HttpApplication.AuthenticateRequest" /> event was raised for the request and is processing.</summary>
		// Token: 0x04000E1B RID: 3611
		AuthenticateRequest = 2,
		/// <summary>Indicates that the <see cref="E:System.Web.HttpApplication.AuthorizeRequest" /> event was raised for the request and is processing.</summary>
		// Token: 0x04000E1C RID: 3612
		AuthorizeRequest = 4,
		/// <summary>Indicates that the <see cref="E:System.Web.HttpApplication.ResolveRequestCache" /> event was raised for the request and is processing.</summary>
		// Token: 0x04000E1D RID: 3613
		ResolveRequestCache = 8,
		/// <summary>Indicates that the <see cref="E:System.Web.HttpApplication.MapRequestHandler" /> event was raised for the request and is processing.</summary>
		// Token: 0x04000E1E RID: 3614
		MapRequestHandler = 16,
		/// <summary>Indicates that the <see cref="E:System.Web.HttpApplication.AcquireRequestState" /> event was raised for the request and is processing.</summary>
		// Token: 0x04000E1F RID: 3615
		AcquireRequestState = 32,
		/// <summary>Indicates a point in the application life cycle just before the handler that processes the request is mapped.</summary>
		// Token: 0x04000E20 RID: 3616
		PreExecuteRequestHandler = 64,
		/// <summary>Indicates that the handler that is mapped to the requested resource is being invoked to process the request.</summary>
		// Token: 0x04000E21 RID: 3617
		ExecuteRequestHandler = 128,
		/// <summary>Indicates that the <see cref="E:System.Web.HttpApplication.ReleaseRequestState" /> event was raised for the request and is processing.</summary>
		// Token: 0x04000E22 RID: 3618
		ReleaseRequestState = 256,
		/// <summary>Indicates that the <see cref="E:System.Web.HttpApplication.UpdateRequestCache" /> event was raised for the request and is processing.</summary>
		// Token: 0x04000E23 RID: 3619
		UpdateRequestCache = 512,
		/// <summary>Indicates that the <see cref="E:System.Web.HttpApplication.LogRequest" /> event was raised for the request and is processing.</summary>
		// Token: 0x04000E24 RID: 3620
		LogRequest = 1024,
		/// <summary>Indicates that the <see cref="E:System.Web.HttpApplication.EndRequest" /> event was raised for the request and is processing.</summary>
		// Token: 0x04000E25 RID: 3621
		EndRequest = 2048,
		/// <summary>Indicates that processing of the request is complete and that the response is being sent.</summary>
		// Token: 0x04000E26 RID: 3622
		SendResponse = 536870912
	}
}
