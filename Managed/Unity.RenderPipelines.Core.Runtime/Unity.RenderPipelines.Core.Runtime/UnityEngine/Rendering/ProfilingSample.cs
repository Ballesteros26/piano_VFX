using System;
using UnityEngine.Profiling;

namespace UnityEngine.Rendering
{
	// Token: 0x0200003D RID: 61
	[Obsolete("Please use ProfilingScope")]
	public struct ProfilingSample : IDisposable
	{
		// Token: 0x0600017C RID: 380 RVA: 0x000078E0 File Offset: 0x00005AE0
		public ProfilingSample(CommandBuffer cmd, string name, CustomSampler sampler = null)
		{
			this.m_Cmd = cmd;
			this.m_Name = name;
			this.m_Disposed = false;
			if (cmd != null && name != "")
			{
				cmd.BeginSample(name);
			}
			this.m_Sampler = sampler;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00007915 File Offset: 0x00005B15
		public ProfilingSample(CommandBuffer cmd, string format, object arg)
		{
			this = new ProfilingSample(cmd, string.Format(format, arg), null);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00007926 File Offset: 0x00005B26
		public ProfilingSample(CommandBuffer cmd, string format, params object[] args)
		{
			this = new ProfilingSample(cmd, string.Format(format, args), null);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00007937 File Offset: 0x00005B37
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00007940 File Offset: 0x00005B40
		private void Dispose(bool disposing)
		{
			if (this.m_Disposed)
			{
				return;
			}
			if (disposing && this.m_Cmd != null && this.m_Name != "")
			{
				this.m_Cmd.EndSample(this.m_Name);
			}
			this.m_Disposed = true;
		}

		// Token: 0x04000102 RID: 258
		private readonly CommandBuffer m_Cmd;

		// Token: 0x04000103 RID: 259
		private readonly string m_Name;

		// Token: 0x04000104 RID: 260
		private bool m_Disposed;

		// Token: 0x04000105 RID: 261
		private CustomSampler m_Sampler;
	}
}
