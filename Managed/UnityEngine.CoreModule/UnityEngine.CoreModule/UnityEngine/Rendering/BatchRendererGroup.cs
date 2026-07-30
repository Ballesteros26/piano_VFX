using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000356 RID: 854
	[NativeHeader("Runtime/Math/Matrix4x4.h")]
	[NativeHeader("Runtime/Camera/BatchRendererGroup.h")]
	[RequiredByNativeCode]
	[StructLayout(0)]
	public class BatchRendererGroup : IDisposable
	{
		// Token: 0x06001D0B RID: 7435 RVA: 0x000304E0 File Offset: 0x0002E6E0
		public BatchRendererGroup(BatchRendererGroup.OnPerformCulling cullingCallback)
		{
			this.m_PerformCulling = cullingCallback;
			this.m_GroupHandle = BatchRendererGroup.Create(this);
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x00030508 File Offset: 0x0002E708
		public void Dispose()
		{
			BatchRendererGroup.Destroy(this.m_GroupHandle);
			this.m_GroupHandle = IntPtr.Zero;
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x00030524 File Offset: 0x0002E724
		public int AddBatch(Mesh mesh, int subMeshIndex, Material material, int layer, ShadowCastingMode castShadows, bool receiveShadows, bool invertCulling, Bounds bounds, int instanceCount, MaterialPropertyBlock customProps, GameObject associatedSceneObject, ulong sceneCullingMask = 9223372036854775808UL)
		{
			return this.AddBatch_Injected(mesh, subMeshIndex, material, layer, castShadows, receiveShadows, invertCulling, ref bounds, instanceCount, customProps, associatedSceneObject, sceneCullingMask);
		}

		// Token: 0x06001D0E RID: 7438
		[MethodImpl(4096)]
		public extern void SetBatchFlags(int batchIndex, ulong flags);

		// Token: 0x06001D0F RID: 7439 RVA: 0x0003054C File Offset: 0x0002E74C
		public void SetBatchPropertyMetadata(int batchIndex, NativeArray<int> cbufferLengths, NativeArray<int> cbufferMetadata)
		{
			this.InternalSetBatchPropertyMetadata(batchIndex, (IntPtr)cbufferLengths.GetUnsafeReadOnlyPtr<int>(), cbufferLengths.Length, (IntPtr)cbufferMetadata.GetUnsafeReadOnlyPtr<int>(), cbufferMetadata.Length);
		}

		// Token: 0x06001D10 RID: 7440
		[MethodImpl(4096)]
		private extern void InternalSetBatchPropertyMetadata(int batchIndex, IntPtr cbufferLengths, int cbufferLengthsCount, IntPtr cbufferMetadata, int cbufferMetadataCount);

		// Token: 0x06001D11 RID: 7441
		[MethodImpl(4096)]
		public extern void SetInstancingData(int batchIndex, int instanceCount, MaterialPropertyBlock customProps);

		// Token: 0x06001D12 RID: 7442 RVA: 0x0003057C File Offset: 0x0002E77C
		public unsafe NativeArray<Matrix4x4> GetBatchMatrices(int batchIndex)
		{
			int num = 0;
			void* batchMatrices = this.GetBatchMatrices(batchIndex, out num);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Matrix4x4>(batchMatrices, num, Allocator.Invalid);
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x000305A4 File Offset: 0x0002E7A4
		public unsafe NativeArray<int> GetBatchScalarArrayInt(int batchIndex, string propertyName)
		{
			int num = 0;
			void* batchScalarArray = this.GetBatchScalarArray(batchIndex, propertyName, out num);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(batchScalarArray, num, Allocator.Invalid);
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x000305D0 File Offset: 0x0002E7D0
		public unsafe NativeArray<float> GetBatchScalarArray(int batchIndex, string propertyName)
		{
			int num = 0;
			void* batchScalarArray = this.GetBatchScalarArray(batchIndex, propertyName, out num);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<float>(batchScalarArray, num, Allocator.Invalid);
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x000305FC File Offset: 0x0002E7FC
		public unsafe NativeArray<int> GetBatchVectorArrayInt(int batchIndex, string propertyName)
		{
			int num = 0;
			void* batchVectorArray = this.GetBatchVectorArray(batchIndex, propertyName, out num);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(batchVectorArray, num, Allocator.Invalid);
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x00030628 File Offset: 0x0002E828
		public unsafe NativeArray<Vector4> GetBatchVectorArray(int batchIndex, string propertyName)
		{
			int num = 0;
			void* batchVectorArray = this.GetBatchVectorArray(batchIndex, propertyName, out num);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Vector4>(batchVectorArray, num, Allocator.Invalid);
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x00030654 File Offset: 0x0002E854
		public unsafe NativeArray<Matrix4x4> GetBatchMatrixArray(int batchIndex, string propertyName)
		{
			int num = 0;
			void* batchMatrixArray = this.GetBatchMatrixArray(batchIndex, propertyName, out num);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Matrix4x4>(batchMatrixArray, num, Allocator.Invalid);
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x00030680 File Offset: 0x0002E880
		public unsafe NativeArray<int> GetBatchScalarArrayInt(int batchIndex, int propertyName)
		{
			int num = 0;
			void* batchScalarArray_Internal = this.GetBatchScalarArray_Internal(batchIndex, propertyName, out num);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(batchScalarArray_Internal, num, Allocator.Invalid);
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x000306AC File Offset: 0x0002E8AC
		public unsafe NativeArray<float> GetBatchScalarArray(int batchIndex, int propertyName)
		{
			int num = 0;
			void* batchScalarArray_Internal = this.GetBatchScalarArray_Internal(batchIndex, propertyName, out num);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<float>(batchScalarArray_Internal, num, Allocator.Invalid);
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x000306D8 File Offset: 0x0002E8D8
		public unsafe NativeArray<int> GetBatchVectorArrayInt(int batchIndex, int propertyName)
		{
			int num = 0;
			void* batchVectorArray_Internal = this.GetBatchVectorArray_Internal(batchIndex, propertyName, out num);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(batchVectorArray_Internal, num, Allocator.Invalid);
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x00030704 File Offset: 0x0002E904
		public unsafe NativeArray<Vector4> GetBatchVectorArray(int batchIndex, int propertyName)
		{
			int num = 0;
			void* batchVectorArray_Internal = this.GetBatchVectorArray_Internal(batchIndex, propertyName, out num);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Vector4>(batchVectorArray_Internal, num, Allocator.Invalid);
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x00030730 File Offset: 0x0002E930
		public unsafe NativeArray<Matrix4x4> GetBatchMatrixArray(int batchIndex, int propertyName)
		{
			int num = 0;
			void* batchMatrixArray_Internal = this.GetBatchMatrixArray_Internal(batchIndex, propertyName, out num);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Matrix4x4>(batchMatrixArray_Internal, num, Allocator.Invalid);
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x00030759 File Offset: 0x0002E959
		public void SetBatchBounds(int batchIndex, Bounds bounds)
		{
			this.SetBatchBounds_Injected(batchIndex, ref bounds);
		}

		// Token: 0x06001D1E RID: 7454
		[MethodImpl(4096)]
		public extern int GetNumBatches();

		// Token: 0x06001D1F RID: 7455
		[MethodImpl(4096)]
		public extern void RemoveBatch(int index);

		// Token: 0x06001D20 RID: 7456
		[MethodImpl(4096)]
		private unsafe extern void* GetBatchMatrices(int batchIndex, out int matrixCount);

		// Token: 0x06001D21 RID: 7457
		[MethodImpl(4096)]
		private unsafe extern void* GetBatchScalarArray(int batchIndex, string propertyName, out int elementCount);

		// Token: 0x06001D22 RID: 7458
		[MethodImpl(4096)]
		private unsafe extern void* GetBatchVectorArray(int batchIndex, string propertyName, out int elementCount);

		// Token: 0x06001D23 RID: 7459
		[MethodImpl(4096)]
		private unsafe extern void* GetBatchMatrixArray(int batchIndex, string propertyName, out int elementCount);

		// Token: 0x06001D24 RID: 7460
		[NativeName("GetBatchScalarArray")]
		[MethodImpl(4096)]
		private unsafe extern void* GetBatchScalarArray_Internal(int batchIndex, int propertyName, out int elementCount);

		// Token: 0x06001D25 RID: 7461
		[NativeName("GetBatchVectorArray")]
		[MethodImpl(4096)]
		private unsafe extern void* GetBatchVectorArray_Internal(int batchIndex, int propertyName, out int elementCount);

		// Token: 0x06001D26 RID: 7462
		[NativeName("GetBatchMatrixArray")]
		[MethodImpl(4096)]
		private unsafe extern void* GetBatchMatrixArray_Internal(int batchIndex, int propertyName, out int elementCount);

		// Token: 0x06001D27 RID: 7463
		[MethodImpl(4096)]
		private static extern IntPtr Create(BatchRendererGroup group);

		// Token: 0x06001D28 RID: 7464
		[MethodImpl(4096)]
		private static extern void Destroy(IntPtr groupHandle);

		// Token: 0x06001D29 RID: 7465 RVA: 0x00030764 File Offset: 0x0002E964
		[RequiredByNativeCode]
		private unsafe static void InvokeOnPerformCulling(BatchRendererGroup group, ref BatchRendererCullingOutput context, ref LODParameters lodParameters)
		{
			NativeArray<Plane> nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Plane>((void*)context.cullingPlanes, context.cullingPlanesCount, Allocator.Invalid);
			NativeArray<BatchVisibility> nativeArray2 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<BatchVisibility>((void*)context.batchVisibility, context.batchVisibilityCount, Allocator.Invalid);
			NativeArray<int> nativeArray3 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)context.visibleIndices, context.visibleIndicesCount, Allocator.Invalid);
			try
			{
				context.cullingJobsFence = group.m_PerformCulling(group, new BatchCullingContext(nativeArray, nativeArray2, nativeArray3, lodParameters));
			}
			finally
			{
				JobHandle.ScheduleBatchedJobs();
			}
		}

		// Token: 0x06001D2A RID: 7466
		[MethodImpl(4096)]
		private extern int AddBatch_Injected(Mesh mesh, int subMeshIndex, Material material, int layer, ShadowCastingMode castShadows, bool receiveShadows, bool invertCulling, ref Bounds bounds, int instanceCount, MaterialPropertyBlock customProps, GameObject associatedSceneObject, ulong sceneCullingMask = 9223372036854775808UL);

		// Token: 0x06001D2B RID: 7467
		[MethodImpl(4096)]
		private extern void SetBatchBounds_Injected(int batchIndex, ref Bounds bounds);

		// Token: 0x04000A29 RID: 2601
		private IntPtr m_GroupHandle = IntPtr.Zero;

		// Token: 0x04000A2A RID: 2602
		private BatchRendererGroup.OnPerformCulling m_PerformCulling;

		// Token: 0x02000357 RID: 855
		// (Invoke) Token: 0x06001D2D RID: 7469
		public delegate JobHandle OnPerformCulling(BatchRendererGroup rendererGroup, BatchCullingContext cullingContext);
	}
}
