using System;

namespace UnityEngine
{
	// Token: 0x0200000E RID: 14
	public abstract class Subsystem : ISubsystem
	{
		// Token: 0x06000040 RID: 64
		public abstract void Start();

		// Token: 0x06000041 RID: 65
		public abstract void Stop();

		// Token: 0x06000042 RID: 66 RVA: 0x00002928 File Offset: 0x00000B28
		public void Destroy()
		{
			bool flag = Internal_SubsystemInstances.s_StandaloneSubsystemInstances.Remove(this);
			if (flag)
			{
				this.OnDestroy();
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000043 RID: 67
		public abstract bool running { get; }

		// Token: 0x06000044 RID: 68
		protected abstract void OnDestroy();

		// Token: 0x0400000C RID: 12
		internal ISubsystemDescriptor m_subsystemDescriptor;
	}
}
