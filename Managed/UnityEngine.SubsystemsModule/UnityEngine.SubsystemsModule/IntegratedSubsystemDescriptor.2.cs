using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000006 RID: 6
	[NativeType(Header = "Modules/Subsystems/SubsystemDescriptor.h")]
	[UsedByNativeCode("SubsystemDescriptor")]
	[StructLayout(0)]
	public class IntegratedSubsystemDescriptor<TSubsystem> : IntegratedSubsystemDescriptor where TSubsystem : IntegratedSubsystem
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000020F0 File Offset: 0x000002F0
		internal override ISubsystem CreateImpl()
		{
			return this.Create();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002110 File Offset: 0x00000310
		public TSubsystem Create()
		{
			IntPtr intPtr = Internal_SubsystemDescriptors.Create(this.m_Ptr);
			TSubsystem tsubsystem = (TSubsystem)((object)Internal_SubsystemInstances.Internal_GetInstanceByPtr(intPtr));
			bool flag = tsubsystem != null;
			if (flag)
			{
				tsubsystem.m_subsystemDescriptor = this;
			}
			return tsubsystem;
		}
	}
}
