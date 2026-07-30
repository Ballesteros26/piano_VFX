using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Specifies the apartment state of a <see cref="T:System.Threading.Thread" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200046A RID: 1130
	[ComVisible(true)]
	[Serializable]
	public enum ApartmentState
	{
		/// <summary>The <see cref="T:System.Threading.Thread" /> will create and enter a single-threaded apartment.</summary>
		// Token: 0x04001CA2 RID: 7330
		STA,
		/// <summary>The <see cref="T:System.Threading.Thread" /> will create and enter a multithreaded apartment.</summary>
		// Token: 0x04001CA3 RID: 7331
		MTA,
		/// <summary>The <see cref="P:System.Threading.Thread.ApartmentState" /> property has not been set.</summary>
		// Token: 0x04001CA4 RID: 7332
		Unknown
	}
}
