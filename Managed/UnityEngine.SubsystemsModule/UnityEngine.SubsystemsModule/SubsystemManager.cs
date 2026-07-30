using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000A RID: 10
	[NativeType(Header = "Modules/Subsystems/SubsystemManager.h")]
	public static class SubsystemManager
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00002512 File Offset: 0x00000712
		static SubsystemManager()
		{
			SubsystemManager.StaticConstructScriptingClassMap();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000251C File Offset: 0x0000071C
		public static void GetAllSubsystemDescriptors(List<ISubsystemDescriptor> descriptors)
		{
			descriptors.Clear();
			foreach (ISubsystemDescriptorImpl subsystemDescriptorImpl in Internal_SubsystemDescriptors.s_IntegratedSubsystemDescriptors)
			{
				descriptors.Add(subsystemDescriptorImpl);
			}
			foreach (ISubsystemDescriptor subsystemDescriptor in Internal_SubsystemDescriptors.s_StandaloneSubsystemDescriptors)
			{
				descriptors.Add(subsystemDescriptor);
			}
		}

		// Token: 0x06000027 RID: 39
		[MethodImpl(4096)]
		internal static extern void ReportSingleSubsystemAnalytics(string id);

		// Token: 0x06000028 RID: 40 RVA: 0x000025C4 File Offset: 0x000007C4
		public static void GetSubsystemDescriptors<T>(List<T> descriptors) where T : ISubsystemDescriptor
		{
			descriptors.Clear();
			foreach (ISubsystemDescriptorImpl subsystemDescriptorImpl in Internal_SubsystemDescriptors.s_IntegratedSubsystemDescriptors)
			{
				bool flag = subsystemDescriptorImpl is T;
				if (flag)
				{
					descriptors.Add((T)((object)subsystemDescriptorImpl));
				}
			}
			foreach (ISubsystemDescriptor subsystemDescriptor in Internal_SubsystemDescriptors.s_StandaloneSubsystemDescriptors)
			{
				bool flag2 = subsystemDescriptor is T;
				if (flag2)
				{
					descriptors.Add((T)((object)subsystemDescriptor));
				}
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002694 File Offset: 0x00000894
		public static void GetInstances<T>(List<T> instances) where T : ISubsystem
		{
			instances.Clear();
			foreach (ISubsystem subsystem in Internal_SubsystemInstances.s_IntegratedSubsystemInstances)
			{
				bool flag = subsystem is T;
				if (flag)
				{
					instances.Add((T)((object)subsystem));
				}
			}
			foreach (ISubsystem subsystem2 in Internal_SubsystemInstances.s_StandaloneSubsystemInstances)
			{
				bool flag2 = subsystem2 is T;
				if (flag2)
				{
					instances.Add((T)((object)subsystem2));
				}
			}
		}

		// Token: 0x0600002A RID: 42
		[MethodImpl(4096)]
		internal static extern void DestroyInstance_Internal(IntPtr instancePtr);

		// Token: 0x0600002B RID: 43
		[MethodImpl(4096)]
		internal static extern void StaticConstructScriptingClassMap();

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600002C RID: 44 RVA: 0x00002764 File Offset: 0x00000964
		// (remove) Token: 0x0600002D RID: 45 RVA: 0x00002798 File Offset: 0x00000998
		[field: DebuggerBrowsable(0)]
		public static event Action reloadSubsytemsStarted;

		// Token: 0x0600002E RID: 46 RVA: 0x000027CC File Offset: 0x000009CC
		[RequiredByNativeCode]
		private static void Internal_ReloadSubsystemsStarted()
		{
			bool flag = SubsystemManager.reloadSubsytemsStarted != null;
			if (flag)
			{
				SubsystemManager.reloadSubsytemsStarted.Invoke();
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600002F RID: 47 RVA: 0x000027F4 File Offset: 0x000009F4
		// (remove) Token: 0x06000030 RID: 48 RVA: 0x00002828 File Offset: 0x00000A28
		[field: DebuggerBrowsable(0)]
		public static event Action reloadSubsytemsCompleted;

		// Token: 0x06000031 RID: 49 RVA: 0x0000285C File Offset: 0x00000A5C
		[RequiredByNativeCode]
		private static void Internal_ReloadSubsystemsCompleted()
		{
			bool flag = SubsystemManager.reloadSubsytemsCompleted != null;
			if (flag)
			{
				SubsystemManager.reloadSubsytemsCompleted.Invoke();
			}
		}
	}
}
