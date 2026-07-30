using System;
using System.Security.Permissions;
using System.Security.Principal;
using System.Web.Configuration;

namespace System.Web.Security
{
	/// <summary>Verifies that the user has permission to access the URL requested. This class cannot be inherited.</summary>
	// Token: 0x020004D4 RID: 1236
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class UrlAuthorizationModule : IHttpModule
	{
		/// <summary>Creates an instance of the <see cref="T:System.Web.Security.UrlAuthorizationModule" /> class.</summary>
		// Token: 0x06003837 RID: 14391 RVA: 0x00002050 File Offset: 0x00000250
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public UrlAuthorizationModule()
		{
		}

		/// <summary>Releases all resources, other than memory, used by the <see cref="T:System.Web.Security.UrlAuthorizationModule" />.</summary>
		// Token: 0x06003838 RID: 14392 RVA: 0x0000393A File Offset: 0x00001B3A
		public void Dispose()
		{
		}

		/// <summary>Initializes the <see cref="T:System.Web.Security.UrlAuthorizationModule" /> object.</summary>
		/// <param name="app">The current <see cref="T:System.Web.HttpApplication" /> instance. </param>
		// Token: 0x06003839 RID: 14393 RVA: 0x00097396 File Offset: 0x00095596
		public void Init(HttpApplication app)
		{
			app.AuthorizeRequest += this.OnAuthorizeRequest;
		}

		// Token: 0x0600383A RID: 14394 RVA: 0x000973AC File Offset: 0x000955AC
		private void OnAuthorizeRequest(object sender, EventArgs args)
		{
			HttpApplication httpApplication = (HttpApplication)sender;
			HttpContext context = httpApplication.Context;
			if (context == null || context.SkipAuthorization)
			{
				return;
			}
			HttpRequest request = context.Request;
			if (!((AuthorizationSection)WebConfigurationManager.GetSection("system.web/authorization", request.Path, context)).IsValidUser(context.User, request.HttpMethod))
			{
				HttpException ex = new HttpException(401, "Unauthorized");
				HttpResponse response = context.Response;
				response.StatusCode = 401;
				response.Write(ex.GetHtmlErrorMessage());
				httpApplication.CompleteRequest();
			}
		}

		/// <summary>Determines whether the user has access to the requested file.</summary>
		/// <returns>true if the current user can access the file; otherwise, false.</returns>
		/// <param name="virtualPath">The virtual path to the file.</param>
		/// <param name="user">An <see cref="T:System.Security.Principal.IPrincipal" /> object representing the current user.</param>
		/// <param name="verb">The HTTP verb used to make the request.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="virtualPath" /> is null.- or -<paramref name="user" /> is null.- or -<paramref name="verb" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="virtualPath" /> is outside of the application root path.</exception>
		// Token: 0x0600383B RID: 14395 RVA: 0x00097438 File Offset: 0x00095638
		public static bool CheckUrlAccessForPrincipal(string virtualPath, IPrincipal user, string verb)
		{
			AuthorizationSection authorizationSection = (AuthorizationSection)WebConfigurationManager.GetSection("system.web/authorization", virtualPath);
			return authorizationSection == null || authorizationSection.IsValidUser(user, verb);
		}

		// Token: 0x0600383C RID: 14396 RVA: 0x00097463 File Offset: 0x00095663
		internal static void ReportUrlAuthorizationFailure(HttpContext context, object webEventSource)
		{
			context.Response.StatusCode = 401;
			context.Response.Write(new HttpException(401, "Unauthorized").GetHtmlErrorMessage());
			context.ApplicationInstance.CompleteRequest();
		}
	}
}
