using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace System.Web.Services.Diagnostics
{
	// Token: 0x020000BB RID: 187
	internal static class Tracing
	{
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x00016A28 File Offset: 0x00014C28
		private static object InternalSyncObject
		{
			get
			{
				if (Tracing.internalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange(ref Tracing.internalSyncObject, obj, null);
				}
				return Tracing.internalSyncObject;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00016A54 File Offset: 0x00014C54
		internal static bool On
		{
			get
			{
				if (!Tracing.tracingInitialized)
				{
					Tracing.InitializeLogging();
				}
				return Tracing.tracingEnabled;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00016A67 File Offset: 0x00014C67
		internal static bool IsVerbose
		{
			get
			{
				return Tracing.ValidateSettings(Tracing.Asmx, TraceEventType.Verbose);
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x00016A75 File Offset: 0x00014C75
		internal static TraceSource Asmx
		{
			get
			{
				if (!Tracing.tracingInitialized)
				{
					Tracing.InitializeLogging();
				}
				if (!Tracing.tracingEnabled)
				{
					return null;
				}
				return Tracing.asmxTraceSource;
			}
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00016A94 File Offset: 0x00014C94
		private static void InitializeLogging()
		{
			object obj = Tracing.InternalSyncObject;
			lock (obj)
			{
				if (!Tracing.tracingInitialized)
				{
					bool flag2 = false;
					Tracing.asmxTraceSource = new TraceSource("System.Web.Services.Asmx");
					if (Tracing.asmxTraceSource.Switch.ShouldTrace(TraceEventType.Critical))
					{
						flag2 = true;
						AppDomain currentDomain = AppDomain.CurrentDomain;
						currentDomain.UnhandledException += Tracing.UnhandledExceptionHandler;
						currentDomain.DomainUnload += Tracing.AppDomainUnloadEvent;
						currentDomain.ProcessExit += Tracing.ProcessExitEvent;
					}
					Tracing.tracingEnabled = flag2;
					Tracing.tracingInitialized = true;
				}
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00016B40 File Offset: 0x00014D40
		private static void Close()
		{
			if (Tracing.asmxTraceSource != null)
			{
				Tracing.asmxTraceSource.Close();
			}
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00016B54 File Offset: 0x00014D54
		private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs args)
		{
			Exception ex = (Exception)args.ExceptionObject;
			Tracing.ExceptionCatch(TraceEventType.Error, sender, "UnhandledExceptionHandler", ex);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00016B7B File Offset: 0x00014D7B
		private static void ProcessExitEvent(object sender, EventArgs e)
		{
			Tracing.Close();
			Tracing.appDomainShutdown = true;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00016B7B File Offset: 0x00014D7B
		private static void AppDomainUnloadEvent(object sender, EventArgs e)
		{
			Tracing.Close();
			Tracing.appDomainShutdown = true;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00016B88 File Offset: 0x00014D88
		private static bool ValidateSettings(TraceSource traceSource, TraceEventType traceLevel)
		{
			if (!Tracing.tracingEnabled)
			{
				return false;
			}
			if (!Tracing.tracingInitialized)
			{
				Tracing.InitializeLogging();
			}
			return traceSource != null && traceSource.Switch.ShouldTrace(traceLevel) && !Tracing.appDomainShutdown;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00016BBC File Offset: 0x00014DBC
		internal static void Information(string format, params object[] args)
		{
			if (!Tracing.ValidateSettings(Tracing.Asmx, TraceEventType.Information))
			{
				return;
			}
			Tracing.TraceEvent(TraceEventType.Information, Res.GetString(format, args));
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0000210D File Offset: 0x0000030D
		private static void TraceEvent(TraceEventType eventType, string format)
		{
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00016BD9 File Offset: 0x00014DD9
		internal static Exception ExceptionThrow(TraceMethod method, Exception e)
		{
			return Tracing.ExceptionThrow(TraceEventType.Error, method, e);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00016BE4 File Offset: 0x00014DE4
		internal static Exception ExceptionThrow(TraceEventType eventType, TraceMethod method, Exception e)
		{
			if (!Tracing.ValidateSettings(Tracing.Asmx, eventType))
			{
				return e;
			}
			Tracing.TraceEvent(eventType, Res.GetString("TraceExceptionThrown", new object[]
			{
				method.ToString(),
				e.GetType(),
				e.Message
			}));
			Tracing.StackTrace(eventType, e);
			return e;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00016C39 File Offset: 0x00014E39
		internal static Exception ExceptionCatch(TraceMethod method, Exception e)
		{
			return Tracing.ExceptionCatch(TraceEventType.Error, method, e);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00016C44 File Offset: 0x00014E44
		internal static Exception ExceptionCatch(TraceEventType eventType, TraceMethod method, Exception e)
		{
			if (!Tracing.ValidateSettings(Tracing.Asmx, eventType))
			{
				return e;
			}
			Tracing.TraceEvent(eventType, Res.GetString("TraceExceptionCought", new object[]
			{
				method,
				e.GetType(),
				e.Message
			}));
			Tracing.StackTrace(eventType, e);
			return e;
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00016C94 File Offset: 0x00014E94
		internal static Exception ExceptionCatch(TraceEventType eventType, object target, string method, Exception e)
		{
			if (!Tracing.ValidateSettings(Tracing.Asmx, eventType))
			{
				return e;
			}
			Tracing.TraceEvent(eventType, Res.GetString("TraceExceptionCought", new object[]
			{
				TraceMethod.MethodId(target, method),
				e.GetType(),
				e.Message
			}));
			Tracing.StackTrace(eventType, e);
			return e;
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00016CEC File Offset: 0x00014EEC
		internal static Exception ExceptionIgnore(TraceEventType eventType, TraceMethod method, Exception e)
		{
			if (!Tracing.ValidateSettings(Tracing.Asmx, eventType))
			{
				return e;
			}
			Tracing.TraceEvent(eventType, Res.GetString("TraceExceptionIgnored", new object[]
			{
				method,
				e.GetType(),
				e.Message
			}));
			Tracing.StackTrace(eventType, e);
			return e;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00016D3C File Offset: 0x00014F3C
		private static void StackTrace(TraceEventType eventType, Exception e)
		{
			if (Tracing.IsVerbose && !string.IsNullOrEmpty(e.StackTrace))
			{
				Tracing.TraceEvent(eventType, Res.GetString("TraceExceptionDetails", new object[] { e.ToString() }));
			}
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00016D71 File Offset: 0x00014F71
		internal static string TraceId(string id)
		{
			return Res.GetString(id);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00016D7C File Offset: 0x00014F7C
		private static string GetHostByAddress(string ipAddress)
		{
			string text;
			try
			{
				text = Dns.GetHostByAddress(ipAddress).HostName;
			}
			catch
			{
				text = null;
			}
			return text;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00016DB0 File Offset: 0x00014FB0
		internal static List<string> Details(HttpRequest request)
		{
			if (request == null)
			{
				return null;
			}
			List<string> list = new List<string>();
			list.Add(Res.GetString("TraceUserHostAddress", new object[] { request.UserHostAddress }));
			string text = ((request.UserHostAddress == request.UserHostName) ? Tracing.GetHostByAddress(request.UserHostAddress) : request.UserHostName);
			if (!string.IsNullOrEmpty(text))
			{
				list.Add(Res.GetString("TraceUserHostName", new object[] { text }));
			}
			list.Add(Res.GetString("TraceUrl", new object[] { request.HttpMethod, request.Url }));
			if (request.UrlReferrer != null)
			{
				list.Add(Res.GetString("TraceUrlReferrer", new object[] { request.UrlReferrer }));
			}
			return list;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00016E88 File Offset: 0x00015088
		internal static void Enter(string callId, TraceMethod caller)
		{
			Tracing.Enter(callId, caller, null, null);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00016E93 File Offset: 0x00015093
		internal static void Enter(string callId, TraceMethod caller, List<string> details)
		{
			Tracing.Enter(callId, caller, null, details);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00016E9E File Offset: 0x0001509E
		internal static void Enter(string callId, TraceMethod caller, TraceMethod callDetails)
		{
			Tracing.Enter(callId, caller, callDetails, null);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00016EAC File Offset: 0x000150AC
		internal static void Enter(string callId, TraceMethod caller, TraceMethod callDetails, List<string> details)
		{
			if (!Tracing.ValidateSettings(Tracing.Asmx, TraceEventType.Information))
			{
				return;
			}
			string text = ((callDetails == null) ? Res.GetString("TraceCallEnter", new object[] { callId, caller }) : Res.GetString("TraceCallEnterDetails", new object[] { callId, caller, callDetails }));
			if (details != null && details.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder(text);
				foreach (string text2 in details)
				{
					stringBuilder.Append(Environment.NewLine);
					stringBuilder.Append("    ");
					stringBuilder.Append(text2);
				}
				text = stringBuilder.ToString();
			}
			Tracing.TraceEvent(TraceEventType.Information, text);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00016F7C File Offset: 0x0001517C
		internal static XmlDeserializationEvents GetDeserializationEvents()
		{
			return new XmlDeserializationEvents
			{
				OnUnknownElement = new XmlElementEventHandler(Tracing.OnUnknownElement),
				OnUnknownAttribute = new XmlAttributeEventHandler(Tracing.OnUnknownAttribute)
			};
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00016FB8 File Offset: 0x000151B8
		internal static void Exit(string callId, TraceMethod caller)
		{
			if (!Tracing.ValidateSettings(Tracing.Asmx, TraceEventType.Information))
			{
				return;
			}
			Tracing.TraceEvent(TraceEventType.Information, Res.GetString("TraceCallExit", new object[] { callId, caller }));
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00016FE8 File Offset: 0x000151E8
		internal static void OnUnknownElement(object sender, XmlElementEventArgs e)
		{
			if (!Tracing.ValidateSettings(Tracing.Asmx, TraceEventType.Warning))
			{
				return;
			}
			if (e.Element == null)
			{
				return;
			}
			string text = RuntimeUtils.ElementString(e.Element);
			string text2 = ((e.ExpectedElements == null) ? "WebUnknownElement" : ((e.ExpectedElements.Length == 0) ? "WebUnknownElement1" : "WebUnknownElement2"));
			Tracing.TraceEvent(TraceEventType.Warning, Res.GetString(text2, new object[] { text, e.ExpectedElements }));
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00017060 File Offset: 0x00015260
		internal static void OnUnknownAttribute(object sender, XmlAttributeEventArgs e)
		{
			if (!Tracing.ValidateSettings(Tracing.Asmx, TraceEventType.Warning))
			{
				return;
			}
			if (e.Attr == null)
			{
				return;
			}
			if (RuntimeUtils.IsKnownNamespace(e.Attr.NamespaceURI))
			{
				return;
			}
			string text = ((e.ExpectedAttributes == null) ? "WebUnknownAttribute" : ((e.ExpectedAttributes.Length == 0) ? "WebUnknownAttribute2" : "WebUnknownAttribute3"));
			Tracing.TraceEvent(TraceEventType.Warning, Res.GetString(text, new object[]
			{
				e.Attr.Name,
				e.Attr.Value,
				e.ExpectedAttributes
			}));
		}

		// Token: 0x04000369 RID: 873
		private static bool tracingEnabled = true;

		// Token: 0x0400036A RID: 874
		private static bool tracingInitialized;

		// Token: 0x0400036B RID: 875
		private static bool appDomainShutdown;

		// Token: 0x0400036C RID: 876
		private const string TraceSourceAsmx = "System.Web.Services.Asmx";

		// Token: 0x0400036D RID: 877
		private static TraceSource asmxTraceSource;

		// Token: 0x0400036E RID: 878
		private static object internalSyncObject;
	}
}
