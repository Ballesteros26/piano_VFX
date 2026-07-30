using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Instrumentation;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.Util;
using System.Web.WebSockets;
using Unity;

namespace System.Web
{
	/// <summary>Encapsulates all HTTP-specific information about an individual HTTP request.</summary>
	// Token: 0x0200008A RID: 138
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HttpContext : IServiceProvider
	{
		// Token: 0x17000248 RID: 584
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x0000EF97 File Offset: 0x0000D197
		private static DefaultResourceProviderFactory DefaultProviderFactory
		{
			get
			{
				if (HttpContext.default_provider_factory == null)
				{
					HttpContext.default_provider_factory = new DefaultResourceProviderFactory();
				}
				return HttpContext.default_provider_factory;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpContext" /> class that uses the specified worker-request object.</summary>
		/// <param name="wr">The <see cref="T:System.Web.HttpWorkerRequest" /> object for the current HTTP request.</param>
		// Token: 0x0600061B RID: 1563 RVA: 0x0000EFB0 File Offset: 0x0000D1B0
		public HttpContext(HttpWorkerRequest wr)
		{
			this.WorkerRequest = wr;
			this.request = new HttpRequest(this.WorkerRequest, this);
			this.response = new HttpResponse(this.WorkerRequest, this);
			this.SessionStateBehavior = SessionStateBehavior.Default;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpContext" /> class by using the specified request and response objects.</summary>
		/// <param name="request">The <see cref="T:System.Web.HttpRequest" /> object for the current HTTP request.</param>
		/// <param name="response">The <see cref="T:System.Web.HttpResponse" /> object for the current HTTP request.</param>
		// Token: 0x0600061C RID: 1564 RVA: 0x0000F000 File Offset: 0x0000D200
		public HttpContext(HttpRequest request, HttpResponse response)
		{
			this.request = request;
			this.response = response;
			this.request.Context = this;
			this.response.Context = this;
			this.SessionStateBehavior = SessionStateBehavior.Default;
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x0000F040 File Offset: 0x0000D240
		// (set) Token: 0x0600061E RID: 1566 RVA: 0x0000F048 File Offset: 0x0000D248
		internal bool IsProcessingInclude
		{
			get
			{
				return this._isProcessingInclude;
			}
			set
			{
				this._isProcessingInclude = value;
			}
		}

		/// <summary>Gets an array of errors accumulated while processing an HTTP request.</summary>
		/// <returns>An array of <see cref="T:System.Exception" /> objects for the current HTTP request.</returns>
		// Token: 0x1700024A RID: 586
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x0000F054 File Offset: 0x0000D254
		public Exception[] AllErrors
		{
			get
			{
				if (this.errors == null)
				{
					return null;
				}
				if (this.errors is Exception)
				{
					return new Exception[] { (Exception)this.errors };
				}
				return (Exception[])((ArrayList)this.errors).ToArray(typeof(Exception));
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpApplicationState" /> object for the current HTTP request.</summary>
		/// <returns>The <see cref="T:System.Web.HttpApplicationState" /> for the current HTTP request.To get the <see cref="T:System.Web.HttpApplication" /> object for the current HTTP request, use <see cref="P:System.Web.HttpContext.ApplicationInstance" />. (ASP.NET uses ApplicationInstance instead of Application as a property name to refer to the current <see cref="T:System.Web.HttpApplication" /> instance in order to avoid confusion between ASP.NET and classic ASP. In classic ASP, Application refers to the global application state dictionary.) </returns>
		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x000097F0 File Offset: 0x000079F0
		public HttpApplicationState Application
		{
			get
			{
				return HttpApplicationFactory.ApplicationState;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.HttpApplication" /> object for the current HTTP request.</summary>
		/// <returns>The <see cref="T:System.Web.HttpApplication" /> for the current HTTP request.ASP.NET uses ApplicationInstance instead of Application as a property name to refer to the current <see cref="T:System.Web.HttpApplication" /> instance in order to avoid confusion between ASP.NET and classic ASP. In classic ASP, Application refers to the global application state dictionary.</returns>
		/// <exception cref="T:System.InvalidOperationException">The Web application is running under IIS 7.0 in Integrated mode, and an attempt was made to change the property value from a non-null value to null.</exception>
		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x0000F0AC File Offset: 0x0000D2AC
		// (set) Token: 0x06000622 RID: 1570 RVA: 0x0000F0B4 File Offset: 0x0000D2B4
		public HttpApplication ApplicationInstance
		{
			get
			{
				return this.app_instance;
			}
			set
			{
				this.app_instance = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Caching.Cache" /> object for the current application domain.</summary>
		/// <returns>The <see cref="T:System.Web.Caching.Cache" /> for the current application domain.</returns>
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x0000F0BD File Offset: 0x0000D2BD
		public Cache Cache
		{
			get
			{
				return HttpRuntime.Cache;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x0000F0C4 File Offset: 0x0000D2C4
		internal Cache InternalCache
		{
			get
			{
				return HttpRuntime.InternalCache;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.HttpContext" /> object for the current HTTP request.</summary>
		/// <returns>The <see cref="T:System.Web.HttpContext" /> for the current HTTP request.</returns>
		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x0000F0CB File Offset: 0x0000D2CB
		// (set) Token: 0x06000626 RID: 1574 RVA: 0x0000F0DC File Offset: 0x0000D2DC
		public static HttpContext Current
		{
			get
			{
				return (HttpContext)CallContext.GetData("c");
			}
			set
			{
				CallContext.SetData("c", value);
			}
		}

		/// <summary>Gets the first error (if any) accumulated during HTTP request processing.</summary>
		/// <returns>The first <see cref="T:System.Exception" /> for the current HTTP request/response process; otherwise, null if no errors were accumulated during the HTTP request processing. The default is null.</returns>
		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x0000F0E9 File Offset: 0x0000D2E9
		public Exception Error
		{
			get
			{
				if (this.errors == null || this.errors is Exception)
				{
					return (Exception)this.errors;
				}
				return (Exception)((ArrayList)this.errors)[0];
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.IHttpHandler" /> object responsible for processing the HTTP request.</summary>
		/// <returns>An <see cref="T:System.Web.IHttpHandler" /> responsible for processing the HTTP request.</returns>
		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x0000F122 File Offset: 0x0000D322
		// (set) Token: 0x06000629 RID: 1577 RVA: 0x0000F12A File Offset: 0x0000D32A
		public IHttpHandler Handler
		{
			get
			{
				return this.handler;
			}
			set
			{
				this.handler = value;
			}
		}

		/// <summary>Gets a value indicating whether custom errors are enabled for the current HTTP request.</summary>
		/// <returns>true if custom errors are enabled; otherwise, false.</returns>
		// Token: 0x17000252 RID: 594
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x0000F134 File Offset: 0x0000D334
		public bool IsCustomErrorEnabled
		{
			get
			{
				bool flag;
				try
				{
					flag = this.IsCustomErrorEnabledUnsafe;
				}
				catch
				{
					flag = false;
				}
				return flag;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x0000F160 File Offset: 0x0000D360
		internal bool IsCustomErrorEnabledUnsafe
		{
			get
			{
				CustomErrorsSection customErrorsSection = (CustomErrorsSection)WebConfigurationManager.GetSection("system.web/customErrors");
				return customErrorsSection.Mode == CustomErrorsMode.On || (customErrorsSection.Mode == CustomErrorsMode.RemoteOnly && !this.Request.IsLocal);
			}
		}

		/// <summary>Gets a value indicating whether the current HTTP request is in debug mode.</summary>
		/// <returns>true if the request is in debug mode; otherwise, false.</returns>
		// Token: 0x17000254 RID: 596
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0000F1A0 File Offset: 0x0000D3A0
		public bool IsDebuggingEnabled
		{
			get
			{
				return RuntimeHelpers.DebuggingEnabled;
			}
		}

		/// <summary>Gets a key/value collection that can be used to organize and share data between an <see cref="T:System.Web.IHttpModule" /> interface and an <see cref="T:System.Web.IHttpHandler" /> interface during an HTTP request.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> key/value collection that provides access to an individual value in the collection by a specified key.</returns>
		// Token: 0x17000255 RID: 597
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x0000F1A7 File Offset: 0x0000D3A7
		public IDictionary Items
		{
			get
			{
				if (this.items == null)
				{
					this.items = new Hashtable();
				}
				return this.items;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpRequest" /> object for the current HTTP request.</summary>
		/// <returns>The <see cref="T:System.Web.HttpRequest" /> for the current HTTP request.</returns>
		/// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
		// Token: 0x17000256 RID: 598
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x0000F1C2 File Offset: 0x0000D3C2
		public HttpRequest Request
		{
			get
			{
				return this.request;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpResponse" /> object for the current HTTP response.</summary>
		/// <returns>The <see cref="T:System.Web.HttpResponse" /> for the current HTTP response.</returns>
		/// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
		// Token: 0x17000257 RID: 599
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x0000F1CA File Offset: 0x0000D3CA
		public HttpResponse Response
		{
			get
			{
				return this.response;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpServerUtility" /> object that provides methods used in processing Web requests.</summary>
		/// <returns>The <see cref="T:System.Web.HttpServerUtility" /> for the current HTTP request.</returns>
		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x0000F1D2 File Offset: 0x0000D3D2
		public HttpServerUtility Server
		{
			get
			{
				if (this.server == null)
				{
					this.server = new HttpServerUtility(this);
				}
				return this.server;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.SessionState.HttpSessionState" /> object for the current HTTP request.</summary>
		/// <returns>The <see cref="T:System.Web.SessionState.HttpSessionState" /> object for the current HTTP request.</returns>
		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x0000F1EE File Offset: 0x0000D3EE
		public HttpSessionState Session
		{
			get
			{
				return this.session_state;
			}
		}

		/// <summary>Gets or sets a value that specifies whether the <see cref="T:System.Web.Security.UrlAuthorizationModule" /> object should skip the authorization check for the current request.</summary>
		/// <returns>true if <see cref="T:System.Web.Security.UrlAuthorizationModule" /> should skip the authorization check; otherwise, false. The default is false.</returns>
		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x0000F1F6 File Offset: 0x0000D3F6
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x0000F1FE File Offset: 0x0000D3FE
		public bool SkipAuthorization
		{
			get
			{
				return this.skip_authorization;
			}
			[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
			set
			{
				this.skip_authorization = value;
			}
		}

		/// <summary>Gets the initial timestamp of the current HTTP request.</summary>
		/// <returns>The timestamp of the current HTTP request.</returns>
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0000F207 File Offset: 0x0000D407
		public DateTime Timestamp
		{
			get
			{
				return this.time_stamp.ToLocalTime();
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.TraceContext" /> object for the current HTTP response.</summary>
		/// <returns>The <see cref="T:System.Web.TraceContext" /> for the current HTTP response.</returns>
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x0000F214 File Offset: 0x0000D414
		public TraceContext Trace
		{
			get
			{
				if (this.trace_context == null)
				{
					this.trace_context = new TraceContext(this);
				}
				return this.trace_context;
			}
		}

		/// <summary>Gets or sets security information for the current HTTP request.</summary>
		/// <returns>Security information for the current HTTP request.</returns>
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x0000F230 File Offset: 0x0000D430
		// (set) Token: 0x06000637 RID: 1591 RVA: 0x0000F238 File Offset: 0x0000D438
		public IPrincipal User
		{
			get
			{
				return this.user;
			}
			[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
			set
			{
				this.user = value;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x0000F241 File Offset: 0x0000D441
		// (set) Token: 0x06000639 RID: 1593 RVA: 0x0000F249 File Offset: 0x0000D449
		internal bool MapRequestHandlerDone { get; set; }

		/// <summary>Gets a <see cref="T:System.Web.RequestNotification" /> value that indicates the current <see cref="T:System.Web.HttpApplication" /> event that is processing. </summary>
		/// <returns>One of the <see cref="T:System.Web.RequestNotification" /> values.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The operation requires integrated pipeline mode in IIS 7.0 and at least the .NET Framework version 3.0.</exception>
		// Token: 0x1700025F RID: 607
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x0000F252 File Offset: 0x0000D452
		public RequestNotification CurrentNotification
		{
			get
			{
				throw new PlatformNotSupportedException("This property is not supported on Mono.");
			}
		}

		/// <summary>Gets a value that is the current processing point in the ASP.NET pipeline just after an <see cref="T:System.Web.HttpApplication" /> event has finished processing. </summary>
		/// <returns>true if custom errors are enabled; otherwise, false.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The operation requires the integrated pipeline mode in IIS 7.0 and at least the .NET Framework 3.0.</exception>
		// Token: 0x17000260 RID: 608
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x0000F252 File Offset: 0x0000D452
		public bool IsPostNotification
		{
			get
			{
				throw new PlatformNotSupportedException("This property is not supported on Mono.");
			}
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0000F25E File Offset: 0x0000D45E
		internal void PushHandler(IHttpHandler handler)
		{
			if (handler == null)
			{
				return;
			}
			if (this.handlers == null)
			{
				this.handlers = new LinkedList<IHttpHandler>();
			}
			this.handlers.AddLast(handler);
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0000F284 File Offset: 0x0000D484
		internal void PopHandler()
		{
			if (this.handlers == null || this.handlers.Count == 0)
			{
				return;
			}
			this.handlers.RemoveLast();
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0000F2A7 File Offset: 0x0000D4A7
		private IHttpHandler GetCurrentHandler()
		{
			if (this.handlers == null || this.handlers.Count == 0)
			{
				return null;
			}
			return this.handlers.Last.Value;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0000F2D0 File Offset: 0x0000D4D0
		private IHttpHandler GetPreviousHandler()
		{
			if (this.handlers == null || this.handlers.Count <= 1)
			{
				return null;
			}
			LinkedListNode<IHttpHandler> previous = this.handlers.Last.Previous;
			if (previous != null)
			{
				return previous.Value;
			}
			return null;
		}

		/// <summary>Gets the <see cref="T:System.Web.IHttpHandler" /> object that represents the currently executing handler.</summary>
		/// <returns>An <see cref="T:System.Web.IHttpHandler" /> that represents the currently executing handler. </returns>
		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x0000F311 File Offset: 0x0000D511
		public IHttpHandler CurrentHandler
		{
			get
			{
				return this.GetCurrentHandler();
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.IHttpHandler" /> object for the parent handler.</summary>
		/// <returns>An <see cref="T:System.Web.IHttpHandler" /> instance, or null if no previous handler was found.</returns>
		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x0000F319 File Offset: 0x0000D519
		public IHttpHandler PreviousHandler
		{
			get
			{
				return this.GetPreviousHandler();
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x0000F321 File Offset: 0x0000D521
		internal bool ProfileInitialized
		{
			get
			{
				return this.profile != null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Profile.ProfileBase" /> object for the current user profile.</summary>
		/// <returns>A <see cref="T:System.Web.Profile.ProfileBase" /> if the application configuration file contains a definition for the profile's properties; otherwise, null.</returns>
		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x0000F32C File Offset: 0x0000D52C
		// (set) Token: 0x06000644 RID: 1604 RVA: 0x0000F388 File Offset: 0x0000D588
		public ProfileBase Profile
		{
			get
			{
				if (this.profile == null)
				{
					if (this.Request.IsAuthenticated)
					{
						this.profile = ProfileBase.Create(this.User.Identity.Name);
					}
					else
					{
						this.profile = ProfileBase.Create(this.Request.AnonymousID, false);
					}
				}
				return this.profile;
			}
			internal set
			{
				this.profile = value;
			}
		}

		/// <summary>Adds an exception to the exception collection for the current HTTP request.</summary>
		/// <param name="errorInfo">The <see cref="T:System.Exception" /> to add to the exception collection.</param>
		// Token: 0x06000645 RID: 1605 RVA: 0x0000F394 File Offset: 0x0000D594
		public void AddError(Exception errorInfo)
		{
			if (this.errors == null)
			{
				this.errors = errorInfo;
				return;
			}
			ArrayList arrayList;
			if (this.errors is Exception)
			{
				arrayList = new ArrayList();
				arrayList.Add(this.errors);
				this.errors = arrayList;
			}
			else
			{
				arrayList = (ArrayList)this.errors;
			}
			arrayList.Add(errorInfo);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0000F3EE File Offset: 0x0000D5EE
		internal void ClearError(Exception e)
		{
			if (this.errors == e)
			{
				this.errors = null;
			}
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0000F400 File Offset: 0x0000D600
		internal bool HasError(Exception e)
		{
			return this.errors == e || (this.errors is ArrayList && ((ArrayList)this.errors).Contains(e));
		}

		/// <summary>Clears all errors for the current HTTP request.</summary>
		// Token: 0x06000648 RID: 1608 RVA: 0x0000F42D File Offset: 0x0000D62D
		public void ClearError()
		{
			this.errors = null;
		}

		/// <summary>Returns requested configuration information for the current application.</summary>
		/// <returns>An object containing configuration information. (Cast the returned configuration section to the appropriate configuration type before use.)</returns>
		/// <param name="name">The application configuration tag for which information is requested.</param>
		// Token: 0x06000649 RID: 1609 RVA: 0x0000F436 File Offset: 0x0000D636
		[Obsolete("The recommended alternative is System.Web.Configuration.WebConfigurationManager.GetWebApplicationSection in System.Web.dll. http://go.microsoft.com/fwlink/?linkid=14202")]
		public static object GetAppConfig(string name)
		{
			return ConfigurationSettings.GetConfig(name);
		}

		/// <summary>Returns requested configuration information for the current HTTP request.</summary>
		/// <returns>The specified <see cref="T:System.Configuration.ConfigurationSection" />, null if the section does not exist, or an internal object if the section is not accessible at run time. (Cast the returned object to the appropriate configuration type before use.) </returns>
		/// <param name="name">The configuration tag for which information is requested.</param>
		// Token: 0x0600064A RID: 1610 RVA: 0x0000F43E File Offset: 0x0000D63E
		[Obsolete("The recommended alternative is System.Web.HttpContext.GetSection in System.Web.dll. http://go.microsoft.com/fwlink/?linkid=14202")]
		public object GetConfig(string name)
		{
			return this.GetSection(name);
		}

		/// <summary>Gets an application-level resource object based on the specified <see cref="P:System.Web.Compilation.ResourceExpressionFields.ClassKey" /> and <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" /> properties.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the requested application-level resource object; otherwise, null if a resource object is not found or if a resource object is found but it does not have the requested property.</returns>
		/// <param name="classKey">A string that represents the <see cref="P:System.Web.Compilation.ResourceExpressionFields.ClassKey" /> property of the requested resource object.</param>
		/// <param name="resourceKey">A string that represents the <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" />   property of the requested resource object.</param>
		/// <exception cref="T:System.Resources.MissingManifestResourceException">A resource object with the specified <paramref name="classKey" /> parameter was not found.- or -The main assembly does not contain the resources for the neutral culture, and these resources are required because the appropriate satellite assembly is missing.</exception>
		// Token: 0x0600064B RID: 1611 RVA: 0x0000F447 File Offset: 0x0000D647
		public static object GetGlobalResourceObject(string classKey, string resourceKey)
		{
			return HttpContext.GetGlobalResourceObject(classKey, resourceKey, Thread.CurrentThread.CurrentUICulture);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0000F45C File Offset: 0x0000D65C
		private static bool EnsureProviderFactory()
		{
			if (HttpContext.resource_providers == null)
			{
				HttpContext.resource_providers = new Dictionary<string, IResourceProvider>();
			}
			if (HttpContext.provider_factory != null)
			{
				return true;
			}
			GlobalizationSection globalizationSection = WebConfigurationManager.GetSection("system.web/globalization") as GlobalizationSection;
			if (globalizationSection == null)
			{
				return false;
			}
			string text = globalizationSection.ResourceProviderFactoryType;
			bool flag = false;
			if (string.IsNullOrEmpty(text))
			{
				flag = true;
				text = typeof(DefaultResourceProviderFactory).AssemblyQualifiedName;
			}
			ResourceProviderFactory resourceProviderFactory = Activator.CreateInstance(HttpApplication.LoadType(text, true)) as ResourceProviderFactory;
			if (resourceProviderFactory == null && flag)
			{
				return false;
			}
			HttpContext.provider_factory = resourceProviderFactory;
			if (flag)
			{
				HttpContext.default_provider_factory = resourceProviderFactory as DefaultResourceProviderFactory;
			}
			return true;
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0000F4EC File Offset: 0x0000D6EC
		internal static IResourceProvider GetResourceProvider(string virtualPath, bool isLocal)
		{
			if (!HttpContext.EnsureProviderFactory())
			{
				return null;
			}
			IResourceProvider resourceProvider = null;
			if (!HttpContext.resource_providers.TryGetValue(virtualPath, out resourceProvider))
			{
				if (isLocal)
				{
					resourceProvider = HttpContext.provider_factory.CreateLocalResourceProvider(virtualPath);
				}
				else
				{
					resourceProvider = HttpContext.provider_factory.CreateGlobalResourceProvider(virtualPath);
				}
				if (resourceProvider == null)
				{
					if (isLocal)
					{
						resourceProvider = HttpContext.DefaultProviderFactory.CreateLocalResourceProvider(virtualPath);
					}
					else
					{
						resourceProvider = HttpContext.DefaultProviderFactory.CreateGlobalResourceProvider(virtualPath);
					}
					if (resourceProvider == null)
					{
						return null;
					}
				}
				HttpContext.resource_providers.Add(virtualPath, resourceProvider);
			}
			return resourceProvider;
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0000F564 File Offset: 0x0000D764
		private static object GetGlobalObjectFromFactory(string classKey, string resourceKey, CultureInfo culture)
		{
			IResourceProvider resourceProvider = HttpContext.GetResourceProvider(classKey, false);
			if (resourceProvider == null)
			{
				return null;
			}
			return resourceProvider.GetObject(resourceKey, culture);
		}

		/// <summary>Gets an application-level resource object based on the specified <see cref="P:System.Web.Compilation.ResourceExpressionFields.ClassKey" /> and <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" /> properties, and on the <see cref="T:System.Globalization.CultureInfo" /> object.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the requested application-level resource object, which is localized for the specified culture; otherwise, null if a resource object is not found or if a resource object is found but it does not have the requested property.</returns>
		/// <param name="classKey">A string that represents the <see cref="P:System.Web.Compilation.ResourceExpressionFields.ClassKey" /> property of the requested resource object.</param>
		/// <param name="resourceKey">A string that represents a <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" />   property of the requested resource object.</param>
		/// <param name="culture">A string that represents the <see cref="T:System.Globalization.CultureInfo" /> object of the requested resource.</param>
		/// <exception cref="T:System.Resources.MissingManifestResourceException">A resource object for which the specified <paramref name="classKey" /> parameter was not found.- or -The main assembly does not contain the resources for the neutral culture, and these resources are required because the appropriate satellite assembly is missing.</exception>
		// Token: 0x0600064F RID: 1615 RVA: 0x0000F586 File Offset: 0x0000D786
		public static object GetGlobalResourceObject(string classKey, string resourceKey, CultureInfo culture)
		{
			return HttpContext.GetGlobalObjectFromFactory("Resources." + classKey, resourceKey, culture);
		}

		/// <summary>Gets a page-level resource object based on the specified <see cref="P:System.Web.Compilation.ExpressionBuilderContext.VirtualPath" /> and <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" /> properties.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the requested page-level resource object; otherwise, null if a matching resource object is found but not a <paramref name="resourceKey" /> parameter.</returns>
		/// <param name="virtualPath">The <see cref="P:System.Web.Compilation.ExpressionBuilderContext.VirtualPath" /> property for the local resource object.</param>
		/// <param name="resourceKey">A string that represents a <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" />   property of the requested resource object</param>
		/// <exception cref="T:System.Resources.MissingManifestResourceException">A resource object was not found for the specified <paramref name="virtualPath" /> parameter.</exception>
		/// <exception cref="T:System.ArgumentException">The specified <paramref name="virtualPath" /> parameter is not in the current application's root directory.</exception>
		/// <exception cref="T:System.InvalidOperationException">The resource class for the page was not found.</exception>
		// Token: 0x06000650 RID: 1616 RVA: 0x0000F59A File Offset: 0x0000D79A
		public static object GetLocalResourceObject(string virtualPath, string resourceKey)
		{
			return HttpContext.GetLocalResourceObject(virtualPath, resourceKey, Thread.CurrentThread.CurrentUICulture);
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0000F5B0 File Offset: 0x0000D7B0
		private static object GetLocalObjectFromFactory(string virtualPath, string resourceKey, CultureInfo culture)
		{
			IResourceProvider resourceProvider = HttpContext.GetResourceProvider(virtualPath, true);
			if (resourceProvider == null)
			{
				return null;
			}
			return resourceProvider.GetObject(resourceKey, culture);
		}

		/// <summary>Gets a page-level resource object based on the specified <see cref="P:System.Web.Compilation.ExpressionBuilderContext.VirtualPath" /> and <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" /> properties, and on the <see cref="T:System.Globalization.CultureInfo" /> object.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the requested local resource object, which is localized for the specified culture; otherwise null if a matching resource object is found but not a <paramref name="resourceKey" /> parameter.</returns>
		/// <param name="virtualPath">The <see cref="P:System.Web.Compilation.ExpressionBuilderContext.VirtualPath" /> property for the local resource object.</param>
		/// <param name="resourceKey">A string that represents a <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" />   property of the requested resource object.</param>
		/// <param name="culture">A string that represents the <see cref="T:System.Globalization.CultureInfo" /> object of the requested resource object.</param>
		/// <exception cref="T:System.Resources.MissingManifestResourceException">A resource object was not found for the specified <paramref name="virtualPath" /> Parameter.</exception>
		/// <exception cref="T:System.ArgumentException">The specified <paramref name="virtualPath" /> parameter is not in the current application's root directory.</exception>
		/// <exception cref="T:System.InvalidOperationException">The resource class for the page was not found.</exception>
		// Token: 0x06000652 RID: 1618 RVA: 0x0000F5D2 File Offset: 0x0000D7D2
		public static object GetLocalResourceObject(string virtualPath, string resourceKey, CultureInfo culture)
		{
			if (!VirtualPathUtility.IsAbsolute(virtualPath))
			{
				throw new ArgumentException("The specified virtualPath was not rooted.");
			}
			return HttpContext.GetLocalObjectFromFactory(virtualPath, resourceKey, culture);
		}

		/// <summary>Gets a specified configuration section for the current application's default configuration. </summary>
		/// <returns>The specified <see cref="T:System.Configuration.ConfigurationSection" />, null if the section does not exist, or an internal object if the section is not accessible at run time.</returns>
		/// <param name="sectionName">The configuration section path (in XPath format) and the configuration element name.</param>
		// Token: 0x06000653 RID: 1619 RVA: 0x0000F5EF File Offset: 0x0000D7EF
		public object GetSection(string sectionName)
		{
			return WebConfigurationManager.GetSection(sectionName);
		}

		/// <summary>Returns an object for the current service type.</summary>
		/// <returns>A <see cref="T:System.Web.HttpContext" />; otherwise, null if no service is found.</returns>
		/// <param name="service">A type of <see cref="T:System.Web.HttpContext" /> service to set the service provider to.</param>
		// Token: 0x06000654 RID: 1620 RVA: 0x0000F5F8 File Offset: 0x0000D7F8
		object IServiceProvider.GetService(Type service)
		{
			if (service == typeof(HttpWorkerRequest))
			{
				return this.WorkerRequest;
			}
			if (service == typeof(HttpApplication))
			{
				return this.ApplicationInstance;
			}
			if (service == typeof(HttpRequest))
			{
				return this.Request;
			}
			if (service == typeof(HttpResponse))
			{
				return this.Response;
			}
			if (service == typeof(HttpSessionState))
			{
				return this.Session;
			}
			if (service == typeof(HttpApplicationState))
			{
				return this.Application;
			}
			if (service == typeof(IPrincipal))
			{
				return this.User;
			}
			if (service == typeof(Cache))
			{
				return this.Cache;
			}
			if (service == typeof(HttpContext))
			{
				return HttpContext.Current;
			}
			if (service == typeof(IHttpHandler))
			{
				return this.Handler;
			}
			if (service == typeof(HttpServerUtility))
			{
				return this.Server;
			}
			if (service == typeof(TraceContext))
			{
				return this.Trace;
			}
			return null;
		}

		/// <summary>Enables you to specify a handler for the request.</summary>
		/// <param name="handler">The object that should process the request.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Web.HttpContext.RemapHandler(System.Web.IHttpHandler)" /> method was called after the <see cref="E:System.Web.HttpApplication.MapRequestHandler" /> event occurred.</exception>
		// Token: 0x06000655 RID: 1621 RVA: 0x0000F731 File Offset: 0x0000D931
		public void RemapHandler(IHttpHandler handler)
		{
			if (this.MapRequestHandlerDone)
			{
				throw new InvalidOperationException("The RemapHandler method was called after the MapRequestHandler event occurred.");
			}
			this.Handler = handler;
		}

		/// <summary>Rewrites the URL using the given path.</summary>
		/// <param name="path">The internal rewrite path.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is null.</exception>
		/// <exception cref="T:System.Web.HttpException">The <paramref name="path" /> parameter is not in the current application's root directory.</exception>
		// Token: 0x06000656 RID: 1622 RVA: 0x0000F74D File Offset: 0x0000D94D
		public void RewritePath(string path)
		{
			this.RewritePath(path, true);
		}

		/// <summary>Rewrites the URL by using the given path, path information, and query string information.</summary>
		/// <param name="filePath">The internal rewrite path.</param>
		/// <param name="pathInfo">Additional path information for a resource. For more information, see <see cref="P:System.Web.HttpRequest.PathInfo" />.</param>
		/// <param name="queryString">The request query string.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is not in the current application's root directory.</exception>
		/// <exception cref="T:System.Web.HttpException">The <paramref name="filePath" /> parameter is not in the current application's root directory.</exception>
		// Token: 0x06000657 RID: 1623 RVA: 0x0000F757 File Offset: 0x0000D957
		public void RewritePath(string filePath, string pathInfo, string queryString)
		{
			this.RewritePath(filePath, pathInfo, queryString, false);
		}

		/// <summary>Rewrites the URL using the given path and a Boolean value that specifies whether the virtual path for server resources is modified.</summary>
		/// <param name="path">The internal rewrite path.</param>
		/// <param name="rebaseClientPath">true to reset the virtual path; false to keep the virtual path unchanged.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is null.</exception>
		/// <exception cref="T:System.Web.HttpException">The <paramref name="path" /> parameter is not in the current application's root directory.</exception>
		// Token: 0x06000658 RID: 1624 RVA: 0x0000F764 File Offset: 0x0000D964
		public void RewritePath(string path, bool rebaseClientPath)
		{
			int num = path.IndexOf('?');
			if (num != -1)
			{
				this.RewritePath(path.Substring(0, num), string.Empty, path.Substring(num + 1), rebaseClientPath);
				return;
			}
			this.RewritePath(path, null, null, rebaseClientPath);
		}

		/// <summary>Rewrites the URL using the given virtual path, path information, query string information, and a Boolean value that specifies whether the client file path is set to the rewrite path. </summary>
		/// <param name="filePath">The virtual path to the resource that services the request.</param>
		/// <param name="pathInfo">Additional path information to use for the URL redirect. For more information, see <see cref="P:System.Web.HttpRequest.PathInfo" />.</param>
		/// <param name="queryString">The request query string to use for the URL redirect.</param>
		/// <param name="setClientFilePath">true to set the file path used for client resources to the value of the <paramref name="filePath" /> parameter; otherwise false.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is not in the current application's root directory.</exception>
		/// <exception cref="T:System.Web.HttpException">The <paramref name="filePath" /> parameter is not in the current application's root directory.</exception>
		// Token: 0x06000659 RID: 1625 RVA: 0x0000F7A8 File Offset: 0x0000D9A8
		public void RewritePath(string filePath, string pathInfo, string queryString, bool setClientFilePath)
		{
			if (filePath == null)
			{
				throw new ArgumentNullException("filePath");
			}
			if (!VirtualPathUtility.IsValidVirtualPath(filePath))
			{
				throw new HttpException("'" + HttpUtility.HtmlEncode(filePath) + "' is not a valid virtual path.");
			}
			filePath = VirtualPathUtility.Canonize(filePath);
			bool flag = VirtualPathUtility.IsAppRelative(filePath);
			bool flag2 = !flag && VirtualPathUtility.IsAbsolute(filePath);
			HttpRequest httpRequest = this.Request;
			if (httpRequest == null)
			{
				return;
			}
			if (flag || flag2)
			{
				if (flag)
				{
					filePath = VirtualPathUtility.ToAbsolute(filePath);
				}
			}
			else
			{
				filePath = VirtualPathUtility.AppendTrailingSlash(httpRequest.BaseVirtualDir) + filePath;
			}
			if (!StrUtils.StartsWith(filePath, HttpRuntime.AppDomainAppVirtualPath))
			{
				throw new HttpException(404, "The virtual path '" + HttpUtility.HtmlEncode(filePath) + "' maps to another application.", filePath);
			}
			httpRequest.SetCurrentExePath(filePath);
			httpRequest.SetFilePath(filePath);
			if (setClientFilePath)
			{
				httpRequest.ClientFilePath = filePath;
			}
			if (pathInfo != null)
			{
				httpRequest.SetPathInfo(pathInfo);
			}
			if (queryString != null)
			{
				httpRequest.QueryStringRaw = queryString;
			}
		}

		/// <summary>Sets the type of session state behavior that is required in order to support an HTTP request.</summary>
		/// <param name="sessionStateBehavior">One of the enumeration values that specifies what type of session state behavior is required.</param>
		/// <exception cref="T:System.InvalidOperationException">The method was called after the <see cref="E:System.Web.HttpApplication.AcquireRequestState" /> event was raised. </exception>
		// Token: 0x0600065A RID: 1626 RVA: 0x0000F88D File Offset: 0x0000DA8D
		public void SetSessionStateBehavior(SessionStateBehavior sessionStateBehavior)
		{
			this.SessionStateBehavior = sessionStateBehavior;
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0000F896 File Offset: 0x0000DA96
		internal void SetSession(HttpSessionState state)
		{
			this.session_state = state;
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x0000F89F File Offset: 0x0000DA9F
		// (set) Token: 0x0600065D RID: 1629 RVA: 0x0000F8A7 File Offset: 0x0000DAA7
		internal string ErrorPage
		{
			get
			{
				return this.error_page;
			}
			set
			{
				this.error_page = value;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x0000F8B0 File Offset: 0x0000DAB0
		// (set) Token: 0x0600065F RID: 1631 RVA: 0x0000F8DC File Offset: 0x0000DADC
		internal TimeSpan ConfigTimeout
		{
			get
			{
				if (this.config_timeout == null)
				{
					this.config_timeout = HttpRuntime.Section.ExecutionTimeout;
				}
				return (TimeSpan)this.config_timeout;
			}
			set
			{
				this.config_timeout = value;
				if (this.timer != null)
				{
					long num = Math.Max((long)(value - (DateTime.UtcNow - this.time_stamp)).TotalMilliseconds, 0L);
					if (num > (long)((ulong)(-2)))
					{
						num = (long)((ulong)(-2));
					}
					this.timer.Change(num, -1L);
				}
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x0000F93D File Offset: 0x0000DB3D
		// (set) Token: 0x06000661 RID: 1633 RVA: 0x0000F945 File Offset: 0x0000DB45
		internal SessionStateBehavior SessionStateBehavior { get; private set; }

		// Token: 0x06000662 RID: 1634 RVA: 0x0000F950 File Offset: 0x0000DB50
		private void TimeoutReached(object state)
		{
			HttpRuntime.QueuePendingRequest(false);
			if (Interlocked.CompareExchange(ref this.timeout_possible, 0, 0) == 0)
			{
				if (this.timer != null)
				{
					this.timer.Change(2000, 0);
				}
				return;
			}
			this.StopTimeoutTimer();
			this.thread.Abort(new StepTimeout());
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0000F9A4 File Offset: 0x0000DBA4
		internal void StartTimeoutTimer()
		{
			this.thread = Thread.CurrentThread;
			this.timer = new Timer(new TimerCallback(this.TimeoutReached), null, (int)this.ConfigTimeout.TotalMilliseconds, -1);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0000F9E4 File Offset: 0x0000DBE4
		internal void StopTimeoutTimer()
		{
			if (this.timer != null)
			{
				this.timer.Dispose();
				this.timer = null;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x0000FA00 File Offset: 0x0000DC00
		internal bool TimeoutPossible
		{
			get
			{
				return Interlocked.CompareExchange(ref this.timeout_possible, 1, 1) == 1;
			}
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0000FA12 File Offset: 0x0000DC12
		internal void BeginTimeoutPossible()
		{
			this.timeout_possible = 1;
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0000FA1B File Offset: 0x0000DC1B
		internal void EndTimeoutPossible()
		{
			Interlocked.CompareExchange(ref this.timeout_possible, 0, 1);
		}

		/// <summary>Gets or sets a value that indicates whether asynchronous operations are allowed during parts of ASP.NET request processing when they are not expected.</summary>
		/// <returns>false if ASP.NET will throw an exception when the asynchronous API is used at a time when it is not expected; otherwise, true. The default value is false.</returns>
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x0000FA2C File Offset: 0x0000DC2C
		// (set) Token: 0x06000669 RID: 1641 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public bool AllowAsyncDuringSyncStages
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

		/// <summary>Gets or sets an object that contains flags that pertain to asynchronous preload mode. </summary>
		/// <returns>An object that contains flags that pertain to asynchronous preload mode.</returns>
		// Token: 0x1700026A RID: 618
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x0000FA48 File Offset: 0x0000DC48
		// (set) Token: 0x0600066B RID: 1643 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public AsyncPreloadModeFlags AsyncPreloadMode
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
		// Token: 0x1700026B RID: 619
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x0000FA64 File Offset: 0x0000DC64
		public bool IsWebSocketRequest
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a value that indicates whether the connection is upgrading from an HTTP connection to an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> connection.</summary>
		/// <returns>true if the connection is upgrading; otherwise, false.</returns>
		// Token: 0x1700026C RID: 620
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x0000FA80 File Offset: 0x0000DC80
		public bool IsWebSocketRequestUpgrading
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a reference to the page-instrumentation service instance for this request.</summary>
		/// <returns>The page-instrumentation service instance for this request.</returns>
		// Token: 0x1700026D RID: 621
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public PageInstrumentationService PageInstrumentation
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets a value that specifies whether the ASP.NET runtime should call <see cref="M:System.Threading.Thread.Abort" /> on the thread that is servicing this request when the request times out.</summary>
		/// <returns>true if <see cref="M:System.Threading.Thread.Abort" /> will be called when the thread times out; otherwise, false. The default is true.</returns>
		// Token: 0x1700026E RID: 622
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x0000FA9C File Offset: 0x0000DC9C
		// (set) Token: 0x06000670 RID: 1648 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public bool ThreadAbortOnTimeout
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

		/// <summary>Gets the negotiated protocol that was sent from the server to the client for an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> connection.</summary>
		/// <returns>The negotiated protocol.</returns>
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string WebSocketNegotiatedProtocol
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the ordered list of protocols requested by the client.</summary>
		/// <returns>The requested protocols, or null if this is not an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> request or if no list is present.</returns>
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x0000FAB7 File Offset: 0x0000DCB7
		public IList<string> WebSocketRequestedProtocols
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
		// Token: 0x06000673 RID: 1651 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void AcceptWebSocketRequest(Func<AspNetWebSocketContext, Task> userFunc)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Accepts an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> request using the specified user function and options object.</summary>
		/// <param name="userFunc">The user function.</param>
		/// <param name="options">The options object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="userFunc" /> parameter is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The request is not an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> request.</exception>
		// Token: 0x06000674 RID: 1652 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void AcceptWebSocketRequest(Func<AspNetWebSocketContext, Task> userFunc, AspNetWebSocketOptions options)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Raises a virtual event that occurs when the HTTP part of the request is ending.</summary>
		/// <returns>The subscription token.</returns>
		/// <param name="callback">The HTTP context object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="callback" /> parameter is null.</exception>
		// Token: 0x06000675 RID: 1653 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public ISubscriptionToken AddOnRequestCompleted(Action<HttpContext> callback)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Enables an object's <see cref="M:System.IDisposable.Dispose" /> method to be called when the <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> connection part of this request is completed.</summary>
		/// <returns>The subscription token.</returns>
		/// <param name="target">The object whose <see cref="M:System.IDisposable.Dispose" /> method must be called when the <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> connection part of the request is completed.</param>
		// Token: 0x06000676 RID: 1654 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public ISubscriptionToken DisposeOnPipelineCompleted(IDisposable target)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x04000F33 RID: 3891
		internal HttpWorkerRequest WorkerRequest;

		// Token: 0x04000F34 RID: 3892
		private HttpApplication app_instance;

		// Token: 0x04000F35 RID: 3893
		private HttpRequest request;

		// Token: 0x04000F36 RID: 3894
		private HttpResponse response;

		// Token: 0x04000F37 RID: 3895
		private HttpSessionState session_state;

		// Token: 0x04000F38 RID: 3896
		private HttpServerUtility server;

		// Token: 0x04000F39 RID: 3897
		private TraceContext trace_context;

		// Token: 0x04000F3A RID: 3898
		private IHttpHandler handler;

		// Token: 0x04000F3B RID: 3899
		private string error_page;

		// Token: 0x04000F3C RID: 3900
		private bool skip_authorization;

		// Token: 0x04000F3D RID: 3901
		private IPrincipal user;

		// Token: 0x04000F3E RID: 3902
		private object errors;

		// Token: 0x04000F3F RID: 3903
		private Hashtable items;

		// Token: 0x04000F40 RID: 3904
		private object config_timeout;

		// Token: 0x04000F41 RID: 3905
		private int timeout_possible;

		// Token: 0x04000F42 RID: 3906
		private DateTime time_stamp = DateTime.UtcNow;

		// Token: 0x04000F43 RID: 3907
		private Timer timer;

		// Token: 0x04000F44 RID: 3908
		private Thread thread;

		// Token: 0x04000F45 RID: 3909
		private bool _isProcessingInclude;

		// Token: 0x04000F46 RID: 3910
		[ThreadStatic]
		private static ResourceProviderFactory provider_factory;

		// Token: 0x04000F47 RID: 3911
		[ThreadStatic]
		private static DefaultResourceProviderFactory default_provider_factory;

		// Token: 0x04000F48 RID: 3912
		[ThreadStatic]
		private static Dictionary<string, IResourceProvider> resource_providers;

		// Token: 0x04000F49 RID: 3913
		internal static Assembly AppGlobalResourcesAssembly;

		// Token: 0x04000F4A RID: 3914
		private ProfileBase profile;

		// Token: 0x04000F4B RID: 3915
		private LinkedList<IHttpHandler> handlers;
	}
}
