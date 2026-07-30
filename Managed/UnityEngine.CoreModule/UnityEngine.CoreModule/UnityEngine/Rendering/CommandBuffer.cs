using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Profiling;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200034F RID: 847
	[NativeType("Runtime/Graphics/CommandBuffer/RenderingCommandBuffer.h")]
	[NativeHeader("Runtime/Shaders/RayTracingShader.h")]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Export/Graphics/RenderingCommandBuffer.bindings.h")]
	[NativeHeader("Runtime/Shaders/ComputeShader.h")]
	public class CommandBuffer : IDisposable
	{
		// Token: 0x06001B64 RID: 7012 RVA: 0x0002CE3E File Offset: 0x0002B03E
		public void ConvertTexture(RenderTargetIdentifier src, RenderTargetIdentifier dst)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.ConvertTexture_Internal(src, 0, dst, 0);
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x0002CE55 File Offset: 0x0002B055
		public void ConvertTexture(RenderTargetIdentifier src, int srcElement, RenderTargetIdentifier dst, int dstElement)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.ConvertTexture_Internal(src, srcElement, dst, dstElement);
		}

		// Token: 0x06001B66 RID: 7014
		[NativeMethod("AddWaitAllAsyncReadbackRequests")]
		[MethodImpl(4096)]
		public extern void WaitAllAsyncReadbackRequests();

		// Token: 0x06001B67 RID: 7015 RVA: 0x0002CE6D File Offset: 0x0002B06D
		public void RequestAsyncReadback(ComputeBuffer src, Action<AsyncGPUReadbackRequest> callback)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_RequestAsyncReadback_1(src, callback, null);
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x0002CE86 File Offset: 0x0002B086
		public void RequestAsyncReadback(GraphicsBuffer src, Action<AsyncGPUReadbackRequest> callback)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_RequestAsyncReadback_8(src, callback, null);
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x0002CE9F File Offset: 0x0002B09F
		public void RequestAsyncReadback(ComputeBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_RequestAsyncReadback_2(src, size, offset, callback, null);
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x0002CEBB File Offset: 0x0002B0BB
		public void RequestAsyncReadback(GraphicsBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_RequestAsyncReadback_9(src, size, offset, callback, null);
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x0002CED7 File Offset: 0x0002B0D7
		public void RequestAsyncReadback(Texture src, Action<AsyncGPUReadbackRequest> callback)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_RequestAsyncReadback_3(src, callback, null);
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x0002CEF0 File Offset: 0x0002B0F0
		public void RequestAsyncReadback(Texture src, int mipIndex, Action<AsyncGPUReadbackRequest> callback)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_RequestAsyncReadback_4(src, mipIndex, callback, null);
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x0002CF0A File Offset: 0x0002B10A
		public void RequestAsyncReadback(Texture src, int mipIndex, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_RequestAsyncReadback_5(src, mipIndex, GraphicsFormatUtility.GetGraphicsFormat(dstFormat, QualitySettings.activeColorSpace == ColorSpace.Linear), callback, null);
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x0002CF33 File Offset: 0x0002B133
		public void RequestAsyncReadback(Texture src, int mipIndex, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_RequestAsyncReadback_5(src, mipIndex, dstFormat, callback, null);
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x0002CF50 File Offset: 0x0002B150
		public void RequestAsyncReadback(Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, Action<AsyncGPUReadbackRequest> callback)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_RequestAsyncReadback_6(src, mipIndex, x, width, y, height, z, depth, callback, null);
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x0002CF84 File Offset: 0x0002B184
		public void RequestAsyncReadback(Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_RequestAsyncReadback_7(src, mipIndex, x, width, y, height, z, depth, GraphicsFormatUtility.GetGraphicsFormat(dstFormat, QualitySettings.activeColorSpace == ColorSpace.Linear), callback, null);
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x0002CFC4 File Offset: 0x0002B1C4
		public void RequestAsyncReadback(Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_RequestAsyncReadback_7(src, mipIndex, x, width, y, height, z, depth, dstFormat, callback, null);
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x0002CFF8 File Offset: 0x0002B1F8
		public unsafe void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, ComputeBuffer src, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_1(src, callback, &asyncRequestNativeArrayData);
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x0002D02C File Offset: 0x0002B22C
		public unsafe void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, ComputeBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_2(src, size, offset, callback, &asyncRequestNativeArrayData);
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x0002D064 File Offset: 0x0002B264
		public unsafe void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, GraphicsBuffer src, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_8(src, callback, &asyncRequestNativeArrayData);
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x0002D098 File Offset: 0x0002B298
		public unsafe void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, GraphicsBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_9(src, size, offset, callback, &asyncRequestNativeArrayData);
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x0002D0D0 File Offset: 0x0002B2D0
		public unsafe void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_3(src, callback, &asyncRequestNativeArrayData);
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x0002D104 File Offset: 0x0002B304
		public unsafe void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_4(src, mipIndex, callback, &asyncRequestNativeArrayData);
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x0002D138 File Offset: 0x0002B338
		public unsafe void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_5(src, mipIndex, GraphicsFormatUtility.GetGraphicsFormat(dstFormat, QualitySettings.activeColorSpace == ColorSpace.Linear), callback, &asyncRequestNativeArrayData);
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x0002D17C File Offset: 0x0002B37C
		public unsafe void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_5(src, mipIndex, dstFormat, callback, &asyncRequestNativeArrayData);
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x0002D1B4 File Offset: 0x0002B3B4
		public unsafe void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_6(src, mipIndex, x, width, y, height, z, depth, callback, &asyncRequestNativeArrayData);
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x0002D1F4 File Offset: 0x0002B3F4
		public unsafe void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_7(src, mipIndex, x, width, y, height, z, depth, GraphicsFormatUtility.GetGraphicsFormat(dstFormat, QualitySettings.activeColorSpace == ColorSpace.Linear), callback, &asyncRequestNativeArrayData);
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x0002D244 File Offset: 0x0002B444
		public unsafe void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_7(src, mipIndex, x, width, y, height, z, depth, dstFormat, callback, &asyncRequestNativeArrayData);
		}

		// Token: 0x06001B7D RID: 7037
		[NativeMethod("AddRequestAsyncReadback")]
		[MethodImpl(4096)]
		private unsafe extern void Internal_RequestAsyncReadback_1([NotNull] ComputeBuffer src, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null);

		// Token: 0x06001B7E RID: 7038
		[NativeMethod("AddRequestAsyncReadback")]
		[MethodImpl(4096)]
		private unsafe extern void Internal_RequestAsyncReadback_2([NotNull] ComputeBuffer src, int size, int offset, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null);

		// Token: 0x06001B7F RID: 7039
		[NativeMethod("AddRequestAsyncReadback")]
		[MethodImpl(4096)]
		private unsafe extern void Internal_RequestAsyncReadback_3([NotNull] Texture src, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null);

		// Token: 0x06001B80 RID: 7040
		[NativeMethod("AddRequestAsyncReadback")]
		[MethodImpl(4096)]
		private unsafe extern void Internal_RequestAsyncReadback_4([NotNull] Texture src, int mipIndex, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null);

		// Token: 0x06001B81 RID: 7041
		[NativeMethod("AddRequestAsyncReadback")]
		[MethodImpl(4096)]
		private unsafe extern void Internal_RequestAsyncReadback_5([NotNull] Texture src, int mipIndex, GraphicsFormat dstFormat, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null);

		// Token: 0x06001B82 RID: 7042
		[NativeMethod("AddRequestAsyncReadback")]
		[MethodImpl(4096)]
		private unsafe extern void Internal_RequestAsyncReadback_6([NotNull] Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null);

		// Token: 0x06001B83 RID: 7043
		[NativeMethod("AddRequestAsyncReadback")]
		[MethodImpl(4096)]
		private unsafe extern void Internal_RequestAsyncReadback_7([NotNull] Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, GraphicsFormat dstFormat, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null);

		// Token: 0x06001B84 RID: 7044
		[NativeMethod("AddRequestAsyncReadback")]
		[MethodImpl(4096)]
		private unsafe extern void Internal_RequestAsyncReadback_8([NotNull] GraphicsBuffer src, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null);

		// Token: 0x06001B85 RID: 7045
		[NativeMethod("AddRequestAsyncReadback")]
		[MethodImpl(4096)]
		private unsafe extern void Internal_RequestAsyncReadback_9([NotNull] GraphicsBuffer src, int size, int offset, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null);

		// Token: 0x06001B86 RID: 7046
		[NativeMethod("AddSetInvertCulling")]
		[MethodImpl(4096)]
		public extern void SetInvertCulling(bool invertCulling);

		// Token: 0x06001B87 RID: 7047 RVA: 0x0002D285 File Offset: 0x0002B485
		private void ConvertTexture_Internal(RenderTargetIdentifier src, int srcElement, RenderTargetIdentifier dst, int dstElement)
		{
			this.ConvertTexture_Internal_Injected(ref src, srcElement, ref dst, dstElement);
		}

		// Token: 0x06001B88 RID: 7048
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetSinglePassStereo", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Internal_SetSinglePassStereo(SinglePassStereoMode mode);

		// Token: 0x06001B89 RID: 7049
		[FreeFunction("RenderingCommandBuffer_Bindings::InitBuffer")]
		[MethodImpl(4096)]
		private static extern IntPtr InitBuffer();

		// Token: 0x06001B8A RID: 7050
		[FreeFunction("RenderingCommandBuffer_Bindings::CreateGPUFence_Internal", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern IntPtr CreateGPUFence_Internal(GraphicsFenceType fenceType, SynchronisationStageFlags stage);

		// Token: 0x06001B8B RID: 7051
		[FreeFunction("RenderingCommandBuffer_Bindings::WaitOnGPUFence_Internal", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void WaitOnGPUFence_Internal(IntPtr fencePtr, SynchronisationStageFlags stage);

		// Token: 0x06001B8C RID: 7052
		[FreeFunction("RenderingCommandBuffer_Bindings::ReleaseBuffer", HasExplicitThis = true, IsThreadSafe = true)]
		[MethodImpl(4096)]
		private extern void ReleaseBuffer();

		// Token: 0x06001B8D RID: 7053
		[FreeFunction("RenderingCommandBuffer_Bindings::SetComputeFloatParam", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetComputeFloatParam([NotNull] ComputeShader computeShader, int nameID, float val);

		// Token: 0x06001B8E RID: 7054
		[FreeFunction("RenderingCommandBuffer_Bindings::SetComputeIntParam", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetComputeIntParam([NotNull] ComputeShader computeShader, int nameID, int val);

		// Token: 0x06001B8F RID: 7055 RVA: 0x0002D294 File Offset: 0x0002B494
		[FreeFunction("RenderingCommandBuffer_Bindings::SetComputeVectorParam", HasExplicitThis = true)]
		public void SetComputeVectorParam([NotNull] ComputeShader computeShader, int nameID, Vector4 val)
		{
			this.SetComputeVectorParam_Injected(computeShader, nameID, ref val);
		}

		// Token: 0x06001B90 RID: 7056
		[FreeFunction("RenderingCommandBuffer_Bindings::SetComputeVectorArrayParam", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetComputeVectorArrayParam([NotNull] ComputeShader computeShader, int nameID, Vector4[] values);

		// Token: 0x06001B91 RID: 7057 RVA: 0x0002D2A0 File Offset: 0x0002B4A0
		[FreeFunction("RenderingCommandBuffer_Bindings::SetComputeMatrixParam", HasExplicitThis = true)]
		public void SetComputeMatrixParam([NotNull] ComputeShader computeShader, int nameID, Matrix4x4 val)
		{
			this.SetComputeMatrixParam_Injected(computeShader, nameID, ref val);
		}

		// Token: 0x06001B92 RID: 7058
		[FreeFunction("RenderingCommandBuffer_Bindings::SetComputeMatrixArrayParam", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetComputeMatrixArrayParam([NotNull] ComputeShader computeShader, int nameID, Matrix4x4[] values);

		// Token: 0x06001B93 RID: 7059
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetComputeFloats", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Internal_SetComputeFloats([NotNull] ComputeShader computeShader, int nameID, float[] values);

		// Token: 0x06001B94 RID: 7060
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetComputeInts", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Internal_SetComputeInts([NotNull] ComputeShader computeShader, int nameID, int[] values);

		// Token: 0x06001B95 RID: 7061
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetComputeTextureParam", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Internal_SetComputeTextureParam([NotNull] ComputeShader computeShader, int kernelIndex, int nameID, ref RenderTargetIdentifier rt, int mipLevel, RenderTextureSubElement element);

		// Token: 0x06001B96 RID: 7062
		[FreeFunction("RenderingCommandBuffer_Bindings::SetComputeBufferParam", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Internal_SetComputeBufferParam([NotNull] ComputeShader computeShader, int kernelIndex, int nameID, ComputeBuffer buffer);

		// Token: 0x06001B97 RID: 7063
		[FreeFunction("RenderingCommandBuffer_Bindings::SetComputeGraphicsBufferParam", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Internal_SetComputeGraphicsBufferParam([NotNull] ComputeShader computeShader, int kernelIndex, int nameID, GraphicsBuffer buffer);

		// Token: 0x06001B98 RID: 7064
		[FreeFunction("RenderingCommandBuffer_Bindings::SetComputeConstantBufferParam", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetComputeConstantBufferParam([NotNull] ComputeShader computeShader, int nameID, ComputeBuffer buffer, int offset, int size);

		// Token: 0x06001B99 RID: 7065
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DispatchCompute", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern void Internal_DispatchCompute([NotNull] ComputeShader computeShader, int kernelIndex, int threadGroupsX, int threadGroupsY, int threadGroupsZ);

		// Token: 0x06001B9A RID: 7066
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DispatchComputeIndirect", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern void Internal_DispatchComputeIndirect([NotNull] ComputeShader computeShader, int kernelIndex, ComputeBuffer indirectBuffer, uint argsOffset);

		// Token: 0x06001B9B RID: 7067
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DispatchComputeIndirect", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern void Internal_DispatchComputeIndirectGraphicsBuffer([NotNull] ComputeShader computeShader, int kernelIndex, GraphicsBuffer indirectBuffer, uint argsOffset);

		// Token: 0x06001B9C RID: 7068
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingBufferParam", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Internal_SetRayTracingBufferParam([NotNull] RayTracingShader rayTracingShader, int nameID, ComputeBuffer buffer);

		// Token: 0x06001B9D RID: 7069
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingTextureParam", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Internal_SetRayTracingTextureParam([NotNull] RayTracingShader rayTracingShader, int nameID, ref RenderTargetIdentifier rt);

		// Token: 0x06001B9E RID: 7070
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingFloatParam", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Internal_SetRayTracingFloatParam([NotNull] RayTracingShader rayTracingShader, int nameID, float val);

		// Token: 0x06001B9F RID: 7071
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingIntParam", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Internal_SetRayTracingIntParam([NotNull] RayTracingShader rayTracingShader, int nameID, int val);

		// Token: 0x06001BA0 RID: 7072 RVA: 0x0002D2AC File Offset: 0x0002B4AC
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingVectorParam", HasExplicitThis = true)]
		private void Internal_SetRayTracingVectorParam([NotNull] RayTracingShader rayTracingShader, int nameID, Vector4 val)
		{
			this.Internal_SetRayTracingVectorParam_Injected(rayTracingShader, nameID, ref val);
		}

		// Token: 0x06001BA1 RID: 7073
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingVectorArrayParam", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Internal_SetRayTracingVectorArrayParam([NotNull] RayTracingShader rayTracingShader, int nameID, Vector4[] values);

		// Token: 0x06001BA2 RID: 7074 RVA: 0x0002D2B8 File Offset: 0x0002B4B8
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingMatrixParam", HasExplicitThis = true)]
		private void Internal_SetRayTracingMatrixParam([NotNull] RayTracingShader rayTracingShader, int nameID, Matrix4x4 val)
		{
			this.Internal_SetRayTracingMatrixParam_Injected(rayTracingShader, nameID, ref val);
		}

		// Token: 0x06001BA3 RID: 7075
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingMatrixArrayParam", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Internal_SetRayTracingMatrixArrayParam([NotNull] RayTracingShader rayTracingShader, int nameID, Matrix4x4[] values);

		// Token: 0x06001BA4 RID: 7076
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingFloats", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Internal_SetRayTracingFloats([NotNull] RayTracingShader rayTracingShader, int nameID, float[] values);

		// Token: 0x06001BA5 RID: 7077
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingInts", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Internal_SetRayTracingInts([NotNull] RayTracingShader rayTracingShader, int nameID, int[] values);

		// Token: 0x06001BA6 RID: 7078
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_BuildRayTracingAccelerationStructure", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Internal_BuildRayTracingAccelerationStructure([NotNull] RayTracingAccelerationStructure accelerationStructure);

		// Token: 0x06001BA7 RID: 7079
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingAccelerationStructure", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Internal_SetRayTracingAccelerationStructure([NotNull] RayTracingShader rayTracingShader, int nameID, RayTracingAccelerationStructure accelerationStructure);

		// Token: 0x06001BA8 RID: 7080
		[NativeMethod("AddSetRayTracingShaderPass")]
		[MethodImpl(4096)]
		public extern void SetRayTracingShaderPass([NotNull] RayTracingShader rayTracingShader, string passName);

		// Token: 0x06001BA9 RID: 7081
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DispatchRays", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern void Internal_DispatchRays([NotNull] RayTracingShader rayTracingShader, string rayGenShaderName, uint width, uint height, uint depth, Camera camera = null);

		// Token: 0x06001BAA RID: 7082
		[NativeMethod("AddGenerateMips")]
		[MethodImpl(4096)]
		private extern void Internal_GenerateMips(RenderTexture rt);

		// Token: 0x06001BAB RID: 7083
		[NativeMethod("AddResolveAntiAliasedSurface")]
		[MethodImpl(4096)]
		private extern void Internal_ResolveAntiAliasedSurface(RenderTexture rt, RenderTexture target);

		// Token: 0x06001BAC RID: 7084
		[NativeMethod("AddCopyCounterValue")]
		[MethodImpl(4096)]
		private extern void CopyCounterValueCC(ComputeBuffer src, ComputeBuffer dst, uint dstOffsetBytes);

		// Token: 0x06001BAD RID: 7085
		[NativeMethod("AddCopyCounterValue")]
		[MethodImpl(4096)]
		private extern void CopyCounterValueGC(GraphicsBuffer src, ComputeBuffer dst, uint dstOffsetBytes);

		// Token: 0x06001BAE RID: 7086
		[NativeMethod("AddCopyCounterValue")]
		[MethodImpl(4096)]
		private extern void CopyCounterValueCG(ComputeBuffer src, GraphicsBuffer dst, uint dstOffsetBytes);

		// Token: 0x06001BAF RID: 7087
		[NativeMethod("AddCopyCounterValue")]
		[MethodImpl(4096)]
		private extern void CopyCounterValueGG(GraphicsBuffer src, GraphicsBuffer dst, uint dstOffsetBytes);

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001BB0 RID: 7088
		// (set) Token: 0x06001BB1 RID: 7089
		public extern string name
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001BB2 RID: 7090
		public extern int sizeInBytes
		{
			[NativeMethod("GetBufferSize")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06001BB3 RID: 7091
		[NativeMethod("ClearCommands")]
		[MethodImpl(4096)]
		public extern void Clear();

		// Token: 0x06001BB4 RID: 7092 RVA: 0x0002D2C4 File Offset: 0x0002B4C4
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawMesh", HasExplicitThis = true)]
		private void Internal_DrawMesh([NotNull] Mesh mesh, Matrix4x4 matrix, Material material, int submeshIndex, int shaderPass, MaterialPropertyBlock properties)
		{
			this.Internal_DrawMesh_Injected(mesh, ref matrix, material, submeshIndex, shaderPass, properties);
		}

		// Token: 0x06001BB5 RID: 7093
		[NativeMethod("AddDrawRenderer")]
		[MethodImpl(4096)]
		private extern void Internal_DrawRenderer([NotNull] Renderer renderer, Material material, int submeshIndex, int shaderPass);

		// Token: 0x06001BB6 RID: 7094 RVA: 0x0002D2D6 File Offset: 0x0002B4D6
		private void Internal_DrawRenderer(Renderer renderer, Material material, int submeshIndex)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_DrawRenderer(renderer, material, submeshIndex, -1);
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x0002D2ED File Offset: 0x0002B4ED
		private void Internal_DrawRenderer(Renderer renderer, Material material)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_DrawRenderer(renderer, material, 0);
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x0002D303 File Offset: 0x0002B503
		[NativeMethod("AddDrawProcedural")]
		private void Internal_DrawProcedural(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int vertexCount, int instanceCount, MaterialPropertyBlock properties)
		{
			this.Internal_DrawProcedural_Injected(ref matrix, material, shaderPass, topology, vertexCount, instanceCount, properties);
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x0002D318 File Offset: 0x0002B518
		[NativeMethod("AddDrawProceduralIndexed")]
		private void Internal_DrawProceduralIndexed(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int indexCount, int instanceCount, MaterialPropertyBlock properties)
		{
			this.Internal_DrawProceduralIndexed_Injected(indexBuffer, ref matrix, material, shaderPass, topology, indexCount, instanceCount, properties);
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x0002D339 File Offset: 0x0002B539
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawProceduralIndirect", HasExplicitThis = true)]
		private void Internal_DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			this.Internal_DrawProceduralIndirect_Injected(ref matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, properties);
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x0002D350 File Offset: 0x0002B550
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawProceduralIndexedIndirect", HasExplicitThis = true)]
		private void Internal_DrawProceduralIndexedIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			this.Internal_DrawProceduralIndexedIndirect_Injected(indexBuffer, ref matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, properties);
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x0002D371 File Offset: 0x0002B571
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawProceduralIndirect", HasExplicitThis = true)]
		private void Internal_DrawProceduralIndirectGraphicsBuffer(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			this.Internal_DrawProceduralIndirectGraphicsBuffer_Injected(ref matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, properties);
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x0002D388 File Offset: 0x0002B588
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawProceduralIndexedIndirect", HasExplicitThis = true)]
		private void Internal_DrawProceduralIndexedIndirectGraphicsBuffer(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			this.Internal_DrawProceduralIndexedIndirectGraphicsBuffer_Injected(indexBuffer, ref matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, properties);
		}

		// Token: 0x06001BBE RID: 7102
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawMeshInstanced", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Internal_DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, int shaderPass, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties);

		// Token: 0x06001BBF RID: 7103
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawMeshInstancedProcedural", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Internal_DrawMeshInstancedProcedural(Mesh mesh, int submeshIndex, Material material, int shaderPass, int count, MaterialPropertyBlock properties);

		// Token: 0x06001BC0 RID: 7104
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawMeshInstancedIndirect", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Internal_DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties);

		// Token: 0x06001BC1 RID: 7105
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawMeshInstancedIndirect", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Internal_DrawMeshInstancedIndirectGraphicsBuffer(Mesh mesh, int submeshIndex, Material material, int shaderPass, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties);

		// Token: 0x06001BC2 RID: 7106 RVA: 0x0002D3A9 File Offset: 0x0002B5A9
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawOcclusionMesh", HasExplicitThis = true)]
		private void Internal_DrawOcclusionMesh(RectInt normalizedCamViewport)
		{
			this.Internal_DrawOcclusionMesh_Injected(ref normalizedCamViewport);
		}

		// Token: 0x06001BC3 RID: 7107
		[FreeFunction("RenderingCommandBuffer_Bindings::SetRandomWriteTarget_Texture", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern void SetRandomWriteTarget_Texture(int index, ref RenderTargetIdentifier rt);

		// Token: 0x06001BC4 RID: 7108
		[FreeFunction("RenderingCommandBuffer_Bindings::SetRandomWriteTarget_Buffer", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern void SetRandomWriteTarget_Buffer(int index, ComputeBuffer uav, bool preserveCounterValue);

		// Token: 0x06001BC5 RID: 7109
		[FreeFunction("RenderingCommandBuffer_Bindings::SetRandomWriteTarget_Buffer", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern void SetRandomWriteTarget_GraphicsBuffer(int index, GraphicsBuffer uav, bool preserveCounterValue);

		// Token: 0x06001BC6 RID: 7110
		[FreeFunction("RenderingCommandBuffer_Bindings::ClearRandomWriteTargets", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern void ClearRandomWriteTargets();

		// Token: 0x06001BC7 RID: 7111 RVA: 0x0002D3B3 File Offset: 0x0002B5B3
		[FreeFunction("RenderingCommandBuffer_Bindings::SetViewport", HasExplicitThis = true, ThrowsException = true)]
		public void SetViewport(Rect pixelRect)
		{
			this.SetViewport_Injected(ref pixelRect);
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x0002D3BD File Offset: 0x0002B5BD
		[FreeFunction("RenderingCommandBuffer_Bindings::EnableScissorRect", HasExplicitThis = true, ThrowsException = true)]
		public void EnableScissorRect(Rect scissor)
		{
			this.EnableScissorRect_Injected(ref scissor);
		}

		// Token: 0x06001BC9 RID: 7113
		[FreeFunction("RenderingCommandBuffer_Bindings::DisableScissorRect", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern void DisableScissorRect();

		// Token: 0x06001BCA RID: 7114
		[FreeFunction("RenderingCommandBuffer_Bindings::CopyTexture_Internal", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void CopyTexture_Internal(ref RenderTargetIdentifier src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, ref RenderTargetIdentifier dst, int dstElement, int dstMip, int dstX, int dstY, int mode);

		// Token: 0x06001BCB RID: 7115 RVA: 0x0002D3C8 File Offset: 0x0002B5C8
		[FreeFunction("RenderingCommandBuffer_Bindings::Blit_Texture", HasExplicitThis = true)]
		private void Blit_Texture(Texture source, ref RenderTargetIdentifier dest, Material mat, int pass, Vector2 scale, Vector2 offset, int sourceDepthSlice, int destDepthSlice)
		{
			this.Blit_Texture_Injected(source, ref dest, mat, pass, ref scale, ref offset, sourceDepthSlice, destDepthSlice);
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x0002D3E8 File Offset: 0x0002B5E8
		[FreeFunction("RenderingCommandBuffer_Bindings::Blit_Identifier", HasExplicitThis = true)]
		private void Blit_Identifier(ref RenderTargetIdentifier source, ref RenderTargetIdentifier dest, Material mat, int pass, Vector2 scale, Vector2 offset, int sourceDepthSlice, int destDepthSlice)
		{
			this.Blit_Identifier_Injected(ref source, ref dest, mat, pass, ref scale, ref offset, sourceDepthSlice, destDepthSlice);
		}

		// Token: 0x06001BCD RID: 7117
		[FreeFunction("RenderingCommandBuffer_Bindings::GetTemporaryRT", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter, GraphicsFormat format, int antiAliasing, bool enableRandomWrite, RenderTextureMemoryless memorylessMode, bool useDynamicScale);

		// Token: 0x06001BCE RID: 7118 RVA: 0x0002D408 File Offset: 0x0002B608
		public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter, GraphicsFormat format, int antiAliasing, bool enableRandomWrite, RenderTextureMemoryless memorylessMode)
		{
			this.GetTemporaryRT(nameID, width, height, depthBuffer, filter, format, antiAliasing, enableRandomWrite, memorylessMode, false);
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x0002D430 File Offset: 0x0002B630
		public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter, GraphicsFormat format, int antiAliasing, bool enableRandomWrite)
		{
			this.GetTemporaryRT(nameID, width, height, depthBuffer, filter, format, antiAliasing, enableRandomWrite, RenderTextureMemoryless.None);
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x0002D454 File Offset: 0x0002B654
		public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter, GraphicsFormat format, int antiAliasing)
		{
			this.GetTemporaryRT(nameID, width, height, depthBuffer, filter, format, antiAliasing, false, RenderTextureMemoryless.None);
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x0002D476 File Offset: 0x0002B676
		public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter, GraphicsFormat format)
		{
			this.GetTemporaryRT(nameID, width, height, depthBuffer, filter, format, 1);
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x0002D48C File Offset: 0x0002B68C
		public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing, bool enableRandomWrite, RenderTextureMemoryless memorylessMode, bool useDynamicScale)
		{
			this.GetTemporaryRT(nameID, width, height, depthBuffer, filter, GraphicsFormatUtility.GetGraphicsFormat(format, readWrite), antiAliasing, enableRandomWrite, memorylessMode, useDynamicScale);
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x0002D4BC File Offset: 0x0002B6BC
		public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing, bool enableRandomWrite, RenderTextureMemoryless memorylessMode)
		{
			this.GetTemporaryRT(nameID, width, height, depthBuffer, filter, format, readWrite, antiAliasing, enableRandomWrite, memorylessMode, false);
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x0002D4E4 File Offset: 0x0002B6E4
		public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing, bool enableRandomWrite)
		{
			this.GetTemporaryRT(nameID, width, height, depthBuffer, filter, format, readWrite, antiAliasing, enableRandomWrite, RenderTextureMemoryless.None);
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x0002D50C File Offset: 0x0002B70C
		public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing)
		{
			this.GetTemporaryRT(nameID, width, height, depthBuffer, filter, format, readWrite, antiAliasing, false);
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x0002D530 File Offset: 0x0002B730
		public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter, RenderTextureFormat format, RenderTextureReadWrite readWrite)
		{
			this.GetTemporaryRT(nameID, width, height, depthBuffer, filter, format, readWrite, 1);
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x0002D551 File Offset: 0x0002B751
		public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter, RenderTextureFormat format)
		{
			this.GetTemporaryRT(nameID, width, height, depthBuffer, filter, GraphicsFormatUtility.GetGraphicsFormat(format, RenderTextureReadWrite.Default));
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x0002D56A File Offset: 0x0002B76A
		public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter)
		{
			this.GetTemporaryRT(nameID, width, height, depthBuffer, filter, SystemInfo.GetGraphicsFormat(DefaultFormat.LDR));
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x0002D581 File Offset: 0x0002B781
		public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer)
		{
			this.GetTemporaryRT(nameID, width, height, depthBuffer, FilterMode.Point);
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x0002D591 File Offset: 0x0002B791
		public void GetTemporaryRT(int nameID, int width, int height)
		{
			this.GetTemporaryRT(nameID, width, height, 0);
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x0002D59F File Offset: 0x0002B79F
		[FreeFunction("RenderingCommandBuffer_Bindings::GetTemporaryRTWithDescriptor", HasExplicitThis = true)]
		private void GetTemporaryRTWithDescriptor(int nameID, RenderTextureDescriptor desc, FilterMode filter)
		{
			this.GetTemporaryRTWithDescriptor_Injected(nameID, ref desc, filter);
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x0002D5AB File Offset: 0x0002B7AB
		public void GetTemporaryRT(int nameID, RenderTextureDescriptor desc, FilterMode filter)
		{
			this.GetTemporaryRTWithDescriptor(nameID, desc, filter);
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x0002D5B8 File Offset: 0x0002B7B8
		public void GetTemporaryRT(int nameID, RenderTextureDescriptor desc)
		{
			this.GetTemporaryRT(nameID, desc, FilterMode.Point);
		}

		// Token: 0x06001BDE RID: 7134
		[FreeFunction("RenderingCommandBuffer_Bindings::GetTemporaryRTArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void GetTemporaryRTArray(int nameID, int width, int height, int slices, int depthBuffer, FilterMode filter, GraphicsFormat format, int antiAliasing, bool enableRandomWrite, bool useDynamicScale);

		// Token: 0x06001BDF RID: 7135 RVA: 0x0002D5C8 File Offset: 0x0002B7C8
		public void GetTemporaryRTArray(int nameID, int width, int height, int slices, int depthBuffer, FilterMode filter, GraphicsFormat format, int antiAliasing, bool enableRandomWrite)
		{
			this.GetTemporaryRTArray(nameID, width, height, slices, depthBuffer, filter, format, antiAliasing, enableRandomWrite, false);
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x0002D5F0 File Offset: 0x0002B7F0
		public void GetTemporaryRTArray(int nameID, int width, int height, int slices, int depthBuffer, FilterMode filter, GraphicsFormat format, int antiAliasing)
		{
			this.GetTemporaryRTArray(nameID, width, height, slices, depthBuffer, filter, format, antiAliasing, false);
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x0002D614 File Offset: 0x0002B814
		public void GetTemporaryRTArray(int nameID, int width, int height, int slices, int depthBuffer, FilterMode filter, GraphicsFormat format)
		{
			this.GetTemporaryRTArray(nameID, width, height, slices, depthBuffer, filter, format, 1);
		}

		// Token: 0x06001BE2 RID: 7138 RVA: 0x0002D638 File Offset: 0x0002B838
		public void GetTemporaryRTArray(int nameID, int width, int height, int slices, int depthBuffer, FilterMode filter, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing, bool enableRandomWrite)
		{
			this.GetTemporaryRTArray(nameID, width, height, slices, depthBuffer, filter, GraphicsFormatUtility.GetGraphicsFormat(format, readWrite), antiAliasing, enableRandomWrite, false);
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x0002D664 File Offset: 0x0002B864
		public void GetTemporaryRTArray(int nameID, int width, int height, int slices, int depthBuffer, FilterMode filter, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing)
		{
			this.GetTemporaryRTArray(nameID, width, height, slices, depthBuffer, filter, GraphicsFormatUtility.GetGraphicsFormat(format, readWrite), antiAliasing, false);
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x0002D690 File Offset: 0x0002B890
		public void GetTemporaryRTArray(int nameID, int width, int height, int slices, int depthBuffer, FilterMode filter, RenderTextureFormat format, RenderTextureReadWrite readWrite)
		{
			this.GetTemporaryRTArray(nameID, width, height, slices, depthBuffer, filter, GraphicsFormatUtility.GetGraphicsFormat(format, readWrite), 1, false);
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x0002D6BC File Offset: 0x0002B8BC
		public void GetTemporaryRTArray(int nameID, int width, int height, int slices, int depthBuffer, FilterMode filter, RenderTextureFormat format)
		{
			this.GetTemporaryRTArray(nameID, width, height, slices, depthBuffer, filter, GraphicsFormatUtility.GetGraphicsFormat(format, RenderTextureReadWrite.Default), 1, false);
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x0002D6E4 File Offset: 0x0002B8E4
		public void GetTemporaryRTArray(int nameID, int width, int height, int slices, int depthBuffer, FilterMode filter)
		{
			this.GetTemporaryRTArray(nameID, width, height, slices, depthBuffer, filter, SystemInfo.GetGraphicsFormat(DefaultFormat.LDR), 1, false);
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x0002D70A File Offset: 0x0002B90A
		public void GetTemporaryRTArray(int nameID, int width, int height, int slices, int depthBuffer)
		{
			this.GetTemporaryRTArray(nameID, width, height, slices, depthBuffer, FilterMode.Point);
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x0002D71C File Offset: 0x0002B91C
		public void GetTemporaryRTArray(int nameID, int width, int height, int slices)
		{
			this.GetTemporaryRTArray(nameID, width, height, slices, 0);
		}

		// Token: 0x06001BE9 RID: 7145
		[FreeFunction("RenderingCommandBuffer_Bindings::ReleaseTemporaryRT", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void ReleaseTemporaryRT(int nameID);

		// Token: 0x06001BEA RID: 7146 RVA: 0x0002D72C File Offset: 0x0002B92C
		[FreeFunction("RenderingCommandBuffer_Bindings::ClearRenderTarget", HasExplicitThis = true)]
		public void ClearRenderTarget(bool clearDepth, bool clearColor, Color backgroundColor, float depth)
		{
			this.ClearRenderTarget_Injected(clearDepth, clearColor, ref backgroundColor, depth);
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x0002D73A File Offset: 0x0002B93A
		public void ClearRenderTarget(bool clearDepth, bool clearColor, Color backgroundColor)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.ClearRenderTarget(clearDepth, clearColor, backgroundColor, 1f);
		}

		// Token: 0x06001BEC RID: 7148
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalFloat", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetGlobalFloat(int nameID, float value);

		// Token: 0x06001BED RID: 7149
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalInt", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetGlobalInt(int nameID, int value);

		// Token: 0x06001BEE RID: 7150 RVA: 0x0002D755 File Offset: 0x0002B955
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalVector", HasExplicitThis = true)]
		public void SetGlobalVector(int nameID, Vector4 value)
		{
			this.SetGlobalVector_Injected(nameID, ref value);
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x0002D760 File Offset: 0x0002B960
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalColor", HasExplicitThis = true)]
		public void SetGlobalColor(int nameID, Color value)
		{
			this.SetGlobalColor_Injected(nameID, ref value);
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x0002D76B File Offset: 0x0002B96B
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalMatrix", HasExplicitThis = true)]
		public void SetGlobalMatrix(int nameID, Matrix4x4 value)
		{
			this.SetGlobalMatrix_Injected(nameID, ref value);
		}

		// Token: 0x06001BF1 RID: 7153
		[FreeFunction("RenderingCommandBuffer_Bindings::EnableShaderKeyword", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void EnableShaderKeyword(string keyword);

		// Token: 0x06001BF2 RID: 7154
		[FreeFunction("RenderingCommandBuffer_Bindings::DisableShaderKeyword", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void DisableShaderKeyword(string keyword);

		// Token: 0x06001BF3 RID: 7155 RVA: 0x0002D776 File Offset: 0x0002B976
		[FreeFunction("RenderingCommandBuffer_Bindings::SetViewMatrix", HasExplicitThis = true, ThrowsException = true)]
		public void SetViewMatrix(Matrix4x4 view)
		{
			this.SetViewMatrix_Injected(ref view);
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x0002D780 File Offset: 0x0002B980
		[FreeFunction("RenderingCommandBuffer_Bindings::SetProjectionMatrix", HasExplicitThis = true, ThrowsException = true)]
		public void SetProjectionMatrix(Matrix4x4 proj)
		{
			this.SetProjectionMatrix_Injected(ref proj);
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x0002D78A File Offset: 0x0002B98A
		[FreeFunction("RenderingCommandBuffer_Bindings::SetViewProjectionMatrices", HasExplicitThis = true, ThrowsException = true)]
		public void SetViewProjectionMatrices(Matrix4x4 view, Matrix4x4 proj)
		{
			this.SetViewProjectionMatrices_Injected(ref view, ref proj);
		}

		// Token: 0x06001BF6 RID: 7158
		[NativeMethod("AddSetGlobalDepthBias")]
		[MethodImpl(4096)]
		public extern void SetGlobalDepthBias(float bias, float slopeBias);

		// Token: 0x06001BF7 RID: 7159
		[FreeFunction("RenderingCommandBuffer_Bindings::SetExecutionFlags", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern void SetExecutionFlags(CommandBufferExecutionFlags flags);

		// Token: 0x06001BF8 RID: 7160
		[FreeFunction("RenderingCommandBuffer_Bindings::ValidateAgainstExecutionFlags", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern bool ValidateAgainstExecutionFlags(CommandBufferExecutionFlags requiredFlags, CommandBufferExecutionFlags invalidFlags);

		// Token: 0x06001BF9 RID: 7161
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalFloatArrayListImpl", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetGlobalFloatArrayListImpl(int nameID, object values);

		// Token: 0x06001BFA RID: 7162
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalVectorArrayListImpl", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetGlobalVectorArrayListImpl(int nameID, object values);

		// Token: 0x06001BFB RID: 7163
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalMatrixArrayListImpl", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetGlobalMatrixArrayListImpl(int nameID, object values);

		// Token: 0x06001BFC RID: 7164
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalFloatArray", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern void SetGlobalFloatArray(int nameID, float[] values);

		// Token: 0x06001BFD RID: 7165
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalVectorArray", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern void SetGlobalVectorArray(int nameID, Vector4[] values);

		// Token: 0x06001BFE RID: 7166
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalMatrixArray", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern void SetGlobalMatrixArray(int nameID, Matrix4x4[] values);

		// Token: 0x06001BFF RID: 7167
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalTexture_Impl", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetGlobalTexture_Impl(int nameID, ref RenderTargetIdentifier rt, RenderTextureSubElement element);

		// Token: 0x06001C00 RID: 7168
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalBuffer", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetGlobalBufferInternal(int nameID, ComputeBuffer value);

		// Token: 0x06001C01 RID: 7169
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalBuffer", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetGlobalGraphicsBufferInternal(int nameID, GraphicsBuffer value);

		// Token: 0x06001C02 RID: 7170
		[FreeFunction("RenderingCommandBuffer_Bindings::SetShadowSamplingMode_Impl", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetShadowSamplingMode_Impl(ref RenderTargetIdentifier shadowmap, ShadowSamplingMode mode);

		// Token: 0x06001C03 RID: 7171
		[FreeFunction("RenderingCommandBuffer_Bindings::IssuePluginEventInternal", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void IssuePluginEventInternal(IntPtr callback, int eventID);

		// Token: 0x06001C04 RID: 7172
		[FreeFunction("RenderingCommandBuffer_Bindings::BeginSample", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void BeginSample(string name);

		// Token: 0x06001C05 RID: 7173
		[FreeFunction("RenderingCommandBuffer_Bindings::EndSample", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void EndSample(string name);

		// Token: 0x06001C06 RID: 7174 RVA: 0x0002D796 File Offset: 0x0002B996
		public void BeginSample(CustomSampler sampler)
		{
			this.BeginSample_CustomSampler(sampler);
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x0002D7A1 File Offset: 0x0002B9A1
		public void EndSample(CustomSampler sampler)
		{
			this.EndSample_CustomSampler(sampler);
		}

		// Token: 0x06001C08 RID: 7176
		[FreeFunction("RenderingCommandBuffer_Bindings::BeginSample_CustomSampler", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void BeginSample_CustomSampler([NotNull] CustomSampler sampler);

		// Token: 0x06001C09 RID: 7177
		[FreeFunction("RenderingCommandBuffer_Bindings::EndSample_CustomSampler", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void EndSample_CustomSampler([NotNull] CustomSampler sampler);

		// Token: 0x06001C0A RID: 7178
		[FreeFunction("RenderingCommandBuffer_Bindings::IssuePluginEventAndDataInternal", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void IssuePluginEventAndDataInternal(IntPtr callback, int eventID, IntPtr data);

		// Token: 0x06001C0B RID: 7179
		[FreeFunction("RenderingCommandBuffer_Bindings::IssuePluginCustomBlitInternal", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void IssuePluginCustomBlitInternal(IntPtr callback, uint command, ref RenderTargetIdentifier source, ref RenderTargetIdentifier dest, uint commandParam, uint commandFlags);

		// Token: 0x06001C0C RID: 7180
		[FreeFunction("RenderingCommandBuffer_Bindings::IssuePluginCustomTextureUpdateInternal", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void IssuePluginCustomTextureUpdateInternal(IntPtr callback, Texture targetTexture, uint userData, bool useNewUnityRenderingExtTextureUpdateParamsV2);

		// Token: 0x06001C0D RID: 7181
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalConstantBuffer", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetGlobalConstantBufferInternal(ComputeBuffer buffer, int nameID, int offset, int size);

		// Token: 0x06001C0E RID: 7182
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalConstantBuffer", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetGlobalConstantGraphicsBufferInternal(GraphicsBuffer buffer, int nameID, int offset, int size);

		// Token: 0x06001C0F RID: 7183 RVA: 0x0002D7AC File Offset: 0x0002B9AC
		[FreeFunction("RenderingCommandBuffer_Bindings::IncrementUpdateCount", HasExplicitThis = true)]
		public void IncrementUpdateCount(RenderTargetIdentifier dest)
		{
			this.IncrementUpdateCount_Injected(ref dest);
		}

		// Token: 0x06001C10 RID: 7184
		[FreeFunction("RenderingCommandBuffer_Bindings::SetInstanceMultiplier", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetInstanceMultiplier(uint multiplier);

		// Token: 0x06001C11 RID: 7185 RVA: 0x0002D7B6 File Offset: 0x0002B9B6
		public void SetRenderTarget(RenderTargetIdentifier rt)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.SetRenderTargetSingle_Internal(rt, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x0002D7D0 File Offset: 0x0002B9D0
		public void SetRenderTarget(RenderTargetIdentifier rt, RenderBufferLoadAction loadAction, RenderBufferStoreAction storeAction)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = loadAction == RenderBufferLoadAction.Clear;
			if (flag)
			{
				throw new ArgumentException("RenderBufferLoadAction.Clear is not supported");
			}
			this.SetRenderTargetSingle_Internal(rt, loadAction, storeAction, loadAction, storeAction);
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x0002D808 File Offset: 0x0002BA08
		public void SetRenderTarget(RenderTargetIdentifier rt, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = colorLoadAction == RenderBufferLoadAction.Clear || depthLoadAction == RenderBufferLoadAction.Clear;
			if (flag)
			{
				throw new ArgumentException("RenderBufferLoadAction.Clear is not supported");
			}
			this.SetRenderTargetSingle_Internal(rt, colorLoadAction, colorStoreAction, depthLoadAction, depthStoreAction);
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x0002D848 File Offset: 0x0002BA48
		public void SetRenderTarget(RenderTargetIdentifier rt, int mipLevel)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = mipLevel < 0;
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid value for mipLevel ({0})", mipLevel));
			}
			this.SetRenderTargetSingle_Internal(new RenderTargetIdentifier(rt, mipLevel, CubemapFace.Unknown, 0), RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x0002D894 File Offset: 0x0002BA94
		public void SetRenderTarget(RenderTargetIdentifier rt, int mipLevel, CubemapFace cubemapFace)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = mipLevel < 0;
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid value for mipLevel ({0})", mipLevel));
			}
			this.SetRenderTargetSingle_Internal(new RenderTargetIdentifier(rt, mipLevel, cubemapFace, 0), RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x0002D8E0 File Offset: 0x0002BAE0
		public void SetRenderTarget(RenderTargetIdentifier rt, int mipLevel, CubemapFace cubemapFace, int depthSlice)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = depthSlice < -1;
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid value for depthSlice ({0})", depthSlice));
			}
			bool flag2 = mipLevel < 0;
			if (flag2)
			{
				throw new ArgumentException(string.Format("Invalid value for mipLevel ({0})", mipLevel));
			}
			this.SetRenderTargetSingle_Internal(new RenderTargetIdentifier(rt, mipLevel, cubemapFace, depthSlice), RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x0002D94A File Offset: 0x0002BB4A
		public void SetRenderTarget(RenderTargetIdentifier color, RenderTargetIdentifier depth)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.SetRenderTargetColorDepth_Internal(color, depth, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x0002D964 File Offset: 0x0002BB64
		public void SetRenderTarget(RenderTargetIdentifier color, RenderTargetIdentifier depth, int mipLevel)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = mipLevel < 0;
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid value for mipLevel ({0})", mipLevel));
			}
			this.SetRenderTargetColorDepth_Internal(new RenderTargetIdentifier(color, mipLevel, CubemapFace.Unknown, 0), depth, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x0002D9B0 File Offset: 0x0002BBB0
		public void SetRenderTarget(RenderTargetIdentifier color, RenderTargetIdentifier depth, int mipLevel, CubemapFace cubemapFace)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = mipLevel < 0;
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid value for mipLevel ({0})", mipLevel));
			}
			this.SetRenderTargetColorDepth_Internal(new RenderTargetIdentifier(color, mipLevel, cubemapFace, 0), depth, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x0002D9FC File Offset: 0x0002BBFC
		public void SetRenderTarget(RenderTargetIdentifier color, RenderTargetIdentifier depth, int mipLevel, CubemapFace cubemapFace, int depthSlice)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = depthSlice < -1;
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid value for depthSlice ({0})", depthSlice));
			}
			bool flag2 = mipLevel < 0;
			if (flag2)
			{
				throw new ArgumentException(string.Format("Invalid value for mipLevel ({0})", mipLevel));
			}
			this.SetRenderTargetColorDepth_Internal(new RenderTargetIdentifier(color, mipLevel, cubemapFace, depthSlice), depth, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x0002DA68 File Offset: 0x0002BC68
		public void SetRenderTarget(RenderTargetIdentifier color, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderTargetIdentifier depth, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = colorLoadAction == RenderBufferLoadAction.Clear || depthLoadAction == RenderBufferLoadAction.Clear;
			if (flag)
			{
				throw new ArgumentException("RenderBufferLoadAction.Clear is not supported");
			}
			this.SetRenderTargetColorDepth_Internal(color, depth, colorLoadAction, colorStoreAction, depthLoadAction, depthStoreAction);
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x0002DAAC File Offset: 0x0002BCAC
		public void SetRenderTarget(RenderTargetIdentifier[] colors, RenderTargetIdentifier depth)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = colors.Length < 1;
			if (flag)
			{
				throw new ArgumentException(string.Format("colors.Length must be at least 1, but was {0}", colors.Length));
			}
			bool flag2 = colors.Length > SystemInfo.supportedRenderTargetCount;
			if (flag2)
			{
				throw new ArgumentException(string.Format("colors.Length is {0} and exceeds the maximum number of supported render targets ({1})", colors.Length, SystemInfo.supportedRenderTargetCount));
			}
			this.SetRenderTargetMulti_Internal(colors, depth, null, null, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x0002DB24 File Offset: 0x0002BD24
		public void SetRenderTarget(RenderTargetIdentifier[] colors, RenderTargetIdentifier depth, int mipLevel, CubemapFace cubemapFace, int depthSlice)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = colors.Length < 1;
			if (flag)
			{
				throw new ArgumentException(string.Format("colors.Length must be at least 1, but was {0}", colors.Length));
			}
			bool flag2 = colors.Length > SystemInfo.supportedRenderTargetCount;
			if (flag2)
			{
				throw new ArgumentException(string.Format("colors.Length is {0} and exceeds the maximum number of supported render targets ({1})", colors.Length, SystemInfo.supportedRenderTargetCount));
			}
			this.SetRenderTargetMultiSubtarget(colors, depth, null, null, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, mipLevel, cubemapFace, depthSlice);
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x0002DBA0 File Offset: 0x0002BDA0
		public void SetRenderTarget(RenderTargetBinding binding, int mipLevel, CubemapFace cubemapFace, int depthSlice)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = binding.colorRenderTargets.Length < 1;
			if (flag)
			{
				throw new ArgumentException(string.Format("The number of color render targets must be at least 1, but was {0}", binding.colorRenderTargets.Length));
			}
			bool flag2 = binding.colorRenderTargets.Length > SystemInfo.supportedRenderTargetCount;
			if (flag2)
			{
				throw new ArgumentException(string.Format("The number of color render targets ({0}) and exceeds the maximum supported number of render targets ({1})", binding.colorRenderTargets.Length, SystemInfo.supportedRenderTargetCount));
			}
			bool flag3 = binding.colorLoadActions.Length != binding.colorRenderTargets.Length;
			if (flag3)
			{
				throw new ArgumentException(string.Format("The number of color load actions provided ({0}) does not match the number of color render targets ({1})", binding.colorLoadActions.Length, binding.colorRenderTargets.Length));
			}
			bool flag4 = binding.colorStoreActions.Length != binding.colorRenderTargets.Length;
			if (flag4)
			{
				throw new ArgumentException(string.Format("The number of color store actions provided ({0}) does not match the number of color render targets ({1})", binding.colorLoadActions.Length, binding.colorRenderTargets.Length));
			}
			bool flag5 = binding.depthLoadAction == RenderBufferLoadAction.Clear || Array.IndexOf<RenderBufferLoadAction>(binding.colorLoadActions, RenderBufferLoadAction.Clear) > -1;
			if (flag5)
			{
				throw new ArgumentException("RenderBufferLoadAction.Clear is not supported");
			}
			bool flag6 = binding.colorRenderTargets.Length == 1;
			if (flag6)
			{
				this.SetRenderTargetColorDepthSubtarget(binding.colorRenderTargets[0], binding.depthRenderTarget, binding.colorLoadActions[0], binding.colorStoreActions[0], binding.depthLoadAction, binding.depthStoreAction, mipLevel, cubemapFace, depthSlice);
			}
			else
			{
				this.SetRenderTargetMultiSubtarget(binding.colorRenderTargets, binding.depthRenderTarget, binding.colorLoadActions, binding.colorStoreActions, binding.depthLoadAction, binding.depthStoreAction, mipLevel, cubemapFace, depthSlice);
			}
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x0002DD68 File Offset: 0x0002BF68
		public void SetRenderTarget(RenderTargetBinding binding)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = binding.colorRenderTargets.Length < 1;
			if (flag)
			{
				throw new ArgumentException(string.Format("The number of color render targets must be at least 1, but was {0}", binding.colorRenderTargets.Length));
			}
			bool flag2 = binding.colorRenderTargets.Length > SystemInfo.supportedRenderTargetCount;
			if (flag2)
			{
				throw new ArgumentException(string.Format("The number of color render targets ({0}) and exceeds the maximum supported number of render targets ({1})", binding.colorRenderTargets.Length, SystemInfo.supportedRenderTargetCount));
			}
			bool flag3 = binding.colorLoadActions.Length != binding.colorRenderTargets.Length;
			if (flag3)
			{
				throw new ArgumentException(string.Format("The number of color load actions provided ({0}) does not match the number of color render targets ({1})", binding.colorLoadActions.Length, binding.colorRenderTargets.Length));
			}
			bool flag4 = binding.colorStoreActions.Length != binding.colorRenderTargets.Length;
			if (flag4)
			{
				throw new ArgumentException(string.Format("The number of color store actions provided ({0}) does not match the number of color render targets ({1})", binding.colorLoadActions.Length, binding.colorRenderTargets.Length));
			}
			bool flag5 = binding.depthLoadAction == RenderBufferLoadAction.Clear || Array.IndexOf<RenderBufferLoadAction>(binding.colorLoadActions, RenderBufferLoadAction.Clear) > -1;
			if (flag5)
			{
				throw new ArgumentException("RenderBufferLoadAction.Clear is not supported");
			}
			bool flag6 = binding.colorRenderTargets.Length == 1;
			if (flag6)
			{
				this.SetRenderTargetColorDepth_Internal(binding.colorRenderTargets[0], binding.depthRenderTarget, binding.colorLoadActions[0], binding.colorStoreActions[0], binding.depthLoadAction, binding.depthStoreAction);
			}
			else
			{
				this.SetRenderTargetMulti_Internal(binding.colorRenderTargets, binding.depthRenderTarget, binding.colorLoadActions, binding.colorStoreActions, binding.depthLoadAction, binding.depthStoreAction);
			}
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x0002DF25 File Offset: 0x0002C125
		private void SetRenderTargetSingle_Internal(RenderTargetIdentifier rt, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction)
		{
			this.SetRenderTargetSingle_Internal_Injected(ref rt, colorLoadAction, colorStoreAction, depthLoadAction, depthStoreAction);
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x0002DF35 File Offset: 0x0002C135
		private void SetRenderTargetColorDepth_Internal(RenderTargetIdentifier color, RenderTargetIdentifier depth, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction)
		{
			this.SetRenderTargetColorDepth_Internal_Injected(ref color, ref depth, colorLoadAction, colorStoreAction, depthLoadAction, depthStoreAction);
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x0002DF48 File Offset: 0x0002C148
		private void SetRenderTargetMulti_Internal(RenderTargetIdentifier[] colors, RenderTargetIdentifier depth, RenderBufferLoadAction[] colorLoadActions, RenderBufferStoreAction[] colorStoreActions, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction)
		{
			this.SetRenderTargetMulti_Internal_Injected(colors, ref depth, colorLoadActions, colorStoreActions, depthLoadAction, depthStoreAction);
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x0002DF5C File Offset: 0x0002C15C
		private void SetRenderTargetColorDepthSubtarget(RenderTargetIdentifier color, RenderTargetIdentifier depth, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction, int mipLevel, CubemapFace cubemapFace, int depthSlice)
		{
			this.SetRenderTargetColorDepthSubtarget_Injected(ref color, ref depth, colorLoadAction, colorStoreAction, depthLoadAction, depthStoreAction, mipLevel, cubemapFace, depthSlice);
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x0002DF80 File Offset: 0x0002C180
		private void SetRenderTargetMultiSubtarget(RenderTargetIdentifier[] colors, RenderTargetIdentifier depth, RenderBufferLoadAction[] colorLoadActions, RenderBufferStoreAction[] colorStoreActions, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction, int mipLevel, CubemapFace cubemapFace, int depthSlice)
		{
			this.SetRenderTargetMultiSubtarget_Injected(colors, ref depth, colorLoadActions, colorStoreActions, depthLoadAction, depthStoreAction, mipLevel, cubemapFace, depthSlice);
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x0002DFA4 File Offset: 0x0002C1A4
		[NativeMethod("ProcessVTFeedback")]
		private void Internal_ProcessVTFeedback(RenderTargetIdentifier rt, IntPtr resolver, int slice, int x, int width, int y, int height, int mip)
		{
			this.Internal_ProcessVTFeedback_Injected(ref rt, resolver, slice, x, width, y, height, mip);
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x0002DFC8 File Offset: 0x0002C1C8
		[SecuritySafeCritical]
		public void SetComputeBufferData(ComputeBuffer buffer, Array data)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Array passed to RenderingCommandBuffer.SetBufferData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			this.InternalSetComputeBufferData(buffer, data, 0, 0, data.Length, UnsafeUtility.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x0002E030 File Offset: 0x0002C230
		[SecuritySafeCritical]
		public void SetComputeBufferData<T>(ComputeBuffer buffer, List<T> data) where T : struct
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag2)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to RenderingCommandBuffer.SetBufferData(List<>) must be blittable.\n{1}", typeof(T), UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			this.InternalSetComputeBufferData(buffer, NoAllocHelpers.ExtractArrayFromList(data), 0, 0, NoAllocHelpers.SafeLength<T>(data), Marshal.SizeOf(typeof(T)));
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x0002E0A2 File Offset: 0x0002C2A2
		[SecuritySafeCritical]
		public void SetComputeBufferData<T>(ComputeBuffer buffer, NativeArray<T> data) where T : struct
		{
			this.InternalSetComputeBufferNativeData(buffer, (IntPtr)data.GetUnsafeReadOnlyPtr<T>(), 0, 0, data.Length, UnsafeUtility.SizeOf<T>());
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x0002E0C8 File Offset: 0x0002C2C8
		[SecuritySafeCritical]
		public void SetComputeBufferData(ComputeBuffer buffer, Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Array passed to RenderingCommandBuffer.SetBufferData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			bool flag3 = managedBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Length;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (managedBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", managedBufferStartIndex, graphicsBufferStartIndex, count));
			}
			this.InternalSetComputeBufferData(buffer, data, managedBufferStartIndex, graphicsBufferStartIndex, count, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x0002E170 File Offset: 0x0002C370
		[SecuritySafeCritical]
		public void SetComputeBufferData<T>(ComputeBuffer buffer, List<T> data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag2)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to RenderingCommandBuffer.SetBufferData(List<>) must be blittable.\n{1}", typeof(T), UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			bool flag3 = managedBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Count;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (managedBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", managedBufferStartIndex, graphicsBufferStartIndex, count));
			}
			this.InternalSetComputeBufferData(buffer, NoAllocHelpers.ExtractArrayFromList(data), managedBufferStartIndex, graphicsBufferStartIndex, count, Marshal.SizeOf(typeof(T)));
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x0002E224 File Offset: 0x0002C424
		[SecuritySafeCritical]
		public void SetComputeBufferData<T>(ComputeBuffer buffer, NativeArray<T> data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
		{
			bool flag = nativeBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || nativeBufferStartIndex + count > data.Length;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (nativeBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", nativeBufferStartIndex, graphicsBufferStartIndex, count));
			}
			this.InternalSetComputeBufferNativeData(buffer, (IntPtr)data.GetUnsafeReadOnlyPtr<T>(), nativeBufferStartIndex, graphicsBufferStartIndex, count, UnsafeUtility.SizeOf<T>());
		}

		// Token: 0x06001C2C RID: 7212
		[FreeFunction(Name = "RenderingCommandBuffer_Bindings::InternalSetComputeBufferNativeData", HasExplicitThis = true, ThrowsException = true)]
		[SecurityCritical]
		[MethodImpl(4096)]
		private extern void InternalSetComputeBufferNativeData([NotNull] ComputeBuffer buffer, IntPtr data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize);

		// Token: 0x06001C2D RID: 7213
		[SecurityCritical]
		[FreeFunction(Name = "RenderingCommandBuffer_Bindings::InternalSetComputeBufferData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern void InternalSetComputeBufferData([NotNull] ComputeBuffer buffer, Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize);

		// Token: 0x06001C2E RID: 7214
		[FreeFunction(Name = "RenderingCommandBuffer_Bindings::InternalSetComputeBufferCounterValue", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetComputeBufferCounterValue([NotNull] ComputeBuffer buffer, uint counterValue);

		// Token: 0x06001C2F RID: 7215 RVA: 0x0002E298 File Offset: 0x0002C498
		~CommandBuffer()
		{
			this.Dispose(false);
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x0002E2CC File Offset: 0x0002C4CC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x0002E2DE File Offset: 0x0002C4DE
		private void Dispose(bool disposing)
		{
			this.ReleaseBuffer();
			this.m_Ptr = IntPtr.Zero;
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x0002E2F3 File Offset: 0x0002C4F3
		public CommandBuffer()
		{
			this.m_Ptr = CommandBuffer.InitBuffer();
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x0002E308 File Offset: 0x0002C508
		public void Release()
		{
			this.Dispose();
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x0002E314 File Offset: 0x0002C514
		public GraphicsFence CreateAsyncGraphicsFence()
		{
			return this.CreateGraphicsFence(GraphicsFenceType.AsyncQueueSynchronisation, SynchronisationStageFlags.PixelProcessing);
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x0002E330 File Offset: 0x0002C530
		public GraphicsFence CreateAsyncGraphicsFence(SynchronisationStage stage)
		{
			return this.CreateGraphicsFence(GraphicsFenceType.AsyncQueueSynchronisation, GraphicsFence.TranslateSynchronizationStageToFlags(stage));
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x0002E350 File Offset: 0x0002C550
		public GraphicsFence CreateGraphicsFence(GraphicsFenceType fenceType, SynchronisationStageFlags stage)
		{
			GraphicsFence graphicsFence = default(GraphicsFence);
			graphicsFence.m_Ptr = this.CreateGPUFence_Internal(fenceType, stage);
			graphicsFence.InitPostAllocation();
			graphicsFence.Validate();
			return graphicsFence;
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x0002E38A File Offset: 0x0002C58A
		public void WaitOnAsyncGraphicsFence(GraphicsFence fence)
		{
			this.WaitOnAsyncGraphicsFence(fence, SynchronisationStage.VertexProcessing);
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x0002E396 File Offset: 0x0002C596
		public void WaitOnAsyncGraphicsFence(GraphicsFence fence, SynchronisationStage stage)
		{
			this.WaitOnAsyncGraphicsFence(fence, GraphicsFence.TranslateSynchronizationStageToFlags(stage));
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x0002E3A8 File Offset: 0x0002C5A8
		public void WaitOnAsyncGraphicsFence(GraphicsFence fence, SynchronisationStageFlags stage)
		{
			bool flag = fence.m_FenceType > GraphicsFenceType.AsyncQueueSynchronisation;
			if (flag)
			{
				throw new ArgumentException("Attempting to call WaitOnAsyncGPUFence on a fence that is not of GraphicsFenceType.AsyncQueueSynchronization");
			}
			fence.Validate();
			bool flag2 = fence.IsFencePending();
			if (flag2)
			{
				this.WaitOnGPUFence_Internal(fence.m_Ptr, stage);
			}
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x0002E3EF File Offset: 0x0002C5EF
		public void SetComputeFloatParam(ComputeShader computeShader, string name, float val)
		{
			this.SetComputeFloatParam(computeShader, Shader.PropertyToID(name), val);
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x0002E401 File Offset: 0x0002C601
		public void SetComputeIntParam(ComputeShader computeShader, string name, int val)
		{
			this.SetComputeIntParam(computeShader, Shader.PropertyToID(name), val);
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x0002E413 File Offset: 0x0002C613
		public void SetComputeVectorParam(ComputeShader computeShader, string name, Vector4 val)
		{
			this.SetComputeVectorParam(computeShader, Shader.PropertyToID(name), val);
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x0002E425 File Offset: 0x0002C625
		public void SetComputeVectorArrayParam(ComputeShader computeShader, string name, Vector4[] values)
		{
			this.SetComputeVectorArrayParam(computeShader, Shader.PropertyToID(name), values);
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x0002E437 File Offset: 0x0002C637
		public void SetComputeMatrixParam(ComputeShader computeShader, string name, Matrix4x4 val)
		{
			this.SetComputeMatrixParam(computeShader, Shader.PropertyToID(name), val);
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x0002E449 File Offset: 0x0002C649
		public void SetComputeMatrixArrayParam(ComputeShader computeShader, string name, Matrix4x4[] values)
		{
			this.SetComputeMatrixArrayParam(computeShader, Shader.PropertyToID(name), values);
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x0002E45B File Offset: 0x0002C65B
		public void SetComputeFloatParams(ComputeShader computeShader, string name, params float[] values)
		{
			this.Internal_SetComputeFloats(computeShader, Shader.PropertyToID(name), values);
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x0002E46D File Offset: 0x0002C66D
		public void SetComputeFloatParams(ComputeShader computeShader, int nameID, params float[] values)
		{
			this.Internal_SetComputeFloats(computeShader, nameID, values);
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x0002E47A File Offset: 0x0002C67A
		public void SetComputeIntParams(ComputeShader computeShader, string name, params int[] values)
		{
			this.Internal_SetComputeInts(computeShader, Shader.PropertyToID(name), values);
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x0002E48C File Offset: 0x0002C68C
		public void SetComputeIntParams(ComputeShader computeShader, int nameID, params int[] values)
		{
			this.Internal_SetComputeInts(computeShader, nameID, values);
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x0002E499 File Offset: 0x0002C699
		public void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, string name, RenderTargetIdentifier rt)
		{
			this.Internal_SetComputeTextureParam(computeShader, kernelIndex, Shader.PropertyToID(name), ref rt, 0, RenderTextureSubElement.Default);
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x0002E4AF File Offset: 0x0002C6AF
		public void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, int nameID, RenderTargetIdentifier rt)
		{
			this.Internal_SetComputeTextureParam(computeShader, kernelIndex, nameID, ref rt, 0, RenderTextureSubElement.Default);
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x0002E4C0 File Offset: 0x0002C6C0
		public void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, string name, RenderTargetIdentifier rt, int mipLevel)
		{
			this.Internal_SetComputeTextureParam(computeShader, kernelIndex, Shader.PropertyToID(name), ref rt, mipLevel, RenderTextureSubElement.Default);
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x0002E4D7 File Offset: 0x0002C6D7
		public void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, int nameID, RenderTargetIdentifier rt, int mipLevel)
		{
			this.Internal_SetComputeTextureParam(computeShader, kernelIndex, nameID, ref rt, mipLevel, RenderTextureSubElement.Default);
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x0002E4E9 File Offset: 0x0002C6E9
		public void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, string name, RenderTargetIdentifier rt, int mipLevel, RenderTextureSubElement element)
		{
			this.Internal_SetComputeTextureParam(computeShader, kernelIndex, Shader.PropertyToID(name), ref rt, mipLevel, element);
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x0002E501 File Offset: 0x0002C701
		public void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, int nameID, RenderTargetIdentifier rt, int mipLevel, RenderTextureSubElement element)
		{
			this.Internal_SetComputeTextureParam(computeShader, kernelIndex, nameID, ref rt, mipLevel, element);
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x0002E514 File Offset: 0x0002C714
		public void SetComputeBufferParam(ComputeShader computeShader, int kernelIndex, int nameID, ComputeBuffer buffer)
		{
			this.Internal_SetComputeBufferParam(computeShader, kernelIndex, nameID, buffer);
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x0002E523 File Offset: 0x0002C723
		public void SetComputeBufferParam(ComputeShader computeShader, int kernelIndex, string name, ComputeBuffer buffer)
		{
			this.Internal_SetComputeBufferParam(computeShader, kernelIndex, Shader.PropertyToID(name), buffer);
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x0002E537 File Offset: 0x0002C737
		public void SetComputeBufferParam(ComputeShader computeShader, int kernelIndex, int nameID, GraphicsBuffer buffer)
		{
			this.Internal_SetComputeGraphicsBufferParam(computeShader, kernelIndex, nameID, buffer);
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x0002E546 File Offset: 0x0002C746
		public void SetComputeBufferParam(ComputeShader computeShader, int kernelIndex, string name, GraphicsBuffer buffer)
		{
			this.Internal_SetComputeGraphicsBufferParam(computeShader, kernelIndex, Shader.PropertyToID(name), buffer);
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x0002E55A File Offset: 0x0002C75A
		public void DispatchCompute(ComputeShader computeShader, int kernelIndex, int threadGroupsX, int threadGroupsY, int threadGroupsZ)
		{
			this.Internal_DispatchCompute(computeShader, kernelIndex, threadGroupsX, threadGroupsY, threadGroupsZ);
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x0002E56B File Offset: 0x0002C76B
		public void DispatchCompute(ComputeShader computeShader, int kernelIndex, ComputeBuffer indirectBuffer, uint argsOffset)
		{
			this.Internal_DispatchComputeIndirect(computeShader, kernelIndex, indirectBuffer, argsOffset);
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x0002E57A File Offset: 0x0002C77A
		public void DispatchCompute(ComputeShader computeShader, int kernelIndex, GraphicsBuffer indirectBuffer, uint argsOffset)
		{
			this.Internal_DispatchComputeIndirectGraphicsBuffer(computeShader, kernelIndex, indirectBuffer, argsOffset);
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x0002E589 File Offset: 0x0002C789
		public void BuildRayTracingAccelerationStructure(RayTracingAccelerationStructure accelerationStructure)
		{
			this.Internal_BuildRayTracingAccelerationStructure(accelerationStructure);
		}

		// Token: 0x06001C52 RID: 7250 RVA: 0x0002E594 File Offset: 0x0002C794
		public void SetRayTracingAccelerationStructure(RayTracingShader rayTracingShader, string name, RayTracingAccelerationStructure rayTracingAccelerationStructure)
		{
			this.Internal_SetRayTracingAccelerationStructure(rayTracingShader, Shader.PropertyToID(name), rayTracingAccelerationStructure);
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x0002E5A6 File Offset: 0x0002C7A6
		public void SetRayTracingAccelerationStructure(RayTracingShader rayTracingShader, int nameID, RayTracingAccelerationStructure rayTracingAccelerationStructure)
		{
			this.Internal_SetRayTracingAccelerationStructure(rayTracingShader, nameID, rayTracingAccelerationStructure);
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x0002E5B3 File Offset: 0x0002C7B3
		public void SetRayTracingBufferParam(RayTracingShader rayTracingShader, string name, ComputeBuffer buffer)
		{
			this.Internal_SetRayTracingBufferParam(rayTracingShader, Shader.PropertyToID(name), buffer);
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x0002E5C5 File Offset: 0x0002C7C5
		public void SetRayTracingBufferParam(RayTracingShader rayTracingShader, int nameID, ComputeBuffer buffer)
		{
			this.Internal_SetRayTracingBufferParam(rayTracingShader, nameID, buffer);
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x0002E5D2 File Offset: 0x0002C7D2
		public void SetRayTracingTextureParam(RayTracingShader rayTracingShader, string name, RenderTargetIdentifier rt)
		{
			this.Internal_SetRayTracingTextureParam(rayTracingShader, Shader.PropertyToID(name), ref rt);
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x0002E5E5 File Offset: 0x0002C7E5
		public void SetRayTracingTextureParam(RayTracingShader rayTracingShader, int nameID, RenderTargetIdentifier rt)
		{
			this.Internal_SetRayTracingTextureParam(rayTracingShader, nameID, ref rt);
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x0002E5F3 File Offset: 0x0002C7F3
		public void SetRayTracingFloatParam(RayTracingShader rayTracingShader, string name, float val)
		{
			this.Internal_SetRayTracingFloatParam(rayTracingShader, Shader.PropertyToID(name), val);
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x0002E605 File Offset: 0x0002C805
		public void SetRayTracingFloatParam(RayTracingShader rayTracingShader, int nameID, float val)
		{
			this.Internal_SetRayTracingFloatParam(rayTracingShader, nameID, val);
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x0002E612 File Offset: 0x0002C812
		public void SetRayTracingFloatParams(RayTracingShader rayTracingShader, string name, params float[] values)
		{
			this.Internal_SetRayTracingFloats(rayTracingShader, Shader.PropertyToID(name), values);
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x0002E624 File Offset: 0x0002C824
		public void SetRayTracingFloatParams(RayTracingShader rayTracingShader, int nameID, params float[] values)
		{
			this.Internal_SetRayTracingFloats(rayTracingShader, nameID, values);
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x0002E631 File Offset: 0x0002C831
		public void SetRayTracingIntParam(RayTracingShader rayTracingShader, string name, int val)
		{
			this.Internal_SetRayTracingIntParam(rayTracingShader, Shader.PropertyToID(name), val);
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x0002E643 File Offset: 0x0002C843
		public void SetRayTracingIntParam(RayTracingShader rayTracingShader, int nameID, int val)
		{
			this.Internal_SetRayTracingIntParam(rayTracingShader, nameID, val);
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x0002E650 File Offset: 0x0002C850
		public void SetRayTracingIntParams(RayTracingShader rayTracingShader, string name, params int[] values)
		{
			this.Internal_SetRayTracingInts(rayTracingShader, Shader.PropertyToID(name), values);
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x0002E662 File Offset: 0x0002C862
		public void SetRayTracingIntParams(RayTracingShader rayTracingShader, int nameID, params int[] values)
		{
			this.Internal_SetRayTracingInts(rayTracingShader, nameID, values);
		}

		// Token: 0x06001C60 RID: 7264 RVA: 0x0002E66F File Offset: 0x0002C86F
		public void SetRayTracingVectorParam(RayTracingShader rayTracingShader, string name, Vector4 val)
		{
			this.Internal_SetRayTracingVectorParam(rayTracingShader, Shader.PropertyToID(name), val);
		}

		// Token: 0x06001C61 RID: 7265 RVA: 0x0002E681 File Offset: 0x0002C881
		public void SetRayTracingVectorParam(RayTracingShader rayTracingShader, int nameID, Vector4 val)
		{
			this.Internal_SetRayTracingVectorParam(rayTracingShader, nameID, val);
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x0002E68E File Offset: 0x0002C88E
		public void SetRayTracingVectorArrayParam(RayTracingShader rayTracingShader, string name, params Vector4[] values)
		{
			this.Internal_SetRayTracingVectorArrayParam(rayTracingShader, Shader.PropertyToID(name), values);
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x0002E6A0 File Offset: 0x0002C8A0
		public void SetRayTracingVectorArrayParam(RayTracingShader rayTracingShader, int nameID, params Vector4[] values)
		{
			this.Internal_SetRayTracingVectorArrayParam(rayTracingShader, nameID, values);
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x0002E6AD File Offset: 0x0002C8AD
		public void SetRayTracingMatrixParam(RayTracingShader rayTracingShader, string name, Matrix4x4 val)
		{
			this.Internal_SetRayTracingMatrixParam(rayTracingShader, Shader.PropertyToID(name), val);
		}

		// Token: 0x06001C65 RID: 7269 RVA: 0x0002E6BF File Offset: 0x0002C8BF
		public void SetRayTracingMatrixParam(RayTracingShader rayTracingShader, int nameID, Matrix4x4 val)
		{
			this.Internal_SetRayTracingMatrixParam(rayTracingShader, nameID, val);
		}

		// Token: 0x06001C66 RID: 7270 RVA: 0x0002E6CC File Offset: 0x0002C8CC
		public void SetRayTracingMatrixArrayParam(RayTracingShader rayTracingShader, string name, params Matrix4x4[] values)
		{
			this.Internal_SetRayTracingMatrixArrayParam(rayTracingShader, Shader.PropertyToID(name), values);
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x0002E6DE File Offset: 0x0002C8DE
		public void SetRayTracingMatrixArrayParam(RayTracingShader rayTracingShader, int nameID, params Matrix4x4[] values)
		{
			this.Internal_SetRayTracingMatrixArrayParam(rayTracingShader, nameID, values);
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x0002E6EB File Offset: 0x0002C8EB
		public void DispatchRays(RayTracingShader rayTracingShader, string rayGenName, uint width, uint height, uint depth, Camera camera = null)
		{
			this.Internal_DispatchRays(rayTracingShader, rayGenName, width, height, depth, camera);
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x0002E700 File Offset: 0x0002C900
		public void GenerateMips(RenderTexture rt)
		{
			bool flag = rt == null;
			if (flag)
			{
				throw new ArgumentNullException("rt");
			}
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_GenerateMips(rt);
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x0002E738 File Offset: 0x0002C938
		public void ResolveAntiAliasedSurface(RenderTexture rt, RenderTexture target = null)
		{
			bool flag = rt == null;
			if (flag)
			{
				throw new ArgumentNullException("rt");
			}
			this.Internal_ResolveAntiAliasedSurface(rt, target);
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x0002E768 File Offset: 0x0002C968
		public void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int submeshIndex, int shaderPass, MaterialPropertyBlock properties)
		{
			bool flag = mesh == null;
			if (flag)
			{
				throw new ArgumentNullException("mesh");
			}
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag2 = submeshIndex < 0 || submeshIndex >= mesh.subMeshCount;
			if (flag2)
			{
				submeshIndex = Mathf.Clamp(submeshIndex, 0, mesh.subMeshCount - 1);
				Debug.LogWarning(string.Format("submeshIndex out of range. Clampped to {0}.", submeshIndex));
			}
			bool flag3 = material == null;
			if (flag3)
			{
				throw new ArgumentNullException("material");
			}
			this.Internal_DrawMesh(mesh, matrix, material, submeshIndex, shaderPass, properties);
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x0002E7FF File Offset: 0x0002C9FF
		public void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int submeshIndex, int shaderPass)
		{
			this.DrawMesh(mesh, matrix, material, submeshIndex, shaderPass, null);
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x0002E811 File Offset: 0x0002CA11
		public void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int submeshIndex)
		{
			this.DrawMesh(mesh, matrix, material, submeshIndex, -1);
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x0002E821 File Offset: 0x0002CA21
		public void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material)
		{
			this.DrawMesh(mesh, matrix, material, 0);
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x0002E830 File Offset: 0x0002CA30
		public void DrawRenderer(Renderer renderer, Material material, int submeshIndex, int shaderPass)
		{
			bool flag = renderer == null;
			if (flag)
			{
				throw new ArgumentNullException("renderer");
			}
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag2 = submeshIndex < 0;
			if (flag2)
			{
				submeshIndex = Mathf.Max(submeshIndex, 0);
				Debug.LogWarning(string.Format("submeshIndex out of range. Clampped to {0}.", submeshIndex));
			}
			bool flag3 = material == null;
			if (flag3)
			{
				throw new ArgumentNullException("material");
			}
			this.Internal_DrawRenderer(renderer, material, submeshIndex, shaderPass);
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x0002E8A8 File Offset: 0x0002CAA8
		public void DrawRenderer(Renderer renderer, Material material, int submeshIndex)
		{
			this.DrawRenderer(renderer, material, submeshIndex, -1);
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x0002E8B6 File Offset: 0x0002CAB6
		public void DrawRenderer(Renderer renderer, Material material)
		{
			this.DrawRenderer(renderer, material, 0);
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x0002E8C4 File Offset: 0x0002CAC4
		public void DrawProcedural(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int vertexCount, int instanceCount, MaterialPropertyBlock properties)
		{
			bool flag = material == null;
			if (flag)
			{
				throw new ArgumentNullException("material");
			}
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_DrawProcedural(matrix, material, shaderPass, topology, vertexCount, instanceCount, properties);
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x0002E903 File Offset: 0x0002CB03
		public void DrawProcedural(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int vertexCount, int instanceCount)
		{
			this.DrawProcedural(matrix, material, shaderPass, topology, vertexCount, instanceCount, null);
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x0002E917 File Offset: 0x0002CB17
		public void DrawProcedural(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int vertexCount)
		{
			this.DrawProcedural(matrix, material, shaderPass, topology, vertexCount, 1);
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x0002E92C File Offset: 0x0002CB2C
		public void DrawProcedural(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int indexCount, int instanceCount, MaterialPropertyBlock properties)
		{
			bool flag = indexBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("indexBuffer");
			}
			bool flag2 = material == null;
			if (flag2)
			{
				throw new ArgumentNullException("material");
			}
			this.Internal_DrawProceduralIndexed(indexBuffer, matrix, material, shaderPass, topology, indexCount, instanceCount, properties);
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x0002E978 File Offset: 0x0002CB78
		public void DrawProcedural(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int indexCount, int instanceCount)
		{
			this.DrawProcedural(indexBuffer, matrix, material, shaderPass, topology, indexCount, instanceCount, null);
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x0002E999 File Offset: 0x0002CB99
		public void DrawProcedural(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int indexCount)
		{
			this.DrawProcedural(indexBuffer, matrix, material, shaderPass, topology, indexCount, 1);
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x0002E9B0 File Offset: 0x0002CBB0
		public void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			bool flag = material == null;
			if (flag)
			{
				throw new ArgumentNullException("material");
			}
			bool flag2 = bufferWithArgs == null;
			if (flag2)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_DrawProceduralIndirect(matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, properties);
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x0002EA03 File Offset: 0x0002CC03
		public void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset)
		{
			this.DrawProceduralIndirect(matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, null);
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x0002EA17 File Offset: 0x0002CC17
		public void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs)
		{
			this.DrawProceduralIndirect(matrix, material, shaderPass, topology, bufferWithArgs, 0);
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x0002EA2C File Offset: 0x0002CC2C
		public void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			bool flag = indexBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("indexBuffer");
			}
			bool flag2 = material == null;
			if (flag2)
			{
				throw new ArgumentNullException("material");
			}
			bool flag3 = bufferWithArgs == null;
			if (flag3)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			this.Internal_DrawProceduralIndexedIndirect(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, properties);
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x0002EA8C File Offset: 0x0002CC8C
		public void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset)
		{
			this.DrawProceduralIndirect(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, null);
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x0002EAAD File Offset: 0x0002CCAD
		public void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs)
		{
			this.DrawProceduralIndirect(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs, 0);
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x0002EAC4 File Offset: 0x0002CCC4
		public void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			bool flag = material == null;
			if (flag)
			{
				throw new ArgumentNullException("material");
			}
			bool flag2 = bufferWithArgs == null;
			if (flag2)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_DrawProceduralIndirectGraphicsBuffer(matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, properties);
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x0002EB17 File Offset: 0x0002CD17
		public void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset)
		{
			this.DrawProceduralIndirect(matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, null);
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x0002EB2B File Offset: 0x0002CD2B
		public void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs)
		{
			this.DrawProceduralIndirect(matrix, material, shaderPass, topology, bufferWithArgs, 0);
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x0002EB40 File Offset: 0x0002CD40
		public void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			bool flag = indexBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("indexBuffer");
			}
			bool flag2 = material == null;
			if (flag2)
			{
				throw new ArgumentNullException("material");
			}
			bool flag3 = bufferWithArgs == null;
			if (flag3)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			this.Internal_DrawProceduralIndexedIndirectGraphicsBuffer(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, properties);
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x0002EBA0 File Offset: 0x0002CDA0
		public void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset)
		{
			this.DrawProceduralIndirect(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, null);
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x0002EBC1 File Offset: 0x0002CDC1
		public void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs)
		{
			this.DrawProceduralIndirect(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs, 0);
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x0002EBD8 File Offset: 0x0002CDD8
		public void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, int shaderPass, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("DrawMeshInstanced is not supported.");
			}
			bool flag2 = mesh == null;
			if (flag2)
			{
				throw new ArgumentNullException("mesh");
			}
			bool flag3 = submeshIndex < 0 || submeshIndex >= mesh.subMeshCount;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("submeshIndex", "submeshIndex out of range.");
			}
			bool flag4 = material == null;
			if (flag4)
			{
				throw new ArgumentNullException("material");
			}
			bool flag5 = matrices == null;
			if (flag5)
			{
				throw new ArgumentNullException("matrices");
			}
			bool flag6 = count < 0 || count > Mathf.Min(Graphics.kMaxDrawMeshInstanceCount, matrices.Length);
			if (flag6)
			{
				throw new ArgumentOutOfRangeException("count", string.Format("Count must be in the range of 0 to {0}.", Mathf.Min(Graphics.kMaxDrawMeshInstanceCount, matrices.Length)));
			}
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag7 = count > 0;
			if (flag7)
			{
				this.Internal_DrawMeshInstanced(mesh, submeshIndex, material, shaderPass, matrices, count, properties);
			}
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x0002ECD4 File Offset: 0x0002CED4
		public void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, int shaderPass, Matrix4x4[] matrices, int count)
		{
			this.DrawMeshInstanced(mesh, submeshIndex, material, shaderPass, matrices, count, null);
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x0002ECE8 File Offset: 0x0002CEE8
		public void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, int shaderPass, Matrix4x4[] matrices)
		{
			this.DrawMeshInstanced(mesh, submeshIndex, material, shaderPass, matrices, matrices.Length);
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x0002ED00 File Offset: 0x0002CF00
		public void DrawMeshInstancedProcedural(Mesh mesh, int submeshIndex, Material material, int shaderPass, int count, MaterialPropertyBlock properties = null)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("DrawMeshInstancedProcedural is not supported.");
			}
			bool flag2 = mesh == null;
			if (flag2)
			{
				throw new ArgumentNullException("mesh");
			}
			bool flag3 = submeshIndex < 0 || submeshIndex >= mesh.subMeshCount;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("submeshIndex", "submeshIndex out of range.");
			}
			bool flag4 = material == null;
			if (flag4)
			{
				throw new ArgumentNullException("material");
			}
			bool flag5 = count <= 0;
			if (flag5)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag6 = count > 0;
			if (flag6)
			{
				this.Internal_DrawMeshInstancedProcedural(mesh, submeshIndex, material, shaderPass, count, properties);
			}
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x0002EDB8 File Offset: 0x0002CFB8
		public void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			bool flag2 = mesh == null;
			if (flag2)
			{
				throw new ArgumentNullException("mesh");
			}
			bool flag3 = submeshIndex < 0 || submeshIndex >= mesh.subMeshCount;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("submeshIndex", "submeshIndex out of range.");
			}
			bool flag4 = material == null;
			if (flag4)
			{
				throw new ArgumentNullException("material");
			}
			bool flag5 = bufferWithArgs == null;
			if (flag5)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			this.Internal_DrawMeshInstancedIndirect(mesh, submeshIndex, material, shaderPass, bufferWithArgs, argsOffset, properties);
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x0002EE58 File Offset: 0x0002D058
		public void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, ComputeBuffer bufferWithArgs, int argsOffset)
		{
			this.DrawMeshInstancedIndirect(mesh, submeshIndex, material, shaderPass, bufferWithArgs, argsOffset, null);
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x0002EE6C File Offset: 0x0002D06C
		public void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, ComputeBuffer bufferWithArgs)
		{
			this.DrawMeshInstancedIndirect(mesh, submeshIndex, material, shaderPass, bufferWithArgs, 0, null);
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x0002EE80 File Offset: 0x0002D080
		public void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			bool flag2 = mesh == null;
			if (flag2)
			{
				throw new ArgumentNullException("mesh");
			}
			bool flag3 = submeshIndex < 0 || submeshIndex >= mesh.subMeshCount;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("submeshIndex", "submeshIndex out of range.");
			}
			bool flag4 = material == null;
			if (flag4)
			{
				throw new ArgumentNullException("material");
			}
			bool flag5 = bufferWithArgs == null;
			if (flag5)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			this.Internal_DrawMeshInstancedIndirectGraphicsBuffer(mesh, submeshIndex, material, shaderPass, bufferWithArgs, argsOffset, properties);
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x0002EF20 File Offset: 0x0002D120
		public void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, GraphicsBuffer bufferWithArgs, int argsOffset)
		{
			this.DrawMeshInstancedIndirect(mesh, submeshIndex, material, shaderPass, bufferWithArgs, argsOffset, null);
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x0002EF34 File Offset: 0x0002D134
		public void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, GraphicsBuffer bufferWithArgs)
		{
			this.DrawMeshInstancedIndirect(mesh, submeshIndex, material, shaderPass, bufferWithArgs, 0, null);
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x0002EF47 File Offset: 0x0002D147
		public void DrawOcclusionMesh(RectInt normalizedCamViewport)
		{
			this.Internal_DrawOcclusionMesh(normalizedCamViewport);
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x0002EF52 File Offset: 0x0002D152
		public void SetRandomWriteTarget(int index, RenderTargetIdentifier rt)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.SetRandomWriteTarget_Texture(index, ref rt);
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x0002EF68 File Offset: 0x0002D168
		public void SetRandomWriteTarget(int index, ComputeBuffer buffer, bool preserveCounterValue)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.SetRandomWriteTarget_Buffer(index, buffer, preserveCounterValue);
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x0002EF7E File Offset: 0x0002D17E
		public void SetRandomWriteTarget(int index, ComputeBuffer buffer)
		{
			this.SetRandomWriteTarget(index, buffer, false);
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x0002EF8B File Offset: 0x0002D18B
		public void SetRandomWriteTarget(int index, GraphicsBuffer buffer, bool preserveCounterValue)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.SetRandomWriteTarget_GraphicsBuffer(index, buffer, preserveCounterValue);
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x0002EFA1 File Offset: 0x0002D1A1
		public void SetRandomWriteTarget(int index, GraphicsBuffer buffer)
		{
			this.SetRandomWriteTarget(index, buffer, false);
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x0002EFAE File Offset: 0x0002D1AE
		public void CopyCounterValue(ComputeBuffer src, ComputeBuffer dst, uint dstOffsetBytes)
		{
			this.CopyCounterValueCC(src, dst, dstOffsetBytes);
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x0002EFBB File Offset: 0x0002D1BB
		public void CopyCounterValue(GraphicsBuffer src, ComputeBuffer dst, uint dstOffsetBytes)
		{
			this.CopyCounterValueGC(src, dst, dstOffsetBytes);
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x0002EFC8 File Offset: 0x0002D1C8
		public void CopyCounterValue(ComputeBuffer src, GraphicsBuffer dst, uint dstOffsetBytes)
		{
			this.CopyCounterValueCG(src, dst, dstOffsetBytes);
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x0002EFD5 File Offset: 0x0002D1D5
		public void CopyCounterValue(GraphicsBuffer src, GraphicsBuffer dst, uint dstOffsetBytes)
		{
			this.CopyCounterValueGG(src, dst, dstOffsetBytes);
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x0002EFE4 File Offset: 0x0002D1E4
		public void CopyTexture(RenderTargetIdentifier src, RenderTargetIdentifier dst)
		{
			this.CopyTexture_Internal(ref src, -1, -1, -1, -1, -1, -1, ref dst, -1, -1, -1, -1, 1);
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x0002F008 File Offset: 0x0002D208
		public void CopyTexture(RenderTargetIdentifier src, int srcElement, RenderTargetIdentifier dst, int dstElement)
		{
			this.CopyTexture_Internal(ref src, srcElement, -1, -1, -1, -1, -1, ref dst, dstElement, -1, -1, -1, 2);
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x0002F030 File Offset: 0x0002D230
		public void CopyTexture(RenderTargetIdentifier src, int srcElement, int srcMip, RenderTargetIdentifier dst, int dstElement, int dstMip)
		{
			this.CopyTexture_Internal(ref src, srcElement, srcMip, -1, -1, -1, -1, ref dst, dstElement, dstMip, -1, -1, 3);
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x0002F058 File Offset: 0x0002D258
		public void CopyTexture(RenderTargetIdentifier src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, RenderTargetIdentifier dst, int dstElement, int dstMip, int dstX, int dstY)
		{
			this.CopyTexture_Internal(ref src, srcElement, srcMip, srcX, srcY, srcWidth, srcHeight, ref dst, dstElement, dstMip, dstX, dstY, 4);
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x0002F084 File Offset: 0x0002D284
		public void Blit(Texture source, RenderTargetIdentifier dest)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Blit_Texture(source, ref dest, null, -1, new Vector2(1f, 1f), new Vector2(0f, 0f), Texture2DArray.allSlices, 0);
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x0002F0CC File Offset: 0x0002D2CC
		public void Blit(Texture source, RenderTargetIdentifier dest, Vector2 scale, Vector2 offset)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Blit_Texture(source, ref dest, null, -1, scale, offset, Texture2DArray.allSlices, 0);
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x0002F0F8 File Offset: 0x0002D2F8
		public void Blit(Texture source, RenderTargetIdentifier dest, Material mat)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Blit_Texture(source, ref dest, mat, -1, new Vector2(1f, 1f), new Vector2(0f, 0f), Texture2DArray.allSlices, 0);
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x0002F140 File Offset: 0x0002D340
		public void Blit(Texture source, RenderTargetIdentifier dest, Material mat, int pass)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Blit_Texture(source, ref dest, mat, pass, new Vector2(1f, 1f), new Vector2(0f, 0f), Texture2DArray.allSlices, 0);
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x0002F188 File Offset: 0x0002D388
		public void Blit(RenderTargetIdentifier source, RenderTargetIdentifier dest)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Blit_Identifier(ref source, ref dest, null, -1, new Vector2(1f, 1f), new Vector2(0f, 0f), Texture2DArray.allSlices, 0);
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x0002F1D0 File Offset: 0x0002D3D0
		public void Blit(RenderTargetIdentifier source, RenderTargetIdentifier dest, Vector2 scale, Vector2 offset)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Blit_Identifier(ref source, ref dest, null, -1, scale, offset, Texture2DArray.allSlices, 0);
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x0002F200 File Offset: 0x0002D400
		public void Blit(RenderTargetIdentifier source, RenderTargetIdentifier dest, Material mat)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Blit_Identifier(ref source, ref dest, mat, -1, new Vector2(1f, 1f), new Vector2(0f, 0f), Texture2DArray.allSlices, 0);
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x0002F248 File Offset: 0x0002D448
		public void Blit(RenderTargetIdentifier source, RenderTargetIdentifier dest, Material mat, int pass)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Blit_Identifier(ref source, ref dest, mat, pass, new Vector2(1f, 1f), new Vector2(0f, 0f), Texture2DArray.allSlices, 0);
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x0002F294 File Offset: 0x0002D494
		public void Blit(RenderTargetIdentifier source, RenderTargetIdentifier dest, int sourceDepthSlice, int destDepthSlice)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Blit_Identifier(ref source, ref dest, null, -1, new Vector2(1f, 1f), new Vector2(0f, 0f), sourceDepthSlice, destDepthSlice);
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x0002F2DC File Offset: 0x0002D4DC
		public void Blit(RenderTargetIdentifier source, RenderTargetIdentifier dest, Vector2 scale, Vector2 offset, int sourceDepthSlice, int destDepthSlice)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Blit_Identifier(ref source, ref dest, null, -1, scale, offset, sourceDepthSlice, destDepthSlice);
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x0002F308 File Offset: 0x0002D508
		public void Blit(RenderTargetIdentifier source, RenderTargetIdentifier dest, Material mat, int pass, int destDepthSlice)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Blit_Identifier(ref source, ref dest, mat, pass, new Vector2(1f, 1f), new Vector2(0f, 0f), Texture2DArray.allSlices, destDepthSlice);
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x0002F352 File Offset: 0x0002D552
		public void SetGlobalFloat(string name, float value)
		{
			this.SetGlobalFloat(Shader.PropertyToID(name), value);
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x0002F363 File Offset: 0x0002D563
		public void SetGlobalInt(string name, int value)
		{
			this.SetGlobalInt(Shader.PropertyToID(name), value);
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x0002F374 File Offset: 0x0002D574
		public void SetGlobalVector(string name, Vector4 value)
		{
			this.SetGlobalVector(Shader.PropertyToID(name), value);
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x0002F385 File Offset: 0x0002D585
		public void SetGlobalColor(string name, Color value)
		{
			this.SetGlobalColor(Shader.PropertyToID(name), value);
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x0002F396 File Offset: 0x0002D596
		public void SetGlobalMatrix(string name, Matrix4x4 value)
		{
			this.SetGlobalMatrix(Shader.PropertyToID(name), value);
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x0002F3A7 File Offset: 0x0002D5A7
		public void SetGlobalFloatArray(string propertyName, List<float> values)
		{
			this.SetGlobalFloatArray(Shader.PropertyToID(propertyName), values);
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x0002F3B8 File Offset: 0x0002D5B8
		public void SetGlobalFloatArray(int nameID, List<float> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Count == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			this.SetGlobalFloatArrayListImpl(nameID, values);
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x0002F3FA File Offset: 0x0002D5FA
		public void SetGlobalFloatArray(string propertyName, float[] values)
		{
			this.SetGlobalFloatArray(Shader.PropertyToID(propertyName), values);
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x0002F40B File Offset: 0x0002D60B
		public void SetGlobalVectorArray(string propertyName, List<Vector4> values)
		{
			this.SetGlobalVectorArray(Shader.PropertyToID(propertyName), values);
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x0002F41C File Offset: 0x0002D61C
		public void SetGlobalVectorArray(int nameID, List<Vector4> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Count == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			this.SetGlobalVectorArrayListImpl(nameID, values);
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x0002F45E File Offset: 0x0002D65E
		public void SetGlobalVectorArray(string propertyName, Vector4[] values)
		{
			this.SetGlobalVectorArray(Shader.PropertyToID(propertyName), values);
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x0002F46F File Offset: 0x0002D66F
		public void SetGlobalMatrixArray(string propertyName, List<Matrix4x4> values)
		{
			this.SetGlobalMatrixArray(Shader.PropertyToID(propertyName), values);
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x0002F480 File Offset: 0x0002D680
		public void SetGlobalMatrixArray(int nameID, List<Matrix4x4> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Count == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			this.SetGlobalMatrixArrayListImpl(nameID, values);
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x0002F4C2 File Offset: 0x0002D6C2
		public void SetGlobalMatrixArray(string propertyName, Matrix4x4[] values)
		{
			this.SetGlobalMatrixArray(Shader.PropertyToID(propertyName), values);
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x0002F4D3 File Offset: 0x0002D6D3
		public void SetGlobalTexture(string name, RenderTargetIdentifier value)
		{
			this.SetGlobalTexture(Shader.PropertyToID(name), value, RenderTextureSubElement.Default);
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x0002F4E5 File Offset: 0x0002D6E5
		public void SetGlobalTexture(int nameID, RenderTargetIdentifier value)
		{
			this.SetGlobalTexture_Impl(nameID, ref value, RenderTextureSubElement.Default);
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x0002F4F3 File Offset: 0x0002D6F3
		public void SetGlobalTexture(string name, RenderTargetIdentifier value, RenderTextureSubElement element)
		{
			this.SetGlobalTexture(Shader.PropertyToID(name), value, element);
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x0002F505 File Offset: 0x0002D705
		public void SetGlobalTexture(int nameID, RenderTargetIdentifier value, RenderTextureSubElement element)
		{
			this.SetGlobalTexture_Impl(nameID, ref value, element);
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x0002F513 File Offset: 0x0002D713
		public void SetGlobalBuffer(string name, ComputeBuffer value)
		{
			this.SetGlobalBufferInternal(Shader.PropertyToID(name), value);
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x0002F524 File Offset: 0x0002D724
		public void SetGlobalBuffer(int nameID, ComputeBuffer value)
		{
			this.SetGlobalBufferInternal(nameID, value);
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x0002F530 File Offset: 0x0002D730
		public void SetGlobalBuffer(string name, GraphicsBuffer value)
		{
			this.SetGlobalGraphicsBufferInternal(Shader.PropertyToID(name), value);
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x0002F541 File Offset: 0x0002D741
		public void SetGlobalBuffer(int nameID, GraphicsBuffer value)
		{
			this.SetGlobalGraphicsBufferInternal(nameID, value);
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x0002F54D File Offset: 0x0002D74D
		public void SetGlobalConstantBuffer(ComputeBuffer buffer, int nameID, int offset, int size)
		{
			this.SetGlobalConstantBufferInternal(buffer, nameID, offset, size);
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x0002F55C File Offset: 0x0002D75C
		public void SetGlobalConstantBuffer(ComputeBuffer buffer, string name, int offset, int size)
		{
			this.SetGlobalConstantBufferInternal(buffer, Shader.PropertyToID(name), offset, size);
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x0002F570 File Offset: 0x0002D770
		public void SetGlobalConstantBuffer(GraphicsBuffer buffer, int nameID, int offset, int size)
		{
			this.SetGlobalConstantGraphicsBufferInternal(buffer, nameID, offset, size);
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x0002F57F File Offset: 0x0002D77F
		public void SetGlobalConstantBuffer(GraphicsBuffer buffer, string name, int offset, int size)
		{
			this.SetGlobalConstantGraphicsBufferInternal(buffer, Shader.PropertyToID(name), offset, size);
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x0002F593 File Offset: 0x0002D793
		public void SetShadowSamplingMode(RenderTargetIdentifier shadowmap, ShadowSamplingMode mode)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.SetShadowSamplingMode_Impl(ref shadowmap, mode);
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x0002F5A9 File Offset: 0x0002D7A9
		public void SetSinglePassStereo(SinglePassStereoMode mode)
		{
			this.Internal_SetSinglePassStereo(mode);
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x0002F5B4 File Offset: 0x0002D7B4
		public void IssuePluginEvent(IntPtr callback, int eventID)
		{
			bool flag = callback == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("Null callback specified.");
			}
			this.IssuePluginEventInternal(callback, eventID);
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x0002F5E8 File Offset: 0x0002D7E8
		public void IssuePluginEventAndData(IntPtr callback, int eventID, IntPtr data)
		{
			bool flag = callback == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("Null callback specified.");
			}
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.IssuePluginEventAndDataInternal(callback, eventID, data);
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x0002F623 File Offset: 0x0002D823
		public void IssuePluginCustomBlit(IntPtr callback, uint command, RenderTargetIdentifier source, RenderTargetIdentifier dest, uint commandParam, uint commandFlags)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.IssuePluginCustomBlitInternal(callback, command, ref source, ref dest, commandParam, commandFlags);
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x0002F640 File Offset: 0x0002D840
		[Obsolete("Use IssuePluginCustomTextureUpdateV2 to register TextureUpdate callbacks instead. Callbacks will be passed event IDs kUnityRenderingExtEventUpdateTextureBeginV2 or kUnityRenderingExtEventUpdateTextureEndV2, and data parameter of type UnityRenderingExtTextureUpdateParamsV2.", false)]
		public void IssuePluginCustomTextureUpdate(IntPtr callback, Texture targetTexture, uint userData)
		{
			this.IssuePluginCustomTextureUpdateInternal(callback, targetTexture, userData, false);
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x0002F640 File Offset: 0x0002D840
		[Obsolete("Use IssuePluginCustomTextureUpdateV2 to register TextureUpdate callbacks instead. Callbacks will be passed event IDs kUnityRenderingExtEventUpdateTextureBeginV2 or kUnityRenderingExtEventUpdateTextureEndV2, and data parameter of type UnityRenderingExtTextureUpdateParamsV2.", false)]
		public void IssuePluginCustomTextureUpdateV1(IntPtr callback, Texture targetTexture, uint userData)
		{
			this.IssuePluginCustomTextureUpdateInternal(callback, targetTexture, userData, false);
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x0002F64E File Offset: 0x0002D84E
		public void IssuePluginCustomTextureUpdateV2(IntPtr callback, Texture targetTexture, uint userData)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.IssuePluginCustomTextureUpdateInternal(callback, targetTexture, userData, true);
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x0002F668 File Offset: 0x0002D868
		public void ProcessVTFeedback(RenderTargetIdentifier rt, IntPtr resolver, int slice, int x, int width, int y, int height, int mip)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_ProcessVTFeedback(rt, resolver, slice, x, width, y, height, mip);
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x0002F694 File Offset: 0x0002D894
		[Obsolete("CommandBuffer.CreateGPUFence has been deprecated. Use CreateGraphicsFence instead (UnityUpgradable) -> CreateAsyncGraphicsFence(*)", false)]
		public GPUFence CreateGPUFence(SynchronisationStage stage)
		{
			return default(GPUFence);
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x0002F6B0 File Offset: 0x0002D8B0
		[Obsolete("CommandBuffer.CreateGPUFence has been deprecated. Use CreateGraphicsFence instead (UnityUpgradable) -> CreateAsyncGraphicsFence()", false)]
		public GPUFence CreateGPUFence()
		{
			return default(GPUFence);
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x00002EC3 File Offset: 0x000010C3
		[Obsolete("CommandBuffer.WaitOnGPUFence has been deprecated. Use WaitOnGraphicsFence instead (UnityUpgradable) -> WaitOnAsyncGraphicsFence(*)", false)]
		public void WaitOnGPUFence(GPUFence fence, SynchronisationStage stage)
		{
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x00002EC3 File Offset: 0x000010C3
		[Obsolete("CommandBuffer.WaitOnGPUFence has been deprecated. Use WaitOnGraphicsFence instead (UnityUpgradable) -> WaitOnAsyncGraphicsFence(*)", false)]
		public void WaitOnGPUFence(GPUFence fence)
		{
		}

		// Token: 0x06001CCE RID: 7374
		[MethodImpl(4096)]
		private extern void ConvertTexture_Internal_Injected(ref RenderTargetIdentifier src, int srcElement, ref RenderTargetIdentifier dst, int dstElement);

		// Token: 0x06001CCF RID: 7375
		[MethodImpl(4096)]
		private extern void SetComputeVectorParam_Injected(ComputeShader computeShader, int nameID, ref Vector4 val);

		// Token: 0x06001CD0 RID: 7376
		[MethodImpl(4096)]
		private extern void SetComputeMatrixParam_Injected(ComputeShader computeShader, int nameID, ref Matrix4x4 val);

		// Token: 0x06001CD1 RID: 7377
		[MethodImpl(4096)]
		private extern void Internal_SetRayTracingVectorParam_Injected(RayTracingShader rayTracingShader, int nameID, ref Vector4 val);

		// Token: 0x06001CD2 RID: 7378
		[MethodImpl(4096)]
		private extern void Internal_SetRayTracingMatrixParam_Injected(RayTracingShader rayTracingShader, int nameID, ref Matrix4x4 val);

		// Token: 0x06001CD3 RID: 7379
		[MethodImpl(4096)]
		private extern void Internal_DrawMesh_Injected(Mesh mesh, ref Matrix4x4 matrix, Material material, int submeshIndex, int shaderPass, MaterialPropertyBlock properties);

		// Token: 0x06001CD4 RID: 7380
		[MethodImpl(4096)]
		private extern void Internal_DrawProcedural_Injected(ref Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int vertexCount, int instanceCount, MaterialPropertyBlock properties);

		// Token: 0x06001CD5 RID: 7381
		[MethodImpl(4096)]
		private extern void Internal_DrawProceduralIndexed_Injected(GraphicsBuffer indexBuffer, ref Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int indexCount, int instanceCount, MaterialPropertyBlock properties);

		// Token: 0x06001CD6 RID: 7382
		[MethodImpl(4096)]
		private extern void Internal_DrawProceduralIndirect_Injected(ref Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties);

		// Token: 0x06001CD7 RID: 7383
		[MethodImpl(4096)]
		private extern void Internal_DrawProceduralIndexedIndirect_Injected(GraphicsBuffer indexBuffer, ref Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties);

		// Token: 0x06001CD8 RID: 7384
		[MethodImpl(4096)]
		private extern void Internal_DrawProceduralIndirectGraphicsBuffer_Injected(ref Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties);

		// Token: 0x06001CD9 RID: 7385
		[MethodImpl(4096)]
		private extern void Internal_DrawProceduralIndexedIndirectGraphicsBuffer_Injected(GraphicsBuffer indexBuffer, ref Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties);

		// Token: 0x06001CDA RID: 7386
		[MethodImpl(4096)]
		private extern void Internal_DrawOcclusionMesh_Injected(ref RectInt normalizedCamViewport);

		// Token: 0x06001CDB RID: 7387
		[MethodImpl(4096)]
		private extern void SetViewport_Injected(ref Rect pixelRect);

		// Token: 0x06001CDC RID: 7388
		[MethodImpl(4096)]
		private extern void EnableScissorRect_Injected(ref Rect scissor);

		// Token: 0x06001CDD RID: 7389
		[MethodImpl(4096)]
		private extern void Blit_Texture_Injected(Texture source, ref RenderTargetIdentifier dest, Material mat, int pass, ref Vector2 scale, ref Vector2 offset, int sourceDepthSlice, int destDepthSlice);

		// Token: 0x06001CDE RID: 7390
		[MethodImpl(4096)]
		private extern void Blit_Identifier_Injected(ref RenderTargetIdentifier source, ref RenderTargetIdentifier dest, Material mat, int pass, ref Vector2 scale, ref Vector2 offset, int sourceDepthSlice, int destDepthSlice);

		// Token: 0x06001CDF RID: 7391
		[MethodImpl(4096)]
		private extern void GetTemporaryRTWithDescriptor_Injected(int nameID, ref RenderTextureDescriptor desc, FilterMode filter);

		// Token: 0x06001CE0 RID: 7392
		[MethodImpl(4096)]
		private extern void ClearRenderTarget_Injected(bool clearDepth, bool clearColor, ref Color backgroundColor, float depth);

		// Token: 0x06001CE1 RID: 7393
		[MethodImpl(4096)]
		private extern void SetGlobalVector_Injected(int nameID, ref Vector4 value);

		// Token: 0x06001CE2 RID: 7394
		[MethodImpl(4096)]
		private extern void SetGlobalColor_Injected(int nameID, ref Color value);

		// Token: 0x06001CE3 RID: 7395
		[MethodImpl(4096)]
		private extern void SetGlobalMatrix_Injected(int nameID, ref Matrix4x4 value);

		// Token: 0x06001CE4 RID: 7396
		[MethodImpl(4096)]
		private extern void SetViewMatrix_Injected(ref Matrix4x4 view);

		// Token: 0x06001CE5 RID: 7397
		[MethodImpl(4096)]
		private extern void SetProjectionMatrix_Injected(ref Matrix4x4 proj);

		// Token: 0x06001CE6 RID: 7398
		[MethodImpl(4096)]
		private extern void SetViewProjectionMatrices_Injected(ref Matrix4x4 view, ref Matrix4x4 proj);

		// Token: 0x06001CE7 RID: 7399
		[MethodImpl(4096)]
		private extern void IncrementUpdateCount_Injected(ref RenderTargetIdentifier dest);

		// Token: 0x06001CE8 RID: 7400
		[MethodImpl(4096)]
		private extern void SetRenderTargetSingle_Internal_Injected(ref RenderTargetIdentifier rt, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction);

		// Token: 0x06001CE9 RID: 7401
		[MethodImpl(4096)]
		private extern void SetRenderTargetColorDepth_Internal_Injected(ref RenderTargetIdentifier color, ref RenderTargetIdentifier depth, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction);

		// Token: 0x06001CEA RID: 7402
		[MethodImpl(4096)]
		private extern void SetRenderTargetMulti_Internal_Injected(RenderTargetIdentifier[] colors, ref RenderTargetIdentifier depth, RenderBufferLoadAction[] colorLoadActions, RenderBufferStoreAction[] colorStoreActions, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction);

		// Token: 0x06001CEB RID: 7403
		[MethodImpl(4096)]
		private extern void SetRenderTargetColorDepthSubtarget_Injected(ref RenderTargetIdentifier color, ref RenderTargetIdentifier depth, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction, int mipLevel, CubemapFace cubemapFace, int depthSlice);

		// Token: 0x06001CEC RID: 7404
		[MethodImpl(4096)]
		private extern void SetRenderTargetMultiSubtarget_Injected(RenderTargetIdentifier[] colors, ref RenderTargetIdentifier depth, RenderBufferLoadAction[] colorLoadActions, RenderBufferStoreAction[] colorStoreActions, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction, int mipLevel, CubemapFace cubemapFace, int depthSlice);

		// Token: 0x06001CED RID: 7405
		[MethodImpl(4096)]
		private extern void Internal_ProcessVTFeedback_Injected(ref RenderTargetIdentifier rt, IntPtr resolver, int slice, int x, int width, int y, int height, int mip);

		// Token: 0x040009FC RID: 2556
		internal IntPtr m_Ptr;
	}
}
