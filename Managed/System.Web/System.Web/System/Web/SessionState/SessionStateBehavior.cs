using System;

namespace System.Web.SessionState
{
	/// <summary>Specifies the type of session support that is required in order to handle a request. </summary>
	// Token: 0x02000492 RID: 1170
	public enum SessionStateBehavior
	{
		/// <summary>The default ASP.NET logic is used to determine the session state behavior for the request. The default logic looks for the existence of marker session state interfaces on the <see cref="T:System.Web.IHttpHandler" />.</summary>
		// Token: 0x04001D47 RID: 7495
		Default,
		/// <summary>Full read-write session state behavior is enabled for the request. This setting overrides whatever session behavior would have been determined by inspecting the handler for the request.</summary>
		// Token: 0x04001D48 RID: 7496
		Required,
		/// <summary>Read-only session state is enabled for the request. This means that session state cannot be updated. This setting overrides whatever session state behavior would have been determined by inspecting the handler for the request.</summary>
		// Token: 0x04001D49 RID: 7497
		ReadOnly,
		/// <summary>Session state is not enabled for processing the request. This setting overrides whatever session behavior would have been determined by inspecting the handler for the request.</summary>
		// Token: 0x04001D4A RID: 7498
		Disabled
	}
}
