using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000004 RID: 4
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadFromAsyncOperation.h")]
	[RequiredByNativeCode]
	[StructLayout(0)]
	public class AssetBundleCreateRequest : AsyncOperation
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000045 RID: 69
		public extern AssetBundle assetBundle
		{
			[NativeMethod("GetAssetBundleBlocking")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000046 RID: 70
		[NativeMethod("SetEnableCompatibilityChecks")]
		[MethodImpl(4096)]
		private extern void SetEnableCompatibilityChecks(bool set);

		// Token: 0x06000047 RID: 71 RVA: 0x0000278B File Offset: 0x0000098B
		internal void DisableCompatibilityChecks()
		{
			this.SetEnableCompatibilityChecks(false);
		}
	}
}
