using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling.LowLevel;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Profiling
{
	// Token: 0x0200002A RID: 42
	[UsedByNativeCode]
	[NativeHeader("Runtime/Profiler/ScriptBindings/ProfilerMarker.bindings.h")]
	public struct ProfilerMarker
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000023AE File Offset: 0x000005AE
		public IntPtr Handle
		{
			get
			{
				return this.m_Ptr;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000023B6 File Offset: 0x000005B6
		[MethodImpl(256)]
		public ProfilerMarker(string name)
		{
			this.m_Ptr = ProfilerUnsafeUtility.CreateMarker(name, 1, MarkerFlags.Default, 0);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000023C8 File Offset: 0x000005C8
		[Conditional("ENABLE_PROFILER")]
		[Pure]
		[MethodImpl(256)]
		public void Begin()
		{
			ProfilerUnsafeUtility.BeginSample(this.m_Ptr);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000023D7 File Offset: 0x000005D7
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(256)]
		public void Begin(Object contextUnityObject)
		{
			ProfilerUnsafeUtility.Internal_BeginWithObject(this.m_Ptr, contextUnityObject);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000023E7 File Offset: 0x000005E7
		[Conditional("ENABLE_PROFILER")]
		[Pure]
		[MethodImpl(256)]
		public void End()
		{
			ProfilerUnsafeUtility.EndSample(this.m_Ptr);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000023F6 File Offset: 0x000005F6
		[Conditional("ENABLE_PROFILER")]
		internal void GetName(ref string name)
		{
			name = ProfilerUnsafeUtility.Internal_GetName(this.m_Ptr);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002408 File Offset: 0x00000608
		[Pure]
		[MethodImpl(256)]
		public ProfilerMarker.AutoScope Auto()
		{
			return new ProfilerMarker.AutoScope(this.m_Ptr);
		}

		// Token: 0x04000094 RID: 148
		[NativeDisableUnsafePtrRestriction]
		[NonSerialized]
		internal readonly IntPtr m_Ptr;

		// Token: 0x0200002B RID: 43
		[UsedByNativeCode]
		public struct AutoScope : IDisposable
		{
			// Token: 0x06000062 RID: 98 RVA: 0x00002425 File Offset: 0x00000625
			[MethodImpl(256)]
			internal AutoScope(IntPtr markerPtr)
			{
				this.m_Ptr = markerPtr;
				ProfilerUnsafeUtility.BeginSample(markerPtr);
			}

			// Token: 0x06000063 RID: 99 RVA: 0x00002436 File Offset: 0x00000636
			[MethodImpl(256)]
			public void Dispose()
			{
				ProfilerUnsafeUtility.EndSample(this.m_Ptr);
			}

			// Token: 0x04000095 RID: 149
			[NativeDisableUnsafePtrRestriction]
			internal readonly IntPtr m_Ptr;
		}
	}
}
