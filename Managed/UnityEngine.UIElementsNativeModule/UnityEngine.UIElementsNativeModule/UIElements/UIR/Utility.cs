using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000028 RID: 40
	[NativeHeader("Modules/UIElementsNative/UIRendererUtility.h")]
	[VisibleToOtherModules(new string[] { "Unity.UIElements" })]
	internal class Utility
	{
		// Token: 0x0600016D RID: 365 RVA: 0x00003E78 File Offset: 0x00002078
		public static void SetVectorArray<T>(MaterialPropertyBlock props, int name, NativeSlice<T> vector4s) where T : struct
		{
			int num = vector4s.Length * vector4s.Stride / 16;
			Utility.SetVectorArray(props, name, new IntPtr(vector4s.GetUnsafePtr<T>()), num);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600016E RID: 366 RVA: 0x00003EB0 File Offset: 0x000020B0
		// (remove) Token: 0x0600016F RID: 367 RVA: 0x00003EE4 File Offset: 0x000020E4
		[field: DebuggerBrowsable(0)]
		public static event Action<bool> GraphicsResourcesRecreate;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000170 RID: 368 RVA: 0x00003F18 File Offset: 0x00002118
		// (remove) Token: 0x06000171 RID: 369 RVA: 0x00003F4C File Offset: 0x0000214C
		[field: DebuggerBrowsable(0)]
		public static event Action EngineUpdate;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000172 RID: 370 RVA: 0x00003F80 File Offset: 0x00002180
		// (remove) Token: 0x06000173 RID: 371 RVA: 0x00003FB4 File Offset: 0x000021B4
		[field: DebuggerBrowsable(0)]
		public static event Action FlushPendingResources;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000174 RID: 372 RVA: 0x00003FE8 File Offset: 0x000021E8
		// (remove) Token: 0x06000175 RID: 373 RVA: 0x0000401C File Offset: 0x0000221C
		[field: DebuggerBrowsable(0)]
		public static event Action<Camera> RegisterIntermediateRenderers;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000176 RID: 374 RVA: 0x00004050 File Offset: 0x00002250
		// (remove) Token: 0x06000177 RID: 375 RVA: 0x00004084 File Offset: 0x00002284
		[field: DebuggerBrowsable(0)]
		public static event Action<IntPtr> RenderNodeAdd;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000178 RID: 376 RVA: 0x000040B8 File Offset: 0x000022B8
		// (remove) Token: 0x06000179 RID: 377 RVA: 0x000040EC File Offset: 0x000022EC
		[field: DebuggerBrowsable(0)]
		public static event Action<IntPtr> RenderNodeExecute;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600017A RID: 378 RVA: 0x00004120 File Offset: 0x00002320
		// (remove) Token: 0x0600017B RID: 379 RVA: 0x00004154 File Offset: 0x00002354
		[field: DebuggerBrowsable(0)]
		public static event Action<IntPtr> RenderNodeCleanup;

		// Token: 0x0600017C RID: 380 RVA: 0x00004187 File Offset: 0x00002387
		[RequiredByNativeCode]
		internal static void RaiseGraphicsResourcesRecreate(bool recreate)
		{
			Action<bool> graphicsResourcesRecreate = Utility.GraphicsResourcesRecreate;
			if (graphicsResourcesRecreate != null)
			{
				graphicsResourcesRecreate.Invoke(recreate);
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000419C File Offset: 0x0000239C
		[RequiredByNativeCode]
		internal static void RaiseEngineUpdate()
		{
			bool flag = Utility.EngineUpdate != null;
			if (flag)
			{
				Utility.EngineUpdate.Invoke();
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000041C3 File Offset: 0x000023C3
		[RequiredByNativeCode]
		internal static void RaiseFlushPendingResources()
		{
			Action flushPendingResources = Utility.FlushPendingResources;
			if (flushPendingResources != null)
			{
				flushPendingResources.Invoke();
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000041D7 File Offset: 0x000023D7
		[RequiredByNativeCode]
		internal static void RaiseRegisterIntermediateRenderers(Camera camera)
		{
			Action<Camera> registerIntermediateRenderers = Utility.RegisterIntermediateRenderers;
			if (registerIntermediateRenderers != null)
			{
				registerIntermediateRenderers.Invoke(camera);
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000041EC File Offset: 0x000023EC
		[RequiredByNativeCode]
		internal static void RaiseRenderNodeAdd(IntPtr userData)
		{
			Action<IntPtr> renderNodeAdd = Utility.RenderNodeAdd;
			if (renderNodeAdd != null)
			{
				renderNodeAdd.Invoke(userData);
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00004201 File Offset: 0x00002401
		[RequiredByNativeCode]
		internal static void RaiseRenderNodeExecute(IntPtr userData)
		{
			Action<IntPtr> renderNodeExecute = Utility.RenderNodeExecute;
			if (renderNodeExecute != null)
			{
				renderNodeExecute.Invoke(userData);
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00004216 File Offset: 0x00002416
		[RequiredByNativeCode]
		internal static void RaiseRenderNodeCleanup(IntPtr userData)
		{
			Action<IntPtr> renderNodeCleanup = Utility.RenderNodeCleanup;
			if (renderNodeCleanup != null)
			{
				renderNodeCleanup.Invoke(userData);
			}
		}

		// Token: 0x06000183 RID: 387
		[ThreadSafe]
		[MethodImpl(4096)]
		private static extern IntPtr AllocateBuffer(int elementCount, int elementStride, bool vertexBuffer);

		// Token: 0x06000184 RID: 388
		[ThreadSafe]
		[MethodImpl(4096)]
		private static extern void FreeBuffer(IntPtr buffer);

		// Token: 0x06000185 RID: 389
		[ThreadSafe]
		[MethodImpl(4096)]
		private static extern void UpdateBufferRanges(IntPtr buffer, IntPtr ranges, int rangeCount, int writeRangeStart, int writeRangeEnd);

		// Token: 0x06000186 RID: 390
		[ThreadSafe]
		[MethodImpl(4096)]
		private static extern void SetVectorArray(MaterialPropertyBlock props, int name, IntPtr vector4s, int count);

		// Token: 0x06000187 RID: 391
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern IntPtr GetVertexDeclaration(VertexAttributeDescriptor[] vertexAttributes);

		// Token: 0x06000188 RID: 392 RVA: 0x0000422C File Offset: 0x0000242C
		public static void RegisterIntermediateRenderer(Camera camera, Material material, Matrix4x4 transform, Bounds aabb, int renderLayer, int shadowCasting, bool receiveShadows, int sameDistanceSortPriority, ulong sceneCullingMask, int rendererCallbackFlags, IntPtr userData, int userDataSize)
		{
			Utility.RegisterIntermediateRenderer_Injected(camera, material, ref transform, ref aabb, renderLayer, shadowCasting, receiveShadows, sameDistanceSortPriority, sceneCullingMask, rendererCallbackFlags, userData, userDataSize);
		}

		// Token: 0x06000189 RID: 393
		[ThreadSafe]
		[MethodImpl(4096)]
		public unsafe static extern void DrawRanges(IntPtr ib, IntPtr* vertexStreams, int streamCount, IntPtr ranges, int rangeCount, IntPtr vertexDecl);

		// Token: 0x0600018A RID: 394
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern void SetPropertyBlock(MaterialPropertyBlock props);

		// Token: 0x0600018B RID: 395 RVA: 0x00004254 File Offset: 0x00002454
		[ThreadSafe]
		public static void SetScissorRect(RectInt scissorRect)
		{
			Utility.SetScissorRect_Injected(ref scissorRect);
		}

		// Token: 0x0600018C RID: 396
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern void DisableScissor();

		// Token: 0x0600018D RID: 397
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern bool IsScissorEnabled();

		// Token: 0x0600018E RID: 398
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern uint InsertCPUFence();

		// Token: 0x0600018F RID: 399
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern bool CPUFencePassed(uint fence);

		// Token: 0x06000190 RID: 400
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern void WaitForCPUFencePassed(uint fence);

		// Token: 0x06000191 RID: 401
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern void SyncRenderThread();

		// Token: 0x06000192 RID: 402 RVA: 0x00004260 File Offset: 0x00002460
		[ThreadSafe]
		public static RectInt GetActiveViewport()
		{
			RectInt rectInt;
			Utility.GetActiveViewport_Injected(out rectInt);
			return rectInt;
		}

		// Token: 0x06000193 RID: 403
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern void ProfileDrawChainBegin();

		// Token: 0x06000194 RID: 404
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern void ProfileDrawChainEnd();

		// Token: 0x06000195 RID: 405
		[MethodImpl(4096)]
		public static extern void ProfileImmediateRendererBegin();

		// Token: 0x06000196 RID: 406
		[MethodImpl(4096)]
		public static extern void ProfileImmediateRendererEnd();

		// Token: 0x06000197 RID: 407
		[MethodImpl(4096)]
		public static extern void NotifyOfUIREvents(bool subscribe);

		// Token: 0x06000198 RID: 408 RVA: 0x00004278 File Offset: 0x00002478
		[ThreadSafe]
		public static Matrix4x4 GetUnityProjectionMatrix()
		{
			Matrix4x4 matrix4x;
			Utility.GetUnityProjectionMatrix_Injected(out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00004290 File Offset: 0x00002490
		[ThreadSafe]
		public static Matrix4x4 GetDeviceProjectionMatrix()
		{
			Matrix4x4 matrix4x;
			Utility.GetDeviceProjectionMatrix_Injected(out matrix4x);
			return matrix4x;
		}

		// Token: 0x0600019A RID: 410
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern bool DebugIsMainThread();

		// Token: 0x0600019D RID: 413
		[MethodImpl(4096)]
		private static extern void RegisterIntermediateRenderer_Injected(Camera camera, Material material, ref Matrix4x4 transform, ref Bounds aabb, int renderLayer, int shadowCasting, bool receiveShadows, int sameDistanceSortPriority, ulong sceneCullingMask, int rendererCallbackFlags, IntPtr userData, int userDataSize);

		// Token: 0x0600019E RID: 414
		[MethodImpl(4096)]
		private static extern void SetScissorRect_Injected(ref RectInt scissorRect);

		// Token: 0x0600019F RID: 415
		[MethodImpl(4096)]
		private static extern void GetActiveViewport_Injected(out RectInt ret);

		// Token: 0x060001A0 RID: 416
		[MethodImpl(4096)]
		private static extern void GetUnityProjectionMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x060001A1 RID: 417
		[MethodImpl(4096)]
		private static extern void GetDeviceProjectionMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x04000079 RID: 121
		private static ProfilerMarker s_MarkerRaiseEngineUpdate = new ProfilerMarker("UIR.RaiseEngineUpdate");

		// Token: 0x02000029 RID: 41
		[Flags]
		internal enum RendererCallbacks
		{
			// Token: 0x0400007B RID: 123
			RendererCallback_Init = 1,
			// Token: 0x0400007C RID: 124
			RendererCallback_Exec = 2,
			// Token: 0x0400007D RID: 125
			RendererCallback_Cleanup = 4
		}

		// Token: 0x0200002A RID: 42
		internal enum GPUBufferType
		{
			// Token: 0x0400007F RID: 127
			Vertex,
			// Token: 0x04000080 RID: 128
			Index
		}

		// Token: 0x0200002B RID: 43
		public class GPUBuffer<T> : IDisposable where T : struct
		{
			// Token: 0x060001A2 RID: 418 RVA: 0x000042B6 File Offset: 0x000024B6
			public GPUBuffer(int elementCount, Utility.GPUBufferType type)
			{
				this.elemCount = elementCount;
				this.elemStride = UnsafeUtility.SizeOf<T>();
				this.buffer = Utility.AllocateBuffer(elementCount, this.elemStride, type == Utility.GPUBufferType.Vertex);
			}

			// Token: 0x060001A3 RID: 419 RVA: 0x000042E8 File Offset: 0x000024E8
			public void Dispose()
			{
				Utility.FreeBuffer(this.buffer);
			}

			// Token: 0x060001A4 RID: 420 RVA: 0x000042F7 File Offset: 0x000024F7
			public void UpdateRanges(NativeSlice<GfxUpdateBufferRange> ranges, int rangesMin, int rangesMax)
			{
				Utility.UpdateBufferRanges(this.buffer, new IntPtr(ranges.GetUnsafePtr<GfxUpdateBufferRange>()), ranges.Length, rangesMin, rangesMax);
			}

			// Token: 0x17000058 RID: 88
			// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000431C File Offset: 0x0000251C
			public int ElementStride
			{
				get
				{
					return this.elemStride;
				}
			}

			// Token: 0x17000059 RID: 89
			// (get) Token: 0x060001A6 RID: 422 RVA: 0x00004334 File Offset: 0x00002534
			public int Count
			{
				get
				{
					return this.elemCount;
				}
			}

			// Token: 0x1700005A RID: 90
			// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000434C File Offset: 0x0000254C
			internal IntPtr BufferPointer
			{
				get
				{
					return this.buffer;
				}
			}

			// Token: 0x04000081 RID: 129
			private IntPtr buffer;

			// Token: 0x04000082 RID: 130
			private int elemCount;

			// Token: 0x04000083 RID: 131
			private int elemStride;
		}
	}
}
