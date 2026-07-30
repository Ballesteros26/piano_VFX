using System;
using System.ComponentModel;
using System.Configuration;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Util;
using Unity;

namespace System.Web.SessionState
{
	/// <summary>Provides session-state services for an application. This class cannot be inherited.</summary>
	// Token: 0x020004A4 RID: 1188
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class SessionStateModule : IHttpModule, ISessionStateModule
	{
		/// <summary>The event that occurs when a session is created.</summary>
		// Token: 0x14000100 RID: 256
		// (add) Token: 0x060035D8 RID: 13784 RVA: 0x0008D74C File Offset: 0x0008B94C
		// (remove) Token: 0x060035D9 RID: 13785 RVA: 0x0008D75F File Offset: 0x0008B95F
		public event EventHandler Start
		{
			add
			{
				this.events.AddHandler(SessionStateModule.startEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(SessionStateModule.startEvent, value);
			}
		}

		/// <summary>Occurs when a session ends.</summary>
		// Token: 0x14000101 RID: 257
		// (add) Token: 0x060035DA RID: 13786 RVA: 0x0008D772 File Offset: 0x0008B972
		// (remove) Token: 0x060035DB RID: 13787 RVA: 0x0008D785 File Offset: 0x0008B985
		public event EventHandler End
		{
			add
			{
				this.events.AddHandler(SessionStateModule.endEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(SessionStateModule.endEvent, value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.SessionState.SessionStateModule" /> class.</summary>
		// Token: 0x060035DC RID: 13788 RVA: 0x0008D798 File Offset: 0x0008B998
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public SessionStateModule()
		{
		}

		/// <summary>Executes final cleanup code before the <see cref="T:System.Web.SessionState.SessionStateModule" /> object is released from memory.</summary>
		// Token: 0x060035DD RID: 13789 RVA: 0x0008D7AC File Offset: 0x0008B9AC
		public void Dispose()
		{
			this.app.BeginRequest -= this.OnBeginRequest;
			this.app.AcquireRequestState -= this.OnAcquireRequestState;
			this.app.ReleaseRequestState -= this.OnReleaseRequestState;
			this.app.EndRequest -= this.OnEndRequest;
			this.handler.Dispose();
		}

		/// <summary>Calls initialization code when a <see cref="T:System.Web.SessionState.SessionStateModule" /> object is created.</summary>
		/// <param name="app">The current application. </param>
		/// <exception cref="T:System.Web.HttpException">The mode attribute in the sessionState Element (ASP.NET Settings Schema) configuration element is set to <see cref="F:System.Web.SessionState.SessionStateMode.StateServer" /> or <see cref="F:System.Web.SessionState.SessionStateMode.SQLServer" />, and the ASP.NET application has less than <see cref="F:System.Web.AspNetHostingPermissionLevel.Medium" /> trust.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The mode attribute in the sessionState Element (ASP.NET Settings Schema) configuration element is set to <see cref="F:System.Web.SessionState.SessionStateMode.Custom" /> and the customProvider attribute is empty or does not exist.-or-The mode attribute in the sessionState Element (ASP.NET Settings Schema) configuration element is set to <see cref="F:System.Web.SessionState.SessionStateMode.Custom" /> and the provider identified by name in the customProvider attribute has not been added to the providers Element for sessionState (ASP.NET Settings Schema) sub-element.</exception>
		// Token: 0x060035DE RID: 13790 RVA: 0x0008D820 File Offset: 0x0008BA20
		[EnvironmentPermission(SecurityAction.Assert, Read = "MONO_XSP_STATIC_SESSION")]
		public void Init(HttpApplication app)
		{
			this.config = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
			ProviderSettings providerSettings;
			switch (this.config.Mode)
			{
			case SessionStateMode.Off:
				return;
			case SessionStateMode.InProc:
				providerSettings = new ProviderSettings(null, typeof(SessionInProcHandler).AssemblyQualifiedName);
				break;
			case SessionStateMode.StateServer:
				providerSettings = new ProviderSettings(null, typeof(SessionStateServerHandler).AssemblyQualifiedName);
				break;
			case SessionStateMode.SQLServer:
				providerSettings = new ProviderSettings(null, typeof(SessionSQLServerHandler).AssemblyQualifiedName);
				break;
			case SessionStateMode.Custom:
				providerSettings = this.config.Providers[this.config.CustomProvider];
				if (providerSettings == null)
				{
					throw new HttpException(string.Format("Cannot find '{0}' provider.", this.config.CustomProvider));
				}
				break;
			default:
				throw new NotImplementedException(string.Format("The mode '{0}' is not implemented.", this.config.Mode));
			}
			this.handler = (SessionStateStoreProviderBase)ProvidersHelper.InstantiateProvider(providerSettings, typeof(SessionStateStoreProviderBase));
			if (string.IsNullOrEmpty(this.config.SessionIDManagerType))
			{
				this.idManager = new SessionIDManager();
			}
			else
			{
				Type type = HttpApplication.LoadType(this.config.SessionIDManagerType, true);
				this.idManager = (ISessionIDManager)Activator.CreateInstance(type);
			}
			try
			{
				this.idManager.Initialize();
			}
			catch (Exception ex)
			{
				throw new HttpException("Failed to initialize session ID manager.", ex);
			}
			this.supportsExpiration = this.handler.SetItemExpireCallback(new SessionStateItemExpireCallback(this.OnSessionExpired));
			HttpRuntimeSection section = HttpRuntime.Section;
			this.executionTimeout = section.ExecutionTimeout;
			this.app = app;
			app.BeginRequest += this.OnBeginRequest;
			app.AcquireRequestState += this.OnAcquireRequestState;
			app.ReleaseRequestState += this.OnReleaseRequestState;
			app.EndRequest += this.OnEndRequest;
		}

		// Token: 0x060035DF RID: 13791 RVA: 0x0008DA1C File Offset: 0x0008BC1C
		internal static bool IsCookieLess(HttpContext context, SessionStateSection config)
		{
			if (config.Cookieless == HttpCookieMode.UseCookies)
			{
				return false;
			}
			if (config.Cookieless == HttpCookieMode.UseUri)
			{
				return true;
			}
			object obj = context.Items["_SessionIDManager_IsCookieLess"];
			return obj != null && (bool)obj;
		}

		// Token: 0x060035E0 RID: 13792 RVA: 0x0008DA5C File Offset: 0x0008BC5C
		private void OnBeginRequest(object o, EventArgs args)
		{
			HttpContext context = ((HttpApplication)o).Context;
			string filePath = context.Request.FilePath;
			string directory = VirtualPathUtility.GetDirectory(filePath);
			string sessionId = UrlUtils.GetSessionId(directory);
			if (sessionId == null)
			{
				return;
			}
			string text = UrlUtils.RemoveSessionId(directory, filePath);
			context.Request.SetFilePath(text);
			context.Request.SetHeader("AspFilterSessionId", sessionId);
			context.Response.SetAppPathModifier(sessionId);
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x0008DAC8 File Offset: 0x0008BCC8
		private void OnAcquireRequestState(object o, EventArgs args)
		{
			HttpContext context = ((HttpApplication)o).Context;
			if (!(context.Handler is IRequiresSessionState))
			{
				return;
			}
			bool flag = context.Handler is IReadOnlySessionState;
			bool flag2;
			if (this.idManager.InitializeRequest(context, false, out flag2))
			{
				return;
			}
			string text = this.idManager.GetSessionID(context);
			this.handler.InitializeRequest(context);
			this.storeData = this.GetStoreData(context, text, flag);
			this.storeIsNew = false;
			if (this.storeData == null && !this.storeLocked)
			{
				this.storeIsNew = true;
				text = this.idManager.CreateSessionID(context);
				bool flag3;
				bool flag4;
				this.idManager.SaveSessionID(context, text, out flag3, out flag4);
				if (flag3)
				{
					if (flag2)
					{
						this.handler.CreateUninitializedItem(context, text, (int)this.config.Timeout.TotalMinutes);
					}
					context.Response.End();
					return;
				}
				this.storeData = this.handler.CreateNewStoreData(context, (int)this.config.Timeout.TotalMinutes);
			}
			else if (this.storeData == null && this.storeLocked)
			{
				this.WaitForStoreUnlock(context, text, flag);
			}
			else if (this.storeData != null && !this.storeLocked && this.storeSessionAction == SessionStateActions.InitializeItem && SessionStateModule.IsCookieLess(context, this.config))
			{
				this.storeData = this.handler.CreateNewStoreData(context, (int)this.config.Timeout.TotalMinutes);
			}
			this.container = this.CreateContainer(text, this.storeData, this.storeIsNew, flag);
			SessionStateUtility.AddHttpSessionStateToContext(this.app.Context, this.container);
			if (this.storeIsNew)
			{
				this.OnSessionStart();
				HttpSessionState session = this.app.Session;
				if (session != null)
				{
					this.storeData.Timeout = session.Timeout;
				}
			}
			this.supportsExpiration = this.handler.SetItemExpireCallback(new SessionStateItemExpireCallback(this.OnSessionExpired));
		}

		// Token: 0x060035E2 RID: 13794 RVA: 0x0008DCC0 File Offset: 0x0008BEC0
		private void OnReleaseRequestState(object o, EventArgs args)
		{
			HttpContext context = ((HttpApplication)o).Context;
			if (!(context.Handler is IRequiresSessionState))
			{
				return;
			}
			try
			{
				if (!this.container.IsAbandoned)
				{
					if (!this.container.IsReadOnly)
					{
						this.handler.SetAndReleaseItemExclusive(context, this.container.SessionID, this.storeData, this.storeLockId, this.storeIsNew);
					}
					else
					{
						this.handler.ReleaseItemExclusive(context, this.container.SessionID, this.storeLockId);
					}
					this.handler.ResetItemTimeout(context, this.container.SessionID);
				}
				else
				{
					this.handler.RemoveItem(context, this.container.SessionID, this.storeLockId, this.storeData);
					this.handler.ReleaseItemExclusive(context, this.container.SessionID, this.storeLockId);
					if (this.supportsExpiration)
					{
						this.handler.SetItemExpireCallback(null);
					}
					SessionStateUtility.RaiseSessionEnd(this.container, this, args);
				}
				SessionStateUtility.RemoveHttpSessionStateFromContext(context);
			}
			finally
			{
				this.container = null;
				this.storeData = null;
			}
		}

		// Token: 0x060035E3 RID: 13795 RVA: 0x0008DDEC File Offset: 0x0008BFEC
		private void OnEndRequest(object o, EventArgs args)
		{
			if (this.handler == null)
			{
				return;
			}
			if (this.container != null)
			{
				this.OnReleaseRequestState(o, args);
			}
			HttpApplication httpApplication = o as HttpApplication;
			if (httpApplication == null)
			{
				return;
			}
			if (this.handler != null)
			{
				this.handler.EndRequest(httpApplication.Context);
			}
		}

		// Token: 0x060035E4 RID: 13796 RVA: 0x0008DE38 File Offset: 0x0008C038
		private SessionStateStoreData GetStoreData(HttpContext context, string sessionId, bool isReadOnly)
		{
			SessionStateStoreData sessionStateStoreData = (isReadOnly ? this.handler.GetItem(context, sessionId, out this.storeLocked, out this.storeLockAge, out this.storeLockId, out this.storeSessionAction) : this.handler.GetItemExclusive(context, sessionId, out this.storeLocked, out this.storeLockAge, out this.storeLockId, out this.storeSessionAction));
			if (this.storeLockId == null)
			{
				this.storeLockId = 0;
			}
			return sessionStateStoreData;
		}

		// Token: 0x060035E5 RID: 13797 RVA: 0x0008DEA8 File Offset: 0x0008C0A8
		private void WaitForStoreUnlock(HttpContext context, string sessionId, bool isReadOnly)
		{
			DateTime now = DateTime.Now;
			while (DateTime.Now - now < this.executionTimeout)
			{
				Thread.Sleep(500);
				this.storeData = this.GetStoreData(context, sessionId, isReadOnly);
				if (this.storeData == null && this.storeLocked && this.storeLockAge > this.executionTimeout)
				{
					this.handler.ReleaseItemExclusive(context, sessionId, this.storeLockId);
					return;
				}
				if (this.storeData != null && !this.storeLocked)
				{
					return;
				}
			}
		}

		// Token: 0x060035E6 RID: 13798 RVA: 0x0008DF38 File Offset: 0x0008C138
		private HttpSessionStateContainer CreateContainer(string sessionId, SessionStateStoreData data, bool isNew, bool isReadOnly)
		{
			if (data == null)
			{
				return new HttpSessionStateContainer(sessionId, null, null, 0, isNew, this.config.Cookieless, this.config.Mode, isReadOnly);
			}
			return new HttpSessionStateContainer(sessionId, data.Items, data.StaticObjects, data.Timeout, isNew, this.config.Cookieless, this.config.Mode, isReadOnly);
		}

		// Token: 0x060035E7 RID: 13799 RVA: 0x0008DF9C File Offset: 0x0008C19C
		private void OnSessionExpired(string id, SessionStateStoreData item)
		{
			SessionStateUtility.RaiseSessionEnd(this.CreateContainer(id, item, false, true), this, EventArgs.Empty);
		}

		// Token: 0x060035E8 RID: 13800 RVA: 0x0008DFB4 File Offset: 0x0008C1B4
		private void OnSessionStart()
		{
			EventHandler eventHandler = this.events[SessionStateModule.startEvent] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x060035EA RID: 13802 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void ReleaseSessionState(HttpContext context)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x060035EB RID: 13803 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public Task ReleaseSessionStateAsync(HttpContext context)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x04001D7F RID: 7551
		internal const string HeaderName = "AspFilterSessionId";

		// Token: 0x04001D80 RID: 7552
		internal const string CookielessFlagName = "_SessionIDManager_IsCookieLess";

		// Token: 0x04001D81 RID: 7553
		private static readonly object startEvent = new object();

		// Token: 0x04001D82 RID: 7554
		private static readonly object endEvent = new object();

		// Token: 0x04001D83 RID: 7555
		private SessionStateSection config;

		// Token: 0x04001D84 RID: 7556
		private SessionStateStoreProviderBase handler;

		// Token: 0x04001D85 RID: 7557
		private ISessionIDManager idManager;

		// Token: 0x04001D86 RID: 7558
		private bool supportsExpiration;

		// Token: 0x04001D87 RID: 7559
		private HttpApplication app;

		// Token: 0x04001D88 RID: 7560
		private bool storeLocked;

		// Token: 0x04001D89 RID: 7561
		private TimeSpan storeLockAge;

		// Token: 0x04001D8A RID: 7562
		private object storeLockId;

		// Token: 0x04001D8B RID: 7563
		private SessionStateActions storeSessionAction;

		// Token: 0x04001D8C RID: 7564
		private bool storeIsNew;

		// Token: 0x04001D8D RID: 7565
		private SessionStateStoreData storeData;

		// Token: 0x04001D8E RID: 7566
		private HttpSessionStateContainer container;

		// Token: 0x04001D8F RID: 7567
		private TimeSpan executionTimeout;

		// Token: 0x04001D90 RID: 7568
		private EventHandlerList events = new EventHandlerList();
	}
}
