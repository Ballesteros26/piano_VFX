using System;
using System.Diagnostics;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000371 RID: 881
	public static class RenderPipelineManager
	{
		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001E3D RID: 7741 RVA: 0x000333F5 File Offset: 0x000315F5
		// (set) Token: 0x06001E3E RID: 7742 RVA: 0x000333FC File Offset: 0x000315FC
		public static RenderPipeline currentPipeline { get; private set; }

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06001E3F RID: 7743 RVA: 0x00033404 File Offset: 0x00031604
		// (remove) Token: 0x06001E40 RID: 7744 RVA: 0x00033438 File Offset: 0x00031638
		[field: DebuggerBrowsable(0)]
		public static event Action<ScriptableRenderContext, Camera[]> beginFrameRendering;

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06001E41 RID: 7745 RVA: 0x0003346C File Offset: 0x0003166C
		// (remove) Token: 0x06001E42 RID: 7746 RVA: 0x000334A0 File Offset: 0x000316A0
		[field: DebuggerBrowsable(0)]
		public static event Action<ScriptableRenderContext, Camera> beginCameraRendering;

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06001E43 RID: 7747 RVA: 0x000334D4 File Offset: 0x000316D4
		// (remove) Token: 0x06001E44 RID: 7748 RVA: 0x00033508 File Offset: 0x00031708
		[field: DebuggerBrowsable(0)]
		public static event Action<ScriptableRenderContext, Camera[]> endFrameRendering;

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06001E45 RID: 7749 RVA: 0x0003353C File Offset: 0x0003173C
		// (remove) Token: 0x06001E46 RID: 7750 RVA: 0x00033570 File Offset: 0x00031770
		[field: DebuggerBrowsable(0)]
		public static event Action<ScriptableRenderContext, Camera> endCameraRendering;

		// Token: 0x06001E47 RID: 7751 RVA: 0x000335A3 File Offset: 0x000317A3
		internal static void BeginFrameRendering(ScriptableRenderContext context, Camera[] cameras)
		{
			Action<ScriptableRenderContext, Camera[]> action = RenderPipelineManager.beginFrameRendering;
			if (action != null)
			{
				action.Invoke(context, cameras);
			}
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x000335B9 File Offset: 0x000317B9
		internal static void BeginCameraRendering(ScriptableRenderContext context, Camera camera)
		{
			Action<ScriptableRenderContext, Camera> action = RenderPipelineManager.beginCameraRendering;
			if (action != null)
			{
				action.Invoke(context, camera);
			}
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x000335CF File Offset: 0x000317CF
		internal static void EndFrameRendering(ScriptableRenderContext context, Camera[] cameras)
		{
			Action<ScriptableRenderContext, Camera[]> action = RenderPipelineManager.endFrameRendering;
			if (action != null)
			{
				action.Invoke(context, cameras);
			}
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x000335E5 File Offset: 0x000317E5
		internal static void EndCameraRendering(ScriptableRenderContext context, Camera camera)
		{
			Action<ScriptableRenderContext, Camera> action = RenderPipelineManager.endCameraRendering;
			if (action != null)
			{
				action.Invoke(context, camera);
			}
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x000335FC File Offset: 0x000317FC
		[RequiredByNativeCode]
		internal static void CleanupRenderPipeline()
		{
			bool flag = RenderPipelineManager.currentPipeline != null && !RenderPipelineManager.currentPipeline.disposed;
			if (flag)
			{
				RenderPipelineManager.currentPipeline.Dispose();
				RenderPipelineManager.s_CurrentPipelineAsset = null;
				RenderPipelineManager.currentPipeline = null;
				SupportedRenderingFeatures.active = new SupportedRenderingFeatures();
			}
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x0003364C File Offset: 0x0003184C
		private static void GetCameras(ScriptableRenderContext context)
		{
			int numberOfCameras = context.GetNumberOfCameras();
			bool flag = numberOfCameras != RenderPipelineManager.s_CameraCapacity;
			if (flag)
			{
				Array.Resize<Camera>(ref RenderPipelineManager.s_Cameras, numberOfCameras);
				RenderPipelineManager.s_CameraCapacity = numberOfCameras;
			}
			for (int i = 0; i < numberOfCameras; i++)
			{
				RenderPipelineManager.s_Cameras[i] = context.GetCamera(i);
			}
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x000336A8 File Offset: 0x000318A8
		[RequiredByNativeCode]
		private static void DoRenderLoop_Internal(RenderPipelineAsset pipe, IntPtr loopPtr)
		{
			RenderPipelineManager.PrepareRenderPipeline(pipe);
			bool flag = RenderPipelineManager.currentPipeline == null;
			if (!flag)
			{
				ScriptableRenderContext scriptableRenderContext = new ScriptableRenderContext(loopPtr);
				Array.Clear(RenderPipelineManager.s_Cameras, 0, RenderPipelineManager.s_Cameras.Length);
				RenderPipelineManager.GetCameras(scriptableRenderContext);
				RenderPipelineManager.currentPipeline.InternalRender(scriptableRenderContext, RenderPipelineManager.s_Cameras);
				Array.Clear(RenderPipelineManager.s_Cameras, 0, RenderPipelineManager.s_Cameras.Length);
			}
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x00033714 File Offset: 0x00031914
		private static void PrepareRenderPipeline(RenderPipelineAsset pipelineAsset)
		{
			bool flag = RenderPipelineManager.s_CurrentPipelineAsset != pipelineAsset;
			if (flag)
			{
				RenderPipelineManager.CleanupRenderPipeline();
				RenderPipelineManager.s_CurrentPipelineAsset = pipelineAsset;
			}
			bool flag2 = RenderPipelineManager.s_CurrentPipelineAsset != null && (RenderPipelineManager.currentPipeline == null || RenderPipelineManager.currentPipeline.disposed);
			if (flag2)
			{
				RenderPipelineManager.currentPipeline = RenderPipelineManager.s_CurrentPipelineAsset.InternalCreatePipeline();
			}
		}

		// Token: 0x04000ACD RID: 2765
		private static RenderPipelineAsset s_CurrentPipelineAsset;

		// Token: 0x04000ACE RID: 2766
		private static Camera[] s_Cameras = new Camera[0];

		// Token: 0x04000ACF RID: 2767
		private static int s_CameraCapacity = 0;
	}
}
