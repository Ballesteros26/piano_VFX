using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200030E RID: 782
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/AsyncGPUReadbackManaged.h")]
	internal struct AsyncRequestNativeArrayData
	{
		// Token: 0x06001AC4 RID: 6852 RVA: 0x0002BC20 File Offset: 0x00029E20
		public static AsyncRequestNativeArrayData CreateAndCheckAccess<T>(NativeArray<T> array) where T : struct
		{
			return new AsyncRequestNativeArrayData
			{
				nativeArrayBuffer = array.GetUnsafePtr<T>(),
				lengthInBytes = (long)(array.Length * UnsafeUtility.SizeOf<T>())
			};
		}

		// Token: 0x04000838 RID: 2104
		public unsafe void* nativeArrayBuffer;

		// Token: 0x04000839 RID: 2105
		public long lengthInBytes;
	}
}
