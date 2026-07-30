using System;

namespace System.Web.SessionState
{
	/// <summary>Defines the contract that a custom session-state identifier manager must implement.</summary>
	// Token: 0x02000496 RID: 1174
	public interface ISessionIDManager
	{
		/// <summary>Creates a unique session identifier.</summary>
		/// <returns>A unique session identifier.</returns>
		/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> object that references server objects used to process HTTP requests (for example, the <see cref="P:System.Web.HttpContext.Request" /> and <see cref="P:System.Web.HttpContext.Response" /> properties). </param>
		// Token: 0x06003568 RID: 13672
		string CreateSessionID(HttpContext context);

		/// <summary>Gets the session identifier from the context of the current HTTP request.</summary>
		/// <returns>The current session identifier sent with the HTTP request.</returns>
		/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> object that references server objects used to process HTTP requests (for example, the <see cref="P:System.Web.HttpContext.Request" /> and <see cref="P:System.Web.HttpContext.Response" /> properties).</param>
		// Token: 0x06003569 RID: 13673
		string GetSessionID(HttpContext context);

		/// <summary>Initializes the <see cref="T:System.Web.SessionState.SessionIDManager" /> object.</summary>
		// Token: 0x0600356A RID: 13674
		void Initialize();

		/// <summary>Performs per-request initialization of the <see cref="T:System.Web.SessionState.SessionIDManager" /> object.</summary>
		/// <returns>true to indicate that the initialization performed a redirect; otherwise, false.</returns>
		/// <param name="context">The <see cref="T:System.Web.HttpContext" /> object that contains information about the current request.</param>
		/// <param name="suppressAutoDetectRedirect">true if the session-ID manager should redirect to determine cookie support; otherwise, false to suppress automatic redirection to determine cookie support.</param>
		/// <param name="supportSessionIDReissue">When this method returns, contains a Boolean that indicates whether the <see cref="T:System.Web.SessionState.ISessionIDManager" /> object supports issuing new session IDs when the original ID is out of date. This parameter is passed uninitialized. Session ID reuse is appropriate when the session-state ID is encoded on a URL and the potential exists for the URL to be shared or emailed.If a custom session-state implementation partitions cookies by virtual path, session state should also be supported.</param>
		// Token: 0x0600356B RID: 13675
		bool InitializeRequest(HttpContext context, bool suppressAutoDetectRedirect, out bool supportSessionIDReissue);

		/// <summary>Deletes the session identifier from the cookie or from the URL.</summary>
		/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> object that references server objects used to process HTTP requests (for example, the <see cref="P:System.Web.HttpContext.Request" /> and <see cref="P:System.Web.HttpContext.Response" /> properties).</param>
		// Token: 0x0600356C RID: 13676
		void RemoveSessionID(HttpContext context);

		/// <summary>Saves a newly created session identifier to the HTTP response.</summary>
		/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> object that references server objects used to process HTTP requests (for example, the <see cref="P:System.Web.HttpContext.Request" /> and <see cref="P:System.Web.HttpContext.Response" /> properties).</param>
		/// <param name="id">The session identifier. </param>
		/// <param name="redirected">When this method returns, contains a Boolean value that is true if the response is redirected to the current URL with the session identifier added to the URL; otherwise, false. </param>
		/// <param name="cookieAdded">When this method returns, contains a Boolean value that is true if a cookie has been added to the HTTP response; otherwise, false. </param>
		// Token: 0x0600356D RID: 13677
		void SaveSessionID(HttpContext context, string id, out bool redirected, out bool cookieAdded);

		/// <summary>Confirms that the supplied session identifier is valid.</summary>
		/// <returns>true if the session identifier is valid; otherwise, false.</returns>
		/// <param name="id">The session identifier to validate. </param>
		// Token: 0x0600356E RID: 13678
		bool Validate(string id);
	}
}
