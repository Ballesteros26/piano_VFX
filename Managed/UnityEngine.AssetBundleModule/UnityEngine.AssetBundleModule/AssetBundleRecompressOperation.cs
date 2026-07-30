using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000006 RID: 6
	[RequiredByNativeCode]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleRecompressOperation.h")]
	[StructLayout(0)]
	public class AssetBundleRecompressOperation : AsyncOperation
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000050 RID: 80
		public extern string humanReadableResult
		{
			[NativeMethod("GetResultStr")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000051 RID: 81
		public extern string inputPath
		{
			[NativeMethod("GetInputPath")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000052 RID: 82
		public extern string outputPath
		{
			[NativeMethod("GetOutputPath")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000053 RID: 83
		public extern AssetBundleLoadResult result
		{
			[NativeMethod("GetResult")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000054 RID: 84
		public extern bool success
		{
			[NativeMethod("GetSuccess")]
			[MethodImpl(4096)]
			get;
		}
	}
}
