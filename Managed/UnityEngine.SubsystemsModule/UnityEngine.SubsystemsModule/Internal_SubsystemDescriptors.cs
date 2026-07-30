using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000009 RID: 9
	internal static class Internal_SubsystemDescriptors
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002400 File Offset: 0x00000600
		[RequiredByNativeCode]
		internal static bool Internal_AddDescriptor(SubsystemDescriptor descriptor)
		{
			foreach (ISubsystemDescriptor subsystemDescriptor in Internal_SubsystemDescriptors.s_StandaloneSubsystemDescriptors)
			{
				bool flag = subsystemDescriptor == descriptor;
				if (flag)
				{
					return false;
				}
			}
			Internal_SubsystemDescriptors.s_StandaloneSubsystemDescriptors.Add(descriptor);
			SubsystemManager.ReportSingleSubsystemAnalytics(descriptor.id);
			return true;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000247C File Offset: 0x0000067C
		[RequiredByNativeCode]
		internal static void Internal_InitializeManagedDescriptor(IntPtr ptr, ISubsystemDescriptorImpl desc)
		{
			desc.ptr = ptr;
			Internal_SubsystemDescriptors.s_IntegratedSubsystemDescriptors.Add(desc);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002494 File Offset: 0x00000694
		[RequiredByNativeCode]
		internal static void Internal_ClearManagedDescriptors()
		{
			foreach (ISubsystemDescriptorImpl subsystemDescriptorImpl in Internal_SubsystemDescriptors.s_IntegratedSubsystemDescriptors)
			{
				subsystemDescriptorImpl.ptr = IntPtr.Zero;
			}
			Internal_SubsystemDescriptors.s_IntegratedSubsystemDescriptors.Clear();
		}

		// Token: 0x06000022 RID: 34
		[MethodImpl(4096)]
		public static extern IntPtr Create(IntPtr descriptorPtr);

		// Token: 0x06000023 RID: 35
		[MethodImpl(4096)]
		public static extern string GetId(IntPtr descriptorPtr);

		// Token: 0x04000006 RID: 6
		internal static List<ISubsystemDescriptorImpl> s_IntegratedSubsystemDescriptors = new List<ISubsystemDescriptorImpl>();

		// Token: 0x04000007 RID: 7
		internal static List<ISubsystemDescriptor> s_StandaloneSubsystemDescriptors = new List<ISubsystemDescriptor>();
	}
}
