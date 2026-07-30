using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000BD RID: 189
	[NativeHeader("Runtime/Export/Debug/Debug.bindings.h")]
	public class Debug
	{
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x00006934 File Offset: 0x00004B34
		public static ILogger unityLogger
		{
			get
			{
				return Debug.s_Logger;
			}
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000694C File Offset: 0x00004B4C
		[ExcludeFromDocs]
		public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration)
		{
			bool flag = true;
			Debug.DrawLine(start, end, color, duration, flag);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00006968 File Offset: 0x00004B68
		[ExcludeFromDocs]
		public static void DrawLine(Vector3 start, Vector3 end, Color color)
		{
			bool flag = true;
			float num = 0f;
			Debug.DrawLine(start, end, color, num, flag);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000698C File Offset: 0x00004B8C
		[ExcludeFromDocs]
		public static void DrawLine(Vector3 start, Vector3 end)
		{
			bool flag = true;
			float num = 0f;
			Color white = Color.white;
			Debug.DrawLine(start, end, white, num, flag);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x000069B3 File Offset: 0x00004BB3
		[FreeFunction("DebugDrawLine", IsThreadSafe = true)]
		public static void DrawLine(Vector3 start, Vector3 end, [DefaultValue("Color.white")] Color color, [DefaultValue("0.0f")] float duration, [DefaultValue("true")] bool depthTest)
		{
			Debug.DrawLine_Injected(ref start, ref end, ref color, duration, depthTest);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x000069C4 File Offset: 0x00004BC4
		[ExcludeFromDocs]
		public static void DrawRay(Vector3 start, Vector3 dir, Color color, float duration)
		{
			bool flag = true;
			Debug.DrawRay(start, dir, color, duration, flag);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x000069E0 File Offset: 0x00004BE0
		[ExcludeFromDocs]
		public static void DrawRay(Vector3 start, Vector3 dir, Color color)
		{
			bool flag = true;
			float num = 0f;
			Debug.DrawRay(start, dir, color, num, flag);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00006A04 File Offset: 0x00004C04
		[ExcludeFromDocs]
		public static void DrawRay(Vector3 start, Vector3 dir)
		{
			bool flag = true;
			float num = 0f;
			Color white = Color.white;
			Debug.DrawRay(start, dir, white, num, flag);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00006A2B File Offset: 0x00004C2B
		public static void DrawRay(Vector3 start, Vector3 dir, [DefaultValue("Color.white")] Color color, [DefaultValue("0.0f")] float duration, [DefaultValue("true")] bool depthTest)
		{
			Debug.DrawLine(start, start + dir, color, duration, depthTest);
		}

		// Token: 0x06000477 RID: 1143
		[FreeFunction("PauseEditor")]
		[MethodImpl(4096)]
		public static extern void Break();

		// Token: 0x06000478 RID: 1144
		[MethodImpl(4096)]
		public static extern void DebugBreak();

		// Token: 0x06000479 RID: 1145 RVA: 0x00006A40 File Offset: 0x00004C40
		public static void Log(object message)
		{
			Debug.unityLogger.Log(LogType.Log, message);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00006A50 File Offset: 0x00004C50
		public static void Log(object message, Object context)
		{
			Debug.unityLogger.Log(LogType.Log, message, context);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00006A61 File Offset: 0x00004C61
		public static void LogFormat(string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Log, format, args);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00006A72 File Offset: 0x00004C72
		public static void LogFormat(Object context, string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Log, context, format, args);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00006A84 File Offset: 0x00004C84
		public static void LogFormat(LogType logType, LogOption logOptions, Object context, string format, params object[] args)
		{
			DebugLogHandler debugLogHandler = Debug.unityLogger.logHandler as DebugLogHandler;
			bool flag = debugLogHandler == null;
			if (flag)
			{
				Debug.unityLogger.LogFormat(logType, context, format, args);
			}
			else
			{
				debugLogHandler.LogFormat(logType, logOptions, context, format, args);
			}
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00006AC9 File Offset: 0x00004CC9
		public static void LogError(object message)
		{
			Debug.unityLogger.Log(LogType.Error, message);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00006AD9 File Offset: 0x00004CD9
		public static void LogError(object message, Object context)
		{
			Debug.unityLogger.Log(LogType.Error, message, context);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00006AEA File Offset: 0x00004CEA
		public static void LogErrorFormat(string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Error, format, args);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00006AFB File Offset: 0x00004CFB
		public static void LogErrorFormat(Object context, string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Error, context, format, args);
		}

		// Token: 0x06000482 RID: 1154
		[MethodImpl(4096)]
		public static extern void ClearDeveloperConsole();

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000483 RID: 1155
		// (set) Token: 0x06000484 RID: 1156
		public static extern bool developerConsoleVisible
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00006B0D File Offset: 0x00004D0D
		public static void LogException(Exception exception)
		{
			Debug.unityLogger.LogException(exception, null);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00006B1D File Offset: 0x00004D1D
		public static void LogException(Exception exception, Object context)
		{
			Debug.unityLogger.LogException(exception, context);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00006B2D File Offset: 0x00004D2D
		public static void LogWarning(object message)
		{
			Debug.unityLogger.Log(LogType.Warning, message);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00006B3D File Offset: 0x00004D3D
		public static void LogWarning(object message, Object context)
		{
			Debug.unityLogger.Log(LogType.Warning, message, context);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00006B4E File Offset: 0x00004D4E
		public static void LogWarningFormat(string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Warning, format, args);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00006B5F File Offset: 0x00004D5F
		public static void LogWarningFormat(Object context, string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Warning, context, format, args);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00006B74 File Offset: 0x00004D74
		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.Log(LogType.Assert, "Assertion failed");
			}
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00006B9C File Offset: 0x00004D9C
		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition, Object context)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.Log(LogType.Assert, "Assertion failed", context);
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00006BC4 File Offset: 0x00004DC4
		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition, object message)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.Log(LogType.Assert, message);
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00006BE8 File Offset: 0x00004DE8
		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition, string message)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.Log(LogType.Assert, message);
			}
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00006C0C File Offset: 0x00004E0C
		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition, object message, Object context)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.Log(LogType.Assert, message, context);
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00006C30 File Offset: 0x00004E30
		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition, string message, Object context)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.Log(LogType.Assert, message, context);
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00006C54 File Offset: 0x00004E54
		[Conditional("UNITY_ASSERTIONS")]
		public static void AssertFormat(bool condition, string format, params object[] args)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.LogFormat(LogType.Assert, format, args);
			}
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00006C78 File Offset: 0x00004E78
		[Conditional("UNITY_ASSERTIONS")]
		public static void AssertFormat(bool condition, Object context, string format, params object[] args)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.LogFormat(LogType.Assert, context, format, args);
			}
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00006C9D File Offset: 0x00004E9D
		[Conditional("UNITY_ASSERTIONS")]
		public static void LogAssertion(object message)
		{
			Debug.unityLogger.Log(LogType.Assert, message);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00006CAD File Offset: 0x00004EAD
		[Conditional("UNITY_ASSERTIONS")]
		public static void LogAssertion(object message, Object context)
		{
			Debug.unityLogger.Log(LogType.Assert, message, context);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00006CBE File Offset: 0x00004EBE
		[Conditional("UNITY_ASSERTIONS")]
		public static void LogAssertionFormat(string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Assert, format, args);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00006CCF File Offset: 0x00004ECF
		[Conditional("UNITY_ASSERTIONS")]
		public static void LogAssertionFormat(Object context, string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Assert, context, format, args);
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000497 RID: 1175
		[StaticAccessor("GetBuildSettings()", StaticAccessorType.Dot)]
		[NativeProperty(TargetType = TargetType.Field)]
		public static extern bool isDebugBuild
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000498 RID: 1176
		[FreeFunction("DeveloperConsole_OpenConsoleFile")]
		[MethodImpl(4096)]
		internal static extern void OpenConsoleFile();

		// Token: 0x06000499 RID: 1177
		[MethodImpl(4096)]
		internal static extern void GetDiagnosticSwitches(List<DiagnosticSwitch> results);

		// Token: 0x0600049A RID: 1178
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern object GetDiagnosticSwitch(string name);

		// Token: 0x0600049B RID: 1179
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern void SetDiagnosticSwitch(string name, object value, bool setPersistent);

		// Token: 0x0600049C RID: 1180 RVA: 0x00006CE4 File Offset: 0x00004EE4
		[RequiredByNativeCode]
		internal static bool CallOverridenDebugHandler(Exception exception, Object obj)
		{
			bool flag = Debug.s_Logger.logHandler is DebugLogHandler;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				Debug.s_Logger.LogException(exception, obj);
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00006D20 File Offset: 0x00004F20
		[Conditional("UNITY_ASSERTIONS")]
		[EditorBrowsable(1)]
		[Obsolete("Assert(bool, string, params object[]) is obsolete. Use AssertFormat(bool, string, params object[]) (UnityUpgradable) -> AssertFormat(*)", true)]
		public static void Assert(bool condition, string format, params object[] args)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.LogFormat(LogType.Assert, format, args);
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x00006D44 File Offset: 0x00004F44
		[EditorBrowsable(1)]
		[Obsolete("Debug.logger is obsolete. Please use Debug.unityLogger instead (UnityUpgradable) -> unityLogger")]
		public static ILogger logger
		{
			get
			{
				return Debug.s_Logger;
			}
		}

		// Token: 0x060004A1 RID: 1185
		[MethodImpl(4096)]
		private static extern void DrawLine_Injected(ref Vector3 start, ref Vector3 end, [DefaultValue("Color.white")] ref Color color, [DefaultValue("0.0f")] float duration, [DefaultValue("true")] bool depthTest);

		// Token: 0x04000228 RID: 552
		internal static ILogger s_Logger = new Logger(new DebugLogHandler());
	}
}
