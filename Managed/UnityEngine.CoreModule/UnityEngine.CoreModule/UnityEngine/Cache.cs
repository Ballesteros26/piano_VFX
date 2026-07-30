using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000A8 RID: 168
	[StaticAccessor("CacheWrapper", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Misc/Cache.h")]
	public struct Cache : IEquatable<Cache>
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600029A RID: 666 RVA: 0x000052E4 File Offset: 0x000034E4
		internal int handle
		{
			get
			{
				return this.m_Handle;
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x000052FC File Offset: 0x000034FC
		public static bool operator ==(Cache lhs, Cache rhs)
		{
			return lhs.handle == rhs.handle;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00005320 File Offset: 0x00003520
		public static bool operator !=(Cache lhs, Cache rhs)
		{
			return lhs.handle != rhs.handle;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00005348 File Offset: 0x00003548
		public override int GetHashCode()
		{
			return this.m_Handle;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00005360 File Offset: 0x00003560
		public override bool Equals(object other)
		{
			return other is Cache && this.Equals((Cache)other);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000538C File Offset: 0x0000358C
		public bool Equals(Cache other)
		{
			return this.handle == other.handle;
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x000053B0 File Offset: 0x000035B0
		public bool valid
		{
			get
			{
				return Cache.Cache_IsValid(this.m_Handle);
			}
		}

		// Token: 0x060002A1 RID: 673
		[MethodImpl(4096)]
		internal static extern bool Cache_IsValid(int handle);

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x000053D0 File Offset: 0x000035D0
		public bool ready
		{
			get
			{
				return Cache.Cache_IsReady(this.m_Handle);
			}
		}

		// Token: 0x060002A3 RID: 675
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern bool Cache_IsReady(int handle);

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x000053F0 File Offset: 0x000035F0
		public bool readOnly
		{
			get
			{
				return Cache.Cache_IsReadonly(this.m_Handle);
			}
		}

		// Token: 0x060002A5 RID: 677
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern bool Cache_IsReadonly(int handle);

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00005410 File Offset: 0x00003610
		public string path
		{
			get
			{
				return Cache.Cache_GetPath(this.m_Handle);
			}
		}

		// Token: 0x060002A7 RID: 679
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern string Cache_GetPath(int handle);

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x00005430 File Offset: 0x00003630
		public int index
		{
			get
			{
				return Cache.Cache_GetIndex(this.m_Handle);
			}
		}

		// Token: 0x060002A9 RID: 681
		[MethodImpl(4096)]
		internal static extern int Cache_GetIndex(int handle);

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002AA RID: 682 RVA: 0x00005450 File Offset: 0x00003650
		public long spaceFree
		{
			get
			{
				return Cache.Cache_GetSpaceFree(this.m_Handle);
			}
		}

		// Token: 0x060002AB RID: 683
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern long Cache_GetSpaceFree(int handle);

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002AC RID: 684 RVA: 0x00005470 File Offset: 0x00003670
		// (set) Token: 0x060002AD RID: 685 RVA: 0x0000548D File Offset: 0x0000368D
		public long maximumAvailableStorageSpace
		{
			get
			{
				return Cache.Cache_GetMaximumDiskSpaceAvailable(this.m_Handle);
			}
			set
			{
				Cache.Cache_SetMaximumDiskSpaceAvailable(this.m_Handle, value);
			}
		}

		// Token: 0x060002AE RID: 686
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern long Cache_GetMaximumDiskSpaceAvailable(int handle);

		// Token: 0x060002AF RID: 687
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern void Cache_SetMaximumDiskSpaceAvailable(int handle, long value);

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x000054A0 File Offset: 0x000036A0
		public long spaceOccupied
		{
			get
			{
				return Cache.Cache_GetCachingDiskSpaceUsed(this.m_Handle);
			}
		}

		// Token: 0x060002B1 RID: 689
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern long Cache_GetCachingDiskSpaceUsed(int handle);

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x000054C0 File Offset: 0x000036C0
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x000054DD File Offset: 0x000036DD
		public int expirationDelay
		{
			get
			{
				return Cache.Cache_GetExpirationDelay(this.m_Handle);
			}
			set
			{
				Cache.Cache_SetExpirationDelay(this.m_Handle, value);
			}
		}

		// Token: 0x060002B4 RID: 692
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern int Cache_GetExpirationDelay(int handle);

		// Token: 0x060002B5 RID: 693
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern void Cache_SetExpirationDelay(int handle, int value);

		// Token: 0x060002B6 RID: 694 RVA: 0x000054F0 File Offset: 0x000036F0
		public bool ClearCache()
		{
			return Cache.Cache_ClearCache(this.m_Handle);
		}

		// Token: 0x060002B7 RID: 695
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern bool Cache_ClearCache(int handle);

		// Token: 0x060002B8 RID: 696 RVA: 0x00005510 File Offset: 0x00003710
		public bool ClearCache(int expiration)
		{
			return Cache.Cache_ClearCache_Expiration(this.m_Handle, expiration);
		}

		// Token: 0x060002B9 RID: 697
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern bool Cache_ClearCache_Expiration(int handle, int expiration);

		// Token: 0x040001F4 RID: 500
		private int m_Handle;
	}
}
