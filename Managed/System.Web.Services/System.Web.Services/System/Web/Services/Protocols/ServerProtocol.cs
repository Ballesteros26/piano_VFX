using System;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web.Caching;

namespace System.Web.Services.Protocols
{
	/// <summary>The .NET Framework uses classes that are derived from the <see cref="T:System.Web.Services.Protocols.ServerProtocol" /> class to process XML Web service requests.</summary>
	// Token: 0x02000052 RID: 82
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class ServerProtocol
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00008D40 File Offset: 0x00006F40
		internal static object InternalSyncObject
		{
			get
			{
				if (ServerProtocol.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange(ref ServerProtocol.s_InternalSyncObject, obj, null);
				}
				return ServerProtocol.s_InternalSyncObject;
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00008D6C File Offset: 0x00006F6C
		internal void SetContext(Type type, HttpContext context, HttpRequest request, HttpResponse response)
		{
			PartialTrustHelpers.FailIfInPartialTrustOutsideAspNet();
			this.type = type;
			this.context = context;
			this.request = request;
			this.response = response;
			this.Initialize();
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00008D98 File Offset: 0x00006F98
		internal virtual void CreateServerInstance()
		{
			this.target = Activator.CreateInstance(this.ServerType.Type);
			WebService webService = this.target as WebService;
			if (webService != null)
			{
				webService.SetContext(this.context);
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00008DD8 File Offset: 0x00006FD8
		internal virtual void DisposeServerInstance()
		{
			if (this.target == null)
			{
				return;
			}
			IDisposable disposable = this.target as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
			this.target = null;
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpContext" /> object for the derived class.</summary>
		/// <returns>An <see cref="T:System.Web.HttpContext" /> object.</returns>
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00008E0A File Offset: 0x0000700A
		protected internal HttpContext Context
		{
			get
			{
				return this.context;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpRequest" /> object for the derived class.</summary>
		/// <returns>An <see cref="T:System.Web.HttpRequest" /> object. </returns>
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00008E12 File Offset: 0x00007012
		protected internal HttpRequest Request
		{
			get
			{
				return this.request;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpResponse" /> object for the derived class.</summary>
		/// <returns>An <see cref="T:System.Web.HttpResponse" /> object.</returns>
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00008E1A File Offset: 0x0000701A
		protected internal HttpResponse Response
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00008E22 File Offset: 0x00007022
		internal Type Type
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Gets the service object that is invoked.</summary>
		/// <returns>The service object that is invoked.</returns>
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00008E2A File Offset: 0x0000702A
		protected internal virtual object Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00002B51 File Offset: 0x00000D51
		internal virtual bool WriteException(Exception e, Stream outputStream)
		{
			return false;
		}

		// Token: 0x060001D3 RID: 467
		internal abstract bool Initialize();

		// Token: 0x060001D4 RID: 468
		internal abstract object[] ReadParameters();

		// Token: 0x060001D5 RID: 469
		internal abstract void WriteReturns(object[] returns, Stream outputStream);

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001D6 RID: 470
		internal abstract LogicalMethodInfo MethodInfo { get; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001D7 RID: 471
		internal abstract ServerType ServerType { get; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001D8 RID: 472
		internal abstract bool IsOneWay { get; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00006C2F File Offset: 0x00004E2F
		internal virtual Exception OnewayInitException
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00008E32 File Offset: 0x00007032
		internal WebMethodAttribute MethodAttribute
		{
			get
			{
				if (this.methodAttr == null)
				{
					this.methodAttr = this.MethodInfo.MethodAttribute;
				}
				return this.methodAttr;
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00008E53 File Offset: 0x00007053
		internal string GenerateFaultString(Exception e)
		{
			return this.GenerateFaultString(e, false);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00008E5D File Offset: 0x0000705D
		internal static void SetHttpResponseStatusCode(HttpResponse httpResponse, int statusCode)
		{
			httpResponse.TrySkipIisCustomErrors = true;
			httpResponse.StatusCode = statusCode;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00008E70 File Offset: 0x00007070
		internal string GenerateFaultString(Exception e, bool htmlEscapeMessage)
		{
			bool flag = this.Context != null && !this.Context.IsCustomErrorEnabled;
			if (flag && !htmlEscapeMessage)
			{
				return e.ToString();
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (flag)
			{
				ServerProtocol.GenerateFaultString(e, stringBuilder);
			}
			else
			{
				for (Exception ex = e; ex != null; ex = ex.InnerException)
				{
					string text = (htmlEscapeMessage ? HttpUtility.HtmlEncode(ex.Message) : ex.Message);
					if (text.Length == 0)
					{
						text = e.GetType().Name;
					}
					stringBuilder.Append(text);
					if (ex.InnerException != null)
					{
						stringBuilder.Append(" ---> ");
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00008F14 File Offset: 0x00007114
		private static void GenerateFaultString(Exception e, StringBuilder builder)
		{
			builder.Append(e.GetType().FullName);
			if (e.Message != null && e.Message.Length > 0)
			{
				builder.Append(": ");
				builder.Append(HttpUtility.HtmlEncode(e.Message));
			}
			if (e.InnerException != null)
			{
				builder.Append(" ---> ");
				ServerProtocol.GenerateFaultString(e.InnerException, builder);
				builder.Append(Environment.NewLine);
				builder.Append("   ");
				builder.Append(Res.GetString("StackTraceEnd"));
			}
			if (e.StackTrace != null)
			{
				builder.Append(Environment.NewLine);
				builder.Append(e.StackTrace);
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00008FD1 File Offset: 0x000071D1
		internal void WriteOneWayResponse()
		{
			this.context.Response.ContentType = null;
			this.Response.StatusCode = 202;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00002B24 File Offset: 0x00000D24
		private static string DefaultCreateCustomKeyForAspNetWebServiceMetadataCache(Type protocolType, Type serverType, string originalKey)
		{
			return originalKey;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00008FF4 File Offset: 0x000071F4
		private static ServerProtocol.CreateCustomKeyForAspNetWebServiceMetadataCache GetCreateCustomKeyForAspNetWebServiceMetadataCacheDelegate(Type serverType)
		{
			PartialTrustHelpers.FailIfInPartialTrustOutsideAspNet();
			string text = "CreateCustomKeyForAspNetWebServiceMetadataCache-" + serverType.FullName;
			ServerProtocol.CreateCustomKeyForAspNetWebServiceMetadataCache createCustomKeyForAspNetWebServiceMetadataCache = (ServerProtocol.CreateCustomKeyForAspNetWebServiceMetadataCache)HttpRuntime.Cache.Get(text);
			if (createCustomKeyForAspNetWebServiceMetadataCache == null)
			{
				MethodInfo createKeyMethod = serverType.GetMethod("CreateCustomKeyForAspNetWebServiceMetadataCache", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.ExactBinding, null, new Type[]
				{
					typeof(Type),
					typeof(Type),
					typeof(string)
				}, null);
				if (createKeyMethod == null)
				{
					createCustomKeyForAspNetWebServiceMetadataCache = new ServerProtocol.CreateCustomKeyForAspNetWebServiceMetadataCache(ServerProtocol.DefaultCreateCustomKeyForAspNetWebServiceMetadataCache);
				}
				else
				{
					createCustomKeyForAspNetWebServiceMetadataCache = (Type pt, Type st, string originalString) => (string)createKeyMethod.Invoke(null, new object[] { pt, st, originalString });
				}
				HttpRuntime.Cache.Add(text, createCustomKeyForAspNetWebServiceMetadataCache, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
			}
			return createCustomKeyForAspNetWebServiceMetadataCache;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000090C0 File Offset: 0x000072C0
		private string CreateKey(Type protocolType, Type serverType, bool excludeSchemeHostPort = false, string keySuffix = null)
		{
			string fullName = protocolType.FullName;
			string fullName2 = serverType.FullName;
			string text = serverType.TypeHandle.Value.ToString();
			string text2 = (excludeSchemeHostPort ? this.Request.Url.AbsolutePath : this.Request.Url.GetLeftPart(UriPartial.Path));
			StringBuilder stringBuilder = new StringBuilder(fullName.Length + text2.Length + fullName2.Length + text.Length);
			stringBuilder.Append(fullName);
			stringBuilder.Append(text2);
			stringBuilder.Append(fullName2);
			stringBuilder.Append(text);
			if (keySuffix != null)
			{
				stringBuilder.Append(keySuffix);
			}
			return ServerProtocol.GetCreateCustomKeyForAspNetWebServiceMetadataCacheDelegate(serverType)(protocolType, serverType, stringBuilder.ToString());
		}

		/// <summary>Stores a <see cref="T:System.Object" /> in the cache using a key that is created from the specified protocol type and server type.</summary>
		/// <param name="protocolType">A <see cref="T:System.Type" /> that is used to create the key to store <paramref name="value" /> in the cache.</param>
		/// <param name="serverType">A <see cref="T:System.Type" /> that is used to create the key to store <paramref name="value" /> in the cache.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to be stored in the cache.</param>
		// Token: 0x060001E3 RID: 483 RVA: 0x00009183 File Offset: 0x00007383
		protected void AddToCache(Type protocolType, Type serverType, object value)
		{
			this.AddToCache(protocolType, serverType, value, false);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000918F File Offset: 0x0000738F
		internal void AddToCache(Type protocolType, Type serverType, object value, bool excludeSchemeHostPort)
		{
			PartialTrustHelpers.FailIfInPartialTrustOutsideAspNet();
			HttpRuntime.Cache.Insert(this.CreateKey(protocolType, serverType, excludeSchemeHostPort, null), value, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
		}

		/// <summary>Retrieves the <see cref="T:System.Object" /> that is stored in the cache using the key that was created from the specified protocol type and server type.</summary>
		/// <returns>The <see cref="T:System.Object" /> that is stored in the cache using the key that was created from <paramref name="protocolType" /> and <paramref name="serverType" />.</returns>
		/// <param name="protocolType">A <see cref="T:System.Type" /> that is used to create the key to retrieve <paramref name="value" /> from the cache.</param>
		/// <param name="serverType">A <see cref="T:System.Type" /> that is used to create the key to retrieve <paramref name="value" /> from the cache.</param>
		// Token: 0x060001E5 RID: 485 RVA: 0x000091B9 File Offset: 0x000073B9
		protected object GetFromCache(Type protocolType, Type serverType)
		{
			return this.GetFromCache(protocolType, serverType, false);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000091C4 File Offset: 0x000073C4
		internal object GetFromCache(Type protocolType, Type serverType, bool excludeSchemeHostPort)
		{
			PartialTrustHelpers.FailIfInPartialTrustOutsideAspNet();
			return HttpRuntime.Cache.Get(this.CreateKey(protocolType, serverType, excludeSchemeHostPort, null));
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000091E0 File Offset: 0x000073E0
		internal bool IsCacheUnderPressure(Type protocolType, Type serverType)
		{
			PartialTrustHelpers.FailIfInPartialTrustOutsideAspNet();
			string text = this.CreateKey(protocolType, serverType, true, "CachePressure");
			ServerProtocol.ServerProtocolCachePressure serverProtocolCachePressure = (ServerProtocol.ServerProtocolCachePressure)HttpRuntime.Cache.Get(text);
			if (serverProtocolCachePressure != null)
			{
				return serverProtocolCachePressure.Pressure < 10 && Interlocked.Increment(ref serverProtocolCachePressure.Pressure) >= 10;
			}
			HttpRuntime.Cache.Insert(text, new ServerProtocol.ServerProtocolCachePressure
			{
				Pressure = 1
			}, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
			return false;
		}

		// Token: 0x0400022F RID: 559
		private Type type;

		// Token: 0x04000230 RID: 560
		private HttpRequest request;

		// Token: 0x04000231 RID: 561
		private HttpResponse response;

		// Token: 0x04000232 RID: 562
		private HttpContext context;

		// Token: 0x04000233 RID: 563
		private object target;

		// Token: 0x04000234 RID: 564
		private WebMethodAttribute methodAttr;

		// Token: 0x04000235 RID: 565
		private static object s_InternalSyncObject;

		// Token: 0x02000053 RID: 83
		// (Invoke) Token: 0x060001EA RID: 490
		private delegate string CreateCustomKeyForAspNetWebServiceMetadataCache(Type protocolType, Type serverType, string originalKey);

		// Token: 0x02000054 RID: 84
		private class ServerProtocolCachePressure
		{
			// Token: 0x04000236 RID: 566
			public int Pressure;
		}
	}
}
