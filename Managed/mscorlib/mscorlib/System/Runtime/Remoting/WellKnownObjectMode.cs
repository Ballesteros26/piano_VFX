using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	/// <summary>Defines how well-known objects are activated.</summary>
	// Token: 0x02000768 RID: 1896
	[ComVisible(true)]
	[Serializable]
	public enum WellKnownObjectMode
	{
		/// <summary>Every incoming message is serviced by the same object instance.</summary>
		// Token: 0x040029E7 RID: 10727
		Singleton = 1,
		/// <summary>Every incoming message is serviced by a new object instance.</summary>
		// Token: 0x040029E8 RID: 10728
		SingleCall
	}
}
