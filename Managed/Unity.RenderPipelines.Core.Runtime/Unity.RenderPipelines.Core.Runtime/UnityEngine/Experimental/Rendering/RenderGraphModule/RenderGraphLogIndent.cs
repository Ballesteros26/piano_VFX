using System;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x0200000E RID: 14
	internal struct RenderGraphLogIndent : IDisposable
	{
		// Token: 0x0600004D RID: 77 RVA: 0x000030C8 File Offset: 0x000012C8
		public RenderGraphLogIndent(RenderGraphLogger logger, int indentation = 1)
		{
			this.m_Disposed = false;
			this.m_Indentation = indentation;
			this.m_Logger = logger;
			this.m_Logger.IncrementIndentation(this.m_Indentation);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000030F0 File Offset: 0x000012F0
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000030F9 File Offset: 0x000012F9
		private void Dispose(bool disposing)
		{
			if (this.m_Disposed)
			{
				return;
			}
			if (disposing)
			{
				this.m_Logger.DecrementIndentation(this.m_Indentation);
			}
			this.m_Disposed = true;
		}

		// Token: 0x04000038 RID: 56
		private int m_Indentation;

		// Token: 0x04000039 RID: 57
		private RenderGraphLogger m_Logger;

		// Token: 0x0400003A RID: 58
		private bool m_Disposed;
	}
}
