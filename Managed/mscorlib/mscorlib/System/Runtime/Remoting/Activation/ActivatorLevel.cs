using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Activation
{
	/// <summary>Defines the appropriate position for a <see cref="T:System.Activator" /> in the chain of activators.</summary>
	// Token: 0x020007BB RID: 1979
	[ComVisible(true)]
	[Serializable]
	public enum ActivatorLevel
	{
		/// <summary>Constructs a blank object and runs the constructor.</summary>
		// Token: 0x04002A6A RID: 10858
		Construction = 4,
		/// <summary>Finds or creates a suitable context.</summary>
		// Token: 0x04002A6B RID: 10859
		Context = 8,
		/// <summary>Finds or creates a <see cref="T:System.AppDomain" />.</summary>
		// Token: 0x04002A6C RID: 10860
		AppDomain = 12,
		/// <summary>Starts a process.</summary>
		// Token: 0x04002A6D RID: 10861
		Process = 16,
		/// <summary>Finds a suitable computer.</summary>
		// Token: 0x04002A6E RID: 10862
		Machine = 20
	}
}
