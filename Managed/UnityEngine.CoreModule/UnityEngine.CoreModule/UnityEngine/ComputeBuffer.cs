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
	// Token: 0x020001D5 RID: 469
	[NativeHeader("Runtime/Export/Shaders/ComputeShader.bindings.h")]
	[NativeHeader("Runtime/Shaders/ComputeShader.h")]
	[UsedByNativeCode]
	public sealed class ComputeBuffer : IDisposable
	{
		// Token: 0x0600147B RID: 5243 RVA: 0x00021A10 File Offset: 0x0001FC10
		~ComputeBuffer()
		{
			this.Dispose(false);
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x00021A44 File Offset: 0x0001FC44
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x00021A58 File Offset: 0x0001FC58
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				ComputeBuffer.DestroyBuffer(this);
			}
			else
			{
				bool flag = this.m_Ptr != IntPtr.Zero;
				if (flag)
				{
					Debug.LogWarning("GarbageCollector disposing of ComputeBuffer. Please use ComputeBuffer.Release() or .Dispose() to manually release the buffer.");
				}
			}
			this.m_Ptr = IntPtr.Zero;
		}

		// Token: 0x0600147E RID: 5246
		[FreeFunction("ComputeShader_Bindings::InitBuffer")]
		[MethodImpl(4096)]
		private static extern IntPtr InitBuffer(int count, int stride, ComputeBufferType type, ComputeBufferMode usage);

		// Token: 0x0600147F RID: 5247
		[FreeFunction("ComputeShader_Bindings::DestroyBuffer")]
		[MethodImpl(4096)]
		private static extern void DestroyBuffer(ComputeBuffer buf);

		// Token: 0x06001480 RID: 5248 RVA: 0x00021AA2 File Offset: 0x0001FCA2
		public ComputeBuffer(int count, int stride)
			: this(count, stride, ComputeBufferType.Default, ComputeBufferMode.Immutable, 3)
		{
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x00021AB1 File Offset: 0x0001FCB1
		public ComputeBuffer(int count, int stride, ComputeBufferType type)
			: this(count, stride, type, ComputeBufferMode.Immutable, 3)
		{
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x00021AC0 File Offset: 0x0001FCC0
		public ComputeBuffer(int count, int stride, ComputeBufferType type, ComputeBufferMode usage)
			: this(count, stride, type, usage, 3)
		{
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x00021AD0 File Offset: 0x0001FCD0
		internal ComputeBuffer(int count, int stride, ComputeBufferType type, ComputeBufferMode usage, int stackDepth)
		{
			bool flag = count <= 0;
			if (flag)
			{
				throw new ArgumentException("Attempting to create a zero length compute buffer", "count");
			}
			bool flag2 = stride <= 0;
			if (flag2)
			{
				throw new ArgumentException("Attempting to create a compute buffer with a negative or null stride", "stride");
			}
			this.m_Ptr = ComputeBuffer.InitBuffer(count, stride, type, usage);
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x00021B2D File Offset: 0x0001FD2D
		public void Release()
		{
			this.Dispose();
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x00021B38 File Offset: 0x0001FD38
		public bool IsValid()
		{
			return this.m_Ptr != IntPtr.Zero;
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06001486 RID: 5254
		public extern int count
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001487 RID: 5255
		public extern int stride
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001488 RID: 5256
		private extern ComputeBufferMode usage
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x00021B5C File Offset: 0x0001FD5C
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
				throw new ArgumentException(string.Format("Array passed to ComputeBuffer.SetData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			this.InternalSetData(data, 0, 0, data.Length, UnsafeUtility.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x00021BC4 File Offset: 0x0001FDC4
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
				throw new ArgumentException(string.Format("List<{0}> passed to ComputeBuffer.SetData(List<>) must be blittable.\n{1}", typeof(T), UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			this.InternalSetData(NoAllocHelpers.ExtractArrayFromList(data), 0, 0, NoAllocHelpers.SafeLength<T>(data), Marshal.SizeOf(typeof(T)));
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x00021C35 File Offset: 0x0001FE35
		[SecuritySafeCritical]
		public void SetData<T>(NativeArray<T> data) where T : struct
		{
			this.InternalSetNativeData((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), 0, 0, data.Length, UnsafeUtility.SizeOf<T>());
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x00021C58 File Offset: 0x0001FE58
		[SecuritySafeCritical]
		public void SetData(Array data, int managedBufferStartIndex, int computeBufferStartIndex, int count)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Array passed to ComputeBuffer.SetData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			bool flag3 = managedBufferStartIndex < 0 || computeBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Length;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (managedBufferStartIndex:{0} computeBufferStartIndex:{1} count:{2})", managedBufferStartIndex, computeBufferStartIndex, count));
			}
			this.InternalSetData(data, managedBufferStartIndex, computeBufferStartIndex, count, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x00021CFC File Offset: 0x0001FEFC
		[SecuritySafeCritical]
		public void SetData<T>(List<T> data, int managedBufferStartIndex, int computeBufferStartIndex, int count) where T : struct
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag2)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to ComputeBuffer.SetData(List<>) must be blittable.\n{1}", typeof(T), UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			bool flag3 = managedBufferStartIndex < 0 || computeBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Count;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (managedBufferStartIndex:{0} computeBufferStartIndex:{1} count:{2})", managedBufferStartIndex, computeBufferStartIndex, count));
			}
			this.InternalSetData(NoAllocHelpers.ExtractArrayFromList(data), managedBufferStartIndex, computeBufferStartIndex, count, Marshal.SizeOf(typeof(T)));
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x00021DAC File Offset: 0x0001FFAC
		[SecuritySafeCritical]
		public void SetData<T>(NativeArray<T> data, int nativeBufferStartIndex, int computeBufferStartIndex, int count) where T : struct
		{
			bool flag = nativeBufferStartIndex < 0 || computeBufferStartIndex < 0 || count < 0 || nativeBufferStartIndex + count > data.Length;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (nativeBufferStartIndex:{0} computeBufferStartIndex:{1} count:{2})", nativeBufferStartIndex, computeBufferStartIndex, count));
			}
			this.InternalSetNativeData((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), nativeBufferStartIndex, computeBufferStartIndex, count, UnsafeUtility.SizeOf<T>());
		}

		// Token: 0x0600148F RID: 5263
		[SecurityCritical]
		[FreeFunction(Name = "ComputeShader_Bindings::InternalSetNativeData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern void InternalSetNativeData(IntPtr data, int nativeBufferStartIndex, int computeBufferStartIndex, int count, int elemSize);

		// Token: 0x06001490 RID: 5264
		[SecurityCritical]
		[FreeFunction(Name = "ComputeShader_Bindings::InternalSetData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern void InternalSetData(Array data, int managedBufferStartIndex, int computeBufferStartIndex, int count, int elemSize);

		// Token: 0x06001491 RID: 5265 RVA: 0x00021E1C File Offset: 0x0002001C
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
				throw new ArgumentException(string.Format("Array passed to ComputeBuffer.GetData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			this.InternalGetData(data, 0, 0, data.Length, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x00021E84 File Offset: 0x00020084
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
				throw new ArgumentException(string.Format("Array passed to ComputeBuffer.GetData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			bool flag3 = managedBufferStartIndex < 0 || computeBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Length;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count argument (managedBufferStartIndex:{0} computeBufferStartIndex:{1} count:{2})", managedBufferStartIndex, computeBufferStartIndex, count));
			}
			this.InternalGetData(data, managedBufferStartIndex, computeBufferStartIndex, count, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x06001493 RID: 5267
		[SecurityCritical]
		[FreeFunction(Name = "ComputeShader_Bindings::InternalGetData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern void InternalGetData(Array data, int managedBufferStartIndex, int computeBufferStartIndex, int count, int elemSize);

		// Token: 0x06001494 RID: 5268
		[MethodImpl(4096)]
		private unsafe extern void* BeginBufferWrite(int offset = 0, int size = 0);

		// Token: 0x06001495 RID: 5269 RVA: 0x00021F28 File Offset: 0x00020128
		public unsafe NativeArray<T> BeginWrite<T>(int computeBufferStartIndex, int count) where T : struct
		{
			bool flag = this.usage != ComputeBufferMode.SubUpdates;
			if (flag)
			{
				throw new ArgumentException("ComputeBuffer must be created with usage mode ComputeBufferMode.SubUpdates to be able to be mapped with BeginWrite");
			}
			int num = UnsafeUtility.SizeOf<T>();
			bool flag2 = computeBufferStartIndex < 0 || count < 0 || (computeBufferStartIndex + count) * num > this.count * this.stride;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (computeBufferStartIndex:{0} count:{1} elementSize:{2}, this.count:{3}, this.stride{4})", new object[] { computeBufferStartIndex, count, num, this.count, this.stride }));
			}
			void* ptr = this.BeginBufferWrite(computeBufferStartIndex * num, count * num);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(ptr, count, Allocator.Invalid);
		}

		// Token: 0x06001496 RID: 5270
		[MethodImpl(4096)]
		private extern void EndBufferWrite(int bytesWritten = 0);

		// Token: 0x06001497 RID: 5271 RVA: 0x00021FE8 File Offset: 0x000201E8
		public void EndWrite<T>(int countWritten) where T : struct
		{
			bool flag = countWritten < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (countWritten:{0})", countWritten));
			}
			int num = UnsafeUtility.SizeOf<T>();
			this.EndBufferWrite(countWritten * num);
		}

		// Token: 0x170003E2 RID: 994
		// (set) Token: 0x06001498 RID: 5272 RVA: 0x00022024 File Offset: 0x00020224
		public string name
		{
			set
			{
				this.SetName(value);
			}
		}

		// Token: 0x06001499 RID: 5273
		[FreeFunction(Name = "ComputeShader_Bindings::SetName", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetName(string name);

		// Token: 0x0600149A RID: 5274
		[MethodImpl(4096)]
		public extern void SetCounterValue(uint counterValue);

		// Token: 0x0600149B RID: 5275
		[MethodImpl(4096)]
		public static extern void CopyCount(ComputeBuffer src, ComputeBuffer dst, int dstOffsetBytes);

		// Token: 0x0600149C RID: 5276
		[MethodImpl(4096)]
		public extern IntPtr GetNativeBufferPtr();

		// Token: 0x04000687 RID: 1671
		internal IntPtr m_Ptr;
	}
}
