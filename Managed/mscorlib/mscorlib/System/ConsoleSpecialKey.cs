using System;

namespace System
{
	/// <summary>Specifies combinations of modifier and console keys that can interrupt the current process.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000140 RID: 320
	[Serializable]
	public enum ConsoleSpecialKey
	{
		/// <summary>The <see cref="F:System.ConsoleModifiers.Control" /> modifier key plus the <see cref="F:System.ConsoleKey.C" /> console key.</summary>
		// Token: 0x04000881 RID: 2177
		ControlC,
		/// <summary>The <see cref="F:System.ConsoleModifiers.Control" /> modifier key plus the BREAK console key.</summary>
		// Token: 0x04000882 RID: 2178
		ControlBreak
	}
}
