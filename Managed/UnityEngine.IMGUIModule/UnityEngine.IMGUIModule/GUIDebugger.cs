using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000014 RID: 20
	[NativeHeader("Modules/IMGUI/GUIDebugger.bindings.h")]
	internal class GUIDebugger
	{
		// Token: 0x06000190 RID: 400 RVA: 0x00007876 File Offset: 0x00005A76
		[NativeConditional("UNITY_EDITOR")]
		public static void LogLayoutEntry(Rect rect, int left, int right, int top, int bottom, GUIStyle style)
		{
			GUIDebugger.LogLayoutEntry_Injected(ref rect, left, right, top, bottom, style);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00007886 File Offset: 0x00005A86
		[NativeConditional("UNITY_EDITOR")]
		public static void LogLayoutGroupEntry(Rect rect, int left, int right, int top, int bottom, GUIStyle style, bool isVertical)
		{
			GUIDebugger.LogLayoutGroupEntry_Injected(ref rect, left, right, top, bottom, style, isVertical);
		}

		// Token: 0x06000192 RID: 402
		[NativeConditional("UNITY_EDITOR")]
		[NativeMethod("LogEndGroup")]
		[StaticAccessor("GetGUIDebuggerManager()", StaticAccessorType.Dot)]
		[MethodImpl(4096)]
		public static extern void LogLayoutEndGroup();

		// Token: 0x06000193 RID: 403 RVA: 0x00007898 File Offset: 0x00005A98
		[NativeConditional("UNITY_EDITOR")]
		[StaticAccessor("GetGUIDebuggerManager()", StaticAccessorType.Dot)]
		public static void LogBeginProperty(string targetTypeAssemblyQualifiedName, string path, Rect position)
		{
			GUIDebugger.LogBeginProperty_Injected(targetTypeAssemblyQualifiedName, path, ref position);
		}

		// Token: 0x06000194 RID: 404
		[NativeConditional("UNITY_EDITOR")]
		[StaticAccessor("GetGUIDebuggerManager()", StaticAccessorType.Dot)]
		[MethodImpl(4096)]
		public static extern void LogEndProperty();

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000195 RID: 405
		[NativeConditional("UNITY_EDITOR")]
		public static extern bool active
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000197 RID: 407
		[MethodImpl(4096)]
		private static extern void LogLayoutEntry_Injected(ref Rect rect, int left, int right, int top, int bottom, GUIStyle style);

		// Token: 0x06000198 RID: 408
		[MethodImpl(4096)]
		private static extern void LogLayoutGroupEntry_Injected(ref Rect rect, int left, int right, int top, int bottom, GUIStyle style, bool isVertical);

		// Token: 0x06000199 RID: 409
		[MethodImpl(4096)]
		private static extern void LogBeginProperty_Injected(string targetTypeAssemblyQualifiedName, string path, ref Rect position);
	}
}
