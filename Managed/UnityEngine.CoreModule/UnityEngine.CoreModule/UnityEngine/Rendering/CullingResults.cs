using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x02000363 RID: 867
	[NativeHeader("Runtime/Export/RenderPipeline/ScriptableRenderPipeline.bindings.h")]
	[NativeHeader("Runtime/Graphics/ScriptableRenderLoop/ScriptableCulling.h")]
	[NativeHeader("Runtime/Scripting/ScriptingCommonStructDefinitions.h")]
	public struct CullingResults : IEquatable<CullingResults>
	{
		// Token: 0x06001DA2 RID: 7586
		[FreeFunction("ScriptableRenderPipeline_Bindings::GetLightIndexCount")]
		[MethodImpl(4096)]
		private static extern int GetLightIndexCount(IntPtr cullingResultsPtr);

		// Token: 0x06001DA3 RID: 7587
		[FreeFunction("ScriptableRenderPipeline_Bindings::GetReflectionProbeIndexCount")]
		[MethodImpl(4096)]
		private static extern int GetReflectionProbeIndexCount(IntPtr cullingResultsPtr);

		// Token: 0x06001DA4 RID: 7588
		[FreeFunction("FillLightAndReflectionProbeIndices")]
		[MethodImpl(4096)]
		private static extern void FillLightAndReflectionProbeIndices(IntPtr cullingResultsPtr, ComputeBuffer computeBuffer);

		// Token: 0x06001DA5 RID: 7589
		[FreeFunction("FillLightAndReflectionProbeIndices")]
		[MethodImpl(4096)]
		private static extern void FillLightAndReflectionProbeIndicesGraphicsBuffer(IntPtr cullingResultsPtr, GraphicsBuffer buffer);

		// Token: 0x06001DA6 RID: 7590
		[FreeFunction("GetLightIndexMapSize")]
		[MethodImpl(4096)]
		private static extern int GetLightIndexMapSize(IntPtr cullingResultsPtr);

		// Token: 0x06001DA7 RID: 7591
		[FreeFunction("GetReflectionProbeIndexMapSize")]
		[MethodImpl(4096)]
		private static extern int GetReflectionProbeIndexMapSize(IntPtr cullingResultsPtr);

		// Token: 0x06001DA8 RID: 7592
		[FreeFunction("FillLightIndexMapScriptable")]
		[MethodImpl(4096)]
		private static extern void FillLightIndexMap(IntPtr cullingResultsPtr, IntPtr indexMapPtr, int indexMapSize);

		// Token: 0x06001DA9 RID: 7593
		[FreeFunction("FillReflectionProbeIndexMapScriptable")]
		[MethodImpl(4096)]
		private static extern void FillReflectionProbeIndexMap(IntPtr cullingResultsPtr, IntPtr indexMapPtr, int indexMapSize);

		// Token: 0x06001DAA RID: 7594
		[FreeFunction("SetLightIndexMapScriptable")]
		[MethodImpl(4096)]
		private static extern void SetLightIndexMap(IntPtr cullingResultsPtr, IntPtr indexMapPtr, int indexMapSize);

		// Token: 0x06001DAB RID: 7595
		[FreeFunction("SetReflectionProbeIndexMapScriptable")]
		[MethodImpl(4096)]
		private static extern void SetReflectionProbeIndexMap(IntPtr cullingResultsPtr, IntPtr indexMapPtr, int indexMapSize);

		// Token: 0x06001DAC RID: 7596
		[FreeFunction("ScriptableRenderPipeline_Bindings::GetShadowCasterBounds")]
		[MethodImpl(4096)]
		private static extern bool GetShadowCasterBounds(IntPtr cullingResultsPtr, int lightIndex, out Bounds bounds);

		// Token: 0x06001DAD RID: 7597
		[FreeFunction("ScriptableRenderPipeline_Bindings::ComputeSpotShadowMatricesAndCullingPrimitives")]
		[MethodImpl(4096)]
		private static extern bool ComputeSpotShadowMatricesAndCullingPrimitives(IntPtr cullingResultsPtr, int activeLightIndex, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData);

		// Token: 0x06001DAE RID: 7598
		[FreeFunction("ScriptableRenderPipeline_Bindings::ComputePointShadowMatricesAndCullingPrimitives")]
		[MethodImpl(4096)]
		private static extern bool ComputePointShadowMatricesAndCullingPrimitives(IntPtr cullingResultsPtr, int activeLightIndex, CubemapFace cubemapFace, float fovBias, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData);

		// Token: 0x06001DAF RID: 7599 RVA: 0x000322F8 File Offset: 0x000304F8
		[FreeFunction("ScriptableRenderPipeline_Bindings::ComputeDirectionalShadowMatricesAndCullingPrimitives")]
		private static bool ComputeDirectionalShadowMatricesAndCullingPrimitives(IntPtr cullingResultsPtr, int activeLightIndex, int splitIndex, int splitCount, Vector3 splitRatio, int shadowResolution, float shadowNearPlaneOffset, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData)
		{
			return CullingResults.ComputeDirectionalShadowMatricesAndCullingPrimitives_Injected(cullingResultsPtr, activeLightIndex, splitIndex, splitCount, ref splitRatio, shadowResolution, shadowNearPlaneOffset, out viewMatrix, out projMatrix, out shadowSplitData);
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001DB0 RID: 7600 RVA: 0x0003231A File Offset: 0x0003051A
		public unsafe NativeArray<VisibleLight> visibleLights
		{
			get
			{
				return this.GetNativeArray<VisibleLight>((void*)this.m_AllocationInfo->visibleLightsPtr, this.m_AllocationInfo->visibleLightCount);
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001DB1 RID: 7601 RVA: 0x00032338 File Offset: 0x00030538
		public unsafe NativeArray<VisibleLight> visibleOffscreenVertexLights
		{
			get
			{
				return this.GetNativeArray<VisibleLight>((void*)this.m_AllocationInfo->visibleOffscreenVertexLightsPtr, this.m_AllocationInfo->visibleOffscreenVertexLightCount);
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001DB2 RID: 7602 RVA: 0x00032356 File Offset: 0x00030556
		public unsafe NativeArray<VisibleReflectionProbe> visibleReflectionProbes
		{
			get
			{
				return this.GetNativeArray<VisibleReflectionProbe>((void*)this.m_AllocationInfo->visibleReflectionProbesPtr, this.m_AllocationInfo->visibleReflectionProbeCount);
			}
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x00032374 File Offset: 0x00030574
		private unsafe NativeArray<T> GetNativeArray<T>(void* dataPointer, int length) where T : struct
		{
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(dataPointer, length, Allocator.Invalid);
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001DB4 RID: 7604 RVA: 0x00032390 File Offset: 0x00030590
		public int lightIndexCount
		{
			get
			{
				return CullingResults.GetLightIndexCount(this.ptr);
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001DB5 RID: 7605 RVA: 0x000323B0 File Offset: 0x000305B0
		public int reflectionProbeIndexCount
		{
			get
			{
				return CullingResults.GetReflectionProbeIndexCount(this.ptr);
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001DB6 RID: 7606 RVA: 0x000323D0 File Offset: 0x000305D0
		public int lightAndReflectionProbeIndexCount
		{
			get
			{
				return CullingResults.GetLightIndexCount(this.ptr) + CullingResults.GetReflectionProbeIndexCount(this.ptr);
			}
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x000323F9 File Offset: 0x000305F9
		public void FillLightAndReflectionProbeIndices(ComputeBuffer computeBuffer)
		{
			CullingResults.FillLightAndReflectionProbeIndices(this.ptr, computeBuffer);
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x00032409 File Offset: 0x00030609
		public void FillLightAndReflectionProbeIndices(GraphicsBuffer buffer)
		{
			CullingResults.FillLightAndReflectionProbeIndicesGraphicsBuffer(this.ptr, buffer);
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x0003241C File Offset: 0x0003061C
		public NativeArray<int> GetLightIndexMap(Allocator allocator)
		{
			int lightIndexMapSize = CullingResults.GetLightIndexMapSize(this.ptr);
			NativeArray<int> nativeArray = new NativeArray<int>(lightIndexMapSize, allocator, NativeArrayOptions.UninitializedMemory);
			CullingResults.FillLightIndexMap(this.ptr, (IntPtr)nativeArray.GetUnsafePtr<int>(), lightIndexMapSize);
			return nativeArray;
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x0003245D File Offset: 0x0003065D
		public void SetLightIndexMap(NativeArray<int> lightIndexMap)
		{
			CullingResults.SetLightIndexMap(this.ptr, (IntPtr)lightIndexMap.GetUnsafeReadOnlyPtr<int>(), lightIndexMap.Length);
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x00032480 File Offset: 0x00030680
		public NativeArray<int> GetReflectionProbeIndexMap(Allocator allocator)
		{
			int reflectionProbeIndexMapSize = CullingResults.GetReflectionProbeIndexMapSize(this.ptr);
			NativeArray<int> nativeArray = new NativeArray<int>(reflectionProbeIndexMapSize, allocator, NativeArrayOptions.UninitializedMemory);
			CullingResults.FillReflectionProbeIndexMap(this.ptr, (IntPtr)nativeArray.GetUnsafePtr<int>(), reflectionProbeIndexMapSize);
			return nativeArray;
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x000324C1 File Offset: 0x000306C1
		public void SetReflectionProbeIndexMap(NativeArray<int> lightIndexMap)
		{
			CullingResults.SetReflectionProbeIndexMap(this.ptr, (IntPtr)lightIndexMap.GetUnsafeReadOnlyPtr<int>(), lightIndexMap.Length);
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x000324E4 File Offset: 0x000306E4
		public bool GetShadowCasterBounds(int lightIndex, out Bounds outBounds)
		{
			return CullingResults.GetShadowCasterBounds(this.ptr, lightIndex, out outBounds);
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x00032504 File Offset: 0x00030704
		public bool ComputeSpotShadowMatricesAndCullingPrimitives(int activeLightIndex, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData)
		{
			return CullingResults.ComputeSpotShadowMatricesAndCullingPrimitives(this.ptr, activeLightIndex, out viewMatrix, out projMatrix, out shadowSplitData);
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x00032528 File Offset: 0x00030728
		public bool ComputePointShadowMatricesAndCullingPrimitives(int activeLightIndex, CubemapFace cubemapFace, float fovBias, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData)
		{
			return CullingResults.ComputePointShadowMatricesAndCullingPrimitives(this.ptr, activeLightIndex, cubemapFace, fovBias, out viewMatrix, out projMatrix, out shadowSplitData);
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x00032550 File Offset: 0x00030750
		public bool ComputeDirectionalShadowMatricesAndCullingPrimitives(int activeLightIndex, int splitIndex, int splitCount, Vector3 splitRatio, int shadowResolution, float shadowNearPlaneOffset, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData)
		{
			return CullingResults.ComputeDirectionalShadowMatricesAndCullingPrimitives(this.ptr, activeLightIndex, splitIndex, splitCount, splitRatio, shadowResolution, shadowNearPlaneOffset, out viewMatrix, out projMatrix, out shadowSplitData);
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x00002EC3 File Offset: 0x000010C3
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		internal void Validate()
		{
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x0003257C File Offset: 0x0003077C
		public bool Equals(CullingResults other)
		{
			return this.ptr.Equals(other.ptr) && this.m_AllocationInfo == other.m_AllocationInfo;
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x000325B8 File Offset: 0x000307B8
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is CullingResults && this.Equals((CullingResults)obj);
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x000325F0 File Offset: 0x000307F0
		public override int GetHashCode()
		{
			int hashCode = this.ptr.GetHashCode();
			return (hashCode * 397) ^ this.m_AllocationInfo;
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x00032624 File Offset: 0x00030824
		public static bool operator ==(CullingResults left, CullingResults right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x00032640 File Offset: 0x00030840
		public static bool operator !=(CullingResults left, CullingResults right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06001DC7 RID: 7623
		[MethodImpl(4096)]
		private static extern bool ComputeDirectionalShadowMatricesAndCullingPrimitives_Injected(IntPtr cullingResultsPtr, int activeLightIndex, int splitIndex, int splitCount, ref Vector3 splitRatio, int shadowResolution, float shadowNearPlaneOffset, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData);

		// Token: 0x04000A8C RID: 2700
		internal IntPtr ptr;

		// Token: 0x04000A8D RID: 2701
		private unsafe CullingAllocationInfo* m_AllocationInfo;
	}
}
