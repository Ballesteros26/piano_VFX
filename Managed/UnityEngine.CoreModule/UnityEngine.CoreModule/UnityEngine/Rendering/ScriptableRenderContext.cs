using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x02000378 RID: 888
	[NativeType("Runtime/Graphics/ScriptableRenderLoop/ScriptableRenderContext.h")]
	[NativeHeader("Modules/UI/CanvasManager.h")]
	[NativeHeader("Runtime/Graphics/ScriptableRenderLoop/ScriptableDrawRenderersUtility.h")]
	[NativeHeader("Runtime/Export/RenderPipeline/ScriptableRenderContext.bindings.h")]
	[NativeHeader("Modules/UI/Canvas.h")]
	[NativeHeader("Runtime/Export/RenderPipeline/ScriptableRenderPipeline.bindings.h")]
	public struct ScriptableRenderContext : IEquatable<ScriptableRenderContext>
	{
		// Token: 0x06001E89 RID: 7817
		[FreeFunction("ScriptableRenderContext::BeginRenderPass")]
		[MethodImpl(4096)]
		private static extern void BeginRenderPass_Internal(IntPtr self, int width, int height, int samples, IntPtr colors, int colorCount, int depthAttachmentIndex);

		// Token: 0x06001E8A RID: 7818
		[FreeFunction("ScriptableRenderContext::BeginSubPass")]
		[MethodImpl(4096)]
		private static extern void BeginSubPass_Internal(IntPtr self, IntPtr colors, int colorCount, IntPtr inputs, int inputCount, bool isDepthReadOnly);

		// Token: 0x06001E8B RID: 7819
		[FreeFunction("ScriptableRenderContext::EndSubPass")]
		[MethodImpl(4096)]
		private static extern void EndSubPass_Internal(IntPtr self);

		// Token: 0x06001E8C RID: 7820
		[FreeFunction("ScriptableRenderContext::EndRenderPass")]
		[MethodImpl(4096)]
		private static extern void EndRenderPass_Internal(IntPtr self);

		// Token: 0x06001E8D RID: 7821 RVA: 0x00034068 File Offset: 0x00032268
		[FreeFunction("ScriptableRenderPipeline_Bindings::Internal_Cull")]
		private static void Internal_Cull(ref ScriptableCullingParameters parameters, ScriptableRenderContext renderLoop, IntPtr results)
		{
			ScriptableRenderContext.Internal_Cull_Injected(ref parameters, ref renderLoop, results);
		}

		// Token: 0x06001E8E RID: 7822
		[FreeFunction("InitializeSortSettings")]
		[MethodImpl(4096)]
		internal static extern void InitializeSortSettings(Camera camera, out SortingSettings sortingSettings);

		// Token: 0x06001E8F RID: 7823 RVA: 0x00034073 File Offset: 0x00032273
		private void Submit_Internal()
		{
			ScriptableRenderContext.Submit_Internal_Injected(ref this);
		}

		// Token: 0x06001E90 RID: 7824 RVA: 0x0003407B File Offset: 0x0003227B
		private int GetNumberOfCameras_Internal()
		{
			return ScriptableRenderContext.GetNumberOfCameras_Internal_Injected(ref this);
		}

		// Token: 0x06001E91 RID: 7825 RVA: 0x00034083 File Offset: 0x00032283
		private Camera GetCamera_Internal(int index)
		{
			return ScriptableRenderContext.GetCamera_Internal_Injected(ref this, index);
		}

		// Token: 0x06001E92 RID: 7826 RVA: 0x0003408C File Offset: 0x0003228C
		private void DrawRenderers_Internal(IntPtr cullResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, IntPtr renderTypes, IntPtr stateBlocks, int stateCount)
		{
			ScriptableRenderContext.DrawRenderers_Internal_Injected(ref this, cullResults, ref drawingSettings, ref filteringSettings, renderTypes, stateBlocks, stateCount);
		}

		// Token: 0x06001E93 RID: 7827 RVA: 0x0003409D File Offset: 0x0003229D
		private void DrawShadows_Internal(IntPtr shadowDrawingSettings)
		{
			ScriptableRenderContext.DrawShadows_Internal_Injected(ref this, shadowDrawingSettings);
		}

		// Token: 0x06001E94 RID: 7828 RVA: 0x000340A6 File Offset: 0x000322A6
		[NativeThrows]
		private void ExecuteCommandBuffer_Internal(CommandBuffer commandBuffer)
		{
			ScriptableRenderContext.ExecuteCommandBuffer_Internal_Injected(ref this, commandBuffer);
		}

		// Token: 0x06001E95 RID: 7829 RVA: 0x000340AF File Offset: 0x000322AF
		[NativeThrows]
		private void ExecuteCommandBufferAsync_Internal(CommandBuffer commandBuffer, ComputeQueueType queueType)
		{
			ScriptableRenderContext.ExecuteCommandBufferAsync_Internal_Injected(ref this, commandBuffer, queueType);
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x000340B9 File Offset: 0x000322B9
		private void SetupCameraProperties_Internal(Camera camera, bool stereoSetup, int eye)
		{
			ScriptableRenderContext.SetupCameraProperties_Internal_Injected(ref this, camera, stereoSetup, eye);
		}

		// Token: 0x06001E97 RID: 7831 RVA: 0x000340C4 File Offset: 0x000322C4
		private void StereoEndRender_Internal(Camera camera, int eye, bool isFinalPass)
		{
			ScriptableRenderContext.StereoEndRender_Internal_Injected(ref this, camera, eye, isFinalPass);
		}

		// Token: 0x06001E98 RID: 7832 RVA: 0x000340CF File Offset: 0x000322CF
		private void StartMultiEye_Internal(Camera camera, int eye)
		{
			ScriptableRenderContext.StartMultiEye_Internal_Injected(ref this, camera, eye);
		}

		// Token: 0x06001E99 RID: 7833 RVA: 0x000340D9 File Offset: 0x000322D9
		private void StopMultiEye_Internal(Camera camera)
		{
			ScriptableRenderContext.StopMultiEye_Internal_Injected(ref this, camera);
		}

		// Token: 0x06001E9A RID: 7834 RVA: 0x000340E2 File Offset: 0x000322E2
		private void DrawSkybox_Internal(Camera camera)
		{
			ScriptableRenderContext.DrawSkybox_Internal_Injected(ref this, camera);
		}

		// Token: 0x06001E9B RID: 7835 RVA: 0x000340EB File Offset: 0x000322EB
		private void InvokeOnRenderObjectCallback_Internal()
		{
			ScriptableRenderContext.InvokeOnRenderObjectCallback_Internal_Injected(ref this);
		}

		// Token: 0x06001E9C RID: 7836 RVA: 0x000340F3 File Offset: 0x000322F3
		private void DrawGizmos_Internal(Camera camera, GizmoSubset gizmoSubset)
		{
			ScriptableRenderContext.DrawGizmos_Internal_Injected(ref this, camera, gizmoSubset);
		}

		// Token: 0x06001E9D RID: 7837 RVA: 0x000340FD File Offset: 0x000322FD
		private void DrawUIOverlay_Internal(Camera camera)
		{
			ScriptableRenderContext.DrawUIOverlay_Internal_Injected(ref this, camera);
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x00034108 File Offset: 0x00032308
		internal IntPtr Internal_GetPtr()
		{
			return this.m_Ptr;
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x00034120 File Offset: 0x00032320
		internal ScriptableRenderContext(IntPtr ptr)
		{
			this.m_Ptr = ptr;
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x0003412A File Offset: 0x0003232A
		public void BeginRenderPass(int width, int height, int samples, NativeArray<AttachmentDescriptor> attachments, int depthAttachmentIndex = -1)
		{
			ScriptableRenderContext.BeginRenderPass_Internal(this.m_Ptr, width, height, samples, (IntPtr)attachments.GetUnsafeReadOnlyPtr<AttachmentDescriptor>(), attachments.Length, depthAttachmentIndex);
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x00034154 File Offset: 0x00032354
		public ScopedRenderPass BeginScopedRenderPass(int width, int height, int samples, NativeArray<AttachmentDescriptor> attachments, int depthAttachmentIndex = -1)
		{
			this.BeginRenderPass(width, height, samples, attachments, depthAttachmentIndex);
			return new ScopedRenderPass(this);
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x0003417F File Offset: 0x0003237F
		public void BeginSubPass(NativeArray<int> colors, NativeArray<int> inputs, bool isDepthReadOnly = false)
		{
			ScriptableRenderContext.BeginSubPass_Internal(this.m_Ptr, (IntPtr)colors.GetUnsafeReadOnlyPtr<int>(), colors.Length, (IntPtr)inputs.GetUnsafeReadOnlyPtr<int>(), inputs.Length, isDepthReadOnly);
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x000341B3 File Offset: 0x000323B3
		public void BeginSubPass(NativeArray<int> colors, bool isDepthReadOnly = false)
		{
			ScriptableRenderContext.BeginSubPass_Internal(this.m_Ptr, (IntPtr)colors.GetUnsafeReadOnlyPtr<int>(), colors.Length, IntPtr.Zero, 0, isDepthReadOnly);
		}

		// Token: 0x06001EA4 RID: 7844 RVA: 0x000341DC File Offset: 0x000323DC
		public ScopedSubPass BeginScopedSubPass(NativeArray<int> colors, NativeArray<int> inputs, bool isDepthReadOnly = false)
		{
			this.BeginSubPass(colors, inputs, isDepthReadOnly);
			return new ScopedSubPass(this);
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x00034204 File Offset: 0x00032404
		public ScopedSubPass BeginScopedSubPass(NativeArray<int> colors, bool isDepthReadOnly = false)
		{
			this.BeginSubPass(colors, isDepthReadOnly);
			return new ScopedSubPass(this);
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x0003422A File Offset: 0x0003242A
		public void EndSubPass()
		{
			ScriptableRenderContext.EndSubPass_Internal(this.m_Ptr);
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x00034239 File Offset: 0x00032439
		public void EndRenderPass()
		{
			ScriptableRenderContext.EndRenderPass_Internal(this.m_Ptr);
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x00034248 File Offset: 0x00032448
		public void Submit()
		{
			this.Submit_Internal();
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x00034254 File Offset: 0x00032454
		internal int GetNumberOfCameras()
		{
			return this.GetNumberOfCameras_Internal();
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x0003426C File Offset: 0x0003246C
		internal Camera GetCamera(int index)
		{
			return this.GetCamera_Internal(index);
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x00034285 File Offset: 0x00032485
		public void DrawRenderers(CullingResults cullingResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings)
		{
			this.DrawRenderers_Internal(cullingResults.ptr, ref drawingSettings, ref filteringSettings, IntPtr.Zero, IntPtr.Zero, 0);
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x000342A4 File Offset: 0x000324A4
		public unsafe void DrawRenderers(CullingResults cullingResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, ref RenderStateBlock stateBlock)
		{
			ShaderTagId shaderTagId = default(ShaderTagId);
			fixed (RenderStateBlock* ptr = &stateBlock)
			{
				RenderStateBlock* ptr2 = ptr;
				this.DrawRenderers_Internal(cullingResults.ptr, ref drawingSettings, ref filteringSettings, (IntPtr)((void*)(&shaderTagId)), (IntPtr)((void*)ptr2), 1);
			}
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x000342E4 File Offset: 0x000324E4
		public void DrawRenderers(CullingResults cullingResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, NativeArray<ShaderTagId> renderTypes, NativeArray<RenderStateBlock> stateBlocks)
		{
			bool flag = renderTypes.Length != stateBlocks.Length;
			if (flag)
			{
				throw new ArgumentException(string.Format("Arrays {0} and {1} should have same length, but {2} had length {3} while {4} had length {5}.", new object[] { "renderTypes", "stateBlocks", "renderTypes", renderTypes.Length, "stateBlocks", stateBlocks.Length }));
			}
			this.DrawRenderers_Internal(cullingResults.ptr, ref drawingSettings, ref filteringSettings, (IntPtr)renderTypes.GetUnsafeReadOnlyPtr<ShaderTagId>(), (IntPtr)stateBlocks.GetUnsafeReadOnlyPtr<RenderStateBlock>(), renderTypes.Length);
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x0003438C File Offset: 0x0003258C
		public unsafe void DrawShadows(ref ShadowDrawingSettings settings)
		{
			fixed (ShadowDrawingSettings* ptr = &settings)
			{
				ShadowDrawingSettings* ptr2 = ptr;
				this.DrawShadows_Internal((IntPtr)((void*)ptr2));
			}
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x000343B4 File Offset: 0x000325B4
		public void ExecuteCommandBuffer(CommandBuffer commandBuffer)
		{
			bool flag = commandBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("commandBuffer");
			}
			this.ExecuteCommandBuffer_Internal(commandBuffer);
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x000343E0 File Offset: 0x000325E0
		public void ExecuteCommandBufferAsync(CommandBuffer commandBuffer, ComputeQueueType queueType)
		{
			bool flag = commandBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("commandBuffer");
			}
			this.ExecuteCommandBufferAsync_Internal(commandBuffer, queueType);
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x0003440A File Offset: 0x0003260A
		public void SetupCameraProperties(Camera camera, bool stereoSetup = false)
		{
			this.SetupCameraProperties(camera, stereoSetup, 0);
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x00034417 File Offset: 0x00032617
		public void SetupCameraProperties(Camera camera, bool stereoSetup, int eye)
		{
			this.SetupCameraProperties_Internal(camera, stereoSetup, eye);
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x00034424 File Offset: 0x00032624
		public void StereoEndRender(Camera camera)
		{
			this.StereoEndRender(camera, 0, true);
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x00034431 File Offset: 0x00032631
		public void StereoEndRender(Camera camera, int eye)
		{
			this.StereoEndRender(camera, eye, true);
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x0003443E File Offset: 0x0003263E
		public void StereoEndRender(Camera camera, int eye, bool isFinalPass)
		{
			this.StereoEndRender_Internal(camera, eye, isFinalPass);
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x0003444B File Offset: 0x0003264B
		public void StartMultiEye(Camera camera)
		{
			this.StartMultiEye(camera, 0);
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x00034457 File Offset: 0x00032657
		public void StartMultiEye(Camera camera, int eye)
		{
			this.StartMultiEye_Internal(camera, eye);
		}

		// Token: 0x06001EB8 RID: 7864 RVA: 0x00034463 File Offset: 0x00032663
		public void StopMultiEye(Camera camera)
		{
			this.StopMultiEye_Internal(camera);
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x0003446E File Offset: 0x0003266E
		public void DrawSkybox(Camera camera)
		{
			this.DrawSkybox_Internal(camera);
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x00034479 File Offset: 0x00032679
		public void InvokeOnRenderObjectCallback()
		{
			this.InvokeOnRenderObjectCallback_Internal();
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x00034483 File Offset: 0x00032683
		public void DrawGizmos(Camera camera, GizmoSubset gizmoSubset)
		{
			this.DrawGizmos_Internal(camera, gizmoSubset);
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x0003448F File Offset: 0x0003268F
		public void DrawUIOverlay(Camera camera)
		{
			this.DrawUIOverlay_Internal(camera);
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x0003449C File Offset: 0x0003269C
		public unsafe CullingResults Cull(ref ScriptableCullingParameters parameters)
		{
			CullingResults cullingResults = default(CullingResults);
			ScriptableRenderContext.Internal_Cull(ref parameters, this, (IntPtr)((void*)(&cullingResults)));
			return cullingResults;
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x00002EC3 File Offset: 0x000010C3
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		internal void Validate()
		{
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x000344CC File Offset: 0x000326CC
		public bool Equals(ScriptableRenderContext other)
		{
			return this.m_Ptr.Equals(other.m_Ptr);
		}

		// Token: 0x06001EC0 RID: 7872 RVA: 0x000344F4 File Offset: 0x000326F4
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is ScriptableRenderContext && this.Equals((ScriptableRenderContext)obj);
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x0003452C File Offset: 0x0003272C
		public override int GetHashCode()
		{
			return this.m_Ptr.GetHashCode();
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x0003454C File Offset: 0x0003274C
		public static bool operator ==(ScriptableRenderContext left, ScriptableRenderContext right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x00034568 File Offset: 0x00032768
		public static bool operator !=(ScriptableRenderContext left, ScriptableRenderContext right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06001EC4 RID: 7876
		[MethodImpl(4096)]
		private static extern void Internal_Cull_Injected(ref ScriptableCullingParameters parameters, ref ScriptableRenderContext renderLoop, IntPtr results);

		// Token: 0x06001EC5 RID: 7877
		[MethodImpl(4096)]
		private static extern void Submit_Internal_Injected(ref ScriptableRenderContext _unity_self);

		// Token: 0x06001EC6 RID: 7878
		[MethodImpl(4096)]
		private static extern int GetNumberOfCameras_Internal_Injected(ref ScriptableRenderContext _unity_self);

		// Token: 0x06001EC7 RID: 7879
		[MethodImpl(4096)]
		private static extern Camera GetCamera_Internal_Injected(ref ScriptableRenderContext _unity_self, int index);

		// Token: 0x06001EC8 RID: 7880
		[MethodImpl(4096)]
		private static extern void DrawRenderers_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr cullResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, IntPtr renderTypes, IntPtr stateBlocks, int stateCount);

		// Token: 0x06001EC9 RID: 7881
		[MethodImpl(4096)]
		private static extern void DrawShadows_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr shadowDrawingSettings);

		// Token: 0x06001ECA RID: 7882
		[MethodImpl(4096)]
		private static extern void ExecuteCommandBuffer_Internal_Injected(ref ScriptableRenderContext _unity_self, CommandBuffer commandBuffer);

		// Token: 0x06001ECB RID: 7883
		[MethodImpl(4096)]
		private static extern void ExecuteCommandBufferAsync_Internal_Injected(ref ScriptableRenderContext _unity_self, CommandBuffer commandBuffer, ComputeQueueType queueType);

		// Token: 0x06001ECC RID: 7884
		[MethodImpl(4096)]
		private static extern void SetupCameraProperties_Internal_Injected(ref ScriptableRenderContext _unity_self, Camera camera, bool stereoSetup, int eye);

		// Token: 0x06001ECD RID: 7885
		[MethodImpl(4096)]
		private static extern void StereoEndRender_Internal_Injected(ref ScriptableRenderContext _unity_self, Camera camera, int eye, bool isFinalPass);

		// Token: 0x06001ECE RID: 7886
		[MethodImpl(4096)]
		private static extern void StartMultiEye_Internal_Injected(ref ScriptableRenderContext _unity_self, Camera camera, int eye);

		// Token: 0x06001ECF RID: 7887
		[MethodImpl(4096)]
		private static extern void StopMultiEye_Internal_Injected(ref ScriptableRenderContext _unity_self, Camera camera);

		// Token: 0x06001ED0 RID: 7888
		[MethodImpl(4096)]
		private static extern void DrawSkybox_Internal_Injected(ref ScriptableRenderContext _unity_self, Camera camera);

		// Token: 0x06001ED1 RID: 7889
		[MethodImpl(4096)]
		private static extern void InvokeOnRenderObjectCallback_Internal_Injected(ref ScriptableRenderContext _unity_self);

		// Token: 0x06001ED2 RID: 7890
		[MethodImpl(4096)]
		private static extern void DrawGizmos_Internal_Injected(ref ScriptableRenderContext _unity_self, Camera camera, GizmoSubset gizmoSubset);

		// Token: 0x06001ED3 RID: 7891
		[MethodImpl(4096)]
		private static extern void DrawUIOverlay_Internal_Injected(ref ScriptableRenderContext _unity_self, Camera camera);

		// Token: 0x04000AF2 RID: 2802
		private IntPtr m_Ptr;
	}
}
