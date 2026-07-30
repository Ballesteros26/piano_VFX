using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Buffers
{
	// Token: 0x0200040D RID: 1037
	internal abstract class ArrayPool<T>
	{
		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001FAD RID: 8109 RVA: 0x0007B80B File Offset: 0x00079A0B
		public static ArrayPool<T> Shared
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Volatile.Read<ArrayPool<T>>(ref ArrayPool<T>.s_sharedInstance) ?? ArrayPool<T>.EnsureSharedCreated();
			}
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x0007B820 File Offset: 0x00079A20
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static ArrayPool<T> EnsureSharedCreated()
		{
			Interlocked.CompareExchange<ArrayPool<T>>(ref ArrayPool<T>.s_sharedInstance, ArrayPool<T>.Create(), null);
			return ArrayPool<T>.s_sharedInstance;
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x0007B838 File Offset: 0x00079A38
		public static ArrayPool<T> Create()
		{
			return new DefaultArrayPool<T>();
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x0007B83F File Offset: 0x00079A3F
		public static ArrayPool<T> Create(int maxArrayLength, int maxArraysPerBucket)
		{
			return new DefaultArrayPool<T>(maxArrayLength, maxArraysPerBucket);
		}

		// Token: 0x06001FB1 RID: 8113
		public abstract T[] Rent(int minimumLength);

		// Token: 0x06001FB2 RID: 8114
		public abstract void Return(T[] array, bool clearArray = false);

		// Token: 0x04001B87 RID: 7047
		private static ArrayPool<T> s_sharedInstance;
	}
}
