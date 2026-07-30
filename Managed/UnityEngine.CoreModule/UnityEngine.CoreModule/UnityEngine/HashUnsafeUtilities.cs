using System;

namespace UnityEngine
{
	// Token: 0x02000159 RID: 345
	public static class HashUnsafeUtilities
	{
		// Token: 0x06000FDD RID: 4061 RVA: 0x0001594F File Offset: 0x00013B4F
		public unsafe static void ComputeHash128(void* data, ulong dataSize, ulong* hash1, ulong* hash2)
		{
			SpookyHash.Hash(data, dataSize, hash1, hash2);
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x0001595C File Offset: 0x00013B5C
		public unsafe static void ComputeHash128(void* data, ulong dataSize, Hash128* hash)
		{
			ulong u64_ = hash->u64_0;
			ulong u64_2 = hash->u64_1;
			HashUnsafeUtilities.ComputeHash128(data, dataSize, &u64_, &u64_2);
			*hash = new Hash128(u64_, u64_2);
		}
	}
}
