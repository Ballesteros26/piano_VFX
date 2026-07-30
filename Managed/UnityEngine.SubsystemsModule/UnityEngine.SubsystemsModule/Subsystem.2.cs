using System;

namespace UnityEngine
{
	// Token: 0x0200000F RID: 15
	public abstract class Subsystem<TSubsystemDescriptor> : Subsystem where TSubsystemDescriptor : ISubsystemDescriptor
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002950 File Offset: 0x00000B50
		public TSubsystemDescriptor SubsystemDescriptor
		{
			get
			{
				return (TSubsystemDescriptor)((object)this.m_subsystemDescriptor);
			}
		}
	}
}
