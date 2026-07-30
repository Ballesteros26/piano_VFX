using System;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine
{
	// Token: 0x02000158 RID: 344
	public static class HashUtilities
	{
		// Token: 0x06000FD8 RID: 4056 RVA: 0x00015808 File Offset: 0x00013A08
		public unsafe static void AppendHash(ref Hash128 inHash, ref Hash128 outHash)
		{
			fixed (Hash128* ptr = &outHash)
			{
				Hash128* ptr2 = ptr;
				fixed (Hash128* ptr3 = &inHash)
				{
					Hash128* ptr4 = ptr3;
					HashUnsafeUtilities.ComputeHash128((void*)ptr4, (ulong)((long)sizeof(Hash128)), ptr2);
				}
			}
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x0001583C File Offset: 0x00013A3C
		public unsafe static void QuantisedMatrixHash(ref Matrix4x4 value, ref Hash128 hash)
		{
			fixed (Hash128* ptr = &hash)
			{
				Hash128* ptr2 = ptr;
				int* ptr3 = stackalloc int[(UIntPtr)64];
				for (int i = 0; i < 16; i++)
				{
					ptr3[i] = (int)(value[i] * 1000f + 0.5f);
				}
				HashUnsafeUtilities.ComputeHash128((void*)ptr3, 64UL, ptr2);
			}
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x00015898 File Offset: 0x00013A98
		public unsafe static void QuantisedVectorHash(ref Vector3 value, ref Hash128 hash)
		{
			fixed (Hash128* ptr = &hash)
			{
				Hash128* ptr2 = ptr;
				int* ptr3 = stackalloc int[(UIntPtr)12];
				for (int i = 0; i < 3; i++)
				{
					ptr3[i] = (int)(value[i] * 1000f + 0.5f);
				}
				HashUnsafeUtilities.ComputeHash128((void*)ptr3, 12UL, ptr2);
			}
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x000158F0 File Offset: 0x00013AF0
		public unsafe static void ComputeHash128<T>(ref T value, ref Hash128 hash) where T : struct
		{
			void* ptr = UnsafeUtility.AddressOf<T>(ref value);
			ulong num = (ulong)((long)UnsafeUtility.SizeOf<T>());
			Hash128* ptr2 = (Hash128*)UnsafeUtility.AddressOf<Hash128>(ref hash);
			HashUnsafeUtilities.ComputeHash128(ptr, num, ptr2);
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x0001591C File Offset: 0x00013B1C
		public unsafe static void ComputeHash128(byte[] value, ref Hash128 hash)
		{
			fixed (byte* ptr = &value[0])
			{
				byte* ptr2 = ptr;
				ulong num = (ulong)((long)value.Length);
				Hash128* ptr3 = (Hash128*)UnsafeUtility.AddressOf<Hash128>(ref hash);
				HashUnsafeUtilities.ComputeHash128((void*)ptr2, num, ptr3);
			}
		}
	}
}
