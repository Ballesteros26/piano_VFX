using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Internal;

namespace Unity.Collections
{
	// Token: 0x02000063 RID: 99
	[DebuggerDisplay("Length = {Length}")]
	[NativeContainerSupportsMinMaxWriteRestriction]
	[NativeContainer]
	[DebuggerTypeProxy(typeof(NativeSliceDebugView<>))]
	public struct NativeSlice<T> : IEnumerable<T>, IEnumerable, IEquatable<NativeSlice<T>> where T : struct
	{
		// Token: 0x0600011C RID: 284 RVA: 0x00003552 File Offset: 0x00001752
		public NativeSlice(NativeSlice<T> slice, int start)
		{
			this = new NativeSlice<T>(slice, start, slice.Length - start);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00003567 File Offset: 0x00001767
		public NativeSlice(NativeSlice<T> slice, int start, int length)
		{
			this.m_Stride = slice.m_Stride;
			this.m_Buffer = slice.m_Buffer + this.m_Stride * start;
			this.m_Length = length;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00003592 File Offset: 0x00001792
		public NativeSlice(NativeArray<T> array)
		{
			this = new NativeSlice<T>(array, 0, array.Length);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000035A5 File Offset: 0x000017A5
		public NativeSlice(NativeArray<T> array, int start)
		{
			this = new NativeSlice<T>(array, start, array.Length - start);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000035BC File Offset: 0x000017BC
		public static implicit operator NativeSlice<T>(NativeArray<T> array)
		{
			return new NativeSlice<T>(array);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000035D4 File Offset: 0x000017D4
		public unsafe NativeSlice(NativeArray<T> array, int start, int length)
		{
			this.m_Stride = UnsafeUtility.SizeOf<T>();
			byte* ptr = (byte*)array.m_Buffer + this.m_Stride * start;
			this.m_Buffer = ptr;
			this.m_Length = length;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000360C File Offset: 0x0000180C
		public NativeSlice<U> SliceConvert<U>() where U : struct
		{
			int num = UnsafeUtility.SizeOf<U>();
			NativeSlice<U> nativeSlice;
			nativeSlice.m_Buffer = this.m_Buffer;
			nativeSlice.m_Stride = num;
			nativeSlice.m_Length = this.m_Length * this.m_Stride / num;
			return nativeSlice;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00003650 File Offset: 0x00001850
		public NativeSlice<U> SliceWithStride<U>(int offset) where U : struct
		{
			NativeSlice<U> nativeSlice;
			nativeSlice.m_Buffer = this.m_Buffer + offset;
			nativeSlice.m_Stride = this.m_Stride;
			nativeSlice.m_Length = this.m_Length;
			return nativeSlice;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000368C File Offset: 0x0000188C
		public NativeSlice<U> SliceWithStride<U>() where U : struct
		{
			return this.SliceWithStride<U>(0);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00002EC3 File Offset: 0x000010C3
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckReadIndex(int index)
		{
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00002EC3 File Offset: 0x000010C3
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckWriteIndex(int index)
		{
		}

		// Token: 0x1700001C RID: 28
		public unsafe T this[int index]
		{
			get
			{
				return UnsafeUtility.ReadArrayElementWithStride<T>((void*)this.m_Buffer, index, this.m_Stride);
			}
			[WriteAccessRequired]
			set
			{
				UnsafeUtility.WriteArrayElementWithStride<T>((void*)this.m_Buffer, index, this.m_Stride, value);
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000036E3 File Offset: 0x000018E3
		[WriteAccessRequired]
		public void CopyFrom(NativeSlice<T> slice)
		{
			UnsafeUtility.MemCpyStride(this.GetUnsafePtr<T>(), this.Stride, slice.GetUnsafeReadOnlyPtr<T>(), slice.Stride, UnsafeUtility.SizeOf<T>(), this.m_Length);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00003718 File Offset: 0x00001918
		[WriteAccessRequired]
		public unsafe void CopyFrom(T[] array)
		{
			GCHandle gchandle = GCHandle.Alloc(array, 3);
			IntPtr intPtr = gchandle.AddrOfPinnedObject();
			int num = UnsafeUtility.SizeOf<T>();
			UnsafeUtility.MemCpyStride(this.GetUnsafePtr<T>(), this.Stride, (void*)intPtr, num, num, this.m_Length);
			gchandle.Free();
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000376C File Offset: 0x0000196C
		public void CopyTo(NativeArray<T> array)
		{
			int num = UnsafeUtility.SizeOf<T>();
			UnsafeUtility.MemCpyStride(array.GetUnsafePtr<T>(), num, this.GetUnsafeReadOnlyPtr<T>(), this.Stride, num, this.m_Length);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000037A8 File Offset: 0x000019A8
		public unsafe void CopyTo(T[] array)
		{
			GCHandle gchandle = GCHandle.Alloc(array, 3);
			IntPtr intPtr = gchandle.AddrOfPinnedObject();
			int num = UnsafeUtility.SizeOf<T>();
			UnsafeUtility.MemCpyStride((void*)intPtr, num, this.GetUnsafeReadOnlyPtr<T>(), this.Stride, num, this.m_Length);
			gchandle.Free();
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000037FC File Offset: 0x000019FC
		public T[] ToArray()
		{
			T[] array = new T[this.Length];
			this.CopyTo(array);
			return array;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00003823 File Offset: 0x00001A23
		public int Stride
		{
			get
			{
				return this.m_Stride;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600012F RID: 303 RVA: 0x0000382B File Offset: 0x00001A2B
		public int Length
		{
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00003834 File Offset: 0x00001A34
		public NativeSlice<T>.Enumerator GetEnumerator()
		{
			return new NativeSlice<T>.Enumerator(ref this);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000384C File Offset: 0x00001A4C
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new NativeSlice<T>.Enumerator(ref this);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000386C File Offset: 0x00001A6C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000388C File Offset: 0x00001A8C
		public bool Equals(NativeSlice<T> other)
		{
			return this.m_Buffer == other.m_Buffer && this.m_Stride == other.m_Stride && this.m_Length == other.m_Length;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000038CC File Offset: 0x00001ACC
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is NativeSlice<T> && this.Equals((NativeSlice<T>)obj);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00003904 File Offset: 0x00001B04
		public override int GetHashCode()
		{
			int num = this.m_Buffer;
			num = (num * 397) ^ this.m_Stride;
			return (num * 397) ^ this.m_Length;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00003940 File Offset: 0x00001B40
		public static bool operator ==(NativeSlice<T> left, NativeSlice<T> right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000395C File Offset: 0x00001B5C
		public static bool operator !=(NativeSlice<T> left, NativeSlice<T> right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0400011F RID: 287
		[NativeDisableUnsafePtrRestriction]
		internal unsafe byte* m_Buffer;

		// Token: 0x04000120 RID: 288
		internal int m_Stride;

		// Token: 0x04000121 RID: 289
		internal int m_Length;

		// Token: 0x02000064 RID: 100
		[ExcludeFromDocs]
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06000138 RID: 312 RVA: 0x00003979 File Offset: 0x00001B79
			public Enumerator(ref NativeSlice<T> array)
			{
				this.m_Array = array;
				this.m_Index = -1;
			}

			// Token: 0x06000139 RID: 313 RVA: 0x00002EC3 File Offset: 0x000010C3
			public void Dispose()
			{
			}

			// Token: 0x0600013A RID: 314 RVA: 0x00003990 File Offset: 0x00001B90
			public bool MoveNext()
			{
				this.m_Index++;
				return this.m_Index < this.m_Array.Length;
			}

			// Token: 0x0600013B RID: 315 RVA: 0x000039C3 File Offset: 0x00001BC3
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x0600013C RID: 316 RVA: 0x000039CD File Offset: 0x00001BCD
			public T Current
			{
				get
				{
					return this.m_Array[this.m_Index];
				}
			}

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x0600013D RID: 317 RVA: 0x000039E0 File Offset: 0x00001BE0
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000122 RID: 290
			private NativeSlice<T> m_Array;

			// Token: 0x04000123 RID: 291
			private int m_Index;
		}
	}
}
