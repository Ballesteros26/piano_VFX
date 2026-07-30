using System;

namespace UnityEngine
{
	// Token: 0x02000005 RID: 5
	public abstract class SubsystemDescriptor : ISubsystemDescriptor
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020B5 File Offset: 0x000002B5
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000020BD File Offset: 0x000002BD
		public string id { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000020C6 File Offset: 0x000002C6
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000020CE File Offset: 0x000002CE
		public Type subsystemImplementationType { get; set; }

		// Token: 0x0600000F RID: 15 RVA: 0x000020D8 File Offset: 0x000002D8
		ISubsystem ISubsystemDescriptor.Create()
		{
			return this.CreateImpl();
		}

		// Token: 0x06000010 RID: 16
		internal abstract ISubsystem CreateImpl();
	}
}
