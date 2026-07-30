using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine.Internal;

namespace Unity.Collections
{
	// Token: 0x0200005C RID: 92
	[NativeContainer]
	[DebuggerTypeProxy(typeof(NativeArrayDebugView<>))]
	[NativeContainerSupportsMinMaxWriteRestriction]
	[NativeContainerSupportsDeallocateOnJobCompletion]
	[NativeContainerSupportsDeferredConvertListToArray]
	[DebuggerDisplay("Length = {Length}")]
	public struct NativeArray<T> : IDisposable, IEnumerable<T>, IEnumerable, IEquatable<NativeArray<T>> where T : struct
	{
		// Token: 0x060000DD RID: 221 RVA: 0x00002DC4 File Offset: 0x00000FC4
		public NativeArray(int length, Allocator allocator, NativeArrayOptions options = NativeArrayOptions.ClearMemory)
		{
			NativeArray<T>.Allocate(length, allocator, out this);
			bool flag = (options & NativeArrayOptions.ClearMemory) == NativeArrayOptions.ClearMemory;
			if (flag)
			{
				UnsafeUtility.MemClear(this.m_Buffer, (long)this.Length * (long)UnsafeUtility.SizeOf<T>());
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00002DFF File Offset: 0x00000FFF
		public NativeArray(T[] array, Allocator allocator)
		{
			NativeArray<T>.Allocate(array.Length, allocator, out this);
			NativeArray<T>.Copy(array, this);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00002E1A File Offset: 0x0000101A
		public NativeArray(NativeArray<T> array, Allocator allocator)
		{
			NativeArray<T>.Allocate(array.Length, allocator, out this);
			NativeArray<T>.Copy(array, this);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00002E3C File Offset: 0x0000103C
		private static void Allocate(int length, Allocator allocator, out NativeArray<T> array)
		{
			long num = (long)UnsafeUtility.SizeOf<T>() * (long)length;
			array = default(NativeArray<T>);
			array.m_Buffer = UnsafeUtility.Malloc(num, UnsafeUtility.AlignOf<T>(), allocator);
			array.m_Length = length;
			array.m_AllocatorLabel = allocator;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00002E7B File Offset: 0x0000107B
		public int Length
		{
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00002E84 File Offset: 0x00001084
		[BurstDiscard]
		internal static void IsUnmanagedAndThrow()
		{
			bool flag = !UnsafeUtility.IsValidNativeContainerElementType<T>();
			if (flag)
			{
				throw new InvalidOperationException(string.Format("{0} used in NativeArray<{1}> must be unmanaged (contain no managed types) and cannot itself be a native container type.", typeof(T), typeof(T)));
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00002EC3 File Offset: 0x000010C3
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckElementReadAccess(int index)
		{
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00002EC3 File Offset: 0x000010C3
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckElementWriteAccess(int index)
		{
		}

		// Token: 0x17000016 RID: 22
		public T this[int index]
		{
			get
			{
				return UnsafeUtility.ReadArrayElement<T>(this.m_Buffer, index);
			}
			[WriteAccessRequired]
			set
			{
				UnsafeUtility.WriteArrayElement<T>(this.m_Buffer, index, value);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00002EF7 File Offset: 0x000010F7
		public bool IsCreated
		{
			get
			{
				return this.m_Buffer != null;
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00002F06 File Offset: 0x00001106
		[WriteAccessRequired]
		public void Dispose()
		{
			UnsafeUtility.Free(this.m_Buffer, this.m_AllocatorLabel);
			this.m_Buffer = null;
			this.m_Length = 0;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00002F2C File Offset: 0x0000112C
		public JobHandle Dispose(JobHandle inputDeps)
		{
			JobHandle jobHandle = new NativeArrayDisposeJob
			{
				Data = new NativeArrayDispose
				{
					m_Buffer = this.m_Buffer,
					m_AllocatorLabel = this.m_AllocatorLabel
				}
			}.Schedule(inputDeps);
			this.m_Buffer = null;
			this.m_Length = 0;
			return jobHandle;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00002F88 File Offset: 0x00001188
		[WriteAccessRequired]
		public void CopyFrom(T[] array)
		{
			NativeArray<T>.Copy(array, this);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00002F98 File Offset: 0x00001198
		[WriteAccessRequired]
		public void CopyFrom(NativeArray<T> array)
		{
			NativeArray<T>.Copy(array, this);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00002FA8 File Offset: 0x000011A8
		public void CopyTo(T[] array)
		{
			NativeArray<T>.Copy(this, array);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00002FB8 File Offset: 0x000011B8
		public void CopyTo(NativeArray<T> array)
		{
			NativeArray<T>.Copy(this, array);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00002FC8 File Offset: 0x000011C8
		public T[] ToArray()
		{
			T[] array = new T[this.Length];
			NativeArray<T>.Copy(this, array, this.Length);
			return array;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00002FFC File Offset: 0x000011FC
		public NativeArray<T>.Enumerator GetEnumerator()
		{
			return new NativeArray<T>.Enumerator(ref this);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00003014 File Offset: 0x00001214
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new NativeArray<T>.Enumerator(ref this);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00003034 File Offset: 0x00001234
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00003054 File Offset: 0x00001254
		public bool Equals(NativeArray<T> other)
		{
			return this.m_Buffer == other.m_Buffer && this.m_Length == other.m_Length;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00003088 File Offset: 0x00001288
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is NativeArray<T> && this.Equals((NativeArray<T>)obj);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000030C0 File Offset: 0x000012C0
		public override int GetHashCode()
		{
			return (this.m_Buffer * 397) ^ this.m_Length;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000030E8 File Offset: 0x000012E8
		public static bool operator ==(NativeArray<T> left, NativeArray<T> right)
		{
			return left.Equals(right);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00003104 File Offset: 0x00001304
		public static bool operator !=(NativeArray<T> left, NativeArray<T> right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00003121 File Offset: 0x00001321
		public static void Copy(NativeArray<T> src, NativeArray<T> dst)
		{
			NativeArray<T>.Copy(src, 0, dst, 0, src.Length);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00003135 File Offset: 0x00001335
		public static void Copy(T[] src, NativeArray<T> dst)
		{
			NativeArray<T>.Copy(src, 0, dst, 0, src.Length);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00003145 File Offset: 0x00001345
		public static void Copy(NativeArray<T> src, T[] dst)
		{
			NativeArray<T>.Copy(src, 0, dst, 0, src.Length);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00003159 File Offset: 0x00001359
		public static void Copy(NativeArray<T> src, NativeArray<T> dst, int length)
		{
			NativeArray<T>.Copy(src, 0, dst, 0, length);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00003167 File Offset: 0x00001367
		public static void Copy(T[] src, NativeArray<T> dst, int length)
		{
			NativeArray<T>.Copy(src, 0, dst, 0, length);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00003175 File Offset: 0x00001375
		public static void Copy(NativeArray<T> src, T[] dst, int length)
		{
			NativeArray<T>.Copy(src, 0, dst, 0, length);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00003183 File Offset: 0x00001383
		public unsafe static void Copy(NativeArray<T> src, int srcIndex, NativeArray<T> dst, int dstIndex, int length)
		{
			UnsafeUtility.MemCpy((void*)((byte*)dst.m_Buffer + dstIndex * UnsafeUtility.SizeOf<T>()), (void*)((byte*)src.m_Buffer + srcIndex * UnsafeUtility.SizeOf<T>()), (long)(length * UnsafeUtility.SizeOf<T>()));
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000031B4 File Offset: 0x000013B4
		public unsafe static void Copy(T[] src, int srcIndex, NativeArray<T> dst, int dstIndex, int length)
		{
			GCHandle gchandle = GCHandle.Alloc(src, 3);
			IntPtr intPtr = gchandle.AddrOfPinnedObject();
			UnsafeUtility.MemCpy((void*)((byte*)dst.m_Buffer + dstIndex * UnsafeUtility.SizeOf<T>()), (void*)((byte*)(void*)intPtr + srcIndex * UnsafeUtility.SizeOf<T>()), (long)(length * UnsafeUtility.SizeOf<T>()));
			gchandle.Free();
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00003208 File Offset: 0x00001408
		public unsafe static void Copy(NativeArray<T> src, int srcIndex, T[] dst, int dstIndex, int length)
		{
			GCHandle gchandle = GCHandle.Alloc(dst, 3);
			IntPtr intPtr = gchandle.AddrOfPinnedObject();
			UnsafeUtility.MemCpy((void*)((byte*)(void*)intPtr + dstIndex * UnsafeUtility.SizeOf<T>()), (void*)((byte*)src.m_Buffer + srcIndex * UnsafeUtility.SizeOf<T>()), (long)(length * UnsafeUtility.SizeOf<T>()));
			gchandle.Free();
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00002EC3 File Offset: 0x000010C3
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckReinterpretLoadRange<U>(int sourceIndex) where U : struct
		{
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00002EC3 File Offset: 0x000010C3
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckReinterpretStoreRange<U>(int destIndex) where U : struct
		{
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000325C File Offset: 0x0000145C
		public unsafe U ReinterpretLoad<U>(int sourceIndex) where U : struct
		{
			byte* ptr = (byte*)this.m_Buffer + (long)UnsafeUtility.SizeOf<T>() * (long)sourceIndex;
			return UnsafeUtility.ReadArrayElement<U>((void*)ptr, 0);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00003288 File Offset: 0x00001488
		public unsafe void ReinterpretStore<U>(int destIndex, U data) where U : struct
		{
			byte* ptr = (byte*)this.m_Buffer + (long)UnsafeUtility.SizeOf<T>() * (long)destIndex;
			UnsafeUtility.WriteArrayElement<U>((void*)ptr, 0, data);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000032B4 File Offset: 0x000014B4
		private NativeArray<U> InternalReinterpret<U>(int length) where U : struct
		{
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<U>(this.m_Buffer, length, this.m_AllocatorLabel);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000032DC File Offset: 0x000014DC
		public NativeArray<U> Reinterpret<U>() where U : struct
		{
			return this.InternalReinterpret<U>(this.Length);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000032FC File Offset: 0x000014FC
		public NativeArray<U> Reinterpret<U>(int expectedTypeSize) where U : struct
		{
			long num = (long)UnsafeUtility.SizeOf<T>();
			long num2 = (long)UnsafeUtility.SizeOf<U>();
			long num3 = (long)this.Length * num;
			long num4 = num3 / num2;
			return this.InternalReinterpret<U>((int)num4);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00003334 File Offset: 0x00001534
		public unsafe NativeArray<T> GetSubArray(int start, int length)
		{
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)((byte*)this.m_Buffer + (long)UnsafeUtility.SizeOf<T>() * (long)start), length, Allocator.Invalid);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00003360 File Offset: 0x00001560
		public NativeArray<T>.ReadOnly AsReadOnly()
		{
			return new NativeArray<T>.ReadOnly(this.m_Buffer, this.m_Length);
		}

		// Token: 0x04000114 RID: 276
		[NativeDisableUnsafePtrRestriction]
		internal unsafe void* m_Buffer;

		// Token: 0x04000115 RID: 277
		internal int m_Length;

		// Token: 0x04000116 RID: 278
		internal Allocator m_AllocatorLabel;

		// Token: 0x0200005D RID: 93
		[ExcludeFromDocs]
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06000109 RID: 265 RVA: 0x00003383 File Offset: 0x00001583
			public Enumerator(ref NativeArray<T> array)
			{
				this.m_Array = array;
				this.m_Index = -1;
			}

			// Token: 0x0600010A RID: 266 RVA: 0x00002EC3 File Offset: 0x000010C3
			public void Dispose()
			{
			}

			// Token: 0x0600010B RID: 267 RVA: 0x0000339C File Offset: 0x0000159C
			public bool MoveNext()
			{
				this.m_Index++;
				return this.m_Index < this.m_Array.Length;
			}

			// Token: 0x0600010C RID: 268 RVA: 0x000033CF File Offset: 0x000015CF
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x0600010D RID: 269 RVA: 0x000033D9 File Offset: 0x000015D9
			public T Current
			{
				get
				{
					return this.m_Array[this.m_Index];
				}
			}

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x0600010E RID: 270 RVA: 0x000033EC File Offset: 0x000015EC
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000117 RID: 279
			private NativeArray<T> m_Array;

			// Token: 0x04000118 RID: 280
			private int m_Index;
		}

		// Token: 0x0200005E RID: 94
		[NativeContainer]
		[NativeContainerIsReadOnly]
		public struct ReadOnly
		{
			// Token: 0x0600010F RID: 271 RVA: 0x000033F9 File Offset: 0x000015F9
			internal unsafe ReadOnly(void* buffer, int length)
			{
				this.m_Buffer = buffer;
				this.m_Length = length;
			}

			// Token: 0x1700001A RID: 26
			public T this[int index]
			{
				get
				{
					return UnsafeUtility.ReadArrayElement<T>(this.m_Buffer, index);
				}
			}

			// Token: 0x06000111 RID: 273 RVA: 0x0000342C File Offset: 0x0000162C
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private void CheckElementReadAccess(int index)
			{
				bool flag = index < 0 && index >= this.m_Length;
				if (flag)
				{
					throw new IndexOutOfRangeException(string.Format("Index {0} is out of range (must be between 0 and {1}).", index, this.m_Length - 1));
				}
			}

			// Token: 0x04000119 RID: 281
			[NativeDisableUnsafePtrRestriction]
			internal unsafe void* m_Buffer;

			// Token: 0x0400011A RID: 282
			internal int m_Length;
		}
	}
}
