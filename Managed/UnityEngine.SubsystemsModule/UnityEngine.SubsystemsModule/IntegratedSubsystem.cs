using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000C RID: 12
	[NativeType(Header = "Modules/Subsystems/Subsystem.h")]
	[UsedByNativeCode]
	[StructLayout(0)]
	public class IntegratedSubsystem : ISubsystem
	{
		// Token: 0x06000036 RID: 54
		[MethodImpl(4096)]
		internal extern void SetHandle(IntegratedSubsystem inst);

		// Token: 0x06000037 RID: 55
		[MethodImpl(4096)]
		public extern void Start();

		// Token: 0x06000038 RID: 56
		[MethodImpl(4096)]
		public extern void Stop();

		// Token: 0x06000039 RID: 57 RVA: 0x00002884 File Offset: 0x00000A84
		public void Destroy()
		{
			IntPtr ptr = this.m_Ptr;
			Internal_SubsystemInstances.Internal_RemoveInstanceByPtr(this.m_Ptr);
			SubsystemManager.DestroyInstance_Internal(ptr);
			this.m_Ptr = IntPtr.Zero;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000028B8 File Offset: 0x00000AB8
		public bool running
		{
			get
			{
				return this.valid && this.Internal_IsRunning();
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000028DC File Offset: 0x00000ADC
		internal bool valid
		{
			get
			{
				return this.m_Ptr != IntPtr.Zero;
			}
		}

		// Token: 0x0600003C RID: 60
		[MethodImpl(4096)]
		internal extern bool Internal_IsRunning();

		// Token: 0x0400000A RID: 10
		internal IntPtr m_Ptr;

		// Token: 0x0400000B RID: 11
		internal ISubsystemDescriptor m_subsystemDescriptor;
	}
}
