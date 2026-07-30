using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200004B RID: 75
	[ExecuteAlways]
	[AddComponentMenu("Rendering/Post-process Debug", 1002)]
	public sealed class PostProcessDebug : MonoBehaviour
	{
		// Token: 0x060000ED RID: 237 RVA: 0x0000997F File Offset: 0x00007B7F
		private void OnEnable()
		{
			this.m_CmdAfterEverything = new CommandBuffer
			{
				name = "Post-processing Debug Overlay"
			};
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00009997 File Offset: 0x00007B97
		private void OnDisable()
		{
			if (this.m_CurrentCamera != null)
			{
				this.m_CurrentCamera.RemoveCommandBuffer(CameraEvent.AfterImageEffects, this.m_CmdAfterEverything);
			}
			this.m_CurrentCamera = null;
			this.m_PreviousPostProcessLayer = null;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000099C8 File Offset: 0x00007BC8
		private void Update()
		{
			this.UpdateStates();
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000099D0 File Offset: 0x00007BD0
		private void Reset()
		{
			this.postProcessLayer = base.GetComponent<PostProcessLayer>();
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000099E0 File Offset: 0x00007BE0
		private void UpdateStates()
		{
			if (this.m_PreviousPostProcessLayer != this.postProcessLayer)
			{
				if (this.m_CurrentCamera != null)
				{
					this.m_CurrentCamera.RemoveCommandBuffer(CameraEvent.AfterImageEffects, this.m_CmdAfterEverything);
					this.m_CurrentCamera = null;
				}
				this.m_PreviousPostProcessLayer = this.postProcessLayer;
				if (this.postProcessLayer != null)
				{
					this.m_CurrentCamera = this.postProcessLayer.GetComponent<Camera>();
					this.m_CurrentCamera.AddCommandBuffer(CameraEvent.AfterImageEffects, this.m_CmdAfterEverything);
				}
			}
			if (this.postProcessLayer == null || !this.postProcessLayer.enabled)
			{
				return;
			}
			if (this.lightMeter)
			{
				this.postProcessLayer.debugLayer.RequestMonitorPass(MonitorType.LightMeter);
			}
			if (this.histogram)
			{
				this.postProcessLayer.debugLayer.RequestMonitorPass(MonitorType.Histogram);
			}
			if (this.waveform)
			{
				this.postProcessLayer.debugLayer.RequestMonitorPass(MonitorType.Waveform);
			}
			if (this.vectorscope)
			{
				this.postProcessLayer.debugLayer.RequestMonitorPass(MonitorType.Vectorscope);
			}
			this.postProcessLayer.debugLayer.RequestDebugOverlay(this.debugOverlay);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00009AFC File Offset: 0x00007CFC
		private void OnPostRender()
		{
			this.m_CmdAfterEverything.Clear();
			if (this.postProcessLayer == null || !this.postProcessLayer.enabled || !this.postProcessLayer.debugLayer.debugOverlayActive)
			{
				return;
			}
			this.m_CmdAfterEverything.Blit(this.postProcessLayer.debugLayer.debugOverlayTarget, BuiltinRenderTextureType.CameraTarget);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00009B64 File Offset: 0x00007D64
		private void OnGUI()
		{
			if (this.postProcessLayer == null || !this.postProcessLayer.enabled)
			{
				return;
			}
			RenderTexture.active = null;
			Rect rect = new Rect(5f, 5f, 0f, 0f);
			PostProcessDebugLayer debugLayer = this.postProcessLayer.debugLayer;
			this.DrawMonitor(ref rect, debugLayer.lightMeter, this.lightMeter);
			this.DrawMonitor(ref rect, debugLayer.histogram, this.histogram);
			this.DrawMonitor(ref rect, debugLayer.waveform, this.waveform);
			this.DrawMonitor(ref rect, debugLayer.vectorscope, this.vectorscope);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00009C0C File Offset: 0x00007E0C
		private void DrawMonitor(ref Rect rect, Monitor monitor, bool enabled)
		{
			if (!enabled || monitor.output == null)
			{
				return;
			}
			rect.width = (float)monitor.output.width;
			rect.height = (float)monitor.output.height;
			GUI.DrawTexture(rect, monitor.output);
			rect.x += (float)monitor.output.width + 5f;
		}

		// Token: 0x04000104 RID: 260
		public PostProcessLayer postProcessLayer;

		// Token: 0x04000105 RID: 261
		private PostProcessLayer m_PreviousPostProcessLayer;

		// Token: 0x04000106 RID: 262
		public bool lightMeter;

		// Token: 0x04000107 RID: 263
		public bool histogram;

		// Token: 0x04000108 RID: 264
		public bool waveform;

		// Token: 0x04000109 RID: 265
		public bool vectorscope;

		// Token: 0x0400010A RID: 266
		public DebugOverlay debugOverlay;

		// Token: 0x0400010B RID: 267
		private Camera m_CurrentCamera;

		// Token: 0x0400010C RID: 268
		private CommandBuffer m_CmdAfterEverything;
	}
}
