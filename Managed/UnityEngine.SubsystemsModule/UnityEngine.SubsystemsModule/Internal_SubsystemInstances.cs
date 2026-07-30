using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000008 RID: 8
	internal static class Internal_SubsystemInstances
	{
		// Token: 0x06000018 RID: 24 RVA: 0x000021EE File Offset: 0x000003EE
		[RequiredByNativeCode]
		internal static void Internal_InitializeManagedInstance(IntPtr ptr, IntegratedSubsystem inst)
		{
			inst.m_Ptr = ptr;
			inst.SetHandle(inst);
			Internal_SubsystemInstances.s_IntegratedSubsystemInstances.Add(inst);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000220C File Offset: 0x0000040C
		[RequiredByNativeCode]
		internal static void Internal_ClearManagedInstances()
		{
			foreach (ISubsystem subsystem in Internal_SubsystemInstances.s_IntegratedSubsystemInstances)
			{
				((IntegratedSubsystem)subsystem).m_Ptr = IntPtr.Zero;
			}
			Internal_SubsystemInstances.s_IntegratedSubsystemInstances.Clear();
			Internal_SubsystemInstances.s_StandaloneSubsystemInstances.Clear();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002284 File Offset: 0x00000484
		[RequiredByNativeCode]
		internal static void Internal_RemoveInstanceByPtr(IntPtr ptr)
		{
			for (int i = Internal_SubsystemInstances.s_IntegratedSubsystemInstances.Count - 1; i >= 0; i--)
			{
				bool flag = ((IntegratedSubsystem)Internal_SubsystemInstances.s_IntegratedSubsystemInstances[i]).m_Ptr == ptr;
				if (flag)
				{
					((IntegratedSubsystem)Internal_SubsystemInstances.s_IntegratedSubsystemInstances[i]).m_Ptr = IntPtr.Zero;
					Internal_SubsystemInstances.s_IntegratedSubsystemInstances.RemoveAt(i);
				}
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000022FC File Offset: 0x000004FC
		internal static IntegratedSubsystem Internal_GetInstanceByPtr(IntPtr ptr)
		{
			foreach (ISubsystem subsystem in Internal_SubsystemInstances.s_IntegratedSubsystemInstances)
			{
				IntegratedSubsystem integratedSubsystem = (IntegratedSubsystem)subsystem;
				bool flag = integratedSubsystem.m_Ptr == ptr;
				if (flag)
				{
					return integratedSubsystem;
				}
			}
			return null;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000236C File Offset: 0x0000056C
		internal static void Internal_AddStandaloneSubsystem(Subsystem inst)
		{
			Internal_SubsystemInstances.s_StandaloneSubsystemInstances.Add(inst);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000237C File Offset: 0x0000057C
		internal static Subsystem Internal_FindStandaloneSubsystemInstanceGivenDescriptor(SubsystemDescriptor descriptor)
		{
			foreach (ISubsystem subsystem in Internal_SubsystemInstances.s_StandaloneSubsystemInstances)
			{
				Subsystem subsystem2 = (Subsystem)subsystem;
				bool flag = subsystem2.m_subsystemDescriptor == descriptor;
				if (flag)
				{
					return subsystem2;
				}
			}
			return null;
		}

		// Token: 0x04000004 RID: 4
		internal static List<ISubsystem> s_IntegratedSubsystemInstances = new List<ISubsystem>();

		// Token: 0x04000005 RID: 5
		internal static List<ISubsystem> s_StandaloneSubsystemInstances = new List<ISubsystem>();
	}
}
