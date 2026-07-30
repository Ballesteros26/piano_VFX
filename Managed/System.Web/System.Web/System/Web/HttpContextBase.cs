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
	/// <summary>Serves as the base class for classes that contain HTTP-specific information about an individual HTTP request.</summary>
	// Token: 0x0200008C RID: 140
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class HttpContextBase : IServiceProvider
	{
		// Token: 0x06000678 RID: 1656 RVA: 0x00003A1F File Offset: 0x00001C1F
		private void NotImplemented()
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, gets an array of errors (if any) that accumulated when an HTTP request was being processed.</summary>
		/// <returns>An array of <see cref="T:System.Exception" /> objects for the current HTTP request, or null if no errors accumulated during the HTTP request processing.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x0000FABF File Offset: 0x0000DCBF
		public virtual Exception[] AllErrors
		{
			get
			{
				this.NotImplemented();
				return null;
			}
		}

		/// <summary>When overridden in a derived class, gets the <see cref="T:System.Web.HttpApplicationState" /> object for the current HTTP request.</summary>
		/// <returns>The application state object for the current HTTP request.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x0000FABF File Offset: 0x0000DCBF
		public virtual HttpApplicationStateBase Application
		{
			get
			{
				this.NotImplemented();
				return null;
			}
		}

		/// <summary>When overridden in a derived class, gets or sets the <see cref="T:System.Web.HttpApplication" /> object for the current HTTP request.</summary>
		/// <returns>The object for the current HTTP request.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x0000FABF File Offset: 0x0000DCBF
		// (set) Token: 0x0600067C RID: 1660 RVA: 0x0000FAC8 File Offset: 0x0000DCC8
		public virtual HttpApplication ApplicationInstance
		{
			get
			{
				this.NotImplemented();
				return null;
			}
			set
			{
				this.NotImplemented();
			}
		}

		/// <summary>When overridden in a derived class, gets the <see cref="T:System.Web.Caching.Cache" /> object for the current application domain.</summary>
		/// <returns>The cache for the current application domain.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x0000FABF File Offset: 0x0000DCBF
		public virtual Cache Cache
		{
			get
			{
				this.NotImplemented();
				return null;
			}
		}

		/// <summary>When overridden in a derived class, gets the <see cref="T:System.Web.IHttpHandler" /> object that represents the handler that is currently executing.</summary>
		/// <returns>An object that represents the currently executing handler. </returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x0000FABF File Offset: 0x0000DCBF
		public virtual IHttpHandler CurrentHandler
		{
			get
			{
				this.NotImplemented();
				return null;
			}
		}

		/// <summary>When overridden in a derived class, gets a <see cref="T:System.Web.RequestNotification" /> value that indicates the <see cref="T:System.Web.HttpApplication" /> event that is currently processing. </summary>
		/// <returns>One of the <see cref="T:System.Web.RequestNotification" /> values.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x0000FAD0 File Offset: 0x0000DCD0
		public virtual RequestNotification CurrentNotification
		{
			get
			{
				this.NotImplemented();
				return (RequestNotification)0;
			}
		}

		/// <summary>When overridden in a derived class, gets the first error (if any) that accumulated when an HTTP request was being processed.</summary>
		/// <returns>The first exception for the current HTTP request/response process, or null if no errors accumulated during the HTTP request processing.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x0000FABF File Offset: 0x0000DCBF
		public virtual Exception Error
		{
			get
			{
				this.NotImplemented();
				return null;
			}
		}

		/// <summary>When overridden in a derived class, gets or sets the <see cref="T:System.Web.IHttpHandler" /> object that is responsible for processing the HTTP request.</summary>
		/// <returns>The object that is responsible for processing the HTTP request.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x0000FABF File Offset: 0x0000DCBF
		// (set) Token: 0x06000682 RID: 1666 RVA: 0x0000FAC8 File Offset: 0x0000DCC8
		public virtual IHttpHandler Handler
		{
			get
			{
				this.NotImplemented();
				return null;
			}
			set
			{
				this.NotImplemented();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether custom errors are enabled for the current HTTP request.</summary>
		/// <returns>true if custom errors are enabled; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x0000FAD0 File Offset: 0x0000DCD0
		public virtual bool IsCustomErrorEnabled
		{
			get
			{
				this.NotImplemented();
				return false;
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the current HTTP request is in debug mode.</summary>
		/// <returns>true if the request is in debug mode; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x0000FAD0 File Offset: 0x0000DCD0
		public virtual bool IsDebuggingEnabled
		{
			get
			{
				this.NotImplemented();
				return false;
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether an <see cref="T:System.Web.HttpApplication" /> event has finished processing. </summary>
		/// <returns>true if the event has finished processing; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x0000FAD0 File Offset: 0x0000DCD0
		public virtual bool IsPostNotification
		{
			get
			{
				this.NotImplemented();
				return false;
			}
		}

		/// <summary>When overridden in a derived class, gets a key/value collection that can be used to organize and share data between a module and a handler during an HTTP request.</summary>
		/// <returns>A key/value collection that provides access to an individual value in the collection by using a specified key.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x0000FABF File Offset: 0x0000DCBF
		public virtual IDictionary Items
		{
			get
			{
				this.NotImplemented();
				return null;
			}
		}

		/// <summary>When overridden in a derived class, gets the <see cref="T:System.Web.IHttpHandler" /> object for the parent handler.</summary>
		/// <returns>An <see cref="T:System.Web.IHttpHandler" /> object that represents the parent handler, or null if no parent handler was found.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x0000FABF File Offset: 0x0000DCBF
		public virtual IHttpHandler PreviousHandler
		{
			get
			{
				this.NotImplemented();
				return null;
			}
		}

		/// <summary>When overridden in a derived class, gets the <see cref="T:System.Web.Profile.ProfileBase" /> object for the current user profile.</summary>
		/// <returns>If the profile properties are defined in the application configuration file and profiles are enabled for the application, an object that represents the current user profile; otherwise, null.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x0000FABF File Offset: 0x0000DCBF
		public virtual ProfileBase Profile
		{
			get
			{
				this.NotImplemented();
				return null;
			}
		}

		/// <summary>When overridden in a derived class, gets the <see cref="T:System.Web.HttpRequest" /> object for the current HTTP request.</summary>
		/// <returns>The current HTTP request.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x0000FABF File Offset: 0x0000DCBF
		public virtual HttpRequestBase Request
		{
			get
			{
				this.NotImplemented();
				return null;
			}
		}

		/// <summary>When overridden in a derived class, gets the <see cref="T:System.Web.HttpResponse" /> object for the current HTTP response.</summary>
		/// <returns>The current HTTP response.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x0000FABF File Offset: 0x0000DCBF
		public virtual HttpResponseBase Response
		{
			get
			{
				this.NotImplemented();
				return null;
			}
		}

		/// <summary>When overridden in a derived class, gets the <see cref="T:System.Web.HttpServerUtility" /> object that provides methods that are used when Web requests are being processed.</summary>
		/// <returns>The server utility object for the current HTTP request.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000281 RID: 641
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x0000FABF File Offset: 0x0000DCBF
		public virtual HttpServerUtilityBase Server
		{
			get
			{
				this.NotImplemented();
				return null;
			}
		}

		/// <summary>When overridden in a derived class, gets the <see cref="T:System.Web.SessionState.HttpSessionState" /> object for the current HTTP request.</summary>
		/// <returns>The session-state object for the current HTTP request.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x0000FABF File Offset: 0x0000DCBF
		public virtual HttpSessionStateBase Session
		{
			get
			{
				this.NotImplemented();
				return null;
			}
		}

		/// <summary>When overridden in a derived class, gets or sets a value that specifies whether the <see cref="T:System.Web.Security.UrlAuthorizationModule" /> object should skip the authorization check for the current request.</summary>
		/// <returns>true if <see cref="T:System.Web.Security.UrlAuthorizationModule" /> should skip the authorization check; otherwise, false. </returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000283 RID: 643
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x0000FAD0 File Offset: 0x0000DCD0
		// (set) Token: 0x0600068E RID: 1678 RVA: 0x0000FAC8 File Offset: 0x0000DCC8
		public virtual bool SkipAuthorization
		{
			get
			{
				this.NotImplemented();
				return false;
			}
			set
			{
				this.NotImplemented();
			}
		}

		/// <summary>When overridden in a derived class, gets the initial timestamp of the current HTTP request.</summary>
		/// <returns>The timestamp of the current HTTP request.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000284 RID: 644
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x0000FAD9 File Offset: 0x0000DCD9
		public virtual DateTime Timestamp
		{
			get
			{
				this.NotImplemented();
				return DateTime.MinValue;
			}
		}

		/// <summary>When overridden in a derived class, gets the <see cref="T:System.Web.TraceContext" /> object for the current HTTP response.</summary>
		/// <returns>The trace object for the current HTTP response.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x0000FABF File Offset: 0x0000DCBF
		public virtual TraceContext Trace
		{
			get
			{
				this.NotImplemented();
				return null;
			}
		}

		/// <summary>When overridden in a derived class, gets or sets security information for the current HTTP request.</summary>
		/// <returns>An object that contains security information for the current HTTP request.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x0000FABF File Offset: 0x0000DCBF
		// (set) Token: 0x06000692 RID: 1682 RVA: 0x0000FAC8 File Offset: 0x0000DCC8
		public virtual IPrincipal User
		{
			get
			{
				this.NotImplemented();
				return null;
			}
			set
			{
				this.NotImplemented();
			}
		}

		/// <summary>When overridden in a derived class, adds an exception to the exception collection for the current HTTP request.</summary>
		/// <param name="errorInfo">The exception to add to the exception collection.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000693 RID: 1683 RVA: 0x0000FAC8 File Offset: 0x0000DCC8
		public virtual void AddError(Exception errorInfo)
		{
			this.NotImplemented();
		}

		/// <summary>When overridden in a derived class, clears all errors for the current HTTP request.</summary>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000694 RID: 1684 RVA: 0x0000FAC8 File Offset: 0x0000DCC8
		public virtual void ClearError()
		{
			this.NotImplemented();
		}

		/// <summary>When overridden in a derived class, gets an application-level resource object based on the specified <see cref="P:System.Web.Compilation.ResourceExpressionFields.ClassKey" /> and <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" /> properties.</summary>
		/// <returns>The requested application-level resource object, or null if no matching resource object is found.</returns>
		/// <param name="classKey">A string that represents the <see cref="P:System.Web.Compilation.ResourceExpressionFields.ClassKey" /> property of the requested resource object.</param>
		/// <param name="resourceKey">A string that represents the <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" />   property of the requested resource object.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000695 RID: 1685 RVA: 0x0000FABF File Offset: 0x0000DCBF
		public virtual object GetGlobalResourceObject(string classKey, string resourceKey)
		{
			this.NotImplemented();
			return null;
		}

		/// <summary>When overridden in a derived class, gets an application-level resource object based on the specified <see cref="P:System.Web.Compilation.ResourceExpressionFields.ClassKey" /> and <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" /> properties, and on the <see cref="T:System.Globalization.CultureInfo" /> object.</summary>
		/// <returns>The requested application-level resource object, which is localized for the specified culture, or null if no matching resource object is found.</returns>
		/// <param name="classKey">A string that represents the <see cref="P:System.Web.Compilation.ResourceExpressionFields.ClassKey" /> property of the requested resource object.</param>
		/// <param name="resourceKey">A string that represents the <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" />   property of the requested resource object.</param>
		/// <param name="culture">A string that represents the <see cref="T:System.Globalization.CultureInfo" /> object of the requested resource.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000696 RID: 1686 RVA: 0x0000FABF File Offset: 0x0000DCBF
		public virtual object GetGlobalResourceObject(string classKey, string resourceKey, CultureInfo culture)
		{
			this.NotImplemented();
			return null;
		}

		/// <summary>When overridden in a derived class, gets a page-level resource object based on the specified <see cref="P:System.Web.Compilation.ExpressionBuilderContext.VirtualPath" /> and <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" /> properties.</summary>
		/// <returns>The requested page-level resource object, or null if no matching resource object is found.</returns>
		/// <param name="virtualPath">A string that represents the <see cref="P:System.Web.Compilation.ExpressionBuilderContext.VirtualPath" /> property of the local resource object.</param>
		/// <param name="resourceKey">A string that represents the <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" />   property of the requested resource object.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000697 RID: 1687 RVA: 0x0000FABF File Offset: 0x0000DCBF
		public virtual object GetLocalResourceObject(string virtualPath, string resourceKey)
		{
			this.NotImplemented();
			return null;
		}

		/// <summary>When overridden in a derived class, gets a page-level resource object based on the specified <see cref="P:System.Web.Compilation.ExpressionBuilderContext.VirtualPath" /> and <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" /> properties, and on the <see cref="T:System.Globalization.CultureInfo" /> object.</summary>
		/// <returns>The requested local resource object, which is localized for the specified culture, or null if no matching resource object is found.</returns>
		/// <param name="virtualPath">A string that represents the <see cref="P:System.Web.Compilation.ExpressionBuilderContext.VirtualPath" /> property of the local resource object.</param>
		/// <param name="resourceKey">A string that represents the <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" />   property of the requested resource object.</param>
		/// <param name="culture">A string that represents the <see cref="T:System.Globalization.CultureInfo" /> object of the requested resource object.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000698 RID: 1688 RVA: 0x0000FABF File Offset: 0x0000DCBF
		public virtual object GetLocalResourceObject(string virtualPath, string resourceKey, CultureInfo culture)
		{
			this.NotImplemented();
			return null;
		}

		/// <summary>When overridden in a derived class, gets the specified configuration section of the current application's default configuration. </summary>
		/// <returns>The specified section, or null if the section does not exist.</returns>
		/// <param name="sectionName">The configuration section path (in XPath format) and the configuration element name.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000699 RID: 1689 RVA: 0x0000FABF File Offset: 0x0000DCBF
		public virtual object GetSection(string sectionName)
		{
			this.NotImplemented();
			return null;
		}

		/// <summary>When overridden in a derived class, returns an object for the current service type.</summary>
		/// <returns>The current service type, or null if no service is found.</returns>
		/// <param name="serviceType">The type of service object to get.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600069A RID: 1690 RVA: 0x0000FABF File Offset: 0x0000DCBF
		public virtual object GetService(Type serviceType)
		{
			this.NotImplemented();
			return null;
		}

		/// <summary>When overridden in a derived class, specifies a handler for the request.</summary>
		/// <param name="handler">The object that should process the request.</param>
		/// <exception cref="T:System.NotImplementedException">A derived type fails to implement this method.</exception>
		// Token: 0x0600069B RID: 1691 RVA: 0x0000FAC8 File Offset: 0x0000DCC8
		public virtual void RemapHandler(IHttpHandler handler)
		{
			this.NotImplemented();
		}

		/// <summary>When overridden in a derived class, rewrites the URL by using the specified path.</summary>
		/// <param name="path">The replacement path.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600069C RID: 1692 RVA: 0x0000FAC8 File Offset: 0x0000DCC8
		public virtual void RewritePath(string path)
		{
			this.NotImplemented();
		}

		/// <summary>When overridden in a derived class, rewrites the URL by using the specified path and a value that specifies whether the virtual path for server resources is modified.</summary>
		/// <param name="path">The replacement path.</param>
		/// <param name="rebaseClientPath">true to reset the virtual path; false to keep the virtual path unchanged.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600069D RID: 1693 RVA: 0x0000FAC8 File Offset: 0x0000DCC8
		public virtual void RewritePath(string path, bool rebaseClientPath)
		{
			this.NotImplemented();
		}

		/// <summary>When overridden in a derived class, rewrites the URL by using the specified path, path information, and query string information.</summary>
		/// <param name="filePath">The replacement path.</param>
		/// <param name="pathInfo">Additional path information for a resource.</param>
		/// <param name="queryString">The request query string.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600069E RID: 1694 RVA: 0x0000FAC8 File Offset: 0x0000DCC8
		public virtual void RewritePath(string filePath, string pathInfo, string queryString)
		{
			this.NotImplemented();
		}

		/// <summary>When overridden in a derived class, rewrites the URL by using the specified path, path information, query string information, and a value that specifies whether the client file path is set to the rewrite path. </summary>
		/// <param name="filePath">The replacement path.</param>
		/// <param name="pathInfo">Additional path information for a resource.</param>
		/// <param name="queryString">The request query string.</param>
		/// <param name="setClientFilePath">true to set the file path used for client resources to the value of the <paramref name="filePath" /> parameter; otherwise, false.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600069F RID: 1695 RVA: 0x0000FAC8 File Offset: 0x0000DCC8
		public virtual void RewritePath(string filePath, string pathInfo, string queryString, bool setClientFilePath)
		{
			this.NotImplemented();
		}

		/// <summary>When overridden in a derived class, sets the type of session state behavior that is required to support an HTTP request.</summary>
		/// <param name="sessionStateBehavior">One of the enumeration values that specifies what type of session state behavior is required.</param>
		/// <exception cref="T:System.NotImplementedException">A derived type fails to implement this method.</exception>
		// Token: 0x060006A0 RID: 1696 RVA: 0x0000FAC8 File Offset: 0x0000DCC8
		public virtual void SetSessionStateBehavior(SessionStateBehavior sessionStateBehavior)
		{
			this.NotImplemented();
		}

		/// <summary>When implemented in a derived class, gets or sets a value that indicates whether asynchronous operations are allowed during parts of ASP.NET request processing when they are not expected.</summary>
		/// <returns>false if ASP.NET will throw an exception when the asynchronous API is used at a time when it is not expected; otherwise, true. The default value is false.</returns>
		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x0000FAE8 File Offset: 0x0000DCE8
		// (set) Token: 0x060006A3 RID: 1699 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual bool AllowAsyncDuringSyncStages
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

		/// <summary>When implemented in a derived class, gets or sets an object that contains flags that pertain to asynchronous preload mode.</summary>
		/// <returns>An object that contains flags that pertain to asynchronous preload mode.</returns>
		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x0000FB04 File Offset: 0x0000DD04
		// (set) Token: 0x060006A5 RID: 1701 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual AsyncPreloadModeFlags AsyncPreloadMode
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

		/// <summary>When implemented in a derived class, gets a value that indicates whether the request is an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> connection request.</summary>
		/// <returns>true if the request is an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> request; otherwise, false.</returns>
		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x0000FB20 File Offset: 0x0000DD20
		public virtual bool IsWebSocketRequest
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>When implemented in a derived class, gets a value that indicates whether the connection is upgrading from an HTTP connection to an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> connection.</summary>
		/// <returns>true if the connection is upgrading; otherwise, false.</returns>
		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0000FB3C File Offset: 0x0000DD3C
		public virtual bool IsWebSocketRequestUpgrading
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>When implemented in a derived class, gets a reference to the page-instrumentation service instance for this request.</summary>
		/// <returns>The page-instrumentation service instance for this request.</returns>
		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual PageInstrumentationService PageInstrumentation
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>When implemented in a derived class, gets or sets a value that specifies whether the ASP.NET runtime should call <see cref="M:System.Threading.Thread.Abort" /> on the thread that is servicing this request when the request times out.</summary>
		/// <returns>true if <see cref="M:System.Threading.Thread.Abort" /> will be called when the thread times out; otherwise, false. The default is true.</returns>
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0000FB58 File Offset: 0x0000DD58
		// (set) Token: 0x060006AA RID: 1706 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual bool ThreadAbortOnTimeout
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

		/// <summary>When implemented in a derived class, gets the negotiated protocol that was sent from the server to the client for an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> connection.</summary>
		/// <returns>The negotiated protocol.</returns>
		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual string WebSocketNegotiatedProtocol
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>When implemented in a derived class, gets the ordered list of protocols that were requested by the client.</summary>
		/// <returns>The requested protocols, or null if this is not an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> request or if no list is present.</returns>
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x0000FAB7 File Offset: 0x0000DCB7
		public virtual IList<string> WebSocketRequestedProtocols
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>When implemented in a derived class, accepts an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> request using the specified user function.</summary>
		/// <param name="userFunc">The user function.</param>
		// Token: 0x060006AD RID: 1709 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void AcceptWebSocketRequest(Func<AspNetWebSocketContext, Task> userFunc)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>When implemented in a derived class, accepts an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> request using the specified user function and options object.</summary>
		/// <param name="userFunc">The user function.</param>
		/// <param name="options">The options object.</param>
		// Token: 0x060006AE RID: 1710 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void AcceptWebSocketRequest(Func<AspNetWebSocketContext, Task> userFunc, AspNetWebSocketOptions options)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>When implemented in a derived class, raises a virtual event that occurs when the HTTP part of the request is ending.</summary>
		/// <returns>The subscription token.</returns>
		/// <param name="callback">The HTTP context object.</param>
		// Token: 0x060006AF RID: 1711 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual ISubscriptionToken AddOnRequestCompleted(Action<HttpContextBase> callback)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>When implemented in a derived class, enables an object's <see cref="M:System.IDisposable.Dispose" /> method to be called when the <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> connection part of this request is completed.</summary>
		/// <returns>The subscription token.</returns>
		/// <param name="target">The object whose <see cref="M:System.IDisposable.Dispose" /> method must be called when the <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> connection part of the request is completed.</param>
		// Token: 0x060006B0 RID: 1712 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual ISubscriptionToken DisposeOnPipelineCompleted(IDisposable target)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
