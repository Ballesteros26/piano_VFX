using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Configuration;
using System.Web.Instrumentation;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.WebSockets;
using Unity;

namespace System.Web
{
	/// <summary>Encapsulates the HTTP intrinsic object that contains HTTP-specific information about an individual HTTP request.</summary>
	// Token: 0x0200008D RID: 141
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HttpContextWrapper : HttpContextBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpContextWrapper" /> class by using the specified context object.</summary>
		/// <param name="httpContext">The object that this wrapper class provides access to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="httpContext" /> is null.</exception>
		// Token: 0x060006B1 RID: 1713 RVA: 0x0000FB73 File Offset: 0x0000DD73
		public HttpContextWrapper(HttpContext httpContext)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			this.w = httpContext;
		}

		/// <summary>Gets an array of errors (if any) that accumulated when an HTTP request was being processed.</summary>
		/// <returns>An array of <see cref="T:System.Exception" /> objects for the current HTTP request, or null if no errors accumulated during the HTTP request processing.</returns>
		// Token: 0x1700028F RID: 655
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x0000FB90 File Offset: 0x0000DD90
		public override Exception[] AllErrors
		{
			get
			{
				return this.w.AllErrors;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpApplicationState" /> object for the current HTTP request.</summary>
		/// <returns>The state object for the current HTTP request.</returns>
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0000FB9D File Offset: 0x0000DD9D
		public override HttpApplicationStateBase Application
		{
			get
			{
				return new HttpApplicationStateWrapper(this.w.Application);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.HttpApplication" /> object for the current HTTP request.</summary>
		/// <returns>The object for the current HTTP request.</returns>
		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x0000FBAF File Offset: 0x0000DDAF
		// (set) Token: 0x060006B5 RID: 1717 RVA: 0x0000FBBC File Offset: 0x0000DDBC
		public override HttpApplication ApplicationInstance
		{
			get
			{
				return this.w.ApplicationInstance;
			}
			set
			{
				this.w.ApplicationInstance = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Caching.Cache" /> object for the current application domain.</summary>
		/// <returns>The cache object for the current application domain.</returns>
		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x0000FBCA File Offset: 0x0000DDCA
		public override Cache Cache
		{
			get
			{
				return this.w.Cache;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.IHttpHandler" /> object that represents the handler that is currently executing.</summary>
		/// <returns>An object that represents the handler that is currently executing.</returns>
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x0000FBD7 File Offset: 0x0000DDD7
		public override IHttpHandler CurrentHandler
		{
			get
			{
				return this.w.CurrentHandler;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.RequestNotification" /> value that indicates the current <see cref="T:System.Web.HttpApplication" /> event that is processing.</summary>
		/// <returns>One of the <see cref="T:System.Web.RequestNotification" /> values.</returns>
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x0000FBE4 File Offset: 0x0000DDE4
		public override RequestNotification CurrentNotification
		{
			get
			{
				return this.w.CurrentNotification;
			}
		}

		/// <summary>Gets the first error (if any) that accumulated when an HTTP request was being processed.</summary>
		/// <returns>The first exception for the current HTTP request, or null if no errors accumulated when the HTTP request was being processed. The default is null.</returns>
		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x0000FBF1 File Offset: 0x0000DDF1
		public override Exception Error
		{
			get
			{
				return this.w.Error;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.IHttpHandler" /> object that is responsible for processing the HTTP request.</summary>
		/// <returns>The object that is responsible for processing the HTTP request.</returns>
		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x0000FBFE File Offset: 0x0000DDFE
		// (set) Token: 0x060006BB RID: 1723 RVA: 0x0000FC0B File Offset: 0x0000DE0B
		public override IHttpHandler Handler
		{
			get
			{
				return this.w.Handler;
			}
			set
			{
				this.w.Handler = value;
			}
		}

		/// <summary>Gets a value that indicates whether custom errors are enabled for the current HTTP request.</summary>
		/// <returns>true if custom errors are enabled; otherwise, false.</returns>
		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x0000FC19 File Offset: 0x0000DE19
		public override bool IsCustomErrorEnabled
		{
			get
			{
				return this.w.IsCustomErrorEnabled;
			}
		}

		/// <summary>Gets a value that indicates whether the current HTTP request is in debug mode.</summary>
		/// <returns>true if the request is in debug mode; otherwise, false.</returns>
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x0000FC26 File Offset: 0x0000DE26
		public override bool IsDebuggingEnabled
		{
			get
			{
				return this.w.IsDebuggingEnabled;
			}
		}

		/// <summary>Gets a value that indicates whether an <see cref="T:System.Web.HttpApplication" /> event has finished processing.</summary>
		/// <returns>true if the event has finished processing; otherwise, false.</returns>
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x0000FC33 File Offset: 0x0000DE33
		public override bool IsPostNotification
		{
			get
			{
				return this.w.IsPostNotification;
			}
		}

		/// <summary>Gets a key/value collection that can be used to organize and share data between a module and a handler during an HTTP request.</summary>
		/// <returns>A key/value collection that provides access to an individual value in the collection by using a specified key.</returns>
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x0000FC40 File Offset: 0x0000DE40
		public override IDictionary Items
		{
			get
			{
				return this.w.Items;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.IHttpHandler" /> object for the parent handler.</summary>
		/// <returns>An <see cref="T:System.Web.IHttpHandler" /> object that represents the parent handler, or null if no parent handler was found.</returns>
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0000FC4D File Offset: 0x0000DE4D
		public override IHttpHandler PreviousHandler
		{
			get
			{
				return this.w.PreviousHandler;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Profile.ProfileBase" /> object for the current user profile.</summary>
		/// <returns>If profile properties are defined in the application configuration file and profiles are enabled for the application, an object that represents the current user profile; otherwise, null.</returns>
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x0000FC5A File Offset: 0x0000DE5A
		public override ProfileBase Profile
		{
			get
			{
				return this.w.Profile;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpRequestBase" /> object for the current HTTP request.</summary>
		/// <returns>The current HTTP request.</returns>
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x0000FC67 File Offset: 0x0000DE67
		public override HttpRequestBase Request
		{
			get
			{
				return new HttpRequestWrapper(this.w.Request);
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpResponseBase" /> object for the current HTTP response.</summary>
		/// <returns>The current HTTP response.</returns>
		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x0000FC79 File Offset: 0x0000DE79
		public override HttpResponseBase Response
		{
			get
			{
				return new HttpResponseWrapper(this.w.Response);
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpServerUtilityBase" /> object that provides methods that are used when Web requests are being processed.</summary>
		/// <returns>The server utility object for the current HTTP request.</returns>
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x0000FC8B File Offset: 0x0000DE8B
		public override HttpServerUtilityBase Server
		{
			get
			{
				return new HttpServerUtilityWrapper(this.w.Server);
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpSessionStateBase" /> object for the current HTTP request.</summary>
		/// <returns>The session-state object for the current HTTP request.</returns>
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0000FC9D File Offset: 0x0000DE9D
		public override HttpSessionStateBase Session
		{
			get
			{
				if (this.w.Session != null)
				{
					return new HttpSessionStateWrapper(this.w.Session);
				}
				return null;
			}
		}

		/// <summary>Gets or sets a value that specifies whether the <see cref="T:System.Web.Security.UrlAuthorizationModule" /> object should skip the authorization check for the current request.</summary>
		/// <returns>true if <see cref="T:System.Web.Security.UrlAuthorizationModule" /> should skip the authorization check; otherwise, false. The default is false.</returns>
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x0000FCBE File Offset: 0x0000DEBE
		// (set) Token: 0x060006C7 RID: 1735 RVA: 0x0000FCCB File Offset: 0x0000DECB
		public override bool SkipAuthorization
		{
			get
			{
				return this.w.SkipAuthorization;
			}
			set
			{
				this.w.SkipAuthorization = value;
			}
		}

		/// <summary>Gets the initial timestamp of the current HTTP request.</summary>
		/// <returns>The timestamp of the current HTTP request.</returns>
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x0000FCD9 File Offset: 0x0000DED9
		public override DateTime Timestamp
		{
			get
			{
				return this.w.Timestamp;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.TraceContext" /> object for the current HTTP response.</summary>
		/// <returns>The trace object for the current HTTP response.</returns>
		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x0000FCE6 File Offset: 0x0000DEE6
		public override TraceContext Trace
		{
			get
			{
				return this.w.Trace;
			}
		}

		/// <summary>Gets or sets security information for the current HTTP request.</summary>
		/// <returns>An object that contains security information for the current HTTP request.</returns>
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x0000FCF3 File Offset: 0x0000DEF3
		// (set) Token: 0x060006CB RID: 1739 RVA: 0x0000FD00 File Offset: 0x0000DF00
		public override IPrincipal User
		{
			get
			{
				return this.w.User;
			}
			set
			{
				this.w.User = value;
			}
		}

		/// <summary>Adds an exception to the exception collection for the current HTTP request.</summary>
		/// <param name="errorInfo">The exception to add to the exception collection.</param>
		// Token: 0x060006CC RID: 1740 RVA: 0x0000FD0E File Offset: 0x0000DF0E
		public override void AddError(Exception errorInfo)
		{
			this.w.AddError(errorInfo);
		}

		/// <summary>Clears all errors for the current HTTP request.</summary>
		// Token: 0x060006CD RID: 1741 RVA: 0x0000FD1C File Offset: 0x0000DF1C
		public override void ClearError()
		{
			this.w.ClearError();
		}

		/// <summary>Gets an application-level resource object based on the specified <see cref="P:System.Web.Compilation.ResourceExpressionFields.ClassKey" /> and <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" /> properties.</summary>
		/// <returns>The requested application-level resource object, or null if no matching resource object is found.</returns>
		/// <param name="classKey">A string that represents the <see cref="P:System.Web.Compilation.ResourceExpressionFields.ClassKey" /> property of the requested resource object.</param>
		/// <param name="resourceKey">A string that represents the <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" />   property of the requested resource object.</param>
		// Token: 0x060006CE RID: 1742 RVA: 0x0000FD29 File Offset: 0x0000DF29
		public override object GetGlobalResourceObject(string classKey, string resourceKey)
		{
			return HttpContext.GetGlobalResourceObject(classKey, resourceKey);
		}

		/// <summary>Gets an application-level resource object based on the specified <see cref="P:System.Web.Compilation.ResourceExpressionFields.ClassKey" /> and <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" /> properties, and on the <see cref="T:System.Globalization.CultureInfo" /> object.</summary>
		/// <returns>The requested application-level resource object, which is localized for the specified culture, or null if no matching resource object is found.</returns>
		/// <param name="classKey">A string that represents the <see cref="P:System.Web.Compilation.ResourceExpressionFields.ClassKey" /> property of the requested resource object.</param>
		/// <param name="resourceKey">A string that represents the <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" />   property of the requested resource object.</param>
		/// <param name="culture">A string that represents the <see cref="T:System.Globalization.CultureInfo" /> object of the requested resource.</param>
		// Token: 0x060006CF RID: 1743 RVA: 0x0000FD32 File Offset: 0x0000DF32
		public override object GetGlobalResourceObject(string classKey, string resourceKey, CultureInfo culture)
		{
			return HttpContext.GetGlobalResourceObject(classKey, resourceKey, culture);
		}

		/// <summary>Gets a page-level resource object based on the specified <see cref="P:System.Web.Compilation.ExpressionBuilderContext.VirtualPath" /> and <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" /> properties.</summary>
		/// <returns>The requested page-level resource object, or null if no matching resource object is found.</returns>
		/// <param name="virtualPath">A string that represents the <see cref="P:System.Web.Compilation.ExpressionBuilderContext.VirtualPath" /> property of the local resource object.</param>
		/// <param name="resourceKey">A string that represents the <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" />   property of the requested resource object.</param>
		// Token: 0x060006D0 RID: 1744 RVA: 0x0000FD3C File Offset: 0x0000DF3C
		public override object GetLocalResourceObject(string virtualPath, string resourceKey)
		{
			return HttpContext.GetLocalResourceObject(virtualPath, resourceKey);
		}

		/// <summary>Gets a page-level resource object based on the specified <see cref="P:System.Web.Compilation.ExpressionBuilderContext.VirtualPath" /> and <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" /> properties, and on the <see cref="T:System.Globalization.CultureInfo" /> object.</summary>
		/// <returns>The requested local resource object, which is localized for the specified culture, or null if no matching resource object is found.</returns>
		/// <param name="virtualPath">A string that represents the <see cref="P:System.Web.Compilation.ExpressionBuilderContext.VirtualPath" /> property of the local resource object.</param>
		/// <param name="resourceKey">A string that represents the <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" />   property of the requested resource object.</param>
		/// <param name="culture">A string that represents the <see cref="T:System.Globalization.CultureInfo" /> object of the requested resource object.</param>
		// Token: 0x060006D1 RID: 1745 RVA: 0x0000FD45 File Offset: 0x0000DF45
		public override object GetLocalResourceObject(string virtualPath, string resourceKey, CultureInfo culture)
		{
			return HttpContext.GetLocalResourceObject(virtualPath, resourceKey, culture);
		}

		/// <summary>Gets the specified configuration section of the current application's default configuration.</summary>
		/// <returns>The specified section, or null if the section does not exist.</returns>
		/// <param name="sectionName">The configuration section path (in XPath format) and the configuration element name.</param>
		// Token: 0x060006D2 RID: 1746 RVA: 0x0000FD4F File Offset: 0x0000DF4F
		public override object GetSection(string sectionName)
		{
			return this.w.GetSection(sectionName);
		}

		/// <summary>Returns an object for the current service type.</summary>
		/// <returns>The current service type, or null if no service is found.</returns>
		/// <param name="serviceType">The type of service to get.</param>
		// Token: 0x060006D3 RID: 1747 RVA: 0x0000FD5D File Offset: 0x0000DF5D
		public override object GetService(Type serviceType)
		{
			return ((IServiceProvider)this.w).GetService(serviceType);
		}

		/// <summary>Enables you to specify a handler for the request.</summary>
		/// <param name="handler">The object that should process the request.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Web.HttpContextWrapper.RemapHandler(System.Web.IHttpHandler)" /> method was called after the <see cref="E:System.Web.HttpApplication.MapRequestHandler" /> event occurred.</exception>
		// Token: 0x060006D4 RID: 1748 RVA: 0x0000FD6B File Offset: 0x0000DF6B
		public override void RemapHandler(IHttpHandler handler)
		{
			this.w.RemapHandler(handler);
		}

		/// <summary>Rewrites the URL by using the specified path.</summary>
		/// <param name="path">The replacement path.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.</exception>
		/// <exception cref="T:System.Web.HttpException">
		///   <paramref name="path" /> is not in the current application's root directory.</exception>
		// Token: 0x060006D5 RID: 1749 RVA: 0x0000FD79 File Offset: 0x0000DF79
		public override void RewritePath(string path)
		{
			this.w.RewritePath(path);
		}

		/// <summary>Rewrites the URL by using the specified path and a value that specifies whether the virtual path for server resources is modified.</summary>
		/// <param name="path">The path to rewrite to.</param>
		/// <param name="rebaseClientPath">true to reset the virtual path; false to keep the virtual path unchanged.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.</exception>
		/// <exception cref="T:System.Web.HttpException">
		///   <paramref name="path" /> is not in the current application's root directory.</exception>
		// Token: 0x060006D6 RID: 1750 RVA: 0x0000FD87 File Offset: 0x0000DF87
		public override void RewritePath(string path, bool rebaseClientPath)
		{
			this.w.RewritePath(path, rebaseClientPath);
		}

		/// <summary>Rewrites the URL by using the specified path, path information, and query string information.</summary>
		/// <param name="filePath">The replacement path.</param>
		/// <param name="pathInfo">Additional path information for a resource.</param>
		/// <param name="queryString">The request query string.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="filePath" /> parameter is null.</exception>
		/// <exception cref="T:System.Web.HttpException">The <paramref name="filePath" /> parameter is not in the current application's root directory.</exception>
		// Token: 0x060006D7 RID: 1751 RVA: 0x0000FD96 File Offset: 0x0000DF96
		public override void RewritePath(string filePath, string pathInfo, string queryString)
		{
			this.w.RewritePath(filePath, pathInfo, queryString);
		}

		/// <summary>Rewrites the URL by using the specified path, path information, query string information, and a value that specifies whether the client file path is set to the rewrite path.</summary>
		/// <param name="filePath">The replacement path.</param>
		/// <param name="pathInfo">Additional path information for a resource.</param>
		/// <param name="queryString">The request query string.</param>
		/// <param name="setClientFilePath">true to set the file path used for client resources to the value of the <paramref name="filePath" /> parameter; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="filePath" /> is null.</exception>
		/// <exception cref="T:System.Web.HttpException">
		///   <paramref name="filePath" /> is not in the current application's root directory.</exception>
		// Token: 0x060006D8 RID: 1752 RVA: 0x0000FDA6 File Offset: 0x0000DFA6
		public override void RewritePath(string filePath, string pathInfo, string queryString, bool setClientFilePath)
		{
			this.w.RewritePath(filePath, pathInfo, queryString, setClientFilePath);
		}

		/// <summary>Sets the type of session state behavior that is required in order to support an HTTP request.</summary>
		/// <param name="sessionStateBehavior">One of the enumeration values that specifies what type of session state behavior is required.</param>
		// Token: 0x060006D9 RID: 1753 RVA: 0x0000FDB8 File Offset: 0x0000DFB8
		public override void SetSessionStateBehavior(SessionStateBehavior sessionStateBehavior)
		{
			this.w.SetSessionStateBehavior(sessionStateBehavior);
		}

		/// <summary>Gets or sets a value that indicates whether asynchronous operations are allowed during parts of ASP.NET request processing when they are not expected.</summary>
		/// <returns>false if ASP.NET will throw an exception when the asynchronous API is used at a time when it is not expected; otherwise, true. The default value is false.</returns>
		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x0000FDC8 File Offset: 0x0000DFC8
		// (set) Token: 0x060006DB RID: 1755 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override bool AllowAsyncDuringSyncStages
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets an object that contains flags that pertain to asynchronous preload mode.</summary>
		/// <returns>An object that contains flags that pertain to asynchronous preload mode.Although this property can be set programmatically, changing the property value only has effect if the property is set before the ExecuteRequestHandler step in the ASP.NET request pipeline.</returns>
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x0000FDE4 File Offset: 0x0000DFE4
		// (set) Token: 0x060006DD RID: 1757 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override AsyncPreloadModeFlags AsyncPreloadMode
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return AsyncPreloadModeFlags.None;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a value that indicates whether the request is an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> request.</summary>
		/// <returns>true if the request is an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> request; otherwise, false.</returns>
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x0000FE00 File Offset: 0x0000E000
		public override bool IsWebSocketRequest
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a value that indicates whether the connection is upgrading from an HTTP connection to an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> connection.</summary>
		/// <returns>true if the connection is upgrading; otherwise, false.</returns>
		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x0000FE1C File Offset: 0x0000E01C
		public override bool IsWebSocketRequestUpgrading
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a reference to the page-instrumentation service instance for this request.</summary>
		/// <returns>The page-instrumentation service instance for this request.</returns>
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override PageInstrumentationService PageInstrumentation
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets a value that specifies whether the ASP.NET runtime should call <see cref="M:System.Threading.Thread.Abort" /> on the thread that is servicing this request when the request times out.</summary>
		/// <returns>true if <see cref="M:System.Threading.Thread.Abort" /> will be called when the thread times out; otherwise, false. The default is true.</returns>
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0000FE38 File Offset: 0x0000E038
		// (set) Token: 0x060006E2 RID: 1762 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override bool ThreadAbortOnTimeout
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets the negotiated protocol that was sent from the server to the client for an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> request.</summary>
		/// <returns>The negotiated protocol.</returns>
		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override string WebSocketNegotiatedProtocol
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the ordered list of protocols requested by the client.</summary>
		/// <returns>The requested protocols, or null if this is not an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> request or if no list is present.</returns>
		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x0000FAB7 File Offset: 0x0000DCB7
		public override IList<string> WebSocketRequestedProtocols
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Accepts an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> request using the specified user function.</summary>
		/// <param name="userFunc">The user function.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="userFunc" /> parameter is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The request is not an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> request.</exception>
		// Token: 0x060006E5 RID: 1765 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override void AcceptWebSocketRequest(Func<AspNetWebSocketContext, Task> userFunc)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Accepts an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> request using the specified user function and options object.</summary>
		/// <param name="userFunc">The user function.</param>
		/// <param name="options">The options object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="userFunc" /> parameter is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The request is not an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> request.</exception>
		// Token: 0x060006E6 RID: 1766 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override void AcceptWebSocketRequest(Func<AspNetWebSocketContext, Task> userFunc, AspNetWebSocketOptions options)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Raises a virtual event that occurs when the HTTP part of the request is ending.</summary>
		/// <returns>The subscription token.</returns>
		/// <param name="callback">The HTTP context object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="callback" /> parameter is null.</exception>
		// Token: 0x060006E7 RID: 1767 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override ISubscriptionToken AddOnRequestCompleted(Action<HttpContextBase> callback)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Enables an object's <see cref="M:System.IDisposable.Dispose" /> method to be called when the <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> connection part of this request is completed.</summary>
		/// <returns>The subscription token.</returns>
		/// <param name="target">The object whose <see cref="M:System.IDisposable.Dispose" /> method must be called when the <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> connection part of the request is completed.</param>
		// Token: 0x060006E8 RID: 1768 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override ISubscriptionToken DisposeOnPipelineCompleted(IDisposable target)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x04000F4E RID: 3918
		private HttpContext w;
	}
}
