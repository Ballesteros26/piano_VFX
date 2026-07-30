using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000004 RID: 4
	[UsedByNativeCode("SubsystemDescriptorBase")]
	[StructLayout(0)]
	public abstract class IntegratedSubsystemDescriptor : ISubsystemDescriptorImpl, ISubsystemDescriptor
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002050 File Offset: 0x00000250
		public string id
		{
			get
			{
				return Internal_SubsystemDescriptors.GetId(this.m_Ptr);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002070 File Offset: 0x00000270
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002088 File Offset: 0x00000288
		IntPtr ISubsystemDescriptorImpl.ptr
		{
			get
			{
				return this.m_Ptr;
			}
			set
			{
				this.m_Ptr = value;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002094 File Offset: 0x00000294
		ISubsystem ISubsystemDescriptor.Create()
		{
			return this.CreateImpl();
		}

		// Token: 0x06000009 RID: 9
		internal abstract ISubsystem CreateImpl();

		// Token: 0x04000001 RID: 1
		internal IntPtr m_Ptr;
	}
}
