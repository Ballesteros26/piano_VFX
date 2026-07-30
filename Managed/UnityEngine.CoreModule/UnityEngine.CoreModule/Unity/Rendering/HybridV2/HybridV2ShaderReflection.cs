using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Bindings;

namespace Unity.Rendering.HybridV2
{
	// Token: 0x0200004C RID: 76
	public class HybridV2ShaderReflection
	{
		// Token: 0x060000BE RID: 190
		[FreeFunction("ShaderScripting::GetDOTSInstancingCbuffersPointer")]
		[MethodImpl(4096)]
		private static extern IntPtr GetDOTSInstancingCbuffersPointer([NotNull] Shader shader, ref int cbufferCount);

		// Token: 0x060000BF RID: 191
		[FreeFunction("ShaderScripting::GetDOTSInstancingPropertiesPointer")]
		[MethodImpl(4096)]
		private static extern IntPtr GetDOTSInstancingPropertiesPointer([NotNull] Shader shader, ref int propertyCount);

		// Token: 0x060000C0 RID: 192
		[FreeFunction("Shader::GetDOTSReflectionVersionNumber")]
		[MethodImpl(4096)]
		public static extern uint GetDOTSReflectionVersionNumber();

		// Token: 0x060000C1 RID: 193 RVA: 0x00002B38 File Offset: 0x00000D38
		public unsafe static NativeArray<DOTSInstancingCbuffer> GetDOTSInstancingCbuffers(Shader shader)
		{
			bool flag = shader == null;
			NativeArray<DOTSInstancingCbuffer> nativeArray;
			if (flag)
			{
				nativeArray = default(NativeArray<DOTSInstancingCbuffer>);
			}
			else
			{
				int num = 0;
				IntPtr dotsinstancingCbuffersPointer = HybridV2ShaderReflection.GetDOTSInstancingCbuffersPointer(shader, ref num);
				bool flag2 = dotsinstancingCbuffersPointer == IntPtr.Zero;
				if (flag2)
				{
					nativeArray = default(NativeArray<DOTSInstancingCbuffer>);
				}
				else
				{
					NativeArray<DOTSInstancingCbuffer> nativeArray2 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<DOTSInstancingCbuffer>((void*)dotsinstancingCbuffersPointer, num, Allocator.Temp);
					nativeArray = nativeArray2;
				}
			}
			return nativeArray;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00002BA0 File Offset: 0x00000DA0
		public unsafe static NativeArray<DOTSInstancingProperty> GetDOTSInstancingProperties(Shader shader)
		{
			bool flag = shader == null;
			NativeArray<DOTSInstancingProperty> nativeArray;
			if (flag)
			{
				nativeArray = default(NativeArray<DOTSInstancingProperty>);
			}
			else
			{
				int num = 0;
				IntPtr dotsinstancingPropertiesPointer = HybridV2ShaderReflection.GetDOTSInstancingPropertiesPointer(shader, ref num);
				bool flag2 = dotsinstancingPropertiesPointer == IntPtr.Zero;
				if (flag2)
				{
					nativeArray = default(NativeArray<DOTSInstancingProperty>);
				}
				else
				{
					NativeArray<DOTSInstancingProperty> nativeArray2 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<DOTSInstancingProperty>((void*)dotsinstancingPropertiesPointer, num, Allocator.Temp);
					nativeArray = nativeArray2;
				}
			}
			return nativeArray;
		}
	}
}
