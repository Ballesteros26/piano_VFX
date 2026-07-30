using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000377 RID: 887
	public struct ScopedSubPass : IDisposable
	{
		// Token: 0x06001E87 RID: 7815 RVA: 0x00034020 File Offset: 0x00032220
		internal ScopedSubPass(ScriptableRenderContext context)
		{
			this.m_Context = context;
		}

		// Token: 0x06001E88 RID: 7816 RVA: 0x0003402C File Offset: 0x0003222C
		public void Dispose()
		{
			try
			{
				this.m_Context.EndSubPass();
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("The ScopedSubPass instance is not valid. This can happen if it was constructed using the default constructor.", ex);
			}
		}

		// Token: 0x04000AF1 RID: 2801
		private ScriptableRenderContext m_Context;
	}
}
