using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000376 RID: 886
	public struct ScopedRenderPass : IDisposable
	{
		// Token: 0x06001E85 RID: 7813 RVA: 0x00033FD9 File Offset: 0x000321D9
		internal ScopedRenderPass(ScriptableRenderContext context)
		{
			this.m_Context = context;
		}

		// Token: 0x06001E86 RID: 7814 RVA: 0x00033FE4 File Offset: 0x000321E4
		public void Dispose()
		{
			try
			{
				this.m_Context.EndRenderPass();
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("The ScopedRenderPass instance is not valid. This can happen if it was constructed using the default constructor.", ex);
			}
		}

		// Token: 0x04000AF0 RID: 2800
		private ScriptableRenderContext m_Context;
	}
}
