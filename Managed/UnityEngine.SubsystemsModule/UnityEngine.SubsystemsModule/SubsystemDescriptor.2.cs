using System;

namespace UnityEngine
{
	// Token: 0x02000007 RID: 7
	public class SubsystemDescriptor<TSubsystem> : SubsystemDescriptor where TSubsystem : Subsystem
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002160 File Offset: 0x00000360
		internal override ISubsystem CreateImpl()
		{
			return this.Create();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002180 File Offset: 0x00000380
		public TSubsystem Create()
		{
			TSubsystem tsubsystem = Internal_SubsystemInstances.Internal_FindStandaloneSubsystemInstanceGivenDescriptor(this) as TSubsystem;
			bool flag = tsubsystem != null;
			TSubsystem tsubsystem2;
			if (flag)
			{
				tsubsystem2 = tsubsystem;
			}
			else
			{
				TSubsystem tsubsystem3 = Activator.CreateInstance(base.subsystemImplementationType) as TSubsystem;
				tsubsystem3.m_subsystemDescriptor = this;
				Internal_SubsystemInstances.Internal_AddStandaloneSubsystem(tsubsystem3);
				tsubsystem2 = tsubsystem3;
			}
			return tsubsystem2;
		}
	}
}
