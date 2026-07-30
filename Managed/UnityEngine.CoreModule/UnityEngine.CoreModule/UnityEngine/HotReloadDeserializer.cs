using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200015C RID: 348
	[NativeConditional("HOT_RELOAD_AVAILABLE")]
	[NativeType(Header = "Runtime/Export/HotReload/HotReload.bindings.h")]
	internal static class HotReloadDeserializer
	{
		// Token: 0x06000FEB RID: 4075
		[FreeFunction("HotReload::Prepare")]
		[MethodImpl(4096)]
		internal static extern void PrepareHotReload();

		// Token: 0x06000FEC RID: 4076
		[FreeFunction("HotReload::Finish")]
		[MethodImpl(4096)]
		internal static extern void FinishHotReload(Type[] typesToReset);

		// Token: 0x06000FED RID: 4077
		[FreeFunction("HotReload::CreateEmptyAsset")]
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern Object CreateEmptyAsset(Type type);

		// Token: 0x06000FEE RID: 4078
		[FreeFunction("HotReload::DeserializeAsset")]
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern void DeserializeAsset(Object asset, byte[] data);

		// Token: 0x06000FEF RID: 4079
		[FreeFunction("HotReload::RemapInstanceIds")]
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void RemapInstanceIds(Object editorAsset, int[] editorToPlayerInstanceIdMapKeys, int[] editorToPlayerInstanceIdMapValues);

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0001666F File Offset: 0x0001486F
		internal static void RemapInstanceIds(Object editorAsset, Dictionary<int, int> editorToPlayerInstanceIdMap)
		{
			HotReloadDeserializer.RemapInstanceIds(editorAsset, Enumerable.ToArray<int>(editorToPlayerInstanceIdMap.Keys), Enumerable.ToArray<int>(editorToPlayerInstanceIdMap.Values));
		}

		// Token: 0x06000FF1 RID: 4081
		[FreeFunction("HotReload::FinalizeAssetCreation")]
		[MethodImpl(4096)]
		internal static extern void FinalizeAssetCreation(Object asset);

		// Token: 0x06000FF2 RID: 4082
		[FreeFunction("HotReload::GetDependencies")]
		[MethodImpl(4096)]
		internal static extern Object[] GetDependencies(Object asset);

		// Token: 0x06000FF3 RID: 4083
		[FreeFunction("HotReload::GetNullDependencies")]
		[MethodImpl(4096)]
		internal static extern int[] GetNullDependencies(Object asset);
	}
}
