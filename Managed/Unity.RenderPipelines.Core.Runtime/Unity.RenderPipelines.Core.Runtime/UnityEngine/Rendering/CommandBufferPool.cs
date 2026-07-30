using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200001A RID: 26
	public static class CommandBufferPool
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x0000484D File Offset: 0x00002A4D
		public static CommandBuffer Get()
		{
			CommandBuffer commandBuffer = CommandBufferPool.s_BufferPool.Get();
			commandBuffer.name = "Unnamed Command Buffer";
			return commandBuffer;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004864 File Offset: 0x00002A64
		public static CommandBuffer Get(string name)
		{
			CommandBuffer commandBuffer = CommandBufferPool.s_BufferPool.Get();
			commandBuffer.name = name;
			return commandBuffer;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004877 File Offset: 0x00002A77
		public static void Release(CommandBuffer buffer)
		{
			CommandBufferPool.s_BufferPool.Release(buffer);
		}

		// Token: 0x04000088 RID: 136
		private static ObjectPool<CommandBuffer> s_BufferPool = new ObjectPool<CommandBuffer>(null, delegate(CommandBuffer x)
		{
			x.Clear();
		}, true);
	}
}
