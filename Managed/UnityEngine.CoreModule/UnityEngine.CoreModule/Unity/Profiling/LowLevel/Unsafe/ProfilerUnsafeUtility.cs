using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Profiling.LowLevel.Unsafe
{
	// Token: 0x02000032 RID: 50
	[UsedByNativeCode]
	[NativeHeader("Runtime/Profiler/ScriptBindings/ProfilerMarker.bindings.h")]
	public static class ProfilerUnsafeUtility
	{
		// Token: 0x06000064 RID: 100
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern IntPtr CreateMarker(string name, ushort categoryId, MarkerFlags flags, int metadataCount);

		// Token: 0x06000065 RID: 101
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern void SetMarkerMetadata(IntPtr markerPtr, int index, string name, byte type, byte unit);

		// Token: 0x06000066 RID: 102
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern void BeginSample(IntPtr markerPtr);

		// Token: 0x06000067 RID: 103
		[ThreadSafe]
		[MethodImpl(4096)]
		public unsafe static extern void BeginSampleWithMetadata(IntPtr markerPtr, int metadataCount, void* metadata);

		// Token: 0x06000068 RID: 104
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern void EndSample(IntPtr markerPtr);

		// Token: 0x06000069 RID: 105
		[ThreadSafe]
		[MethodImpl(4096)]
		public unsafe static extern void SingleSampleWithMetadata(IntPtr markerPtr, int metadataCount, void* metadata);

		// Token: 0x0600006A RID: 106
		[ThreadSafe]
		[MethodImpl(4096)]
		public unsafe static extern void* CreateCounterValue(out IntPtr counterPtr, string name, ushort categoryId, MarkerFlags flags, byte dataType, byte dataUnit, int dataSize, ProfilerCounterOptions counterOptions);

		// Token: 0x0600006B RID: 107
		[ThreadSafe]
		[MethodImpl(4096)]
		public unsafe static extern void FlushCounterValue(void* counterValuePtr);

		// Token: 0x0600006C RID: 108
		[ThreadSafe]
		[MethodImpl(4096)]
		internal static extern void Internal_BeginWithObject(IntPtr markerPtr, Object contextUnityObject);

		// Token: 0x0600006D RID: 109
		[NativeConditional("ENABLE_PROFILER")]
		[MethodImpl(4096)]
		internal static extern string Internal_GetName(IntPtr markerPtr);

		// Token: 0x040000BB RID: 187
		public const ushort CategoryRender = 0;

		// Token: 0x040000BC RID: 188
		public const ushort CategoryScripts = 1;

		// Token: 0x040000BD RID: 189
		public const ushort CategoryGUI = 4;

		// Token: 0x040000BE RID: 190
		public const ushort CategoryPhysics = 5;

		// Token: 0x040000BF RID: 191
		public const ushort CategoryAnimation = 6;

		// Token: 0x040000C0 RID: 192
		public const ushort CategoryAi = 7;

		// Token: 0x040000C1 RID: 193
		public const ushort CategoryAudio = 8;

		// Token: 0x040000C2 RID: 194
		public const ushort CategoryVideo = 11;

		// Token: 0x040000C3 RID: 195
		public const ushort CategoryParticles = 12;

		// Token: 0x040000C4 RID: 196
		public const ushort CategoryLightning = 13;

		// Token: 0x040000C5 RID: 197
		public const ushort CategoryNetwork = 14;

		// Token: 0x040000C6 RID: 198
		public const ushort CategoryLoading = 15;

		// Token: 0x040000C7 RID: 199
		public const ushort CategoryOther = 16;

		// Token: 0x040000C8 RID: 200
		public const ushort CategoryVr = 22;

		// Token: 0x040000C9 RID: 201
		public const ushort CategoryAllocation = 23;

		// Token: 0x040000CA RID: 202
		public const ushort CategoryInput = 30;
	}
}
