using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Unity;

namespace System.Web.SessionState
{
	/// <summary>Provides helper methods used by session-state modules and session-state store providers to manage session information for an ASP.NET application. This class cannot be inherited.</summary>
	// Token: 0x020004A8 RID: 1192
	public static class SessionStateUtility
	{
		/// <summary>Applies the session data to the context for the current request.</summary>
		/// <param name="context">The <see cref="T:System.Web.HttpContext" /> object to which to add the <see cref="T:System.Web.SessionState.HttpSessionState" /> object.</param>
		/// <param name="container">The <see cref="T:System.Web.SessionState.IHttpSessionState" /> implementation instance to add to the specified HTTP context.</param>
		/// <exception cref="T:System.Web.HttpException">An <see cref="T:System.Web.SessionState.HttpSessionState" /> object for the current session has already been added to the specified <paramref name="context" />.</exception>
		// Token: 0x0600360F RID: 13839 RVA: 0x0008E450 File Offset: 0x0008C650
		public static void AddHttpSessionStateToContext(HttpContext context, IHttpSessionState container)
		{
			if (context == null || container == null)
			{
				return;
			}
			if (context.Session != null)
			{
				throw new HttpException("An HttpSessionState object for the current session has already been added to the specified context.");
			}
			HttpSessionState httpSessionState = new HttpSessionState(container);
			context.SetSession(httpSessionState);
		}

		/// <summary>Retrieves session data from the context for the current request.</summary>
		/// <returns>An <see cref="T:System.Web.SessionState.IHttpSessionState" /> implementation instance populated with session data from the current request.</returns>
		/// <param name="context">The <see cref="T:System.Web.HttpContext" /> from which to retrieve session data.</param>
		// Token: 0x06003610 RID: 13840 RVA: 0x0008E488 File Offset: 0x0008C688
		public static IHttpSessionState GetHttpSessionStateFromContext(HttpContext context)
		{
			HttpSessionState session;
			if (context == null || (session = context.Session) == null)
			{
				return null;
			}
			return session.Container;
		}

		/// <summary>Gets a reference to the static objects collection for the specified context.</summary>
		/// <returns>An <see cref="T:System.Web.HttpStaticObjectsCollection" /> collection populated with the <see cref="P:System.Web.SessionState.HttpSessionState.StaticObjects" /> property value for the specified <see cref="T:System.Web.HttpContext" />.</returns>
		/// <param name="context">The <see cref="T:System.Web.HttpContext" /> from which to get the static objects collection.</param>
		// Token: 0x06003611 RID: 13841 RVA: 0x0008E4AC File Offset: 0x0008C6AC
		public static HttpStaticObjectsCollection GetSessionStaticObjects(HttpContext context)
		{
			HttpSessionState session;
			if (context == null || (session = context.Session) == null)
			{
				return null;
			}
			return session.Container.StaticObjects;
		}

		/// <summary>Executes the Session_OnEnd event defined in the Global.asax file for the ASP.NET application.</summary>
		/// <param name="session">The <see cref="T:System.Web.SessionState.IHttpSessionState" /> implementation instance for the session that has ended.</param>
		/// <param name="eventSource">The event source object to supply to the Session_OnEnd event.</param>
		/// <param name="eventArgs">The <see cref="T:System.EventArgs" /> object to supply to the Session_OnEnd event.</param>
		// Token: 0x06003612 RID: 13842 RVA: 0x0008E4D3 File Offset: 0x0008C6D3
		public static void RaiseSessionEnd(IHttpSessionState session, object eventSource, EventArgs eventArgs)
		{
			HttpApplicationFactory.InvokeSessionEnd(new HttpSessionState(session), eventSource, eventArgs);
		}

		/// <summary>Removes session data from the specified context.</summary>
		/// <param name="context">The <see cref="T:System.Web.HttpContext" /> from which to remove session data.</param>
		// Token: 0x06003613 RID: 13843 RVA: 0x0008E4E2 File Offset: 0x0008C6E2
		public static void RemoveHttpSessionStateFromContext(HttpContext context)
		{
			if (context == null)
			{
				return;
			}
			context.SetSession(null);
		}

		/// <summary>Gets or sets a serialization surrogate selector that is used for session serialization customization.</summary>
		/// <returns>A serialization surrogate selector.</returns>
		// Token: 0x170010FB RID: 4347
		// (get) Token: 0x06003614 RID: 13844 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06003615 RID: 13845 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public static ISurrogateSelector SerializationSurrogateSelector
		{
			[CompilerGenerated]
			[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[CompilerGenerated]
			[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x06003616 RID: 13846 RVA: 0x0008E4F0 File Offset: 0x0008C6F0
		public static bool IsSessionStateReadOnly(HttpContext context)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		// Token: 0x06003617 RID: 13847 RVA: 0x0008E50C File Offset: 0x0008C70C
		public static bool IsSessionStateRequired(HttpContext context)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}
	}
}
