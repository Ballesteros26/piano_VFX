using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000809 RID: 2057
	internal interface IInternalMessage
	{
		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x0600524E RID: 21070
		// (set) Token: 0x0600524F RID: 21071
		Identity TargetIdentity { get; set; }

		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x06005250 RID: 21072
		// (set) Token: 0x06005251 RID: 21073
		string Uri { get; set; }

		// Token: 0x06005252 RID: 21074
		bool HasProperties();
	}
}
