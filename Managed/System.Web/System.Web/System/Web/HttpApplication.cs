using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;
using System.Web.Caching;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.SessionState;
using System.Web.Util;
using Unity;

namespace System.Web
{
	/// <summary>Defines the methods, properties, and events that are common to all application objects in an ASP.NET application. This class is the base class for applications that are defined by the user in the Global.asax file.</summary>
	// Token: 0x02000077 RID: 119
	[ToolboxItem(false)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HttpApplication : IHttpAsyncHandler, IHttpHandler, IComponent, IDisposable
	{
		/// <summary>Occurs when the application is disposed.</summary>
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600048C RID: 1164 RVA: 0x00009593 File Offset: 0x00007793
		// (remove) Token: 0x0600048D RID: 1165 RVA: 0x000095A6 File Offset: 0x000077A6
		public virtual event EventHandler Disposed
		{
			add
			{
				this.nonApplicationEvents.AddHandler(HttpApplication.disposedEvent, value);
			}
			remove
			{
				this.nonApplicationEvents.RemoveHandler(HttpApplication.disposedEvent, value);
			}
		}

		/// <summary>Occurs when an unhandled exception is thrown.</summary>
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600048E RID: 1166 RVA: 0x000095B9 File Offset: 0x000077B9
		// (remove) Token: 0x0600048F RID: 1167 RVA: 0x000095CC File Offset: 0x000077CC
		public virtual event EventHandler Error
		{
			add
			{
				this.nonApplicationEvents.AddHandler(HttpApplication.errorEvent, value);
			}
			remove
			{
				this.nonApplicationEvents.RemoveHandler(HttpApplication.errorEvent, value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpApplication" /> class.</summary>
		// Token: 0x06000490 RID: 1168 RVA: 0x000095DF File Offset: 0x000077DF
		public HttpApplication()
		{
			this.done = new ManualResetEvent(false);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000960C File Offset: 0x0000780C
		internal void InitOnce(bool full_init)
		{
			if (HttpApplication.initialization_exception != null)
			{
				return;
			}
			if (this.modcoll != null)
			{
				return;
			}
			object obj = this.this_lock;
			lock (obj)
			{
				if (HttpApplication.initialization_exception == null)
				{
					if (this.modcoll == null)
					{
						bool flag2 = this.context == null;
						try
						{
							HttpModulesSection httpModulesSection = (HttpModulesSection)WebConfigurationManager.GetWebApplicationSection("system.web/httpModules");
							HttpContext httpContext = HttpContext.Current;
							HttpContext.Current = new HttpContext(new SimpleWorkerRequest(string.Empty, string.Empty, new StringWriter()));
							if (this.context == null)
							{
								this.context = HttpContext.Current;
							}
							HttpModuleCollection httpModuleCollection = httpModulesSection.LoadModules(this);
							HttpModuleCollection httpModuleCollection2 = this.CreateDynamicModules();
							for (int i = 0; i < httpModuleCollection2.Count; i++)
							{
								httpModuleCollection.AddModule(httpModuleCollection2.GetKey(i), httpModuleCollection2.Get(i));
							}
							Interlocked.CompareExchange<HttpModuleCollection>(ref this.modcoll, httpModuleCollection, null);
							HttpContext.Current = httpContext;
							if (full_init)
							{
								HttpApplicationFactory.AttachEvents(this);
								this.Init();
								this.fullInitComplete = true;
							}
						}
						catch (Exception ex)
						{
							HttpApplication.initialization_exception = ex;
							Console.Error.WriteLine("Exception while initOnce: " + ex.ToString());
							Console.Error.WriteLine("Please restart your app to unlock it");
						}
						finally
						{
							if (flag2)
							{
								this.context = null;
							}
						}
					}
				}
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x000097B0 File Offset: 0x000079B0
		// (set) Token: 0x06000493 RID: 1171 RVA: 0x000097B8 File Offset: 0x000079B8
		internal bool InApplicationStart
		{
			get
			{
				return this.in_application_start;
			}
			set
			{
				this.in_application_start = value;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x000097C1 File Offset: 0x000079C1
		internal string AssemblyLocation
		{
			get
			{
				if (this.assemblyLocation == null)
				{
					this.assemblyLocation = base.GetType().Assembly.Location;
				}
				return this.assemblyLocation;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x000097E7 File Offset: 0x000079E7
		internal static Exception InitializationException
		{
			get
			{
				return HttpApplication.initialization_exception;
			}
		}

		/// <summary>Gets the current state of an application.</summary>
		/// <returns>The <see cref="T:System.Web.HttpApplicationState" /> for the current request.</returns>
		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x000097F0 File Offset: 0x000079F0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public HttpApplicationState Application
		{
			get
			{
				return HttpApplicationFactory.ApplicationState;
			}
		}

		/// <summary>Gets HTTP-specific information about the current request.</summary>
		/// <returns>The <see cref="T:System.Web.HttpContext" /> for the current request.</returns>
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x000097F7 File Offset: 0x000079F7
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HttpContext Context
		{
			get
			{
				return this.context;
			}
		}

		/// <summary>Gets the list of event handler delegates that process all application events.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventHandlerList" /> that contains the names of the event handler delegates.</returns>
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x000097FF File Offset: 0x000079FF
		protected EventHandlerList Events
		{
			get
			{
				if (this.events == null)
				{
					this.events = new EventHandlerList();
				}
				return this.events;
			}
		}

		/// <summary>Gets the collection of modules for the current application.</summary>
		/// <returns>An <see cref="T:System.Web.HttpModuleCollection" /> that contains the names of the modules for the application.</returns>
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x0000981A File Offset: 0x00007A1A
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HttpModuleCollection Modules
		{
			[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
			get
			{
				if (this.modcoll == null)
				{
					this.modcoll = new HttpModuleCollection();
				}
				return this.modcoll;
			}
		}

		/// <summary>Gets the intrinsic request object for the current request.</summary>
		/// <returns>The <see cref="T:System.Web.HttpRequest" /> object that the application is processing.</returns>
		/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.HttpRequest" /> object is null.</exception>
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x0000983C File Offset: 0x00007A3C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public HttpRequest Request
		{
			get
			{
				if (this.context == null)
				{
					throw HttpException.NewWithCode(global::Locale.GetText("No context is available."), 3001);
				}
				if (!HttpApplicationFactory.ContextAvailable)
				{
					throw HttpException.NewWithCode(global::Locale.GetText("Request is not available in this context."), 3001);
				}
				return this.context.Request;
			}
		}

		/// <summary>Gets the intrinsic response object for the current request.</summary>
		/// <returns>The <see cref="T:System.Web.HttpResponse" /> object that the application is processing.</returns>
		/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.HttpResponse" /> object is null. </exception>
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x00009890 File Offset: 0x00007A90
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public HttpResponse Response
		{
			get
			{
				if (this.context == null)
				{
					throw HttpException.NewWithCode(global::Locale.GetText("No context is available."), 3001);
				}
				if (!HttpApplicationFactory.ContextAvailable)
				{
					throw HttpException.NewWithCode(global::Locale.GetText("Response is not available in this context."), 3001);
				}
				return this.context.Response;
			}
		}

		/// <summary>Gets the intrinsic server object for the current request.</summary>
		/// <returns>The <see cref="T:System.Web.HttpServerUtility" /> object that the application is processing.</returns>
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x000098E1 File Offset: 0x00007AE1
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HttpServerUtility Server
		{
			get
			{
				if (this.context != null)
				{
					return this.context.Server;
				}
				return new HttpServerUtility(null);
			}
		}

		/// <summary>Gets the intrinsic session object that provides access to session data.</summary>
		/// <returns>The <see cref="T:System.Web.SessionState.HttpSessionState" /> object for the current session.</returns>
		/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.SessionState.HttpSessionState" /> object is null. </exception>
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x00009900 File Offset: 0x00007B00
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public HttpSessionState Session
		{
			get
			{
				if (this.session != null)
				{
					return this.session;
				}
				if (this.context == null)
				{
					throw HttpException.NewWithCode(global::Locale.GetText("No context is available."), 3001);
				}
				HttpSessionState httpSessionState = this.context.Session;
				if (httpSessionState == null)
				{
					throw HttpException.NewWithCode(global::Locale.GetText("Session state is not available in the context."), 3001);
				}
				return httpSessionState;
			}
		}

		/// <summary>Gets or sets a site interface for an <see cref="T:System.ComponentModel.IComponent" /> implementation.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ISite" /> object that allows a container to manage and communicate with its child components.</returns>
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0000995C File Offset: 0x00007B5C
		// (set) Token: 0x0600049F RID: 1183 RVA: 0x00009964 File Offset: 0x00007B64
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ISite Site
		{
			get
			{
				return this.isite;
			}
			set
			{
				this.isite = value;
			}
		}

		/// <summary>Gets the intrinsic user object for the current request.</summary>
		/// <returns>The <see cref="T:System.Security.Principal.IPrincipal" /> object that represents the current authenticated or anonymous user.</returns>
		/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Security.Principal.IPrincipal" /> object is null. </exception>
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00009970 File Offset: 0x00007B70
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public IPrincipal User
		{
			get
			{
				if (this.context == null)
				{
					throw new HttpException(global::Locale.GetText("No context is available."));
				}
				if (this.context.User == null)
				{
					throw new HttpException(global::Locale.GetText("No currently authenticated user."));
				}
				return this.context.User;
			}
		}

		/// <summary>Occurs just before ASP.NET sends HTTP headers to the client.</summary>
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060004A1 RID: 1185 RVA: 0x000099BD File Offset: 0x00007BBD
		// (remove) Token: 0x060004A2 RID: 1186 RVA: 0x000099CB File Offset: 0x00007BCB
		public event EventHandler PreSendRequestHeaders
		{
			add
			{
				this.AddEventHandler(HttpApplication.PreSendRequestHeadersEvent, value);
			}
			remove
			{
				this.RemoveEventHandler(HttpApplication.PreSendRequestHeadersEvent, value);
			}
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x000099DC File Offset: 0x00007BDC
		internal void TriggerPreSendRequestHeaders()
		{
			EventHandler eventHandler = this.Events[HttpApplication.PreSendRequestHeadersEvent] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		/// <summary>Occurs just before ASP.NET sends content to the client.</summary>
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060004A4 RID: 1188 RVA: 0x00009A0E File Offset: 0x00007C0E
		// (remove) Token: 0x060004A5 RID: 1189 RVA: 0x00009A1C File Offset: 0x00007C1C
		public event EventHandler PreSendRequestContent
		{
			add
			{
				this.AddEventHandler(HttpApplication.PreSendRequestContentEvent, value);
			}
			remove
			{
				this.RemoveEventHandler(HttpApplication.PreSendRequestContentEvent, value);
			}
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00009A2C File Offset: 0x00007C2C
		internal void TriggerPreSendRequestContent()
		{
			EventHandler eventHandler = this.Events[HttpApplication.PreSendRequestContentEvent] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		/// <summary>Occurs when ASP.NET acquires the current state (for example, session state) that is associated with the current request.</summary>
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060004A7 RID: 1191 RVA: 0x00009A5E File Offset: 0x00007C5E
		// (remove) Token: 0x060004A8 RID: 1192 RVA: 0x00009A6C File Offset: 0x00007C6C
		public event EventHandler AcquireRequestState
		{
			add
			{
				this.AddEventHandler(HttpApplication.AcquireRequestStateEvent, value);
			}
			remove
			{
				this.RemoveEventHandler(HttpApplication.AcquireRequestStateEvent, value);
			}
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.AcquireRequestState" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.AcquireRequestState" /> event handlers for the current request.</summary>
		/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.AcquireRequestState" />. </param>
		/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.AcquireRequestState" />. </param>
		// Token: 0x060004A9 RID: 1193 RVA: 0x00009A7C File Offset: 0x00007C7C
		public void AddOnAcquireRequestStateAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			AsyncInvoker asyncInvoker = new AsyncInvoker(bh, eh, this);
			this.AcquireRequestState += asyncInvoker.Invoke;
		}

		/// <summary>Occurs when a security module has established the identity of the user.</summary>
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060004AA RID: 1194 RVA: 0x00009AA4 File Offset: 0x00007CA4
		// (remove) Token: 0x060004AB RID: 1195 RVA: 0x00009AB2 File Offset: 0x00007CB2
		public event EventHandler AuthenticateRequest
		{
			add
			{
				this.AddEventHandler(HttpApplication.AuthenticateRequestEvent, value);
			}
			remove
			{
				this.RemoveEventHandler(HttpApplication.AuthenticateRequestEvent, value);
			}
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.AuthenticateRequest" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.AuthenticateRequest" /> event handlers for the current request.</summary>
		/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.AuthenticateRequest" />. </param>
		/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.AuthenticateRequest" />. </param>
		// Token: 0x060004AC RID: 1196 RVA: 0x00009AC0 File Offset: 0x00007CC0
		public void AddOnAuthenticateRequestAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			AsyncInvoker asyncInvoker = new AsyncInvoker(bh, eh, this);
			this.AuthenticateRequest += asyncInvoker.Invoke;
		}

		/// <summary>Occurs when a security module has verified user authorization.</summary>
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060004AD RID: 1197 RVA: 0x00009AE8 File Offset: 0x00007CE8
		// (remove) Token: 0x060004AE RID: 1198 RVA: 0x00009AF6 File Offset: 0x00007CF6
		public event EventHandler AuthorizeRequest
		{
			add
			{
				this.AddEventHandler(HttpApplication.AuthorizeRequestEvent, value);
			}
			remove
			{
				this.RemoveEventHandler(HttpApplication.AuthorizeRequestEvent, value);
			}
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.AuthorizeRequest" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.AuthorizeRequest" /> event handlers for the current request.</summary>
		/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.AuthorizeRequest" />. </param>
		/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.AuthorizeRequest" />. </param>
		// Token: 0x060004AF RID: 1199 RVA: 0x00009B04 File Offset: 0x00007D04
		public void AddOnAuthorizeRequestAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			AsyncInvoker asyncInvoker = new AsyncInvoker(bh, eh, this);
			this.AuthorizeRequest += asyncInvoker.Invoke;
		}

		/// <summary>Occurs as the first event in the HTTP pipeline chain of execution when ASP.NET responds to a request.</summary>
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060004B0 RID: 1200 RVA: 0x00009B2C File Offset: 0x00007D2C
		// (remove) Token: 0x060004B1 RID: 1201 RVA: 0x00009B43 File Offset: 0x00007D43
		public event EventHandler BeginRequest
		{
			add
			{
				if (this.InApplicationStart)
				{
					return;
				}
				this.AddEventHandler(HttpApplication.BeginRequestEvent, value);
			}
			remove
			{
				if (this.InApplicationStart)
				{
					return;
				}
				this.RemoveEventHandler(HttpApplication.BeginRequestEvent, value);
			}
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.BeginRequest" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.BeginRequest" /> event handlers for the current request.</summary>
		/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.BeginRequest" />. </param>
		/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.BeginRequest" />. </param>
		// Token: 0x060004B2 RID: 1202 RVA: 0x00009B5C File Offset: 0x00007D5C
		public void AddOnBeginRequestAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			AsyncInvoker asyncInvoker = new AsyncInvoker(bh, eh, this);
			this.BeginRequest += asyncInvoker.Invoke;
		}

		/// <summary>Occurs as the last event in the HTTP pipeline chain of execution when ASP.NET responds to a request.</summary>
		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060004B3 RID: 1203 RVA: 0x00009B84 File Offset: 0x00007D84
		// (remove) Token: 0x060004B4 RID: 1204 RVA: 0x00009B9B File Offset: 0x00007D9B
		public event EventHandler EndRequest
		{
			add
			{
				if (this.InApplicationStart)
				{
					return;
				}
				this.AddEventHandler(HttpApplication.EndRequestEvent, value);
			}
			remove
			{
				if (this.InApplicationStart)
				{
					return;
				}
				this.RemoveEventHandler(HttpApplication.EndRequestEvent, value);
			}
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.EndRequest" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.EndRequest" /> event handlers for the current request.</summary>
		/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.EndRequest" />. </param>
		/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.EndRequest" />. </param>
		// Token: 0x060004B5 RID: 1205 RVA: 0x00009BB4 File Offset: 0x00007DB4
		public void AddOnEndRequestAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			AsyncInvoker asyncInvoker = new AsyncInvoker(bh, eh, this);
			this.EndRequest += asyncInvoker.Invoke;
		}

		/// <summary>Occurs when the ASP.NET event handler (for example, a page or an XML Web service) finishes execution.</summary>
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060004B6 RID: 1206 RVA: 0x00009BDC File Offset: 0x00007DDC
		// (remove) Token: 0x060004B7 RID: 1207 RVA: 0x00009BEA File Offset: 0x00007DEA
		public event EventHandler PostRequestHandlerExecute
		{
			add
			{
				this.AddEventHandler(HttpApplication.PostRequestHandlerExecuteEvent, value);
			}
			remove
			{
				this.RemoveEventHandler(HttpApplication.PostRequestHandlerExecuteEvent, value);
			}
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostRequestHandlerExecute" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostRequestHandlerExecute" /> event handlers for the current request.</summary>
		/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostRequestHandlerExecute" />. </param>
		/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostRequestHandlerExecute" />. </param>
		// Token: 0x060004B8 RID: 1208 RVA: 0x00009BF8 File Offset: 0x00007DF8
		public void AddOnPostRequestHandlerExecuteAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			AsyncInvoker asyncInvoker = new AsyncInvoker(bh, eh, this);
			this.PostRequestHandlerExecute += asyncInvoker.Invoke;
		}

		/// <summary>Occurs just before ASP.NET starts executing an event handler (for example, a page or an XML Web service).</summary>
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060004B9 RID: 1209 RVA: 0x00009C20 File Offset: 0x00007E20
		// (remove) Token: 0x060004BA RID: 1210 RVA: 0x00009C2E File Offset: 0x00007E2E
		public event EventHandler PreRequestHandlerExecute
		{
			add
			{
				this.AddEventHandler(HttpApplication.PreRequestHandlerExecuteEvent, value);
			}
			remove
			{
				this.RemoveEventHandler(HttpApplication.PreRequestHandlerExecuteEvent, value);
			}
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PreRequestHandlerExecute" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PreRequestHandlerExecute" /> event handlers for the current request.</summary>
		/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PreRequestHandlerExecute" />. </param>
		/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PreRequestHandlerExecute" />. </param>
		// Token: 0x060004BB RID: 1211 RVA: 0x00009C3C File Offset: 0x00007E3C
		public void AddOnPreRequestHandlerExecuteAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			AsyncInvoker asyncInvoker = new AsyncInvoker(bh, eh, this);
			this.PreRequestHandlerExecute += asyncInvoker.Invoke;
		}

		/// <summary>Occurs after ASP.NET finishes executing all request event handlers. This event causes state modules to save the current state data.</summary>
		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060004BC RID: 1212 RVA: 0x00009C64 File Offset: 0x00007E64
		// (remove) Token: 0x060004BD RID: 1213 RVA: 0x00009C72 File Offset: 0x00007E72
		public event EventHandler ReleaseRequestState
		{
			add
			{
				this.AddEventHandler(HttpApplication.ReleaseRequestStateEvent, value);
			}
			remove
			{
				this.RemoveEventHandler(HttpApplication.ReleaseRequestStateEvent, value);
			}
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.ReleaseRequestState" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.ReleaseRequestState" /> event handlers for the current request.</summary>
		/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.ReleaseRequestState" />. </param>
		/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.ReleaseRequestState" />. </param>
		// Token: 0x060004BE RID: 1214 RVA: 0x00009C80 File Offset: 0x00007E80
		public void AddOnReleaseRequestStateAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			AsyncInvoker asyncInvoker = new AsyncInvoker(bh, eh, this);
			this.ReleaseRequestState += asyncInvoker.Invoke;
		}

		/// <summary>Occurs when ASP.NET finishes an authorization event to let the caching modules serve requests from the cache, bypassing execution of the event handler (for example, a page or an XML Web service).</summary>
		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060004BF RID: 1215 RVA: 0x00009CA8 File Offset: 0x00007EA8
		// (remove) Token: 0x060004C0 RID: 1216 RVA: 0x00009CB6 File Offset: 0x00007EB6
		public event EventHandler ResolveRequestCache
		{
			add
			{
				this.AddEventHandler(HttpApplication.ResolveRequestCacheEvent, value);
			}
			remove
			{
				this.RemoveEventHandler(HttpApplication.ResolveRequestCacheEvent, value);
			}
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.ResolveRequestCache" /> event handler to the collection of asynchronous <see cref="E:System.Web.HttpApplication.ResolveRequestCache" /> event handlers for the current request.</summary>
		/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.ResolveRequestCache" />. </param>
		/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.ResolveRequestCache" />. </param>
		// Token: 0x060004C1 RID: 1217 RVA: 0x00009CC4 File Offset: 0x00007EC4
		public void AddOnResolveRequestCacheAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			AsyncInvoker asyncInvoker = new AsyncInvoker(bh, eh, this);
			this.ResolveRequestCache += asyncInvoker.Invoke;
		}

		/// <summary>Occurs when ASP.NET finishes executing an event handler in order to let caching modules store responses that will be used to serve subsequent requests from the cache.</summary>
		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060004C2 RID: 1218 RVA: 0x00009CEC File Offset: 0x00007EEC
		// (remove) Token: 0x060004C3 RID: 1219 RVA: 0x00009CFA File Offset: 0x00007EFA
		public event EventHandler UpdateRequestCache
		{
			add
			{
				this.AddEventHandler(HttpApplication.UpdateRequestCacheEvent, value);
			}
			remove
			{
				this.RemoveEventHandler(HttpApplication.UpdateRequestCacheEvent, value);
			}
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.UpdateRequestCache" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.UpdateRequestCache" /> event handlers for the current request.</summary>
		/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.UpdateRequestCache" />. </param>
		/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.UpdateRequestCache" />. </param>
		// Token: 0x060004C4 RID: 1220 RVA: 0x00009D08 File Offset: 0x00007F08
		public void AddOnUpdateRequestCacheAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			AsyncInvoker asyncInvoker = new AsyncInvoker(bh, eh, this);
			this.UpdateRequestCache += asyncInvoker.Invoke;
		}

		/// <summary>Occurs when a security module has established the identity of the user.</summary>
		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060004C5 RID: 1221 RVA: 0x00009D30 File Offset: 0x00007F30
		// (remove) Token: 0x060004C6 RID: 1222 RVA: 0x00009D3E File Offset: 0x00007F3E
		public event EventHandler PostAuthenticateRequest
		{
			add
			{
				this.AddEventHandler(HttpApplication.PostAuthenticateRequestEvent, value);
			}
			remove
			{
				this.RemoveEventHandler(HttpApplication.PostAuthenticateRequestEvent, value);
			}
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostAuthenticateRequest" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostAuthenticateRequest" /> event handlers for the current request.</summary>
		/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostAuthenticateRequest" />. </param>
		/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostAuthenticateRequest" />. </param>
		// Token: 0x060004C7 RID: 1223 RVA: 0x00009D4C File Offset: 0x00007F4C
		public void AddOnPostAuthenticateRequestAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			this.AddOnPostAuthenticateRequestAsync(bh, eh, null);
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostAuthorizeRequest" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostAuthorizeRequest" /> event handlers for the current request.</summary>
		/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostAuthorizeRequest" />. </param>
		/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostAuthorizeRequest" />. </param>
		/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.PostAuthorizeRequest" />.</param>
		// Token: 0x060004C8 RID: 1224 RVA: 0x00009D58 File Offset: 0x00007F58
		public void AddOnPostAuthenticateRequestAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			AsyncInvoker asyncInvoker = new AsyncInvoker(beginHandler, endHandler, this, state);
			this.PostAuthenticateRequest += asyncInvoker.Invoke;
		}

		/// <summary>Occurs when the user for the current request has been authorized.</summary>
		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060004C9 RID: 1225 RVA: 0x00009D81 File Offset: 0x00007F81
		// (remove) Token: 0x060004CA RID: 1226 RVA: 0x00009D8F File Offset: 0x00007F8F
		public event EventHandler PostAuthorizeRequest
		{
			add
			{
				this.AddEventHandler(HttpApplication.PostAuthorizeRequestEvent, value);
			}
			remove
			{
				this.RemoveEventHandler(HttpApplication.PostAuthorizeRequestEvent, value);
			}
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostAuthorizeRequest" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostAuthorizeRequest" /> event handlers for the current request.</summary>
		/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostAuthorizeRequest" />. </param>
		/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostAuthorizeRequest" />. </param>
		// Token: 0x060004CB RID: 1227 RVA: 0x00009D9D File Offset: 0x00007F9D
		public void AddOnPostAuthorizeRequestAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			this.AddOnPostAuthorizeRequestAsync(bh, eh, null);
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostAuthorizeRequest" /> to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostAuthorizeRequest" /> event handlers for the current request.</summary>
		/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostAuthorizeRequest" />. </param>
		/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostAuthorizeRequest" />. </param>
		/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.PostAuthorizeRequest" />.</param>
		// Token: 0x060004CC RID: 1228 RVA: 0x00009DA8 File Offset: 0x00007FA8
		public void AddOnPostAuthorizeRequestAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			AsyncInvoker asyncInvoker = new AsyncInvoker(beginHandler, endHandler, this, state);
			this.PostAuthorizeRequest += asyncInvoker.Invoke;
		}

		/// <summary>Occurs when ASP.NET bypasses execution of the current event handler and allows a caching module to serve a request from the cache.</summary>
		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060004CD RID: 1229 RVA: 0x00009DD1 File Offset: 0x00007FD1
		// (remove) Token: 0x060004CE RID: 1230 RVA: 0x00009DDF File Offset: 0x00007FDF
		public event EventHandler PostResolveRequestCache
		{
			add
			{
				this.AddEventHandler(HttpApplication.PostResolveRequestCacheEvent, value);
			}
			remove
			{
				this.RemoveEventHandler(HttpApplication.PostResolveRequestCacheEvent, value);
			}
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostResolveRequestCache" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostResolveRequestCache" /> event handlers for the current request.</summary>
		/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostResolveRequestCache" />. </param>
		/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostResolveRequestCache" />. </param>
		// Token: 0x060004CF RID: 1231 RVA: 0x00009DED File Offset: 0x00007FED
		public void AddOnPostResolveRequestCacheAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			this.AddOnPostResolveRequestCacheAsync(bh, eh, null);
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostResolveRequestCache" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostResolveRequestCache" /> event handlers for the current request.</summary>
		/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostResolveRequestCache" />. </param>
		/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostResolveRequestCache" />. </param>
		/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.PostResolveRequestCache" />.</param>
		// Token: 0x060004D0 RID: 1232 RVA: 0x00009DF8 File Offset: 0x00007FF8
		public void AddOnPostResolveRequestCacheAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			AsyncInvoker asyncInvoker = new AsyncInvoker(beginHandler, endHandler, this, state);
			this.PostResolveRequestCache += asyncInvoker.Invoke;
		}

		/// <summary>Occurs when ASP.NET has mapped the current request to the appropriate event handler.</summary>
		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060004D1 RID: 1233 RVA: 0x00009E21 File Offset: 0x00008021
		// (remove) Token: 0x060004D2 RID: 1234 RVA: 0x00009E2F File Offset: 0x0000802F
		public event EventHandler PostMapRequestHandler
		{
			add
			{
				this.AddEventHandler(HttpApplication.PostMapRequestHandlerEvent, value);
			}
			remove
			{
				this.RemoveEventHandler(HttpApplication.PostMapRequestHandlerEvent, value);
			}
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostMapRequestHandler" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostMapRequestHandler" /> event handlers for the current request.</summary>
		/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostMapRequestHandler" />. </param>
		/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostMapRequestHandler" />. </param>
		// Token: 0x060004D3 RID: 1235 RVA: 0x00009E3D File Offset: 0x0000803D
		public void AddOnPostMapRequestHandlerAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			this.AddOnPostMapRequestHandlerAsync(bh, eh, null);
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostMapRequestHandler" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostMapRequestHandler" /> event handlers for the current request.</summary>
		/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostMapRequestHandler" />. </param>
		/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostMapRequestHandler" />. </param>
		/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.PostMapRequestHandler" /> collection.</param>
		// Token: 0x060004D4 RID: 1236 RVA: 0x00009E48 File Offset: 0x00008048
		public void AddOnPostMapRequestHandlerAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			AsyncInvoker asyncInvoker = new AsyncInvoker(beginHandler, endHandler, this, state);
			this.PostMapRequestHandler += asyncInvoker.Invoke;
		}

		/// <summary>Occurs when the request state (for example, session state) that is associated with the current request has been obtained.</summary>
		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060004D5 RID: 1237 RVA: 0x00009E71 File Offset: 0x00008071
		// (remove) Token: 0x060004D6 RID: 1238 RVA: 0x00009E7F File Offset: 0x0000807F
		public event EventHandler PostAcquireRequestState
		{
			add
			{
				this.AddEventHandler(HttpApplication.PostAcquireRequestStateEvent, value);
			}
			remove
			{
				this.RemoveEventHandler(HttpApplication.PostAcquireRequestStateEvent, value);
			}
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostAcquireRequestState" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostAcquireRequestState" /> event handlers for the current request.</summary>
		/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostAcquireRequestState" />. </param>
		/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostAcquireRequestState" />. </param>
		// Token: 0x060004D7 RID: 1239 RVA: 0x00009E8D File Offset: 0x0000808D
		public void AddOnPostAcquireRequestStateAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			this.AddOnPostAcquireRequestStateAsync(bh, eh, null);
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostAcquireRequestState" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostAcquireRequestState" /> event handlers for the current request.</summary>
		/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostAcquireRequestState" />. </param>
		/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostAcquireRequestState" />. </param>
		/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.PostAcquireRequestState" />.</param>
		// Token: 0x060004D8 RID: 1240 RVA: 0x00009E98 File Offset: 0x00008098
		public void AddOnPostAcquireRequestStateAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			AsyncInvoker asyncInvoker = new AsyncInvoker(beginHandler, endHandler, this, state);
			this.PostAcquireRequestState += asyncInvoker.Invoke;
		}

		/// <summary>Occurs when ASP.NET has completed executing all request event handlers and the request state data has been stored.</summary>
		// Token: 0x14000014 RID: 20
		// (add) Token: 0x060004D9 RID: 1241 RVA: 0x00009EC1 File Offset: 0x000080C1
		// (remove) Token: 0x060004DA RID: 1242 RVA: 0x00009ECF File Offset: 0x000080CF
		public event EventHandler PostReleaseRequestState
		{
			add
			{
				this.AddEventHandler(HttpApplication.PostReleaseRequestStateEvent, value);
			}
			remove
			{
				this.RemoveEventHandler(HttpApplication.PostReleaseRequestStateEvent, value);
			}
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostReleaseRequestState" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostReleaseRequestState" /> event handlers for the current request.</summary>
		/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostReleaseRequestState" />. </param>
		/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostReleaseRequestState" />. </param>
		// Token: 0x060004DB RID: 1243 RVA: 0x00009EDD File Offset: 0x000080DD
		public void AddOnPostReleaseRequestStateAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			this.AddOnPostReleaseRequestStateAsync(bh, eh, null);
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostReleaseRequestState" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostReleaseRequestState" /> event handlers for the current request.</summary>
		/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostReleaseRequestState" />. </param>
		/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostReleaseRequestState" />. </param>
		/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.PostReleaseRequestState" />.</param>
		// Token: 0x060004DC RID: 1244 RVA: 0x00009EE8 File Offset: 0x000080E8
		public void AddOnPostReleaseRequestStateAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			AsyncInvoker asyncInvoker = new AsyncInvoker(beginHandler, endHandler, this, state);
			this.PostReleaseRequestState += asyncInvoker.Invoke;
		}

		/// <summary>Occurs when ASP.NET finishes updating caching modules and storing responses that are used to serve subsequent requests from the cache.</summary>
		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060004DD RID: 1245 RVA: 0x00009F11 File Offset: 0x00008111
		// (remove) Token: 0x060004DE RID: 1246 RVA: 0x00009F1F File Offset: 0x0000811F
		public event EventHandler PostUpdateRequestCache
		{
			add
			{
				this.AddEventHandler(HttpApplication.PostUpdateRequestCacheEvent, value);
			}
			remove
			{
				this.RemoveEventHandler(HttpApplication.PostUpdateRequestCacheEvent, value);
			}
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostUpdateRequestCache" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostUpdateRequestCache" /> event handlers for the current request.</summary>
		/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostUpdateRequestCache" />. </param>
		/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostUpdateRequestCache" />. </param>
		// Token: 0x060004DF RID: 1247 RVA: 0x00009F2D File Offset: 0x0000812D
		public void AddOnPostUpdateRequestCacheAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			this.AddOnPostUpdateRequestCacheAsync(bh, eh, null);
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostUpdateRequestCache" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostUpdateRequestCache" /> event handlers for the current request.</summary>
		/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the event. </param>
		/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostUpdateRequestCache" />. </param>
		/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.PostUpdateRequestCache" />.</param>
		// Token: 0x060004E0 RID: 1248 RVA: 0x00009F38 File Offset: 0x00008138
		public void AddOnPostUpdateRequestCacheAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			AsyncInvoker asyncInvoker = new AsyncInvoker(beginHandler, endHandler, this, state);
			this.PostUpdateRequestCache += asyncInvoker.Invoke;
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.AcquireRequestState" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.AcquireRequestState" /> event handlers for the current request.</summary>
		/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.AcquireRequestState" />.</param>
		/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.AcquireRequestState" />.</param>
		/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.AcquireRequestState" />.</param>
		// Token: 0x060004E1 RID: 1249 RVA: 0x00009F64 File Offset: 0x00008164
		public void AddOnAcquireRequestStateAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			AsyncInvoker asyncInvoker = new AsyncInvoker(beginHandler, endHandler, this, state);
			this.AcquireRequestState += asyncInvoker.Invoke;
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.AuthenticateRequest" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.AuthenticateRequest" /> event handlers for the current request.</summary>
		/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.AuthenticateRequest" />. </param>
		/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.AuthenticateRequest" />. </param>
		/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.AuthenticateRequest" />.</param>
		// Token: 0x060004E2 RID: 1250 RVA: 0x00009F90 File Offset: 0x00008190
		public void AddOnAuthenticateRequestAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			AsyncInvoker asyncInvoker = new AsyncInvoker(beginHandler, endHandler, this, state);
			this.AuthenticateRequest += asyncInvoker.Invoke;
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.AuthorizeRequest" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.AuthorizeRequest" /> event handlers for the current request.</summary>
		/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.AuthorizeRequest" />. </param>
		/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.AuthorizeRequest" />. </param>
		/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.AcquireRequestState" />.</param>
		// Token: 0x060004E3 RID: 1251 RVA: 0x00009FBC File Offset: 0x000081BC
		public void AddOnAuthorizeRequestAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			AsyncInvoker asyncInvoker = new AsyncInvoker(beginHandler, endHandler, this, state);
			this.AuthorizeRequest += asyncInvoker.Invoke;
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.BeginRequest" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.BeginRequest" /> event handlers for the current request.</summary>
		/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.BeginRequest" />. </param>
		/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.BeginRequest" />. </param>
		/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.BeginRequest" />.</param>
		// Token: 0x060004E4 RID: 1252 RVA: 0x00009FE8 File Offset: 0x000081E8
		public void AddOnBeginRequestAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			AsyncInvoker asyncInvoker = new AsyncInvoker(beginHandler, endHandler, this, state);
			this.BeginRequest += asyncInvoker.Invoke;
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.EndRequest" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.EndRequest" /> event handlers for the current request.</summary>
		/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.EndRequest" />. </param>
		/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.EndRequest" />. </param>
		/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.EndRequest" />.</param>
		// Token: 0x060004E5 RID: 1253 RVA: 0x0000A014 File Offset: 0x00008214
		public void AddOnEndRequestAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			AsyncInvoker asyncInvoker = new AsyncInvoker(beginHandler, endHandler, this, state);
			this.EndRequest += asyncInvoker.Invoke;
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostRequestHandlerExecute" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostRequestHandlerExecute" /> event handlers for the current request.</summary>
		/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostRequestHandlerExecute" />. </param>
		/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostRequestHandlerExecute" />. </param>
		/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.PostRequestHandlerExecute" />.</param>
		// Token: 0x060004E6 RID: 1254 RVA: 0x0000A040 File Offset: 0x00008240
		public void AddOnPostRequestHandlerExecuteAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			AsyncInvoker asyncInvoker = new AsyncInvoker(beginHandler, endHandler, this, state);
			this.PostRequestHandlerExecute += asyncInvoker.Invoke;
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PreRequestHandlerExecute" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PreRequestHandlerExecute" /> event handlers for the current request.</summary>
		/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PreRequestHandlerExecute" />. </param>
		/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PreRequestHandlerExecute" />. </param>
		/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.PreRequestHandlerExecute" /> collection.</param>
		// Token: 0x060004E7 RID: 1255 RVA: 0x0000A06C File Offset: 0x0000826C
		public void AddOnPreRequestHandlerExecuteAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			AsyncInvoker asyncInvoker = new AsyncInvoker(beginHandler, endHandler, this, state);
			this.PreRequestHandlerExecute += asyncInvoker.Invoke;
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.ReleaseRequestState" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.ReleaseRequestState" /> event handlers for the current request.</summary>
		/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.ReleaseRequestState" />. </param>
		/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.ReleaseRequestState" />. </param>
		/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.ReleaseRequestState" />.</param>
		// Token: 0x060004E8 RID: 1256 RVA: 0x0000A098 File Offset: 0x00008298
		public void AddOnReleaseRequestStateAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			AsyncInvoker asyncInvoker = new AsyncInvoker(beginHandler, endHandler, this, state);
			this.ReleaseRequestState += asyncInvoker.Invoke;
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.ResolveRequestCache" /> event handler to the collection of asynchronous <see cref="E:System.Web.HttpApplication.ResolveRequestCache" /> event handlers for the current request.</summary>
		/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.ResolveRequestCache" />. </param>
		/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.ResolveRequestCache" />. </param>
		/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.ResolveRequestCache" />.</param>
		// Token: 0x060004E9 RID: 1257 RVA: 0x0000A0C4 File Offset: 0x000082C4
		public void AddOnResolveRequestCacheAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			AsyncInvoker asyncInvoker = new AsyncInvoker(beginHandler, endHandler, this, state);
			this.ResolveRequestCache += asyncInvoker.Invoke;
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.UpdateRequestCache" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.UpdateRequestCache" /> event handlers for the current request.</summary>
		/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.UpdateRequestCache" />. </param>
		/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.UpdateRequestCache" />. </param>
		/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.UpdateRequestCache" />.</param>
		// Token: 0x060004EA RID: 1258 RVA: 0x0000A0F0 File Offset: 0x000082F0
		public void AddOnUpdateRequestCacheAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			AsyncInvoker asyncInvoker = new AsyncInvoker(beginHandler, endHandler, this, state);
			this.UpdateRequestCache += asyncInvoker.Invoke;
		}

		/// <summary>Occurs just before ASP.NET performs any logging for the current request.</summary>
		// Token: 0x14000016 RID: 22
		// (add) Token: 0x060004EB RID: 1259 RVA: 0x0000A119 File Offset: 0x00008319
		// (remove) Token: 0x060004EC RID: 1260 RVA: 0x0000A127 File Offset: 0x00008327
		public event EventHandler LogRequest
		{
			add
			{
				this.AddEventHandler(HttpApplication.LogRequestEvent, value);
			}
			remove
			{
				this.RemoveEventHandler(HttpApplication.LogRequestEvent, value);
			}
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.LogRequest" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.LogRequest" /> event handlers for the current request.</summary>
		/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.LogRequest" />.</param>
		/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.LogRequest" />.</param>
		// Token: 0x060004ED RID: 1261 RVA: 0x0000A135 File Offset: 0x00008335
		public void AddOnLogRequestAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			this.AddOnLogRequestAsync(bh, eh, null);
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.LogRequest" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.LogRequest" /> event handlers for the current request.</summary>
		/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.LogRequest" />.</param>
		/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.LogRequest" />.</param>
		/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.LogRequest" />.</param>
		// Token: 0x060004EE RID: 1262 RVA: 0x0000A140 File Offset: 0x00008340
		public void AddOnLogRequestAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			AsyncInvoker asyncInvoker = new AsyncInvoker(beginHandler, endHandler, this, state);
			this.LogRequest += asyncInvoker.Invoke;
		}

		/// <summary>Occurs when the handler is selected to respond to the request.</summary>
		// Token: 0x14000017 RID: 23
		// (add) Token: 0x060004EF RID: 1263 RVA: 0x0000A169 File Offset: 0x00008369
		// (remove) Token: 0x060004F0 RID: 1264 RVA: 0x0000A177 File Offset: 0x00008377
		public event EventHandler MapRequestHandler
		{
			add
			{
				this.AddEventHandler(HttpApplication.MapRequestHandlerEvent, value);
			}
			remove
			{
				this.RemoveEventHandler(HttpApplication.MapRequestHandlerEvent, value);
			}
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.MapRequestHandler" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.MapRequestHandler" /> event handlers for the current request.</summary>
		/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.MapRequestHandler" />.</param>
		/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.MapRequestHandler" />.</param>
		// Token: 0x060004F1 RID: 1265 RVA: 0x0000A185 File Offset: 0x00008385
		public void AddOnMapRequestHandlerAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			this.AddOnMapRequestHandlerAsync(bh, eh, null);
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.MapRequestHandler" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.MapRequestHandler" /> event handlers for the current request.</summary>
		/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.MapRequestHandler" />.</param>
		/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.MapRequestHandler" />.</param>
		/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.MapRequestHandler" />.</param>
		// Token: 0x060004F2 RID: 1266 RVA: 0x0000A190 File Offset: 0x00008390
		public void AddOnMapRequestHandlerAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			AsyncInvoker asyncInvoker = new AsyncInvoker(beginHandler, endHandler, this, state);
			this.MapRequestHandler += asyncInvoker.Invoke;
		}

		/// <summary>Occurs when ASP.NET has completed processing all the event handlers for the <see cref="E:System.Web.HttpApplication.LogRequest" /> event.</summary>
		// Token: 0x14000018 RID: 24
		// (add) Token: 0x060004F3 RID: 1267 RVA: 0x0000A1B9 File Offset: 0x000083B9
		// (remove) Token: 0x060004F4 RID: 1268 RVA: 0x0000A1C7 File Offset: 0x000083C7
		public event EventHandler PostLogRequest
		{
			add
			{
				this.AddEventHandler(HttpApplication.PostLogRequestEvent, value);
			}
			remove
			{
				this.RemoveEventHandler(HttpApplication.PostLogRequestEvent, value);
			}
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostLogRequest" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostLogRequest" /> event handlers for the current request.</summary>
		/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostLogRequest" />.</param>
		/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostLogRequest" />.</param>
		// Token: 0x060004F5 RID: 1269 RVA: 0x0000A1D5 File Offset: 0x000083D5
		public void AddOnPostLogRequestAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			this.AddOnPostLogRequestAsync(bh, eh, null);
		}

		/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostLogRequest" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostLogRequest" /> event handlers for the current request.</summary>
		/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostLogRequest" />.</param>
		/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostLogRequest" />.</param>
		/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.PostLogRequest" />.</param>
		// Token: 0x060004F6 RID: 1270 RVA: 0x0000A1E0 File Offset: 0x000083E0
		public void AddOnPostLogRequestAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			AsyncInvoker asyncInvoker = new AsyncInvoker(beginHandler, endHandler, this, state);
			this.PostLogRequest += asyncInvoker.Invoke;
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060004F7 RID: 1271 RVA: 0x0000A20C File Offset: 0x0000840C
		// (remove) Token: 0x060004F8 RID: 1272 RVA: 0x0000A244 File Offset: 0x00008444
		internal event EventHandler DefaultAuthentication;

		// Token: 0x060004F9 RID: 1273 RVA: 0x0000A279 File Offset: 0x00008479
		private void AddEventHandler(object key, EventHandler handler)
		{
			if (this.fullInitComplete)
			{
				return;
			}
			this.Events.AddHandler(key, handler);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0000A291 File Offset: 0x00008491
		private void RemoveEventHandler(object key, EventHandler handler)
		{
			if (this.fullInitComplete)
			{
				return;
			}
			this.Events.RemoveHandler(key, handler);
		}

		/// <summary>Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the <see cref="E:System.Web.HttpApplication.EndRequest" /> event.</summary>
		// Token: 0x060004FB RID: 1275 RVA: 0x0000A2A9 File Offset: 0x000084A9
		public void CompleteRequest()
		{
			this.stop_processing = true;
		}

		// Token: 0x17000206 RID: 518
		// (set) Token: 0x060004FC RID: 1276 RVA: 0x0000A2B2 File Offset: 0x000084B2
		internal bool RequestCompleted
		{
			set
			{
				this.stop_processing = value;
			}
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000A2BC File Offset: 0x000084BC
		internal void DisposeInternal()
		{
			this.Dispose();
			HttpModuleCollection httpModuleCollection = new HttpModuleCollection();
			Interlocked.Exchange<HttpModuleCollection>(ref this.modcoll, httpModuleCollection);
			if (httpModuleCollection != null)
			{
				for (int i = httpModuleCollection.Count - 1; i >= 0; i--)
				{
					httpModuleCollection.Get(i).Dispose();
				}
			}
			EventHandler eventHandler = this.nonApplicationEvents[HttpApplication.disposedEvent] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
			this.done.Close();
			this.done = null;
		}

		/// <summary>Disposes the <see cref="T:System.Web.HttpApplication" /> instance.</summary>
		// Token: 0x060004FE RID: 1278 RVA: 0x0000393A File Offset: 0x00001B3A
		public virtual void Dispose()
		{
		}

		/// <summary>Gets the name of the default output-cache provider that is configured for a Web site. </summary>
		/// <returns>The name of the default provider.</returns>
		/// <param name="context">An <see cref="T:System.Web.HttpContext" /> that provides references to intrinsic server objects that are used to service HTTP requests.</param>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">
		///   <paramref name="context" /> is null or is an empty string.</exception>
		// Token: 0x060004FF RID: 1279 RVA: 0x0000A33D File Offset: 0x0000853D
		public virtual string GetOutputCacheProviderName(HttpContext context)
		{
			return OutputCache.DefaultProviderName;
		}

		/// <summary>Provides an application-wide implementation of the <see cref="P:System.Web.UI.PartialCachingAttribute.VaryByCustom" /> property.</summary>
		/// <returns>If the value of the <paramref name="custom" /> parameter is "browser", the browser's <see cref="P:System.Web.Configuration.HttpCapabilitiesBase.Type" />; otherwise, null.</returns>
		/// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that contains information about the current Web request. </param>
		/// <param name="custom">The custom string that specifies which cached response is used to respond to the current request. </param>
		// Token: 0x06000500 RID: 1280 RVA: 0x0000A344 File Offset: 0x00008544
		public virtual string GetVaryByCustomString(HttpContext context, string custom)
		{
			if (custom == null)
			{
				throw new NullReferenceException();
			}
			if (string.Compare(custom, "browser", true, Helpers.InvariantCulture) == 0)
			{
				return context.Request.Browser.Type;
			}
			return null;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0000A374 File Offset: 0x00008574
		private bool ShouldHandleException(Exception e)
		{
			return !(e is ParseException);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0000A384 File Offset: 0x00008584
		internal void ProcessError(Exception e)
		{
			bool flag = this.context.Error == null;
			this.context.AddError(e);
			if (flag && this.ShouldHandleException(e))
			{
				EventHandler eventHandler = this.nonApplicationEvents[HttpApplication.errorEvent] as EventHandler;
				if (eventHandler != null)
				{
					try
					{
						eventHandler(this, EventArgs.Empty);
						if (this.stop_processing)
						{
							this.context.ClearError();
						}
					}
					catch (ThreadAbortException ex)
					{
						this.context.ClearError();
						if (FlagEnd.Value == ex.ExceptionState || HttpRuntime.DomainUnloading)
						{
							Thread.ResetAbort();
						}
						else
						{
							this.context.AddError(ex);
						}
					}
					catch (Exception ex2)
					{
						this.context.AddError(ex2);
					}
				}
			}
			this.stop_processing = true;
			HttpException ex3 = e as HttpException;
			if (ex3 != null && ex3.GetHttpCode() == 404)
			{
				this.removeConfigurationFromCache = true;
			}
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0000A478 File Offset: 0x00008678
		internal void Tick()
		{
			try
			{
				if (this.pipeline.MoveNext() && (bool)this.pipeline.Current)
				{
					this.PipelineDone();
				}
			}
			catch (ThreadAbortException ex)
			{
				object exceptionState = ex.ExceptionState;
				Thread.ResetAbort();
				if (exceptionState is StepTimeout)
				{
					this.ProcessError(HttpException.NewWithCode("The request timed out.", 2002));
				}
				else
				{
					this.context.ClearError();
					if (FlagEnd.Value != exceptionState && !HttpRuntime.DomainUnloading)
					{
						this.context.AddError(ex);
					}
				}
				this.stop_processing = true;
				this.PipelineDone();
			}
			catch (Exception ex2)
			{
				ThreadAbortException ex3 = ex2.InnerException as ThreadAbortException;
				if (ex3 != null && FlagEnd.Value == ex3.ExceptionState && !HttpRuntime.DomainUnloading)
				{
					this.context.ClearError();
					Thread.ResetAbort();
				}
				else
				{
					this.ProcessError(ex2);
				}
				this.stop_processing = true;
				this.PipelineDone();
			}
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000A578 File Offset: 0x00008778
		private void Resume()
		{
			if (this.in_begin)
			{
				this.must_yield = false;
				return;
			}
			this.Tick();
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0000A590 File Offset: 0x00008790
		private void async_callback_completed_cb(IAsyncResult ar)
		{
			if (this.current_ai.end != null)
			{
				try
				{
					this.current_ai.end(ar);
				}
				catch (Exception ex)
				{
					this.ProcessError(ex);
				}
			}
			this.Resume();
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000A5E0 File Offset: 0x000087E0
		private void async_handler_complete_cb(IAsyncResult ar)
		{
			IHttpAsyncHandler httpAsyncHandler = ((ar != null) ? (ar.AsyncState as IHttpAsyncHandler) : null);
			try
			{
				if (httpAsyncHandler != null)
				{
					httpAsyncHandler.EndProcessRequest(ar);
				}
			}
			catch (Exception ex)
			{
				this.ProcessError(ex);
			}
			this.Resume();
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0000A62C File Offset: 0x0000882C
		private IEnumerable RunHooks(Delegate list)
		{
			Delegate[] invocationList = list.GetInvocationList();
			foreach (EventHandler d in invocationList)
			{
				if (d.Target != null && d.Target is AsyncInvoker)
				{
					this.current_ai = (AsyncInvoker)d.Target;
					try
					{
						this.must_yield = true;
						this.in_begin = true;
						this.context.BeginTimeoutPossible();
						this.current_ai.begin(this, EventArgs.Empty, new AsyncCallback(this.async_callback_completed_cb), this.current_ai.data);
					}
					finally
					{
						this.in_begin = false;
						this.context.EndTimeoutPossible();
					}
					if (this.must_yield)
					{
						yield return this.stop_processing;
					}
					else if (this.stop_processing)
					{
						yield return true;
					}
				}
				else
				{
					try
					{
						this.context.BeginTimeoutPossible();
						d(this, EventArgs.Empty);
					}
					finally
					{
						this.context.EndTimeoutPossible();
					}
					if (this.stop_processing)
					{
						yield return true;
					}
				}
				d = null;
			}
			Delegate[] array = null;
			yield break;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0000A644 File Offset: 0x00008844
		private static void FinalErrorWrite(HttpResponse response, string error)
		{
			try
			{
				response.Write(error);
				response.Flush(true);
			}
			catch
			{
				response.Close();
			}
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0000A67C File Offset: 0x0000887C
		private void OutputPage()
		{
			if (this.context.Error == null)
			{
				try
				{
					this.context.Response.Flush(true);
				}
				catch (Exception ex)
				{
					this.context.AddError(ex);
				}
			}
			Exception ex2 = this.context.Error;
			if (ex2 != null)
			{
				HttpResponse response = this.context.Response;
				if (!response.HeadersSent)
				{
					response.ClearHeaders();
					response.ClearContent();
					if (ex2 is HttpException)
					{
						response.StatusCode = ((HttpException)ex2).GetHttpCode();
					}
					else
					{
						ex2 = HttpException.NewWithCode(string.Empty, ex2, 3009);
						response.StatusCode = 500;
					}
					HttpException ex3 = (HttpException)ex2;
					if (!this.RedirectCustomError(ref ex3))
					{
						HttpApplication.FinalErrorWrite(response, ex3.GetHtmlErrorMessage());
						return;
					}
					response.Flush(true);
					return;
				}
				else
				{
					if (!(ex2 is HttpException))
					{
						ex2 = HttpException.NewWithCode(string.Empty, ex2, 3009);
					}
					HttpApplication.FinalErrorWrite(response, ((HttpException)ex2).GetHtmlErrorMessage());
				}
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0000A784 File Offset: 0x00008984
		private void PipelineDone()
		{
			try
			{
				EventHandler eventHandler = this.Events[HttpApplication.EndRequestEvent] as EventHandler;
				if (eventHandler != null)
				{
					eventHandler(this, EventArgs.Empty);
				}
			}
			catch (Exception ex)
			{
				this.ProcessError(ex);
			}
			try
			{
				this.OutputPage();
			}
			catch (ThreadAbortException ex2)
			{
				this.ProcessError(ex2);
				Thread.ResetAbort();
			}
			catch (Exception ex3)
			{
				Console.WriteLine("Internal error: OutputPage threw an exception " + ex3);
			}
			finally
			{
				this.context.WorkerRequest.EndOfRequest();
				if (this.factory != null && this.context.Handler != null)
				{
					this.factory.ReleaseHandler(this.context.Handler);
					this.context.Handler = null;
					this.factory = null;
				}
				this.context.PopHandler();
				this.pipeline = null;
				this.current_ai = null;
			}
			this.PostDone();
			if (this.begin_iar != null)
			{
				this.begin_iar.Complete();
			}
			else
			{
				this.done.Set();
			}
			HttpApplication.requests_total_counter.Increment();
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0000A8C0 File Offset: 0x00008AC0
		[Conditional("PIPELINE_TIMER")]
		private void StartTimer(string name)
		{
			if (this.tim == null)
			{
				this.tim = new HttpApplication.Tim();
			}
			this.tim.Name = name;
			this.tim.Start();
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0000A8EC File Offset: 0x00008AEC
		[Conditional("PIPELINE_TIMER")]
		private void StopTimer()
		{
			this.tim.Stop();
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0000A8F9 File Offset: 0x00008AF9
		private IEnumerator Pipeline()
		{
			if (this.stop_processing)
			{
				yield return true;
			}
			HttpRequest request = this.context.Request;
			if (request != null)
			{
				request.Validate();
			}
			this.context.MapRequestHandlerDone = false;
			Delegate @delegate = this.Events[HttpApplication.BeginRequestEvent];
			if (@delegate != null)
			{
				foreach (object obj in this.RunHooks(@delegate))
				{
					bool flag = (bool)obj;
					yield return flag;
				}
				IEnumerator enumerator = null;
			}
			@delegate = this.Events[HttpApplication.AuthenticateRequestEvent];
			if (@delegate != null)
			{
				foreach (object obj2 in this.RunHooks(@delegate))
				{
					bool flag2 = (bool)obj2;
					yield return flag2;
				}
				IEnumerator enumerator = null;
			}
			if (this.DefaultAuthentication != null)
			{
				foreach (object obj3 in this.RunHooks(this.DefaultAuthentication))
				{
					bool flag3 = (bool)obj3;
					yield return flag3;
				}
				IEnumerator enumerator = null;
			}
			@delegate = this.Events[HttpApplication.PostAuthenticateRequestEvent];
			if (@delegate != null)
			{
				foreach (object obj4 in this.RunHooks(@delegate))
				{
					bool flag4 = (bool)obj4;
					yield return flag4;
				}
				IEnumerator enumerator = null;
			}
			@delegate = this.Events[HttpApplication.AuthorizeRequestEvent];
			if (@delegate != null)
			{
				foreach (object obj5 in this.RunHooks(@delegate))
				{
					bool flag5 = (bool)obj5;
					yield return flag5;
				}
				IEnumerator enumerator = null;
			}
			@delegate = this.Events[HttpApplication.PostAuthorizeRequestEvent];
			if (@delegate != null)
			{
				foreach (object obj6 in this.RunHooks(@delegate))
				{
					bool flag6 = (bool)obj6;
					yield return flag6;
				}
				IEnumerator enumerator = null;
			}
			@delegate = this.Events[HttpApplication.ResolveRequestCacheEvent];
			if (@delegate != null)
			{
				foreach (object obj7 in this.RunHooks(@delegate))
				{
					bool flag7 = (bool)obj7;
					yield return flag7;
				}
				IEnumerator enumerator = null;
			}
			@delegate = this.Events[HttpApplication.PostResolveRequestCacheEvent];
			if (@delegate != null)
			{
				foreach (object obj8 in this.RunHooks(@delegate))
				{
					bool flag8 = (bool)obj8;
					yield return flag8;
				}
				IEnumerator enumerator = null;
			}
			@delegate = this.Events[HttpApplication.MapRequestHandlerEvent];
			if (@delegate != null)
			{
				foreach (object obj9 in this.RunHooks(@delegate))
				{
					bool flag9 = (bool)obj9;
					yield return flag9;
				}
				IEnumerator enumerator = null;
			}
			this.context.MapRequestHandlerDone = true;
			IHttpHandler handler = null;
			try
			{
				handler = this.GetHandler(this.context, this.context.Request.CurrentExecutionFilePath);
				this.context.Handler = handler;
				this.context.PushHandler(handler);
			}
			catch (FileNotFoundException ex)
			{
				if (this.context.Request.IsLocal)
				{
					this.ProcessError(HttpException.NewWithCode(404, string.Format("File not found {0}", ex.FileName), ex, this.context.Request.FilePath, 3001));
				}
				else
				{
					this.ProcessError(HttpException.NewWithCode(404, "File not found: " + Path.GetFileName(ex.FileName), this.context.Request.FilePath, 3001));
				}
			}
			catch (DirectoryNotFoundException ex2)
			{
				if (!this.context.Request.IsLocal)
				{
					ex2 = null;
				}
				this.ProcessError(HttpException.NewWithCode(404, "Directory not found", ex2, 3001));
			}
			catch (Exception ex3)
			{
				this.ProcessError(ex3);
			}
			if (this.stop_processing)
			{
				yield return true;
			}
			@delegate = this.Events[HttpApplication.PostMapRequestHandlerEvent];
			if (@delegate != null)
			{
				foreach (object obj10 in this.RunHooks(@delegate))
				{
					bool flag10 = (bool)obj10;
					yield return flag10;
				}
				IEnumerator enumerator = null;
			}
			@delegate = this.Events[HttpApplication.AcquireRequestStateEvent];
			if (@delegate != null)
			{
				foreach (object obj11 in this.RunHooks(@delegate))
				{
					bool flag11 = (bool)obj11;
					yield return flag11;
				}
				IEnumerator enumerator = null;
			}
			@delegate = this.Events[HttpApplication.PostAcquireRequestStateEvent];
			if (@delegate != null)
			{
				foreach (object obj12 in this.RunHooks(@delegate))
				{
					bool flag12 = (bool)obj12;
					yield return flag12;
				}
				IEnumerator enumerator = null;
			}
			@delegate = this.Events[HttpApplication.PreRequestHandlerExecuteEvent];
			if (@delegate != null)
			{
				using (IEnumerator enumerator2 = this.RunHooks(@delegate).GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if ((bool)enumerator2.Current)
						{
							goto IL_09DB;
						}
					}
				}
			}
			IHttpHandler handler2 = this.context.Handler;
			if (handler2 != null && handler != handler2)
			{
				this.context.PopHandler();
				handler = handler2;
				this.context.PushHandler(handler);
			}
			try
			{
				this.context.BeginTimeoutPossible();
				if (handler == null)
				{
					throw new InvalidOperationException("No handler for the current request.");
				}
				IHttpAsyncHandler httpAsyncHandler = handler as IHttpAsyncHandler;
				if (httpAsyncHandler != null)
				{
					this.must_yield = true;
					this.in_begin = true;
					httpAsyncHandler.BeginProcessRequest(this.context, new AsyncCallback(this.async_handler_complete_cb), handler);
				}
				else
				{
					this.must_yield = false;
					handler.ProcessRequest(this.context);
				}
				if (this.context.Error != null)
				{
					throw new TargetInvocationException(this.context.Error);
				}
			}
			finally
			{
				this.in_begin = false;
				this.context.EndTimeoutPossible();
			}
			if (this.must_yield)
			{
				yield return this.stop_processing;
			}
			else if (this.stop_processing)
			{
				goto IL_09DB;
			}
			@delegate = this.Events[HttpApplication.PostRequestHandlerExecuteEvent];
			if (@delegate != null)
			{
				using (IEnumerator enumerator2 = this.RunHooks(@delegate).GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if ((bool)enumerator2.Current)
						{
							break;
						}
					}
				}
			}
			IL_09DB:
			@delegate = this.Events[HttpApplication.ReleaseRequestStateEvent];
			if (@delegate != null)
			{
				foreach (object obj13 in this.RunHooks(@delegate))
				{
					bool flag13 = (bool)obj13;
				}
			}
			if (this.stop_processing)
			{
				yield return true;
			}
			@delegate = this.Events[HttpApplication.PostReleaseRequestStateEvent];
			if (@delegate != null)
			{
				foreach (object obj14 in this.RunHooks(@delegate))
				{
					bool flag14 = (bool)obj14;
					yield return flag14;
				}
				IEnumerator enumerator = null;
			}
			if (this.context.Error == null)
			{
				this.context.Response.DoFilter(true);
			}
			@delegate = this.Events[HttpApplication.UpdateRequestCacheEvent];
			if (@delegate != null)
			{
				foreach (object obj15 in this.RunHooks(@delegate))
				{
					bool flag15 = (bool)obj15;
					yield return flag15;
				}
				IEnumerator enumerator = null;
			}
			@delegate = this.Events[HttpApplication.PostUpdateRequestCacheEvent];
			if (@delegate != null)
			{
				foreach (object obj16 in this.RunHooks(@delegate))
				{
					bool flag16 = (bool)obj16;
					yield return flag16;
				}
				IEnumerator enumerator = null;
			}
			@delegate = this.Events[HttpApplication.LogRequestEvent];
			if (@delegate != null)
			{
				foreach (object obj17 in this.RunHooks(@delegate))
				{
					bool flag17 = (bool)obj17;
					yield return flag17;
				}
				IEnumerator enumerator = null;
			}
			@delegate = this.Events[HttpApplication.PostLogRequestEvent];
			if (@delegate != null)
			{
				foreach (object obj18 in this.RunHooks(@delegate))
				{
					bool flag18 = (bool)obj18;
					yield return flag18;
				}
				IEnumerator enumerator = null;
			}
			this.PipelineDone();
			yield break;
			yield break;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0000A908 File Offset: 0x00008B08
		internal CultureInfo GetThreadCulture(HttpRequest request, CultureInfo culture, bool isAuto)
		{
			if (!isAuto)
			{
				return culture;
			}
			CultureInfo cultureInfo = null;
			string[] userLanguages = request.UserLanguages;
			try
			{
				if (userLanguages != null && userLanguages.Length != 0)
				{
					cultureInfo = CultureInfo.CreateSpecificCulture(userLanguages[0]);
				}
			}
			catch
			{
			}
			if (cultureInfo == null)
			{
				cultureInfo = culture;
			}
			return cultureInfo;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000A950 File Offset: 0x00008B50
		private void PreStart()
		{
			GlobalizationSection globalizationSection = (GlobalizationSection)WebConfigurationManager.GetSection("system.web/globalization");
			this.app_culture = globalizationSection.GetCulture();
			this.autoCulture = globalizationSection.IsAutoCulture;
			this.appui_culture = globalizationSection.GetUICulture();
			this.autoUICulture = globalizationSection.IsAutoUICulture;
			this.context.StartTimeoutTimer();
			Thread currentThread = Thread.CurrentThread;
			if (this.app_culture != null)
			{
				this.prev_app_culture = currentThread.CurrentCulture;
				CultureInfo threadCulture = this.GetThreadCulture(this.Request, this.app_culture, this.autoCulture);
				if (!threadCulture.Equals(Helpers.InvariantCulture))
				{
					currentThread.CurrentCulture = threadCulture;
				}
			}
			if (this.appui_culture != null)
			{
				this.prev_appui_culture = currentThread.CurrentUICulture;
				CultureInfo threadCulture2 = this.GetThreadCulture(this.Request, this.appui_culture, this.autoUICulture);
				if (!threadCulture2.Equals(Helpers.InvariantCulture))
				{
					currentThread.CurrentUICulture = threadCulture2;
				}
			}
			this.prev_user = Thread.CurrentPrincipal;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000AA3C File Offset: 0x00008C3C
		private void PostDone()
		{
			if (this.removeConfigurationFromCache)
			{
				WebConfigurationManager.RemoveConfigurationFromCache(this.context);
				this.removeConfigurationFromCache = false;
			}
			Thread currentThread = Thread.CurrentThread;
			if (Thread.CurrentPrincipal != this.prev_user)
			{
				Thread.CurrentPrincipal = this.prev_user;
			}
			if (this.prev_appui_culture != null && this.prev_appui_culture != currentThread.CurrentUICulture)
			{
				currentThread.CurrentUICulture = this.prev_appui_culture;
			}
			if (this.prev_app_culture != null && this.prev_app_culture != currentThread.CurrentCulture)
			{
				currentThread.CurrentCulture = this.prev_app_culture;
			}
			if (this.context == null)
			{
				this.context = HttpContext.Current;
			}
			this.context.StopTimeoutTimer();
			this.context.Request.ReleaseResources();
			this.context.Response.ReleaseResources();
			this.context = null;
			this.session = null;
			HttpContext.Current = null;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0000AB18 File Offset: 0x00008D18
		private void Start(object x)
		{
			CultureInfo[] array = x as CultureInfo[];
			if (array != null && array.Length == 2)
			{
				Thread currentThread = Thread.CurrentThread;
				currentThread.CurrentCulture = array[0];
				currentThread.CurrentUICulture = array[1];
			}
			this.InitOnce(true);
			if (HttpApplication.initialization_exception != null)
			{
				Exception ex = HttpApplication.initialization_exception;
				HttpException ex2 = HttpException.NewWithCode(string.Empty, ex, 3001);
				this.context.Response.StatusCode = 500;
				HttpApplication.FinalErrorWrite(this.context.Response, ex2.GetHtmlErrorMessage());
				this.PipelineDone();
				return;
			}
			HttpContext.Current = this.Context;
			this.PreStart();
			this.pipeline = this.Pipeline();
			this.Tick();
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0000ABCC File Offset: 0x00008DCC
		internal static Hashtable GetHandlerCache()
		{
			Cache internalCache = HttpRuntime.InternalCache;
			Hashtable hashtable = internalCache["@@HttpHandlerCache@@"] as Hashtable;
			if (hashtable == null)
			{
				hashtable = new Hashtable();
				internalCache.Insert("@@HttpHandlerCache@@", hashtable);
			}
			return hashtable;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0000AC06 File Offset: 0x00008E06
		internal static void ClearHandlerCache()
		{
			HttpApplication.GetHandlerCache().Clear();
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0000AC14 File Offset: 0x00008E14
		private object LocateHandler(HttpRequest req, string verb, string url)
		{
			Hashtable handlerCache = HttpApplication.GetHandlerCache();
			string text = verb + url;
			object obj = handlerCache[text];
			if (obj != null)
			{
				return obj;
			}
			bool flag;
			obj = (WebConfigurationManager.GetSection("system.web/httpHandlers", req.Path, req.Context) as HttpHandlersSection).LocateHandler(verb, url, out flag);
			IHttpHandler httpHandler = obj as IHttpHandler;
			if (flag && httpHandler != null && httpHandler.IsReusable)
			{
				handlerCache[text] = obj;
			}
			return obj;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0000AC82 File Offset: 0x00008E82
		internal IHttpHandler GetHandler(HttpContext context, string url)
		{
			return this.GetHandler(context, url, false);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0000AC90 File Offset: 0x00008E90
		internal IHttpHandler GetHandler(HttpContext context, string url, bool ignoreContextHandler)
		{
			if (!ignoreContextHandler && context.Handler != null)
			{
				return context.Handler;
			}
			HttpRequest request = context.Request;
			string requestType = request.RequestType;
			object obj = this.LocateHandler(request, requestType, url);
			this.factory = obj as IHttpHandlerFactory;
			IHttpHandler httpHandler;
			if (this.factory == null)
			{
				httpHandler = (IHttpHandler)obj;
			}
			else
			{
				httpHandler = this.factory.GetHandler(context, requestType, url, request.MapPath(url));
			}
			return httpHandler;
		}

		/// <summary>Enables processing of HTTP Web requests by a custom HTTP handler that implements the <see cref="T:System.Web.IHttpHandler" /> interface.</summary>
		/// <param name="context">An <see cref="T:System.Web.HttpContext" /> that provides references to the intrinsic server objects that are used to service HTTP requests.</param>
		/// <exception cref="T:System.Web.HttpException">In all cases.</exception>
		// Token: 0x06000517 RID: 1303 RVA: 0x0000ACFD File Offset: 0x00008EFD
		void IHttpHandler.ProcessRequest(HttpContext context)
		{
			this.begin_iar = null;
			this.context = context;
			this.done.Reset();
			this.Start(null);
			this.done.WaitOne();
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0000AD2C File Offset: 0x00008F2C
		internal void SetContext(HttpContext context)
		{
			this.context = context;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0000AD35 File Offset: 0x00008F35
		internal void SetSession(HttpSessionState session)
		{
			this.session = session;
		}

		/// <summary>Initiates an asynchronous call to the HTTP event handler.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that contains information about the status of the process.</returns>
		/// <param name="context">An <see cref="T:System.Web.HttpContext" /> that provides references to intrinsic server objects that are used to service HTTP requests.</param>
		/// <param name="cb">The <see cref="T:System.AsyncCallback" /> to call when the asynchronous method call is complete. If the <paramref name="cb" /> parameter is null, the delegate is not called.</param>
		/// <param name="extraData">Any extra data that is required to process the request.</param>
		// Token: 0x0600051A RID: 1306 RVA: 0x0000AD40 File Offset: 0x00008F40
		IAsyncResult IHttpAsyncHandler.BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
		{
			this.context = context;
			this.done.Reset();
			this.begin_iar = new AsyncRequestState(this.done, cb, extraData);
			CultureInfo[] array = new CultureInfo[]
			{
				Thread.CurrentThread.CurrentCulture,
				Thread.CurrentThread.CurrentUICulture
			};
			if (Thread.CurrentThread.IsThreadPoolThread)
			{
				this.Start(null);
			}
			else
			{
				ThreadPool.QueueUserWorkItem(delegate(object x)
				{
					try
					{
						this.Start(x);
					}
					catch (Exception ex)
					{
						Console.Error.WriteLine(ex);
					}
				});
			}
			return this.begin_iar;
		}

		/// <summary>Provides an asynchronous process End method when the process finishes.</summary>
		/// <param name="result">An <see cref="T:System.IAsyncResult" /> that contains information about the status of the process. </param>
		// Token: 0x0600051B RID: 1307 RVA: 0x0000ADBF File Offset: 0x00008FBF
		void IHttpAsyncHandler.EndProcessRequest(IAsyncResult result)
		{
			if (!result.IsCompleted)
			{
				result.AsyncWaitHandle.WaitOne();
			}
			this.begin_iar = null;
		}

		/// <summary>Executes custom initialization code after all event handler modules have been added.</summary>
		// Token: 0x0600051C RID: 1308 RVA: 0x0000393A File Offset: 0x00001B3A
		public virtual void Init()
		{
		}

		/// <summary>Gets a Boolean value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler" /> object.</summary>
		/// <returns>true if the <see cref="T:System.Web.IHttpHandler" /> object is reusable; otherwise, false.</returns>
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x00008B66 File Offset: 0x00006D66
		bool IHttpHandler.IsReusable
		{
			get
			{
				return true;
			}
		}

		/// <summary>Registers an application module.</summary>
		/// <param name="moduleType">The type of the module.</param>
		// Token: 0x0600051E RID: 1310 RVA: 0x0000ADDC File Offset: 0x00008FDC
		public static void RegisterModule(Type moduleType)
		{
			if (!((HttpRuntimeSection)WebConfigurationManager.GetSection("system.web/httpRuntime")).AllowDynamicModuleRegistration)
			{
				throw new InvalidOperationException("The Application has requested to register a dynamic Module, but dynamic module registration is disabled in web.config.");
			}
			HttpApplication.dynamicModuleManeger.Add(moduleType);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0000AE0C File Offset: 0x0000900C
		private HttpModuleCollection CreateDynamicModules()
		{
			HttpModuleCollection httpModuleCollection = new HttpModuleCollection();
			foreach (DynamicModuleInfo dynamicModuleInfo in HttpApplication.dynamicModuleManeger.LockAndGetModules())
			{
				IHttpModule httpModule = this.CreateModuleInstance(dynamicModuleInfo.Type);
				httpModule.Init(this);
				httpModuleCollection.AddModule(dynamicModuleInfo.Name, httpModule);
			}
			return httpModuleCollection;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0000AE80 File Offset: 0x00009080
		private IHttpModule CreateModuleInstance(Type type)
		{
			return (IHttpModule)Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, null, null);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0000AE95 File Offset: 0x00009095
		internal void ClearError()
		{
			this.context.ClearError();
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0000AEA4 File Offset: 0x000090A4
		private bool RedirectErrorPage(string error_page)
		{
			if (this.context.Request.QueryString["aspxerrorpath"] != null)
			{
				return false;
			}
			this.Response.Redirect(error_page + "?aspxerrorpath=" + this.Request.Path, false);
			return true;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0000AEF4 File Offset: 0x000090F4
		private bool RedirectCustomError(ref HttpException httpEx)
		{
			bool flag;
			try
			{
				if (!this.context.IsCustomErrorEnabledUnsafe)
				{
					flag = false;
				}
				else
				{
					CustomErrorsSection customErrorsSection = (CustomErrorsSection)WebConfigurationManager.GetSection("system.web/customErrors");
					if (customErrorsSection == null)
					{
						if (this.context.ErrorPage != null)
						{
							flag = this.RedirectErrorPage(this.context.ErrorPage);
						}
						else
						{
							flag = false;
						}
					}
					else
					{
						CustomError customError = customErrorsSection.Errors[this.context.Response.StatusCode.ToString()];
						string text = ((customError == null) ? null : customError.Redirect);
						if (text == null)
						{
							text = this.context.ErrorPage;
							if (text == null)
							{
								text = customErrorsSection.DefaultRedirect;
							}
						}
						if (text == null)
						{
							flag = false;
						}
						else if (customErrorsSection.RedirectMode == CustomErrorsRedirectMode.ResponseRewrite)
						{
							this.context.Server.Execute(text);
							flag = true;
						}
						else
						{
							flag = this.RedirectErrorPage(text);
						}
					}
				}
			}
			catch (Exception ex)
			{
				httpEx = HttpException.NewWithCode(500, string.Empty, ex, 3009);
				flag = false;
			}
			return flag;
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x0000AFFC File Offset: 0x000091FC
		internal static string BinDirectory
		{
			get
			{
				if (HttpApplication.binDirectory == null)
				{
					string applicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
					foreach (string text in HttpApplication.BinDirs)
					{
						string text2 = Path.Combine(applicationBase, text);
						if (Directory.Exists(text2))
						{
							HttpApplication.binDirectory = text2;
							break;
						}
					}
				}
				return HttpApplication.binDirectory;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x0000B058 File Offset: 0x00009258
		internal static string[] BinDirectoryAssemblies
		{
			get
			{
				ArrayList arrayList = null;
				string text = HttpApplication.BinDirectory;
				if (text != null)
				{
					arrayList = new ArrayList();
					string[] files = Directory.GetFiles(text, "*.dll");
					arrayList.AddRange(files);
				}
				if (arrayList == null)
				{
					return new string[0];
				}
				return (string[])arrayList.ToArray(typeof(string));
			}
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0000B0A8 File Offset: 0x000092A8
		internal static Type LoadType(string typeName)
		{
			return HttpApplication.LoadType(typeName, false);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0000B0B4 File Offset: 0x000092B4
		internal static Type LoadType(string typeName, bool throwOnMissing)
		{
			Type type = Type.GetType(typeName);
			if (type != null)
			{
				return type;
			}
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			for (int i = 0; i < assemblies.Length; i++)
			{
				type = assemblies[i].GetType(typeName, false);
				if (type != null)
				{
					return type;
				}
			}
			IList topLevelAssemblies = BuildManager.TopLevelAssemblies;
			if (topLevelAssemblies != null && topLevelAssemblies.Count > 0)
			{
				foreach (object obj in topLevelAssemblies)
				{
					Assembly assembly = (Assembly)obj;
					if (!(assembly == null))
					{
						type = assembly.GetType(typeName, false);
						if (type != null)
						{
							return type;
						}
					}
				}
			}
			Exception ex = null;
			try
			{
				type = null;
				type = HttpApplication.LoadTypeFromBin(typeName);
			}
			catch (Exception ex)
			{
			}
			if (type != null)
			{
				return type;
			}
			if (throwOnMissing)
			{
				throw new TypeLoadException(string.Format("Type '{0}' cannot be found", typeName), ex);
			}
			return null;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0000B1C4 File Offset: 0x000093C4
		internal static Type LoadType<TBaseType>(string typeName, bool throwOnMissing)
		{
			Type type = HttpApplication.LoadType(typeName, throwOnMissing);
			if (typeof(TBaseType).IsAssignableFrom(type))
			{
				return type;
			}
			if (throwOnMissing)
			{
				throw new TypeLoadException(string.Format("Type '{0}' found but it doesn't derive from base type '{1}'.", typeName, typeof(TBaseType)));
			}
			return null;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0000B20C File Offset: 0x0000940C
		internal static Type LoadTypeFromBin(string typeName)
		{
			string[] binDirectoryAssemblies = HttpApplication.BinDirectoryAssemblies;
			int i = 0;
			while (i < binDirectoryAssemblies.Length)
			{
				string text = binDirectoryAssemblies[i];
				Assembly assembly = null;
				try
				{
					assembly = Assembly.LoadFrom(text);
				}
				catch (FileLoadException)
				{
					goto IL_0038;
				}
				catch (BadImageFormatException)
				{
					goto IL_0038;
				}
				goto IL_0023;
				IL_0038:
				i++;
				continue;
				IL_0023:
				Type type = assembly.GetType(typeName, false);
				if (!(type == null))
				{
					return type;
				}
				goto IL_0038;
			}
			return null;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0000B278 File Offset: 0x00009478
		// Note: this type is marked as 'beforefieldinit'.
		static HttpApplication()
		{
			HttpApplication.PreSendRequestHeadersEvent = new object();
			HttpApplication.PreSendRequestContentEvent = new object();
			HttpApplication.AcquireRequestStateEvent = new object();
			HttpApplication.AuthenticateRequestEvent = new object();
			HttpApplication.AuthorizeRequestEvent = new object();
			HttpApplication.BeginRequestEvent = new object();
			HttpApplication.EndRequestEvent = new object();
			HttpApplication.PostRequestHandlerExecuteEvent = new object();
			HttpApplication.PreRequestHandlerExecuteEvent = new object();
			HttpApplication.ReleaseRequestStateEvent = new object();
			HttpApplication.ResolveRequestCacheEvent = new object();
			HttpApplication.UpdateRequestCacheEvent = new object();
			HttpApplication.PostAuthenticateRequestEvent = new object();
			HttpApplication.PostAuthorizeRequestEvent = new object();
			HttpApplication.PostResolveRequestCacheEvent = new object();
			HttpApplication.PostMapRequestHandlerEvent = new object();
			HttpApplication.PostAcquireRequestStateEvent = new object();
			HttpApplication.PostReleaseRequestStateEvent = new object();
			HttpApplication.PostUpdateRequestCacheEvent = new object();
			HttpApplication.LogRequestEvent = new object();
			HttpApplication.MapRequestHandlerEvent = new object();
			HttpApplication.PostLogRequestEvent = new object();
		}

		/// <summary>Occurs when the managed objects that are associated with the request have been released.</summary>
		// Token: 0x1400001A RID: 26
		// (add) Token: 0x0600052C RID: 1324 RVA: 0x0000B3E4 File Offset: 0x000095E4
		// (remove) Token: 0x0600052D RID: 1325 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public event EventHandler RequestCompleted
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void OnExecuteRequestStep(Action<HttpContextBase, Action> callback)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000E90 RID: 3728
		private static readonly object disposedEvent = new object();

		// Token: 0x04000E91 RID: 3729
		private static readonly object errorEvent = new object();

		// Token: 0x04000E92 RID: 3730
		internal static PerformanceCounter requests_total_counter = new PerformanceCounter("ASP.NET", "Requests Total");

		// Token: 0x04000E93 RID: 3731
		internal static readonly string[] BinDirs = new string[] { "Bin", "bin" };

		// Token: 0x04000E94 RID: 3732
		private object this_lock = new object();

		// Token: 0x04000E95 RID: 3733
		private HttpContext context;

		// Token: 0x04000E96 RID: 3734
		private HttpSessionState session;

		// Token: 0x04000E97 RID: 3735
		private ISite isite;

		// Token: 0x04000E98 RID: 3736
		private volatile HttpModuleCollection modcoll;

		// Token: 0x04000E99 RID: 3737
		private string assemblyLocation;

		// Token: 0x04000E9A RID: 3738
		private IHttpHandlerFactory factory;

		// Token: 0x04000E9B RID: 3739
		private bool autoCulture;

		// Token: 0x04000E9C RID: 3740
		private bool autoUICulture;

		// Token: 0x04000E9D RID: 3741
		private bool stop_processing;

		// Token: 0x04000E9E RID: 3742
		private bool in_application_start;

		// Token: 0x04000E9F RID: 3743
		private IEnumerator pipeline;

		// Token: 0x04000EA0 RID: 3744
		private ManualResetEvent done;

		// Token: 0x04000EA1 RID: 3745
		private AsyncRequestState begin_iar;

		// Token: 0x04000EA2 RID: 3746
		private AsyncInvoker current_ai;

		// Token: 0x04000EA3 RID: 3747
		private EventHandlerList events;

		// Token: 0x04000EA4 RID: 3748
		private EventHandlerList nonApplicationEvents = new EventHandlerList();

		// Token: 0x04000EA5 RID: 3749
		private CultureInfo app_culture;

		// Token: 0x04000EA6 RID: 3750
		private CultureInfo appui_culture;

		// Token: 0x04000EA7 RID: 3751
		private CultureInfo prev_app_culture;

		// Token: 0x04000EA8 RID: 3752
		private CultureInfo prev_appui_culture;

		// Token: 0x04000EA9 RID: 3753
		private IPrincipal prev_user;

		// Token: 0x04000EAA RID: 3754
		private static string binDirectory;

		// Token: 0x04000EAB RID: 3755
		private static volatile Exception initialization_exception;

		// Token: 0x04000EAC RID: 3756
		private bool removeConfigurationFromCache;

		// Token: 0x04000EAD RID: 3757
		private bool fullInitComplete;

		// Token: 0x04000EAE RID: 3758
		private static DynamicModuleManager dynamicModuleManeger = new DynamicModuleManager();

		// Token: 0x04000EAF RID: 3759
		private bool must_yield;

		// Token: 0x04000EB0 RID: 3760
		private bool in_begin;

		// Token: 0x04000EC8 RID: 3784
		private HttpApplication.Tim tim;

		// Token: 0x04000EC9 RID: 3785
		private const string HANDLER_CACHE = "@@HttpHandlerCache@@";

		// Token: 0x02000078 RID: 120
		private class Tim
		{
			// Token: 0x0600052F RID: 1327 RVA: 0x00002050 File Offset: 0x00000250
			public Tim()
			{
			}

			// Token: 0x06000530 RID: 1328 RVA: 0x0000B3EB File Offset: 0x000095EB
			public Tim(string name)
			{
				this.name = name;
			}

			// Token: 0x1700020A RID: 522
			// (get) Token: 0x06000531 RID: 1329 RVA: 0x0000B3FA File Offset: 0x000095FA
			// (set) Token: 0x06000532 RID: 1330 RVA: 0x0000B402 File Offset: 0x00009602
			public string Name
			{
				get
				{
					return this.name;
				}
				set
				{
					this.name = value;
				}
			}

			// Token: 0x06000533 RID: 1331 RVA: 0x0000B40B File Offset: 0x0000960B
			public void Start()
			{
				this.start = DateTime.UtcNow;
			}

			// Token: 0x06000534 RID: 1332 RVA: 0x0000B418 File Offset: 0x00009618
			public void Stop()
			{
				Console.WriteLine("{0}: {1}ms", this.name, (DateTime.UtcNow - this.start).TotalMilliseconds);
			}

			// Token: 0x04000ECA RID: 3786
			private string name;

			// Token: 0x04000ECB RID: 3787
			private DateTime start;
		}
	}
}
