using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.AssetBundlePatching
{
	// Token: 0x0200000B RID: 11
	[NativeHeader("Modules/AssetBundle/Public/AssetBundlePatching.h")]
	public static class AssetBundleUtility
	{
		// Token: 0x06000061 RID: 97
		[FreeFunction]
		[MethodImpl(4096)]
		public static extern void PatchAssetBundles(AssetBundle[] bundles, string[] filenames);
	}
}
