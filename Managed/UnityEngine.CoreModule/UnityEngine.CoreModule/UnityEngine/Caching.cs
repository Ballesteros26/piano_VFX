using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000AA RID: 170
	[NativeHeader("Runtime/Misc/CachingManager.h")]
	[StaticAccessor("GetCachingManager()", StaticAccessorType.Dot)]
	public sealed class Caching
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002BA RID: 698
		// (set) Token: 0x060002BB RID: 699
		public static extern bool compressionEnabled
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002BC RID: 700
		public static extern bool ready
		{
			[NativeName("GetIsReady")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060002BD RID: 701
		[MethodImpl(4096)]
		public static extern bool ClearCache();

		// Token: 0x060002BE RID: 702 RVA: 0x00005530 File Offset: 0x00003730
		public static bool ClearCache(int expiration)
		{
			return Caching.ClearCache_Int(expiration);
		}

		// Token: 0x060002BF RID: 703
		[NativeName("ClearCache")]
		[MethodImpl(4096)]
		internal static extern bool ClearCache_Int(int expiration);

		// Token: 0x060002C0 RID: 704 RVA: 0x00005548 File Offset: 0x00003748
		public static bool ClearCachedVersion(string assetBundleName, Hash128 hash)
		{
			bool flag = string.IsNullOrEmpty(assetBundleName);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle name cannot be null or empty.");
			}
			return Caching.ClearCachedVersionInternal(assetBundleName, hash);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00005576 File Offset: 0x00003776
		[NativeName("ClearCachedVersion")]
		internal static bool ClearCachedVersionInternal(string assetBundleName, Hash128 hash)
		{
			return Caching.ClearCachedVersionInternal_Injected(assetBundleName, ref hash);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00005580 File Offset: 0x00003780
		public static bool ClearOtherCachedVersions(string assetBundleName, Hash128 hash)
		{
			bool flag = string.IsNullOrEmpty(assetBundleName);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle name cannot be null or empty.");
			}
			return Caching.ClearCachedVersions(assetBundleName, hash, true);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x000055B0 File Offset: 0x000037B0
		public static bool ClearAllCachedVersions(string assetBundleName)
		{
			bool flag = string.IsNullOrEmpty(assetBundleName);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle name cannot be null or empty.");
			}
			return Caching.ClearCachedVersions(assetBundleName, default(Hash128), false);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x000055E7 File Offset: 0x000037E7
		internal static bool ClearCachedVersions(string assetBundleName, Hash128 hash, bool keepInputVersion)
		{
			return Caching.ClearCachedVersions_Injected(assetBundleName, ref hash, keepInputVersion);
		}

		// Token: 0x060002C5 RID: 709
		[MethodImpl(4096)]
		internal static extern Hash128[] GetCachedVersions(string assetBundleName);

		// Token: 0x060002C6 RID: 710 RVA: 0x000055F4 File Offset: 0x000037F4
		public static void GetCachedVersions(string assetBundleName, List<Hash128> outCachedVersions)
		{
			bool flag = string.IsNullOrEmpty(assetBundleName);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle name cannot be null or empty.");
			}
			bool flag2 = outCachedVersions == null;
			if (flag2)
			{
				throw new ArgumentNullException("Input outCachedVersions cannot be null.");
			}
			outCachedVersions.AddRange(Caching.GetCachedVersions(assetBundleName));
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00005638 File Offset: 0x00003838
		[Obsolete("Please use IsVersionCached with Hash128 instead.")]
		public static bool IsVersionCached(string url, int version)
		{
			return Caching.IsVersionCached(url, new Hash128(0U, 0U, 0U, (uint)version));
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000565C File Offset: 0x0000385C
		public static bool IsVersionCached(string url, Hash128 hash)
		{
			bool flag = string.IsNullOrEmpty(url);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle url cannot be null or empty.");
			}
			return Caching.IsVersionCached(url, "", hash);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00005690 File Offset: 0x00003890
		public static bool IsVersionCached(CachedAssetBundle cachedBundle)
		{
			bool flag = string.IsNullOrEmpty(cachedBundle.name);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle name cannot be null or empty.");
			}
			return Caching.IsVersionCached("", cachedBundle.name, cachedBundle.hash);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x000056D5 File Offset: 0x000038D5
		[NativeName("IsCached")]
		internal static bool IsVersionCached(string url, string assetBundleName, Hash128 hash)
		{
			return Caching.IsVersionCached_Injected(url, assetBundleName, ref hash);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x000056E0 File Offset: 0x000038E0
		[Obsolete("Please use MarkAsUsed with Hash128 instead.")]
		public static bool MarkAsUsed(string url, int version)
		{
			return Caching.MarkAsUsed(url, new Hash128(0U, 0U, 0U, (uint)version));
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00005704 File Offset: 0x00003904
		public static bool MarkAsUsed(string url, Hash128 hash)
		{
			bool flag = string.IsNullOrEmpty(url);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle url cannot be null or empty.");
			}
			return Caching.MarkAsUsed(url, "", hash);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00005738 File Offset: 0x00003938
		public static bool MarkAsUsed(CachedAssetBundle cachedBundle)
		{
			bool flag = string.IsNullOrEmpty(cachedBundle.name);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle name cannot be null or empty.");
			}
			return Caching.MarkAsUsed("", cachedBundle.name, cachedBundle.hash);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000577D File Offset: 0x0000397D
		internal static bool MarkAsUsed(string url, string assetBundleName, Hash128 hash)
		{
			return Caching.MarkAsUsed_Injected(url, assetBundleName, ref hash);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00005788 File Offset: 0x00003988
		[Obsolete("This function is obsolete and will always return -1. Use IsVersionCached instead.")]
		public static int GetVersionFromCache(string url)
		{
			return -1;
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000579C File Offset: 0x0000399C
		[Obsolete("Please use use Cache.spaceOccupied to get used bytes per cache.")]
		public static int spaceUsed
		{
			get
			{
				return (int)Caching.spaceOccupied;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002D1 RID: 721
		[Obsolete("This property is only used for the current cache, use Cache.spaceOccupied to get used bytes per cache.")]
		public static extern long spaceOccupied
		{
			[NativeName("GetCachingDiskSpaceUsed")]
			[StaticAccessor("GetCachingManager().GetCurrentCache()", StaticAccessorType.Dot)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x000057B4 File Offset: 0x000039B4
		[Obsolete("Please use use Cache.spaceOccupied to get used bytes per cache.")]
		public static int spaceAvailable
		{
			get
			{
				return (int)Caching.spaceFree;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002D3 RID: 723
		[Obsolete("This property is only used for the current cache, use Cache.spaceFree to get unused bytes per cache.")]
		public static extern long spaceFree
		{
			[StaticAccessor("GetCachingManager().GetCurrentCache()", StaticAccessorType.Dot)]
			[NativeName("GetCachingDiskSpaceFree")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002D4 RID: 724
		// (set) Token: 0x060002D5 RID: 725
		[Obsolete("This property is only used for the current cache, use Cache.maximumAvailableStorageSpace to access the maximum available storage space per cache.")]
		[StaticAccessor("GetCachingManager().GetCurrentCache()", StaticAccessorType.Dot)]
		public static extern long maximumAvailableDiskSpace
		{
			[NativeName("GetMaximumDiskSpaceAvailable")]
			[MethodImpl(4096)]
			get;
			[NativeName("SetMaximumDiskSpaceAvailable")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002D6 RID: 726
		// (set) Token: 0x060002D7 RID: 727
		[StaticAccessor("GetCachingManager().GetCurrentCache()", StaticAccessorType.Dot)]
		[Obsolete("This property is only used for the current cache, use Cache.expirationDelay to access the expiration delay per cache.")]
		public static extern int expirationDelay
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x000057CC File Offset: 0x000039CC
		public static Cache AddCache(string cachePath)
		{
			bool flag = string.IsNullOrEmpty(cachePath);
			if (flag)
			{
				throw new ArgumentNullException("Cache path cannot be null or empty.");
			}
			bool flag2 = false;
			bool flag3 = cachePath.Replace('\\', '/').StartsWith(Application.streamingAssetsPath);
			if (flag3)
			{
				flag2 = true;
			}
			else
			{
				bool flag4 = !Directory.Exists(cachePath);
				if (flag4)
				{
					throw new ArgumentException("Cache path '" + cachePath + "' doesn't exist.");
				}
				bool flag5 = (File.GetAttributes(cachePath) & 1) == 1;
				if (flag5)
				{
					flag2 = true;
				}
			}
			bool valid = Caching.GetCacheByPath(cachePath).valid;
			if (valid)
			{
				throw new InvalidOperationException("Cache with path '" + cachePath + "' has already been added.");
			}
			return Caching.AddCache(cachePath, flag2);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00005880 File Offset: 0x00003A80
		[NativeName("AddCachePath")]
		internal static Cache AddCache(string cachePath, bool isReadonly)
		{
			Cache cache;
			Caching.AddCache_Injected(cachePath, isReadonly, out cache);
			return cache;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00005898 File Offset: 0x00003A98
		[StaticAccessor("CachingManagerWrapper", StaticAccessorType.DoubleColon)]
		[NativeThrows]
		[NativeName("Caching_GetCacheHandleAt")]
		public static Cache GetCacheAt(int cacheIndex)
		{
			Cache cache;
			Caching.GetCacheAt_Injected(cacheIndex, out cache);
			return cache;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000058B0 File Offset: 0x00003AB0
		[StaticAccessor("CachingManagerWrapper", StaticAccessorType.DoubleColon)]
		[NativeThrows]
		[NativeName("Caching_GetCacheHandleByPath")]
		public static Cache GetCacheByPath(string cachePath)
		{
			Cache cache;
			Caching.GetCacheByPath_Injected(cachePath, out cache);
			return cache;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x000058C8 File Offset: 0x00003AC8
		public static void GetAllCachePaths(List<string> cachePaths)
		{
			cachePaths.Clear();
			for (int i = 0; i < Caching.cacheCount; i++)
			{
				cachePaths.Add(Caching.GetCacheAt(i).path);
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00005908 File Offset: 0x00003B08
		[NativeName("Caching_RemoveCacheByHandle")]
		[NativeThrows]
		[StaticAccessor("CachingManagerWrapper", StaticAccessorType.DoubleColon)]
		public static bool RemoveCache(Cache cache)
		{
			return Caching.RemoveCache_Injected(ref cache);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00005911 File Offset: 0x00003B11
		[NativeName("Caching_MoveCacheBeforeByHandle")]
		[NativeThrows]
		[StaticAccessor("CachingManagerWrapper", StaticAccessorType.DoubleColon)]
		public static void MoveCacheBefore(Cache src, Cache dst)
		{
			Caching.MoveCacheBefore_Injected(ref src, ref dst);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000591C File Offset: 0x00003B1C
		[NativeName("Caching_MoveCacheAfterByHandle")]
		[StaticAccessor("CachingManagerWrapper", StaticAccessorType.DoubleColon)]
		[NativeThrows]
		public static void MoveCacheAfter(Cache src, Cache dst)
		{
			Caching.MoveCacheAfter_Injected(ref src, ref dst);
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002E0 RID: 736
		public static extern int cacheCount
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00005928 File Offset: 0x00003B28
		[StaticAccessor("CachingManagerWrapper", StaticAccessorType.DoubleColon)]
		public static Cache defaultCache
		{
			[NativeName("Caching_GetDefaultCacheHandle")]
			get
			{
				Cache cache;
				Caching.get_defaultCache_Injected(out cache);
				return cache;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00005940 File Offset: 0x00003B40
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x00005955 File Offset: 0x00003B55
		[StaticAccessor("CachingManagerWrapper", StaticAccessorType.DoubleColon)]
		public static Cache currentCacheForWriting
		{
			[NativeName("Caching_GetCurrentCacheHandle")]
			get
			{
				Cache cache;
				Caching.get_currentCacheForWriting_Injected(out cache);
				return cache;
			}
			[NativeName("Caching_SetCurrentCacheByHandle")]
			[NativeThrows]
			set
			{
				Caching.set_currentCacheForWriting_Injected(ref value);
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00005960 File Offset: 0x00003B60
		[Obsolete("This function is obsolete. Please use ClearCache.  (UnityUpgradable) -> ClearCache()")]
		public static bool CleanCache()
		{
			return Caching.ClearCache();
		}

		// Token: 0x060002E6 RID: 742
		[MethodImpl(4096)]
		private static extern bool ClearCachedVersionInternal_Injected(string assetBundleName, ref Hash128 hash);

		// Token: 0x060002E7 RID: 743
		[MethodImpl(4096)]
		private static extern bool ClearCachedVersions_Injected(string assetBundleName, ref Hash128 hash, bool keepInputVersion);

		// Token: 0x060002E8 RID: 744
		[MethodImpl(4096)]
		private static extern bool IsVersionCached_Injected(string url, string assetBundleName, ref Hash128 hash);

		// Token: 0x060002E9 RID: 745
		[MethodImpl(4096)]
		private static extern bool MarkAsUsed_Injected(string url, string assetBundleName, ref Hash128 hash);

		// Token: 0x060002EA RID: 746
		[MethodImpl(4096)]
		private static extern void AddCache_Injected(string cachePath, bool isReadonly, out Cache ret);

		// Token: 0x060002EB RID: 747
		[MethodImpl(4096)]
		private static extern void GetCacheAt_Injected(int cacheIndex, out Cache ret);

		// Token: 0x060002EC RID: 748
		[MethodImpl(4096)]
		private static extern void GetCacheByPath_Injected(string cachePath, out Cache ret);

		// Token: 0x060002ED RID: 749
		[MethodImpl(4096)]
		private static extern bool RemoveCache_Injected(ref Cache cache);

		// Token: 0x060002EE RID: 750
		[MethodImpl(4096)]
		private static extern void MoveCacheBefore_Injected(ref Cache src, ref Cache dst);

		// Token: 0x060002EF RID: 751
		[MethodImpl(4096)]
		private static extern void MoveCacheAfter_Injected(ref Cache src, ref Cache dst);

		// Token: 0x060002F0 RID: 752
		[MethodImpl(4096)]
		private static extern void get_defaultCache_Injected(out Cache ret);

		// Token: 0x060002F1 RID: 753
		[MethodImpl(4096)]
		private static extern void get_currentCacheForWriting_Injected(out Cache ret);

		// Token: 0x060002F2 RID: 754
		[MethodImpl(4096)]
		private static extern void set_currentCacheForWriting_Injected(ref Cache value);
	}
}
