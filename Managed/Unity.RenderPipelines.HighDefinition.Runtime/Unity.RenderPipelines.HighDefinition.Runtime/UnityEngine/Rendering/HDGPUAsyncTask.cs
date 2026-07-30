using System;
using UnityEngine.Rendering.HighDefinition;

namespace UnityEngine.Rendering
{
	// Token: 0x02000018 RID: 24
	internal struct HDGPUAsyncTask
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00003583 File Offset: 0x00001783
		public HDGPUAsyncTask(string taskName, ComputeQueueType queueType = ComputeQueueType.Background)
		{
			this.m_StartFence = default(GraphicsFence);
			this.m_EndFence = default(GraphicsFence);
			this.m_TaskName = taskName;
			this.m_QueueType = queueType;
			this.m_TaskStage = HDGPUAsyncTask.AsyncTaskStage.NotTriggered;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000035B2 File Offset: 0x000017B2
		private void PushStartFenceAndExecuteCmdBuffer(CommandBuffer cmd, ScriptableRenderContext renderContext)
		{
			this.m_StartFence = cmd.CreateAsyncGraphicsFence();
			renderContext.ExecuteCommandBuffer(cmd);
			cmd.Clear();
			this.m_TaskStage = HDGPUAsyncTask.AsyncTaskStage.StartFenceCreated;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000035D8 File Offset: 0x000017D8
		public void Start(CommandBuffer cmd, in HDGPUAsyncTaskParams asyncParams, Action<CommandBuffer, HDGPUAsyncTaskParams> asyncTask, bool pushStartFence = true)
		{
			if (pushStartFence)
			{
				this.PushStartFenceAndExecuteCmdBuffer(cmd, asyncParams.renderContext);
			}
			CommandBuffer commandBuffer = CommandBufferPool.Get(this.m_TaskName);
			commandBuffer.SetExecutionFlags(CommandBufferExecutionFlags.AsyncCompute);
			if (pushStartFence)
			{
				commandBuffer.WaitOnAsyncGraphicsFence(this.m_StartFence);
			}
			asyncTask(commandBuffer, asyncParams);
			this.m_EndFence = commandBuffer.CreateAsyncGraphicsFence();
			ScriptableRenderContext renderContext = asyncParams.renderContext;
			renderContext.ExecuteCommandBufferAsync(commandBuffer, this.m_QueueType);
			CommandBufferPool.Release(commandBuffer);
			this.m_TaskStage = HDGPUAsyncTask.AsyncTaskStage.AsyncCmdEnqueued;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003654 File Offset: 0x00001854
		public void EndWithPostWork(CommandBuffer cmd, HDCamera hdCamera, Action<CommandBuffer, HDCamera> postWork)
		{
			cmd.WaitOnAsyncGraphicsFence(this.m_EndFence);
			postWork(cmd, hdCamera);
			this.m_TaskStage = HDGPUAsyncTask.AsyncTaskStage.TaskCompleted;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003671 File Offset: 0x00001871
		public void End(CommandBuffer cmd, HDCamera hdCamera)
		{
			this.EndWithPostWork(cmd, hdCamera, delegate(CommandBuffer _1, HDCamera _2)
			{
			});
		}

		// Token: 0x0400006A RID: 106
		private GraphicsFence m_StartFence;

		// Token: 0x0400006B RID: 107
		private GraphicsFence m_EndFence;

		// Token: 0x0400006C RID: 108
		private string m_TaskName;

		// Token: 0x0400006D RID: 109
		private ComputeQueueType m_QueueType;

		// Token: 0x0400006E RID: 110
		private HDGPUAsyncTask.AsyncTaskStage m_TaskStage;

		// Token: 0x02000189 RID: 393
		private enum AsyncTaskStage
		{
			// Token: 0x0400109F RID: 4255
			NotTriggered,
			// Token: 0x040010A0 RID: 4256
			StartFenceCreated,
			// Token: 0x040010A1 RID: 4257
			AsyncCmdEnqueued,
			// Token: 0x040010A2 RID: 4258
			TaskCompleted
		}
	}
}
