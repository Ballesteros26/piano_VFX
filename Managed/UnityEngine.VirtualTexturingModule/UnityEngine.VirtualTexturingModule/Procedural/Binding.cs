using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering.VirtualTexturing.Procedural
{
	// Token: 0x0200000C RID: 12
	[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h")]
	[StaticAccessor("VirtualTexturing::Procedural", StaticAccessorType.DoubleColon)]
	internal static class Binding
	{
		// Token: 0x06000022 RID: 34 RVA: 0x000021F4 File Offset: 0x000003F4
		internal static ulong Create(CreationParameters p)
		{
			return Binding.Create_Injected(ref p);
		}

		// Token: 0x06000023 RID: 35
		[MethodImpl(4096)]
		internal static extern void Destroy(ulong handle);

		// Token: 0x06000024 RID: 36
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern int PopRequests(ulong handle, IntPtr requestHandles);

		// Token: 0x06000025 RID: 37
		[ThreadSafe]
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern void GetRequestParameters(IntPtr requestHandles, IntPtr requestParameters, int length);

		// Token: 0x06000026 RID: 38
		[ThreadSafe]
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern void UpdateRequestState(IntPtr requestHandles, IntPtr requestUpdates, int length);

		// Token: 0x06000027 RID: 39
		[MethodImpl(4096)]
		internal static extern void BindToMaterialPropertyBlock(ulong handle, [NotNull] MaterialPropertyBlock material, string name);

		// Token: 0x06000028 RID: 40
		[MethodImpl(4096)]
		internal static extern void BindToMaterial(ulong handle, [NotNull] Material material, string name);

		// Token: 0x06000029 RID: 41
		[MethodImpl(4096)]
		internal static extern void BindGlobally(ulong handle, string name);

		// Token: 0x0600002A RID: 42 RVA: 0x000021FD File Offset: 0x000003FD
		[NativeThrows]
		public static void RequestRegion(ulong handle, Rect r, int mipMap, int numMips)
		{
			Binding.RequestRegion_Injected(handle, ref r, mipMap, numMips);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002209 File Offset: 0x00000409
		[NativeThrows]
		public static void InvalidateRegion(ulong handle, Rect r, int mipMap, int numMips)
		{
			Binding.InvalidateRegion_Injected(handle, ref r, mipMap, numMips);
		}

		// Token: 0x0600002C RID: 44
		[MethodImpl(4096)]
		private static extern ulong Create_Injected(ref CreationParameters p);

		// Token: 0x0600002D RID: 45
		[MethodImpl(4096)]
		private static extern void RequestRegion_Injected(ulong handle, ref Rect r, int mipMap, int numMips);

		// Token: 0x0600002E RID: 46
		[MethodImpl(4096)]
		private static extern void InvalidateRegion_Injected(ulong handle, ref Rect r, int mipMap, int numMips);
	}
}
