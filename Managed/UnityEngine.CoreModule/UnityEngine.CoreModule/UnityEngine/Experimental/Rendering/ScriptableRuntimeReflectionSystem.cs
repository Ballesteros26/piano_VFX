using System;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x020003CF RID: 975
	public abstract class ScriptableRuntimeReflectionSystem : IScriptableRuntimeReflectionSystem, IDisposable
	{
		// Token: 0x060021D1 RID: 8657 RVA: 0x0003961C File Offset: 0x0003781C
		public virtual bool TickRealtimeProbes()
		{
			return false;
		}

		// Token: 0x060021D2 RID: 8658 RVA: 0x00002EC3 File Offset: 0x000010C3
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x060021D3 RID: 8659 RVA: 0x0003962F File Offset: 0x0003782F
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
