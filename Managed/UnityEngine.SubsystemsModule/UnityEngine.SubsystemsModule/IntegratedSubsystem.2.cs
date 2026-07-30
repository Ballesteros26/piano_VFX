using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000D RID: 13
	[UsedByNativeCode("Subsystem_TSubsystemDescriptor")]
	public class IntegratedSubsystem<TSubsystemDescriptor> : IntegratedSubsystem where TSubsystemDescriptor : ISubsystemDescriptor
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002900 File Offset: 0x00000B00
		public TSubsystemDescriptor SubsystemDescriptor
		{
			get
			{
				return (TSubsystemDescriptor)((object)this.m_subsystemDescriptor);
			}
		}
	}
}
