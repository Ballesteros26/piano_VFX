using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000FA RID: 250
	[NativeHeader("Runtime/Shaders/ComputeShader.h")]
	[NativeHeader("Runtime/GfxDevice/GfxBuffer.h")]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Export/Graphics/GraphicsBuffer.bindings.h")]
	[NativeHeader("Runtime/Shaders/GraphicsBuffer.h")]
	public sealed class GraphicsBuffer : IDisposable
	{
		// Token: 0x06000B09 RID: 2825 RVA: 0x0000ED6C File Offset: 0x0000CF6C
		~GraphicsBuffer()
		{
			this.Dispose(false);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0000EDA0 File Offset: 0x0000CFA0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0000EDB4 File Offset: 0x0000CFB4
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				GraphicsBuffer.DestroyBuffer(this);
			}
			else
			{
				bool flag = this.m_Ptr != IntPtr.Zero;
				if (flag)
				{
					Debug.LogWarning("GarbageCollector disposing of GraphicsBuffer. Please use GraphicsBuffer.Release() or .Dispose() to manually release the buffer.");
				}
			}
			this.m_Ptr = IntPtr.Zero;
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0000EE00 File Offset: 0x0000D000
		private static bool RequiresCompute(GraphicsBuffer.Target target)
		{
			int num = 3;
			return (target & (GraphicsBuffer.Target)num) != target;
		}

		// Token: 0x06000B0D RID: 2829
		[FreeFunction("GraphicsBuffer_Bindings::InitBuffer")]
		[MethodImpl(4096)]
		private static extern IntPtr InitBuffer(GraphicsBuffer.Target target, int count, int stride);

		// Token: 0x06000B0E RID: 2830
		[FreeFunction("GraphicsBuffer_Bindings::DestroyBuffer")]
		[MethodImpl(4096)]
		private static extern void DestroyBuffer(GraphicsBuffer buf);

		// Token: 0x06000B0F RID: 2831 RVA: 0x0000EE20 File Offset: 0x0000D020
		public GraphicsBuffer(GraphicsBuffer.Target target, int count, int stride)
		{
			bool flag = GraphicsBuffer.RequiresCompute(target) && !SystemInfo.supportsComputeShaders;
			if (flag)
			{
				throw new ArgumentException("Attempting to create a graphics buffer that requires compute shader support, but compute shaders are not supported on this platform. Target: " + target);
			}
			bool flag2 = count <= 0;
			if (flag2)
			{
				throw new ArgumentException("Attempting to create a zero length graphics buffer", "count");
			}
			bool flag3 = stride <= 0;
			if (flag3)
			{
				throw new ArgumentException("Attempting to create a graphics buffer with a negative or null stride", "stride");
			}
			bool flag4 = (target & GraphicsBuffer.Target.Index) != (GraphicsBuffer.Target)0 && stride != 2 && stride != 4;
			if (flag4)
			{
				throw new ArgumentException("Attempting to create an index buffer with an invalid stride: " + stride, "stride");
			}
			bool flag5 = GraphicsBuffer.RequiresCompute(target) && stride % 4 != 0;
			if (flag5)
			{
				throw new ArgumentException("Stride must be a multiple of 4 unless the buffer is only used as a vertex buffer and/or index buffer ", "stride");
			}
			this.m_Ptr = GraphicsBuffer.InitBuffer(target, count, stride);
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0000EF04 File Offset: 0x0000D104
		public void Release()
		{
			this.Dispose();
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0000EF10 File Offset: 0x0000D110
		public bool IsValid()
		{
			return this.m_Ptr != IntPtr.Zero;
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000B12 RID: 2834
		public extern int count
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000B13 RID: 2835
		public extern int stride
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0000EF34 File Offset: 0x0000D134
		[SecuritySafeCritical]
		public void SetData(Array data)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Array passed to GraphicsBuffer.SetData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			this.InternalSetData(data, 0, 0, data.Length, UnsafeUtility.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0000EF9C File Offset: 0x0000D19C
		[SecuritySafeCritical]
		public void SetData<T>(List<T> data) where T : struct
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag2)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to GraphicsBuffer.SetData(List<>) must be blittable.\n{1}", typeof(T), UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			this.InternalSetData(NoAllocHelpers.ExtractArrayFromList(data), 0, 0, NoAllocHelpers.SafeLength<T>(data), Marshal.SizeOf(typeof(T)));
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0000F00D File Offset: 0x0000D20D
		[SecuritySafeCritical]
		public void SetData<T>(NativeArray<T> data) where T : struct
		{
			this.InternalSetNativeData((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), 0, 0, data.Length, UnsafeUtility.SizeOf<T>());
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0000F030 File Offset: 0x0000D230
		[SecuritySafeCritical]
		public void SetData(Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Array passed to GraphicsBuffer.SetData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			bool flag3 = managedBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Length;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (managedBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", managedBufferStartIndex, graphicsBufferStartIndex, count));
			}
			this.InternalSetData(data, managedBufferStartIndex, graphicsBufferStartIndex, count, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0000F0D4 File Offset: 0x0000D2D4
		[SecuritySafeCritical]
		public void SetData<T>(List<T> data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag2)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to GraphicsBuffer.SetData(List<>) must be blittable.\n{1}", typeof(T), UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			bool flag3 = managedBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Count;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (managedBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", managedBufferStartIndex, graphicsBufferStartIndex, count));
			}
			this.InternalSetData(NoAllocHelpers.ExtractArrayFromList(data), managedBufferStartIndex, graphicsBufferStartIndex, count, Marshal.SizeOf(typeof(T)));
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0000F184 File Offset: 0x0000D384
		[SecuritySafeCritical]
		public void SetData<T>(NativeArray<T> data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
		{
			bool flag = nativeBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || nativeBufferStartIndex + count > data.Length;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (nativeBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", nativeBufferStartIndex, graphicsBufferStartIndex, count));
			}
			this.InternalSetNativeData((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), nativeBufferStartIndex, graphicsBufferStartIndex, count, UnsafeUtility.SizeOf<T>());
		}

		// Token: 0x06000B1A RID: 2842
		[SecurityCritical]
		[FreeFunction(Name = "GraphicsBuffer_Bindings::InternalSetNativeData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern void InternalSetNativeData(IntPtr data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize);

		// Token: 0x06000B1B RID: 2843
		[FreeFunction(Name = "GraphicsBuffer_Bindings::InternalSetData", HasExplicitThis = true, ThrowsException = true)]
		[SecurityCritical]
		[MethodImpl(4096)]
		private extern void InternalSetData(Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize);

		// Token: 0x06000B1C RID: 2844 RVA: 0x0000F1F4 File Offset: 0x0000D3F4
		[SecurityCritical]
		public void GetData(Array data)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Array passed to GraphicsBuffer.GetData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			this.InternalGetData(data, 0, 0, data.Length, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0000F25C File Offset: 0x0000D45C
		[SecurityCritical]
		public void GetData(Array data, int managedBufferStartIndex, int computeBufferStartIndex, int count)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Array passed to GraphicsBuffer.GetData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			bool flag3 = managedBufferStartIndex < 0 || computeBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Length;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count argument (managedBufferStartIndex:{0} computeBufferStartIndex:{1} count:{2})", managedBufferStartIndex, computeBufferStartIndex, count));
			}
			this.InternalGetData(data, managedBufferStartIndex, computeBufferStartIndex, count, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x06000B1E RID: 2846
		[FreeFunction(Name = "GraphicsBuffer_Bindings::InternalGetData", HasExplicitThis = true, ThrowsException = true)]
		[SecurityCritical]
		[MethodImpl(4096)]
		private extern void InternalGetData(Array data, int managedBufferStartIndex, int computeBufferStartIndex, int count, int elemSize);

		// Token: 0x06000B1F RID: 2847
		[FreeFunction(Name = "GraphicsBuffer_Bindings::InternalGetNativeBufferPtr", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern IntPtr GetNativeBufferPtr();

		// Token: 0x06000B20 RID: 2848
		[FreeFunction(Name = "GraphicsBuffer_Bindings::SetName", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetName(string name);

		// Token: 0x06000B21 RID: 2849
		[MethodImpl(4096)]
		public extern void SetCounterValue(uint counterValue);

		// Token: 0x06000B22 RID: 2850
		[FreeFunction(Name = "GraphicsBuffer_Bindings::CopyCount")]
		[MethodImpl(4096)]
		private static extern void CopyCountCC(ComputeBuffer src, ComputeBuffer dst, int dstOffsetBytes);

		// Token: 0x06000B23 RID: 2851
		[FreeFunction(Name = "GraphicsBuffer_Bindings::CopyCount")]
		[MethodImpl(4096)]
		private static extern void CopyCountGC(GraphicsBuffer src, ComputeBuffer dst, int dstOffsetBytes);

		// Token: 0x06000B24 RID: 2852
		[FreeFunction(Name = "GraphicsBuffer_Bindings::CopyCount")]
		[MethodImpl(4096)]
		private static extern void CopyCountCG(ComputeBuffer src, GraphicsBuffer dst, int dstOffsetBytes);

		// Token: 0x06000B25 RID: 2853
		[FreeFunction(Name = "GraphicsBuffer_Bindings::CopyCount")]
		[MethodImpl(4096)]
		private static extern void CopyCountGG(GraphicsBuffer src, GraphicsBuffer dst, int dstOffsetBytes);

		// Token: 0x06000B26 RID: 2854 RVA: 0x0000F300 File Offset: 0x0000D500
		public static void CopyCount(ComputeBuffer src, ComputeBuffer dst, int dstOffsetBytes)
		{
			GraphicsBuffer.CopyCountCC(src, dst, dstOffsetBytes);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0000F30C File Offset: 0x0000D50C
		public static void CopyCount(GraphicsBuffer src, ComputeBuffer dst, int dstOffsetBytes)
		{
			GraphicsBuffer.CopyCountGC(src, dst, dstOffsetBytes);
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0000F318 File Offset: 0x0000D518
		public static void CopyCount(ComputeBuffer src, GraphicsBuffer dst, int dstOffsetBytes)
		{
			GraphicsBuffer.CopyCountCG(src, dst, dstOffsetBytes);
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0000F324 File Offset: 0x0000D524
		public static void CopyCount(GraphicsBuffer src, GraphicsBuffer dst, int dstOffsetBytes)
		{
			GraphicsBuffer.CopyCountGG(src, dst, dstOffsetBytes);
		}

		// Token: 0x040002A6 RID: 678
		internal IntPtr m_Ptr;

		// Token: 0x020000FB RID: 251
		[Flags]
		public enum Target
		{
			// Token: 0x040002A8 RID: 680
			Vertex = 1,
			// Token: 0x040002A9 RID: 681
			Index = 2,
			// Token: 0x040002AA RID: 682
			Structured = 16,
			// Token: 0x040002AB RID: 683
			Raw = 32,
			// Token: 0x040002AC RID: 684
			Append = 64,
			// Token: 0x040002AD RID: 685
			Counter = 128,
			// Token: 0x040002AE RID: 686
			IndirectArguments = 256,
			// Token: 0x040002AF RID: 687
			Constant = 512
		}
	}
}
