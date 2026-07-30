using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000005 RID: 5
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleManifest.h")]
	public class AssetBundleManifest : Object
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00002050 File Offset: 0x00000250
		private AssetBundleManifest()
		{
		}

		// Token: 0x0600004A RID: 74
		[NativeMethod("GetAllAssetBundles")]
		[MethodImpl(4096)]
		public extern string[] GetAllAssetBundles();

		// Token: 0x0600004B RID: 75
		[NativeMethod("GetAllAssetBundlesWithVariant")]
		[MethodImpl(4096)]
		public extern string[] GetAllAssetBundlesWithVariant();

		// Token: 0x0600004C RID: 76 RVA: 0x000027A0 File Offset: 0x000009A0
		[NativeMethod("GetAssetBundleHash")]
		public Hash128 GetAssetBundleHash(string assetBundleName)
		{
			Hash128 hash;
			this.GetAssetBundleHash_Injected(assetBundleName, out hash);
			return hash;
		}

		// Token: 0x0600004D RID: 77
		[NativeMethod("GetDirectDependencies")]
		[MethodImpl(4096)]
		public extern string[] GetDirectDependencies(string assetBundleName);

		// Token: 0x0600004E RID: 78
		[NativeMethod("GetAllDependencies")]
		[MethodImpl(4096)]
		public extern string[] GetAllDependencies(string assetBundleName);

		// Token: 0x0600004F RID: 79
		[MethodImpl(4096)]
		private extern void GetAssetBundleHash_Injected(string assetBundleName, out Hash128 ret);
	}
}
