using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the browsable state of a property or method from within an editor.</summary>
	// Token: 0x0200026A RID: 618
	public enum EditorBrowsableState
	{
		/// <summary>The property or method is always browsable from within an editor.</summary>
		// Token: 0x040012D3 RID: 4819
		Always,
		/// <summary>The property or method is never browsable from within an editor.</summary>
		// Token: 0x040012D4 RID: 4820
		Never,
		/// <summary>The property or method is a feature that only advanced users should see. An editor can either show or hide such properties.</summary>
		// Token: 0x040012D5 RID: 4821
		Advanced
	}
}
