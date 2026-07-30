using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000007 RID: 7
	[RequiredByNativeCode]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadAssetOperation.h")]
	[StructLayout(0)]
	public class AssetBundleRequest : AsyncOperation
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000056 RID: 86
		public extern Object asset
		{
			[NativeMethod("GetLoadedAsset")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000057 RID: 87
		public extern Object[] allAssets
		{
			[NativeMethod("GetAllLoadedAssets")]
			[MethodImpl(4096)]
			get;
		}
	}
}
