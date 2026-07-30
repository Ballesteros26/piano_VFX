using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Specifies the scheduling priority of a <see cref="T:System.Threading.Thread" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000497 RID: 1175
	[ComVisible(true)]
	[Serializable]
	public enum ThreadPriority
	{
		/// <summary>The <see cref="T:System.Threading.Thread" /> can be scheduled after threads with any other priority.</summary>
		// Token: 0x04001D19 RID: 7449
		Lowest,
		/// <summary>The <see cref="T:System.Threading.Thread" /> can be scheduled after threads with Normal priority and before those with Lowest priority.</summary>
		// Token: 0x04001D1A RID: 7450
		BelowNormal,
		/// <summary>The <see cref="T:System.Threading.Thread" /> can be scheduled after threads with AboveNormal priority and before those with BelowNormal priority. Threads have Normal priority by default.</summary>
		// Token: 0x04001D1B RID: 7451
		Normal,
		/// <summary>The <see cref="T:System.Threading.Thread" /> can be scheduled after threads with Highest priority and before those with Normal priority.</summary>
		// Token: 0x04001D1C RID: 7452
		AboveNormal,
		/// <summary>The <see cref="T:System.Threading.Thread" /> can be scheduled before threads with any other priority.</summary>
		// Token: 0x04001D1D RID: 7453
		Highest
	}
}
