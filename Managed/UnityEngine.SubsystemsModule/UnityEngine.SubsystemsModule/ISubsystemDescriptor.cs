using System;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	public interface ISubsystemDescriptor
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		string id { get; }

		// Token: 0x06000002 RID: 2
		ISubsystem Create();
	}
}
